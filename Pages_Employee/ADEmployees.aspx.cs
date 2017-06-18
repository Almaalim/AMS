using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Elmah;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;

public partial class ADEmployees : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProCs = new EmployeePro();
    EmployeeSql SqlCs = new EmployeeSql();

    PageFun pgCs = new PageFun();
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    string MainQuery = " SELECT * FROM spActiveEmployeeView WHERE ISNULL(EmpDeleted,0) = 0 ";
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
                /*** Check AMS License ***/
                pgCs.CheckAMSLicense();
                /*** get Permission    ***/
                ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("10");
                UILang();
                /*** Common Code ************************************/

                string ADJoinMethod = ActiveDirectoryFun.getJoinMethod(ActiveDirectoryFun.ADTypeEnum.EMP);
                if (ADJoinMethod == "EmpID") { grdData.Columns[0].Visible = true; }
                if (ADJoinMethod == "ID") { grdData.Columns[4].Visible = true; }
                if (ADJoinMethod == "Email") { grdData.Columns[3].Visible = true; }

                FillGrid(new SqlCommand(MainQuery));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ButtonAction Events

    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ADDT = (DataTable)ViewState["grdDataDT"];
            if (!DBCs.IsNullOrEmpty(ADDT))
            {
                UsersPro AD = ActiveDirectoryFun.FillAD(ActiveDirectoryFun.ADTypeEnum.EMP, "", "", "");
                if (!AD.ADValid)
                {
                    CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, AD.ADError);
                    return;
                }

                for (int i = 0; i < ADDT.Rows.Count; i++)
                {
                    if (AD.ADJoinMethod == "EmpID") { AD.ADVal = ADDT.Rows[i]["EmpID"].ToString(); }
                    if (AD.ADJoinMethod == "ID") { AD.ADVal = ADDT.Rows[i]["EmpNationalID"].ToString(); }
                    if (AD.ADJoinMethod == "Email") { AD.ADVal = ADDT.Rows[i]["EmpEmailID"].ToString(); }

                    ADDT.Rows[i]["EmpADUser"] = ActiveDirectoryFun.GetAD(AD);
                    ADDT.AcceptChanges();
                }

                grdData.DataSource = (DataTable)ADDT;
                ViewState["grdDataDT"] = (DataTable)ADDT;
                grdData.DataBind();

                BtnStatus("11");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            DataTable ADDT = (DataTable)ViewState["grdDataDT"];
            DataRow[] DRs = ADDT.Select("EmpADUser <> '' OR EmpADUser IS NOT NULL");

            foreach (DataRow DR in DRs)
            {
                ProCs.EmpID = DR["EmpID"].ToString();
                ProCs.EmpADUser = DR["EmpADUser"].ToString();
                ProCs.TransactionBy = pgCs.LoginID;

                SqlCs.Employee_Update_ADUser(ProCs);
            }

            BtnStatus("10");
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
    protected void BtnStatus(String pBtn) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnFind.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
        btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
        if (pBtn[0] != '0') { btnFind.Enabled = Permht.ContainsKey("Update"); }
        if (pBtn[1] != '0')
        {
            btnSave.Enabled = Permht.ContainsKey("Update");
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

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        if (!DBCs.IsNullOrEmpty((DataTable)ViewState["grdDataDT"]))
        {
            grdData.DataSource = (DataTable)ViewState["grdDataDT"];
            grdData.DataBind();
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
                case DataControlRowType.DataRow: { break; }
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
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdData.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        grdData.PageIndex = 0;
        if (!DBCs.IsNullOrEmpty((DataTable)ViewState["grdDataDT"]))
        {
            grdData.DataSource = (DataTable)ViewState["grdDataDT"];
            grdData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand("SEELCT * FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0"));
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
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }
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

    protected void IsRepeat_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIsRepeat))
            {
                DataTable ADDT = (DataTable)ViewState["grdDataDT"];
                if (!DBCs.IsNullOrEmpty(ADDT))
                {
                    var result = from row in ADDT.AsEnumerable()
                                 group row by row.Field<string>("EmpADUser") into grp
                                 where (grp.Count() > 1 && !string.IsNullOrEmpty(grp.Key))
                                 select new { UsrID = grp.Key, UsrCount = grp.Count() };

                    bool repeat = false;
                    string ADrepeat = "";

                    foreach (var t in result)
                    {
                        repeat = true;
                        if (string.IsNullOrEmpty(ADrepeat)) { ADrepeat = t.UsrID; } else { ADrepeat += "," + t.UsrID; }
                    }

                    if (repeat) { e.IsValid = false; }

                    CtrlCs.ValidMsg(this, ref cvIsRepeat, true, General.Msg("Can not save, there repeating users : " + ADrepeat, "لا يمكن الحفظ, هناك مستخدمين مكررين : " + ADrepeat));
                }
                else
                {
                    e.IsValid = false;
                    CtrlCs.ValidMsg(this, ref cvIsRepeat, true, General.Msg("There is no user Save", "لا يوجد أي مستخدم للحفظ"));
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
}
