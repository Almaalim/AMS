using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using Elmah;

public partial class Pages_Admin_ImportSetting : BasePage
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
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ImportSetting "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            chkIpsSaveTransInFile.Checked    = Convert.ToBoolean(DT.Rows[0]["IpsSaveTransInFile"]);
            chkIpsEncryptTransInFile.Checked = Convert.ToBoolean(DT.Rows[0]["IpsEncryptTransInFile"]);
            chkIpsRunProcess.Checked         = Convert.ToBoolean(DT.Rows[0]["IpsRunProcess"]);

            ShowProcess(chkIpsRunProcess.Checked);

            ucImportTimes.FillTimes(DT.Rows[0]["IpsImportScheduleTimes"].ToString());
            ucProcessTimes.FillTimes(DT.Rows[0]["IpsProcessScheduleTimes"].ToString());
        }
    } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        chkIpsSaveTransInFile.Enabled    = pStatus;
        chkIpsEncryptTransInFile.Enabled = pStatus;
        chkIpsRunProcess.Enabled         = pStatus;
        ucImportTimes.Enabled(pStatus);
        ucProcessTimes.Enabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.IpsSaveTransInFile     = chkIpsSaveTransInFile.Checked;
            ProCs.IpsEncryptTransInFile  = chkIpsEncryptTransInFile.Checked;
            ProCs.IpsRunProcess          = chkIpsRunProcess.Checked;
            ProCs.IpsImportScheduleTimes = ucImportTimes.getTimes();

            if (!chkIpsRunProcess.Checked) { ProCs.IpsProcessScheduleTimes = ucProcessTimes.getTimes(); }

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowProcess(bool pStatus)
    {
        divProcess1.Visible = !pStatus;
        divProcess2.Visible = !pStatus;
        ucProcessTimes.isValid(!pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkIpsRunProcess_CheckedChanged(object sender, EventArgs e)
    {
        ShowProcess(chkIpsRunProcess.Checked);
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
            SqlCs.ImportSetting_InsertUpdate(ProCs);

            TaskSchedulerFun SF = new TaskSchedulerFun();
            SF.CreateScheduled(ProCs.IpsImportScheduleTimes, ProCs.IpsProcessScheduleTimes, ProCs.IpsRunProcess);

            UIEnabled(false);
            BtnStatus("100");

            Populate();
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
