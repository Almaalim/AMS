using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Elmah;
using System.Globalization;
using System.Data.SqlClient;

public partial class UnstyleShift : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpWrkPro ProCs = new EmpWrkPro();
    EmpWrkSql SqlCs = new EmpWrkSql();
   
    GregorianCalendar Grn = new GregorianCalendar();
    UmAlQuraCalendar Umq = new UmAlQuraCalendar();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            /*** Fill Session ************************************/

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();
                if (LicDf.FetchLic("US") == "0") { Server.Transfer("login.aspx"); }
                /*** get Permission    ***/
                ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("1000");
                UILang();
                UIEnabled(false);
                FillList();            
                FirstGridViewRow();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/ 
            }

            BindGrid();
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

            ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DTCs.FindCurrentMonth()));
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DTCs.FindCurrentYear()));
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

        }

        if (pgCs.LangAr)
        {
            
        }
        else
        {

        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        grdAddData.Enabled = pStatus;

        ddlMonth.Enabled = !pStatus;
        ddlYear.Enabled  = !pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            int Year  = Convert.ToInt32(ddlYear.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int DayCount = DTCs.FindLastDay(Month, Year);

            ProCs.FirstDayDate = DTCs.FindDateFromPart(1, Month, Year).ToString();
            ProCs.DayCount     = DTCs.FindLastDay(Month, Year).ToString();
            ProCs.EmpIDs       = ViewState["EmpIDs"].ToString();
            ProCs.WktIDs       = ViewState["WktIDs"].ToString();

            ProCs.TransactionBy  = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            ViewState["CurrentTable"] = null;
            FirstGridViewRow();
            
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
    #region ButtonAction Events

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "ADD";
        UIClear();
        UIEnabled(true);
        BtnStatus("0011");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenCs.IsNullOrEmpty(ViewState["CommandName"])) { return; }
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            if (ViewState["CommandName"].ToString() == "ADD") { SqlCs.UnstyleWkt_Insert(ProCs); }

            UIClear();
            UIEnabled(false);
            BtnStatus("1000");
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
        //btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Import"); }
        //if (Status[1] != '0') { btnModify.Enabled = Permht.ContainsKey("Update"); }

        //if (Status[0] != '0') { btnImport.Enabled = Permht.ContainsKey("Import"); }
        //if (Status[1] != '0') { btnExport.Enabled = Permht.ContainsKey("Export"); }
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdAddData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _Imgb = new ImageButton();
            _Imgb.ID = "imgbtnDelete";
            _Imgb.ImageUrl = "../images/Button_Icons/button_delete.png";
            _Imgb.Click += DeleteRow;
            _Imgb.CommandArgument = (e.Row.DataItem as DataRowView).Row["RowID"].ToString();
            e.Row.Cells[0].Controls.Add(_Imgb);

            if (e.Row.RowIndex == 0) { _Imgb.Visible = false; }

            Label _lblRowID = new Label();
            _lblRowID.ID = "lblRowID";
            _lblRowID.Width = 10;
            _lblRowID.Text = (e.Row.DataItem as DataRowView).Row["RowID"].ToString();
            e.Row.Cells[1].Controls.Add(_lblRowID);

            TextBox _txtEmpID = new TextBox();
            _txtEmpID.ID = "txtEmpID";
            _txtEmpID.Width = 80;
            _txtEmpID.MaxLength = 10;
            _txtEmpID.Text = (e.Row.DataItem as DataRowView).Row["EmpID"].ToString();
            e.Row.Cells[2].Controls.Add(_txtEmpID);

            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT WktID," + General.Msg("WktNameEn", "WktNameAr") + " AS WktName FROM WorkingTime WHERE ISNULL(" + General.Msg("WktNameEn", "WktNameAr") + ",'') != '' "));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                DropDownList _ddl = new DropDownList();
                for (int i = 1; i < Convert.ToInt16(ViewState["DayCount"]); i++)
                {
                    _ddl = new DropDownList();
                    _ddl.ID = "ddl" + i.ToString();
                    _ddl.Width = 150;
                    _ddl.DataSource = DT;
                    _ddl.DataTextField = "WktName";
                    _ddl.DataValueField = "WktID";
                    _ddl.DataBind();
                    _ddl.Items.Insert(0, new ListItem(General.Msg("- Select Worktime -", "- اختر جدول العمل -"), "0"));

                    e.Row.Cells[i + 2].Controls.Add(_ddl);
                }
            }
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            ImageButton _Imgbtn = new ImageButton();
            _Imgbtn.ID = "btnAddRecord";
            _Imgbtn.ImageUrl = "../images/Button_Icons/button_add.png";
            _Imgbtn.Click += new ImageClickEventHandler(btnAddRecord_click);
            e.Row.Cells[0].Controls.Add(_Imgbtn);
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdAddData_RowCommand(object sender, GridViewCommandEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void FirstGridViewRow()
    {
        try
        {
            grdAddData.Columns.Clear();
            
            TemplateField Ctfield = new TemplateField();
            Ctfield.HeaderText = "";
            grdAddData.Columns.Add(Ctfield);

            BoundField bfield = new BoundField();
            bfield.HeaderText = General.Msg("#","م");
            bfield.DataField = "RowID";
            grdAddData.Columns.Add(bfield);
  
            bfield = new BoundField();
            bfield.HeaderText = General.Msg("Employee ID", "رقم الموظف");
            bfield.DataField = "EmpID";
            grdAddData.Columns.Add(bfield);
 
            int Year  = Convert.ToInt32(ddlYear.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int DayCount = DTCs.FindLastDay(Month, Year);

            ViewState["DayCount"] = DayCount + 1;

            TemplateField tfield = new TemplateField();
            for (int i = 1; i < DayCount + 1; i++)
            {
                tfield = new TemplateField();
                tfield.HeaderText = i.ToString("00") + "/" + Month.ToString("00") + "/" + Year.ToString();
                grdAddData.Columns.Add(tfield);
            }

            BindGrid();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void BindGrid()
    {
        if (ViewState["CurrentTable"] == null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowID", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpID", typeof(string)));

            DataColumn _dc = new DataColumn();
            for (int i = 1; i < Convert.ToInt16(ViewState["DayCount"]); i++)
            {
                _dc = new DataColumn(i.ToString(), typeof(int));
                dt.Columns.Add(_dc);
            }

            DataRow dr = dt.NewRow();
            dr["RowID"] = 1;
            dr["EmpID"] = "";
            for (int i = 1; i < Convert.ToInt16(ViewState["DayCount"]); i++)
            {
                dr[i.ToString()] = 0;
            }
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdAddData.DataSource = dt;
            grdAddData.DataBind();
        }
        else
        {
            grdAddData.DataSource = (DataTable)ViewState["CurrentTable"];
            grdAddData.DataBind();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        int CtrlIndex = 2;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                { 
                    drCurrentRow = dtCurrentTable.NewRow();
                    
                    //TextBox _txtEmpID = (TextBox)grdAddData.Rows[0].Cells[1].FindControl("txtEmpID");
                    //DropDownList _ddl1 = (DropDownList)grdAddData.Rows[0].Cells[2].FindControl("ddl1");
                    //string cc = _ddl1.ClientID;
                    
                    drCurrentRow["RowID"] = i + 1;
 
                    dtCurrentTable.Rows[i - 1]["EmpID"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtEmpID"];
                    
                    for (int k = 1; k < Convert.ToInt16(ViewState["DayCount"]); k++)
                    {
                        dtCurrentTable.Rows[i - 1][k.ToString()] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$ddl" + k.ToString()];
                    }
                    
                    rowIndex++;
                    CtrlIndex++;
                }
                
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
 
                grdAddData.DataSource = dtCurrentTable;
                grdAddData.DataBind();
            }
        }

        SetPreviousData();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     Label _lblRowID = (Label)grdAddData.Rows[rowIndex].Cells[1].FindControl("lblRowID");
                     _lblRowID.Text = dt.Rows[i]["RowID"].ToString();
                    //string cc = _txtEmpID.ClientID;
                    
                    TextBox _txtEmpID = (TextBox)grdAddData.Rows[rowIndex].Cells[1].FindControl("txtEmpID");
                    _txtEmpID.Text = dt.Rows[i]["EmpID"].ToString();
                    //string cc = _txtEmpID.ClientID;

                    for (int k = 1; k < Convert.ToInt16(ViewState["DayCount"]); k++)
                    {
                        DropDownList _dll = (DropDownList)grdAddData.Rows[rowIndex].Cells[k].FindControl("ddl" + k.ToString());
                        _dll.SelectedIndex = _dll.Items.IndexOf(_dll.Items.FindByValue(dt.Rows[i][k.ToString()].ToString()));
                        //string dd = _dll.ClientID;
                    }

                    rowIndex++;
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetRowData()
    {
        int rowIndex = 0;
        int CtrlIndex = 2;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            int count = dtCurrentTable.Rows.Count;
            dtCurrentTable.Rows.Clear();

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                
                    drCurrentRow["RowID"] = i + 1;

                    drCurrentRow["EmpID"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtEmpID"];
                    
                    for (int k = 1; k < Convert.ToInt16(ViewState["DayCount"]); k++)
                    {
                        drCurrentRow[k.ToString()] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$ddl" + k.ToString()];
                    }

                    rowIndex++;
                    CtrlIndex++;

                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
            
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DeleteRow(object sender, EventArgs e)
    {
        ImageButton _Imgb = (sender as ImageButton);
        int rowIndex = Convert.ToInt32(_Imgb.CommandArgument);

        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
                        
            if (dt.Rows.Count > 1)
            {   
                dt.Rows.Remove(dt.Rows[rowIndex-1]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grdAddData.DataSource = dt;
                grdAddData.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["RowID"] = Convert.ToString(i + 1);
                }
                
                SetPreviousData();
            }
        }

        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Id: " + id + " Name: " + name + " Country: " + country + "')", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void btnAddRecord_click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void Valid_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvValid))
            {
                if (!ReadGrdData()) {  e.IsValid = false; }
            }
        }
        catch
        {
            e.IsValid = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool ReadGrdData()
    {
        int CtrlIndex = 2;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            int count = dtCurrentTable.Rows.Count;

            string EmpIDs = "";
            string WktIDs = "";
            string EmpID  = "";
            string WktID  = "";

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    EmpID = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtEmpID"];

                    if (string.IsNullOrEmpty(EmpID)) { CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("You must enter the employee ID", "يجب إدخال رقم الموظف")); return false; }
                    if (!GenCs.isEmpID(EmpID)) { CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("Employee ID " + EmpID + " does not exist already,Please enter another ID", "رقم الموظف " + EmpID + " غير موجود,أدخل رقما آخر")); return false; }

                    WktID = "";
                    for (int k = 1; k < Convert.ToInt16(ViewState["DayCount"]); k++)
                    {
                        string WktVal = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$ddl" + k.ToString()];
                        if (string.IsNullOrEmpty(WktID)) { WktID = WktVal; } else { WktID += "," + WktVal; }
                    }

                    CtrlIndex++;
                    if (string.IsNullOrEmpty(EmpIDs)) { EmpIDs = EmpID; } else { EmpIDs += "," + EmpID; }
                    if (string.IsNullOrEmpty(WktIDs)) { WktIDs = WktID; } else { WktIDs += "-" + WktID; }
                }

                ViewState["EmpIDs"] = EmpIDs;
                ViewState["WktIDs"] = WktIDs;

                return true;
            }
        }

        CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("Data must be entered for at least one employee", "يجب إدخال البيانات لموظف واحد على الأقل")); 
        return false;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
