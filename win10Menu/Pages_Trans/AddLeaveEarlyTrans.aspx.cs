using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;
using System.Collections;

public partial class AddLeaveEarlyTrans : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    TransDumpPro ProCs = new TransDumpPro();
    TransDumpSql SqlCs = new TransDumpSql();
    ////SendEmail sendEmail = new SendEmail();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    //string MainQuery = " SELECT * FROM Branch WHERE ISNULL(BrcDeleted,0) = 0 ";
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
                BtnStatus("1000");
                UIEnabled(false);
                UILang();
                CtrlCs.FillGridEmpty(ref grdData, 50);
                FillList();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                txtTodayDate.Text = DTCs.FindCurrentDate();
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
            CtrlCs.FillMachineList(ref ddlLocation, rfvLocation, false, false, "O");
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string FindDep()
    {
        string DepID = "";

        DataTable DT = DBCs.FetchData(" SELECT DepID FROM Employee WHERE EmpID = @P1 ", new string[] { txtEmpID.Text });
        if (!DBCs.IsNullOrEmpty(DT)) { DepID = DT.Rows[0]["DepID"].ToString(); }

        return DepID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ExecuteProc()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        SqlConnection con = new SqlConnection(General.ConnString);
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand();
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandText = "spOrderTransactionToday";
        da.SelectCommand.Connection = con;

        da.SelectCommand.Parameters.Add(new SqlParameter("@ipFromDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateTime.Now.ToString("MM/dd/yyyy")));
        da.SelectCommand.Parameters.Add(new SqlParameter("@ipUsrName" , SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pgCs.LoginID));
        da.SelectCommand.Parameters.Add(new SqlParameter("@ipDepIDSTR", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FindDep()));

        con.Open();
        da.SelectCommand.ExecuteNonQuery();
        con.Close();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        //UIClear();
        BtnStatus("1000");
        UIEnabled(false);
        CtrlCs.FillGridEmpty(ref grdData, 50);
        if (!Page.IsValid) { return; }
        
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Search()
    {
        try
        {
            if (txtEmpIDSearch.Text.Trim().Length > 1) { txtEmpID.Text = (txtEmpIDSearch.Text.Split('-'))[0].ToString(); }
            else { txtEmpID.Text = (txtEmpNameSearch.Text.Split('-'))[1].ToString(); }
        }
        catch { return; }
        
        SqlCommand cmd = new SqlCommand();
        StringBuilder FQ = new StringBuilder();

        if (!string.IsNullOrEmpty(txtEmpID.Text))
        {
            ExecuteProc();

            FQ.Append(" SELECT TrnDate, TrnTime, EmpID, MacID, TrnType, UsrName, TrnDesc ");
            FQ.Append(" ,(SELECT MacLocationEn FROM Machine WHERE MacID = e.MacID) AS LocationEn ");
            FQ.Append(" ,(SELECT MacLocationAr FROM Machine WHERE MacID = e.MacID) AS LocationAr ");
            FQ.Append(" FROM TransDumpToday e ");
            FQ.Append(" WHERE EmpID = @EmpID ");
            FQ.Append(" AND CONVERT(CHAR(10), e.TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101)");
            FQ.Append(" AND UsrProcess = @LoginID ");
            FQ.Append(" AND ISNULL(TrnDesc,'') != 'Ignore'");
            FQ.Append(" ORDER BY TrnTime ");

            cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
            cmd.Parameters.AddWithValue("@LoginID", pgCs.LoginID);
        
            grdData.SelectedIndex = -1;
            cmd.CommandText = FQ.ToString();
            FillGrid(cmd);
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
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[3].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtEmpID.Enabled          = false;
        txtEmpName.Enabled        = false;
        calDate.SetEnabled(false);
        ddlType.Enabled           = false;
        tpickerTime.Enabled       = pStatus;
        ddlLocation.Enabled       = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            ProCs.EmpID    = txtEmpID.Text;
            ProCs.TrnDate  = calDate.getGDateDefFormat();
            ProCs.TrnTime  = tpickerTime.getDateTime().ToString();
            ProCs.TrnType  = ddlType.SelectedValue;
            ProCs.MacID    = ddlLocation.SelectedValue;
            ProCs.UsrName  = pgCs.LoginID;
            ProCs.TrnAddBy = "OE";
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        txtEmpID.Text   = "";
        txtEmpName.Text = "";
        calDate.ClearDate();
        ddlType.ClearSelection();
        tpickerTime.ClearTime();
        ddlLocation.ClearSelection();
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
        ViewState["CommandName"] = "ADD";
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e) { }
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

            if (commandName == "ADD") { SqlCs.Insert(ProCs); }

            string TransTime = string.Format("{0:00}", tpickerTime.getHours()) + ":" + string.Format("{0:00}", tpickerTime.getMinutes()) + ":" + string.Format("{0:00}", tpickerTime.getSeconds());
            ////sendEmail.SendAddTransMailToEmp(txtEmpID.Text, calDate.getGDateDefFormat(), TransTime, pgCs.Lang);  

            Search();
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
        //UIClear();
        tpickerTime.ClearTime();
        ddlLocation.ClearSelection();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        //btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
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
                        e.Row.Cells[6].Visible = false; //To hide ID column in grid view
                        e.Row.Cells[7].Visible = false; //To hide ID column in grid view
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
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }
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
        grdData.PageIndex = e.NewPageIndex;
        grdData.SelectedIndex = -1;
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID) { }
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

    protected void TpickerTime_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvtpickerTime))
            {
                if (tpickerTime.getHours() == -1 || tpickerTime.getMinutes() == -1 || tpickerTime.getSeconds() == -1) 
                { 
                    CtrlCs.ValidMsg(this, ref cvtpickerTime, false, General.Msg("Time is required", "الوقت مطلوب"));
                    e.IsValid = false; 
                }
                else 
                {
                    StringBuilder SQ = new StringBuilder();
                    SQ.Append(" SELECT TrnTime FROM TransDumpToday WHERE EmpID = @P1 ");
                    SQ.Append(" AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101)");
                    SQ.Append(" AND ISNULL(TrnDesc,'') != 'Ignore'"); 
                    SQ.Append(" ORDER BY TrnTime DESC");

                    DataTable DT = DBCs.FetchData(SQ.ToString(), new string[] { txtEmpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        DateTime TrnTime = (DateTime)DT.Rows[0]["TrnTime"];
                        string LastTime = string.Format("{0:00}", TrnTime.Hour)  + string.Format("{0:00}", TrnTime.Minute) + string.Format("{0:00}", TrnTime.Second);
                        
                        int OutTime = tpickerTime.getIntTime(); 
                        if ( Convert.ToInt64(LastTime) >= OutTime ) 
                        { 
                            CtrlCs.ValidMsg(this, ref cvtpickerTime, true, General.Msg("You must be a OUT time is greater than the IN time of the last IN Transaction", "يجب أن يكون وقت الخروج أكبر من وقت الدخول  في أخر حركة  دخول"));
                            e.IsValid = false; 
                        }
                    } 
                    else { e.IsValid = false; }
                }            
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FindEmp_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvFindEmp))
            {
                if (!string.IsNullOrEmpty(txtEmpIDSearch.Text))
                {
                    UIClear();
                    txtEmpID.Text = (txtEmpIDSearch.Text.Split('-'))[0].ToString();
                    
                    DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { txtEmpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        txtEmpName.Text = General.Msg(DT.Rows[0]["EmpNameEn"].ToString(), DT.Rows[0]["EmpNameAr"].ToString());
                        calDate.SetTodayDate();
                        e.IsValid = true;
                    }
                    else { e.IsValid = false; UIClear(); }
                }
                else { e.IsValid = true; }
            }

            else if (source.Equals(cvFindEmpName))
            {
                if (!string.IsNullOrEmpty(txtEmpNameSearch.Text))
                {
                    UIClear();
                    txtEmpID.Text = (txtEmpNameSearch.Text.Split('-'))[1].ToString();
                    
                    DataTable DT = DBCs.FetchData(" SELECT EmpID,EmpNameAr,EmpNameEn FROM Employee WHERE EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { txtEmpID.Text });
                    if (!DBCs.IsNullOrEmpty(DT))
                    {
                        txtEmpName.Text = General.Msg(DT.Rows[0]["EmpNameEn"].ToString(), DT.Rows[0]["EmpNameAr"].ToString());
                        calDate.SetTodayDate();
                        e.IsValid = true;
                    }
                    else { e.IsValid = false; UIClear(); }
                }
                else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IsAdd_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            //if (!string.IsNullOrEmpty(txtEmpID.Text))
            //{
            //    StringBuilder SQ = new StringBuilder();
            //    SQ.Append(" SELECT TrnType ");
            //    SQ.Append(" FROM TransDumpToday ");
            //    SQ.Append(" WHERE EmpID = @P1 ");
            //    SQ.Append(" AND CONVERT(CHAR(10), TrnDate, 101) = CONVERT(CHAR(10), GETDATE(), 101)");
            //    SQ.Append(" AND ISNULL(TrnDesc,'') != 'Ignore'"); 
            //    SQ.Append(" ORDER BY TrnTime DESC");

            //    DataTable DT = DBCs.FetchData(SQ.ToString(), new string[] { txtEmpID.Text });
            //    if (!DBCs.IsNullOrEmpty(DT))
            //    {
            //        string TrnType = DT.Rows[0]["TrnType"].ToString();
            //        if (TrnType != "1") { e.IsValid = false; }
            //    }
            //    else { e.IsValid = false; }
            //}
            //else { e.IsValid = false; }
        }
        catch { e.IsValid = false; }
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

