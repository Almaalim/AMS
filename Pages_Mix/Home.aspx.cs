using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using InfoSoftGlobal;
using FusionCharts.Charts;
using System.Web;
using System.IO;

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
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                //string url  = HttpContext.Current.Request.Url.AbsoluteUri;
                //string path = HttpContext.Current.Request.Url.AbsolutePath;
                //string host = HttpContext.Current.Request.Url.Host;

                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();
                /*** Common Code ************************************/

                /*** Charts *****************************************/
                DTCs.MonthPopulateList(ref ddlMonth);
                lblYear.Text = DTCs.FindCurrentYear();
                
                if (pgCs.LoginType == "USR") { USR_FillTodayStatus();  USR_FillMonthlyStatus(); } else if (pgCs.LoginType == "EMP") { EMP_FillTodayStatus(); EMP_FillMonthlyStatus(); }
                /*** Charts *****************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pgCs.LoginType == "USR") { USR_FillMonthlyStatus(); } else if (pgCs.LoginType == "EMP") { EMP_FillMonthlyStatus(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnChartsFilter_Click(object sender, EventArgs e)
    {
        //string DepID = getDepChartsFilter();
        //string EmpID = getEmpChartsFilter();
        //string Type = ddlTypeChartsFilter.SelectedValue;
        //string Date = calDate.getGDateDBFormat();
        //string Month = ddlMonth.SelectedValue;
        //string Year = ddlYear.SelectedValue;
        //string CALENDAR_TYPE = (pgCs.DateType == "Hijri") ? "H" : "G";

        //DateTime SDATE = new DateTime();
        //DateTime EDATE = new DateTime();
        //DTCs.FindMonthDates(Year, Month, out SDATE, out EDATE);

        //SqlCommand cmd = new SqlCommand();
        //StringBuilder CQ = new StringBuilder();

        //if (Type == "M")
        //{
        //    CQ.Append(" SELECT SUM(MsmShiftDuration) SumShiftDuration, SUM(MsmWorkDuration) SumWorkDuration, SUM(MsmBeginLate) SumBeginLateDuration ");
        //    CQ.Append(" , SUM(MsmDays_Work) SumWorkDays, SUM(MsmDays_Absent_WithoutVac) SumAbsentDays ");
        //    CQ.Append(" , SUM(MsmGapDur_WithoutExc) SumGapsDuration ");
        //    CQ.Append(" FROM MonthSummary WHERE ");
        //}
        //else if (Type == "D")
        //{
        //    CQ.Append(" SELECT SUM(DsmShiftDuration) SumShiftDuration, SUM(DsmWorkDuration) SumWorkDuration, SUM(DsmBeginLate) SumBeginLateDuration ");
        //    CQ.Append(" , COUNT(CASE WHEN DsmStatus IN ('P','PE','UE','A','CM','JB') THEN DsmStatus ELSE NULL END) SumWorkDays ");
        //    CQ.Append(" , COUNT(CASE WHEN DsmStatus IN ('A') THEN DsmStatus ELSE NULL END) SumAbsentDays ");
        //    CQ.Append(" , SUM(DsmGapDur_WithoutExc) SumGapsDuration ");
        //    CQ.Append(" FROM DaySummary WHERE ");
        //}

        //if (EmpID != "All") { CQ.Append(" EmpID = @EmpID"); cmd.Parameters.AddWithValue("@EmpID", EmpID); }
        //else
        //{
        //    if (DepID == "All") { CQ.Append(" EmpID IN (SELECT EmpID FROM spActiveEmployeeView WHERE DepID IN (" + pgCs.DepList + "))"); }
        //    else { CQ.Append(" EmpID IN (SELECT EmpID FROM spActiveEmployeeView WHERE DepID = @DepID)"); cmd.Parameters.AddWithValue("@DepID", DepID); }
        //}

        //if (Type == "M")
        //{
        //    CQ.Append(" AND MsmCalendar = @MsmCalendar AND CONVERT(VARCHAR(10),MsmStartDate,101) = CONVERT(VARCHAR(10),@MsmStartDate,101) AND CONVERT(VARCHAR(10),MsmEndDate,101) = CONVERT(VARCHAR(10),@MsmEndDate,101) ");
        //    cmd.Parameters.AddWithValue("@MsmCalendar", CALENDAR_TYPE);
        //    cmd.Parameters.AddWithValue("@MsmStartDate", SDATE);
        //    cmd.Parameters.AddWithValue("@MsmEndDate", EDATE);
        //}
        //else if (Type == "D")
        //{
        //    CQ.Append(" AND CONVERT(VARCHAR(10),DsmDate,101) = CONVERT(VARCHAR(10),@DsmDate,101) ");
        //    cmd.Parameters.AddWithValue("@DsmDate", Date);
        //}
        
        //cmd.CommandText = CQ.ToString();

        //DataTable ChartDT = new DataTable();
        //ChartDT = DBCs.FetchData(cmd);

        //FillChartWorkDurtion(ChartDT);
        //FillChartBeginLateDurtion(ChartDT);
        //bool EmpDay = (Type == "D" && EmpID != "All") ? true : false;
        //FillChartAbsentDays(EmpDay, ChartDT, EmpID, Date);
        //FillChartDurations(ChartDT);
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
        //pnlChartWorkDurtion.Controls.Clear();
        //pnlChartWorkDurtion.Controls.Add(new LiteralControl(StrChart));
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
        //pnlChartBeginLateDurtion.Controls.Clear();
        //pnlChartBeginLateDurtion.Controls.Add(new LiteralControl(StrChart));
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
        //pnlChartAbsentDays.Controls.Clear();
        //pnlChartAbsentDays.Controls.Add(new LiteralControl(StrChart));
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
        //pnlChartDurations.Controls.Clear();
        //pnlChartDurations.Controls.Add(new LiteralControl(outPut));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //<div>Name : <b>$label</b><br/>Revenue : <b>$$value</b></div>
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region EMP CHART

    public void EMP_FillTodayStatus()
    {
        string titel = General.Msg("Status Today", "حالة اليوم"); 
        DataTable DT = new DataTable();
        DT = DBCs.FetchProcedureData("spTodayEmployeeStatus", new string[] { "@iFilter", "@iIDList" }, new string[] { "Emp", pgCs.LoginEmpID });

        if (!DBCs.IsNullOrEmpty(DT))
        {
            string AttendanceState = DT.Rows[0]["AttendanceState"].ToString();
            string StatusName = "";
            string Color = "";
            switch (AttendanceState.Trim())
            {
                case "P"  : StatusName = General.Msg("Present" ,"حاضر");            Color = "#008000"; break;
                case "A"  : StatusName = General.Msg("Absent"  ,"غائب");            Color = "#FF0000"; break;
                case "WE" : StatusName = General.Msg("Week End"  ,"عطلة الأسبوع");   Color = "#e6ff1e"; break;
                case "N"  : StatusName = General.Msg("No Work" ,"لا يوجد عمل");      Color = "#e6ff1e"; break;
                case "H"  : StatusName = General.Msg("Holiday" ,"عطلة");             Color = "#b0e0e6"; break;
                case "PE" : StatusName = General.Msg("Vacation" ,"إجازة");           Color = "#b0e0e6"; break;
                case "UE" : StatusName = General.Msg("Vacation" ,"إجازة");           Color = "#b0e0e6"; break;
                case "CM" : StatusName = General.Msg("Commission" ,"إنتداب");        Color = "#b0e0e6"; break;
                case "JB" : StatusName = General.Msg("Job Assignment" ,"مهمة عمل");  Color = "#b0e0e6"; break;
                case "E"  : StatusName = General.Msg("Excused" ,"مستأذن");           Color = "#e6ff1e"; break;
            }

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' showborder='0' labelFontSize='16' bgAlpha='0' canvasbgAlpha='0' use3dlighting='0' enablesmartlabels='0'  showlabels='1' showValues='0' showpercentvalues='0' showtooltip='0' theme='fint' pieRadius='75'>", titel);
            strXML.AppendFormat("<set label='{0}' value='100' displayValue='{1}' color='{2}'/>", StatusName, StatusName,  Color);
            strXML.Append("</chart>");

            Chart ChartObj = new Chart("pie2d", "      ", "100%", "200", "xml", strXML.ToString());
            litTodayStatus.Text = ChartObj.Render();
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            DataTable DT2 = new DataTable();
            DT2 = DBCs.FetchProcedureData("spTodayEmployeeTransactionWithworkTimeLimit", new string[] { "@EmpID" }, new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT2))
            {
                StringBuilder strXML2 = new StringBuilder();
                strXML2.AppendFormat("<chart caption='{0}' adjustDiv ='0' xaxisname='Time' yaxisname='' showXAxisValues='0' showYAxisValues='0' showDivLineValues='0' palette='1' bgAlpha='0' canvasbgAlpha='0' showborder='0' showToolTip='0' bgcolor='#ffffff' theme='fint' anchorradius='6' anchorborderthickness='2' anchorborderColor='#127fcb' anchorBgColor='#d3f7ff'>",General.Msg("Today Transaction", "حركات اليوم"));

                foreach (DataRow DR2 in DT2.Rows)
                {
                    //Int64 i = Convert.ToInt64(DTCs.TimeToInt(DR2["TrnTime"]));
                    if (DR2["TrnType"].ToString() == "SW")
                    {
                        strXML2.AppendFormat("<set label='' value='{0}' displayValue='{1}' anchorradius='10' anchorAlpha='70' anchorBorderColor='#cc3333' anchorBgColor='#ff9900'/>", 10, DisplayFun.GrdDisplayTime(DR2["TrnTime"]));
                    }
                    else if (DR2["TrnType"].ToString() == "EW")
                    {
                        strXML2.AppendFormat("<set label='' value='{0}' displayValue='{1}' anchorradius='10' anchorAlpha='70' anchorBorderColor='#cc3333' anchorBgColor='#ff9900'/>", 10, DisplayFun.GrdDisplayTime(DR2["TrnTime"]));
                    }
                    else
                    {
                        strXML2.AppendFormat("<set label='' value='{0}' displayValue='{1}' />", 10, DisplayFun.GrdDisplayTime(DR2["TrnTime"]));
                    }
                }
                strXML2.AppendFormat("</chart>");

                Chart ChartObj2 = new Chart("line", "           ", "100%", "200", "xml", strXML2.ToString());
                litPresentEmp.Text = ChartObj2.Render();
            }
            //DataTable DT2 = new DataTable();
            //DT2 = DBCs.FetchData(cmd);
            //if (!DBCs.IsNullOrEmpty(DT2))
            //{
            //    StringBuilder strXML2 = new StringBuilder();
            //    strXML2.AppendFormat("<chart caption='{0}' palette='1' showborder='0' showOpenValue='1' showCloseValue='1' showHighLowValue= '0' showToolTip='0' bgcolor='#ffffff' theme='fint'>", General.Msg("Today Transaction", "حركات اليوم"));
            //    strXML2.Append("<dataset>");

            //    foreach (DataRow DR2 in DT2.Rows)
            //    {
            //        Int64 i = Convert.ToInt64(DTCs.TimeToInt(DR2["TrnTime"]));              
            //        strXML2.AppendFormat("<set value='{0}' displayValue='{1}' />", i, DisplayFun.GrdDisplayTime(DR2["TrnTime"]));
            //    }

            //    strXML2.Append("</dataset>");
            //    strXML2.Append("</chart>");
           
            //    Chart ChartObj2 = new Chart("sparkline", "           ", "100%", "200", "xml", strXML2.ToString());
            //    litPresentEmp.Text = ChartObj2.Render();
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            //StringBuilder strXML3 = new StringBuilder();
            //strXML3.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' bgAlpha='0' numbersuffix='%' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' chartbottommargin='25' theme='fint'>", General.Msg("Absence","الغياب"), TDT.Rows[0]["EmpACount"].ToString());
            //strXML3.Append("<colorrange>");
            //strXML3.Append("<color minvalue='0'  maxvalue='25'  code='#6baa01' alpha='25'/>");
            //strXML3.Append("<color minvalue='25' maxvalue='50'  code='#f8bd19' alpha='25'/>");
            //strXML3.Append("<color minvalue='50' maxvalue='75'  code='#e44a00' alpha='25'/>");
            //strXML3.Append("<color minvalue='75' maxvalue='100' code='#e44a00' alpha='25'/>");
            //strXML3.Append("</colorrange>");
            //strXML3.AppendFormat("<value>{0}</value>",(Convert.ToInt64(TDT.Rows[0]["EmpACount"]) / Convert.ToInt64(TDT.Rows[0]["EmpAllCount"]) * 100).ToString());
            //strXML3.Append("<target>100</target>");
            //strXML3.Append("</chart>");

            //Chart ChartObj3 = new Chart("hbullet", "            ", "100%", "90", "xml", strXML3.ToString());
            //litAbsenceEmp.Text = ChartObj3.Render();
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //StringBuilder strXML4 = new StringBuilder();
            //strXML4.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' bgAlpha='0' numbersuffix='%' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' chartbottommargin='25' theme='fint'>", General.Msg("Vacations","الإجازات"), TDT.Rows[0]["EmpVCount"].ToString());
            //strXML4.Append("<colorrange>");
            //strXML4.Append("<color minvalue='0'  maxvalue='25'  code='#6baa01' alpha='25'/>");
            //strXML4.Append("<color minvalue='25' maxvalue='50'  code='#f8bd19' alpha='25'/>");
            //strXML4.Append("<color minvalue='50' maxvalue='75'  code='#e44a00' alpha='25'/>");
            //strXML4.Append("<color minvalue='75' maxvalue='100' code='#e44a00' alpha='25'/>");
            //strXML4.Append("</colorrange>");
            //strXML4.AppendFormat("<value>{0}</value>",(Convert.ToInt64(TDT.Rows[0]["EmpVCount"]) / Convert.ToInt64(TDT.Rows[0]["EmpAllCount"]) * 100).ToString());
            //strXML4.Append("<target>100</target>");
            //strXML4.Append("</chart>");

            //Chart ChartObj4 = new Chart("hbullet", "             ", "100%", "90", "xml", strXML4.ToString());
            //litVacationsEmp.Text = ChartObj4.Render();
            /////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillMonthlyStatus()
    {
        DateTime Param_FromDate = DateTime.Now;
        DateTime Param_ToDate = DateTime.Now;
        DTCs.FindMonthDates(lblYear.Text, ddlMonth.SelectedValue, out Param_FromDate, out Param_ToDate);

        try { EMP_FillChart_M01(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { EMP_FillChart_M02(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { EMP_FillChart_M03(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { EMP_FillChart_M04(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillChart_M01(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("General measure of the work", "المقياس العام للعمل");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT ISNULL(CAST(MsmWorkDuration AS DECIMAL),0) / ISNULL(MsmShiftDuration,0) * 100 AS PercentWork ");
        Q.Append(" FROM MonthSummaryInfoView ");
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 
        Q.Append(" AND EmpID IN ('" + pgCs.LoginEmpID + "') ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' numbersuffix='%' exportEnabled='1' lowerlimit='0' upperlimit='100' showValue='1' valueBelowPivot='1' theme='fint'>",titel);
            strXML.Append("<colorrange>");
            strXML.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00'/>");
            strXML.Append("<color minvalue='25' maxvalue='50'  code='#e44a00'/>");
            strXML.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19'/>");
            strXML.Append("<color minvalue='75' maxvalue='100' code='#6baa01'/>");
            strXML.Append("</colorrange>");
            strXML.AppendFormat("<dials><dial value='{0}'/></dials>", ChartDT.Rows[0]["PercentWork"].ToString());
            strXML.Append("</chart>");

            Chart ChartObj = new Chart("angulargauge", " ", "100%", "450", "xml", strXML.ToString());
            litChartMonthly_01.Text = ChartObj.Render();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillChart_M02(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Attendees", "الحضور");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(MsmShiftDuration,0)) AS ShiftDuration ");
        Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS WorkPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_All AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS OVTPercent  "); 
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_BeginEarly AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_BeginEarlyPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_OutLate    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_OutLatePercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_OutOfShift AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_OutOfShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_NoShift    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_NoShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_InVac      AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_InVacPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_BeginEarly AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_BeginEarly,0)) * 100,0),0) AS ET_BeginEarlyPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_BeginEarly  AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_BeginEarly,0)) * 100,0),0) AS OT_BeginEarlyPercent  "); 
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_OutLate    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutLate,0)) * 100,0),0) AS ET_OutLatePercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_OutLate     AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutLate,0)) * 100,0),0) AS OT_OutLatePercent ");	  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_OutOfShift AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutOfShift,0)) * 100,0),0) AS ET_OutOfShiftPercent ");	
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_OutOfShift  AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutOfShift,0)) * 100,0),0) AS OT_OutOfShiftPercent ");	  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_NoShift    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_NoShift,0)) * 100,0),0) AS ET_NoShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_NoShift     AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_NoShift,0)) * 100,0),0) AS OT_NoShiftPercent	 ");  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_InVac      AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_InVac,0)) * 100,0),0) AS ET_InVacPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_InVac       AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_InVac,0)) * 100,0),0) AS OT_InVacPercent ");
        Q.Append(" FROM MonthSummaryInfoView ");  
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");  
        Q.Append(" AND EmpID IN ('" + pgCs.LoginEmpID + "') ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        DataTable ADT = new DataTable();
        ADT = DBCs.FetchData(cmd);

        cmd = new SqlCommand();
        Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(GapDuration,0)) AS GapTotal");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS BL_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_ExcPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS OE_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_ExcPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS MG_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_ExcPercent");
        Q.Append(" FROM EmployeeGapInfoView");
        Q.Append(" WHERE GapDate BETWEEN @Param_FromDate AND @Param_ToDate");
        Q.Append(" AND EmpID IN ('" + pgCs.LoginEmpID + "') ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);     
        cmd.CommandText = Q.ToString();

        StringBuilder strXML = new StringBuilder();
        DataTable GDT = new DataTable();
        GDT = DBCs.FetchData(cmd);

        if (!DBCs.IsNullOrEmpty(ADT) && !DBCs.IsNullOrEmpty(GDT))
        {
            if (Convert.ToDouble(ADT.Rows[0]["WorkPercent"]) > 0)
            {
                Double GapPercent = 0;
                if (GDT.Rows[0]["GapTotal"] != DBNull.Value && ADT.Rows[0]["ShiftDuration"] != DBNull.Value)
                {
                    GapPercent = Convert.ToDouble(GDT.Rows[0]["GapTotal"]) / Convert.ToDouble(ADT.Rows[0]["ShiftDuration"]) * 100;
                }

                strXML.AppendFormat("<chart caption='{0}' showValue='1' captionfontsize='14' subcaptionfontsize='14' basefontcolor='#333333' basefont='Helvetica Neue,Arial' basefontsize='9' subcaptionfontbold='0' bgcolor='#ffffff' canvasbgcolor='#ffffff' showborder='0' showshadow='0' showcanvasborder='0' piefillalpha='60' pieborderthickness='2' hoverfillcolor='#cccccc' piebordercolor='#ffffff' usehovercolor='1' showvaluesintooltip='1' showpercentintooltip='0' numbersuffix='%' decimals='2' plottooltext='$label, $Value%'>", titel);
                strXML.AppendFormat("<category label='{0}' color='#ffffff' value=''>", General.Msg("Attendees", "الحضور"));
                    strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}'>", "", string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}'>", "", string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}' />", General.Msg("Actual Attendance", "الحضور الفعلي"), string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                    strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Gaps", "الثغرات"), string.Format("{0:0.00}", GapPercent).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Begin Late", "الدخول المتأخر"), string.Format("{0:0.00}", GDT.Rows[0]["BL_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Out Early", "الخروج المبكر"), string.Format("{0:0.00}", GDT.Rows[0]["OE_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["OE_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["OE_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["OE_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("During work", "أثناء الدوام"), string.Format("{0:0.00}", GDT.Rows[0]["MG_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["MG_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["MG_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                    strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Overtime", "الوقت الإضافي"), string.Format("{0:0.00}", ADT.Rows[0]["OVTPercent"]).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Begin Early", "الحضور المبكر"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_BeginEarlyPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_BeginEarlyPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_BeginEarlyPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Out Late", "الخروج المتأخر"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_OutLatePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_OutLatePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_OutLatePercent"]).ToString());
                        strXML.AppendFormat("</category>");                                                                                                                                      
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Out Of Shift", "خارج الوردية"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_OutOfShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_OutOfShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_OutOfShiftPercent"]).ToString());
                        strXML.AppendFormat("</category>");

                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("No Shift", "خارج الدوام"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_NoShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_NoShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_NoShiftPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("During Vacation", "أثناء الإجازة"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_InVacPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_InVacPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_InVacPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                strXML.AppendFormat("</category>");
                strXML.AppendFormat("</chart>");
            }
            else
            {
                strXML.AppendFormat("<chart caption='{0}' showValue='1' captionfontsize='14' subcaptionfontsize='14' basefontcolor='#333333' basefont='Helvetica Neue,Arial' basefontsize='9' subcaptionfontbold='0' bgcolor='#ffffff' canvasbgcolor='#ffffff' showborder='0' showshadow='0' showcanvasborder='0' piefillalpha='60' pieborderthickness='2' hoverfillcolor='#cccccc' piebordercolor='#ffffff' usehovercolor='1' showvaluesintooltip='1' showpercentintooltip='0' numbersuffix='%' decimals='2' plottooltext='$label, $Value%'>", titel);
                strXML.AppendFormat("<category label='{0}{1}' color='#f8bd19'>", General.Msg("Attendees", "الحضور"), " 0.00%");
                   
                strXML.AppendFormat("</category>");
                strXML.AppendFormat("</chart>");
            }

            Chart ChartObj = new Chart("multilevelpie", "  ", "100%", "450", "xml", strXML.ToString());
            litChartMonthly_02.Text = ChartObj.Render();
        } 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillChart_M03(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("IN Transactions", "حركات الدخول");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT TrnTime ");
        Q.Append(" FROM Trans ");
        Q.Append(" WHERE TrnDate BETWEEN @Param_FromDate AND @Param_ToDate ");
        Q.Append(" AND EmpID = '" + pgCs.LoginEmpID + "' AND TrnFlag = 'IN' ");
        Q.Append(" ORDER BY TrnDate");
        
        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        DataTable DT = new DataTable();
        DT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(DT))
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' showborder='0' showOpenValue='1' showCloseValue='1' showHighLowValue= '1' showToolTip='1' plottooltext='$displayValue' bgcolor='#ffffff' theme='fint'>", titel);
            strXML.Append("<dataset>");

            foreach (DataRow DR in DT.Rows)
            {
                Int64 i = Convert.ToInt64(DTCs.TimeToInt(DR["TrnTime"]));              
                strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", i, DisplayFun.GrdDisplayTime(DR["TrnTime"]));
            }

            strXML.Append("</dataset>");
            strXML.Append("</chart>");
           
            Chart ChartObj = new Chart("sparkline", "                ", "100%", "100", "xml", strXML.ToString());
            litChartMonthly_05.Text = ChartObj.Render();
        }
        else
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' showborder='0' showOpenValue='1' showCloseValue='1' showHighLowValue= '0' showToolTip='1' plottooltext='$displayValue' bgcolor='#ffffff' theme='fint'>", titel);
            strXML.Append("<dataset>");
                          
            strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", 0, "");
            strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", 0, "");

            strXML.Append("</dataset>");
            strXML.Append("</chart>");
           
            Chart ChartObj = new Chart("sparkline", "                ", "100%", "100", "xml", strXML.ToString());
            litChartMonthly_05.Text = ChartObj.Render();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillChart_M04(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Out Transactions", "حركات الخروج");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT TrnTime ");
        Q.Append(" FROM Trans ");
        Q.Append(" WHERE TrnDate BETWEEN @Param_FromDate AND @Param_ToDate ");
        Q.Append(" AND EmpID = '" + pgCs.LoginEmpID + "' AND TrnFlag = 'OU' ");
        Q.Append(" ORDER BY TrnDate");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        DataTable DT = new DataTable();
        DT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(DT))
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' showborder='0' showOpenValue='1' showCloseValue='1' showHighLowValue= '1' showToolTip='1' plottooltext='$displayValue' bgcolor='#ffffff' theme='fint'>", titel);
            strXML.Append("<dataset>");

            foreach (DataRow DR in DT.Rows)
            {
                Int64 i = Convert.ToInt64(DTCs.TimeToInt(DR["TrnTime"]));              
                strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", i, DisplayFun.GrdDisplayTime(DR["TrnTime"]));
            }

            strXML.Append("</dataset>");
            strXML.Append("</chart>");
           
            Chart ChartObj = new Chart("sparkline", "             ", "100%", "100", "xml", strXML.ToString());
            litChartMonthly_06.Text = ChartObj.Render();
        }
        else
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' showborder='0' showOpenValue='1' showCloseValue='1' showHighLowValue= '0' showToolTip='1' plottooltext='$displayValue' bgcolor='#ffffff' theme='fint'>", titel);
            strXML.Append("<dataset>");
                          
            strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", 0, "");
            strXML.AppendFormat("<set value='{0}' displayValue='{1}' />", 0, "");

            strXML.Append("</dataset>");
            strXML.Append("</chart>");
           
            Chart ChartObj = new Chart("sparkline", "             ", "100%", "100", "xml", strXML.ToString());
            litChartMonthly_06.Text = ChartObj.Render();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EMP_FillChart_M08(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Best three Employees", "أفضل ثلاث موظفين");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT EmpID, EmpNameEn, EmpNameAr, WorkPercent ");
        Q.Append(" FROM (SELECT TOP 3 EmpID, EmpNameEn, EmpNameAr, ISNULL(SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS WorkPercent ");
        Q.Append("       FROM dbo.MonthSummaryInfoView ");
        Q.Append("       WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");
        Q.Append("       AND DepID IN (" + pgCs.DepList + ") ");
        Q.Append("       GROUP BY EmpID, EmpNameEn, EmpNameAr) M ");
        Q.Append(" WHERE WorkPercent > 0 ");
        Q.Append(" ORDER BY WorkPercent DESC ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        StringBuilder strXML = new StringBuilder();
        DataTable DT = new DataTable();
        DT = DBCs.FetchData(cmd);

        if (!DBCs.IsNullOrEmpty(DT))
        {
            strXML.AppendFormat("<chart caption='{0}' yAxisName='{1}' numbersuffix='%' decimals='2' theme='fint' LabelPadding='50' rotatevalues='0' plotToolText=''>", titel, General.Msg("Attendance rate", "نسبة الحضور"));
            strXML.AppendFormat("<annotations autoScale='0' scaleImages='1' origW='400' origH='300'>");
            strXML.AppendFormat("<groups id='user-images'>");
            
            strXML.AppendFormat("<items id='Emp1' type='image' url='{0}' x='$xaxis.label.0.x - 24' y='$xaxis.label.0.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("<items id='Emp2' type='image' url='{0}' x='$xaxis.label.1.x - 24' y='$xaxis.label.1.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("<items id='Emp3' type='image' url='{0}' x='$xaxis.label.2.x - 24' y='$xaxis.label.2.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("</groups>");
            strXML.AppendFormat("</annotations>");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[0]["WorkPercent"].ToString());
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[1][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[1]["WorkPercent"].ToString());
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[2][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[2]["WorkPercent"].ToString());
            strXML.AppendFormat("</chart>");
        }
        else
        {
            strXML.AppendFormat("<chart caption='{0}' yAxisName='{1}' numbersuffix='%' decimals='2' theme='fint' LabelPadding='50' rotatevalues='0' plotToolText=''>", titel, General.Msg("Attendance rate", "نسبة الحضور"));
            strXML.AppendFormat("<annotations autoScale='0' scaleImages='1' origW='400' origH='300'>");
            strXML.AppendFormat("<groups id='user-images'>");
            
            strXML.AppendFormat("<items id='Emp1' type='image' url='{0}' x='$xaxis.label.0.x - 24' y='$xaxis.label.0.y - 52'/>", GetImage(""));
            strXML.AppendFormat("<items id='Emp2' type='image' url='{0}' x='$xaxis.label.1.x - 24' y='$xaxis.label.1.y - 52'/>", GetImage(""));
            strXML.AppendFormat("<items id='Emp3' type='image' url='{0}' x='$xaxis.label.2.x - 24' y='$xaxis.label.2.y - 52'/>", GetImage(""));
            strXML.AppendFormat("</groups>");
            strXML.AppendFormat("</annotations>");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "1", "");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "2", "");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "3", "");
            strXML.AppendFormat("</chart>");
        }

        Chart ChartObj = new Chart("column2d", "    ", "100%", "450", "xml", strXML.ToString());
        litChartMonthly_04.Text = ChartObj.Render();
    }

     #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region USR CHART

    public void USR_FillTodayStatus()
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
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void USR_FillMonthlyStatus()
    {
        DateTime Param_FromDate = DateTime.Now;
        DateTime Param_ToDate = DateTime.Now;
        DTCs.FindMonthDates(lblYear.Text, ddlMonth.SelectedValue, out Param_FromDate, out Param_ToDate);

        try { USR_FillChart_M01(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { USR_FillChart_M02(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { USR_FillChart_M03(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
        try { USR_FillChart_M04(Param_FromDate, Param_ToDate); } catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void USR_FillChart_M01(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("General measure of the work of the Departments", "المقياس العام لدوام الأقسام");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / SUM(ISNULL(MsmShiftDuration,0)) * 100 AS PercentWork ");
        Q.Append(" FROM dbo.MonthSummaryInfoView ");
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) ");
 	    Q.Append(" AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 
        Q.Append(" AND DepID IN (" + pgCs.DepList + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' numbersuffix='%' exportEnabled='1' lowerlimit='0' upperlimit='100' showValue='1' valueBelowPivot='1' theme='fint'>",titel);
            strXML.Append("<colorrange>");
            strXML.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00'/>");
            strXML.Append("<color minvalue='25' maxvalue='50'  code='#e44a00'/>");
            strXML.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19'/>");
            strXML.Append("<color minvalue='75' maxvalue='100' code='#6baa01'/>");
            strXML.Append("</colorrange>");
            strXML.AppendFormat("<dials><dial value='{0}'/></dials>", ChartDT.Rows[0]["PercentWork"].ToString());
            strXML.Append("</chart>");

            Chart ChartObj = new Chart("angulargauge", " ", "100%", "450", "xml", strXML.ToString());
            litChartMonthly_01.Text = ChartObj.Render();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void USR_FillChart_M02(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Attendees", "الحضور");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(MsmShiftDuration,0)) AS ShiftDuration ");
        Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS WorkPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_All AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS OVTPercent  "); 
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_BeginEarly AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_BeginEarlyPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_OutLate    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_OutLatePercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_OutOfShift AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_OutOfShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_NoShift    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_NoShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmTotalOVT_InVac      AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_All,0)) * 100,0),0) AS OVT_InVacPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_BeginEarly AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_BeginEarly,0)) * 100,0),0) AS ET_BeginEarlyPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_BeginEarly  AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_BeginEarly,0)) * 100,0),0) AS OT_BeginEarlyPercent  "); 
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_OutLate    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutLate,0)) * 100,0),0) AS ET_OutLatePercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_OutLate     AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutLate,0)) * 100,0),0) AS OT_OutLatePercent ");	  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_OutOfShift AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutOfShift,0)) * 100,0),0) AS ET_OutOfShiftPercent ");	
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_OutOfShift  AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_OutOfShift,0)) * 100,0),0) AS OT_OutOfShiftPercent ");	  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_NoShift    AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_NoShift,0)) * 100,0),0) AS ET_NoShiftPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_NoShift     AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_NoShift,0)) * 100,0),0) AS OT_NoShiftPercent	 ");  
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmExtraTimeDur_InVac      AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_InVac,0)) * 100,0),0) AS ET_InVacPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmOverTimeDur_InVac       AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmTotalOVT_InVac,0)) * 100,0),0) AS OT_InVacPercent ");
        Q.Append(" FROM dbo.MonthSummaryInfoView ");  
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");  
        Q.Append(" AND DepID IN (" + pgCs.DepList + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        DataTable ADT = new DataTable();
        ADT = DBCs.FetchData(cmd);

        cmd = new SqlCommand();
        Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(GapDuration,0)) AS GapTotal");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS BL_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'BL' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS BL_ExcPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS OE_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'OE' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS OE_ExcPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG') THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(ISNULL(GapDuration,0)) AS DECIMAL),0) * 100 AS MG_ALLPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 1) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_GracePercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 0 AND ISNULL(ExcID,0)  = 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_GapPercent");
	    Q.Append("       ,SUM(CASE WHEN (GapType = 'MG' AND GapGraceFlag = 0 AND ISNULL(ExcID,0) != 0) THEN GapDuration ELSE 0 END) / NULLIF(CAST(SUM(CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0 END) AS DECIMAL),0) * 100 AS MG_ExcPercent");
        Q.Append(" FROM EmployeeGapInfoView");
        Q.Append(" WHERE GapDate BETWEEN @Param_FromDate AND @Param_ToDate");
        Q.Append(" AND DepID IN (" + pgCs.DepList + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);     
        cmd.CommandText = Q.ToString();

        StringBuilder strXML = new StringBuilder();
        DataTable GDT = new DataTable();
        GDT = DBCs.FetchData(cmd);

        if (!DBCs.IsNullOrEmpty(ADT) && !DBCs.IsNullOrEmpty(GDT))
        {
            if (Convert.ToDouble(ADT.Rows[0]["WorkPercent"]) > 0)
            {
                Double GapPercent = 0;
                if (GDT.Rows[0]["GapTotal"] != DBNull.Value && ADT.Rows[0]["ShiftDuration"] != DBNull.Value)
                {
                    GapPercent = Convert.ToDouble(GDT.Rows[0]["GapTotal"]) / Convert.ToDouble(ADT.Rows[0]["ShiftDuration"]) * 100;
                }

                strXML.AppendFormat("<chart caption='{0}' showValue='1' captionfontsize='14' subcaptionfontsize='14' basefontcolor='#333333' basefont='Helvetica Neue,Arial' basefontsize='9' subcaptionfontbold='0' bgcolor='#ffffff' canvasbgcolor='#ffffff' showborder='0' showshadow='0' showcanvasborder='0' piefillalpha='60' pieborderthickness='2' hoverfillcolor='#cccccc' piebordercolor='#ffffff' usehovercolor='1' showvaluesintooltip='1' showpercentintooltip='0' numbersuffix='%' decimals='2' plottooltext='$label, $Value%'>", titel);
                strXML.AppendFormat("<category label='{0}' color='#ffffff' value=''>", General.Msg("Attendees", "الحضور"));
                    strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}'>", "", string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}'>", "", string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#f8bd19' value='{1}' />", General.Msg("Actual Attendance", "الحضور الفعلي"), string.Format("{0:0.00}", ADT.Rows[0]["WorkPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                    strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Gaps", "الثغرات"), string.Format("{0:0.00}", GapPercent).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Begin Late", "الدخول المتأخر"), string.Format("{0:0.00}", GDT.Rows[0]["BL_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("Out Early", "الخروج المبكر"), string.Format("{0:0.00}", GDT.Rows[0]["OE_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["OE_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["OE_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["OE_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}'>", General.Msg("During work", "أثناء الدوام"), string.Format("{0:0.00}", GDT.Rows[0]["MG_ALLPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Grace", "المسموح"), string.Format("{0:0.00}", GDT.Rows[0]["MG_GracePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Excuse", "الإستئذان"), string.Format("{0:0.00}", GDT.Rows[0]["MG_ExcPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#e44a00' value='{1}' />", General.Msg("Without Excuse", "بدون استئذان"), string.Format("{0:0.00}", GDT.Rows[0]["BL_GapPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                    strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Overtime", "الوقت الإضافي"), string.Format("{0:0.00}", ADT.Rows[0]["OVTPercent"]).ToString());
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Begin Early", "الحضور المبكر"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_BeginEarlyPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_BeginEarlyPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_BeginEarlyPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Out Late", "الخروج المتأخر"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_OutLatePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_OutLatePercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_OutLatePercent"]).ToString());
                        strXML.AppendFormat("</category>");                                                                                                                                      
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("Out Of Shift", "خارج الوردية"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_OutOfShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_OutOfShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_OutOfShiftPercent"]).ToString());
                        strXML.AppendFormat("</category>");

                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("No Shift", "خارج الدوام"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_NoShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_NoShiftPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_NoShiftPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                        strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}'>", General.Msg("During Vacation", "أثناء الإجازة"), string.Format("{0:0.00}", ADT.Rows[0]["OVT_InVacPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("With approval", "مع موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["OT_InVacPercent"]).ToString());
                            strXML.AppendFormat("<category label='{0}' color='#008ee4' value='{1}' />", General.Msg("Without approval", "بدون موافقة"), string.Format("{0:0.00}", ADT.Rows[0]["ET_InVacPercent"]).ToString());
                        strXML.AppendFormat("</category>");
                    strXML.AppendFormat("</category>");
                strXML.AppendFormat("</category>");
                strXML.AppendFormat("</chart>");
            }
            else
            {
                strXML.AppendFormat("<chart caption='{0}' showValue='1' captionfontsize='14' subcaptionfontsize='14' basefontcolor='#333333' basefont='Helvetica Neue,Arial' basefontsize='9' subcaptionfontbold='0' bgcolor='#ffffff' canvasbgcolor='#ffffff' showborder='0' showshadow='0' showcanvasborder='0' piefillalpha='60' pieborderthickness='2' hoverfillcolor='#cccccc' piebordercolor='#ffffff' usehovercolor='1' showvaluesintooltip='1' showpercentintooltip='0' numbersuffix='%' decimals='2' plottooltext='$label, $Value%'>", titel);
                strXML.AppendFormat("<category label='{0}{1}' color='#f8bd19'>", General.Msg("Attendees", "الحضور"), " 0.00%");
                   
                strXML.AppendFormat("</category>");
                strXML.AppendFormat("</chart>");
            }

            Chart ChartObj = new Chart("multilevelpie", "  ", "100%", "450", "xml", strXML.ToString());
            litChartMonthly_02.Text = ChartObj.Render();
        } 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void USR_FillChart_M03(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Attendance rate vs Absence rate", "نسبة الحضور مقابل نسبة الغياب");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT DepID,DepNameEn,DepNameAr ");
        Q.Append("       ,COUNT(DepID) AS EmpCount ");
        Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS WorkPercent ");
	    Q.Append("       ,ISNULL(SUM(ISNULL(CAST(MsmDays_Absent_WithoutVac AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmDays_Work,0)),0) * 100,0) AS AbsentPercent ");
        Q.Append(" FROM dbo.MonthSummaryInfoView ");
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");
        Q.Append(" AND DepID IN (" + pgCs.DepList + ") ");
        Q.Append(" GROUP BY DepID,DepNameEn,DepNameAr ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        DataTable DT = new DataTable();
        DT = DBCs.FetchData(cmd);

        if (!DBCs.IsNullOrEmpty(DT))
        {
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' xaxisminvalue='-10' xaxismaxvalue='100' yaxisminvalue='0' yaxismaxvalue='100' plotfillalpha='70' plotfillhovercolor='#6baa01' ", titel);
            strXML.AppendFormat(" showplotborder='0' xaxisname='{0}' yaxisname='{1}' numdivlines='2' showvalues='1' showtrendlinelabels='0' ", General.Msg("Absence rate", "نسبة الغياب"), General.Msg("Attendance rate", "نسبة الحضور"));
            strXML.AppendFormat(" drawquadrant='1' quadrantlinealpha='80' quadrantlinethickness='3' quadrantxval='50' quadrantyval='15000' ");
            strXML.AppendFormat(" basefontcolor='#333333' basefont='Helvetica Neue,Arial' captionfontsize='14' subcaptionfontsize='14' subcaptionfontbold='0' showborder='0' bgcolor='#ffffff' ");
            strXML.AppendFormat(" showshadow='0' canvasbgcolor='#ffffff' canvasborderalpha='0' divlinealpha='100' divlinecolor='#999999' divlinethickness='1' divlinedashed='1' divlinedashlen='1' ");
            //strXML.AppendFormat(" use3dlighting='0' showplotborder='0' showyaxisline='1' yaxislinethickness='1' yaxislinecolor='#999999' showxaxisline='1' xaxislinethickness='1' ");
            strXML.AppendFormat(" xaxislinecolor='#999999' showalternatehgridcolor='0' showalternatevgridcolor='0'>");
                strXML.AppendFormat("<categories>");
                    strXML.AppendFormat("<category label=''     x='-10' />");
                    strXML.AppendFormat("<category label='0%'   x='0'    showverticalline='1' />");
                    strXML.AppendFormat("<category label='10%'  x='10'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='20%'  x='20'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='30%'  x='30'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='40%'  x='40'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='50%'  x='50'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='60%'  x='60'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='70%'  x='70'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='80%'  x='80'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='90%'  x='90'   showverticalline='1' />");
                    strXML.AppendFormat("<category label='100%' x='100'  showverticalline='1' />");
                    strXML.AppendFormat("<category label=''     x='110'  showverticalline='1' />");
                strXML.AppendFormat("</categories>");
                strXML.AppendFormat("<dataset color='#00aee4'>");
                    
                foreach (DataRow DR in DT.Rows)
                {
                    //Quarter 1{br}Total Sale: $195K{br}Rank: 1"
                    strXML.AppendFormat("<set x='{0}' y='{1}' z='{2}' name='{3}' tooltext='{4}' />",DR["AbsentPercent"].ToString() ,DR["WorkPercent"].ToString() , DR["EmpCount"].ToString(), DR["EmpCount"].ToString(), DR[General.Msg("DepNameEn", "DepNameAr")].ToString());
                }
                strXML.AppendFormat("</dataset>");
                //strXML.AppendFormat("<trendlines>");
                //    strXML.AppendFormat("<line startvalue='20000' endvalue='30000' istrendzone='1' color='#aaaaaa' alpha='14' />");
                //    strXML.AppendFormat("<line startvalue='10000' endvalue='20000' istrendzone='1' color='#aaaaaa' alpha='7' />");
                //strXML.AppendFormat("</trendlines>");
                //strXML.AppendFormat("<vtrendlines>");
                //    strXML.AppendFormat("<line startvalue='44' istrendzone='0' color='#0066cc' thickness='1' dashed='1' displayvalue='Gross Avg.' />");
                //strXML.AppendFormat("</vtrendlines>");
            strXML.AppendFormat("</chart>");

            Chart ChartObj = new Chart("bubble", "   ", "100%", "450", "xml", strXML.ToString());
            litChartMonthly_03.Text = ChartObj.Render();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void USR_FillChart_M04(DateTime Param_FromDate, DateTime Param_ToDate)
    {
        string titel = General.Msg("Best three Employees", "أفضل ثلاث موظفين");
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT EmpID, EmpNameEn, EmpNameAr, WorkPercent ");
        Q.Append(" FROM (SELECT TOP 3 EmpID, EmpNameEn, EmpNameAr, ISNULL(SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / NULLIF(SUM(ISNULL(MsmShiftDuration,0)),0) * 100,0) AS WorkPercent ");
        Q.Append("       FROM dbo.MonthSummaryInfoView ");
        Q.Append("       WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");
        Q.Append("       AND DepID IN (" + pgCs.DepList + ") ");
        Q.Append("       GROUP BY EmpID, EmpNameEn, EmpNameAr) M ");
        Q.Append(" WHERE WorkPercent > 0 ");
        Q.Append(" ORDER BY WorkPercent DESC ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);      
        cmd.CommandText = Q.ToString();

        StringBuilder strXML = new StringBuilder();
        DataTable DT = new DataTable();
        DT = DBCs.FetchData(cmd);

        if (!DBCs.IsNullOrEmpty(DT))
        {
            strXML.AppendFormat("<chart caption='{0}' yAxisName='{1}' numbersuffix='%' decimals='2' theme='fint' LabelPadding='50' rotatevalues='0' plotToolText=''>", titel, General.Msg("Attendance rate", "نسبة الحضور"));
            strXML.AppendFormat("<annotations autoScale='0' scaleImages='1' origW='400' origH='300'>");
            strXML.AppendFormat("<groups id='user-images'>");
            
            strXML.AppendFormat("<items id='Emp1' type='image' url='{0}' x='$xaxis.label.0.x - 24' y='$xaxis.label.0.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("<items id='Emp2' type='image' url='{0}' x='$xaxis.label.1.x - 24' y='$xaxis.label.1.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("<items id='Emp3' type='image' url='{0}' x='$xaxis.label.2.x - 24' y='$xaxis.label.2.y - 52'/>", GetImage(DT.Rows[0]["EmpID"].ToString()));
            strXML.AppendFormat("</groups>");
            strXML.AppendFormat("</annotations>");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[0]["WorkPercent"].ToString());
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[1][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[1]["WorkPercent"].ToString());
            strXML.AppendFormat("<data label='{0}' value='{1}' />", DT.Rows[2][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), DT.Rows[2]["WorkPercent"].ToString());
            strXML.AppendFormat("</chart>");
        }
        else
        {
            strXML.AppendFormat("<chart caption='{0}' yAxisName='{1}' numbersuffix='%' decimals='2' theme='fint' LabelPadding='50' rotatevalues='0' plotToolText=''>", titel, General.Msg("Attendance rate", "نسبة الحضور"));
            strXML.AppendFormat("<annotations autoScale='0' scaleImages='1' origW='400' origH='300'>");
            strXML.AppendFormat("<groups id='user-images'>");
            
            strXML.AppendFormat("<items id='Emp1' type='image' url='{0}' x='$xaxis.label.0.x - 24' y='$xaxis.label.0.y - 52'/>", GetImage(""));
            strXML.AppendFormat("<items id='Emp2' type='image' url='{0}' x='$xaxis.label.1.x - 24' y='$xaxis.label.1.y - 52'/>", GetImage(""));
            strXML.AppendFormat("<items id='Emp3' type='image' url='{0}' x='$xaxis.label.2.x - 24' y='$xaxis.label.2.y - 52'/>", GetImage(""));
            strXML.AppendFormat("</groups>");
            strXML.AppendFormat("</annotations>");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "1", "");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "2", "");
            strXML.AppendFormat("<data label='{0}' value='{1}' />", "3", "");
            strXML.AppendFormat("</chart>");
        }

        Chart ChartObj = new Chart("column2d", "    ", "100%", "450", "xml", strXML.ToString());
        litChartMonthly_04.Text = ChartObj.Render();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetImage(string ID) //http://static.fusioncharts.com/sampledata/userimages/1.png
    {
        string url = "../images/EmpImage/EmptyEmployee.png";
        if (!string.IsNullOrEmpty(ID))
        {
            string path = HttpContext.Current.Server.MapPath("~/images/EmpImage/" + ID + ".png");
            if (File.Exists(path)) { url = "../images/EmpImage/" + ID + ".png"; }
        }

        return url;
    }

     #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}