using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;

public partial class EmployeeGaps : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpRequestPro ProCs = new EmpRequestPro();
    EmpRequestSql SqlCs = new EmpRequestSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = "";

    string[] arrStatusImage = { "~/images/ERS_Images/Waiting.png", "~/images/ERS_Images/approve.png", "~/images/ERS_Images/reject.png", "~/images/ERS_Images/NotFound.png" };
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            MainQuery = " SELECT * FROM Gap WHERE GapGraceFlag ='0' AND EmpID = '" + pgCs.LoginEmpID + "'";
            /*** Fill Session ************************************/
            
            
            //AND ExcID NOT IN ( SELECT ExcID FROM ExcuseType WHERE ExcCategory IN ('OS','MG')) 
            

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense();    
                /*** get Requst Permission ***/ ViewState["ReqHT"] = pgCs.GetAllRequestPerm();
                /*** Common Code ************************************/

                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);

                string CurrentMonth = DTCs.FindCurrentMonth();
                string CurrentYear  = DTCs.FindCurrentYear();

                if (Session["ERSRefresh"] == null) { Session["ERSRefresh"] = "NotUpdate"; }
                
                if (Session["ERSRefresh"].ToString() == "Update")
                {
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Session["ERSRefreshMonth"].ToString()));
                    ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Session["ERSRefreshYear"].ToString()));
                    FillGapTable(Convert.ToInt32(Session["ERSRefreshMonth"]), Convert.ToInt32(Session["ERSRefreshYear"]));
                    Session["ERSRefresh"] = "NotUpdate";
                }
                else
                {
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToInt32(CurrentMonth).ToString()));
                    ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(CurrentYear));
                    FillGapTable(Convert.ToInt32(CurrentMonth), Convert.ToInt32(CurrentYear));
                }

                Session["AttendanceListMonth"] = CurrentMonth;
                Session["AttendanceListYear"]  = CurrentYear;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGapTable(int pMonth, int pYear)
    {
        try
        {
            FillName();

            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery + " AND GapID = '0' ";

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            FQ.Append(MainQuery);
            FQ.Append(" AND GapDate BETWEEN @SDate AND @EDate ");
            FQ.Append(" ORDER BY GapDate,GapShift,GapStartTime ");
                
            cmd.Parameters.AddWithValue("@SDate", SDate);
            cmd.Parameters.AddWithValue("@EDate", EDate);
            sql = FQ.ToString();

            clearDur();
            cmd.CommandText = sql;
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillName()
    {
        DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
        if (!DBCs.IsNullOrEmpty(DT)) { LitEmpName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string convertDayToArabic(string day)
    {
        string arDay = "";
        switch (day)
        {
            case "Sat":
                arDay = "السبت";
                break;
            case "Sun":
                arDay = "الأحد";
                break;
            case "Mon":
                arDay = "الأثنين";
                break;
            case "Tue":
                arDay = "الثلاثاء";
                break;
            case "Wed":
                arDay = "الأربعاء";
                break;
            case "Thu":
                arDay = "الخميس";
                break;
            case "Fri":
                arDay = "الجمعة";
                break;
        }
        return arDay;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayDayName(object pGDate)
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(pGDate)))
            {
                GregorianCalendar Grn = new GregorianCalendar();
                string dayNameEn = Grn.GetDayOfWeek(Convert.ToDateTime(pGDate)).ToString();
                string dayNameAr = convertDayToArabic(Convert.ToDateTime(pGDate).ToString("ddd"));

                return General.Msg(dayNameEn, dayNameAr);
            }

            return string.Empty;
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((ddlMonth.SelectedIndex >= 0) && (ddlYear.SelectedIndex >= 0)) { FillGapTable(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue)); }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindApprovalSequence(string pReqType)
    {
        try
        {
            DataTable HDT = DBCs.FetchData(" SELECT * FROM EmpApprovalLevel Where RetID = @P1 AND EmpID = @P2 ", new string[] { pReqType,pgCs.LoginEmpID  });
            if (!DBCs.IsNullOrEmpty(HDT)) { return true; } else { return false; }
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayExcuseType(object pExcID)
    {
        try
        {
            if (!string.IsNullOrEmpty(pExcID.ToString())) 
            {
                DataTable DT = DBCs.FetchData(" SELECT ExcID,ExcNameAr,ExcNameEn FROM ExcuseType WHERE ExcID = @P1 ", new string[] { pExcID.ToString() });
                if (!DBCs.IsNullOrEmpty(DT)) { General.Msg(DT.Rows[0]["ExcNameEn"].ToString(), DT.Rows[0]["ExcNameAr"].ToString()); }
            }
            return "";
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGapSummary() 
    {
        DateTime SDate = DateTime.Now;
        DateTime EDate = DateTime.Now;
        DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

        string QSum = " SELECT GapID FROM Gap WHERE GapGraceFlag ='0' AND EmpID = @P1 AND GapDate BETWEEN @P2 AND @P3 ";

        //SelectSumQuery    = " SELECT GapID FROM Gap WHERE GapGraceFlag ='0' AND EmpID = '" + pgCs.LoginEmpID + "'";
        //SelectSumQuery += " AND GapDate BETWEEN '" + startDate + "' AND '" + endDate + "' ";


        int Deduction = 0;
        //======TotalGap
        DataTable TGDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " )  ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGDT)) { if (TGDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapsDur.Text = DisplayFun.GrdDisplayDuration(TGDT.Rows[0]["SumDur"]); } }
        
        //======TotalGapWithoutExcuse
        DataTable TGWEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NULL ) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGWEDT)) 
        { 
            if (TGWEDT.Rows[0]["SumDur"] != DBNull.Value) 
            { 
                lblTotalGapWithoutExcuseDur.Text = DisplayFun.GrdDisplayDuration(TGWEDT.Rows[0]["SumDur"]);
                Deduction += Convert.ToInt32(TGWEDT.Rows[0]["SumDur"]);
            } 
        }
        
        //======TotalGapWithExcuse
        DataTable TGEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL ) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGEDT)) { if (TGEDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseDur.Text = DisplayFun.GrdDisplayDuration(TGEDT.Rows[0]["SumDur"]); } }

        //======TotalGapWithExcusePaid
        DataTable TGPEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND ExcID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '1' ) ) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGPEDT)) { if (TGPEDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcusePaidDur.Text = DisplayFun.GrdDisplayDuration(TGPEDT.Rows[0]["SumDur"]); } }

        //======TotalGapWithExcuseUnPaid
        DataTable TGUEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND ExcID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '0' ) ) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGUEDT)) 
        { 
            if (TGUEDT.Rows[0]["SumDur"] != DBNull.Value) 
            { 
                lblTotalGapWithExcuseUnPaidDur.Text = DisplayFun.GrdDisplayDuration(TGUEDT.Rows[0]["SumDur"]); 
                Deduction += Convert.ToInt32(TGUEDT.Rows[0]["SumDur"]);
            } 
        }
        
        //======GapWithExcuseByReq
        DataTable TGREDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND GapID IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 ) ) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGREDT)) { if (TGREDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseByReqDur.Text = DisplayFun.GrdDisplayDuration(TGREDT.Rows[0]["SumDur"]); } }
        
        //======TotalGapWithExcusePaidDurByReq
        DataTable TGNREDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND GapID IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 AND ErqTypeID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '1' ))) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGNREDT)) { if (TGNREDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcusePaidByReqDur.Text = DisplayFun.GrdDisplayDuration(TGNREDT.Rows[0]["SumDur"]); } }

        //======TotalGapWithExcuseUnPaidDurByReq
        DataTable TGUREDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND GapID IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 AND ErqTypeID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '0' ))) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGUREDT)) { if (TGUREDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseUnPaidByReqDur.Text = DisplayFun.GrdDisplayDuration(TGUREDT.Rows[0]["SumDur"]); } }
        
        //======TotalGapWithExcuseWaitByReq
        DataTable TGWREDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND GapID IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 0 )) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGWREDT))  { if (TGWREDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseWaitByReqDur.Text = DisplayFun.GrdDisplayDuration(TGWREDT.Rows[0]["SumDur"]); } }

        //======TotalGapWithExcuseByUser
        DataTable TGSEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND GapID NOT IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 )) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGSEDT))  { if (TGSEDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseByUserDur.Text = DisplayFun.GrdDisplayDuration(TGSEDT.Rows[0]["SumDur"]); } }
        
        //======TotalGapWithExcusePaidDurByUser
        DataTable TGPSEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND ExcID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '1' ) AND GapID NOT IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 )) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGPSEDT))  { if (TGPSEDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcusePaidByUserDur.Text = DisplayFun.GrdDisplayDuration(TGPSEDT.Rows[0]["SumDur"]); } }

        //======TotalGapWithExcuseUnPaidDurByUser
        DataTable TGUSEDT = DBCs.FetchData(" SELECT SUM(GapDuration) SumDur FROM Gap WHERE GapID IN ( " + QSum + " AND ExcID IS NOT NULL AND ExcID IN ( SELECT ExcID FROM ExcuseType WHERE ExcIsPaid = '0' ) AND GapID NOT IN ( SELECT GapOvtID FROM EmpRequest WHERE RetID = 'EXC' AND ErqReqStatus = 1 )) ", new string[] { pgCs.LoginEmpID, SDate.ToString(), EDate.ToString() });
        if (!DBCs.IsNullOrEmpty(TGUSEDT))  { if (TGUSEDT.Rows[0]["SumDur"] != DBNull.Value) { lblTotalGapWithExcuseUnPaidByUserDur.Text = DisplayFun.GrdDisplayDuration(TGUSEDT.Rows[0]["SumDur"]); } }

        //======Deduction
        lblTotalDeductionDur.Text = DisplayFun.GrdDisplayDuration(Deduction);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void clearDur()
    {
        lblTotalGapsDur.Text = "00:00:00";
        lblTotalGapWithoutExcuseDur.Text = "00:00:00";
        
        lblTotalGapWithExcuseDur.Text = "00:00:00";
        lblTotalGapWithExcusePaidDur.Text = "00:00:00";
        lblTotalGapWithExcuseUnPaidDur.Text = "00:00:00";

        lblTotalGapWithExcuseByReqDur.Text = "00:00:00";
        lblTotalGapWithExcusePaidByReqDur.Text = "00:00:00";
        lblTotalGapWithExcuseUnPaidByReqDur.Text = "00:00:00";
        lblTotalGapWithExcuseWaitByReqDur.Text = "00:00:00";
        lblTotalGapWithExcuseByUserDur.Text = "00:00:00";
        lblTotalGapWithExcusePaidByUserDur.Text = "00:00:00";
        lblTotalGapWithExcuseUnPaidByUserDur.Text = "00:00:00";
        lblTotalDeductionDur.Text = "00:00:00";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton GapButton = (ImageButton)e.Row.Cells[7].Controls[1];
                ImageButton StatusReqButton = (ImageButton)e.Row.Cells[8].Controls[1];

                GapButton.Visible = pgCs.GetRequestPerm(ViewState["ReqHT"], "EXC");

                StatusReqButton.Visible = true;
                int StatusReq = FoundRequest(((DataRowView)e.Row.DataItem)["GapID"].ToString());

                GapButton.ImageUrl = "~/images/ERS_Images/SendRequest.png";
                GapButton.ToolTip = General.Msg("Send Excuse Request", "إرسال طلب استئذان");
                if (StatusReq != 3)
                {
                    StatusReqButton.ImageUrl = arrStatusImage[StatusReq];
                    if (StatusReq != 2)
                    {
                        GapButton.Enabled = false;
                        GapButton.ToolTip = "";
                        GapButton.ImageUrl = "~/images/ERS_Images/SendRequestGray.png";
                    }
                    else { GapButton.Enabled = true; }
                }
                else
                {
                    bool StatusExc = FoundExc(((DataRowView)e.Row.DataItem)["GapID"].ToString());
                    if (StatusExc)
                    {
                        GapButton.Enabled = false;
                        GapButton.ToolTip = "";
                        GapButton.ImageUrl = "~/images/ERS_Images/SendRequestGray.png";
                        StatusReqButton.ImageUrl = "~/images/ERS_Images/addByuser.png";
                    }
                    else
                    {
                        StatusReqButton.ImageUrl = arrStatusImage[StatusReq];
                        GapButton.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("SendReq_Command"):
                    
                    if (FindApprovalSequence("EXC"))
                    {
                        lblNamePopup.Text = General.Msg("Excuse Request", "طلب استئذان");
                        ifrmPopup.Attributes.Add("src", "../Pages_Request/ExcuseRequest2.aspx?ID=" + e.CommandArgument.ToString());
                        Session["ERSRefreshMonth"] = ddlMonth.SelectedValue;
                        Session["ERSRefreshYear"] = ddlYear.SelectedValue;
                        Session["ParentExcuseRequest"] = "../Pages_Attend/EmployeeGaps.aspx";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup();", true);
                    }

                    break;
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
    protected void FillGrid(SqlCommand cmd)
    {
        DataTable GDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            grdData.DataSource = (DataTable)GDT;
            ViewState["grdDataDT"] = (DataTable)GDT;
            grdData.DataBind();

            FillGapSummary();
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected int FoundRequest(string pGapID)
    {
        int ReqStat = 3;
        try
        {
            DataTable DT = new DataTable();

            if (pgCs.GetRequestPerm(ViewState["ReqHT"], "EXC"))
            {
                DT = DBCs.FetchData(" SELECT ErqReqStatus FROM EmpRequest WHERE EmpID = @P1 AND RetID ='EXC' AND GapOvtID = @P2 ORDER By ErqID DESC ", new string[] { pgCs.LoginEmpID, pGapID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string ReqStatus = DT.Rows[0]["ErqReqStatus"].ToString();
                    if (ReqStatus == "0") { ReqStat = 0; }
                    else if (ReqStatus == "1") { ReqStat = 1; }
                    else if (ReqStatus == "2") { ReqStat = 2; }
                    else { ReqStat = 3; }
                }
            }
        }
        catch (Exception e1) { }

        return ReqStat;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FoundExc(string pGapID)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT ExcID FROM Gap WHERE GapID = @P1 ", new string[] { pGapID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["ExcID"] != DBNull.Value) { return true; } else { return false; }
            }
            else { return false; }
        }
        catch (Exception e1)
        {
            return false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}