using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Globalization;

public partial class AttendanceList : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpRequestSql ReqSqlCs = new EmpRequestSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    bool ShoweAlert;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            //CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/

            if (Session["ShowedAlert"] != null) { ShoweAlert = Convert.ToBoolean(Session["ShowedAlert"].ToString()); }

            if (ddlMonth.SelectedIndex >= 0 && ddlYear.SelectedIndex >= 0)
            {
                Session["AttendanceListMonth"] = ddlMonth.SelectedValue;
                Session["AttendanceListYear"] = ddlYear.SelectedValue;
            }

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense();  
                /*** get Requst Permission ***/ ViewState["ReqHT"] = pgCs.GetAllRequestPerm();
                /*** Common Code ************************************/

                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);

                if (Session["ERSRefresh"] == null ) { Session["ERSRefresh"] = "NotUpdate"; }
                
                if (Session["ERSRefresh"].ToString() == "Update" )
                {
                    FillMonthTable(Convert.ToInt32(Session["ERSRefreshMonth"]), Convert.ToInt32(Session["ERSRefreshYear"]));
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Session["ERSRefreshMonth"].ToString()));
                    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Session["ERSRefreshYear"].ToString()));
                    Session["ERSRefresh"] = "NotUpdate";
                }
                else
                {
                    string CurrentMonth = DTCs.FindCurrentMonth();
                    string CurrentYear  = DTCs.FindCurrentYear();

                    FillMonthTable(Convert.ToInt32(CurrentMonth), Convert.ToInt32(CurrentYear));
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(CurrentMonth));
                    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(CurrentYear));
                }

                
                if (!ShoweAlert)
                {
                    int TotalGaps, TotalAbs;

                    FindGapsForCurrentMonth(out TotalGaps, out TotalAbs);

                    if (TotalGaps > 0 || TotalAbs > 0)
                    {
                        lblNamePopupAlert.Text = General.Msg("Alert", "تنبيه");
                        ifrmPopupAlert.Attributes.Add("src", "AlertEmpForGaps.aspx?Gaps=" + TotalGaps + "&Abs=" + TotalAbs);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup('divPopupAlert');", true);
                    }

                    ShoweAlert = true;
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected DataTable EmptyMonthTable()
    {
        DataTable EmptyMonthdt = new DataTable();
        EmptyMonthdt.Columns.Add(new DataColumn("DayName"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("DayGDate" , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("DayHDate" , typeof(string)));
        
        EmptyMonthdt.Columns.Add(new DataColumn("SsmShift"    , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SsmPunchIn"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SsmPunchOut" , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SsmBeginLate", typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SsmOutEarly" , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SsmGapDur_MG", typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("AnyStatus"   , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("AnyExcID"    , typeof(string)));
        
        EmptyMonthdt.Columns.Add(new DataColumn("Status"    , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("StatusName", typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SVisible"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SEnabled"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SImage"    , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("SToolTip"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("RVisible"  , typeof(string)));
        EmptyMonthdt.Columns.Add(new DataColumn("RImage"    , typeof(string)));
       
        EmptyMonthdt.Columns.Add(new DataColumn("empty"    , typeof(string)));
        
        return EmptyMonthdt;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillMonthTable(int pMonth, int pYear)
    {
        try
        {
            Session["AttendanceListMonth"] = pMonth.ToString();
            Session["AttendanceListYear"]  = pYear.ToString();

            Literal1.Text = General.Msg("Attendance List : ", "الحضور : ") + pMonth.ToString("00") + "/" + pYear.ToString();

            GregorianCalendar Grn = new GregorianCalendar();
            UmAlQuraCalendar Umq = new UmAlQuraCalendar();
            DataTable MonthDT = EmptyMonthTable();
           
            string GDate;
            string HDate;

            int LastDay = DTCs.FindLastDay(pMonth, pYear);
            grdData.PageSize = LastDay;

            DateTime TodayDate = DTCs.ConvertToDatetime(DTCs.GDateNow("dd/MM/yyyy"), "Gregorian");
            DataTable DT = FindDayStatus(pYear.ToString(), pMonth.ToString());
            bool ReqPermVac = pgCs.GetRequestPerm(ViewState["ReqHT"], "VAC");
            bool ReqPermCom = pgCs.GetRequestPerm(ViewState["ReqHT"], "COM");
            bool ReqPermJob = pgCs.GetRequestPerm(ViewState["ReqHT"], "JOB");
            bool ReqPermESH = pgCs.GetRequestPerm(ViewState["ReqHT"], "ESH");

            DataRow MonthRow = null;
            for (int i = 1; i <= LastDay; i++)
            {
                if (pgCs.DateType == "Hijri")
                {
                    HDate = i.ToString("00") + "/" + pMonth.ToString("00") + "/" + pYear;
                    GDate = DTCs.HijriToGreg(HDate, "dd/MM/yyyy");   
                }
                else
                {
                    GDate = new DateTime(pYear, pMonth, i, Grn).ToString("dd/MM/yyyy");
                    HDate = DTCs.GregToHijri(GDate, "dd/MM/yyyy");   
                }

                DateTime GDateDT = DTCs.ConvertToDatetime(GDate, "Gregorian");
                string dayNameEn = Grn.GetDayOfWeek(GDateDT).ToString();
                string dayNameAr = convertDayToArabic(GDateDT.ToString("ddd"));

                if (i == 1) { Session["AttendanceListStartDate"] = GDate; }
                //////////////////////////////////////////////////////////////////
                MonthRow = MonthDT.NewRow();
                MonthRow["DayName"]  = General.Msg(dayNameEn,dayNameAr);
                MonthRow["DayGDate"] = GDate;
                MonthRow["DayHDate"] = HDate;

                if (!DBCs.IsNullOrEmpty(DT))
                {
                    DataRow[] DRs = DT.Select("DsmDate = '" + GDateDT + "'");
                    MonthRow["SsmShift"]     = DRs[0]["SsmShift"].ToString();
                    MonthRow["SsmPunchIn"]   = DRs[0]["SsmPunchIn"].ToString();
                    MonthRow["SsmPunchOut"]  = DRs[0]["SsmPunchOut"].ToString();
                    MonthRow["SsmBeginLate"] = DRs[0]["SsmBeginLate"].ToString();
                    MonthRow["SsmOutEarly"]  = DRs[0]["SsmOutEarly"].ToString();
                    MonthRow["SsmGapDur_MG"] = DRs[0]["SsmGapDur_MG"].ToString();
                    MonthRow["AnyStatus"]    = DRs[0]["AnyStatus"].ToString();
                    MonthRow["AnyExcID"]     = DRs[0]["AnyExcID"].ToString();
                }

                string[] StatusValue = FindStatusValue(DT, GDateDT, TodayDate, ReqPermVac || ReqPermCom || ReqPermJob, ReqPermESH);
                MonthRow["Status"]     = StatusValue[0];
                MonthRow["StatusName"] = StatusValue[1];
                MonthRow["SVisible"]   = StatusValue[2];
                MonthRow["SEnabled"]   = StatusValue[3];
                MonthRow["SImage"]     = StatusValue[4];
                MonthRow["SToolTip"]   = StatusValue[5];
                MonthRow["RVisible"]   = StatusValue[6];
                MonthRow["RImage"]     = StatusValue[7];

                MonthDT.Rows.Add(MonthRow);
                MonthDT.AcceptChanges();
                //////////////////////////////////////////////////////////////////
            }     
            Session["MonthDT"] = (DataTable)MonthDT;
            grdData.DataSource = (DataTable)MonthDT;
            grdData.DataBind();
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FindDayStatus(string Year, string Month)
    {
        try
        {
            DateTime TodayDate = DTCs.ConvertToDatetime(DTCs.GDateNow(), "Gregorian");
            
            DateTime SDate;
            DateTime EDate;
            DTCs.FindMonthDates(Year, Month, out SDate, out EDate);
            
            DataTable DT = DBCs.FetchData(" SELECT DsmDate,SsmShift,SsmPunchIn,SsmPunchOut,SsmBeginLate,SsmOutEarly,SsmGapDur_MG,AnyStatus,Status,AnyExcID FROM AttendanceListView WHERE EmpID = @P1 AND DsmDate BETWEEN @P2 AND @P3 ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
            if (!DBCs.IsNullOrEmpty(DT)) { return DT; } else { return new DataTable(); }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return new DataTable();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string[] FindStatusValue(DataTable DT,DateTime GDayDate, DateTime TodayDate, bool VacPerm, bool ESHPerm)
    {
        try
        {
            bool isFind = false;
            //MonthRow["Status"],MonthRow["StatusName"],MonthRow["SVisible"],MonthRow["SEnabled"],MonthRow["SImage"],MonthRow["SToolTip"],MonthRow["RVisible"],MonthRow["RImage"]
            if (!DBCs.IsNullOrEmpty(DT))
            {
                DataRow[] DRs = DT.Select("DsmDate = '" + GDayDate + "'");
                if (DRs.Count() > 0)
                {
                    isFind = true;
                    if (!GenCs.IsNullOrEmptyDB(DRs[0]["Status"]))
                    {
                        string Status     = DRs[0]["Status"].ToString().Trim();
                        string StatusName = DisplayFun.GrdDisplayDayStatus(Status, pgCs.Version);
                        string AnyStatus  = DRs[0]["AnyStatus"].ToString().Trim();

                        switch (Status)
                        {
                            case "P": return new string[] { "P", StatusName, "T", "F", FindIconStatus("Present", 0), FindNameStatus("Present"), "F", "" };
                            case "A":
                                {
                                    if (AnyStatus == "D")
                                    { 
                                    
                                        if (VacPerm) { return new string[] { "A", StatusName, "T", "T", FindIconStatus("Absent", 0), FindNameStatus("SendVAC"), "T", FindIconStatus("EmpRequest", 3) }; }
                                        else         { return new string[] { "A", StatusName, "T", "F", FindIconStatus("Absent", 0), FindNameStatus("SendVAC"), "T", FindIconStatus("EmpRequest", 3) }; }
                                    }
                                    else if (AnyStatus == "S")
                                    { 
                                    
                                        if (ESHPerm) { return new string[] { "A", StatusName, "T", "T", FindIconStatus("Absent", 0), FindNameStatus("SendESH"), "T", FindIconStatus("EmpRequest", 3) }; }
                                        else         { return new string[] { "A", StatusName, "T", "F", FindIconStatus("Absent", 0), FindNameStatus("SendESH"), "T", FindIconStatus("EmpRequest", 3) }; }
                                    }

                                    break;
                                }
                            case "H"  : return new string[] { "H" , StatusName, "T", "F", FindIconStatus("Holiday", 0), FindNameStatus("Holiday"), "F", "" };
                            case "PE" : return new string[] { "PE", StatusName, "T", "F", FindIconStatus("Absent", 0), "", "T", FindIconStatus("EmpRequest", 1) };
                            case "UE" : return new string[] { "UE", StatusName, "T", "F", FindIconStatus("Absent", 0), "", "T", FindIconStatus("EmpRequest", 1) };
                            case "CM" : return new string[] { "CM", StatusName, "T", "F", FindIconStatus("Absent", 0), "", "T", FindIconStatus("EmpRequest", 1) };
                            case "JB" : return new string[] { "JB", StatusName, "T", "F", FindIconStatus("Absent", 0), "", "T", FindIconStatus("EmpRequest", 1) };
                            case "WE" : return new string[] { "WE", StatusName, "T", "F", FindIconStatus("NoWork", 0), "", "F", "" };

                            case "IN" : return new string[] { "IN", StatusName, "T", "F", FindIconStatus("Present", 0), "", "F", "" };
                            case "OU" : return new string[] { "OU", StatusName, "T", "F", FindIconStatus("Present", 0), "", "F", "" };
                            case "OI" : return new string[] { "OI", StatusName, "T", "F", FindIconStatus("Present", 0), "", "F", "" };
                        }
                    }
                }

            }
            
            if (!isFind)
            {
                if (GDayDate < TodayDate)  
                { 
                    if (!FoundWorkTime(GDayDate)) { return new string[] { "N", DisplayFun.GrdDisplayDayStatus("N", pgCs.Version), "T", "F", FindIconStatus("NoWork", 0), "", "F", "" }; }  
                    else { return new string[] { "NP", DisplayFun.GrdDisplayDayStatus("NP", pgCs.Version), "T", "F", FindIconStatus("NoWork", 0), "", "F", "" }; }
                }
                else if (GDayDate == TodayDate || GDayDate > TodayDate) 
                {
                    string Status = (GDayDate == TodayDate) ? "T" : "";
                    string ReqBy     = "";
                    string ReqType   = "";
                    int    ReqStatus = 3;

                    bool isHolFound = FoundHoliday(GDayDate);
                    if (isHolFound)
                    {
                        return new string[] { "H" , DisplayFun.GrdDisplayDayStatus("H", pgCs.Version), "T", "F", FindIconStatus("Holiday", 0), FindNameStatus("Holiday"), "F", "" };
                    }

                    bool isVacFound = FoundAbsentDayRequest(GDayDate, out ReqBy, out ReqType, out ReqStatus);
                    if (isVacFound)
                    {
                        if (ReqBy == "EMP") 
                        { 
                            if (ReqStatus == 1) { return new string[] { Status, FindNameStatus(ReqType), "T", "F", FindIconStatus("Absent", 0), FindNameStatus("TodayVac"), "T", FindIconStatus("EmpRequest", ReqStatus) }; }
                            else { return new string[] { Status, DisplayFun.GrdDisplayDayStatus(Status, pgCs.Version), "T", "F", FindIconStatus("Absent", 0), FindNameStatus("TodayVac"), "T", FindIconStatus("EmpRequest", ReqStatus) }; }
                        }
                        else if (ReqBy == "USR")
                        {
                            return new string[] { Status, FindNameStatus(ReqType), "T", "F", FindIconStatus("Absent", 0), FindNameStatus("TodayVac"), "T", FindIconStatus("UsrRequest", 0) };
                        }
                    }

                    if (Status == "T") { return new string[] { Status, DisplayFun.GrdDisplayDayStatus(Status, pgCs.Version), "T", "F", FindIconStatus("Today", 0), FindNameStatus("Today"), "F", "" }; }
                    if (Status == "")  { return new string[] { Status, DisplayFun.GrdDisplayDayStatus(Status, pgCs.Version), "F", "F", "", "", "F", "" }; }
                }
               
            }

            return new string[] { "", "", "", "", "", "", "", "" };
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return new string[] { "", "", "", "", "", "", "", "" };
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string convertDayToArabic(string day)
    {
        string arDay = "";
        switch (day)
        {
            case "Sat": arDay = "السبت";   break;
            case "Sun": arDay = "الأحد";    break;
            case "Mon": arDay = "الاثنين";  break;
            case "Tue": arDay = "الثلاثاء"; break;
            case "Wed": arDay = "الأربعاء"; break;
            case "Thu": arDay = "الخميس";  break;
            case "Fri": arDay = "الجمعة";  break;
        }
        return arDay;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((ddlMonth.SelectedIndex >= 0) && (ddlYear.SelectedIndex >= 0))
            {
                FillMonthTable(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
            }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected bool FoundWorkTime(DateTime GDate)
    {
        try
        {
            DataTable DT = ReqSqlCs.FetchWorkTime(GDate, pgCs.LoginEmpID, true);
            if (DBCs.IsNullOrEmpty(DT)) { return false; } else { return true; }
        }
        catch (Exception e1) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FoundHoliday(DateTime GDate)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM Holiday WHERE @P1 BETWEEN HolStartDate AND HolEndDate AND ISNULL(HolDeleted,0) = 0 AND HolStatus = 'True' ", new string[] { GDate.ToString() });
            if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception e1) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string FindNameStatus(string Status)
    {
        string StatusName = "";
        switch (Status)
        {
            case "Present": StatusName = General.Msg("Present" ,"حاضر");  break;
            case "Absent" : StatusName = General.Msg("Absent"  ,"غائب");  break;
            case "Holiday": StatusName = General.Msg("Holiday" ,"عطلة");  break;
            case "NoWork" : StatusName = General.Msg("No Work" ,"لا يوجد عمل"); break;
            case "Today"  : StatusName = General.Msg("In Process" ,"تحت الإجراء"); break;
            case "IN"     : StatusName = General.Msg("IN Only" ,"دخول فقط"); break;
            case "OUT"    : StatusName = General.Msg("OUT Only" ,"خروج فقط"); break;

            case "VAC": StatusName = General.Msg("Vacation" ,"إجازة");  break;
            case "COM": StatusName = General.Msg("Commission" ,"إنتداب"); break;
            case "JOB": StatusName = General.Msg("Job Assignment" ,"مهمة عمل");  break;

            case "SendVAC": StatusName = General.Msg("Send Request", "إرسال طلب");  break;  //Msg("Send Vacation Request", "إرسال طلب إجازة");
            case "SendEXC": StatusName = General.Msg("Send Excuse Request", "إرسال طلب استئذان");  break; 
            case "SendOVT": StatusName = General.Msg("Send Overtime Request", "إرسال طلب وقت إضافي");  break;
            case "SendESH": StatusName = General.Msg("Send Shift Request", "إرسال طلب غياب وردية");  break;

            case "TodayVac": StatusName = General.Msg("Today", "اليوم");  break;
        }
        return StatusName;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string FindIconStatus(string Status, int ReqStatus)
    {
        string[] ReqStatusIcon  = { "Waiting.png", "approve.png", "reject.png", "NotFound.png" };
        string[] SendStatusIcon = { "SendRequestGray.png", "SendRequest.png" };
        string PIcon = "~/images/ERS_Images/";

        switch (Status)
        {
            case "Present": PIcon += "person_Present.png";  break; 
            case "Absent" : PIcon += "person_Absent.png";   break;
            case "Holiday": PIcon += "person_no work.png";  break;
            case "NoWork" : PIcon += "person_no work.png";  break;
            case "Today"  : PIcon += "person_Wiat.png";     break;
            
            case "EmpRequest": PIcon += ReqStatusIcon[ReqStatus];  break;
            case "UsrRequest": PIcon += "addByuser.png";           break;

            case "SendRequest": PIcon += SendStatusIcon[ReqStatus];  break;
        }
        return PIcon;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FindGapsForCurrentMonth(out int TotalGaps, out int TotalAbs)
    {
        TotalGaps = 0;
        TotalAbs = 0;

        DateTime SDate;
        DateTime EDate;
        DTCs.FindMonthDates(DTCs.FindCurrentYear(), DTCs.FindCurrentMonth(), out SDate, out EDate);

        DataTable DT = DBCs.FetchData(" SELECT MsmDays_Absent_WithoutVac, MsmGapDur_WithoutExc From MonthSummary WHERE EmpID = @P1 AND CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@P2,103) ", new string[] { pgCs.LoginEmpID, SDate.ToString("dd/MM/yyyy") });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            TotalGaps = Convert.ToInt32(DT.Rows[0]["MsmGapDur_WithoutExc"].ToString());
            TotalAbs  = Convert.ToInt32(DT.Rows[0]["MsmDays_Absent_WithoutVac"].ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region grdData GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (DTCs.IsGregDateType(Session["DateType"])) { e.Row.Cells[2].Visible = false; } else { e.Row.Cells[1].Visible = false; }
        e.Row.Cells[12].Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strGDate   = DTCs.FormatGreg(((DataRowView)e.Row.DataItem)["DayGDate"].ToString(), "dd/MM/yyyy");
            string Status     = ((DataRowView)e.Row.DataItem)["Status"].ToString();
            string StatusName = ((DataRowView)e.Row.DataItem)["StatusName"].ToString();
            string AnyStatus  = ((DataRowView)e.Row.DataItem)["AnyStatus"].ToString();
            string ShiftID    = ((DataRowView)e.Row.DataItem)["SsmShift"].ToString();


            string SVisible = ((DataRowView)e.Row.DataItem)["SVisible"].ToString();
            string SEnabled = ((DataRowView)e.Row.DataItem)["SEnabled"].ToString();
            string SImage   = ((DataRowView)e.Row.DataItem)["SImage"].ToString();
            string SToolTip = ((DataRowView)e.Row.DataItem)["SToolTip"].ToString();
            string RVisible = ((DataRowView)e.Row.DataItem)["RVisible"].ToString();
            string RImage   = ((DataRowView)e.Row.DataItem)["RImage"].ToString();

            ImageButton StatusButton    = (ImageButton)e.Row.Cells[10].Controls[1];
            ImageButton StatusReqButton = (ImageButton)e.Row.Cells[11].Controls[1];
            
            e.Row.Cells[9].Text = StatusName;

            StatusButton.Visible     = (SVisible == "T") ? true : false;
            StatusButton.Enabled     = (SEnabled == "T") ? true : false;
            StatusButton.ImageUrl    = SImage;
            StatusButton.ToolTip     = SToolTip;
            StatusReqButton.Visible  = (RVisible == "T") ? true : false;
            StatusReqButton.ImageUrl = RImage;

            if (Status == "A" )
            {
                int ReqStatus = 3;
                bool isESHFound = FoundShiftExcuseRequest(strGDate, ShiftID, out ReqStatus);
                if (isESHFound)
                {
                    StatusReqButton.ImageUrl = FindIconStatus("EmpRequest", ReqStatus);
                    if (ReqStatus != 2)
                    {
                        StatusButton.Enabled = false;
                        StatusButton.ToolTip = "";
                        StatusButton.ImageUrl = FindIconStatus("SendRequest", 0);
                    }
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case ("SendVacDay_Command"):
                {
                    string[] Argument = e.CommandArgument.ToString().Split('-');
                    string DateDay   = Argument[0];
                    string AnyStatus = Argument[1];
                    string ShiftID   = Argument[2];

                    if (AnyStatus == "D")
                    {
                        lblNamePopup.Text = General.Msg("Absent Day Request", "طلب غياب يوم");
                        ifrmPopup.Attributes.Add("src", "VacationRequest2.aspx?ID=" + DateDay + "&Type=ALL");
                    }
                    else if (AnyStatus == "S") 
                    {
                        lblNamePopup.Text = General.Msg("Shift Excuse Request", "طلب استئذان وردية");
                        ifrmPopup.Attributes.Add("src", "ShiftExcuseRequest2.aspx?ID=" + DateDay + "&SID=" + ShiftID + "");
                    }

                    Session["ERSRefreshMonth"] = ddlMonth.SelectedValue;
                    Session["ERSRefreshYear"]  = ddlYear.SelectedValue;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup('');", true);
                }

                break;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FoundAbsentDayRequest(DateTime GDate, out string ReqBy, out string ReqType, out int ReqStatus)
    {
        bool found = false;
        ReqBy      = "";
        ReqType    = "";
        ReqStatus  = 3;

        DataTable ReqDT = new DataTable();

        bool ReqPermVac = pgCs.GetRequestPerm(ViewState["ReqHT"], "VAC");
        bool ReqPermCom = pgCs.GetRequestPerm(ViewState["ReqHT"], "COM");
        bool ReqPermJob = pgCs.GetRequestPerm(ViewState["ReqHT"], "JOB");
        try
        {
            if (ReqPermVac || ReqPermCom || ReqPermJob)
            {
                ReqDT = DBCs.FetchData(" SELECT RetID, ErqReqStatus FROM EmpRequest WHERE @P1 BETWEEN ErqStartDate AND ErqEndDate AND EmpID = @P2 AND RetID IN ('VAC','COM','JOB') ORDER By ErqID DESC ", new string[] { GDate.ToString(),pgCs.LoginEmpID });
                if (!DBCs.IsNullOrEmpty(ReqDT))
                {
                    found = true;
                    ReqBy = "EMP";
                    ReqType = ReqDT.Rows[0]["RetID"].ToString();
                    ReqStatus = Convert.ToInt32(ReqDT.Rows[0]["ErqReqStatus"].ToString()); // 0 Wait // 1 Approve // 2 Reject
                }
            }
            
            if(!found || ReqStatus == 2 || ReqStatus == 0)
            {
                DataTable VDT = DBCs.FetchData(" SELECT VtpID FROM EmpVacRel WHERE ISNULL(EvrDeleted,0) = 0 AND EmpID = @P1 AND @P2 BETWEEN EvrStartDate AND EvrEndDate ", new string[] { pgCs.LoginEmpID, GDate.ToString() });
                if (!DBCs.IsNullOrEmpty(VDT))
                {
                    found = true;
                    ReqBy = "USR";
                    
                    DataTable VTDT = DBCs.FetchData(" SELECT ISNULL(VtpCategory,'VAC') VtpCategory FROM VacationType WHERE VtpID = @P1 ", new string[] { VDT.Rows[0]["VtpID"].ToString() });
                    if (!DBCs.IsNullOrEmpty(VTDT))  { ReqType = VTDT.Rows[0]["VtpCategory"].ToString(); }
                }
            }
        }
        catch (Exception e1) { }

        return found;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FoundShiftExcuseRequest(string GDate, string ShiftID, out int ReqStatus)
    {
        bool found = false;
        ReqStatus  = 3;

        try
        {
            if (pgCs.GetRequestPerm(ViewState["ReqHT"], "ESH"))
            {
                DataTable ReqDT = DBCs.FetchData(" SELECT ErqReqStatus FROM EmpRequest WHERE RetID ='ESH' AND CONVERT(VARCHAR(12),ErqStartDate,103)= CONVERT(VARCHAR(12),@P1,103) AND EmpID = @P2 AND ShiftID = @P3 ORDER By ErqID DESC", new string[] { GDate, pgCs.LoginEmpID, ShiftID });
                if (!DBCs.IsNullOrEmpty(ReqDT))
                {
                    found = true;
                    ReqStatus = Convert.ToInt32(ReqDT.Rows[0]["ErqReqStatus"].ToString()); // 0 Wait // 1 Approve // 2 Reject
                }
            }
        }
        catch (Exception e1) { }

        return found;
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

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