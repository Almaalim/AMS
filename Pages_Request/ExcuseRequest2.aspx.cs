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
using System.Threading;
using System.Data.SqlClient;

public partial class ExcuseRequest2 : BasePage
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            if (pgCs.Lang == "AR") { LanguageSwitch.Href = "~/CSS/Metro/MetroAr.css"; } else { LanguageSwitch.Href = "~/CSS/Metro/Metro.css"; }
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense(); 
                BtnStatus("11");
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FindApprovalSequence();
                spnReqFile.Visible = GenCs.CheckFileRequired("EXC");

                if (Request.QueryString["ID"] != null)
                {
                    ViewState["ErqStatusTime"] = "P";
                    UIEnabled(true,false);
                    //lblEndDate.Visible = calEndDate.Visible = false;
                    
                    DataTable DT = DBCs.FetchData(" SELECT G.EmpID,G.GapDate,G.GapStartTime,G.GapEndTime,E.EmpNameAr,E.EmpNameEn From Gap G,Employee E WHERE G.EmpID = E.EmpID AND GapID = @P1 ", new string[] { Request.QueryString["ID"].ToString() });
                    if (!DBCs.IsNullOrEmpty(DT)) 
                    {
                        txtGapID.Text = Request.QueryString["ID"].ToString();
                        
                        calStartDate.SetGDate(DT.Rows[0]["GapDate"], pgCs.DateFormat);
                        
                        tpFrom.SetTime(Convert.ToDateTime(DT.Rows[0]["GapStartTime"]));
                        tpTo.SetTime(Convert.ToDateTime(DT.Rows[0]["GapEndTime"]));
                    }
                }
                else
                {
                    UIEnabled(true, true);
                    ViewState["ErqStatusTime"] = "F";
                    lblGapID.Visible = false;
                    txtGapID.Visible = false;
                    divName.Visible  = false;
                    //lblEndDate.Visible = calEndDate.Visible = false;
                }

                Session["AttendanceListMonth"] = DTCs.FindCurrentMonth();
                Session["AttendanceListYear"] = DTCs.FindCurrentYear();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PostbackFunction();", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillList()
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { lblName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn","EmpNameAr")].ToString(); }

            CtrlCs.FillExcuseTypeList(ref ddlExcType, rfvExcType, false, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindApprovalSequence()
    {
        try
        {
            bool isFind = GenCs.FindEmpApprovalSequence("EXC", pgCs.LoginEmpID);
            btnSave.Enabled = btnCancel.Enabled = isFind;
            if (!isFind) { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Info, "vgShowMsg", General.ApprovalSequenceMsg()); }
        }
        catch (Exception e1) { ErrorSignal.FromCurrentContext().Raise(e1); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus1, bool pStatus2)
    {
        txtGapID.Enabled   = pStatus2;
        calStartDate.SetEnabled(pStatus2);
        tpFrom.Enabled     = pStatus2;
        tpTo.Enabled       = pStatus2;
        ddlExcType.Enabled = pStatus1;
        txtDesc.Enabled    = pStatus1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        ProCs.RetID     = "EXC";
        ProCs.ErqTypeID = ddlExcType.SelectedValue;  
        ProCs.EmpID     = pgCs.LoginEmpID;
        if (Request.QueryString["ID"] != null) { ProCs.GapOvtID = txtGapID.Text; }

        ProCs.ErqStartDate  = calStartDate.getGDateDBFormat();
        ProCs.ErqEndDate    = ProCs.ErqStartDate;
        ProCs.ErqStartTime  = tpFrom.getDateTime(calStartDate.getGDate()).ToString();
        ProCs.ErqEndTime    = tpTo.getDateTime(calStartDate.getGDate()).ToString();
        ProCs.ErqReason     = txtDesc.Text;
        ProCs.ErqReqStatus  = "0";
        ProCs.ErqStatusTime = ViewState["ErqStatusTime"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        txtGapID.Text = "";
        ddlExcType.SelectedIndex = -1;
        txtDesc.Text = "";
        if (Request.QueryString["ID"] != null) { } else { calStartDate.ClearDate(); /**/ tpFrom.ClearTime(); /**/ tpTo.ClearTime(); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            BtnStatus("00");
            FillPropeties();

            try
            {
                if (!string.IsNullOrEmpty(fudReqFile.PostedFile.FileName))
                {
                    string dateFile = DateTime.Now.ToLongDateString() + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                    string GapID = txtGapID.Text;
                    string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                    string[] nameArr = FileName.Split('.');
                    string name = nameArr[0];
                    string type = nameArr[1];
                    string NewFileName = GapID + "-EXC." + type;
                    if (string.IsNullOrEmpty(GapID)) { NewFileName = pgCs.LoginEmpID + "-" + dateFile + "-EXC." + type; }
                    fudReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                    ProCs.ErqReqFilePath = NewFileName;
                }
            }
            catch { }

            int ID = SqlCs.Insert(ProCs);

            UIClear();
            BtnStatus("11");
            if (Request.QueryString["ID"] != null)
            {
                Session["ERSRefresh"] = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hideparentPopupSave('divPopup','../Pages_Attend/EmployeeGaps.aspx');", true);
            }
            else
            {
                CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Success, "vgShowMsg", General.Msg("Saved data successfully", "تم حفظ البيانات بنجاح"));
            }
        }
        catch (Exception ex) 
        { 
            BtnStatus("11");
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, vsShowMsg, cvShowMsg, "vgShowMsg", ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("11");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Exit();", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [Save,Cancel]
    {
        btnSave.Enabled   = GenCs.FindStatus(Status[0]);
        btnCancel.Enabled = GenCs.FindStatus(Status[1]);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void MaxTime_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string ReqSum = "";
        string GapSum = "";
        try
        {
            if (source.Equals(cvMaxTime))
            {
                if (!string.IsNullOrEmpty(calStartDate.getGDate()) && ddlExcType.SelectedIndex >0 )
                {
                    string Month = "";
                    string Year  = "";

                    if (pgCs.DateType == "Hijri")
                    {
                        Month = DTCs.GetHDatePart("M", calStartDate.getGDate(), pgCs.DateFormat);
                        Year  = DTCs.GetHDatePart("Y", calStartDate.getGDate(), pgCs.DateFormat);
                    }
                    else
                    {
                        Month = DTCs.GetGDatePart("M", calStartDate.getGDate(), pgCs.DateFormat);
                        Year  = DTCs.GetGDatePart("Y", calStartDate.getGDate(), pgCs.DateFormat);
                    }
                    
                    
                    DateTime SDate = DateTime.Now;
                    DateTime EDate = DateTime.Now;
                    DTCs.FindMonthDates(Year, Month, out SDate, out EDate);

                    int[] iFrom = new int[3] { tpFrom.getHours(), tpFrom.getMinutes(), tpFrom.getSeconds() };
                    int[] iTo   = new int[3] { tpTo.getHours(), tpTo.getMinutes(), tpTo.getSeconds() };
                    int Dur = DTCs.FindDuration(iFrom, iTo);

                    DataTable RDT = DBCs.FetchData(" SELECT SUM(DATEDIFF(S, ErqStartTime , ErqEndTime)) SUMDuration FROM EmpRequest WHERE EmpID = @P1 AND ErqStartDate BETWEEN @P2 AND @P3 AND RetID ='EXC' and ErqReqStatus in (0) AND ErqTypeID = @P4 ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString(), ddlExcType.SelectedValue });
                    //if (!DBCs.IsNullOrEmpty(RDT))

                    if (RDT.Rows[0]["SUMDuration"].ToString() != "0") { ReqSum = RDT.Rows[0]["SUMDuration"].ToString(); }

                    DataTable GDT = DBCs.FetchData(" SELECT SUM(GapDuration) SUMDuration FROM Gap WHERE EmpID = @P1 AND GapDate BETWEEN @P2 AND @P3 AND ExcID = @P4 ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString(), ddlExcType.SelectedValue });
                    if (GDT.Rows[0]["SUMDuration"].ToString() != "0") { GapSum = GDT.Rows[0]["SUMDuration"].ToString(); }

                    if (string.IsNullOrEmpty(ReqSum) || ReqSum == "0") { ReqSum = "0"; }
                    if (string.IsNullOrEmpty(GapSum) || GapSum == "0") { GapSum = "0"; }

                    //if ( ReqSum == "0" && GapSum == "0" ) { e.IsValid = true; }
                    //else
                    //{
                        DataTable MDT = DBCs.FetchData(" SELECT ExcMaxHoursPerMonth FROM ExcuseType WHERE ExcID = @P1 ", new string[] { ddlExcType.SelectedValue });
                        if (MDT.Rows[0]["ExcMaxHoursPerMonth"] == DBNull.Value) { e.IsValid = true; }
                        else if (MDT.Rows[0]["ExcMaxHoursPerMonth"].ToString() == "-1") { e.IsValid = true; }
                        else
                        {
                            //string ss = Maxdt.Rows[0]["ExcMaxHoursPerMonth"].ToString();

                            if (Convert.ToInt32(ReqSum) + Convert.ToInt32(GapSum) + Dur > Convert.ToInt32(MDT.Rows[0]["ExcMaxHoursPerMonth"]))
                            {
                                CtrlCs.ValidMsg(this, ref cvMaxTime, true, General.Msg("Have reached the maximum allowable This Excuse Type, you can not submit this request", "لقد بلغت الحد الأقصى المسموح لهذا النوع من الاستئذانات ,لا يمكنك تقديم هذا الطلب"));
                                e.IsValid = false;
                            }
                            else { e.IsValid = true; }
                        }
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Time_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {
            if (source.Equals(cvTime))
            {
                if (tpFrom.getIntTime() >= 0 && tpTo.getIntTime() >= 0)
                {
                    TimeSpan tsFrom = new TimeSpan(tpFrom.getHours(), tpFrom.getMinutes(), tpFrom.getSeconds());
                    TimeSpan tsTo = new TimeSpan(tpTo.getHours(), tpTo.getMinutes(), tpTo.getSeconds());

                    if (tsFrom.TotalSeconds >= tsTo.TotalSeconds) { e.IsValid = false; }
                }
            }
        }
        catch (Exception ex)
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void MaxTimeExcForShift_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        if (source.Equals(cvMaxTimeExcforShift) && ddlExcType.SelectedIndex > 0 && !String.IsNullOrEmpty(calStartDate.getGDate()) && !string.IsNullOrEmpty(tpFrom.getTime()) && !string.IsNullOrEmpty(tpTo.getTime()))
        {
            /////Get Max Percent From Excuse type
            int PercentMaxExc = ExcusePercentMax.GetPercentMaxExc(ddlExcType.SelectedValue.ToString());

            if (PercentMaxExc > 0)
            {
                int[] iFrom = new int[3] { tpFrom.getHours(), tpFrom.getMinutes(), tpFrom.getSeconds() };
                int[] iTo   = new int[3] { tpTo.getHours(), tpTo.getMinutes(), tpTo.getSeconds() };
                int DurRequest = DTCs.FindDuration(iFrom, iTo);

                /////Get total shift duration for Employee
                int SecondsShift = ExcusePercentMax.GetSecondsShiftDuration(pgCs.LoginEmpID, "Gregorian", calStartDate.getGDate());
                ////Get How Maximum Seconds he allwoabl
                int MaxSeconds = ExcusePercentMax.GetMaxSeconds(SecondsShift, PercentMaxExc);
                ////Get how much total Requested for emp and date and Excuse Type 
                int TotalRequestedExc = ExcusePercentMax.GetTotalRequestedExc(pgCs.LoginEmpID, "Gregorian", calStartDate.getGDate(), ddlExcType.SelectedValue);

                if (TotalRequestedExc + DurRequest > MaxSeconds)
                {
                    CtrlCs.ValidMsg(this, ref cvMaxTimeExcforShift, true, General.Msg("Have reached the maximum allowable This Excuse Type in this Day By " + DTCs.ShowDurtion(TotalRequestedExc + DurRequest - MaxSeconds) + ", you can not submit this request", "لقد بلغت نسبة الحد الأقصى المسموح لهذا النوع من الاستئذانات في هذا اليوم بزيادة " + DTCs.ShowDurtion(TotalRequestedExc + DurRequest - MaxSeconds) + ",لا يمكنك تقديم هذا الطلب"));
                    e.IsValid = false;
                }
                else { e.IsValid = true; }
            }
            else { e.IsValid = true; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvReqFile))
            {
                if (GenCs.CheckFileRequired("EXC")) { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName)) { e.IsValid = false; } }
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