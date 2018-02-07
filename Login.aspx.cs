using System;
using System.Linq;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using Elmah;

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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_PreInit(object sender, EventArgs e)
    {
        string Applang = (Application["LoginLang"] != null) ? Application["LoginLang"].ToString() : getAppLanguage();
        Application["LoginLang"] = Applang;
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

        txtname.Text = "admin";
        txtpass.Attributes["value"] = "admin";
        //string DecPass = CryptorEngine.Encrypt("1,2,3,4", true);

        if (Application["LoginLang"] != null)
        {
            string Language = Application["LoginLang"].ToString();
            if (Language == "AR") { pnlMain.Attributes.Add("dir", "rtl"); } else { pnlMain.Attributes.Add("dir", "ltr"); }
        }

        txtname.Focus();

        /*** Check AMS Connect ********************************************/
        if (!DBCs.isConnect()) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, "No connection to the database <br /> لا يوجد اتصال بقاعدة البيانات"); return; }
        /******************************************************************/

        /*** Check AMS License ********************************************/
        if (LicDf.FetchLic("LC") != "1") { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, "There is no License to use this Application <br /> لا يوجد ترخيص لإستخدام هذا البرنامج"); return; }
        /******************************************************************/

        loginTD.Attributes["class"] = "tp_logoin";

        if (!IsPostBack) { }
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

            DataTable DT = DBCs.FetchData(" SELECT UsrAD FROM LoginView WHERE UsrAD = @P1 ", new string[] { loginName });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (ActiveDirectoryFun.Authenticate(loginName, password, AD_Domain)) { isLogin = FindLoginInfo("AD", loginName, password); }
            }
            else
            {
                isLogin = FindLoginInfo("DB_AD", loginName, password);
            }    
        }
        else
        {
            isLogin = FindLoginInfo("DB", loginName, password);
        }
            
        if (isLogin)
        {
            ///////////////////////////////////////////////////////////////////////////////////////
            getApplicationSetting();
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
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        bool isLogin = false;

        StringBuilder QU = new StringBuilder();
        QU.Append(" SELECT * FROM LoginView ");
        if (Type == "DB")    { QU.Append(" WHERE UsrName = @P1 "); }  
        if (Type == "AD")    { QU.Append(" WHERE UsrAD   = @P1 "); }
        if (Type == "DB_AD") { QU.Append(" WHERE UsrName = @P1 AND UsrAD IS NULL "); }      
        //QU.Append(" AND GETDATE() <= '2018-02-02 23:59:59' ");

        DataTable DT = DBCs.FetchData(QU.ToString(), new string[] { loginName });
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            //////////////////////////////////////////////////////////////
            if (Type == "DB" || Type == "DB_AD")
            {
                //isLogin = true;
                string DecPass = CryptorEngine.Decrypt(DT.Rows[0]["UsrPass"].ToString(), true);
                if (DecPass == loginPass && DT.Rows[0]["UsrName"].ToString().Trim() == loginName) { isLogin = true; } else { return false; }
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

            if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) { Session["DepartmentList"] = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(), true); } else { Session["UsrDepartments"] = ""; }
            
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
    protected void getApplicationSetting()
    {
        Session["DataLangEn"] = true;
        Session["DataLangAr"] = true;
        Session["DateType"]   = "Gregorian";
        Session["DateFormat"] = "dd/MM/yyyy";

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT AppCalendar,AppDateFormat,AppDataLangRequired, AppSessionDuration FROM ApplicationSetup "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataRow DR = (DataRow)DT.Rows[0];
            if (DR["AppCalendar"].ToString() == "H") { Session["DateType"] = "Hijri"; } else { Session["DateType"] = "Gregorian"; }
            if (!string.IsNullOrEmpty(DR["AppDateFormat"].ToString())) { Session["DateFormat"] = DR["AppDateFormat"].ToString(); }
            
            if (DR["AppDataLangRequired"].ToString() == "B" || DR["AppDataLangRequired"].ToString() == "E") { Session["DataLangEn"] = true; } else { Session["DataLangEn"] = false; }
            if (DR["AppDataLangRequired"].ToString() == "B" || DR["AppDataLangRequired"].ToString() == "A") { Session["DataLangAr"] = true; } else { Session["DataLangAr"] = false; }
            if (DR["AppSessionDuration"] != DBNull.Value)
            {
                int TimeOutValue = Convert.ToInt32(DR["AppSessionDuration"]);
                if (TimeOutValue > 0) { Session.Timeout = TimeOutValue / 60; }
            }
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

            string IPAddress = GetIPAddress();

            logSqlCs.InOutLog_Insert(Session["UserName"].ToString(), clientPCName, IPAddress);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public string GetIPAddress()
    {
        string IPAddress = "";

        try
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) { IPAddress = Convert.ToString(IP); }
            }          
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }

        return IPAddress;
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
