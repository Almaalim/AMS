using System;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;

public partial class ChangePassword : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    UsersSql    UsrSqlCs = new UsersSql();
    EmployeeSql EmpSqlCs = new EmployeeSql();
    
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
            
            if (!string.IsNullOrEmpty(txtOldpassword.Text)) { ViewState["OldPass"] = txtOldpassword.Text; }
            if (ViewState["OldPass"] != null) { txtOldpassword.Attributes["value"] = ViewState["OldPass"].ToString(); }

            if (!string.IsNullOrEmpty(txtNewpassword.Text)) { ViewState["NewPass"] = txtNewpassword.Text; }
            if (ViewState["NewPass"] != null) { txtNewpassword.Attributes["value"] = ViewState["NewPass"].ToString(); }

            if (!string.IsNullOrEmpty(txtConfirmpassword.Text)) { ViewState["ConfirmPass"] = txtConfirmpassword.Text; }
            if (ViewState["ConfirmPass"] != null) { txtConfirmpassword.Attributes["value"] = ViewState["ConfirmPass"].ToString(); }

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** Common Code ************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            string EncPass = CryptorEngine.Encrypt(txtNewpassword.Text, true);

            if (pgCs.LoginType == "EMP") { EmpSqlCs.Employee_Update_Password(pgCs.LoginID, EncPass, pgCs.LoginID); }
            else { UsrSqlCs.AppUser_Update_Pass(pgCs.LoginID, EncPass); }
            btnCancel_Click(null, null);
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
        ViewState["OldPass"]  = null;
        txtOldpassword.Attributes["value"] = ""; 
        
        ViewState["NewPass"]  = null;
        txtNewpassword.Attributes["value"] = ""; 

        ViewState["ConfirmPass"]  = null;
        txtConfirmpassword.Attributes["value"] = ""; 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void Oldpassword_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvOldpassword))
            {
                if (string.IsNullOrEmpty(txtOldpassword.Text)) {  }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvOldpassword, true, General.Msg("Please enter correct password", "من فضلك أدخل كلة السر الحالية الصحيحة"));

                    DataTable DT = new DataTable();
                    if (pgCs.LoginType == "EMP")
                    {
                        DT = DBCs.FetchData(" SELECT EmpPWD FROM Employee WHERE EmpID = @P1 AND ISNULL(EmpDeleted,0) = 0 ", new string[] { pgCs.LoginID });
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            if (DT.Rows[0][0] != DBNull.Value)
                            {
                                string DecPass = CryptorEngine.Decrypt(DT.Rows[0][0].ToString(), true);
                                if (DecPass != txtOldpassword.Text) { e.IsValid = false; }
                            }
                            else { e.IsValid = false; }
                        }
                        else { e.IsValid = false; }
                    }
                    else
                    {
                        DT = DBCs.FetchData(" SELECT UsrPassword FROM AppUser WHERE UsrName = @P1 AND ISNULL(UsrDeleted,0) = 0 ", new string[] { pgCs.LoginID });
                        if (!DBCs.IsNullOrEmpty(DT))
                        {
                            if (DT.Rows[0][0] != DBNull.Value)
                            {
                                string DecPass = CryptorEngine.Decrypt(DT.Rows[0][0].ToString(), true);
                                if (DecPass != txtOldpassword.Text) { e.IsValid = false; }
                            }
                            else { e.IsValid = false; }
                        }
                        else { e.IsValid = false; }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Newpassword_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvNewpassword))
            {
                if (string.IsNullOrEmpty(txtNewpassword.Text) || string.IsNullOrEmpty(txtOldpassword.Text)) {  }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvNewpassword, true, General.Msg("Old and New passwords are same", "كلمة السر الجديدة متطابقة مع كلمة السر القديمة"));
                    if (txtOldpassword.Text == txtNewpassword.Text) { e.IsValid = false; } else { e.IsValid = true; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Confirmpassword_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvConfirmpassword))
            {
                if (string.IsNullOrEmpty(txtNewpassword.Text) || string.IsNullOrEmpty(txtConfirmpassword.Text)) {  }
                else
                {
                    CtrlCs.ValidMsg(this, ref cvConfirmpassword, true, General.Msg("New and Confirm passwords are not same", "كلمة السر الجديدة وتأكيدها غير متطابقين"));
                    if (txtNewpassword.Text != txtConfirmpassword.Text) { e.IsValid = false; } else { e.IsValid = true; }
                }
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