using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Elmah;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

public partial class EmployeeExcuse : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpExcRelPro ProCs = new EmpExcRelPro();
    EmpExcRelSql SqlCs = new EmpExcRelSql();

    EmpRequestSql ReqSqlCs = new EmpRequestSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM EmployeeExcusePeriodRelInfoView WHERE ExrID = ExrID ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            for (int i = 1; i <= 2; i++)
            {
                AjaxControlToolkit.AnimationExtender aniExtShow = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderShow" + i.ToString());
                AjaxControlToolkit.AnimationExtender aniExtClose = (AjaxControlToolkit.AnimationExtender)this.Master.FindControl("ContentPlaceHolder1").FindControl("AnimationExtenderClose" + i.ToString());
                ImageButton lnkShow = (ImageButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("lnkShow" + i.ToString());
                if (aniExtShow != null) { CtrlCs.Animation(ref aniExtShow, ref aniExtClose, ref lnkShow, i.ToString()); }
            }
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/
            
            MainQuery += " AND DepID IN (" + pgCs.DepList + ") ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                FillWorkTimeList(true);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillList()
    {
        try
        {
            CtrlCs.FillExcuseTypeList(ddlExcType, rvExcType, true);            
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillWorkTimeList(bool isAll)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT WktID, WktIsActive, WktNameAr, WktNameEn FROM WorkingTime WHERE ISNULL(WktDeleted, 0) = 0 AND WtpID IN (SELECT WtpID FROM WorkType WHERE WtpInitial !='RO') ");
        if (!isAll) {Q.Append(" AND WktIsActive = 'True' "); }
        if (!string.IsNullOrEmpty(txtEmpID.Text)) {Q.Append(" AND WktID IN (SELECT WktID FROM EmpWrkRel WHERE EmpID = @P1) "); }
        
        ddlWktID.Items.Clear();
        DataTable WDT = DBCs.FetchData(Q.ToString(), new string[] { txtEmpID.Text.Trim() });
        if (!DBCs.IsNullOrEmpty(WDT))
        {
            CtrlCs.PopulateDDL(ddlWktID, WDT, General.Msg("WktNameEn", "WktNameAr"), "WktID", General.Msg("-Select Work Time-", "-اختر وقت العمل-"));
            rfvddlWktID.InitialValue = ddlWktID.Items[0].Text;
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

        if (ddlFilter.SelectedIndex > 0 && !String.IsNullOrEmpty(txtFilter.Text.Trim()))
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
        BtnStatus("1000");
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
            grdData.Columns[3].Visible = false;
            grdData.Columns[5].Visible = false;
            grdData.Columns[7].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameEn").Enabled = false;
            ddlFilter.Items.FindByValue("ExcNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[4].Visible = false;
            grdData.Columns[6].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameAr").Enabled = false;
            ddlFilter.Items.FindByValue("ExcNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        txtEmpID.Enabled = pStatus;
        ddlExcType.Enabled = pStatus;
        ddlWktID.Enabled = pStatus;
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
        tpStartTime.Enabled = pStatus;
        tpEndTime.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
        chkExrIsOvernight.Enabled = pStatus;
        chkExrIsOT.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.ExrID = txtID.Text; }

            ProCs.EmpIDs = txtEmpID.Text;
            if (ddlExcType.SelectedIndex > 0) { ProCs.ExcID = ddlExcType.SelectedValue; }
            if (ddlWktID.SelectedIndex > 0) { ProCs.WktID = ddlWktID.SelectedValue; }

            ProCs.ExrStartDate = calStartDate.getGDateDBFormat();
            if (calEndDate.getGDate() != "") { ProCs.ExrEndDate = calEndDate.getGDateDBFormat(); } else { ProCs.ExrEndDate = calStartDate.getGDateDBFormat(); }
            
            ProCs.ExrStartTime = tpStartTime.getDateTime().ToString();
            ProCs.ExrEndTime = tpEndTime.getDateTime().ToString();
            
            ProCs.ExrDesc = txtDesc.Text;
            ProCs.ExrISOvernight = chkExrIsOvernight.Checked;
            ProCs.ExrIsOverTime = chkExrIsOT.Checked;

            ProCs.ExrIsStopped   = false;
            ProCs.ExrAddBy = "USR"; // 'USR' = FROM USER, 'REQ' = FROM Request, 'EXC' = FROM Execuse Premit, 'INT' = FROM Emport Data

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
        ddlExcType.SelectedIndex = -1;
        ddlWktID.SelectedIndex = -1;
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        tpStartTime.ClearTime();
        tpEndTime.ClearTime();
        txtDesc.Text = "";
        chkExrIsOvernight.Checked = false;
        chkExrIsOT.Checked = false;

        ddlExcType.Show(DDLAttributes.DropDownListAttributes.ShowType.ALL);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void txtEmpID_TextChanged(object sender, EventArgs e) { FillWorkTimeList(false); }

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
        ddlExcType.Show(DDLAttributes.DropDownListAttributes.ShowType.ActiveOnly);
        FillWorkTimeList(false);   
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        calStartDate.SetTodayDate();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        txtEmpID.Enabled = false;
        BtnStatus("0011");
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

            if (commandName == "ADD") { SqlCs.Period_Insert_WithUpdateSummary(ProCs); }

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
        BtnStatus("1000");
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
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM EmpExcRel WHERE ExrAddBy IN ('REQ', 'INT') AND ExrID = @P1 ", new string[] { ID });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        CtrlCs.ShowDelMsg(this, false);
                        return;
                    }
                    
                    SqlCs.Period_Delete_WithUpdateSummary(ID, pgCs.LoginID);

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
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("ExrID =" + pID + "");

            txtID.Text               = DRs[0]["ExrID"].ToString();
            txtEmpID.Text            = DRs[0]["EmpID"].ToString();
            
            ddlExcType.SelectedIndex = ddlExcType.Items.IndexOf(ddlExcType.Items.FindByValue(DRs[0]["ExcID"].ToString()));
            FillWorkTimeList(true);

            ddlWktID.SelectedIndex = ddlWktID.Items.IndexOf(ddlWktID.Items.FindByValue(DRs[0]["WktID"].ToString()));
            txtDesc.Text = DRs[0]["ExrDesc"].ToString();
            if (DRs[0]["ExrStartTime"] != DBNull.Value) { tpStartTime.SetTime(Convert.ToDateTime(DRs[0]["ExrStartTime"])); }
            if (DRs[0]["ExrEndTime"] != DBNull.Value)   { tpEndTime.SetTime(Convert.ToDateTime(DRs[0]["ExrEndTime"])); }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            calStartDate.SetGDate(DRs[0]["ExrStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["ExrEndDate"], pgCs.DateFormat);

            if (DRs[0]["ExrISOvernight"] != DBNull.Value) { chkExrIsOvernight.Checked = Convert.ToBoolean(DRs[0]["ExrISOvernight"]); }
            if (DRs[0]["ExrIsOverTime"] != DBNull.Value)  { chkExrIsOT.Checked = Convert.ToBoolean(DRs[0]["ExrIsOverTime"]); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(SqlCommand cmd)
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
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

    protected void FindEmp_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtEmpID.Text))
            {
                if(!GenCs.isEmpID(txtEmpID.Text)) { e.IsValid = false; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ExcuseTimeValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvExcuseTime))
            {
                int FromTime = tpStartTime.getIntTime();
                int ToTime = tpEndTime.getIntTime();

                if (FromTime <= 0 || ToTime <= 0 || FromTime > ToTime) { e.IsValid = false; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}