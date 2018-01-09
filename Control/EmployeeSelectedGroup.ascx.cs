using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Elmah;
using System.Data.SqlClient;
using System.Text;

public partial class EmployeeSelectedGroup : System.Web.UI.UserControl
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _ValidationType = "ALL";
    public string ValidationType
    {
        get { return _ValidationType; }
        set { if (_ValidationType != value) { _ValidationType = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdLeftGrid);
            CtrlCs.RefreshGridEmpty(ref grdRightGrid);
            /*** Fill Session ************************************/
            
            btnSelectEmp.ImageUrl   = General.Msg("images/Control_Images/next.png","images/Control_Images/back.png");
            btnDeSelectEmp.ImageUrl = General.Msg("images/Control_Images/back.png","images/Control_Images/next.png");

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Common Code ************************************/

                FillList();
                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);

                cvSelectEmployees.ValidationGroup = ValidationGroupName;
            }
  
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillList()
    {
        try
        {
            CtrlCs.PopulateDepartmentList(ref ddlDepartment, pgCs.DepList, pgCs.Version);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillRotation(string RwtID)
    {
        try
        {
            Random randNum = new Random();
            string GroupIDs   = "";
            string GroupNames = "";
            string EmpGroup   = "";
            ViewState["EmpGroup"] = "";

            DataTable DT = DBCs.FetchData(" SELECT * FROM RotationWorkTime_GrpRel WHERE RwtID = @P1 ORDER BY OrderID ", new string[] { RwtID });
            foreach (DataRow DR in DT.Rows)
            {
                ListItem _ls = new ListItem();
                _ls.Value = randNum.Next(99).ToString();
                _ls.Text  = DR["GrpName"].ToString();
                AddGruop(_ls);

                string p1 = (string.IsNullOrEmpty(GroupIDs)) ? "" : ",";
                GroupIDs += p1 + _ls.Value;

                string p2 = (string.IsNullOrEmpty(GroupNames)) ? "" : ",";
                GroupNames += p2 + _ls.Text;

                string perifx = (string.IsNullOrEmpty(EmpGroup)) ? "" : ",";
                EmpGroup += perifx + "'" + DR["EmpIDs_All"].ToString().Replace(",", "','") + "'";
            }

            ViewState["GroupsInfo"] = GroupIDs + "-" + GroupNames;
            ViewState["EmpGroup"]   = EmpGroup;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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

                string IN = "";
                for (int k = 0; k < ddlGrpName.Items.Count; k++)
                {
                    if (ViewState["dtEmpIds_" + ddlGrpName.Items[k].Value] != null)
                    {
                        string perifx = (string.IsNullOrEmpty(IN)) ? "" : ",";
                        IN += perifx + ViewState["dtEmpIds_" + ddlGrpName.Items[k].Value];
                    }
                }

                string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
                StringBuilder Q = new StringBuilder();
                Q.Append(" SELECT EmpID, EmpName" + lang + " AS EmpName ");
                Q.Append(" FROM spActiveEmployeeView ");
                Q.Append(" WHERE DepID = " + ddlDepartment.SelectedValue.ToString() + "");
                if (!string.IsNullOrEmpty(IN)) { Q.Append(" AND EmpID NOT IN (" + IN + ")"); }
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["EmpGroup"]))) { Q.Append(" AND EmpID NOT IN (" + Convert.ToString(ViewState["EmpGroup"]) + ")"); }

                dtLeft = DBCs.FetchData(new SqlCommand(Q.ToString()));
                if (!DBCs.IsNullOrEmpty(dtLeft))
                {
                    grdLeftGrid.DataSource = (DataTable)dtLeft;
                    grdLeftGrid.DataBind();
                    grdLeftGrid.Columns[0].Visible = true;
                    ViewState["dtLeft"] = (DataTable)dtLeft;
                }
                else
                {
                    CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                    ViewState["dtLeft"] = (DataTable)dtLeft;
                }
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
        }
        catch (Exception e1) { }
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
    protected void chkLeftAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)grdLeftGrid.HeaderRow.FindControl("chkLeftAll");

        if (chkAll.Checked == true)
        {

            foreach (GridViewRow gvRow in grdLeftGrid.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkLeftSelect");
                chkSel.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvRow in grdLeftGrid.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkLeftSelect");
                chkSel.Checked = false;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkRightAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)grdRightGrid.HeaderRow.FindControl("chkRightAll");

        if (chkAll.Checked == true)
        {

            foreach (GridViewRow gvRow in grdRightGrid.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkRightSelect");
                chkSel.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvRow in grdRightGrid.Rows)
            {
                CheckBox chkSel = (CheckBox)gvRow.FindControl("chkRightSelect");
                chkSel.Checked = false;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSelectEmp_Click(object sender, EventArgs e)
    {
        try
        {
            dtRight = EmptyDataTable();
            dtLeft  = EmptyDataTable();
            if (ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] == null) { ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()]; }
            if (ViewState["dtLeft"] == null) { ViewState["dtLeft"] = EmptyDataTable(); } else { dtLeft = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < grdLeftGrid.Rows.Count; i++)
            {
                GridViewRow gvr = grdLeftGrid.Rows[i];
                if (((CheckBox)(gvr.FindControl("chkLeftSelect"))).Checked)
                {
                    if (!String.IsNullOrEmpty(gvr.Cells[1].Text) && gvr.Cells[1].Text != "&nbsp;")
                    {
                        DataRow dRow = null;
                        dRow = dtRight.NewRow();
                        dRow["EmpID"] = gvr.Cells[1].Text;
                        dRow["EmpName"] = gvr.Cells[2].Text;
                        dtRight.Rows.Add(dRow);
                        dtRight.AcceptChanges();
                        ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] = (DataTable)dtRight;

                        DataRow[] rows = dtLeft.Select("EmpID ='" + gvr.Cells[1].Text + "'");
                        DataRow dr = rows[0];
                        rows.Count();
                        dtLeft.Rows.Remove(dr);
                        dtLeft.AcceptChanges();
                        ViewState["dtLeft"] = (DataTable)dtLeft;
                    }
                }
            }

            if (!DBCs.IsNullOrEmpty(dtRight))
            {
                grdRightGrid.Columns[0].Visible = true;
                grdRightGrid.DataSource = (DataTable)dtRight;
                grdRightGrid.DataBind();

                string IDs = "";
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    string perifx = (string.IsNullOrEmpty(IDs)) ? "" : ",";
                    IDs += perifx + "'" + dtRight.Rows[i]["EmpID"].ToString() + "'";
                }
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = IDs;

            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = null;
            }

            if (!DBCs.IsNullOrEmpty(dtLeft))
            {
                grdLeftGrid.Columns[0].Visible = true;
                grdLeftGrid.DataSource = (DataTable)dtLeft;
                grdLeftGrid.DataBind();
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
        }
        catch (Exception e1) {    }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDeSelectEmp_Click(object sender, EventArgs e)
    {
        try
        {
            dtRight = EmptyDataTable();
            dtLeft = EmptyDataTable();
            if (ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] == null) { ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()]; }
            if (ViewState["dtLeft"] == null) { ViewState["dtLeft"] = EmptyDataTable(); } else { dtLeft = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < grdRightGrid.Rows.Count; i++)
            {
                GridViewRow gvr = grdRightGrid.Rows[i];
                if (((CheckBox)(gvr.FindControl("chkRightSelect"))).Checked)
                {
                    if (!String.IsNullOrEmpty(gvr.Cells[1].Text) && gvr.Cells[1].Text != "&nbsp;")
                    {
                        DataRow dRow = null;
                        dRow = dtLeft.NewRow();
                        dRow["EmpID"] = gvr.Cells[1].Text;
                        dRow["EmpName"] = gvr.Cells[2].Text;

                        DataTable DT = DBCs.FetchData("SELECT DepID FROM Employee WHERE EmpID = @P1 ", new string[] { gvr.Cells[1].Text });
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            DataRow depRow = DT.Rows[0];
                            if (ddlDepartment.SelectedValue == depRow["DepID"].ToString())
                            {
                                dtLeft.Rows.Add(dRow);
                                dtLeft.AcceptChanges();
                                ViewState["dtLeft"] = (DataTable)dtLeft;
                            }
                            DataRow[] rows = dtRight.Select("EmpID ='" + gvr.Cells[1].Text + "'");
                            DataRow dr = rows[0];

                            dtRight.Rows.Remove(dr);
                            dtRight.AcceptChanges();
                            ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] = (DataTable)dtRight;
                        }
                    }
                }
            }

            if (!DBCs.IsNullOrEmpty(dtRight))
            {
                grdRightGrid.Columns[0].Visible = true;
                grdRightGrid.DataSource = (DataTable)dtRight;
                grdRightGrid.DataBind();

                string IDs = "";
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    string perifx = (string.IsNullOrEmpty(IDs)) ? "" : ",";
                    IDs += perifx + "'" + dtRight.Rows[i]["EmpID"].ToString() + "'";
                }
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = IDs;
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = null;
            }

            if (!DBCs.IsNullOrEmpty(dtLeft))
            {
                grdLeftGrid.Columns[0].Visible = true;
                grdLeftGrid.DataSource = (DataTable)dtLeft;
                grdLeftGrid.DataBind();
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                ViewState["dtLeft"] = (DataTable)dtLeft;
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Clear()
    {
        ddlDepartment.SelectedIndex = -1;
        CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
        CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
        ViewState["dtLeft"] = EmptyDataTable();

        if (ddlGrpName.Items.Count != 0)
        {
            for (int h = 0; h < ddlGrpName.Items.Count; h++)
            {
                ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] = EmptyDataTable();
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = "";
            }
        }
        ddlGrpName.Items.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlGrpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGrpName.SelectedIndex > -1)
        {
            //ddlDepartment.SelectedIndex = -1;
            if (ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()] != null)
            {
                grdRightGrid.DataSource = (DataTable)ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()];
                grdRightGrid.DataBind();
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearGruop() { ddlGrpName.Items.Clear(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void AddGruop(ListItem pLS) { ddlGrpName.Items.Add(pLS); ddlGrpName.SelectedIndex = 0; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable getEmpSelected(string pGroupName) { return (DataTable)ViewState["dtRight_" + pGroupName]; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public ListItemCollection getGroupList() { return ddlGrpName.Items; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void SelectEmployees_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            bool isSelect = false;
            string strGroup = "";
            if (ddlGrpName.Items.Count != 0)
            {
                for (int i = 0; i < ddlGrpName.Items.Count; i++)
                {
                    DataTable EmployeeInsertdt = (DataTable)ViewState["dtRight_" + ddlGrpName.SelectedValue.ToString()];
                    if (DBCs.IsNullOrEmpty(EmployeeInsertdt))
                    {
                        if (strGroup == "") { strGroup += ddlGrpName.Items[i].Text; } else { strGroup += "," + ddlGrpName.Items[i].Text; }
                    }
                    else
                    {
                        if (ValidationType == "ONE")
                        {
                            isSelect = true;
                        }
                    }
                }

                if (ValidationType == "ALL")
                {
                    if (!string.IsNullOrEmpty(strGroup))
                    {
                        ValidMsg(ref cvSelectEmployees, true, General.Msg("Select Employees for Gruops : " + strGroup, "يجب إختيار موظفين للمجموعات :" + strGroup));
                        e.IsValid = false;
                    }
                }
                else
                {
                    if (!isSelect)
                    {
                        ValidMsg(ref cvSelectEmployees, true, General.Msg("At least one employee must be selected", "يجب إختيار موظف واحد على الأقل"));
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
    public void ValidMsg(ref CustomValidator cv, bool isShow, string pMsg)
    {
        if (isShow)
        {
            cv.ErrorMessage = pMsg;
            cv.Text = Server.HtmlDecode("&lt;img src='../App_Themes/ThemeEn/Images/Validation/message_exclamation.png' title='" + pMsg + "' /&gt;");
        }
        else
        {
            cv.ErrorMessage = "";
            cv.Text = Server.HtmlDecode("&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='" + pMsg + "' /&gt;");
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}