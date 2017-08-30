using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Elmah;

public class SMSFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();
    string Prefix = "966";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _SMSGateway;
    public string SMSGateway { get { return _SMSGateway; } set { _SMSGateway = value; } }

    string _SMSSenderID;
    public string SMSSenderID { get { return _SMSSenderID; } set { _SMSSenderID = value; } }

    string _SMSSenderNo;
    public string SMSSenderNo { get { return _SMSSenderNo; } set { _SMSSenderNo = value; } }

    string _SMSUser;
    public string SMSUser { get { return _SMSUser; } set { _SMSUser = value; } }

    string _SMSPass;
    public string SMSPass { get { return _SMSPass; } set { _SMSPass = value; } }

    string _CalendarType;
    public string CalendarType { get { return _CalendarType; } set { _CalendarType = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FillSMSSetting()
    {
        try
        {
            SMSGateway  = string.Empty;
            SMSSenderID = string.Empty;
            SMSSenderNo = string.Empty;
            SMSUser     = string.Empty;
            SMSPass     = string.Empty;
            CalendarType = "Gregorian";

            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM SMSConfig"));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["SmsGateway"]  != DBNull.Value) { SMSGateway  = DT.Rows[0]["SmsGateway"].ToString(); }
                if (DT.Rows[0]["SmsSenderID"] != DBNull.Value) { SMSSenderID = DT.Rows[0]["SmsSenderID"].ToString(); }
                if (DT.Rows[0]["SmsSenderNo"] != DBNull.Value) { SMSSenderNo = DT.Rows[0]["SmsSenderNo"].ToString(); }
                if (DT.Rows[0]["SmsUser"]     != DBNull.Value) { SMSUser     = DT.Rows[0]["SmsUser"].ToString(); }
                if (DT.Rows[0]["SmsPass"]     != DBNull.Value) { SMSPass     = CryptorEngine.Decrypt(DT.Rows[0]["SmsPass"].ToString(), true); }
            }
            else { return false; }

            CalendarType = GenCs.GetCalendarType();

            if (string.IsNullOrEmpty(SMSGateway))  { return false; }
            if (string.IsNullOrEmpty(SMSSenderID)) { return false; }
            if (string.IsNullOrEmpty(SMSUser))     { return false; }
            if (string.IsNullOrEmpty(SMSPass))     { return false; }

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindMobileNo(string ID, string Type)
    {
        try
        {
            if (!string.IsNullOrEmpty(ID))
            {
                string Q = "";
                if      (Type == "MGR") { Q = " SELECT UsrMobileNo FROM AppUser  WHERE UsrName = @P1 "; }
                else if (Type == "EMP") { Q = " SELECT EmpMobileNo FROM Employee WHERE EmpID   = @P1 "; }

                DataTable DT = DBCs.FetchData(Q, new string[] { ID });
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    if (DT.Rows[0][0] != DBNull.Value) { return FormatMobileNo(DT.Rows[0][0].ToString(), Prefix); }
                }
            }
        }
        catch (Exception e1) { }

        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindLang(string ID, string Type)
    {
        try
        {
            if (!string.IsNullOrEmpty(ID))
            {
                string Q = "";
                if      (Type == "MGR") { Q = " SELECT UsrLanguage FROM AppUser  WHERE UsrName = @P1 "; }
                else if (Type == "EMP") { Q = " SELECT EmpLanguage FROM Employee WHERE EmpID   = @P1 "; }

                DataTable DT = DBCs.FetchData(Q, new string[] { ID });
                if (!DBCs.IsNullOrEmpty(DT)) { if (DT.Rows[0][0] != DBNull.Value) { return DT.Rows[0][0].ToString(); } }
            }
        }
        catch (Exception e1) { }

        return "EN";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindEmpMobileNo(string IDs, out string EnNOs, out string ArNOs)
    {
        EnNOs = ArNOs = "";

        try
        {
            if (!string.IsNullOrEmpty(IDs))
            {
                StringBuilder QI = new StringBuilder();
                QI.Append(" SELECT 'EN' Lang, EmpMobileNo FROM Employee WHERE EmpLanguage = 'EN' AND EmpID IN (SELECT ID1 FROM UF_CSVToTable1Col('" + IDs + "')) ");
                QI.Append(" UNION ALL ");
                QI.Append(" SELECT 'AR' Lang, EmpMobileNo FROM Employee WHERE EmpLanguage = 'AR' AND EmpID IN (SELECT ID1 FROM UF_CSVToTable1Col('" + IDs + "')) ");

                DataTable DT = DBCs.FetchData(new SqlCommand(QI.ToString()));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    string sEnNo = "";
                    DataRow[] EnDRs = DT.Select("Lang = 'EN'");
                    foreach(DataRow EnDR in EnDRs)
                    {
                        string FormatEnNo = FormatMobileNo(EnDR["EmpMobileNo"].ToString(), Prefix);
                        if (string.IsNullOrEmpty(sEnNo)) { sEnNo = FormatEnNo; } else { sEnNo += "," + FormatEnNo; }
                    }

                    string sArNo = "";
                    DataRow[] ArDRs = DT.Select("Lang = 'AR'");
                    foreach (DataRow ArDR in ArDRs)
                    {
                        string FormatArNo = FormatMobileNo(ArDR["EmpMobileNo"].ToString(), Prefix);
                        if (string.IsNullOrEmpty(sArNo)) { sArNo = FormatArNo; } else { sArNo += "," + FormatArNo; }
                    }

                    EnNOs = sEnNo;
                    ArNOs = sArNo;
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FormatMobileNo(string NO, string Prefix)
    {
        string MobileNo = "";

        try
        {
            if (!string.IsNullOrEmpty(NO) && !string.IsNullOrEmpty(Prefix))
            {
                MobileNo = Prefix + String.Format("{0:#########}", double.Parse(NO));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }

        return MobileNo;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}