using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using Elmah;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Globalization;

public partial class EmployeeExcusePermit : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpExcPermPro ProCs = new EmpExcPermPro();
    EmpExcPermSql SqlCs = new EmpExcPermSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM EmployeeMasterInfoView WHERE EmpStatus = '1' ";
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
            
            MainQuery += " AND DepID IN (" + pgCs.DepList + ") ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("000");
                UIEnabled(false);
                //UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                ViewState["CommandName"] = "";
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
            CtrlCs.FillExcuseTypeList(ref ddlExcType, rfvddlExcType, true, true);
            CtrlCs.FillVacationTypeList(ref ddlVacType, rfvddlVacType, true, true, "LIC");
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindStatus(object pEmpID)
    {
        try
        {
            DateTime NowDate = DTCs.ConvertToDatetime(DTCs.GDateNow(), "Gregorian");
            DataTable DT = DBCs.FetchData("SELECT * FROM TransDump WHERE EmpID = @P1 AND CONVERT(VARCHAR(12),TrnDate,103) = CONVERT(VARCHAR(12),@P2,103) ", new string[] { pEmpID.ToString(), NowDate.ToString("dd/MM/yyyy") });
            if (!DBCs.IsNullOrEmpty(DT)) { return General.Msg("Present","حاضر"); } else { return General.Msg("Absent","غائب"); }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return "";
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
            UIClear();

            if (ddlFilter.Text == "EmpID") 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " = @P1";
                cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            }
            else 
            { 
                sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1";
                cmd.Parameters.AddWithValue("@P1", "%" + txtFilter.Text.Trim() + "%");
            }
        }

        UIClear();
        BtnStatus("000");
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
            
        }
        else
        {
           
        }

        if (pgCs.LangAr)
        {
           
        }
        else
        {
           
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        //txtEmpID.Enabled = pStatus;
        rblExprType.Enabled = pStatus;
        ddlExcType.Enabled = pStatus;
        ddlVacType.Enabled = pStatus;
        chkVacNoPresent.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
        rblPeriodExc.Enabled = pStatus;
        ddlShiftID.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.EmpID = txtID.Text; }

            ProCs.EmpID = txtEmpID.Text;
            ProCs.ExprType = rblExprType.SelectedValue.ToString();
            if (divExcType.Visible) { ProCs.ExcID = ddlExcType.SelectedValue.ToString(); ProCs.ExcType = rblPeriodExc.SelectedValue.ToString(); }
            if (divVacType.Visible) { ProCs.VtpID = ddlVacType.SelectedValue.ToString(); }

            // Now we will always save Shift by '1'
            // We updated to choose shift
            ProCs.ShiftID = ddlShiftID.SelectedValue;

            if (chkVacNoPresent.Checked) { ProCs.ExprVacIfNoPresent = true; } else { ProCs.ExprVacIfNoPresent = false; }

            string Today = DTCs.GDateNow();
            ProCs.ExprStartDate = DTCs.ToDBFormat(Today, "Gregorian");
            ProCs.ExprEndDate   = DTCs.ToDBFormat(Today, "Gregorian"); 
            ProCs.ExprDesc = txtDesc.Text;
        
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
        txtEmpID.Text = "";
        try { rblExprType.ClearSelection(); } catch { }
        ddlExcType.SelectedIndex = -1;
        ddlVacType.SelectedIndex = -1;
        chkVacNoPresent.Checked = false;
        txtDesc.Text = "";
        ddlShiftID.Items.Clear();
        try { rblPeriodExc.ClearSelection(); } catch { }

        UIVisible("Vac");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rblExprType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UIVisible(rblExprType.SelectedValue);
        if (rblExprType.SelectedValue == "Vac") { ddlVacType.SelectedIndex = 1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIVisible(string pType)
    {
        if      (pType == "Exc") { divExcType.Visible = true; divVacType.Visible = false; divCheckNoPresent.Visible = false; }
        else if (pType == "Vac") { divExcType.Visible = false; divCheckNoPresent.Visible = false; divVacType.Visible = true; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlExcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlExcType.SelectedIndex > 0 && ddlExcType.SelectedIndex == 1) { divCheckNoPresent.Visible = true; }
        //else { divCheckNoPresent.Visible = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkVacNoPresent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVacNoPresent.Checked) { divVacType.Visible = true; ddlVacType.Enabled = true; }
        else { divVacType.Visible = false; ddlVacType.Enabled = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rblPeriodExc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblPeriodExc.SelectedValue == "BL") { divCheckNoPresent.Visible = true; }
        else { divCheckNoPresent.Visible = false; }
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
        //UIClear();
        try { rblExprType.ClearSelection(); } catch { }
        ddlExcType.SelectedIndex = -1;
        ddlShiftID.SelectedIndex = -1;
        ddlVacType.SelectedIndex = -1;
        chkVacNoPresent.Checked = false;
        txtDesc.Text = "";
        try { rblPeriodExc.ClearSelection(); } catch { }

        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        //ViewState["CommandName"] = "EDIT";
        //UIEnabled(true);
        //BtnStatus("0011");
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

            if (commandName == "ADD")
            {
                SqlCs.Insert(ProCs);
            }

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UIClear();
        BtnStatus("000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
        
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
                        //ImageButton delBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                        //Hashtable ht = (Hashtable)ViewState["ht"];
                        //if (ht.ContainsKey("Delete")) { delBtn.Enabled = true; } else { delBtn.Enabled = false; }
                        //delBtn.Attributes.Add("OnClick", General.MsgDelete());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }
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
            BtnStatus("000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                PopulateUI(grdData.SelectedRow.Cells[0].Text);
                
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
            DataRow[] DRs = DT.Select("EmpID =" + pID + "");

            txtID.Text    = DRs[0]["EmpID"].ToString();
            txtEmpID.Text = DRs[0]["EmpID"].ToString().Trim();
            if (FillShiftName())
            {
                BtnStatus("100");
            }
            else
            {
                BtnStatus("000");
                CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("This employee does not have a working time for the current day, can not add an administrative license", "هذا الموظف لا يملك وقت عمل لليوم الحالي، لا يمكن إضافة رخصة إدارية"));
            }
        }
        catch (Exception ex)
        {
            BtnStatus("000");
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
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
    protected bool FillShiftName()
    {
        ddlShiftID.Items.Clear();
        DateTime NowDate = DTCs.ConvertToDatetime(DTCs.GDateNow(), "Gregorian");
        string MQ = " SELECT WktID FROM EmpWrkRel WHERE EmpID = @P1 AND ISNULL(EwrDeleted,0) = 0 "
                  + " AND @P2 BETWEEN EwrStartDate AND EwrEndDate "
                  + " AND (((DATEPART(DW, @P2) = 1) AND (EwrSun = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 2) AND (EwrMon = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 3) AND (EwrTue = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 4) AND (EwrWed = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 5) AND (EwrThu = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 6) AND (EwrFri = 1)) OR "
                  + " ((DATEPART(DW, @P2) = 7) AND (EwrSat = 1)))"
                  + " ORDER BY EwrID DESC";

        DataTable DT = DBCs.FetchData(MQ, new string[] { txtEmpID.Text, NowDate.ToString() });
        if (!DBCs.IsNullOrEmpty(DT)) 
        { 
            string Q = " SELECT WktNameAr,WktNameEn,WktShift1NameAr,WktShift1NameEn,WktShift2NameAr,WktShift2NameEn,WktShift3NameAr,WktShift3NameEn "
                        + " ,WktShift1From,WktShift1To,WktShift2From,WktShift2To,WktShift3From,WktShift3To"
                        + " FROM WorkingTime WHERE WktID = @P1 ";
             DataTable WDT = DBCs.FetchData(Q, new string[] { DT.Rows[0]["WktID"].ToString() });

            if (!DBCs.IsNullOrEmpty(WDT)) 
            { 
                //ddlShiftID.Items.Add(new ListItem(General.Msg("-Select Shift-", "-اختر الوردية-")));
                for (int i = 1; i <= 3; i++)
                {
                    string ID = i.ToString();
                    if (WDT.Rows[0]["WktShift" + ID + "NameAr"] != DBNull.Value || WDT.Rows[0]["WktShift" + ID + "NameEn"] != DBNull.Value)
                    {
                        if (!string.IsNullOrEmpty(WDT.Rows[0]["WktShift" + ID + "NameAr"].ToString()) || !string.IsNullOrEmpty(WDT.Rows[0]["WktShift" + ID + "NameEn"].ToString()))
                        {
                            string strShiftName = string.Empty;

                            strShiftName = General.Msg(WDT.Rows[0]["WktShift" + ID + "NameEn"].ToString(),WDT.Rows[0]["WktShift" + ID + "NameAr"].ToString());
                            ddlShiftID.Items.Add(new ListItem(strShiftName, ID));
                        }
                    }
                }
                //rfvddlShiftID.InitialValue = ddlShiftID.Items[0].Text;
                ddlShiftID.SelectedIndex = 0;
            }

            return true;
        }
        else
        {
            return false;
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

    protected void CheckExcuseEmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        DateTime NowDate = DTCs.ConvertToDatetime(DTCs.GDateNow(), "Gregorian");

        try
        {
            if (source.Equals(cvCheckExcuseEmpID) && !string.IsNullOrEmpty(txtEmpID.Text))
            {
                if (rblExprType.SelectedValue == "Vac")
                {
                    DataTable DT = DBCs.FetchData("SELECT * FROM EmpExcPermRel WHERE EmpID = @P1 AND @P2 BETWEEN ExprStartDate AND ExprEndDate ", new string[] { txtEmpID.Text, NowDate.ToString("MM/dd/yyyy") });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ValidMsg(this, ref cvCheckExcuseEmpID, true, General.Msg("This employee already has license or permission for today, can not add other permit", "هذا الموظف لدية رخصة أو استئذان لهذا اليوم، لايمكن اضافة رخصة أخرى"));
                        e.IsValid = false;
                    }
                    else 
                    { 
                        bool Have = HaveTrans(txtEmpID.Text);
                        if (Have)
                        {
                            CtrlCs.ValidMsg(this, ref cvCheckExcuseEmpID, true, General.Msg("There are Transaction on the date specified Please choose another date", "يوجد حركات في التاريخ المحدد الرجاء اختيار تاريخ آخر"));
                            e.IsValid = false;
                        }
                    }
                }
                else if (rblExprType.SelectedValue == "Exc")
                {
                    if (ddlExcType.SelectedIndex > 0)
                    {
                        string pExprType = rblExprType.SelectedValue.ToString();

                        DataTable DT1 = DBCs.FetchData("SELECT * FROM EmpExcPermRel WHERE EmpID = @P1 AND @P2 BETWEEN ExprStartDate AND ExprEndDate AND ExprType = 'Vac' ", new string[] { txtEmpID.Text, NowDate.ToString("MM/dd/yyyy") });
                        if (!DBCs.IsNullOrEmpty(DT1))
                        {
                            CtrlCs.ValidMsg(this, ref cvCheckExcuseEmpID, true, General.Msg("This employee already has license for today, can not add other Excuse", "هذا الموظف لدية رخصة لهذا اليوم، لايمكن اضافة استئذان آخر"));
                            e.IsValid = false;
                            return;
                        }
                        
                        DataTable DT2 = DBCs.FetchData("SELECT * FROM EmpExcPermRel WHERE EmpID = @P1 AND @P2 BETWEEN ExprStartDate AND ExprEndDate AND ExprType = @P3 AND ExcType = @P4 ", new string[] { txtEmpID.Text, NowDate.ToString("MM/dd/yyyy"), pExprType,rblPeriodExc.SelectedValue });
                        if (!DBCs.IsNullOrEmpty(DT2))
                        {
                            CtrlCs.ValidMsg(this, ref cvCheckExcuseEmpID, true, General.Msg("This employee already has same Excuse Type for today, can not add other Excuse", "هذا الموظف لدية استئذان لنفس النوع لهذا اليوم، لايمكن اضافة استئذان آخر"));
                            e.IsValid = false;
                        }

                        else { e.IsValid = true; }
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
    public bool HaveTrans(string pEmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            StringBuilder Q = new StringBuilder();
            Q.Append(" SELECT TrnDate FROM Trans WHERE ISNULL(TrnFlagOT, 0) IN (0,1,2) AND EmpID = @P1 AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101) ");
            Q.Append(" UNION ");
            Q.Append(" SELECT TrnDate FROM TransDump WHERE ISNULL(TrnFlagOT, 0) IN (0,1,2) AND EmpID = @P1 AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101) ");

            DataTable DT = DBCs.FetchData(Q.ToString(), new string[] { pEmpID });
            if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception e1)
        {
            return false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
}