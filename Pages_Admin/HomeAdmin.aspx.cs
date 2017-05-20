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

public partial class AdminPage_HomeAdmin : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                /*** Common Code ************************************/

                if (isConnect()) { lblvDBConnect.Text = "Connect"; } else { lblvDBConnect.Text = "Not Connect"; }
                lblvADStatus.Text = getADStatus();
                lblvActiveVersionName.Text = LicDf.FindActiveVersion();
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }    
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool isConnect()
    {
        string Conn = General.ConnString;
        System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(Conn);
        try
        {
            using (sqlConn)
            {
                sqlConn.Open();
                sqlConn.Close();
                sqlConn.Dispose();
                return true;
            }
        }
        catch (Exception ex)
        {
            sqlConn.Close();
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getADStatus()
	{
        string ADstatus = "OFF"; 

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM ADConfig "));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if (DT.Rows[0]["ADEnabled"] != DBNull.Value)
            {
                string ADEnabled = DT.Rows[0]["ADEnabled"].ToString();
                try { ADEnabled = CryptorEngine.Decrypt(ADEnabled, true); } catch (Exception e1) { }
                if (ADEnabled == "1") { ADstatus = "ON"; } else { ADstatus = "OFF"; }
            }
        }

        return ADstatus;
	}  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
