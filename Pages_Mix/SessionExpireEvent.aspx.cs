using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionExpireEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SessionExpire"] != null)
        {
            if (Request.QueryString["SessionExpire"] == "True")
            {
                string loginUserName = string.Empty;
                if (Session["UserName"] != null)
                {
                    loginUserName = Convert.ToString(Session["UserName"]);
                }

                UsersSql logSqlCs = new UsersSql();
                logSqlCs.InOutLog_Update(loginUserName);

                Session["Permissions"] = null;
                Session.Contents.RemoveAll();
                Session.Abandon();
                Response.Redirect(@"~/login.aspx");
            }
        }
       
    }
}