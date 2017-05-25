using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;
using System.Collections;

public partial class EmployeeTransactions : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    GregorianCalendar Grn = new GregorianCalendar();
    UmAlQuraCalendar  Umq = new UmAlQuraCalendar();
    
    string MainShiftQuery = " SELECT * FROM ShiftSummary ";
    string MainTransQuery = " SELECT * FROM Trans ";
    string MainGapQuery   = " SELECT * FROM Gap ";   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdShift,45);
            CtrlCs.RefreshGridEmpty(ref grdTrans,45);
            CtrlCs.RefreshGridEmpty(ref grdGap,45);
            /*** Fill Session ************************************/
            
            MainShiftQuery += " WHERE EmpID IN (SELECT EmpID FROM Employee WHERE DepID IN (" + pgCs.DepList + ") ) ";
            MainTransQuery += " WHERE EmpID IN (SELECT EmpID FROM Employee WHERE DepID IN (" + pgCs.DepList + ") ) ";
            MainGapQuery   += " WHERE EmpID IN (SELECT EmpID FROM Employee WHERE DepID IN (" + pgCs.DepList + ") ) ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                CtrlCs.FillGridEmpty(ref grdShift, 45);
                CtrlCs.FillGridEmpty(ref grdTrans, 45);
                CtrlCs.FillGridEmpty(ref grdGap, 45);
                UILang();
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                TabContainer1.ActiveTabIndex = 0;
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
            DTCs.YearPopulateList(ref ddlYear);
            DTCs.MonthPopulateList(ref ddlMonth);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UILang()
    {
        if (pgCs.Lang == "AR")
        {
            TabContainer1.Tabs[0].HeaderText = "تفاصيل الوردية";
            TabContainer1.Tabs[1].HeaderText = "تفاصيل الحركات";
            TabContainer1.Tabs[2].HeaderText = "تفاصيل الثغرات";
        }
        else
        {
                 
            TabContainer1.Tabs[0].HeaderText = "Shift Details";
            TabContainer1.Tabs[1].HeaderText = "Transaction Details";
            TabContainer1.Tabs[2].HeaderText = "Gap Details";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnMonthFilter_Click(object sender, ImageClickEventArgs e) { FillShiftGrid(); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region grdShift Events

    protected void grdShift_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        DropDownList _ddlPager = CtrlCs.PagerList(grdShift);
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
        grdShift.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdShift.PageIndex = 0;
        FillShiftGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        //if (e.Row.RowIndex == 0) {
                            e.Row.Style.Add("height", "19px"); e.Row.Style.Add("vertical-align", "bottom"); 
                        //}
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdShift, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdShift.PageIndex = e.NewPageIndex;
        FillShiftGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //UIClear();
            ClearShiftTab();
            ClearTransTab();
            ClearGapTab();

            if (CtrlCs.isGridEmpty(grdShift.SelectedRow.Cells[0].Text) && grdShift.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdShift, 45);
                CtrlCs.FillGridEmpty(ref grdTrans, 45);
                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                PopulateShiftUI(grdShift.SelectedRow.Cells[1].Text);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateShiftUI(string pID)
    {
        try
        {
            Label lblDate = (Label)grdShift.SelectedRow.Cells[0].FindControl("lblShiftDate");
            string strDate = DTCs.GetGregDateFromCurrDateType(lblDate.Text, "MM/dd/yyyy");

            int shiftID = Convert.ToInt16(grdShift.SelectedRow.Cells[1].Text);
            hdnShiftID.Value = Convert.ToString(shiftID);

            lblShtTabDateV.Text = lblDate.Text;
            
            FillShiftTab(strDate, shiftID);
            FillTransGrid(strDate, shiftID);
            FillGapsGrid(strDate, shiftID);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillShiftGrid()
    {
        try
        {
            CtrlCs.FillGridEmpty(ref grdShift, 45);
            CtrlCs.FillGridEmpty(ref grdTrans, 45);
            CtrlCs.FillGridEmpty(ref grdGap, 45);

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string empString = txtEmployeeID.Text;
                string[] empstr = empString.Split('-');
                
                DateTime SDate;
                DateTime EDate;
                DTCs.FindMonthDates(ddlYear.SelectedItem.Value, ddlMonth.SelectedItem.Value, out SDate, out EDate);

                string strFilter = MainShiftQuery;
                strFilter += " AND (SsmDate BETWEEN '" + SDate + "' AND '" + EDate + "')";
                strFilter += " AND EmpID = '" + empstr[0].ToString() + "'";

                DataTable DT = DBCs.FetchData(new SqlCommand(strFilter));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    grdShift.DataSource = (DataTable)DT;
                    grdShift.DataBind();
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdShift_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region grdTrans Events

    protected void grdTrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        e.Row.Style.Add("height", "19px"); e.Row.Style.Add("vertical-align", "bottom"); 
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdTrans, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTrans.PageIndex = e.NewPageIndex;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //UIClear();
            ClearTransTab();
            ClearGapTab();

            if (CtrlCs.isGridEmpty(grdTrans.SelectedRow.Cells[0].Text) && grdTrans.SelectedRow.Cells.Count == 1)
            {
                if (CtrlCs.isGridEmpty(grdShift.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdShift, 45); }

                CtrlCs.FillGridEmpty(ref grdTrans, 45);
                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                PopulateTransUI();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateTransUI()
    {
        try
        {
            Label lblDate = (Label)grdTrans.SelectedRow.Cells[0].FindControl("lblTransDate");
            string strDate = DTCs.GetGregDateFromCurrDateType(lblDate.Text, "MM/dd/yyyy");

            Int16 shiftID = Convert.ToInt16(hdnShiftID.Value);

            lblTrnTabDateV.Text = lblDate.Text;

            Label lblTime = (Label)grdTrans.SelectedRow.Cells[0].FindControl("lblTranstime");
            lblTrnTabTimeV.Text = lblTime.Text;

            Label lblType = (Label)grdTrans.SelectedRow.Cells[0].FindControl("lblTranstype");
            lblTrnTabTypeV.Text = lblType.Text;

            FillGapsGrid(strDate, shiftID);

            TabContainer1.ActiveTabIndex = 1;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillTransGrid(string date, int shiftID)
    {
        try
        {
            CtrlCs.FillGridEmpty(ref grdTrans, 45);
            //CtrlCs.FillGridEmpty(ref grdGap, 45);

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string empString = txtEmployeeID.Text;
                string[] empstr = empString.Split('-');
               
                StringBuilder query = new StringBuilder();
                query.Append(MainTransQuery);
                query.Append(" AND EmpID = '" + empstr[0].ToString() + "'");
                query.Append(" AND TrnShift = " + shiftID + "");
                query.Append(" AND CONVERT(CHAR(12),TrnDate, 103) = CONVERT(CHAR(12)," + "'" + date + "'" + ", 103)");
                query.Append(" ORDER BY TrnTime ASC");

                
                DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    grdTrans.DataSource = (DataTable)DT;
                    grdTrans.DataBind();
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
    #region grdGap Events

    protected void grdGap_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        break;
                    }
                 default:
                    {
                        e.Row.Cells[5].Visible = false; //To hide ID column in grid view
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdGap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        e.Row.Style.Add("height", "19px"); e.Row.Style.Add("vertical-align", "bottom"); 
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdGap, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdGap_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGap.PageIndex = e.NewPageIndex;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdGap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //UIClear();
            ClearGapTab();

            if (CtrlCs.isGridEmpty(grdGap.SelectedRow.Cells[0].Text) && grdGap.SelectedRow.Cells.Count == 1)
            {
                if (CtrlCs.isGridEmpty(grdShift.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdShift, 45); }
                if (CtrlCs.isGridEmpty(grdTrans.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdTrans, 45); }

                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                PopulateGapUI();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateGapUI()
    {
        try
        {
            Label lblDate = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapDate1");
            lblGapTabDateV.Text = lblDate.Text;

            Label lblStartTime = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapStartTime1");
            lblGapTabStartTimeV.Text = lblStartTime.Text;

            Label lblEndTime = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapEndTime1");
            lblGapTabEndTimeV.Text = lblEndTime.Text;

            TabContainer1.ActiveTabIndex = 2;
            int gapID = Convert.ToInt32(grdGap.SelectedRow.Cells[4].Text);
            FillGapTab(gapID);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGapsGrid(string date, int shiftID)
    {
        try
        {
            CtrlCs.FillGridEmpty(ref grdGap, 45);

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string empString = txtEmployeeID.Text;
                string[] empstr = empString.Split('-');
               
                StringBuilder query = new StringBuilder();
                query.Append(MainGapQuery);
                query.Append(" AND EmpID = '" + empstr[0].ToString() + "'");
                query.Append(" AND GapShift = " + shiftID + "");
                query.Append(" AND CONVERT(CHAR(12),GapDate, 101) = CONVERT(CHAR(12)," + "'" + date + "'" + ", 101)");
                query.Append(" ORDER BY GapStartTime ASC");
                
                DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    grdGap.DataSource = (DataTable)DT;
                    grdGap.DataBind();
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
    #region Tabs Events

    public void FillShiftTab(string strDate, int shiftID)
    {
        try
        {
            ClearShiftTab();

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string empString = txtEmployeeID.Text;
                string[] empstr = empString.Split('-');
                
                StringBuilder query = new StringBuilder();
                query.Append(MainShiftQuery);
                query.Append(" AND EmpID = '" + empstr[0].ToString() + "'");
                query.Append(" AND SsmShift = " + shiftID + "");
                query.Append(" AND CONVERT(CHAR(12),SsmDate, 101) = CONVERT(CHAR(12)," + "'" + strDate + "'" + ", 101)");

                DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    lblShtTabWorkingtimeV.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmWorkDuration"].ToString());
                    lblShtTabPeriodV.Text        = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmShiftDuration"].ToString());
                    lblShtTabActDurationV.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmActualWorkDuration"].ToString());
                    lblShtTabWorkDurationV.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmWorkDuration"].ToString());
                    lblShtTabShiftDurationV.Text = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmShiftDuration"].ToString());
                    lblShtTabBeginEarlyV.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmBeginEarly"].ToString());
                    lblShtTabBeginLateV.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmBeginLate"].ToString());
                    lblShtTabOvertimeV.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOverTime"].ToString());
                    lblShtTabOutEarlyV.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOutEarly"].ToString());
                    lblShtTabOutLateV.Text       = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOutLate"].ToString());
                    lblShtTabExtraTimeV.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmExtratime"].ToString());
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillTransTab(string date, int shiftID)
    {
        try
        {
            ClearTransTab();

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string empString = txtEmployeeID.Text;
                string[] empstr = empString.Split('-');
                
                StringBuilder query = new StringBuilder();
                query.Append(MainTransQuery);
                query.Append(" AND EmpID = '" + empstr[0].ToString() + "'");
                query.Append(" AND TrnShift = " + shiftID + "");
                query.Append(" AND CONVERT(CHAR(12),TrnDate, 101) = CONVERT(CHAR(10)," + "'" + date + "'" + ", 101)");
                query.Append(" ORDER BY TrnTime ASC");

                DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    //lblShtWorkingtimeV.Text = dr["SsmWorkDuration"].ToString();
                    //lblShtPeriodV.Text = dr["SsmShiftDuration"].ToString();
                    //lblActDurationV.Text = dr["SsmActualWorkDuration"].ToString();
                    //lblWorkDurationV.Text = dr["SsmWorkDuration"].ToString();
                    //lblShiftDurationV.Text = dr["SsmShiftDuration"].ToString();
                    //lblBeginEarlyV.Text = dr["SsmBeginEarly"].ToString();
                    //lblBeginLateV.Text = dr["SsmBeginLate"].ToString();
                    //lblOvertimeAmtV.Text = dr["SsmOverTimeAmount"].ToString();
                    //lblOutEarlyV.Text = dr["SsmOutEarly"].ToString();
                    //lblOutLateV.Text = dr["SsmOutLate"].ToString();
                    //lblOverTimePercentV.Text = dr["SsmOverTime"].ToString();
                }
            }

        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillGapTab(int gapID)
    {
        try
        {
            ClearGapTab();

            StringBuilder query = new StringBuilder();
            query.Append("SELECT DISTINCT GapDesc ,GapDuration,(SELECT " + General.Msg("ExcNameEn","ExcNameAr") + " FROM ExcuseType WHERE ExcID = g.ExcID) AS excusetype FROM Gap g WHERE GapID = " + gapID + "");
            

            DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                lblGapTabExcuseTypeV.Text  = DT.Rows[0]["excusetype"].ToString();
                lblGapTabGapDurationV.Text = DisplayFun.GrdDisplayDuration(DT.Rows[0]["GapDuration"].ToString());
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ClearShiftTab()
    {
        lblShtTabDateV.Text = "";
        lblShtTabWorkingtimeV.Text = "";
        lblShtTabPeriodV.Text = "";
        lblShtTabActDurationV.Text = "";
        lblShtTabShiftDurationV.Text = "";
        lblShtTabWorkDurationV.Text = "";
        lblShtTabBeginEarlyV.Text = "";
        lblShtTabBeginLateV.Text = "";
        lblShtTabExtraTimeV.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ClearTransTab()
    {
        lblTrnTabDateV.Text = "";
        lblTrnTabTimeV.Text = "";
        lblTrnTabTypeV.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ClearGapTab()
    {
        lblGapTabDateV.Text = "";
        lblGapTabStartTimeV.Text = "";
        lblGapTabEndTimeV.Text = "";
        lblGapTabGapDurationV.Text = "";
        lblGapTabExcuseTypeV.Text = "";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
