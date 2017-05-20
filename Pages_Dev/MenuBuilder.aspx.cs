using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Elmah;

public partial class MenuBuilder : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    MenuPro ProCs = new MenuPro();
    MenuSql SqlCs = new MenuSql();
    
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            //pgCs.FillSession(); 
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ //pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ //ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("10000");
                UIEnabled(false,false,true);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FillTree();
                TreeView1.Enabled = true;
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
            DataTable MDT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuParentID = 0"));
            if (!DBCs.IsNullOrEmpty(MDT)) { CtrlCs.PopulateDDL(ddlMenuItemParent, MDT, "MnuTextEn", "MnuID", General.Msg("-Select Parent-","-Select Parent-")); }
                
            DataTable CDT = DBCs.FetchData(new SqlCommand("SELECT DISTINCT MnuType FROM Menu WHERE MnuType NOT IN ('Command','ReportCommand')"));
            if (!DBCs.IsNullOrEmpty(CDT)) { CtrlCs.PopulateDDL(ddlMnuType, CDT, "MnuType", "MnuType", General.Msg("-Select Type-","-Select Type-")); }
                
            DataTable RDT = DBCs.FetchData(new SqlCommand("SELECT * FROM ReportGroup"));
            if (!DBCs.IsNullOrEmpty(RDT)) { CtrlCs.PopulateDDL(ddlRgpID, RDT, "RgpEnName", "RgpID", General.Msg("-Select Report Group-","-Select Report Group-")); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillTree()
    {
        try
        {
            DataSet ds = new DataSet();
            string sql = "SELECT MnuNumber, MnuID, MnuPermissionID, MnuTextEn as MnuText, MnuDescription, MnuParentID, MnuVisible FROM Menu WHERE MnuType NOT IN ('Command','ReportCommand') order by MnuOrder";
            ds = (DataSet)CtrlCs.FetchMenuDataset(sql);
            xmlDataSource1.Data = ds.GetXml();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus,bool pStatus2,bool pTreeStatus)
    {
        txtMenuItemText.Enabled = pStatus;
        txtMenuItemTextArabic.Enabled = pStatus;
        ddlMenuItemParent.Enabled = pStatus;
        txtMenuItemToolTip.Enabled = pStatus;
        txtMenuItemToolTipArabic.Enabled = pStatus;
        txtMenuItemServer.Enabled = pStatus;
        txtMenuItemURL.Enabled = pStatus;
        ddlItemtype.Enabled = pStatus;
        ddlMnuType.Enabled = pStatus;
        chkVisible.Enabled = pStatus;

        ddlRgpID.Enabled = pStatus2;
        cblPageCommands.Enabled = pStatus2;
        cblReportCommands.Enabled = pStatus2;

        TreeView1.Enabled = pTreeStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            if (!string.IsNullOrEmpty(hdnMenuID.Value)) { ProCs.MnuNumber = hdnMenuID.Value; }
            ProCs.MnuTextEn = txtMenuItemText.Text;
            ProCs.MnuTextAr = txtMenuItemTextArabic.Text;
            ProCs.MnuDescription = txtMenuItemToolTip.Text;
            ProCs.MnuArabicDescription = txtMenuItemToolTipArabic.Text;
            if (ddlMenuItemParent.SelectedIndex > 0)
            {
                ProCs.MnuParentID = ddlMenuItemParent.SelectedValue;
                if (ddlMnuType.SelectedIndex > 0) { ProCs.MnuType = ddlMnuType.SelectedValue; } else { ProCs.MnuType = "SubMenu"; }
            }
            else
            {
                ProCs.MnuParentID = "0";
                if (ddlMnuType.SelectedIndex > 0) { ProCs.MnuType = ddlMnuType.SelectedValue; } else { ProCs.MnuType = "Menu"; }
            }

            if (ddlRgpID.SelectedIndex > 0) { ProCs.RgpID = ddlRgpID.SelectedValue; }
            ProCs.MnuServer = txtMenuItemServer.Text;
            ProCs.MnuURL = txtMenuItemURL.Text;
            if (chkVisible.Checked) { ProCs.MnuVisible = "True"; } else { ProCs.MnuVisible = "False"; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtMenuItemText.Text = "";
        txtMenuItemTextArabic.Text = "";
        txtMenuItemToolTip.Text = "";
        txtMenuItemToolTipArabic.Text = "";
        txtMenuItemURL.Text = "";
        ddlMenuItemParent.ClearSelection();
        ddlMnuType.ClearSelection();
        ddlRgpID.ClearSelection();
        txtMenuItemServer.Text = "./";
        chkVisible.Checked = false;
        cblPageCommands.ClearSelection();
        cblReportCommands.ClearSelection();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMenuItemParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblPageCommands.ClearSelection();
        ddlRgpID.SelectedIndex = -1;
        ddlRgpID.Enabled = false;
        if (ddlMenuItemParent.SelectedIndex > 0) 
        { 
            cblPageCommands.Enabled = true;
            if (ddlMenuItemParent.SelectedItem.Text == "Reports") { ddlRgpID.Enabled = true; }
        } 
        else { cblPageCommands.Enabled = false; }
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRgpID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMenuItemParent.SelectedIndex > 0)
        {
            if (ddlRgpID.SelectedIndex > 0) { cblPageCommands.Enabled = false; cblReportCommands.Enabled = true; cblPageCommands.ClearSelection(); } 
            else { cblPageCommands.Enabled = true; cblReportCommands.Enabled = false; cblReportCommands.ClearSelection(); }
        }
        else
        {
            cblPageCommands.Enabled   = false;
            cblReportCommands.Enabled = false;
        }
        UpdatePanel1.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string GetCommandType() { if (ddlRgpID.SelectedIndex > 0) { return "ReportCommand"; } else { return "Command"; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected CheckBoxList GetCommandList()
    {
        if (ddlRgpID.SelectedIndex > 0) { return Master.FindControl("ContentPlaceHolder1").FindControl("cblReportCommands") as CheckBoxList; }
        else { return  Master.FindControl("ContentPlaceHolder1").FindControl("cblPageCommands") as CheckBoxList; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string searchPar)
    {
        try
        {
            UIClear();
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuID = '" + searchPar + "'"));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                BtnStatus("11100");
                hdnMenuID.Value = DT.Rows[0]["MnuID"].ToString();
                txtMenuItemText.Text = DT.Rows[0]["MnuTextEn"].ToString();
                txtMenuItemTextArabic.Text = DT.Rows[0]["MnuTextAr"].ToString();
                txtMenuItemToolTip.Text = DT.Rows[0]["MnuDescription"].ToString();
                txtMenuItemToolTipArabic.Text = DT.Rows[0]["MnuArabicDescription"].ToString();
                txtMenuItemURL.Text = DT.Rows[0]["MnuURL"].ToString();
                txtMenuItemServer.Text = DT.Rows[0]["MnuServer"].ToString();
                if (DT.Rows[0]["MnuVisible"].ToString() == "True") { chkVisible.Checked = true; } else { chkVisible.Checked = false; }
                ddlMenuItemParent.SelectedIndex = ddlMenuItemParent.Items.IndexOf(ddlMenuItemParent.Items.FindByValue(DT.Rows[0]["MnuParentID"].ToString()));

                ddlMnuType.SelectedIndex = ddlMnuType.Items.IndexOf(ddlMnuType.Items.FindByValue(DT.Rows[0]["MnuType"].ToString()));
                ddlRgpID.SelectedIndex = ddlRgpID.Items.IndexOf(ddlRgpID.Items.FindByValue(DT.Rows[0]["RgpID"].ToString()));

                DataTable MDT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuType = '" + GetCommandType() +"' AND MnuParentID = '" + searchPar + "'"));
                if (!DBCs.IsNullOrEmpty(MDT)) 
                {
                    CheckBoxList _List = GetCommandList();
                    for (int i = 0; i < MDT.Rows.Count; i++)
                    {
                        int index = _List.Items.IndexOf(_List.Items.FindByValue(MDT.Rows[i]["MnuTextEn"].ToString()));
                        if (index > -1) { _List.Items[index].Selected = true; } //else { _List.Items[index].Selected = false; }
                    }
                }
            }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
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
        UIClear();
        ViewState["CommandName"] = "ADD";
        UIEnabled(true,false,false);
        BtnStatus("00011");
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true,false,false);

        if (ddlMenuItemParent.SelectedIndex > 0)
        {
            if (ddlMenuItemParent.SelectedItem.Text == "Reports") { ddlRgpID.Enabled = true; }
            if (ddlRgpID.SelectedIndex > 0) { cblReportCommands.Enabled = true; } else { cblPageCommands.Enabled = true; }
        }

        BtnStatus("00011");
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!Page.IsValid) { return; }
            //if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            CheckBoxList _List = GetCommandList();
            string CommandType = GetCommandType();

            FillPropeties();

            if (commandName == "ADD")
            {
                int menuID = SqlCs.Menu_Insert(ProCs);
                if (menuID > 0 && ddlMenuItemParent.SelectedIndex > 0)
                {
                    for (int i = 0; i < _List.Items.Count; i++)
                    {
                        if (_List.Items[i].Selected == true)
                        {
                            string[] name = _List.Items[i].Text.Split('-');
                            int CommandID = SqlCs.Menu_Insert_Command(name[0], name[1], menuID.ToString(), chkVisible.Checked.ToString(),CommandType,i.ToString(),"");
                        }
                    }
                }
            }
            else if (commandName == "EDIT")
            {
                bool boolmenuID = SqlCs.Menu_Update(ProCs);
                int CommandID = 0;

                if (boolmenuID && ddlMenuItemParent.SelectedIndex > 0)
                {
                    for (int i = 0; i < _List.Items.Count; i++)
                    {
                        string dd = hdnMenuID.Value;
                                
                        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuType = '" + CommandType + "' AND MnuTextEn = '" + _List.Items[i].Value + "' AND MnuParentID = " + hdnMenuID.Value + ""));
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            if (_List.Items[i].Selected)
                            {
                                boolmenuID = SqlCs.Menu_Update_Command(DT.Rows[0]["MnuNumber"].ToString(), chkVisible.Checked.ToString(),i.ToString(),"");
                            }
                            else
                            {
                                boolmenuID = SqlCs.Menu_Delete_Command(DT.Rows[0]["MnuNumber"].ToString(),"");
                            }
                        }
                        else
                        {
                            if (cblPageCommands.Items[i].Selected)
                            {
                                string[] name = _List.Items[i].Text.Split('-');
                                CommandID = SqlCs.Menu_Insert_Command(name[0], name[1], hdnMenuID.Value, chkVisible.Checked.ToString(), CommandType,i.ToString(),"");
                            }
                        }
                    }
                }
            }

            UIClear();
            BtnStatus("10000");
            UIEnabled(false,false,true);
            //UpdatePanel1.Update();
            //UpdatePanel2.Update();
            CtrlCs.ShowSaveMsg(this);

            Response.Redirect(@"~/Pages_Dev/MenuBuilder.aspx");
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
        BtnStatus("10000");
        UIEnabled(false,false,true);
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(hdnMenuID.Value) != string.Empty)
            {
                bool boolmenuID = SqlCs.Menu_Delete(hdnMenuID.Value,"");

                UpdatePanel1.Update();
                UpdatePanel2.Update();
                BtnStatus("10000");
                CtrlCs.ShowDelMsg(this, true);
                
                //Response.Redirect(@"~/Pages_Dev/MenuBuilder.aspx");
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
    protected void BtnStatus(string Status) //string pBtn = [Modify,Save,Cancel]
    {
        //Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnDelete.Enabled = GenCs.FindStatus(Status[2]);
        btnSave.Enabled   = GenCs.FindStatus(Status[3]);
        btnCancel.Enabled = GenCs.FindStatus(Status[4]);
        
        //if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        //if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region TreeView Events

     protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        ddlMenuItemParent.Items.Clear();
        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuParentID = 0"));
        if (!DBCs.IsNullOrEmpty(DT)) { CtrlCs.PopulateDDL(ddlMenuItemParent, DT, "MnuTextEn", "MnuID", "-Select Parent-"); }
        
        PopulateUI(TreeView1.SelectedNode.Value);
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void TreeView1_DataBound(object sender, EventArgs e)
    {
        TreeView1.CollapseAll();
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
