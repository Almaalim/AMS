using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class Machine : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    MachinesPro ProCs = new MachinesPro();
    MachineSql  SqlCs = new MachineSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = " SELECT * FROM MachineInfoView WHERE MacVirtualType IS NULL AND ISNULL(MacDeleted,0) = 0 ";
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
            CtrlCs.FillMachineTypeList(ref ddlMacType, rfvMacType, false, true);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool DeviceStatus(string devNum)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT COUNT(*) FROM Machine WHERE ISNULL(MacDeleted,0) = 0 AND MacStatus ='true' "));
        int dbDevNum = Convert.ToInt32(DT.Rows[0][0]);

        if ((dbDevNum < Convert.ToInt32(devNum))) { return false; }

        return true;
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
            sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1 ";
            cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
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

    public void UILang()
    {
        if (pgCs.LangEn)
        {
            spnMacLocEn.Visible = true;
        }
        else
        {
            grdData.Columns[3].Visible = false;
            ddlFilter.Items.FindByValue("MacLocationEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            spnMacLocAr.Visible = true;
        }
        else
        {
            grdData.Columns[2].Visible = false;
            ddlFilter.Items.FindByValue("MacLocationAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM Configuration"));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["cfgOrderTransType"].ToString() == "MT") { ddlMacTrnType.Enabled = pStatus; } else { ddlMacTrnType.Enabled = false; }
        }

        txtID.Enabled = txtID.Visible = false;

        ddlMacType.Enabled = pStatus;
      
        txtMacLocEn.Enabled = pStatus;
        txtMacLocAr.Enabled = pStatus;
        txtMachineNo.Enabled = pStatus;
        txtMachinePort.Enabled = pStatus;
        txtMachineIP.Enabled = pStatus;
        calMachineInsDate.SetEnabled(pStatus);
        chkStatus.Enabled = pStatus;
        ddlMacTransactionType.Enabled = pStatus;

        if (LicDf.FetchLic("RP") == "0") { ddlMacTransactionType.Items.FindByValue("RP").Enabled = false; } 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SpnVisible(bool show)
    {
        if (show)
        {
            spnMacIp.Visible = false;
            spnMacPort.Visible = false;
        }
        else
        {
            spnMacIp.Visible = true;
            spnMacPort.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.MacID = txtID.Text; }
            ProCs.MtpID = ddlMacType.SelectedValue;

            if (ddlMacTrnType.SelectedIndex > 0) { ProCs.MacInOutType = Convert.ToBoolean(Convert.ToInt32(ddlMacTrnType.SelectedValue)); }
            ProCs.MacTransactionType = ddlMacTransactionType.SelectedValue;

            if (!string.IsNullOrEmpty(txtMacLocAr.Text)) { ProCs.MacLocationAr = txtMacLocAr.Text; }
            if (!string.IsNullOrEmpty(txtMacLocEn.Text)) { ProCs.MacLocationEn = txtMacLocEn.Text; }

            if (!string.IsNullOrEmpty(txtMachineNo.Text)) { ProCs.MacNo = txtMachineNo.Text; }

            if (!string.IsNullOrEmpty(txtMachinePort.Text)) { ProCs.MacPort = txtMachinePort.Text; }
            if (!string.IsNullOrEmpty(txtMachineIP.Text)) { ProCs.MacIP = txtMachineIP.Text; }
            ProCs.MacInstallDate = calMachineInsDate.getGDateDBFormat();

            // if dev num more than lincense, the status will be false
            string licDeviceNum = LicDf.FetchLic("DN");
            if (DeviceStatus(licDeviceNum)) { ProCs.MacStatus = false; } else { ProCs.MacStatus = chkStatus.Checked; }
            
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
            
            ddlMacType.SelectedIndex = 0;
            ddlMacTrnType.SelectedIndex = 0;
            ddlMacTransactionType.SelectedIndex = 0;
            txtMacLocAr.Text = "";
            txtMacLocEn.Text = "";
            txtMachineNo.Text = "";
            txtMachinePort.Text = "";
            txtMachineIP.Text = "";
            calMachineInsDate.ClearDate();
            chkStatus.Checked = false;

            SpnVisible(false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMacTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMacTransactionType.SelectedValue == "RP") { SpnVisible(false); } else { SpnVisible(true); }
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
            BtnStatus("0011");
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
        try
        {
            UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "EDIT";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ////////////////////////////////////
            DeviceStatus(LicDf.FetchLic("DN")); // for License Machine Count //
            ////////////////////////////////////
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (ViewState["CommandName"].ToString() == "ADD") { SqlCs.Insert(ProCs); } else if (ViewState["CommandName"].ToString() == "EDIT") { SqlCs.Update(ProCs); }

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
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

                    System.Text.StringBuilder QDel = new System.Text.StringBuilder();
                    QDel.Append(" SELECT MacID FROM TransDump WHERE MacID = @P1 ");
                    QDel.Append(" UNION ");
                    QDel.Append(" SELECT MacID FROM Trans WHERE MacID = @P1 ");
                    QDel.Append(" UNION ");
                    QDel.Append(" SELECT MacID FROM RoundPatrolTransaction WHERE MacID = @P1 ");
                    QDel.Append(" UNION ");
                    QDel.Append(" SELECT MacID FROM InspectionToursTrans WHERE MacID = @P1 ");

                    DataTable DT = DBCs.FetchData(QDel.ToString(), new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }

                    SqlCs.Delete(ID, pgCs.LoginID);
                    
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
                BtnStatus("1100");
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
            DataRow[] DRs = DT.Select("MacID =" + pID + "");

            txtID.Text = DRs[0]["MacID"].ToString();
            ddlMacType.SelectedIndex = ddlMacType.Items.IndexOf(ddlMacType.Items.FindByValue(DRs[0]["MtpID"].ToString()));

            if (!string.IsNullOrEmpty(DRs[0]["MacInOutType"].ToString()))
            {
                if (DRs[0]["MacInOutType"].ToString() == "True") { ddlMacTrnType.SelectedIndex = 1; } else { ddlMacTrnType.SelectedIndex = 2; }
            }
            else { ddlMacTrnType.SelectedIndex = 0; }

            ddlMacTransactionType.SelectedIndex = ddlMacTransactionType.Items.IndexOf(ddlMacTransactionType.Items.FindByValue(DRs[0]["MacTransactionType"].ToString()));
            if (ddlMacTransactionType.SelectedValue == "RP") { SpnVisible(false); } else { SpnVisible(true); }

            txtMacLocAr.Text    = DRs[0]["MacLocationAr"].ToString();
            txtMacLocEn.Text    = DRs[0]["MacLocationEn"].ToString();
            txtMachineNo.Text   = DRs[0]["MacNo"].ToString();
            txtMachinePort.Text = DRs[0]["MacPort"].ToString();
            txtMachineIP.Text   = DRs[0]["MacIP"].ToString();

            calMachineInsDate.SetGDate(DRs[0]["MacInstallDate"], pgCs.DateFormat);
            if (DRs[0]["MacStatus"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["MacStatus"]); }
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
    #region Custom Validate Events

    protected void MachineValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string NameEn = txtMacLocEn.Text.Trim();
            string NameAr = txtMacLocAr.Text.Trim();

            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND MacID != @P2 "; }

            if (source.Equals(cvMacLocEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvMacLocEn, false, General.Msg("Location (En) Is Required", "اسم الجهاز بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(NameEn)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(NameEn))
                {
                    CtrlCs.ValidMsg(this, ref cvMacLocEn, true, General.Msg("Entered Machine English Name exist already,Please enter another name", "إسم الجهاز بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Machine WHERE MacLocationEn = @P1 AND ISNULL(MacDeleted,0) = 0 " + UQ, new string[] { NameEn, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }

            if (source.Equals(cvMacLocAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvMacLocAr, false, General.Msg("Location (Ar) Is Required", "اسم الجهاز بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(NameAr)) { e.IsValid = false; }
                }
                if (!string.IsNullOrEmpty(NameAr))
                {
                    CtrlCs.ValidMsg(this, ref cvMacLocAr, true, General.Msg("Entered Machine Arabic Name exist already,Please enter another name", "إسم الجهاز بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM Machine WHERE MacLocationAr = @P1 AND ISNULL(MacDeleted,0) = 0 " + UQ, new string[] { NameAr, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }

            if (source.Equals(cvMachineNo))
            {
                if (!string.IsNullOrEmpty(txtMachineNo.Text.Trim()))
                {
                    CtrlCs.ValidMsg(this, ref cvMachineNo, true, General.Msg("Entered Machine Number exist already,Please enter another Number", "رقم الجهاز تم إدخاله مسبقا ، الرجاء إدخال رقم آخر")); 
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM Machine WHERE MacNo = @P1 AND ISNULL(MacDeleted,0) = 0 " + UQ, new string[] { txtMachineNo.Text.Trim(), txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }

            if (ddlMacTransactionType.SelectedValue != "RP")
            {
                if (source.Equals(cvMacIP))
                {
                    CtrlCs.ValidMsg(this, ref cvMacIP, false, General.Msg("IP Adress Is Required", "عنوان IP مطلوب"));
                    if (string.IsNullOrEmpty(txtMachineIP.Text.Trim())) { e.IsValid = false; }
                }
                if (source.Equals(cvMacPort))
                {
                    CtrlCs.ValidMsg(this, ref cvMacPort, false, General.Msg("Port Is Required", "رقم المنفذ مطلوب"));
                    if (string.IsNullOrEmpty(txtMachinePort.Text.Trim())) { e.IsValid = false; }
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