using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;

public partial class ExcuseWizard : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpExcRelPro ProCs = new EmpExcRelPro();
    EmpExcRelSql SqlCs = new EmpExcRelSql();
    
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
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                UILang();
                UIEnabled(true);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
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
            CtrlCs.FillExcuseTypeList(ref ddlExcType, rfvddlExcType, false, false);
            CtrlCs.FillWorkingTimeList(ref ddlWktID, rfvddlWktID, false, true);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UILang()
    {
        ImageButton StartStep      = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartStep") as ImageButton;
        ImageButton FinishBackStep = this.WizardData.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishBackStep") as ImageButton;

        if (pgCs.DateType == "AR")
        {
            //MainTable.Attributes.Add("dir", "rtl");
            StartStep.ImageUrl = "../images/Wizard_Image/step_previous.png";
            FinishBackStep.ImageUrl = "../images/Wizard_Image/step_next.png";
        }
        else
        {
            //MainTable.Attributes.Add("dir", "ltr");
            StartStep.ImageUrl = "../images/Wizard_Image/step_next.png";
            FinishBackStep.ImageUrl = "../images/Wizard_Image/step_previous.png";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ddlExcType.SelectedIndex = 0;
        ddlWktID.SelectedIndex = 0;
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        tpStartTime.SetTime(00, 00);
        tpEndTime.SetTime(00, 00);
        txtAvailable.Text = "";
        txtDesc.Text = "";
        txtPhoneNo.Text = "";
        ucEmployeeSelected.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlExcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //////not allow chose unActive Excuse
        DataTable DT = DBCs.FetchData("  SELECT * FROM ExcuseType WHERE ExcID =  @P1 ", new string[] { ddlExcType.SelectedValue });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (Boolean.Parse(DT.Rows[0]["ExcStatus"].ToString()) == false)
            {
                ddlExcType.SelectedIndex = 0;
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("You can not select this excuse, because is unactive", "لا يمكن استخدام هذا النوع من الاستئذان، لأنه غير مفعل"));
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Wizard Events

    protected void WizardData_PreRender(object sender, EventArgs e)
    {
        Repeater SideBarList = WizardData.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
        SideBarList.DataSource = WizardData.WizardSteps;
        SideBarList.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string GetClassForWizardStep(object wizardStep)
    {
        WizardStep step = wizardStep as WizardStep;
        if (step == null) { return ""; }
        int stepIndex = WizardData.WizardSteps.IndexOf(step);
        if (stepIndex < WizardData.ActiveStepIndex) { return "prevStep"; } else if (stepIndex > WizardData.ActiveStepIndex) { return "nextStep"; } else { return "currentStep"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void WizardData_ActiveStepChanged(object sender, EventArgs e) { }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Start Step Events

    protected void btnStartStep_Click(object sender, EventArgs e)
    {
       if (!CtrlCs.PageIsValid(this, VSStart)) { return; }
        WizardData.ActiveStepIndex = 1;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Next Step Events

    protected void btnBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex = WizardData.ActiveStepIndex - 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void btnNextStep_Click(object sender, EventArgs e) { }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Finish Step Events

    protected void btnFinishBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex = WizardData.ActiveStepIndex - 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void btnFinishStep_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid) { return; }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            DataTable EmployeeInsertdt = ucEmployeeSelected.EmpSelected;
            if (!DBCs.IsNullOrEmpty(EmployeeInsertdt))
            {
                ProCs.EmpIDs       = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);
                ProCs.ExcID        = ddlExcType.SelectedValue.ToString();
                ProCs.WktID        = ddlWktID.SelectedValue.ToString();
                ProCs.ExrStartDate = calStartDate.getGDateDBFormat();
                ProCs.ExrEndDate   = calEndDate.getGDateDBFormat();
                ProCs.ExrStartTime = tpStartTime.getDateTime().ToString();
                ProCs.ExrEndTime   = tpEndTime.getDateTime().ToString();
                ProCs.ExrDesc      = txtDesc.Text;

                ProCs.ExrISOvernight = false;
                ProCs.ExrIsOverTime  = false;
                ProCs.ExrIsStopped   = false;
                ProCs.ExrAddBy = "USR";
                ProCs.TransactionBy = pgCs.LoginID;

                SqlCs.Period_Insert_WithUpdateSummary(ProCs);

                ////////////////////////////////////////////////////////////////////// 
                CtrlCs.ShowSaveMsg(this);
                WizardData.ActiveStepIndex = 0;
                UIClear();
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void ExcuseTimeValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvExcuseTime))
            {
                int FromTime = tpStartTime.getIntTime();
                int ToTime = tpEndTime.getIntTime();

                if (FromTime <= 0 || ToTime <= 0 || FromTime > ToTime) { e.IsValid = false; } else { e.IsValid = true; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}