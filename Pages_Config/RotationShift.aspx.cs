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

public partial class RotationShift : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    WorkTimePro ProCs = new WorkTimePro();
    WorkTimeSql SqlCs = new WorkTimeSql();
    
    EmpWrkPro EWProCs = new EmpWrkPro();
    EmpWrkSql EWSqlCs = new EmpWrkSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT WktID,WktNameAr,WktNameEn,WktShift1From,WktShift1To,WktShift1Duration,WktShift1IsOptional "
                     + " FROM WorkingTime "
                     + " WHERE WtpID IN ( SELECT WtpID FROM WorkType WHERE WtpInitial ='RO' ) AND ISNULL(WktDeleted,0) = 0 ";
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

            for (int i = 1; i <= 2; i++)
            {
                AjaxControlToolkit.AnimationExtender aniExtShow  = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderShow" + i.ToString());
                AjaxControlToolkit.AnimationExtender aniExtClose = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderClose" + i.ToString());
                ImageButton lnkShow = (ImageButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("lnkShow" + i.ToString());
                if (aniExtShow != null) { CtrlCs.Animation(ref aniExtShow, ref aniExtClose, ref lnkShow, i.ToString()); }
            }

            ImageButton StartStep = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartStep") as ImageButton;
            ImageButton BackStep = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnBackStep") as ImageButton;
            ImageButton NextStep = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnNextStep") as ImageButton;
            ImageButton FinishBackStep = this.WizardData.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishBackStep") as ImageButton;
            
            StartStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            BackStep.ImageUrl = "images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");
            NextStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            FinishBackStep.ImageUrl = "images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("10");
                //UIEnabled(false);
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
            CtrlCs.FillWorkingTimeList(ref ddlWktID, null, false, false);
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
            if (ddlFilter.Text == "WktID") 
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
        BtnStatus("10");
        UIEnabled(false);
        ClearWizardUI();
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
            spnWktNameEn_viw0.Visible = true;
        }

        if (pgCs.LangAr)
        {
            spnWktNameAr_viw0.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool status)
    {
        txtWktNameAr.Enabled = status;
        txtWktNameEn.Enabled = status;
        txtWktNameAr.Enabled = status;
        txtDuration.Enabled = status;
        calStartDate.SetEnabled(status);
        calEndDate.SetEnabled(status);
        lstWorkTime.Enabled = status;
        lstRotationGroup.Enabled = status;
        lstEmployeeGroup.Enabled = status;

        calStartDate_viw0.SetEnabled(status);
        calEndDate_viw0.SetEnabled(status);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties(DateTime[] SDatesDT, DateTime[] EDatesDT)
    {
         try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.WktNameAr = txtWktNameAr_viw0.Text;
            ProCs.WktNameEn = txtWktNameEn_viw0.Text;
            ProCs.WktShift1Duration = txtDuration_viw0.Text;
            ProCs.WktShift1From = calStartDate_viw0.getGDateDBFormat();
            ProCs.WktShift1To   = calEndDate_viw0.getGDateDBFormat();
            ProCs.WktShift1IsOptional = chkRotOnlyWorkDays_viw0.Checked;

            EWProCs.EwrSat = chkEwrSat_viw0.Checked;
            EWProCs.EwrSun = chkEwrSun_viw0.Checked;
            EWProCs.EwrMon = chkEwrMon_viw0.Checked;
            EWProCs.EwrTue = chkEwrTue_viw0.Checked;
            EWProCs.EwrWed = chkEwrWed_viw0.Checked;
            EWProCs.EwrThu = chkEwrThu_viw0.Checked;
            EWProCs.EwrFri = chkEwrFri_viw0.Checked;

            EWProCs.DateLen = SDatesDT.Length.ToString();
            EWProCs.SDates  = GenCs.CreateIDsNumber(SDatesDT);
            EWProCs.EDates  = GenCs.CreateIDsNumber(EDatesDT);
            EWProCs.GrpLen  = lstRotationGroup_viw2.Items.Count.ToString();
            EWProCs.WktIDs  = GenCs.CreateIDsNumber("V", lstWorkTime_viw1);
            EWProCs.GrpIDs  = GenCs.CreateIDsNumber("T", lstRotationGroup_viw2);

            string EmpIDs = "";
            for (int lG = 0; lG < lstRotationGroup_viw2.Items.Count; lG++)
            {
                DataTable EmpDT = ucEmployeeSelectedGroup.getEmpSelected(lstRotationGroup_viw2.Items[lG].Value);
                string Emps = GenCs.CreateIDsNumber("EmpID",EmpDT);
                if (string.IsNullOrEmpty(EmpIDs)) { EmpIDs += Emps; } else { EmpIDs += "-" + Emps; }   
            }
            EWProCs.EmpIDs = EmpIDs;

            EWProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = ""; 
        
        txtWktNameAr.Text = "";
        txtWktNameEn.Text = "";
        txtWktNameAr.Text = "";
        txtDuration.Text = "";
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        lstWorkTime.Items.Clear();
        lstRotationGroup.Items.Clear();
        lstEmployeeGroup.Items.Clear();
        chkEwrSat.Checked = false;
        chkEwrSun.Checked = false;
        chkEwrMon.Checked = false;
        chkEwrTue.Checked = false;
        chkEwrWed.Checked = false;
        chkEwrThu.Checked = false;
        chkEwrFri.Checked = false;
        chkRotOnlyWorkDays.Checked = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearWizardUI()
    {
        txtWktNameEn_viw0.Text = "";
        txtWktNameAr_viw0.Text = "";
        txtDuration_viw0.Text = "";
        calStartDate_viw0.ClearDate();
        calEndDate_viw0.ClearDate();
        chkEwrSat_viw0.Checked = false;
        chkEwrSun_viw0.Checked = false;
        chkEwrMon_viw0.Checked = false;
        chkEwrTue_viw0.Checked = false;
        chkEwrWed_viw0.Checked = false;
        chkEwrThu_viw0.Checked = false;
        chkEwrFri_viw0.Checked = false;
        chkRotOnlyWorkDays_viw0.Checked = false;

        ddlWktID.SelectedIndex = -1;
        lstWorkTime_viw1.Items.Clear();
        txtGrpName_viw2.Text = "";
        lstRotationGroup_viw2.Items.Clear();

        ucEmployeeSelectedGroup.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstRotationGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstEmployeeGroup.Items.Clear();
        FillEmpList(ViewState["ID"].ToString(), lstRotationGroup.SelectedItem.Text);
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
            ClearWizardUI();
            UIEnabled(true);
            BtnStatus("01");
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        ClearWizardUI();
        MultiView1.ActiveViewIndex = 0;
        BtnStatus("10");
        UIEnabled(false);      
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Cancel]
    {
        Hashtable Permht  = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnCancel.Enabled = GenCs.FindStatus(Status[1]);
        
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

                    DataTable DT = DBCs.FetchData(" SELECT EwrID FROM EmpWrkRel WHERE RotID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        for (int i = 0; i < DT.Rows.Count; i++) { if (string.IsNullOrEmpty(EwrID)) { EwrID += DT.Rows[i][0].ToString(); } else { EwrID += "," + DT.Rows[i][0].ToString(); } }
                        
                       
                        DataTable TDT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Trans WHERE EwrID IN ( " + EwrID + " ) "));
                        //DataTable TDT = DBCs.FetchData(" SELECT * FROM Trans WHERE EwrID IN ( @P1 ) ", new string[] { EwrID });
                        if (!DBCs.IsNullOrEmpty(TDT))
                        {
                            CtrlCs.ShowDelMsg(this, false);
                            return;
                        }
                    }

                    SqlCs.RotationWkt_Delete(ID, pgCs.LoginID);
                    
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
            BtnStatus("10");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
                BtnStatus("11");
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
            DataRow[] DRs = DT.Select("WktID =" + pID + "");

            string RotID = DRs[0]["WktID"].ToString();
            ViewState["ID"] = DRs[0]["WktID"].ToString();

            txtWktNameAr.Text = DRs[0]["WktNameEn"].ToString();
            txtWktNameEn.Text = DRs[0]["WktNameAr"].ToString();
            txtDuration.Text  = DRs[0]["WktShift1Duration"].ToString();
            if (DRs[0]["WktShift1IsOptional"] != DBNull.Value) { chkRotOnlyWorkDays.Checked = Convert.ToBoolean(DRs[0]["WktShift1IsOptional"]); }

            calStartDate.SetGDate(DRs[0]["WktShift1From"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["WktShift1To"], pgCs.DateFormat);

            DataTable Daysdt = DBCs.FetchData(" SELECT EwrSat, EwrSun, EwrMon, EwrTue,EwrWed,EwrThu,EwrFri FROM EmpWrkRel WHERE RotID = @P1 ", new string[] { RotID });
            if (!DBCs.IsNullOrEmpty(Daysdt))
            {
                if (Daysdt.Rows[0]["EwrSat"] != DBNull.Value) { chkEwrSat.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrSat"]); } 
                if (Daysdt.Rows[0]["EwrSun"] != DBNull.Value) { chkEwrSun.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrSun"]); }
                if (Daysdt.Rows[0]["EwrMon"] != DBNull.Value) { chkEwrMon.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrMon"]); } 
                if (Daysdt.Rows[0]["EwrTue"] != DBNull.Value) { chkEwrTue.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrTue"]); }
                if (Daysdt.Rows[0]["EwrWed"] != DBNull.Value) { chkEwrWed.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrWed"]); }
                if (Daysdt.Rows[0]["EwrThu"] != DBNull.Value) { chkEwrThu.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrThu"]); }
                if (Daysdt.Rows[0]["EwrFri"] != DBNull.Value) { chkEwrFri.Checked = Convert.ToBoolean(Daysdt.Rows[0]["EwrFri"]); }
            }

            DataTable WktDT = DBCs.FetchData(" SELECT R.RotID, R.WktID, W.WktNameAr, W.WktNameEn FROM RotationWrkRel R,WorkingTime W  WHERE R.WktID = W.WktID AND R.RotID = @P1 ORDER BY OrderID ", new string[] { RotID });
            if (!DBCs.IsNullOrEmpty(WktDT))
            {
                for (int i = 0; i < WktDT.Rows.Count; i++)
                {
                    ListItem _listItem = new ListItem();
                    _listItem.Text = General.Msg(WktDT.Rows[i]["WktNameEn"].ToString(),WktDT.Rows[i]["WktNameAr"].ToString());
                    _listItem.Value = WktDT.Rows[i]["WktID"].ToString();
                    lstWorkTime.Items.Add(_listItem);
                }
            }

            DataTable GrpDT = DBCs.FetchData(" SELECT RotID, GrpName FROM RotationGrpRel WHERE RotID = @P1 ORDER BY OrderID ", new string[] { RotID });
            if (!DBCs.IsNullOrEmpty(GrpDT))
            {
                for (int i = 0; i < GrpDT.Rows.Count; i++)
                {
                    ListItem _listItem = new ListItem();
                    _listItem.Text = GrpDT.Rows[i]["GrpName"].ToString();
                    _listItem.Value = GrpDT.Rows[i]["GrpName"].ToString();
                    lstRotationGroup.Items.Add(_listItem);
                }
                lstRotationGroup.Enabled = true;
                lstRotationGroup.SelectedIndex = 0;
                FillEmpList(RotID, lstRotationGroup.Items[0].Text);
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
    protected void FillEmpList(string pRotID, string pGrp)
    {
        DataTable EmpDT = DBCs.FetchData(" SELECT DISTINCT W.EmpID,E.EmpNameAr,E.EmpNameEn FROM EmpWrkRel W,Employee E WHERE W.EmpID = E.EmpID AND RotID = @P1 AND GrpName = @P2 ", new string[] { pRotID,pGrp });
        if (!DBCs.IsNullOrEmpty(EmpDT))
        {
            for (int i = 0; i < EmpDT.Rows.Count; i++)
            {
                ListItem _listItem = new ListItem();
                _listItem.Text = General.Msg(EmpDT.Rows[i]["EmpNameEn"].ToString(),EmpDT.Rows[i]["EmpNameAr"].ToString());
                _listItem.Value = EmpDT.Rows[i]["EmpID"].ToString();
                lstEmployeeGroup.Items.Add(_listItem);
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
    protected void btnStartStep_Click(object sender, EventArgs e) 
    {
        if (!CtrlCs.PageIsValid(this, VSStart)) { return; }
        
        WizardData.ActiveStepIndex += 1;     
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex -= 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnNextStep_Click(object sender, EventArgs e) 
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
        if (!CtrlCs.PageIsValid(this, VSStep1)) { return; }
        
        WizardData.ActiveStepIndex += 1;  
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void NextStepGroups()
    {
        if (!CtrlCs.PageIsValid(this, VSStep2)) { return; }
        
        if (lstRotationGroup_viw2.Items.Count > 0)
        {
            ucEmployeeSelectedGroup.ClearGruop();
            for (int i = 0; i < lstRotationGroup_viw2.Items.Count; i++)
            {
                ListItem ls = new ListItem();
                ls.Text = lstRotationGroup_viw2.Items[i].Text;
                ls.Value = lstRotationGroup_viw2.Items[i].Value;
                ucEmployeeSelectedGroup.AddGruop(ls);
            }
        }
        WizardData.ActiveStepIndex += 1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFinishBackStep_Click(object sender, EventArgs e) 
    {
        WizardData.ActiveStepIndex -= 1; 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSaveFinish_Click(object sender, EventArgs e)  { Save(); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region worktime Events

    protected void btnAdd_viw1_Click(object sender, EventArgs e)
    {
        if (ddlWktID.SelectedIndex > 0)
        {
            if (lstWorkTime_viw1.Items.IndexOf(lstWorkTime_viw1.Items.FindByValue(ddlWktID.SelectedValue.ToString())) == -1)
            {
                ListItem ls = new ListItem();
                ls.Text = ddlWktID.SelectedItem.Text;
                ls.Value = ddlWktID.SelectedValue.ToString();
                lstWorkTime_viw1.Items.Add(ls);
                lstWorkTime_viw1.SelectedIndex = lstWorkTime_viw1.Items.IndexOf(lstWorkTime_viw1.Items.FindByValue(ddlWktID.SelectedValue.ToString()));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUP_viw1_Click(object sender, EventArgs e)
    {
        if (lstWorkTime_viw1.SelectedIndex > 0)
        {
            int oldIndex = lstWorkTime_viw1.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstWorkTime_viw1.SelectedItem.Text;
            ls.Value = lstWorkTime_viw1.SelectedValue.ToString();
            lstWorkTime_viw1.Items.RemoveAt(lstWorkTime_viw1.SelectedIndex);
            lstWorkTime_viw1.Items.Insert(oldIndex - 1, ls);
            lstWorkTime_viw1.SelectedIndex = oldIndex - 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDOWN_viw1_Click(object sender, EventArgs e)
    {
        if (lstWorkTime_viw1.SelectedIndex > -1 && lstWorkTime_viw1.SelectedIndex < lstWorkTime_viw1.Items.Count - 1)
        {
            int oldIndex = lstWorkTime_viw1.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstWorkTime_viw1.SelectedItem.Text;
            ls.Value = lstWorkTime_viw1.SelectedValue.ToString();
            lstWorkTime_viw1.Items.RemoveAt(lstWorkTime_viw1.SelectedIndex);
            lstWorkTime_viw1.Items.Insert(oldIndex + 1, ls);
            lstWorkTime_viw1.SelectedIndex = oldIndex + 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnRemove_viw1_Click(object sender, EventArgs e)
    {
        if (lstWorkTime_viw1.SelectedIndex > -1) { lstWorkTime_viw1.Items.RemoveAt(lstWorkTime_viw1.SelectedIndex); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region groups Events

    protected void btnAdd_viw2_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtGrpName_viw2.Text))
        {
            if (lstRotationGroup_viw2.Items.IndexOf(lstRotationGroup_viw2.Items.FindByText(txtGrpName_viw2.Text)) == -1)
            {
                ListItem ls = new ListItem();
                ls.Text = txtGrpName_viw2.Text;
                Random randNum = new Random();
                ls.Value = randNum.Next(99).ToString();//txtGrpName_viw2.Text;
                lstRotationGroup_viw2.Items.Add(ls);
                lstRotationGroup_viw2.SelectedIndex = lstRotationGroup_viw2.Items.IndexOf(lstRotationGroup_viw2.Items.FindByText(txtGrpName_viw2.Text));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUP_viw2_Click(object sender, EventArgs e)
    {
        if (lstRotationGroup_viw2.SelectedIndex > 0)
        {
            int oldIndex = lstRotationGroup_viw2.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstRotationGroup_viw2.SelectedItem.Text;
            ls.Value = lstRotationGroup_viw2.SelectedValue.ToString();
            lstRotationGroup_viw2.Items.RemoveAt(lstRotationGroup_viw2.SelectedIndex);
            lstRotationGroup_viw2.Items.Insert(oldIndex - 1, ls);
            lstRotationGroup_viw2.SelectedIndex = oldIndex - 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDOWN_viw2_Click(object sender, EventArgs e)
    {
        if (lstRotationGroup_viw2.SelectedIndex > -1 && lstRotationGroup_viw2.SelectedIndex < lstRotationGroup_viw2.Items.Count - 1)
        {
            int oldIndex = lstRotationGroup_viw2.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstRotationGroup_viw2.SelectedItem.Text;
            ls.Value = lstRotationGroup_viw2.SelectedValue.ToString();
            lstRotationGroup_viw2.Items.RemoveAt(lstRotationGroup_viw2.SelectedIndex);
            lstRotationGroup_viw2.Items.Insert(oldIndex + 1, ls);
            lstRotationGroup_viw2.SelectedIndex = oldIndex + 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnRemove_viw2_Click(object sender, EventArgs e)
    {
        if (lstRotationGroup_viw2.SelectedIndex > -1) { lstRotationGroup_viw2.Items.RemoveAt(lstRotationGroup_viw2.SelectedIndex); }
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
            DateTime StartDate = DTCs.ConvertToDatetime(calStartDate_viw0.getGDate(), "Gregorian");
            DateTime EndDate   = DTCs.ConvertToDatetime(calEndDate_viw0.getGDate(), "Gregorian");
            int iDuration = Convert.ToInt32(txtDuration_viw0.Text);
            int iLoop = (EndDate - StartDate).Days + 1;
            bool[] wrkdays;

            if (chkRotOnlyWorkDays_viw0.Checked)
            {
                wrkdays = new bool[] { chkEwrSun_viw0.Checked, chkEwrMon_viw0.Checked, chkEwrTue_viw0.Checked, chkEwrWed_viw0.Checked, chkEwrThu_viw0.Checked, chkEwrFri_viw0.Checked, chkEwrSat_viw0.Checked };
            }
            else
            {
                wrkdays = new bool[] { true, true, true, true, true, true, true };
            }

            bool foundDay = false;
            int FindDay = 0;
            int loopCount = 0;
            DateTime Startday = new DateTime(), EndDay = new DateTime();
            DateTime[] SDatesDT = new DateTime[1];
            DateTime[] EDatesDT   = new DateTime[1];

            for (int d = 0; d < iLoop; d++)
            {
                int dayID = Convert.ToInt32(StartDate.AddDays(d).DayOfWeek);
                if (wrkdays[dayID])
                {
                    foundDay = true;
                    FindDay += 1;
                    if (FindDay == 1) { Startday = StartDate.AddDays(d); }
                    if (FindDay == iDuration)
                    {
                        loopCount += 1;
                        Array.Resize<DateTime>(ref SDatesDT, loopCount);
                        Array.Resize<DateTime>(ref EDatesDT, loopCount);
                        EndDay = StartDate.AddDays(d);
                        SDatesDT[loopCount - 1] = Startday;
                        EDatesDT[loopCount - 1] = EndDay;
                        FindDay = 0;
                    }
                }
            }

            if (!foundDay) 
            { 
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("There are no working days (which have been selected) within the period specified", "لا توجد أيام عمل (التي تم اختيارها ) داخل الفترة المحددة"));
                return; 
            }

            if (FindDay > 0)
            {
                loopCount += 1;
                Array.Resize<DateTime>(ref SDatesDT, loopCount);
                Array.Resize<DateTime>(ref EDatesDT, loopCount);
                SDatesDT[loopCount - 1] = Startday;
                EDatesDT[loopCount - 1] = EndDate;
            }

            FillPropeties(SDatesDT, EDatesDT);
            EWSqlCs.RotationWkt_Insert(ProCs,EWProCs);
            
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
            if (source.Equals(cvtxtWktNameEn_viw0))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvtxtWktNameEn_viw0, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtWktNameEn_viw0.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(txtWktNameEn_viw0.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvtxtWktNameEn_viw0, true, General.Msg("Entered English Name exist already,Please enter another name", "الاسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM WorkingTime WHERE WktNameEn = @P1 ", new string[] { txtWktNameEn_viw0.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvtxtWktNameAr_viw0))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvtxtWktNameAr_viw0, false, General.Msg("Name (Ar) Is Required", "الاسم العربي مطلوب"));
                    if (string.IsNullOrEmpty(txtWktNameAr_viw0.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(txtWktNameAr_viw0.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvtxtWktNameAr_viw0, true, General.Msg("Entered Arabic Name exist already,Please enter another name", "الاسم العربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM WorkingTime WHERE WktNameAr = @P1 ", new string[] { txtWktNameAr_viw0.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SelectWorkDays_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (!chkEwrSat_viw0.Checked && !chkEwrSun_viw0.Checked && !chkEwrMon_viw0.Checked && !chkEwrTue_viw0.Checked && !chkEwrWed_viw0.Checked && !chkEwrThu_viw0.Checked && !chkEwrFri_viw0.Checked)
            {
                e.IsValid = false;
            }
            else { e.IsValid = true; }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstWorkTime_viw1_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (lstWorkTime_viw1.Items.Count < 1) { e.IsValid = false; }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstRotationGroup_viw2_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (lstRotationGroup_viw2.Items.Count < 1) { e.IsValid = false; }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}