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

public partial class AppendingTransactions : BasePage
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
    
    string MainQuery = " SELECT * FROM TransDumpInfoView ";
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
                FillList();
                btnFilter_Click(null, null);
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                spnINReqFile.Visible = spnOUTReqFile.Visible = CheckFileRequired("ATR");
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

            CtrlCs.FillVirtualMachineList(ref ddlINLocation, rvINLocation, false, "IN");
            CtrlCs.FillVirtualMachineList(ref ddlOUTLocation, rvOUTLocation, false, "OUT");
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
        string empString = txtEmployeeID.Text;
        string[] empstr = empString.Split('-');
        txtEmpID.Text = empstr[0];
        
        try
        {
            SqlCommand cmd = new SqlCommand();
            string sql = MainQuery + " WHERE EmpID = '0' ";

            if (!string.IsNullOrEmpty(txtEmpID.Text))
            {
                DateTime SDate = DateTime.Now;
                DateTime EDate = DateTime.Now;
                DTCs.FindMonthDates(ddlYear.SelectedValue, ddlMonth.SelectedValue, out SDate, out EDate);

                StringBuilder FQ = new StringBuilder();
                FQ.Append(MainQuery);
                FQ.Append(" WHERE DepID IN (" + pgCs.DepList + ") ");
                FQ.Append(" AND EmpID = @EmpID");
                FQ.Append(" AND (TrnDate BETWEEN @SDate AND @EDate)");
                FQ.Append(" AND UsrName IS NOT NULL");

                sql = FQ.ToString();
                cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
                cmd.Parameters.AddWithValue("@SDate", SDate);
                cmd.Parameters.AddWithValue("@EDate", EDate);
            }

            UIClear(false);
            BtnStatus("1000");
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
            grdData.Columns[4].Visible = false;
        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {
            grdData.Columns[5].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtEmpID.Enabled = pStatus;
        calDate.SetEnabled(pStatus);
        chkIN.Enabled    = pStatus;
        chkOUT.Enabled   = pStatus;

        StatusINUI(false);
        StatusOUTUI(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties(string Type)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.EmpIDs     = txtEmpID.Text;
            ProCs.TrnDate    = calDate.getGDateDBFormat();
            ProCs.TransactionBy = pgCs.LoginID;

            if (Type == "IN")
            {
                ProCs.TrnType   = "1";
                ProCs.TrnTime   = tpINTime.getDateTime().ToString();
                ProCs.MacID     = ddlINLocation.SelectedValue;
                ProCs.TrnReason = txtINDesc.Text;
                ProCs.UsrName   = pgCs.LoginID;

                try
                {
                    if (!string.IsNullOrEmpty(fudINReqFile.PostedFile.FileName))
                    {
                        string dateFile = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                        string FileName = System.IO.Path.GetFileName(fudINReqFile.PostedFile.FileName);
                        string[] names = FileName.Split('.');
                        string NewFileName = txtEmpID.Text + "-" + dateFile + "-INATR." + names[1];
                        fudINReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                        ProCs.TrnFilePath = NewFileName;
                    }
                }
                catch(Exception ex) { }
            }
            
            if (Type == "OUT")
            {
                ProCs.TrnType   = "0";
                ProCs.TrnTime   = tpOUTTime.getDateTime().ToString();
                ProCs.MacID     = ddlOUTLocation.SelectedValue;
                ProCs.TrnReason = txtOUTDesc.Text;
                ProCs.UsrName   = pgCs.LoginID;

                try
                {
                    if (!string.IsNullOrEmpty(fudOUTReqFile.PostedFile.FileName))
                    {
                        string dateFile = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                        string FileName = System.IO.Path.GetFileName(fudOUTReqFile.PostedFile.FileName);
                        string[] names = FileName.Split('.');
                        string NewFileName = txtEmpID.Text + "-" + dateFile + "-OUTATR." + names[1];
                        fudOUTReqFile.PostedFile.SaveAs(Server.MapPath(@"./RequestsFiles/") + NewFileName);
                        ProCs.TrnFilePath = NewFileName;
                    }
                }
                catch(Exception ex) { }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear(bool clear)
    {
        ViewState["CommandName"] = "";
        
        if (clear) { txtEmpID.Text = ""; }
        calDate.ClearDate();
        chkIN.Checked  = false;
        chkOUT.Checked = false;
        ClearINUI();
        ClearOUTUI();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void StatusINUI(bool pStatus)
    {
        tpINTime.Enabled      = pStatus;
        ddlINLocation.Enabled = pStatus;
        txtINDesc.Enabled     = pStatus;
        fudINReqFile.Enabled  = pStatus;
        rvINLocation.Enabled  = pStatus;
        rvINDesc.Enabled      = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void StatusOUTUI(bool pStatus)
    {
        tpOUTTime.Enabled      = pStatus;
        ddlOUTLocation.Enabled = pStatus;
        txtOUTDesc.Enabled     = pStatus;
        fudOUTReqFile.Enabled  = pStatus;
        rvOUTLocation.Enabled  = pStatus;
        rvOUTDesc.Enabled      = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ClearINUI()
    {
        tpINTime.ClearTime();
        ddlINLocation.ClearSelection();
        txtINDesc.Text = "";
        //fudINReqFile;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ClearOUTUI()
    {
        tpOUTTime.ClearTime();
        ddlOUTLocation.ClearSelection();
        txtOUTDesc.Text = "";
        //fudINReqFile;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkIN_CheckedChanged(object sender, EventArgs e)
    {
        ClearINUI();
        StatusINUI(chkIN.Checked);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void chkOUT_CheckedChanged(object sender, EventArgs e)
    {
        ClearOUTUI();
        StatusOUTUI(chkOUT.Checked);
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
        UIClear(false);
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

            if (commandName == "ADD")
            {
                if (chkIN.Checked)
                {
                    FillPropeties("IN");
                    SqlCs.Wizard_Trans_WithAttachment(ProCs);
                }

                if (chkOUT.Checked)
                {
                    FillPropeties("OUT");
                    SqlCs.Wizard_Trans_WithAttachment(ProCs);
                }
            }
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
        UIClear(true);
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
        //ClearUI(false);
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
            if (source.Equals(cvINTime))  { if (tpINTime.getIntTime()  < 0 && chkIN.Checked)  { e.IsValid = false; } }
            if (source.Equals(cvOUTTime)) { if (tpOUTTime.getIntTime() < 0 && chkOUT.Checked) { e.IsValid = false; } }
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
                DataTable DT = DBCs.FetchData("SELECT EmpID FROM Employee WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 AND EmpID = @P1 AND DepID IN (" + pgCs.DepList + ") ", new string[] { txtEmpID.Text });
                if (DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ReqFile_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        if (source.Equals(cvINReqFile))
        {
            if (CheckFileRequired("ATR") && chkIN.Checked)
            {
                try { if (string.IsNullOrEmpty(fudINReqFile.PostedFile.FileName) ) { e.IsValid = false; } } catch { e.IsValid = false; }
            }
        }

        if (source.Equals(cvOUTReqFile))
        {
            if (CheckFileRequired("ATR") && chkOUT.Checked)
            {
                try { if (string.IsNullOrEmpty(fudOUTReqFile.PostedFile.FileName) ) { e.IsValid = false; } } catch { e.IsValid = false; }
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

