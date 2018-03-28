using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Elmah;
using System.Data.SqlClient;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using System.IO;
using System.Xml;
using Ionic.Zip;
using System.Web;

public partial class Reports : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReportPro ProCs = new ReportPro();
    ReportSql SqlCs = new ReportSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun DTCs = new DTFun();
    ReportFun RepCs = new ReportFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Param_DepIDs
    //Param_Lang
    //Param_CatIDs
    //Param_EmpIDs
    //Param_FromDate
    //Param_ToDate

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected override void InitializeCulture()
    //{
    //    //UICulture = CurrentCulture;
    //    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //    //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CurrentCulture);
    //    //base.InitializeCulture();
    //}
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            /*** Fill Session ************************************/

            if (ViewState["pnlshow"] != null)
            {
                if (!string.IsNullOrEmpty(hdnShow.Value))
                {
                    //string ss = hdnShow.Value;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hidePopup('','" + pnlDate.ClientID + "','" + pnlDateFromTo.ClientID + "');", true);
                    //hdnShow.Value = ss;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "showPopup('" + DivPopup.ClientID + "','" + pnlDate.ClientID + "','" + pnlDateFromTo.ClientID + "','" + hdnShow.Value + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key33", "hidePopup('','" + pnlDate.ClientID + "','" + pnlDateFromTo.ClientID + "');", true);
                }
            }

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();
                string QS = "";
                if (Request.QueryString.Count != 0) { QS = "?" + Request.QueryString.ToString(); }
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getReportPerm(Request.Url.AbsolutePath + QS);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key55", "hidePopup('" + DivPopup.ClientID + "','" + pnlDate.ClientID + "','" + pnlDateFromTo.ClientID + "');", true);
                FillList();
                /*** Common Code ************************************/

                trvDept.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
                ViewState["grpRep"] = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"] : "";

                if (ViewState["grpRep"].ToString() == "F") { }

                FillReport();

                btnEventsEnable(false);
                dltReport.RepeatColumns = 4;
                ShowPanels();

                calDate.SetEnabled(true);
                calStartDate.SetEnabled(true);
                calEndDate.SetEnabled(true);
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

            CtrlCs.FillMachineList(ref ddlLocation, null, false, true, "A", "A");
            CtrlCs.FillWorkTypeList(ref ddlWtpID, null, false);
            CtrlCs.FillCategoryList(ref ddlCatName, null, true);
            CtrlCs.FillExcuseTypeList(ref ddlExcType, null, false, true);
            CtrlCs.FillVacationTypeList(ref ddlVacType, null, false, true, "ALL");

            FillDepTree();

            ddlUserName.Items.Clear();
            DataTable PDT = DBCs.FetchData(new SqlCommand("SELECT * FROM ViewAppUser_PermissionGroup WHERE ISNULL(GrpDeleted,0) = 0 AND UsrStatus = 'True'"));
            if (!DBCs.IsNullOrEmpty(PDT)) { CtrlCs.PopulateDDL(ddlUserName, PDT, General.Msg("UsrName", "UsrName"), "UsrName", General.Msg("-Select User Name -", "-اختر اسم المستخدم-")); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillReport()
    {
        StringBuilder RQ = new StringBuilder();
        RQ.Append(" SELECT * FROM Report WHERE RgpID = @P1 AND RepVisible = 'True' ");
        RQ.Append(" AND (CHARINDEX('General',VerID) > 0 OR CHARINDEX(@P2,VerID) > 0) ");
        RQ.Append(" AND RepID NOT IN (SELECT RepGeneralID FROM Report WHERE RepGeneralID IS NOT NULL AND CHARINDEX(@P2,VerID) > 0)");

        DataTable DT = DBCs.FetchData(RQ.ToString(), new string[] { ViewState["grpRep"].ToString(), pgCs.Version });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            dltReport.DataSource = DT;
            dltReport.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowPanels()
    {
        pnlMonth.Visible = false;
        pnlWorkTime.Visible = false;
        pnlMachine.Visible = false;
        pnlCategory.Visible = false;
        pnlUsers.Visible = false;
        pnlVacType.Visible = false;
        pnlExcType.Visible = false;
        pnlDepartmnets.Visible = false;
        pnlEmployee.Visible = false;
        pnlDaysCount.Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Clear()
    {
        lblRepTitel.Text = "";
        lblRepDesc.Text  = "";
        calDate.ClearDate();
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        txtArWorkTimeName.Text = "";
        txtEnWorkTimeName.Text = "";
        ddlWtpID.SelectedIndex = -1;
        ddlLocation.SelectedIndex = -1;
        ddlCatName.SelectedIndex = -1;
        ddlUserName.SelectedIndex = -1;
        ddlVacType.SelectedIndex = -1;
        ddlExcType.SelectedIndex = -1;
        FillDepTree();
        ucEmployeeSelected.ClearAll();
        ddlMonth.SelectedIndex = -1;
        ddlYear.SelectedIndex = -1;
        txtDaysCount.Text = "20";

        FavClear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckBitWise(int panelPermission, int permission) { if ((panelPermission | permission) == panelPermission) { return true; } else { return false; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dltReport_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string RepID = e.CommandArgument.ToString();
        ViewState["RepID"] = Session["RepID"] = RepID;

        /////////////////Change Style
        //foreach (DataListItem item in dltReport.Items)
        //{
        //    System.Web.UI.WebControls.LinkButton lnkRef = (System.Web.UI.WebControls.LinkButton)item.FindControl("lnkBtnEn");
        //    System.Web.UI.WebControls.Image imgRef = (System.Web.UI.WebControls.Image)item.FindControl("Image1");
        //    if (lnkRef != null && imgRef != null)
        //    {
        //        lnkRef.Font.Bold = false;
        //    }
        //}

        //System.Web.UI.WebControls.LinkButton lnkCurrRef = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("lnkBtnEn");
        //System.Web.UI.WebControls.Image imgCurrRef = (System.Web.UI.WebControls.Image)e.Item.FindControl("Image1");
        //lnkCurrRef.Font.Bold = true;
        /////////////////
        string pnlshow = "";
        Clear();
        calDate.SetValidationEnabled(false);
        calStartDate.SetValidationEnabled(false);
        calEndDate.SetValidationEnabled(false);

        string M = Convert.ToInt16(DTCs.FindCurrentMonth()).ToString();
        ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(M));
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DTCs.FindCurrentYear()));

        ViewState["pnlshow"] = null;

        RepParametersPro RepProCs = new RepParametersPro();
        RepProCs.RepID = RepID;
        RepProCs.RepLang = pgCs.Lang;
        RepProCs = RepCs.GetReportInfo(RepProCs);

        lblRepTitel.Text = RepProCs.RepName;
        lblRepDesc.Text  = RepProCs.RepDesc;

        int RepPanels = Convert.ToInt32(RepProCs.RepPanels);
        if (CheckBitWise(RepPanels, 1)) { pnlshow = pnlDate.ClientID; /**/ calDate.SetValidationEnabled(true); }
        if (CheckBitWise(RepPanels, 2)) { pnlshow = pnlDateFromTo.ClientID; /**/ calStartDate.SetValidationEnabled(true); /**/ calEndDate.SetValidationEnabled(true); }
        pnlWorkTime.Visible = CheckBitWise(RepPanels, 8192);
        pnlMachine.Visible = CheckBitWise(RepPanels, 128);
        pnlEmployee.Visible = CheckBitWise(RepPanels, 32);
        pnlDepartmnets.Visible = CheckBitWise(RepPanels, 64); /**/ if (pnlDepartmnets.Visible) { FillDepTree(); }
        pnlCategory.Visible = CheckBitWise(RepPanels, 256);
        pnlUsers.Visible = CheckBitWise(RepPanels, 512);
        pnlMonth.Visible = CheckBitWise(RepPanels, 8);
        if (CheckBitWise(RepPanels, 4)) { pnlshow = pnlDate.ClientID; /**/ calDate.SetEnabled(false); /**/ calDate.SetValidationEnabled(true); /**/ calDate.SetTodayDate(); }
        pnlVacType.Visible = CheckBitWise(RepPanels, 1024);
        pnlExcType.Visible = CheckBitWise(RepPanels, 2048);
        pnlDaysCount.Visible = CheckBitWise(RepPanels, 4096);

        btnEventsEnable(true);

        hdnShow.Value = pnlshow;
        ViewState["pnlshow"] = pnlshow;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key22", "showPopup('" + DivPopup.ClientID + "','" + pnlDate.ClientID + "','" + pnlDateFromTo.ClientID + "','" + pnlshow + "');", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dltReport_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        //{
        //    e.Item.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dltReport, "Select$" + e.Item.ItemIndex);
        //}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getRepName() { return General.Msg("dataitem.RepNameEn", "dataitem.RepNameAr"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void btnEventsEnable(bool status)
    {
        btnSetAsDefault.Enabled = status;
        btnShow.Enabled = status;
        btnAddFavorite.Enabled = status;
        btnExportRecord.Enabled = status;
        btnEditReport.Enabled = status;
        //btnExport.Enabled = status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string WorkTime()
    {
        ViewState["WorkTimeParam"] = "";

        if (txtEnWorkTimeName.Text.Trim() == "" && txtArWorkTimeName.Text.Trim() == "" && ddlWtpID.SelectedIndex < 1) { return ""; }

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT WorkingTime.WktID, WorkType.WtpID FROM WorkingTime, WorkType WHERE 1 = 1 ");
        if (txtEnWorkTimeName.Text.Trim() != "") { FQ.Append(" AND WorkingTime.WktNameEn LIKE @NameEn "); /**/ cmd.Parameters.AddWithValue("@NameEn", txtEnWorkTimeName.Text.Trim() + "%"); }
        if (txtArWorkTimeName.Text.Trim() != "") { FQ.Append(" AND WorkingTime.WktNameAr LIKE @NameAr "); /**/ cmd.Parameters.AddWithValue("@NameAr", txtArWorkTimeName.Text.Trim() + "%"); }
        if (ddlWtpID.SelectedIndex > 0) { FQ.Append(" AND WorkType.WtpID = @WtpID "); /**/ cmd.Parameters.AddWithValue("@WtpID", ddlWtpID.SelectedValue); }
        FQ.Append(" AND ISNULL(WorkingTime.WktDeleted,0) = 0 AND WorkType.WtpInitial <> 'RO' ");

        cmd.CommandText = FQ.ToString();

        DataTable DT = DBCs.FetchData(cmd);

        ViewState["WorkTimeParam"] = GenCs.CreateIDsNumber("WktID", DT);

        return ViewState["WorkTimeParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Machine()
    {
        ViewState["MachineParam"] = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT MacID FROM Machine WHERE 1 = 1 ");
        if (ddlLocation.SelectedIndex > 0) { FQ.Append(" AND MacID LIKE @ID "); /**/ cmd.Parameters.AddWithValue("@ID", ddlLocation.SelectedValue); }
        FQ.Append(" AND ISNULL(MacDeleted,0) = 0 ");

        cmd.CommandText = FQ.ToString();
        DataTable DT = DBCs.FetchData(cmd);

        ViewState["MachineParam"] = GenCs.CreateIDsNumber("MacID", DT);

        return ViewState["MachineParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Employee()
    {
        DataTable EmployeeInsertdt = ucEmployeeSelected.getEmpSelected();
        //return GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt);
        return GenCs.CreateIDsString("EmpID", EmployeeInsertdt);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Department()
    {
        ViewState["DepartmentParam"] = "";

        string depID = "";
        if (trvDept.CheckedNodes.Count > 0)
        {
            foreach (TreeNode node in trvDept.CheckedNodes) { if (string.IsNullOrEmpty(depID)) { depID += node.Value; } else { depID += "," + node.Value; } }
        }

        ViewState["DepartmentParam"] = depID;
        return ViewState["DepartmentParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Category()
    {
        ViewState["CategoryParam"] = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT CatID FROM Category WHERE 1 = 1 ");
        if (ddlCatName.SelectedIndex > 0) { FQ.Append(" AND CatID = @ID "); /**/ cmd.Parameters.AddWithValue("@ID", ddlCatName.SelectedValue); }
        FQ.Append(" AND ISNULL(CatDeleted,0) = 0 ");

        cmd.CommandText = FQ.ToString();
        DataTable DT = DBCs.FetchData(cmd);

        ViewState["CategoryParam"] = GenCs.CreateIDsNumber("CatID", DT);
        return ViewState["CategoryParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Permission()
    {
        string permID = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT GrpPermissions FROM ViewAppUser_PermissionGroup WHERE UsrName = @UsrName AND ISNULL(GrpDeleted,0) = 0");

        cmd.Parameters.AddWithValue("@UsrName", ddlUserName.SelectedValue);
        cmd.CommandText = FQ.ToString();

        DataTable DT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(DT))
        {
            string cipherText;
            cipherText = DT.Rows[0][0].ToString();
            permID = CryptorEngine.Decrypt(cipherText, true);
        }

        return permID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string VacationType()
    {
        ViewState["VacationParam"] = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT VtpID FROM VacationType WHERE 1 = 1 ");
        if (ddlVacType.SelectedIndex > 0) { FQ.Append(" AND VtpID LIKE @ID "); /**/ cmd.Parameters.AddWithValue("@ID", ddlVacType.SelectedValue); }

        cmd.CommandText = FQ.ToString();
        DataTable DT = DBCs.FetchData(cmd);
        ViewState["VacationParam"] = GenCs.CreateIDsNumber("VtpID", DT);

        return ViewState["VacationParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string ExcuseType()
    {
        ViewState["ExcuseParam"] = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();
        FQ.Append(" SELECT ExcID FROM ExcuseType WHERE 1 = 1 ");
        if (ddlExcType.SelectedIndex > 0) { FQ.Append(" AND ExcID LIKE @ID "); /**/ cmd.Parameters.AddWithValue("@ID", ddlExcType.SelectedValue); }

        cmd.CommandText = FQ.ToString();
        DataTable DT = DBCs.FetchData(cmd);

        ViewState["ExcuseParam"] = GenCs.CreateIDsNumber("ExcID", DT);
        return ViewState["ExcuseParam"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Report Events

    protected void FillParameter(string RepID)
    {
        ViewState["RepProCs"] = "";

        RepParametersPro RepProCs = new RepParametersPro();
        RepProCs.RepID = RepID;
        RepProCs.RepUser = pgCs.LoginID;
        RepProCs.DateType = pgCs.DateType;
        RepProCs.RepLang = pgCs.Lang;

        RepProCs = RepCs.GetReportInfo(RepProCs);

        if (pnlDate.Visible && !string.IsNullOrEmpty(calDate.getGDate())) { RepProCs.Date = calDate.getGDateDefFormat(); }
        if (pnlDateFromTo.Visible && !string.IsNullOrEmpty(calStartDate.getGDate())) { RepProCs.DateFrom = calStartDate.getGDateDefFormat(); }
        if (pnlDateFromTo.Visible && !string.IsNullOrEmpty(calEndDate.getGDate())) { RepProCs.DateTo = calEndDate.getGDateDefFormat(); }

        if (pnlMonth.Visible && ddlMonth.SelectedIndex >= 0)
        {
            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

            RepProCs.DateFrom = SDate.ToString("MM/dd/yyyy");
            RepProCs.DateTo   = EDate.ToString("MM/dd/yyyy");

            RepProCs.MonthDate = ddlMonth.SelectedValue;
            RepProCs.YearDate  = ddlYear.SelectedValue;
        }

        if (pnlWorkTime.Visible) { RepProCs.WktID = ViewState["WorkTimeParam"].ToString(); }
        if (pnlMachine.Visible) { RepProCs.MacID = ViewState["MachineParam"].ToString(); }
        if (pnlEmployee.Visible) { RepProCs.EmpID = Employee(); }
        if (pnlDepartmnets.Visible) { RepProCs.DepID = ViewState["DepartmentParam"].ToString(); }
        if (pnlCategory.Visible) { RepProCs.CatID = ViewState["CategoryParam"].ToString(); }
        if (pnlUsers.Visible) { RepProCs.UsrName = ddlUserName.SelectedValue.ToString(); RepProCs.Permissions = Permission(); }
        if (pnlVacType.Visible) { RepProCs.VtpID = ViewState["VacationParam"].ToString(); }
        if (pnlExcType.Visible) { RepProCs.ExcID = ViewState["ExcuseParam"].ToString(); }
        if (pnlDaysCount.Visible) { RepProCs.DaysCount = txtDaysCount.Text; }

        ViewState["RepProCs"] = RepProCs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Session["RepProCs"] = null;
            if (!CtrlCs.PageIsValid(this, vsShow)) { return; }
            FillParameter(ViewState["RepID"].ToString());

            Session["RepProCs"] = ViewState["RepProCs"];
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }

        Response.Redirect(@"~/Pages_Report/ReportViewer.aspx");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsShow)) { return; }

            FillParameter(ViewState["RepID"].ToString());

            RepParametersPro RepProCs = new RepParametersPro();
            RepProCs = (RepParametersPro)ViewState["RepProCs"];

            StiReport Rep = RepCs.CreateReport(RepProCs);
            //Rep.Print(false,System.Drawing.Printing.PrinterSettings.);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnEditReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["RepID"])) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("Please Select Report to edit it", "رجاء حدد تقرير للتعديل")); return; }

            Session["RDRepID"] = ViewState["RepID"].ToString();
            Session["RDRgpID"] = ViewState["grpRep"].ToString();
            Response.Redirect(@"~/Pages_Report/ReportDesigner.aspx");

            //string RepID = ViewState["RepID"].ToString();
            //string RepTemp = "";

            //DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { RepID });
            //if (!DBCs.IsNullOrEmpty(DT)) { RepTemp = General.Msg(DT.Rows[0]["RepTempEn"].ToString(), DT.Rows[0]["RepTempAr"].ToString()); }

            //if (string.IsNullOrEmpty(RepTemp)) { return; }

            //StiReport Rep = new StiReport();
            //Rep.LoadFromString(RepTemp);
            //Rep.Dictionary.Databases.Clear();
            //Rep.Dictionary.Databases.Add(new StiSqlDatabase("Connection", General.ConnString));
            //Rep.Dictionary.Synchronize();
            //Rep.Compile();

            //string Lang = Convert.ToString(Session["Language"]) == "AR" ? "Ar" : "En"; 
            //StiWebDesigner1.Localization = string.Format("Localization/{0}.xml", Lang);
            //StiWebDesigner1.Report = Rep;


            //StiWebDesigner1.Design(Rep);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSetAsDefault_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["RepID"])) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("Please Select Report to edit it", "رجاء حدد تقرير للتعديل")); return; }

            string RepID = ViewState["RepID"].ToString();
            string RepTemp = "";

            DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { RepID });
            if (!DBCs.IsNullOrEmpty(DT)) { RepTemp = General.Msg(DT.Rows[0]["RepTempDefEn"].ToString(), DT.Rows[0]["RepTempDefAr"].ToString()); }

            ProCs.RepID = RepID;
            ProCs.RepTemp = RepTemp;
            ProCs.RepLang = pgCs.Lang;
            ProCs.TransactionBy = pgCs.LoginID;

            SqlCs.Report_Update_DefTemplate(ProCs);
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Report Updated Successfully", "تم تحديث التقرير بنجاح"));
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Viewer Events

    //protected void StiWebDesigner1_PreInit(object sender, StiWebDesigner.StiPreInitEventArgs e)
    //{
    //    pgCs.FillLangSession();
    //    e.WebDesigner.LocalizationDirectory = "Localization";
    //    e.WebDesigner.Localization = pgCs.Lang.ToLower();
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void StiWebDesigner1_SaveReport(object sender, StiSaveReportEventArgs e)
    {
        StiReport Rep = e.Report;
        string RepTemp = e.Report.SaveToString().ToString();

        ProCs.RepID = Session["RepID"].ToString();
        ProCs.RepTemp = RepTemp;
        ProCs.RepLang = pgCs.Lang;
        ProCs.TransactionBy = Session["UserName"].ToString();

        try
        {
            SqlCs.Report_Update_Template(ProCs);
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Report Updated Successfully", "تم تحديث التقرير بنجاح"));
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Import-Upload Events

    protected void btnUploadReport_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(fileUpload.PostedFile.FileName.ToString())) { return; }

        string reportString = string.Empty;

        // Split on directory separator
        string FileName = fileUpload.PostedFile.FileName.ToString();
        string[] parts = FileName.Split('\\');
        //remove .mrt extention from report name
        string[] repName = parts[parts.Length - 1].ToString().Split('.');
        string reportName = repName[0].ToString();

        string path = string.Empty;

        string DirRepPath = Server.MapPath("TempFiles\\Reports\\Import\\");
        string DirRepName = Server.MapPath("TempFiles\\Reports\\Import\\") + reportName;

        string location = DirRepPath + fileUpload.FileName;
        fileUpload.SaveAs(location);
        /////////////////////////////////////////////////////////////
        using (ZipFile zip = Ionic.Zip.ZipFile.Read(location))
        {
            //if (Directory.Exists(DirRepName)) { Directory.Delete(DirRepName,true); }
            zip.ExtractAll(DirRepName, ExtractExistingFileAction.OverwriteSilently);
        }

        XmlDocument XmlDatabase = new XmlDocument();
        XmlDatabase.Load(DirRepName + "\\RepSetting.enc");
        XmlNode MainElement = XmlDatabase.DocumentElement;
        foreach (XmlNode Node in MainElement)
        {
            foreach (XmlNode LoadedNode in Node)
            {
                if (!string.IsNullOrEmpty(LoadedNode.InnerText.ToString()))
                {
                    if (LoadedNode.Name == "RepID") { ProCs.RepID = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RgpID") { ProCs.RgpID = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepNameAr") { ProCs.RepNameAr = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepNameEn") { ProCs.RepNameEn = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepForSchedule") { if (!string.IsNullOrEmpty(LoadedNode.InnerText.ToString())) { ProCs.RepForSchedule = Convert.ToBoolean(LoadedNode.InnerText.ToString()); } else { ProCs.RepForSchedule = false; } }
                    if (LoadedNode.Name == "RepDescAr") { ProCs.RepDescAr = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepDescEn") { ProCs.RepDescEn = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepPanels") { if (!string.IsNullOrEmpty(LoadedNode.InnerText.ToString())) { ProCs.RepPanels = LoadedNode.InnerText.ToString(); } else { ProCs.RepPanels = "0"; } }
                    if (LoadedNode.Name == "RepProcedureName") { ProCs.RepProcedureName = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepParametersProc") { ProCs.RepParametersProc = LoadedNode.InnerText.ToString(); }

                    if (LoadedNode.Name == "RepVisible") { if (!string.IsNullOrEmpty(LoadedNode.InnerText.ToString())) { ProCs.RepVisible = Convert.ToBoolean(LoadedNode.InnerText.ToString()); } else { ProCs.RepVisible = false; } }
                    if (LoadedNode.Name == "RepType") { ProCs.RepType = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepOrientation") { ProCs.RepOrientation = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "VerID") { ProCs.VerID = LoadedNode.InnerText.ToString(); }
                    if (LoadedNode.Name == "RepGeneralID") { ProCs.RepGeneralID = LoadedNode.InnerText.ToString(); }
                }
            }
        }

        ProCs.RepTempEn = CretaeRepString(DirRepName + "\\En.mrt");
        ProCs.RepTempDefEn = CretaeRepString(DirRepName + "\\DefEn.mrt");
        ProCs.RepTempAr = CretaeRepString(DirRepName + "\\Ar.mrt");
        ProCs.RepTempDefAr = CretaeRepString(DirRepName + "\\DefAr.mrt");

        ProCs.TransactionBy = pgCs.LoginID;

        try
        {
            DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { ProCs.RepID });
            if (!DBCs.IsNullOrEmpty(DT)) { SqlCs.Rep_Update(ProCs); } else { SqlCs.Rep_Insert(ProCs); }

            FillReport();

            //File.Delete(location);

            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Report Uploaded Successfully", "تم رفع التقرير بنجاح"));
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string CretaeRepString(string FilePath)
    {
        string RepStr = "";
        StiReport StiRep = new StiReport();
        StiRep.Load(FilePath);
        StiRep.Dictionary.Databases.Clear();
        StiRep.Dictionary.Databases.Add(new StiSqlDatabase("Connection", General.ConnString));
        RepStr = StiRep.SaveToString();
        return RepStr;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void btnExportRecord_Click(object sender, EventArgs e)
    {
        if (ViewState["RepID"].ToString() != null)
        {
            string RepName = "";
            string DirRepPath = Server.MapPath("TempFiles\\Reports\\Export\\");
            string DirRepName = Server.MapPath("TempFiles\\Reports\\Export\\") + ViewState["RepID"].ToString().Trim();

            try
            {
                DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { ViewState["RepID"].ToString() });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    if (Directory.Exists(DirRepName)) { Directory.Delete(DirRepName, true); }
                    Directory.CreateDirectory(DirRepName);
                    //////////////////////
                    string SettingPathFile = DirRepName + "\\RepSetting.enc";
                    //if (File.Exists(SettingPathFile)) { File.Delete(SettingPathFile); }

                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.AppendChild(XMLDoc.CreateXmlDeclaration("1.0", null, null));
                    XmlNode HeaderNode = XMLDoc.CreateElement("Reports"); /*H database*/ XMLDoc.AppendChild(HeaderNode);
                    XmlNode SettingNode = XMLDoc.CreateElement("Report"); /***/ HeaderNode.AppendChild(SettingNode);
                    string[,] sName = new string[15, 2] { /*00*/  { "RepID"             , DT.Rows[0]["RepID"].ToString() }
                                                        , /*01*/  { "RgpID"             , DT.Rows[0]["RgpID"].ToString() }
                                                        , /*02*/  { "RepNameAr"         , DT.Rows[0]["RepNameAr"].ToString() }
                                                        , /*03*/  { "RepNameEn"         , DT.Rows[0]["RepNameEn"].ToString() }
                                                        , /*04*/  { "RepForSchedule"    , DT.Rows[0]["RepForSchedule"].ToString() }
                                                        , /*05*/  { "RepDescAr"         , DT.Rows[0]["RepDescAr"].ToString() }
                                                        , /*06*/  { "RepDescEn"         , DT.Rows[0]["RepDescEn"].ToString() }
                                                        , /*07*/  { "RepPanels"         , DT.Rows[0]["RepPanels"].ToString() }
                                                        , /*08*/  { "RepProcedureName"  , DT.Rows[0]["RepProcedureName"].ToString() }
                                                        , /*09*/  { "RepParametersProc" , DT.Rows[0]["RepParametersProc"].ToString() }
                                                        , /*10*/  { "RepVisible"        , DT.Rows[0]["RepVisible"].ToString() }
                                                        , /*11*/  { "RepType"           , DT.Rows[0]["RepType"].ToString() }
                                                        , /*12*/  { "RepOrientation"    , DT.Rows[0]["RepOrientation"].ToString() }
                                                        , /*13*/  { "VerID"             , DT.Rows[0]["VerID"].ToString() }
                                                        , /*14*/  { "RepGeneralID"      , DT.Rows[0]["RepGeneralID"].ToString() }
                    };

                    for (int k = 0; k < 15; k++)
                    {
                        XmlElement Node = XMLDoc.CreateElement(sName[k, 0]);
                        Node.AppendChild(XMLDoc.CreateTextNode(sName[k, 1]));
                        SettingNode.AppendChild(Node);
                    }

                    XMLDoc.Save(SettingPathFile);


                    RepName = DT.Rows[0]["RepNameEn"].ToString() + "_Export.ams";
                    if (File.Exists(RepName)) { File.Delete(RepName); }

                    string RepEnPath = DirRepName + "\\En.mrt";
                    WriteFile(DT.Rows[0]["RepTempEn"].ToString(), RepEnPath);

                    string RepArPath = DirRepName + "\\Ar.mrt";
                    WriteFile(DT.Rows[0]["RepTempAr"].ToString(), RepArPath);

                    string RepDefEnPath = DirRepName + "\\DefEn.mrt";
                    WriteFile(DT.Rows[0]["RepTempDefEn"].ToString(), RepDefEnPath);

                    string RepDefArPath = DirRepName + "\\DefAr.mrt";
                    WriteFile(DT.Rows[0]["RepTempDefAr"].ToString(), RepDefArPath);
                    ZipFiles.Zip(DirRepPath + RepName, SettingPathFile, RepArPath, RepEnPath, RepDefArPath, RepDefEnPath);
                    if (Directory.Exists(DirRepName)) { Directory.Delete(DirRepName, true); }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
            }


            string Source = Server.MapPath(@"TempFiles\Reports\Export\" + RepName);
            string Destination = "attachment; filename=" + RepName;
            //Response.ContentType = "text/plain";
            Response.Clear();
            Response.ContentType = "application/x-zip-compressed";
            Response.AppendHeader("Content-Disposition", Destination);
            Response.TransmitFile(Source);
            Response.End();

            ////////////////////////
            ////// Read Report name to using in filename Export
            //string repName = "";
            //if (Language == "AR") { repName = dr["RepNameAr"].ToString(); }
            //else { repName = dr["RepNameEn"].ToString(); }
            ////// check directory in server, if not there create it
            //string dir = Server.MapPath(@"./TempFiles/Reports/");
            //DirCheck(dir);
            //string fileLoc = dir + repName + "_Export.txt";

            //FileStream fs = null;
            ////// check file is there delete it  
            //if (File.Exists(fileLoc)) { File.Delete(fileLoc); }
            ////// create file
            //using (fs = File.Create(fileLoc)) { };

            //DataTable_ExportImport.ExportDataTabletoFile(dt, "$&", true, fileLoc);

        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void WriteFile(string pMassege, string pPath)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //if (!File.Exists(pPath)) { }

        FileStream MyFile = new FileStream(pPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
        StreamWriter MyStream = new StreamWriter(MyFile);
        MyStream.BaseStream.Seek(0, SeekOrigin.End);
        MyStream.WriteLine(pMassege);
        MyStream.Flush();
        MyStream.Close();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnImportRecord_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(fileUpload.PostedFile.FileName.ToString()))
            return;
        try
        {
            string dir = Server.MapPath(@"./Reports/");
            DirCheck(dir);

            string fileName = Path.GetFileName(fileUpload.FileName);
            fileName = fileName.Replace("_Export.txt", "");
            string filePath = dir + fileName + "_Import.txt";
            fileUpload.SaveAs(filePath);

            DataTable dtImp = new DataTable();

            DataTable_ExportImport.ImportDataTableFromFile(filePath, "$&", true, dtImp);

            FillRepObject(dtImp);
            ///we move this code to inside FillRepObject
            //repSql.Insert(rep);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void DirCheck(string Dir) { if (!Directory.Exists(Dir)) { Directory.CreateDirectory(Dir); } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillRepObject(DataTable RDT)
    {
        ProCs.RepID = RDT.Rows[0]["RepID"].ToString();
        ProCs.RgpID = RDT.Rows[0]["RgpID"].ToString();
        ProCs.RepNameAr = RDT.Rows[0]["RepNameAr"].ToString();
        ProCs.RepNameEn = RDT.Rows[0]["RepNameEn"].ToString();
        ProCs.RepDescAr = RDT.Rows[0]["RepDescAr"].ToString();
        ProCs.RepDescEn = RDT.Rows[0]["RepDescEn"].ToString();

        if (!string.IsNullOrEmpty(RDT.Rows[0]["RepPanels"].ToString())) { ProCs.RepPanels = RDT.Rows[0]["RepPanels"].ToString(); }
        if (!string.IsNullOrEmpty(RDT.Rows[0]["RepForSchedule"].ToString())) { ProCs.RepForSchedule = Convert.ToBoolean(RDT.Rows[0]["RepForSchedule"].ToString()); }
        if (!string.IsNullOrEmpty(RDT.Rows[0]["RepVisible"].ToString())) { ProCs.RepVisible = Convert.ToBoolean(RDT.Rows[0]["RepVisible"].ToString()); }

        ProCs.RepTempAr = RDT.Rows[0]["RepTempAr"].ToString();
        ProCs.RepTempEn = RDT.Rows[0]["RepTempEn"].ToString();
        ProCs.RepTempDefAr = RDT.Rows[0]["RepTempDefAr"].ToString();
        ProCs.RepTempDefEn = RDT.Rows[0]["RepTempDefEn"].ToString();

        ProCs.RepProcedureName = RDT.Rows[0]["RepProcedureName"].ToString();
        ProCs.RepParametersProc = RDT.Rows[0]["RepParametersProc"].ToString();

        ProCs.TransactionBy = pgCs.LoginID;

        DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { RDT.Rows[0]["RepID"].ToString() });
        if (!DBCs.IsNullOrEmpty(DT)) { SqlCs.Rep_Update(ProCs); } else { SqlCs.Rep_Insert(ProCs); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region TreeView Events

    protected void trvDept_SelectedNodeChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvDept_DataBound(object sender, EventArgs e)
    {
        //trvDept_TreeNodeDataBound(null, null); 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void trvDept_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        try
        {
            string dd = e.Node.ImageToolTip;
            //if (dd == "T") { e.Node.ShowCheckBox = true; } else { e.Node.ShowCheckBox = false; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillDepTree()
    {
        string  Lang  = HttpContext.Current.Cache["RepLangCache"] as string;
        DataSet DepDS = HttpContext.Current.Cache["RepDepDSCache"] as DataSet;
        if (DepDS == null || Lang != General.Msg("EN", "AR"))
        {
            Lang = General.Msg("EN", "AR");

            DataSet DS = new DataSet();
            DS = CtrlCs.FillBrcDepTreeDS(pgCs.Version, (Session["DepartmentList"] != null ? pgCs.DepList : "0"));
            xdsDept.Data = DS.GetXml();

            HttpContext.Current.Cache.Insert("RepLangCache", Lang);
            HttpContext.Current.Cache.Insert("RepDepDSCache", DS);
        }
        else
        {
             xdsDept.Data = DepDS.GetXml();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SearchTree(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.ChildNodes.Count > 0) { SearchTree(node.ChildNodes); }

            char sp = '-';
            if (pgCs.Version == "BorderGuard") { sp = '/'; }
            else { sp = '-'; } // (ActiveVersion == "General")

            string[] Names = node.Text.Split(sp);

            if (Names.Length > 0)
            {
                node.Text = node.Text.Replace(@"<font color='Red'>", @"");
                node.Text = node.Text.Replace(@"<font color='#4E6877'>", @"");
                node.Text = node.Text.Replace(@"</font>", @"");

                //if (Names[Names.Length - 1].ToLower().StartsWith(txtSearchByDep.Text.ToLower())) { node.Text = String.Format(" <font color='Red'>{0}</font>", node.Text); }
                //else { node.Text = String.Format(" <font color='#4E6877'>{0}</font>", node.Text);  }

                if (Names[Names.Length - 1].ToLower().Contains(txtSearchByDep.Text.ToLower())) { node.Text = String.Format(" <font color='Red'>{0}</font>", node.Text); }
                else { node.Text = String.Format(" <font color='#4E6877'>{0}</font>", node.Text); }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearchDep_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSearchByDep.Text)) { return; }
            SearchTree(trvDept.Nodes);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Favorite Report Events

    protected void btnAddFavorite_Click(object sender, EventArgs e)
    {
        txtFavNameEn.Text = txtFavNameAr.Text = "";
        txtFavNameEn.Enabled = txtFavNameAr.Enabled = true;
        btnFavSave.Enabled = btnFavCancel.Enabled = true;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFavCancel_Click(object sender, EventArgs e) { FavClear(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFavSave_Click(object sender, EventArgs e)
    {
        try
        {
            Session["RepProCs"] = null;
            if (!CtrlCs.PageIsValid(this, vsShow)) { return; }
            if (string.IsNullOrEmpty(txtFavNameEn.Text) || string.IsNullOrEmpty(txtFavNameAr.Text))
            {
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("The Arabic and English name must be entered", "يجب إدخال الاسم العربي والانجليزي"));
                return;
            }

            DataTable DT = DBCs.FetchData("SELECT * FROM FavoriteForms WHERE FavUsrName = @P1 AND (FavNameAr = @P2 OR FavNameEn = @P3) ", new string[] { pgCs.LoginID, txtFavNameAr.Text, txtFavNameEn.Text });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("The Arabic or English name is repeated", "الاسم العربي اوالانجليزي مكرر"));
                return;
            }

            FillParameter(ViewState["RepID"].ToString());
            Session["RepProCs"] = ViewState["RepProCs"];
            RepCs.FavReportInsert(ViewState["RepID"].ToString(), pgCs.LoginID, txtFavNameEn.Text, txtFavNameAr.Text, (RepParametersPro)ViewState["RepProCs"]);

            FavClear();
            CtrlCs.ShowSaveMsg(this);

            AMSMasterPage master = (AMSMasterPage)this.Master;
            master.FillFavForm();
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FavClear()
    {
        txtFavNameEn.Text = txtFavNameAr.Text = "";
        txtFavNameEn.Enabled = txtFavNameAr.Enabled = false;
        btnFavSave.Enabled = btnFavCancel.Enabled = false;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void Validate_ServerValidate(object source, ServerValidateEventArgs e)
    {
        if (source.Equals(cvMonthList)) { if (pnlMonth.Visible) { } }

        if (source.Equals(cvWorkTime))
        {
            if (pnlWorkTime.Visible && WorkTime() == "")
            {
                CtrlCs.ValidMsg(this, ref cvWorkTime, true, General.Msg("Fill at least one field in WorkTime filters!", "يجب ملء حقل واحد على الأقل في حقول وقت العمل"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvMachine))
        {
            if (pnlMachine.Visible && Machine() == "")
            {
                CtrlCs.ValidMsg(this, ref cvMachine, true, General.Msg("Fill at least one feild in Machine filters!", "يجب ملء حقل واحد على الأقل في حقول الجهاز"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvDepartment))
        {
            if (pnlDepartmnets.Visible && Department() == "")
            {
                CtrlCs.ValidMsg(this, ref cvDepartment, true, General.Msg("must be select at least one department of Department List!", "يجب اخيتار قسم واحد على الأقل من قائمة الأقسام"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvCategory))
        {
            if (pnlCategory.Visible && Category() == "")
            {
                CtrlCs.ValidMsg(this, ref cvCategory, true, General.Msg("Fill at least one feild in Category filters!", "يجب ملء حقل واحد على الأقل في حقول التصنيف"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvUser))
        {
            if (pnlUsers.Visible) //&& AppUsers() == ""
            {
                //cvUser.ErrorMessage = ShowMsgValidate("Fill at least one feild in User filters!", "يجب إدخال أسم المستخدم");
                //e.IsValid = false;
            }
        }

        if (source.Equals(cvVacType))
        {
            if (pnlVacType.Visible && VacationType() == "")
            {
                CtrlCs.ValidMsg(this, ref cvVacType, true, General.Msg("Fill at least one feild in Vacation Type filters!", "يجب ملء حقل واحد على الأقل في حقول نوع الإجازة"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvExcType))
        {
            if (pnlExcType.Visible && ExcuseType() == "")
            {
                CtrlCs.ValidMsg(this, ref cvMachine, true, General.Msg("Fill at least one feild in Excuse Type filters!", "يجب ملء حقل واحد على الأقل في حقول نوع الاستئذان"));
                e.IsValid = false;
            }
        }

        if (source.Equals(cvDaysCount))
        {
            if (pnlDaysCount.Visible)
            {
                if (string.IsNullOrEmpty(txtDaysCount.Text)) { e.IsValid = false; } else { if (Convert.ToInt32(txtDaysCount.Text) < 1) { e.IsValid = false; } }
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}
/*
   public void ExportCSV(DataTable data, string fileName)
   {

       HttpContext context = HttpContext.Current;

       context.Response.Clear();
       context.Response.ContentType = "text/csv";
       context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv");

       //rite column header names
       for (int i = 0; i < data.Columns.Count - 1; i++)
       {
           if (i > 0)
           {
               context.Response.Write(",");
           }
           context.Response.Write(data.Columns[i].ColumnName);
       }
       context.Response.Write(Environment.NewLine);

       //Write data
       foreach (DataRow row in data.Rows)
       {

           for (int i = 0; i < data.Columns.Count - 1; i++)
           {
               if (i > 0)
               {
                   context.Response.Write(",");
               }
               context.Response.Write(data.Columns[i].ColumnName);
           }
           context.Response.Write(Environment.NewLine);
       }
       context.Response.End();

   }
   */
