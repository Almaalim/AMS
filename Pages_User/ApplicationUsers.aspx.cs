using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;

public partial class ApplicationUsers : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersPro ProCs = new UsersPro();
    UsersSql SqlCs = new UsersSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = " SELECT * FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 ";
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
            FillRepTree("");
            /*** TreeView Code ***********************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                if (LicDf.FetchLic("ML") == "1") { divML.Visible = true; } else { divML.Visible = false; }
                BtnStatus("1000");
                UIEnabled(false);
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                /*** Common Code ************************************/

                int ErsEnable   = Convert.ToInt32(LicDf.FetchLic("ER"));
                int AlertEnable = Convert.ToInt32(LicDf.FetchLic("ES"));

                if ((ErsEnable == 1) && (AlertEnable == 1))
                {
                    lblMob.Visible  = lblEmail.Visible    = true;
                    cvEmail.Enabled = cdvMobileNo.Enabled = true;
                }
                else
                {
                    lblMob.Visible  = lblEmail.Visible    = false;
                    cvEmail.Enabled = cdvMobileNo.Enabled = false;
                }

                TreeView1.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
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
            CtrlCs.FillPermissionGroupList(ref ddlPermissionGroup, null, false, "Forms");
            CtrlCs.FillPermissionGroupList(ref ddlRepPermissionGroup, null, false, "Reports");
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
            PageDS = GenCs.FillPageTree(pgCs.Lang, pgCs.Version, PermSet);
            xmlDataSource1.Data = PageDS.GetXml();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillRepTree(string PermSet)
    {
        try
        {
            DataSet RepDS = new DataSet();
            RepDS = GenCs.FillRepTree(pgCs.Version, PermSet);
            xmlReportSource.Data = RepDS.GetXml();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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

    public void UIEnabled(bool pStatus)
    {
        txtUsername.Enabled = pStatus;
        txtPassword.Enabled = pStatus;
        ddlLanguage.Enabled = pStatus;
        txtFullname.Enabled = pStatus;
        chkActiveUser.Enabled = pStatus;
        chkAdminMiniLogger.Enabled = pStatus;
        txtDescription.Enabled = pStatus;
        txtDomainName.Enabled = pStatus;
        ddlPermissionGroup.Enabled = pStatus;
        ddlRepPermissionGroup.Enabled = pStatus;
        calStartDate.SetEnabled(pStatus); 
        calEndDate.SetEnabled(pStatus); 
        txtMobileNo.Enabled = pStatus;
        txtEmailID.Enabled = pStatus;
        chkEmailAlert.Enabled = pStatus;
        chkMobileAlert.Enabled = pStatus;
        chkActUser.Enabled = pStatus;
        txtEmpID.Enabled   = pStatus;
        txtUsrADUser.Enabled = pStatus;
        btnGetADUsr.Enabled = pStatus;

        btnActingUser.Enabled = false;
        //string commandName = ViewState["CommandName"].ToString();
        //if (commandName == "update") { btnActingUser.Enabled = pStatus; } else { btnActingUser.Enabled = false; }

        TreeView1.Enabled = false;
        trvReport.Enabled = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            ProCs.UsrName            = txtUsername.Text.Trim();
            ProCs.UsrFullName        = txtFullname.Text.Trim();
            ProCs.EmpID              = txtEmpID.Text.Trim();
            ProCs.UsrPassword        = CryptorEngine.Encrypt(txtPassword.Text.Trim(), true);
            if (ddlLanguage.SelectedIndex > 0) { ProCs.UsrLanguage = ddlLanguage.SelectedValue; }
            ProCs.UsrStatus          = chkActiveUser.Checked;
            ProCs.UsrActingUser      = chkActUser.Checked;
            ProCs.UsrAdminMiniLogger = chkAdminMiniLogger.Checked;
            ProCs.UsrDesc            = txtDescription.Text.Trim();

            if (!string.IsNullOrEmpty(calStartDate.getGDate())) { ProCs.UsrStartDate  = calStartDate.getGDateDBFormat(); }
            if (!string.IsNullOrEmpty(calEndDate.getGDate()))   { ProCs.UsrExpireDate = calEndDate.getGDateDBFormat(); }

            if (ddlPermissionGroup.SelectedIndex > 0)    { ProCs.UsrGrpID       = ddlPermissionGroup.SelectedValue; }
            if (ddlRepPermissionGroup.SelectedIndex > 0) { ProCs.UsrReportGrpID = ddlRepPermissionGroup.SelectedValue; }
            ProCs.UsrDomainName = txtDomainName.Text.Trim();
            
            ProCs.UsrEMailID     = txtEmailID.Text;
            ProCs.UsrMobileNo    = txtMobileNo.Text;
            ProCs.UsrSendEmailAlert = chkEmailAlert.Checked;
            ProCs.UsrSendSMSAlert = chkMobileAlert.Checked;

            ProCs.TransactionBy  = pgCs.LoginID;

            if (!string.IsNullOrEmpty(txtUsrADUser.Text)) { ProCs.UsrADUser = txtUsrADUser.Text; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            txtUsername.Text = "";
            txtPassword.Attributes["value"] = "";
            txtEmpID.Text = "";
            ddlLanguage.ClearSelection();
            txtFullname.Text = "";
            chkActiveUser.Checked = false;
            chkAdminMiniLogger.Checked = false;
            chkActUser.Checked = false;
            txtDescription.Text = "";
            txtDomainName.Text = "";
            ddlPermissionGroup.ClearSelection();
            ddlRepPermissionGroup.ClearSelection();
            calStartDate.ClearDate();
            calEndDate.ClearDate();
            ViewState["RepPermissions"] = null;
            ViewState["Permissions"] = null;
            TreeView1.CheckedNodes.Clear();
            TreeView1.DataBind();

            trvReport.CheckedNodes.Clear();
            trvReport.DataBind();

            chkEmailAlert.Checked = false;
            chkMobileAlert.Checked = false;
            txtEmailID.Text = "";
            txtMobileNo.Text = "";
            txtUsrADUser.Text= "";
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlPermissionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Permissions"] = string.Empty;

            if (ddlPermissionGroup.SelectedIndex > 0)
            {
                int groupID = Convert.ToInt32(ddlPermissionGroup.SelectedValue);
                if (groupID < 1) { return; }
                
                // To show the permissions in the treeview as per the selected index
                DataTable DT = DBCs.FetchData(" SELECT * FROM PermissionGroup WHERE GrpID = @P1 AND ISNULL(GrpDeleted,0) = 0 ", new string[] { groupID.ToString() });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string cipherText = DT.Rows[0]["GrpPermissions"].ToString();
                    string permmissionSet = CryptorEngine.Decrypt(cipherText, true);
                    ViewState["Permissions"] = permmissionSet.ToString();

                    if (!string.IsNullOrEmpty(permmissionSet))
                    {
                        FillPageTree(permmissionSet);
                    }
                }

                TreeView1.DataBind();
                TreeView1.Visible = true;
            }
            else
            {
                TreeView1.Visible = false;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRepPermissionGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["RepPermissions"] = null;

            if (ddlRepPermissionGroup.SelectedIndex > 0)
            {
                int repgroupID = Convert.ToInt32(ddlRepPermissionGroup.SelectedValue);
                if (repgroupID < 1) { return; }

                DataTable DT = DBCs.FetchData(" SELECT * FROM PermissionGroup WHERE GrpID = @P1 AND ISNULL(GrpDeleted,0) = 0 ", new string[] { repgroupID.ToString() });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string cipherText = DT.Rows[0]["GrpPermissions"].ToString();
                    string permmissionSet = CryptorEngine.Decrypt(cipherText, true);
                    ViewState["RepPermissions"] = permmissionSet.ToString();


                    if (!string.IsNullOrEmpty(permmissionSet))
                    {
                        FillRepTree(permmissionSet);
                    }
                }

                trvReport.DataBind();
                trvReport.Visible = true;
            }
            else
            {
                TreeView1.Visible = false;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnGetADUsr_Click(object sender, ImageClickEventArgs e)
    {
        txtUsrADUser.Text = "";

        UsersPro AD = ActiveDirectoryFun.FillAD(ActiveDirectoryFun.ADTypeEnum.USR, txtEmpID.Text.Trim(), txtEmpID.Text.Trim(), txtEmailID.Text.Trim());
        if (!AD.ADValid)      { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADError); return; }
        if (!AD.ADValidation) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADMsgValidation); return; }

        txtUsrADUser.Text = ActiveDirectoryFun.GetAD(AD);
        if (string.IsNullOrEmpty(txtUsrADUser.Text)) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADMsgNotExists); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected int GetUserCount()
    {
        // Verifying from license for the number of users allowed
        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT Count(*) as Count FROM AppUser  WHERE  UsrStatus = '1' AND ISNULL(UsrDeleted,0) = 0"));
        if (!DBCs.IsNullOrEmpty(DT)) { return Convert.ToInt32(DT.Rows[0][0]); } else { return 0; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            // Verifying from license for the number of users allowed
            int AllowedUsers = Convert.ToInt32(LicDf.FetchLic("UN"));

            int AvailableUsers = GetUserCount();

            if (AvailableUsers >= AllowedUsers)
            {
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.Msg("Upgrade license to add new user", "يجب ترقية الرخصة لإضافة مستخدم جديد"));
            }
            else
            {
                ViewState["CommandName"] = "ADD";
                UIClear();
                txtUsername.Focus();
                UIEnabled(true);
                BtnStatus("0011");
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
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            UIEnabled(true);
            txtUsername.Enabled = false;

            if (txtUsername.Text.Trim() == "admin")
            {
                chkActiveUser.Enabled = false;
                chkActiveUser.Checked = true;
                ddlPermissionGroup.Enabled = false;
                ddlRepPermissionGroup.Enabled = false;           
            }
            else
            {
                chkActiveUser.Enabled = true;
                ddlPermissionGroup.Enabled = true;
                ddlRepPermissionGroup.Enabled = true;
            }
            ViewState["CommandName"] = "EDIT";
            BtnStatus("0011");
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
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }
            string CommandName = ViewState["CommandName"].ToString();

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (CommandName == "ADD") { SqlCs.AppUser_Insert(ProCs); } else if (CommandName == "EDIT") { SqlCs.AppUser_Update(ProCs); }
            
            UIClear();
            UIEnabled(false);
            BtnStatus("1000");
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
        string commandName = ViewState["CommandName"].ToString();
        if      (commandName == string.Empty) { return; }
        else if (commandName == "ADD")  { UIClear(); }
        else if (commandName == "EDIT") { PopulateUI(txtUsername.Text); }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnActingUser_Click(object sender, EventArgs e)
    {

        ActPopulateUI(txtUsername.Text);
        DivPopup.Visible = true;

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup('" + DivPopup.ClientID + "');", true);
    } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = btnActingUser.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = btnActingUser.Enabled = Permht.ContainsKey("Update"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
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
                case DataControlRowType.DataRow: { break; }
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
                    string userID = e.CommandArgument.ToString();
                    if (userID.Trim() == "admin")
                    {
                        CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("The admin user can not delete", "لا يمكن حذف المستخدم admin"));
                        return;
                    }
                    
                    bool isDel = SqlCs.AppUser_Delete(userID, pgCs.LoginID);
                    
                    if (isDel) { CtrlCs.ShowDelMsg(this, true); } else { CtrlCs.ShowDelMsg(this, false); }
                    UIClear();
                    UIEnabled(false);
                    BtnStatus("1000");
                    btnFilter_Click(null,null);
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
        UIClear();
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            trvReport.Visible = false;
            TreeView1.Visible = false;
            UIClear();

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[0].Text);
                BtnStatus("1100");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string userName)
    {
        try
        {
            UIEnabled(false);
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("UsrName ='" + userName + "'");

            txtUsername.Text                    = userName;
            txtFullname.Text                    = DRs[0]["UsrFullName"].ToString();
            txtEmpID.Text                       = DRs[0]["EmpID"].ToString();
            ddlLanguage.SelectedIndex           = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(DRs[0]["UsrLanguage"].ToString()));
            if (DRs[0]["UsrStatus"]          != DBNull.Value) { chkActiveUser.Checked      = Convert.ToBoolean(DRs[0]["UsrStatus"]); }          else { chkActiveUser.Checked      = false; }
            if (DRs[0]["UsrActingUser"]      != DBNull.Value) { chkActUser.Checked         = Convert.ToBoolean(DRs[0]["UsrActingUser"]); }      else { chkActUser.Checked         = false; }
            if (DRs[0]["UsrAdminMiniLogger"] != DBNull.Value) { chkAdminMiniLogger.Checked = Convert.ToBoolean(DRs[0]["UsrAdminMiniLogger"]); } else { chkAdminMiniLogger.Checked = false; }
            txtDescription.Text                 = DRs[0]["UsrDesc"].ToString();
            txtDomainName.Text                  = DRs[0]["UsrDomainName"].ToString();
            ddlPermissionGroup.SelectedIndex    = ddlPermissionGroup.Items.IndexOf(ddlPermissionGroup.Items.FindByValue(DRs[0]["GrpID"].ToString()));
            ddlRepPermissionGroup.SelectedIndex = ddlRepPermissionGroup.Items.IndexOf(ddlRepPermissionGroup.Items.FindByValue(DRs[0]["ReportGrpID"].ToString()));
            
            txtEmpID.Text                       = DRs[0]["UsrStartDate"].ToString();

            calStartDate.SetGDate(DRs[0]["UsrStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["UsrExpireDate"],  pgCs.DateFormat);

            txtPassword.Attributes["value"]     = CryptorEngine.Decrypt(DRs[0]["UsrPassword"].ToString(), true);
            txtEmailID.Text                     = DRs[0]["UsrEMailID"].ToString();
            txtMobileNo.Text                    = DRs[0]["UsrMobileNo"].ToString(); 
            txtUsrADUser.Text                   = DRs[0]["UsrADUser"].ToString(); 
            
            if (DRs[0]["UsrSendEmailAlert"] != DBNull.Value) { chkEmailAlert.Checked  = Convert.ToBoolean(DRs[0]["UsrSendEmailAlert"]); } else { chkEmailAlert.Checked  = false; }
            if (DRs[0]["UsrSendSMSAlert"]   != DBNull.Value) { chkMobileAlert.Checked = Convert.ToBoolean(DRs[0]["UsrSendSMSAlert"]);   } else { chkMobileAlert.Checked = false; }
            //////////////////////////////////////////////
            ViewState["Permissions"] = string.Empty;
            string permmissionSet    = string.Empty;
            if (!string.IsNullOrEmpty(DRs[0]["GrpID"].ToString()) && DRs[0]["GrpID"].ToString() != "0")
            {
                TreeView1.Visible = true;
                int groupID = Convert.ToInt32(DRs[0]["GrpID"]);
               
                DataTable PDT = DBCs.FetchData(" SELECT * FROM PermissionGroup WHERE GrpID = @p1 AND ISNULL(GrpDeleted,0) = 0 ", new string[] { groupID.ToString() });
                if (!DBCs.IsNullOrEmpty(PDT))
                {
                    string cipherText        = PDT.Rows[0]["GrpPermissions"].ToString();
                    permmissionSet           = CryptorEngine.Decrypt(cipherText, true);
                    ViewState["Permissions"] = Convert.ToString(permmissionSet);
                }
            }
            else { TreeView1.Visible = false; }
            //////////////////////////////////////////////
            ViewState["RepPermissions"] = string.Empty;
            string RepPermmissionSet    = string.Empty;
            if (!string.IsNullOrEmpty(DRs[0]["ReportGrpID"].ToString()) && DRs[0]["ReportGrpID"].ToString() != "0")
            {
                trvReport.Visible = true;
                int repgroupID = Convert.ToInt32(DRs[0]["ReportGrpID"]);
                DataTable RDT = DBCs.FetchData(" SELECT * FROM PermissionGroup WHERE GrpID = @p1 AND ISNULL(GrpDeleted,0) = 0 ", new string[] { repgroupID.ToString() });

                if (!DBCs.IsNullOrEmpty(RDT))
                {
                    string cipherText = RDT.Rows[0]["GrpPermissions"].ToString();
                    RepPermmissionSet = CryptorEngine.Decrypt(cipherText, true);
                    ViewState["RepPermissions"] = Convert.ToString(RepPermmissionSet);
                }
            }
            else { trvReport.Visible = false; }
            //////////////////////////////////////////////
            if (!string.IsNullOrEmpty(permmissionSet))
            {
                FillPageTree(permmissionSet);
                TreeView1.DataBind();
            }
            //////////////////////////////////////////////   
            if (!string.IsNullOrEmpty(RepPermmissionSet))
            {
                FillRepTree(RepPermmissionSet);
                trvReport.DataBind();
            }
            //////////////////////////////////////////////
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

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void TreeView1_DataBound(object sender, EventArgs e)
    {
        TreeView1.ExpandAll();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        try
        {
            // Binding the permissions to the tree view
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
                        if (Convert.ToInt32(usrPermissions[i]) == str) { e.Node.Checked = true; }
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
        trvReport.ExpandAll();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvReport_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        try
        {
            if (ViewState["RepPermissions"] != null)
            {
                // Showing the treeview as per the permissions available
                string str = Convert.ToString(e.Node.Value.Trim());
                
                string permmissionSet = Convert.ToString(ViewState["RepPermissions"]);
                //string permmissionSet = CryptorEngine.Decrypt(cipherText, true);

                if (permmissionSet != string.Empty)
                {
                    string[] usrPermissions = permmissionSet.Split(',');
                    for (int i = 0; i < usrPermissions.Length; i++)
                    {
                        if (Convert.ToString(usrPermissions[i]) == str) { e.Node.Checked = true; }
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

    protected void Email_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmail))
            {
                if (string.IsNullOrEmpty(txtEmailID.Text)) 
                {
                    string ErsEnable   = LicDf.FetchLic("ER");
                    string AlertEnable = LicDf.FetchLic("ES");

                    if ((ErsEnable == "1") && (AlertEnable == "1")) { e.IsValid = false; }
                }
                else
                {
                    if (txtEmailID.Text.IndexOf('@') < 0)
                    {
                        CtrlCs.ValidMsg(this, ref cvEmail, true, General.Msg("Please enter email in correct format", "البريد الالكتروني غير صحيح"));
                        e.IsValid = false;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void MobileNo_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cdvMobileNo))
            {
                if (string.IsNullOrEmpty(txtMobileNo.Text))
                {
                    string ErsEnable   = LicDf.FetchLic("ER");
                    string AlertEnable = LicDf.FetchLic("ES");

                    if ((ErsEnable == "1") && (AlertEnable == "1")) { e.IsValid = false; }
                }
                else
                {
                    if (!IsNumeric(txtMobileNo.Text))
                    {
                        CtrlCs.ValidMsg(this, ref cdvMobileNo, true, General.Msg("Please enter correct MobileNo", "رقم الجوال غير صحيح"));
                        e.IsValid = false;
                    }
                }
                
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool IsNumeric(string strVal)
    {
        try
        {
            double doub = double.Parse(strVal);
            return true;
        }
        catch { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void EmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmpID))
            {
                if (!string.IsNullOrEmpty(txtEmpID.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvEmpID, true, General.Msg("Entered Employee ID does not exist already,Please enter another ID", "رقم الموظف غير موجود,أدخل رقما آخر"));
                    if (!GenCs.isEmpID(txtEmpID.Text)) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UsrName_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND UsrName != @P2"; }

            if (source.Equals(cvUsrName))
            {
                if (!string.IsNullOrEmpty(txtUsername.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvUsrName, true, General.Msg("User Name already exists", "اسم المستخدم موجود مسبقا"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM AppUser WHERE UsrName = @P1 AND ISNULL(UsrDeleted,0) = 0 " + UQ, new string[] { txtUsername.Text, txtUsername.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ADUser_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND UsrName != @P2"; }


            if (source.Equals(cvADUser))
            {
                if (!string.IsNullOrEmpty(txtUsrADUser.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvADUser, true, General.Msg("Active Directory Name already exists", "اسم مستخدم Active Directory موجود مسبقا"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM AppUser WHERE UsrADUser = @P1 AND ISNULL(UsrDeleted,0) = 0 " + UQ, new string[] { txtUsrADUser.Text, txtUsername.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Acting

    protected void ActPopulateUI(string User)
    {
        UIActClear();

        DataTable DT = DBCs.FetchData(" SELECT * FROM AppUser WHERE UsrName = @P1 ", new string[] { User });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            txtActID.Text = User;
            txtActLoginName.Text = DT.Rows[0]["UsrActUserName"].ToString();
            if (DT.Rows[0]["UsrActPwd"] != DBNull.Value) { txtActPassword.Attributes["value"] = CryptorEngine.Decrypt(DT.Rows[0]["UsrActPwd"].ToString(), true); }

            txtActEmpID.Text = DT.Rows[0]["UsrActEmpID"].ToString();
            txtActEmailID.Text = DT.Rows[0]["UsrActEMailID"].ToString();
            txtActADUser.Text = DT.Rows[0]["UsrADActUser"].ToString();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ActFillPropeties()
    {
        try
        {
            ProCs.UsrName = txtActID.Text;
            ProCs.UsrActingUser = true;
            ProCs.UsrActUserName = txtActLoginName.Text.Trim();
            ProCs.UsrActPwd = CryptorEngine.Encrypt(txtActPassword.Text.Trim(), true);
            if (!string.IsNullOrEmpty(txtActEmpID.Text))   { ProCs.UsrActEmpID   = txtActEmpID.Text.Trim(); }
            if (!string.IsNullOrEmpty(txtActEmailID.Text)) { ProCs.UsrActEMailID = txtActEmailID.Text; }
            if (!string.IsNullOrEmpty(txtActADUser.Text))  { ProCs.UsrADActUser  = txtActADUser.Text; }

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnActSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave2)) { return; }

            if (string.IsNullOrEmpty(txtActID.Text))
            {
                CtrlCs.ShowMsg(this, vsShowMsg2, cvShowMsg2, CtrlFun.TypeMsg.Validation, "vgShowMsg2", General.Msg("UsrName is empty", "اسم المستخدم فارغ"));
                return;
            }

            ActFillPropeties();
            SqlCs.AppUser_Update_Acting(ProCs);
            //CtrlCs.ShowSaveMsg2(this);
            UIActClear();
            DivPopup.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg2(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnActCancel_Click(object sender, EventArgs e)
    {
        UIActClear();
        DivPopup.Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnActADUser_Click(object sender, ImageClickEventArgs e)
    {
        txtActADUser.Text = "";

        UsersPro AD = ActiveDirectoryFun.FillAD(ActiveDirectoryFun.ADTypeEnum.USR, txtActEmpID.Text.Trim(), txtActEmpID.Text.Trim(), txtActEmailID.Text.Trim());
        if (!AD.ADValid)      { CtrlCs.ShowMsg(this, vsShowMsg2, cvShowMsg2, CtrlFun.TypeMsg.Validation, "vgShowMsg2", AD.ADMsgValidation); return; }
        if (!AD.ADValidation) { CtrlCs.ShowMsg(this, vsShowMsg2, cvShowMsg2, CtrlFun.TypeMsg.Validation, "vgShowMsg2", AD.ADMsgValidation); return; }

        txtActADUser.Text = ActiveDirectoryFun.GetAD(AD);
        if (string.IsNullOrEmpty(txtActADUser.Text)) { CtrlCs.ShowMsg(this, vsShowMsg2, cvShowMsg2, CtrlFun.TypeMsg.Validation, "vgShowMsg2", AD.ADMsgNotExists); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIActClear()
    {
        try
        {
            txtActID.Text = "";
            txtActLoginName.Text = "";
            txtActPassword.Text = "";
            txtActEmpID.Text = "";
            txtActEmailID.Text = "";
            txtActADUser.Text = "";
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Custom Validate Events

    protected void ActEmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvActEmpID))
            {
                if (string.IsNullOrEmpty(txtActEmpID.Text)) { e.IsValid = true; }
                if (!string.IsNullOrEmpty(txtActEmpID.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvActEmpID, true, General.Msg("Entered Employee ID does not exist already,Please enter another ID", "رقم الموظف غير موجود,أدخل رقما آخر"));

                    DataTable DT = DBCs.FetchData(" SELECT * FROM Employee WHERE EmpID = @P1 AND ISNULL(EmpDeleted,0) = 0 ", new string[] { txtActEmpID.Text });
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
    protected void ActEmail_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvActEmailID))
            {
                if (string.IsNullOrEmpty(txtActEmailID.Text))
                {
                    //int ErsEnable = Convert.ToInt32(LicDf.FetchLic("ER"));
                    //int AlertEnable = Convert.ToInt32(LicDf.FetchLic("ES"));

                    //if ((ErsEnable == 1) && (AlertEnable == 1)) { e.IsValid = false; } else { e.IsValid = true; }

                    e.IsValid = true;
                }
                else
                {
                    string str = Convert.ToString(txtActEmailID.Text);
                    if (str.IndexOf('@') < 0)
                    {
                        CtrlCs.ValidMsg(this, ref cvActEmailID, true, General.Msg("Please enter email in correct format", "البريد الالكتروني غير صحيح"));
                        e.IsValid = false;
                    }
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
    protected void ActADUser_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = " AND UsrName != @P2 ";

            if (source.Equals(cvActADUser))
            {
                if (!string.IsNullOrEmpty(txtActADUser.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvActADUser, true, General.Msg("Active Directory Name already exists", "اسم مستخدم Active Directory موجود مسبقا"));
                    DataTable DT = DBCs.FetchData(" SELECT * FROM AppUser WHERE ( UsrADUser = @P1 OR UsrADActUser = @P1 ) AND ISNULL(UsrDeleted,0) = 0 " + UQ, new string[] { cvActADUser.Text, txtActID.Text });
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
    protected void ShowMsg2_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
