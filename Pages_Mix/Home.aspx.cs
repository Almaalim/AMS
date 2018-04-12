﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using InfoSoftGlobal;
using FusionCharts.Charts;

public partial class Home : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun DTCs = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            UIChartsTypeShow(ddlTypeChartsFilter.SelectedValue);
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                //string url  = HttpContext.Current.Request.Url.AbsoluteUri;
                //string path = HttpContext.Current.Request.Url.AbsolutePath;
                //string host = HttpContext.Current.Request.Url.Host;

                /*** Common Code ************************************/
                /*** Check AMS License ***/
                pgCs.CheckAMSLicense();
                /*** Common Code ************************************/

                /*** Charts *****************************************/
                string DepList = "";
                if (pgCs.LoginType == "USR")
                {
                    DepList = pgCs.DepList;
                }
                else if (pgCs.LoginType == "EMP")
                {
                    DataTable DT = DBCs.FetchData(" SELECT DepID FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
                    if (!DBCs.IsNullOrEmpty(DT)) { DepList = DT.Rows[0]["DepID"].ToString(); }
                }

                CtrlCs.PopulateDepartmentList(ref ddlDepChartsFilter, DepList, pgCs.Version, General.Msg("All", "الكل"));
                FillEmployeeList("0", DepList);
                if (pgCs.LoginType == "EMP")
                {
                    ddlEmpChartsFilter.SelectedIndex = ddlEmpChartsFilter.Items.IndexOf(ddlEmpChartsFilter.Items.FindByValue(pgCs.LoginEmpID));
                    DivList.Visible = false;
                }

                UIChartsTypeShow("M");
                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);
                FillTodayStatus();
                btnChartsFilter_Click(null, null);
                /*** Charts *****************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillEmployeeList(string DepID, string DepList)
    {
        ddlEmpChartsFilter.Items.Clear();

        string Condition = "WHERE DepID = " + DepID + "";
        if (DepID == "0" || DepID == "All") { Condition = "WHERE DepID IN (" + DepList + ")"; }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM spActiveEmployeeView " + Condition));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            CtrlCs.PopulateDDL(ddlEmpChartsFilter, DT, General.Msg("EmpNameEn", "EmpNameAr"), "EmpID", General.Msg("All", "الكل"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getDepChartsFilter()
    {
        string DepID = ddlDepChartsFilter.SelectedValue;
        if (DepID == General.Msg("All","الكل")) { DepID = "All"; }

        return DepID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getEmpChartsFilter()
    {
        string EmpID = ddlEmpChartsFilter.SelectedValue;
        if (EmpID == General.Msg("All","الكل")) { EmpID = "All"; }

        return EmpID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlDepChartsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeList(getDepChartsFilter(), pgCs.DepList);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlTypeChartsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        UIChartsTypeShow(ddlTypeChartsFilter.SelectedValue);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIChartsTypeShow(string TypeID)
    {
        if (TypeID == "D")
        {
            DivMonth1.Visible = DivMonth2.Visible = DivYear1.Visible = DivYear2.Visible = false;
            DivDay1.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup('" + DivDay2.ClientID + "');", true);
            calDate.SetEnabled(true);
        }
        else
        {
            DivMonth1.Visible = DivMonth2.Visible = DivYear1.Visible = DivYear2.Visible = true;
            DivDay1.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hidePopup('" + DivDay2.ClientID + "');", true);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnChartsFilter_Click(object sender, EventArgs e)
    {
        string DepID = getDepChartsFilter();
        string EmpID = getEmpChartsFilter();
        string Type = ddlTypeChartsFilter.SelectedValue;
        string Date = calDate.getGDateDBFormat();
        string Month = ddlMonth.SelectedValue;
        string Year = ddlYear.SelectedValue;
        string CALENDAR_TYPE = (pgCs.DateType == "Hijri") ? "H" : "G";

        DateTime SDATE = new DateTime();
        DateTime EDATE = new DateTime();
        DTCs.FindMonthDates(Year, Month, out SDATE, out EDATE);

        SqlCommand cmd = new SqlCommand();
        StringBuilder CQ = new StringBuilder();

        if (Type == "M")
        {
            CQ.Append(" SELECT SUM(MsmShiftDuration) SumShiftDuration, SUM(MsmWorkDuration) SumWorkDuration, SUM(MsmBeginLate) SumBeginLateDuration ");
            CQ.Append(" , SUM(MsmDays_Work) SumWorkDays, SUM(MsmDays_Absent_WithoutVac) SumAbsentDays ");
            CQ.Append(" , SUM(MsmGapDur_WithoutExc) SumGapsDuration ");
            CQ.Append(" FROM MonthSummary WHERE ");
        }
        else if (Type == "D")
        {
            CQ.Append(" SELECT SUM(DsmShiftDuration) SumShiftDuration, SUM(DsmWorkDuration) SumWorkDuration, SUM(DsmBeginLate) SumBeginLateDuration ");
            CQ.Append(" , COUNT(CASE WHEN DsmStatus IN ('P','PE','UE','A','CM','JB') THEN DsmStatus ELSE NULL END) SumWorkDays ");
            CQ.Append(" , COUNT(CASE WHEN DsmStatus IN ('A') THEN DsmStatus ELSE NULL END) SumAbsentDays ");
            CQ.Append(" , SUM(DsmGapDur_WithoutExc) SumGapsDuration ");
            CQ.Append(" FROM DaySummary WHERE ");
        }

        if (EmpID != "All") { CQ.Append(" EmpID = @EmpID"); cmd.Parameters.AddWithValue("@EmpID", EmpID); }
        else
        {
            if (DepID == "All") { CQ.Append(" EmpID IN (SELECT EmpID FROM spActiveEmployeeView WHERE DepID IN (" + pgCs.DepList + "))"); }
            else { CQ.Append(" EmpID IN (SELECT EmpID FROM spActiveEmployeeView WHERE DepID = @DepID)"); cmd.Parameters.AddWithValue("@DepID", DepID); }
        }

        if (Type == "M")
        {
            CQ.Append(" AND MsmCalendar = @MsmCalendar AND CONVERT(VARCHAR(10),MsmStartDate,101) = CONVERT(VARCHAR(10),@MsmStartDate,101) AND CONVERT(VARCHAR(10),MsmEndDate,101) = CONVERT(VARCHAR(10),@MsmEndDate,101) ");
            cmd.Parameters.AddWithValue("@MsmCalendar", CALENDAR_TYPE);
            cmd.Parameters.AddWithValue("@MsmStartDate", SDATE);
            cmd.Parameters.AddWithValue("@MsmEndDate", EDATE);
        }
        else if (Type == "D")
        {
            CQ.Append(" AND CONVERT(VARCHAR(10),DsmDate,101) = CONVERT(VARCHAR(10),@DsmDate,101) ");
            cmd.Parameters.AddWithValue("@DsmDate", Date);
        }
        
        cmd.CommandText = CQ.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);

        FillChartWorkDurtion(ChartDT);
        FillChartBeginLateDurtion(ChartDT);
        bool EmpDay = (Type == "D" && EmpID != "All") ? true : false;
        FillChartAbsentDays(EmpDay, ChartDT, EmpID, Date);
        FillChartDurations(ChartDT);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartWorkDurtion(DataTable DT)
    {
        string titel = "";

        object SumShiftDuration = "0";
        object SumWorkDuration = "0";
        string SumNotWorkDuration = "1";

        object ShowSumShiftDuration = "0";
        object ShowSumWorkDuration = "0";
        string ShowSumNotWorkDuration = "0";

        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataRow DR = DT.Rows[0];

            if (!GenCs.IsNullOrEmptyDB(DR["SumShiftDuration"]) && !GenCs.IsNullOrEmptyDB(DR["SumWorkDuration"]))
            {
                SumShiftDuration = ShowSumShiftDuration = DR["SumShiftDuration"];
                SumWorkDuration = ShowSumWorkDuration = DR["SumWorkDuration"]; 
                SumNotWorkDuration = ShowSumNotWorkDuration = (Convert.ToInt32(SumShiftDuration) - Convert.ToInt32(SumWorkDuration)).ToString();
            }
        }

        titel = General.Msg("Total work hours required", "مجموع ساعات العمل المطلوبة");

        StringBuilder strXML = new StringBuilder();
        strXML.AppendFormat("<?xml version='1.0' encoding='UTF-8'?>");
        
        strXML.AppendFormat("<chart caption='{0}' subcaption='{1}'  palette='4'  exportEnabled='1' rotateYAxisName='0' showValues='1' showLabels='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='1' labelDisplay='wrap' showLegend='1' pieRadius='80' hasRTLText='1' logoURL='../images/LogoEn.png' logoAlpha='40' logoScale='50' logoPosition='TL'>", titel, DisplayFun.GrdDisplayDuration(ShowSumShiftDuration));

        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}'/>", General.Msg("Total actual working hours", "مجموع ساعات العمل الفعلية"), SumWorkDuration.ToString(), DisplayFun.GrdDisplayDuration(ShowSumWorkDuration));
        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' isSliced ='1'/>", General.Msg("Total hours of non - working", "مجموع ساعات عدم العمل"), SumNotWorkDuration, DisplayFun.GrdDisplayDuration(ShowSumNotWorkDuration));
        strXML.Append("</chart>");

        string StrChart = "";
        Chart ChartWorkDurtion = new Chart("pie2d", " ", "100%", "350", "xml", strXML.ToString());
        StrChart = ChartWorkDurtion.Render();
        pnlChartWorkDurtion.Controls.Clear();
        pnlChartWorkDurtion.Controls.Add(new LiteralControl(StrChart));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartBeginLateDurtion(DataTable DT)
    {
        string titel = "";

        object SumWorkDuration = "0";
        object SumBeginLateDuration = "0";
        string SumNotDuration = "1";

        object ShowSumWorkDuration = "0";
        object ShowSumBeginLateDuration = "0";
        string ShowSumNotDuration = "0";

        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataRow DR = DT.Rows[0];
            if (!GenCs.IsNullOrEmptyDB(DR["SumWorkDuration"]) && !GenCs.IsNullOrEmptyDB(DR["SumBeginLateDuration"]))
            {
                SumWorkDuration = ShowSumWorkDuration = DR["SumWorkDuration"];
                SumBeginLateDuration = ShowSumBeginLateDuration = DR["SumBeginLateDuration"];

                SumNotDuration = ShowSumNotDuration = (Convert.ToInt32(SumWorkDuration) - Convert.ToInt32(SumBeginLateDuration)).ToString();
            }
        }

        titel = General.Msg("Total actual hours required", "مجموع ساعات العمل الفعلية");

        StringBuilder strXML = new StringBuilder();
        strXML.AppendFormat("<?xml version='1.0' encoding='UTF-8'?>");
        
        strXML.AppendFormat("<chart caption='{0}' subcaption='{1}'  palette='4'  exportEnabled='1' rotateYAxisName='0' showValues='1' showLabels='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='1' labelDisplay='wrap' showLegend='1' pieRadius='80' hasRTLText='1'>", titel, DisplayFun.GrdDisplayDuration(ShowSumWorkDuration));

        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' isSliced ='1' color ='FF0000'/>", General.Msg("Total hours of delay", "مجموع ساعات التأخير"), SumBeginLateDuration,  DisplayFun.GrdDisplayDuration(ShowSumBeginLateDuration));
        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' color ='009900'/>",General.Msg("Total working hours", "مجموع ساعات العمل"), SumNotDuration, DisplayFun.GrdDisplayDuration(ShowSumNotDuration));
        strXML.Append("</chart>");

        string StrChart = "";
        Chart ChartBeginLateDurtion = new Chart("pie2d", "  ", "100%", "350", "xml", strXML.ToString());
        StrChart = ChartBeginLateDurtion.Render();
        pnlChartBeginLateDurtion.Controls.Clear();
        pnlChartBeginLateDurtion.Controls.Add(new LiteralControl(StrChart));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartAbsentDays(bool EmpDay, DataTable DT, string EmpID, string Date)
    {
        string titel = "";
        string Subtitel = "";

        object SumWorkDays = "0";
        object SumAbsentDays = "1";
        object DsmStatus = "N";
        string SumNotDuration = "0";

        object ShowSumWorkDays = "0";
        object ShowSumAbsentDays = "0";
        string ShowSumNotDuration = "0";

        DataTable CDT = new DataTable();
        if (!EmpDay)
        { CDT = DT; }
        else
        {
            StringBuilder DQ = new StringBuilder();
            DQ.Append(" SELECT DsmStatus  ");
            DQ.Append(" , (CASE WHEN DsmStatus IN ('P','PE','UE','A','CM','JB','WE','H') THEN 1 ELSE 0 END) SumWorkDays ");
            DQ.Append(" , (CASE WHEN DsmStatus IN ('A') THEN 1 ELSE 0 END) SumAbsentDays ");
            DQ.Append(" FROM DaySummary WHERE EmpID = @P1 AND CONVERT(VARCHAR(10),DsmDate,101) = CONVERT(VARCHAR(10),@P2,101) ");
            CDT = DBCs.FetchData(DQ.ToString(), new string[] { EmpID, Date });
        }

        if (!DBCs.IsNullOrEmpty(CDT))
        {
            DataRow DR = CDT.Rows[0];
            if (!GenCs.IsNullOrEmptyDB(DR["SumWorkDays"]) && !GenCs.IsNullOrEmptyDB(DR["SumAbsentDays"]))
            {
                if (!EmpDay)
                {
                    SumWorkDays = ShowSumWorkDays = DR["SumWorkDays"];
                    SumAbsentDays = ShowSumAbsentDays = DR["SumAbsentDays"];
                    SumNotDuration = ShowSumNotDuration = (Convert.ToInt32(SumWorkDays) - Convert.ToInt32(SumAbsentDays)).ToString();
                }
                else
                {
                    SumWorkDays = ShowSumWorkDays = DR["SumWorkDays"];
                    SumAbsentDays = ShowSumAbsentDays = DR["SumAbsentDays"];
                    DsmStatus = DR["DsmStatus"];
                }
            }
        }

        if (!EmpDay) { titel = General.Msg("Total working days required", "مجموع أيام العمل المطلوبة"); } else { titel = General.Msg("Status of the day", "حالة اليوم"); }
        if (!EmpDay) { Subtitel = SumWorkDays.ToString(); } else { Subtitel = DisplayFun.GrdDisplayDayStatus(DsmStatus, pgCs.Version); }

        StringBuilder strXML = new StringBuilder();
        strXML.AppendFormat("<?xml version='1.0' encoding='UTF-8'?>");
        
        strXML.AppendFormat("<chart caption='{0}' subcaption='{1}'  palette='4' exportEnabled='1' rotateYAxisName='0' showValues='1' showLabels='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='{2}' labelDisplay='wrap' showLegend='1' pieRadius='80' hasRTLText='1'>", titel, Subtitel,(!EmpDay) ? "1" : "0");

        if (!EmpDay)
        {
            strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' isSliced ='1' color='e6ff1e'/>", General.Msg("Total days of absence", "مجموع أيام الغياب"), SumAbsentDays,  ShowSumAbsentDays);
            strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' color ='b0e0e6'/>", General.Msg("Total working days", "مجموع أيام العمل"), SumNotDuration, ShowSumNotDuration);
        }
        else
        {
            strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' isSliced ='1' color='e6ff1e'/>", DisplayFun.GrdDisplayDayStatus(DsmStatus, pgCs.Version), SumAbsentDays, "");
            strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}' color ='b0e0e6'/>",DisplayFun.GrdDisplayDayStatus(DsmStatus, pgCs.Version), SumNotDuration, "");
        }
        strXML.Append("</chart>");

        string StrChart = "";
        Chart ChartBeginLateDurtion = new Chart("pie2d", "   ", "100%", "350", "xml", strXML.ToString());
        StrChart = ChartBeginLateDurtion.Render();
        pnlChartAbsentDays.Controls.Clear();
        pnlChartAbsentDays.Controls.Add(new LiteralControl(StrChart));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartDurations(DataTable DT)
    {
        string titel = "";

        object SumShiftDuration = "0";
        object SumWorkDuration = "0";
        object SumGapsDuration = "0";

        object ShowSumShiftDuration = "0";
        object ShowSumWorkDuration = "0";
        object ShowSumGapsDuration = "0";

        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataRow DR = DT.Rows[0];
            if (!GenCs.IsNullOrEmptyDB(DR["SumShiftDuration"]) && !GenCs.IsNullOrEmptyDB(DR["SumWorkDuration"]) && !GenCs.IsNullOrEmptyDB(DR["SumGapsDuration"]))
            {
                SumShiftDuration = ShowSumShiftDuration = DR["SumShiftDuration"];
                SumWorkDuration = ShowSumWorkDuration = DR["SumWorkDuration"];
                SumGapsDuration = ShowSumGapsDuration = DR["SumGapsDuration"];
            }
        }

        titel = General.Msg("Total working periods", "مجموع فترات العمل");

        StringBuilder strXML = new StringBuilder();
        strXML.AppendFormat("<chart caption='{0}' palette='1'  exportEnabled='1' xAxisName='{1}' yAxisName='{2}' rotateYAxisName='0' showValues='1' decimals='0' showborder='0' formatNumberScale='0' labelDisplay='Stagger' showLegend='1'>", titel,  General.Msg("Total", "المجموع"), General.Msg("Hour", "ساعة"));

        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}'/>", General.Msg("work hours required", "ساعات العمل المطلوبة"), DisplayDuration(SumShiftDuration), DisplayFun.GrdDisplayDuration(ShowSumShiftDuration));
        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}'/>", General.Msg("actual hours required", "ساعات العمل الفعلية"), DisplayDuration(SumWorkDuration), DisplayFun.GrdDisplayDuration(ShowSumWorkDuration));
        strXML.AppendFormat("<set label='{0}' value='{1}' displayValue='{2}'/>", General.Msg("hours of gaps without Excuse", "ساعات الثغرات بدون إستئذان"), DisplayDuration(SumGapsDuration), DisplayFun.GrdDisplayDuration(ShowSumGapsDuration));

        strXML.Append("</chart>");

        string outPut = "";
        Chart sales = new Chart("column2d", "    ", "100%", "350", "xml", strXML.ToString());
        outPut = sales.Render();
        pnlChartDurations.Controls.Clear();
        pnlChartDurations.Controls.Add(new LiteralControl(outPut));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string DisplayDuration(object pDuration)
    {
        try
        {
            if (pDuration == DBNull.Value) { return "0"; }
            if (string.IsNullOrEmpty(pDuration.ToString())) { return "0"; }

            TimeSpan tsGen = TimeSpan.FromSeconds(Convert.ToDouble(pDuration));
            int Hours = tsGen.Hours;
            if (tsGen.Days > 0) { Hours = (tsGen.Days * 24) + tsGen.Hours; }
            return Hours.ToString("00");// +tsGen.Minutes.ToString("00") + tsGen.Seconds.ToString("00");
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "00:00:00";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillTodayStatus()
    {
        string titel = General.Msg("Employees Status Today", "حالة الموظفين اليوم"); 
        DataTable TDT = new DataTable();
        TDT = DBCs.FetchProcedureData("FindTodayEmployeeStatus_Home", new string[] { "@DepIDs" }, new string[] { pgCs.DepList });

        if (!DBCs.IsNullOrEmpty(TDT))
        {
            string Subtitel =  General.Msg("Number of Employees","عدد الموظفين") + ": " + TDT.Rows[0]["EmpAllCount"].ToString();
            string Deftitel =  General.Msg("Attendees","الحضور") + " " + TDT.Rows[0]["EmpPCount"].ToString();
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' subcaption='{1}' showborder='0' bgAlpha='0' canvasbgAlpha='0' use3dlighting='0' enablesmartlabels='0' startingangle='310' showlabels='0' showpercentvalues='0' showlegend='1' legendPosition='right' legendIconScale='1.5' legendBorderThickness='0' legendBorderAlpha='0' legendBgAlpha='0' defaultcenterlabel='{2}' centerlabel='$label  $value' centerlabelbold='1' showtooltip='0' decimals='0' usedataplotcolorforlabels='1' theme='fint' pieRadius='100'>", titel, Subtitel, Deftitel);
            strXML.AppendFormat("<set label='{0}' value='{1}'/>",General.Msg("Attendees","الحضور"), TDT.Rows[0]["EmpPCount"].ToString());
            strXML.AppendFormat("<set label='{0}' value='{1}'/>",General.Msg("Absence","الغياب"), TDT.Rows[0]["EmpACount"].ToString());
            strXML.AppendFormat("<set label='{0}' value='{1}'/>",General.Msg("Vacations","الإجازات"), TDT.Rows[0]["EmpVCount"].ToString());
            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("doughnut2d", "      ", "100%", "270", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            litTodayStatus.Text = StrChart;

            StringBuilder strXML2 = new StringBuilder();
            strXML2.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' bgAlpha='0' numbersuffix='%' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' chartbottommargin='25' theme='fint'>", General.Msg("Attendees","الحضور"), TDT.Rows[0]["EmpPCount"].ToString());
            strXML2.Append("<colorrange>");
            strXML2.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00' alpha='25'/>");
            strXML2.Append("<color minvalue='25' maxvalue='50'  code='#e44a00' alpha='25'/>");
            strXML2.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19' alpha='25'/>");
            strXML2.Append("<color minvalue='75' maxvalue='100' code='#6baa01' alpha='25'/>");
            strXML2.Append("</colorrange>");
            strXML2.AppendFormat("<value>{0}</value>",(Convert.ToInt64(TDT.Rows[0]["EmpPCount"]) / Convert.ToInt64(TDT.Rows[0]["EmpAllCount"]) * 100).ToString());
            strXML2.Append("<target>100</target>");
            strXML2.Append("</chart>");

            string StrChart2 = "";
            Chart ChartObj2 = new Chart("hbullet", "           ", "100%", "90", "xml", strXML2.ToString());
            StrChart2 = ChartObj2.Render();
            litPresentEmp.Text = StrChart2;


            StringBuilder strXML3 = new StringBuilder();
            strXML3.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' bgAlpha='0' numbersuffix='%' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' chartbottommargin='25' theme='fint'>", General.Msg("Absence","الغياب"), TDT.Rows[0]["EmpACount"].ToString());
            strXML3.Append("<colorrange>");
            strXML3.Append("<color minvalue='0'  maxvalue='25'  code='#6baa01' alpha='25'/>");
            strXML3.Append("<color minvalue='25' maxvalue='50'  code='#f8bd19' alpha='25'/>");
            strXML3.Append("<color minvalue='50' maxvalue='75'  code='#e44a00' alpha='25'/>");
            strXML3.Append("<color minvalue='75' maxvalue='100' code='#e44a00' alpha='25'/>");
            strXML3.Append("</colorrange>");
            strXML3.AppendFormat("<value>{0}</value>",(Convert.ToInt64(TDT.Rows[0]["EmpACount"]) / Convert.ToInt64(TDT.Rows[0]["EmpAllCount"]) * 100).ToString());
            strXML3.Append("<target>100</target>");
            strXML3.Append("</chart>");

            string StrChart3 = "";
            Chart ChartObj3 = new Chart("hbullet", "            ", "100%", "90", "xml", strXML3.ToString());
            StrChart3 = ChartObj3.Render();
            litAbsenceEmp.Text = StrChart3;

            StringBuilder strXML4 = new StringBuilder();
            strXML4.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' bgAlpha='0' numbersuffix='%' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' chartbottommargin='25' theme='fint'>", General.Msg("Vacations","الإجازات"), TDT.Rows[0]["EmpVCount"].ToString());
            strXML4.Append("<colorrange>");
            strXML4.Append("<color minvalue='0'  maxvalue='25'  code='#6baa01' alpha='25'/>");
            strXML4.Append("<color minvalue='25' maxvalue='50'  code='#f8bd19' alpha='25'/>");
            strXML4.Append("<color minvalue='50' maxvalue='75'  code='#e44a00' alpha='25'/>");
            strXML4.Append("<color minvalue='75' maxvalue='100' code='#e44a00' alpha='25'/>");
            strXML4.Append("</colorrange>");
            strXML4.AppendFormat("<value>{0}</value>",(Convert.ToInt64(TDT.Rows[0]["EmpVCount"]) / Convert.ToInt64(TDT.Rows[0]["EmpAllCount"]) * 100).ToString());
            strXML4.Append("<target>100</target>");
            strXML4.Append("</chart>");

            string StrChart4 = "";
            Chart ChartObj4 = new Chart("hbullet", "             ", "100%", "90", "xml", strXML4.ToString());
            StrChart4 = ChartObj4.Render();
            litVacationsEmp.Text = StrChart4;


      //      SqlCommand cmd = new SqlCommand();
      //      StringBuilder Q = new StringBuilder();

      //      Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn, D.DsmDate AS DsmDate, ISNULL(D.EmpActualWork,0) EmpActualWork, ISNULL(D.EmpActualWorkSecond,0) EmpActualWorkSecond ");
      //      Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		    //Q.Append("         (SELECT EmpID, DsmDate, DsmWorkDuration AS EmpActualWorkSecond, CAST(DsmWorkDuration AS DECIMAL)/ 60 / 60 AS EmpActualWork ");
		    //Q.Append("          FROM dbo.DaySummaryInfoView ");
		    //Q.Append("          WHERE DsmDate BETWEEN @Param_FromDate AND @Param_ToDate AND DsmStatus NOT IN ('WE','N') ");
      //      Q.Append("          AND EmpID IN (" + Param_OneEmpID + ")) D "); 
	     //   Q.Append("     ON E.EmpID = D.EmpID ");
      //      Q.Append(" WHERE E.EmpID IN (" + Param_OneEmpID + ") "); 
      //      Q.Append(" ORDER BY E.EmpID,D.DsmDate ");

        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}