using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class RequestApproval : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReqApprovalPro ProCs = new ReqApprovalPro();
    ReqApprovalSql SqlCs = new ReqApprovalSql();
    
    GapPro GapProCs = new GapPro();
    GapSql GapSqlCs = new GapSql();  
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = "";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            MainQuery = " SELECT * FROM EmployeeRequestInfoView WHERE ErqID IN ( SELECT EmpRequestApprovalStatus.ReqID FROM EmpRequestApprovalStatus WHERE '" + pgCs.LoginMainUser + "' IN (SELECT * FROM UF_CSVToTable(EmpRequestApprovalStatus.MgrID)) AND EmpRequestApprovalStatus.EraStatus = 0 )";
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense(); 
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillList();
                
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"].ToString() == "ALL")
                    {
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex  = 0;
                    }
                }
                                
                btnFilter_Click(null, null);
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                divVisible(false);
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
            DTCs.YearPopulateList(ref ddlYear, pgCs.DateType, true);
            DTCs.MonthPopulateList(ref ddlMonth, pgCs.DateType, true);
            
            DataTable DT = DBCs.FetchData(" SELECT DISTINCT EmpID,EmpNameAr,EmpNameEn FROM EmployeeApprovalLevelInfoView WHERE EmpStatus = 'True' AND @P1 IN (SELECT * FROM UF_CSVToTable(EalMgrID)) ", new string[] { pgCs.LoginMainUser });
            if (!DBCs.IsNullOrEmpty(DT)) { CtrlCs.PopulateDDL(ddlFilter, DT, "EmpID", General.Msg("EmpNameEn","EmpNameAr"), "EmpID", General.Msg("-Select Employee-","- اختر الموظف-")); }

            CtrlCs.FillExcuseTypeList(ref ddlExcType, null, false, true);
            CtrlCs.FillVacationTypeList(ref ddlVacType, null, false, true, "ALL");
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayType(object pType)
    {
        try
        {
            if      (pType.ToString() == "EXC") { return General.Msg("Excuse","استئذان"); }
            else if (pType.ToString() == "VAC") { return General.Msg("Vacation","إجازة"); }
            else if (pType.ToString() == "OVT") { return General.Msg("Overtime","عمل إضافي"); }
            else if (pType.ToString() == "SWP") { return General.Msg("Swap Shift Time","تبديل وقت عمل"); }
            else if (pType.ToString() == "ESH") { return General.Msg("Excuse Shift","استئذان وردية"); }
            else if (pType.ToString() == "COM") { return General.Msg("Commission","انتداب"); }
            else if (pType.ToString() == "JOB") { return General.Msg("Job Assignment","مهمة عمل"); }
            else if (pType.ToString() == "ATR") { return General.Msg("Add Transaction","إضافة حركة"); }
            else if (pType.ToString() == "CTR") { return General.Msg("Completion Transaction","إكمال حركة"); }
            else { return ""; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string MakeValidFileName(string name)
    {
        string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidReStr = string.Format(@"[{0}]+", invalidChars);
        string replace = Regex.Replace(name, invalidReStr, "_").Replace(";", "").Replace(",", "");
        return replace;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ServerMapPath(string path) { return HttpContext.Current.Server.MapPath(path); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static HttpResponse GetHttpResponse() { return HttpContext.Current.Response; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex == 0) || (ddlMonth.SelectedIndex == 0 && ddlYear.SelectedIndex > 0))
            {
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("You must select the month and year", "يجب اختيار الشهر والسنة"));
                return;
            }

            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery;

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            
            StringBuilder FQ = new StringBuilder();    
            FQ.Append(MainQuery);

            if (ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
            {
                DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);
                FQ.Append(" AND ErqStartDate BETWEEN @SDate AND @EDate ");
                cmd.Parameters.AddWithValue("@SDate", SDate);
                cmd.Parameters.AddWithValue("@EDate", EDate);
            }

            if (ddlFilter.SelectedIndex > 0)
            {
                FQ.Append(" AND EmpID = @EmpID ");
                cmd.Parameters.AddWithValue("@EmpID",ddlFilter.SelectedValue.Trim());
            }

            sql = FQ.ToString();
                  
            UIClear();
            divVisible(false);
            BtnStatus("0000");
            UIEnabled(false);
            grdData.SelectedIndex = -1;
            cmd.CommandText = sql;
            FillGrid(cmd);
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
    #region DataItem Events

    public void UILang()
    {
        if (pgCs.LangEn)
        {

        }
        else
        {
            grdData.Columns[5].Visible = false;
        }

        if (pgCs.LangAr)
        {

        }
        else
        {
            grdData.Columns[4].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        ddlVacType.Enabled              = pStatus;
        ddlVacHospitalType.Enabled      = pStatus;
        ddlExcType.Enabled              = pStatus;
        txtType.Enabled                 = pStatus;
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
        calStartDate2.SetEnabled(pStatus);
        calEndDate2.SetEnabled(pStatus);
        txtEmpID2.Enabled               = pStatus;
        txtEmpName.Enabled              = pStatus;
        txtShiftID.Enabled              = pStatus;
        txtShiftName.Enabled            = pStatus;
        txtWorkTimeID.Enabled           = pStatus;
        txtWorkTimeName.Enabled         = pStatus;
        tpFrom.Enabled                  = pStatus;
        tpTo.Enabled                    = pStatus;
        txtDesc.Enabled                 = pStatus;   
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtID.Text = "";
        ddlVacType.SelectedIndex         = -1;
        ddlVacHospitalType.SelectedIndex = -1;
        ddlExcType.SelectedIndex         = -1;
        txtType.Text = "";
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        calStartDate2.ClearDate();
        calEndDate2.ClearDate();
        txtEmpID2.Text       = "";
        txtEmpName.Text      = "";
        txtShiftID.Text      = "";
        txtShiftName.Text    = "";
        txtWorkTimeID.Text   = "";
        txtWorkTimeName.Text = "";
        tpFrom.SetTime(00, 00,00);
        tpTo.SetTime(00, 00, 00);
        txtDesc.Text = "";

        BtnStatus("100");
        tpTimeSplit.SetTime(00, 00, 00);
        rdlSplit.SelectedIndex = 0;
        txtGapOrOVTID.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void divVisible(bool pStatus)
    {
        divVacType.Visible  = pStatus;
        divExcType.Visible  = pStatus;
        divType.Visible     = pStatus;
        divTimeFrom.Visible = pStatus;
        divTimeTo.Visible   = pStatus;
        divSplit.Visible    = pStatus;
        divbtnSplit.Visible = pStatus;
        divDate.Visible     = pStatus;
        divEndDate.Visible  = pStatus;
        divEmp2.Visible     = pStatus;
        divReason.Visible   = pStatus;
        divWorkTime.Visible = pStatus;
        divFileReq.Visible  = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnReqFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = ServerMapPath("RequestsFiles/") + txtReqFile.Text;
            string[] fileNameArr = txtReqFile.Text.Split('\\');
            string fileName = fileNameArr[fileNameArr.Length - 1];
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=" + MakeValidFileName(fileName));
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        catch (Exception ex) { }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Split Events

    protected void ShowSplit(string pType, string pErqID, string pEmpID)
    {
        try
        {
            DataTable DT1 = DBCs.FetchData(" SELECT * FROM EmpRequestApprovalStatus WHERE ReqID = @P1 AND EmpID = @P2 AND EraLevelSequenceNo = 0 AND @P3 IN (SELECT * FROM UF_CSVToTable(MgrID)) AND RetID = @P4 ", new string[] { pErqID,pEmpID,pgCs.LoginMainUser,pType });
            if (!DBCs.IsNullOrEmpty(DT1)) { divbtnSplit.Visible = true; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSplit_Click(object sender, EventArgs e)
    {
        try
        {
            tpTimeSplit.SetTime(tpFrom.getHours(), tpFrom.getMinutes(),tpFrom.getSeconds());
            rdlSplit.SelectedIndex = 0;
            BtnStatus("011");
            divSplit.Visible = true;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            DataTable DT = DBCs.FetchData(" SELECT * FROM Gap WHERE GapID = @P1 ", new string[] { txtGapOrOVTID.Text });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                DateTime updateS = DateTime.Now;
                DateTime InsertS = DateTime.Now;
                DateTime updateE = DateTime.Now;
                DateTime InsertE = DateTime.Now;

                if (rdlSplit.SelectedIndex == 0)
                {
                    updateS = tpFrom.getDateTime();
                    updateE = tpTimeSplit.getDateTime();
                    InsertS = (tpTimeSplit.getDateTime()).AddSeconds(1);
                    InsertE = tpTo.getDateTime();
                }
                else if (rdlSplit.SelectedIndex == 1)
                {
                    InsertS = tpFrom.getDateTime();
                    InsertE = tpTimeSplit.getDateTime();
                    updateS = (tpTimeSplit.getDateTime()).AddSeconds(1);
                    updateE = tpTo.getDateTime();
                }

                GapProCs.EmpID        = DT.Rows[0]["EmpID"].ToString();
                GapProCs.EwrID        = DT.Rows[0]["EwrID"].ToString();
                GapProCs.GapDate      = DT.Rows[0]["GapDate"].ToString();
                GapProCs.GapShift     = DT.Rows[0]["GapShift"].ToString();
                GapProCs.GapStartTime = InsertS.ToString();
                GapProCs.GapEndTime   = InsertE.ToString();
                GapProCs.GapAlert     = DT.Rows[0]["GapAlert"].ToString();
                GapProCs.UsrName      = pgCs.LoginMainUser;
                GapProCs.GapGraceFlag = DT.Rows[0]["GapGraceFlag"].ToString();
                GapProCs.GapDesc      = DT.Rows[0]["GapDesc"].ToString();
                GapProCs.GapType      = DT.Rows[0]["GapType"].ToString();
                GapSqlCs.Insert(GapProCs);
                GapProCs.GapID        = DT.Rows[0]["GapID"].ToString();
                GapProCs.GapStartTime = updateS.ToString();
                GapProCs.GapEndTime   = updateE.ToString();
                GapProCs.UsrName      = pgCs.LoginMainUser;
                GapSqlCs.Update(GapProCs);

                btnFilter_Click(null,null);
                CtrlCs.ShowSaveMsg(this);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        tpTimeSplit.SetTime(tpFrom.getHours(), tpFrom.getMinutes());
        rdlSplit.SelectedIndex = 0;
        BtnStatus("100");
        divSplit.Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) 
    {
        btnSplit.Enabled  = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
    }
 
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        DropDownList _ddlPager = CtrlCs.PagerList(grdData);
                        _ddlPager.SelectedIndexChanged += new EventHandler(ddlPager_SelectedIndexChanged);
                        Table pagerTable = e.Row.Cells[0].Controls[0] as Table;
                        pagerTable.Rows[0].Cells.Add(CtrlCs.PagerCell(_ddlPager));
                        break;
                    }
                 default:
                    {
                        e.Row.Cells[1].Visible  = false; //To hide ID column in grid view
                        e.Row.Cells[2].Visible  = false;
                        e.Row.Cells[3].Visible  = false;
                        e.Row.Cells[6].Visible  = false;
                        e.Row.Cells[7].Visible  = false;
                        e.Row.Cells[8].Visible  = false;
                        e.Row.Cells[9].Visible  = false;
                        e.Row.Cells[14].Visible = false;
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
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdData.PageIndex = 0;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton OkBtn     = (ImageButton)e.Row.FindControl("imgbtnOkPaid");
                        ImageButton CancelBtn = (ImageButton)e.Row.FindControl("imgbtnCancel");
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        AMSMasterPage master = (AMSMasterPage)this.Master;
        string empid = "";

        try
        {
            if (e.CommandName == "OkPaid" || e.CommandName == "Rejected")
            {
                ProCs.MgrID     = pgCs.LoginMainUser;

                string[] arrArgument = e.CommandArgument.ToString().Split(',');
                ProCs.RetID = arrArgument[0];
                ProCs.ReqID = arrArgument[1];
                empid       = arrArgument[2];

                switch (e.CommandName)
                {
                    case ("OkPaid"):
                        ProCs.EraStatus = "1";
                        SqlCs.ApprovalOrRejected(ProCs);
                        
                        btnFilter_Click(null,null);
                        master.ShowIsExistingRequest();
                        
                        break;

                    case ("Rejected"):
                        ProCs.EraStatus = "2";
                        SqlCs.ApprovalOrRejected(ProCs);

                        btnFilter_Click(null,null);
                        master.ShowIsExistingRequest();
                        
                        break;
                }
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
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataView dataView = new DataView(DT);

            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = "ASC";
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }
            else
            {
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }

            dataView.Sort = e.SortExpression + " " + sortDirection;

            grdData.DataSource = dataView;
            grdData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ConvertSortDirectionToSql(string sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;

            case "DESC":
                newSortDirection = "ASC";
                break;
        }

        return newSortDirection;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(false);

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text, grdData.SelectedRow.Cells[0].Text);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pErqID, string pEmpID)
    {
        try
        {
            divVisible(false);
            divDate.Visible   = true;
            divReason.Visible = true;

            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("ErqID =" + pErqID + "");

            txtID.Text   = DRs[0]["ErqID"].ToString();
            string RetID = DRs[0]["RetID"].ToString();
            txtDesc.Text = DRs[0]["ErqReason"].ToString();
            
            calStartDate.SetGDate(DRs[0]["ErqStartDate"], pgCs.DateFormat);
            
            if (RetID == "VAC")
            {
                divVacType.Visible = true;
                ddlVacType.SelectedIndex = ddlVacType.Items.IndexOf(ddlVacType.Items.FindByValue(DRs[0]["ErqTypeID"].ToString()));

                lblVacHospitalType.Visible = ddlVacHospitalType.Visible = false;

                if (pgCs.Version == "Al_JoufUN")
                {
                    if (DRs[0]["VacHospitalType"] != DBNull.Value)
                    {
                        if (DRs[0]["VacHospitalType"].ToString() != "N")
                        {
                            lblVacHospitalType.Visible = ddlVacHospitalType.Visible = true;
                            ddlVacHospitalType.SelectedIndex = ddlVacHospitalType.Items.IndexOf(ddlVacHospitalType.Items.FindByValue(DRs[0]["VacHospitalType"].ToString()));
                        }
                    }
                }
            }

            if (RetID == "VAC" || RetID == "SWP")
            {
                divEndDate.Visible = true;
                calEndDate.SetGDate(DRs[0]["ErqEndDate"], pgCs.DateFormat);
            }
            
            if (RetID == "EXC")
            {
                divExcType.Visible = true;
                ddlExcType.SelectedIndex = ddlExcType.Items.IndexOf(ddlExcType.Items.FindByValue(DRs[0]["ErqTypeID"].ToString()));
            }

            if (RetID == "EXC" || RetID == "OVT")
            {
                txtGapOrOVTID.Text = DRs[0]["GapOvtID"].ToString();

                divTimeFrom.Visible = true;
                divTimeTo.Visible = true;
                if (DRs[0]["ErqStartTime"] != DBNull.Value) { tpFrom.SetTime(Convert.ToDateTime(DRs[0]["ErqStartTime"])); }
                if (DRs[0]["ErqEndTime"]   != DBNull.Value) { tpTo.SetTime(Convert.ToDateTime(DRs[0]["ErqEndTime"])); }
                
                if (RetID == "EXC" ) { ShowSplit(RetID,pErqID,pEmpID); }
            }

            if (RetID == "ESH")
            {
                divWorkTime.Visible = true;
                divTimeFrom.Visible = true;
                divTimeTo.Visible   = true;
                txtWorkTimeID.Text  = DRs[0]["WktID"].ToString();
                txtShiftID.Text     = DRs[0]["ShiftID"].ToString();

                DataTable WDT = DBCs.FetchData(" SELECT * FROM WorkingTime WHERE WktID = @P1 ", new string[] { DRs[0]["WktID"].ToString() });
                if (!DBCs.IsNullOrEmpty(WDT))
                {
                    txtWorkTimeName.Text = WDT.Rows[0][General.Msg("WktNameEn","WktNameAr")].ToString();
                    txtShiftName.Text    = WDT.Rows[0]["WktShift" + DRs[0]["ShiftID"].ToString() + General.Msg("NameEn","NameAr")].ToString();
                    tpFrom.SetTime(Convert.ToDateTime(WDT.Rows[0]["WktShift" + DRs[0]["ShiftID"].ToString() + "From"]));
                    tpTo.SetTime(Convert.ToDateTime(WDT.Rows[0]["WktShift" + DRs[0]["ShiftID"].ToString() + "To"])); 
                }
            }

            if (!string.IsNullOrEmpty(DRs[0]["ErqReqFilePath"].ToString()))
            {
                divFileReq.Visible = true;
                txtReqFile.Text = DRs[0]["ErqReqFilePath"].ToString();
            }

            if (RetID == "SWP")
            {
                divFileReq.Visible = false;
                divType.Visible    = true;
                divEmp2.Visible    = true;

                if (DRs[0]["ErqTypeID"].ToString() == "1") { txtType.Text = "Working Day(s)"; } else if (DRs[0]["ErqTypeID"].ToString() == "2") { txtType.Text = "Off Day(s)"; }
                
                txtEmpID2.Text = DRs[0]["EmpID2"].ToString();
                txtEmpName.Text = DRs[0][General.Msg("Emp2NameEn","Emp2NameAr")].ToString();
                calStartDate2.SetGDate(DRs[0]["ErqStartDate2"], pgCs.DateFormat);
                calEndDate2.SetGDate(DRs[0]["ErqEndDate2"], pgCs.DateFormat);
            }

            if (RetID == "COM" || RetID == "JOB")
            {
                divVacType.Visible = false;
                divEndDate.Visible = true;
                ddlVacType.SelectedIndex = ddlVacType.Items.IndexOf(ddlVacType.Items.FindByValue(DRs[0]["ErqTypeID"].ToString()));
                calEndDate.SetGDate(DRs[0]["ErqEndDate"], pgCs.DateFormat);
            } 
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void TimeSplit_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvTimeSplit))
            {
                if (tpTo.getIntTime() >= tpFrom.getIntTime())
                {
                    if (tpTimeSplit.getIntTime() > tpFrom.getIntTime() && tpTimeSplit.getIntTime() < tpTo.getIntTime()) { e.IsValid = true; } else { e.IsValid = false; }
                    return;
                }
                else
                {
                    if (tpTimeSplit.getIntTime() == 240000) { e.IsValid = true; }
                    else if (tpTimeSplit.getIntTime() < 240000) { if (tpTimeSplit.getIntTime() > tpFrom.getIntTime()) { e.IsValid = true; } else { e.IsValid = false; } }
                    else if (tpTimeSplit.getIntTime() > 240000) { if (tpTimeSplit.getIntTime() < tpTo.getIntTime()) { e.IsValid = true; } else { e.IsValid = false; } }
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