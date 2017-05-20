using System;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class Configuration : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ConfigPro ProCs = new ConfigPro();
    ConfigSql SqlCs = new ConfigSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    
    //DataTable dt;  
    string MainQuery = "SELECT * FROM Configuration";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            for (int i = 1; i <= 14; i++)
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
                BtnStatus("0100");
                UIEnabled(false);
                //UILang();

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                PopulateUI();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
                ddlcfgOrderTransType.SelectedIndex = ddlcfgOrderTransType.Items.IndexOf(ddlcfgOrderTransType.Items.FindByValue(DT.Rows[0]["cfgOrderTransType"].ToString()));
                if (DT.Rows[0]["cfgAutoIn"] != DBNull.Value) { chkAutoIn.Checked = Convert.ToBoolean(DT.Rows[0]["cfgAutoIn"]); }
                if (DT.Rows[0]["cfgIsTakeAutoIn"] != DBNull.Value) { chkIsTakeAutoIn.Checked = Convert.ToBoolean(DT.Rows[0]["cfgIsTakeAutoIn"]); }
                if (DT.Rows[0]["cfgAutoOut"] != DBNull.Value) { chkAutoOut.Checked = Convert.ToBoolean(DT.Rows[0]["cfgAutoOut"]); }
                if (DT.Rows[0]["cfgIsTakeAutoOut"] != DBNull.Value) { chkIsTakeAutoOut.Checked = Convert.ToBoolean(DT.Rows[0]["cfgIsTakeAutoOut"]); }

                txtMiddleGapCount.Text = DT.Rows[0]["cfgMiddleGapCount"].ToString();
                txtICApprovalPercent.Text = DT.Rows[0]["cfgICApprovalPercent"].ToString();
                ddlRedundantInSelection.SelectedIndex = ddlRedundantInSelection.Items.IndexOf(ddlRedundantInSelection.Items.FindByValue(DT.Rows[0]["cfgRedundantInSelection"].ToString()));
                ddlRedundantOutSelection.SelectedIndex = ddlRedundantInSelection.Items.IndexOf(ddlRedundantInSelection.Items.FindByValue(DT.Rows[0]["cfgRedundantOutSelection"].ToString()));

                if (DT.Rows[0]["cfgMaxPercOT"] != DBNull.Value) { txtMaxPercOT.SetTime(Convert.ToInt32(DT.Rows[0]["cfgMaxPercOT"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                if (DT.Rows[0]["cfgOTBeginEarlyFlag"] != DBNull.Value) { chkOTBeginEarlyFlag.Checked = Convert.ToBoolean(DT.Rows[0]["cfgOTBeginEarlyFlag"]); }
                if (DT.Rows[0]["cfgOTNoShiftFlag"] != DBNull.Value) { chkOTNoShiftFlag.Checked = Convert.ToBoolean(DT.Rows[0]["cfgOTNoShiftFlag"]); }
                if (DT.Rows[0]["cfgOTOutLateFlag"] != DBNull.Value) { chkOTOutLateFlag.Checked = Convert.ToBoolean(DT.Rows[0]["cfgOTOutLateFlag"]); }
                if (DT.Rows[0]["cfgOTOutOfShiftFlag"] != DBNull.Value) { chkOTOutOfShiftFlag.Checked = Convert.ToBoolean(DT.Rows[0]["cfgOTOutOfShiftFlag"]); }
                if (DT.Rows[0]["cfgOTInVacFlag"] != DBNull.Value) { chkOTVacationFlag.Checked = Convert.ToBoolean(DT.Rows[0]["cfgOTInVacFlag"]); }

                if (DT.Rows[0]["cfgOTBeginEarlyInterval"] != DBNull.Value) { txtOTBeginEarlyInterval.SetTime(Convert.ToInt32(DT.Rows[0]["cfgOTBeginEarlyInterval"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                if (DT.Rows[0]["cfgOTNoShiftInterval"] != DBNull.Value) { txtOTNoShiftInterval.SetTime(Convert.ToInt32(DT.Rows[0]["cfgOTNoShiftInterval"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                if (DT.Rows[0]["cfgOTOutLateInterval"] != DBNull.Value) { txtOTOutLateInterval.SetTime(Convert.ToInt32(DT.Rows[0]["cfgOTOutLateInterval"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                if (DT.Rows[0]["cfgOTOutOfShiftInterval"] != DBNull.Value) { txtOTOutOfShiftInterval.SetTime(Convert.ToInt32(DT.Rows[0]["cfgOTOutOfShiftInterval"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                if (DT.Rows[0]["cfgOTInVacInterval"] != DBNull.Value) { txtOTVacationInterval.SetTime(Convert.ToInt32(DT.Rows[0]["cfgOTInVacInterval"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }

                calVacResetDate.SetGDate(DT.Rows[0]["cfgVacResetDate"], pgCs.DateFormat);
                
                txtDaysLimitReqVac.Text = DT.Rows[0]["cfgDaysLimitReqVac"].ToString();

                if (DT.Rows[0]["cfgApprovalsMonthCount"] != DBNull.Value) { txtcfgApprovalsMonthCount.Text = DT.Rows[0]["cfgApprovalsMonthCount"].ToString(); }
                if (DT.Rows[0]["cfgTransInDaysCount"]    != DBNull.Value) { txtcfgTransInDaysCount.Text    = DT.Rows[0]["cfgTransInDaysCount"].ToString(); }

                DataTable RDT = DBCs.FetchData(new SqlCommand(" SELECT RetID,RetNameEn,RetNameAr FROM RequestType ORDER BY RetOrder "));
                if (!DBCs.IsNullOrEmpty(RDT))
                {
                    cblFormRequest.DataSource = RDT;
                    cblFormRequest.DataTextField = General.Msg("RetNameEn", "RetNameAr");
                    cblFormRequest.DataValueField = "RetID";
                    cblFormRequest.DataBind();
                }

                try
                {
                    string FormReq = DT.Rows[0]["cfgFormReq"].ToString();
                    string[] ArrFormReq = FormReq.Split(',');
                    for (int i = 0; i < ArrFormReq.Length; i++)
                    {
                        int index = cblFormRequest.Items.IndexOf(cblFormRequest.Items.FindByValue(ArrFormReq[i]));
                        if (index > -1) { cblFormRequest.Items[index].Selected = true; } else { cblFormRequest.Items[index].Selected = false; }
                    }
                }
                catch (Exception ex)
                {
                    for (int i = 0; i < cblFormRequest.Items.Count; i++) { cblFormRequest.Items[i].Selected = false; }
                }

                if (DT.Rows[0]["cfgSessionDuration"] != DBNull.Value) { txtSessionDuration.SetTime(Convert.ToInt32(DT.Rows[0]["cfgSessionDuration"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
                //txtDepartmentLevels.Text = dr["cfgDepartmentLevels"].ToString();
                //chkActiveDirectoryValidationUser.Checked = Convert.ToBoolean(dr["cfgActiveDirectoryValidationUser"]);
                //chkActiveDirectoryValidationEmployee.Checked = Convert.ToBoolean(dr["cfgActiveDirectoryValidationEmployee"]);
                rdlDataLang.SelectedIndex = rdlDataLang.Items.IndexOf(rdlDataLang.Items.FindByValue(DT.Rows[0]["cfgDataLang"].ToString()));
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
        ddlcfgOrderTransType.Enabled = pStatus;
        chkAutoIn.Enabled = pStatus;
        chkIsTakeAutoIn.Enabled = pStatus;
        chkAutoOut.Enabled = pStatus;
        chkIsTakeAutoOut.Enabled = pStatus;
        txtMiddleGapCount.Enabled = pStatus;
        txtICApprovalPercent.Enabled = pStatus;
        ddlRedundantInSelection.Enabled = pStatus;
        ddlRedundantOutSelection.Enabled = pStatus;
        
        txtMaxPercOT.Enabled = pStatus;
        chkOTBeginEarlyFlag.Enabled = pStatus;
        chkOTNoShiftFlag.Enabled = pStatus;
        chkOTOutLateFlag.Enabled = pStatus;
        chkOTOutOfShiftFlag.Enabled = pStatus;
        chkOTVacationFlag.Enabled = pStatus;
        txtOTBeginEarlyInterval.Enabled = pStatus;
        txtOTNoShiftInterval.Enabled = pStatus;
        txtOTOutLateInterval.Enabled = pStatus;
        txtOTOutOfShiftInterval.Enabled = pStatus;
        txtOTVacationInterval.Enabled = pStatus;
        cblFormRequest.Enabled = pStatus;

        txtSessionDuration.Enabled = pStatus;
        rdlDataLang.Enabled = pStatus;

        calVacResetDate.SetEnabled(pStatus);
        txtcfgApprovalsMonthCount.Enabled = pStatus;
        txtcfgTransInDaysCount.Enabled = pStatus;

        //limitDayToReqVac
        txtDaysLimitReqVac.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ProCs.cfgOrderTransType = ddlcfgOrderTransType.SelectedValue;
            ProCs.cfgAutoIn = chkAutoIn.Checked;
            ProCs.cfgIsTakeAutoIn = chkIsTakeAutoIn.Checked;
            ProCs.cfgAutoOut = chkAutoOut.Checked;
            ProCs.cfgIsTakeAutoOut = chkIsTakeAutoOut.Checked;
            ProCs.cfgMiddleGapCount = txtMiddleGapCount.Text;
            ProCs.cfgICApprovalPercent = txtICApprovalPercent.Text;
            ProCs.cfgRedundantInSelection = ddlRedundantInSelection.SelectedValue;
            ProCs.cfgRedundantOutSelection = ddlRedundantOutSelection.SelectedValue;

            if (txtMaxPercOT.getTimeInSecond() == -1) { ProCs.cfgMaxPercOT = null; } else { ProCs.cfgMaxPercOT = txtMaxPercOT.getTimeInSecond().ToString(); }
            ProCs.cfgOTBeginEarlyFlag = Convert.ToBoolean(chkOTBeginEarlyFlag.Checked);
            ProCs.cfgOTNoShiftFlag    = Convert.ToBoolean(chkOTNoShiftFlag.Checked);
            ProCs.cfgOTOutLateFlag    = Convert.ToBoolean(chkOTOutLateFlag.Checked);
            ProCs.cfgOTOutOfShiftFlag = Convert.ToBoolean(chkOTOutOfShiftFlag.Checked);
            ProCs.cfgOTInVacFlag      = Convert.ToBoolean(chkOTVacationFlag.Checked);

            if (txtOTBeginEarlyInterval.getTimeInSecond() == -1) { ProCs.cfgOTBeginEarlyInterval = null; } else { ProCs.cfgOTBeginEarlyInterval = txtOTBeginEarlyInterval.getTimeInSecond().ToString(); }
            if (txtOTNoShiftInterval.getTimeInSecond() == -1) { ProCs.cfgOTNoShiftInterval = null; } else { ProCs.cfgOTNoShiftInterval = txtOTNoShiftInterval.getTimeInSecond().ToString(); }
            if (txtOTOutLateInterval.getTimeInSecond() == -1) { ProCs.cfgOTOutLateInterval = null; } else { ProCs.cfgOTOutLateInterval = txtOTOutLateInterval.getTimeInSecond().ToString(); }
            if (txtOTOutOfShiftInterval.getTimeInSecond() == -1) { ProCs.cfgOTOutOfShiftInterval = null; } else { ProCs.cfgOTOutOfShiftInterval = txtOTOutOfShiftInterval.getTimeInSecond().ToString(); }
            if (txtOTVacationInterval.getTimeInSecond() == -1) { ProCs.cfgOTInVacInterval = null; } else { ProCs.cfgOTInVacInterval = txtOTVacationInterval.getTimeInSecond().ToString(); }

            ProCs.cfgVacResetDate = calVacResetDate.getGDateDBFormat();

            if (string.IsNullOrEmpty(txtDaysLimitReqVac.Text)) { ProCs.cfgDaysLimitReqVac = "0"; } else { ProCs.cfgDaysLimitReqVac = txtDaysLimitReqVac.Text; }

            string FormReq = "";
            for (int i = 0; i < cblFormRequest.Items.Count; i++)
            {
                if (cblFormRequest.Items[i].Selected) { if (string.IsNullOrEmpty(FormReq)) {  FormReq += cblFormRequest.Items[i].Value; }  else { FormReq += "," + cblFormRequest.Items[i].Value; } }
            }
            ProCs.cfgFormReq = FormReq;

            if (txtSessionDuration.getTimeInSecond() == -1) { ProCs.cfgSessionDuration = null; } else { ProCs.cfgSessionDuration = txtSessionDuration.getTimeInSecond().ToString(); }

            if (!string.IsNullOrEmpty(txtcfgApprovalsMonthCount.Text)) { ProCs.cfgApprovalsMonthCount = txtcfgApprovalsMonthCount.Text; }
            if (!string.IsNullOrEmpty(txtcfgTransInDaysCount.Text))    { ProCs.cfgTransInDaysCount    = txtcfgTransInDaysCount.Text; }

            if (rdlDataLang.SelectedIndex == -1) { ProCs.cfgDataLang = "E"; } else { ProCs.cfgDataLang = rdlDataLang.SelectedValue.ToString(); }

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
            ddlcfgOrderTransType.SelectedIndex = 0;
            chkAutoIn.Checked = false;
            chkIsTakeAutoIn.Checked = false;
            chkIsTakeAutoOut.Checked = false;
            chkAutoOut.Checked = false;
            txtMiddleGapCount.Text = "0";
            txtICApprovalPercent.Text = "0";
            ddlRedundantInSelection.SelectedIndex = 0;
            ddlRedundantOutSelection.SelectedIndex = 1;

            txtMaxPercOT.ClearTime();
            chkOTBeginEarlyFlag.Checked = false;
            chkOTNoShiftFlag.Checked = false;
            chkOTOutLateFlag.Checked = false;
            chkOTOutOfShiftFlag.Checked = false;
            chkOTVacationFlag.Checked = false;

            txtOTBeginEarlyInterval.ClearTime();
            txtOTNoShiftInterval.ClearTime();
            txtOTOutLateInterval.ClearTime();
            txtOTOutOfShiftInterval.ClearTime();
            txtOTVacationInterval.ClearTime();

            calVacResetDate.ClearDate();
            txtDaysLimitReqVac.Text = "0";

            for (int i = 0; i < cblFormRequest.Items.Count; i++) { cblFormRequest.Items[i].Selected = false; }
 
            txtSessionDuration.SetTime(10, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
            rdlDataLang.SelectedIndex = 0;

            txtcfgApprovalsMonthCount.Text = "";
            txtcfgTransInDaysCount.Text    = "";
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAutoIn_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAutoIn.Checked)
        {
            chkIsTakeAutoIn.Enabled = true;
        }
        else
        {
            chkIsTakeAutoIn.Enabled = false;
            chkIsTakeAutoIn.Checked = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAutoOut_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAutoOut.Checked)
        {
            chkIsTakeAutoOut.Enabled = true;
        }
        else
        {
            chkIsTakeAutoOut.Enabled = false;
            chkIsTakeAutoOut.Checked = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlcfgOrderTransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcfgOrderTransType.SelectedValue == "FILO") { PropertiesOrderTrans(false); } else { PropertiesOrderTrans(true); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void PropertiesOrderTrans(bool pStatus)
    {
        chkIsTakeAutoIn.Enabled = pStatus;
        chkIsTakeAutoOut.Enabled = pStatus;
        ddlRedundantInSelection.Enabled = pStatus;
        ddlRedundantOutSelection.Enabled = pStatus;

        if (!pStatus)
        {
            chkIsTakeAutoIn.Checked = pStatus;
            chkIsTakeAutoOut.Checked = pStatus;

            ddlRedundantInSelection.SelectedIndex = ddlRedundantInSelection.Items.IndexOf(ddlRedundantInSelection.Items.FindByText("First"));
            ddlRedundantInSelection.SelectedIndex = ddlRedundantInSelection.Items.IndexOf(ddlRedundantInSelection.Items.FindByText("Last"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            UIEnabled(true);
            if (chkAutoIn.Checked) { chkIsTakeAutoIn.Enabled = true; } else { chkIsTakeAutoIn.Enabled = false; }
            if (chkAutoOut.Checked) { chkIsTakeAutoOut.Enabled = true; } else { chkIsTakeAutoOut.Enabled = false; }
            BtnStatus("0011");
            ViewState["CommandName"] = "EDIT";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
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

            BtnStatus("0100");
            UIEnabled(false);
            
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BtnStatus("0100");
        UIEnabled(false);
        PopulateUI();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
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