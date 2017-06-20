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

public partial class DepartmentLevel : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

                ViewState["LevelDT"] = EmptyDataTable();
                if (pgCs.Version == "Al_JoufUN") { FillDepLevelList(30); } else { FillDepLevelList(6); }
                FillLevelFromDB();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillDepLevelList(int count)
	{
        for (int i = 1; i <= count; i++) { ddlCount.Items.Add(i.ToString()); }
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    public void FillLevelFromDB()
	{
        string NameEns = "";
        string NameArs = "";
        ddlCount.SelectedIndex = ddlCount.Items.IndexOf(ddlCount.Items.FindByValue("1"));

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM DepLevel "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            ddlCount.SelectedIndex = ddlCount.Items.IndexOf(ddlCount.Items.FindByValue(DT.Rows.Count.ToString()));
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(NameEns)) { NameEns = DT.Rows[i]["DplEnglishName"].ToString(); } else { NameEns += "," + DT.Rows[i]["DplEnglishName"].ToString(); }
                if (string.IsNullOrEmpty(NameArs)) { NameArs = DT.Rows[i]["DplArabicName"].ToString();  } else { NameArs += "," + DT.Rows[i]["DplArabicName"].ToString(); }
            }
        }

        FillLevel(NameEns,NameArs,Convert.ToInt32(ddlCount.SelectedValue));
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillLevelFromHide()
	{
        string NameEns = hdnNameEn.Value;
        string NameArs = hdnNameAr.Value;
        FillLevel(NameEns,NameArs,Convert.ToInt32(ddlCount.SelectedValue));
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillLevel(string NameEns, string NameArs, int Count)
	{
        hdnNameEn.Value = hdnNameAr.Value = "";
 
        string[] en = { };
        string[] ar = { };

        if (!string.IsNullOrEmpty(NameEns))
        {
            en = NameEns.Split(',');
            ar = NameArs.Split(',');
        }
        
        for (int i = 0; i < Count; i++)
        {
            Controls.Remove(FindControl("lblLevelEn_" + i.ToString()));
            Controls.Remove(FindControl("txtLevelEn_" + i.ToString()));
            Controls.Remove(FindControl("lblLevelAr_" + i.ToString()));
            Controls.Remove(FindControl("txtLevelAr_" + i.ToString()));
            //==Create Row ===========//
            TableRow tr = new TableRow();
            //==Create column 1========//
            TableCell td1 = new TableCell();
            Label _label1 = new Label();
            _label1.ID    = "lblLevelEn_" + i.ToString();
            _label1.Text  = General.Msg("Level " + (i+1).ToString() + " Name(En) :", " الاسم (E)" + "للمستوى " + (i + 1).ToString() + " :");
            td1.Controls.Add(_label1);
            //==Create column 2========//
            TableCell td2     = new TableCell();
            TextBox _TextBox1 = new TextBox();
            _TextBox1.ID       = "txtLevelEn_" + i.ToString();
            try {  _TextBox1.Text = en[i]; } catch { }
            _TextBox1.Attributes.Add("onblur", "CreateName();");
            td2.Controls.Add(_TextBox1);
            //==Create column 3========//
            TableCell td3     = new TableCell();
            td3.Width = 50;
            //==Create column 4========//
            TableCell td4 = new TableCell();
            Label _label2 = new Label();
            _label2.ID    = "lblLevelAr_" + i.ToString();
            _label2.Text = General.Msg("Level " + (i + 1).ToString() + " Name(Ar) :", " الاسم (ع)" + "للمستوى " + (i + 1).ToString() + " :");
            td4.Controls.Add(_label2);
            //==Create column 5========//
            TableCell td5     = new TableCell();
            TextBox _TextBox2 = new TextBox();
            _TextBox2.ID       = "txtLevelAr_" + i.ToString();
            try {  _TextBox2.Text = ar[i]; } catch { }
            _TextBox2.Attributes.Add("onblur", "CreateName();");
            td5.Controls.Add(_TextBox2);
            //=========================//
            tr.Cells.Add(td1);
            tr.Cells.Add(td2);
            tr.Cells.Add(td3);
            tr.Cells.Add(td4);
            tr.Cells.Add(td5);
            tblLevel.Rows.Add(tr);
            //=========================//
            if (string.IsNullOrEmpty(hdnNameEn.Value)) { hdnNameEn.Value = _TextBox1.Text; } else { hdnNameEn.Value += "," + _TextBox1.Text; }
            if (string.IsNullOrEmpty(hdnNameAr.Value)) { hdnNameAr.Value = _TextBox2.Text; } else { hdnNameAr.Value += "," + _TextBox2.Text; }
            //=========================//
        }
        string ss = "";
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected DataTable EmptyDataTable()
    {
        DataTable LevelDT = new DataTable();
        LevelDT.Columns.Add(new DataColumn("LevelID", typeof(string)));
        LevelDT.Columns.Add(new DataColumn("LevelNameEn", typeof(string)));
        LevelDT.Columns.Add(new DataColumn("LevelNameAr", typeof(string)));
        return LevelDT;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        tblLevel.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlCount_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (ddlCount.SelectedIndex > -1)
       {
            //int levelCount    = Convert.ToInt32(ddlCount.SelectedValue);
            //int LevelsAllowed = Convert.ToInt32(LicDf.FetchLic("DC"));
            //if (levelCount > LevelsAllowed) { return; }
            FillLevelFromHide();
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
            //if (!CtrlCs.PageIsValid(this, vsSave)) { return; }
            
            int res = 0;
            string[] en = hdnNameEn.Value.Split(',');
            string[] ar = hdnNameAr.Value.Split(',');


            StringBuilder sb = new StringBuilder();
            sb.Append(" Delete FROM DepLevel; ");

            int Levelcount = Convert.ToInt32(ddlCount.SelectedValue);
            for (int i = 0; i < Levelcount; i++)
            {
                if (string.IsNullOrEmpty(en[i]) || string.IsNullOrEmpty(ar[i])) 
                { 
                    CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Validation, General.Msg("Fill All levels Names", "يجب إدخال جميع أسماء المستويات"));
                    return; 
                }
             
                sb.Append(" INSERT INTO DepLevel(DplID,DplEnglishName,DplArabicName) values(" + i.ToString() + ",'" + en[i] + "','" + ar[i] + "'); ");
            }

            res = DBCs.ExecuteData(sb.ToString());

            UIEnabled(false);
            BtnStatus("100");

            FillLevelFromDB();
            CtrlCs.ShowSaveMsg(this);
        }
        catch (Exception ex) 
        { 
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BtnStatus("100");
        UIEnabled(false);
        FillLevelFromDB();
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
    #region Custom Validate Events

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
}
