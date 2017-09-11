using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Elmah;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class DepartmentLevel : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AdminPro ProCs = new AdminPro();
    AdminSql SqlCs = new AdminSql();

    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
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
                BtnStatus("100");
                UIEnabled(false);
                /*** Common Code ************************************/

                FirstGridViewRow();
                FillGrid();
            }

            BindGrid();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        grdAddData.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            //ProCs.DplIDs     = ViewState["DplIDs"].ToString();
            ProCs.DplNameArs = ViewState["DplNameArs"].ToString();
            ProCs.DplNameEns = ViewState["DplNameEns"].ToString();

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
            FillGrid();
            BindGrid();
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

    protected void btnModify_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "EDIT";
        UIEnabled(true);
        BtnStatus("011");
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

            if (ViewState["CommandName"].ToString() == "EDIT") { SqlCs.DepLevel_InsertUpdate(ProCs); }

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
        BtnStatus("100");
        UIEnabled(false);
        UIClear();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
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
            _Imgb.CommandArgument = (e.Row.DataItem as DataRowView).Row["DplID"].ToString();
            e.Row.Cells[0].Controls.Add(_Imgb);

            if (e.Row.RowIndex == 0) { _Imgb.Visible = false; }

            Label _lblRowID = new Label();
            _lblRowID.ID = "lblDplID";
            _lblRowID.Width = 10;
            _lblRowID.Text = (e.Row.DataItem as DataRowView).Row["DplID"].ToString();
            e.Row.Cells[1].Controls.Add(_lblRowID);

            TextBox _txtNameAr = new TextBox();
            _txtNameAr.ID = "txtDplNameAr";
            //_txtNameAr.Width = 200;
            _txtNameAr.MaxLength = 50;
            _txtNameAr.CssClass = "tb_without_border";
            _txtNameAr.Text = (e.Row.DataItem as DataRowView).Row["DplNameAr"].ToString();
            e.Row.Cells[2].Controls.Add(_txtNameAr);

            TextBox _txtNameEn = new TextBox();
            _txtNameEn.ID = "txtDplNameEn";
            //_txtNameEn.Width = 200;
            _txtNameEn.MaxLength = 50;
            _txtNameEn.CssClass = "tb_without_border";
            _txtNameEn.Text = (e.Row.DataItem as DataRowView).Row["DplNameEn"].ToString();
            e.Row.Cells[3].Controls.Add(_txtNameEn);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
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
            bfield.HeaderText = General.Msg("Level ID","رقم المستوى");
            bfield.DataField = "DplID";
            grdAddData.Columns.Add(bfield);
  
            bfield = new BoundField();
            string HeaderAr = (pgCs.LangAr) ? General.Msg("* Name(Ar)", "* الاسم (ع)") : General.Msg("Name(Ar)", "الاسم (ع)");
            bfield.HeaderText = HeaderAr;
            bfield.DataField = "DplNameAr";
            grdAddData.Columns.Add(bfield);


            //var _SpanAr = new HtmlGenericControl("span");
            //_SpanAr.InnerHtml = "*";
            //_SpanAr.Attributes["class"] = "RequiredField";
            //e.Row.Cells[2].Controls.Add(_SpanAr);

            //<span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>


            bfield = new BoundField();
            string HeaderEn = (pgCs.LangEn) ? General.Msg("* Name(En)", "* الاسم (E)") : General.Msg("Name(En)", "الاسم (E)");
            bfield.HeaderText = HeaderEn;
            bfield.DataField = "DplNameEn";
            grdAddData.Columns.Add(bfield);

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
            dt.Columns.Add(new DataColumn("DplID", typeof(string)));
            dt.Columns.Add(new DataColumn("DplNameAr", typeof(string)));
            dt.Columns.Add(new DataColumn("DplNameEn", typeof(string)));

            DataRow dr = dt.NewRow();
            dr["DplID"] = 1;
            dr["DplNameAr"] = "";
            dr["DplNameEn"] = "";
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
                    drCurrentRow["DplID"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["DplNameAr"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameAr"];
                    dtCurrentTable.Rows[i - 1]["DplNameEn"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameEn"];

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
                    Label _lblDplID = (Label)grdAddData.Rows[rowIndex].Cells[1].FindControl("lblDplID");
                    _lblDplID.Text = dt.Rows[i]["DplID"].ToString();

                    TextBox _txtDplNameAr = (TextBox)grdAddData.Rows[rowIndex].Cells[2].FindControl("txtDplNameAr");
                    _txtDplNameAr.Text = dt.Rows[i]["DplNameAr"].ToString();

                    TextBox _txtDplNameEn = (TextBox)grdAddData.Rows[rowIndex].Cells[3].FindControl("txtDplNameEn");
                    _txtDplNameEn.Text = dt.Rows[i]["DplNameEn"].ToString();

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
                    drCurrentRow["DplID"] = i + 1;
                    drCurrentRow["DplNameAr"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameAr"];
                    drCurrentRow["DplNameEn"] = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameEn"];

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
                dt.Rows.Remove(dt.Rows[rowIndex - 1]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grdAddData.DataSource = dt;
                grdAddData.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["DplID"] = Convert.ToString(i + 1);
                }

                SetPreviousData();
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void btnAddRecord_click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid()
    {
        DataTable GDT = DBCs.FetchData(new SqlCommand(" SELECT DplID + 1 AS DplID, DplArabicName AS DplNameAr, DplEnglishName AS DplNameEn FROM DepLevel ORDER BY DplID "));
        if (!DBCs.IsNullOrEmpty(GDT))
        {
            GDT.Columns["DplID"].ReadOnly = false;
            ViewState["CurrentTable"] = GDT;
        }
        else
        {
            ViewState["CurrentTable"] = null;
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

            //string IDs     = "";
            string NamesAr = "";
            string NamesEn = "";

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    //IDs = "";
                    //string IDVal = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$lblDplID"];
                    //if (string.IsNullOrEmpty(IDVal)) { IDs = IDVal; } else { IDs += "," + IDVal; }

                    string NameArVal = "";
                    NameArVal = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameAr"];
                    if (pgCs.LangAr) { if (string.IsNullOrEmpty(NameArVal)) { CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("The Arabic name must be entered for all levels", "يجب إدخال الاسم العربي لكل المستويات")); return false; } }
                    if (CtrlIndex == 2) { NamesAr = NameArVal; } else { NamesAr += "," + NameArVal; }
                    
                    string NameEnVal = "";
                    NameEnVal = Request.Form["ctl00$ContentPlaceHolder1$grdAddData$ctl" + CtrlIndex.ToString("00") + "$txtDplNameEn"];
                    if (pgCs.LangEn) { if (string.IsNullOrEmpty(NameEnVal)) { CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("The English name must be entered for all levels", "يجب إدخال الاسم الانجليزي لكل المستويات")); return false; } }
                    if (CtrlIndex == 2) { NamesEn = NameEnVal; } else { NamesEn += "," + NameEnVal; }


                    CtrlIndex++;
                }

                //ViewState["DplIDs"]     = IDs;
                ViewState["DplNameArs"] = NamesAr;
                ViewState["DplNameEns"] = NamesEn;

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
