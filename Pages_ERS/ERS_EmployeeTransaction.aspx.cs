using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;

public partial class ERS_EmployeeTransaction : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
 
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
            MainQuery = "SELECT * FROM TransInfoView WHERE TrnShift IS NOT NULL AND EmpID = '" + pgCs.LoginEmpID + "'";
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckERSLicense();  
                /*** Common Code ************************************/

                DTCs.YearPopulateList(ref ddlYear);
                DTCs.MonthPopulateList(ref ddlMonth);

                string CurrentMonth = DTCs.FindCurrentMonth();
                string CurrentYear  = DTCs.FindCurrentYear();

                FillData(CurrentMonth, CurrentYear);
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(CurrentMonth));
                ddlYear.SelectedIndex  = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(CurrentYear));
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillData(string Month, string Year)
    {
        try
        {
            Literal1.Text = General.Msg(Convert.ToInt32(Month).ToString("00") + "/" + Year.ToString(), Convert.ToInt32(Month).ToString("00") + "/" + Year.ToString());

            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery;

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            FQ.Append(MainQuery);
            FQ.Append(" AND TrnDate BETWEEN @SDate AND @EDate ");
            FQ.Append(" ORDER BY TrnDate,TrnShift,TrnTime ");
                
            cmd.Parameters.AddWithValue("@SDate", SDate);
            cmd.Parameters.AddWithValue("@EDate", EDate);
            sql = FQ.ToString();

            cmd.CommandText = sql;
            FillGrid(cmd);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string convertDayToArabic(string day)
    {
        string arDay = "";
        switch (day)
        {
            case "Sat":
                arDay = "السبت";
                break;
            case "Sun":
                arDay = "الأحد";
                break;
            case "Mon":
                arDay = "الأثنين";
                break;
            case "Tue":
                arDay = "الثلاثاء";
                break;
            case "Wed":
                arDay = "الأربعاء";
                break;
            case "Thu":
                arDay = "الخميس";
                break;
            case "Fri":
                arDay = "الجمعة";
                break;
        }
        return arDay;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GrdDisplayDayName(object pGDate)
    {
        try
        {
            GregorianCalendar Grn = new GregorianCalendar();
            string dayNameEn = Grn.GetDayOfWeek(Convert.ToDateTime(pGDate)).ToString();
            string dayNameAr = convertDayToArabic(Convert.ToDateTime(pGDate).ToString("ddd"));

            return General.Msg(dayNameEn, dayNameAr);
        }
        catch (Exception ex) 
        { 
            ErrorSignal.FromCurrentContext().Raise(ex); 
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((ddlMonth.SelectedIndex >= 0) && (ddlYear.SelectedIndex >= 0)) { FillData(ddlMonth.SelectedValue, ddlYear.SelectedValue); }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }   
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}