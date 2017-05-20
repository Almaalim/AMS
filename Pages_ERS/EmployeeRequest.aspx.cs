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
using System.Data.SqlClient;

public partial class EmployeeRequest : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReqApprovalSql SqlCs = new ReqApprovalSql();
    EmpRequestSql EmpReqSqlCs = new EmpRequestSql();
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
            MainQuery += " SELECT * FROM EmpRequestInfo WHERE EmpID = '" + pgCs.LoginEmpID + "'";
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense(); 
                UIEnabled(false);
                UILang();
                FillList();
                btnFilter_Click(null, null);
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                divVisible(false);

                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DTCs.FindCurrentMonth()));
                ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DTCs.FindCurrentYear()));
                Session["AttendanceListMonth"] = ddlMonth.SelectedValue;
                Session["AttendanceListYear"]  = ddlYear.SelectedValue;
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
            DataTable DT = DBCs.FetchData("SELECT RetNameEn,RetNameAr FROM RequestType WHERE RetID = @P1 ", new string[] { pType.ToString() });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                return General.Msg(DT.Rows[0]["RetNameEn"].ToString(), DT.Rows[0]["RetNameAr"].ToString());
            }
            else { return ""; }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayReqType(object pReqTpe, object pID, object pLang)
    {
        try
        {
            DataTable Typedt = new DataTable();
            if (pReqTpe.ToString() == "EXC" || pReqTpe.ToString() == "ESH")
            {
                DataTable EDT = DBCs.FetchData("SELECT ExcNameAr,ExcNameEn FROM ExcuseType WHERE ExcID = @P1 ", new string[] { pID.ToString() });
                if (!DBCs.IsNullOrEmpty(EDT))
                {
                    return General.Msg(EDT.Rows[0]["ExcNameEn"].ToString(), EDT.Rows[0]["ExcNameAr"].ToString());
                }
                else { return ""; }
            }
            else if (pReqTpe.ToString() == "VAC")
            {
                DataTable VDT = DBCs.FetchData("SELECT VtpNameAr,VtpNameEn FROM VacationType WHERE VtpID = @P1 ", new string[] { pID.ToString() });
                if (!DBCs.IsNullOrEmpty(VDT))
                {
                    return General.Msg(VDT.Rows[0]["VtpNameEn"].ToString(), VDT.Rows[0]["VtpNameAr"].ToString());
                }
                else { return ""; }
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery;

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            FQ.Append(MainQuery);
            FQ.Append(" AND ErqStartDate BETWEEN @SDate AND @EDate ");
                
            cmd.Parameters.AddWithValue("@SDate", SDate);
            cmd.Parameters.AddWithValue("@EDate", EDate);
            sql = FQ.ToString();

            cmd.CommandText = sql;
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ServerMapPath(string path) { return HttpContext.Current.Server.MapPath(path); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static HttpResponse GetHttpResponse() { return HttpContext.Current.Response; }
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
            res.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        catch (Exception ex) { }
    }
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
        ddlVacType.SelectedIndex = -1;
        ddlVacHospitalType.SelectedIndex = -1;
        ddlExcType.SelectedIndex = -1;
        txtType.Text = "";
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        calStartDate2.ClearDate();
        calEndDate2.ClearDate();
        txtEmpID2.Text = "";
        txtEmpName.Text = "";
        txtShiftID.Text = "";
        txtShiftName.Text = "";
        txtWorkTimeID.Text = "";
        txtWorkTimeName.Text = "";
        tpFrom.ClearTime();
        tpTo.ClearTime();
        txtDesc.Text = "";
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
        divDate.Visible     = pStatus;
        divEndDate.Visible  = pStatus;
        divEmp2.Visible     = pStatus;
        divReason.Visible   = pStatus;
        divWorkTime.Visible = pStatus;
        divFileReq.Visible  = pStatus;
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
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[11].Visible = false;
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
                        string Status = e.Row.Cells[11].Text;
                        ImageButton StatusButton = (ImageButton)e.Row.Cells[12].Controls[1];      
                        if (Status == "0") { StatusButton.ImageUrl = "~/images/ERS_Images/Waiting.png"; }
                        else if (Status == "1") { StatusButton.ImageUrl = "~/images/ERS_Images/approve.png"; }
                        else if (Status == "2") { StatusButton.ImageUrl = "~/images/ERS_Images/reject.png"; }

                        ImageButton DeleteButton = (ImageButton)e.Row.Cells[13].Controls[1];
                        DataTable DT = DBCs.FetchData(" SELECT * FROM EmpRequestApprovalStatus WHERE EraStatus IN (1,2) AND ReqID ", new string[] { e.Row.Cells[1].Text });
                        if (!DBCs.IsNullOrEmpty(DT)) { DeleteButton.Enabled = false; } else { DeleteButton.Enabled = true; }

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
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string ID = e.CommandArgument.ToString();
                    
                    DataTable DT = DBCs.FetchData(" SELECT * FROM EmpRequestApprovalStatus WHERE EraStatus IN (1,2) AND ReqID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }
                    
                    bool delete = EmpReqSqlCs.Delete(ID, pgCs.LoginID);
                    if (delete) { SqlCs.Delete(ID, pgCs.LoginID); }
                    
                    btnFilter_Click(null,null);

                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
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
            divVisible(false);

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM EmpRequestInfo WHERE ErqID = @P1 ", new string[] { pID });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                string RetID = DT.Rows[0]["RetID"].ToString();
                divDate.Visible   = true;
                divReason.Visible = true;

                calStartDate.SetGDate(DT.Rows[0]["ErqStartDate"], pgCs.DateFormat);
                
                txtDesc.Text = DT.Rows[0]["ErqReason"].ToString();

                if (RetID == "VAC")
                {
                    divVacType.Visible = true;
                    ddlVacType.SelectedIndex = ddlVacType.Items.IndexOf(ddlVacType.Items.FindByValue(DT.Rows[0]["ErqTypeID"].ToString()));

                    lblVacHospitalType.Visible = ddlVacHospitalType.Visible = false;

                    if (pgCs.Version == "Al_JoufUN")
                    {
                        if (DT.Rows[0]["VacHospitalType"] != DBNull.Value)
                        {
                            if (DT.Rows[0]["VacHospitalType"].ToString() != "N")
                            {
                                lblVacHospitalType.Visible = ddlVacHospitalType.Visible = true;
                                ddlVacHospitalType.SelectedIndex = ddlVacHospitalType.Items.IndexOf(ddlVacHospitalType.Items.FindByValue(DT.Rows[0]["VacHospitalType"].ToString()));
                            }
                        }
                    }
                }

                if (RetID == "VAC" || RetID == "SWP")
                {
                    divEndDate.Visible = true;
                    calEndDate.SetGDate(DT.Rows[0]["ErqEndDate"], pgCs.DateFormat);
                }

                if (RetID == "EXC")
                {
                    divExcType.Visible = true;
                    ddlExcType.SelectedIndex = ddlExcType.Items.IndexOf(ddlExcType.Items.FindByValue(DT.Rows[0]["ErqTypeID"].ToString()));
                }

                if (RetID == "EXC" || RetID == "OVT")
                {
                    txtGapOrOVTID.Text = DT.Rows[0]["GapOvtID"].ToString();

                    divTimeFrom.Visible = true;
                    if (DT.Rows[0]["ErqStartTime"] != DBNull.Value) { tpFrom.SetTime(Convert.ToDateTime(DT.Rows[0]["ErqStartTime"])); }
                    if (DT.Rows[0]["ErqEndTime"]   != DBNull.Value) { tpTo.SetTime(Convert.ToDateTime(DT.Rows[0]["ErqEndTime"])); }
                }

                if (RetID == "ESH")
                {
                    divWorkTime.Visible = true;
                    divTimeFrom.Visible = true;
                    txtWorkTimeID.Text = DT.Rows[0]["WktID"].ToString();
                    txtShiftID.Text    = DT.Rows[0]["ShiftID"].ToString();

                    string Q = " SELECT WktNameAr,WktNameEn,WktShift1NameAr,WktShift1NameEn,WktShift2NameAr,WktShift2NameEn,WktShift3NameAr,WktShift3NameEn "
                             + " ,WktShift1From,WktShift1To,WktShift2From,WktShift2To,WktShift3From,WktShift3To"
                             + " FROM WorkingTime "
                             + " WHERE WktID = @P1 ";
                    DataTable SDT = DBCs.FetchData(Q, new string[] { DT.Rows[0]["WktID"].ToString() });
                    if (!DBCs.IsNullOrEmpty(SDT)) 
                    {
                        txtWorkTimeName.Text = General.Msg(SDT.Rows[0]["WktNameEn"].ToString(), SDT.Rows[0]["WktNameAr"].ToString());
                        txtShiftName.Text = General.Msg(SDT.Rows[0]["WktShift" + DT.Rows[0]["ShiftID"].ToString() + "NameEn"].ToString(), SDT.Rows[0]["WktShift" + DT.Rows[0]["ShiftID"].ToString() + "NameAr"].ToString());
                        tpFrom.SetTime(Convert.ToDateTime(SDT.Rows[0]["WktShift" + DT.Rows[0]["ShiftID"].ToString() + "From"]));
                        tpTo.SetTime(Convert.ToDateTime(SDT.Rows[0]["WktShift" + DT.Rows[0]["ShiftID"].ToString() + "To"]));
                    }
                }

                if (!string.IsNullOrEmpty(DT.Rows[0]["ErqReqFilePath"].ToString()))
                {
                    divFileReq.Visible = true;
                    txtReqFile.Text = DT.Rows[0]["ErqReqFilePath"].ToString();
                }


                if (RetID == "SWP")
                {
                    divFileReq.Visible = false;
                    divType.Visible = true;
                    divEmp2.Visible = true;

                    if (DT.Rows[0]["ErqTypeID"].ToString() == "1") { txtType.Text = "Working Day(s)"; } else if (DT.Rows[0]["ErqTypeID"].ToString() == "2") { txtType.Text = "Off Day(s)"; }

                    txtEmpID2.Text = DT.Rows[0]["EmpID2"].ToString();
                    if (pgCs.Lang == "AR") { txtEmpName.Text = DT.Rows[0]["Emp2NameAr"].ToString(); } else { txtEmpName.Text = DT.Rows[0]["Emp2NameEn"].ToString(); }

                    calStartDate2.SetGDate(DT.Rows[0]["ErqStartDate2"], pgCs.DateFormat);
                    calEndDate2.SetGDate(DT.Rows[0]["ErqEndDate2"], pgCs.DateFormat);
                }

                if (RetID == "COM" || RetID == "JOB")
                {
                    divVacType.Visible = false;
                    divEndDate.Visible = true;
                    ddlVacType.SelectedIndex = ddlVacType.Items.IndexOf(ddlVacType.Items.FindByValue(DT.Rows[0]["ErqTypeID"].ToString()));
                    calEndDate.SetGDate(DT.Rows[0]["ErqEndDate"], pgCs.DateFormat);
                }
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}