using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Elmah;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class Pages_Employee_TestEmployeeExcuseDay : BasePage
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
    
    string MainQuery = "";
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

            MainQuery = "SELECT * FROM EmployeeExcuseDayRelInfoView WHERE DepID IN (" + pgCs.DepList + ") ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);   
                UILang();

                /**********/
                hfSearchCriteria.Value = " DepID IN (" + pgCs.DepList + ") ";
                FillGrid();
                /**********/
                
                //FillGrid(new SqlCommand(MainQuery));
                //CtrlCs.FillGridEmpty(ref grdData, 50); 
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    /**********/
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            if (ddlFilter.Text == "EmpID") 
            {
                hfSearchCriteria.Value = " DepID IN (" + pgCs.DepList + ") AND " + ddlFilter.SelectedItem.Value + " = '" + txtFilter.Text.Trim() + "'";

                //sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " = @P1";
                //cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim());
            }
            else 
            { 
                hfSearchCriteria.Value = " DepID IN (" + pgCs.DepList + ") AND " + ddlFilter.SelectedItem.Value + " LIKE '%" + txtFilter.Text.Trim() + "%'";

                //sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1";
                //cmd.Parameters.AddWithValue("@P1", "%" + txtFilter.Text.Trim() + "%");
            }         
        }
        else
        {
            hfSearchCriteria.Value = " DepID IN (" + pgCs.DepList + ") ";
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        //cmd.CommandText = sql;
        //FillGrid(cmd);

        /**********/
        FillGrid();
        /**********/

        //grdData.DataBind();
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
        //ddlWktID.Enabled = pStatus;
        calStartDate.SetEnabled(pStatus);
        tpStartTime.Enabled = pStatus;
        tpEndTime.Enabled = pStatus;
        txtDesc.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.ExrID = txtID.Text; }

            ProCs.EmpID           = txtEmpID.Text;
            if (ddlExcType.SelectedIndex > 0) { ProCs.ExcID = ddlExcType.SelectedValue; }         
            ProCs.ExrStartDate = calStartDate.getGDateDBFormat();
            ProCs.ExrEndDate   = calStartDate.getGDateDBFormat();
            
            ProCs.ExrStartTime = tpStartTime.getDateTime(calStartDate.getGDateDefFormat()).ToString();
            ProCs.ExrEndTime   = tpEndTime.getDateTime(calStartDate.getGDateDefFormat()).ToString();
            
            ProCs.ExrDesc = txtDesc.Text;
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
        
        txtID.Text    = "";
        txtEmpID.Text = "";
        txtDesc.Text  = "";
        calStartDate.ClearDate();
        tpStartTime.ClearTime();
        tpEndTime.ClearTime();
        
        ddlExcType.Show(DDLAttributes.DropDownListAttributes.ShowType.ALL);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void txtEmpID_TextChanged(object sender, EventArgs e) { }

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
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");
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

            if (commandName == "ADD") { SqlCs.Day_Insert_WithUpdateSummary(ProCs); }

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
        
        //if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
        //if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void PopulateUI(string pID)
    {
        try
        {
            DataTable DT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = DT.Select("ExrID =" + pID + "");

            txtID.Text    = DRs[0]["ExrID"].ToString();
            txtEmpID.Text = DRs[0]["EmpID"].ToString();
            
            ddlExcType.SelectedIndex = ddlExcType.Items.IndexOf(ddlExcType.Items.FindByValue(DRs[0]["ExcID"].ToString()));
           
            txtDesc.Text = DRs[0]["ExrDesc"].ToString();
            if (DRs[0]["ExrStartTime"] != DBNull.Value) { tpStartTime.SetTime(Convert.ToDateTime(DRs[0]["ExrStartTime"])); }
            if (DRs[0]["ExrEndTime"] != DBNull.Value)   { tpEndTime.SetTime(Convert.ToDateTime(DRs[0]["ExrEndTime"])); }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            calStartDate.SetGDate(DRs[0]["ExrStartDate"], pgCs.DateFormat);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected void FillGrid(SqlCommand cmd)
    //{
    //    //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
    //    //DataTable GDT = DBCs.FetchData(cmd);
    //    //if (!DBCs.IsNullOrEmpty(GDT))
    //    //{
    //    //    grdData.DataSource = (DataTable)GDT;
    //    //    ViewState["grdDataDT"] = (DataTable)GDT;
    //    //    grdData.DataBind();
    //    //}
    //    //else
    //    //{
    //    //    CtrlCs.FillGridEmpty(ref grdData, 50);
    //    //}
    //}

    private void FillGrid()
    {
        try
        {
            grdData.PageIndex = 0;
            grdData.DataSource = null;
            grdData.DataSourceID = "odsGrdData";
            grdData.DataBind();
        }
        catch (Exception ex) { }

        if (grdData.Rows.Count == 0)
        {
            grdData.PageIndex = 0;
            grdData.DataSourceID = "";
            CtrlCs.FillGridEmpty(ref grdData, 50);
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

    protected void EmpID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtEmpID.Text.Trim()))
            {
                CtrlCs.ValidMsg(this, ref cvEmpID, false, General.Msg("Emloyee ID is required", "رقم الموظف مطلوب"));
                e.IsValid = false;
            }
            else
            {
                CtrlCs.ValidMsg(this, ref cvEmpID, true, General.Msg("Employee ID does not exist", "رقم الموظف غير موجود"));
                if (!GenCs.isEmpID(txtEmpID.Text.Trim(), pgCs.DepList)) { e.IsValid = false; }
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
                if (!tpStartTime.isEmpty() && !tpEndTime.isEmpty())
                {
                    int FromTime = tpStartTime.getIntTime();
                    int ToTime   = tpEndTime.getIntTime();

                    if (FromTime > ToTime) { e.IsValid = false; }
                }
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
    protected void grdData_DataBound(object sender, EventArgs e)
    {
        //DataPager pager = grdData.BottomPagerRow.FindControl("pager") as DataPager;

        if (grdData.Rows.Count > 0)
        {
            //pager.Visible = true;
    
            if (ViewState["PageSize"] != null)
            {
                DropDownList _ddlPager = CtrlCs.PagerList(grdData);

                //DropDownList ddPagesize = grdData.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                _ddlPager.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
            }

            //Label lblCount = grdData.BottomPagerRow.FindControl("lblPageCount") as Label;
            //int totRecords = (grdData.PageIndex * grdData.PageSize) + grdData.PageSize;
            //int totCustomerCount = AdvWorksDB.GetCustomersCount(hfSearchCriteria.Value);

            //totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
            //lblCount.Text = ((grdData.PageIndex * grdData.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totCustomerCount.ToString();
            //grdData.BottomPagerRow.Visible = true;
        }
        else
        {
            //pager.Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        // handle event
        DropDownList ddpagesize = sender as DropDownList;
        grdData.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
        ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
        grdData.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                        _ddlPager.SelectedIndexChanged += new EventHandler(ddlPager2_SelectedIndexChanged);
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
    void ddlPager2_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        ViewState["PageSize"] = ((DropDownList)sender).SelectedValue;

        //grdData.PageIndex = 0;
        //btnFilter_Click(null, null);
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
                        if (ht.ContainsKey("Delete"))
                        { delBtn.Enabled = true; }
                        else
                        { delBtn.Enabled = false; }
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

                    SqlCs.Day_Delete_WithUpdateSummary(ID, pgCs.LoginID);

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
        //DataTable DT = DBCs.FetchData(new SqlCommand(MainQuery));
        //if (!DBCs.IsNullOrEmpty(DT))
        //{
        //    DataView dataView = new DataView(DT);

        //    if (ViewState["SortDirection"] == null)
        //    {
        //        ViewState["SortDirection"] = "ASC";
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }
        //    else
        //    {
        //        sortDirection = Convert.ToString(ViewState["SortDirection"]);
        //        sortDirection = ConvertSortDirectionToSql(sortDirection);
        //        ViewState["SortDirection"] = sortDirection;
        //        ViewState["SortExpression"] = Convert.ToString(e.SortExpression);
        //    }

        //    dataView.Sort = e.SortExpression + " " + sortDirection;

        //    grdData.DataSource = dataView;
        //    grdData.DataBind();
        //}
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
                //CtrlCs.FillGridEmpty(ref grdData, 50);
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
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void odsGrdData_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DataTable DT = (DataTable)e.OutputParameters["DT"]; 
        if (DT != null) { ViewState["grdDataDT"] = DT; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}