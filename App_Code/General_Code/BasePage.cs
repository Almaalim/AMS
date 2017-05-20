using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : System.Web.UI.Page
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected override void OnPreInit(EventArgs e)
    {

        base.OnPreInit(e);


        if (Convert.ToString(Request.QueryString["lp"]) == "0")
        {
            Page.MasterPageFile = "~/AMSMasterPageWithoutLeftpanel.master";
        }
        else
            if (Convert.ToString(Request.QueryString["lp"]) == "1")
            {
                Page.MasterPageFile = "~/AMSMasterPage.master";
            }
       
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public String CurrentCulture
    {
        get
        {
            if (null != Session["PreferedCulture"])
                return Session["PreferedCulture"].ToString();
            else
                return "en-US";
        }
        set
        {
            Session["PreferedCulture"] = value;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected override void InitializeCulture()
    {
        string CurrentCulture = "en-US";
        if (Session["Language"] != null)
        {
            if (Session["Language"].ToString() == "AR") { CurrentCulture = "ar-Sa"; Session["Part1align"] = "left"; Session["Part2align"] = "right"; } 
            else { CurrentCulture = "en-US";  Session["Part1align"] = "right"; Session["Part2align"] = "left";}
        }
        else
        {
            CurrentCulture = "en-US"; Session["Part1align"] = "right"; Session["Part2align"] = "left";
        }
        UICulture = CurrentCulture;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CurrentCulture);
        base.InitializeCulture();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [System.Web.Services.WebMethod]
    public static void BrowserCloseMethod()
    {
        // Write Custom Code
        HttpContext.Current.Session.Abandon();
        string loginUserName = string.Empty;
        if (HttpContext.Current.Session["UserName"] != null)
        {
            loginUserName = Convert.ToString(HttpContext.Current.Session["UserName"]);
        }

        UsersSql logSql = new UsersSql();
        logSql.InOutLog_Update(loginUserName);

        HttpContext.Current.Session["Permissions"] = null;
        HttpContext.Current.Session.Contents.RemoveAll();
        HttpContext.Current.Session.Abandon();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowAlert(Page pg,string msg)
    {
        ScriptManager.RegisterStartupScript(pg, pg.GetType(), "key", "alert('" + msg + "')", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowAdminAlert(Page pg)
    {
        string msg = General.Msg("Transaction failed to commit please contact your administrator", "النظام غير قادر على حفظ البيانات, الرجاء الاتصال بمدير النظام");
        ScriptManager.RegisterStartupScript(pg, pg.GetType(), "key", "alert('" + msg + "')", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
}
