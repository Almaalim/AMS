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

public partial class EmpApprovalLevel : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpAppLevelPro ProCs = new EmpAppLevelPro();
    EmpAppLevelSql SqlCs = new EmpAppLevelSql();
    
    EmpRequestSql ReqSqlCs = new EmpRequestSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT DISTINCT EalLevelCount ,EmpID ,RetID, EmpNameAr, EmpNameEn FROM EmployeeApprovalLevelInfoView WHERE EmpStatus = 'True' ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                //if (((Hashtable)ViewState["ht"]).ContainsKey("Insert")) { btnAdd.Enabled = true; btnCancel.Enabled = true; }
                //if (((Hashtable)ViewState["ht"]).ContainsKey("Search")) { btnFilter.Enabled = true; }
                
                BtnStatus("1000");
                UIEnabled(false);
                //UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                changeVersion();
                ImageButton StartStep      = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartStep")       as ImageButton;
                ImageButton BackStep       = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnBackStep")         as ImageButton;
                ImageButton NextStep       = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnNextStep")         as ImageButton;
                ImageButton FinishBackStep = this.WizardData.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishBackStep") as ImageButton;

                StartStep.ImageUrl      = General.Msg("~/images/Wizard_Image/step_next.png","~/images/Wizard_Image/step_previous.png");
                BackStep.ImageUrl       = General.Msg("~/images/Wizard_Image/step_previous.png","~/images/Wizard_Image/step_next.png");
                NextStep.ImageUrl       = General.Msg("~/images/Wizard_Image/step_next.png","~/images/Wizard_Image/step_previous.png");
                FinishBackStep.ImageUrl = General.Msg("~/images/Wizard_Image/step_previous.png","~/images/Wizard_Image/step_next.png");
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
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT RetID,RetNameEn,RetNameAr FROM RequestType WHERE RetID NOT IN ('ATR','CTR') ORDER BY RetOrder "));
            if (!DBCs.IsNullOrEmpty(DT)) 
            { 
                CtrlCs.PopulateDDL(ddlRetID     , DT, General.Msg("RetNameEn","RetNameAr"), "RetID", General.Msg("- Select Request Type -","- اختر نوع الطلب -"));
                CtrlCs.PopulateDDL(ddlRetID_viw0, DT, General.Msg("RetNameEn","RetNameAr"), "RetID", General.Msg("- Select Request Type -","- اختر نوع الطلب -"));
                rfvRetID_viw0.InitialValue = ddlRetID_viw0.Items[0].Text;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getRequestType(object pRetID)
    {
        try
        {
            if (pRetID == DBNull.Value) { return string.Empty; }
            
            DataTable DT = DBCs.FetchData(" SELECT RetNameEn, RetNameAr FROM RequestType WHERE RetID = @P1 ", new string[] { pRetID.ToString() });
            if (!DBCs.IsNullOrEmpty(DT)) { return General.Msg(DT.Rows[0]["RetNameEn"].ToString(),DT.Rows[0]["RetNameAr"].ToString()); }
            return string.Empty;
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void changeVersion()
    {
       
        if (pgCs.Version == "BorderGuard")
        {
            btnAdd.Visible = btnModify.Visible = btnSave.Visible = btnCancel.Visible = false;
        }
        else // (PageSession.ActiveVersion == "General")
        {
            btnAdd.Visible = btnModify.Visible = btnSave.Visible = btnCancel.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UIClearWizard()
    {
        ddlRetID_viw0.SelectedIndex = -1;
        ucManagersSelectedWizard.Clear();
        ucEmployeeSelected.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            if (ddlFilter.SelectedIndex == 1) { sql += " AND EmpID     LIKE @EmpID ";    /**/ cmd.Parameters.AddWithValue("@EmpID", txtFilter.Text.Trim() + "%"); } 
            if (ddlFilter.SelectedIndex == 2) { sql += " AND EmpNameAr LIKE @EmpNameAr"; /**/ cmd.Parameters.AddWithValue("@EmpNameAr", "%" + txtFilter.Text.Trim() + "%"); } 
            if (ddlFilter.SelectedIndex == 3) { sql += " AND EmpNameEn LIKE @EmpNameEn"; /**/ cmd.Parameters.AddWithValue("@EmpNameEn", "%" + txtFilter.Text.Trim() + "%"); } 
            if (ddlFilter.SelectedIndex == 4) { sql += " AND EalMgrID  LIKE @EalMgrID";  /**/ cmd.Parameters.AddWithValue("@EalMgrID",  "%" + txtFilter.Text.Trim() + "%"); } 
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        cmd.CommandText = sql;
        FillGrid(cmd);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        ucManagersSelected.Enabled(pStatus);
        //ucManagersSelectedWizard.Enabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        ddlRetID.SelectedIndex = -1;
        ucManagersSelected.Clear();
        txtEmpID.Text = "";
        txtEmpNameAr.Text = "";
        txtEmpNameEn.Text = "";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        UIClear();
        ViewState["CommandName"] = "ADD";
        UIEnabled(false);
        BtnStatus("0001");

        MultiView1.ActiveViewIndex = 1;
        if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1) { CtrlCs.FillGridEmpty(ref grdData, 50); }
        UIClearWizard();
        WizardData.ActiveStepIndex = 0;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        BtnStatus("0011");
        MultiView1.ActiveViewIndex = 0;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        AMSMasterPage master = (AMSMasterPage)this.Master;
        try
        {
            bool sucess = false;
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }
            
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            if (commandName == "EDIT")
            {
                ProCs.TransactionBy = pgCs.LoginID;
                ProCs.EalLevelCount = ucManagersSelected.getLevel().ToString();
                ProCs.EmpIDs = txtEmpID.Text;
                ProCs.RetID  = ddlRetID.SelectedValue.ToString();

                string levelIDs = "";
                string MgrIDs   = "";

                int count     = ucManagersSelected.getLevel();
                string[] Mgrs = ucManagersSelected.getMgr();

                for (int k = 0; k < count; k++)
                {
                    if (string.IsNullOrEmpty(levelIDs)) { levelIDs = k.ToString(); } else { levelIDs += ',' + k.ToString(); }
                    if (string.IsNullOrEmpty(MgrIDs))   { MgrIDs   = Mgrs[k]; }      else { MgrIDs   += '-' + Mgrs[k]; } 
                }

                ProCs.EalMgrIDs   = MgrIDs;
                ProCs.EalLevelIDs = levelIDs;
                SqlCs.Insert(ProCs);
                    
                master.FoundRequest();
            }

            if (sucess)
            {
                btnFilter_Click(null,null);
                CtrlCs.ShowSaveMsg(this);
                MultiView1.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        UIClearWizard();
        BtnStatus("1000");
        //UIEnabled(false);
        MultiView1.ActiveViewIndex = 0;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        DropDownList _ddlPager = CtrlCs.PagerList(grdData);
                        _ddlPager.SelectedIndexChanged += new EventHandler(ddlPager_SelectedIndexChanged);
                        Table pagerTable = e.Row.Cells[0].Controls[0] as Table;
                        pagerTable.Rows[0].Cells.Add(CtrlCs.PagerCell(_ddlPager));
                        break;
                    }
                 default:
                    {
                        e.Row.Cells[4].Visible = false; //To hide ID column in grid view
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdData.PageIndex = 0;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        Hashtable ht = (Hashtable)ViewState["ht"];
                        if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                        delBtn.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string[] IDs = e.CommandArgument.ToString().Split(',');
                    ProCs.EmpID = IDs[0];
                    ProCs.RetID = IDs[1];
                    bool re = SqlCs.Delete(ProCs.EmpID, ProCs.RetID, pgCs.LoginID);
                    
                    btnFilter_Click(null,null);
                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataView dataView = new DataView(DT);

            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = "ASC";
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }
            else
            {
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }

            dataView.Sort = e.SortExpression + " " + sortDirection;

            grdData.DataSource = dataView;
            grdData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ConvertSortDirectionToSql(string sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;

            case "DESC":
                newSortDirection = "ASC";
                break;
        }

        return newSortDirection;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(false);
            BtnStatus("1000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[0].Text, grdData.SelectedRow.Cells[4].Text);
                BtnStatus("1100");
                MultiView1.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID,string pType)
    {
        try
        {
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("EmpID = " + pID + " AND  RetID = '" + pType + "'");

            txtEmpID.Text     = DRs[0]["EmpID"].ToString();
            txtEmpNameAr.Text = DRs[0]["EmpNameAr"].ToString();
            txtEmpNameEn.Text = DRs[0]["EmpNameEn"].ToString();
           
            ddlRetID.SelectedIndex = ddlRetID.Items.IndexOf(ddlRetID.Items.FindByValue(DRs[0]["RetID"].ToString()));

            DataTable MDT = DBCs.FetchData(" SELECT distinct EalMgrID,EalLevelSequenceNo FROM EmpApprovalLevel WHERE EmpID = @P1 AND RetID = @P2 ORDER BY EalLevelSequenceNo ", new string[] { DRs[0]["EmpID"].ToString(), DRs[0]["RetID"].ToString() });
            if (!DBCs.IsNullOrEmpty(MDT))
            {
                ucManagersSelected.setLevel(Convert.ToInt32(DRs[0]["EalLevelCount"]));
                ucManagersSelected.setMgr(MDT);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(SqlCommand cmd)
    {
        DataTable GDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            grdData.DataSource = (DataTable)GDT;
            ViewState["grdDataDT"] = (DataTable)GDT;
            grdData.DataBind();
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region viw0 Events

    protected void ddlRetID_viw0_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        ucEmployeeSelected.SetCondition(" SELECT DISTINCT EmpID FROM EmpApprovalLevel WHERE RetID = '" + ddlRetID_viw0.SelectedValue + "'");
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
            string commandName = Convert.ToString(ViewState["CommandName"]);
            if (commandName == string.Empty) { return; }
            if (!Page.IsValid) { return; }

            DataTable EmployeeInsertdt = ucEmployeeSelected.EmpSelected;
            if (!DBCs.IsNullOrEmpty(EmployeeInsertdt))
            {
                ProCs.TransactionBy = pgCs.LoginID;
                
                ProCs.EmpIDs        = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);
                ProCs.EalLevelCount = ucManagersSelectedWizard.getLevel().ToString();
                ProCs.RetID         = ddlRetID_viw0.SelectedValue.ToString();

                string levelIDs = "";
                string MgrIDs   = "";

                int count     = ucManagersSelectedWizard.getLevel();
                string[] Mgrs = ucManagersSelectedWizard.getMgr();

                for (int k = 0; k < count; k++)
                {
                    if (string.IsNullOrEmpty(levelIDs)) { levelIDs = k.ToString(); } else { levelIDs += ',' + k.ToString(); }
                    if (string.IsNullOrEmpty(MgrIDs))   { MgrIDs   = Mgrs[k]; }      else { MgrIDs   += '-' + Mgrs[k]; } 
                }

                ProCs.EalMgrIDs   = MgrIDs;
                ProCs.EalLevelIDs = levelIDs;
                SqlCs.Insert(ProCs);
            }
            //////////////////////////////////////////////////////////////////////
            btnFilter_Click(null, null);
            MultiView1.ActiveViewIndex = 0;
            UIClearWizard();
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            CtrlCs.ShowAdminMsg(this, ex.ToString());
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

    protected void Warning_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvDeleteReq))
            {
                CtrlCs.ValidMsg(this, ref cvDeleteReq, true, General.Msg("This employee has requests of this Type are still waiting, when pressed to save will be deleted and not deleted press Cancel"
                                                                          , "هذا الموظف يملك طلبات من هذا النوع ما زالت في الانتظار ، عند الضغط على حفظ سيتم حذفها ولعدم حذفها الضغط على إلغاء"));
                DataTable DT = DBCs.FetchData(" SELECT ErqID FROM EmpRequest WHERE EmpID = @P1 AND RetID = @P2 AND ErqReqStatus = 0 ", new string[] { txtEmpID.Text, ddlRetID.SelectedValue });
                if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
            }
        }
        catch { e.IsValid = true; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}