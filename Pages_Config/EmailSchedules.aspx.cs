using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
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
                ttRunScheduleEvery.SetTime(1, 0);
                divHour.Visible = true;
                cvRunScheduleEvery.Enabled = true;
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
            for (int i = 1; i <= 31; i++)
            {
                ListItem _li = new ListItem(i.ToString(), i.ToString());
                cblCalendarDays.Items.Add(_li);
            }

            ListItem _li2 = new ListItem(General.Msg("Last","الأخير"), "Last");
            cblCalendarDays.Items.Add(_li2);

            DTCs.MonthPopulateList(ref cblMonth, pgCs.DateType);

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
            sql = MainQuery + " AND " + ddlFilter.SelectedValue + " LIKE @P1 ";
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
        txtNameAr.Enabled = pStatus;
        txtNameEn.Enabled = pStatus;
        txtDescription.Enabled = pStatus;
        chkStatus.Enabled = pStatus;
        ddlScheduleType.Enabled = pStatus;
        ttRunScheduleEvery.Enabled = pStatus;
        ddlWeekOfMonth.Enabled = pStatus;
        txtRepeatDays.Enabled = pStatus;
        txtRepeatWeek.Enabled = pStatus;
        tpStartTime.Enabled = pStatus;
        txtSubject.Enabled = pStatus;
        txtMessage.Enabled = pStatus;
        ddlReportGroup.Enabled = pStatus;
        lbxUsers.Enabled = pStatus;
        ddlReportFormat.Enabled = pStatus;

        cblDaysOfWeek.Enabled = pStatus;
        cblCalendarDays.Enabled = pStatus;
        cblMonth.Enabled = pStatus;
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
        txtID.Enabled = txtID.Visible = false;
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
            ProCs.SchStartMin  = tpStartTime.getMinutes().ToString();

            if (!string.IsNullOrEmpty(ttRunScheduleEvery.getHours().ToString()))
            {
                ProCs.SchEveryHour   = ttRunScheduleEvery.getHours().ToString();
                ProCs.SchEveryMinute = ttRunScheduleEvery.getMinutes().ToString();
            }

            if (ddlWeekOfMonth.SelectedIndex > 0) { ProCs.SchWeekOfMonth = ddlWeekOfMonth.SelectedValue; }
            
            if (!string.IsNullOrEmpty(txtRepeatDays.Text)) { ProCs.SchRepeatDays = txtRepeatDays.Text; }
            if (!string.IsNullOrEmpty(txtRepeatWeek.Text)) { ProCs.SchRepeatWeeks = txtRepeatWeek.Text; }
            if (!string.IsNullOrEmpty(txtMessage.Text))    { ProCs.SchEmailBodyContent = txtMessage.Text; }
            if (!string.IsNullOrEmpty(txtSubject.Text))    { ProCs.SchEmailSubject = txtSubject.Text; }
            if (ddlReportGroup.SelectedIndex > 0)          { ProCs.ReportID = ddlReportGroup.SelectedValue; }

            string Users = string.Empty;
            if (lbxUsers.SelectedIndex > -1)
            {
                if (lbxUsers.Items.Count > 0)
                {
                    for (int i = 0; i < lbxUsers.Items.Count; i++)
                    {
                        if (lbxUsers.Items[i].Selected)
                        {
                            if (string.IsNullOrEmpty(Users)) { Users = lbxUsers.Items[i].Text.Trim(); } else { Users += "," + lbxUsers.Items[i].Text.Trim(); }
                        }
                    }
                }
            }
            ProCs.SchUsers = Users;

            if (ddlReportFormat.SelectedIndex > 0) { ProCs.SchReportFormat = ddlReportFormat.SelectedValue; }


            string SchWeekDays = null;
            if (ddlScheduleType.SelectedValue == "Hourly") { }
            else if (ddlScheduleType.SelectedValue == "Weekly" || ddlScheduleType.SelectedValue == "WeeklySkip" || ddlScheduleType.SelectedValue == "WeekNumber")
            {
                for (int i = 0; i < cblDaysOfWeek.Items.Count; i++)
                {
                    if (cblDaysOfWeek.Items[i].Selected) { if (string.IsNullOrEmpty(SchWeekDays)) { SchWeekDays += cblDaysOfWeek.Items[i].Value; } else { SchWeekDays += "," + cblDaysOfWeek.Items[i].Value; } }
                }
            }

            string SchMonths = null;
            if (ddlScheduleType.SelectedItem.Value == "WeekNumber" || ddlScheduleType.SelectedValue == "Calendar" )
            {
                for (int i = 0; i < cblMonth.Items.Count; i++)
                {
                    if (cblMonth.Items[i].Selected) { if (string.IsNullOrEmpty(SchMonths)) { SchMonths += cblMonth.Items[i].Value; } else { SchMonths += "," + cblMonth.Items[i].Value; } }
                }
            }

            string SchDays = null;
            if (ddlScheduleType.SelectedValue == "Calendar")
            {
                for (int i = 0; i < cblCalendarDays.Items.Count; i++)
                {
                    if (cblCalendarDays.Items[i].Selected) { if (string.IsNullOrEmpty(SchDays)) { SchDays += cblCalendarDays.Items[i].Value; } else { SchDays += "," + cblCalendarDays.Items[i].Value; } }
                }
            }

            if (pgCs.DateType == "Hijri") { ProCs.SchCalendar = "H"; } else { ProCs.SchCalendar = "G"; }

            ProCs.SchDays     = SchDays;
            ProCs.SchMonths   = SchMonths;
            ProCs.SchWeekDays = SchWeekDays;

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
            txtNameAr.Text = "";
            txtNameEn.Text = "";
            txtDescription.Text = "";
            chkStatus.Checked = false;
            ddlScheduleType.SelectedIndex = -1;
            ttRunScheduleEvery.SetTime(1, 0);
            ddlWeekOfMonth.SelectedIndex = -1;
            txtRepeatDays.Text = "";
            txtRepeatWeek.Text = "";
            tpStartTime.ClearTime();
            txtSubject.Text = "";
            txtMessage.Text = "";
            ddlReportGroup.SelectedIndex = -1;
            lbxUsers.ClearSelection();
            ddlReportFormat.SelectedIndex = -1;

            for (int i = 0; i < cblDaysOfWeek.Items.Count; i++) { cblDaysOfWeek.Items[i].Selected = false; }
            for (int i = 0; i < cblCalendarDays.Items.Count; i++) { cblCalendarDays.Items[i].Selected = false; }
            for (int i = 0; i < cblMonth.Items.Count; i++) { cblMonth.Items[i].Selected = false; }
            calStartDate.ClearDate();
            calEndDate.ClearDate();

            UIShow(ddlScheduleType.SelectedValue, 0);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
        divHour.Visible        = false; /**/ cvRunScheduleEvery.Enabled = false;
        divDaysOfWeek.Visible  = false; /**/ cvDaysOfWeek.Enabled = false;
        divDayRepeat.Visible   = false; /**/ rvRepeatDays.Enabled = false;
        divWeekRepeat.Visible  = false;
        divMonths.Visible      = false; /**/ cvMonth.Enabled = false;
        divWeekOfMonth.Visible = false;
        divCalendar.Visible    = false; /**/ cvCalendarDays.Enabled = false;

        switch (ScheduleType)
        {
            case "Hourly":
                divHour.Visible            = true;
                cvRunScheduleEvery.Enabled = true;
                break;
            case "Weekly":
                divDaysOfWeek.Visible = true;
                cvDaysOfWeek.Enabled  = true;
                break;
            case "Daily":
                divDayRepeat.Visible = true;
                rvRepeatDays.Enabled = true;
                break;
            case "WeeklySkip":
                divWeekRepeat.Visible = true;
                divDaysOfWeek.Visible = true;
                cvDaysOfWeek.Enabled  = true;
                break;
            case "WeekNumber":
                divMonths.Visible      = true;
                divWeekOfMonth.Visible = true;
                divDaysOfWeek.Visible  = true;

                cvMonth.Enabled        = true;
                cvDaysOfWeek.Enabled   = true;
                break;
            case "Calendar":
                divMonths.Visible   = true;
                divCalendar.Visible = true;

                cvMonth.Enabled        = true;
                cvCalendarDays.Enabled = true;
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
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
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
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
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
            DataRow[] DRs = DT.Select("SchID =" + pID + "");

            txtID.Text          = DRs[0]["SchID"].ToString();
            txtNameEn.Text      = DRs[0]["SchNameEn"].ToString();
            txtNameAr.Text      = DRs[0]["SchNameAr"].ToString();
            txtDescription.Text = DRs[0]["SchDescription"].ToString();
            
            tpStartTime.SetTime(Convert.ToInt32(DRs[0]["SchStartHour"]), Convert.ToInt32(DRs[0]["SchStartMin"]));
            if (DRs[0]["SchIsActive"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["SchIsActive"]); }

            ddlScheduleType.SelectedIndex = ddlScheduleType.Items.IndexOf(ddlScheduleType.Items.FindByValue(DRs[0]["SchType"].ToString()));


            int hours = 0;
            int minute = 0;
            if (DRs[0]["SchEveryHour"]   != DBNull.Value) { hours  = Convert.ToInt32(DRs[0]["SchEveryHour"]); }
            if (DRs[0]["SchEveryMinute"] != DBNull.Value) { minute = Convert.ToInt32(DRs[0]["SchEveryMinute"]); }
            ttRunScheduleEvery.SetTime(hours, minute);

            ddlWeekOfMonth.SelectedIndex = ddlWeekOfMonth.Items.IndexOf(ddlWeekOfMonth.Items.FindByValue(DRs[0]["SchWeekOfMonth"].ToString()));

            txtRepeatDays.Text = DRs[0]["SchRepeatDays"].ToString();
            txtRepeatWeek.Text = DRs[0]["SchRepeatWeeks"].ToString();

            calStartDate.SetGDate(DRs[0]["SchStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["SchEndDate"], pgCs.DateFormat);
       
            txtSubject.Text = DRs[0]["SchEmailSubject"].ToString();
            txtMessage.Text = DRs[0]["SchEmailBodyContent"].ToString();

            string ss = DRs[0]["ReportID"].ToString();
            ddlReportGroup.SelectedIndex = ddlReportGroup.Items.IndexOf(ddlReportGroup.Items.FindByValue(DRs[0]["ReportID"].ToString()));
        
            string strSchUsers = DRs[0]["SchUsers"].ToString();
            string[] SchUsers = strSchUsers.Split(',');

            for (int i = 0; i < SchUsers.Length; i++)
            {
                for (int j = 0; j < lbxUsers.Items.Count; j++) { if (lbxUsers.Items[j].Value.ToString().Trim() == SchUsers[i]) { lbxUsers.Items[j].Selected = true; } }
            }

            ddlReportFormat.SelectedIndex = ddlReportFormat.Items.IndexOf(ddlReportFormat.Items.FindByValue(DRs[0]["SchReportFormat"].ToString().Trim()));

            if (!GenCs.IsNullOrEmptyDB(DRs[0]["SchWeekDays"]))
            {
                string[] WeekDays = DRs[0]["SchWeekDays"].ToString().Split(',');
                for (int i = 0; i < WeekDays.Length; i++)
                {
                    int index = cblDaysOfWeek.Items.IndexOf(cblDaysOfWeek.Items.FindByValue(WeekDays[i]));
                    if (index > -1) { cblDaysOfWeek.Items[index].Selected = true; } else { cblDaysOfWeek.Items[index].Selected = false; }
                }
            }

            if (!GenCs.IsNullOrEmptyDB(DRs[0]["SchDays"]))
            {
                string[] Days = DRs[0]["SchDays"].ToString().Split(',');
                for (int i = 0; i < Days.Length; i++)
                {
                    int index = cblCalendarDays.Items.IndexOf(cblCalendarDays.Items.FindByValue(Days[i]));
                    if (index > -1) { cblCalendarDays.Items[index].Selected = true; } else { cblCalendarDays.Items[index].Selected = false; }
                }
            }

            if (!GenCs.IsNullOrEmptyDB(DRs[0]["SchMonths"]))
            {
                string[] Months = DRs[0]["SchMonths"].ToString().Split(',');
                for (int i = 0; i < Months.Length; i++)
                {
                    int index = cblMonth.Items.IndexOf(cblMonth.Items.FindByValue(Months[i]));
                    if (index > -1) { cblMonth.Items[index].Selected = true; } else { cblMonth.Items[index].Selected = false; }
                }
            }

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
                string Days = DT.Rows[i]["ShwWeekDayId"].ToString();

                int index = cblDaysOfWeek.Items.IndexOf(cblDaysOfWeek.Items.FindByValue(Days));
                if (index > -1) { cblDaysOfWeek.Items[index].Selected = true; } else { cblDaysOfWeek.Items[index].Selected = false; }
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
                string Days = DT.Rows[i]["ShmMonthId"].ToString();

                int index = cblMonth.Items.IndexOf(cblMonth.Items.FindByValue(Days));
                if (index > -1) { cblMonth.Items[index].Selected = true; } else { cblMonth.Items[index].Selected = false; }
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
                string Days = DT.Rows[i]["ShdDayId"].ToString();

                int index = cblCalendarDays.Items.IndexOf(cblCalendarDays.Items.FindByValue(Days));
                if (index > -1) { cblCalendarDays.Items[index].Selected = true; } else { cblCalendarDays.Items[index].Selected = false; }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayType(object pType)
    {
        try
        {
            if      (pType.ToString() == "Hourly")     { return General.Msg("Hourly", "ساعات"); }
            else if (pType.ToString() == "Daily")      { return General.Msg("Daily", "يومي"); }
            else if (pType.ToString() == "Weekly")     { return General.Msg("Weekly", "اسبوعي"); }
            else if (pType.ToString() == "WeeklySkip") { return General.Msg("Weekly skip", "تخطي الأسبوعي"); }
            else if (pType.ToString() == "WeekNumber") { return General.Msg("Week number", "عدد الأسابيع"); }
            else if (pType.ToString() == "Calendar")   { return General.Msg("Calendar", "التقويم"); }
            else if (pType.ToString() == "Once")       { return General.Msg("Run once", "تشغيل مرة واحدة"); }
            else { return string.Empty; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
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
            string NameEn = txtNameEn.Text.Trim();
            string NameAr = txtNameAr.Text.Trim();

            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND SchID != @P2 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الإسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(NameEn)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(NameEn))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered Schedule English Name exist already,Please enter another name", "الإسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Schedule WHERE SchNameEn = @P1 AND ISNULL(SchDeleted,0) = 0 " + UQ, new string[] { NameEn, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            if (source.Equals(cvNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, false, General.Msg("Name (Ar) Is Required", "الإسم بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(NameAr)) { e.IsValid = false; }
                }
                if (!string.IsNullOrEmpty(NameAr))
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Schedule Arabic Name exist already,Please enter another name", "الإسم بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Schedule WHERE SchNameAr = @P1 AND ISNULL(SchDeleted,0) = 0 " + UQ, new string[] { NameAr, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void RunScheduleEvery_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvRunScheduleEvery))
            {
                if (ttRunScheduleEvery.getTimeInSecond() <= 0) { e.IsValid = false; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DaysOfWeek_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvDaysOfWeek))
            {
                e.IsValid = cblDaysOfWeek.SelectedItem != null;
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Month_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvMonth))
            {
                e.IsValid = cblMonth.SelectedItem != null;
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CalendarDays_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvCalendarDays))
            {
                e.IsValid = cblCalendarDays.SelectedItem != null;
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