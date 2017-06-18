using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography;
using Elmah;
using System.Net;
using System.Data.SqlClient;
using System.Text;

public class General
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public General() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ConnString
    {
        get
        {
            string connectionString        = ConfigurationManager.AppSettings["Main.ConnectionString"].ToString();
            string connectionStringDecrypt = connectionString;

            try
            {
                string IsEncryption = ConfigurationManager.AppSettings["Encryption.ConnectionString"].ToString();
                if (!string.IsNullOrEmpty(IsEncryption))
                {
                    if (IsEncryption == "1") { connectionStringDecrypt = CryptorEngine.Decrypt(connectionString, true); }
                }
            }
            catch { }
            
            return connectionStringDecrypt;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string UrlWSFingerprintTools { get { return ConfigurationManager.AppSettings["Url.WSFingerprintTools"].ToString(); } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindStatus(char Status)
    {
        try
        {
            return Convert.ToBoolean(Convert.ToInt32(Status.ToString()));
        }
        catch { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string CreateIDsString(string Col, DataTable DT)
    {
        string IDs = "";

        if (!IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = "'" + DT.Rows[i][Col].ToString() + "'"; } else { IDs += "," + "'" + DT.Rows[i][Col].ToString() + "'"; } }
        }
        
        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string CreateIDsSerail(int Max)
    {
        string IDs = "";

        if (Max > 0) 
        {
            for (int i = 0; i < Max; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = Max.ToString(); } else { IDs += "," + Max.ToString(); } }
        }
        
        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public string CreateIDsNumber(string Col, DataTable DT)
    {
        string IDs = "";

        if (!IsNullOrEmpty(DT))
        {
            for (int i = 0; i < DT.Rows.Count; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = DT.Rows[i][Col].ToString(); } else { IDs += "," + DT.Rows[i][Col].ToString(); } }
        }
        
        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    public string CreateIDsNumber(string Col, ListBox _LB)
    {
        string IDs = "";

        if (_LB.Items.Count > 0) 
        {
            if (Col == "V") { for (int i = 0; i < _LB.Items.Count; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = _LB.Items[i].Value; } else { IDs += "," + _LB.Items[i].Value; } } }
            if (Col == "T") { for (int i = 0; i < _LB.Items.Count; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = _LB.Items[i].Text; } else { IDs += "," + _LB.Items[i].Text; } } }
        }
        
        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string CreateIDsNumber(DateTime[] ADT)
    {
        string IDs = "";

        if (ADT.Length > 0) 
        {
            for (int i = 0; i < ADT.Length; i++) { if (string.IsNullOrEmpty(IDs)) { IDs = ADT[i].ToString(); } else { IDs += "," + ADT[i].ToString(); } }
        }
        
        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     public string CreateIDsNumber(TreeView _TV)
    {
        string IDs = "";

        foreach (TreeNode node in _TV.CheckedNodes)
        {
            if (string.IsNullOrEmpty(IDs)) { IDs = node.Value; } else { IDs += "," + node.Value; }
        }

        return IDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    public bool IsNullOrEmpty(DataTable DT)
    {
        if (DT == null)         { return true; }
        if (DT.Rows.Count == 0) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsNullOrEmpty(object obj)
    {
        if (obj == null) { return true; }
        if (string.IsNullOrEmpty(obj.ToString())) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsNullOrEmptyDB(object obj)
    {
        if (obj == DBNull.Value) { return true; }
        if (string.IsNullOrEmpty(obj.ToString())) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GetIPAddress()
    {
        string strHostName = System.Net.Dns.GetHostName();
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];

        return ipAddress.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string AddString(string strFull, string strAdded, string split)
    {
       if (string.IsNullOrEmpty(strFull)) { return strFull = strAdded; } else { return strFull += split + strAdded; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string Msg(string msgEn, string msgAr)
    {
         if (HttpContext.Current.Session["Language"].ToString() == "AR") { return msgAr; } else { return msgEn; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string Msg(string Lang, string msgEn, string msgAr)
    {
         if (Lang == "AR") {  return msgAr; } else { return msgEn; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ProcedureMsg()
    {
        return Msg("Error Inside Procedure ", "Error Inside Procedure ");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ApprovalSequenceMsg()
    {
        return Msg("No sequence approvals for this type of Request, please refer to the management", "لا يوجد سلسة موافقات لهذا النوع من الطلبات ، الرجاء مراجعة الإدارة");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool isEmpID(string ID)
    {
        DBFun DBCs = new DBFun();

        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM spActiveEmployeeView WHERE EmpID = @P1 AND ISNULL(EmpDeleted,0) = 0 ", new string[] { ID });
            if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindEmpApprovalSequence(string ReqType, string EmpID)
    {
        DBFun DBCs = new DBFun();

        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM EmpApprovalLevel WHERE RetID = @P1 AND EmpID = @P2 ", new string[] { ReqType, EmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }

        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckFileRequired(string ReqType) 
    {
        DBFun DBCs = new DBFun();

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgFormReq FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if      (DT.Rows[0]["cfgFormReq"] == DBNull.Value)                  { return false; }
            else if (string.IsNullOrEmpty(DT.Rows[0]["cfgFormReq"].ToString())) { return false; }
            else if (!DT.Rows[0]["cfgFormReq"].ToString().Contains(ReqType))    { return false; }
            else { return true; }
        }
        else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetCalendarType()
    {
        DBFun DBCs = new DBFun();
        string CalendarType = "Gregorian";

        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ApplicationSetup "));
            if (!DBCs.IsNullOrEmpty(DT)) 
            { 
                if (DT.Rows[0]["AppCalendar"].ToString() == "H") { CalendarType = "Hijri"; } else { CalendarType = "Gregorian"; }
            }
        }
        catch { }

        return CalendarType;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetCalendarFormat()
    {
        DBFun DBCs = new DBFun();
        string CalendarFormat = "dd/MM/yyyy";

        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ApplicationSetup "));
            if (!DBCs.IsNullOrEmpty(DT)) 
            { 
                if (!string.IsNullOrEmpty(DT.Rows[0]["AppDateFormat"].ToString())) { CalendarFormat = DT.Rows[0]["AppDateFormat"].ToString(); }
            }
        }
        catch { }

        return CalendarFormat;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillPageTree(string Currentlang, string Version, string PermSet)
    {
        DBFun DBCs = new DBFun();

        string lang = (Currentlang == "AR") ? "Ar" : "En";
        string LicPage  = LicDf.FindLicPage();
        string listPage = "'Menu','SubMenu','Command','AD_SubMenu','AD_MainMenu'" + (string.IsNullOrEmpty(LicPage) ? "" : "," + LicPage);
            
        StringBuilder MQ = new StringBuilder();
        MQ.Append(" SELECT MnuNumber,MnuID,MnuPermissionID,MnuImageURL,MnuText" + lang + " as MnuText,MnuDescription as MnuDescription,(MnuServer + '' + MnuURL) as MnuURL,MnuParentID,MnuVisible,MnuOrder ");
        MQ.Append(" FROM Menu WHERE  MnuVisible = 'True' AND MnuType IN (" + listPage + ") ");
        MQ.Append(" AND MnuPermissionID IN (SELECT MnuNumber FROM Menu WHERE  MnuVisible = 'True' AND MnuType IN (" + listPage + "))");
        if (!string.IsNullOrEmpty(PermSet)) { MQ.Append(" AND MnuNumber IN (" + PermSet + ")"); }
        MQ.Append(" AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");
        MQ.Append(" ORDER BY MnuOrder ");
            
        return DBCs.FetchMenuData(MQ.ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillRepTree(string Version)
    {
        DBFun DBCs = new DBFun();

        DataSet RepDS = new DataSet();
        StringBuilder RQ = new StringBuilder();
        RQ.Append(" SELECT CONVERT(CHAR(10),RgpID) AS RepID,RgpArName AS RepNameAr,RgpEnName AS RepNameEn,CONVERT(CHAR(10),RgpParID) AS RgpID FROM ReportGroup");
        RQ.Append(" WHERE ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");
        RQ.Append(" UNION ");
        RQ.Append(" SELECT RepID,RepNameAr,RepNameEn,CONVERT(CHAR(10),RgpID) AS RgpID FROM Report WHERE ISNULL(RepDeleted,0) = 0  ");
        RQ.Append(" AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");

        return DBCs.FetchReportData(RQ.ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillRepTree(string Version, string PermSet)
    {
        DBFun DBCs = new DBFun();

        DataSet RepDS = new DataSet();
        StringBuilder RQ = new StringBuilder();
        RQ.Append(" SELECT CONVERT(CHAR(10),RgpID) AS RepID,RgpArName AS RepNameAr,RgpEnName AS RepNameEn,CONVERT(CHAR(10),RgpParID) AS RgpID FROM ReportGroup");
        RQ.Append(" WHERE ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");
        RQ.Append(" AND RgpID IN (" + PermSet + ") ");
        RQ.Append(" UNION ");
        RQ.Append(" SELECT RepID,RepNameAr,RepNameEn,CONVERT(CHAR(10),RgpID) AS RgpID FROM Report WHERE ISNULL(RepDeleted,0) = 0  ");
        RQ.Append(" AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");
        RQ.Append(" AND RgpID IN (" + PermSet + ") ");

        return DBCs.FetchReportData(RQ.ToString());
    }

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}