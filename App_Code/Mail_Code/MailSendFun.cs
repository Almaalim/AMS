using System;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data.SqlClient;
using System.IO;
using Stimulsoft.Report;

public class MailSendFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General   GenCs = new General();
    DBFun     DBCs  = new DBFun();
    DTFun     DTCs  = new DTFun();
    ReportFun RepCs = new ReportFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public bool Send(MailFun MailCs ,string ToEmail, string Subject, string body,bool isAttachment, System.Net.Mail.Attachment Attachment, out string Err)
        {
            Err = null;

            try
            {
                System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
                if (isAttachment && Attachment == null) { Err = "Attachment Is Null"; return false; }
                if (isAttachment) { msgMail.Attachments.Add(Attachment); }
 
                MailAddress FromMail = new MailAddress(MailCs.SenderEmailID);
                msgMail.Body         = body;
                msgMail.Subject      = Subject;
                msgMail.To.Add(ToEmail);
                msgMail.From         = FromMail;
                msgMail.IsBodyHtml   = true;
                msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                if (!string.IsNullOrEmpty(ToEmail.Trim()))
                {
                   
                    SmtpClient SClient = new SmtpClient(MailCs.EmailServer, MailCs.EmailPort);
                    SClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    if (MailCs.EmailCredential)
                    {
                        SClient.UseDefaultCredentials = false;
                        SClient.Credentials = new NetworkCredential(MailCs.SenderEmailID, MailCs.SenderEmailPass);
                    }
                        
                    if (MailCs.EmailSsl)
                    {
                        SClient.EnableSsl = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    }

                    SClient.Timeout = 3600000; // 3600000 milliseconds = 3600 seconds  = 1 hour
                    SClient.Send(msgMail);

                    return true;
                }
            }
            catch (Exception EX)
            {
                Err = EX.Message;
                //General.WriteTimeMessage("Error : Schedules ID :" + SchID + ",To email : " + ToEmail);
                //General.WriteMessage("Message:" + EX.Message);


                //General.WriteMessage("From:" + EmailSetting.SenderEmailPass);
                //General.WriteMessage("SenderEMailPass:" + EmailSetting.SenderEmailPass);
                //General.WriteMessage("Server:" + EmailSetting.ServerID);
                //General.WriteMessage("PortNo:" + EmailSetting.PortNo.ToString());
                //General.WriteMessage("Body:" + body);
                //General.WriteMessage("Subject:" + Subject);
                //General.WriteMessage("to.Add:" + ToEmail);
                //General.WriteMessage("from:" + FromMail);
                //General.WriteMessage(EX.StackTrace.ToString());              
            }

            return false;
        }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public System.Net.Mail.Attachment Create(string RepID, string UserName, string UsrLanguage, string ExportType, string CalendarType)
    {
        try
        {
            RepParametersPro RepProCs = new RepParametersPro();  
            RepProCs = RepCs.FillMailReportParam(RepID, UserName, UsrLanguage, CalendarType);
            
            /* ########################################## */
            StiReport RepMail = RepCs.CreateReport(RepProCs);
            if (RepMail == null) 
            { 
                //General.WriteMessage(General.Schedule_FileLogPath, "Error : When Create Report "); 
                return null; 
            }
            /* ########################################## */
            System.Net.Mail.Attachment attachment;
            MemoryStream ms = new MemoryStream();

            if (ExportType.Trim() == "Pdf") { RepMail.ExportDocument(StiExportFormat.Pdf, ms); } else { RepMail.ExportDocument(StiExportFormat.Excel, ms); }
            ms.Seek(0, SeekOrigin.Begin);
            
            if (ExportType.Trim() == "Pdf")
            {
                attachment = new System.Net.Mail.Attachment(ms, RepProCs.RepName + ".pdf");
            }
            else
            {
                attachment = new System.Net.Mail.Attachment(ms, RepProCs.RepName + ".xls");
            }
            /* ########################################## */
            return attachment;
        }
        catch (Exception ex)
        {
            //General.WriteMessage(General.Schedule_FileLogPath, "Error : When Create Attachment ");
            //General.WriteTimeMessage(General.Schedule_FileLogPath, ex.Message);
            return null;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}