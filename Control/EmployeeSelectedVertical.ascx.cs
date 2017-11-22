using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using Elmah;
using System.Data.SqlClient;

public partial class EmployeeSelectedVertical : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    DataTable dtLeft;
    DataTable dtRight;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _ValidationGroupName;
    public string ValidationGroupName
    {
        get { return _ValidationGroupName; }
        set { if (_ValidationGroupName != value) { _ValidationGroupName = value; } }
    }

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
            
            btnSelectEmp.ImageUrl   = General.Msg("images/Control_Images/next.png","images/Control_Images/back.png");
            btnDeSelectEmp.ImageUrl = General.Msg("images/Control_Images/back.png","images/Control_Images/next.png");

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                CtrlCs.PopulateDepartmentList(ref ddlDepartment, pgCs.DepList, pgCs.Version);
                
                cblEmpSelect.Items.Clear();
                cblEmpSelected.Items.Clear();
                cvSelectEmployees.ValidationGroup = cvSearchByID.ValidationGroup = cvSelectByName.ValidationGroup = ValidationGroupName;

                divAllEmp.Visible = false;
                rdoSelectAll.Checked = true;
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
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT EtpID, EtpNameAr, EtpNameEn FROM EmploymentType WHERE ISNULL(EtpDeleted,0) = 0 "));
            if (!DBCs.IsNullOrEmpty(DT)) { CtrlCs.PopulateDDL(ddlEmpTypeID, DT, General.Msg("EtpNameEn","EtpNameAr"), "EtpID", General.Msg("ALL","الكل")); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlEmpTypeID_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdoSelectAll.Checked    = false;
        rdoSelectByID.Checked   = false;
        rdoSelectByName.Checked = false;
        rdoSelectDep.Checked    = false;

        Clear(false);
        txtSearchByID.Enabled   = false;  txtSearchByID.Text   = "";
        txtSearchByName.Enabled = false;  txtSearchByName.Text = "";

        string EmpCon = FindCon();
        Session["EmpConSelect"] = EmpCon;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                dtLeft = new DataTable();

                if (ViewState["dtLeft"] == null) { ViewState["dtLeft"] = EmptyDataTable(); }

                string GeneralCon = "";
                if (ViewState["Condition"] != null) { if (string.IsNullOrEmpty(ViewState["Condition"].ToString())) { GeneralCon = ""; } else { GeneralCon = ViewState["Condition"].ToString(); } }

                string IN = ( ViewState["dtEmpIds"] != null && !string.IsNullOrEmpty(ViewState["dtEmpIds"].ToString()) )  ? " AND E.EmpID NOT IN (" + ViewState["dtEmpIds"].ToString() + ")" : "";
                string lang = ( pgCs.Lang == "AR" ) ? "Ar" : "En";
                
                string EmpCon = FindCon();

                StringBuilder Q = new StringBuilder();
                Q.Append(" SELECT E.EmpID AS EmpID, E.EmpID + ' - ' + E.EmpName" + lang + " AS EmpName ");
                Q.Append(" FROM Employee E,Department D ");
                Q.Append(" WHERE E.DepID = D.DepID AND EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
                Q.Append(" AND D.DepID = " + ddlDepartment.SelectedValue.ToString() + "");
                Q.Append(" " + IN + " ");
                Q.Append(" " + GeneralCon + " ");
                Q.Append(" " + EmpCon + " ");

                dtLeft = DBCs.FetchData(new SqlCommand(Q.ToString()));
                FillEmpSelect();
            }
            else
            {
                cblEmpSelect.Items.Clear();
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillEmpSelect()
    {
        if (!DBCs.IsNullOrEmpty(dtLeft))
        {
            cblEmpSelect.DataSource = dtLeft;
            cblEmpSelect.DataTextField = "EmpName";
            cblEmpSelect.DataValueField = "EmpID";
            cblEmpSelect.DataBind();
            ViewState["dtLeft"] = (DataTable)dtLeft;
        }
        else
        {
            cblEmpSelect.Items.Clear();
            ViewState["dtLeft"] = (DataTable)dtLeft;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillEmpSelected()
    {
        if (!DBCs.IsNullOrEmpty(dtRight))
        {
            cblEmpSelected.DataSource = dtRight;
            cblEmpSelected.DataTextField = "EmpName";
            cblEmpSelected.DataValueField = "EmpID";
            cblEmpSelected.DataBind();

            string EmployeeSelecteID = "";
            for (int i = 0; i < dtRight.Rows.Count; i++)
            {
                if (i == 0) { EmployeeSelecteID += dtRight.Rows[i]["EmpID"].ToString(); } else { EmployeeSelecteID += "," + dtRight.Rows[i]["EmpID"].ToString(); }
            }
            ViewState["dtEmpIds"] = EmployeeSelecteID;
        }
        else
        {
            cblEmpSelected.Items.Clear();
            ViewState["dtEmpIds"] = null;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected DataTable EmptyDataTable()
    {
        DataTable Emptydt = new DataTable();
        Emptydt.Columns.Add(new DataColumn("EmpID", typeof(string)));
        Emptydt.Columns.Add(new DataColumn("EmpName", typeof(string)));
        return Emptydt;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAllEmpSelect_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < cblEmpSelect.Items.Count; i++) { cblEmpSelect.Items[i].Selected = chkAllEmpSelect.Checked; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAllEmpSelected_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < cblEmpSelected.Items.Count; i++) { cblEmpSelected.Items[i].Selected = chkAllEmpSelected.Checked; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSelectEmp_Click(object sender, EventArgs e)
    {
        try
        {
            dtRight = EmptyDataTable();
            dtLeft  = EmptyDataTable();
            if (ViewState["dtRight"] == null) { ViewState["dtRight"] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight"]; }
            if (ViewState["dtLeft"] == null)  { ViewState["dtLeft"]  = EmptyDataTable(); } else { dtLeft  = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < cblEmpSelect.Items.Count; i++)
            {
                if (cblEmpSelect.Items[i].Selected)
                {
                    DataRow dRow = dtRight.NewRow();
                    dRow["EmpID"]   = cblEmpSelect.Items[i].Value;
                    dRow["EmpName"] = cblEmpSelect.Items[i].Text;
                    dtRight.Rows.Add(dRow);
                    dtRight.AcceptChanges();
                    ViewState["dtRight"] = (DataTable)dtRight;

                    DataRow[] rows = dtLeft.Select("EmpID ='" + cblEmpSelect.Items[i].Value + "'");
                    rows.Count();
                    dtLeft.Rows.Remove(rows[0]);
                    dtLeft.AcceptChanges();
                    ViewState["dtLeft"] = (DataTable)dtLeft;
                }
            }

            FillEmpSelected();
            FillEmpSelect();

        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDeSelectEmp_Click(object sender, EventArgs e)
    {
        try
        {
            dtRight = EmptyDataTable();
            dtLeft = EmptyDataTable();
            if (ViewState["dtRight"] == null) { ViewState["dtRight"] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight"]; }
            if (ViewState["dtLeft"] == null) { ViewState["dtLeft"] = EmptyDataTable(); } else { dtLeft = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < cblEmpSelected.Items.Count; i++)
            {
                if (cblEmpSelected.Items[i].Selected)
                {
                    DataRow dRow = dtLeft.NewRow();
                    dRow["EmpID"] = cblEmpSelected.Items[i].Value;
                    dRow["EmpName"] = cblEmpSelected.Items[i].Text;

                    DataTable DT = DBCs.FetchData("SELECT DepID FROM Employee WHERE EmpID = @P1 ", new string[] { cblEmpSelected.Items[i].Value });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        if (ddlDepartment.SelectedValue == DT.Rows[0]["DepID"].ToString())
                        {
                            dtLeft.Rows.Add(dRow);
                            dtLeft.AcceptChanges();
                            ViewState["dtLeft"] = (DataTable)dtLeft;
                        }
                        DataRow[] rows = dtRight.Select("EmpID ='" + cblEmpSelected.Items[i].Value + "'");
                        dtRight.Rows.Remove(rows[0]);
                        dtRight.AcceptChanges();
                        ViewState["dtRight"] = (DataTable)dtRight;
                    }
                }
            }

            FillEmpSelected();
            FillEmpSelect();
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearAll()
    {
        rdoSelectAll.Checked      = false;
        rdoSelectByID.Checked     = false;
        rdoSelectByName.Checked   = false;
        rdoSelectDep.Checked      = false;
        chkAllEmpSelect.Checked   = false;
        chkAllEmpSelected.Checked = false;
        ddlDepartment.SelectedIndex = -1;
        
        txtSearchByID.Text = "";
        txtSearchByName.Text = "";
        txtSearchByDep.Text = "";
        cblEmpSelect.Items.Clear();
        cblEmpSelected.Items.Clear();
        ViewState["dtLeft"] = EmptyDataTable();
        ViewState["dtRight"] = EmptyDataTable();
        ViewState["dtEmpIds"] = "";

        ddlDepartment.Enabled  = false;
        txtSearchByDep.Enabled = false;
        btnFilter.Enabled      = false;
        pnlLeftGrid.Enabled    = false;
        pnlRightGrid.Enabled   = false;
        btnSelectEmp.Enabled   = false;
        btnDeSelectEmp.Enabled = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Clear(bool pStatus)
    {
        chkAllEmpSelect.Checked   = false;
        chkAllEmpSelected.Checked = false;
        ddlDepartment.SelectedIndex = -1;
        txtSearchByDep.Text = "";
        cblEmpSelect.Items.Clear();
        cblEmpSelected.Items.Clear();
        ViewState["dtLeft"] = EmptyDataTable();
        ViewState["dtRight"] = EmptyDataTable();
        ViewState["dtEmpIds"] = "";

        ddlDepartment.Enabled  = pStatus;
        txtSearchByDep.Enabled = pStatus;
        btnFilter.Enabled      = pStatus;
        pnlLeftGrid.Enabled    = pStatus;
        pnlRightGrid.Enabled   = pStatus;
        btnSelectEmp.Enabled   = pStatus;
        btnDeSelectEmp.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCondition(string pCon)
    {
        if (string.IsNullOrEmpty(pCon)) { ViewState["Condition"] = ""; } else { ViewState["Condition"] = " AND E.EmpID NOT IN (" + pCon + ")"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rdoSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        Clear(false);
        txtSearchByID.Enabled   = false;  txtSearchByID.Text   = "";
        txtSearchByName.Enabled = false;  txtSearchByName.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rdoSelectDep_CheckedChanged(object sender, EventArgs e)
    {
        Clear(true);
        txtSearchByID.Enabled   = false;  txtSearchByID.Text   = "";
        txtSearchByName.Enabled = false;  txtSearchByName.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rdoSelectByID_CheckedChanged(object sender, EventArgs e)
    {
        Clear(false);
        txtSearchByID.Enabled = true; txtSearchByID.Text = "";
        txtSearchByName.Enabled = false; txtSearchByName.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rdoSelectByName_CheckedChanged(object sender, EventArgs e)
    {
        Clear(false);
        txtSearchByID.Enabled   = false; txtSearchByID.Text   = "";
        txtSearchByName.Enabled = true;  txtSearchByName.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchByDep.Text)) { return; }
        string[] deps = txtSearchByDep.Text.Split('-');
        
        if (deps.Count() != 2)   { return; }
        if (string.IsNullOrEmpty(deps[1])) { return; }

        ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(deps[1]));
        ddlDepartment_SelectedIndexChanged(null, null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCon()
    {
        string Con = "";
        if (ddlEmpTypeID.SelectedIndex > 0) { Con += " AND EtpID =" + ddlEmpTypeID.SelectedValue + ""; }
        return Con;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable getEmpSelected()
    {
        DataTable EmpDT = EmptyDataTable();

        try
        {
            if (rdoSelectAll.Checked)       
            { 
                string EmpCon = FindCon();
                StringBuilder Q = new StringBuilder();
                Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameEn AS EmpName ");
                Q.Append(" FROM Employee E,Department D ");
                Q.Append(" WHERE E.DepID = D.DepID AND EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
                Q.Append(" AND D.DepID IN (" + pgCs.DepList + ") ");
                Q.Append(" " + EmpCon + " ");
                
                EmpDT = DBCs.FetchData(Q.ToString());
            }
            else if (rdoSelectByID.Checked) 
            { 
                DataRow EmpDR    = EmpDT.NewRow();
                string[] IDs     = txtSearchByID.Text.Split('-');
                EmpDR["EmpID"]   = IDs[0];
                try { EmpDR["EmpName"] = IDs[1]; } catch { }
                
                EmpDT.Rows.Add(EmpDR);
                EmpDT.AcceptChanges();
            }
            else if (rdoSelectByName.Checked) 
            { 
                DataRow EmpDR    = EmpDT.NewRow();
                string[] IDs     = txtSearchByName.Text.Split('-');
                try { EmpDR["EmpID"] = IDs[1]; } catch { }
                EmpDR["EmpName"] = IDs[0];

                //if (!string.IsNullOrEmpty(EmpDR["EmpID"].ToString()))
                //{
                    EmpDT.Rows.Add(EmpDR);
                    EmpDT.AcceptChanges();
                //}
            }
            else if (rdoSelectDep.Checked)
            {
                for (int i = 0; i < cblEmpSelected.Items.Count; i++)
                {
                    DataRow DR    = EmpDT.NewRow();
                    DR["EmpID"]   = cblEmpSelected.Items[i].Value;
                    DR["EmpName"] = cblEmpSelected.Items[i].Text;
                    EmpDT.Rows.Add(DR);
                    EmpDT.AcceptChanges();
                }
            }
        }
        catch (Exception e1) { }

        return EmpDT;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Custom Validate Events

    protected bool FindEmp(string EmpID)
    {
        string EmpCon = FindCon();
        
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT E.EmpID ");
        Q.Append(" FROM Employee E,Department D ");
        Q.Append(" WHERE E.DepID =  D.DepID ");
        Q.Append(" AND E.EmpStatus ='True' AND ISNULL(E.EmpDeleted, 0) = 0 ");
        Q.Append(" AND E.EmpID = '" + EmpID + "' ");
        Q.Append(" AND D.DepID IN (" + pgCs.DepList + ") ");
        Q.Append(" " + EmpCon + " ");
        
        DataTable FDT = DBCs.FetchData(Q.ToString());
        if (!DBCs.IsNullOrEmpty(FDT)) { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SelectEmployees_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string SelectMsg = General.Msg("Please Select Employee", "من فضلك اختر موظف");
            string FindMsg   = General.Msg("Employee Not found", "الموظف غير موجود");
            if (!rdoSelectAll.Checked && !rdoSelectByID.Checked && !rdoSelectByName.Checked && !rdoSelectDep.Checked)
            {
                e.IsValid = false;
                cvSearchByID.ErrorMessage = SelectMsg;
                cvSearchByID.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='" + SelectMsg + "' /&gt;");
                return;
            }

            if (source.Equals(cvSearchByID) && rdoSelectByID.Checked)
            {
                cvSearchByID.ErrorMessage = SelectMsg;
                cvSearchByID.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='" + SelectMsg + "' /&gt;");

                string[] IDs = txtSearchByID.Text.Split('-');
                if (string.IsNullOrEmpty(IDs[0])) { e.IsValid = false; }
                else
                {
                    cvSearchByID.ErrorMessage = FindMsg;
                    cvSearchByID.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='" + FindMsg + "' /&gt;");
                    if (!FindEmp(IDs[0])) { e.IsValid = false; }
                }
            }
            
            if (source.Equals(cvSelectByName) && rdoSelectByName.Checked)
            {
                cvSelectByName.ErrorMessage = SelectMsg;
                cvSelectByName.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='" + SelectMsg + "' /&gt;");

                string[] IDs = txtSearchByName.Text.Split('-');
                if (string.IsNullOrEmpty(IDs[1])) { e.IsValid = false; }
                else
                {
                    cvSelectByName.ErrorMessage = FindMsg;
                    cvSelectByName.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='" + FindMsg + "' /&gt;");
                    if (!FindEmp(IDs[1])) { e.IsValid = false; }
                }
            }
            
            if (source.Equals(cvSelectEmployees) && rdoSelectDep.Checked)
            {
                DataTable EmployeeInsertdt = (DataTable)ViewState["dtRight"];
                if (!DBCs.IsNullOrEmpty(EmployeeInsertdt)) { e.IsValid = true; } else { e.IsValid = false; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}