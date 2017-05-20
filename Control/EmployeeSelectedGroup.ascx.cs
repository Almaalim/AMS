﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Elmah;
using System.Data.SqlClient;

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
                /*** Common Code ************************************/

                CtrlCs.PopulateDepartmentList(ref ddlDepartment, pgCs.DepList, pgCs.Version);
                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);

                cvSelectEmployees.ValidationGroup = ValidationGroupName;
            }
  
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
                        if (IN == "") { IN += ViewState["dtEmpIds_" + ddlGrpName.Items[k].Value]; }
                        else { IN += "," + ViewState["dtEmpIds_" + ddlGrpName.Items[k].Value]; }
                    }
                }
                if (IN != "") { IN = " AND E.EmpID NOT IN (" + IN + ")"; } 

                string Q = "SELECT E.EmpID, " + General.Msg("E.EmpNameEn", "E.EmpNameAr") + " AS EmpName FROM Employee E,Department D WHERE EmpStatus ='True' AND ISNULL(EmpDeleted,0) = 0 AND E.DepID = D.DepID AND D.DepID = " + ddlDepartment.SelectedValue.ToString() + IN;
                dtLeft = DBCs.FetchData(new SqlCommand(Q));
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

                string EmpIds = "";
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    DataRow dRowEmp = dtRight.Rows[i];
                    if (i == 0) { EmpIds += dRowEmp["EmpID"].ToString(); } else { EmpIds += "," + dRowEmp["EmpID"].ToString(); }
                }
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = EmpIds;
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

                string EmployeeSelecteID = "";
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    DataRow dRowEmp = dtRight.Rows[i];
                    if (i == 0) { EmployeeSelecteID += dRowEmp["EmpID"].ToString(); } else { EmployeeSelecteID += "," + dRowEmp["EmpID"].ToString(); }
                }
                ViewState["dtEmpIds_" + ddlGrpName.SelectedValue.ToString()] = EmployeeSelecteID;
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
            ddlDepartment.SelectedIndex = -1;
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void SelectEmployees_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
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
                }
                if (String.IsNullOrEmpty(strGroup))
                {
                    e.IsValid = true;
                }
                else
                {
                    cvSelectEmployees.ErrorMessage = "Select Employees for Gruop : " + strGroup;
                    e.IsValid = false;
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