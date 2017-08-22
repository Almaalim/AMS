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

public partial class Pages_Admin_TransactionLog : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun DTCs = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";

    string MainQuery = " SELECT * FROM TransactionLogInfoView ";
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
                BtnStatus("1");
                UIEnabled(true);
                UILang();
                FillList();
                btnFilter_Click(null, null);
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
        DataTable DT1 = DBCs.FetchData(new SqlCommand(" SELECT DISTINCT LogTableName, TableNameEn, TableNameAr FROM TransactionLogInfoView "));
        if (!DBCs.IsNullOrEmpty(DT1))
        {
            CtrlCs.PopulateDDL(ddlLogTableName, DT1, General.Msg("TableNameEn", "TableNameAr"), "LogTableName", General.Msg("-Select Table-", "-اختر الجدول-"));
        }

        DataTable DT2 = DBCs.FetchData(new SqlCommand(" SELECT DISTINCT LogTransactionBy FROM TransactionLogInfoView "));
        if (!DBCs.IsNullOrEmpty(DT2))
        {
            CtrlCs.PopulateDDL(ddlLogTransactionBy, DT2, "LogTransactionBy", "LogTransactionBy", General.Msg("-Select User-", "-اختر المستخدم-"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            bool find = false;
            string sql = MainQuery + " WHERE LogID = LogID ";

            if (ddlLogTableName.SelectedIndex > 0)
            {
                sql += " AND LogTableName = @TableName";
                cmd.Parameters.AddWithValue("@TableName", ddlLogTableName.SelectedValue);
                find = true;
            }

            if (ddlLogTransactionType.SelectedIndex > 0)
            {
                sql += " AND LogTransactionType = @TransactionType";
                cmd.Parameters.AddWithValue("@TransactionType", ddlLogTransactionType.SelectedValue);
                find = true;
            }

            if (ddlLogTransactionBy.SelectedIndex > 0)
            {
                sql += " AND LogTransactionBy = @TransactionBy";
                cmd.Parameters.AddWithValue("@TransactionBy", ddlLogTransactionBy.SelectedValue);
                find = true;
            }

            if (!string.IsNullOrEmpty(calStartDate.getGDate()))
            {
                find = true;
                DateTime fromDate = DTCs.ConvertToDatetime(calStartDate.getGDate(), "Gregorian");
                DateTime ToDate   = fromDate;

                if (!string.IsNullOrEmpty(calEndDate.getGDate()))
                {
                    ToDate = DTCs.ConvertToDatetime(calEndDate.getGDate(), "Gregorian");
                }
                
                sql += " AND LogTransactionDate BETWEEN @FromDate AND @ToDate";
                cmd.Parameters.AddWithValue("@FromDate", String.Format("{0:MM/dd/yyyy}", fromDate) + " 00:00:00");
                cmd.Parameters.AddWithValue("@ToDate", String.Format("{0:MM/dd/yyyy}", ToDate) + " 23:59:59");
            }
            
            BtnStatus("1");
            grdData.SelectedIndex = -1;
            if (find) { cmd.CommandText = sql; } else { cmd.CommandText = MainQuery + " WHERE LogID = -1 "; }
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
        }

        if (pgCs.LangAr)
        {

        }
        else
        {
            grdData.Columns[1].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        calStartDate.SetEnabled(pStatus);
        calEndDate.SetEnabled(pStatus);
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void BtnStatus(string Status) //string pBtn = [Search]
    {
        //Hashtable Permht = (Hashtable)ViewState["ht"];
        btnFilter.Enabled = GenCs.FindStatus(Status[0]);

        //if (Status[0] != '0') { btnFilter.Enabled = Permht.ContainsKey("Search"); }
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
                        //e.Row.Cells[1].Visible = false;
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
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
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
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e) { }
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
    public static string GrdDisplayType(object ID)
    {
        try
        {
            if      (ID.ToString() == "I") { return General.Msg("Add", "إضافة"); }
            else if (ID.ToString() == "U") { return General.Msg("Update", "تعديل"); }
            else if (ID.ToString() == "D") { return General.Msg("Delete", "حذف"); }
            else { return string.Empty; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
}