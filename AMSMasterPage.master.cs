using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using Elmah;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

public partial class AMSMasterPage : System.Web.UI.MasterPage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersSql logSqlCs = new UsersSql();
    CustPageSql CpgSqlCs = new CustPageSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun DTCs = new DTFun();
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
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0) { Request.Browser.Adapters.Clear(); }
            lnkLanguage.Text = General.Msg("عربي", "English");
            SetPhotoUser();
            CreateMenu(pgCs.LoginType, pgCs.LoginEmpID, false);
            FillFavForm();
            /*** Fill Session ************************************/

            //int ss = Session.Timeout;
            //Session.Timeout = 2;
            //Session.SessionID
            //try {// Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 10) + "; URL=Login.aspx"); }
            //catch (Exception ex) { }

            if (!IsPostBack)
            {
                //Dear Developer
                // I am adding this line for more than 5 times in this project and has been removed.Which brings up extra work for me.So if you concerned about team work and since
                // we are working on source control i hope that i dont need to add rework on this again.
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PostbackFunction();", true);
                ChangeLogo();
                SetPageTitel();
                ShowLogo();

                lblcurrentYear.Text = DateTime.Now.Year.ToString();
                lblUserName.Text = " " + pgCs.LoginID;

                if (pgCs.LoginType == "USR")
                {
                    //FillFavForm();
                    ShowIsExistingRequest();
                    lnkShortcut.Enabled = true;
                }
                else if (pgCs.LoginType == "EMP")
                {
                    lnkShortcut.Enabled = false;

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
        //Dear Developer
        // I am adding this line for more than 5 times in this project and has been removed.Which brings up extra work for me.So if you concerned about team work and since
        // we are working on source control i hope that i dont need to add rework on this again.
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PostbackFunction();", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetPageTitel()
    {
        try
        {
            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
            string PageName = PageFileInfo.Name + (!string.IsNullOrEmpty(QS) ? "?" + QS : "");
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
    protected void SetPhotoUser()
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath("~/images/Users/" + pgCs.LoginID.Trim() + ".png");
            if (File.Exists(path)) { UserImg.ImageUrl = "images/Users/" + pgCs.LoginID.Trim() + ".png"; } else { UserImg.ImageUrl = "~/images/Users/Default.png"; }
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
            if (pgCs.Version == "BorderGuard") { divCustLogo.Visible = false; }
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
    public void Refresh()
    {
        try
        {
            if (pgCs.LoginType == "USR")
            {
                FillFavForm();
                ShowIsExistingRequest();
                lnkShortcut.Enabled = true;
            }
        }
        catch (Exception e1) { }
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

        string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol = (pgCs.Lang == "AR") ? "MnuArabicDescription" : "MnuDescription";
        string LicPage = LicDf.FindLicPage();
        string listPage = "'SubMenu','AD_MainMenu','AD_SubMenu'" + (string.IsNullOrEmpty(LicPage) ? "" : "," + LicPage);

        string MPerm = (GenCs.IsNullOrEmpty(Session["MenuPermissions"])) ? "0" : Session["MenuPermissions"].ToString();
        string RPerm = (GenCs.IsNullOrEmpty(Session["ReportPermissions"])) ? "0" : Session["ReportPermissions"].ToString();

        QMuen.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText," + DescCol + " as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        QMuen.Append(" FROM Menu WHERE MnuVisible = 'True' AND MnuType IN ('Menu') ");

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

        string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol = (pgCs.Lang == "AR") ? "MnuArabicDescription" : "MnuDescription";
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

        string lang = (pgCs.Lang == "AR") ? "Ar" : "En";
        string DescCol = (pgCs.Lang == "AR") ? "MnuArabicDescription" : "MnuDescription";
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

            foreach (DataRow DR in DRs)
            {
                DataRow[] SDRs = DT.Select("MnuParentID = " + DR["MnuNumber"].ToString() + "");
                if (SDRs.Length > 0)
                {
                    Label _lbl = new Label();
                    _lbl.ID = "lbl_" + DR["MnuNumber"].ToString();
                    _lbl.Text = DR["MnuText"].ToString();
                    _lbl.CssClass = "menu-subtitle";
                    divSideMenu.Controls.Add(_lbl);
                }

                isFirst = true;
                FirstItem = "";
                MultiItem = "";
                iMultiItem = 0;
                foreach (DataRow SDR in SDRs)
                {
                    
                    string menuCss = SDR["MnuDescription"].ToString();
                    menuCss = menuCss.Replace(" ", "");
                    menuCss = Regex.Replace(menuCss, @"[^0-9a-zA-Z]+", "");
                    if (isFirst)
                    {
                        
                        FirstItem = "<div class='square-big'>";
                        FirstItem += "<a title='" + SDR["MnuText"].ToString() + "' class='SideMenuItem " + menuCss + "' href='" + SDR["MnuURL"].ToString().Replace("~", "..") + "'>" + SDR["MnuText"].ToString() + "</a>";
                        FirstItem += "</div>";
                        isFirst = false;
                    }
                    else
                    {
                         
                        if (iMultiItem >= 4) { iMultiItem = 0; }
                        iMultiItem += 1;
                        if (iMultiItem == 1) { MultiItem += SMultiItem; }

                        MultiItem += "<div class='sub-square'>";
                        MultiItem += "<a title='" + SDR["MnuText"].ToString() + "' class='SideMenuItem " + menuCss + "' href='" + SDR["MnuURL"].ToString().Replace("~", "..") + "'>" + SDR["MnuText"].ToString() + "</a>";
                        MultiItem += "</div>";

                        if (iMultiItem == 4) { MultiItem += EMultiItem; }
                    }
                }
                if (!string.IsNullOrEmpty(MultiItem) && iMultiItem < 4) { MultiItem += EMultiItem; }


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

    protected void LnkShortcut_Click(object sender, EventArgs e)
    {
        try
        {
            FavoriteFormsPro FavProCs = new FavoriteFormsPro();
            FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();

            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
            string PageName = PageFileInfo.Name + (!string.IsNullOrEmpty(QS) ? "?" + QS : "");

            if (PageName != "Home.aspx")
            {
                DataTable DT = DBCs.FetchData(" SELECT MnuNumber FROM Menu WHERE MnuURL = @P1 ", new string[] { PageName });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    FavProCs.FavFormID = DT.Rows[0]["MnuNumber"].ToString();
                    FavProCs.FavUsrName = pgCs.LoginID;
                    FavProCs.FavType = "P";

                    FavSqlCs.Insert(FavProCs);
                }

                Refresh();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillFavForm()
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM FavoriteFormsView WHERE FavUsrName = @P1 ORDER BY FavType ", new string[] { pgCs.LoginID });
            foreach (DataRow DR in DT.Rows)
            {
                FavForm.Controls.Add(new LiteralControl("<li>"));

                LinkButton _lnk = new LinkButton();
                _lnk.ID = "lnk_GoFavForm_" + DR["FavID"].ToString();
                _lnk.CssClass = "folder"; //CssClass="home"

                if (DR["FavType"].ToString() == "P") { _lnk.PostBackUrl = DR["FormUrl"].ToString(); }
                else if (DR["FavType"].ToString() == "R") { _lnk.PostBackUrl = "~/Pages_Report/ReportViewer.aspx?ID=" + DR["FavID"].ToString() + "_" + DR["FavFormID"].ToString(); }

                Label _lbl = new Label();
                _lbl.ID = "lbl_" + DR["FavID"].ToString();
                _lbl.Text = DR[General.Msg("FormNameEn", "FormNameAr")].ToString();
                _lnk.Controls.Add(_lbl);
                FavForm.Controls.Add(_lnk);

                FavForm.Controls.Add(new LiteralControl("</li>"));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void dlFavReport_ItemCommand(object source, DataListCommandEventArgs e)
    {
        //FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();
        //string FavID = e.CommandArgument.ToString();
        //FavSqlCs.Delete(FavID, pgCs.LoginID);

        //FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();
        //string FavID = e.CommandArgument.ToString();
        //FavSqlCs.Delete(FavID, pgCs.LoginID);
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Request Events

    public void ShowIsExistingRequest()
    {
        try
        {
            string ERSLic = LicDf.FetchLic("ER");

            divRequest.Visible = false;
            if (ERSLic == "1")
            {
                DataTable DT = DBCs.FetchData(" SELECT COUNT(ErqID) ReqNo FROM EmpRequest WHERE ErqID IN ( SELECT E.ReqID FROM EmpRequestApprovalStatus E WHERE @P1 IN (SELECT * FROM UF_CSVToTable(E.MgrID)) AND E.EraStatus = 0 ) ", new string[] { pgCs.LoginID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string ReqNo = DT.Rows[0]["ReqNo"].ToString();

                    if (Convert.ToInt32(ReqNo) > 0)
                    {
                        lnkRequest.Text = General.Msg("There waiting requests - " + ReqNo + " Requests", "يوجد طلبات في الانتظار - عددها " + ReqNo);
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