using System;
using System.Web.Services;
using System.Text;
using System.Data;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class Mail_WS : System.Web.Services.WebService 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();

    MailFun     MailCs     = new MailFun();
    MailSql     MailSqlCs  = new MailSql();
    MailSendFun MailSendCs = new MailSendFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool CheckUser(string UWS,string PWS)
    {
        try
        {
            DataTable DT = DBCs.FetchData("SELECT * FROM WSUsers WHERE WSStatus = 'True' AND WSName = @P1", new string[] { "WSMail" });
            if (DBCs.IsNullOrEmpty(DT)) { return false; }
            else
            {
                string User = CryptorEngine.Decrypt(DT.Rows[0]["WSUsr"].ToString(), true);
                string Pass = CryptorEngine.Decrypt(DT.Rows[0]["WSPassword"].ToString(), true);

                if (Pass == PWS && User == UWS) { return true; } else { return false; }
            }
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public string Mail_Request_Start(string UWS, string PWS) 
    {
        if (!DBCs.isConnect())          { return "Error : Can not connect to the database"; }
        if (!CheckUser(UWS, PWS))       { return "Error : You do not have permission to run this property"; }
        if (!MailCs.FillEmailSetting()) { return "Error : When read setting Email"; }

        //return "OK"; 

        try
        {
            bool isSend = false;
            string Ver = LicDf.FindActiveVersion();
            string Version = (Ver == "General" ? "0" : Ver);

            StringBuilder MQ = new StringBuilder();
            MQ.Append(" SELECT * FROM Mail_Request_View WHERE MailType IN ('Request','Respond') AND MailReqIsSend = 0 ");
            MQ.Append(" AND (CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "',VerID) > 0) ");
            MQ.Append(" AND MailID NOT IN ( SELECT MailID FROM EmailType WHERE VerID = 'General' AND MailCode IN (SELECT MailCode FROM EmailType WHERE VerID = '" + Version + "')) ");

            DataTable DT = DBCs.FetchData(new SqlCommand(MQ.ToString()));
            foreach (DataRow DR in DT.Rows)
            {
                string MailReqID = (DR["MailReqID"] != DBNull.Value) ? DR["MailReqID"].ToString() : "";
                string MailCode = (DR["MailCode"] != DBNull.Value) ? DR["MailCode"].ToString() : "";
                string MailJoinID = (DR["MailJoinID"] != DBNull.Value) ? DR["MailJoinID"].ToString() : "";
                string MailSendingToList = (DR["MailSendingToList"] != DBNull.Value) ? DR["MailSendingToList"].ToString() : "";
                string MailNameAr = (DR["MailNameAr"] != DBNull.Value) ? DR["MailNameAr"].ToString() : "";
                string MailNameEn = (DR["MailNameEn"] != DBNull.Value) ? DR["MailNameEn"].ToString() : "";
                string MailTemp = (DR["MailTemp"] != DBNull.Value) ? DR["MailTemp"].ToString() : "";
                string MailViewName = (DR["MailViewName"] != DBNull.Value) ? DR["MailViewName"].ToString() : "";
                string MailPKList = (DR["MailPKList"] != DBNull.Value) ? DR["MailPKList"].ToString() : "";
                //string MailPKList        = (DR["MailPKList"]        != DBNull.Value) ? CryptorEngine.Decrypt(DDT.Rows[0]["MailPKList"].ToString(),true)) : "";
                string MailSendType = (DR["MailSendType"] != DBNull.Value) ? DR["MailSendType"].ToString() : "";
                string Err = null;

                string Subject = MailNameAr + "   " + MailNameEn;
                string[] PKList = MailPKList.Split(',');

                DataTable DDT = DBCs.FetchData(" SELECT * FROM " + MailViewName + " WHERE VAL_PKID = @P1 ", new string[] { MailJoinID });
                if (!DBCs.IsNullOrEmpty(DDT))
                {
                    string body = MailCs.CreateBodyMail(MailTemp, PKList, DDT.Rows[0], MailCs.CalendarType);
                    ///////////////////////////////////////////////
                    string[] Emails = MailSendingToList.Split(',');
                    for (int i = 0; i < Emails.Length; i++)
                    {
                        Err = null;
                        string email = MailCs.FindEmail(Emails[i], MailSendType);
                        if (!string.IsNullOrEmpty(email))
                        {
                            isSend = MailSendCs.Send(MailCs, email, Subject, body, false, null, out Err);
                            int LogID = MailSqlCs.MailLog_Request_Insert(MailCode, MailReqID.ToString(), isSend, Emails[i], null, Err);
                        }
                    }

                    if (isSend) { MailSqlCs.Mail_Request_UpdateStatus(MailReqID); }
                }
            }

            return "";
        }
        catch (Exception ex) { return ex.Message; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public string Mail_Notification_Start(string UWS, string PWS)
    {
        try
        {
            if (!DBCs.isConnect())          { return "Error : Can not connect to the database"; }
            if (!CheckUser(UWS, PWS))       { return "Error : You do not have permission to run this property"; }
            if (!MailCs.FillEmailSetting()) { return "Error : When read setting Email"; }

            DataTable DT = DBCs.FetchProcedureData("Mail_Notification_Select");      
            if (!DBCs.IsNullOrEmpty(DT))
            {
                for (int i = 0; i < DT.Rows.Count; i++) { Mail_Notification_Process(MailCs, DT.Rows[i]["MailID"].ToString(), DT.Rows[i]["ShiftID"].ToString()); }
            }

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Mail_Notification_Process(MailFun pMailCs, string MailID, string ShiftID)
    {
        try
        {
            bool isSend = false;

            DataTable DT = DBCs.FetchData(" SELECT * FROM EmailType WHERE MailType = 'Notification' AND MailID = @P1", new string[] { MailID });      
            foreach (DataRow DR in DT.Rows)
            {
                string MailCode          = (DR["MailCode"]          != DBNull.Value) ? DR["MailCode"].ToString()          : "";
                string MailNameAr        = (DR["MailNameAr"]        != DBNull.Value) ? DR["MailNameAr"].ToString()        : "";
                string MailNameEn        = (DR["MailNameEn"]        != DBNull.Value) ? DR["MailNameEn"].ToString()        : "";
                string MailTemp          = (DR["MailTemp"]          != DBNull.Value) ? DR["MailTemp"].ToString()          : "";
                string MailViewName      = (DR["MailViewName"]      != DBNull.Value) ? DR["MailViewName"].ToString()      : "";
                string MailViewCondition = (DR["MailViewCondition"] != DBNull.Value) ? DR["MailViewCondition"].ToString() : "";
                //string MailViewCondition = (DR["MailViewCondition"] != DBNull.Value) ? CryptorEngine.Decrypt(DDT.Rows[0]["MailViewCondition"].ToString(),true)) : "";       
                string MailPKList        = (DR["MailPKList"]        != DBNull.Value) ? DR["MailPKList"].ToString()        : "";
                //string MailPKList        = (DR["MailPKList"]        != DBNull.Value) ? CryptorEngine.Decrypt(DDT.Rows[0]["MailPKList"].ToString(),true)) : "";
                string MailSendType      = (DR["MailSendType"]      != DBNull.Value) ? DR["MailSendType"].ToString()      : "";
                string Err          = null;

                int MailReqID = MailSqlCs.Mail_Notification_Insert(MailCode,ShiftID);

                string Subject  = MailNameAr + "   " + MailNameEn;
                string[] PKList = MailPKList.Split(',');
                string Condition = MailCs.CreateCondition(MailViewCondition, MailCs.CalendarType);

                StringBuilder QB = new StringBuilder();
                QB.Append(" SELECT * FROM " + MailViewName + (!string.IsNullOrEmpty(MailViewCondition) ? " WHERE " + Condition + " " : ""));
                QB.Append(" AND " + (MailSendType == "MGR" ? "VAL_USRID" :"VAL_EMPID") + " ");
                QB.Append(" NOT IN (SELECT MailSendingTo FROM EmailNotificationLog WHERE ShiftID = [VAL_ShiftID] AND CAST (CONVERT(CHAR(10), MailCreatedDT, 101) AS DATETIME) = CAST (CONVERT(CHAR(10), GETDATE(), 101) AS DATETIME) AND MailIsSend = 'True' )");

                string VQ = QB.ToString(); 
                if (VQ.IndexOf("[VAL_ShiftID]") > 0) { VQ = VQ.Replace("[VAL_ShiftID]", ShiftID); }
                    
                DataTable DDT = DBCs.FetchData(VQ);
                foreach (DataRow DDR in DDT.Rows)
                {
                    string body = MailCs.CreateBodyMail(MailTemp, PKList, DDT.Rows[0], MailCs.CalendarType);
                    ///////////////////////////////////////////////
                    string SendTo = (MailSendType == "MGR" ? DDR["USRID"].ToString() : DDR["EMPID"].ToString()) ;

                    string email = pMailCs.FindEmail(SendTo, MailSendType); //"a.rabia@almaalim.com";
                    if (!string.IsNullOrEmpty(email)) 
                    { 
                        Err = null;
                        isSend = MailSendCs.Send(pMailCs, email, Subject, body, false, null, out Err); 
                        int LogID = MailSqlCs.MailLog_Notification_Insert(MailCode,MailReqID.ToString(),isSend,SendTo,ShiftID,Err);
                    }
                }
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public string Mail_Schedule_Start(string UWS, string PWS)
    {
        try
        {
            if (!DBCs.isConnect())          { return "Error : Can not connect to the database"; }
            if (!CheckUser(UWS, PWS))       { return "Error : You do not have permission to run this property"; }
            if (!MailCs.FillEmailSetting()) { return "Error : When read setting Email"; }

            DataTable DT = DBCs.FetchProcedureData("Mail_Schedule_Select");      
            if (!DBCs.IsNullOrEmpty(DT))
            {
                for (int i = 0; i < DT.Rows.Count; i++) { Mail_Schedule_Process(MailCs, DT.Rows[i]["SchID"].ToString()); }
            }

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Mail_Schedule_Process(MailFun pMailCs, string SchID)
    {
        try
        {
            bool isSend = false;

            DataTable DT = DBCs.FetchData(" SELECT * FROM Schedule WHERE SchID = @P1 ", new string[] { SchID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                string ExportType = "";
                string Subject = "";
                string body = "";
                string RepID = "";
                string UserNames = "";
                string Err = null;

                if (DT.Rows[0]["SchReportFormat"] != DBNull.Value) { ExportType = DT.Rows[0]["SchReportFormat"].ToString(); }
                if (DT.Rows[0]["SchEmailSubject"] != DBNull.Value) { Subject = DT.Rows[0]["SchEmailSubject"].ToString(); }
                if (DT.Rows[0]["SchEmailBodyContent"] != DBNull.Value) { body = DT.Rows[0]["SchEmailBodyContent"].ToString(); }
                if (DT.Rows[0]["ReportID"] != DBNull.Value) { RepID = DT.Rows[0]["ReportID"].ToString(); }
                if (DT.Rows[0]["SchUsers"] != DBNull.Value) { UserNames = DT.Rows[0]["SchUsers"].ToString(); }

                //////////////////////////////////////
                string[] Users = UserNames.Split(',');
                for (int i = 0; i < Users.Length; i++)
                {
                    Err = null;
                    string email = MailCs.FindEmail(Users[i], "MGR");
                    string lang  = MailCs.FindLang(Users[i] , "MGR");

                    if (!string.IsNullOrEmpty(email))
                    {
                        System.Net.Mail.Attachment attachment = MailSendCs.Create(RepID, Users[i], lang, ExportType, pMailCs.CalendarType);
                        isSend = MailSendCs.Send(pMailCs, email, Subject, body, true, attachment, out Err);
                    }

                    if (isSend)
                    {
                        //General.WriteTimeMessage(General.Schedule_FileLogPath,"Schedules ID :" + SchID + ",To email : " + email + ", executed successfully"); 
                    }
                }
                //////////////////////////////////////
                if (isSend)
                {
                    MailSqlCs.Schedule_UpdateLastTime(SchID);
                }
                //////////////////////////////////////
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet Mail_GetConfig(string UWS, string PWS)
    {
        try
        {
            if (!DBCs.isConnect()) { return null; }
            if (!CheckUser(UWS, PWS)) { return null; }

            DataSet DS = new DataSet();

            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM EmailConfig"));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                foreach (DataRow DR in DT.Rows)
                {
                    string Pass = CryptorEngine.Decrypt(DR["EmlSenderPassword"].ToString(), true);
                    DR["EmlSenderPassword"] = Pass;
                }

                DS.Tables.Add(DT);
                DS.Tables[0].TableName = "Data";
            }

            return DS;
        }
        catch (Exception ex)
        {
            //return ex.Message;
            return null;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
