using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using InfoSoftGlobal;

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
    protected void ddlDepChartsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeList(ddlDepChartsFilter.SelectedValue, pgCs.DepList);
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
            DivMonth.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup('" + DivDay.ClientID + "');", true);
            calDate.SetEnabled(true);
        }
        else
        {
            DivMonth.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hidePopup('" + DivDay.ClientID + "');", true);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnChartsFilter_Click(object sender, EventArgs e)
    {
        string DepID = ddlDepChartsFilter.SelectedValue;
        string EmpID = ddlEmpChartsFilter.SelectedValue;
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
            else { CQ.Append(" EmpID IN (SELECT EmpID FROM spActiveEmployeeView WHERE DepID = @DepID"); cmd.Parameters.AddWithValue("@DepID", DepID); }
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
        litChartWorkDurtion.Text = General.Msg("No data as required", "لا يوجد بيانات حسب الخيارات المطلوبة");
        string xmlChart = "";
        string xmlData = "";
        string titel = "";
        string xmlStyle = "";

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

        xmlData += "<set label='" + General.Msg("Total actual working hours", "مجموع ساعات العمل الفعلية") + " " + DisplayFun.GrdDisplayDuration(ShowSumWorkDuration) + "' value='" + SumWorkDuration.ToString() + "' />";
        xmlData += "<set label='" + General.Msg("Total hours of non - working", "مجموع ساعات عدم العمل") + " " + DisplayFun.GrdDisplayDuration(ShowSumNotWorkDuration) + "' value='" + SumNotWorkDuration + "' isSliced ='1'/>";

        xmlStyle = " <styles> "
                + " <definition> "
                + "  <style name='CaptionAnim'  type='animation' param='_y' easing='Bounce' start='0' duration='2' /> "
                + "  <style name='CaptionFont'  type='font' isHTML='1' font='GE SS Two Light' size='18' color='666666' bold='1' underline='0' /> "
                + "  <style name='AxisNameFont' type='font' isHTML='1' font='GE SS Two Light' size='14' color='666666' bold='1' /> "
                + "  <style name='DataFont'     type='font' isHTML='1' font='GE SS Two Light' size='12' color='666666' /> "
                + " </definition> "
                + " <application> "
                + " <apply toObject='TOOLTIP'     styles='AxisNameFont' /> "
                + " <apply toObject='xAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='yAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='DataLabels'  styles='DataFont' /> "
                + " <apply toObject='DataValues'  styles='DataFont' /> "

                + " <apply toObject='Caption'     styles='CaptionFont' /> " //,CaptionAnim
                + " </application> "
                + " </styles> ";

        titel = General.Msg("Total work hours required", "مجموع ساعات العمل المطلوبة") + " " + DisplayFun.GrdDisplayDuration(ShowSumShiftDuration);
        xmlChart = " <chart palette='4' caption='" + titel + "' rotateYAxisName='0' showValues='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='1'> ";
        xmlChart += xmlData + xmlStyle + "</chart>";

        litChartWorkDurtion.Text = FusionCharts.RenderChart("../FusionCharts/Pie2D.swf", "", xmlChart, "ChartWorkDurtion", "100%", "350", false, false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartBeginLateDurtion(DataTable DT)
    {
        litChartBeginLateDurtion.Text = General.Msg("No data as required", "لا يوجد بيانات حسب الخيارات المطلوبة");
        string xmlChart = "";
        string xmlData = "";
        string titel = "";
        string xmlStyle = "";

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

        xmlData += "<set label='" + General.Msg("Total hours of delay", "مجموع ساعات التأخير") + " " + DisplayFun.GrdDisplayDuration(ShowSumBeginLateDuration) + "' value='" + SumBeginLateDuration + "' isSliced ='1' color ='FF0000'/>";
        xmlData += "<set label='" + General.Msg("Total working hours", "مجموع ساعات العمل") + " " + DisplayFun.GrdDisplayDuration(ShowSumNotDuration) + "' value='" + SumNotDuration + "' color ='009900'/>";

        xmlStyle = " <styles> "
                + " <definition> "
                + "  <style name='CaptionAnim'  type='animation' param='_y' easing='Bounce' start='0' duration='2' /> "
                + "  <style name='CaptionFont'  type='font' isHTML='1' font='GE SS Two Light' size='18' color='666666' bold='1' underline='0' /> "
                + "  <style name='AxisNameFont' type='font' isHTML='1' font='GE SS Two Light' size='14' color='666666' bold='1' /> "
                + "  <style name='DataFont'     type='font' isHTML='1' font='GE SS Two Light' size='12' color='666666' /> "
                + " </definition> "
                + " <application> "
                + " <apply toObject='TOOLTIP'     styles='AxisNameFont' /> "
                + " <apply toObject='xAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='yAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='DataLabels'  styles='DataFont' /> "
                + " <apply toObject='DataValues'  styles='DataFont' /> "

                + " <apply toObject='Caption'     styles='CaptionFont' /> " //,CaptionAnim
                + " </application> "
                + " </styles> ";

        titel = General.Msg("Total actual hours required", "مجموع ساعات العمل الفعلية") + " " + DisplayFun.GrdDisplayDuration(ShowSumWorkDuration);
        xmlChart = " <chart palette='4' caption='" + titel + "' rotateYAxisName='0' showValues='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='1'> ";
        xmlChart += xmlData + xmlStyle + "</chart>";

        litChartBeginLateDurtion.Text = FusionCharts.RenderChart("../FusionCharts/Pie2D.swf", "", xmlChart, "ChartBeginLateDurtion", "100%", "350", false, false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartAbsentDays(bool EmpDay, DataTable DT, string EmpID, string Date)
    {
        LitChartAbsentDays.Text = General.Msg("No data as required", "لا يوجد بيانات حسب الخيارات المطلوبة");
        string xmlChart = "";
        string xmlData = "";
        string titel = "";
        string xmlStyle = "";

        object SumWorkDays = "0";
        object SumAbsentDays = "1";
        object DsmStatus = "N";
        string SumNotDuration = "0";

        object ShowSumWorkDays = "0";
        object ShowSumAbsentDays = "0";
        string ShowSumNotDuration = "0";

        DataTable CDT = new DataTable();
        if (!EmpDay) { CDT = DT; }
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

        if (!EmpDay)
        {
            xmlData += "<set label='" + General.Msg("Total days of absence", "مجموع أيام الغياب") + " " + ShowSumAbsentDays + "' value='" + SumAbsentDays + "' isSliced ='1' color='e6ff1e'/>";
            xmlData += "<set label='" + General.Msg("Total working days", "مجموع أيام العمل") + " " + ShowSumNotDuration + "' value='" + SumNotDuration + "' color='b0e0e6' />";
        }
        else
        {
            xmlData += "<set label='" + DisplayFun.GrdDisplayDayStatus(DsmStatus, pgCs.Version) + "' value='" + SumAbsentDays + "' isSliced ='1' color='e6ff1e'/>";
            xmlData += "<set label='" + DisplayFun.GrdDisplayDayStatus(DsmStatus, pgCs.Version) + "' value='" + SumWorkDays + "' color='b0e0e6' />";
        }

        xmlStyle = " <styles> "
                + " <definition> "
                + "  <style name='CaptionAnim'  type='animation' param='_y' easing='Bounce' start='0' duration='2' /> "
                + "  <style name='CaptionFont'  type='font' isHTML='1' font='GE SS Two Light' size='18' color='666666' bold='1' underline='0' /> "
                + "  <style name='AxisNameFont' type='font' isHTML='1' font='GE SS Two Light' size='14' color='666666' bold='1' /> "
                + "  <style name='DataFont'     type='font' isHTML='1' font='GE SS Two Light' size='12' color='666666' /> "
                + " </definition> "
                + " <application> "
                + " <apply toObject='TOOLTIP'     styles='AxisNameFont' /> "
                + " <apply toObject='xAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='yAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='DataLabels'  styles='DataFont' /> "
                + " <apply toObject='DataValues'  styles='DataFont' /> "

                + " <apply toObject='Caption'     styles='CaptionFont' /> " //,CaptionAnim
                + " </application> "
                + " </styles> ";

        if (!EmpDay) { titel = General.Msg("Total working days required", "مجموع أيام العمل المطلوبة") + " " + SumWorkDays; }
        else { titel = General.Msg("Status of the day", "حالة اليوم"); }
        xmlChart = " <chart palette='4' caption='" + titel + "' rotateYAxisName='0' showValues='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='" + (!EmpDay ? "1" : "0") + "'> ";
        xmlChart += xmlData + xmlStyle + "</chart>";

        LitChartAbsentDays.Text = FusionCharts.RenderChart("../FusionCharts/Pie2D.swf", "", xmlChart, "ChartAbsentDays", "100%", "350", false, false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChartDurations(DataTable DT)
    {
        LitChartDurations.Text = General.Msg("No data as required", "لا يوجد بيانات حسب الخيارات المطلوبة");
        string xmlChart = "";
        string xmlData = "";
        string titel = "";
        string xmlStyle = "";

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

        xmlData += "<set label='" + General.Msg("work hours required", "ساعات العمل المطلوبة") + " " + DisplayFun.GrdDisplayDuration(ShowSumShiftDuration) + "' value='" + DisplayDuration(SumShiftDuration) + "' />";
        xmlData += "<set label='" + General.Msg("actual hours required", "ساعات العمل الفعلية") + " " + DisplayFun.GrdDisplayDuration(ShowSumWorkDuration) + "' value='" + DisplayDuration(SumWorkDuration) + "' />";
        xmlData += "<set label='" + General.Msg("hours of gaps without Excuse", "ساعات الثغرات بدون إستئذان") + " " + DisplayFun.GrdDisplayDuration(ShowSumGapsDuration) + "' value='" + DisplayDuration(SumGapsDuration) + "' />";

        xmlStyle = " <styles> "
                + " <definition> "
                + "  <style name='CaptionAnim'  type='animation' param='_y' easing='Bounce' start='0' duration='2' /> "
                + "  <style name='CaptionFont'  type='font' isHTML='1' font='GE SS Two Light' size='18' color='666666' bold='1' underline='0' /> "
                + "  <style name='AxisNameFont' type='font' isHTML='1' font='GE SS Two Light' size='14' color='666666' bold='1' /> "
                + "  <style name='DataFont'     type='font' isHTML='1' font='GE SS Two Light' size='12' color='666666' /> "
                + " </definition> "
                + " <application> "
                + " <apply toObject='TOOLTIP'     styles='AxisNameFont' /> "
                + " <apply toObject='xAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='yAxisName'   styles='AxisNameFont' /> "
                + " <apply toObject='DATALABELS'  styles='DataFont' /> "
                + " <apply toObject='DataValues'  styles='DataFont' /> "
                + " <apply toObject='Caption'     styles='CaptionFont' /> "
                + " </application> "
                + " </styles> ";

        titel = General.Msg("Total working periods", "مجموع فترات العمل");

        xmlChart = " <chart palette='1' caption='" + titel + "' xAxisName='" + General.Msg("Total", "المجموع") + "' "
                + " yAxisName='" + General.Msg("Hour", "ساعة") + "' rotateYAxisName='0' showValues='0' decimals='0' showborder='0' formatNumberScale='0' labelDisplay='Stagger' > ";
        xmlChart += xmlData + xmlStyle + "</chart>";

        LitChartDurations.Text = FusionCharts.RenderChart("../FusionCharts/Column2D.swf", "", xmlChart, "ChartDurations", "100%", "350", false, false);
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
}