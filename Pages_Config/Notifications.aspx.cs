﻿using System;
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

public partial class Pages_Config_Notifications : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    MailPro ProCs = new MailPro();
    MailSql SqlCs = new MailSql();
    
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    
    string sortDirection = "ASC";
    string sortExpression = "";
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
            string Version = (pgCs.Version == "General" ? "0" : pgCs.Version);
            MainQuery = " SELECT * FROM EmailType WHERE MailType = 'Notification' AND (CHARINDEX('General',VerID) > 0 OR CHARINDEX('" + Version + "', VerID) > 0) ";
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/pgCs.CheckAMSLicense();
                /*** get Permission    ***/ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("0000");
                UIEnabled(false);
                //UILang();
                FillGrid(new SqlCommand(MainQuery));
                FillList();

                ViewState["CommandName"] = "";
                /*** Common Code ************************************/

                divDaysOfWeek.Visible = true;
                divCalendar.Visible   = false;
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
            for (int i=1; i<= 31; i++)
            {
                ListItem _li = new ListItem(i.ToString(), i.ToString());
                cblCalendardays.Items.Add(_li);
            }
            
            ListItem _li2 = new ListItem("Last", "Last");
            cblCalendardays.Items.Add(_li2);
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Search Events

    //protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    string sql = MainQuery;

    //    if (ddlFilter.SelectedIndex > 0)
    //    {
    //        UIClear();
    //        sql = MainQuery + " AND " + ddlFilter.SelectedItem.Value + " LIKE @P1 ";
    //        cmd.Parameters.AddWithValue("@P1", txtFilter.Text.Trim() + "%");
    //    }

    //    UIClear();
    //    BtnStatus("1000");
    //    UIEnabled(false);
    //    grdData.SelectedIndex = -1;
    //    cmd.CommandText = sql;
    //    FillGrid(cmd);
    //}

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
        //if (pgCs.LangEn)
        //{
        //   spnNameEn.Visible = true;
        //}
        //else
        //{
        //    grdData.Columns[3].Visible = false;
        //    ddlFilter.Items.FindByValue("SchNameEn").Enabled = false;
        //}

        //if (pgCs.LangAr)
        //{
        //    spnNameAr.Visible = true;
        //}
        //else
        //{
        //    grdData.Columns[2].Visible = false;
        //    ddlFilter.Items.FindByValue("SchNameAr").Enabled = false;
        //}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        txtNameAr.Enabled       = pStatus;
        txtNameEn.Enabled       = pStatus;
        txtDescription.Enabled  = pStatus;
        ddlMailSendType.Enabled = false;
        chkStatus.Enabled       = pStatus;
        ddlScheduleType.Enabled = pStatus;
        cblDaysOfWeek.Enabled   = pStatus;
        cblCalendardays.Enabled = pStatus;
        tpStartTime.Enabled     = pStatus;

        txtID.Enabled = txtID.Visible = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!string.IsNullOrEmpty(txtID.Text)) { ProCs.MailID = txtID.Text; }
            ProCs.MailNameAr = txtNameAr.Text.Trim();
            ProCs.MailNameEn = txtNameEn.Text.Trim();
            ProCs.MailDesc   = txtDescription.Text.Trim();
            ProCs.SchActive  = chkStatus.Checked;

            string SchDays = null;
            if (ddlScheduleType.SelectedIndex > -1)
            {
                ProCs.SchType = ddlScheduleType.SelectedValue;
                
                if (ddlScheduleType.SelectedValue == "Monthly")
                {
                    for (int i = 0; i < cblCalendardays.Items.Count; i++)
                    {
                        if (cblCalendardays.Items[i].Selected) { if (string.IsNullOrEmpty(SchDays)) { SchDays += cblCalendardays.Items[i].Value; } else { SchDays += "," + cblCalendardays.Items[i].Value; } }
                    }
                }
                else
                {
                    for (int i = 0; i < cblDaysOfWeek.Items.Count; i++)
                    {
                        if (cblDaysOfWeek.Items[i].Selected) { if (string.IsNullOrEmpty(SchDays)) { SchDays += cblDaysOfWeek.Items[i].Value; } else { SchDays += "," + cblDaysOfWeek.Items[i].Value; } }
                    }
                }
            }
            ProCs.SchDays = SchDays;

            ProCs.SchStartTimeShift1 = tpStartTime.getDateTime().ToString();

            ProCs.TransactionBy = pgCs.LoginID;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UIClear()
    {
        try
        {
            ViewState["CommandName"] = ""; 
            txtID.Text = "";
            
            txtNameAr.Text = "";
            txtNameEn.Text = "";
            txtDescription.Text = "";
            ddlMailSendType.SelectedIndex = -1;
            chkStatus.Checked = false;
            ddlScheduleType.SelectedIndex = -1;
            for (int i = 0; i < cblDaysOfWeek.Items.Count; i++) { cblDaysOfWeek.Items[i].Selected = false; }
            for (int i = 0; i < cblCalendardays.Items.Count; i++) { cblCalendardays.Items[i].Selected = false; }
            tpStartTime.ClearTime();

            divDaysOfWeek.Visible = true;
            divCalendar.Visible   = false;
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlScheduleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCalendar.Visible   = false;
        divDaysOfWeek.Visible = false;

        if (ddlScheduleType.SelectedValue == "Monthly") { divCalendar.Visible = true; } else { divDaysOfWeek.Visible = true; }
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
        try
        {
            UIClear();
            UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "ADD";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            UIEnabled(true);
            BtnStatus("0011");
            ViewState["CommandName"] = "EDIT";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
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

            //if      (ViewState["CommandName"].ToString() == "ADD")  { SqlCs.Insert(ProCs); } 
            if (ViewState["CommandName"].ToString() == "EDIT") { SqlCs.Notification_Update(ProCs); }

            UIClear();
            BtnStatus("1000");
            UIEnabled(false);
            grdData.SelectedIndex = -1;
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
        UIClear();
        BtnStatus("1000");
        UIEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        //btnAdd.Enabled    = GenCs.FindStatus(Status[0]);
        btnModify.Enabled = GenCs.FindStatus(Status[1]);
        btnSave.Enabled   = GenCs.FindStatus(Status[2]);
        btnCancel.Enabled = GenCs.FindStatus(Status[3]);
        
        //if (Status[0] != '0') { btnAdd.Enabled = Permht.ContainsKey("Insert"); }
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
                        e.Row.Cells[2].Visible = false; //To hide ID column in grid view
                        e.Row.Cells[3].Visible = false; //To hide ID column in grid view
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

        UIClear();
        UIEnabled(false);
        BtnStatus("0000");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                //        ImageButton _btnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");
                //        _btnDelete.Enabled = pgCs.getPermission(ViewState["ht"], "Delete");
                //        _btnDelete.Attributes.Add("OnClick", CtrlCs.ConfirmDeleteMsg());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdData, "Select$" + e.Row.RowIndex);
                        break;
                    }
            }
        
        GridView gridView = (GridView)sender;
        int cellIndex = -1;
        foreach (DataControlField field in gridView.Columns)
        {
            if (ViewState["SortExpression"] != null)
            {
                if (field.SortExpression == Convert.ToString(ViewState["SortExpression"]))
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                    break;
                }
            }
        }

        if (cellIndex > -1)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //this is a header row,set the sort style
                e.Row.Cells[cellIndex].CssClass += (sortDirection == "ASC" ? " sortasc" : " sortdesc");
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //switch (e.CommandName)
            //{
            //    case ("Delete1"):
            //        string ID = e.CommandArgument.ToString();

            //        SqlCs.Delete(ID, pgCs.LoginID);

            //        btnFilter_Click(null,null);
                    
            //        CtrlCs.ShowDelMsg(this, true);
            //        break;
            //}
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.ToString());
        }
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

        UIClear();
        UIEnabled(false);
        BtnStatus("0000");
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
                PopulateUI(grdData.SelectedRow.Cells[2].Text);
                BtnStatus("0100");
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
            DataRow[] DRs = DT.Select("MailID =" + pID + "");

            txtID.Text          = DRs[0]["MailID"].ToString();
            txtNameEn.Text      = DRs[0]["MailNameEn"].ToString();
            txtNameAr.Text      = DRs[0]["MailNameAr"].ToString();
            txtDescription.Text = DRs[0]["MailDesc"].ToString();
            if (DRs[0]["SchActive"] != DBNull.Value) { chkStatus.Checked = Convert.ToBoolean(DRs[0]["SchActive"]); }
            ddlMailSendType.SelectedIndex = ddlMailSendType.Items.IndexOf(ddlMailSendType.Items.FindByValue(DRs[0]["MailSendType"].ToString()));
            ddlScheduleType.SelectedIndex = ddlScheduleType.Items.IndexOf(ddlScheduleType.Items.FindByValue(DRs[0]["SchType"].ToString()));
            if (DRs[0]["SchStartTimeShift1"] != DBNull.Value) { tpStartTime.SetTime(Convert.ToDateTime(DRs[0]["SchStartTimeShift1"])); }
            
            if (DRs[0]["SchDays"] != DBNull.Value) 
            { 
                string SchDays = DRs[0]["SchDays"].ToString();
                string[] Days = SchDays.Split(',');
                if (DRs[0]["SchType"].ToString() == "Monthly")
                {
                    divCalendar.Visible = true;
                    divDaysOfWeek.Visible = false; 
                    for (int i = 0; i < Days.Length; i++)
                    {
                        int index = cblCalendardays.Items.IndexOf(cblCalendardays.Items.FindByValue(Days[i]));
                        if (index > -1) { cblCalendardays.Items[index].Selected = true; } else { cblCalendardays.Items[index].Selected = false; }
                    }
                }
                else
                {
                    divCalendar.Visible = false;
                    divDaysOfWeek.Visible = true; 
                    for (int i = 0; i < Days.Length; i++)
                    {
                        int index = cblDaysOfWeek.Items.IndexOf(cblDaysOfWeek.Items.FindByValue(Days[i]));
                        if (index > -1) { cblDaysOfWeek.Items[index].Selected = true; } else { cblDaysOfWeek.Items[index].Selected = false; }
                    }
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
    protected void grdData_PreRender(object sender, EventArgs e) { CtrlCs.GridRender((GridView)sender); }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void NameValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            string UQ = string.Empty;
            if (ViewState["CommandName"].ToString() == "EDIT") { UQ = " AND MailID != @P2 "; }

            if (source.Equals(cvNameEn))
            {
                if (pgCs.LangEn)
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, false, General.Msg("Name (En) Is Required", "الإسم بالإنجليزي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameEn.Text)) { e.IsValid = false; }
                }
                
                if (!string.IsNullOrEmpty(txtNameEn.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameEn, true, General.Msg("Entered English Name exist already,Please enter another name", "الإسم بالإنجليزي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM EmailType WHERE MailNameEn = @P1 " + UQ, new string[] { txtNameEn.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
                }
            }
            if (source.Equals(cvNameAr))
            {
                if (pgCs.LangAr)
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, false, General.Msg("Name (Ar) Is Required", "الإسم بالعربي مطلوب"));
                    if (string.IsNullOrEmpty(txtNameAr.Text)) { e.IsValid = false; }
                }
                if (!string.IsNullOrEmpty(txtNameAr.Text))
                {
                    CtrlCs.ValidMsg(this, ref cvNameAr, true, General.Msg("Entered Arabic Name exist already,Please enter another name", "الإسم بالعربي مدخل مسبقا ، الرجاء إدخال إسم آخر"));

                    DataTable DT = DBCs.FetchData("SELECT * FROM EmailType WHERE MailNameAr = @P1 " + UQ, new string[] { txtNameAr.Text, txtID.Text });
                    if (!DBCs.IsNullOrEmpty(DT)) { e.IsValid = false; }
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