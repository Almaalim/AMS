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
using System.Data.SqlClient;
using System.Threading;

public partial class ShiftExcuseRequest2 : BasePage
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
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FindApprovalSequence();
                spnReqFile.Visible = GenCs.CheckFileRequired("ESH");

                if (Request.QueryString["ID"] != null)
                {
                    UIEnabled(true,false);
                    string GDate   = Request.QueryString["ID"].ToString();
                    string ShiftID = Request.QueryString["SID"].ToString();
               
                    txtShiftID.Text = ShiftID;      
                    calDate.SetGDate(GDate);
                    ViewState["ErqStatusTime"] = "P";
                    bool Find = FindWorkTime(GDate, ShiftID);
                }
                else
                {
                    UIEnabled(true, true);
                    ViewState["ErqStatusTime"] = "F";
                    divName.Visible = false;
                }

                Session["AttendanceListMonth"] = DTCs.FindCurrentMonth();
                Session["AttendanceListYear"]  = DTCs.FindCurrentYear();
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
            DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 ", new string[] { pgCs.LoginEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { lblName.Text = DT.Rows[0]["EmpID"].ToString() + " - " + DT.Rows[0][General.Msg("EmpNameEn","EmpNameAr")].ToString(); }

            CtrlCs.FillExcuseTypeList(ref ddlExcType, rfvExcType, false, true);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFillWorkTime_Click(object sender, EventArgs e) 
    {
        if (!string.IsNullOrEmpty(calDate.getGDate()))
        {
            bool Find = FindWorkTime(calDate.getGDate(),null);
            if (!Find) 
            { 
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("You do not have to work on that date, please choose another date", "لا يوجد لديك عمل في هذا التاريخ، الرجاء اختيار تاريخ آخر"));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FindWorkTime(string pDate,string pShiftID)
    {
        try
        {
            DateTime Date = DTCs.ConvertToDatetime(pDate, "Gregorian");
            string MQ = " SELECT WktID "
                      + " FROM EmpWrkRel "
                      + " WHERE EmpID = @P1 "
                      + " AND @P2 BETWEEN EwrStartDate AND EwrEndDate "
                      + " AND (((DATEPART(DW, @P2) = 1) AND (EwrSun = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 2) AND (EwrMon = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 3) AND (EwrTue = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 4) AND (EwrWed = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 5) AND (EwrThu = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 6) AND (EwrFri = 1)) OR "
                      + " ((DATEPART(DW, @P2) = 7) AND (EwrSat = 1)))"
                      + " ORDER BY EwrID DESC";
            DataTable DT = DBCs.FetchData(MQ, new string[] { pgCs.LoginEmpID, Date.ToString() });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                txtWorkTimeID.Text = DT.Rows[0]["WktID"].ToString();
                string WQ = " SELECT WktNameAr,WktNameEn,WktShift1NameAr,WktShift1NameEn,WktShift2NameAr,WktShift2NameEn,WktShift3NameAr,WktShift3NameEn "
                         + " ,WktShift1From,WktShift1To,WktShift2From,WktShift2To,WktShift3From,WktShift3To"
                         + " FROM WorkingTime "
                         + " WHERE WktID = @P1 ";
                
                DataTable WDT = DBCs.FetchData(WQ, new string[] { DT.Rows[0]["WktID"].ToString() });
                if (!DBCs.IsNullOrEmpty(WDT))
                {
                    txtWorkTimeName.Text = WDT.Rows[0][General.Msg("WktNameEn","WktNameAr")].ToString();

                    ddlShiftName.Items.Add(new ListItem(General.Msg("-Select Shift-","-اختر الوردية-"), "0"));
                    for (int i = 1; i <= 3; i++)
                    {
                        string ID = i.ToString();
                        if (WDT.Rows[0]["WktShift" + ID + "NameAr"] != DBNull.Value || WDT.Rows[0]["WktShift" + ID + "NameEn"] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(WDT.Rows[0]["WktShift" + ID + "NameAr"].ToString()) || !string.IsNullOrEmpty(WDT.Rows[0]["WktShift" + ID + "NameEn"].ToString()) )
                            {
                                ddlShiftName.Items.Add(new ListItem("WktShift" + ID + General.Msg("NameEn","NameAr"), "0"));
                            }
                        }
                    }
                    rfvShiftName.InitialValue = ddlShiftName.Items[0].Text;

                    if (!string.IsNullOrEmpty(pShiftID))
                    {
                        ddlShiftName.SelectedIndex = ddlShiftName.Items.IndexOf(ddlShiftName.Items.FindByValue(pShiftID));
                        txtShiftID.Text = pShiftID;
                        tpFrom.SetTime(Convert.ToDateTime(WDT.Rows[0]["WktShift" + pShiftID + "From"]));
                        tpTo.SetTime(Convert.ToDateTime(WDT.Rows[0]["WktShift" + pShiftID + "To"]));
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindApprovalSequence()
    {
        try
        {
            bool isFind = GenCs.FindEmpApprovalSequence("ESH", pgCs.LoginEmpID);
            btnSave.Enabled = btnCancel.Enabled = isFind;
            if (!isFind) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.ApprovalSequenceMsg()); }
        }
        catch (Exception e1) { ErrorSignal.FromCurrentContext().Raise(e1); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus1, bool pStatus2)
    {
        calDate.SetEnabled(pStatus2);
        btnFillWorkTime.Visible = pStatus2;
        txtWorkTimeName.Enabled = false;
        txtWorkTimeID.Enabled = false;
        ddlShiftName.Enabled = pStatus2;
        txtShiftID.Enabled = false;
        tpFrom.Enabled = false;
        tpTo.Enabled = false;
        ddlExcType.Enabled = pStatus1;
        //fudReqFile.Enabled = pStatus1;
        txtDesc.Enabled = pStatus1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ProCs.RetID        = "ESH";
        ProCs.ErqTypeID    = ddlExcType.SelectedValue;
        ProCs.EmpID        = pgCs.LoginEmpID;
        ProCs.ErqStartDate = calDate.getGDateDBFormat();
        ProCs.ErqStartTime = tpFrom.getDateTime(ProCs.ErqStartDate).ToString();
        ProCs.ErqEndTime   = tpTo.getDateTime(ProCs.ErqStartDate).ToString();
        ProCs.ErqReason    = txtDesc.Text;
        ProCs.WktID        = txtWorkTimeID.Text;
        ProCs.ShiftID      = txtShiftID.Text;
        ProCs.ErqReqStatus = "0";
        ProCs.ErqStatusTime = ViewState["ErqStatusTime"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        calDate.ClearDate();
        txtWorkTimeName.Text = "";
        txtWorkTimeID.Text = "";
        ddlShiftName.SelectedIndex = -1;
        txtShiftID.Text = "";
        tpFrom.ClearTime();
        tpTo.ClearTime();
        ddlExcType.SelectedIndex = -1;
        txtDesc.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlShiftName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtWorkTimeID.Text))
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM WorkingTime WHERE WktID = @P1 ", new string[] { txtWorkTimeID.Text });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (ddlShiftName.SelectedIndex > 0)
                {
                    txtShiftID.Text = (ddlShiftName.SelectedIndex).ToString();
                    tpFrom.SetTime(Convert.ToDateTime(DT.Rows[0]["WktShift" + txtShiftID.Text + "From"]));
                    tpTo.SetTime(Convert.ToDateTime(DT.Rows[0]["WktShift" + txtShiftID.Text + "To"]));
                }
                else
                {
                    txtShiftID.Text = "";
                    tpFrom.ClearTime();
                    tpTo.ClearTime();
                }
            }
            else
            {
                txtShiftID.Text = "";
                tpFrom.ClearTime();
                tpTo.ClearTime();
            }
        }
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
                    string dateFile = DateTime.Now.ToLongDateString() + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
                    string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                    string[] nameArr = FileName.Split('.');
                    string name = nameArr[0];
                    string type = nameArr[1];
                    string NewFileName = pgCs.LoginEmpID + "-" + dateFile + "-ESH" + "." + type;
                    fudReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                    ProCs.ErqReqFilePath = NewFileName;
                    //empReq.ErqReqFilePath = "\\\\" + ipServer + "\\" + Server.MapPath(@"./RequestsFiles/" + FileName).ToString().Substring(3);
                }
            }
            catch { }


            int ID = SqlCs.Insert(ProCs);
            //mailCs.SendMailToMGR(DT.Rows[0]["EalMgrID"].ToString(), ID.ToString(), pgCs.DateType, pgCs.Lang, mailCs.FindLoginUrl(Request.Url));
            ////mail.Insert("EshRequest", ID.ToString(), Reqdr["EalMgrID"].ToString());
            
            UIClear();
            BtnStatus("11");
            if (Request.QueryString["ID"] != null)
            {
                Session["ERSRefresh"] = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "hideparentPopupSave('','AttendanceList.aspx');", true);
            }
            else
            {
                CtrlCs.ShowSaveMsg(this);
            }
        }
        catch (Exception ex) 
        { 
            BtnStatus("11");
            ErrorSignal.FromCurrentContext().Raise(ex); 
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("11");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Exit();", true);
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

    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (source.Equals(cvReqFile))
        {
            if (GenCs.CheckFileRequired("ESH")) { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName)) { e.IsValid = false; } }
        }
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}