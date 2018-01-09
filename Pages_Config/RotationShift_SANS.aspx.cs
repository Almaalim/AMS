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

public partial class RotationShift_SANS : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    RotationPro ProCs = new RotationPro();
    RotationSql SqlCs = new RotationSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM RotationWorkTime WHERE RwtType = 'SANS' AND ISNULL(RwtDeleted,0) = 0 ";
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

            ImageButton _StartStep      = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("viw2_btnStartStep") as ImageButton;
            ImageButton _BackStep       = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("viw2_btnBackStep") as ImageButton;
            ImageButton _NextStep       = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("viw2_btnNextStep") as ImageButton;
            ImageButton _FinishBackStep = this.WizardData.FindControl("FinishNavigationTemplateContainerID").FindControl("viw2_btnFinishBackStep") as ImageButton;
            
            _StartStep.ImageUrl      = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            _BackStep.ImageUrl       = "~/images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");
            _NextStep.ImageUrl       = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            _FinishBackStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
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
            CtrlCs.FillWorkingTimeList1Sift(ref viw2_ddlWktID, null, true, false);
            CtrlCs.FillMgrsList2(ref viw2_ddlUser, null, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected DataTable EmptyDataTable()
    {
        DataTable Emptydt = new DataTable();
        Emptydt.Columns.Add(new DataColumn("EmpID", typeof(string)));
        Emptydt.Columns.Add(new DataColumn("EmpNameEn", typeof(string)));
        Emptydt.Columns.Add(new DataColumn("Department", typeof(string)));
        return Emptydt;
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

        if (ddlFilter.SelectedIndex > 0)
        {
            UIClear();
            if (ddlFilter.Text == "RwtID") 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedValue + " = @P1 ";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            }
            else 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedValue + " LIKE @P1 ";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
            }
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        UIWizardClear();
        MultiView1.ActiveViewIndex = 0;
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

    public void UILang()
    {
        if (pgCs.LangEn)
        {
            viw2_spnRwtNameEn.Visible = true;
        }

        if (pgCs.LangAr)
        {
            viw2_spnRwtNameAr.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool status)
    {
        viw1_txtRwtNameAr.Enabled            = status;
        viw1_txtRwtNameEn.Enabled            = status;
        viw1_txtRwtWorkDaysCount.Enabled     = status;
        viw1_txtRwtNotWorkDaysCount.Enabled  = status;
        viw1_txtRwtRotationDaysCount.Enabled = status;
        viw1_lstWorkTime.Enabled             = status;
        viw1_lstRotationGroup.Enabled        = status;
        viw1_lstEmployeeGroup.Enabled        = status;
        viw1_calStartDate.SetEnabled(status);
        viw1_calEndDate.SetEnabled(status);
       
        viw2_calStartDate.SetEnabled(true);
        viw2_calEndDate.SetEnabled(true);       
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
         try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!string.IsNullOrEmpty(viw2_txtRwtNameAr.Text)) { ProCs.RwtNameAr = viw2_txtRwtNameAr.Text; }
            if (!string.IsNullOrEmpty(viw2_txtRwtNameEn.Text)) { ProCs.RwtNameEn = viw2_txtRwtNameEn.Text; }
           
            ProCs.RwtFromDate          = viw2_calStartDate.getGDateDBFormat();
            ProCs.RwtToDate            = viw2_calEndDate.getGDateDBFormat();
            ProCs.RwtWorkDaysCount     = Convert.ToInt32(viw2_txtRwtWorkDaysCount.Text);
            ProCs.RwtNotWorkDaysCount  = Convert.ToInt32(viw2_txtRwtNotWorkDaysCount.Text);
            ProCs.RwtRotationDaysCount = Convert.ToInt32(viw2_txtRwtRotationDaysCount.Text);
            ProCs.RwtIsActive          = true;
            ProCs.RwtType              = "SANS";
           
            ProCs.WktIDs   = GenCs.CreateIDsNumber("V", viw2_lstWorkTime);
            ProCs.GrpIDs   = GenCs.CreateIDsNumber("T", viw2_lstRotationGrp);
            ProCs.GrpUsers = GenCs.CreateIDsNumber("T", viw2_lstRotationUsr);

            if (viw2_lstRotationGrp.Items.Count/(ProCs.RwtWorkDaysCount/ProCs.RwtRotationDaysCount) == 1)
            {
                ProCs.GrpBasicLen = viw2_lstRotationGrp.Items.Count;
                ProCs.IsDouble    = false;
            }
            else if (viw2_lstRotationGrp.Items.Count/(ProCs.RwtWorkDaysCount/ProCs.RwtRotationDaysCount) == 2)
            {
                ProCs.GrpBasicLen = viw2_lstRotationGrp.Items.Count/2;
                ProCs.IsDouble    = true;
            }

            string EmpIDs = "";
            for (int lG = 0; lG < viw2_lstRotationGrp.Items.Count; lG++)
            {
                DataTable EmpDT = viw2_ucEmployeeSelectedGroup.getEmpSelected(viw2_lstRotationGrp.Items[lG].Value);
                string Emps = GenCs.CreateIDsNumber("EmpID",EmpDT);
                if (string.IsNullOrEmpty(EmpIDs)) { EmpIDs += Emps; } else { EmpIDs += "-" + Emps; }   
            }
            ProCs.EmpIDs = EmpIDs;

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = ""; 
        
        viw1_txtRwtNameAr.Text            = "";
        viw1_txtRwtNameEn.Text            = "";
        viw1_txtRwtWorkDaysCount.Text     = "";
        viw1_txtRwtNotWorkDaysCount.Text  = "";
        viw1_txtRwtRotationDaysCount.Text = "";
        viw1_calStartDate.ClearDate();
        viw1_calEndDate.ClearDate();
        viw1_lstWorkTime.Items.Clear();
        viw1_lstRotationGroup.Items.Clear();
        viw1_lstEmployeeGroup.Items.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UIWizardClear()
    {
        viw2_txtRwtNameEn.Text            = "";
        viw2_txtRwtNameAr.Text            = "";
        viw2_txtRwtWorkDaysCount.Text     = "";
        viw2_txtRwtNotWorkDaysCount.Text  = "";
        viw2_txtRwtRotationDaysCount.Text = "";

        viw2_calStartDate.ClearDate();
        viw2_calEndDate.ClearDate();

        viw2_ddlWktID.SelectedIndex = -1;
        viw2_lstWorkTime.Items.Clear();
        viw2_txtGrpName.Text = "";
        viw2_lstRotationGrp.Items.Clear();
        viw2_lstRotationUsr.Items.Clear();

        viw2_ucEmployeeSelectedGroup.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw1_lstRotationGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        viw1_lstEmployeeGroup.Items.Clear();
        FillEmpList(ViewState["ID"].ToString(), viw1_lstRotationGroup.SelectedValue);
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
        try
        {
            UIClear();
            UIWizardClear();
            UIEnabled(true);
            BtnStatus("0001");
            ViewState["CommandName"] = "ADD";
            MultiView1.ActiveViewIndex = 1;
            WizardData.ActiveStepIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        try
        {
            viw3_calAddStartDate.ClearDate();
            viw3_calAddStartDate.SetEnabled(true);
            viw3_ucEmployeeSelectedGroup.Clear();
            BtnStatus("0001");
            ViewState["CommandName"] = "ADDEMP";
            viw3_ucEmployeeSelectedGroup.FillRotation(ViewState["ID"].ToString());
            MultiView1.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDelEmp_Click(object sender, EventArgs e)
    {
        try
        {
            viw4_calAddStartDate.ClearDate();
            viw4_calAddStartDate.SetEnabled(true);
            viw4_ucGroupsSelected.Clear();
            BtnStatus("0001");
            ViewState["CommandName"] = "DELEMP";
            viw4_ucGroupsSelected.RwtID = ViewState["ID"].ToString();
            viw4_ucGroupsSelected.FillList();
            MultiView1.ActiveViewIndex = 3;
            WizardData.ActiveStepIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        UIWizardClear();
        MultiView1.ActiveViewIndex = 0;
        BtnStatus("1000");
        UIEnabled(false);      
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Cancel]
    {
        Hashtable Permht  = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnAddEmp.Enabled = GenCs.FindStatus(Status[1]);
        btnDelEmp.Enabled = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
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
                        e.Row.Cells[1].Visible = false; //To hide ID column in grid view
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
        switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton _btnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        _btnDelete.Enabled = pgCs.getPermission(ViewState["ht"], "Delete");
                        _btnDelete.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        
        GridView gridView = (GridView)sender;
        int cellIndex = -1;
        foreach (DataControlField field in gridView.Columns)
        {
            if (ViewState["SortExpression"] != null)
            {
                if (field.SortExpression == Convert.ToString(ViewState["SortExpression"]))
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                    break;
                }
            }
        }

        if (cellIndex > -1)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //this is a header row,set the sort style
                e.Row.Cells[cellIndex].CssClass += (sortDirection == "ASC" ? " sortasc" : " sortdesc");
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string ID = e.CommandArgument.ToString();
                    string EwrID = "0";

                    DataTable DT = DBCs.FetchData(" SELECT EwrID FROM EmpWrkRel WHERE RwtID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        for (int i = 0; i < DT.Rows.Count; i++) { if (string.IsNullOrEmpty(EwrID)) { EwrID += DT.Rows[i][0].ToString(); } else { EwrID += "," + DT.Rows[i][0].ToString(); } }
  
                        DataTable TDT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Trans WHERE EwrID IN ( " + EwrID + " ) "));
                        if (!DBCs.IsNullOrEmpty(TDT))
                        {
                            CtrlCs.ShowDelMsg(this, false);
                            return;
                        }
                    }

                    SqlCs.SANS_RotationWorkTime_Delete(ID, pgCs.LoginID);
                    
                    btnFilter_Click(null,null);
                    
                    CtrlCs.ShowDelMsg(this, true);
                    break; 
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
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
                BtnStatus("1111");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("RwtID =" + pID + "");

            string RwtID = DRs[0]["RwtID"].ToString();
            ViewState["ID"] = DRs[0]["RwtID"].ToString();

            viw1_txtRwtNameAr.Text = Convert.ToString(DRs[0]["RwtNameEn"]);
            viw1_txtRwtNameEn.Text = Convert.ToString(DRs[0]["RwtNameAr"]);
            viw1_txtRwtWorkDaysCount.Text     = DRs[0]["RwtWorkDaysCount"].ToString();
            viw1_txtRwtNotWorkDaysCount.Text  = DRs[0]["RwtNotWorkDaysCount"].ToString();
            viw1_txtRwtRotationDaysCount.Text = DRs[0]["RwtRotationDaysCount"].ToString();

            viw1_calStartDate.SetGDate(DRs[0]["RwtFromDate"], pgCs.DateFormat);
            viw1_calEndDate.SetGDate(DRs[0]["RwtToDate"], pgCs.DateFormat);

            DataTable WktDT = DBCs.FetchData(" SELECT R.WktID, W.WktNameAr, W.WktNameEn FROM RotationWorkTime_WrkRel R,WorkingTime W  WHERE R.WktID = W.WktID AND R.RwtID = @P1 ORDER BY OrderID ", new string[] { RwtID });
            if (!DBCs.IsNullOrEmpty(WktDT))
            {
                for (int i = 0; i < WktDT.Rows.Count; i++)
                {
                    ListItem _listItem = new ListItem();
                    _listItem.Text = General.Msg(WktDT.Rows[i]["WktNameEn"].ToString(),WktDT.Rows[i]["WktNameAr"].ToString());
                    _listItem.Value = WktDT.Rows[i]["WktID"].ToString();
                    viw1_lstWorkTime.Items.Add(_listItem);
                }
            }

            DataTable GrpDT = DBCs.FetchData(" SELECT RwtID, GrpName, GrpUserName FROM RotationWorkTime_GrpRel WHERE RwtID = @P1 ORDER BY OrderID ", new string[] { RwtID });
            if (!DBCs.IsNullOrEmpty(GrpDT))
            {
                for (int i = 0; i < GrpDT.Rows.Count; i++)
                {
                    ListItem _li = new ListItem();
                    _li.Text  = GrpDT.Rows[i]["GrpName"].ToString() + " - " + Convert.ToString(GrpDT.Rows[i]["GrpUserName"]);
                    _li.Value = GrpDT.Rows[i]["GrpName"].ToString();
                    viw1_lstRotationGroup.Items.Add(_li);
                }
                viw1_lstRotationGroup.Enabled = true;
                viw1_lstRotationGroup.SelectedIndex = 0;
                FillEmpList(RwtID, viw1_lstRotationGroup.Items[0].Value);
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillEmpList(string RwtID, string GroupID)
    {
        DataTable EmpDT = DBCs.FetchData(" SELECT E.EmpID,E.EmpNameAr,E.EmpNameEn FROM Employee E, UF_CSVToTable1Col_WithOrder((SELECT EmpIDs FROM RotationWorkTime_GrpRel WHERE RwtID = @P1 AND GrpName = @P2),',') R WHERE R.SCol = E.EmpID ", new string[] { RwtID, GroupID });
        if (!DBCs.IsNullOrEmpty(EmpDT))
        {
            for (int i = 0; i < EmpDT.Rows.Count; i++)
            {
                ListItem _listItem = new ListItem();
                _listItem.Text = General.Msg(EmpDT.Rows[i]["EmpNameEn"].ToString(),EmpDT.Rows[i]["EmpNameAr"].ToString());
                _listItem.Value = EmpDT.Rows[i]["EmpID"].ToString();
                viw1_lstEmployeeGroup.Items.Add(_listItem);
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

        //if (Convert.ToString(ViewState["CommandName"]) != "ADD")
        //{
        //    WizardData.WizardSteps.Remove(this.WizardStep1);
        //    WizardData.WizardSteps.Remove(this.WizardStep2);
        //}    

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
        int step = WizardData.ActiveStepIndex;
        if (step > 0 && step < WizardData.WizardSteps.Count - 1)
        {
            WebControl stepNavTemplate = this.WizardData.FindControl("StepNavigationTemplateContainerID") as WebControl;
            if (stepNavTemplate != null)
            {
                ImageButton save = stepNavTemplate.FindControl("btnNextStep") as ImageButton;
                if (save != null)
                {
                    save.ValidationGroup = "VGStep" + step.ToString();
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnStartStep_Click(object sender, EventArgs e) 
    {
        if (!CtrlCs.PageIsValid(this, viw2_vsStart)) { return; }
              
        WizardData.ActiveStepIndex += 1;
        //if (Convert.ToString(ViewState["CommandName"]) == "ADD") {  }
        //else
        //{
        //    ucEmployeeSelectedGroup.FillList();
        //    ucEmployeeSelectedGroup.FillRotation(ViewState["ID"].ToString());
        //}            
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex -= 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnNextStep_Click(object sender, EventArgs e) 
    {
        switch (WizardData.ActiveStepIndex)
        {
            case (1): NextStepWorktime(); break;
            case (2): NextStepGroups(); break;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void NextStepWorktime()
    {
        if (!CtrlCs.PageIsValid(this, viw2_vsStep1)) { return; }
        
        WizardData.ActiveStepIndex += 1;  
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void NextStepGroups()
    {
        if (!CtrlCs.PageIsValid(this, viw2_vsStep2)) { return; }
        
        if (viw2_lstRotationGrp.Items.Count > 0)
        {
            viw2_ucEmployeeSelectedGroup.ClearGruop();
            for (int i = 0; i < viw2_lstRotationGrp.Items.Count; i++)
            {
                ListItem _ls = new ListItem(viw2_lstRotationGrp.Items[i].Text, viw2_lstRotationGrp.Items[i].Value);
                viw2_ucEmployeeSelectedGroup.AddGruop(_ls);
            }
        }
        WizardData.ActiveStepIndex += 1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnFinishBackStep_Click(object sender, EventArgs e) 
    {
       WizardData.ActiveStepIndex -= 1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnSaveFinish_Click(object sender, EventArgs e)  { Save(); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region worktime Events

    protected void viw2_btnAddWkt_Click(object sender, EventArgs e)
    {
        if (viw2_ddlWktID.SelectedIndex > 0)
        {
            if (viw2_lstWorkTime.Items.IndexOf(viw2_lstWorkTime.Items.FindByValue(viw2_ddlWktID.SelectedValue.ToString())) == -1)
            {
                ListItem ls = new ListItem();
                ls.Text = viw2_ddlWktID.SelectedItem.Text;
                ls.Value = viw2_ddlWktID.SelectedValue.ToString();
                viw2_lstWorkTime.Items.Add(ls);
                viw2_lstWorkTime.SelectedIndex = viw2_lstWorkTime.Items.IndexOf(viw2_lstWorkTime.Items.FindByValue(viw2_ddlWktID.SelectedValue.ToString()));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnUpWkt_Click(object sender, EventArgs e)
    {
        if (viw2_lstWorkTime.SelectedIndex > 0)
        {
            int oldIndex = viw2_lstWorkTime.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = viw2_lstWorkTime.SelectedItem.Text;
            ls.Value = viw2_lstWorkTime.SelectedValue.ToString();
            viw2_lstWorkTime.Items.RemoveAt(viw2_lstWorkTime.SelectedIndex);
            viw2_lstWorkTime.Items.Insert(oldIndex - 1, ls);
            viw2_lstWorkTime.SelectedIndex = oldIndex - 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnDownWkt_Click(object sender, EventArgs e)
    {
        if (viw2_lstWorkTime.SelectedIndex > -1 && viw2_lstWorkTime.SelectedIndex < viw2_lstWorkTime.Items.Count - 1)
        {
            int oldIndex = viw2_lstWorkTime.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = viw2_lstWorkTime.SelectedItem.Text;
            ls.Value = viw2_lstWorkTime.SelectedValue.ToString();
            viw2_lstWorkTime.Items.RemoveAt(viw2_lstWorkTime.SelectedIndex);
            viw2_lstWorkTime.Items.Insert(oldIndex + 1, ls);
            viw2_lstWorkTime.SelectedIndex = oldIndex + 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnRemoveWkt_Click(object sender, EventArgs e)
    {
        if (viw2_lstWorkTime.SelectedIndex > -1) { viw2_lstWorkTime.Items.RemoveAt(viw2_lstWorkTime.SelectedIndex); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region groups Events

    Random randNum = new Random();

    protected void viw2_btnAddGrp_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(viw2_txtGrpName.Text) && viw2_ddlUser.SelectedIndex > 0)
        {
            int iName = viw2_lstRotationGrp.Items.IndexOf(viw2_lstRotationGrp.Items.FindByText(viw2_txtGrpName.Text));
            int iUser = viw2_lstRotationUsr.Items.IndexOf(viw2_lstRotationUsr.Items.FindByText(viw2_ddlUser.SelectedValue));

            if (iName == -1 && iUser == -1)
            {
                string R = randNum.Next(99).ToString();
                ListItem _NameLS = new ListItem(viw2_txtGrpName.Text, R);
                ListItem _UserLS = new ListItem(viw2_ddlUser.SelectedValue, R);

                viw2_lstRotationGrp.Items.Add(_NameLS);
                viw2_lstRotationUsr.Items.Add(_UserLS);

                viw2_lstRotationGrp.SelectedIndex = viw2_lstRotationGrp.Items.IndexOf(viw2_lstRotationGrp.Items.FindByText(viw2_txtGrpName.Text));
                viw2_lstRotationUsr.SelectedIndex = viw2_lstRotationUsr.Items.IndexOf(viw2_lstRotationUsr.Items.FindByText(viw2_ddlUser.Text));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnUpGrp_Click(object sender, EventArgs e)
    {
        if (viw2_lstRotationGrp.SelectedIndex > 0)
        {
            int OldIndex = viw2_lstRotationGrp.SelectedIndex;
            int NewIndex = OldIndex - 1;

            ListItem _NameLS = new ListItem(viw2_lstRotationGrp.SelectedItem.Text, viw2_lstRotationGrp.SelectedValue.ToString());
            ListItem _UserLS = new ListItem(viw2_lstRotationUsr.SelectedItem.Text, viw2_lstRotationUsr.SelectedValue.ToString());

            viw2_lstRotationGrp.Items.RemoveAt(OldIndex);
            viw2_lstRotationUsr.Items.RemoveAt(OldIndex);

            viw2_lstRotationGrp.Items.Insert(NewIndex, _NameLS);
            viw2_lstRotationUsr.Items.Insert(NewIndex, _UserLS);

            viw2_lstRotationGrp.SelectedIndex = viw2_lstRotationUsr.SelectedIndex = NewIndex;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnDownGrp_Click(object sender, EventArgs e)
    {
        if (viw2_lstRotationGrp.SelectedIndex > -1 && viw2_lstRotationGrp.SelectedIndex < viw2_lstRotationGrp.Items.Count - 1)
        {
            int OldIndex = viw2_lstRotationGrp.SelectedIndex;
            int NewIndex = OldIndex + 1;

            ListItem _NameLS = new ListItem(viw2_lstRotationGrp.SelectedItem.Text, viw2_lstRotationGrp.SelectedValue.ToString());
            ListItem _UserLS = new ListItem(viw2_lstRotationUsr.SelectedItem.Text, viw2_lstRotationUsr.SelectedValue.ToString());

            viw2_lstRotationGrp.Items.RemoveAt(OldIndex);
            viw2_lstRotationUsr.Items.RemoveAt(OldIndex);

            viw2_lstRotationGrp.Items.Insert(NewIndex, _NameLS);
            viw2_lstRotationUsr.Items.Insert(NewIndex, _UserLS);

            viw2_lstRotationGrp.SelectedIndex = viw2_lstRotationUsr.SelectedIndex = NewIndex;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw2_btnRemoveGrp_Click(object sender, EventArgs e)
    {
        if (viw2_lstRotationGrp.SelectedIndex > -1)
        {
            int OldIndex = viw2_lstRotationGrp.SelectedIndex;
            viw2_lstRotationGrp.Items.RemoveAt(OldIndex);
            viw2_lstRotationUsr.Items.RemoveAt(OldIndex);
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region selectEmployee Events
    
    protected void Save()
    {
        try
        {
            if (!Page.IsValid) { return; }

            FillPropeties();
            SqlCs.SANS_RotationWorkTime_Insert(ProCs);

            btnFilter_Click(null,null);
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw3_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, viw3_vsSave)) { return; }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
            string GrpIDs = "";
            string EmpIDs = "";
            ListItemCollection _lic = viw3_ucEmployeeSelectedGroup.getGroupList();
            foreach (ListItem _li in _lic)
            {
                DataTable DT = viw3_ucEmployeeSelectedGroup.getEmpSelected(_li.Value);
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string perifx = (string.IsNullOrEmpty(GrpIDs)) ? "" : ",";
                    GrpIDs += perifx + _li.Text;

                    string Emps = GenCs.CreateIDsNumber("EmpID",DT);
                    if (string.IsNullOrEmpty(EmpIDs)) { EmpIDs += Emps; } else { EmpIDs += "-" + Emps; }
                }  
            }
       
            ProCs.GrpIDs = GrpIDs;
            ProCs.EmpIDs = EmpIDs;
            ProCs.RwtAddStartDate = viw3_calAddStartDate.getGDateDBFormat();
            ProCs.RwtID = ViewState["ID"].ToString();
            ProCs.TransactionBy = pgCs.LoginID;

            SqlCs.SANS_RotationWorkTime_AddEmployees(ProCs);

            btnFilter_Click(null,null);
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void viw4_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, viw4_vsSave)) { return; }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
            ProCs.EmpIDs = GenCs.CreateIDsNumber("EmpID",viw4_ucGroupsSelected.EmpSelected);
            ProCs.RwtAddStartDate = viw4_calAddStartDate.getGDateDBFormat();
            ProCs.RwtID = ViewState["ID"].ToString();
            ProCs.TransactionBy = pgCs.LoginID;

            SqlCs.SANS_RotationWorkTime_DeleteEmployees(ProCs);

            btnFilter_Click(null,null);
            CtrlCs.ShowSaveMsg(this);
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

    protected void NameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(viw2_cvRwtNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvRwtNameEn, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(viw2_txtRwtNameEn.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(viw2_txtRwtNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvRwtNameEn, true, General.Msg("Entered English Name exist already,Please enter another name", "الاسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM RotationWorkTime WHERE RwtNameEn = @P1 ", new string[] { viw2_txtRwtNameEn.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(viw2_cvRwtNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvRwtNameAr, false, General.Msg("Name (Ar) Is Required", "الاسم العربي مطلوب"));
                    if (string.IsNullOrEmpty(viw2_txtRwtNameAr.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(viw2_txtRwtNameAr.Text))
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvRwtNameAr, true, General.Msg("Entered Arabic Name exist already,Please enter another name", "الاسم العربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM RotationWorkTime WHERE RwtNameAr = @P1 ", new string[] { viw2_txtRwtNameAr.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstWorkTime_viw1_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (viw2_lstWorkTime.Items.Count < 1)
            {
                CtrlCs.ValidMsg(this, ref viw2_cvlstWorkTime, false, General.Msg("Work Time is required", "يجب إدخال أوقات العمل"));
                e.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(viw2_txtRwtWorkDaysCount.Text) && !string.IsNullOrEmpty(viw2_txtRwtRotationDaysCount.Text))
            {
                int RotCount = Convert.ToInt32(viw2_txtRwtWorkDaysCount.Text) / Convert.ToInt32(viw2_txtRwtRotationDaysCount.Text);
                if (RotCount != viw2_lstWorkTime.Items.Count)
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvlstWorkTime, true, General.Msg("You must enter " + RotCount.ToString() + " working times", "يجب إدخال " + RotCount.ToString() + " أوقات العمل"));
                    e.IsValid = false;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstRotationGroup_viw2_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (viw2_lstRotationGrp.Items.Count < 1)
            {
                CtrlCs.ValidMsg(this, ref viw2_cvlstRotationGroup, false, General.Msg("Group Name is required", "يجب إدخال أسماء المجموعات"));
                e.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(viw2_txtRwtWorkDaysCount.Text) && !string.IsNullOrEmpty(viw2_txtRwtRotationDaysCount.Text))
            {
                int RotCount = Convert.ToInt32(viw2_txtRwtWorkDaysCount.Text) / Convert.ToInt32(viw2_txtRwtRotationDaysCount.Text);
                if (RotCount != viw2_lstRotationGrp.Items.Count && RotCount * 2 != viw2_lstRotationGrp.Items.Count)
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvlstRotationGroup, true, General.Msg("You must enter " + RotCount.ToString() + " or " + (RotCount * 2).ToString() + " Groups", "يجب إدخال " + RotCount.ToString() + "  أو " + (RotCount * 2).ToString() + " مجموعات"));
                    e.IsValid = false;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void RotationDaysValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(viw2_txtRwtRotationDaysCount.Text))
            {
                CtrlCs.ValidMsg(this, ref viw2_cvRwtRotationDaysCount, false, General.Msg("No. of Rotation Days is required", "عدد أيام التدوير مطلوب"));
                e.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(viw2_txtRwtWorkDaysCount.Text) && !string.IsNullOrEmpty(viw2_txtRwtRotationDaysCount.Text))
            {
                int RotCount = Convert.ToInt32(viw2_txtRwtWorkDaysCount.Text) % Convert.ToInt32(viw2_txtRwtRotationDaysCount.Text);
                if (RotCount != 0)
                {
                    CtrlCs.ValidMsg(this, ref viw2_cvRwtRotationDaysCount, true, General.Msg("The number of working days must be divisible by the number of days of rotation", "يجب أن يكون عدد أيام العمل قابل للقسمة مع عدد أيام التدوير"));
                    e.IsValid = false;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void AddDateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(viw3_calAddStartDate.getGDate()))
            {
                CtrlCs.ValidMsg(this, ref viw3_cvAddStartDate, false, General.Msg("Date is required", "التاريخ مطلوب"));
                e.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(viw3_calAddStartDate.getGDate()) && !string.IsNullOrEmpty(viw1_calStartDate.getGDate()) && !string.IsNullOrEmpty(viw1_calEndDate.getGDate()))
            {
                int iStartDate = DTCs.ConvertDateTimeToInt(viw1_calStartDate.getGDate(), "Gregorian");
                int iEndDate = DTCs.ConvertDateTimeToInt(viw1_calEndDate.getGDate(), "Gregorian");
                int iAddDate = DTCs.ConvertDateTimeToInt(viw3_calAddStartDate.getGDate(), "Gregorian");

                if (iAddDate < iStartDate || iAddDate > iEndDate)
                {
                    CtrlCs.ValidMsg(this, ref viw3_cvAddStartDate, true, General.Msg("The start date of the extension must be within the rotation date", "تاريخ بداية الإضافة يجب أن يكون ضمن تاريخ التدوير"));
                    e.IsValid = false;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void AddDateValidate_viw2_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(viw4_calAddStartDate.getGDate()))
            {
                CtrlCs.ValidMsg(this, ref viw4_cvAddStartDate, false, General.Msg("Date is required", "التاريخ مطلوب"));
                e.IsValid = false;
            }
            else if (!string.IsNullOrEmpty(viw4_calAddStartDate.getGDate()) && !string.IsNullOrEmpty(viw1_calStartDate.getGDate()) && !string.IsNullOrEmpty(viw1_calEndDate.getGDate()))
            {
                int iStartDate = DTCs.ConvertDateTimeToInt(viw1_calStartDate.getGDate(), "Gregorian");
                int iEndDate   = DTCs.ConvertDateTimeToInt(viw1_calEndDate.getGDate(), "Gregorian");
                int iAddDate   = DTCs.ConvertDateTimeToInt(viw4_calAddStartDate.getGDate(), "Gregorian");

                if (iAddDate < iStartDate || iAddDate > iEndDate)
                {
                    CtrlCs.ValidMsg(this, ref viw4_cvAddStartDate, true, General.Msg("The start date of the extension must be within the rotation date", "تاريخ بداية الإضافة يجب أن يكون ضمن تاريخ التدوير"));
                    e.IsValid = false;
                }
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