using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;

public partial class AddLeaveEarlyTrans : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    TransDumpPro ProCs = new TransDumpPro();
    TransDumpSql SqlCs = new TransDumpSql();
    ////SendEmail sendEmail = new SendEmail();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM TransDumpInfoView ";
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
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                CtrlCs.FillGridEmpty(ref grdData, 50);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                txtTodayDate.Text = DTCs.FindCurrentDate();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillList()
    {
        try
        {
            CtrlCs.FillVirtualMachineList(ref ddlLocation, rfvLocation, false, "OUT"); 
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BtnStatus("1000");
        UIEnabled(false);
        CtrlCs.FillGridEmpty(ref grdData, 50);
        if (!Page.IsValid) { return; }
        
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Search()
    {
        try
        {
            if (txtEmpIDSearch.Text.Trim().Length > 1) { txtEmpID.Text = (txtEmpIDSearch.Text.Split('-'))[0].ToString(); }
            else { txtEmpID.Text = (txtEmpNameSearch.Text.Split('-'))[1].ToString(); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return; }
        
        try
        {
            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery + " WHERE EmpID = '0' ";

            if (!string.IsNullOrEmpty(txtEmpID.Text))
            {
                StringBuilder FQ = new StringBuilder();
                FQ.Append(MainQuery);
                FQ.Append(" WHERE DepID IN (" + pgCs.DepList + ") ");
                FQ.Append(" AND EmpID = @EmpID");
                FQ.Append(" AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101)");
                //FQ.Append(" AND UsrName IS NOT NULL");
                FQ.Append(" ORDER BY TrnTime ");

                sql = FQ.ToString();
                cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
            }

            //UIClear();
            BtnStatus("1000");
            UIEnabled(false);
            grdData.SelectedIndex = -1;
            cmd.CommandText = sql;
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

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
            grdData.Columns[2].Visible = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[3].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtEmpID.Enabled          = false;
        txtEmpName.Enabled        = false;
        calDate.SetEnabled(false);
        ddlType.Enabled           = false;
        tpOUTTime.Enabled       = pStatus;
        ddlLocation.Enabled       = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.EmpID    = txtEmpID.Text;
            ProCs.TrnDate  = calDate.getGDateDBFormat();
            ProCs.TrnTime  = tpOUTTime.getDateTime().ToString();
            ProCs.TrnType  = ddlType.SelectedValue;
            ProCs.MacID    = ddlLocation.SelectedValue;
            ProCs.UsrName  = pgCs.LoginID;
            ProCs.TrnAddBy = "OE";
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        txtEmpID.Text   = "";
        txtEmpName.Text = "";
        calDate.ClearDate();
        ddlType.ClearSelection();
        tpOUTTime.ClearTime();
        ddlLocation.ClearSelection();
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
        if (!Page.IsValid) { return; }
        
        //UIClear();
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

            //string TransTime = string.Format("{0:00}", tpOUTTime.getHours()) + ":" + string.Format("{0:00}", tpOUTTime.getMinutes()) + ":" + string.Format("{0:00}", tpOUTTime.getSeconds());
            ////sendEmail.SendAddTransMailToEmp(txtEmpID.Text, calDate.getGDateDefFormat(), TransTime, pgCs.Lang);  

            Search();
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
        //UIClear();
        tpOUTTime.ClearTime();
        ddlLocation.ClearSelection();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        //btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        //if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
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
                        e.Row.Cells[5].Visible = false; //To hide ID column in grid view
                        e.Row.Cells[6].Visible = false; //To hide ID column in grid view
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
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        //if (!DBCs.IsNullOrEmpty(DT))
        //{
        //    DataView dataView = new DataView(DT);

        //    if (ViewState["SortDirection"] == null)
        //    {
        //        ViewState["SortDirection"] = "ASC";
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }
        //    else
        //    {
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }

        //    dataView.Sort = e.SortExpression + " " + sortDirection;

        //    grdData.DataSource = dataView;
        //    grdData.DataBind();
        //}
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
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID) { }
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

    protected void tpOUTTime_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvtpOUTTime))
            {
                if (tpOUTTime.getIntTime()  < 0)
                {
                    CtrlCs.ValidMsg(this, ref cvtpOUTTime, false, General.Msg("Time is required", "الوقت مطلوب"));
                    e.IsValid = false;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FindEmp_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvFindEmp))
            {
                if (!string.IsNullOrEmpty(txtEmpIDSearch.Text.Trim()))
                {
                    UIClear();
                    txtEmpID.Text = (txtEmpIDSearch.Text.Split('-'))[0].ToString();
                    
                    if (!string.IsNullOrEmpty(txtEmpID.Text))
                    {
                        DataTable DT = DBCs.FetchData("SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 AND EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { txtEmpID.Text });
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            txtEmpName.Text = General.Msg(DT.Rows[0]["EmpNameEn"].ToString(), DT.Rows[0]["EmpNameAr"].ToString());
                            calDate.SetTodayDate();
                            e.IsValid = true;
                        }
                        else { e.IsValid = false; UIClear(); }
                    }
                }
                else { e.IsValid = true; }
            }

            else if (source.Equals(cvFindEmpName))
            {
                if (!string.IsNullOrEmpty(txtEmpNameSearch.Text.Trim()))
                {
                    UIClear();
                    txtEmpID.Text = (txtEmpNameSearch.Text.Split('-'))[1].ToString();
                    
                    if (!string.IsNullOrEmpty(txtEmpID.Text))
                    {
                        DataTable DT = DBCs.FetchData("SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 AND EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { txtEmpID.Text });
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            txtEmpName.Text = General.Msg(DT.Rows[0]["EmpNameEn"].ToString(), DT.Rows[0]["EmpNameAr"].ToString());
                            calDate.SetTodayDate();
                            e.IsValid = true;
                        }
                        else { e.IsValid = false; UIClear(); }
                    }
                }
                else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IsAdd_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtEmpID.Text))
            {
                StringBuilder SQ = new StringBuilder();
                SQ.Append(" SELECT COUNT(TrnDate) TrnCount,  COUNT(CASE WHEN (TrnAddBy = 'OE') THEN TrnDate ELSE NULL END) OECount");
                SQ.Append(" FROM TransDumpInfoView ");
                SQ.Append(" WHERE EmpID = @P1 ");
                SQ.Append(" AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101)");

                DataTable DT = DBCs.FetchData(SQ.ToString(), new string[] { txtEmpID.Text });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    int TrnCount = Convert.ToInt32(DT.Rows[0]["TrnCount"]);
                    int OECount  = Convert.ToInt32(DT.Rows[0]["OECount"]);

                    if (TrnCount == 0)
                    {
                        CtrlCs.ValidMsg(this, ref cvIsAdd, true, General.Msg("This employee does not have transaction this day, can not add an early leave transaction", "هذا الموظف لا يملك حركات هذا اليوم,لا يمكن إضافة حركة خروج مبكر"));
                        e.IsValid = false;
                    }
                    else if (OECount > 0)
                    {
                        CtrlCs.ValidMsg(this, ref cvIsAdd, true, General.Msg("An early leave transaction has been added to this employee, can not add an early leave transaction", "تمت إضافة حركة خروج مبكر لهذا الموظف ,لا يمكن إضافة حركة خروج مبكر"));
                        e.IsValid = false;
                    }
                }
                else { e.IsValid = false; }
            }
            else
            {
                CtrlCs.ValidMsg(this, ref cvIsAdd, true, General.Msg("You must search an employee first", "يجب البحث عن موظف أولا"));
                e.IsValid = false;
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

