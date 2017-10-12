using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Configuration;
using Elmah;
using System.Data.SqlClient;

public partial class ADSetting : BasePage
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
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();   
                BtnStatus("100");
                UIEnabled(false);
                ADPopulate();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
                ViewState["LoginID"] = pgCs.LoginID;
                ViewState["Lang"]    = pgCs.Lang;
                ViewState["Version"] = pgCs.Version;
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ADPopulate()
	{
       try
       {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ADConfig "));
            if (!DBCs.IsNullOrEmpty(DT)) 
            {
                chkADEnabled.Checked = false;
                if (DT.Rows[0]["ADEnabled"] != DBNull.Value)
                {
                    string ADEnabled = DT.Rows[0]["ADEnabled"].ToString();
                    try { ADEnabled = CryptorEngine.Decrypt(ADEnabled, true); } catch (Exception e1) { }
                    if (ADEnabled == "1") { chkADEnabled.Checked = true; } else { chkADEnabled.Checked = false; }
                }

                if (DT.Rows[0]["ADDomainName"] != DBNull.Value) { txtADDomainName.Text = DT.Rows[0]["ADDomainName"].ToString(); }
                if (DT.Rows[0]["ADJoinMethod"] != DBNull.Value) { ddlADJoinMethod.SelectedIndex = ddlADJoinMethod.Items.IndexOf(ddlADJoinMethod.Items.FindByValue(DT.Rows[0]["ADJoinMethod"].ToString())); }
                if (DT.Rows[0]["ADColName"]    != DBNull.Value) { txtADColName.Text = DT.Rows[0]["ADColName"].ToString(); }
            }
            else
            {
                ProCs.ADEnabled     = CryptorEngine.Encrypt("0", true);
                ProCs.ADJoinMethod  = "Email";
                ProCs.AMSUsrColName = "UsrEMailID"; 
                ProCs.AMSEmpColName = "EmpEmailID";
                ProCs.ADUsrColName  = "samaccountname";
                ProCs.TransactionBy = ViewState["LoginID"].ToString();
                SqlCs.ADConfig_InsertUpdate(ProCs);

                ADPopulate();
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
        chkADEnabled.Enabled    = pStatus;
        txtADDomainName.Enabled = pStatus;
        ddlADJoinMethod.Enabled = pStatus;
        txtADColName.Enabled    = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.ADEnabled = CryptorEngine.Encrypt((chkADEnabled.Checked) ? "1" : "0", true);
            
            ProCs.ADJoinMethod = ddlADJoinMethod.SelectedValue;
            switch (ddlADJoinMethod.SelectedValue)
            {
                case "Email": ProCs.AMSUsrColName = "UsrEMailID"; ProCs.AMSEmpColName = "EmpEmailID";    break; //ADColName = "mail"; 
                case "ID"   : ProCs.AMSUsrColName = "EmpID";      ProCs.AMSEmpColName = "EmpNationalID"; break; //ADColName = "employeeID";
                case "EmpID": ProCs.AMSUsrColName = "EmpID";      ProCs.AMSEmpColName = "EmpID";         break; //ADColName = "mail"; 
            }

            ProCs.ADUsrColName = "samaccountname";

            if (!string.IsNullOrEmpty(txtADDomainName.Text)) { ProCs.ADDomainName = txtADDomainName.Text; }
            if (!string.IsNullOrEmpty(txtADColName.Text))    { ProCs.ADColName    = txtADColName.Text; }

            ProCs.TransactionBy = ViewState["LoginID"].ToString();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        //ViewState["CommandName"] = "";
        
        //txtID.Text = "";
        //txtNameAr.Text = "";
        //txtNameEn.Text = "";
        //txtBranchAddress.Text = "";
        //txtBranchCity.Text = "";
        //txtBranchCountry.Text = "";
        //txtBranchPoBox.Text = "";
        //txtBranchTell.Text = "";
        //txtBranchEmail.Text = "";
        //chkStatus.Checked = false;
        //ddlBranchManagerID.SelectedIndex = -1;
        //ddlBranchParentName.SelectedIndex = -1;
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

            FillPropeties();

            //System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            //AuthenticationSection AuthSection = (AuthenticationSection)config.GetSection("system.web/authentication");
            //AuthSection.Mode = AuthenticationMode.Windows;
            
            //IdentitySection IdenSection = (IdentitySection)config.GetSection("system.web/identity");
            //if (chkADEnabled.Checked) { IdenSection.Impersonate = true; } else { IdenSection.Impersonate = false; } 
            
            //config.Save();
            SqlCs.ADConfig_InsertUpdate(ProCs);
            ////////////////////////

            //using Microsoft.Web.Administration;  // C:\Windows\System32\inetsrv
            //using (ServerManager serverManager = new ServerManager())
            //{
                
            //    Microsoft.Web.Administration.Configuration config = serverManager.GetApplicationHostConfiguration();
            //    Microsoft.Web.Administration.ConfigurationSection anonymousAuthenticationSection = config.GetSection("system.webServer/security/authentication/anonymousAuthentication", webSiteName + "/" + applicationName);
            //    anonymousAuthenticationSection["enabled"] = false;
            //    Microsoft.Web.Administration.ConfigurationSection windowsAuthenticationSection = config.GetSection("system.webServer/security/authentication/windowsAuthentication", webSiteName + "/" + applicationName);
            //    windowsAuthenticationSection["enabled"] = true;
            //    serverManager.CommitChanges();
            //}

            //using(ServerManager serverManager = new ServerManager()) 
            //{ 
            //    string[] aAppUrl = Request.Url.AbsolutePath.Split('/');
            //    string AppName = aAppUrl[1];


            //     Microsoft.Web.Administration.Configuration config = serverManager.GetApplicationHostConfiguration();
            //     Microsoft.Web.Administration.ConfigurationSection anonymousAuthenticationSection = config.GetSection("system.webServer/security/authentication/anonymousAuthentication", "Default Web Site" + "/" + AppName);
            //     anonymousAuthenticationSection["enabled"] = false;
            //     serverManager.CommitChanges();
            //  }

            ////////////////////////
            UIEnabled(false);
            BtnStatus("100");
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
        ADPopulate();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) 
    {
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
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
