using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Elmah;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class EmployeeVacation : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpVacPro ProCs = new EmpVacPro();
    EmpVacSql SqlCs = new EmpVacSql();
    EmpRequestSql ReqSqlCs = new EmpRequestSql();
    VactionDaysCal VacCs = new VactionDaysCal();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM EmployeeVactionInfoView WHERE VtpCategory = 'VAC' ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            MainQuery += " AND DepID IN (" + pgCs.DepList + ") ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
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
            CtrlCs.FillVacationTypeList(ddlVacType, rvVacType, true, "VAC");
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            UIClear();

            if (ddlFilter.Text == "EmpID") 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " = @P1";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            }
            else 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1";
                cmd.Parameters.AddWithValue("@P1", "%" + txtFilter.Text.Trim() + "%");
            }
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        cmd.CommandText = sql;
        FillGrid(cmd);
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
            grdData.Columns[3].Visible = false;
            grdData.Columns[5].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameEn").Enabled = false;
            ddlFilter.Items.FindByValue("VtpNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[4].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameAr").Enabled = false;
            ddlFilter.Items.FindByValue("VtpNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        txtEmpID.Enabled = pStatus;
        ddlVacType.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
        txtAvailable.Enabled = pStatus;
        txtPhoneNo.Enabled = pStatus;
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.EvrID = txtID.Text; }

            ProCs.EmpIDs          = txtEmpID.Text;
            if (ddlVacType.SelectedIndex > 0) { ProCs.VtpID = ddlVacType.SelectedValue; }
            ProCs.EvrStartDate    = calStartDate.getGDateDBFormat();
            ProCs.EvrEndDate      = calEndDate.getGDateDBFormat();
            ProCs.EvrDesc         = txtDesc.Text;
            ProCs.EvrAvailability = txtAvailable.Text;
            ProCs.EvrPhone        = txtPhoneNo.Text;

            ProCs.EvrAddBy      = "USR"; // 'USR' = FROM USER, 'REQ' = FROM Request, 'EXC' = FROM Execuse Premit, 'INT' = FROM Emport Data
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtID.Text = "";
        txtEmpID.Text = "";
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        txtAvailable.Text = "";
        txtDesc.Text = "";
        txtPhoneNo.Text = "";

        ddlVacType.Show(DDLAttributes.DropDownListAttributes.ShowType.ALL);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        UIClear();
        ddlVacType.Show(DDLAttributes.DropDownListAttributes.ShowType.ActiveOnly);
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        txtEmpID.Enabled = false;
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (commandName == "ADD") { SqlCs.Insert_WithUpdateSummary(ProCs); }

            btnFilter_Click(null,null);
            CtrlCs.ShowSaveMsg(this);
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
        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
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
                        e.Row.Cells[1].Visible = false; //To hide ID column in grid view
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
                        ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        Hashtable ht = (Hashtable)ViewState["ht"];
                        if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                        delBtn.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
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
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM EmpVacRel WHERE EvrAddBy IN ('REQ', 'INT') AND EvrID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }

                    SqlCs.Delete_WithUpdateSummary(ID, pgCs.LoginID);

                    btnFilter_Click(null,null);

                    CtrlCs.ShowDelMsg(this, true);
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
            BtnStatus("1000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
                BtnStatus("1100");
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
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("EvrID =" + pID + "");

            txtID.Text               = DRs[0]["EvrID"].ToString();
            txtEmpID.Text            = DRs[0]["EmpID"].ToString();
            ddlVacType.SelectedIndex = ddlVacType.Items.IndexOf(ddlVacType.Items.FindByValue(DRs[0]["VtpID"].ToString()));
            txtAvailable.Text        = DRs[0]["EvrAvailability"].ToString();
            txtDesc.Text             = DRs[0]["EvrDesc"].ToString();
            txtPhoneNo.Text          = DRs[0]["EvrPhone"].ToString();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            calStartDate.SetGDate(DRs[0]["EvrStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["EvrEndDate"], pgCs.DateFormat);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(SqlCommand cmd)
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
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

    protected void MaxDays_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvMaxDays) && !string.IsNullOrEmpty(txtEmpID.Text) && ddlVacType.SelectedIndex > 0 && !String.IsNullOrEmpty(calStartDate.getGDate()) && !String.IsNullOrEmpty(calEndDate.getGDate()))
            {
                int iStartDate = DTCs.ConvertDateTimeToInt(calStartDate.getGDate(), "Gregorian");
                int iEndDate   = DTCs.ConvertDateTimeToInt(calEndDate.getGDate(), "Gregorian");

                if (iStartDate <= iEndDate)
                {
                    bool NestingDates = VacCs.FindNestingDates("Gregorian", calStartDate.getGDate(), calEndDate.getGDate(), txtEmpID.Text);
                    if (!NestingDates)
                    {
                        int VacDays = VacCs.FindVacDays(pgCs.DateType, txtEmpID.Text, ddlVacType.SelectedValue, true);
                        if (VacDays == -1)
                        {
                            CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("Reset date has not been entered for vacations, or an error occurred please contact your system administrator", "لم يتم إدخال تاريخ حساب الإجازات، أو حدث خطأ ما الرجاء الاتصال بمدير النظام"));
                            e.IsValid = false;
                        }
                        else
                        {
                            int MaxDays        = VacCs.FindMaxDays(txtEmpID.Text, ddlVacType.SelectedValue);
                            int VacDaysRequest = VacCs.FindVacDaysRequest("Gregorian", calStartDate.getGDate(), calEndDate.getGDate());
                            if (MaxDays == 0)
                            {
                                e.IsValid = true;
                            }
                            else if (VacDays + VacDaysRequest > MaxDays)
                            {
                                CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("I have reached the maximum number of days for this type of vacations, " + "<br />" + " you can not submit a new request", "لقد بلغت الحد الأقصى من الأيام لهذا النوع من الإجازات،لا يمكنك تقديم طلب جديد"));
                                e.IsValid = false;
                            }
                        }
                    }
                    else
                    {
                        CtrlCs.ValidMsg(this, ref cvMaxDays, true, General.Msg("There are other vacations on the date specified Please choose another date", "يوجد إجازة أخرى في التاريخ المحدد الرجاء اختيار تاريخ آخر"));
                        e.IsValid = false;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     protected void EmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtEmpID.Text))
            {
                CtrlCs.ValidMsg(this, ref cvEmpID, false, General.Msg("Emloyee ID is required", "رقم الموظف مطلوب"));
                e.IsValid = false;
            }
            else
            {
                CtrlCs.ValidMsg(this, ref cvEmpID, true, General.Msg("Employee ID does not exist", "رقم الموظف غير موجود"));
                if (!GenCs.isEmpID(txtEmpID.Text, pgCs.DepList)) { e.IsValid = false; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Days_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //if (source.Equals(cvDays))
        //{
        //    if (!String.IsNullOrEmpty(calStartDate.txtSelectedDate.Text) && !String.IsNullOrEmpty(calEndDate.txtSelectedDate.Text))
        //    {
        //        bool WorkDays = IsWorkDays(calStartDate.txtSelectedDate.Text, calEndDate.txtSelectedDate.Text, loginUserName);
        //        if (!WorkDays)
        //        {
        //            cvDays.ErrorMessage = General.Msg("You do not have a Worktime in the given period", "لا يوجد لديك عمل في الفترة المحددة");
        //            cvDays.Text = Server.HtmlDecode("&lt;img src='images/message_exclamation.png' title='You do not have a Worktime in the given period' /&gt;");
        //            e.IsValid = false;
        //        }
        //        e.IsValid = true;
        //    }
        //}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsWorkDays(string pStartDate, string pEndDate, string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime StartDate = DTCs.ConvertToDatetime(pStartDate, pgCs.DateType);
            DateTime EndDate   = DTCs.ConvertToDatetime(pEndDate, pgCs.DateType);
            DateTime Date = StartDate;
            int Days = Convert.ToInt32((EndDate - StartDate).TotalDays + 1);

            for (int i = 0; i < Days; i++)
            {
                Date = StartDate.AddDays(i);

                DataTable DT = ReqSqlCs.FetchWorkTime(Date, pEmpID, true);
                if (DBCs.IsNullOrEmpty(DT)) { return false; } 
            }
            return true;
        }
        catch (Exception ex) { return false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}