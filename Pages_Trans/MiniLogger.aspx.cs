using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Elmah;

public partial class Pages_Trans_MiniLogger : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs = new PageFun();
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
                /*** Check AMS License ***/
                pgCs.CheckAMSLicense();
                /*** get Permission    ***/
                ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);
                /*** Common Code ************************************/

                string ApplicationPath = (string.IsNullOrEmpty(Request.ApplicationPath) || Request.ApplicationPath == "/") ? "" : Request.ApplicationPath;
                string URL = Request.Url.Scheme + "://" + Request.Url.Authority + ApplicationPath + "/Service/Fingerprint_WS.asmx";

                hfdConn.Value = URL;
                hfdLoginID.Value = pgCs.LoginID.Replace("\\", "....");
                hfdLang.Value = pgCs.Lang;
                hfdEmpID.Value = pgCs.LoginEmpID;

                string ID = hfdConn.Value + "," + hfdLoginID.Value + "," + hfdEmpID.Value + "," + hfdLang.Value;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "javascript:Connect('" + ID + "');", true);
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}