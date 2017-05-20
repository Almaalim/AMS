using System;
using System.Linq;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersSql logSqlCs = new UsersSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string calendarType = string.Empty;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_PreInit(object sender, EventArgs e) 
    { 
        //string Applang = (Session["Language"] != null) ? Session["Language"].ToString() : getAppLanguage();
        //Session["Language"] = Applang;
        //if (Session["Language"].ToString() == "AR") { Session["Part1align"] = "left"; Session["Part2align"] = "right"; } else { Session["Part1align"] = "right"; Session["Part2align"] = "left";}
        //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo((Applang == "AR") ? "ar-Sa" : "en-US");
    
        string Applang = (ViewState["Language"] != null) ? ViewState["Language"].ToString() : getAppLanguage();
        ViewState["Language"] = Applang;
        if (ViewState["Language"].ToString() == "AR") { ViewState["Part1align"] = "left"; ViewState["Part2align"] = "right"; } else { ViewState["Part1align"] = "right"; ViewState["Part2align"] = "left";}
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo((Applang == "AR") ? "ar-Sa" : "en-US");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getAppLanguage()
    {
        try
        {
            string[] AllKeys = ConfigurationManager.AppSettings.AllKeys;
            if (AllKeys.Contains("Language.StartDefault"))
            {
                string lang = ConfigurationManager.AppSettings["Language.StartDefault"].ToString();
                if (!string.IsNullOrEmpty(lang)) { return lang; }
            }
        }
        catch { }

        return "EN";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();

        //txtname.Text = "admin";
        //txtpass.Attributes["value"] = "admin";

        if (ViewState["Language"] != null)
        {
            string Language = ViewState["Language"].ToString();
            if (Language == "AR") { pnlMain.Attributes.Add("dir", "rtl"); } else { pnlMain.Attributes.Add("dir", "ltr"); }
        }

        txtname.Focus();

        /*** Check AMS Connect ********************************************/
        if (!DBCs.isConnect()) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, "No connection to the database <br /> لا يوجد اتصال بقاعدة البيانات"); return; }
        /******************************************************************/

        /*** Check AMS License ********************************************/
        if (LicDf.FetchLic("LC") != "1") { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, "There is no License to use this Application <br /> لا يوجد ترخيص لإستخدام هذا البرنامج"); return; }
        /******************************************************************/

        string ActiveVersion = LicDf.FindActiveVersion();
        if (ActiveVersion == "BorderGuard") { loginTD.Attributes["class"] = "tp_logoin_BorderGuard"; } else { loginTD.Attributes["class"] = "tp_logoin"; }

        if (!IsPostBack)
        {
            if (ActiveVersion == "BorderGuard") { divLogo.Visible = false; tdLogo.Width = "150px"; }
            else { divLogo.Visible = true; ShowLogo(); tdLogo.Width = "250px";}


            ViewState["DateType"]   = GenCs.GetCalendarType();
            ViewState["DateFormat"] = GenCs.GetCalendarFormat();
            
            DataTable CDT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Configuration "));
            if (!DBCs.IsNullOrEmpty(CDT)) 
            {
                int TimeOutValue = Convert.ToInt32(CDT.Rows[0]["cfgSessionDuration"]);
                if (TimeOutValue > 0) { Session.Timeout = TimeOutValue / 60; }
            }

            bool AD_Active = ActiveDirectoryFun.ADEnabled();
            if (AD_Active)
            {
                if (ActiveVersion == "BorderGuard") { lblDomain.Visible = lblDomainName.Visible = false; }
                else { lblDomain.Visible = lblDomainName.Visible = true; }
                    
                    
                lblDomainName.Text = ActiveDirectoryFun.GetDomainFromDB(ActiveDirectoryFun.ADTypeEnum.USR);
                txtname.Text = ActiveDirectoryFun.GetWinLoginName();
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowLogo()
    {
        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ApplicationSetup "));
            if (!DBCs.IsNullOrEmpty(DT)) { if (DT.Rows[0][0] != DBNull.Value) { imgLogo.ImageUrl = "~/Pages_Mix/ReadImage.aspx?ID=Logo"; return; } }

            imgLogo.ImageUrl = "~/images/LoginInsertLogo.jpg";
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnLogin_Click(object sender, EventArgs e)
    { 
        if (!Page.IsValid) { return; }

        bool   isLogin   = false;
        string loginName = txtname.Text;
        string password  = txtpass.Text;
       
        bool AD_Active = ActiveDirectoryFun.ADEnabled();

        if (AD_Active)
        {
            string AD_Domain = lblDomainName.Text = ActiveDirectoryFun.GetDomainFromDB(ActiveDirectoryFun.ADTypeEnum.USR);
            if (ActiveDirectoryFun.Authenticate(loginName, password, AD_Domain)) { isLogin = FindLoginInfo("AD", loginName, password); }
        }
        else
        {
            isLogin = FindLoginInfo("DB", loginName, password);
        }
            
        if (isLogin)
        {
            ///////////////////////////////////////////////////////////////////////////////////////
            Session["DateType"]   = ViewState["DateType"].ToString();
            Session["DateFormat"] = ViewState["DateFormat"].ToString();
            Session["Part1align"] = ViewState["Part1align"].ToString();
            Session["Part2align"] = ViewState["Part2align"].ToString();
            ///////////////////////////////////////////////////////////////////////////////////////
            DataLang();
            ///////////////////////////////////////////////////////////////////////////////////////
            LoginReg();
            ///////////////////////////////////////////////////////////////////////////////////////
            Response.Redirect(ViewState["HomePage"].ToString(), false);
            ///////////////////////////////////////////////////////////////////////////////////////
        }
        else
        {
            txtname.Focus();
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, "Are You do not have access,Please contact the Administrator <br /> لا تملك صلاحية للدخول ، الرجاء الإتصال بمدير النظام");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FindLoginInfo(string Type, string loginName, string loginPass)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        bool isLogin = false;

        StringBuilder QU = new StringBuilder();
        QU.Append(" SELECT * FROM LoginView ");
        if (Type == "DB") { QU.Append(" WHERE UsrName = @P1 "); }  
        if (Type == "AD") { QU.Append(" WHERE UsrAD   = @P1 "); }
        
        DataTable DT = DBCs.FetchData(QU.ToString(), new string[] { loginName });
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            //////////////////////////////////////////////////////////////
            if (Type == "DB")
            {
                if (DT.Rows[0]["UsrPass"].ToString().Trim() == loginPass && DT.Rows[0]["UsrName"].ToString().Trim() == loginName) { isLogin = true; } else { return false; }         
            }
            else if (Type == "AD") { isLogin = true; }

            /////////////////////////////////////////////////////////////

            Session["PreferedCulture"] = "en-US";
            Session["Language"] = DT.Rows[0]["UsrLang"].ToString();
            Session["UserName"] = DT.Rows[0]["UsrName"].ToString();
            Session["MainUser"] = DT.Rows[0]["MainUser"].ToString();
            if (DT.Rows[0]["EmpID"] != DBNull.Value) { Session["LoginEmpID"] = DT.Rows[0]["EmpID"].ToString(); } else { Session["LoginEmpID"] = ""; }

            string LoginType = DT.Rows[0]["LoginType"].ToString();

            if      (LoginType == "USR" || LoginType == "ACT") { Session["UserType"] = "USR"; /**/ ViewState["HomePage"] = @"~/Pages_Mix/Home.aspx"; }  //@"~/Pages_User/PermissionGroups.aspx"; } 
            else if (LoginType == "EMP")                       { Session["UserType"] = "EMP"; /**/ ViewState["HomePage"] = @"~/Pages_Mix/Home.aspx"; }

            if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) { Session["DepartmentList"] = DT.Rows[0]["UsrDepartments"].ToString(); } else { Session["UsrDepartments"] = ""; }
            //Session["DepartmentList"] = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(),true); 

            Session["MenuPermissions"]   = "";
            Session["ReportPermissions"] = "";
            if (DT.Rows[0]["PGrpPermissions"] != DBNull.Value) { Session["MenuPermissions"]   = CryptorEngine.Decrypt(DT.Rows[0]["PGrpPermissions"].ToString(), true); }
            if (DT.Rows[0]["RGrpPermissions"] != DBNull.Value) { Session["ReportPermissions"] = CryptorEngine.Decrypt(DT.Rows[0]["RGrpPermissions"].ToString(), true); }

            Session["ActiveVersion"] = LicDf.FindActiveVersion();
            Session["ShowedAlert"] = "false";
        }

        return isLogin;
    }    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DataLang()
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgDataLang FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            DataRow Configdr = (DataRow)DT.Rows[0];
            if (Configdr["cfgDataLang"].ToString() == "B" || Configdr["cfgDataLang"].ToString() == "E") { Session["DataLangEn"] = true; } else { Session["DataLangEn"] = false; }
            if (Configdr["cfgDataLang"].ToString() == "B" || Configdr["cfgDataLang"].ToString() == "A") { Session["DataLangAr"] = true; } else { Session["DataLangAr"] = false; }
        }
        else
        {
            Session["DataLangEn"] = true;
            Session["DataLangAr"] = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void LoginReg()
    {
        try
        {
            string clientPCName;
            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
            clientPCName = computer_name[0].ToString();
            logSqlCs.InOutLog_Insert(Session["UserName"].ToString(), clientPCName);
        }
        catch (Exception e1) { }
    }
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
