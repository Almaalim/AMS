using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;
using System.Collections;

public partial class AutoOutWizard : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProCs = new EmployeePro();
    EmployeeSql SqlCs = new EmployeeSql();
    
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
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
            }
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
            StartStep.ImageUrl = "~/images/Wizard_Image/step_previous.png";
            FinishBackStep.ImageUrl = "images/Wizard_Image/step_next.png";
        }
        else
        {
            //MainTable.Attributes.Add("dir", "ltr");
            StartStep.ImageUrl = "~/images/Wizard_Image/step_next.png";
            FinishBackStep.ImageUrl = "images/Wizard_Image/step_previous.png";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        rdlAutoOut.SelectedIndex = -1;
        rdlAutoOut.SelectedIndex = -1;
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

    protected void btnFinishBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex = WizardData.ActiveStepIndex - 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFinishStep_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid) { return; }

            DataTable EmployeeInsertdt = ucEmployeeSelected.EmpSelected;

            if (!DBCs.IsNullOrEmpty(EmployeeInsertdt))
            {

                ProCs.EmpIDs = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);

                if (rdlAutoOut.Items[0].Selected) { ProCs.EmpAutoOutWizard = "1"; } else if (rdlAutoOut.Items[1].Selected) { ProCs.EmpAutoOutWizard = "0"; } else { ProCs.EmpAutoOutWizard = "2"; }
                if (rdlAutoIn.Items[0].Selected)  { ProCs.EmpAutoInWizard  = "1"; } else if (rdlAutoIn.Items[1].Selected)  { ProCs.EmpAutoInWizard  = "0"; } else { ProCs.EmpAutoInWizard  = "2"; }

                ProCs.TransactionBy = pgCs.LoginID;
                SqlCs.Wizard_AutoInOut(ProCs);
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


    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void WorkTimeValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEnWorkTimeName))
            {
                CtrlCs.ValidMsg(this, ref cvEnWorkTimeName, false, General.Msg("You must choose the type of Auto transaction required", "يجب اختيار نوع الحركة المطلوبة"));
                if ((rdlAutoOut.SelectedIndex == -1) && (rdlAutoIn.SelectedIndex == -1)) { e.IsValid = false; }

                if ((rdlAutoOut.SelectedIndex > -1))
                {
                    CtrlCs.ValidMsg(this, ref cvEnWorkTimeName, true, General.Msg("Make AutoOut eanbled in general configuration", "تأكد من تمكين الخيار AutoOut الإعدادات العامة"));
                    DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Configuration WHERE cfgAutoOut = 1 "));
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = true; } else { e.IsValid = false; }
                }
            }
            if (source.Equals(cvArWorkTimeName))
            {
                CtrlCs.ValidMsg(this, ref cvArWorkTimeName, false, General.Msg("You must choose the type of Auto transaction required", "يجب اختيار نوع الحركة المطلوبة"));
                if ((rdlAutoOut.SelectedIndex == -1) && (rdlAutoIn.SelectedIndex == -1)) { e.IsValid = false; } 
              

                if ((rdlAutoIn.SelectedIndex > -1))
                {
                     CtrlCs.ValidMsg(this, ref cvArWorkTimeName, true, General.Msg("Make AutoIn eanbled in general configuration", "تأكد من تمكين الخيار AutoIn الإعدادات العامة"));
                    DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Configuration WHERE cfgAutoIn = 1 "));
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = true; } else { e.IsValid = false; }
                }
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



