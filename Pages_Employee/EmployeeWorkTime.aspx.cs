using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class EmployeeWorkTime : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpWrkPro ProCs = new EmpWrkPro();
    EmpWrkSql SqlCs = new EmpWrkSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    DataTable dt;
    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM EmployeeWorkTimeRelInfoView WHERE ISNULL(EwrWrkDefault,0) = 0";
    string WhereQuery = "";
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
            WhereQuery = " ISNULL(EwrWrkDefault,0) = 0 AND DepID IN (" + pgCs.DepList + ") ";

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                
                hfSearchCriteria.Value = WhereQuery;
                FillGrid();

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
            CtrlCs.FillWorkingTimeList(ddlWktID, rvWktID, true);
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
        string sql = MainQuery;

        if (ddlFilter.SelectedIndex > 0 && !string.IsNullOrEmpty(txtFilter.Text.Trim()))
        {
            if (ddlFilter.Text == "EmpID") 
            {
                hfSearchCriteria.Value = WhereQuery + " AND " + ddlFilter.SelectedItem.Value + " = '" + txtFilter.Text.Trim() + "'";
            }
            else 
            { 
                hfSearchCriteria.Value = WhereQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE '%" + txtFilter.Text.Trim() + "%'";
            }         
        }
        else
        {
            hfSearchCriteria.Value = WhereQuery;
        }

        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        grdData.SelectedIndex = -1;
        FillGrid();
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
            grdData.Columns[6].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameEn").Enabled = false;
            ddlFilter.Items.FindByValue("WktNameEn").Enabled = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[5].Visible = false;
            ddlFilter.Items.FindByValue("EmpNameAr").Enabled = false;
            ddlFilter.Items.FindByValue("WktNameAr").Enabled = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtID.Enabled = txtID.Visible = false;
        txtEmpID.Enabled = pStatus;
        ddlWktID.Enabled = pStatus;        
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
        dclDays.Enabled(pStatus);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.EwrID = txtID.Text; }

            ProCs.EmpIDs = txtEmpID.Text;
            if (ddlWktID.SelectedIndex > 0) { ProCs.WktID = ddlWktID.SelectedValue; }

            ProCs.EwrStartDate = calStartDate.getGDateDBFormat();
            if (!string.IsNullOrEmpty(calEndDate.getGDate())) { ProCs.EwrEndDate = calEndDate.getGDateDBFormat(); } else { ProCs.EwrEndDate = calStartDate.getGDateDBFormat(); }

            ProCs.EwrSun = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Sunday);
            ProCs.EwrMon = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Monday);
            ProCs.EwrTue = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Tuesday);
            ProCs.EwrWed = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Wednesday);
            ProCs.EwrThu = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Tharsday);
            ProCs.EwrFri = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Friday);
            ProCs.EwrSat = dclDays.GetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Saturday);

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
        ddlWktID.SelectedIndex = -1;
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        dclDays.Clear();

        ddlWktID.Show(DDLAttributes.DropDownListAttributes.ShowType.ALL);
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
        ddlWktID.Show(DDLAttributes.DropDownListAttributes.ShowType.ActiveOnly);
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        DataTable DT = DBCs.FetchData(" SELECT EwrID FROM Trans WHERE EwrID = @P1 ", new string[] { txtID.Text });
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if (DT.Rows.Count > 0 ) 
            { 
                CtrlCs.ShowMsg(this,CtrlFun.TypeMsg.Validation, General.Msg("You can not Update worktime in the specified period of existence of previous Transaction for Employee ", "لا يمكن تعديل وقت العمل في الفترة المحددة لوجود حركات سابقة فيها للموظف "));
                return;
            }
        }

        ViewState["CommandName"] = "EDIT";
        string oldval = ddlWktID.SelectedValue;
        ddlWktID.Show(DDLAttributes.DropDownListAttributes.ShowType.ActiveOnly);
        ddlWktID.SelectedIndex = ddlWktID.Items.IndexOf(ddlWktID.Items.FindByValue(oldval));

        UIEnabled(true);
        txtEmpID.Enabled   = false;
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

            string SaveIDs    = "";
            string notSaveIDs = "";

            if      (commandName == "ADD")  { SqlCs.Wkt_Insert_WithMoveBack(ProCs, out SaveIDs, out notSaveIDs); } 
            else if (commandName == "EDIT") { SqlCs.Wkt_Update_WithMoveBack(ProCs, out SaveIDs, out notSaveIDs); } 

            if (!string.IsNullOrEmpty(notSaveIDs)) { CtrlCs.ShowMsg(this,CtrlFun.TypeMsg.Validation, General.Msg(@"You can not add\update worktime in the specified period of existence of previous Transaction for Employee ", @"لا يمكن إضافة/تعديل وقت عمل في الفترة المحددة لوجود حركات سابقة فيها للموظف")); } 

            if (!string.IsNullOrEmpty(SaveIDs)) 
            { 
                btnFilter_Click(null, null); 
                CtrlCs.ShowSaveMsg(this);
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

    protected void grdData_DataBound(object sender, EventArgs e)
    {
        if (grdData.Rows.Count > 0)
        {
            if (ViewState["PageSize"] != null)
            {
                DropDownList _ddlPager = CtrlCs.PagerList(grdData);
                _ddlPager.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
            }
        }
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
        ViewState["PageSize"] = ((DropDownList)sender).SelectedValue;
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
                    
                    DataTable DT = DBCs.FetchData("SELECT * FROM Trans WHERE EwrID = @P1 ", new string[] { ID });
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
        //grdData.PageIndex = e.NewPageIndex;
        //grdData.SelectedIndex = -1;
        //btnFilter_Click(null,null);
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
            DataRow[] DRs = DT.Select("EwrID =" + pID + "");

            txtID.Text      = DRs[0]["EwrID"].ToString();
            txtEmpID.Text   = DRs[0]["EmpID"].ToString();
            ddlWktID.SelectedIndex = ddlWktID.Items.IndexOf(ddlWktID.Items.FindByValue(DRs[0]["WktID"].ToString()));

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            calStartDate.SetGDate(DRs[0]["EwrStartDate"], pgCs.DateFormat);
            calEndDate.SetGDate(DRs[0]["EwrEndDate"], pgCs.DateFormat);

            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Sunday   , Convert.ToInt32(DRs[0]["EwrSun"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Monday   , Convert.ToInt32(DRs[0]["EwrMon"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Tuesday  , Convert.ToInt32(DRs[0]["EwrTue"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Wednesday, Convert.ToInt32(DRs[0]["EwrWed"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Tharsday , Convert.ToInt32(DRs[0]["EwrThu"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Friday   , Convert.ToInt32(DRs[0]["EwrFri"]));
            dclDays.SetDayValue(AlmaalimControl.DaysChekboxlist.DaysEnum.Saturday , Convert.ToInt32(DRs[0]["EwrSat"]));
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillGrid()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        try
        {
            grdData.PageIndex = 0;
            grdData.DataSource = null;
            grdData.DataSourceID = "odsGrdData";
            HfRefresh.Value = "T";
            grdData.DataBind();
            HfRefresh.Value = "F";
        }
        catch (Exception ex) { }

        if (grdData.Rows.Count == 0)
        {
            grdData.PageIndex = 0;
            grdData.DataSourceID = "";
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}