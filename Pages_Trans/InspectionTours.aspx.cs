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

public partial class InspectionTours : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    InspectionToursPro ProCs = new InspectionToursPro();
    InspectionToursSql SqlCs = new InspectionToursSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun DTCs = new DTFun();

    SMSSendFun SmsSendCs = new SMSSendFun();

    string sortDirection = "ASC";
    string sortExpression = "";

    string MainQuery = " SELECT * FROM InspectionToursMaster ";
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

            ImageButton StartStep = this.WizardData.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartStep") as ImageButton;
            ImageButton BackStep = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnBackStep") as ImageButton;
            ImageButton NextStep = this.WizardData.FindControl("StepNavigationTemplateContainerID").FindControl("btnNextStep") as ImageButton;
            ImageButton FinishBackStep = this.WizardData.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishBackStep") as ImageButton;

            StartStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            BackStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");
            NextStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_next.png", "step_previous.png");
            FinishBackStep.ImageUrl = "~/images/Wizard_Image/" + General.Msg("step_previous.png", "step_next.png");

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("10");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                CtrlCs.FillGridEmpty(ref grdItmEmpIDs, 50);
                FillList();

                ViewState["CommandName"] = "";
                ViewState["PeriodCondetion"] = "";
                /*** Common Code ************************************/
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
            CtrlCs.FillMachineList(ref ddlItmMacIDs, null, false, false, "A", "IT");
            CtrlCs.FillMachineList(ref ddlItmMacIDs_WZ, rvItmMacIDs_WZ, false, false, "A", "IT");
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery + " WHERE ItmID = ItmID ";

        if (ddlFilter.SelectedIndex > 0)
        {
            UIClear();
            //if (ddlFilter.Text == "WktID")
            //{
            //    sql = MainQuery + " AND " + ddlFilter.SelectedValue + " = @P1 ";
            //    cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            //}
            //else
            {
                sql += " AND " + ddlFilter.SelectedValue + " LIKE @P1 ";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
            }
        }

        UIClear();
        BtnStatus("10");
        UIEnabled(false);
        UIClear_WZ();
        MultiView1.ActiveViewIndex = 0;
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
            spnItmNameEn_WZ.Visible = true;
        }

        if (pgCs.LangAr)
        {
            spnItmNameAr_WZ.Visible = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool status)
    {
        txtID.Enabled = txtID.Visible = false;
        txtEmpIDs.Enabled = txtEmpIDs.Visible = false;
        txtItmNameEn.Enabled      = false;
        txtItmNameAr.Enabled      = false;
        calItmDate.SetEnabled(status);
        tpItmTimeFrom.Enabled     = false;
        tpItmTimeTo.Enabled       = false;
        ddlItmMacType.Enabled     = false;
        ddlItmMacIDs.Enabled      = false;
        chkItmIsSend.Enabled      = false;
        chkItmIsProcess.Enabled   = false;
        txtItmDescription.Enabled = false;

        calItmDate_WZ.SetEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtItmNameEn_WZ.Text)) { ProCs.ItmNameEn = txtItmNameEn_WZ.Text; }
            if (!string.IsNullOrEmpty(txtItmNameAr_WZ.Text)) { ProCs.ItmNameAr = txtItmNameAr_WZ.Text; }
            ProCs.ItmDate = calItmDate_WZ.getGDateDBFormat();
            ProCs.ItmTimeFrom = tpItmTimeFrom_WZ.getDateTime(calItmDate_WZ.getGDate()).ToString();
            ProCs.ItmTimeTo = tpItmTimeTo_WZ.getDateTime(calItmDate_WZ.getGDate()).ToString();

            ProCs.ItmMacType = ddlItmMacType_WZ.SelectedValue;
            if (ddlItmMacIDs_WZ.SelectedIndex > 0) { ProCs.ItmMacIDs = ddlItmMacIDs_WZ.SelectedValue; }
            ProCs.ItmIsSend = false;
            ProCs.ItmIsProcess = "0";

            DataTable EmployeeInsertdt = ucEmployeeSelected_WZ.EmpSelected;
            if (!DBCs.IsNullOrEmpty(EmployeeInsertdt)) { ProCs.ItmEmpIDs = GenCs.CreateIDsNumber("EmpID", EmployeeInsertdt); }
            if (!string.IsNullOrEmpty(txtItmDescription_WZ.Text)) { ProCs.ItmDescription = txtItmDescription_WZ.Text; }

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";

        txtID.Text = "";
        txtEmpIDs.Text = "";
        txtItmNameEn.Text = "";
        txtItmNameAr.Text = "";
        calItmDate.ClearDate();
        tpItmTimeFrom.ClearTime();
        tpItmTimeTo.ClearTime();
        ddlItmMacType.SelectedIndex = 0;
        if (ddlItmMacIDs.Items.Count > 0) { ddlItmMacIDs.SelectedIndex = 0; } 
        chkItmIsSend.Checked = false;
        chkItmIsProcess.Checked = false;
        txtItmDescription.Text = "";

        CtrlCs.FillGridEmpty(ref grdItmEmpIDs, 50);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UIClear_WZ()
    {
        txtItmNameEn_WZ.Text = "";
        txtItmNameAr_WZ.Text = "";
        //calItmDate_WZ.ClearDate();
        tpItmTimeFrom_WZ.ClearTime();
        tpItmTimeTo_WZ.ClearTime();
        ddlItmMacType_WZ.SelectedIndex = 0;
        if (ddlItmMacIDs_WZ.Items.Count > 0) { ddlItmMacIDs_WZ.SelectedIndex = 0; } 
        txtItmDescription_WZ.Text = "";

        ucEmployeeSelected_WZ.Clear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlItmMacType_WZ_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItmMacType_WZ.SelectedValue == "1")
        {
            DivMac1_WZ.Visible = DivMac2_WZ.Visible = rvItmMacIDs_WZ.Enabled = false;
            if (ddlItmMacIDs_WZ.Items.Count > 0) { ddlItmMacIDs_WZ.SelectedIndex = 0; } 
        }
        else if (ddlItmMacType_WZ.SelectedValue == "2") { DivMac1_WZ.Visible = DivMac2_WZ.Visible = rvItmMacIDs_WZ.Enabled = true; }
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
            UIClear_WZ();
            UIEnabled(false);
            BtnStatus("01");
            ViewState["CommandName"] = "ADD";
            MultiView1.ActiveViewIndex = 1;
            WizardData.ActiveStepIndex = 0;
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
        UIClear();
        UIClear_WZ();
        MultiView1.ActiveViewIndex = 0;
        BtnStatus("10");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnReSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsEnableResend()) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Error, General.Msg("Can not resend", "لا يمكن إعادة الإرسال")); return; }

            string from = tpItmTimeFrom.getHours().ToString("00") + ":" + tpItmTimeFrom.getMinutes().ToString("00") + ":" + tpItmTimeFrom.getSeconds().ToString("00");
            string to = tpItmTimeTo.getHours().ToString("00") + ":" + tpItmTimeTo.getMinutes().ToString("00") + ":" + tpItmTimeTo.getSeconds().ToString("00");
            SendSMS(txtID.Text, from, to,txtEmpIDs.Text);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SendSMS(string ID, string from, string to, string EmpIDs)
    {
        string msgEn = "عزيزي الموظف ,يجب أن تقوم بإثبات وجودك من الساعة " + from + " إلى الساعة " +  to;
        string msgAr = "Dear employee, you must prove your presence from the hour " + from + " to hour " +  to;

        string res = SmsSendCs.SendSMS(msgEn, msgAr, EmpIDs);
        if (res == "1")
        {
            ProCs.ItmID = ID.ToString();
            ProCs.ItmIsSend = true;
            ProCs.TransactionBy = pgCs.LoginID;
            SqlCs.Update_IsSend(ProCs);
            btnReSend.Enabled = false;
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Sent Succesfully", "تم الإرسال بنجاح"));
        }
        else
        {
            string error = SmsSendCs.SendResult(res);
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Error, General.Msg("Sending failed" + "-" + error, "لم تتم عملية الإرسال" + "-" + error));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnReProsess_Click(object sender, EventArgs e)
    {
        try
        {
            string Prosess = "0";
            if (chkItmIsProcess.Checked) { Prosess = "1";}
            if (!IsEnableReProsess(Prosess)) { CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Error, General.Msg("Can not be reprocessed", "لا يمكن إعادة المعالجة")); return; }

            ProCs.ItmID = txtID.Text;
            ProCs.ItmIsProcess = "2";
            ProCs.TransactionBy = pgCs.LoginID;
            SqlCs.Update_IsProcess(ProCs);
            btnReProsess.Enabled = false;
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Updated, and will be reprocessed soon", "تم التحديث ,وستتم إعادة المعالجة قريبا"));
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled = GenCs.FindStatus(Status[0]);
        btnCancel.Enabled = GenCs.FindStatus(Status[1]);

        btnReSend.Enabled = btnReProsess.Enabled = false;

        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
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
        btnFilter_Click(null, null);
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

        GridView gridView = (GridView)sender;
        int cellIndex = -1;
        foreach (DataControlField field in gridView.Columns)
        {
            if (ViewState["SortExpression"] != null)
            {
                if (field.SortExpression == Convert.ToString(ViewState["SortExpression"]))
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                    break;
                }
            }
        }

        if (cellIndex > -1)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //this is a header row,set the sort style
                e.Row.Cells[cellIndex].CssClass += (sortDirection == "ASC" ? " sortasc" : " sortdesc");
            }
        }
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
                    string[] IDs = e.CommandArgument.ToString().Split(',');
                    string ID = IDs[0];
                    bool IsSend = Convert.ToBoolean(IDs[1]);

                    if (!IsSend)
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }

                    SqlCs.Delete(ID, pgCs.LoginID);

                    btnFilter_Click(null, null);

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
        btnFilter_Click(null, null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UIClear();
            UIEnabled(false);
            BtnStatus("10");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                BtnStatus("11");
                PopulateUI(grdData.SelectedRow.Cells[1].Text);
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
            DataRow[] DRs = DT.Select("ItmID =" + pID + "");

            txtID.Text = DRs[0]["ItmID"].ToString();
            txtItmNameEn.Text = DRs[0]["ItmNameEn"].ToString();
            txtItmNameAr.Text = DRs[0]["ItmNameAr"].ToString();
            calItmDate.SetGDate(DRs[0]["ItmDate"], pgCs.DateFormat);
            if (DRs[0]["ItmTimeFrom"] != DBNull.Value) { tpItmTimeFrom.SetTime(Convert.ToDateTime(DRs[0]["ItmTimeFrom"])); }
            if (DRs[0]["ItmTimeTo"] != DBNull.Value) { tpItmTimeTo.SetTime(Convert.ToDateTime(DRs[0]["ItmTimeTo"])); }

            ddlItmMacType.SelectedIndex = ddlItmMacType.Items.IndexOf(ddlItmMacType.Items.FindByValue(DRs[0]["ItmMacType"].ToString()));
            ddlItmMacIDs.SelectedIndex = ddlItmMacIDs.Items.IndexOf(ddlItmMacIDs.Items.FindByValue(DRs[0]["ItmMacIDs"].ToString()));
            if (ddlItmMacType.SelectedValue == "1") { DivMac1.Visible = DivMac2.Visible = false; } else if (ddlItmMacType.SelectedValue == "2") { DivMac1.Visible = DivMac2.Visible = true; }

            if (DRs[0]["ItmIsSend"] != DBNull.Value) { chkItmIsSend.Checked = Convert.ToBoolean(DRs[0]["ItmIsSend"]); } else { chkItmIsSend.Checked = false; }
            if (DRs[0]["ItmIsProcess"] != DBNull.Value)
            {
                if (DRs[0]["ItmIsProcess"].ToString() == "0") { chkItmIsProcess.Checked = false; } else { chkItmIsProcess.Checked = true; }
            } else { chkItmIsProcess.Checked = false; }

            Hashtable Permht = (Hashtable)ViewState["ht"];
            if (IsEnableResend()) { btnReSend.Enabled = Permht.ContainsKey("Resend"); }
            if (IsEnableReProsess(DRs[0]["ItmIsProcess"].ToString())) { btnReProsess.Enabled = Permht.ContainsKey("Reprosess"); }

            txtItmDescription.Text = DRs[0]["ItmDescription"].ToString();

            string EmpIDs = DRs[0]["ItmEmpIDs"].ToString();
            txtEmpIDs.Text = EmpIDs;
            string IsSend = (chkItmIsSend.Checked) ? "1" : "0";

            StringBuilder QI = new StringBuilder();
            QI.Append(" SELECT DISTINCT I.EmpID, " + General.Msg("E.EmpNameEn", "E.EmpNameAr") + " EmpName, 'True' isAttend, IsProcess ");
            QI.Append(" FROM InspectionToursTrans I, EmployeeMasterInfoView E WHERE I.EmpID = E.EmpID AND ItmID = @P1 ");
            QI.Append(" UNION ALL "); 
            QI.Append(" SELECT I.ID1, " + General.Msg("E.EmpNameEn", "E.EmpNameAr") + " EmpName, 'False' isAttend, " + IsSend + " IsProcess ");
            QI.Append(" FROM UF_CSVToTable1Col('" + EmpIDs + "') I,  EmployeeMasterInfoView E ");
            QI.Append(" WHERE I.ID1 = E.EmpID AND I.ID1 NOT IN (SELECT EmpID FROM InspectionToursTrans WHERE ItmID = @P1) "); 

            DataTable EDT = DBCs.FetchData(QI.ToString(), new string[] { txtID.Text });
            if (!DBCs.IsNullOrEmpty(EDT))
            {
                grdItmEmpIDs.DataSource = (DataTable)EDT;
                grdItmEmpIDs.DataBind();
            }
            else
            {
                CtrlCs.FillGridEmpty(ref grdItmEmpIDs, 50);
            }
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayIsProcess(object pIS)
    {
        try
        {
            if (pIS.ToString() == "1") { return General.Msg("Yes", "نعم"); }
            else if (pIS.ToString() == "0") { return General.Msg("No", "لا"); }
            else if (pIS.ToString() == "2") { return General.Msg("Reprocessing", "إعادة معالجة"); }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool IsEnableResend()
    {
        try
        {
            if (chkItmIsSend.Checked) { return false; }

            int iItmDate = DTCs.ConvertDateTimeToInt(calItmDate.getGDate(), "Gregorian");
            int iNowDate = DTCs.ConvertDateTimeToInt(DTCs.GDateNow("dd/MM/yyyy"), "Gregorian");
            if (iNowDate != iItmDate) { return false; }


            int iItmTime = tpItmTimeFrom.getIntTime();
            int iNowTime = DTCs.iTimeNow();

            if (iItmTime <= iNowTime) { return false; }

            return true;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return false;  }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool IsEnableReProsess(string IsProcess)
    {
        try
        {
            if (IsProcess != "1") { return false; }

            DataTable DT = DBCs.FetchData(" SELECT EmpID FROM InspectionToursTrans WHERE IsProcess = 0 AND ItmID = @P1 ", new string[] { txtID.Text });
            if (DBCs.IsNullOrEmpty(DT)) { return false; }

            return true;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); return false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Wizard Events

    protected void WizardData_PreRender(object sender, EventArgs e)
    {
        Repeater SideBarList = WizardData.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
        SideBarList.DataSource = WizardData.WizardSteps;
        SideBarList.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string GetClassForWizardStep(object wizardStep)
    {
        WizardStep step = wizardStep as WizardStep;
        if (step == null) { return ""; }
        int stepIndex = WizardData.WizardSteps.IndexOf(step);
        if (stepIndex < WizardData.ActiveStepIndex) { return "prevStep"; } else if (stepIndex > WizardData.ActiveStepIndex) { return "nextStep"; } else { return "currentStep"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void WizardData_ActiveStepChanged(object sender, EventArgs e)
    {
        int step = WizardData.ActiveStepIndex;
        if (step > 0 && step < WizardData.WizardSteps.Count - 1)
        {
            WebControl stepNavTemplate = this.WizardData.FindControl("StepNavigationTemplateContainerID") as WebControl;
            if (stepNavTemplate != null)
            {
                ImageButton save = stepNavTemplate.FindControl("btnNextStep") as ImageButton;
                if (save != null)
                {
                    save.ValidationGroup = "vgStep" + step.ToString();
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnStartStep_Click(object sender, EventArgs e)
    {
        if (!CtrlCs.PageIsValid(this, vsStart)) { return; }
        ucEmployeeSelected_WZ.SetConditionIN(" SELECT EmpID FROM TransDump WHERE CONVERT(VARCHAR(12),TrnDate,103)= CONVERT(VARCHAR(12),GETDATE(),103) ");
        WizardData.ActiveStepIndex += 1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnBackStep_Click(object sender, EventArgs e) { WizardData.ActiveStepIndex -= 1; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnNextStep_Click(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFinishBackStep_Click(object sender, EventArgs e)
    {
        WizardData.ActiveStepIndex -= 1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFinishStep_Click(object sender, EventArgs e) { Save_WZ(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Save_WZ()
    {
        try
        {
            if (!Page.IsValid) { return; }
            
            FillPropeties();
            int ID = SqlCs.Insert(ProCs);
            CtrlCs.ShowSaveMsg(this);

            ////////////////////
            string from = tpItmTimeFrom_WZ.getHours().ToString("00") + ":" + tpItmTimeFrom_WZ.getMinutes().ToString("00") + ":" + tpItmTimeFrom_WZ.getSeconds().ToString("00");
            string to   = tpItmTimeTo_WZ.getHours().ToString("00") + ":" + tpItmTimeTo_WZ.getMinutes().ToString("00") + ":" + tpItmTimeTo_WZ.getSeconds().ToString("00");

            SendSMS(ID.ToString(), from, to, ProCs.ItmEmpIDs);

            btnFilter_Click(null, null);
            
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void NameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvItmNameEn_WZ))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvItmNameEn_WZ, false, General.Msg("Name (En) Is Required", "الاسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtItmNameEn_WZ.Text)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(txtItmNameEn_WZ.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvItmNameEn_WZ, true, General.Msg("Entered English Name exist already,Please enter another name", "الاسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM InspectionToursMaster WHERE ItmNameEn = @P1 ", new string[] { txtItmNameEn_WZ.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            else if (source.Equals(cvItmNameAr_WZ))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvItmNameAr_WZ, false, General.Msg("Name (Ar) Is Required", "الاسم العربي مطلوب"));
                    if (string.IsNullOrEmpty(txtItmNameAr_WZ.Text)) { e.IsValid = false; }
                }

                if (!string.IsNullOrEmpty(txtItmNameAr_WZ.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvItmNameAr_WZ, true, General.Msg("Entered Arabic Name exist already,Please enter another name", "الاسم العربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));
                    DataTable DT = DBCs.FetchData("SELECT * FROM InspectionToursMaster WHERE ItmNameAr = @P1 ", new string[] { txtItmNameAr_WZ.Text });
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
    protected void Time_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {          
            if (source.Equals(cvTime_WZ))
            {
                if (tpItmTimeFrom_WZ.getIntTime() >= 0 && tpItmTimeTo_WZ.getIntTime() >= 0)
                {
                    int FromTime = tpItmTimeFrom_WZ.getIntTime();
                    int ToTime   = tpItmTimeTo_WZ.getIntTime();

                    if (FromTime >= 2400) { FromTime = FromTime - 2400; }
                    if (ToTime   >= 2400) { ToTime   = ToTime   - 2400; }

                    //if (chkWktShift1IsOverNight.Checked) { if (FromTime > ToTime) { e.IsValid = true; } else { e.IsValid = false; } }
                    //else { if (ToTime > FromTime) { e.IsValid = true; } else { e.IsValid = false; } }
                    CtrlCs.ValidMsg(this, ref cvTime_WZ, true, General.Msg("End time must be greater than start time", "وقت الإنتهاء يجب أن يكون أكبر من وقت البداية"));
                    if (ToTime < FromTime) { e.IsValid = false; }

                    if (e.IsValid)
                    {
                        CtrlCs.ValidMsg(this, ref cvTime_WZ, true, General.Msg("The start time should be greater than the current time", "وقت البداية يجب أن يكون أكبر من الوقت الحالي"));
                        int cTime = DTCs.iTimeNow();
                        if (ToTime <= cTime) { e.IsValid = false; }
                    }

                    if (e.IsValid)
                    {
                        string mPeriod = "0";
                        int iPeriodSec = 0;
                        if (string.IsNullOrEmpty(ViewState["PeriodCondetion"].ToString())) { iPeriodSec = GetPeriodCondetion(); } else { iPeriodSec = Convert.ToInt32(ViewState["PeriodCondetion"]); }
                        if (iPeriodSec > 0) { mPeriod = (iPeriodSec / 60).ToString(); }

                        CtrlCs.ValidMsg(this, ref cvTime_WZ, true, General.Msg("The period between start and end time should not exceed " + mPeriod + " minutes", "الفترة بين وقت البداية و النهاية يجب أن لا تتجاوز " + mPeriod + " دقيقة/دقائق"));

                        int FromTimeSec = tpItmTimeFrom_WZ.getTimeInSeconds();
                        int ToTimeSec   = tpItmTimeTo_WZ.getTimeInSeconds();

                        if ((ToTimeSec - FromTimeSec) > iPeriodSec) { e.IsValid = false; }
                    }

                }
                else { e.IsValid = false; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected int GetPeriodCondetion()
    {
        int iPeriodSec = 0;

        DataTable DT = DBCs.FetchData(new SqlCommand("SELECT CfgPeriodDifferenceInInspectionTours FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (!GenCs.IsNullOrEmptyDB(DT.Rows[0][0])) { iPeriodSec = Convert.ToInt32(DT.Rows[0][0]); }
        }

        return iPeriodSec;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}