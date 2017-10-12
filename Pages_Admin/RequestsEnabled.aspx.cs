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
using System.Data.SqlClient;
using Elmah;
using System.Security.Cryptography;

public partial class RequestsEnabled : BasePage
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

                Populate();
            }
        }
        catch (Exception ex) { }        
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
    public void Populate()
	{
        chkbReqList.Items.Clear();

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM RequestType WHERE RetVisible = 'True' ORDER By RetOrder "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            CtrlCs.PopulateCBL(chkbReqList, DT, General.Msg("RetNameEn","RetNameAr"), "RetID", "-Select Requset Type-");

            foreach (DataRow row in DT.Rows)
            {
                try
                {
                    string RetID     = row["RetID"].ToString();
                    string ReqStatus = CryptorEngine.Decrypt(row["EmpReqStatus"].ToString(), true);
                    ListItem item    = chkbReqList.Items.FindByValue(RetID);
                    item.Selected    = Convert.ToBoolean(ReqStatus.Substring(3));
                }
                catch { }
            }
        }
	}  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region DataItem Events

    public void UIEnabled(bool pStatus)
    {
        chkbReqList.Enabled = pStatus;
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
            if (!Page.IsValid) { return; }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            UpdateReqStatus();

            UIEnabled(false);
            BtnStatus("100");

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
        Populate();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BtnStatus(string Status) //string pBtn = [ADD,Modify,Save,Cancel]
    {
        btnModify.Enabled = GenCs.FindStatus(Status[0]);
        btnSave.Enabled   = GenCs.FindStatus(Status[1]);
        btnCancel.Enabled = GenCs.FindStatus(Status[2]);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UpdateReqStatus()
    {
        SqlConnection sqlCon = new SqlConnection(General.ConnString);
        using (sqlCon)
        {
            sqlCon.Open();

            foreach (ListItem item in chkbReqList.Items)
            {
                string RetID = item.Value;
                string RetStatus = item.Selected.ToString();

                string sql = "UPDATE RequestType SET EmpReqStatus = @EmpReqStatus WHERE RetID = @RetID ";

                SqlCommand smd = new SqlCommand(sql, sqlCon);
                smd.Parameters.AddWithValue("@EmpReqStatus", CryptorEngine.Encrypt(RetID + RetStatus, true));
                smd.Parameters.AddWithValue("@RetID", RetID);

                smd.ExecuteNonQuery();
            }
            sqlCon.Close();
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}