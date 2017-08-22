using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class MailFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General GenCs = new General();
    DBFun   DBCs  = new DBFun();
    DTFun   DTCs  = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmailServer;
    public string EmailServer { get { return _EmailServer; } set { _EmailServer = value; } }

    string _SenderEmailID;
    public string SenderEmailID { get { return _SenderEmailID; } set { _SenderEmailID = value; } }

    string _SenderEmailPass;
    public string SenderEmailPass { get { return _SenderEmailPass; } set { _SenderEmailPass = value; } }

    int _EmailPort;
    public int EmailPort { get { return _EmailPort; } set { _EmailPort = value; } }

    bool _EmailSsl;
    public bool EmailSsl { get { return _EmailSsl; } set { _EmailSsl = value; } }
        
    bool _EmailCredential;
    public bool EmailCredential { get { return _EmailCredential; } set { _EmailCredential = value; } }

    string _CalendarType;
    public string CalendarType { get { return _CalendarType; } set { _CalendarType = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FillEmailSetting()
    {
        //IP=192.168.0.4    port=25 local almaalim
        //IP=198.57.142.7   port=26 Extrnal almaalim
        try
            {
                EmailServer     = string.Empty;
                SenderEmailID   = string.Empty;
                SenderEmailPass = string.Empty;
                EmailPort       = 25;
                EmailSsl        = false;
                EmailCredential = false;
                CalendarType    = "Gregorian";    

                DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM EmailConfig"));
                if (!DBCs.IsNullOrEmpty(DT))
                {
                    if (DT.Rows[0]["EmlServerID"]       != DBNull.Value) { EmailServer     = DT.Rows[0]["EmlServerID"].ToString(); }
                    if (DT.Rows[0]["EmlPortNo"]         != DBNull.Value) { EmailPort       = Convert.ToInt32(DT.Rows[0]["EmlPortNo"]); }
                    if (DT.Rows[0]["EmlSenderEmail"]    != DBNull.Value) { SenderEmailID   = DT.Rows[0]["EmlSenderEmail"].ToString(); }
                    if (DT.Rows[0]["EmlSenderPassword"] != DBNull.Value) { SenderEmailPass = DT.Rows[0]["EmlSenderPassword"].ToString(); }
                    if (DT.Rows[0]["EmlSsl"]            != DBNull.Value) { EmailSsl        = Convert.ToBoolean(DT.Rows[0]["EmlSsl"]); }
                    if (DT.Rows[0]["EmlCredential"]     != DBNull.Value) { EmailCredential = Convert.ToBoolean(DT.Rows[0]["EmlCredential"]); } 
                }
                else { return false; }
                
                CalendarType = GenCs.GetCalendarType();
                    
                if (string.IsNullOrEmpty(EmailServer))     { return false; }
                if (string.IsNullOrEmpty(SenderEmailID))   { return false; }
                if (string.IsNullOrEmpty(SenderEmailPass)) { return false; }

                return true; 
            }
            catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindEmail(string ID, string Type)
    {
        try
        {
            if (!string.IsNullOrEmpty(ID))
            {
                string Q = "";
                if      (Type == "MGR") { Q = " SELECT UsrEMailID FROM AppUser  WHERE UsrName = @P1 "; }
                else if (Type == "EMP") { Q = " SELECT EmpEmailID FROM Employee WHERE EmpID   = @P1 "; }

                DataTable DT = DBCs.FetchData(Q, new string[] { ID });
                if (!DBCs.IsNullOrEmpty(DT)) { if (DT.Rows[0][0] != DBNull.Value) { return DT.Rows[0][0].ToString(); } }
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
    public string CreateBodyMail(string BodyTemp, string[] PKList, DataRow DR, string CalendarType) 
    {
        string Body = BodyTemp;

        try
        {
            if (!string.IsNullOrEmpty(Body))
            {
                if (Body.IndexOf("[VAL_AMSURL]")     > 0) { Body = Body.Replace("[VAL_AMSURL]", ""); }
                if (Body.IndexOf("[MONTHAR_NAMEAR]") > 0) { Body = Body.Replace("[MONTHAR_NAMEAR]", DTCs.GetMonthName(CalendarType, "AR")); }
                if (Body.IndexOf("[MONTHAR_NAMEEN]") > 0) { Body = Body.Replace("[MONTHAR_NAMEEN]", DTCs.GetMonthName(CalendarType, "EN")); }

                for (int i = 0; i < PKList.Length; i++)
                {
                    if (PKList[i].Contains("VAL_")) 
                    { 
                        if (Body.IndexOf("[" + PKList[i] + "]") > 0) { Body = Body.Replace("[" + PKList[i] + "]", DR[PKList[i]].ToString()); } 
                    }
                    else if (PKList[i].Contains("DATE_")) 
                    {
                        if (Body.IndexOf("[" + PKList[i] + "]") > 0) { Body = Body.Replace("[" + PKList[i] + "]", DTCs.ShowDBDate(DR[PKList[i]], CalendarType, "dd/MM/yyyy")); }
                    }
                    else if (PKList[i].Contains("TIME_")) 
                    { 
                        if (Body.IndexOf("[" + PKList[i] + "]") > 0) { Body = Body.Replace("[" + PKList[i] + "]", DTCs.ShowDBTime(DR[PKList[i]])); }
                    }
                    else if (PKList[i].Contains("ENCRYPT_")) 
                    { 
                        //if (Body.IndexOf("[" + PKList[i] + "]") > 0) { Body = Body.Replace("[" + PKList[i] + "]", DR[PKList[i]].ToString()); }
                        if (Body.IndexOf("[" + PKList[i] + "]") > 0) { Body = Body.Replace("[" + PKList[i] + "]", CryptorEngine.Decrypt(DR[PKList[i]].ToString(),true)); }
                    }
                    else if (PKList[i].Contains("STATUSAR_")) 
                    { 
                        if (Body.IndexOf("[STATUSAR_REQ]") > 0) { Body = Body.Replace("[STATUSAR_REQ]", ShowStatus("AR", DR[PKList[i]].ToString())); }
                        if (Body.IndexOf("[COLOR_TYPE]")   > 0) { Body = Body.Replace("[STATUSAR_REQ]", ShowColor(DR[PKList[i]].ToString())); }
                    }
                    else if (PKList[i].Contains("STATUSEN_")) 
                    { 
                        if (Body.IndexOf("[STATUSEN_REQ]") > 0) { Body = Body.Replace("[STATUSEN_REQ]", ShowStatus("EN", DR[PKList[i]].ToString())); }
                        if (Body.IndexOf("[COLOR_TYPE]")   > 0) { Body = Body.Replace("[STATUSAR_REQ]", ShowColor(DR[PKList[i]].ToString())); }
                    }
                }
            }
            else { Body = ""; }
        }
        catch (Exception ex) { Body = ""; }

        return Body;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string CreateCondition(string StrCondition, string CalendarType) 
    {
        string Condition = StrCondition;

        try
        {
            if (!string.IsNullOrEmpty(Condition))
            {
                if (Condition.IndexOf("[CALENDAR_TYPE]") > 0) { Condition = Condition.Replace("[CALENDAR_TYPE]", (CalendarType == "Hijri" ? "'H'" : "'G'")); } 
                
                if (Condition.IndexOf("[SDATE_STARTDATE]") > 0 || Condition.IndexOf("[EDATE_ENDDATE]") > 0) 
                { 
                    DateTime SDATE = new DateTime(); 
                    DateTime EDATE = new DateTime(); 
                    DTCs.FindMonthDates(CalendarType, DTCs.FindCurrentYear(CalendarType), DTCs.FindCurrentMonth(CalendarType),out SDATE, out EDATE);

                    if (Condition.IndexOf("[SDATE_STARTDATE]") > 0) { Condition = Condition.Replace("[SDATE_STARTDATE]", "'" + SDATE.ToString("MM/dd/yyyy") + "'"); }
                    if (Condition.IndexOf("[EDATE_ENDDATE]")   > 0) { Condition = Condition.Replace("[EDATE_ENDDATE]", "'" + EDATE.ToString("MM/dd/yyyy") + "'"); }
                }
            }
        }
        catch (Exception ex) { Condition = ""; }

        return Condition;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowStatus(string Lang, string Status)
    {
        try
        {
            if      (Status == "1") { return General.Msg(Lang, "Accepted", "مقبول"); }
            else if (Status == "2") { return General.Msg(Lang, "Rejected" ,"مرفوض"); }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowColor(string Status)
    {
        try
        {
            if      (Status == "1") { return "Green"; }
            else if (Status == "2") { return "Red";   }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}