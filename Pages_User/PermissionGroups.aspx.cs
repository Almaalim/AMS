using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using Elmah;
using System.Data.SqlClient;

public partial class PermissionGroups : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersPro ProCs = new UsersPro();
    UsersSql SqlCs = new UsersSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    
    string MainQuery = " SELECT * FROM PermissionGroup WHERE ISNULL(GrpDeleted,0) = 0 ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.Animation(ref AnimationExtenderShow1, ref AnimationExtenderClose1, ref lnkShow1, "1");
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            /*** TreeView Code ***********************************/
            FillPageTree("");
            FillRepTree();
            /*** TreeView Code ***********************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                /*** Common Code ************************************/
                
                trvMenu.Attributes.Add("onclick", "OnTreeClick(event)");
                trvReport.Attributes.Add("onclick", "OnTreeClick(event)");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPageTree(string PermSet)
    {
        try
        {
            DataSet PageDS = new DataSet();
            PageDS = GenCs.FillPageTree(pgCs.Lang, pgCs.Version, "");
            xmlDataSource1.Data = PageDS.GetXml();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillRepTree()
    {
        try
        {
            DataSet RepDS = new DataSet();
            RepDS = GenCs.FillRepTree(pgCs.Lang, pgCs.Version);
            xmlReportSource.Data = RepDS.GetXml();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    //protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    //{
    //    StringBuilder filterQuery = new StringBuilder();
    //    if (ddlFilter.SelectedIndex > 0)
    //    {
    //        UIClear();
    //        filterQuery.Append("SELECT * FROM AppUser WHERE " + ddlFilter.SelectedItem.Value + " LIKE'" + txtFilter.Text.Trim() + "%' AND ISNULL(UsrDeleted,0) = 0");
    //    }
    //    else
    //    {
    //        filterQuery.Append(GenralQuery);
    //    }

    //    FillGrid(filterQuery.ToString());
    //}

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
            //lblGroupName.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            grdData.Columns[2].Visible = false;
        }

        if (pgCs.LangAr)
        {
            //lblGroupNameArb.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            grdData.Columns[0].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtGrpID.Enabled = txtGrpID.Visible = false;
        
        ddlGroupType.Enabled = pStatus;
        txtGroupNameArb.Enabled = pStatus;
        txtGroupNameEn.Enabled = pStatus;
        txtGroupDescription.Enabled = pStatus;

        trvReport.Enabled = pStatus;
        trvMenu.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!string.IsNullOrEmpty(txtGrpID.Text)) { ProCs.GrpID = txtGrpID.Text; }
            ProCs.GrpNameEn      = txtGroupNameEn.Text;
            ProCs.GrpNameAr      = txtGroupNameArb.Text;
            ProCs.GrpDescription = txtGroupDescription.Text;
            ProCs.GrpType        = ddlGroupType.SelectedValue;
            ProCs.TransactionBy  = pgCs.LoginID;

            string perm = "";
            if (ddlGroupType.SelectedItem.Value.ToString() == "Forms") { perm = GenCs.CreateIDsNumber(trvMenu); }
            else { perm = GenCs.CreateIDsNumber(trvReport); }
            
            ProCs.GrpPermissions = CryptorEngine.Encrypt(Convert.ToString(perm), true);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ddlGroupType.ClearSelection();
        txtGroupNameEn.Text = "";
        txtGroupNameArb.Text = "";
        txtGroupDescription.Text = "";
        ViewState["Permissions"] = string.Empty;
        trvReport.CheckedNodes.Clear();
        trvMenu.CheckedNodes.Clear();
        trvMenu.DataBind();
        trvReport.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroupType.SelectedValue == "Forms")
        {
            divMenu.Visible = true;
            divReports.Visible = false;
        }
        else if (ddlGroupType.SelectedValue == "Reports")
        {
            divMenu.Visible = false;
            divReports.Visible = true;
        }
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
        ViewState["CommandName"] = "ADD";
        UIClear();
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
        ddlGroupType.Enabled = false;
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

            if (commandName == "ADD")
            {
                Int32 res = SqlCs.PermissionGroup_Insert(ProCs);
            }
            else if (commandName == "EDIT")
            {
                bool res = SqlCs.PermissionGroup_Update(ProCs);
            }

            UIClear();
            UIEnabled(false);
            BtnStatus("1000");
            FillGrid(new SqlCommand(MainQuery));
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
        FillGrid(new SqlCommand(MainQuery));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                
                Hashtable ht = (Hashtable)ViewState["ht"];
                if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                delBtn.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
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
                    
                    if ((ID == "1") || (ID == "2") || (ID == "3"))
                    {
                        CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("You can not delete the main Permission Groups", "لا يمكن حذف مجموعات الصلاحيات الرئيسية"));
                        return;
                    }

                    DataTable DT = DBCs.FetchData("SELECT * FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 AND (GrpID = @P1 OR ReportGrpID = @P1) ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }
                
                    SqlCs.PermissionGroup_Delete(ID, pgCs.LoginID);                  
                    CtrlCs.ShowDelMsg(this, true);

                    UIClear();
                    FillGrid(new SqlCommand(MainQuery));
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
        //try
        //{

        //System.Text.StringBuilder query = new System.Text.StringBuilder();
        //query.Append("Select * from PermissionGroup where (GrpDeleted is null or GrpDeleted = 0)");

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

        //    grdPerm.DataSource = dataView;
        //    grdPerm.DataBind();
        //}

        //}
        //catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
        FillGrid(new SqlCommand(MainQuery));
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

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[1].Text) && grdData.SelectedRow.Cells.Count == 1)
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
            if (!DBCs.IsNullOrEmpty(DT))
            {
                DataRow[] DRs = DT.Select("GrpID = " + pID + "");
                
                txtGrpID.Text            = DRs[0]["GrpID"].ToString();
                txtGroupNameEn.Text      = DRs[0]["GrpNameEn"].ToString();
                txtGroupNameArb.Text     = DRs[0]["GrpNameAr"].ToString();
                txtGroupDescription.Text = DRs[0]["GrpDescription"].ToString();
                ddlGroupType.SelectedIndex = ddlGroupType.Items.IndexOf(ddlGroupType.Items.FindByValue(DRs[0]["GrpType"].ToString()));
           
                ViewState["Permissions"] = string.Empty;
                if (!string.IsNullOrEmpty(DRs[0]["GrpPermissions"].ToString()))
                {
                    string cipherText = DRs[0]["GrpPermissions"].ToString();
                    string permmissionSet = CryptorEngine.Decrypt(cipherText, true);
                    ViewState["Permissions"] = permmissionSet;
                }

                if (DRs[0]["GrpType"].ToString() == "Forms")
                {
                    divMenu.Visible = true;
                    divReports.Visible = false;
                    trvMenu.DataBind();
                }
                else if (DRs[0]["GrpType"].ToString() == "Reports")
                {
                    divMenu.Visible = false;
                    divReports.Visible = true;
                    trvReport.DataBind();
                }
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
    #region TreeView Events

    protected void trvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        //Label1.Text = "U Have Selected" + trvMenu.SelectedNode.Text.ToString();

        //dt = DBCs.FetchData("select * from Menu where visible='True'");
        //if (!DBCs.IsNullOrEmpty(dt))
        //    DBCs.PopulateDDL(ddlMenuItemParent, dt, "Text", "MenuID", "-Select Parent--");

        //PopulateUI(Convert.ToString(trvMenu.SelectedNode.Value));
        //UpdatePanel1.Update();

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvMenu_DataBound(object sender, EventArgs e)
    {
        trvMenu.CollapseAll();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvMenu_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        try
        {
            // Showing the treeview as per the permissions available
            if (ViewState["Permissions"] != null)
            {
                int str = Convert.ToInt32(e.Node.Value);
                string permmissionSet = Convert.ToString(ViewState["Permissions"]);
                //string permmissionSet = CryptorEngine.Decrypt(cipherText, true);
                if (permmissionSet != string.Empty)
                {
                    string[] usrPermissions = Convert.ToString(permmissionSet).Split(',');
                    for (int i = 0; i < usrPermissions.Length; i++)
                    {
                        if (Convert.ToInt32(usrPermissions[i]) == str)
                            e.Node.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvReport_DataBound(object sender, EventArgs e)
    {
        trvReport.CollapseAll();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvReport_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        try
        {
            if (ViewState["Permissions"] != null)
            {
                // Showing the treeview as per the permissions available
                string str = Convert.ToString(e.Node.Value.Trim());

                string permmissionSet = Convert.ToString(ViewState["Permissions"]);
                //string permmissionSet = CryptorEngine.Decrypt(cipherText, true);

                if (permmissionSet != string.Empty)
                {

                    string[] usrPermissions = Convert.ToString(permmissionSet).Split(',');



                    for (int i = 0; i < usrPermissions.Length; i++)
                    {

                        if (Convert.ToString(usrPermissions[i]) == str)
                            e.Node.Checked = true;

                    }
                }


            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND GrpID != @P2"; }

            if (source.Equals(cvGroupNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvGroupNameEn, false, General.Msg("Name (En) is required", "الاسم بالانجليزية مطلوب")); 
                    if (String.IsNullOrEmpty(txtGroupNameEn.Text)) { e.IsValid = false; }
                }

                if (!String.IsNullOrEmpty(txtGroupNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvGroupNameEn, true, General.Msg("Entered Permission group English Name exist already,Please enter another name", "الاسم بالانجليزية لمجموعة الصلاحيات موجود مسبقا , الرجاء اختيار اسم آخر")); 
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM PermissionGroup WHERE GrpNameEn = @P1 AND ISNULL(GrpDeleted,0) = 0 " + UQ, new string[] { txtGroupNameEn.Text, txtGrpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvGroupNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvGroupNameAr, false, General.Msg("Name (Ar) is required", "الاسم بالعربية مطلوب")); 
                    if (String.IsNullOrEmpty(txtGroupNameArb.Text)) { e.IsValid = false; }
                }

                if (!String.IsNullOrEmpty(txtGroupNameArb.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvGroupNameAr, true, General.Msg("Entered Permission group Arabic Name exist already,Please enter another name", "الاسم بالعربية لمجموعة الصلاحيات موجود مسبقا , الرجاء اختيار اسم آخر")); 
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM PermissionGroup WHERE GrpNameAr = @P1 AND ISNULL(GrpDeleted,0) = 0 " + UQ, new string[] { txtGroupNameArb.Text, txtGrpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            e.IsValid = false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
