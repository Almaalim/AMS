using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Elmah;

public partial class EmailSetting : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AdminPro ProCs = new AdminPro();
    AdminSql SqlCs = new AdminSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/  
                BtnStatus("100");
                UIEnabled(false);
                /*** Common Code ************************************/

                Populate();
            }
        }
        catch (Exception ex) { }     
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Populate()
	{
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM EmailConfig "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            txtServerID.Text = DT.Rows[0]["EmlServerID"].ToString();
            txtPortNo.Text   = DT.Rows[0]["EmlPortNo"].ToString();

            txtSenderEmailID.Text = DT.Rows[0]["EmlSenderEmail"].ToString();
            txtSenderName.Text = DT.Rows[0]["EmlSenderName"].ToString();
            
            txtSenderEmailPassword.Attributes["value"] = CryptorEngine.Decrypt(DT.Rows[0]["EmlSenderPassword"].ToString(), true);

            chkEmailCredential.Checked = false;
            if (DT.Rows[0]["EmlCredential"] != DBNull.Value) { chkEmailCredential.Checked = Convert.ToBoolean(DT.Rows[0]["EmlCredential"]); }

            chkEnableSSL.Checked = false;
            if (DT.Rows[0]["EmlSsl"] != DBNull.Value) { chkEnableSSL.Checked = Convert.ToBoolean(DT.Rows[0]["EmlSsl"]); }
        }
    } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        txtServerID.Enabled            = pStatus;
        txtPortNo.Enabled              = pStatus;
        txtSenderEmailID.Enabled       = pStatus;
        txtSenderName.Enabled          = pStatus;
        txtSenderEmailPassword.Enabled = pStatus;
        chkEnableSSL.Enabled           = pStatus;
        chkEmailCredential.Enabled     = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.EmlServerID       = txtServerID.Text;
            ProCs.EmlPortNo         = txtPortNo.Text;
            if (!string.IsNullOrEmpty(txtSenderEmailID.Text))       { ProCs.EmlSenderEmail    = txtSenderEmailID.Text; }
            if (!string.IsNullOrEmpty(txtSenderName.Text))          { ProCs.EmlSenderName    = txtSenderName.Text; }
            if (!string.IsNullOrEmpty(txtSenderEmailPassword.Text)) { ProCs.EmlSenderPassword = CryptorEngine.Encrypt(txtSenderEmailPassword.Text, true); }
            ProCs.EmlCredential     = chkEmailCredential.Checked;
            ProCs.EmlSsl            = chkEnableSSL.Checked;
            
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        BtnStatus("011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!CtrlCs.PageIsValid(this, vsSave)) { return; }
            if (!Page.IsValid) { return; }

            FillPropeties();
            SqlCs.EmailConfig_InsertUpdate(ProCs);

            UIEnabled(false);
            BtnStatus("100");

            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex) 
        { 
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BtnStatus("100");
        UIEnabled(false);
        Populate();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSendTestEmail_Click(object sender, EventArgs e)
    {
        txtLogSend.Text = "";
        if (string.IsNullOrEmpty(txtSendToEmail.Text)) { txtLogSend.Text = General.Msg("Enter Email To Filed", "أدخل البريد الإلكتروني"); return; } 

        bool isEmail = Regex.IsMatch(txtSendToEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (!isEmail) { txtLogSend.Text = General.Msg("Email is not valid", "البريد الإلكتروني غير صالح"); return; }

        bool isValid = true;
        if (string.IsNullOrEmpty(txtServerID.Text) || string.IsNullOrEmpty(txtPortNo.Text) || string.IsNullOrEmpty(txtSenderEmailID.Text) || string.IsNullOrEmpty(txtSenderEmailPassword.Text)) { isValid = false; }
        if (!isValid) { txtLogSend.Text = General.Msg("No Email Setting data entry","لم يتم إدخال إعدادات البريد الإلكتروني"); return; }

        SendEMail();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool SendEMail()
    {
        try
        {
            System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
            msgMail.Subject = "Test Email From AMS WEB Scheduling Reports ";
            msgMail.Body    = "<b>Test Email</b>";             
               
            msgMail.To.Add(txtSendToEmail.Text.Trim());
            msgMail.From = new MailAddress(txtSenderEmailID.Text.Trim(),txtSenderName.Text);
            msgMail.IsBodyHtml = true;
            msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient SClient = new SmtpClient();
            SClient = new SmtpClient(txtServerID.Text, Convert.ToInt32(txtPortNo.Text));
            SClient.DeliveryMethod = SmtpDeliveryMethod.Network; 
            
            if (chkEmailCredential.Checked)
            {
                SClient.UseDefaultCredentials = false;
                SClient.Credentials = new NetworkCredential(txtSenderEmailID.Text, txtSenderEmailPassword.Text);
            }
            
            if (chkEnableSSL.Checked)
            {
                SClient.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            }

            SClient.Send(msgMail);

            txtLogSend.Text = General.Msg("The email has been sent successfully", "لقد تم إرسال الايميل بنجاح");
            return true;
        }
        catch (Exception ex) { txtLogSend.Text = ex.Message; return false; }
    }  

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

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
