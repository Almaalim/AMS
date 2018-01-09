using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Elmah;
using System.Data.SqlClient;

public partial class Pages_Employee_EmployeeWorktimeTable : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmpWrkPro ProCs = new EmpWrkPro();
    EmpWrkSql SqlCs = new EmpWrkSql();
   
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
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                BtnStatus("00");
                UILang();
                UIEnabled(true);
                FillList();            
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/ 
            }

            ViewState["Month"] = ddlMonth.SelectedValue;
            ViewState["Year"]  = ddlYear.SelectedValue;
            CreateGridViewRow();
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

            DataTable WDT = DBCs.FetchData(new SqlCommand(" SELECT WktID, WktInitialAr, WktInitialEn FROM WorkingTime WHERE ISNULL(WktDeleted,0) = 0 AND WtpID IN (SELECT WtpID FROM WorkType WHERE WtpInitial !='RO') AND WktIsActive = 'True' "));
            if (!DBCs.IsNullOrEmpty(WDT))
            {
                CtrlCs.PopulateDDL2(ddlWktID, WDT, General.Msg("WktInitialEn", "WktInitialAr"), "WktID", General.Msg("- Select Worktime -", "- اختر جدول العمل-"));
            }

            if (pgCs.Version == "SANS")
            {
                lblDep.Text = General.Msg("Group :", "المجموعة :");
                DataTable DT = DBCs.FetchData(" SELECT GrpName FROM RotationWorkTime_GrpRel WHERE GrpUserName = @P1", new string[] { pgCs.LoginID });
                if (!DBCs.IsNullOrEmpty(DT)) { CtrlCs.PopulateDDL(ddlDep, DT, "GrpName", "GrpName", General.Msg("-Select Group-", "-اختر المجموعة-")); }
                rvDep.InitialValue = ddlDep.Items[0].Text;
                string GMsg = General.Msg("Group is required", "المجموعة مطلوبة");
                rvDep.Text = Server.HtmlDecode("&lt;img src='../images/Exclamation.gif' title='" + GMsg + "' /&gt;");
            }
            else
            {
                lblDep.Text = General.Msg("Department :", "القسم :");
                CtrlCs.PopulateDepartmentList(ref ddlDep, pgCs.DepList, pgCs.Version);
                rvDep.InitialValue = ddlDep.Items[0].Text;
                string DMsg = General.Msg("Department is required", "القسم مطلوب");
                rvDep.Text = Server.HtmlDecode("&lt;img src='../images/Exclamation.gif' title='" + DMsg + "' /&gt;");
            }          
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
        if (ddlDep.SelectedIndex > 0)
        {
            if (pgCs.Version == "SANS")
            {
                lblTitel.Text = General.Msg("Working times of the " + ddlDep.SelectedItem.Text + " Group in " + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue, "أوقات العمل للمجموعة  " + ddlDep.SelectedItem.Text + " في " + ddlYear.SelectedValue + "/" + ddlMonth.SelectedValue);
            }
            else
            {
                lblTitel.Text = General.Msg("Working times of the " + ddlDep.SelectedItem.Text + " department in " + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue, "أوقات العمل للقسم  " + ddlDep.SelectedItem.Text + " في " + ddlYear.SelectedValue + "/" + ddlMonth.SelectedValue);
            }

            ViewState["DepID"] = ddlDep.SelectedValue;
            BtnStatus("11");
        }   
        else
        {
            ViewState["DepID"] = "";
            BtnStatus("00");
        }  

        ViewState["Month"] = ddlMonth.SelectedValue;
        ViewState["Year"]  = ddlYear.SelectedValue;
        hdnChangeEmpID.Value = "";
        hdnChangeDayNo.Value = "";
        hdnChangeWktID.Value = "";
        hdnColID.Value  = "";
        hdnEmpid.Value  = "";
        hdnRowID.Value  = "";
        CreateGridViewRow();
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
        if (pgCs.LangEn) { } else { }
        if (pgCs.LangAr) { } else { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UIEnabled(bool pStatus)
    {
        grdData.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.FirstDayDate = ViewState["SDATE"].ToString();
            ProCs.EmpIDs       = hdnChangeEmpID.Value;
            ProCs.DayNos       = hdnChangeDayNo.Value;
            ProCs.WktIDs       = hdnChangeWktID.Value;

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
            ViewState["SDATE"]    = "";
            ViewState["DayCount"] = "";
            hdnChangeEmpID.Value  = "";
            hdnChangeDayNo.Value  = "";
            hdnChangeWktID.Value  = "";
            hdnRowID.Value        = "";
            hdnEmpid.Value        = "";
            hdnColID.Value        = "";
            CreateGridViewRow();       
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CtrlCs.PageIsValid(this, vsSave)) { return; }

            FillPropeties();

            SqlCs.Wkt_Insert_Table(ProCs);

            UIClear();
            BtnStatus("11");
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
        BtnStatus("11");
        CreateGridViewRow();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [Save,Cancel]
    {
        Hashtable Permht = (Hashtable)ViewState["ht"];
        btnSave.Enabled   = GenCs.FindStatus(Status[0]);
        btnCancel.Enabled = GenCs.FindStatus(Status[1]);
        
        if (Status[0] != '0') { btnSave.Enabled = btnCancel.Enabled = Permht.ContainsKey("Update"); }
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridView Events

    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Hashtable Permht = (Hashtable)ViewState["ht"];
            if (Permht.ContainsKey("Update") && ddlDep.SelectedIndex > 0)
            {
                int DayCount = Convert.ToInt32(ViewState["DayCount"]);
                for (int i = 1; i < DayCount; i++)
                {
                    string EmpID = ((DataRowView)e.Row.DataItem)["EmpID"].ToString();
                    Label _lbl = (Label)e.Row.FindControl("lbl" + i.ToString());
                    _lbl.Attributes.Add("OnClick", "DoOpen('" + (e.Row.RowIndex + 2) + "','" + (i + 1).ToString() + "','" + EmpID + "')");
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e) { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    {
                        GridView grv = (GridView)sender;
                        GridViewRow grvr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                        TableCell oTableCell = new TableCell();
                        oTableCell.Text = General.Msg("Employee", "الموظف");
                        oTableCell.HorizontalAlign = HorizontalAlign.Center;
                        oTableCell.ColumnSpan = 2;
                        oTableCell.BorderStyle = BorderStyle.Solid;
                        oTableCell.BorderWidth = 1;
                        grvr.Cells.Add(oTableCell);

                        DateTime DayDate = DTCs.ConvertToDatetime2(ViewState["SDATE"].ToString(),"Gregorian");
                        int DayCount     = Convert.ToInt32(ViewState["DayCount"]);
                        for (int i = 1; i < DayCount; i++)
                        {
                            string DName = DayDate.AddDays(i-1).ToString("ddd");
                            oTableCell = new TableCell();
                            oTableCell.Text = General.Msg(DName, GenCs.convertDayToArabic(DName));
                            oTableCell.HorizontalAlign = HorizontalAlign.Center;
                            oTableCell.ColumnSpan = 1;
                            oTableCell.BorderStyle = BorderStyle.Solid;
                            oTableCell.BorderWidth = 1;
                            grvr.Cells.Add(oTableCell);
                        }

                        grv.Controls[0].Controls.AddAt(0, grvr);

                        break;
                    }
                //case DataControlRowType.DataRow: { break; }
                case DataControlRowType.Pager: { break; }
                default: { break; }
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CreateGridViewRow()
    {
        try
        {
            //DateTime dd = DateTime.Now;

            grdData.Columns.Clear();

            BoundField _bf = new BoundField();
            _bf.HeaderText = General.Msg("ID", "الرقم");
            _bf.DataField = "EmpID";
            _bf.HeaderStyle.Width = 100;
            _bf.ItemStyle.Width = 100;
            grdData.Columns.Add(_bf);

            _bf = new BoundField();
            _bf.HeaderText = General.Msg("Name", "الاسم");
            _bf.DataField = "EmpName";
            _bf.HeaderStyle.Width = 250;
            _bf.ItemStyle.Width = 250;
            grdData.Columns.Add(_bf);

            int Year  = Convert.ToInt32(ViewState["Year"]);
            int Month = Convert.ToInt32(ViewState["Month"]);
            int DayCount = DTCs.FindLastDay(Month, Year);

            ViewState["DayCount"] = DayCount + 1;

            TemplateField _tf = new TemplateField();
            GridTemplate _gt = null;

            for (int i = 1; i < DayCount + 1; i++)
            {
                _tf = new TemplateField();
                _tf.HeaderText = i.ToString();
                _gt = new GridTemplate(i.ToString());
                _tf.ItemTemplate = _gt;
                grdData.Columns.Add(_tf);
            }

            BindGrid();
            
            //lblTime.Text = (DateTime.Now - dd).TotalSeconds.ToString();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void BindGrid()
    {
        DateTime SDATE;
        DateTime EDATE;
        DTCs.FindMonthDates(ViewState["Year"].ToString(), ViewState["Month"].ToString(), out SDATE, out EDATE);

        ViewState["SDATE"] = SDATE.ToString("MM/dd/yyyy");

        string Lang = (pgCs.Lang == "AR") ? "Ar" : "En";

        string EmpType = (pgCs.Version == "SANS") ? "GRP" : "DEP";

        string IDs = "";

        if (string.IsNullOrEmpty(Convert.ToString(ViewState["DepID"])))
        {
            EmpType = "EMP";
            IDs = "-";
        }
        else
        {
            if (EmpType == "EMP")
            {
                IDs = "1001,1004,1005,1016,1020,1034,1036,1037,1038,1041,1042,1045,1046,1061,1062,1063,1068,1069,1070,1072,1073,1074,1075,1076,1077,1078,1080,1082,1083,1084,1086,1087,1088,1089,1090,1094,1096,1099,1100,1101,1102,1103,1104,1105,1106,1112,1113,1114,1115,1116,1117,1118,1120,1121,1125,1126,1127,1128,1129,1130,1131,1132,1133,1134,1135,1136,1137,1138,1139,1140,1141,1143,1144,1145,1146,1147,1148,1149,1150,1151,1152,1153,1154,1155,1156,1157,1159,1160,1161,1162,1163,1164,1165,1166,1167,1168,1169,1170,1172,1178,1180,1181,1182,1184,1187,1188,1190,1192,1193,1197,1199,1215,1216,1217,1219,1220,1221,1222,1223,1224,1228,1233,1234,1239,1240,1242,1243,1245,1246,1247,1248,1249,1252,1253,1255,1256,1257,1258,1259,1260,1261,1262,1264,1265,1266,1267,1268,1269,1274,1275,1278,1811,2001,2002,2003,2004,224,9100,9200";
            }
            else
            {
                IDs = ViewState["DepID"].ToString();
            }
        }

        DataTable DT = new DataTable();
        DT = DBCs.FetchProcedureData("EmployeeWorktimeTable", new string[] { "@istartDate", "@iEndDate", "@iEmpType", "@iEmpIDs", "@iLang" }, new string[] { SDATE.ToString(), EDATE.ToString(), EmpType, IDs, Lang });

        grdData.DataSource = DT;
        grdData.DataBind();
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
                if (string.IsNullOrEmpty(hdnChangeEmpID.Value))
                {
                    CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("No change is made, can not do the save operation", "لم يتم إجراء أي تغيير ,لا يمكن القيام يعملية الحفظ"));
                    e.IsValid = false;
                }
                else
                {
                    int iEmpID = hdnChangeEmpID.Value.Split(',').Length;
                    int iDayNo = hdnChangeDayNo.Value.Split(',').Length;
                    int iWktID = hdnChangeWktID.Value.Split(',').Length;

                    if (iEmpID != iDayNo || iEmpID != iWktID || iDayNo != iWktID)
                    {
                        CtrlCs.ValidMsg(this, ref cvValid, true, General.Msg("An unknown error occurred, can not perform the save operation", "حدث خطأ غير معروف ,لا يمكن القيام يعملية الحفظ"));
                        e.IsValid = false;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
