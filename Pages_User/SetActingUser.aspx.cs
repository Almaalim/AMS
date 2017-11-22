using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;

public partial class SetActingUser : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersPro ProCs = new UsersPro();
    UsersSql SqlCs = new UsersSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
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
                /*** Common Code ************************************/

                 if (Request.QueryString["UsrName"] != null)
                {
                    string UserName = Request.QueryString["UsrName"].ToString().Trim();
                    PopulateUI(UserName);

                }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string User)
    {
        DataTable DT = DBCs.FetchData(" SELECT * FROM AppUser WHERE UsrName = @P1 ", new string[] { User });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            txtID.Text            = User;
            txtUsername.Text      = DT.Rows[0]["UsrActUserName"].ToString();
            txtPassword.Attributes["value"] = DT.Rows[0]["UsrActPwd"].ToString();
            txtEmpID.Text         = DT.Rows[0]["UsrActEmpID"].ToString();
            txtUsrActEMailID.Text = DT.Rows[0]["UsrActEMailID"].ToString();
            txtUsrADActUser.Text  = DT.Rows[0]["UsrADActUser"].ToString();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.UsrName        = txtID.Text;
            ProCs.UsrActingUser  = true;
            ProCs.UsrActUserName = txtUsername.Text.Trim();
            ProCs.UsrActPwd      = txtPassword.Text.Trim();
            if (!string.IsNullOrEmpty(txtEmpID.Text))         { ProCs.UsrActEmpID   = txtEmpID.Text.Trim(); }
            if (!string.IsNullOrEmpty(txtUsrActEMailID.Text)) { ProCs.UsrActEMailID = txtUsrActEMailID.Text; }
            if (!string.IsNullOrEmpty(txtUsrADActUser.Text))  { ProCs.UsrADActUser  = txtUsrADActUser.Text; }

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            if (string.IsNullOrEmpty(txtID.Text)) 
            {
                CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Validation, "vgShowMsg", General.Msg("UsrName is empty", "اسم المستخدم فارغ"));
                return; 
            }
            
            
            FillPropeties();
            SqlCs.AppUser_Update_Acting(ProCs);
            CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Success, "vgShowMsg", General.Msg("Saved data successfully", "تم الحفظ البيانات بنجاح"));
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, vsShowMsg, cvShowMsg, "vgShowMsg", ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnGetADUsr_Click(object sender, ImageClickEventArgs e)
    {
        txtUsrADActUser.Text = "";
        
        UsersPro AD = ActiveDirectoryFun.FillAD(ActiveDirectoryFun.ADTypeEnum.USR, txtEmpID.Text.Trim(), txtEmpID.Text.Trim(), txtUsrActEMailID.Text.Trim());
        if (!AD.ADValid)      { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Validation, "vgShowMsg", AD.ADError); return; }
        if (!AD.ADValidation) { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Validation, "vgShowMsg", AD.ADMsgValidation); return; }

        txtUsrADActUser.Text = ActiveDirectoryFun.GetAD(AD);
        if (string.IsNullOrEmpty(txtUsrADActUser.Text)) { CtrlCs.ShowMsg(this, vsShowMsg, cvShowMsg, CtrlFun.TypeMsg.Validation, "vgShowMsg", AD.ADMsgNotExists); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Custom Validate Events

    protected void EmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmpID))
            {
                if (string.IsNullOrEmpty(txtEmpID.Text)) { e.IsValid = true; }
                if (!string.IsNullOrEmpty(txtEmpID.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvEmpID, true, General.Msg("Entered Employee ID does not exist already,Please enter another ID", "رقم الموظف غير موجود,أدخل رقما آخر"));

                    DataTable DT = DBCs.FetchData(" SELECT * FROM Employee WHERE EmpID = @P1 AND ISNULL(EmpDeleted,0) = 0 ", new string[] { txtEmpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
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
    protected void Email_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmail))
            {
                if (string.IsNullOrEmpty(txtUsrActEMailID.Text))
                {
                    //int ErsEnable = Convert.ToInt32(LicDf.FetchLic("ER"));
                    //int AlertEnable = Convert.ToInt32(LicDf.FetchLic("ES"));

                    //if ((ErsEnable == 1) && (AlertEnable == 1)) { e.IsValid = false; } else { e.IsValid = true; }

                    e.IsValid = true;
                }
                else
                {
                    string str = Convert.ToString(txtUsrActEMailID.Text);
                    if (str.IndexOf('@') < 0)
                    {
                        CtrlCs.ValidMsg(this, ref cvEmail, true, General.Msg("Please enter email in correct format", "البريد الالكتروني غير صحيح"));
                        e.IsValid = false;
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
    protected void ADUser_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = " AND UsrName != @P2 ";

            if (source.Equals(cvADUser))
            {
                if (!string.IsNullOrEmpty(txtUsrADActUser.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvADUser, true, General.Msg("Active Directory Name already exists", "اسم مستخدم Active Directory موجود مسبقا"));
                    DataTable DT = DBCs.FetchData(" SELECT * FROM AppUser WHERE ( UsrADUser = @P1 OR UsrADActUser = @P1 ) AND ISNULL(UsrDeleted,0) = 0 " + UQ, new string[] { txtUsrADActUser.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}