using System;
using System.Web.UI;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using Elmah;
using System.Collections;

public partial class ApplicationSetting : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ApplicationSettingPro ProCs = new ApplicationSettingPro();
    ApplicationSettingSql SqlCs = new ApplicationSettingSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();

    string MainQuery = "SELECT * FROM ApplicationSetup";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();

            for (int i = 1; i <= 1; i++)
            {
                AjaxControlToolkit.AnimationExtender aniExtShow = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderShow" + i.ToString());
                AjaxControlToolkit.AnimationExtender aniExtClose = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderClose" + i.ToString());
                ImageButton lnkShow = (ImageButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("lnkShow" + i.ToString());
                if (aniExtShow != null) { CtrlCs.Animation(ref aniExtShow, ref aniExtClose, ref lnkShow, i.ToString()); }
            }
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("100");
                UIEnabled(false);

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                PopulateUI();
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI()
    {
        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                txtAppCompany.Text  = DT.Rows[0]["AppCompany"].ToString();
                txtAppDisplay.Text  = DT.Rows[0]["AppDisplay"].ToString();
                txtAppCountry.Text  = DT.Rows[0]["AppCountry"].ToString();
                txtAppCity.Text     = DT.Rows[0]["AppCity"].ToString();
                txtAppAddress1.Text = DT.Rows[0]["AppAddress1"].ToString();
                txtAppAddress2.Text = DT.Rows[0]["AppAddress2"].ToString();
                txtAppTelNo1.Text   = DT.Rows[0]["AppTelNo1"].ToString();
                txtAppTelNo2.Text   = DT.Rows[0]["AppTelNo2"].ToString();
                txtAppPOBox.Text    = DT.Rows[0]["AppPOBox"].ToString();

                if (DT.Rows[0]["AppCalendar"]   != DBNull.Value) { ddlAppCalendar.SelectedIndex   = ddlAppCalendar.Items.IndexOf(ddlAppCalendar.Items.FindByValue(DT.Rows[0]["AppCalendar"].ToString())); }       else { ddlAppCalendar.SelectedIndex   = 0; }
                if (DT.Rows[0]["AppDateFormat"] != DBNull.Value) { ddlAppDateFormat.SelectedIndex = ddlAppDateFormat.Items.IndexOf(ddlAppDateFormat.Items.FindByValue(DT.Rows[0]["AppDateFormat"].ToString())); } else { ddlAppDateFormat.SelectedIndex = 0; }

                if (DT.Rows[0]["AppDataLangRequired"] != DBNull.Value) { rdlAppDataLangRequired.SelectedIndex = rdlAppDataLangRequired.Items.IndexOf(rdlAppDataLangRequired.Items.FindByValue(DT.Rows[0]["AppDataLangRequired"].ToString())); } else { rdlAppDataLangRequired.SelectedIndex = 0; }

                if (DT.Rows[0]["AppMiniLogger_VerificationMethod"] != DBNull.Value) { ddlAppMiniLogger_VerificationMethod.SelectedIndex = ddlAppMiniLogger_VerificationMethod.Items.IndexOf(ddlAppMiniLogger_VerificationMethod.Items.FindByValue(DT.Rows[0]["AppMiniLogger_VerificationMethod"].ToString())); } else { ddlAppMiniLogger_VerificationMethod.SelectedIndex = 0; }

                if (DT.Rows[0]["AppSessionDuration"] != DBNull.Value) { txtSessionDuration.SetTime(Convert.ToInt32(DT.Rows[0]["AppSessionDuration"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); } else { txtSessionDuration.SetTime(600, TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        txtAppCompany.Enabled  = pStatus;
        txtAppDisplay.Enabled  = pStatus;
        txtAppCountry.Enabled  = pStatus;
        txtAppCity.Enabled     = pStatus;
        txtAppAddress1.Enabled = pStatus;
        txtAppAddress2.Enabled = pStatus;
        txtAppTelNo1.Enabled   = pStatus;
        txtAppTelNo2.Enabled   = pStatus;
        txtAppPOBox.Enabled    = pStatus;

        ddlAppCalendar.Enabled = pStatus;
        ddlAppDateFormat.Enabled = false;
        rdlAppDataLangRequired.Enabled = pStatus;
        ddlAppMiniLogger_VerificationMethod.Enabled = pStatus;
        txtSessionDuration.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            ProCs.AppCompany = txtAppCompany.Text;
            ProCs.AppDisplay = txtAppDisplay.Text;
            if (!string.IsNullOrEmpty(txtAppCountry.Text))  { ProCs.AppCountry  = txtAppCountry.Text;  }
            if (!string.IsNullOrEmpty(txtAppCity.Text))     { ProCs.AppCity     = txtAppCity.Text;     }
            if (!string.IsNullOrEmpty(txtAppAddress1.Text)) { ProCs.AppAddress1 = txtAppAddress1.Text; }
            if (!string.IsNullOrEmpty(txtAppAddress2.Text)) { ProCs.AppAddress2 = txtAppAddress2.Text; }
            if (!string.IsNullOrEmpty(txtAppTelNo1.Text))   { ProCs.AppTelNo1   = txtAppTelNo1.Text;   }
            if (!string.IsNullOrEmpty(txtAppTelNo2.Text))   { ProCs.AppTelNo2   = txtAppTelNo2.Text;   }
            if (!string.IsNullOrEmpty(txtAppPOBox.Text))    { ProCs.AppPOBox    = txtAppPOBox.Text;    }

            ProCs.AppCalendar = ddlAppCalendar.SelectedValue;
            ProCs.AppDateFormat = ddlAppDateFormat.SelectedValue;
            if (rdlAppDataLangRequired.SelectedIndex == -1) { ProCs.AppDataLangRequired = "E"; } else { ProCs.AppDataLangRequired = rdlAppDataLangRequired.SelectedValue.ToString(); }
            ProCs.AppMiniLogger_VerificationMethod = ddlAppMiniLogger_VerificationMethod.SelectedValue;

            if (txtSessionDuration.getTimeInSecond() == -1 || txtSessionDuration.getTimeInSecond() == 0) { ProCs.AppSessionDuration = "600"; }
            else { ProCs.AppSessionDuration = txtSessionDuration.getTimeInSecond().ToString(); }
            
            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            ViewState["CommandName"] = "";
            txtAppCompany.Text  = "";
            txtAppDisplay.Text  = "";
            txtAppCountry.Text  = "";
            txtAppCity.Text     = "";
            txtAppAddress1.Text = "";
            txtAppAddress2.Text = "";
            txtAppTelNo1.Text   = "";
            txtAppTelNo2.Text   = "";
            txtAppPOBox.Text    = "";

            ddlAppCalendar.SelectedIndex = 0;
            ddlAppDateFormat.SelectedIndex = 0;
            rdlAppDataLangRequired.SelectedIndex = 0;
            ddlAppMiniLogger_VerificationMethod.SelectedIndex = 0;

            txtSessionDuration.SetTime(10, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
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
            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (commandName == "ADD") { } else if (commandName == "EDIT") { SqlCs.InsertUpdate(ProCs); }

            BtnStatus("100");
            UIEnabled(false);

            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BtnStatus("100");
        UIEnabled(false);
        PopulateUI();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);

        if (Status[0] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
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
