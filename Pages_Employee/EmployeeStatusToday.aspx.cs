using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
public partial class Pages_Employee_EmployeeStatusToday : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            CtrlCs.RefreshGridEmpty(ref grdData);
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                UILang();
                FillGrid("DEP", pgCs.DepList);
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Today Status Events

    protected void btnDepFilter_Click(object sender, ImageClickEventArgs e)
    {
        //if (ddlDepFilter.SelectedIndex > 0) { FillGrid(ddlDepFilter.SelectedValue); } else { FillGrid(departmentList); }

        if (string.IsNullOrEmpty(txtSearchByDep.Text)) { FillGrid("DEP", pgCs.DepList); }
        else
        {
            if (ddlFilter.SelectedIndex == 0)
            {
                string[] Deps = txtSearchByDep.Text.Split('-');
                DataTable DT = DBCs.FetchData("SELECT DepID FROM Department WHERE ISNULL(DepDeleted, 0) = 0 AND " + General.Msg("DepNameEn", "DepNameAr") + " = @P1 ", new string[] { Deps[0] });
                if (!DBCs.IsNullOrEmpty(DT)) { FillGrid("DEP", DT.Rows[0][0].ToString()); } else { CtrlCs.FillGridEmpty(ref grdData, 50); }
            }
            else if (ddlFilter.SelectedIndex == 1)
            {
                string[] Deps = txtSearchByDep.Text.Split('-');
                if (!string.IsNullOrEmpty(Deps[0])) { FillGrid("EMP", Deps[0]); } else { CtrlCs.FillGridEmpty(ref grdData, 50); }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public string FindStatus(object Status)
    {
        try
        {
            return DisplayFun.GrdDisplayDayStatus(Status, pgCs.Version);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFilter.SelectedIndex == 0)
        {
            auDepName.ServiceMethod = "GetDepNameList";
        }
        else if (ddlFilter.SelectedIndex == 1)
        {
            auDepName.ServiceMethod = "GetEmployeeIDList";
        }
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
            grdData.Columns[2].Visible = false;
            grdData.Columns[4].Visible = false;
        }

        if (pgCs.LangAr)
        {

        }
        else
        {
            grdData.Columns[1].Visible = false;
            grdData.Columns[3].Visible = false;
        }
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
        btnDepFilter_Click(null, null);
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
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        btnDepFilter_Click(null, null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid(string Type, string ListID) //Type In EMP OR DEP
    {
        DataTable GDT = new DataTable();
        GDT = DBCs.FetchProcedureData("spTodayEmployeeStatus", new string[] { "@iFilter", "@iIDList" }, new string[] { Type, ListID });
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


        //StringBuilder FQ = new StringBuilder();
        //FQ.Append(" SELECT EmpID, EmpNameEn, EmpNameAr, DepNameEn, DepNameAr ");
        //FQ.Append(" FROM EmployeeMasterActiveInfoView ");
        //if (Type == "DEP") { FQ.Append(" WHERE DepID IN ( " + ListID + " )"); }
        //if (Type == "EMP") { FQ.Append(" WHERE EmpID IN ( '" + ListID + "' )"); }

        ////DataTable GDT = DBCs.FetchData(FQ.ToString(), new string[] {}, DepList.Split(','));
        //DataTable GDT = DBCs.FetchData(new SqlCommand(FQ.ToString()));
        //if (!DBCs.IsNullOrEmpty(GDT))
        //{
        //    grdData.DataSource = (DataTable)GDT;
        //    ViewState["grdDataDT"] = (DataTable)GDT;
        //    grdData.DataBind();
        //}
        //else
        //{
        //    CtrlCs.FillGridEmpty(ref grdData, 50);
        //}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}