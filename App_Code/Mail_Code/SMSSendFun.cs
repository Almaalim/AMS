using System;
using System.Net;
using System.IO;
using Elmah;

public class SMSSendFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();

    SMSFun SMSCs = new SMSFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string SendSMS(string SMSmsgEn, string SMSMsgAr, string EmpIDs)
    {
        if (!SMSCs.FillSMSSetting()) { return "Error : When read setting Email"; }

        try
        {
            //bool isSend = false;
            //string Ver = LicDf.FindActiveVersion();
            //string Version = (Ver == "General" ? "0" : Ver);

            string EnNOs = "";
            string ArNOs = "";
            SMSCs.FindEmpMobileNo(EmpIDs, out EnNOs, out ArNOs);

            string reSendEn = "";
            string reSendAr = "";
            if (!string.IsNullOrEmpty(EnNOs)) { reSendEn = SendMessage(SMSCs.SMSUser, SMSCs.SMSPass, SMSmsgEn, SMSCs.SMSSenderID, EnNOs); } else { reSendEn = "1"; }
            if (!string.IsNullOrEmpty(ArNOs)) { reSendAr = SendMessage(SMSCs.SMSUser, SMSCs.SMSPass, SMSMsgAr, SMSCs.SMSSenderID, ArNOs); } else { reSendAr = "1"; }

            if (reSendEn == "1" && reSendAr == "1" ) { return "1"; }
            else if (reSendEn != "1" && reSendAr == "1" ) { return reSendEn; }
            else if (reSendEn == "1" && reSendAr != "1" ) { return reSendAr; }
            else { return reSendEn; }

        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return ex.Message; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string SendMessage(string username, string password, string msg, string sender, string numbers)
    {
        //int temp = '0';

        HttpWebRequest req = (HttpWebRequest)
        WebRequest.Create("http://www.mobily.ws/api/msgSend.php");
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        string postData = "mobile=" + username + "&password=" + password + "&numbers=" + numbers + "&sender=" + sender + "&msg=" + ConvertToUnicode(msg) + "&applicationType=59";
        req.ContentLength = postData.Length;

        StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        stOut.Write(postData);
        stOut.Close();
        // Do the request to get the response
        string strResponse;
        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
        strResponse = stIn.ReadToEnd();
        stIn.Close();
        return strResponse;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ConvertToUnicode(string val)
    {
        string msg2 = string.Empty;

        for (int i = 0; i < val.Length; i++)
        {
            msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
        }

        return msg2;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string convertToUnicode(char ch)
    {
        System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
        byte[] msg = class1.GetBytes(System.Convert.ToString(ch));

        return fourDigits(msg[1] + msg[0].ToString("X"));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string fourDigits(string val)
    {
        string result = string.Empty;

        switch (val.Length)
        {
            case 1: result = "000" + val; break;
            case 2: result = "00" + val; break;
            case 3: result = "0" + val; break;
            case 4: result = val; break;
        }

        return result;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string SendResult(string Result)
    {
        string re = "";
        switch (Result)
        {
            case "1": re = General.Msg("The send operation was successful", "تمت العملية بنجاح"); break; //SMS Have Been Send
            case "2": re = General.Msg("Your balance at Mobily has expired and no longer has any messages", "إن رصيدك لدى موبايلي قد إنتهى ولم يعد به أي رسائل"); break; //balabce is zero (SMS Not Send)
            case "3": re = General.Msg("Your current balance is not enough to complete your submission", "إن رصيدك الحالي لا يكفي لإتمام عملية الإرسال"); break; //balance not enough (SMS Not Send)              
            case "4": re = General.Msg("The username you used to access the message account is incorrect", "إن إسم المستخدم الذي إستخدمته للدخول إلى حساب الرسائل غير صحيح"); break; //error in user name (SMS Not Send)
            case "5": re = General.Msg("There is a password error", "هناك خطأ في كلمة المرور"); break; //error in password (SMS Not Send)
            case "6": re = General.Msg("The sending page is currently not responding", "إن صفحة الإرسال لاتجيب في الوقت الحالي"); break; //there is a problem in sending, try again later  (SMS Not Send)
            case "12": re = General.Msg("Your account needs updating Please check technical support", "إن حسابك بحاجة إلى تحديث يرجى مراجعة الدعم الفني"); break;
            case "13": re = General.Msg("The sender you used in this message is not accepted", "إن إسم المرسل الذي إستخدمته في هذه الرسالة لم يتم قبوله"); break;
            case "14": re = General.Msg("The name of the sender you used is not defined by Mobily", "إن إسم المرسل الذي إستخدمته غير معرف لدى موبايلي"); break;
            case "15": re = General.Msg("There is a wrong mobile number in the numbers you have sent", "يوجد رقم جوال خاطئ في الأرقام التي قمت بالإرسال لها"); break;
            case "16": re = General.Msg("The message you sent does not contain a sender name", "الرسالة التي قمت بإرسالها لا تحتوي على إسم مرسل"); break;
            case "17": re = General.Msg("Make sure to send the message text and make sure the message is converted to Unicode", "التأكد من ارسال نص الرسالة والتأكد من تحويل الرسالة الى يوني كود"); break;
            case "18": re = General.Msg("The send is currently off", "الارسال متوقف حاليا"); break;
            case "19": re = General.Msg("ApplicationType does not exist in the connector", "applicationType غير موجود في الرابط"); break;
            case "-1": re = General.Msg("The sending server was not successfully connected", "لم يتم التواصل مع خادم الإرسال بنجاح"); break;
            case "-2": re = General.Msg("The database that contains your account and your data has not been linked to Mobily", "لم يتم الربط مع قاعدة البيانات التي تحتوي على حسابك وبياناتك لدى موبايلي"); break;
        }

        return re;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}