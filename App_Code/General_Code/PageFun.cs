using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Text;
using System.Data.SqlClient;

public class PageFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _Lang;
    public  string Lang { get { return _Lang; } set { _Lang = value; } }

    private bool _LangEn;
    public  bool LangEn { get { return _LangEn; } set { _LangEn = value; } }

    private bool _LangAr;
    public  bool LangAr { get { return _LangAr; } set { _LangAr = value; } }

    private string _DateType;
    public  string DateType { get { return _DateType; } set { _DateType = value; } }

    private string _DateFormat;
    public  string DateFormat { get { return _DateFormat; } set { _DateFormat = value; } }

    private string _LoginID;
    public  string LoginID { get { return _LoginID; } set { _LoginID = value; } }

    private string _LoginEmpID;
    public  string LoginEmpID { get { return _LoginEmpID; } set { _LoginEmpID = value; } }

    private string _LoginMainUser;
    public  string LoginMainUser { get { return _LoginMainUser; } set { _LoginMainUser = value; } }

    private string _LoginType;
    public  string LoginType { get { return _LoginType; } set { _LoginType = value; } }

    private string _DepList;
    public  string DepList { get { return _DepList; } set { _DepList = value; } }

    private string _Version;
    public  string Version { get { return _Version; } set { _Version = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillSession() { Fill(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Fill()
    {
        try
        {
            System.Web.SessionState.HttpSessionState SS = HttpContext.Current.Session;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo((SS["PreferedCulture"] != null) ? SS["PreferedCulture"].ToString() : "en-US");

            if (SS["Language"]       != null) { Lang          = SS["Language"].ToString(); } else { Lang = "EN"; }
            if (SS["DateType"]       != null) { DateType      = SS["DateType"].ToString(); }
            if (SS["DateFormat"]     != null) { DateFormat    = SS["DateFormat"].ToString(); }
            if (SS["UserName"]       != null) { LoginID       = SS["UserName"].ToString(); } else { GoLogin(); }
            if (SS["MainUser"]       != null) { LoginMainUser = SS["MainUser"].ToString(); } else { GoLogin(); }
            if (SS["UserType"]       != null) { LoginType     = SS["UserType"].ToString(); } else { GoLogin(); }
            
            if (SS["DepartmentList"] != null) { DepList       = SS["DepartmentList"].ToString(); } else { DepList = "0"; }
            if (SS["ActiveVersion"]  != null) { Version       = SS["ActiveVersion"].ToString();  } else { GoLogin(); }
            
            if (SS["DataLangEn"]     != null) { LangEn = Convert.ToBoolean(SS["DataLangEn"]); } else { LangEn = false; }
            if (SS["DataLangAr"]     != null) { LangAr = Convert.ToBoolean(SS["DataLangAr"]); } else { LangAr = false; }

            if (SS["LoginEmpID"]     != null) { LoginEmpID  = SS["LoginEmpID"].ToString(); } else { GoLogin(); }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillDTSession()
    {
        try
        {
            System.Web.SessionState.HttpSessionState SS = HttpContext.Current.Session;
            
            if (SS["DateType"]   != null) { DateType   = SS["DateType"].ToString(); }
            if (SS["DateFormat"] != null) { DateFormat = SS["DateFormat"].ToString(); }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillLangSession()
    {
        try
        {
            System.Web.SessionState.HttpSessionState SS = HttpContext.Current.Session;
            
            if (SS["Language"] != null) { Lang = SS["Language"].ToString(); } else { Lang = "EN";}
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CheckAMSLicense() { if (LicDf.FetchLic("LC") != "1") { GoLogin(); } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CheckERSLicense() { if (LicDf.FetchLic("LC") != "1" || LicDf.FetchLic("ER") != "1") { GoLogin(); } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    public Hashtable getPerm(string UrlPage)
    {
        try
        {
            Hashtable ht = new Hashtable();

            if (HttpContext.Current.Session["MenuPermissions"] != null) 
            { 
                ht = getPermission(HttpContext.Current.Session["MenuPermissions"].ToString(), UrlPage); 
            } 
            else { GoLogin(); }

            return ht;
        }
        catch (Exception ex) { return new Hashtable(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void GoLogin() { HttpContext.Current.Server.Transfer(@"~/login.aspx"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Hashtable getPermission(string pUserPermissions,string pUrlPath)
    {
        Hashtable ht = new Hashtable();
        StringBuilder QPerm = new StringBuilder();
        
        string[] arrUrlPath = pUrlPath.Split('?');
        string UrlPath = arrUrlPath[0];
        string QSPath = "";
        if (arrUrlPath.Length > 1) { QSPath = "?" + arrUrlPath[1]; }

        System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(UrlPath);
        
        QPerm.Remove(0, QPerm.Length);
        QPerm.Append("SELECT MnuTextEn FROM Menu ");
        QPerm.Append(" WHERE MnuVisible = 'True' AND MnuType ='Command' AND MnuParentID > 0 ");
        QPerm.Append(" AND MnuPermissionID IN (SELECT MnuPermissionID FROM Menu WHERE MnuURL = '" +  PageFileInfo.Name + QSPath + "') ");
        QPerm.Append(" AND MnuNumber IN (" + pUserPermissions + ")" );
        
        DataTable DT = DBCs.FetchData(new SqlCommand(QPerm.ToString()));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow dr = (DataRow)DT.Rows[i];
                ht.Add(DT.Rows[i]["MnuTextEn"].ToString(), DT.Rows[i]["MnuTextEn"].ToString());
            }
        }

        return ht;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool getPermission(object PermHT, string Perm)
    {
        Hashtable ht = (Hashtable)PermHT;
        return ht.ContainsKey(Perm);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Hashtable GetAllRequestPerm()
    {
        Hashtable ht = new Hashtable();

        if (LicDf.FetchLic("ER") != "1") {  return ht; }
        else
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM RequestType ORDER BY RetOrder "));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string status = CryptorEngine.Decrypt(DT.Rows[i]["EmpReqStatus"].ToString(), true).Substring(3);
                    if (status == "True") { ht.Add(DT.Rows[i]["RetID"].ToString(), DT.Rows[i]["RetID"].ToString()); }
                }
            }
            return ht;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetRequestPerm(object PermHT, string RetID)
    {
        Hashtable ht = (Hashtable)PermHT;
        return ht.ContainsKey(RetID);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}