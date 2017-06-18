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

public partial class ShiftSwapRequest2 : BasePage
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
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense(); 
                UIEnabled(true);
                BtnStatus("11");
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FindApprovalSequence();
                rfvddlType.InitialValue = ddlType.Items[0].Text;
                divName.Visible = false;

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
    public bool IsWorkDays(string pDaysType, string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian"); 
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian"); 
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                DataTable DT = SqlCs.FetchWorkTime(Date, pEmpID, true);
                if (pDaysType == "Work") { if (DBCs.IsNullOrEmpty(DT)) { return false; } }
                if (pDaysType == "Off")  { if (!DBCs.IsNullOrEmpty(DT)) { return false; } }
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
    public string IsWorkTime(bool pDaysType, string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian");  
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian"); 
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);
            string wktID = "-1";

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                DataTable DT = SqlCs.FetchWorkTime(Date, pEmpID, pDaysType);
                if (!DBCs.IsNullOrEmpty(DT)) 
                {
                    if (i == 0) { wktID = DT.Rows[0]["WktID"].ToString(); }
                    else
                    {
                        if (wktID != DT.Rows[0]["WktID"].ToString()) { return "-1"; }
                    }
                }
            }
            return wktID;
        }
        catch (Exception e1)
        {
            return "-1";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindApprovalSequence()
    {
        try
        {
            bool isFind = GenCs.FindEmpApprovalSequence("SWP", pgCs.LoginEmpID);
            btnSave.Enabled = btnCancel.Enabled = isFind;
            if (!isFind) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.ApprovalSequenceMsg()); }
        }
        catch (Exception e1) { ErrorSignal.FromCurrentContext().Raise(e1); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        ddlType.Enabled = pStatus;
        calStartDate1.SetEnabled(pStatus);
        calEndDate1.SetEnabled(pStatus);
        txtEmpID2.Enabled = pStatus;
        calStartDate2.SetEnabled(pStatus);
        calEndDate2.SetEnabled(pStatus);
        txtDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        ProCs.RetID = "SWP";
        if (ddlType.SelectedValue == "Work") { ProCs.ErqTypeID = "1"; } else { ProCs.ErqTypeID = "2"; } 
        ProCs.EmpID = pgCs.LoginEmpID;
        ProCs.ErqReason = txtDesc.Text;
       
        ProCs.ErqReqStatus = "0";

        ProCs.EmpID2 = txtEmpID2.Text;
        ProCs.ErqEmp2ReqStatus = "0";
        ProCs.ErqStatusTime = "F";
        ProCs.WktID = txtWorktime.Text;

        ProCs.ErqStartDate  = calStartDate1.getGDateDBFormat();
        ProCs.ErqEndDate    = calEndDate1.getGDateDBFormat();
        ProCs.ErqStartDate2 = calStartDate2.getGDateDBFormat();
        ProCs.ErqEndDate2   = calEndDate2.getGDateDBFormat();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ddlType.SelectedIndex = -1;
        calStartDate1.ClearDate();
        calEndDate1.ClearDate();
        txtEmpID2.Text = "";
        calStartDate2.ClearDate();
        calEndDate2.ClearDate();
        txtDesc.Text = "";
        txtWorktime.Text = "";
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

            int res = SqlCs.Insert(ProCs);

            UIClear();
            BtnStatus("11");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Exit();", true);
        }
        catch (Exception ex) 
        { 
            BtnStatus("11");
            ErrorSignal.FromCurrentContext().Raise(ex); 
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
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

    protected void Employee_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmployee) && !String.IsNullOrEmpty(txtEmpID2.Text))
            {
                if (txtEmpID2.Text == pgCs.LoginEmpID)
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee, true, General.Msg("Can not swap between the times of your worktime", "لا يمكن إجراء مبادلة بين أوقات عملك"));
                    e.IsValid = false;
                }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee, true, General.Msg("No Employee with ID!", "لا يوجد موظف بهذا الرقم"));
                    DataTable DT = DBCs.FetchData(" SELECT * FROM Employee WHERE EmpID = @P1 AND EmpStatus = 'True' ", new string[] { txtEmpID2.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = true; } else { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DaysCount_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDaysCount))
        {
            if (!String.IsNullOrEmpty(calStartDate1.getGDate()) && !String.IsNullOrEmpty(calEndDate1.getGDate())
             && !String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate()))
            {
                DateTime StartDate1 = DTCs.ConvertToDatetime(calStartDate1.getGDate(), "Gregorian");
                DateTime EndDate1   = DTCs.ConvertToDatetime(calEndDate1.getGDate(), "Gregorian"); 
                DateTime StartDate2 = DTCs.ConvertToDatetime(calStartDate2.getGDate(), "Gregorian"); 
                DateTime EndDate2   = DTCs.ConvertToDatetime(calEndDate2.getGDate(), "Gregorian");
                
                int Days1 = Convert.ToInt32((EndDate1 - StartDate1).TotalDays + 1);
                int Days2 = Convert.ToInt32((EndDate2 - StartDate2).TotalDays + 1);

                if (Days1 != Days2)
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The number of days required for the swap is equal", "عدد الأيام المطلوبة للتبديل غير متساوية"));
                    e.IsValid = false;
                    return;
                }
            }
            e.IsValid = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays1))
        {
            if (!String.IsNullOrEmpty(calStartDate1.getGDate()) && !String.IsNullOrEmpty(calEndDate1.getGDate()) && ddlType.SelectedIndex > 0)
            {
                if (ddlType.SelectedValue == "Work")
                {
                    bool WorkDays = IsWorkDays("Work", calStartDate1.getGDate(), calEndDate1.getGDate(), pgCs.LoginEmpID);
                    if (!WorkDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the specific worktime in the given period", "لا يوجد لدى الموظف الأول عمل محدد في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
                else
                {
                    bool OffDays = IsWorkDays("Off", calStartDate1.getGDate(), calEndDate1.getGDate(), pgCs.LoginEmpID);
                    if (!OffDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the vacation in the given period", "لا يوجد لدى الموظف الأول إجازة في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
            }
        }
        else if (source.Equals(cvDays2))
        {
            if (!String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate()) && ddlType.SelectedIndex > 0 && !String.IsNullOrEmpty(txtEmpID2.Text))
            {
                if (ddlType.SelectedValue == "Work")
                {
                    bool OffDays = IsWorkDays("Off", calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (!OffDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني إجازة في الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
                else
                {
                    bool WorkDays = IsWorkDays("Work", calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (!WorkDays)
                    {
                        CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني عمل في الفترة المحددة"));
                        cvDays2.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='The employee has no other worktime in the specified period' /&gt;");
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
            }
        }
        ///////////////////////////////////////////
        else if (source.Equals(cvWorkTime))
        {
            if (!String.IsNullOrEmpty(calStartDate2.getGDate()) && !String.IsNullOrEmpty(calEndDate2.getGDate())
                && ddlType.SelectedIndex > 0 && !String.IsNullOrEmpty(pgCs.LoginEmpID) && !String.IsNullOrEmpty(txtEmpID2.Text))
            {
                if (ddlType.SelectedValue == "Work")
                {
                    string WorkTime1 = IsWorkTime(true, calStartDate1.getGDate(), calEndDate1.getGDate(), pgCs.LoginEmpID);
                    string WorkTime2 = IsWorkTime(false, calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (WorkTime1 == "-1" || WorkTime2 == "-1" || (WorkTime1 !=WorkTime2 ) )
                    {
                        CtrlCs.ValidMsg(this, ref cvWorkTime, true, General.Msg("Working time in the period is the not same, the work can not be  swaped specified period", "وقت العمل في الفترة غير متشابه لا يمكن تبديل عمل الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        txtWorktime.Text = WorkTime1;
                        e.IsValid = true;
                    }
                }
                else
                {
                    string WorkTime1 = IsWorkTime(false, calStartDate1.getGDate(), calEndDate1.getGDate(), pgCs.LoginEmpID);
                    string WorkTime2 = IsWorkTime(true, calStartDate2.getGDate(), calEndDate2.getGDate(), txtEmpID2.Text);
                    if (WorkTime1 == "-1" || WorkTime2 == "-1" || (WorkTime1 != WorkTime2))
                    {
                        CtrlCs.ValidMsg(this, ref cvWorkTime, true, General.Msg("Working time in the period is the not same, the work can not be swaped" + " <br />" + "specified period", "وقت العمل في الفترة غير متشابه لا يمكن تبديل عمل الفترة المحددة"));
                        e.IsValid = false;
                    }
                    else
                    {
                        txtWorktime.Text = WorkTime1;
                        e.IsValid = true;
                    }
                }
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}