using System;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class EmployeeSwapShift : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    SwapPro ProCs = new SwapPro();
    SwapSql SqlCs = new SwapSql();
    
    EmpRequestSql ReqSqlCs = new EmpRequestSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";

    string MainQuery = " SELECT * FROM EmployeeSwapInfoView ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();
                if ( LicDf.FetchLic("SS") == "0" ) { Server.Transfer("login.aspx"); }
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsWorkDays(string iType, string iStartDate, string ipEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime Date = DTCs.ConvertToDatetime(iStartDate, "Gregorian");
            
            DataTable DT = ReqSqlCs.FetchWorkTime(Date, ipEmpID, true);
            if (iType == "Work") { if (DBCs.IsNullOrEmpty(DT))  { return false; } }
            if (iType == "Off")  { if (!DBCs.IsNullOrEmpty(DT)) { return false; } }
           
            return true;
        }
        catch (Exception e1)
        {
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UILang()
    {
        if (pgCs.LangEn)
        {

        }
        else
        {
            grdData.Columns[3].Visible = false;
            grdData.Columns[8].Visible = false;
        }

        if (pgCs.LangAr)
        {

        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[7].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        txtEmpID1.Enabled = pStatus;
        ddlType.Enabled = pStatus;
        calStartDate1.SetEnabled(pStatus);
        txtEmpID2.Enabled = pStatus;
        calStartDate2.SetEnabled(pStatus);
        txtDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.SwpID = txtID.Text; }
   
        if (ddlType.SelectedIndex > 0) { ProCs.SwpType = ddlType.SelectedValue; }
        ProCs.SwpEmpID1     = txtEmpID1.Text;
        ProCs.SwpStartDate1 = calStartDate1.getGDateDBFormat();
        ProCs.SwpEmpID2     = txtEmpID2.Text;
        ProCs.SwpStartDate2 = calStartDate2.getGDateDBFormat();
        ProCs.SwpDesc       = txtDesc.Text;
        ProCs.SwpAddBy      = "USR";

        ProCs.TransactionBy = pgCs.LoginID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtID.Text = "";
        txtEmpID1.Text = "";
        ddlType.SelectedIndex = -1;
        calStartDate1.ClearDate();
        txtEmpID2.Text = "";
        calStartDate2.ClearDate();
        txtDesc.Text = "";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        UIClear();
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (commandName == "ADD") { SqlCs.Insert(ProCs); }

            btnCancel_Click(null,null);
            FillGrid(new SqlCommand(MainQuery));
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        DropDownList _ddlPager = CtrlCs.PagerList(grdData);
                        _ddlPager.SelectedIndexChanged += new EventHandler(ddlPager_SelectedIndexChanged);
                        Table pagerTable = e.Row.Cells[0].Controls[0] as Table;
                        pagerTable.Rows[0].Cells.Add(CtrlCs.PagerCell(_ddlPager));
                        break;
                    }
                 default:
                    {
                        e.Row.Cells[1].Visible = false; //To hide ID column in grid view
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdData.PageIndex = 0;
        FillGrid(new SqlCommand(MainQuery));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        Hashtable ht = (Hashtable)ViewState["ht"];
                        if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                        delBtn.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string ID = e.CommandArgument.ToString();
                    //DataTable DT = DBCs.FetchData("SELECT * FROM Department WHERE ISNULL(DepDeleted,0) = 0 AND BrcID = @P1 ", new string[] { ID });
                    //if (!DBCs.IsNullOrEmpty(DT))
                    //{
                    //    CtrlCs.ShowDelMsg(this, false);
                    //    return;
                    //}
                    
                    SqlCs.Delete(ID, pgCs.LoginID);

                    UIClear();
                    BtnStatus("1000");
                    FillGrid(new SqlCommand(MainQuery));
                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataView dataView = new DataView(DT);

            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = "ASC";
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }
            else
            {
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }

            dataView.Sort = e.SortExpression + " " + sortDirection;

            grdData.DataSource = dataView;
            grdData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ConvertSortDirectionToSql(string sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;

            case "DESC":
                newSortDirection = "ASC";
                break;
        }

        return newSortDirection;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        FillGrid(new SqlCommand(MainQuery));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(false);
            BtnStatus("1000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
                BtnStatus("1100");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("SwpID =" + pID + "");

            txtID.Text = DRs[0]["SwpID"].ToString();
            ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(DRs[0]["SwpType"].ToString()));
            txtEmpID1.Text = DRs[0]["SwpEmpID1"].ToString();           
            calStartDate1.SetGDate(DRs[0]["SwpStartDate1"], pgCs.DateFormat);
            txtEmpID2.Text = DRs[0]["SwpEmpID2"].ToString();
            calStartDate2.SetGDate(DRs[0]["SwpStartDate2"], pgCs.DateFormat);
            txtDesc.Text = DRs[0]["SwpDesc"].ToString();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(SqlCommand cmd)
    {
        DataTable GDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            grdData.DataSource = (DataTable)GDT;
            ViewState["grdDataDT"] = (DataTable)GDT;
            grdData.DataBind();
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void Employee_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string EmpID1 = txtEmpID1.Text;
            string EmpID2 = txtEmpID2.Text;

            if (source.Equals(cvEmployee1))
            {
                if (string.IsNullOrEmpty(EmpID1))
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee1, false, General.Msg("Employee ID is required", "رقم الموظف إجباري"));
                    e.IsValid = false;
                }
                else if (!string.IsNullOrEmpty(EmpID2))
                { 
                    if (EmpID2 == EmpID1)
                    {
                        CtrlCs.ValidMsg(this, ref cvEmployee1, true, General.Msg("Can not make a swap between self-employee", "لا يمكن إجراء مبادلة بين الموظف نفسه"));
                        e.IsValid = false;
                    }
                }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee1, true, General.Msg("Employee ID does not exist", "رقم الموظف غير موجود"));
                    DataTable DT = DBCs.FetchData(" SELECT EmpID, CatID FROM spActiveEmployeeView WHERE EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { EmpID1 });
                    if (DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                    else
                    {
                        if (pgCs.Version == "SANS")
                        {
                            DataTable DT2 = DBCs.FetchData(" SELECT EmpID, CatID FROM spActiveEmployeeView WHERE EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { EmpID2 });
                            if (!DBCs.IsNullOrEmpty(DT2))
                            {
                                CtrlCs.ValidMsg(this, ref cvEmployee1, true, General.Msg("Category does not match the specified employee", "التصنيف غير متطابق مع الموظف المحدد"));
                                if (string.IsNullOrEmpty(Convert.ToString(DT.Rows[0]["CatID"])) || string.IsNullOrEmpty(Convert.ToString(DT2.Rows[0]["CatID"]))) { e.IsValid = false; }
                                else if (DT.Rows[0]["CatID"].ToString() != DT2.Rows[0]["CatID"].ToString()) { e.IsValid = false; }
                            }
                        }
                    }         
                }
            }

            if (source.Equals(cvEmployee2))
            {
                if (string.IsNullOrEmpty(EmpID2))
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee2, false, General.Msg("Employee ID is required", "رقم الموظف إجباري"));
                    e.IsValid = false;
                }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("Employee ID does not exist", "رقم الموظف غير موجود"));
                    DataTable DT = DBCs.FetchData(" SELECT EmpID, CatID FROM spActiveEmployeeView WHERE EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { EmpID2 });
                    if (DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; } 
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days1_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays1))
        {
            if (ddlType.SelectedIndex > 0)
                if (ddlType.SelectedValue == "1" || ddlType.SelectedValue == "2") //"Work_work" || "Work_Off"
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the specific worktime in the given period", "لا يوجد لدى الموظف الأول عمل محدد في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID1.Text) && !string.IsNullOrEmpty(calStartDate1.getGDate()))
                    {
                        bool WorkDays = IsWorkDays("Work", calStartDate1.getGDate(), txtEmpID1.Text);
                        if (!WorkDays) { e.IsValid = false; }
                    }
                }
                
                if (ddlType.SelectedValue == "3") //Off_work
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the vacation in the given period", "لا يوجد لدى الموظف الأول إجازة في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID1.Text) && !string.IsNullOrEmpty(calStartDate1.getGDate()))
                    {
                        bool OffDays = IsWorkDays("Off", calStartDate1.getGDate(), txtEmpID1.Text);
                        if (!OffDays) { e.IsValid = false; }
                    }
                }               
            }
        }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days2_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays2))
        {
            if (ddlType.SelectedIndex > 0)
            {
                if (ddlType.SelectedValue == "1" || ddlType.SelectedValue == "3") //Work_work || Off_work
                {
                    CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني عمل في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID2.Text) && !string.IsNullOrEmpty(calStartDate2.getGDate()))
                    {
                        bool WorkDays = IsWorkDays("Work", calStartDate2.getGDate(), txtEmpID2.Text);
                        if (!WorkDays) { e.IsValid = false; }
                    }

                }
                
                if (ddlType.SelectedValue == "2") // Work_Off
                {
                    CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني إجازة في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID2.Text) && !string.IsNullOrEmpty(calStartDate2.getGDate()))
                    {
                        bool OffDays = IsWorkDays("Off", calStartDate2.getGDate(), txtEmpID2.Text);
                        if (!OffDays) { e.IsValid = false; }
                    }
                }               
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}