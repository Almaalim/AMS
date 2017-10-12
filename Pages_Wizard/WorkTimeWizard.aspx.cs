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
using System.Data.SqlClient;

public partial class WorkTimeWizard : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpWrkPro ProCs = new EmpWrkPro();
    EmpWrkSql SqlCs = new EmpWrkSql();
    
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
        ImageButton StartStep = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartStep") as ImageButton;
        ImageButton FinishBackStep = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnFinishBackStep") as ImageButton;

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
        ddlWktID.SelectedIndex = -1;
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        chkEwrSat.Checked = false;
        chkEwrSun.Checked = false;
        chkEwrMon.Checked = false;
        chkEwrTue.Checked = false;
        chkEwrWed.Checked = false;
        chkEwrThu.Checked = false;
        chkEwrFri.Checked = false;

        ucEmployeeSelected.Clear();
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
    protected void WizardData_ActiveStepChanged(object sender, EventArgs e)
    {
        //int step = Wizard1.ActiveStepIndex;    
        //// Disable validation for Step 2. (index is zero-based)   
        //if (step == 1)   
        //{     
        //    //ToggleValidation(false);   
        //    WebControl stepNavTemplate = this.Wizard1.FindControl("StepNavigationTemplateContainerID") as WebControl; 
        //    if (stepNavTemplate != null) 
        //    { 
        //        Button b = stepNavTemplate.FindControl("StepNextButton") as Button; 
        //        if (b != null) 
        //        { 
        //            //b.ValidationGroup = 
        //            b.CausesValidation = true; 
        //        } 
        //    } 
        //}   
        //else  // Enable validation for subsequent steps.   
        //{       
        //    //ToggleValidation(true);   
        //} 
    }

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
    protected void btnNextStep_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid)
        //{
        //    ValidatorCollection ValidatorColl = Page.Validators;
        //    for (int k = 0; k < ValidatorColl.Count; k++)
        //    {
        //        if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsumViw1.ShowSummary = true; return; }
        //        vsumViw1.ShowSummary = false;
        //    }
        //    return;
        //}
        //Wizard1.ActiveStepIndex = 1;
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Finish Step Events

    protected void btnExit_Click(object sender, EventArgs e) {  WizardData.ActiveStepIndex = 0; UIClear(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                ProCs.EmpIDs = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);
                
                ProCs.WktID         = ddlWktID.SelectedValue.ToString();
                ProCs.EwrStartDate  = calStartDate.getGDateDBFormat();
                ProCs.EwrEndDate    = calEndDate.getGDateDBFormat();
                ProCs.EwrSat        = chkEwrSat.Checked;
                ProCs.EwrSun        = chkEwrSun.Checked;
                ProCs.EwrMon        = chkEwrMon.Checked;
                ProCs.EwrTue        = chkEwrTue.Checked;
                ProCs.EwrWed        = chkEwrWed.Checked;
                ProCs.EwrThu        = chkEwrThu.Checked;
                ProCs.EwrFri        = chkEwrFri.Checked;
                ProCs.TransactionBy = pgCs.LoginID;
                
                string SaveIDs    = "";
                string notSaveIDs = "";
                SqlCs.Wkt_Insert_WithMoveBack(ProCs, out SaveIDs, out notSaveIDs);
                
                if (!string.IsNullOrEmpty(SaveIDs)) { txtResult.Text =  General.Msg("Emoloyees WorkTime details saved successfully for Employees " + SaveIDs , "تمت إضافة أوقات العمل بنجاح للموظفين " + SaveIDs); }
                
                if ( !string.IsNullOrEmpty(txtResult.Text) ) {  txtResult.Text += "\r\n"; txtResult.Text += "\r\n"; }
                
                if (!string.IsNullOrEmpty(notSaveIDs)) { txtResult.Text +=  General.Msg("You can not add worktime in the specified period of existence of previous Transaction for Employees " + notSaveIDs , "لا يمكن إضافة وقت عمل في الفترة المحددة لوجود حركات سابقة فيها للموظفين " + notSaveIDs); }

                WizardData.ActiveStepIndex = WizardData.ActiveStepIndex + 1;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }    
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindNestingEwr(string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, "Gregorian"); 
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, "Gregorian");
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);
                string strDate = DTCs.FormatGreg(Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy");
                
                DataTable DT = DBCs.FetchData(" SELECT * FROM Trans WHERE EwrID IS NOT NULL AND Convert(varchar(12),TrnDate,103) = @P1 AND EmpID = @P2 ", new string[] { strDate, pEmpID });
                if (!DBCs.IsNullOrEmpty(DT)) {  return true; }
            }
            return false;
        }
        catch (Exception e1)
        {
            return true;
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
    
    protected void SelectWorkDays_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (!chkEwrSat.Checked && !chkEwrSun.Checked && !chkEwrMon.Checked && !chkEwrTue.Checked && !chkEwrWed.Checked && !chkEwrThu.Checked && !chkEwrFri.Checked)
            {
                e.IsValid = false;
            }
            else { e.IsValid = true; }
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