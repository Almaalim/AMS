using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Elmah;

public partial class EmployeeFingerPrint : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
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
                /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                /*** Common Code ************************************/

                //"http://localhost/AMS_WSFingerprintTools/WSFingerprintTools.asmx"; //
                hfdConnStr.Value   = General.UrlWSFingerprintTools; /* for AMS fingerprint tools activeX with webservice */
                
                //hfdConnStr.Value   = General.ConnString.Replace("\\","....");
                hfdLoginUser.Value = pgCs.LoginID.Replace("\\","....");
                hfdLang.Value      = pgCs.Lang;
                hfdPage.Value      = Request.Url.AbsolutePath;

                string ID = hfdConnStr.Value + "," + hfdLoginUser.Value + "," + hfdLang.Value + "," + hfdPage.Value;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "javascript:Connect('" + ID + "');", true);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}