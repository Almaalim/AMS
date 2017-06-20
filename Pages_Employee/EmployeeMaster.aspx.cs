using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Elmah;
using System.Data;
using System.Data.SqlClient;

public partial class EmployeeMaster : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProCs = new EmployeePro();
    EmployeeSql SqlCs = new EmployeeSql();
    
    MailFun MailCs    = new MailFun();
    MailSql MailSqlCs = new MailSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    
    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = " SELECT * FROM EmployeeMasterInfoView ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            
            for (int i = 1; i <= 6; i++)
            {
                AjaxControlToolkit.AnimationExtender aniExtShow = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderShow" + i.ToString());
                AjaxControlToolkit.AnimationExtender aniExtClose = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderClose" + i.ToString());
                ImageButton lnkShow = (ImageButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("lnkShow" + i.ToString());
                if (aniExtShow != null) { CtrlCs.Animation(ref aniExtShow, ref aniExtClose, ref lnkShow, i.ToString()); }
            }

            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/

            MainQuery += " WHERE DepID IN (" + pgCs.DepList + ")";

            if (!IsPostBack)
            {  
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("100000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                /*** Common Code ************************************/

                changeVersion();

                if (LicDf.FetchLic("ER") == "1") { spnEmail.Visible = spnMobileNo.Visible = true; }
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
            CtrlCs.PopulateDepartmentList(ref ddlDepartment, pgCs.DepList, pgCs.Version);
            rfvddlDep.InitialValue = ddlDepartment.Items[0].Text;

            CtrlCs.FillCategoryList(ref ddlCategory, null, false);
            CtrlCs.FillNationalityList(ref ddlNationality, rfvddlNationality, false);
            CtrlCs.FillEmploymentTypeList(ref ddlEmpTypeID, null, false);
            CtrlCs.FillRanksList(ref ddlRanks, null, false);
            CtrlCs.FillRuleSetList(ref ddlAttenRule, null, false);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string GeneratPass(string emp)
    {
        int collect = DateTime.Now.Hour + DateTime.Now.Minute + Int32.Parse(emp);
        string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-";
        Random randNum = new Random(collect);  //Don't forget to seed your random, or else it won't really be random
        char[] pass = new char[8];
        //again, no need to pass this a variable if you always want 8
        for (int i = 0; i < 8; i++)
        {
            pass[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
            //No need to over complicate this, passing an integer value to Random.Next will "Return a nonnegative random number less than the specified maximum."
        }
        return new string(pass);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void changeVersion()
    {
        if (pgCs.Version == "Al_JoufUN")
        {
            lblEmployeeID.Text      = General.Msg("National ID:", "رقم الهوية :");           
            CtrlCs.ValidMsg(this, ref rvEmployeeID, General.Msg("National ID is required","حقل رقم الهوية إجباري"));
            lblCardID.Text          = General.Msg("Card ID:", "رقم البطاقة :");
            divNationalID.Visible   = rvfNationalID.Enabled = false;
            cvEmployeeIDLen.Enabled = false;
            divRanks.Visible        = false;
            chkHasFingerPrint.Visible = false;
        }
        else if (pgCs.Version == "BorderGuard")
        {
            lblEmployeeID.Text        = General.Msg("National ID:", "رقم الهوية :");
            CtrlCs.ValidMsg(this, ref rvEmployeeID, General.Msg("National ID is required","حقل رقم الهوية إجباري"));
            lblCardID.Text            = General.Msg("Employee ID:", "الرقم الوظيفي :");
            divNationalID.Visible     = rvfNationalID.Enabled = false;
            cvEmployeeIDLen.Enabled   = true;
            divRanks.Visible          = true;
            chkHasFingerPrint.Visible = true;
        }
        else // (pgCs.Version == "General")
        {
            lblEmployeeID.Text      = General.Msg("Employee ID:", "رقم الموظف :");
            CtrlCs.ValidMsg(this, ref rvEmployeeID, General.Msg("Employee ID is required","حقل رقم الموظف إجباري"));
            lblCardID.Text          = General.Msg("Card ID:", "رقم البطاقة :");
            divNationalID.Visible   = rvfNationalID.Enabled = true;
            cvEmployeeIDLen.Enabled = false;
            divRanks.Visible        = false;
            chkHasFingerPrint.Visible = false;
        }
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

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            if (ddlFilter.Text == "EmpID") 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedValue + " = @P1 ";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            }
            else 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedValue + " LIKE @P1 ";
                cmd.Parameters.AddWithValue("@P1", "%" + txtFilter.Text.Trim() + "%");
            }
        }

        UIClear();
        BtnStatus("100000");
        UIEnabled(false);
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
            spnEmpNameEn.Visible = true;
        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[5].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameEn").Enabled = false;
            ddlFilter.Items.FindByValue("DepNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            spnEmpNameAr.Visible = true;
        }
        else
        {
            grdData.Columns[1].Visible = false;
            grdData.Columns[4].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameAr").Enabled = false;
            ddlFilter.Items.FindByValue("DepNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtEmployeeID.Enabled = pStatus;
        txtEmpNameAr.Enabled = pStatus;
        txtEmpNameEn.Enabled = pStatus;
        txtCardID.Enabled = pStatus;
        txtJobTitleAR.Enabled = pStatus;
        txtJobTitleEn.Enabled = pStatus;
        txtNationalID.Enabled = pStatus;
        calJoinDate.SetEnabled(pStatus);
        calLeaveDate.SetEnabled(pStatus);
        ddlCategory.Enabled = pStatus;
        ddlDepartment.Enabled = pStatus;
        ddlEmpTypeID.Enabled = pStatus;
        ddlNationality.Enabled = pStatus;
        ddlBlood.Enabled = pStatus;
        ddlAttenRule.Enabled = pStatus;
        ddlLanguage.Enabled = pStatus;
        txtMobileNo.Enabled = pStatus;
        txtEmail.Enabled = pStatus;
        txtEmpDesc.Enabled = pStatus;
        txtEmpADUser.Enabled = btnGetADUsr.Enabled = pStatus;
        txtMaxPercentOT.Enabled = pStatus;
        chkSendEmailAlert.Enabled = pStatus;
        chkSendSMSAlert.Enabled = pStatus;
        chkStatus.Enabled = pStatus;
        chkAutoIN.Enabled = pStatus;
        chkAutoOut.Enabled = pStatus;
        rblGender.Enabled = pStatus;

        ddlRanks.Enabled = pStatus;
        txtEmpRankTitel.Enabled = pStatus;
        txtEmpRankDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            ProCs.EmpID         = txtEmployeeID.Text;
            ProCs.EmpNameAr     = txtEmpNameAr.Text;
            ProCs.EmpNameEn     = txtEmpNameEn.Text;
            ProCs.EmpCardID     = txtCardID.Text;

            if (pgCs.Version == "Al_JoufUN") { ProCs.EmpNationalID = txtEmployeeID.Text; }
            else if (pgCs.Version == "BorderGuard") { ProCs.EmpNationalID = txtEmployeeID.Text; }
            else { ProCs.EmpNationalID = txtNationalID.Text; } // (ActiveVersion == "General")
            
            if (ddlDepartment.SelectedIndex > 0)  { ProCs.DepID = ddlDepartment.SelectedValue; }
            if (ddlCategory.SelectedIndex > 0)    { ProCs.CatID = ddlCategory.SelectedValue; }
            if (ddlAttenRule.SelectedIndex > 0)   { ProCs.RlsID = ddlAttenRule.SelectedValue; }
            if (ddlNationality.SelectedIndex > 0) { ProCs.NatID = ddlNationality.SelectedValue; }
            if (ddlEmpTypeID.SelectedIndex > 0)   { ProCs.EtpID = ddlEmpTypeID.SelectedValue; }
            if (ddlBlood.SelectedIndex > 0)       { ProCs.EmpBlood = ddlBlood.SelectedValue.ToString(); }
            ProCs.EmpLanguage = ddlLanguage.SelectedValue.ToString();
            ProCs.EmpJobTitleAr = txtJobTitleAR.Text;
            ProCs.EmpJobTitleEn = txtJobTitleEn.Text;
            ProCs.EmpDescription = txtEmpDesc.Text;

            if (ddlRanks.SelectedIndex > 0)  
            { 
                ProCs.EmpRankCode = ddlRanks.SelectedValue;
                if (string.IsNullOrEmpty(txtEmpRankTitel.Text)) { txtEmpRankTitel.Text = ddlRanks.SelectedItem.Text; }
            }
            ProCs.EmpRankTitel = txtEmpRankTitel.Text;
            ProCs.EmpRankDesc  = txtEmpRankDesc.Text;

            if (!string.IsNullOrEmpty(calJoinDate.getGDate())) { ProCs.EmpJoinDate = calJoinDate.getGDateDBFormat(); }
            if (!string.IsNullOrEmpty(calLeaveDate.getGDate())) { ProCs.EmpLeaveDate = calLeaveDate.getGDateDBFormat(); }

            ProCs.EmpEmailID = txtEmail.Text;
            ProCs.EmpMobileNo = txtMobileNo.Text;
            if (!string.IsNullOrEmpty(txtEmpADUser.Text)) { ProCs.EmpADUser = txtEmpADUser.Text; }
            ProCs.EmpPWD = txtEmployeeID.Text;
            ProCs.EmpGender = rblGender.SelectedValue.ToString();
            ProCs.EmpSendEmailAlert = chkSendEmailAlert.Checked;
            ProCs.EmpSendSMSAlert = chkSendSMSAlert.Checked;
            ProCs.EmpStatus = chkStatus.Checked;
            ProCs.EmpAutoIn = chkAutoIN.Checked;
            ProCs.EmpAutoOut = chkAutoOut.Checked;
            if (txtMaxPercentOT.getTimeInSecond() == -1) { ProCs.EmpMaxOvertimePercent = null; } else { ProCs.EmpMaxOvertimePercent = txtMaxPercentOT.getTimeInSecond().ToString(); }

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
            txtEmployeeID.Text = "";
            txtEmpNameAr.Text = "";
            txtEmpNameEn.Text = "";
            txtCardID.Text = "";
            txtJobTitleAR.Text = "";
            txtJobTitleEn.Text = "";
            txtNationalID.Text = "";
            calJoinDate.ClearDate();
            calLeaveDate.ClearDate();
            ddlDepartment.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlBlood.SelectedIndex = 0;       
            ddlLanguage.SelectedIndex = 0;
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            txtEmpADUser.Text = "";
            txtEmpDesc.Text = "";
            txtMaxPercentOT.ClearTime();
            chkSendEmailAlert.Checked = false;
            chkSendSMSAlert.Checked = false;
            chkStatus.Checked = false;
            chkAutoIN.Checked = false;
            chkAutoOut.Checked = false;            
            txtEmpRankTitel.Text = "";
            txtEmpRankDesc.Text = "";
            chkHasFingerPrint.Checked = false;

            if (ddlCategory.Items.Count  > 0) { ddlCategory.SelectedIndex  = 0; }   
            if (ddlEmpTypeID.Items.Count > 0) { ddlEmpTypeID.SelectedIndex = 0; }
            if (ddlAttenRule.Items.Count > 0) { ddlAttenRule.SelectedIndex = 0; }
            if (rblGender.Items.Count    > 0) { rblGender.SelectedIndex    = 0; }
            if (ddlRanks.Items.Count     > 0) { ddlRanks.SelectedIndex     = 0; }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAutoIN_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAutoIN.Checked)
        {
            bool chk = checkAutoInOut("cfgAutoIn");
            if (!chk)
            {
                chkAutoIN.Checked = false;
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.Msg("Auto IN Option is Disable from Configuration Page", "خاصية الحضور التلقائي معطلة من الإعدادات العامة للنظام"));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkAutoOut_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAutoOut.Checked)
        {
            bool chk = checkAutoInOut("cfgAutoOut");
            if (!chk)
            {
                chkAutoOut.Checked = false;
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Info, General.Msg("Auto Out Option is Disable from Configuration Page", "خاصية الإنصراف التلقائي معطلة من الإعدادات العامة للنظام"));
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool checkAutoInOut(string check)
    {
        bool Autocheck = false;
        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT " + check + " FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) { Autocheck = Convert.ToBoolean(DT.Rows[0][check]); }

        return Autocheck;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnGetADUsr_Click(object sender, ImageClickEventArgs e)
    {
        txtEmpADUser.Text = "";
        
        UsersPro AD = ActiveDirectoryFun.FillAD(ActiveDirectoryFun.ADTypeEnum.EMP, txtEmployeeID.Text.Trim(), txtNationalID.Text.Trim(), txtEmail.Text.Trim());
        if (!AD.ADValid)      { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADError); return; }
        if (!AD.ADValidation) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADMsgValidation); return; }

        txtEmpADUser.Text = ActiveDirectoryFun.GetAD(AD);
        if (string.IsNullOrEmpty(txtEmpADUser.Text)) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADMsgNotExists); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool isHasFingerPrint(string pEmpID)
    {
        DataTable DT = DBCs.FetchData(" SELECT EmpID FROM EmpMEMSEnrol WHERE EmpID = @p1 ", new string[] { pEmpID });
        if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
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
        UIClear();
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("001100");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        txtEmployeeID.Enabled = false;
        BtnStatus("001100");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string commandName = ViewState["CommandName"].ToString();
            if (commandName == string.Empty) { return; }

            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (commandName == "ADD") { SqlCs.Insert(ProCs); } else if (commandName == "EDIT") { SqlCs.Update(ProCs); }

            UIClear();
            UIEnabled(false);
            BtnStatus("100000");
            btnFilter_Click(null,null);
            
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
        UIClear();
        BtnStatus("100000");
        UIEnabled(false);
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
        btnVacSet.Enabled = GenCs.FindStatus(Status[4]);
        btnResetPassword.Enabled = GenCs.FindStatus(Status[5]);

        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string empid = ViewState["ID"].ToString();
            string empPass = GeneratPass(empid);
            string lang = ddlLanguage.SelectedValue.ToString();

            string EncPass = CryptorEngine.Encrypt(empPass, true);

            if (!MailCs.FillEmailSetting()) 
            { 
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("Not to enter the e-mail settings, you can not modify the password", "لم يتم إدخال إعدادات لبريد الالكتروني,لا يمكن تعديل كلمة المرور"));
                return; 
            }
             
            SqlCs.Employee_Update_Password(empid, EncPass, pgCs.LoginID);
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("password changed successfully, new password sent it to employee Email", "تم تغيير كلمة المرور بنجاح، وارسلت للبريد الإلكتروني للموظف،"));
            
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnVacSet_Click(object sender, EventArgs e)
    {
        string Name = txtEmployeeID.Text + "-" + General.Msg(txtEmpNameEn.Text, txtEmpNameAr.Text);
        lblNamePopup.Text = General.Msg("Set Employee Vacation", "تخصيص إجازة للموظف");
        ifrmPopup.Attributes.Add("src", "SetEmployeeVacation.aspx?EmpID=" + txtEmployeeID.Text + "&Name=" + Name);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "showPopup();", true);
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
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        Hashtable ht = (Hashtable)ViewState["ht"];
                        if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                        delBtn.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete1"):
                    string ID = e.CommandArgument.ToString();
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM Trans WHERE EmpID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }
                    
                    SqlCs.Delete(ID, pgCs.LoginID);

                    btnFilter_Click(null,null);

                    CtrlCs.ShowDelMsg(this, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            DataView dataView = new DataView(DT);

            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = "ASC";
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }
            else
            {
                sortDirection = Convert.ToString(ViewState["SortDirection"]);
                sortDirection = ConvertSortDirectionToSql(sortDirection);
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
            }

            dataView.Sort = e.SortExpression + " " + sortDirection;

            grdData.DataSource = dataView;
            grdData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ConvertSortDirectionToSql(string sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;

            case "DESC":
                newSortDirection = "ASC";
                break;
        }

        return newSortDirection;
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
            UIEnabled(false);
            BtnStatus("100000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[0].Text);
                BtnStatus("110011");
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
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("EmpID = '" + pID + "'");

            txtEmployeeID.Text = DRs[0]["EmpID"].ToString();
            txtEmpNameAr.Text  = DRs[0]["EmpNameAr"].ToString();
            txtEmpNameEn.Text  = DRs[0]["EmpNameEn"].ToString();
            txtCardID.Text     = DRs[0]["EmpCardID"].ToString();
            txtJobTitleAR.Text = DRs[0]["EmpJobTitleAr"].ToString();
            txtJobTitleEn.Text = DRs[0]["EmpJobTitleEn"].ToString();
            txtNationalID.Text = DRs[0]["EmpNationalID"].ToString();

            ddlRanks.SelectedIndex = ddlRanks.Items.IndexOf(ddlRanks.Items.FindByValue(DRs[0]["EmpRankCode"].ToString()));
            txtEmpRankTitel.Text = DRs[0]["EmpRankTitel"].ToString();
            txtEmpRankDesc.Text  = DRs[0]["EmpRankDesc"].ToString();

            ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(DRs[0]["CatID"].ToString()));

            if (isHasFingerPrint(DRs[0]["EmpID"].ToString())) { chkHasFingerPrint.Checked = true; }

            string ss1 = DRs[0]["EtpID"].ToString();
            string ss2 = DRs[0]["RlsID"].ToString();
            string ss3 = DRs[0]["EmpBlood"].ToString();

            ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(DRs[0]["DepID"].ToString()));
            ddlEmpTypeID.SelectedIndex = ddlEmpTypeID.Items.IndexOf(ddlEmpTypeID.Items.FindByValue(DRs[0]["EtpID"].ToString()));
            ddlNationality.SelectedIndex = ddlNationality.Items.IndexOf(ddlNationality.Items.FindByValue(DRs[0]["NatID"].ToString()));
            ddlBlood.SelectedIndex = ddlBlood.Items.IndexOf(new ListItem(DRs[0]["EmpBlood"].ToString()));
            ddlAttenRule.SelectedIndex = ddlAttenRule.Items.IndexOf(ddlAttenRule.Items.FindByValue(DRs[0]["RlsID"].ToString()));
            ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(DRs[0]["EmpLanguage"].ToString()));
            txtMobileNo.Text = DRs[0]["EmpMobileNo"].ToString();
            txtEmail.Text = DRs[0]["EmpEmailID"].ToString();
            txtEmpDesc.Text = DRs[0]["EmpDescription"].ToString();
            txtEmpADUser.Text = DRs[0]["EmpADUser"].ToString();
            rblGender.SelectedIndex = rblGender.Items.IndexOf(rblGender.Items.FindByValue(DRs[0]["EmpGender"].ToString()));
            if (DRs[0]["EmpMaxOvertimePercent"] != DBNull.Value) { txtMaxPercentOT.SetTime(Convert.ToInt32(DRs[0]["EmpMaxOvertimePercent"]), TextTimeServerControl.TextTime.TimeTypeEnum.Seconds); }
            if (DRs[0]["EmpSendEmailAlert"] != DBNull.Value) { chkSendEmailAlert.Checked = Convert.ToBoolean(DRs[0]["EmpSendEmailAlert"]); }
            if (DRs[0]["EmpSendSMSAlert"] != DBNull.Value) { chkSendSMSAlert.Checked = Convert.ToBoolean(DRs[0]["EmpSendSMSAlert"]); }
            if (DRs[0]["EmpStatus"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["EmpStatus"]); }
            if (DRs[0]["EmpAutoIn"] != DBNull.Value) { chkAutoIN.Checked = Convert.ToBoolean(DRs[0]["EmpAutoIn"]); }
            if (DRs[0]["EmpAutoOut"] != DBNull.Value) { chkAutoOut.Checked = Convert.ToBoolean(DRs[0]["EmpAutoOut"]); }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            calJoinDate.SetGDate(DRs[0]["EmpJoinDate"], pgCs.DateFormat);
            calLeaveDate.SetGDate(DRs[0]["EmpLeaveDate"], pgCs.DateFormat);
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

    protected void EmpNameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if      (source.Equals(cvEmpNameEn)) { if (pgCs.LangEn) { if (string.IsNullOrEmpty(txtEmpNameEn.Text)) { e.IsValid = false; } } }
            else if (source.Equals(cvEmpNameAr)) { if (pgCs.LangAr) { if (string.IsNullOrEmpty(txtEmpNameAr.Text)) { e.IsValid = false; } } }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void EamilMobileRequired_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (LicDf.FetchLic("ER") == "1") { if (txtEmail.Text.Length > 0 && txtMobileNo.Text.Length > 0) { e.IsValid = true; } else { e.IsValid = false; } }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Gender_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvGender))
            {
                if (rblGender.SelectedIndex == -1) { e.IsValid = false; } else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void EmployeeIDLen_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmployeeIDLen))
            {
                if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                {
                    if (txtEmployeeID.Text.Length != 10) 
                    { 
                        CtrlCs.ValidMsg(this, ref cvEmployeeIDLen, true, General.Msg("Must consist National ID of 10 digits", "يجب أن يتكون رقم الهوية من 10 خانات"));
                        e.IsValid = false; 
                        return; 
                    }

                    if (ViewState["CommandName"].ToString() == "EDIT")
                    {
                        CtrlCs.ValidMsg(this, ref cvEmployeeIDLen, true, General.Msg("this ID is there already ", "رقم الموظف موجود مسبقاً"));

                        DataTable DT = DBCs.FetchData("SELECT EmpID FROM Employee WHERE EmpID = @P1 ", new string[] { txtEmployeeID.Text });
                        if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                        
                        return;
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
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND EmpID != @P2 "; }

            if (source.Equals(cvADUser))
            {
                if (!string.IsNullOrEmpty(txtEmpADUser.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvADUser, true, General.Msg("Active Directory Name already exists", "اسم مستخدم Active Directory موجود مسبقا"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM Employee WHERE ISNULL(EmpDeleted,0) = 0 AND @P1 " + UQ, new string[] { txtEmpADUser.Text, txtEmpADUser.Text });
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
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}