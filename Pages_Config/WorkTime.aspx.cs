using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Threading;
using System.Data.SqlClient;

public partial class WorkTime : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    WorkTimePro ProCs = new WorkTimePro();
    WorkTimeSql SqlCs = new WorkTimeSql();
    ////SendEmail   EmailCs = new SendEmail();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();

    Thread _SendEmailThread = null;
    string WkID = string.Empty;
    string MainQuery = "SELECT * FROM WorkingTime, WorkType WHERE WorkingTime.WtpID = WorkType.WtpID AND ISNULL(WorkingTime.WktDeleted,0) = 0 AND WorkType.WtpInitial <> 'RO' ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            for (int i = 1; i <= 4; i++)
            {
                AjaxControlToolkit.AnimationExtender aniExtShow  = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderShow" + i.ToString());
                AjaxControlToolkit.AnimationExtender aniExtClose = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderClose" + i.ToString());
                ImageButton lnkShow = (ImageButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("lnkShow" + i.ToString());
                if (aniExtShow != null) { CtrlCs.Animation(ref aniExtShow, ref aniExtClose, ref lnkShow, i.ToString()); }
            }
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("1000");
                UIEnabled(false, false, false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                if (pgCs.Lang == "AR") { lblShift1Duration.Text = lblShift2Duration.Text = lblShift3Duration.Text = "المدة :"; }
                else { lblShift1Duration.Text = lblShift2Duration.Text = lblShift3Duration.Text = "Duration :"; }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillList()
    {
        try
        {
            CtrlCs.FillWorkTypeList(ref ddlWtpID, rfvWtpID, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int GetSeconds(string txtHours, string txtSeconds)
    {
        string Hours = "0";
        if (!string.IsNullOrEmpty(txtHours.Trim())) { Hours = txtHours.Trim(); }
        string Minute = "0";
        if (!string.IsNullOrEmpty(txtSeconds.Trim())) { Minute = txtSeconds.Trim(); }
        return ((Int32.Parse(txtHours) * 60) * 60) + (Int32.Parse(txtSeconds) * 60);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0)
        {
            UIClear();
            sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1 ";
            cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false,false,false);
        grdData.SelectedIndex = -1;
        cmd.CommandText = sql;
        FillGrid(cmd);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UILang()
    {
        if (pgCs.LangEn)
        {
            spnNameEn.Visible = true;
            spnWktInitialEn.Visible = true;
            spnShift1NameEn.Visible = true;
            spnShift2NameEn.Visible = true;
            spnShift3NameEn.Visible = true;
        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[4].Visible = false;
            ddlFilter.Items.FindByValue("WktNameEn").Enabled = false;
            ddlFilter.Items.FindByValue("WtpNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            spnNameAr.Visible = true;
            spnWktInitialAr.Visible = true;
            spnShift1NameAr.Visible = true;
            spnShift2NameAr.Visible = true;
            spnShift3NameAr.Visible = true;
        }
        else
        {
            grdData.Columns[0].Visible = false;
            grdData.Columns[3].Visible = false;
            ddlFilter.Items.FindByValue("WktNameAr").Enabled = false;
            ddlFilter.Items.FindByValue("WtpNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus, bool pShift2, bool pShift3)
    {
        txtID.Enabled = txtID.Visible = false;
        
        txtNameAr.Enabled         = pStatus;
        txtNameEn.Enabled         = pStatus;
        txtWktInitialAr.Enabled   = pStatus;
        txtWktInitialEn.Enabled   = pStatus;
        txtWorkTimeDesc.Enabled   = pStatus;
        ddlWtpID.Enabled          = pStatus;
        txtMaxPercOT.Enabled      = pStatus;
        chkWrtStatus.Enabled      = pStatus;
        chkShift2Setting.Enabled  = pStatus;
        chkShift3Setting.Enabled  = pStatus;

        //shift 1 info
        txtArShift1Name.Enabled = pStatus;
        txtEnShift1Name.Enabled = pStatus;
        tpShift1From.Enabled = pStatus;
        tpShift1To.Enabled = pStatus;
        txtShift1GraceMin.Enabled = pStatus;
        txtShift1MidGraceMin.Enabled = pStatus;
        txtShift1EndGrace.Enabled = pStatus;
        txtShift1Duration.Enabled = pStatus;
        ////txtShift1DurationMin.Enabled = pStatus;
        chkWktShift1IsOverNight.Enabled = pStatus;
        //txtWktShift1AddPercentOverNight.Enabled = pStatus;
        chkWktShift1IsOptional.Enabled = pStatus;
        
        //shift 2 info
        txtArShift2Name.Enabled = pShift2;
        txtEnShift2Name.Enabled = pShift2;
        tpShift2From.Enabled = pShift2;
        tpShift2To.Enabled = pShift2;
        txtShift2GraceMin.Enabled = pShift2;
        txtShift2MidGraceMin.Enabled = pShift2;
        txtShift2EndGrace.Enabled = pShift2;
        txtShift2Duration.Enabled = pShift2;
        chkWktShift2IsOverNight.Enabled = pShift2;
        //txtWktShift2AddPercentOverNight.Enabled = pShift2;
        chkWktShift2IsOptional.Enabled = pShift2;

        //shift 3 info
        txtArShift3Name.Enabled = pShift3;
        txtEnShift3Name.Enabled = pShift3;
        tpShift3From.Enabled = pShift3;
        tpShift3To.Enabled = pShift3;
        txtShift3GraceMin.Enabled = pShift3;
        txtShift3MidGraceMin.Enabled = pShift3;
        txtShift3EndGrace.Enabled = pShift3;
        txtShift3Duration.Enabled = pShift3;
        chkWktShift3IsOverNight.Enabled = pShift3;
        //txtWktShift3AddPercentOverNight.Enabled = pShift3;
        chkWktShift3IsOptional.Enabled = pShift3;

        ChangeShiftUI(FindWorktimeType(), true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIModifyEnabled(bool pStatus, bool pShift2, bool pShift3)
    {
        txtID.Enabled = txtID.Visible = false;

        txtNameAr.Enabled = pStatus;
        txtNameEn.Enabled = pStatus;
        txtNameAr.Enabled = pStatus;
        txtNameEn.Enabled = pStatus;
        txtWorkTimeDesc.Enabled = pStatus;
        ddlWtpID.Enabled = false;
        txtMaxPercOT.Enabled = false;
        chkWrtStatus.Enabled = pStatus;
        chkShift2Setting.Enabled = false;
        chkShift3Setting.Enabled = false;

        //shift 1 info
        txtArShift1Name.Enabled = pStatus;
        txtEnShift1Name.Enabled = pStatus;
        tpShift1From.Enabled = false;
        tpShift1To.Enabled = false;
        txtShift1GraceMin.Enabled = false;
        txtShift1MidGraceMin.Enabled = false;
        txtShift1EndGrace.Enabled = false;
        txtShift1Duration.Enabled = false;
        chkWktShift1IsOverNight.Enabled = false;
        //txtWktShift1AddPercentOverNight.Enabled = pStatus;
        chkWktShift1IsOptional.Enabled = false;

        //shift 2 info
        txtArShift2Name.Enabled = pShift2;
        txtEnShift2Name.Enabled = pShift2;
        tpShift2From.Enabled = false;
        tpShift2To.Enabled = false;
        txtShift2GraceMin.Enabled = false;
        txtShift2MidGraceMin.Enabled = false;
        txtShift2EndGrace.Enabled = false;
        txtShift2Duration.Enabled = false;
        chkWktShift2IsOverNight.Enabled = false;
        //txtWktShift2AddPercentOverNight.Enabled = pShift2;
        chkWktShift2IsOptional.Enabled = false;

        //shift 3 info
        txtArShift3Name.Enabled = pShift3;
        txtEnShift3Name.Enabled = pShift3;
        tpShift3From.Enabled = false;
        tpShift3To.Enabled = false;
        txtShift3GraceMin.Enabled = false;
        txtShift3MidGraceMin.Enabled = false;
        txtShift3EndGrace.Enabled = false;
        txtShift3Duration.Enabled = false;
        chkWktShift3IsOverNight.Enabled = false;
        //txtWktShift3AddPercentOverNight.Enabled = pShift3;
        chkWktShift3IsOptional.Enabled = false;

        ChangeShiftUI(FindWorktimeType(), true);
        btnCalShift1Duration.Enabled = false;
        btnCalShift2Duration.Enabled = false;
        btnCalShift3Duration.Enabled = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.WktID = txtID.Text; }
            ProCs.WktNameAr      = txtNameAr.Text;
            ProCs.WktNameEn      = txtNameEn.Text;
            ProCs.WktInitialAr   = txtWktInitialAr.Text;   
            ProCs.WktInitialEn   = txtWktInitialEn.Text;

            ProCs.WktDesc        = txtWorkTimeDesc.Text;
            int AddPercent    = txtMaxPercOT.getTimeInSecond(); /****/ ProCs.WktAddPercent = (AddPercent > 0) ? AddPercent.ToString() : "0";
            ProCs.WktIsActive    = chkWrtStatus.Checked;
            if (ddlWtpID.SelectedIndex > 0) { ProCs.WtpID = ddlWtpID.SelectedValue; }
            ProCs.WktShiftCount = FindShiftCount().ToString();

             //*** Shift 1 info ***//
            if (!string.IsNullOrEmpty(txtArShift1Name.Text)) { ProCs.WktShift1NameAr = txtArShift1Name.Text; }
            if (!string.IsNullOrEmpty(txtEnShift1Name.Text)) { ProCs.WktShift1NameEn = txtEnShift1Name.Text; }
            
            ProCs.WktShift1From     = tpShift1From.getDateTime().ToString();
            ProCs.WktShift1To       = tpShift1To.getDateTime().ToString();
            ProCs.WktShift1Duration = txtShift1Duration.getTimeInSecond().ToString();

            int sGraceTime1 = txtShift1GraceMin.getTimeInSecond();    /****/ ProCs.WktShift1Grace       = (sGraceTime1 > 0) ? sGraceTime1.ToString() :"0";
            int mGraceTime1 = txtShift1MidGraceMin.getTimeInSecond(); /****/ ProCs.WktShift1MiddleGrace = (mGraceTime1 > 0) ? mGraceTime1.ToString() :"0";
            int eGraceTime1 = txtShift1EndGrace.getTimeInSecond();    /****/ ProCs.WktShift1EndGrace    = (eGraceTime1 > 0) ? eGraceTime1.ToString() : "0";
            if (divFT1.Visible) { int FTHours1 = txtWktShift1FTHours.getTimeInSecond(); /****/ ProCs.WktShift1FTHours = (FTHours1 > 0) ? FTHours1.ToString() : "0"; }

            ProCs.WktShift1IsOverNight = chkWktShift1IsOverNight.Checked;
            ProCs.WktShift1IsOptional  = chkWktShift1IsOptional.Checked;

            //*** Shift 2 info ***//
            if (chkShift2Setting.Checked)
            {
                if (!string.IsNullOrEmpty(txtArShift2Name.Text)) { ProCs.WktShift2NameAr = txtArShift2Name.Text; }
                if (!string.IsNullOrEmpty(txtEnShift2Name.Text)) { ProCs.WktShift2NameEn = txtEnShift2Name.Text; }

                if (tpShift2From.getIntTime() >= 0) { ProCs.WktShift2From = tpShift2From.getDateTime().ToString(); }
                if (tpShift2To.getIntTime()   >= 0) { ProCs.WktShift2To   = tpShift2To.getDateTime().ToString(); }

                int sGraceTime2 = txtShift2GraceMin.getTimeInSecond();    /****/ ProCs.WktShift2Grace       = (sGraceTime2 > 0) ? sGraceTime2.ToString() : "0";
                int mGraceTime2 = txtShift2MidGraceMin.getTimeInSecond(); /****/ ProCs.WktShift2MiddleGrace = (mGraceTime2 > 0) ? mGraceTime2.ToString() : "0";
                int eGraceTime2 = txtShift2EndGrace.getTimeInSecond();    /****/ ProCs.WktShift2EndGrace    = (eGraceTime2 > 0) ? eGraceTime2.ToString() : "0";
                int Duration2   = txtShift2Duration.getTimeInSecond();    /****/ ProCs.WktShift2Duration    = (Duration2   > 0) ? Duration2.ToString()   : "0";
                if (divFT2.Visible) { int FTHours2 = txtWktShift2FTHours.getTimeInSecond(); /****/ ProCs.WktShift2FTHours = (FTHours2 > 0) ? FTHours2.ToString() : "0"; }

                ProCs.WktShift2IsOverNight = chkWktShift2IsOverNight.Checked;
                ProCs.WktShift2IsOptional  = chkWktShift2IsOptional.Checked;
            }

            //*** Shift 3 info ***//
            if (chkShift3Setting.Checked)
            {
                if (!string.IsNullOrEmpty(txtArShift3Name.Text)) { ProCs.WktShift3NameAr = txtArShift3Name.Text; }
                if (!string.IsNullOrEmpty(txtEnShift3Name.Text)) { ProCs.WktShift3NameEn = txtEnShift3Name.Text; }

                if (tpShift3From.getIntTime() >= 0) { ProCs.WktShift3From = tpShift3From.getDateTime().ToString(); }
                if (tpShift3To.getIntTime()   >= 0) { ProCs.WktShift3To   = tpShift3To.getDateTime().ToString(); }

                int sGraceTime3 = txtShift3GraceMin.getTimeInSecond();    /****/ ProCs.WktShift3Grace       = (sGraceTime3 > 0) ? sGraceTime3.ToString() : "0";
                int mGraceTime3 = txtShift3MidGraceMin.getTimeInSecond(); /****/ ProCs.WktShift3MiddleGrace = (mGraceTime3 > 0) ? mGraceTime3.ToString() : "0";
                int eGraceTime3 = txtShift3EndGrace.getTimeInSecond();    /****/ ProCs.WktShift3EndGrace    = (eGraceTime3 > 0) ? eGraceTime3.ToString() : "0";
                int Duration3   = txtShift3Duration.getTimeInSecond();    /****/ ProCs.WktShift3Duration    = (Duration3   > 0) ? Duration3.ToString()   : "0";
                if (divFT3.Visible) { int FTHours3 = txtWktShift3FTHours.getTimeInSecond(); /****/ ProCs.WktShift3FTHours = (FTHours3 > 0) ? FTHours3.ToString() : "0"; }

                ProCs.WktShift3IsOverNight = chkWktShift3IsOverNight.Checked;
                ProCs.WktShift3IsOptional  = chkWktShift3IsOptional.Checked;
            }

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        txtID.Text = "";
        ViewState["CommandName"] = "";

        txtNameAr.Text = "";
        txtNameEn.Text = "";
        txtWktInitialAr.Text = "";   
        txtWktInitialEn.Text = "";
        txtWorkTimeDesc.Text = "";
        ddlWtpID.SelectedIndex = 0;

        //shift 1 info
        txtArShift1Name.Text = "";
        txtEnShift1Name.Text = "";
        tpShift1From.ClearTime();
        tpShift1To.ClearTime();

        txtShift1GraceMin.ClearTime();
        txtShift1MidGraceMin.ClearTime();
        txtShift1EndGrace.ClearTime();
        txtShift1Duration.ClearTime();
        txtWktShift1FTHours.ClearTime();

        chkWktShift1IsOverNight.Checked = false;
        chkWktShift1IsOptional.Checked = false;

        //shift 2 info  
        txtArShift2Name.Text = "";
        txtEnShift2Name.Text = "";
        tpShift2From.ClearTime();
        tpShift2To.ClearTime();
        txtShift2GraceMin.ClearTime();
        txtShift2MidGraceMin.ClearTime();
        txtShift2EndGrace.ClearTime();
        txtShift2Duration.ClearTime();
        txtWktShift2FTHours.ClearTime();
        chkWktShift2IsOverNight.Checked = false;
        chkWktShift2IsOptional.Checked = false;

        //shift 3 info
        txtArShift3Name.Text = "";
        txtEnShift3Name.Text = "";
        tpShift3From.ClearTime();
        tpShift3To.ClearTime();
        txtShift3GraceMin.ClearTime();
        txtShift3MidGraceMin.ClearTime();
        txtShift3EndGrace.ClearTime();
        txtShift3Duration.ClearTime();
        txtWktShift3FTHours.ClearTime();
        chkWktShift3IsOverNight.Checked = false;
        chkWktShift3IsOptional.Checked = false;

        txtMaxPercOT.ClearTime();
        chkWrtStatus.Checked = false;

        lblShift1Duration.Text = lblShift2Duration.Text = lblShift3Duration.Text = General.Msg("Duration :","المدة :");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkShift2Setting_CheckedChanged(object sender, EventArgs e)
    {
        UIEnabled(true, chkShift2Setting.Checked, chkShift3Setting.Checked);
        if (!chkShift2Setting.Checked)
        {
            //shift 2 info  
            txtArShift2Name.Text = "";
            txtEnShift2Name.Text = "";
            tpShift2From.ClearTime();
            tpShift2To.ClearTime();
            txtShift2Duration.ClearTime();
            txtWktShift2FTHours.ClearTime();
            txtShift2GraceMin.ClearTime();
            txtShift2MidGraceMin.ClearTime();
            txtShift2EndGrace.ClearTime();
            chkWktShift2IsOverNight.Checked = false;
            chkWktShift2IsOptional.Checked  = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkShift3Setting_CheckedChanged(object sender, EventArgs e)
    {
        UIEnabled(true, chkShift2Setting.Checked, chkShift3Setting.Checked);
        if (!chkShift2Setting.Checked)
        {
            txtArShift3Name.Text = "";
            txtEnShift3Name.Text = "";
            tpShift3From.ClearTime();
            tpShift3To.ClearTime();
            txtShift3Duration.ClearTime();
            txtWktShift3FTHours.ClearTime();
            txtShift3GraceMin.ClearTime();
            txtShift3MidGraceMin.ClearTime();
            txtShift3EndGrace.ClearTime();
            chkWktShift3IsOverNight.Checked = false;
            chkWktShift3IsOptional.Checked = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlWtpID_SelectedIndexChanged(object sender, EventArgs e) { ChangeShiftUI(FindWorktimeType(), true); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string FindWorktimeType()
    {
        if (ddlWtpID.SelectedIndex > 0)
        {
            DataTable DT = DBCs.FetchData(" SELECT WtpInitial FROM WorkType WHERE WtpID = @P1 ", new string[] { ddlWtpID.SelectedValue });
            if (!DBCs.IsNullOrEmpty(DT)) { return DT.Rows[0]["WtpInitial"].ToString(); }
        }
        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ChangeShiftUI(string WKType, bool status)
    {
        string Titel = General.Msg("Duration :","المدة :");
        
        txtShift1Duration.Enabled = false; btnCalShift1Duration.Enabled = false;
        txtShift2Duration.Enabled = false; btnCalShift2Duration.Enabled = false;
        txtShift3Duration.Enabled = false; btnCalShift3Duration.Enabled = false;

        divFT1.Visible              = divFT2.Visible              = divFT3.Visible              = false;
        txtWktShift1FTHours.Enabled = txtWktShift2FTHours.Enabled = txtWktShift3FTHours.Enabled = false; 
        cvWktShift1FTHours.Enabled  = cvWktShift2FTHours.Enabled  = cvWktShift3FTHours.Enabled  = false;

        if (!string.IsNullOrEmpty(WKType))
        {
            if (WKType == "RE")
            {
                txtShift1Duration.Enabled    = false; 
                btnCalShift1Duration.Enabled = status;
                if (chkShift2Setting.Checked) { txtShift2Duration.Enabled = false; btnCalShift2Duration.Enabled = status; }
                if (chkShift3Setting.Checked) { txtShift3Duration.Enabled = false; btnCalShift3Duration.Enabled = status; }
            }
            else if (WKType == "FX")
            {
                txtShift1Duration.Enabled    = false; 
                btnCalShift1Duration.Enabled = status;
                divFT1.Visible = cvWktShift1FTHours.Enabled = true;
                txtWktShift1FTHours.Enabled  = status;
                
                if (chkShift2Setting.Checked) 
                { 
                    txtShift2Duration.Enabled = false; 
                    btnCalShift2Duration.Enabled = status; 
                    divFT2.Visible = cvWktShift2FTHours.Enabled = true; 
                    txtWktShift2FTHours.Enabled  = status;
                }
                if (chkShift3Setting.Checked) 
                { 
                    txtShift3Duration.Enabled = false; 
                    btnCalShift3Duration.Enabled = status; 
                    divFT3.Visible = cvWktShift3FTHours.Enabled = true; 
                    txtWktShift3FTHours.Enabled  = status;
                }
            }
            else if (WKType == "HR")
            {
                Titel = General.Msg("Hours (Month) :","عدد الساعات ( شهر ) :");
                txtShift1Duration.Enabled    = status; 
                btnCalShift1Duration.Enabled = false;
                if (chkShift2Setting.Checked) { txtShift2Duration.Enabled = status; btnCalShift2Duration.Enabled = false; }
                if (chkShift3Setting.Checked) { txtShift3Duration.Enabled = status; btnCalShift3Duration.Enabled = false; } 
            }

            lblShift1Duration.Text = lblShift2Duration.Text = lblShift3Duration.Text = Titel;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCalShift1Duration_Click(object sender, EventArgs e)
    {
        if (!CtrlCs.PageIsValid(this, vsCalShift)) { return; }

        string WorktimeType = FindWorktimeType();
        int Duration = 0;
        int FromTime = tpShift1From.getIntTime();
        int ToTime   = tpShift1To.getIntTime();
        
        if (!string.IsNullOrEmpty(WorktimeType))
        {
            if (WorktimeType == "RE" || WorktimeType == "FX") 
            {
                if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                if (ToTime   >= 2400) { ToTime   = ToTime   - 2400; }

                FromTime = Convert.ToInt32(countDigit(FromTime.ToString()).Substring(0, 2)) * 60 + Convert.ToInt32(countDigit(FromTime.ToString()).Substring(2, 2));
                ToTime   = Convert.ToInt32(countDigit(ToTime.ToString()).Substring(0, 2))   * 60 + Convert.ToInt32(countDigit(ToTime.ToString()).Substring(2, 2));

                if (chkWktShift1IsOverNight.Checked)
                {
                    Duration = ( 1440 - FromTime ) + ToTime;
                    txtShift1Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
                else
                {
                    Duration = ToTime - FromTime;
                    txtShift1Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCalShift2Duration_Click(object sender, EventArgs e)
    {
        if (!CtrlCs.PageIsValid(this, vsCalShift)) { return; }

        string WorktimeType = FindWorktimeType();
        int Duration = 0;
        int FromTime = tpShift2From.getIntTime();
        int ToTime   = tpShift2To.getIntTime();

        if (!string.IsNullOrEmpty(WorktimeType))
        {
            if (WorktimeType == "RE" || WorktimeType == "FX")
            {
                if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                if (ToTime   >= 2400) { ToTime   = ToTime   - 2400; }

                FromTime = Convert.ToInt32(countDigit(FromTime.ToString()).Substring(0, 2)) * 60 + Convert.ToInt32(countDigit(FromTime.ToString()).Substring(2, 2));
                ToTime   = Convert.ToInt32(countDigit(ToTime.ToString()).Substring(0, 2))   * 60 + Convert.ToInt32(countDigit(ToTime.ToString()).Substring(2, 2));

                if (chkWktShift2IsOverNight.Checked)
                {
                    Duration = ( 1440 - FromTime ) + ToTime;
                    txtShift2Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
                else
                {
                    Duration = ToTime - FromTime;
                    txtShift2Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCalShift3Duration_Click(object sender, EventArgs e)
    {
        if (!CtrlCs.PageIsValid(this, vsCalShift)) { return; }

        string WorktimeType = FindWorktimeType();
        int Duration = 0;
        int FromTime = tpShift3From.getIntTime();
        int ToTime   = tpShift3To.getIntTime();

        if (!string.IsNullOrEmpty(WorktimeType))
        {
            if (WorktimeType == "RE" || WorktimeType == "FX")
            {
                if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                if (ToTime   >= 2400) { ToTime   = ToTime   - 2400; }

                FromTime = Convert.ToInt32(countDigit(FromTime.ToString()).Substring(0, 2)) * 60 + Convert.ToInt32(countDigit(FromTime.ToString()).Substring(2, 2));
                ToTime   = Convert.ToInt32(countDigit(ToTime.ToString()).Substring(0, 2))   * 60 + Convert.ToInt32(countDigit(ToTime.ToString()).Substring(2, 2));

                if (chkWktShift3IsOverNight.Checked)
                {
                    Duration = ( 1440 - FromTime ) + ToTime;
                    txtShift3Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
                else
                {
                    Duration = ToTime - FromTime;
                    txtShift3Duration.SetTime(Duration, TextTimeServerControl.TextTime.TimeTypeEnum.Minutes);
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string countDigit(string pTime) 
    { 
        if (pTime.Length == 1)      { return "000" + pTime; }
        else if (pTime.Length == 2) { return "00" + pTime; }
        else if (pTime.Length == 3) { return "0"  + pTime; } 
        else { return pTime; } 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected int FindShiftCount() 
    {
        int Count = 1;
        if (chkShift2Setting.Checked) { Count = 2; }
        if (chkShift3Setting.Checked) { Count = 3; }
        
        return Count;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(true, false, false);
            BtnStatus("0011");
            ViewState["CommandName"] = "ADD";
            chkShift2Setting.Checked = false;
            chkShift3Setting.Checked = false;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            //UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "EDIT";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            btnCalShift1Duration_Click(null, null);
            btnCalShift2Duration_Click(null, null);
            btnCalShift3Duration_Click(null, null);
            FillPropeties();

            if (ViewState["CommandName"].ToString() == "ADD") 
            { 
                int wktid = SqlCs.Wkt_Insert(ProCs); 
                WkID = wktid.ToString();
            } 
            else if (ViewState["CommandName"].ToString() == "EDIT") 
            { 
                SqlCs.Wkt_Update(ProCs);
                WkID = ProCs.WktID;
            }

            //_SendEmailThread = new Thread(new ThreadStart(SendWKEmail));
            //_SendEmailThread.Start();

            btnFilter_Click(null,null);
            
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
    ////private void SendWKEmail() { EmailCs.SendWorkTimeToAdmin(ViewState["CommandName"].ToString(), pgCs.LoginID, pgCs.DateType, WkID); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("1000");
        UIEnabled(false, false, false);
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
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager:
                    {
                        DropDownList _ddlPager = CtrlCs.PagerList(grdData);
                        _ddlPager.SelectedIndexChanged += new EventHandler(ddlPager_SelectedIndexChanged);
                        Table pagerTable = e.Row.Cells[0].Controls[0] as Table;
                        pagerTable.Rows[0].Cells.Add(CtrlCs.PagerCell(_ddlPager));
                        break;
                    }
                 default:
                    {
                        e.Row.Cells[1].Visible = false; //To hide ID column in grid view
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdData.PageIndex = 0;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                {
                    ImageButton _btnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                    _btnDelete.Enabled = pgCs.getPermission(ViewState["ht"], "Delete");
                    _btnDelete.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());

                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                    break;
                }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string ID = Convert.ToString(e.CommandArgument);
                    DataTable DT = DBCs.FetchData(" SELECT WktID FROM EmpWrkRel WHERE WktID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }

                    SqlCs.Wkt_Delete(ID, pgCs.LoginID);
                    
                    btnFilter_Click(null,null);
                    
                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        btnFilter_Click(null,null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(false, false, false);
            BtnStatus("1000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
                BtnStatus("1100");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            TextTimeServerControl.TextTime.TimeTypeEnum Seconds = TextTimeServerControl.TextTime.TimeTypeEnum.Seconds;
            chkShift2Setting.Checked = chkShift3Setting.Checked = false;

            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("WktID =" + pID + "");

            txtID.Text     = DRs[0]["WktID"].ToString();
            txtNameAr.Text = Convert.ToString(DRs[0]["WktNameAr"]);
            txtNameEn.Text = Convert.ToString(DRs[0]["WktNameEn"]);

            txtWktInitialAr.Text = Convert.ToString(DRs[0]["WktInitialAr"]);
            txtWktInitialEn.Text = Convert.ToString(DRs[0]["WktInitialEn"]);
            txtWorkTimeDesc.Text   = DRs[0]["WktDesc"].ToString();
            ddlWtpID.SelectedIndex = ddlWtpID.Items.IndexOf(ddlWtpID.Items.FindByValue(DRs[0]["WtpID"].ToString()));
            if (DRs[0]["WktIsActive"]   != DBNull.Value) { chkWrtStatus.Checked = Convert.ToBoolean(DRs[0]["WktIsActive"]); }
            if (DRs[0]["WktAddPercent"] != DBNull.Value) { txtMaxPercOT.SetTime(Convert.ToInt32(DRs[0]["WktAddPercent"]), Seconds); }

           /*** Shift 1 info ***/
            txtArShift1Name.Text = DRs[0]["WktShift1NameAr"].ToString();
            txtEnShift1Name.Text = DRs[0]["WktShift1NameEn"].ToString();

            if (DRs[0]["WktShift1From"]        != DBNull.Value) { tpShift1From.SetTime(Convert.ToDateTime(DRs[0]["WktShift1From"])); }
            if (DRs[0]["WktShift1To"]          != DBNull.Value) { tpShift1To.SetTime(Convert.ToDateTime(DRs[0]["WktShift1To"])); }
            if (DRs[0]["WktShift1Duration"]    != DBNull.Value) { txtShift1Duration.SetTime(Convert.ToInt32(DRs[0]["WktShift1Duration"])      , Seconds); }
            if (DRs[0]["WktShift1FTHours"]     != DBNull.Value) { txtWktShift1FTHours.SetTime(Convert.ToInt32(DRs[0]["WktShift1FTHours"])     , Seconds); }

            if (DRs[0]["WktShift1Grace"]       != DBNull.Value) { txtShift1GraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift1Grace"])         , Seconds); }
            if (DRs[0]["WktShift1MiddleGrace"] != DBNull.Value) { txtShift1MidGraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift1MiddleGrace"]), Seconds); }
            if (DRs[0]["WktShift1EndGrace"]    != DBNull.Value) { txtShift1EndGrace.SetTime(Convert.ToInt32(DRs[0]["WktShift1EndGrace"])      , Seconds); }
            
            if (DRs[0]["WktShift1IsOverNight"] != DBNull.Value) { chkWktShift1IsOverNight.Checked = Convert.ToBoolean(DRs[0]["WktShift1IsOverNight"]); }
            if (DRs[0]["WktShift1IsOptional"]  != DBNull.Value) { chkWktShift1IsOptional.Checked  = Convert.ToBoolean(DRs[0]["WktShift1IsOptional"]); }

            /*** Shift 2 info ***/
            txtArShift2Name.Text = DRs[0]["WktShift2NameAr"].ToString();
            txtEnShift2Name.Text = DRs[0]["WktShift2NameEn"].ToString();
            if (!string.IsNullOrEmpty(txtEnShift2Name.Text) || !string.IsNullOrEmpty(txtArShift2Name.Text) ) { chkShift2Setting.Checked = true; }

            if (chkShift2Setting.Checked)
            { 
                if (DRs[0]["WktShift2From"]        != DBNull.Value) { tpShift2From.SetTime(Convert.ToDateTime(DRs[0]["WktShift2From"])); }
                if (DRs[0]["WktShift2To"]          != DBNull.Value) { tpShift2To.SetTime(Convert.ToDateTime(DRs[0]["WktShift2To"])); }
                if (DRs[0]["WktShift2Duration"]    != DBNull.Value) { txtShift2Duration.SetTime(Convert.ToInt32(DRs[0]["WktShift2Duration"])      , Seconds); }
                if (DRs[0]["WktShift2FTHours"]     != DBNull.Value) { txtWktShift2FTHours.SetTime(Convert.ToInt32(DRs[0]["WktShift2FTHours"])     , Seconds); }

                if (DRs[0]["WktShift2Grace"]       != DBNull.Value) { txtShift2GraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift2Grace"])         , Seconds); }
                if (DRs[0]["WktShift2MiddleGrace"] != DBNull.Value) { txtShift2MidGraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift2MiddleGrace"]), Seconds); }
                if (DRs[0]["WktShift2EndGrace"]    != DBNull.Value) { txtShift2EndGrace.SetTime(Convert.ToInt32(DRs[0]["WktShift2EndGrace"])      , Seconds); }
            
                if (DRs[0]["WktShift2IsOverNight"] != DBNull.Value) { chkWktShift2IsOverNight.Checked = Convert.ToBoolean(DRs[0]["WktShift2IsOverNight"]); }
                if (DRs[0]["WktShift2IsOptional"]  != DBNull.Value) { chkWktShift2IsOptional.Checked  = Convert.ToBoolean(DRs[0]["WktShift2IsOptional"]); }
            }
            /*** Shift 3 info ***/
            txtArShift3Name.Text = DRs[0]["WktShift3NameAr"].ToString();
            txtEnShift3Name.Text = DRs[0]["WktShift3NameEn"].ToString();
            if (!string.IsNullOrEmpty(txtEnShift3Name.Text) || !string.IsNullOrEmpty(txtArShift3Name.Text)) { chkShift3Setting.Checked = true; }
            
            if (chkShift3Setting.Checked)
            { 
                if (DRs[0]["WktShift3From"]        != DBNull.Value) { tpShift3From.SetTime(Convert.ToDateTime(DRs[0]["WktShift3From"])); }
                if (DRs[0]["WktShift3To"]          != DBNull.Value) { tpShift3To.SetTime(Convert.ToDateTime(DRs[0]["WktShift3To"])); }
                if (DRs[0]["WktShift3Duration"]    != DBNull.Value) { txtShift3Duration.SetTime(Convert.ToInt32(DRs[0]["WktShift3Duration"])      , Seconds); }
                if (DRs[0]["WktShift3FTHours"]     != DBNull.Value) { txtWktShift3FTHours.SetTime(Convert.ToInt32(DRs[0]["WktShift3FTHours"])     , Seconds); }

                if (DRs[0]["WktShift3Grace"]       != DBNull.Value) { txtShift3GraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift3Grace"])         , Seconds); }
                if (DRs[0]["WktShift3MiddleGrace"] != DBNull.Value) { txtShift3MidGraceMin.SetTime(Convert.ToInt32(DRs[0]["WktShift3MiddleGrace"]), Seconds); }
                if (DRs[0]["WktShift3EndGrace"]    != DBNull.Value) { txtShift3EndGrace.SetTime(Convert.ToInt32(DRs[0]["WktShift3EndGrace"])      , Seconds); }
            
                if (DRs[0]["WktShift3IsOverNight"] != DBNull.Value) { chkWktShift3IsOverNight.Checked = Convert.ToBoolean(DRs[0]["WktShift3IsOverNight"]); }
                if (DRs[0]["WktShift3IsOptional"]  != DBNull.Value) { chkWktShift3IsOptional.Checked  = Convert.ToBoolean(DRs[0]["WktShift3IsOptional"]); }
            }

            ChangeShiftUI(FindWorktimeType(), false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(SqlCommand cmd)
    {
        DataTable GDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            grdData.DataSource = (DataTable)GDT;
            ViewState["grdDataDT"] = (DataTable)GDT;
            grdData.DataBind();
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }
   
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void WorkTimeValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string NameEn = txtNameEn.Text.Trim();
            string NameAr = txtNameAr.Text.Trim();

            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND WktID != @P2 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(NameEn)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(NameEn))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered Working Time English Name exist already,Please enter another name", "إسم وقت العمل بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM WorkingTime WHERE WktNameEn = @P1 AND ISNULL(WktDeleted,0) = 0 " + UQ, new string[] { NameEn, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, false, General.Msg("Name (Ar) Is Required", "الاسم بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(NameAr)) { e.IsValid = false; }
                }
                if (!string.IsNullOrEmpty(NameAr))
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Working Time Arabic Name exist already,Please enter another name", "إسم وقت العمل بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM WorkingTime WHERE WktNameAr = @P1 AND ISNULL(WktDeleted,0) = 0 " + UQ, new string[] { NameAr, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvInitialEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvInitialEn, false, General.Msg("Initial (En) Is Required", "الرمز بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtWktInitialEn.Text.Trim())) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvInitialAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvInitialAr, false, General.Msg("Initial (Ar) Is Required", "الرمز بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(txtWktInitialAr.Text.Trim())) { e.IsValid = false; }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Shift1Validate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEnShift1Name))
            {
                if (pgCs.LangEn) { if (string.IsNullOrEmpty(txtEnShift1Name.Text.Trim())) { e.IsValid = false; } }
            }
            else if (source.Equals(cvArShift1Name))
            {
                if (pgCs.LangAr) { if (string.IsNullOrEmpty(txtArShift1Name.Text.Trim())) { e.IsValid = false; } }
            }
            else if (source.Equals(cvShift1Duration))
            {
                if ( txtShift1Duration.getTimeInSecond() <= 0 ) { e.IsValid = false; }
            }
            else if (source.Equals(cvShift1Time) || source.Equals(cvShift1Cal) )
            {
                if (tpShift1From.getIntTime() >= 0 && tpShift1To.getIntTime() >= 0)
                {
                    int FromTime = tpShift1From.getIntTime();
                    int ToTime   = tpShift1To.getIntTime();

                    if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                    if (ToTime >= 2400) { ToTime = ToTime - 2400; }

                    if (chkWktShift1IsOverNight.Checked) { if (FromTime > ToTime) { e.IsValid = true; } else { e.IsValid = false; } }
                    else { if (ToTime > FromTime) { e.IsValid = true; } else { e.IsValid = false; } }
                }
                else { e.IsValid = false; }
            }
            else if (source.Equals(cvShift1Overnight))
            {
                if (chkWktShift1IsOverNight.Checked)
                {
                    if (chkShift2Setting.Checked || chkShift3Setting.Checked) { e.IsValid = false; } else { e.IsValid = true; }
                }
            }
            else if (source.Equals(cvWktShift1FTHours))
            {
                if ( txtWktShift1FTHours.getTimeInSecond() <= 0 ) { e.IsValid = false; } else { e.IsValid = true; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Shift2Validate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (chkShift2Setting.Checked)
            {
                if (source.Equals(cvEnShift2Name))
                {
                    if (pgCs.LangEn) { if (string.IsNullOrEmpty(txtEnShift2Name.Text.Trim())) { e.IsValid = false; } }
                }
                else if (source.Equals(cvArShift2Name))
                {
                    if (pgCs.LangAr) { if (string.IsNullOrEmpty(txtArShift2Name.Text.Trim())) { e.IsValid = false; } }
                }
                else if (source.Equals(cvShift2Duration))
                {
                    if (txtShift2Duration.getTimeInSecond() <= 0) { e.IsValid = false; } else { e.IsValid = true; }
                }
                else if (source.Equals(cvShift2Time) ||  source.Equals(cvShift2Cal))
                {
                    if (tpShift2From.getIntTime() >= 0 && tpShift2To.getIntTime() >= 0)
                    {
                        int FromTime = tpShift2From.getIntTime();
                        int ToTime = tpShift2To.getIntTime();

                        if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                        if (ToTime >= 2400) { ToTime = ToTime - 2400; }

                        if (chkWktShift2IsOverNight.Checked) { if (FromTime > ToTime) { e.IsValid = true; } else { e.IsValid = false; } }
                        else { if (ToTime > FromTime) { e.IsValid = true; } else { e.IsValid = false; } }
                    }
                    else { e.IsValid = false; }
                }
                else if (source.Equals(cvShift2FromRequired))
                {
                    if (tpShift2From.getIntTime() >= 0 ) { e.IsValid = true; } else { e.IsValid = false; }
                }
                else if (source.Equals(cvShift2ToRequired))
                {
                    if (tpShift2To.getIntTime() >= 0 ) { e.IsValid = true; } else { e.IsValid = false; }
                }
                else if (source.Equals(cvWktShift2FTHours))
                {
                    if (txtWktShift2FTHours.getTimeInSecond() <= 0) { e.IsValid = false; } else { e.IsValid = true; }
                }
                else if (source.Equals(cvOrderShfit2))
                {
                    if (chkShift2Setting.Checked)
                    {
                        if (tpShift1To.getIntTime() >= 0 && tpShift2From.getIntTime() >= 0)
                        {
                            int ToTime1 = tpShift1To.getIntTime();
                            int FromTime2 = tpShift2From.getIntTime();

                            if (ToTime1 > FromTime2) { e.IsValid = false; } else { e.IsValid = true; }
                        }
                    }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Shift3Validate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (chkShift3Setting.Checked)
            {
                if (source.Equals(cvEnShift3Name))
                {
                    if (pgCs.LangEn) { if (string.IsNullOrEmpty(txtEnShift3Name.Text.Trim())) { e.IsValid = false; } }
                }
                else if (source.Equals(cvArShift3Name))
                {
                    if (pgCs.LangAr) { if (string.IsNullOrEmpty(txtArShift3Name.Text.Trim())) { e.IsValid = false; } }
                }
                else if (source.Equals(cvShift3Duration))
                {
                    if (txtShift3Duration.getTimeInSecond() <= 0) { e.IsValid = false; } else { e.IsValid = true; }
                }
                else if (source.Equals(cvShift3Time) ||  source.Equals(cvShift3Cal))
                {
                    if (tpShift3From.getIntTime() >= 0 && tpShift3To.getIntTime() >= 0)
                    {
                        int FromTime = tpShift3From.getIntTime();
                        int ToTime = tpShift3To.getIntTime();

                        if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                        if (ToTime >= 2400) { ToTime = ToTime - 2400; }

                        if (chkWktShift3IsOverNight.Checked) { if (FromTime > ToTime) { e.IsValid = true; } else { e.IsValid = false; } }
                        else { if (ToTime > FromTime) { e.IsValid = true; } else { e.IsValid = false; } }
                    }
                    else { e.IsValid = false; }
                }
                else if (source.Equals(cvShift3FromRequired))
                {
                    if (tpShift3From.getIntTime() >= 0 ) { e.IsValid = true; } else { e.IsValid = false; }
                }
                else if (source.Equals(cvShift3ToRequired))
                {
                    if (tpShift3To.getIntTime() >= 0 ) { e.IsValid = true; } else { e.IsValid = false; }
                }
                else if (source.Equals(cvWktShift3FTHours))
                {
                    if (txtWktShift3FTHours.getTimeInSecond() <= 0) { e.IsValid = false; } else { e.IsValid = true; }
                }
                else if (source.Equals(cvOrderShfit3))
                {
                    if (chkShift3Setting.Checked)
                    {
                        if (tpShift1To.getIntTime() >= 0 && tpShift1To.getIntTime() >= 0 && tpShift3From.getIntTime() >= 0)
                        {
                            int ToTime1 = tpShift1To.getIntTime();
                            int FromTime2 = tpShift2From.getIntTime();
                            int ToTime2 = tpShift2To.getIntTime();
                            int FromTime3 = tpShift3From.getIntTime();

                            if (ToTime1 > FromTime3 || ToTime2 > FromTime3) { e.IsValid = false; }
                            else if (chkWktShift2IsOverNight.Checked) { e.IsValid = false; }
                            else { e.IsValid = true; }
                        }
                    }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Warning_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIsModify))
            {
                DataTable DT  = DBCs.FetchData(" SELECT EwrID FROM Trans WHERE EwrID IN ( SELECT EwrID FROM EmpWrkRel WHERE WktID = @P1 ) ", new string[] { txtID.Text });
                CtrlCs.ValidMsg(this, ref cvIsModify, true, General.Msg("Names may only be modified to work this time, as it relates to previous tranactions of Employees", "يمكن تعديل الأسماء فقط لوقت العمل هذا ، لارتباطه بحركات سابقة للموظفيين"));
                
                if (!DBCs.IsNullOrEmpty(DT))  
                { 
                    e.IsValid = false; 
                    UIModifyEnabled(true, chkShift2Setting.Checked, chkShift3Setting.Checked); 
                    CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Warning, General.Msg("Names may only be modified to work this time, as it relates to previous tranactions of Employees", "يمكن تعديل الأسماء فقط لوقت العمل هذا ، لارتباطه بحركات سابقة للموظفيين"));
                }
                else 
                { 
                    e.IsValid = true; 
                    UIEnabled(true, chkShift2Setting.Checked, chkShift3Setting.Checked);  
                }
            }
        }
        catch { e.IsValid = true; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}