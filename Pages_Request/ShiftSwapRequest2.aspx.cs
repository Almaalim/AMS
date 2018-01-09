using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
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
            if (pgCs.Lang == "AR") { LanguageSwitch.Href = "~/CSS/Metro/MetroAr.css"; } else { LanguageSwitch.Href = "~/CSS/Metro/Metro.css"; }
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
                divName.Visible = false;

                Session["AttendanceListMonth"] = DTCs.FindCurrentMonth();
                Session["AttendanceListYear"]  = DTCs.FindCurrentYear();
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
            txtEmpID1.Text = pgCs.LoginEmpID;

            if (pgCs.Version == "SANS")
            {
                divCat1.Visible = divCat2.Visible = true;
                CtrlCs.FillCategoryList(ref ddlCatID, null, false);
            }
            
            DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn,CatID FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                lblName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn","EmpNameAr")].ToString();

                if (pgCs.Version == "SANS")
                {
                    ddlCatID.SelectedIndex = ddlCatID.Items.IndexOf(ddlCatID.Items.FindByValue(Convert.ToString(DT.Rows[0]["CatID"])));
                    string Wh = " AND EmpID !='" + pgCs.LoginEmpID + "' AND CatID = " ;
                    Session["EmpConSelect"] = string.IsNullOrEmpty(Convert.ToString(DT.Rows[0]["CatID"])) ? Wh + " 0 " : Wh + DT.Rows[0]["CatID"].ToString();
                }
            } 
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsWorkDays(string iType, string iStartDate, string ipEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime Date = DTCs.ConvertToDatetime(iStartDate, "Gregorian");
            
            DataTable DT = SqlCs.FetchWorkTime(Date, ipEmpID, true);
            if (iType == "Work") { if (DBCs.IsNullOrEmpty(DT))  { return false; } }
            if (iType == "Off")  { if (!DBCs.IsNullOrEmpty(DT)) { return false; } }
           
            return true;
        }
        catch (Exception e1)
        {
            return false;
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
            if (!isFind) { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Info, "vgShowMsg", General.ApprovalSequenceMsg()); }
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
        txtEmpID1.Enabled = false;
        ddlType.Enabled = pStatus;
        ddlCatID.Enabled = false;
        calStartDate1.SetEnabled(pStatus);
        txtEmpID2.Enabled = pStatus;
        calStartDate2.SetEnabled(pStatus);
        txtDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        ProCs.RetID = "SWP";
        ProCs.ErqTypeID = ddlType.SelectedValue; 
        ProCs.EmpID     = pgCs.LoginEmpID;
        ProCs.ErqStartDate  = calStartDate1.getGDateDBFormat();

        ProCs.EmpID2 = txtEmpID2.Text;
        ProCs.ErqStartDate2 = calStartDate2.getGDateDBFormat();

        ProCs.ErqReason = txtDesc.Text;

        ProCs.ErqReqStatus = "0";
        ProCs.ErqEmp2ReqStatus = "0";
        ProCs.ErqStatusTime = "F";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ddlType.SelectedIndex = -1;
        calStartDate1.ClearDate();
        txtEmpID2.Text = "";
        calStartDate2.ClearDate();
        txtDesc.Text = "";
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
            CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Success, "vgShowMsg", General.Msg("Saved data successfully", "تم حفظ البيانات بنجاح"));
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
            if (source.Equals(cvEmployee2))
            {
                if (string.IsNullOrEmpty(txtEmpID2.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvEmployee2, false, General.Msg("swap With Employee ID is required", "التبديل مع رقم الموظف مطلوب"));
                    e.IsValid = false;
                }
                else
                {
                    if (txtEmpID2.Text == txtEmpID1.Text)
                    {
                        CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("Can not swap between the times of your worktime", "لا يمكن إجراء مبادلة بين أوقات عملك"));
                        e.IsValid = false;
                    }
                    else
                    {
                        CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("No Employee with ID", "لا يوجد موظف بهذا الرقم"));
                        DataTable DT = DBCs.FetchData(" SELECT EmpID, CatID FROM spActiveEmployeeView WHERE EmpID = @P1 ", new string[] { txtEmpID2.Text });
                        if (DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                        else
                        {
                            if (pgCs.Version == "SANS")
                            {
                                CtrlCs.ValidMsg(this, ref cvEmployee2, true, General.Msg("Category does not match the specified employee", "التصنيف غير متطابق مع الموظف المحدد"));
                                if (string.IsNullOrEmpty(Convert.ToString(DT.Rows[0]["CatID"])) || string.IsNullOrEmpty(Convert.ToString(ddlCatID.SelectedValue))) { e.IsValid = false; }
                                else if (DT.Rows[0]["CatID"].ToString() != ddlCatID.SelectedValue) { e.IsValid = false; }
                            }
                        }       
                    }    
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days1_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays1))
        {
            if (ddlType.SelectedIndex > 0)
                if (ddlType.SelectedValue == "1" || ddlType.SelectedValue == "2") //"Work_work" || "Work_Off"
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the specific worktime in the given period", "لا يوجد لدى الموظف الأول عمل محدد في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID1.Text) && !string.IsNullOrEmpty(calStartDate1.getGDate()))
                    {
                        bool WorkDays = IsWorkDays("Work", calStartDate1.getGDate(), txtEmpID1.Text);
                        if (!WorkDays) { e.IsValid = false; }
                    }
                }
                
                if (ddlType.SelectedValue == "3") //Off_work
                {
                    CtrlCs.ValidMsg(this, ref cvDays1, true, General.Msg("The first employee does not have the vacation in the given period", "لا يوجد لدى الموظف الأول إجازة في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID1.Text) && !string.IsNullOrEmpty(calStartDate1.getGDate()))
                    {
                        bool OffDays = IsWorkDays("Off", calStartDate1.getGDate(), txtEmpID1.Text);
                        if (!OffDays) { e.IsValid = false; }
                    }
                }               
            }
        }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days2_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (source.Equals(cvDays2))
        {
            if (ddlType.SelectedIndex > 0)
            {
                if (ddlType.SelectedValue == "1" || ddlType.SelectedValue == "3") //Work_work || Off_work
                {
                    CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني عمل في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID2.Text) && !string.IsNullOrEmpty(calStartDate2.getGDate()))
                    {
                        bool WorkDays = IsWorkDays("Work", calStartDate2.getGDate(), txtEmpID2.Text);
                        if (!WorkDays) { e.IsValid = false; }
                    }
                }
                
                if (ddlType.SelectedValue == "2") // Work_Off
                {
                    CtrlCs.ValidMsg(this, ref cvDays2, true, General.Msg("The second employee does not have the vacation in the given period", "لا يوجد لدى الموظف الثاني إجازة في الفترة المحددة"));
                    if (!string.IsNullOrEmpty(txtEmpID2.Text) && !string.IsNullOrEmpty(calStartDate2.getGDate()))
                    {
                        bool OffDays = IsWorkDays("Off", calStartDate2.getGDate(), txtEmpID2.Text);
                        if (!OffDays) { e.IsValid = false; }
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