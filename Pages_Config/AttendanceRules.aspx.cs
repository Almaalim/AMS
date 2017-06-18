using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Elmah;

public partial class AttendanceRules : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    RulePro ProCs = new RulePro();
    RuleSql SqlCs = new RuleSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    //string GenralQuery = " SELECT * FROM MachineInfoView WHERE MacVirtualType IS NULL AND ISNULL(MacDeleted,0) = 0 ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.Animation(ref AnimationExtenderShow1, ref AnimationExtenderClose1, ref lnkShow1, "1");
            CtrlCs.Animation(ref AnimationExtenderShow2, ref AnimationExtenderClose2, ref lnkShow2, "2");
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("100");
                UIEnabled(false);
                CtrlCs.FillGridEmpty(ref grdData, 50);
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
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM RuleSet WHERE ISNULL(RlsDeleted,0) = 0"));
            if (!DBCs.IsNullOrEmpty(DT)) 
            { 
                CtrlCs.PopulateDDL(ddlRuleSet, DT, General.Msg("RlsNameEn","RlsNameEn"), "RlsID", General.Msg("-Select Rule Set-","-اختر مجموعة الضبط-")); 
                rvRuleSet.InitialValue = ddlRuleSet.Items[0].Text;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayMeasure(object pType)
    {
        try
        {
            if      (pType.ToString().Trim() == "Seconds") { return General.Msg("Seconds","ثواني"); }
            else if (pType.ToString().Trim() == "Minutes") { return General.Msg("Minutes", "دقائق"); }
            else if (pType.ToString().Trim() == "Hours")   { return General.Msg("Hours","ساعات"); }
            else if (pType.ToString().Trim() == "Shifts")  { return General.Msg("Shifts", "ورديات"); }
            else if (pType.ToString().Trim() == "Days")    { return General.Msg("Days","أيام"); }
            else { return ""; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex);  return string.Empty; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

     public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;

        ddlRuletype.Enabled = pStatus;
        ddlRuleMeasureIn.Enabled = pStatus;
        txtActionUnits.Enabled = pStatus;
        txtFrequency.Enabled = pStatus;
        ddlActionMeasureIn.Enabled = pStatus;
        txtFrequency.Enabled = pStatus;
        txtRuleUnits.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.RltID = txtID.Text; }
            
            ProCs.RlsID              = ddlRuleSet.SelectedValue;
            ProCs.RltType            = ddlRuletype.SelectedValue;
            ProCs.RltRuleMeasureIn   = ddlRuleMeasureIn.SelectedValue;
            ProCs.RltUnits           = txtRuleUnits.Text;
            ProCs.RltFrequency       = txtFrequency.Text;
            ProCs.RltActionMeasureIn = ddlActionMeasureIn.SelectedValue;
            ProCs.RltActionUnits     = txtActionUnits.Text;
             
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            ViewState["CommandName"] = ""; 
            txtID.Text = "";
            
            ddlRuletype.ClearSelection();
            ddlRuleMeasureIn.ClearSelection();
            txtRuleUnits.Text = "";
            txtActionUnits.Text = "";
            txtFrequency.Text = "";
            ddlActionMeasureIn.ClearSelection();
            txtFrequency.Text = "";
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRuletype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRuleMeasureIn.Items.Clear();
        ddlActionMeasureIn.Items.Clear();
        if (ddlRuletype.SelectedIndex == 1 || ddlRuletype.SelectedIndex == 2 || ddlRuletype.SelectedIndex == 3)
        {
            ddlRuleMeasureIn.Items.Add(new ListItem(General.Msg("Seconds","ثواني"),"Seconds"));
            ddlRuleMeasureIn.Items.Add(new ListItem(General.Msg("Minutes","دقائق"),"Minutes"));
            ddlRuleMeasureIn.Items.Add(new ListItem(General.Msg("Hours","ساعات"),"Hours"));

            ddlActionMeasureIn.Items.Add(new ListItem(General.Msg("Seconds","ثواني"),"Seconds"));
            ddlActionMeasureIn.Items.Add(new ListItem(General.Msg("Minutes","دقائق"),"Minutes"));
            ddlActionMeasureIn.Items.Add(new ListItem(General.Msg("Hours","ساعات"),"Hours"));
        }
        else
        if (ddlRuletype.SelectedIndex == 4)
        {
            ddlRuleMeasureIn.Items.Add(new ListItem(General.Msg("Shifts","ورديات"),"Shifts"));
            ddlRuleMeasureIn.Items.Add(new ListItem(General.Msg("Days","أيام"),"Days"));

            ddlActionMeasureIn.Items.Add(new ListItem(General.Msg("Shifts","ورديات"),"Shifts"));
            ddlActionMeasureIn.Items.Add(new ListItem(General.Msg("Days","أيام"),"Days"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRuleSet_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
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
            UIEnabled(true);
            BtnStatus("011");
            ViewState["CommandName"] = "ADD";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    UIEnabled(true);
        //    BtnStatus("0011");
        //    ViewState["CommandName"] = "EDIT";
        //}
        //catch (Exception ex)
        //{
        //    ErrorSignal.FromCurrentContext().Raise(ex);
        //    CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        //}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (commandName == "ADD") { SqlCs.RuleType_Insert(ProCs); }

            UIClear();
            UIEnabled(false);
            BtnStatus("100");
            FillGrid();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("100");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSaveRuleSet_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsMSave)) { return; }

            ProCs.RlsNameEn     = txtRuleSet.Text;
            ProCs.RlsNameAr     = txtRuleSet.Text;
            ProCs.RlsStatus     = true;
            ProCs.TransactionBy = pgCs.LoginID;

            SqlCs.RuleSet_Insert(ProCs);

            txtRuleSet.Text = "";

            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }

        ddlRuleSet.Items.Clear();
        FillList();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDeleteRuleSet_Click(object sender, EventArgs e)
    {
        try
        {
            System.Text.StringBuilder QDel = new System.Text.StringBuilder();
            QDel.Append(" SELECT RlsID FROM Employee WHERE RlsID = @P1 AND ISNULL(EmpDeleted,0) = 0");
            QDel.Append(" UNION ");
            QDel.Append(" SELECT RlsID FROM RuleType WHERE RlsID = @P1 AND ISNULL(RltDeleted,0) = 0");

            DataTable DT = DBCs.FetchData(QDel.ToString(), new string[] { ddlRuleSet.SelectedValue });

            if (!DBCs.IsNullOrEmpty(DT))
            {
                CtrlCs.ShowDelMsg(this, false);
                return;
            }

            SqlCs.RuleSet_Delete(ddlRuleSet.SelectedValue, pgCs.LoginID);
            ddlRuleSet.Items.Clear();
            FillList();

            CtrlCs.ShowDelMsg(this, true);
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
                        //e.Row.Cells[1].Visible = false; //To hide ID column in grid view
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
        UIClear();
        BtnStatus("100");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        FillGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    string ID = Convert.ToString(e.CommandArgument);

                    //System.Text.StringBuilder QDel = new System.Text.StringBuilder();
                    //QDel.Append(" SELECT MacID FROM TransDump WHERE MacID = " + ID + "");
                    //QDel.Append(" UNION ");
                    //QDel.Append(" SELECT MacID FROM Trans WHERE MacID = " + ID + "");
                    //QDel.Append(" UNION ");
                    //QDel.Append(" SELECT MacID FROM TransTrail WHERE MacID = " + ID + "");
                    //QDel.Append(" UNION ");
                    //QDel.Append(" SELECT MacID FROM RoundPatrolTransaction WHERE MacID = " + ID + "");

                    //dt = DBCs.FetchData(QDel.ToString());
                    //if (!DBCs.IsNullOrEmpty(dt))
                    //{
                    //    CtrlCs.ShowDelMsg(this, false);
                    //    return;
                    //}

                    //FillPropeties();
                    //ProCs.MacID = ID;
                    //ProCs.DeletedBy = SessionCs.LoginID;
                    //ProCs.DeletedDate = DateTime.Now.ToString("dd/MM/yyyy");
                    //ProCs.Deleted = true;
                    
                    SqlCs.RuleType_Delete(ID, pgCs.LoginID);
                    UIClear();
                    BtnStatus("100");
                    UIEnabled(false);
                    grdData.SelectedIndex = -1;
                    FillGrid();

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
        //System.Text.StringBuilder query = new System.Text.StringBuilder();
        //query.Append(GenralQuery);

        //dt = DBCs.FetchData(query.ToString());

        //if (!DBCs.IsNullOrEmpty(dt))
        //{
        //    DataView dataView = new DataView(dt);

        //    if (ViewState["SortDirection"] == null)
        //    {
        //        ViewState["SortDirection"] = "ASC";
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }
        //    else
        //    {
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }

        //    dataView.Sort = e.SortExpression + " " + sortDirection;

        //    grdData.DataSource = dataView;
        //    grdData.DataBind();
        //}
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
        UIClear();
        BtnStatus("100");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        FillGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //UIClear();
            //UIEnabled(false);
            //BtnStatus("100");

            //if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            //{
            //    CtrlCs.FillGridEmpty(ref grdData, 50);
            //}
            //else
            //{
            //    PopulateUI(grdData.SelectedRow.Cells[1].Text);
            //    BtnStatus("1100");
            //}
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            //dt = (DataTable)ViewState["grdDataDT"];
            //DataRow[] DRs = dt.Select("MacID =" + pID + "");

            //txtID.Text = DRs[0]["MacID"].ToString();
            //ddlMacType.SelectedIndex = ddlMacType.Items.IndexOf(ddlMacType.Items.FindByValue(DRs[0]["MtpID"].ToString()));

            //if (!string.IsNullOrEmpty(DRs[0]["MacInOutType"].ToString()))
            //{
            //    if (DRs[0]["MacInOutType"].ToString() == "True") { ddlMacTrnType.SelectedIndex = 1; } else { ddlMacTrnType.SelectedIndex = 2; }
            //}
            //else { ddlMacTrnType.SelectedIndex = 0; }
            
            //txtMacLocAr.Text    = DRs[0]["MacLocationAr"].ToString();
            //txtMacLocEn.Text    = DRs[0]["MacLocationEn"].ToString();
            //txtMachineNo.Text   = DRs[0]["MacNo"].ToString();
            //txtMachinePort.Text = DRs[0]["MacPort"].ToString();
            //txtMachineIP.Text   = DRs[0]["MacIP"].ToString();

            //if (DRs[0]["MacInstallDate"]  != DBNull.Value) { calMachineInsDate.txtSelectedDate.Text = General.ToAnyFormat(SessionCs.DateType, SessionCs.DateFormat, DRs[0]["MacInstallDate"]); }
            //if (DRs[0]["MacStatus"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["MacStatus"]); }
            //if (DRs[0]["MacIsRoundPatrolDevice"] != DBNull.Value) { chkIsRound.Checked = Convert.ToBoolean(DRs[0]["MacIsRoundPatrolDevice"]); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid()
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM RuleType WHERE RlsID =  @P1 AND ISNULL(RltDeleted,0) = 0 ", new string[] { ddlRuleSet.SelectedValue });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            grdData.DataSource = (DataTable)DT;
            ViewState["grdDataDT"] = (DataTable)DT;
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
    #region Custom Validate Events

    protected void RuleSet_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvRuleSet))
            {
                if (string.IsNullOrEmpty(txtRuleSet.Text)) { 
                    e.IsValid = false;
                    return;
                }
                if (!string.IsNullOrEmpty(txtRuleSet.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvRuleSet, true, General.Msg("Entered Ruleset already exists", "اسم مجموعة القواعد تم إدخاله مسبقا ، الرجاء إدخال رقم آخر")); 
                    DataTable DT = DBCs.FetchData(" SELECT * FROM RuleSet where RlsNameEn= @P1 ", new string[] { txtRuleSet.Text.Trim() });
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
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}
