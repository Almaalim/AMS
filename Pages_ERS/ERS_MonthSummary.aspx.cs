using System;
using System.Web.UI;
using Elmah;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class ERS_MonthSummary : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string MainQuery = "";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            MainQuery += " SELECT * FROM MonthSummary WHERE EmpID = '" + pgCs.LoginEmpID + "'";
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense();    
                /*** Common Code ************************************/

                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);

                string CurrentMonth = DTCs.FindCurrentMonth();
                string CurrentYear  = DTCs.FindCurrentYear();

                FillData(CurrentMonth, CurrentYear);
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(CurrentMonth));
                ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(CurrentYear));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
    protected void FillData(string Month, string Year)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery;

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(Year, Month, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            FQ.Append(MainQuery);
            FQ.Append(" AND CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@SDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@EDate,103) ");
            FQ.Append(" AND MsmCalendar = '" + (pgCs.DateType == "Gregorian" ? "G" : "H") + "'");
                
            cmd.Parameters.AddWithValue("@SDate", SDate);
            cmd.Parameters.AddWithValue("@EDate", EDate);
            sql = FQ.ToString();
            cmd.CommandText = sql;

            ClearSummary();
            DataTable DT = DBCs.FetchData(cmd);
            if (!DBCs.IsNullOrEmpty(DT))
            {
                FillSummary(DT);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillSummary(DataTable DT) 
    {
        lblVMsmShiftDuration.Text            = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmShiftDuration"]); 
        lblVMsmWorkDuration.Text             = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmWorkDuration"]);
        lblVMsmWorkDurWithET.Text            = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmWorkDurWithET"]);
        lblVMsmBeginEarly.Text               = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmBeginEarly"]);
        lblVMsmBeginLate.Text                = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmBeginLate"]);
        lblVMsmOutEarly.Text                 = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOutEarly"]);
        lblVMsmOutLate.Text                  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOutLate"]);
        lblVMsmGapDur_MG.Text                = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_MG"]);
        lblVMsmGapDur_WithoutExc.Text        = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_WithoutExc"]);
        lblVMsmGapDur_PaidExc.Text           = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_PaidExc"]);
        lblVMsmGapDur_UnPaidExc.Text         = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_UnPaidExc"]);
        lblVMsmGapDur_Grace.Text             = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_Grace"]);
        lblVMsmGapDur_WithRule.Text          = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_WithRule"]);    
        lblVMsmExtraTimeDur_BeginEarly.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmExtraTimeDur_BeginEarly"]);
        lblVMsmOverTimeDur_BeginEarly.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOverTimeDur_BeginEarly"]);
        lblVMsmExtraTimeDur_OutLate.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmExtraTimeDur_OutLate"]);
        lblVMsmOverTimeDur_OutLate.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOverTimeDur_OutLate"]);
        lblVMsmExtraTimeDur_OutOfShift.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmExtraTimeDur_OutOfShift"]);
        lblVMsmOverTimeDur_OutOfShift.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOverTimeDur_OutOfShift"]);
        lblVMsmExtraTimeDur_NoShift.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmExtraTimeDur_NoShift"]);
        lblVMsmOverTimeDur_NoShift.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOverTimeDur_NoShift"]);
        lblVMsmExtraTimeDur_InVac.Text       = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmExtraTimeDur_InVac"]);
        lblVMsmOverTimeDur_InVac.Text        = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOverTimeDur_InVac"]);
        lblVMsmShifts_Present.Text           = DT.Rows[0]["MsmShifts_Present"].ToString();
        lblVMsmShifts_Absent_WithoutExc.Text = DT.Rows[0]["MsmShifts_Absent_WithoutExc"].ToString();
        lblVMsmShifts_Absent_PaidExc.Text    = DT.Rows[0]["MsmShifts_Absent_PaidExc"].ToString();
        lblVMsmShifts_Absent_UnPaidExc.Text  = DT.Rows[0]["MsmShifts_Absent_UnPaidExc"].ToString();
        lblVMsmShifts_Absent_WithRule.Text   = DT.Rows[0]["MsmShifts_Absent_WithRule"].ToString();
        lblVMsmDays_Work.Text                = DT.Rows[0]["MsmDays_Work"].ToString();
        lblVMsmDays_Present.Text             = DT.Rows[0]["MsmDays_Present"].ToString();
        lblVMsmDays_Absent_WithoutVac.Text   = DT.Rows[0]["MsmDays_Absent_WithoutVac"].ToString();
        lblVMsmDays_Absent_PaidVac.Text      = DT.Rows[0]["MsmDays_Absent_PaidVac"].ToString();
        lblVMsmDays_Absent_UnPaidVac.Text    = DT.Rows[0]["MsmDays_Absent_UnPaidVac"].ToString();
        lblVMsmDays_Absent_WithCom.Text      = DT.Rows[0]["MsmDays_Absent_WithCom"].ToString();
        lblVMsmDays_Absent_WithJob.Text      = DT.Rows[0]["MsmDays_Absent_WithJob"].ToString();
        lblVMsmDays_Absent_WithRule.Text     = DT.Rows[0]["MsmDays_Absent_WithRule"].ToString();
        //lblVMsmDaysAbsentDueToGaps.Text    = DT.Rows[0]["Msm"].ToString();
        //lblVMsmPendingGap.Text             = DT.Rows[0]["Msm"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearSummary()
    {
        string TimeDef = "00:00:00";
        string NoDef   = "0";
        
        lblVMsmShiftDuration.Text = lblVMsmWorkDuration.Text = lblVMsmWorkDurWithET.Text = TimeDef;
        lblVMsmBeginEarly.Text = lblVMsmBeginLate.Text = lblVMsmOutEarly.Text = lblVMsmOutLate.Text = TimeDef;
        lblVMsmGapDur_MG.Text = lblVMsmGapDur_WithoutExc.Text = lblVMsmGapDur_PaidExc.Text = lblVMsmGapDur_UnPaidExc.Text = TimeDef;
        lblVMsmGapDur_Grace.Text = lblVMsmGapDur_WithRule.Text = TimeDef;
        lblVMsmExtraTimeDur_BeginEarly.Text = lblVMsmOverTimeDur_BeginEarly.Text = TimeDef;
        lblVMsmExtraTimeDur_OutLate.Text = lblVMsmOverTimeDur_OutLate.Text = TimeDef;
        lblVMsmExtraTimeDur_OutOfShift.Text = lblVMsmOverTimeDur_OutOfShift.Text = TimeDef;
        lblVMsmExtraTimeDur_NoShift.Text = lblVMsmOverTimeDur_NoShift.Text = TimeDef;
        lblVMsmExtraTimeDur_InVac.Text = lblVMsmOverTimeDur_InVac.Text = TimeDef;
        lblVMsmShifts_Present.Text = lblVMsmShifts_Absent_WithoutExc.Text = lblVMsmShifts_Absent_PaidExc.Text = NoDef;
        lblVMsmShifts_Absent_UnPaidExc.Text = lblVMsmShifts_Absent_WithRule.Text = NoDef;
        lblVMsmDays_Work.Text = lblVMsmDays_Present.Text = NoDef;
        lblVMsmDays_Absent_WithoutVac.Text = lblVMsmDays_Absent_PaidVac.Text = lblVMsmDays_Absent_UnPaidVac.Text = NoDef;
        lblVMsmDays_Absent_WithCom.Text = lblVMsmDays_Absent_WithJob.Text = lblVMsmDays_Absent_WithRule.Text = NoDef;
        //lblVMsmDaysAbsentDueToGaps.Text = lblVMsmPendingGap.Text = NoDef;
    }
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