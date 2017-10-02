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
        TabContainer1.Tabs[0].HeaderText = General.Msg("Shift Details","تفاصيل الوردية");
        TabContainer1.Tabs[1].HeaderText = General.Msg("Transaction Details","تفاصيل الحركات");
        TabContainer1.Tabs[2].HeaderText = General.Msg("Gap Details","تفاصيل الثغرات");
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
            UIShiftClear();
            UITransClear();
            UIGapClear();

            if (CtrlCs.isGridEmpty(grdShift.SelectedRow.Cells[0].Text) && grdShift.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdShift, 45);
                CtrlCs.FillGridEmpty(ref grdTrans, 45);
                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                UIShiftPopulate(grdShift.SelectedRow.Cells[2].Text);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdShift_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }
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
                string EmpID = txtEmployeeID.Text.Split('-')[0];
                
                DateTime SDate;
                DateTime EDate;
                DTCs.FindMonthDates(ddlYear.SelectedItem.Value, ddlMonth.SelectedItem.Value, out SDate, out EDate);

                string Q = MainShiftQuery + " AND EmpID = @P1 AND (SsmDate BETWEEN @P2 AND @P3)";
                DataTable DT = DBCs.FetchData(Q, new string[] { EmpID, SDate.ToString(), EDate.ToString() });
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
    protected void UIShiftPopulate(string pID)
    {
        try
        {
            Label lblDate = (Label)grdShift.SelectedRow.Cells[0].FindControl("lblShiftDate");
            string strDate = DTCs.GetGregDateFromCurrDateType(lblDate.Text, "MM/dd/yyyy");

            int shiftID = Convert.ToInt16(pID);
            hdnShiftID.Value = Convert.ToString(shiftID);

            FillShiftTab(lblDate.Text, strDate, shiftID);
            FillTransGrid(strDate, shiftID);
            FillGapsGrid(strDate, shiftID);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillShiftTab(string displayDate, string strDate, int shiftID)
    {
        try
        {
            UIShiftClear();

            if (!string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                string EmpID = txtEmployeeID.Text.Split('-')[0];
                
                string Q = MainShiftQuery + " AND EmpID = @P1 AND SsmShift = @P2 AND CONVERT(CHAR(12),SsmDate, 101) = CONVERT(CHAR(12),@P3, 101)";
                DataTable DT = DBCs.FetchData(Q, new string[] { EmpID, shiftID.ToString(), strDate.ToString() });

                if (!DBCs.IsNullOrEmpty(DT))
                {
                    lbl_ST_V_Date.Text              = displayDate;
                    lbl_ST_V_ShiftID.Text           = DT.Rows[0]["SsmShift"].ToString();
                    lbl_ST_V_SsmStatus.Text         = DisplayFun.GrdDisplayShiftStatus(DT.Rows[0]["SsmStatus"]);
                    lbl_ST_V_SsmShiftDuration.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmShiftDuration"].ToString());   
                    lbl_ST_V_SsmWorkDuration.Text   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmWorkDuration"].ToString());
                    lbl_ST_V_SsmWorkDurWithET.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmWorkDurWithET"].ToString());
                    lbl_ST_V_SsmStartShiftTime.Text = DisplayFun.GrdDisplayTime(DT.Rows[0]["SsmStartShiftTime"]);
                    lbl_ST_V_SsmEndShiftTime.Text   = DisplayFun.GrdDisplayTime(DT.Rows[0]["SsmEndShiftTime"]);
                    lbl_ST_V_SsmPunchIn.Text        = DisplayFun.GrdDisplayTime(DT.Rows[0]["SsmPunchIn"]);
                    lbl_ST_V_SsmPunchOut.Text       = DisplayFun.GrdDisplayTime(DT.Rows[0]["SsmPunchOut"]);
                    lbl_ST_V_SsmBeginEarly.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmBeginEarly"].ToString());
                    lbl_ST_V_SsmBeginLate.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmBeginLate"].ToString());
                    lbl_ST_V_SsmOutEarly.Text       = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOutEarly"].ToString());
                    lbl_ST_V_SsmOutLate.Text        = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOutLate"].ToString());
                    bl_ST_V_SsmExtraTimeDur.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmExtraTimeDur"].ToString());
                    bl_ST_V_SsmOverTimeDur.Text     = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmOverTimeDur"].ToString());

                    lbl_ST_V_SsmGapDur_WithoutExc.Text = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmGapDur_WithoutExc"].ToString());
                    lbl_ST_V_SsmGapDur_PaidExc.Text    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmGapDur_PaidExc"].ToString());
                    lbl_ST_V_SsmGapDur_UnPaidExc.Text  = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmGapDur_UnPaidExc"].ToString());
                    lbl_ST_V_SsmGapDur_Grace.Text      = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmGapDur_Grace"].ToString());
                    lbl_ST_V_SsmGapDur_MG.Text         = DisplayFun.GrdDisplayDuration(DT.Rows[0]["SsmGapDur_MG"].ToString());            
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIShiftClear()
    {
        lbl_ST_V_Date.Text              = "";
        lbl_ST_V_ShiftID.Text           = "";
        lbl_ST_V_SsmStatus.Text         = "";
        lbl_ST_V_SsmShiftDuration.Text  = ""; 
        lbl_ST_V_SsmWorkDuration.Text   = "";
        lbl_ST_V_SsmWorkDurWithET.Text  = "";
        lbl_ST_V_SsmStartShiftTime.Text = "";
        lbl_ST_V_SsmEndShiftTime.Text   = "";
        lbl_ST_V_SsmPunchIn.Text        = "";
        lbl_ST_V_SsmPunchOut.Text       = "";
        lbl_ST_V_SsmBeginEarly.Text     = "";
        lbl_ST_V_SsmBeginLate.Text      = "";
        lbl_ST_V_SsmOutEarly.Text       = "";
        lbl_ST_V_SsmOutLate.Text        = "";
        bl_ST_V_SsmExtraTimeDur.Text    = "";
        bl_ST_V_SsmOverTimeDur.Text     = "";

        lbl_ST_V_SsmGapDur_WithoutExc.Text = "";
        lbl_ST_V_SsmGapDur_PaidExc.Text    = "";
        lbl_ST_V_SsmGapDur_UnPaidExc.Text  = "";
        lbl_ST_V_SsmGapDur_Grace.Text      = "";
        lbl_ST_V_SsmGapDur_MG.Text         = "";
    }

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
            UITransClear();
            UIGapClear();

            if (CtrlCs.isGridEmpty(grdTrans.SelectedRow.Cells[0].Text) && grdTrans.SelectedRow.Cells.Count == 1)
            {
                if (CtrlCs.isGridEmpty(grdShift.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdShift, 45); }

                CtrlCs.FillGridEmpty(ref grdTrans, 45);
                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                UITransPopulate();
                TabContainer1.ActiveTabIndex = 1;
            }
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
                query.Append(" AND CONVERT(CHAR(12),TrnDate, 101) = CONVERT(CHAR(12)," + "'" + date + "'" + ", 101)");
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public void FillTransTab(string date, int shiftID)
    //{
    //    try
    //    {
    //        ClearTransTab();

    //        if (!string.IsNullOrEmpty(txtEmployeeID.Text))
    //        {
    //            string empString = txtEmployeeID.Text;
    //            string[] empstr = empString.Split('-');

    //            StringBuilder query = new StringBuilder();
    //            query.Append(MainTransQuery);
    //            query.Append(" AND EmpID = '" + empstr[0].ToString() + "'");
    //            query.Append(" AND TrnShift = " + shiftID + "");
    //            query.Append(" AND CONVERT(CHAR(12),TrnDate, 101) = CONVERT(CHAR(10)," + "'" + date + "'" + ", 101)");
    //            query.Append(" ORDER BY TrnTime ASC");

    //            DataTable DT = DBCs.FetchData(new SqlCommand(query.ToString()));
    //            if (!DBCs.IsNullOrEmpty(DT))
    //            {
    //                //lblShtWorkingtimeV.Text = dr["SsmWorkDuration"].ToString();
    //                //lblShtPeriodV.Text = dr["SsmShiftDuration"].ToString();
    //                //lblActDurationV.Text = dr["SsmActualWorkDuration"].ToString();
    //                //lblWorkDurationV.Text = dr["SsmWorkDuration"].ToString();
    //                //lblShiftDurationV.Text = dr["SsmShiftDuration"].ToString();
    //                //lblBeginEarlyV.Text = dr["SsmBeginEarly"].ToString();
    //                //lblBeginLateV.Text = dr["SsmBeginLate"].ToString();
    //                //lblOvertimeAmtV.Text = dr["SsmOverTimeAmount"].ToString();
    //                //lblOutEarlyV.Text = dr["SsmOutEarly"].ToString();
    //                //lblOutLateV.Text = dr["SsmOutLate"].ToString();
    //                //lblOverTimePercentV.Text = dr["SsmOverTime"].ToString();
    //            }
    //        }

    //    }
    //    catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void UITransPopulate()
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
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UITransClear()
    {
        lblTrnTabDateV.Text = "";
        lblTrnTabTimeV.Text = "";
        lblTrnTabTypeV.Text = "";
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
            UIGapClear();

            if (CtrlCs.isGridEmpty(grdGap.SelectedRow.Cells[0].Text) && grdGap.SelectedRow.Cells.Count == 1)
            {
                if (CtrlCs.isGridEmpty(grdShift.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdShift, 45); }
                if (CtrlCs.isGridEmpty(grdTrans.Rows[0].Cells[0].Text)) { CtrlCs.FillGridEmpty(ref grdTrans, 45); }

                CtrlCs.FillGridEmpty(ref grdGap, 45);
            }
            else
            {
                UIGapPopulate();
                TabContainer1.ActiveTabIndex = 2;
            }
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
                string EmpID = txtEmployeeID.Text.Split('-')[0];
                string Q = MainGapQuery + " AND EmpID = @P1 AND GapShift = @P2 AND CONVERT(CHAR(12),GapDate, 101) = CONVERT(CHAR(12), @P3 , 101) ORDER BY GapStartTime ASC";
                DataTable DT = DBCs.FetchData(Q, new string[] { EmpID, shiftID.ToString(), date });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    grdGap.DataSource = (DataTable)DT;
                    grdGap.DataBind();
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UIGapPopulate()
    {
        try
        {
            Label lblDate = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapDate1");
            lblGapTabDateV.Text = lblDate.Text;

            Label lblStartTime = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapStartTime1");
            lblGapTabStartTimeV.Text = lblStartTime.Text;

            Label lblEndTime = (Label)grdGap.SelectedRow.Cells[0].FindControl("lblGapEndTime1");
            lblGapTabEndTimeV.Text = lblEndTime.Text;

            int gapID = Convert.ToInt32(grdGap.SelectedRow.Cells[4].Text);
            StringBuilder SQ = new StringBuilder();
            SQ.Append("SELECT DISTINCT GapDesc ,GapDuration,ExcID,(SELECT " + General.Msg("ExcNameEn","ExcNameAr") + " FROM ExcuseType WHERE ExcID = g.ExcID) AS excusetype FROM Gap g WHERE GapID = " + gapID + "");
            
            DataTable DT = DBCs.FetchData(new SqlCommand(SQ.ToString()));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                lblGapTabExcuseTypeV.Text  = DT.Rows[0]["excusetype"].ToString();
                if (GenCs.IsNullOrEmptyDB(DT.Rows[0]["ExcID"])) { lblGapTabExcuseTypeV.Text = General.Msg("There is no excuse","لا يوجد إستذان"); }
                lblGapTabGapDurationV.Text = DisplayFun.GrdDisplayDuration(DT.Rows[0]["GapDuration"].ToString());
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIGapClear()
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
