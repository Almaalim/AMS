using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class Branches : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    BranchPro ProCs = new BranchPro();
    BranchSql SqlCs = new BranchSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM Branch WHERE ISNULL(BrcDeleted,0) = 0 ";
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
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
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
           CtrlCs.FillMgrsList(ref ddlBranchManagerID, null, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool IsLicensed(string CommandName)
    {
        string licValue = LicDf.FetchLic("MB");

        if (CommandName == "ADD" && licValue == "0") 
        { 
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT COUNT(*) FROM Branch WHERE ISNULL(BrcDeleted,0) = 0"));
            int dbBrcCount = Convert.ToInt32(DT.Rows[0][0]);
            if (dbBrcCount >= 1) 
            { 
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("You dont have license more than one Branch", "لا تملك تصريح لإنشاء أكثر من فرع"));
                return false; 
            } 
        }
        
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

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            sql = MainQuery + " AND " + ddlFilter.SelectedValue + " LIKE @P1";
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
            spnNameEn.Visible = true;
        }
        else
        {
            grdData.Columns[2].Visible = false;
            ddlFilter.Items.FindByValue("BrcNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            spnNameAr.Visible = true;
        }
        else
        {
            grdData.Columns[0].Visible = false;
            ddlFilter.Items.FindByValue("BrcNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        txtNameAr.Enabled           = pStatus;
        txtNameEn.Enabled           = pStatus;
        txtBranchAddress.Enabled    = pStatus;
        txtBranchCity.Enabled       = pStatus;
        txtBranchCountry.Enabled    = pStatus;
        txtBranchPoBox.Enabled      = pStatus;
        txtBranchTell.Enabled       = pStatus;
        txtBranchEmail.Enabled      = pStatus;
        chkStatus.Enabled           = pStatus;
        ddlBranchManagerID.Enabled  = pStatus;
        ddlBranchParentName.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.BrcID = txtID.Text; }

            ProCs.BrcNameAr  = txtNameAr.Text;
            ProCs.BrcNameEn  = txtNameEn.Text;
            ProCs.BrcAddress = txtBranchAddress.Text;
            ProCs.BrcCity    = txtBranchCity.Text;
            ProCs.BrcCountry = txtBranchCountry.Text;
            ProCs.BrcPOBox   = txtBranchPoBox.Text;
            ProCs.BrcTelNo   = txtBranchTell.Text;
            ProCs.BrcEmail   = txtBranchEmail.Text;
            if (ddlBranchManagerID.SelectedIndex > 0) { ProCs.UsrName = ddlBranchManagerID.Text; }
            ProCs.BrcStatus = chkStatus.Checked;
        
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtID.Text = "";
        txtNameAr.Text = "";
        txtNameEn.Text = "";
        txtBranchAddress.Text = "";
        txtBranchCity.Text = "";
        txtBranchCountry.Text = "";
        txtBranchPoBox.Text = "";
        txtBranchTell.Text = "";
        txtBranchEmail.Text = "";
        chkStatus.Checked = false;
        ddlBranchManagerID.SelectedIndex = -1;
        ddlBranchParentName.SelectedIndex = -1;
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
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }

            // for Branch Lincense /////////////////////////////////////////////////////////////////////////////////
            if (!IsLicensed(ViewState["CommandName"].ToString())) { return; }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        //if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }
                        ImageButton _btnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        _btnDelete.Enabled = pgCs.getPermission(ViewState["ht"], "Delete");
                        _btnDelete.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        
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
                    string ID = e.CommandArgument.ToString();
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM Department WHERE ISNULL(DepDeleted,0) = 0 AND BrcID = @P1 ", new string[] { ID });
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
            DataRow[] DRs = DT.Select("BrcID =" + pID + "");

            txtID.Text            = DRs[0]["BrcID"].ToString();
            txtNameAr.Text        = DRs[0]["BrcNameAr"].ToString();
            txtNameEn.Text        = DRs[0]["BrcNameEn"].ToString();
            txtBranchAddress.Text = DRs[0]["BrcAddress"].ToString();
            txtBranchCity.Text    = DRs[0]["BrcCity"].ToString();
            txtBranchCountry.Text = DRs[0]["BrcCountry"].ToString();
            txtBranchPoBox.Text   = DRs[0]["BrcPOBox"].ToString();
            txtBranchTell.Text    = DRs[0]["BrcTelNo"].ToString();
            txtBranchEmail.Text   = DRs[0]["BrcEmail"].ToString();
            
            if (DRs[0]["BrcStatus"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["BrcStatus"]); }
            ddlBranchManagerID.SelectedIndex = ddlBranchManagerID.Items.IndexOf(ddlBranchManagerID.Items.FindByValue(DRs[0]["UsrName"].ToString()));
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

    protected void NameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND BrcID != @P2 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameEn.Text)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(txtNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered Branch English Name exist already,Please enter another name", "إسم الفرع بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM Branch WHERE BrcNameEn = @P1 AND ISNULL(BrcDeleted,0) = 0 " + UQ, new string[] { txtNameEn.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, false, General.Msg("Name (Ar) Is Required", "الاسم بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameAr.Text)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(txtNameAr.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Branch Arabic Name exist already,Please enter another name", "إسم الفرع بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM Branch WHERE BrcNameAr = @P1 AND ISNULL(BrcDeleted,0) = 0 " + UQ, new string[] { txtNameAr.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
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