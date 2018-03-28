using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;

public partial class OverTimeRequest2 : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpRequestPro ProCs = new EmpRequestPro();
    EmpRequestSql SqlCs = new EmpRequestSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            if (pgCs.Lang == "AR") { LanguageSwitch.Href = "~/CSS/Metro/MetroAr.css"; } else { LanguageSwitch.Href = "~/CSS/Metro/Metro.css"; }
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check ERS License ***/ pgCs.CheckERSLicense(); 
                BtnStatus("11");
                UIEnabled(true);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FindApprovalSequence();
                spnReqFile.Visible = GenCs.CheckFileRequired("OVT");

                if (Request.QueryString["ID"] != null)
                {
                    DataTable DT = DBCs.FetchData(" SELECT O.OvtID,O.EmpID,O.OvtDate,O.OvtStartTime,O.OvtEndTime,O.OvtDuration,E.EmpNameAr,E.EmpNameEn From OverTime O,Employee E WHERE O.EmpID = E.EmpID AND O.OvtID = @P1 ", new string[] { Request.QueryString["ID"].ToString() });
                    if (!DBCs.IsNullOrEmpty(DT)) 
                    {
                        txtOvtID.Text = Request.QueryString["ID"].ToString();
                        if (DT.Rows[0]["OvtDate"] != DBNull.Value) 
                        { 
                            txtDate.Text = DTCs.ShowDBDate(DT.Rows[0]["OvtDate"], "dd/MM/yyyy"); 
                        }

                        tpFrom.SetTime(Convert.ToDateTime(DT.Rows[0]["OvtStartTime"]));
                        tpTo.SetTime(Convert.ToDateTime(DT.Rows[0]["OvtEndTime"]));

                        ViewState["OvtDuration"] =  DT.Rows[0]["OvtDuration"].ToString();
                    }
                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PostbackFunction();", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillList()
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { lblName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn","EmpNameAr")].ToString(); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindApprovalSequence()
    {
        try
        {
            bool isFind = GenCs.FindEmpApprovalSequence("OVT", pgCs.LoginEmpID);
            btnSave.Enabled = btnCancel.Enabled = isFind;
            if (!isFind) { if (!isFind) { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Info, "vgShowMsg", General.ApprovalSequenceMsg()); } }
        }
        catch (Exception e1) { ErrorSignal.FromCurrentContext().Raise(e1); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        txtOvtID.Enabled = false;
        txtDate.Enabled = false;
        tpFrom.Enabled = false;
        tpTo.Enabled = false;
        //fudReqFile.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        ProCs.RetID        = "OVT";
        ProCs.EmpID        = pgCs.LoginEmpID;
        ProCs.GapOvtID     = txtOvtID.Text;
        ProCs.ErqStartDate = DTCs.ToDBFormat(txtDate.Text, pgCs.DateType);
        ProCs.ErqStartTime = tpFrom.getDateTime(DTCs.GetGregDateFromCurrDateType(DTCs.ToDefFormat(txtDate.Text, pgCs.DateType), "dd/MM/yyyy")).ToString();
        ProCs.ErqEndDate   = DTCs.ToDBFormat(txtDate.Text, pgCs.DateType);
        ProCs.ErqEndTime   = tpTo.getDateTime(DTCs.GetGregDateFromCurrDateType(DTCs.ToDefFormat(txtDate.Text, pgCs.DateType), "dd/MM/yyyy")).ToString();
        ProCs.ErqReason    = txtDesc.Text;
        ProCs.ErqReqStatus = "0";

        ProCs.ErqStatusTime = "p";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        txtOvtID.Text = "";
        txtDate.Text = "";
        tpFrom.SetTime(00, 00);
        tpTo.SetTime(00, 00);
        txtDesc.Text = "";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            BtnStatus("00");

            FillPropeties();

            try
            {
                if (!string.IsNullOrEmpty(fudReqFile.PostedFile.FileName))
                {
                    //string ipServer = General.GetIPAddress();
                    string OvtID = txtOvtID.Text;
                    string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                    string[] nameArr = FileName.Split('.');
                    string name = nameArr[0];
                    string type = nameArr[1];
                    string NewFileName = OvtID + "-OVT." + type;

                    //' Display properties of the uploaded file
                    //FileName.InnerHtml = MyFile.PostedFile.FileName
                    //FileContent.InnerHtml = MyFile.PostedFile.ContentType 
                    //FileSize.InnerHtml = MyFile.PostedFile.ContentLength
                    //UploadDetails.visible = True
                    fudReqFile.PostedFile.SaveAs(Server.MapPath(@"../RequestsFiles/") + NewFileName);
                    ProCs.ErqReqFilePath = NewFileName;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

            int ID = SqlCs.Insert(ProCs);

            UIClear();
            BtnStatus("11");

            Session["ERSRefresh"] = "Update";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hideparentPopupSave('divPopup2','../Pages_Attend/ERS_EmployeeOvertime.aspx');", true);
            //ShowMsg("Request details saved successfully", "تم حفظ الطلب");
        }
        catch (Exception ex) 
        { 
            BtnStatus("11");
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, vsShowMsg, cvShowMsg, "vgShowMsg", ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("11");
        Session["ERSRefresh"] = "Update";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hideparentPopupSave('divPopup2','../Pages_Attend/ERS_EmployeeOvertime.aspx');", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [Save,Cancel]
    {
        btnSave.Enabled   = GenCs.FindStatus(Status[0]);
        btnCancel.Enabled = GenCs.FindStatus(Status[1]);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void MaxOverTime_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        string max = "";
        try
        {
            if (source.Equals(cvMaxOverTime))
            {
                if (!string.IsNullOrEmpty(txtDate.Text))
                {
                    bool foundWT = false;
                    bool foundEmp = false;
                    DataTable WTMaxdt = new DataTable();
                    DateTime Date = DTCs.ConvertToDatetime(txtDate.Text, pgCs.DateType);

                    foundWT = SqlCs.isWorkDays(Date, pgCs.LoginEmpID);

                    DataTable EMDT = DBCs.FetchData(" SELECT EmpMaxOvertimePercent FROM Employee WHERE EmpID = @P1 ", new string[] {  pgCs.LoginEmpID });
                    if (!DBCs.IsNullOrEmpty(EMDT)) { foundEmp = true; }

                    if (foundWT) 
                    { 
                        WTMaxdt = DBCs.FetchData(" SELECT WktAddPercent FROM WorkingTime WHERE WktID = (SELECT WktID FROM dbo.GetEmpWrkRel_Fun(@P1,@P2) ", new string[] { Date.ToString(), pgCs.LoginEmpID });  
                    }

                    //if  ( EmpMaxdt.Rows[0]["EmpMaxOvertimePercent"] != DBNull.Value ) { max = EmpMaxdt.Rows[0]["EmpMaxOvertimePercent"].ToString(); }
                    if (foundEmp && EMDT.Rows[0]["EmpMaxOvertimePercent"].ToString() != "0" && EMDT.Rows[0]["EmpMaxOvertimePercent"].ToString() != "-1") { max = EMDT.Rows[0]["EmpMaxOvertimePercent"].ToString(); }
                    else if (foundWT && WTMaxdt.Rows[0]["WktAddPercent"].ToString() != "0" && WTMaxdt.Rows[0]["WktAddPercent"].ToString() != "-1") { max = WTMaxdt.Rows[0]["WktAddPercent"].ToString(); }
                    else
                    {
                        DataTable MDT = DBCs.FetchData(new SqlCommand(" SELECT cfgMaxPercOT FROM Configuration "));
                        if (!DBCs.IsNullOrEmpty(MDT))  { if (MDT.Rows[0]["cfgMaxPercOT"] != DBNull.Value) { max = MDT.Rows[0]["cfgMaxPercOT"].ToString(); } }  
                    }

                    if (string.IsNullOrEmpty(max) || max == "0" || max == "-1") { e.IsValid = true; }
                    else
                    {
                        DataTable SMDT = DBCs.FetchData(" SELECT SUM(OvtDuration) SUMDuration FROM OverTime WHERE OvtID IN ( SELECT GapOvtID FROM EmpRequest WHERE EmpID = @P1 AND ErqStartDate = @P2 AND RetID ='OVT' AND ErqReqStatus in (0,1) )", new string[] {  pgCs.LoginEmpID, Date.ToString() });
                        if (SMDT.Rows[0]["SUMDuration"] == DBNull.Value) { e.IsValid = true; }
                        else
                        {
                            if (Convert.ToInt32(ViewState["OvtDuration"]) + Convert.ToInt32(SMDT.Rows[0]["SUMDuration"]) > Convert.ToInt32(max))
                            {
                                CtrlCs.ValidMsg(this, ref cvMaxOverTime, true, General.Msg("Have reached the maximum allowable overtime, you can not submit this request", "لقد بلغت الحد الأقصى المسموح للوقت الإضافي ,لا يمكنك تقديم هذا الطلب"));
                                e.IsValid = false;
                            }
                            else { e.IsValid = true; }
                        }

                    }
                }
            }
        }
        catch ( Exception ex )
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvReqFile))
            {
                if (GenCs.CheckFileRequired("OVT")) { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName)) { e.IsValid = false; } }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

