using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;

public partial class ShiftSwapUser : BasePage
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
    public bool IsWorkDays(string pDaysType, string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian");
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian");
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                DataTable DT = ReqSqlCs.FetchWorkTime(Date, pEmpID, true);
                if (pDaysType == "Work") { if (DBCs.IsNullOrEmpty(DT)) { return false; } }
                if (pDaysType == "Off")  { if (!DBCs.IsNullOrEmpty(DT)) { return false; } }
            }
            return true;
        }
        catch (Exception e1)
        {
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string IsWorkTime(bool pDaysType, string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian");
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian");
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);
            string wktID = "-1";

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                DataTable DT = ReqSqlCs.FetchWorkTime(Date, pEmpID, pDaysType);
                if (!DBCs.IsNullOrEmpty(DT)) 
                {
                    if (i == 0) { wktID = DT.Rows[0]["WktID"].ToString(); }
                    else
                    {
                        if (wktID != DT.Rows[0]["WktID"].ToString()) { return "-1"; }
                    }
                }
            }
            return wktID;
        }
        catch (Exception e1)
        {
            return "-1";
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
        txtEmpID.Enabled = pStatus;
        ddlType.Enabled = pStatus;
        calStartDate1.SetEnabled(pStatus);
        calEndDate1.SetEnabled(pStatus);
        txtEmpID2.Enabled = pStatus;
        calStartDate2.SetEnabled(pStatus);
        calEndDate2.SetEnabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.SwpID = txtID.Text; }
   
        if (ddlType.SelectedIndex > 0) { if (ddlType.SelectedValue == "Work") { ProCs.SwpType = "1"; } else { ProCs.SwpType = "2"; } }
        ProCs.EmpID         = txtEmpID.Text;
        ProCs.SwpStartDate  = calStartDate1.getGDateDBFormat();
        ProCs.SwpEndDate    = calEndDate1.getGDateDBFormat();
        ProCs.EmpID2        = txtEmpID2.Text;
        ProCs.SwpStartDate2 = calStartDate2.getGDateDBFormat();
        ProCs.SwpEndDate2   = calEndDate2.getGDateDBFormat();
        ProCs.WktID         = txtWorktime.Text;

        ProCs.TransactionBy = pgCs.LoginID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtID.Text = "";
        txtEmpID.Text = "";
        ddlType.SelectedIndex = -1;
        calStartDate1.ClearDate();
        calEndDate1.ClearDate();
        txtEmpID2.Text = "";
        calStartDate2.ClearDate();
        calEndDate2.ClearDate();
        txtWorktime.Text = "";
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

            txtID.Text    = DRs[0]["SwpID"].ToString();
            txtEmpID.Text = DRs[0]["EmpID"].ToString();
            ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(DRs[0]["SwpType"].ToString()));
            calStartDate1.SetGDate(DRs[0]["SwpStartDate"], pgCs.DateFormat);
            calEndDate1.SetGDate(DRs[0]["SwpStartDate"], pgCs.DateFormat);

            txtEmpID2.Text = DRs[0]["EmpID2"].ToString();
            calStartDate2.SetGDate(DRs[0]["SwpStartDate2"], pgCs.DateFormat);
            calEndDate2.SetGDate(DRs[0]["SwpStartDate2"], pgCs.DateFormat);
            txtWorktime.Text = DRs[0]["WktID"].ToString();
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
            if (source.Equals(cvEmployee1) && !String.IsNullOrEmpty(txtEmpID.Text))
            {
                CtrlCs.ValidMsg(this, ref cvEmployee1, true, General.Msg("No Employee with ID", "لا يوجد موظف بهذا الرقم")); 
                if (!GenCs.isEmpID(txtEmpID.Text)) { e.IsValid = false; }
            }
            else if (source.Equals(cvEmployee2) && !String.IsNullOrEmpty(txtEmpID2.Text) && !String.IsNullOrEmpty(txtEmpID.Text))
            {
                if (txtEmpID2.Text == txtEmpID.Text)
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("Can not swap between the times of your worktime", "لا يمكن إجراء مبادلة بين أوقات عملك")); 
                    e.IsValid = false;
                }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("No Employee with ID", "لا يوجد موظف بهذا الرقم"));
                    if (!GenCs.isEmpID(txtEmpID2.Text)) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DaysCount_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDaysCount))
        {
            if (!String.IsNullOrEmpty(calStartDate1.getGDate()) && !String.IsNullOrEmpty(calEndDate1.getGDate())
             && !String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate()))
            {
                DateTime StartDate1 = DTCs.ConvertToDatetime(calStartDate1.getGDate(), "Gregorian"); 
                DateTime EndDate1   = DTCs.ConvertToDatetime(calEndDate1.getGDate(), "Gregorian"); 
                DateTime StartDate2 = DTCs.ConvertToDatetime(calStartDate2.getGDate(), "Gregorian"); 
                DateTime EndDate2   = DTCs.ConvertToDatetime(calEndDate2.getGDate(), "Gregorian"); 
                
                int Days1 = Convert.ToInt32((EndDate1 - StartDate1).TotalDays + 1);
                int Days2 = Convert.ToInt32((EndDate2 - StartDate2).TotalDays + 1);

                if (Days1 != Days2)
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The number of days required for the swap is equal", "عدد الأيام المطلوبة للتبديل غير متساوية"));
                    e.IsValid = false;
                    return;
                }
            }
            e.IsValid = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays1))
        {
            if (!String.IsNullOrEmpty(calStartDate1.getGDate()) && !String.IsNullOrEmpty(calEndDate1.getGDate()) && ddlType.SelectedIndex > 0 && !String.IsNullOrEmpty(txtEmpID.Text))
            {
                if (ddlType.SelectedValue == "Work")
                {
                    bool WorkDays = IsWorkDays("Work", calStartDate1.getGDate(), calEndDate1.getGDate(), txtEmpID.Text);
                    if (!WorkDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the specific worktime in the given period", "لا يوجد لدى الموظف الأول عمل محدد في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
                else
                {
                    bool OffDays = IsWorkDays("Off", calStartDate1.getGDate(), calEndDate1.getGDate(), txtEmpID.Text);
                    if (!OffDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the vacation in the given period", "لا يوجد لدى الموظف الأول إجازة في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
            }
        }
        else if (source.Equals(cvDays2))
        {
            if (!String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate()) && ddlType.SelectedIndex > 0 && !String.IsNullOrEmpty(txtEmpID2.Text))
            {
                if (ddlType.SelectedValue == "Work")
                {
                    bool OffDays = IsWorkDays("Off", calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (!OffDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني إجازة في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
                else
                {
                    bool WorkDays = IsWorkDays("Work", calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (!WorkDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني عمل في الفترة المحددة"));
                        cvDays2.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='The employee has no other worktime in the specified period' /&gt;");
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
            }
        }
        ///////////////////////////////////////////
        else if (source.Equals(cvWorkTime))
        {
            if (!String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate())
                && ddlType.SelectedIndex > 0 && !String.IsNullOrEmpty(txtEmpID.Text) && !String.IsNullOrEmpty(txtEmpID2.Text))
            {
                if (ddlType.SelectedValue == "Work")
                {
                    string WorkTime1 = IsWorkTime(true, calStartDate1.getGDate(), calEndDate1.getGDate(), txtEmpID.Text);
                    string WorkTime2 = IsWorkTime(false, calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (WorkTime1 == "-1" || WorkTime2 == "-1" || (WorkTime1 !=WorkTime2 ) )
                    {
                        CtrlCs.ValidMsg(this, ref cvWorkTime, true, General.Msg("Working time in the period is the not same, the work can not be  swaped specified period", "وقت العمل في الفترة غير متشابه لا يمكن تبديل عمل الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        txtWorktime.Text = WorkTime1;
                        e.IsValid = true;
                    }
                }
                else
                {
                    string WorkTime1 = IsWorkTime(false, calStartDate1.getGDate(), calEndDate1.getGDate(), txtEmpID.Text);
                    string WorkTime2 = IsWorkTime(true, calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (WorkTime1 == "-1" || WorkTime2 == "-1" || (WorkTime1 != WorkTime2))
                    {
                        CtrlCs.ValidMsg(this, ref cvWorkTime, true, General.Msg("Working time in the period is the not same, the work can not be swaped" + " <br />" + "specified period", "وقت العمل في الفترة غير متشابه لا يمكن تبديل عمل الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        txtWorktime.Text = WorkTime1;
                        e.IsValid = true;
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