using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Text;
using System.Threading;
using System.Data.SqlClient;

public partial class VacationRequest2 : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpRequestPro ProCs = new EmpRequestPro();
    EmpRequestSql SqlCs = new EmpRequestSql();
    VactionDaysCal VacCs = new VactionDaysCal();

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
                /*** get Requst Permission ***/ ViewState["ReqHT"] = pgCs.GetAllRequestPerm();
                BtnStatus("11");
                UIEnabled(true);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                VisbleReqTypeItem(); // This Function Control to Visble Or Unvisble ddlReqType items 

                if (Request.QueryString["Type"] != null)
                {
                    string Type = Request.QueryString["Type"].ToString();
                    if (Type == "ALL") 
                    {
                        if (pgCs.GetRequestPerm(ViewState["ReqHT"], "VAC"))
                        {
                            ReqTypeChange("VAC");
                            ddlReqType.SelectedIndex = 0;
                        }
                        else if (pgCs.GetRequestPerm(ViewState["ReqHT"], "COM"))
                        {
                            ReqTypeChange("COM");
                            ddlReqType.SelectedIndex = 1;
                        }
                        else if (pgCs.GetRequestPerm(ViewState["ReqHT"], "JOB"))
                        {
                            ReqTypeChange("JOB");
                            ddlReqType.SelectedIndex = 2;
                        }
                    }
                    //////////////////////
                    if (Type == "VAC") { ReqTypeChange("VAC"); ddlReqType.Enabled = false; ddlReqType.SelectedIndex = 0; }
                    if (Type == "COM") { ReqTypeChange("COM"); ddlReqType.Enabled = false; ddlReqType.SelectedIndex = 1; }
                    if (Type == "JOB") { ReqTypeChange("JOB"); ddlReqType.Enabled = false; ddlReqType.SelectedIndex = 2; }
                    if (Type == "LIC") { ReqTypeChange("LIC"); ddlReqType.Enabled = false; ddlReqType.SelectedIndex = 3; }
                }

                if (Request.QueryString["ID"] != null)
                {
                    string GDate = Request.QueryString["ID"].ToString();
                    calStartDate.SetGDate(GDate);
                    calEndDate.SetGDate(GDate);
                    
                    calStartDate.SetEnabled(false);
                    calEndDate.SetEnabled(false);
                    ViewState["ErqStatusTime"] = "P";
                }
                else
                {
                    calStartDate.SetEnabled(true);
                    calEndDate.SetEnabled(true);
                    ViewState["ErqStatusTime"] = "F";
                    divName.Visible = false;
                }

                Session["AttendanceListMonth"] = DTCs.FindCurrentMonth();
                Session["AttendanceListYear"]  = DTCs.FindCurrentYear();
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
            DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { lblName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn","EmpNameAr")].ToString(); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void changeVersion(string ReqType)
    {
        if (pgCs.Version == "Al_JoufUN" && ReqType == "VAC")
        {
            //cvReqDate.Enabled               = true;
            ddlVacHospitalType.AutoPostBack = true;
        }
        else // (ActiveVersion == "General")
        {
            //cvReqDate.Enabled               = false;
            ddlVacHospitalType.AutoPostBack = false;
            spVacHospitalType.Visible = lblVacHospitalType.Visible = ddlVacHospitalType.Visible = rvVacHospitalType.Enabled = false;
        }

        if (pgCs.Version != "BorderGuard")
        {
            ddlReqType.Items.FindByValue("LIC").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        //txtEmpID.Enabled = pStatus;
        ddlVacType.Enabled = pStatus;
        //fudReqFile.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
        txtPhone.Enabled = pStatus;
        txtAvailabitily.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (ddlReqType.SelectedValue == "LIC") { ProCs.RetID = "VAC"; } else { ProCs.RetID = ddlReqType.SelectedValue; }
            ProCs.ErqTypeID       = ddlVacType.SelectedValue;
            if (ddlVacHospitalType.SelectedIndex > 0) { ProCs.VacHospitalType = ddlVacHospitalType.SelectedValue; }
            ProCs.EmpID           = pgCs.LoginEmpID;          
            ProCs.ErqStartDate    = calStartDate.getGDateDBFormat();
            ProCs.ErqEndDate      = calEndDate.getGDateDBFormat();
            ProCs.ErqReason       = txtDesc.Text;
            ProCs.ErqAvailability = txtAvailabitily.Text;
            ProCs.ErqPhone        = txtPhone.Text;
            ProCs.ErqReqStatus    = "0";

            ProCs.ErqStatusTime = ViewState["ErqStatusTime"].ToString();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        if (Request.QueryString["ID"] == null) 
        { 
            calStartDate.ClearDate();
            calEndDate.ClearDate();
        }
        ddlVacType.SelectedIndex          = -1;
        ddlVacHospitalType.SelectedIndex  = -1;
        txtDesc.Text                      = "";
        txtPhone.Text                     = "";
        txtAvailabitily.Text              = "";  
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlVacType_SelectedIndexChanged(object sender, EventArgs e)
    {
        spVacHospitalType.Visible = lblVacHospitalType.Visible = ddlVacHospitalType.Visible = rvVacHospitalType.Enabled = false;

        if (ddlVacType.SelectedIndex > 0)
        {
            DataTable DT = DBCs.FetchData("SELECT * FROM VacationType  WHERE VtpIsMedicalReport = 'True' AND VtpID = @P1 ", new string[] { ddlVacType.SelectedValue });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (pgCs.Version == "Al_JoufUN")
                {
                    spVacHospitalType.Visible = lblVacHospitalType.Visible = ddlVacHospitalType.Visible = rvVacHospitalType.Enabled = true;
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillVacList(string ReqType)
    {
        CtrlCs.FillVacationTypeList(ref ddlVacType, rvExcType, true, false, ReqType);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ReqTypeChange(string ReqType)
    {

        string Type = (ReqType == "LIC") ? "VAC" : ReqType;
        bool isFoundApproval = GenCs.FindEmpApprovalSequence(Type, pgCs.LoginEmpID); 
        
        if (isFoundApproval) { btnSave.Enabled = btnCancel.Enabled = true; }
        else
        {
            btnSave.Enabled = btnCancel.Enabled = false;
            CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Info, "vgShowMsg", General.ApprovalSequenceMsg());
        }
        
        FillVacList(ReqType);
        spnReqFile.Visible = GenCs.CheckFileRequired(Type);
        changeVersion(ReqType);

        if (ReqType == "VAC")
        {
            divVACType.Visible = divDetail.Visible = true;
            rvExcType.Enabled = cvMaxDays.Enabled = true;
        }

        if (ReqType == "COM" || ReqType == "JOB" || ReqType == "LIC")
        {
            divVACType.Visible = divDetail.Visible = false;
            rvExcType.Enabled = cvMaxDays.Enabled = false;
            ddlVacType.SelectedIndex = 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlReqType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UIClear();
        ReqTypeChange(ddlReqType.SelectedValue);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void VisbleReqTypeItem()
    {
        Hashtable Reqht = (Hashtable)ViewState["ReqHT"];
        ddlReqType.Items.FindByValue("VAC").Enabled = Reqht.ContainsKey("VAC");
        ddlReqType.Items.FindByValue("COM").Enabled = Reqht.ContainsKey("COM");
        ddlReqType.Items.FindByValue("JOB").Enabled = Reqht.ContainsKey("JOB");
        ddlReqType.Items.FindByValue("LIC").Enabled = Reqht.ContainsKey("VAC");
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
                
            FillPropeties();
           
            try
            {
                if (!string.IsNullOrEmpty(fudReqFile.PostedFile.FileName))
                {
                    string dateFile = DateTime.Now.ToLongDateString() + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                    string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                    string[] nameArr = FileName.Split('.');
                    string name = nameArr[0];
                    string type = nameArr[1];
                    string NewFileName = pgCs.LoginEmpID + "-" + dateFile + "-VAC." + type;
                    fudReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                    ProCs.ErqReqFilePath = NewFileName;
                    //empReq.ErqReqFilePath = "\\\\" + ipServer + "\\" + Server.MapPath(@"./RequestsFiles/" + FileName).ToString().Substring(3);
                }
            }
            catch { }

            int ID = SqlCs.Insert(ProCs);
            //mailCs.SendMailToMGR(DT.Rows[0]["EalMgrID"].ToString(), ID.ToString(), pgCs.DateType, pgCs.Lang, mailCs.FindLoginUrl(Request.Url));
            //mail.Insert(MailRequest, ID.ToString(), Reqdt.Rows[0]["EalMgrID"].ToString());
            
           
            UIClear();
            BtnStatus("11");
            if (Request.QueryString["ID"] != null)
            {
                Session["ERSRefresh"] = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hideparentPopupSave('','AttendanceList.aspx');", true);
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

    protected void HaveTrans_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvHaveTrans))
            {
                if (!string.IsNullOrEmpty(calStartDate.getGDate()) && !string.IsNullOrEmpty(calEndDate.getGDate()))
                {
                    if (ddlReqType.SelectedValue == "LIC")
                    {
                        bool Have = HaveTrans(calStartDate.getGDate(), calEndDate.getGDate(), pgCs.LoginEmpID);
                        if (Have)
                        {
                            CtrlCs.ValidMsg(this, ref cvHaveTrans, true, General.Msg("There are Transaction on the date specified Please choose another date", "يوجد حركات في التاريخ المحدد الرجاء اختيار تاريخ آخر"));
                            e.IsValid = false;
                        }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void MaxDays_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvMaxDays) && ddlVacType.SelectedIndex > 0 && !String.IsNullOrEmpty(calStartDate.getGDate()) && !String.IsNullOrEmpty(calEndDate.getGDate()) )
            {
                int iStartDate = DTCs.ConvertDateTimeToInt(calStartDate.getGDate(),"Gregorian");
                int iEndDate   = DTCs.ConvertDateTimeToInt(calEndDate.getGDate(),"Gregorian");
                if (iStartDate <= iEndDate)
                {
                    bool NestingDates = VacCs.FindNestingDates("Gregorian", calStartDate.getGDate(), calEndDate.getGDate(), pgCs.LoginEmpID);
                    if ( !NestingDates )
                    {
                        int VacDays = VacCs.FindVacDays("Gregorian", pgCs.LoginEmpID, ddlVacType.SelectedValue,true);
                        if (VacDays == -1)
                        {
                            CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("Reset date has not been entered for vacations, or an error occurred please contact your system administrator", "لم يتم إدخال تاريخ حساب الإجازات، أو حدث خطأ ما الرجاء الاتصال بمدير النظام"));
                            e.IsValid = false;
                        }
                        else
                        {
                            int MaxDays = VacCs.FindMaxDays(pgCs.LoginEmpID, ddlVacType.SelectedValue);
                            int VacDaysRequest = VacCs.FindVacDaysRequest("Gregorian", calStartDate.getGDate(), calEndDate.getGDate()); 
                            if (MaxDays == 0)
                            {
                                e.IsValid = true;
                            }
                            else if (VacDays + VacDaysRequest > MaxDays)
                            {
                                CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("I have reached the maximum number of days for this type of vacations, " + "<br />" + " you can not submit a new request"
                                                                       , "لقد بلغت الحد الأقصى من الأيام لهذا النوع من الإجازات،لا يمكنك تقديم طلب جديد"));
                                
                                e.IsValid = false;
                            }
                            else
                            {
                                e.IsValid = true;
                            }
                        }
                    }
                    else
                    {
                        CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("There are other vacations on the date specified Please choose another date" , "يوجد إجازة أخرى في التاريخ المحدد الرجاء اختيار تاريخ آخر"));
                        e.IsValid = false;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays))
        {
            if (!string.IsNullOrEmpty(calStartDate.getGDate()) && !string.IsNullOrEmpty(calEndDate.getGDate()))
            {
                bool WorkDays = IsWorkDays(calStartDate.getGDate(), calEndDate.getGDate(), pgCs.LoginEmpID);
                if (!WorkDays)
                {
                    CtrlCs.ValidMsg(this, ref cvDays, true, General.Msg("You do not have a Worktime in the given period", "لا يوجد لديك عمل في الفترة المحددة"));
                    e.IsValid = false;
                }
                e.IsValid = true;
            }
        }
        else if (source.Equals(cvReqDate))
            {
                if (!string.IsNullOrEmpty(calStartDate.getGDate()) && !string.IsNullOrEmpty(calEndDate.getGDate()))
                {
                    int DaysLimit = GetDayslimit();
                    if (DaysLimit > 0)
                    {
                        DateTime today = DTCs.ConvertToDatetime(DateTime.Now.ToString("dd/MM/yyyy"), "Gregorian"); 
                        DateTime End   = DTCs.ConvertToDatetime(calEndDate.getGDate(), "Gregorian"); 
                        DateTime Last = End.AddDays(DaysLimit);

                        int itoday = DTCs.ConvertDateTimeToInt("Gregorian", today.ToString("dd/MM/yyyy"));
                        int iLast = DTCs.ConvertDateTimeToInt("Gregorian", Last.ToString("dd/MM/yyyy"));
                        if (itoday > iLast)
                        {
                            CtrlCs.ValidMsg(this, ref cvReqDate, true, General.Msg("You can not leave request, have exceeded " + DaysLimit.ToString() + " days from the date of the end of the vacation", "لا يمكنك طلب إجازة, لقد تجاوزت " + DaysLimit.ToString() + " ايام من تاريخ نهاية الاجازة"));
                            e.IsValid = false;
                        }
                        else { e.IsValid = true; }
                    }
                    else { e.IsValid = true; }
                }
            }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsWorkDays(string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian");
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian");
            DateTime Date      = StartDate;
            int Days           = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                DataTable DT = SqlCs.FetchWorkTime(Date, pEmpID, true);
                if (DBCs.IsNullOrEmpty(DT)) { return false; } 
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
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvReqFile))
            {
                string Type = (ddlReqType.SelectedValue == "LIC") ? "VAC" : ddlReqType.SelectedValue;
                if (GenCs.CheckFileRequired(Type)) { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName)) { e.IsValid = false; } }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int GetDayslimit()
    {
        int Days = 0;

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgDaysLimitReqVac FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            Days = Convert.ToInt32(DT.Rows[0]["cfgDaysLimitReqVac"].ToString());
        }

        return Days;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool HaveTrans(string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian"); 
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian");  
            
            StringBuilder Q = new StringBuilder();
            Q.Append(" SELECT TrnDate FROM Trans WHERE ISNULL(TrnFlagOT, 0) IN (0,1,2) AND EmpID =  @P1 AND TrnDate BETWEEN  @P2 AND @P3 ");
            Q.Append(" UNION ");
            Q.Append(" SELECT TrnDate FROM TransDump WHERE ISNULL(TrnFlagOT, 0) IN (0,1,2) AND EmpID =  @P1 AND TrnDate BETWEEN @P2 AND @P3 ");

            DataTable DT = DBCs.FetchData(Q.ToString(), new string[] { pEmpID, StartDate.ToString(), EndDate.ToString() });
            if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception e1)
        {
            return false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}