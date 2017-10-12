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

public partial class SMSSetting : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AdminPro ProCs = new AdminPro();
    AdminSql SqlCs = new AdminSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    SMSSendFun SmsSendCs = new SMSSendFun();
    SMSFun SMSCs = new SMSFun();
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
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM SMSConfig "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            txtSmsGateway.Text = DT.Rows[0]["SmsGateway"].ToString();

            txtSmsSenderID.Text = DT.Rows[0]["SmsSenderID"].ToString();
            txtSmsSenderNo.Text = DT.Rows[0]["SmsSenderNo"].ToString();

            txtSmsUser.Text = DT.Rows[0]["SmsUser"].ToString();
            txtSmsPass.Attributes["value"] = CryptorEngine.Decrypt(DT.Rows[0]["SmsPass"].ToString(), true);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        txtSmsGateway.Enabled = pStatus;
        txtSmsSenderID.Enabled = pStatus;
        txtSmsSenderNo.Enabled = pStatus;
        txtSmsUser.Enabled = pStatus;
        txtSmsPass.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.SmsGateway  = txtSmsGateway.Text;
            ProCs.SmsSenderID = txtSmsSenderID.Text;
            if (!string.IsNullOrEmpty(txtSmsSenderNo.Text)) { ProCs.SmsSenderNo = txtSmsSenderNo.Text; }
            ProCs.SmsUser     = txtSmsUser.Text; 
            ProCs.SmsPass     = CryptorEngine.Encrypt(txtSmsPass.Text, true);

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
            SqlCs.SMSConfig_InsertUpdate(ProCs);

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
    protected void BtnStatus(string Status) //string pBtn = [Modify,Save,Cancel]
    {
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSendTestSms_Click(object sender, EventArgs e)
    {
        txtLogSend.Text = "";
        if (string.IsNullOrEmpty(txtSendToNo.Text)) { txtLogSend.Text = General.Msg("Enter Mobile No. Filed", "أدخل رقم الجوال"); return; }

        bool isEmail = Regex.IsMatch(txtSendToNo.Text, @"^[0-9\-\+]{9,15}$", RegexOptions.IgnoreCase);
        if (!isEmail) { txtLogSend.Text = General.Msg("Mobile No. is not valid", "رقم الجوال غير صالح"); return; }

        bool isValid = true;
        if (string.IsNullOrEmpty(txtSmsGateway.Text) || string.IsNullOrEmpty(txtSmsSenderID.Text) || string.IsNullOrEmpty(txtSmsUser.Text) || string.IsNullOrEmpty(txtSmsPass.Text)) { isValid = false; }
        if (!isValid) { txtLogSend.Text = General.Msg("No SMS Setting data entry", "لم يتم إدخال إعدادات الرسائل القصيرة"); return; }

        SendSMS();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool SendSMS()
    {
        try
        {
            string Msg = "WorkForce Test Send SMS";
            string No = SMSCs.FormatMobileNo(txtSendToNo.Text, "966");
            string re = SmsSendCs.SendMessage(txtSmsUser.Text, txtSmsPass.Text, Msg, txtSmsSenderID.Text, No);

            txtLogSend.Text = SmsSendCs.SendResult(re);
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
