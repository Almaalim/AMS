using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;

public partial class ERS_EmployeeOvertime : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpRequestPro ProCs = new EmpRequestPro();
    EmpRequestSql SqlCs = new EmpRequestSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string MainQuery = "";
    string[] arrStatusImage = { "~/images/ERS_Images/Waiting.png", "~/images/ERS_Images/approve.png", "~/images/ERS_Images/reject.png", "~/images/ERS_Images/NotFound.png" };
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            MainQuery += " SELECT * FROM OverTime WHERE OvtIsValid ='True' AND EmpID = '" + pgCs.LoginEmpID + "'";
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense();    
                /*** get Requst Permission ***/ ViewState["ReqHT"] = pgCs.GetAllRequestPerm();
                /*** Common Code ************************************/

                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);

                string CurrentMonth = DTCs.FindCurrentMonth();
                string CurrentYear  = DTCs.FindCurrentYear();

                if (Session["ERSRefresh"] == null) { Session["ERSRefresh"] = "NotUpdate"; }
                
                if (Session["ERSRefresh"].ToString() == "Update")
                {
                    FillData(Session["ERSRefreshMonth"].ToString(), Session["ERSRefreshYear"].ToString());
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Session["ERSRefreshMonth"].ToString()));
                    ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Session["ERSRefreshYear"].ToString()));
                    Session["ERSRefresh"] = "NotUpdate";
                }
                else
                {
                    FillData(CurrentMonth, CurrentYear);
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToInt32(CurrentMonth).ToString()));
                    ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(CurrentYear));
                }

                Session["AttendanceListMonth"] = CurrentMonth;
                Session["AttendanceListYear"]  = CurrentYear;

                FillName();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillName()
    {
        DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
        if (!DBCs.IsNullOrEmpty(DT)) { LitEmpName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillData(string Month, string Year)
    {
        try
        {
            //Literal1.Text = General.Msg(Convert.ToInt32(Month).ToString("00") + "/" + Year.ToString(), Convert.ToInt32(Month).ToString("00") + "/" + Year.ToString());

            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery;

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(Year, Month, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            FQ.Append(MainQuery);
            FQ.Append(" AND OvtDate BETWEEN @SDate AND @EDate ");
            FQ.Append(" ORDER BY OvtDate,OvtShift,OvtStartTime ");
                
            cmd.Parameters.AddWithValue("@SDate", SDate);
            cmd.Parameters.AddWithValue("@EDate", EDate);
            sql = FQ.ToString();

            ClearSummary();
            cmd.CommandText = sql;
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string convertDayToArabic(string day)
    {
        string arDay = "";
        switch (day)
        {
            case "Sat":
                arDay = "السبت";
                break;
            case "Sun":
                arDay = "الأحد";
                break;
            case "Mon":
                arDay = "الأثنين";
                break;
            case "Tue":
                arDay = "الثلاثاء";
                break;
            case "Wed":
                arDay = "الأربعاء";
                break;
            case "Thu":
                arDay = "الخميس";
                break;
            case "Fri":
                arDay = "الجمعة";
                break;
        }
        return arDay;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayDayName(object pGDate)
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(pGDate)))
            {
                GregorianCalendar Grn = new GregorianCalendar();
                string dayNameEn = Grn.GetDayOfWeek(Convert.ToDateTime(pGDate)).ToString();
                string dayNameAr = convertDayToArabic(Convert.ToDateTime(pGDate).ToString("ddd"));

                return General.Msg(dayNameEn, dayNameAr);
            }
            return string.Empty;
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((ddlMonth.SelectedIndex >= 0) && (ddlYear.SelectedIndex >= 0)) { FillData(ddlMonth.SelectedValue, ddlYear.SelectedValue); }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillSummary() 
    {
        DateTime SDate = DateTime.Now;
        DateTime EDate = DateTime.Now;
        DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

        StringBuilder QSum = new StringBuilder(); 
        QSum.Append(" SELECT  ISNULL(SUM(CASE WHEN (OvtTypeFlag = '1' AND OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) ET_BeginEarly ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '1' AND OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) OT_BeginEarly ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '2' AND OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) ET_OutLate    ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '2' AND OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) OT_OutLate    ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '3' AND OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) ET_OutShift   ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '3' AND OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) OT_OutShift   ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '4' AND OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) ET_NoShift    ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '4' AND OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) OT_NoShift    ");  
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '5' AND OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) ET_InVac      ");
		QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '5' AND OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) OT_InVac      ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_BeginEarly ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '2') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_OutLate    ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '3') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_OutShift   ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '4') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_NoShift    ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtTypeFlag = '5') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_InVac      ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtReqStatus = '0') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_ExtraTime ");
        QSum.Append("        ,ISNULL(SUM(CASE WHEN (OvtReqStatus = '1') THEN ISNULL(OvtDuration,0) ELSE 0 END),0) T_OverTime  ");
        QSum.Append("        ,ISNULL(SUM(ISNULL(OvtDuration,0),0) Total ");
        QSum.Append(" FROM OverTime ");
        QSum.Append(" WHERE OvtIsValid = 1 AND EmpID = @P1 AND OvtDate BETWEEN @P2 AND @P3 ");

        DataTable DT = DBCs.FetchData(QSum.ToString(), new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(DT)) 
        { 
            lblBeginEarlyVTotal.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_BeginEarly"]);
            lblBeginEarlyVPaid.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["OT_BeginEarly"]);
            lblBeginEarlyVUnpaid.Text = DisplayFun.GrdDisplayDuration(DT.Rows[0]["ET_BeginEarly"]);
            
            lblOutLateVTotal.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_OutLate"]);
            lblOutLateVPaid.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["OT_OutLate"]);
            lblOutLateVUnpaid.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["ET_OutLate"]);
            
            lblOutShiftVTotal.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_OutShift"]);
            lblOutShiftVPaid.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["OT_OutShift"]);
            lblOutShiftVUnpaid.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["ET_OutShift"]);
            
            lblNoShiftVTotal.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_NoShift"]);
            lblNoShiftVPaid.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["OT_NoShift"]);
            lblNoShiftVUnpaid.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["ET_NoShift"]);

            lblInVacVTotal.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_InVac"]);
            lblInVacVPaid.Text       = DisplayFun.GrdDisplayDuration(DT.Rows[0]["OT_InVac"]);
            lblInVacVUnpaid.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["ET_InVac"]);

            lblOvtVTotal.Text        = DisplayFun.GrdDisplayDuration(DT.Rows[0]["Total"]);
            lblOvtVPaid.Text         = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_OverTime"]);
            lblOvtVUnpaid.Text       = DisplayFun.GrdDisplayDuration(DT.Rows[0]["T_ExtraTime"]);
        }
        
        //======TotalOvtWait
        DataTable WDT = DBCs.FetchData(" SELECT SUM(OvtDuration) Total FROM OverTime WHERE OvtIsValid = 1 AND EmpID = @P1 AND OvtDate BETWEEN @P2 AND @P3 AND OvtID IN (SELECT GapOvtID FROM EmpRequest WHERE RetID = 'OVT' AND ErqReqStatus = 0) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(WDT))  { if (WDT.Rows[0]["Total"] != DBNull.Value) { lblOvtVWait.Text = DisplayFun.GrdDisplayDuration(WDT.Rows[0]["Total"]); } }    
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearSummary()
    {
        lblBeginEarlyVTotal.Text = lblBeginEarlyVPaid.Text = lblBeginEarlyVUnpaid.Text = "00:00:00";
        lblOutLateVTotal.Text    = lblOutLateVPaid.Text    = lblOutLateVUnpaid.Text    = "00:00:00";
        lblOutShiftVTotal.Text   = lblOutShiftVPaid.Text   = lblOutShiftVUnpaid.Text   = "00:00:00";      
        lblNoShiftVTotal.Text    = lblNoShiftVPaid.Text    = lblNoShiftVUnpaid.Text    = "00:00:00";     
        lblInVacVTotal.Text      = lblInVacVPaid.Text      = lblInVacVUnpaid.Text      = "00:00:00";
        lblOvtVTotal.Text        = lblOvtVPaid.Text        = lblOvtVUnpaid.Text        = lblOvtVWait.Text = "00:00:00";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton _btnReqSend   = (ImageButton)e.Row.Cells[9].Controls[1];
                ImageButton _btnReqStatus = (ImageButton)e.Row.Cells[10].Controls[1];

                _btnReqSend.Visible = pgCs.GetRequestPerm(ViewState["ReqHT"], "OVT");

                _btnReqStatus.Visible = false;
                int StatusReq = FoundRequest(((DataRowView)e.Row.DataItem)["OvtID"].ToString());

                _btnReqSend.ImageUrl = "~/images/ERS_Images/SendRequest.png";
                _btnReqSend.ToolTip  = General.Msg("Send Overtime Request", "إرسال طلب وقت إضافي");
                if (StatusReq != 3)
                {
                    _btnReqStatus.Visible = true;
                    _btnReqStatus.ImageUrl = arrStatusImage[StatusReq];
                    if (StatusReq != 2)
                    {
                        _btnReqSend.Enabled  = false;
                        _btnReqSend.ToolTip  = "";
                        _btnReqSend.ImageUrl = "~/images/ERS_Images/SendRequestGray.png";
                    }
                    else { _btnReqSend.Enabled = true; }
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
                case ("ReqSend_Command"):
                    
                    //if (FindApprovalSequence("EXC"))
                    //{
                        lblNamePopup.Text = General.Msg("Overtime Request", "طلب وقت إضافي");
                        ifrmPopup.Attributes.Add("src", "../Pages_Request/OverTimeRequest2.aspx?ID=" + e.CommandArgument.ToString());
                        Session["ERSRefreshMonth"] = ddlMonth.SelectedValue;
                        Session["ERSRefreshYear"]  = ddlYear.SelectedValue;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup();", true);
                    //}

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
    protected void FillGrid(SqlCommand cmd)
    {
        DataTable GDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            grdData.DataSource = (DataTable)GDT;
            ViewState["grdDataDT"] = (DataTable)GDT;
            grdData.DataBind();

            FillSummary();
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
    protected int FoundRequest(string ID)
    {
        int ReqStat = 3;
        try
        {
            DataTable DT = new DataTable();

            if (pgCs.GetRequestPerm(ViewState["ReqHT"], "OVT"))
            {
                DT = DBCs.FetchData(" SELECT ErqReqStatus FROM EmpRequest WHERE EmpID = @P1 AND RetID ='OVT' AND GapOvtID = @P2 ORDER By ErqID DESC ", new string[] { pgCs.LoginEmpID, ID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string ReqStatus = DT.Rows[0]["ErqReqStatus"].ToString();
                    if      (ReqStatus == "0") { ReqStat = 0; }
                    else if (ReqStatus == "1") { ReqStat = 1; }
                    else if (ReqStatus == "2") { ReqStat = 2; }
                    else { ReqStat = 3; }
                }
            }
        }
        catch (Exception e1) { }

        return ReqStat;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}