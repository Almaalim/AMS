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

public partial class PendingTransaction : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    TransDumpPro ProCs = new TransDumpPro();
    TransDumpSql SqlCs = new TransDumpSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();

    string sortDirection = "ASC";
    string sortExpression = "";
    
    string MainQuery = " SELECT * FROM TransInfoView ";
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
                BtnStatus("0000");
                UIEnabled(false);
                UILang();
                FillList();
                CtrlCs.FillGridEmpty(ref grdData, 50);
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                rfvType.InitialValue = ddlType.Items[0].Text;
                spnReqFile.Visible = CheckFileRequired("CTR");     
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
            DTCs.YearPopulateList(ref ddlYear);
            DTCs.MonthPopulateList(ref ddlMonth);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillLocationList(string Type)
    {
        CtrlCs.FillMachineList(ref ddlLocation, rvLocation, true, false, Type);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        string empString = txtEmployeeID.Text;
        string[] empstr = empString.Split('-');
        txtEmpID.Text = empstr[0];
        
        try
        {
            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery + " WHERE EmpID = '0' ";

            DateTime SDate = DateTime.Now;
            DateTime EDate = DateTime.Now;
            DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

            StringBuilder FQ = new StringBuilder();
            if (pgCs.Version == "GACA")
            {
                /******************************************************************************************************/
                /*For GACA AMS */
                FQ.Append(" SELECT e.TrnDate, e.TrnTime, e.EmpID, e.MacID, e.TrnType, e.UsrName ");
                FQ.Append(" , (select MacLocationEn from dbo.Machine where MacID = e.MacID) as MacLocationEn ");
                FQ.Append(" , (select MacLocationAr from dbo.Machine where MacID = e.MacID) as MacLocationAr "); 
                FQ.Append(" , (select EmpNameEn from dbo.Employee where EmpID = e.EmpID) as EmpNameEn "); 
                FQ.Append(" , (select EmpNameAr from dbo.Employee where EmpID = e.EmpID) as EmpNameAr "); 
                FQ.Append(" FROM Trans e,GACA_TransICCountVeiw V ");         
                FQ.Append(" WHERE (e.TrnDate = V.TrnDate AND e.EmpID = V.EmpID) ");
                FQ.Append(" AND e.TrnFlag IN ('IC','IG')");
                FQ.Append(" AND ISNULL(e.TrnIgnore,0) = 0");
                FQ.Append(" AND (e.TrnDate between '" + SDate + "' AND '" + EDate + "')");
                FQ.Append(" AND e.EmpID IN (SELECT EmpID FROM Employee WHERE DepID IN (" + pgCs.DepList + ") ) ");

                if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                {
                    FQ.Append(" AND EmpID = @EmpID");
                    cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
                }

                sql = FQ.ToString();
                /******************************************************************************************************/
            }
            else
            {
                /*####################################################################################################*/
                /*For Genral AMS */   
                FQ.Append(MainQuery);
                FQ.Append(" WHERE DepID IN (" + pgCs.DepList + ") ");
                FQ.Append(" AND (TrnDate BETWEEN @SDate AND @EDate)");
                FQ.Append(" AND (TrnFlag IN ('IC','IG')) AND ISNULL(TrnIgnore,0) = 0");
                
                if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                {
                    FQ.Append(" AND EmpID = @EmpID");
                    cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
                }

                cmd.Parameters.AddWithValue("@SDate", SDate);
                cmd.Parameters.AddWithValue("@EDate", EDate);
                sql = FQ.ToString();
                /*####################################################################################################*/
            }
          
            UIClear();
            BtnStatus("0000");
            UIEnabled(false);
            grdData.SelectedIndex = -1;
            cmd.CommandText = sql;
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
            grdData.Columns[7].Visible = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[1].Visible = false;
            grdData.Columns[8].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        ddlType.Enabled           = false;
        tpickerTime.Enabled       = pStatus;
        ddlLocation.Enabled       = pStatus;
        txtDesc.Enabled           = pStatus;
        fudReqFile.Enabled        = pStatus;
        calDate.SetEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.EmpIDs     = txtEmpID.Text;
            ProCs.TrnDate    = calDate.getGDateDBFormat();

            ProCs.TrnType   = ddlType.SelectedValue;
            ProCs.TrnTime   = tpickerTime.getDateTime().ToString();
            ProCs.MacID     = ddlLocation.SelectedItem.Value;
            ProCs.TrnReason = txtDesc.Text;
            ProCs.UsrName   = pgCs.LoginID;

            try
            {
                if (!string.IsNullOrEmpty(fudReqFile.PostedFile.FileName))
                {
                    string dateFile = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                    string FileName = System.IO.Path.GetFileName(fudReqFile.PostedFile.FileName);
                    string[] names = FileName.Split('.');
                    string NewFileName = txtEmpID.Text + "-" + dateFile + "-INATR." + names[1];
                    fudReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                    ProCs.TrnFilePath = NewFileName;
                }
            }
            catch(Exception ex) { }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        ViewState["CommandName"] = "";
        
        txtEmpID.Text = "";
        txtEmpName.Text = "";
        calDate.ClearDate();
        ddlType.ClearSelection();
        tpickerTime.ClearTime();
        ddlLocation.ClearSelection();
        tpickerTime.ClearTime();
        txtDesc.Text = "";
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

            if (commandName == "ADD") { SqlCs.Wizard_Trans_WithAttachment(ProCs); }
            else if (commandName == "EDIT") { }

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
        BtnStatus("0000");
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
                        e.Row.Cells[5].Visible = false;
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
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
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
            BtnStatus("0000");

            if (CtrlCs.isGridEmpty(grdData.SelectedRow.Cells[0].Text) && grdData.SelectedRow.Cells.Count == 1)
            {
                CtrlCs.FillGridEmpty(ref grdData, 50);
            }
            else
            {
                GridViewRow gvr = grdData.SelectedRow;
                
                BtnStatus("1100");
                txtEmpID.Text = gvr.Cells[0].Text;
                if (!string.IsNullOrEmpty(gvr.Cells[1].Text) && string.IsNullOrEmpty(gvr.Cells[2].Text))  { txtEmpName.Text = gvr.Cells[1].Text; }
                if (string.IsNullOrEmpty(gvr.Cells[1].Text)  && !string.IsNullOrEmpty(gvr.Cells[2].Text)) { txtEmpName.Text = gvr.Cells[2].Text; }
                if (!string.IsNullOrEmpty(gvr.Cells[1].Text) && !string.IsNullOrEmpty(gvr.Cells[2].Text))   { txtEmpName.Text = General.Msg (gvr.Cells[2].Text,gvr.Cells[1].Text); } 
               
                string value;
                Label lblDate = (Label)gvr.Cells[3].FindControl("lblTrnDate1");
                if (pgCs.DateType == "Gregorian") { calDate.SetGDate(lblDate.Text); } else { calDate.SetHDate(lblDate.Text); }
                
                if (gvr.Cells[5].Text == "1") { value = "0"; } else { value = "1"; }
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(value));
                if (value == "0") { FillLocationList("O"); } else if (value == "1") { FillLocationList("I"); }
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void Time_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvtpickerTime))  { if (tpickerTime.getIntTime()  < 0)  { e.IsValid = false; } }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FindEmp_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtEmpID.Text))
            {
                if (!GenCs.isEmpID(txtEmpID.Text)) { e.IsValid = false; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (source.Equals(cvReqFile))
        {
            if (CheckFileRequired("CTR"))
            {
                try { if (string.IsNullOrEmpty(fudReqFile.PostedFile.FileName) ) { e.IsValid = false; } } catch { e.IsValid = false; }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckFileRequired(string ReqType)
    {
        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgFormReq FROM Configuration "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if      (DT.Rows[0]["cfgFormReq"] == DBNull.Value)                  { return false; }
            else if (string.IsNullOrEmpty(DT.Rows[0]["cfgFormReq"].ToString())) { return false; }
            else if (!DT.Rows[0]["cfgFormReq"].ToString().Contains(ReqType))    { return false; }
            else { return true; }
        }
        else { return false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
}
