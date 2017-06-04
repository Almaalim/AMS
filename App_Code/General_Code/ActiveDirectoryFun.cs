using System;
using System.Data;
using System.Web.Hosting;
using System.DirectoryServices;
using System.Security.Principal;
using System.Data.SqlClient;

public class ActiveDirectoryFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum ADTypeEnum { EMP, USR }
    static DBFun DBCs = new DBFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public ActiveDirectoryFun() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static UsersPro FillAD(ADTypeEnum ADType, string EmpID, string NationalID, string EmailID)
    {
        UsersPro AD = new UsersPro();
        AD.ADValid         = true;
        AD.ADValidation    = true;
        AD.ADError         = "";
        AD.ADMsgValidation = "";
        AD.ADMsgNotExists  = "";

        DataTable DT = DBCs.FetchData(" SELECT * FROM ADConfig WHERE ADType = @P1 ", new string[] { ADType.ToString() });
        if (DBCs.IsNullOrEmpty(DT)) { AD.ADValid = false; /***/ AD.ADError = General.Msg("Active Directory settings have not been entered", "إعدادات الدليل النشط لم يتم إدخالها"); }
      
        try 
        { 
            //  True = /* FKKhQbK2B48= */  ,  false = /* 9YlaeP0KGBk= */
            string ADEnabled = "0";
            if (DT.Rows[0]["ADEnabled"] != DBNull.Value) { ADEnabled = CryptorEngine.Decrypt(DT.Rows[0]["ADEnabled"].ToString(), true); } //ADDT.Rows[0]["ADEnabled"].ToString(); } 
            if (ADEnabled == "1") { AD.ADEnabled = true; } else { AD.ADEnabled = false; }
        } 
        catch (Exception ex) { AD.ADEnabled = false; }
        
        if (DT.Rows[0]["ADType"]       != DBNull.Value) { if (DT.Rows[0]["ADType"].ToString() == "EMP") { AD.ADType = ActiveDirectoryFun.ADTypeEnum.EMP; } else { AD.ADType = ActiveDirectoryFun.ADTypeEnum.USR; } }
        if (DT.Rows[0]["ADDomainName"] != DBNull.Value) { AD.ADDomainName = DT.Rows[0]["ADDomainName"].ToString(); }
        if (DT.Rows[0]["ADJoinMethod"] != DBNull.Value) { AD.ADJoinMethod = DT.Rows[0]["ADJoinMethod"].ToString(); }
        if (DT.Rows[0]["ADColName"]    != DBNull.Value) { AD.ADColName    = DT.Rows[0]["ADColName"].ToString();    }
        if (DT.Rows[0]["AMSColName"]   != DBNull.Value) { AD.APPColName   = DT.Rows[0]["AMSColName"].ToString();   } 
        if (DT.Rows[0]["ADUsrColName"] != DBNull.Value) { AD.ADUsrColName = DT.Rows[0]["ADUsrColName"].ToString(); }


        if      (string.IsNullOrEmpty(AD.ADDomainName)) { AD.ADValid = false; /***/ AD.ADError = General.Msg("Not enter the domain name", "لم يتم إدخال اسم النطاق"); }
        else if (string.IsNullOrEmpty(AD.ADJoinMethod)) { AD.ADValid = false; /***/ AD.ADError = General.Msg("Not enter the Join Method With Active Directory", "لم يتم إدخال طريقة الربط مع الدليل النشط"); }
        else if (string.IsNullOrEmpty(AD.ADUsrColName)) { AD.ADValid = false; /***/ AD.ADError = General.Msg("Not enter the user column name field in Active Directory", "لم يتم إدخال حقل اسم عمود المستخدم في الدليل النشط"); }

        if (AD.ADJoinMethod == "EmpID") 
        {
            AD.ADVal = EmpID;
            AD.ADMsgNotExists = General.Msg("Employee ID does not exist in Active Directory", "رقم الموظف غير موجود في الدليل النشط");
            if (string.IsNullOrEmpty(EmpID)) { AD.ADValidation = false; /***/ AD.ADMsgValidation = General.Msg("must enter the employee ID first", "يجب إدخال رقم الموظف أولا"); }
        }
        else if (AD.ADJoinMethod == "ID") 
        {
            if (AD.ADType == ADTypeEnum.USR)
            {
                AD.ADVal = EmpID;
                AD.ADMsgNotExists = General.Msg("Employee ID does not exist in Active Directory", "رقم الموظف غير موجود في الدليل النشط");
                if (string.IsNullOrEmpty(EmpID)) { AD.ADValidation = false; /***/ AD.ADMsgValidation = General.Msg("must enter the employee ID first", "يجب إدخال رقم الموظف أولا"); }
            }

            if (AD.ADType == ADTypeEnum.EMP) 
            {
                AD.ADVal = NationalID;
                AD.ADMsgNotExists = General.Msg("National ID does not exist in Active Directory", "رقم الهوية غير موجود في الدليل النشط");
                if (string.IsNullOrEmpty(NationalID)) { AD.ADValidation = false; /***/ AD.ADMsgValidation = General.Msg("must enter the National ID first", "يجب إدخال رقم الهوية أولا"); }
            }
        }
        else if (AD.ADJoinMethod == "Email") 
        {
            AD.ADVal = EmailID;
            AD.ADMsgNotExists = General.Msg("Email does not exist in Active Directory", "البريد الالكتروني غير موجود في الدليل النشط");
            if (string.IsNullOrEmpty(EmailID)) { AD.ADValidation = false; /***/ AD.ADMsgValidation = General.Msg("must enter the email first", "يجب إدخال البريد الالكتروني أولا"); }
        }

        return AD;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetAD(UsersPro AD)
    {
        if (string.IsNullOrEmpty(AD.ADVal)) { return ""; }

        string Val = "";
        if (AD.ADJoinMethod == "EmpID") { Val = AD.ADVal; }
        if (AD.ADJoinMethod == "ID" && AD.ADType == ADTypeEnum.USR) { Val = getNationalID(AD.ADVal); }
        if (AD.ADJoinMethod == "ID" && AD.ADType == ADTypeEnum.EMP) { Val = AD.ADVal;    }
        if (AD.ADJoinMethod == "Email") { Val = AD.ADVal; }


        string ADUser = "";
        ADUser = GetADData(AD.ADDomainName,AD.ADUsrColName, AD.ADColName, Val);

        return ADUser;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetWinLoginName()
    {
        try
        {
            //يجب تحويل الدخول إلى Windows Authentication وإلغاء Anonymouse Authentication في iis manager - Authentication
            // يجب إضافة الخاصية <identity impersonate="true" /> إلى web.config

            string ADDomainName = GetDomainFromDB(ADTypeEnum.USR);
            if (string.IsNullOrEmpty(ADDomainName)) { return ""; }

            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            var result = wi.Name;
            var anonymous = wi.IsAnonymous;      //For ensure your authentification mode
            var Windows = wi.IsAuthenticated;

            string WinName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string LoginName = GetLoginName(WinName);
            
            return LoginName;
        }
        catch (Exception ex) { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetDomainFromDB(ADTypeEnum ADType)
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM ADConfig WHERE ADType = @P1 ", new string[] { ADType.ToString() });
        if (DBCs.IsNullOrEmpty(DT)) { return ""; }

        string ADDomainName = "";
        if (DT.Rows[0]["ADDomainName"] != DBNull.Value) { ADDomainName = DT.Rows[0]["ADDomainName"].ToString(); } else { return ""; }

        if (string.IsNullOrEmpty(ADDomainName)) { return ""; }

        return ADDomainName;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool Authenticate(string login, string password, string domain)
    {
        bool authentic = false;
        try
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, login, password);
            object nativeObject = entry.NativeObject;

            authentic = true;
        }
        catch (DirectoryServicesCOMException) { }
        catch (Exception ex) { }
        return authentic;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private static string GetDomainName(string winLogin)
    {
        int hasDomain = winLogin.IndexOf(@"\");
        if (hasDomain > 0) { winLogin = winLogin.Substring(0, hasDomain); }
        return winLogin;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private static string GetLoginName(string winLogin)
    {
        int hasDomain = winLogin.IndexOf(@"\");
        if (hasDomain > 0) { winLogin = winLogin.Remove(0, hasDomain + 1); }
        return winLogin;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public static string ADValidation(ActiveDirectoryPro AD, string EmpID, string ID, string Email)
    //{
    //    string Msg = ""; 

    //    if (AD.ADJoinMethod == "EmpID" && string.IsNullOrEmpty(EmpID)) { Msg = General.Msg("must enter the employee ID first", "يجب إدخال رقم الموظف أولا"); }
    //    if (AD.ADJoinMethod == "ID") 
    //    { 
    //        if (AD.ADType == ADTypeEnum.USR && string.IsNullOrEmpty(ID)) { Msg = General.Msg("must enter the employee ID first", "يجب إدخال رقم الموظف أولا"); }
    //        if (AD.ADType == ADTypeEnum.EMP && string.IsNullOrEmpty(ID)) { Msg = General.Msg("must enter the National ID first", "يجب إدخال رقم الهوية أولا"); }
    //    }
    //    if (AD.ADJoinMethod == "Email" && string.IsNullOrEmpty(Email)) { Msg = General.Msg("must enter the email first", "يجب إدخال البريد الالكتروني أولا"); }
        
    //    return Msg;
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string getNationalID(string EmpID)
    {
        DataTable DT = DBCs.FetchData(" SELECT EmpNationalID FROM Employee WHERE EmpID = @P1 ", new string[] { EmpID });
        if (DBCs.IsNullOrEmpty(DT)) { return ""; }
        
        if (DT.Rows[0][0] == DBNull.Value) { return ""; }
        return DT.Rows[0][0].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string getJoinMethod(ADTypeEnum ADType)
    {
        DataTable DT = DBCs.FetchData(" SELECT ADJoinMethod FROM ADConfig WHERE ADType = @P1 ", new string[] { ADType.ToString() });
        if (DBCs.IsNullOrEmpty(DT)) { return ""; }

        if (DT.Rows[0][0] == DBNull.Value) { return ""; }
        return DT.Rows[0][0].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool UserValid(UsersPro AD, string Val)
    {
        try
        {
            string Usr = GetADData(AD.ADDomainName, AD.ADUsrColName, AD.ADUsrColName, Val);
            if (string.IsNullOrEmpty(Usr)) { return false; } else { return true; }
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected static string GetADData(string Domain,string UsrProperty, string Property, string Val)
    {
        try
        {
            using (HostingEnvironment.Impersonate())
            {
                DirectoryEntry entry      = new DirectoryEntry("LDAP://" + Domain);
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(objectCategory=person)(" + Property + "=" + Val + "))";
                string usr = "";

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    usr = GetProperty(sResultSet, UsrProperty);

                    //string UserName       = GetProperty(sResultSet, "samaccountname");
                    //string LoginName      = GetProperty(sResultSet, "cn");  // Login Name                  
                    //string FirstName      = GetProperty(sResultSet, "givenName"); // First Name                    
                    //string MiddleInitials = GetProperty(sResultSet, "initials"); // Middle Initials
                    //string email = GetProperty(sResultSet, "mail"); // email address
                }

                return usr;
            }      
        }
        catch (Exception ex) { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
        {
            return searchResult.Properties[PropertyName][0].ToString();
        }
        else
        {
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool ADEnabled()
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT ADEnabled FROM ADConfig WHERE ADType = 'USR' "));
        if (DBCs.IsNullOrEmpty(DT))  { return false; }

        if (DT.Rows[0][0] == DBNull.Value)  { return false; }
        
        string ADEnabled = DT.Rows[0]["ADEnabled"].ToString();
        try { ADEnabled = CryptorEngine.Decrypt(ADEnabled, true); } catch (Exception e1) { }
        if (ADEnabled == "1") { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public static string GetAD(ADTypeEnum ADType, string EmpID, string ID, string Email)
    //{
    //    DataTable ADDT = (DataTable)hc.FetchData("SELECT * FROM ADConfig WHERE ADType = '" + ADType.ToString() + "'");
    //    if (!hc.IsValidDT(ADDT)) { return ""; }

    //    string ADJoinMethod = "";
    //    string ADColName    = "";
    //    string AMSColName   = "";
    //    string ADDomainName = "";
    //    string ADUsrColName = "";

    //    if (ADDT.Rows[0]["ADJoinMethod"] != DBNull.Value) { ADJoinMethod = ADDT.Rows[0]["ADJoinMethod"].ToString(); } else { return ""; }
    //    if (ADDT.Rows[0]["ADColName"]    != DBNull.Value) { ADColName    = ADDT.Rows[0]["ADColName"].ToString();    } else { return ""; }
    //    if (ADDT.Rows[0]["AMSColName"]   != DBNull.Value) { AMSColName   = ADDT.Rows[0]["AMSColName"].ToString();   } else { return ""; }
    //    if (ADDT.Rows[0]["ADDomainName"] != DBNull.Value) { ADDomainName = ADDT.Rows[0]["ADDomainName"].ToString(); } else { return ""; }
    //    if (ADDT.Rows[0]["ADUsrColName"] != DBNull.Value) { ADUsrColName = ADDT.Rows[0]["ADUsrColName"].ToString(); } else { return ""; }

    //    if (string.IsNullOrEmpty(ADColName) || string.IsNullOrEmpty(AMSColName) || string.IsNullOrEmpty(ADDomainName) || string.IsNullOrEmpty(ADUsrColName)) { return ""; }

    //    string Val = "";
    //    if (ADJoinMethod == "EmpID") { Val = EmpID; }
    //    if (ADJoinMethod == "ID" && ADType == ADTypeEnum.USR) { Val = getNationalID(ID); }
    //    if (ADJoinMethod == "ID" && ADType == ADTypeEnum.EMP) { Val = ID;    }
    //    if (ADJoinMethod == "Email") { Val = Email; }


    //    string ADUser = "";
    //    ADUser = GetADData(ADDomainName,ADUsrColName, ADColName, Val);

    //    return ADUser;
    //}
}