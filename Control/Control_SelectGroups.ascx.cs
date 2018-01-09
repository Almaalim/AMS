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

public partial class Control_SelectGroups : System.Web.UI.UserControl
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
    public string RwtID
    {
        get { return ((ViewState["RwtID"] == null) ? "" : ViewState["RwtID"].ToString()); }
        set { ViewState["RwtID"] = value; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable EmpSelected { get { return (DataTable)ViewState["dtRight"]; } }
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

            btnSelectEmp.ImageUrl   = General.Msg("images/Control_Images/next.png", "images/Control_Images/back.png");
            btnDeSelectEmp.ImageUrl = General.Msg("images/Control_Images/back.png", "images/Control_Images/next.png");

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Common Code ************************************/

                CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);

                cvSelectEmployees.ValidationGroup = ValidationGroupName;
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
            DataTable DT = DBCs.FetchData(" SELECT GrpName FROM RotationWorkTime_GrpRel WHERE RwtID = @P1", new string[] { ViewState["RwtID"].ToString() });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                CtrlCs.PopulateDDL(ddlSelectType, DT, "GrpName", "GrpName", General.Msg("-Select Group-", "-اختر المجموعة-"));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlSelectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSelectType.SelectedIndex > 0)
            {
                dtLeft = new DataTable();

                if (ViewState["dtLeft"] == null) { ViewState["dtLeft"] = EmptyDataTable(); }

                string Con = "";
                if (ViewState["Condition"] != null)
                {
                    if (string.IsNullOrEmpty(ViewState["Condition"].ToString())) { Con = ""; } else { Con = ViewState["Condition"].ToString(); }
                }

                string IN = "";   
                if (ViewState["dtEmpIds"] != null) { IN = ViewState["dtEmpIds"].ToString(); }
                          
                string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
                StringBuilder Q = new StringBuilder();
                Q.Append(" SELECT EmpID, EmpName" + lang + " AS EmpName, CONVERT(VARCHAR(500),'') AS GrpID ");
                Q.Append(" FROM Employee ");
                Q.Append(" WHERE EmpID IN (SELECT SCol FROM UF_CSVToTable1Col_WithOrder((SELECT EmpIDs_All FROM RotationWorkTime_GrpRel WHERE GrpName = @P1 AND RwtID = @P2), ',')" + ") ");
                if (!string.IsNullOrEmpty(IN)) { Q.Append(" AND EmpID NOT IN (" + IN + ")"); }

                dtLeft = DBCs.FetchData(Q.ToString(), new string[] { ddlSelectType.SelectedValue.ToString(), ViewState["RwtID"].ToString() } );
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
        DataTable DT = new DataTable();
        DT.Columns.Add(new DataColumn("EmpID"  , typeof(string)));
        DT.Columns.Add(new DataColumn("EmpName", typeof(string)));
        DT.Columns.Add(new DataColumn("GrpID"  , typeof(string)));
        return DT;
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
            dtLeft = EmptyDataTable();
            if (ViewState["dtRight"] == null) { ViewState["dtRight"] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight"]; }
            if (ViewState["dtLeft"]  == null) { ViewState["dtLeft"]  = EmptyDataTable(); } else { dtLeft  = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < grdLeftGrid.Rows.Count; i++)
            {
                GridViewRow gvr = grdLeftGrid.Rows[i];
                if (((CheckBox)(gvr.FindControl("chkLeftSelect"))).Checked)
                {
                    if (!String.IsNullOrEmpty(gvr.Cells[1].Text) && gvr.Cells[1].Text != "&nbsp;")
                    {
                        DataRow dRow = null;
                        dRow = dtRight.NewRow();
                        dRow["EmpID"]   = gvr.Cells[1].Text;
                        dRow["EmpName"] = gvr.Cells[2].Text;
                        dRow["GrpID"]   = ddlSelectType.SelectedValue;
                        dtRight.Rows.Add(dRow);
                        dtRight.AcceptChanges();
                        ViewState["dtRight"] = (DataTable)dtRight;

                        DataRow[] LDR = dtLeft.Select("EmpID ='" + gvr.Cells[1].Text + "'");
                        dtLeft.Rows.Remove(LDR[0]);
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

                ViewState["dtEmpIds"] = IDs;
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
                ViewState["dtEmpIds"] = null;
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
    protected void btnDeSelectEmp_Click(object sender, EventArgs e)
    {
        try
        {
            dtRight = EmptyDataTable();
            dtLeft = EmptyDataTable();
            if (ViewState["dtRight"] == null) { ViewState["dtRight"] = EmptyDataTable(); } else { dtRight = (DataTable)ViewState["dtRight"]; }
            if (ViewState["dtLeft"]  == null) { ViewState["dtLeft"]  = EmptyDataTable(); } else { dtLeft  = (DataTable)ViewState["dtLeft"]; }

            for (int i = 0; i < grdRightGrid.Rows.Count; i++)
            {
                GridViewRow gvr = grdRightGrid.Rows[i];
                if (((CheckBox)(gvr.FindControl("chkRightSelect"))).Checked)
                {
                    if (!String.IsNullOrEmpty(gvr.Cells[1].Text) && gvr.Cells[1].Text != "&nbsp;")
                    {
                        DataRow dRow = null;
                        dRow = dtLeft.NewRow();
                        dRow["EmpID"]   = gvr.Cells[1].Text;
                        dRow["EmpName"] = gvr.Cells[2].Text;
                        dRow["GrpID"]   = ddlSelectType.SelectedValue.ToString();

                        DataRow[] SDR = dtRight.Select("EmpID ='" + gvr.Cells[1].Text + "'");
                        if (ddlSelectType.SelectedValue == Convert.ToString(SDR[0]["GrpID"]))
                        {
                            dtLeft.Rows.Add(dRow);
                            dtLeft.AcceptChanges();
                            ViewState["dtLeft"] = (DataTable)dtLeft;
                        }

                        dtRight.Rows.Remove(SDR[0]);
                        dtRight.AcceptChanges();
                        ViewState["dtRight"] = (DataTable)dtRight;
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

                ViewState["dtEmpIds"] = IDs;
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
                ViewState["dtEmpIds"] = null;
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
        ddlSelectType.SelectedIndex = -1;
        CtrlCs.FillGridEmpty(ref grdLeftGrid, 50);
        CtrlCs.FillGridEmpty(ref grdRightGrid, 50);
        ViewState["dtLeft"] = EmptyDataTable();
        ViewState["dtRight"] = EmptyDataTable();
        ViewState["dtEmpIds"] = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCondition(string pCon)
    {
        if (string.IsNullOrEmpty(pCon)) { ViewState["Condition"] = ""; } else { ViewState["Condition"] = " AND E.EmpID NOT IN (" + pCon + ")"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetConditionIN(string pCon)
    {
        if (string.IsNullOrEmpty(pCon)) { ViewState["Condition"] = ""; } else { ViewState["Condition"] = " AND E.EmpID IN (" + pCon + ")"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void SelectEmployees_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            DataTable DT = (DataTable)ViewState["dtRight"];
            if (DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}