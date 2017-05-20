using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

public class SendEmail1
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    
    string RetID = "";
    string EmpID = "";
    string ErqStartDate = "";
    string ErqEndDate = "";
    string ErqStartDate2 = "";
    string ErqEndDate2 = "";
    string ErqReqStatus = "";

    string EMailServer     = "";
    int    EMailPort;
    string SenderEMailID   = "";
    string SenderEMailPass = "";
    bool   EmailSsl        = false;

    string EmpLang  = "EN";
    string EmpName  = "";

    string EmpNameAr  = "";
    string EmpNameEn  = "";
    string EmpEmail = "";

    string UsrLang  = "EN";
    string UsrEmail = "";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public SendEmail1() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetEMailInfo()
    {
        //IP=192.168.0.4    port=25 local almaalim
        //IP=198.57.142.7   port=26 Extrnal almaalim
        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ApplicationSetup "));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["AppEMailServer"]         != DBNull.Value)   { EMailServer     = DT.Rows[0]["AppEMailServer"].ToString();         } else { return false; }
                if (DT.Rows[0]["AppEMailPort"]           != DBNull.Value)   { EMailPort       = Convert.ToInt32(DT.Rows[0]["AppEMailPort"]);     } else { return false; }
                if (DT.Rows[0]["AppSenderEMailID"]       != DBNull.Value)   { SenderEMailID   = DT.Rows[0]["AppSenderEMailID"].ToString();       } else { return false; }
                if (DT.Rows[0]["AppSenderEMailPassword"] != DBNull.Value)   { SenderEMailPass = DT.Rows[0]["AppSenderEMailPassword"].ToString(); } else { return false; }

                try { if (DT.Rows[0]["AppEmailSsl"] != DBNull.Value) { EmailSsl = Convert.ToBoolean(DT.Rows[0]["AppEmailSsl"]); } } catch { EmailSsl = false; }

                return true;
            }
            else { return false; }
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetEmpInfo(string pEmpID, string pLang)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT EmpEmailID,EmpLanguage,EmpNameEn,EmpNameAr FROM Employee WHERE EmpID = @P1 ", new string[] { pEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                if (DT.Rows[0]["EmpLanguage"] != DBNull.Value) { EmpLang  = DT.Rows[0]["EmpLanguage"].ToString(); } 
                if (DT.Rows[0]["EmpEmailID"]  != DBNull.Value) { EmpEmail = DT.Rows[0]["EmpEmailID"].ToString(); } else { return false; }
                if (pLang == "AR") { if (DT.Rows[0]["EmpNameAr"]  != DBNull.Value) { EmpName = DT.Rows[0]["EmpNameAr"].ToString(); } } 
                else               { if (DT.Rows[0]["EmpNameEn"]  != DBNull.Value) { EmpName = DT.Rows[0]["EmpNameEn"].ToString(); } } 
                
                if (DT.Rows[0]["EmpNameAr"]  != DBNull.Value) { EmpNameAr = DT.Rows[0]["EmpNameAr"].ToString(); }
                if (DT.Rows[0]["EmpNameEn"]  != DBNull.Value) { EmpNameEn = DT.Rows[0]["EmpNameEn"].ToString(); }
                
                return true;
            }
            else { return false; }
        }
        catch (Exception e1) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetUsrInfo(string pUser)
    {
        try
        {
            string InUsr = "";
            string[] Users = pUser.Split(',');
            foreach (string Usr in Users) { if (string.IsNullOrEmpty(InUsr)) { InUsr = "'" + Usr + "'"; } else { InUsr += ",'" + Usr + "'"; } }

            DataTable DT = DBCs.FetchData(" SELECT UsrEmailID,UsrLanguage FROM AppUser WHERE UsrName IN (@P1) ", new string[] { InUsr });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                if (DT.Rows[0]["UsrLanguage"] != DBNull.Value) { UsrLang  = DT.Rows[0]["UsrLanguage"].ToString(); }

                UsrEmail = "";
                foreach (DataRow EmailDR in DT.Rows) 
                {
                    if (EmailDR["UsrEmailID"] != DBNull.Value) 
                    { 
                        if (string.IsNullOrEmpty(UsrEmail)) { UsrEmail = EmailDR["UsrEmailID"].ToString(); } else { UsrEmail += ";" + EmailDR["UsrEmailID"].ToString(); }  
                    }
                }

                if (string.IsNullOrEmpty(UsrEmail)) { return false; }
                
                
                return true;
            }
            else { return false; }
        }
        catch (Exception e1) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void GetReqInfo(string pErqID, string pDateType)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT RetID,EmpID,ErqStartDate,ErqEndDate,ErqStartDate2,ErqEndDate2,ErqReqStatus FROM EmpRequest WHERE ErqID = @P1 ", new string[] { pErqID });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                RetID = DT.Rows[0]["RetID"].ToString();
                EmpID = DT.Rows[0]["EmpID"].ToString();
                ErqReqStatus = DT.Rows[0]["ErqReqStatus"].ToString();
                
                if (DT.Rows[0]["ErqStartDate"] != DBNull.Value)
                {
                    //if (pDateType == "Gregorian") { ErqStartDate = (Convert.ToDateTime(DT.Rows[0]["ErqStartDate"])).ToString("dd/MM/yyyy"); }
                    //else if (pDateType == "Hijri") { ErqStartDate = Convert.ToString(General.GrnToHij(Convert.ToDateTime(DT.Rows[0]["ErqStartDate"]))); }
                }

                if (DT.Rows[0]["ErqEndDate"] != DBNull.Value)
                {
                    //if (pDateType == "Gregorian") { ErqEndDate = (Convert.ToDateTime(DT.Rows[0]["ErqEndDate"])).ToString("dd/MM/yyyy"); }
                    //else if (pDateType == "Hijri") { ErqEndDate = Convert.ToString(General.GrnToHij(Convert.ToDateTime(DT.Rows[0]["ErqEndDate"]))); }
                }

                if (DT.Rows[0]["ErqStartDate2"] != DBNull.Value)
                {
                    //if (pDateType == "Gregorian") { ErqStartDate2 = (Convert.ToDateTime(DT.Rows[0]["ErqStartDate2"])).ToString("dd/MM/yyyy"); }
                    //else if (pDateType == "Hijri") { ErqStartDate2 = Convert.ToString(General.GrnToHij(Convert.ToDateTime(DT.Rows[0]["ErqStartDate2"]))); }
                }

                if (DT.Rows[0]["ErqEndDate2"] != DBNull.Value)
                {
                    //if (pDateType == "Gregorian") { ErqEndDate2 = (Convert.ToDateTime(DT.Rows[0]["ErqEndDate2"])).ToString("dd/MM/yyyy"); }
                    //else if (pDateType == "Hijri") { ErqEndDate2 = Convert.ToString(General.GrnToHij(Convert.ToDateTime(DT.Rows[0]["ErqEndDate2"]))); }
                }
            } 
        }
        catch (Exception e1) {  }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetReqName(string pRetID, string pLang)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT RetNameEn,RetNameAr FROM RequestType WHERE RetID = @P1 ", new string[] { pRetID });
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                return General.Msg(DT.Rows[0]["RetNameEn"].ToString(), DT.Rows[0]["RetNameAr"].ToString());
            }
            return string.Empty;
        }
        catch (Exception e1) { return string.Empty; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool SendEMail(string EmailSubject, string EmailBody,string ToEmail)
    {
        try
        {
            System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
            msgMail.From    = new MailAddress(SenderEMailID);
            msgMail.Subject = EmailSubject;
            msgMail.Body    = EmailBody;               
                      
            string[] ToEmails = ToEmail.Split(';');
            foreach (string email in ToEmails) { msgMail.To.Add(email); }            
            //msgMail.To.Add(ToEmail);
            
            msgMail.IsBodyHtml = true;
            //msgMail.BodyEncoding = UTF8Encoding.UTF8;    
            msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient SClient = new SmtpClient();
            SClient = new SmtpClient(EMailServer, EMailPort);
            SClient.DeliveryMethod = SmtpDeliveryMethod.Network; 
            SClient.UseDefaultCredentials = true;
            SClient.Credentials = new NetworkCredential(SenderEMailID, SenderEMailPass);
            
            if (EmailSsl)
            {
                SClient.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            }

            SClient.Send(msgMail);

            return true;
        } 
        catch (Exception ex) { return false; }
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetReqStatusName(string Status, string Lang)
    {
        string StatusName = "";
        if (Status == "1") { StatusName = ShowMsg(Lang, "Approved", "مقبول"); } else if (Status == "2") { StatusName = ShowMsg(Lang, "Rejected", "مرفوض"); }
        return StatusName;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetReqStatusColor(string Status)
    {
        string ColorName = "";
        if (Status == "1") { ColorName = "Green"; } else if (Status == "2") { ColorName = "Red"; }
        return ColorName;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendMailToEmp(string pErqID, string pDateType,string pLang)
    {
        try
        {
            GetReqInfo(pErqID, pDateType);
            bool EmailInfo = GetEMailInfo();
            bool EmpInfo   = GetEmpInfo(EmpID,pLang);

            if (EmailInfo & EmpInfo)
            {
                string ReqName = GetReqName(RetID, pLang);
                string ReqStatus = "";
                ReqStatus = GetReqStatusName(ErqReqStatus, EmpLang);
                //if (ErqReqStatus == "1") { ReqStatus = ShowMsg(EmpLang, "Approved", "مقبول"); } else if (ErqReqStatus == "2") { ReqStatus = ShowMsg(EmpLang, "Rejected", "مرفوض"); }
                string date = ErqStartDate;
                if (!string.IsNullOrEmpty(ErqEndDate)) { if (ErqStartDate != ErqEndDate) { date += " - " + ErqEndDate; } }

                string EmailSubject = ShowMsg(EmpLang, "Request ID : " + pErqID, "طلب بالرقم " + pErqID);
                string EmailText    = ShowMsg(EmpLang,
                                              " Dear Employee "
                                            + "<br /> "
                                            + "Request ID : " + pErqID
                                            + " <br /> "
                                            + "Request Name : " + ReqName
                                            + " <br /> "
                                            + "Request Date : " + date
                                            + " <br /> "
                                            + "Request Status : " + ReqStatus,
                                            " عزيزي الموظف "
                                            + "<br /> "
                                            + "رقم الطلب : " + pErqID
                                            + " <br /> "
                                            + "نوع الطلب : " + ReqName
                                            + " <br /> "
                                            + "تاريخ الطلب : " + date
                                            + " <br /> "
                                            + "حالة الطلب : " + ReqStatus);
                string EmailBody = @"
                <html>
                <head>
                    <title>For Employee ID : " + EmpID + @" </title>
                </head>
                <body >           
                    <div>
                        <table>
                            <tr>
                                <td align='" + ShowMsg(EmpLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
                                    <span style=""font-size: 18pt; font-family:Arial"">
                                        <br /> "
                                            + EmailText
                                            + @"<br />                                        
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";

                if (HttpContext.Current.Session["ActiveVersion"]  != null) 
                {
                    if (HttpContext.Current.Session["ActiveVersion"].ToString() == "KACare")
                    {
                        EmailBody = GetHtmlFile("ToEMP_KaCare");
                        EmailBody = EmailBody.Replace("[EMPLOYEEAR_NAME]",EmpNameAr);
                        EmailBody = EmailBody.Replace("[EMPLOYEEEN_NAME]",EmpNameEn);

                        EmailBody = EmailBody.Replace("[REQUESTAR_NO]",pErqID);
                        EmailBody = EmailBody.Replace("[REQUESTEN_NO]",pErqID);

                        EmailBody = EmailBody.Replace("[REQUESTAR_TYPE]",GetReqName(RetID, "AR"));
                        EmailBody = EmailBody.Replace("[REQUESTEN_TYPE]",GetReqName(RetID, "EN"));

                        EmailBody = EmailBody.Replace("[REQUESTAR_DATE]",date);
                        EmailBody = EmailBody.Replace("[REQUESTEN_DATE]",date);

                        EmailBody = EmailBody.Replace("[REQUESTAR_STATUS]", GetReqStatusName(ErqReqStatus, "AR"));
                        EmailBody = EmailBody.Replace("[REQUESTEN_STATUS]", GetReqStatusName(ErqReqStatus, "EN"));

                        EmailBody = EmailBody.Replace("[REQUESTAR_COLOR]", GetReqStatusColor(ErqReqStatus));
                        EmailBody = EmailBody.Replace("[REQUESTEN_COLOR]", GetReqStatusColor(ErqReqStatus));
                    }
                } 


                SendEMail(EmailSubject, EmailBody,EmpEmail);
            }
        } 
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendMailToMGR(string pUser, string pErqID, string pDateType, string pLang,string pUrl)
    {
        try
        {
            GetReqInfo(pErqID, pDateType);
            bool EmailInfo = GetEMailInfo();
            bool UsrInfo   = GetUsrInfo(pUser);
            bool EmpInfo   = GetEmpInfo(EmpID,pLang);
            
            if (EmailInfo & UsrInfo)
            {
                string date = ErqStartDate;
                if (!string.IsNullOrEmpty(ErqEndDate)) { if (ErqStartDate != ErqEndDate) { date += " - " + ErqEndDate; } }

                string EmailSubject = ShowMsg(UsrLang, "Request ID : " + pErqID + " is Waiting", "طلب بالرقم " + pErqID + " بالإنتظار");
                string EmailText    = ShowMsg(UsrLang,
                                              " Dear Manager "
                                            + "<br /> "
                                            + "Request ID : " + pErqID
                                            + " <br /> "
                                            + "Employee ID : " + EmpID
                                            + " <br /> "
                                            + "Employee Name : " + EmpName
                                            + " <br /> "
                                            + "Request Type : " + GetReqName(RetID, "EN")
                                            + " <br /> "
                                            + "Request Date : " + date
                                            + " <br /> "
                                            + "Request Status : Waiting"
                                            + " <br /> "
                                            + " <a href='" + pUrl + "'>AMS WEB</a> "

                                            , " عزيزي المدير "
                                            + "<br /> "
                                             + "رقم الطلب : " + pErqID 
                                            + " <br /> "
                                            + "رقم الموظف : " + EmpID  
                                            + " <br /> "
                                            + "اسم الموظف : " + EmpName 
                                            + " <br /> "
                                            + "نوع الطلب : " + GetReqName(RetID, "AR") 
                                            + " <br /> "
                                            + "تاريخ الطلب : " + date
                                            + " <br /> "
                                            + "حالة الطلب : إنتظار"
                                            + " <br /> "
                                            + " <a href='" + pUrl + "'>AMS WEB</a> "
                                            );

                string EmailBody = "";
                EmailBody = @"
                                    <html>
                                    <head>
                                        <title>For Manager : " + pUser + @" </title>
                                    </head>
                                    <body >           
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align='" + ShowMsg(UsrLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
                                                        <span style=""font-size: 16pt; font-family:Arial"">                                        
                                                            <br /> "
                                                            + EmailText
                                                            + @" <br />                                        
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </body>
                                    </html>";

                if (HttpContext.Current.Session["ActiveVersion"]  != null) 
                {
                    if (HttpContext.Current.Session["ActiveVersion"].ToString() == "KACare")
                    {
                        EmailBody = GetHtmlFile("ToMGR_KaCare");
                        EmailBody = EmailBody.Replace("[DirectARManager]",pUser);
                        EmailBody = EmailBody.Replace("[DirectENManager]",pUser);
                        
                        EmailBody = EmailBody.Replace("[EMPLOYEEAR_NAME]",EmpNameAr);
                        EmailBody = EmailBody.Replace("[EMPLOYEEEN_NAME]",EmpNameEn);

                        EmailBody = EmailBody.Replace("[EMPLOYEEAR_ID]",EmpID);
                        EmailBody = EmailBody.Replace("[EMPLOYEEEN_ID]",EmpID);

                        EmailBody = EmailBody.Replace("[REQUESTAR_NO]",pErqID);
                        EmailBody = EmailBody.Replace("[REQUESTEN_NO]",pErqID);

                        EmailBody = EmailBody.Replace("[REQUESTAR_TYPE]",GetReqName(RetID, "AR"));
                        EmailBody = EmailBody.Replace("[REQUESTEN_TYPE]",GetReqName(RetID, "EN"));

                        EmailBody = EmailBody.Replace("[REQUESTAR_DATE]",date);
                        EmailBody = EmailBody.Replace("[REQUESTEN_DATE]",date);

                        EmailBody = EmailBody.Replace("[REQUESTAR_STATUS]","إنتظار");
                        EmailBody = EmailBody.Replace("[REQUESTEN_STATUS]","Waiting");

                        //EmpNameAr
                        //[DirectARManager]  [EMPLOYEEAR_NAME]  [EMPLOYEEAR_ID]  [REQUESTAR_NO]  [REQUESTAR_TYPE]   [REQUESTAR_DATE] [REQUESTAR_STATUS]
                        //[DirectENManager]  [EMPLOYEEEN_NAME]  [EMPLOYEEEN_ID]  [REQUESTEN_NO]  [REQUESTEN_TYPE]   [REQUESTEN_DATE] [REQUESTEN_STATUS]
                    }
                } 

                SendEMail(EmailSubject, EmailBody,UsrEmail);
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendMailToEmpSwap(string pEmpID2, string pErqID, string pDateType, string pLang)
    {
        try
        {
            GetReqInfo(pErqID, pDateType);
            bool EmailInfo = GetEMailInfo();
            bool EmpInfo = GetEmpInfo(pEmpID2, pLang);
            if (EmailInfo & EmpInfo)
            {
                string date1 = ErqStartDate;
                if (!string.IsNullOrEmpty(ErqEndDate)) { if (ErqStartDate != ErqEndDate) { date1 += " - " + ErqEndDate; } }
                string date2 = ErqStartDate2;
                if (!string.IsNullOrEmpty(ErqEndDate2)) { if (ErqStartDate2 != ErqEndDate2) { date2 += " - " + ErqEndDate2; } }

                string EmailSubject = ShowMsg(EmpLang, "Request ID : " + pErqID, "طلب بالرقم " + pErqID);
                string EmailText    = ShowMsg(EmpLang,
                                              " Dear Employee "
                                            + "<br /> "
                                            + "Request ID : " + pErqID
                                            + " <br /> "
                                            + "Employee ID : " + EmpID
                                            + " <br /> "
                                            + "Request Type : " + GetReqName(RetID, "EN")
                                            + " <br /> "
                                            + "Request Date1 : " + date1
                                            + " <br /> "
                                            + "Request Date2 : " + date2
                                            + " <br /> "
                                            + "Request Status : Waiting",
                                            " عزيزي الموظف "
                                            + "<br /> "
                                            + "رقم الطلب : " + pErqID
                                            + " <br /> "
                                            + "رقم الموظف : " + EmpID
                                            + " <br /> "
                                            + "نوع الطلب : " + GetReqName(RetID, "AR")
                                            + " <br /> "
                                            + "من تاريخ  : " + date1
                                            + " <br /> "
                                            + "إلى تاريخ : " + date2
                                            + " <br /> "
                                            + "حالة الطلب : إنتظار");

                string EmailBody = @"
                                    <html>
                                    <head>
                                        <title>For Employee ID : " + pEmpID2 + @" </title>
                                    </head>
                                    <body >           
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align='" + ShowMsg(EmpLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
                                                        <span style=""font-size: 18pt; font-family:Arial"">
                                                            <br /> "
                                                            + EmailText
                                                            + @"<br />                                        
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </body>
                                    </html>";
                
                SendEMail(EmailSubject, EmailBody,EmpEmail);
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool SendResetPasswordToEmp(string pEmpID, string pPass, string pLang)
    {
        try
        {
            bool EmailInfo = GetEMailInfo();
            bool EmpInfo = GetEmpInfo(pEmpID, pLang);
            if (EmailInfo & EmpInfo)
            {
                //string ToEmail = GetEmpEMailID(pEmpID, out empLang);
                string EmailSubject = "AMSWeb: Request New Password";
//                string EmailBody = @"
//                                    <html>
//                                    <head>
//                                        <title>For Employee ID : " + pEmpID + @" </title>
//                                    </head>
//                                    <body >           
//                                        <div>
//                                            <table>
//                                                <tr>
//                                                   <td align='" + ShowMsg(EmpLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
//                                                        <span style=""font-size: 14pt; font-family:Tahoma"">
//                                                            Dear Employee :" + EmpName +
//                                                                "<br /> "
//                                                                 + " <br /> "
//                                                                + "you Requested change password for AMS account"
//                                                                + "<br /> "
//                                                                + "New Password is : " + pPass
//                                                                + " <br /> "
//                                                                + "you can change this password from AMSWEB "
//                                                                + @"<br />                                        
//                                                        </span>
//                                                    </td>
//                                                </tr>
//                                            </table>
//                                        </div>
//                                    </body>
//                                    </html>";
                

                string EmailText    = ShowMsg(EmpLang,
                                              " Dear Employee :" + EmpName
                                            + "<br /> "
                                            + "Password has been changed for for AMS account from AMS administrator system"
                                            + " <br /> "
                                            + "New Password is : " + pPass
                                            + " <br /> "
                                            + "you can change this password from AMSWEB "
                                            ,
                                            " عزيزي الموظف "
                                            + "<br /> "
                                            + "تم تغيير كلمة المرور للحسابك في نظام AMS  عن طريق مدير النظام"
                                            + " <br /> "
                                            + "كلمة المرور الجديدة : " + pPass
                                            + " <br /> "
                                            + "يمكنك تغيير كلمة المرور الجديدة بعد الدخول لحسابك في نظام AMS");
                string EmailBody = @"
                <html>
                <head>
                    <title>For Employee ID : " + pEmpID + @" </title>
                </head>
                <body >           
                    <div>
                        <table>
                            <tr>
                                <td align='" + ShowMsg(EmpLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
                                    <span style=""font-size: 18pt; font-family:Arial"">
                                        <br /> "
                                            + EmailText
                                            + @"<br />                                        
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";



                bool isSend = SendEMail(EmailSubject, EmailBody,EmpEmail);
                return isSend;
            }
            return false;
        }
        catch (Exception e1) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendWorkTimeToAdmin(string Action, string pUser, string pDateType, string pWktID)
    {
        try
        {
            bool EmailInfo = GetEMailInfo();
            bool UsrInfo = GetUsrInfo("admin");
            if (EmailInfo & UsrInfo)
            {
                string WktNameAr = "";
                string WktNameEn = "";
                DataTable DT = DBCs.FetchData(" SELECT WktNameAr,WktNameEn FROM WorkingTime WHERE WktID = @P1 ", new string[] { pWktID });
                if (!DBCs.IsNullOrEmpty(DT)) 
                {
                    if (DT.Rows[0]["WktNameAr"] != DBNull.Value) { WktNameAr = DT.Rows[0]["WktNameAr"].ToString(); }
                    if (DT.Rows[0]["WktNameEn"] != DBNull.Value) { WktNameEn = DT.Rows[0]["WktNameEn"].ToString(); }
                }

                string EmailText    = "";
                string EmailSubject = "";
                if (Action == "Add")
                {
                    EmailSubject = ShowMsg(UsrLang, " Add Worktime with ID : " + pWktID, "إضافة وقت عمل بالرقم " + pWktID);
                    EmailText    = ShowMsg(UsrLang, " The " + pUser + " user by adding the worktime with Name " + WktNameEn, "قام المستخدم " + pUser + "بإضافة وقت عمل بالاسم " + WktNameAr);
                }
                else
                {
                    EmailSubject = ShowMsg(UsrLang, " Update Worktime with ID : " + pWktID, "تعديل وقت عمل بالرقم " + pWktID);
                    EmailText    = ShowMsg(UsrLang, " The " + pUser + " user by updateing the worktime with Name " + WktNameEn, "قام المستخدم " + pUser + "بتعديل وقت عمل بالاسم " + WktNameAr);
                }

                string EmailBody = @"
                                    <html>
                                    <head>
                                        <title>For Administrator </title>
                                    </head>
                                    <body >           
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align='" + ShowMsg("EN","left", "right") + @"' style=""height: 59px; width: 600px; "">
                                                        <span style=""font-size: 18pt; font-family:Arial"">
                                                            <br />
                                                            Dear Administrator
                                                            <br /> "
                                                                + EmailText
                                                                + @"<br />                                        
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </body>
                                    </html>";

                SendEMail(EmailSubject, EmailBody,UsrEmail);
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string ShowMsg(string plang,string pEn, string pAr) { if (plang == "AR") { return pAr; } else { return pEn; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindLoginUrl(Uri pUrl)
    {
        try
        {
            string URL = pUrl.ToString();
            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(pUrl.AbsolutePath);
            string ResPageName = PageFileInfo.Name;
            string[] URLArr = URL.Split('?');
            string LoginUrl = URLArr[0].Replace(PageFileInfo.Name, "Login.aspx");
            return LoginUrl;
        }
        catch (Exception e1) { return string.Empty; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendAddTransMailToEmp(string pEmpID, string pDate, string pTime, string pLang)
    {
        try
        {
            bool EmailInfo = GetEMailInfo();
            bool EmpInfo   = GetEmpInfo(pEmpID,pLang);

            if (EmailInfo & EmpInfo)
            {
                string EmailSubject = ShowMsg(EmpLang, "Warning : Add Leave early Transaction without permission", "إنذار : إضافة حركة خروج مبكر بدون إذن");
                string EmailText    = ShowMsg(EmpLang,
                                              " Dear Employee "
                                            + "<br /> "
                                            + "Add Leave early Transaction without permission "
                                            + " <br /> "
                                            + "Leave early Transaction Date : " + pDate
                                            + " <br /> "
                                            + "Leave early Transaction Time : " + pTime
                                            ,
                                            " عزيزي الموظف "
                                            + "<br /> "
                                            + "إضافة حركة خروج مبكر بدون إذن" 
                                            + " <br /> "
                                            + "تاريخ حركة الخروج المبكر : " + pDate
                                            + " <br /> "
                                            + "وقت حركة الخروج المبكر : " + pTime);
                string EmailBody = @"
                <html>
                <head>
                    <title>For Employee ID : " + EmpID + @" </title>
                </head>
                <body >           
                    <div>
                        <table>
                            <tr>
                                <td align='" + ShowMsg(EmpLang,"left", "right") + @"' style=""height: 59px; width: 600px; "">
                                    <span style=""font-size: 18pt; font-family:Arial"">
                                        <br /> "
                                            + EmailText
                                            + @"<br />                                        
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";


                SendEMail(EmailSubject, EmailBody,EmpEmail);
            }
        } 
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetHtmlFile(string FileName)
    {
        string EmailBody = "";

        try
        {
            string filePath = "";
            if (FileName == "ToMGR_KaCare") { filePath = ServerMapPath("HTML/AMS_ToMGR_KaCare.html"); }
            if (FileName == "ToEMP_KaCare") { filePath = ServerMapPath("HTML/AMS_ToEMP_KaCare.html"); }

            EmailBody =  ReadHtmlFile(filePath).ToString();
        } 
        catch (Exception e1) { }

        return EmailBody;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ServerMapPath(string path) { return HttpContext.Current.Server.MapPath(path); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static System.Text.StringBuilder ReadHtmlFile(string htmlFileNameWithPath)
    {
        System.Text.StringBuilder storeContent = new System.Text.StringBuilder();

        try
        {
            using (System.IO.StreamReader htmlReader = new System.IO.StreamReader(htmlFileNameWithPath))
            {
                string lineStr;
                while ((lineStr = htmlReader.ReadLine()) != null)
                {
                    storeContent.Append(lineStr);
                }
            }
        }
        catch (Exception objError)
        {
            throw objError;
        }

        return storeContent;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}