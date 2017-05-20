using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using Elmah;

public partial class ManagersSelected : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _ValidationGroupName;
    public string ValidationGroupName { get { return _ValidationGroupName; } set { if (_ValidationGroupName != value) { _ValidationGroupName = value; } } }

    private int _LevelCount;
    public int LevelCount { get { return _LevelCount; } set { if (_LevelCount != value) { _LevelCount = value; } } }

    public DataTable EmpSelected { get { return (DataTable)ViewState["dtRight"]; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                FillList();
                /*** Common Code ************************************/

                FillLevelList(LevelCount);
                cvlstMgr.ValidationGroup = ValidationGroupName;
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
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT DISTINCT RTRIM(UsrName) AS UsrName  FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 "));
            if (!DBCs.IsNullOrEmpty(DT)) { CtrlCs.PopulateDDL(ddlMgr, DT, "UsrName", "UsrName", General.Msg("- Select Manager -","- اختر المدير-")); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillLevelList(int Level)
    {
        int Lcount = 1;
        if (Level != null) { Lcount = Level + 1; }

        for (int i = 1; i <= Lcount; i++)
        {
            ListItem l = new ListItem(i.ToString(), i.ToString());
            if (i <= MaxLevel()) { ddlLevel.Items.Add(l); }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool MgrChecK(string usr)
    {
        bool isFound = false;

        DataTable DT = DBCs.FetchData(" SELECT UsrName FROM AppUser WHERE UsrStatus = 'True' AND UsrName = @P1 ", new string[] { usr });
        if (!DBCs.IsNullOrEmpty(DT)) { isFound = true; }

        return isFound;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected int MaxLevel()
    {
        try
        {
            string Multi = LicDf.FetchLic("MB");
            if (Multi == "0") { return 2; }

            int Count = 1;
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT COUNT(DplID) LevelCount FROM DepLevel "));
            if (!DBCs.IsNullOrEmpty(DT)) { if (DT.Rows[0]["LevelCount"] != null) { Count = Convert.ToInt32(DT.Rows[0]["LevelCount"]); } }
            else { Count = 10; }

            return Count += 1;            
        }
        catch { return 2; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg(string pEnErr, string pArErr)
    {
        if (pgCs.DateType == "AR") { ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('" + pArErr + "')", true); }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('" + pEnErr + "')", true); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool MgrReapeat()
    {
        for (int i = 0; i < lstMgr.Items.Count; i++)
        {
            ListItem lsMgr = lstMgr.Items[i];
            string[] Mgrs  = lsMgr.Text.Split(',');

            if (Mgrs.Contains(ddlMgr.SelectedValue)) { return true; }
        }

        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlMgr.SelectedIndex <= 0)       { return; }
        if (!MgrChecK(ddlMgr.SelectedValue)) { ShowMsg("This user is inactive, you can not add", "هذا المستخدم غير فعال، لا يمكن إضافته"); return; }
        if (MgrReapeat())                    { return; }

        int SelectedLevel = Convert.ToInt16(ddlLevel.SelectedValue);
        int lsLevelCount  = lstMgr.Items.Count;

        if (SelectedLevel > lsLevelCount) 
        { 
            lstMgr.Items.Add(new ListItem(ddlMgr.SelectedValue)); 
            if (SelectedLevel +1 <= MaxLevel()) { ddlLevel.Items.Add(new ListItem((SelectedLevel +1).ToString())); }
        }
        else
        {
            ListItem lsMgr = lstMgr.Items[SelectedLevel-1];
            string[] Mgrs  = lsMgr.Text.Split(',');

            string NewMgr = lsMgr.Text + "," + ddlMgr.SelectedValue;
            lstMgr.Items.RemoveAt(SelectedLevel-1);         
            lstMgr.Items.Insert(SelectedLevel-1,new ListItem(NewMgr));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUp_Click(object sender, ImageClickEventArgs e)
    {
        if (lstMgr.SelectedIndex > 0)
        {
            int oldIndex = lstMgr.SelectedIndex;
            ListItem ls = new ListItem(lstMgr.SelectedValue);
            lstMgr.Items.RemoveAt(lstMgr.SelectedIndex);
            lstMgr.Items.Insert(oldIndex - 1, ls);
            lstMgr.SelectedIndex = oldIndex - 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDown_Click(object sender, ImageClickEventArgs e)
    {
        if (lstMgr.SelectedIndex > -1 && lstMgr.SelectedIndex < lstMgr.Items.Count - 1)
        {
            int oldIndex = lstMgr.SelectedIndex;
            ListItem ls = new ListItem(lstMgr.SelectedValue);
            lstMgr.Items.RemoveAt(lstMgr.SelectedIndex);
            lstMgr.Items.Insert(oldIndex + 1, ls);
            lstMgr.SelectedIndex = oldIndex + 1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnRemoveMgr_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlMgr.SelectedIndex <= 0)       { return; }

        int SelectedLevel = Convert.ToInt16(ddlLevel.SelectedValue);
        int lsLevelCount  = lstMgr.Items.Count;

        if (SelectedLevel > lsLevelCount) { }
        else
        {
            ListItem lsMgr = lstMgr.Items[SelectedLevel-1];
            string[] Mgrs  = lsMgr.Text.Split(',');

            if (!Mgrs.Contains(ddlMgr.SelectedValue)) { return; }
            else
            {
                 string NewMgr = "";
                int index = Array.IndexOf(Mgrs, ddlMgr.SelectedValue);
                for(int i = 0; i < Mgrs.Length; i++)
                {
                    if (i != index) { if (string.IsNullOrEmpty(NewMgr)) { NewMgr = Mgrs[i]; } else {  NewMgr += ',' + Mgrs[i];}  }
                }

                if (string.IsNullOrEmpty(NewMgr))
                {
                    lstMgr.Items.RemoveAt(SelectedLevel-1);
                    ddlLevel.Items.RemoveAt(ddlLevel.Items.Count - 1);
                }
                else
                {
                    lstMgr.Items.RemoveAt(SelectedLevel-1);         
                    lstMgr.Items.Insert(SelectedLevel-1,new ListItem(NewMgr));
                }
                
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnRemoveLevel_Click(object sender, ImageClickEventArgs e)
    {
        if (lstMgr.SelectedIndex > -1) 
        { 
            lstMgr.Items.RemoveAt(lstMgr.SelectedIndex);
            ddlLevel.Items.RemoveAt(ddlLevel.Items.Count - 1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Clear()
    {
        ddlMgr.SelectedIndex = -1;
        ddlLevel.Items.Clear();
        FillLevelList(0);
        lstMgr.Items.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Enabled(bool status)
    {
        ddlMgr.Enabled         = status;
        ddlLevel.Enabled       = status;
        btnAdd.Enabled         = status;
        btnUp.Enabled          = status;
        btnDown.Enabled        = status;
        btnRemoveMgr.Enabled   = status;
        btnRemoveLevel.Enabled = status;
        lstMgr.Enabled         = status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setLevel(int Level) { FillLevelList(Level); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setMgr(DataTable Mgrdt) 
    {
        if (DBCs.IsNullOrEmpty(Mgrdt)) { return; }
        foreach (DataRow MgrDR in Mgrdt.Rows) { lstMgr.Items.Add(new ListItem(MgrDR["EalMgrID"].ToString())); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int getLevel() { return lstMgr.Items.Count; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string[] getMgr() 
    {
        if (lstMgr.Items.Count <= 0) { return null; }
        int lCount = lstMgr.Items.Count;

        string[] Mgrs = new string[lstMgr.Items.Count];

        for (int i = 0; i < Mgrs.Length; i++) { Mgrs[i] = (lstMgr.Items[i]).Text; }

        return Mgrs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Custom Validate Events

    protected void lstMgr_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvlstMgr)) { if (lstMgr.Items.Count == 0 ) { e.IsValid = false; } else { e.IsValid = true; } }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}