using System;
using System.Web.UI;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class ConnString : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        chkEncrypt.Checked = false;
        
        string connectionString        = ConfigurationManager.AppSettings["Main.ConnectionString"].ToString();
        string connectionStringDecrypt = connectionString;

        try
        {
            string IsEncryption = ConfigurationManager.AppSettings["Encryption.ConnectionString"].ToString();
            if (!string.IsNullOrEmpty(IsEncryption))
            {
                if (IsEncryption == "1") { chkEncrypt.Checked = true; connectionStringDecrypt = CryptorEngine.Decrypt(connectionString, true); }
            }
        }
        catch { }   


        /////////////////////////////////////////////
        string[] Conn = connectionStringDecrypt.Split(';');

        for (int i = 0; i < Conn.Length; i++)
        {
            if ((Conn[i].Split('='))[0].Trim().ToLower() == "data source")     { txtServerName.Text   = (Conn[i].Split('='))[1].Trim(); }
            if ((Conn[i].Split('='))[0].Trim().ToLower() == "user id")         { txtUserName.Text     = (Conn[i].Split('='))[1].Trim(); }
            if ((Conn[i].Split('='))[0].Trim().ToLower() == "password")        { txtPassword.Attributes["value"] = (Conn[i].Split('='))[1].Trim(); }
            if ((Conn[i].Split('='))[0].Trim().ToLower() == "initial catalog") { txtDatabaseName.Text = (Conn[i].Split('='))[1].Trim(); }
        }
        /////////////////////////////////////////////
    } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public void UpdateSetting(string key, string value)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
        if (config.AppSettings.Settings[key] == null)
        {
            config.AppSettings.Settings.Add(key, value);
        }
        else
        {
            config.AppSettings.Settings[key].Value = value;
        }
        config.Save();
        ConfigurationManager.RefreshSection("appSettings");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void UpdateConnectionString(string key, string value)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
        if (config.ConnectionStrings.ConnectionStrings[key] == null)
        {
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(key, value));
        }
        else
        {
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
        }
        config.Save();
        ConfigurationManager.RefreshSection("connectionStrings");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        chkEncrypt.Enabled      = pStatus;
        txtServerName.Enabled   = pStatus;
        txtUserName.Enabled     = pStatus;
        txtPassword.Enabled     = pStatus;
        txtDatabaseName.Enabled = pStatus;
        btnTest.Enabled         = pStatus;
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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string conn = "";
            bool isConn = Find(out conn);
        
            string DBConEncrypt = conn;
            if (chkEncrypt.Checked) { DBConEncrypt = CryptorEngine.Encrypt(conn, true); }

            //UpdateSetting("Encryption.ConnectionString", Convert.ToInt32(chkEncrypt.Checked).ToString());
            //UpdateSetting("Main.ConnectionString"      , DBConEncrypt);

            //UpdateConnectionString("Elmah.ConnectionString", DBConEncrypt);

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
    protected void btnTest_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) { return; }
       
        string conn = "";
        bool isConn = Find(out conn);

        if (isConn)
        {
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Test connection succeeded", "تم الإتصال بقاعدة البيانات بنجاح"));
        }
        else 
        { 
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("Test connection Failed", "لم يتتم الإتصال بقاعدة البيانات "));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Find(out string ConnString)
    {
        ConnString = "";

        if (string.IsNullOrEmpty(txtServerName.Text))   { return false; } //"Enter Server Name Filed "
        if (string.IsNullOrEmpty(txtUserName.Text))     { return false; } //"Enter User Name Filed "
        if (string.IsNullOrEmpty(txtPassword.Text))     { return false; } //"Enter Password Filed "
        if (string.IsNullOrEmpty(txtDatabaseName.Text)) { return false; } //"Enter Database Name Filed "

        StringBuilder sConn = new StringBuilder();
        sConn.Append("Data Source=" + txtServerName.Text + ";");
        sConn.Append("Initial Catalog=" + txtDatabaseName.Text + ";");
        sConn.Append("User ID=" + txtUserName.Text + ";");
        sConn.Append("Password=" + txtPassword.Text + ";");

        sConn.Append("Integrated Security=False;");
        sConn.Append("Persist Security Info=True;");
        sConn.Append("Max pool size=3500;");

        ConnString = sConn.ToString();
        SqlConnection sqlConn = new SqlConnection(sConn.ToString());
        try
        {
            using (sqlConn)
            {
                sqlConn.Open();
                sqlConn.Close();
            }
        }
        catch (Exception ex)
        {
            sqlConn.Close();
            CtrlCs.ShowAdminMsg(this, ex.Message);
            return false;
        }

        return true;
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
