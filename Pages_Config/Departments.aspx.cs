using System;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;

public partial class Departments : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DepartmentPro ProCs = new DepartmentPro();
    DepartmentSql SqlCs = new DepartmentSql();
    UsersSql UsrSqlCs = new UsersSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    protected string Variable_codebehind;
    string MainQuery = " SELECT * FROM Department WHERE ISNULL(DepDeleted,0) = 0 ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.Animation(ref AnimationExtenderShow1, ref AnimationExtenderClose1, ref lnkShow1, "1");
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("00000");
                UIEnabled(false, true);
                UILang();
                FillList();

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
                Variable_codebehind = General.Msg("Oops, nothing found!","لا يوجد نتائج");
                ViewState["Action"] = "M";
                FillTree("-1");
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
            FillDepParent();
            CtrlCs.FillBranchList(ref ddlBrcParentName, rfvBrcParentName, false);
            CtrlCs.FillMgrsList2(ref ddlDepManagerID, rfDepMng, false);

            //CtrlCs.FillMgrsChosenList(ref ddlDepManagerID, rfDepMng, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillDepParent() { CtrlCs.FillDepartmentList(ref ddlDepParentName, null, true); }
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
            spnManagerName.Visible = true;
        }

        if (pgCs.LangAr)
        {
            spnNameAr.Visible = true;
            spnManagerName.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus1, bool pStatus2)
    {
        txtID.Enabled = txtID.Visible = false;
        txtLevel.Enabled = txtLevel.Visible = false;
        
        //ddlDepParentName.Enabled = pStatus1;
        ddlBrcParentName.Enabled = pStatus2;
        trvDept.Enabled = true;//pStatus2;
        txtNameAr.Enabled = pStatus1;
        txtNameEn.Enabled = pStatus1;
        txtDepartmentDesc.Enabled = pStatus1;
        chkStatus.Enabled = pStatus1;
        ddlDepManagerID.Enabled = pStatus1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties(string pAction)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.DepID = txtID.Text; }
        
        ProCs.DepNameAr = txtNameAr.Text.Trim();
        ProCs.DepNameEn = txtNameEn.Text.Trim();
        ProCs.DepDesc   = txtDepartmentDesc.Text;
        ProCs.DepStatus = chkStatus.Checked;
        if (ddlDepManagerID.SelectedIndex  > 0) { ProCs.UsrName     = ddlDepManagerID.SelectedValue; }
        if (ddlDepParentName.SelectedIndex > 0) { ProCs.DepParentID = ddlDepParentName.SelectedValue; } else { ProCs.DepParentID = "0"; }
        if (ddlBrcParentName.SelectedIndex > 0) { ProCs.BrcID       = ddlBrcParentName.SelectedValue; } else { ProCs.BrcID = "0"; }
        
        if (pAction == "ADD") 
        { 
            if (ddlDepParentName.SelectedIndex > 0) { ProCs.DepLevel = (trvDept.SelectedNode.Depth + 1).ToString(); } else { ProCs.DepLevel = "0"; } 
        }
        else if (pAction == "EDIT") { ProCs.DepLevel = txtLevel.Text; }

        ProCs.TransactionBy = pgCs.LoginID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear(bool pStatus)
    {
        ViewState["CommandName"] = "";
        
        txtNameAr.Text = "";
        txtNameEn.Text = "";
        txtDepartmentDesc.Text = "";
        txtLevel.Text = "";
        chkStatus.Checked = false;
        //ddlBrcParentName.SelectedIndex = -1;
        ddlDepManagerID.SelectedIndex = -1;
        if (pStatus) { ddlDepParentName.SelectedIndex = -1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateDepList_Add(txtID.Text, ddlDepManagerID.Text);

            UIClear(true);
            ViewState["Action"] = "M";
            UIEnabled(false, true);
            pnlMyDialogBox.Visible = false;
            ControlVis(true);
            BtnStatus("10000");
            
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
    protected void btnNo_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateDepList_Delete(txtID.Text, ViewState["PreviousMgr"].ToString());
            UpdateDepList_Add(txtID.Text, ddlDepManagerID.Text);
            
            UIClear(true);
            ViewState["Action"] = "M";
            UIEnabled(false, true);
            pnlMyDialogBox.Visible = false;
            ControlVis(true);
            BtnStatus("10000");
            
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
    protected void ControlVis(bool value)
    {
        lblNameAr.Visible = value;
        txtNameAr.Visible = value;
        lblNameEn.Visible = value;
        txtNameEn.Visible = value;

        lblDepParent.Visible = value;
        ddlDepParentName.Visible = value;
        lblDepMangerID.Visible = value;
        ddlDepManagerID.Visible = value;
        lblBranchtID.Visible = value;
        ddlBrcParentName.Visible = value;

        lblDepartmentDesc.Visible = value;
        txtDepartmentDesc.Visible = value;
        chkStatus.Visible = false; // value;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlBrcParentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrcParentName.SelectedIndex > 0)
        {
            FillTree(ddlBrcParentName.SelectedValue);

            if (ViewState["Action"].ToString() == "M")
            {
                UIClear(true);
                BtnStatus("10000");
            }
            else if (ViewState["Action"].ToString() == "A")
            {
                //UIClear();
                ddlDepParentName.SelectedIndex = -1;
            }
        }
        else
        {
            FillTree("-1");
            UIClear(true);
            ViewState["Action"] = "M";
            UIEnabled(false, true);
            BtnStatus("00000");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UpdateDepList_Add(string DepID, string ManagerID)
    {
        try
        {
            string DepList    = "";
            string encDepList = "";

            DataTable DT = DBCs.FetchData(" SELECT UsrDepartments FROM AppUser WHERE UsrName = @P1 ", new string[] { ManagerID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) 
                {
                    DepList = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(), true);
                }

                if (string.IsNullOrEmpty(DepList)) { DepList = DepID; }
                else
                {
                    string[] Deps = DepList.Split(',');
                    if (!Deps.Contains(DepID)) { DepList += ',' + DepID; }
                }
            
                encDepList = CryptorEngine.Encrypt(DepList, true);
                UsrSqlCs.AppUser_Update_DepList(ManagerID, encDepList, pgCs.LoginID);
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
    protected void UpdateDepList_Delete(string DepID, string ManagerID)
    {
        try
        {
            string DepList    = "";
            string encDepList = "";
            string newDepList = "";

            DataTable DT = DBCs.FetchData(" SELECT UsrDepartments FROM AppUser WHERE UsrName = @P1 ", new string[] { ManagerID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) 
                {
                    DepList = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(), true);
                }

                if (!string.IsNullOrEmpty(DepList))
                {
                    string[] Deps = DepList.Split(',');
                    
                    for (int i = 0; i < Deps.Length; i++)
                    {
                        if (Deps[i] != DepID) { if (string.IsNullOrEmpty(newDepList)) { newDepList += Deps[i]; } else { newDepList += "," + Deps[i]; } }
                    }
                }
            
                encDepList = CryptorEngine.Encrypt(newDepList, true);
                UsrSqlCs.AppUser_Update_DepList(ManagerID, encDepList, pgCs.LoginID);
            }
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
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            UIClear(false);
            if (!FindDemoCount()) { return; }
            
            //if (trvDept.SelectedNode != null) { trvDept.SelectedNode.Selected = false; }
            UIEnabled(true, true);
            BtnStatus("00110");
            ViewState["CommandName"] = "ADD";
            ViewState["Action"] = "A";
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
            if (!FindDemoCount()) { return; }

            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties(ViewState["CommandName"].ToString());

            if (ViewState["CommandName"].ToString() == "ADD")
            {
                int DepID = SqlCs.Insert(ProCs);
                if (ddlDepManagerID.SelectedIndex > 0) { UpdateDepList_Add(DepID.ToString(), ddlDepManagerID.Text); }          
                FillDepParent();          
            }
            else if (ViewState["CommandName"].ToString() == "EDIT")
            {
                SqlCs.Update(ProCs);

                if (ViewState["PreviousMgr"].ToString() != ddlDepManagerID.Text)
                {
                    pnlMyDialogBox.Visible = true;
                    ControlVis(false);
                    BtnStatus("00000");
                    return;
                }
            }

            UIClear(true);
            ViewState["Action"] = "M";
            UIEnabled(false, true);
            BtnStatus("00000");
            FillTree("-1");
            ddlBrcParentName.SelectedIndex = -1;
            
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
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            UIEnabled(true, false);
            BtnStatus("00110");
            ViewState["CommandName"] = "EDIT";
            ViewState["Action"] = "M";
            //Keep the value of previous manager and DepartmentName
            ViewState["PreviousMgr"] = ddlDepManagerID.Text;
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
        UIClear(true);
        UIEnabled(false, true);
        ViewState["Action"] = "M";
        FillTree("-1");
        ddlBrcParentName.SelectedIndex = -1;
        BtnStatus("00000");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            System.Text.StringBuilder QDel = new System.Text.StringBuilder();
            QDel.Append(" SELECT DepID From Employee WHERE ISNULL(EmpDeleted,0) = 0 AND DepID = @P1 ");
            //QDel.Append(" UNION ");
            //QDel.Append(" SELECT DepID From UsrDepRel WHERE ISNULL(UdrDeleted,0) = 0 AND DepID = @P1");

            DataTable DT = DBCs.FetchData(QDel.ToString(), new string[] { txtID.Text });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                CtrlCs.ShowDelMsg(this, false);
                return;
            }

            SqlCs.Delete(txtID.Text, pgCs.LoginID);

            CtrlCs.ShowDelMsg(this, true);

            UIClear(true);
            ViewState["Action"] = "M";
            UIEnabled(false, true);
            BtnStatus("00000");
            FillTree("-1");
            ddlBrcParentName.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel,del]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        btnDelete.Enabled = GenCs.FindStatus(Status[4]);

        if (Status[0] != '0') { btnAdd.Enabled    = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
        if (Status[4] != '0') { btnDelete.Enabled = Permht.ContainsKey("Delete"); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region TreeView Events

    protected void FillTree(string pBrcID)
    {
        DataSet ds = new DataSet();
        ds = CtrlCs.FillDepTreeDS(pBrcID, pgCs.Version);
        xdsDepartment.Data = ds.GetXml();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvDept_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            int rr = trvDept.SelectedNode.Depth;


            if (ViewState["Action"].ToString() == "M")
            {
                UIClear(true);
                PopulateUI(trvDept.SelectedNode.Value);
                BtnStatus("11001");
            }
            else if (ViewState["Action"].ToString() == "A")
            {
                bool isLevel = checkDepLevel(trvDept.SelectedNode.Depth);
                if (isLevel) { ddlDepParentName.SelectedIndex = ddlDepParentName.Items.IndexOf(ddlDepParentName.Items.FindByValue(trvDept.SelectedNode.Value)); }
                else
                {
                    ddlDepParentName.SelectedIndex = -1;
                    CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.Msg("You can not create a sub-Department under this Department because it exceeded the allowable limit for that", "لا يمكن إنشاء قسم فرعي تحت هذا القسم لتجاوزه الحد المسموح لذلك"));
                }
            }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool checkDepLevel(int pLevel)
    {
        try
        {
            string Multi = LicDf.FetchLic("MB");
            
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT COUNT(DplID) LevelCount FROM DepLevel"));
            int Count = 0;
            if (!DBCs.IsNullOrEmpty(DT)) { if (DT.Rows[0]["LevelCount"] != null) { Count = Convert.ToInt32(DT.Rows[0]["LevelCount"]); } }
            if (Multi == "0") { Count = 1; }

            if (pLevel + 1 >= Count) { return false; } else { return true; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvDept_DataBound(object sender, EventArgs e)
    {
        //trvDept.CollapseAll();
        trvDept_TreeNodeDataBound(null, null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvDept_TreeNodeDataBound(object sender, TreeNodeEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pDepID)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT DepID,DepParentID,BrcID,DepNameAr,DepNameEn,DepDesc,UsrName,DepStatus,DepLevel FROM Department WHERE ISNULL(DepDeleted,0) = 0 AND DepID = @P1 ", new string[] { pDepID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                txtID.Text = DT.Rows[0]["DepID"].ToString();
                txtNameAr.Text = DT.Rows[0]["DepNameAr"].ToString();
                txtNameEn.Text = DT.Rows[0]["DepNameEn"].ToString();
                txtDepartmentDesc.Text = DT.Rows[0]["DepDesc"].ToString();
                txtLevel.Text = DT.Rows[0]["DepLevel"].ToString();
                ddlDepManagerID.SelectedIndex = ddlDepManagerID.Items.IndexOf(ddlDepManagerID.Items.FindByValue(DT.Rows[0]["UsrName"].ToString()));
                ddlDepParentName.SelectedIndex = ddlDepParentName.Items.IndexOf(ddlDepParentName.Items.FindByValue(DT.Rows[0]["DepParentID"].ToString()));

                if (DT.Rows[0]["DepStatus"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DT.Rows[0]["DepStatus"]); }

                //ddlBrcParentName.SelectedIndex = ddlBrcParentName.Items.IndexOf(ddlBrcParentName.Items.FindByValue(DT.Rows[0]["BrcID"].ToString()));          
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
            string parent = (ddlDepParentName.SelectedIndex > 0) ? ddlDepParentName.SelectedValue : "0";
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND DepID != @P4 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameEn.Text)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(txtNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered Department English Name exist already,Please enter another name", "إسم القسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM Department WHERE DepNameEn = @P1 AND BrcID = @P2 AND DepParentID = @P3 " + UQ, new string[] { txtNameEn.Text,ddlBrcParentName.SelectedValue, parent, txtID.Text });
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
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Department Arabic Name exist already,Please enter another name", "إسم القسم بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM Department WHERE DepNameAr = @P1 AND BrcID = @P2 AND DepParentID = @P3 " + UQ, new string[] { txtNameAr.Text,ddlBrcParentName.SelectedValue, parent, txtID.Text });
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
    protected bool FindDemoCount()
    {
        bool Found = false;

        try
        {
            if (pgCs.Version != "DEMO") { Found = true; }
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT COUNT(DepID) DemoCount FROM Department WHERE ISNULL(DepDeleted,0) = 0 HAVING COUNT(DepID) <= 5 "));
            if (!DBCs.IsNullOrEmpty(DT)) { Found = true; }
        }
        catch {  }

        if (!Found)
        {
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Warning, General.Msg("Can not add more than five Department, this version is Demo", "لا يمكن إضافة أكثر من خمسة أقسام، هذه النسخة للعرض"));
        }

        return Found;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}