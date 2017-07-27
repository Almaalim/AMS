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

public partial class AppendingTransactionsWizard : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    TransDumpPro ProCs = new TransDumpPro();
    TransDumpSql SqlCs = new TransDumpSql();
    
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
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                WizardData.ActiveStepIndex = 0;

                spnReqFile.Visible = CheckFileRequired("ATR");
                ddlType.SelectedIndex = 1;
                FillLocationList("IN");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillLocationList(string Type)
    {
        string T = "True";
        if (Type == "IN") { T = "True"; } else { T = "False"; }

        ddlLocation.Items.Clear();
        
        DataTable DT = DBCs.FetchData(" SELECT * FROM Machine WHERE MacStatus = 1 AND ISNULL(MacDeleted,0) = 0 AND ISNULL(MacInOutType,@P1) = @P1 ", new string[] { T });
        if (!DBCs.IsNullOrEmpty(DT)) 
        { 
            CtrlCs.PopulateDDL(ddlLocation, DT, General.Msg("MacLocationEn", "MacLocationAr"), "MacID", General.Msg("-Select Location-", "-اختر موقع-")); 
            rfvLocation.InitialValue = ddlLocation.Items[0].Text;
        }
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
        CalDate.SetEnabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ddlType.SelectedIndex = 1;
        FillLocationList("IN");
        CalDate.ClearDate();
        tpickerTime.ClearTime();
        txtDesc.Text = "";
        ucEmployeeSelected.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex <= 0) { ddlLocation.Items.Clear(); } 
        else if (ddlType.SelectedIndex == 1) { FillLocationList("IN"); } 
        else if (ddlType.SelectedIndex == 2) { FillLocationList("OUT"); } 
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void WizardData_FinishButtonClick(object sender, WizardNavigationEventArgs e) { }

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
        
        try
        {
            ProCs.PostedFile   = null;
            ProCs.TrnFilePath = null;

            if (!string.IsNullOrEmpty(fudReqFile.PostedFile.FileName))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string dateFile = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                string[] names = FileName.Split('.');
                string NewFileName = "Wizard-" + dateFile + "-ATR." + names[1];
                ProCs.PostedFile   = fudReqFile.PostedFile;
                ProCs.TrnFilePath = NewFileName;
            }
        }
        catch(Exception ex) { }

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
                string IDs = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);
                
                ProCs.EmpIDs     = IDs;
                ProCs.TrnDate    = CalDate.getGDateDBFormat();
                ProCs.TrnTime    = tpickerTime.getDateTime().ToString();
                ProCs.TrnType    = ddlType.SelectedItem.Value;
                ProCs.MacID      = ddlLocation.SelectedValue;
                ProCs.UsrName    = pgCs.LoginID;            
                ProCs.TrnReason  = txtDesc.Text;
                ProCs.TransactionBy = pgCs.LoginID;

                if (!string.IsNullOrEmpty(ProCs.TrnFilePath) ) 
                {
                    try
                    {
                        ProCs.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + ProCs.TrnFilePath);
                        ProCs.TrnFilePath = ProCs.TrnFilePath;
                    }
                    catch(Exception ex) { }
                }

                bool res = SqlCs.Wizard_Trans_WithAttachment(ProCs);
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

    protected void TpickerTime_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvtpickerTime))  { if (tpickerTime.getIntTime()  < 0 )  { e.IsValid = false; } }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (source.Equals(cvReqFile))
        {
            if (CheckFileRequired("ATR"))
            {
                try { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName) ) { e.IsValid = false; } } catch { e.IsValid = false; }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckFileRequired(string ReqType)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgFormReq FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if      (DT.Rows[0]["cfgFormReq"] == DBNull.Value)                  { return false; }
            else if (string.IsNullOrEmpty(DT.Rows[0]["cfgFormReq"].ToString())) { return false; }
            else if (!DT.Rows[0]["cfgFormReq"].ToString().Contains(ReqType))    { return false; }
            else { return true; }
        }
        else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
}
