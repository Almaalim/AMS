using System;

public partial class LogOut : System.Web.UI.Page
{
    UsersSql logSql = new UsersSql();
    string loginUserName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null) { loginUserName = Session["UserName"].ToString(); }

        logSql.InOutLog_Insert(loginUserName, "", "");
        
        Session.Abandon();
    }
}