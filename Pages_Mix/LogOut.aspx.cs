using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOut : System.Web.UI.Page
{
    UsersSql logSql = new UsersSql();
    string loginUserName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            loginUserName = Session["UserName"].ToString();
        }

        logSql.InOutLog_Insert(loginUserName, "");
        
        Session.Abandon();
    }
}