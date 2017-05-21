using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;
using Elmah;
using System.Web;

public partial class AMSMasterPage : System.Web.UI.MasterPage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersSql logSqlCs = new UsersSql();
    CustPageSql CpgSqlCs = new CustPageSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    ReportFun RepCs = new ReportFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            string ERSLic = LicDf.FetchLic("ER");
            /*** Fill Session ************************************/
            
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0) { Request.Browser.Adapters.Clear(); }
            lnkLanguage.Text = General.Msg("English","عربي");

            if (pgCs.Lang == "AR") { form1.Attributes.Add("dir", "rtl"); } else { form1.Attributes.Add("dir", "ltr"); }
            if (pgCs.Lang == "AR") { Menu1.DynamicMenuItemStyle.CssClass = "mndynamicitemAR"; Menu1.DynamicHoverStyle.CssClass = "mndynamicitemhoverAR"; }
            else { Menu1.DynamicMenuItemStyle.CssClass = "mndynamicitemEN"; Menu1.DynamicHoverStyle.CssClass = "mndynamicitemhoverEN"; }
       
            //int ss = Session.Timeout;
            //Session.Timeout = 2;
            //Session.SessionID
            //try {// Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 10) + "; URL=Login.aspx"); }
            //catch (Exception ex) { }
             
            if (!IsPostBack)
            {
                ChangeLogo();
                SetPageTitel();
                ShowLogo();             
                CreateMenu(pgCs.LoginType, pgCs.LoginEmpID, false);

                lblcurrentYear.Text = DateTime.Now.Year.ToString();
                lblUserName.Text = " " + pgCs.LoginID;
                lnkERS.Text = General.Msg("ERS", "نظام الاستئذانات");

                if (pgCs.LoginType == "USR")
                {
                    //FillFavPage();
                    //FillFavReport();
                    //FillFavForm();
                    FillFavForm();
                    ShowIsExistingRequest(ERSLic);
                    lnkShortcut.Enabled  = true;
                }
                else if (pgCs.LoginType == "EMP")
                {
                    if (ERSLic == "1") { divEmpInfo.Visible = divERS.Visible = true; } else { divEmpInfo.Visible = divERS.Visible = false; }
                    FillMonthSummary();
                    lnkShortcut.Enabled     = false;

                    if (pgCs.Version == "Al_JoufUN")
                    {
                        trDaysAbsentWithUnpaidExcuse.Visible  = false;
                        trGapWithUnpaidExcuse.Visible         = false;
                        trShiftAbsentWithoutExcuse.Visible    = false;
                        trShiftAbsentWithPaidExcuse.Visible   = false;
                        trShiftAbsentWithUnpaidExcuse.Visible = false;
                        trShiftsAbsent.Visible                = false;
                    }
                    else if (pgCs.Version == "KACare")
                    {
                        divEmployeeVacationDays.Visible       = false;
                        trActualWorkDuration.Visible          = false;
                        trWorkDuration.Visible                = false;
                        trWorkingDays.Visible                 = false;
                        trDaysPresent.Visible                 = false;
                        trShiftsAbsent.Visible                = false;
                        trGapWithUnpaidExcuse.Visible         = false;               
                        trShiftAbsentWithoutExcuse.Visible    = false;
                        trShiftAbsentWithPaidExcuse.Visible   = false;
                        trShiftAbsentWithUnpaidExcuse.Visible = false;                
                        trDaysAbsent.Visible                  = false;  
                        //trDaysAbsentWithoutExcuse.Visible     = false;
                        trDaysAbsentWithUnpaidExcuse.Visible  = false;
                        trDaysAbsentWithPaidExcuse.Visible    = false;
                        trTotalOverTime.Visible               = false;
                    }
                    else // (ActiveVersion == "General")
                    {
                        trDaysAbsentWithUnpaidExcuse.Visible  = true;
                        trGapWithUnpaidExcuse.Visible         = true;
                        trShiftAbsentWithoutExcuse.Visible    = true;
                        trShiftAbsentWithPaidExcuse.Visible   = true;
                        trShiftAbsentWithUnpaidExcuse.Visible = true;
                        trShiftsAbsent.Visible                = true;
                    }

                    DataTable DT = DBCs.FetchData(" SELECT * FROM EmpInfoView WHERE EmpID =  @P1 ", new string[] { pgCs.LoginEmpID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        string lan = (pgCs.Lang == "AR") ? "Ar" : "En";
                        if (DT.Rows[0]["EmpName" + lan] != DBNull.Value) { lblEmpNameVal.Text = DT.Rows[0]["EmpName" + lan].ToString(); }
                        if (DT.Rows[0]["DepName" + lan] != DBNull.Value) { lblDepNameVal.Text = DT.Rows[0]["DepName" + lan].ToString(); }
                    } 
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetPageTitel()
    {
        try
        {
            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
            string PageName = PageFileInfo.Name + (!string.IsNullOrEmpty(QS) ? "?" + QS : "" );
            //string sURL = Request.Url.ToString().ToLower();

            DataTable DT = DBCs.FetchData(" SELECT * FROM Menu WHERE MnuURL like @P1 ", new string[] { PageName });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["MnuTextEn"] != DBNull.Value) { lblPageTitel.Text = General.Msg(DT.Rows[0]["MnuTextEn"].ToString(), DT.Rows[0]["MnuTextAr"].ToString()); }
            }   
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ChangeLogo()
    {
        try
        {
            string URL = Request.Url.ToString().ToLower();
            if (pgCs.Version == "BorderGuard")
            {
                //if (URL.EndsWith("home.aspx")) { divLogo.Attributes["class"] = "tp_logo_Home_AlJoufUN_" + Language; } else { divLogo.Attributes["class"] = "tp_logo_AlJoufUN_" + Language; }
                if (URL.EndsWith("home.aspx")) { divLogo.Attributes["class"] = "tp_logo_Home_BorderGuard_" + pgCs.Lang; } else { divLogo.Attributes["class"] = "tp_logo_BorderGuard_" + pgCs.Lang; }
            }
            else // (ActiveVersion == "General")
            {
                if (URL.EndsWith("home.aspx")) { divLogo.Attributes["class"] = "tp_logobgHome" + pgCs.Lang; } else { divLogo.Attributes["class"] = "tp_logobg" + pgCs.Lang; }
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void LnkLogout_Click(object sender, EventArgs e)
    {
        logSqlCs.InOutLog_Update(pgCs.LoginID);

        Session["Permissions"] = null;
        Session.Contents.RemoveAll();
        Session.Abandon();

        //Response.Buffer=<SPAN style="COLOR: blue">true;<o:p></o:p>
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        if (Session["SessionId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkERS_Click(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkLanguage_Click(object sender, EventArgs e)
    {
        if (pgCs.Lang == "AR") { Session["Language"] = "EN"; } else { Session["Language"] = "AR"; }             

        string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
        Response.Redirect(Request.FilePath + QS);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void LnkChangePassword_Click(object sender, EventArgs e)
    {            
        Response.Redirect("~/Pages_Mix/ChangePassword.aspx");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [System.Web.Services.WebMethod]
    public static void BrowserCloseMethod()
    {
        //HttpContext.Current.Session.Abandon();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnERS_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    LinkButton CurrentLinkButton = (LinkButton)sender;
        //    switch (CurrentLinkButton.ID)
        //    {
        //        case "btnAttendanceList":       Response.Redirect("~/Pages_ERS/AttendanceList.aspx");         break;
        //        case "btnTodayTransaction":     Response.Redirect("~/Pages_ERS/ERS_TodayTransaction.aspx");   break;
        //        case "btnMonthSummary":         Response.Redirect("~/Pages_ERS/ERS_MonthSummary.aspx");       break;
        //        case "btnEmployeeTransaction":  Response.Redirect("~/Pages_ERS/ERS_EmployeeTransaction.aspx");   break;
        //        case "btnEmployeeGap":          Response.Redirect("~/Pages_ERS/EmployeeGaps.aspx");           break;
        //        case "btnEmployeeOvt":          Response.Redirect("~/Pages_ERS/ERS_EmployeeOvertime.aspx");   break;
        //        case "btnEmployeeRequest":      Response.Redirect("~/Pages_ERS/EmployeeRequest.aspx");        break;
        //        case "btnEmployeeVacationDays": Response.Redirect("~/Pages_ERS/EmployeeVacationDays.aspx");   break;
        //        case "btnVacationRequest":      Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=VAC"); break;
        //        case "btnCommissionRequest":    Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=COM"); break;
        //        case "btnJobAssignmentRequest": Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=JOB"); break;
        //        case "btnAdministrativeLicensesRequest":  Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=LIC"); break;

        //        case "btnExcuseRequest":        Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=EXC"); break; 
        //        case "btnShiftExcuseRequest":   Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=ESH"); break;
        //        case "btnShiftSwapRequest":     Response.Redirect("~/Pages_ERS/RequestMaster.aspx?Type=SWP"); break;
        //        case "btnShiftSwapEmployeeApproval": Response.Redirect("~/Pages_ERS/ShiftSwap_EmployeeApproval.aspx"); break;
        //    }
        //}
        //catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAdmin_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    LinkButton CurrentLinkButton = (LinkButton)sender;
        //    switch (CurrentLinkButton.ID)
        //    {
        //        case "btnConnString":      Response.Redirect("~/Pages_Admin/ConnString.aspx");       break;
        //        case "btnDepartmentLevel": Response.Redirect("~/Pages_Admin/DepartmentLevel.aspx");  break;
        //        case "btnADSetting":       Response.Redirect("~/Pages_Admin/ADSetting.aspx");        break;
        //        case "btnEmailSetting":    Response.Redirect("~/Pages_Admin/EmailSetting.aspx");     break;
        //        case "btnSpErrors":        Response.Redirect("~/Pages_Admin/SpErrors.aspx");         break;
        //        case "btnAspErrors":       Response.Redirect("~/Pages_Admin/ASPErrors.aspx");        break;
        //        case "btnMacErrors":       Response.Redirect("~/Pages_Admin/MachineErrors.aspx");    break;
        //        case "btnReqEnabled":      Response.Redirect("~/Pages_Admin/RequestsEnabled.aspx");  break;
        //    }
        //}
        //catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillMonthSummary()
    {
        try
        {
            divMonthSummary.Visible = true;

            string Month = "";
            string Year = "";
            string EmpID = "";
            string daylang = "";
            string shiftlang = "";

            if (Session["AttendanceListMonth"] != null) { Month = Session["AttendanceListMonth"].ToString(); }
            if (Session["AttendanceListYear"]  != null) { Year  = Session["AttendanceListYear"].ToString();  }
            if (Session["LoginEmpID"] != null) { EmpID = Session["LoginEmpID"].ToString(); }

            if (!string.IsNullOrEmpty(Month) && !string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(EmpID))
            {
                lblMonthSummary.Text = General.Msg("Attendance " + Convert.ToInt32(Month).ToString("00") + "/" + Year + " Summary", "ملخص حضور " + Convert.ToInt32(Month).ToString("00") + "/" + Year);
                daylang              = General.Msg(" Day(s)", " يوم (أيام)");
                shiftlang            = General.Msg(" Shift(s)", " وردية (ورديات)");

                DateTime SDate;
                DateTime EDate;
                DTCs.FindMonthDates(Year, Month, out SDate, out EDate);

                DataTable DT = DBCs.FetchData(" SELECT * FROM MonthSummary WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = @P1 AND EmpID = @P2 ", new string[] { SDate.ToString("dd/MM/yyyy"), EmpID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    lblActualWorkDurationValue.Text          = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmWorkDurWithET"].ToString());
                    lblWorkDurationValue.Text                = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmWorkDuration"].ToString());
                    lblWorkingDaysValue.Text                 = DT.Rows[0]["MsmDays_Work"].ToString() + daylang;
                    lblDaysPresentValue.Text                 = DT.Rows[0]["MsmDays_Present"].ToString() + daylang;
                    

                    lblBeginLateValue.Text                   = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmBeginLate"].ToString());
                    lblOutEarlyValue.Text                    = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmOutEarly"].ToString());
                    lblGapWithoutExcuseValue.Text            = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_WithoutExc"].ToString());
                    lblGapWithUnpaidExcuseValue.Text         = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_UnPaidExc"].ToString());
                    lblGapWithPaidExcuseValue.Text           = DisplayFun.GrdDisplayDuration(DT.Rows[0]["MsmGapDur_PaidExc"].ToString());

                    lblShiftAbsentWithoutExcuseValue.Text    = DT.Rows[0]["MsmShifts_Absent_WithoutExc"].ToString() + shiftlang;
                    lblShiftAbsentWithUnpaidExcuseValue.Text = DT.Rows[0]["MsmShifts_Absent_UnPaidExc"].ToString() + shiftlang;
                    lblShiftAbsentWithPaidExcuseValue.Text   = DT.Rows[0]["MsmShifts_Absent_PaidExc"].ToString() + shiftlang;
                    lblShiftsAbsentValue.Text                = (Convert.ToInt32(DT.Rows[0]["MsmShifts_Absent_WithoutExc"]) + Convert.ToInt32(DT.Rows[0]["MsmShifts_Absent_UnPaidExc"]) + Convert.ToInt32(DT.Rows[0]["MsmShifts_Absent_PaidExc"])).ToString() + shiftlang;


                    lblDaysAbsentWithoutExcuseValue.Text     = DT.Rows[0]["MsmDays_Absent_WithoutVac"].ToString() + daylang;
                    lblDaysAbsentWithUnpaidExcuseValue.Text  = DT.Rows[0]["MsmDays_Absent_UnPaidVac"].ToString() + daylang;
                    lblDaysAbsentWithPaidExcuseValue.Text    = DT.Rows[0]["MsmDays_Absent_PaidVac"].ToString() + daylang;
                    lblDaysAbsentValue.Text                  = (Convert.ToInt32(DT.Rows[0]["MsmDays_Absent_WithoutVac"]) + Convert.ToInt32(DT.Rows[0]["MsmDays_Absent_UnPaidVac"]) + Convert.ToInt32(DT.Rows[0]["MsmDays_Absent_PaidVac"])).ToString() + daylang;

                    lblTotalOverTimeValue.Text               = (Convert.ToInt32(DT.Rows[0]["MsmOverTimeDur_BeginEarly"]) + Convert.ToInt32(DT.Rows[0]["MsmOverTimeDur_OutLate"]) + Convert.ToInt32(DT.Rows[0]["MsmOverTimeDur_OutOfShift"])
                                                                    + Convert.ToInt32(DT.Rows[0]["MsmOverTimeDur_NoShift"]) + Convert.ToInt32(DT.Rows[0]["MsmOverTimeDur_InVac"])).ToString();
                }
                else
                {
                    lblActualWorkDurationValue.Text = "00:00:00";
                    lblWorkDurationValue.Text       = "00:00:00";
                    lblWorkingDaysValue.Text        = "0"+ daylang;
                    lblDaysPresentValue.Text        = "0"+ daylang;
                    lblDaysAbsentValue.Text         = "0"+ daylang;

                    lblShiftsAbsentValue.Text       = "0" + shiftlang;

                    lblBeginLateValue.Text           = "00:00:00";
                    lblOutEarlyValue.Text            = "00:00:00";
                    lblGapWithoutExcuseValue.Text    = "00:00:00";
                    lblGapWithUnpaidExcuseValue.Text = "00:00:00";
                    lblGapWithPaidExcuseValue.Text   = "00:00:00";

                    lblShiftAbsentWithoutExcuseValue.Text    = "0" + shiftlang;
                    lblShiftAbsentWithUnpaidExcuseValue.Text = "0" + shiftlang;
                    lblShiftAbsentWithPaidExcuseValue.Text   = "0" + shiftlang;

                    lblDaysAbsentWithoutExcuseValue.Text    = "0"+ daylang;
                    lblDaysAbsentWithUnpaidExcuseValue.Text = "0"+ daylang;
                    lblDaysAbsentWithPaidExcuseValue.Text   = "0"+ daylang;
                    lblTotalOverTimeValue.Text = "00:00:00";
                }
            }

            upMonthSummary.Update();
        }
        catch (Exception e1)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    protected void LnkHome_Click(object sender, EventArgs e) { Response.Redirect("~/Pages_Mix/Home.aspx"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkHelp_Click(object sender, EventArgs e)
    {            
        System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
        string ResPageName = PageFileInfo.Name;
        string[] ResPageNameArr = PageFileInfo.Name.Split('.');
        string htmlName = "";
        if (ResPageNameArr[0] == "Home") { htmlName = ResPageNameArr[0] + pgCs.Lang + ".html"; } else { htmlName = ResPageNameArr[0] + pgCs.Lang + ".htm"; }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "ShowHelp('Help/" + htmlName + "');", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowLogo()
    {
        try
        {
            if (pgCs.Version == "BorderGuard") { divCustLogo.Visible = false; divEmpInfo2.Attributes.Add("class", "rp_realpad1"); divFavPage2.Attributes.Add("class", "rp_realpad1"); }
            else
            {
                DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT AppLogo FROM ApplicationSetup "));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    if (DT.Rows[0][0] != DBNull.Value) { imgLogo.ImageUrl = "~/Pages_Mix/ReadImage.aspx?ID=Logo"; return; }
                }

                imgLogo.ImageUrl = "~/images/MasterPage_Images/InsertLogo.JPG";
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkPolicy_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Policies.aspx");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Menu Events

    protected void Menu1_MenuItemDataBound(object sender, MenuEventArgs e)
   {
        System.Xml.XmlElement node = (System.Xml.XmlElement)e.Item.DataItem;
        if (node.ChildNodes.Count != 0)
        {
            e.Item.Selectable = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void CreateMenu(string Type, string EmpID, bool isAdmin)
    {
        try
        {
            StringBuilder QMuen = new StringBuilder();

            if (Type == "USR") { QMuen.Append(FindUsrMenu()); }
            if ((Type == "USR" && !string.IsNullOrEmpty(EmpID)) || (Type == "EMP")) 
            {  
                if (!string.IsNullOrEmpty(QMuen.ToString())) { QMuen.Append(" UNION ALL "); }
                QMuen.Append(FindEmpMenu()); 
            }
            if (Type == "USR" && isAdmin) 
            {  
                if (!string.IsNullOrEmpty(QMuen.ToString())) { QMuen.Append(" UNION ALL "); }
                QMuen.Append(FindAdminMenu()); 
            }

            if (!string.IsNullOrEmpty(QMuen.ToString())) { QMuen.Append(" ORDER BY MnuOrder"); }

            ViewState["QMuen"] = QMuen.ToString();

            FillMenu(QMuen.ToString());
            FillSideMenu(QMuen.ToString());
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    protected string FindUsrMenu()
    {
        StringBuilder QMuen = new StringBuilder();

        string lang     = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol  = (pgCs.Lang == "AR") ? "MnuArabicDescription" : "MnuDescription";
        string LicPage  = LicDf.FindLicPage();
        string listPage = "'SubMenu','AD_MainMenu','AD_SubMenu'" + (string.IsNullOrEmpty(LicPage) ? "" : "," + LicPage);
            
        string MPerm = (GenCs.IsNullOrEmpty(Session["MenuPermissions"])) ? "0" : Session["MenuPermissions"].ToString();
        string RPerm = (GenCs.IsNullOrEmpty(Session["ReportPermissions"])) ? "0" : Session["ReportPermissions"].ToString();
            
        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE MnuType IN ('Menu') ");        
            
        QMuen.Append(" UNION ALL ");
        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE MnuVisible = 'True' AND MnuType IN (" + listPage + ") AND MnuID IN (" + MPerm + ") AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + pgCs.Version + "',VerID) > 0) ");
            
        QMuen.Append(" UNION ALL ");
        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE  MnuVisible ='True' AND MnuType IN ('Reports') AND RgpID IN (" + RPerm + ") AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + pgCs.Version + "',VerID) > 0) ");

        return QMuen.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string FindEmpMenu()
    {
        StringBuilder QMuen = new StringBuilder();

        string lang     = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol  = (pgCs.Lang == "AR") ? "MnuDescription" : "MnuDescription";
        string listPage = "'ERS_MainMenu','ERS_SubMenu'";
            
        Hashtable Reqht = pgCs.GetAllRequestPerm();
        string ERSLic = LicDf.FetchLic("ER");
        if (ERSLic == "1") { listPage += ",'ERS_LMainMenu','ERS_LMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("VAC")) { listPage += ",'ERS_VACMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("VAC")) { listPage += ",'ERS_VACMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("COM")) { listPage += ",'ERS_COMMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("JOB")) { listPage += ",'ERS_JOBMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("EXC")) { listPage += ",'ERS_EXCMenu'"; }
        if (ERSLic == "1" && Reqht.ContainsKey("ESH")) { listPage += ",'ERS_ESHMenu'"; }
        if (ERSLic == "1" && LicDf.FetchLic("SS") == "1" && Reqht.ContainsKey("SWP")) { listPage += ",'ERS_SWPMenu'"; }
            
        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE MnuVisible = 'True' AND MnuType IN (" + listPage + ") AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + pgCs.Version + "',VerID) > 0) ");

        return QMuen.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string FindAdminMenu()
    {
        StringBuilder QMuen = new StringBuilder();

        string lang     = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol  = (pgCs.Lang == "AR") ? "MnuDescription" : "MnuDescription";
        string listPage = "'AD_MainMenu','AD_SubMenu'";
            
        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE MnuVisible = 'True' AND MnuType IN (" + listPage + ") AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + pgCs.Version + "',VerID) > 0) ");

        return QMuen.ToString();
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillMenu(string QMuen)
    {
        DataSet MuenDS = new DataSet();
        MuenDS = DBCs.FetchMenuData(QMuen.ToString());
        xmlDataSource.Data = MuenDS.GetXml();
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillSideMenu(string QMuen)
    {
        bool isFirst = true; 
        string FirstItem = "";
        string SMultiItem = "<div class='square-mult'>";
        string EMultiItem = "</div>";
        string MultiItem = "";
        int iMultiItem = 0;

        DataTable DT = DBCs.FetchData(new SqlCommand(QMuen));
        if (!DBCs.IsNullOrEmpty(DT))
        { 
            DataRow[] DRs = DT.Select("MnuParentID = 0");

            foreach(DataRow DR in DRs)
            {
                DataRow[] SDRs = DT.Select("MnuParentID = " + DR["MnuNumber"].ToString() + "");
                if (SDRs.Length > 0)
                { 
                    Label _lbl = new Label();
                    _lbl.ID    = "lbl_" + DR["MnuNumber"].ToString();
                    _lbl.Text  = DR["MnuText"].ToString();
                    _lbl.CssClass = "menu-subtitle";
                    divSideMenu.Controls.Add(_lbl);              
                }

                isFirst = true;
                FirstItem = "";
                MultiItem = "";
                iMultiItem = 0;
                foreach (DataRow SDR in SDRs)
                {
                    if (isFirst)
                    {
                        FirstItem = "<div class='square-big'>";
                        FirstItem += "<a title='" + SDR["MnuText"].ToString() + "' class='SideMenuItem' href='" + SDR["MnuURL"].ToString().Replace("~", "..") + "'>" + SDR["MnuText"].ToString() + "</a>";
                        FirstItem += "</div>";
                        isFirst = false;
                    }
                    else
                    {
                        if (iMultiItem >= 4)  { iMultiItem = 0; }
                        iMultiItem += 1;
                        if (iMultiItem == 1) { MultiItem += SMultiItem; }

                        MultiItem += "<div class='sub-square'>";
                        MultiItem += "<a title='" + SDR["MnuText"].ToString() + "' class='SideMenuItem' href='" + SDR["MnuURL"].ToString().Replace("~", "..") + "'>" + SDR["MnuText"].ToString() + "</a>";
                        MultiItem += "</div>";

                        if (iMultiItem == 4) { MultiItem += EMultiItem; }
                    }
                }
                if (!string.IsNullOrEmpty(MultiItem) && iMultiItem < 4) {  MultiItem += EMultiItem; }


                if (!string.IsNullOrEmpty(FirstItem)) { divSideMenu.Controls.Add(new LiteralControl("<div class='squares'>")); }               
                if (!string.IsNullOrEmpty(FirstItem)) { divSideMenu.Controls.Add(new LiteralControl(FirstItem)); }
                if (!string.IsNullOrEmpty(MultiItem)) 
                { 
                    //divSideMenu.Controls.Add(new LiteralControl(SMultiItem)); 
                    divSideMenu.Controls.Add(new LiteralControl(MultiItem)); 
                    //divSideMenu.Controls.Add(new LiteralControl(EMultiItem)); 
                }
                if (!string.IsNullOrEmpty(FirstItem)) { divSideMenu.Controls.Add(new LiteralControl("</div>")); }
            }
        }
    }  

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region FavForm Events

    public void FillFavForm()
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM FavoriteFormsView WHERE FavUsrName = @P1 ORDER BY FavType ", new string[] { pgCs.LoginID });
            foreach(DataRow DR in DT.Rows)
            {
                FavForm.Controls.Add(new LiteralControl("<li>"));
                
                LinkButton _lnk = new LinkButton();
                _lnk.ID       = "lnk_GoFavForm_" + DR["FavID"].ToString();
                _lnk.CssClass = "folder"; //CssClass="home"

                if (DR["FavType"].ToString() == "P") { _lnk.PostBackUrl = DR["FormUrl"].ToString(); }
                else if (DR["FavType"].ToString() == "R") { _lnk.PostBackUrl = "~/Pages_Report/ReportViewer.aspx?ID=" + DR["FavID"].ToString() + "_" + DR["FavFormID"].ToString(); }

                Label _lbl = new Label();
                _lbl.ID    = "lbl_" + DR["FavID"].ToString();
                _lbl.Text  = DR[General.Msg("FormNameEn","FormNameAr")].ToString();
                _lnk.Controls.Add(_lbl);              
                FavForm.Controls.Add(_lnk); 

                FavForm.Controls.Add(new LiteralControl("</li>"));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }



    public void FillFavReport()
    {
        try
        {
            divFavReport.Visible = true;

            DataTable DT = new DataTable();
            string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
            DT = DBCs.FetchData(" SELECT FavID, FavFormID AS RepID, FavUsrName AS UsrName, FavName" + lang + " RepName FROM FavoriteForms WHERE FavType = 'R' AND FavUsrName = @P1 ", new string[] { pgCs.LoginID });
        
            dlFavReport.DataSource = DT;
            dlFavReport.DataBind();
            dlFavReport.RepeatColumns = 2;
            upFavReport.Update();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dlFavReport_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "GoFavReport")
            {
                string[] Arg = e.CommandArgument.ToString().Split(',');
                string FavID = Arg[0];
                string RepID = Arg[1];

                RepParametersPro RepProCs = RepCs.FillFavReportParam(FavID, RepID, pgCs.LoginID, pgCs.Lang, pgCs.DateType);

                Session["RepProCs"] = RepProCs;
                Response.Redirect("~/Pages_Report/ReportViewer.aspx");
            }
            else if (e.CommandName == "DeleteReport")
            {
                FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();
                string FavID = e.CommandArgument.ToString();
                FavSqlCs.Delete(FavID, pgCs.LoginID);
                upFavReport.Update();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dlFavReport_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            // e.Item.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.DataList1, "Select$" + e.Item.ItemIndex);
        }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region FavPage Events

    protected void LnkShortcut_Click(object sender, EventArgs e)
    {
        try
        {
            FavoriteFormsPro FavProCs = new FavoriteFormsPro();
            FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();

            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
            string PageName = PageFileInfo.Name + (!string.IsNullOrEmpty(QS) ? "?" + QS : "" );

            if (PageName != "Home.aspx")
            {
                DataTable DT = DBCs.FetchData(" SELECT MnuNumber FROM Menu WHERE MnuURL = @P1 ", new string[] { PageName });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    FavProCs.FavFormID  = DT.Rows[0]["MnuNumber"].ToString();
                    FavProCs.FavUsrName = pgCs.LoginID;
                    FavProCs.FavType    = "P";

                    FavSqlCs.Insert(FavProCs);
                    FillFavPage();
                }
                
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillFavPage()
    {
        try
        {
            divFavPage.Visible = true;
            DataTable DT = new DataTable();

            string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
            DT = DBCs.FetchData(" SELECT FavID, PageID, UsrName, PageName" + lang + " PageName, PageUrl FROM FavoritePagesView WHERE UsrName =  @P1 ", new string[] { pgCs.LoginID });
            
            dlFavPage.DataSource = DT;
            dlFavPage.DataBind();
            dlFavPage.RepeatColumns = 2;
            upFavPage.Update();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dlFavPage_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "GoFavPage")
            {
                string Url = e.CommandArgument.ToString();
                Response.Redirect(Url);
            }
            else if (e.CommandName == "DeletePage")
            {
                FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();
                string FavID = e.CommandArgument.ToString();
                FavSqlCs.Delete(FavID, pgCs.LoginID);
                FillFavPage();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dlFavPage_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            // e.Item.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.DataList1, "Select$" + e.Item.ItemIndex);
        }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Request Events

    // to remove ######################################
    public void FoundRequest()
    {
    }

    public void ShowIsExistingRequest(string ERSLic)
    {
        try
        {
            divRequest.Visible = false;
            if (ERSLic == "1")
            { 
                DataTable DT = DBCs.FetchData(" SELECT COUNT(ErqID) ReqNo FROM EmpRequest WHERE ErqID IN ( SELECT E.ReqID FROM EmpRequestApprovalStatus E WHERE @P1 IN (SELECT * FROM UF_CSVToTable(E.MgrID)) AND E.EraStatus = 0 ) ", new string[] { pgCs.LoginID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string ReqNo = DT.Rows[0]["ReqNo"].ToString();

                    if (Convert.ToInt32(ReqNo) > 0)
                    {
                        lnkRequest.Text = General.Msg("There waiting requests - " + ReqNo + " Requests","يوجد طلبات في الانتظار - عددها " + ReqNo);
                        divRequest.Visible = true;
                        upRequest.Update();
                    }
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages_ERS/RequestApproval.aspx?ID=ALL");
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

