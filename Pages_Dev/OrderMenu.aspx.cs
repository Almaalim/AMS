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

public partial class OrderMenu : BasePage
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
                BtnStatus("100");
                UIEnabled(false);
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
            DataTable MDT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE  MnuType IN ('Menu','ERSMenu','ReportsMenu') ORDER BY MnuOrder"));
            if (!DBCs.IsNullOrEmpty(MDT)) 
            { 
                CtrlCs.PopulateDDL(ddlMenu, MDT, "MnuTextEn", "MnuID", General.Msg("-Select Menu-","-Select Menu-")); 
                FillOrderList("0");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillOrderList(string pID)
    {
        lstOrderMenu.Items.Clear();
        DataTable MDT = DBCs.FetchData(new SqlCommand("SELECT * FROM Menu WHERE MnuType !='Command' AND MnuParentID= " + pID + " ORDER BY MnuOrder"));
        if (!DBCs.IsNullOrEmpty(MDT)) 
        {
            for (int i = 0; i < MDT.Rows.Count; i++)
            {
                DataRow menuRows = (DataRow)MDT.Rows[i];
                ListItem _listItem = new ListItem();
                _listItem.Text = menuRows["MnuTextEn"].ToString();
                _listItem.Value = menuRows["MnuID"].ToString();
                lstOrderMenu.Items.Add(_listItem);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillTree()
    {
        try
        {
            DataSet ds = new DataSet();
            string sql = "SELECT MnuNumber, MnuID, MnuPermissionID, MnuTextEn as MnuText, MnuDescription, MnuParentID, MnuVisible FROM Menu WHERE MnuType !='Command' order by MnuOrder";
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

    public void UIEnabled(bool pStatus)
    {
        ddlMenu.Enabled      = pStatus;
        lstOrderMenu.Enabled = pStatus;
        btnUP.Enabled        = pStatus;
        btnDOWN.Enabled      = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            //if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.BrcID = txtID.Text; }

            //ProCs.BrcNameAr  = txtNameAr.Text;
            //ProCs.BrcNameEn  = txtNameEn.Text;
            //ProCs.BrcAddress = txtBranchAddress.Text;
            //ProCs.BrcCity    = txtBranchCity.Text;
            //ProCs.BrcCountry = txtBranchCountry.Text;
            //ProCs.BrcPOBox   = txtBranchPoBox.Text;
            //ProCs.BrcTelNo   = txtBranchTell.Text;
            //ProCs.BrcEmail   = txtBranchEmail.Text;
            //if (ddlBranchManagerID.SelectedIndex > 0) { ProCs.UsrName = ddlBranchManagerID.Text; }
            //ProCs.BrcStatus = chkStatus.Checked;
        
            //ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        ddlMenu.SelectedIndex = 0;
        FillOrderList("0");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOrderList(ddlMenu.SelectedValue);
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUP_Click(object sender, EventArgs e)
    {
        if (lstOrderMenu.SelectedIndex > 0)
        {
            int oldIndex = lstOrderMenu.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstOrderMenu.SelectedItem.Text;
            ls.Value = lstOrderMenu.SelectedValue.ToString();
            lstOrderMenu.Items.RemoveAt(lstOrderMenu.SelectedIndex);
            lstOrderMenu.Items.Insert(oldIndex - 1, ls);
            lstOrderMenu.SelectedIndex = oldIndex - 1;
            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDOWN_Click(object sender, EventArgs e)
    {
        if (lstOrderMenu.SelectedIndex > -1 && lstOrderMenu.SelectedIndex < lstOrderMenu.Items.Count - 1)
        {
            int oldIndex = lstOrderMenu.SelectedIndex;
            ListItem ls = new ListItem();
            ls.Text = lstOrderMenu.SelectedItem.Text;
            ls.Value = lstOrderMenu.SelectedValue.ToString();
            lstOrderMenu.Items.RemoveAt(lstOrderMenu.SelectedIndex);
            lstOrderMenu.Items.Insert(oldIndex + 1, ls);
            lstOrderMenu.SelectedIndex = oldIndex + 1;
            UpdatePanel1.Update();
            UpdatePanel2.Update();
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
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        BtnStatus("011");
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

            //FillPropeties();

            if (commandName == "EDIT")
            {
                string MenuIDs  = "";
                string OrderIDs = "";
                
                if (lstOrderMenu.Items.Count > 0)
                {
                    for (int i = 0; i < lstOrderMenu.Items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lstOrderMenu.Items[i].Value))
                        {
                            if (string.IsNullOrEmpty(MenuIDs))  { MenuIDs  = lstOrderMenu.Items[i].Value; } else { MenuIDs  += "," + lstOrderMenu.Items[i].Value; }
                            if (string.IsNullOrEmpty(OrderIDs)) { OrderIDs = i.ToString(); }                else { OrderIDs += "," + i.ToString(); }
                        }
                    }

                    SqlCs.Menu_Update_Order(MenuIDs, OrderIDs, "");
                }
            }

            UIClear();
            BtnStatus("100");
            UIEnabled(false);
            //UpdatePanel1.Update();
            //UpdatePanel2.Update();
            CtrlCs.ShowSaveMsg(this);
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
        BtnStatus("100");
        UIEnabled(false);
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [Modify,Save,Cancel]
    {
        //Hashtable Permht = (Hashtable)ViewState["ht"];
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
        
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

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void TreeView1_DataBound(object sender, EventArgs e) { TreeView1.CollapseAll(); }

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
