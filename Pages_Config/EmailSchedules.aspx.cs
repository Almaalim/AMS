using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;
using System.Collections;

public partial class EmailSchedules : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    SchedulePro ProCs = new SchedulePro();
    ScheduleSql SqlCs = new ScheduleSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    
    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = " SELECT * FROM Schedule WHERE ISNULL(SchDeleted,0) = 0 ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.Animation(ref AnimationExtenderShow1, ref AnimationExtenderClose1, ref lnkShow1, "1");
            CtrlCs.Animation(ref AnimationExtenderShow2, ref AnimationExtenderClose2, ref lnkShow2, "2");
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                divHour.Visible = true;
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
            DataTable RDT = DBCs.FetchData(new SqlCommand("SELECT RepID,RepNameEn,RepNameAr FROM Report WHERE RepForSchedule = 1 AND RepVisible = 1"));
            if (!DBCs.IsNullOrEmpty(RDT)) 
            { 
                CtrlCs.PopulateDDL(ddlReportGroup, RDT, General.Msg("RepNameEn","RepNameAr"), "RepID", General.Msg("-Select Report-","-اختر تقرير-")); 
                rvReportGroup.InitialValue = ddlReportGroup.Items[0].Text;
            }

            DataTable UDT = DBCs.FetchData(new SqlCommand(" SELECT UsrName FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 AND UsrStatus = 'True' "));
            if (!DBCs.IsNullOrEmpty(UDT)) 
            { 
                CtrlCs.PopulateLBX(lbxUsers, UDT, "UsrName", "UsrName", General.Msg("-Select Manager-","-اختر المدير-"));
            }
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
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0)
        {
            UIClear();
            sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1 ";
            cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        cmd.CommandText = sql;
        FillGrid(cmd);
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
           spnNameEn.Visible = true;
        }
        else
        {
            grdData.Columns[3].Visible = false;
            ddlFilter.Items.FindByValue("SchNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            spnNameAr.Visible = true;
        }
        else
        {
            grdData.Columns[2].Visible = false;
            ddlFilter.Items.FindByValue("SchNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        DataItemEnabled(this.Page, pStatus);
        txtID.Enabled = txtID.Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void DataItemEnabled(Control parent, bool Status)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                DataItemEnabled(_ChildControl, Status);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    ((TextBox)_ChildControl).Enabled = Status;
                }
                else
                    if (_ChildControl is CheckBox)
                    {
                        ((CheckBox)_ChildControl).Enabled = Status;
                    }
                    else
                        if (_ChildControl is DropDownList)
                        {
                            ((DropDownList)_ChildControl).Enabled = Status;
                        }
                        else
                            if (_ChildControl is ListBox)
                            {
                                ((ListBox)_ChildControl).Enabled = Status;
                            }

            }
        }

        ddlFilter.Enabled = true;
        txtFilter.Enabled = true;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.SchID = txtID.Text; }
            ProCs.SchNameAr = txtNameAr.Text.Trim();
            ProCs.SchNameEn = txtNameEn.Text.Trim();

            ProCs.SchIsActive = chkStatus.Checked;

            ProCs.SchType = ddlScheduleType.SelectedValue;
            ProCs.SchDescription = txtDescription.Text.Trim();

            if (!string.IsNullOrEmpty(calStartDate.getGDate())) { ProCs.SchStartDate = calStartDate.getGDateDBFormat(); }
            if (!string.IsNullOrEmpty(calEndDate.getGDate()))   { ProCs.SchEndDate   = calEndDate.getGDateDBFormat(); }
            
            ProCs.SchStartHour = tpStartTime.getHours().ToString(); 
            ProCs.SchStartMin = tpStartTime.getMinutes().ToString();

            if (!string.IsNullOrEmpty(txtEveryHour.Text)) { ProCs.SchEveryHour = txtEveryHour.Text; }
            if (ddlEveryMinute.SelectedIndex > 0) { ProCs.SchEveryMinute = ddlEveryMinute.SelectedValue; }
            if (ddlWeekOfMonth.SelectedIndex > 0) { ProCs.SchWeekOfMonth = ddlWeekOfMonth.SelectedValue; }
            
            if (!string.IsNullOrEmpty(txtRepeatDays.Text)) { ProCs.SchRepeatDays = txtRepeatDays.Text; }
            if (!string.IsNullOrEmpty(txtRepeatWeek.Text)) { ProCs.SchRepeatWeeks = txtRepeatWeek.Text; }
            if (!string.IsNullOrEmpty(txtMessage.Text))    { ProCs.SchEmailBodyContent = txtMessage.Text; }
            if (!string.IsNullOrEmpty(txtSubject.Text))    { ProCs.SchEmailSubject = txtSubject.Text; }
            if (ddlReportGroup.SelectedIndex > 0)          { ProCs.ReportID = ddlReportGroup.SelectedValue; }

            string SelectedUsers = string.Empty;
            if (lbxUsers.SelectedIndex > -1)
                if (lbxUsers.Items.Count > 0)
                {
                    for (int i = 0; i < lbxUsers.Items.Count; i++)
                    {
                        if (lbxUsers.Items[i].Selected)
                        {
                            SelectedUsers += lbxUsers.Items[i].Text.ToString().Trim();
                            SelectedUsers += ",";
                        }
                    }
                }
            ProCs.SchUsers = SelectedUsers;

            if (ddlReportFormat.SelectedIndex > 0) { ProCs.SchReportFormat = ddlReportFormat.SelectedValue; }

            StringBuilder WQ = new StringBuilder();
            if (ddlScheduleType.SelectedValue == "Hourly") { }
            else if (ddlScheduleType.SelectedValue == "Weekly" || ddlScheduleType.SelectedValue == "WeeklySkip" || ddlScheduleType.SelectedValue == "WeekNumber")
            {
                if (chkSun.Checked) { WQ.Append("1,"); }
                if (chkMon.Checked) { WQ.Append("2,"); }
                if (chkTue.Checked) { WQ.Append("3,"); }
                if (chkWed.Checked) { WQ.Append("4,"); }
                if (chkThu.Checked) { WQ.Append("5,"); }
                if (chkFri.Checked) { WQ.Append("6,"); }
                if (chkSat.Checked) { WQ.Append("7,"); }
            }
        
            StringBuilder MQ = new StringBuilder();
            if (ddlScheduleType.SelectedItem.Value == "WeekNumber" || ddlScheduleType.SelectedValue == "Calendar" )
            {
                if (chkJan.Checked) { MQ.Append("1,"); }
                if (chkFeb.Checked) { MQ.Append("2,"); }
                if (chkMar.Checked) { MQ.Append("3,"); }
                if (chkApr.Checked) { MQ.Append("4,"); }
                if (chkMay.Checked) { MQ.Append("5,"); }
                if (chkJun.Checked) { MQ.Append("6,"); }
                if (chkJul.Checked) { MQ.Append("7,"); }
                if (chkAug.Checked) { MQ.Append("8,"); }
                if (chkSep.Checked) { MQ.Append("9,"); }
                if (chkOct.Checked) { MQ.Append("10,"); }
                if (chkNov.Checked) { MQ.Append("11,"); }
                if (chkDec.Checked) { MQ.Append("12,"); }
            }

            StringBuilder DQ = new StringBuilder();
            if (ddlScheduleType.SelectedValue == "Calendar")
            {
                if (chkDay1.Checked)  { DQ.Append("1,"); }
                if (chkDay2.Checked)  { DQ.Append("2,"); }
                if (chkDay3.Checked)  { DQ.Append("3,"); }
                if (chkDay4.Checked)  { DQ.Append("4,"); }
                if (chkDay5.Checked)  { DQ.Append("5,"); }
                if (chkDay6.Checked)  { DQ.Append("6,"); }
                if (chkDay7.Checked)  { DQ.Append("7,"); }
                if (chkDay8.Checked)  { DQ.Append("8,"); }
                if (chkDay9.Checked)  { DQ.Append("9,"); }
                if (chkDay10.Checked) { DQ.Append("10,"); }
                if (chkDay11.Checked) { DQ.Append("11,"); }
                if (chkDay12.Checked) { DQ.Append("12,"); }
                if (chkDay13.Checked) { DQ.Append("13,"); }
                if (chkDay14.Checked) { DQ.Append("14,"); }
                if (chkDay15.Checked) { DQ.Append("15,"); }
                if (chkDay16.Checked) { DQ.Append("16,"); }
                if (chkDay17.Checked) { DQ.Append("17,"); }
                if (chkDay18.Checked) { DQ.Append("18,"); }
                if (chkDay19.Checked) { DQ.Append("19,"); }
                if (chkDay20.Checked) { DQ.Append("20,"); }
                if (chkDay21.Checked) { DQ.Append("21,"); }
                if (chkDay22.Checked) { DQ.Append("22,"); }
                if (chkDay23.Checked) { DQ.Append("23,"); }
                if (chkDay24.Checked) { DQ.Append("24,"); }
                if (chkDay25.Checked) { DQ.Append("25,"); }
                if (chkDay26.Checked) { DQ.Append("26,"); }
                if (chkDay27.Checked) { DQ.Append("27,"); }
                if (chkDay28.Checked) { DQ.Append("28,"); }
                if (chkDay29.Checked) { DQ.Append("29,"); }
                if (chkDay30.Checked) { DQ.Append("30,"); }
                if (chkDay31.Checked) { DQ.Append("31,"); }
                if (chkDayLast.Checked) { DQ.Append("Last,"); }
            }

            ProCs.SchDays   = DQ.ToString();
            ProCs.SchMonths = MQ.ToString();
            ProCs.SchWeeks  = WQ.ToString();

           ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            ViewState["CommandName"] = ""; 
            txtID.Text = "";
            
            lbxUsers.ClearSelection();

            ClearControls(this.Page);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ClearControls(Control parent)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                ClearControls(_ChildControl);
            }
            else
            {
                if (_ChildControl is TextBox)
                {
                    ((TextBox)_ChildControl).Text = string.Empty;
                }
                else
                    if (_ChildControl is CheckBox)
                    {
                        ((CheckBox)_ChildControl).Checked = false;
                    }
                    else
                        if (_ChildControl is DropDownList)
                        {
                            ((DropDownList)_ChildControl).ClearSelection();
                        }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlScheduleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UIShow(ddlScheduleType.SelectedValue,0);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIShow(string ScheduleType, int schID)
    {
        divHour.Visible = false;
        divDaysOfWeek.Visible = false;
        divDayRepeat.Visible = false;
        divWeekRepeat.Visible = false;
        divMonths.Visible = false;
        divWeekOfMonth.Visible = false;
        divCalendar.Visible = false;

        switch (ScheduleType)
        {
            case "Hourly":
                divHour.Visible = true;
                break;
            case "Weekly":
                divDaysOfWeek.Visible = true;
                if (schID != 0) { PopulateWeek(schID); }
                break;
            case "Daily":
                divDayRepeat.Visible = true;
                break;
            case "WeeklySkip":
                divWeekRepeat.Visible = true;
                divDaysOfWeek.Visible = true;
                if (schID != 0) { PopulateWeek(schID); }
                break;
            case "WeekNumber":
                divMonths.Visible      = true;
                divWeekOfMonth.Visible = true;
                divDaysOfWeek.Visible  = true;
                if (schID != 0) { PopulateMonth(schID); PopulateWeek(schID); }
                break;
            case "Calendar":
                divMonths.Visible = true;
                divCalendar.Visible = true;
                if (schID != 0) { PopulateDays(schID); PopulateMonth(schID); }
                break;
            
            default:
                break;
        }
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
        try
        {
            UIClear();
            UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "ADD";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "EDIT";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if      (ViewState["CommandName"].ToString() == "ADD")  { SqlCs.Insert(ProCs); } 
            else if (ViewState["CommandName"].ToString() == "EDIT") { SqlCs.Update(ProCs); }

            btnFilter_Click(null,null);
            
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
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
                        //e.Row.Cells[1].Visible = false; //To hide ID column in grid view
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
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton _btnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        _btnDelete.Enabled = pgCs.getPermission(ViewState["ht"], "Delete");
                        _btnDelete.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        
        GridView gridView = (GridView)sender;
        int cellIndex = -1;
        foreach (DataControlField field in gridView.Columns)
        {
            if (ViewState["SortExpression"] != null)
            {
                if (field.SortExpression == Convert.ToString(ViewState["SortExpression"]))
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                    break;
                }
            }
        }

        if (cellIndex > -1)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //this is a header row,set the sort style
                e.Row.Cells[cellIndex].CssClass += (sortDirection == "ASC" ? " sortasc" : " sortdesc");
            }
        }
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

                    SqlCs.Delete(ID, pgCs.LoginID);

                    btnFilter_Click(null,null);
                    
                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
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
        btnFilter_Click(null,null);
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
                PopulateUI(grdData.SelectedRow.Cells[0].Text);
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
            DataRow[] DRs = DT.Select("SchID =" + pID + "");

            txtID.Text          = DRs[0]["SchID"].ToString();
            txtNameEn.Text      = DRs[0]["SchNameEn"].ToString();
            txtNameAr.Text      = DRs[0]["SchNameAr"].ToString();
            txtDescription.Text = DRs[0]["SchDescription"].ToString();
            
            tpStartTime.SetTime(Convert.ToInt32(DRs[0]["SchStartHour"]), Convert.ToInt32(DRs[0]["SchStartMin"]));
            if (DRs[0]["SchIsActive"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["SchIsActive"]); }

            ddlScheduleType.SelectedIndex = ddlScheduleType.Items.IndexOf(ddlScheduleType.Items.FindByValue(DRs[0]["SchType"].ToString()));
            txtEveryHour.Text = DRs[0]["SchEveryHour"].ToString();

            ddlEveryMinute.SelectedIndex = ddlEveryMinute.Items.IndexOf(ddlEveryMinute.Items.FindByValue(DRs[0]["SchEveryMinute"].ToString()));
            ddlWeekOfMonth.SelectedIndex = ddlWeekOfMonth.Items.IndexOf(ddlWeekOfMonth.Items.FindByValue(DRs[0]["SchWeekOfMonth"].ToString()));

            txtRepeatDays.Text = DRs[0]["SchRepeatDays"].ToString();
            txtRepeatWeek.Text = DRs[0]["SchRepeatWeeks"].ToString();

            calStartDate.SetGDate(DRs[0]["SchStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["SchEndDate"], pgCs.DateFormat);
       
            txtSubject.Text = DRs[0]["SchEmailSubject"].ToString();
            txtMessage.Text = DRs[0]["SchEmailBodyContent"].ToString();
            ddlReportGroup.SelectedIndex = ddlReportGroup.Items.IndexOf(ddlReportGroup.Items.FindByValue(DRs[0]["ReportID"].ToString()));
        
            string strSchUsers = DRs[0]["SchUsers"].ToString();
            string[] SchUsers = strSchUsers.Split(',');

            for (int i = 0; i < SchUsers.Length; i++)
            {
                for (int j = 0; j < lbxUsers.Items.Count; j++) { if (lbxUsers.Items[j].Value.ToString().Trim() == SchUsers[i]) { lbxUsers.Items[j].Selected = true; } }
            }

            ddlReportFormat.SelectedIndex = ddlReportFormat.Items.IndexOf(ddlReportFormat.Items.FindByValue(DRs[0]["SchReportFormat"].ToString()));

            UIShow(DRs[0]["SchType"].ToString(),Convert.ToInt32(pID));
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void PopulateWeek(int SchID)
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM ScheduleWeek WHERE SchID = @P1 ", new string[] { SchID.ToString() });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["ShwWeekDayId"].ToString())
                {
                    case "1": chkSun.Checked = true; break;
                    case "2": chkMon.Checked = true; break;
                    case "3": chkTue.Checked = true; break;
                    case "4": chkWed.Checked = true; break;
                    case "5": chkThu.Checked = true; break;
                    case "6": chkFri.Checked = true; break;
                    case "7": chkSat.Checked = true; break;
                    default: break;
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void PopulateMonth(int SchID)
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM ScheduleMonth WHERE SchID = @P1 ", new string[] { SchID.ToString() });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["ShmMonthId"].ToString())
                {
                    case "1":  chkJan.Checked = true; break;
                    case "2":  chkFeb.Checked = true; break;
                    case "3":  chkMar.Checked = true; break;
                    case "4":  chkApr.Checked = true; break;
                    case "5":  chkMay.Checked = true; break;
                    case "6":  chkJun.Checked = true; break;
                    case "7":  chkJul.Checked = true; break;
                    case "8":  chkAug.Checked = true; break;
                    case "9":  chkSep.Checked = true; break;
                    case "10": chkOct.Checked = true; break;
                    case "11": chkNov.Checked = true; break;
                    case "12": chkDec.Checked = true; break;
                    default: break;
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void PopulateDays(int SchID)
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM ScheduleDay WHERE SchID = @P1 ", new string[] { SchID.ToString() });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["ShdDayId"].ToString())
                {
                    case "1":  chkDay1.Checked  = true; break;
                    case "2":  chkDay2.Checked  = true; break;
                    case "3":  chkDay3.Checked  = true; break;
                    case "4":  chkDay4.Checked  = true; break;
                    case "5":  chkDay5.Checked  = true; break;
                    case "6":  chkDay6.Checked  = true; break;
                    case "7":  chkDay7.Checked  = true; break;
                    case "8":  chkDay8.Checked  = true; break;
                    case "9":  chkDay9.Checked  = true; break;
                    case "10": chkDay10.Checked = true; break;
                    case "11": chkDay11.Checked = true; break;
                    case "12": chkDay12.Checked = true; break;
                    case "13": chkDay13.Checked = true; break;
                    case "14": chkDay14.Checked = true; break;
                    case "15": chkDay15.Checked = true; break;
                    case "16": chkDay16.Checked = true; break;
                    case "17": chkDay17.Checked = true; break;
                    case "18": chkDay18.Checked = true; break;
                    case "19": chkDay19.Checked = true; break;
                    case "20": chkDay20.Checked = true; break;
                    case "21": chkDay21.Checked = true; break;
                    case "22": chkDay22.Checked = true; break;
                    case "23": chkDay23.Checked = true; break;
                    case "24": chkDay24.Checked = true; break;
                    case "25": chkDay25.Checked = true; break;
                    case "26": chkDay26.Checked = true; break;
                    case "27": chkDay27.Checked = true; break;
                    case "28": chkDay28.Checked = true; break;
                    case "29": chkDay29.Checked = true; break;
                    case "30": chkDay30.Checked = true; break;
                    case "31": chkDay31.Checked = true; break;
                    case "last": chkDayLast.Checked = true; break;
                    default: break;
                }
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void NameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND SchID != @P2 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الإسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameEn.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(txtNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered Schedule English Name exist already,Please enter another name", "الإسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Schedule WHERE SchNameEn = @P1 AND ISNULL(SchDeleted,0) = 0 " + UQ, new string[] { txtNameEn.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            if (source.Equals(cvNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, false, General.Msg("Name (Ar) Is Required", "الإسم بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameAr.Text)) { e.IsValid = false; }
                }
                if (!string.IsNullOrEmpty(txtNameAr.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Schedule Arabic Name exist already,Please enter another name", "الإسم بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Schedule WHERE SchNameAr = @P1 AND ISNULL(SchDeleted,0) = 0 " + UQ, new string[] { txtNameAr.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}