using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageAdmin : System.Web.UI.MasterPage
{
    string dateFormat = string.Empty;
    string menuPermissionList = null;
    
    string loginUserName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        Session["PreferedCulture"] = "en-US";

        if (Convert.ToString(Session["PreferedCulture"]) == "ar-Sa")
        {
            //ldiv.Attributes.Add("dir", "rtl");
            Session["PreferedCulture"] = "ar-Sa";
        }

       

        //if (Session["UserName"] != null)
        //{
        //    loginUserName = Session["UserName"].ToString();
        //}
        //else
        //{
        //    Server.Transfer("login.aspx");
        //}

        lblUserName.Text = Convert.ToString(Session["UserName"]);

        

    }

    protected void lbtnErrors_Click(object sender, EventArgs e)
    {
        Response.Redirect("ASPErrors.aspx");
    }

    protected void lbtnSperrors_Click(object sender, EventArgs e)
    {
        Response.Redirect("sperrors.aspx");
    }

    protected void lbtnMenuBuilder_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuBuilder.aspx");
    }

    protected void lbtnMenuOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderMenu.aspx");
    }

    protected void lbtnMachineErrorLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("MachineErrorLog.aspx");
    }


    public string GetContentFillerText()
    {
        return "3";
    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {


    }

    //protected void LnkLogout_Click(object sender, EventArgs e)
    //{

    //    if (Session["UserName"] != null)
    //    {
    //        loginUserName = Session["UserName"].ToString();
    //    }

    //    logUser.Update(loginUserName);

    //    Session["Permissions"] = null;
    //    Session.Contents.RemoveAll();
    //    Session.Abandon();

    //    //Response.Buffer=<SPAN style="COLOR: blue">true;<o:p></o:p>
    //    Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
    //    Response.Expires = -1500;
    //    Response.CacheControl = "no-cache";
    //    if (Session["SessionId"] == null)
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}

    //protected void languageupdate_Click(object sender, EventArgs e)
    //{

    //    string UsrLoginID = Convert.ToString(Session["UserName"]);
    //    string theme = Convert.ToString(Session["MyTheme"]);
    //    string language = string.Empty;

    //    if (rdArabic.Checked)
    //    {
    //        Session["PreferedCulture"] = "ar-Sa";
    //        language = "AR";
    //        Session["DateFormat"] = "Hijri";
    //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa");
    //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-Sa");

    //    }
    //    if (rdEnglish.Checked)
    //    {
    //        Session["PreferedCulture"] = "en-US";
    //        language = "EN";
    //        Session["DateFormat"] = "Gregorian";
    //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

    //    }

    //    string userRole = Convert.ToString(Session["Role"]);

    //    if ((rdArabic.Checked) || (rdEnglish.Checked))
    //    {

    //        string query = "UPDATE AppUser SET  [UsrLanguage] = '" + language + "'   WHERE   [UsrName] = '" + UsrLoginID + "'";

    //        //int retValue = hc.ExecuteDataQuery(query);


    //        Response.Redirect(Request.FilePath);
    //        //Server.Transfer(Request.FilePath);

    //    }


    //}
    //protected void Menu1_MenuItemDataBound(object sender, MenuEventArgs e)
    //{

    //    //string test1 = e.Item.Text.ToString();

    //    //System.Xml.XmlElement node = (System.Xml.XmlElement)e.Item.DataItem;
    //    //XmlNodeList xNodeList = node.GetElementsByTagName("MenuImage");


    //    //foreach (System.Xml.XmlElement student in node.ChildNodes)
    //    //{

    //    //    foreach (System.Xml.XmlElement field in student.ChildNodes)
    //    //    {
    //    //        string test2 = field.Name;

    //    //        string test3 = field.InnerText;
    //    //    }

    //    //    string test4 = student.Name;

    //    //    string test5 = student.InnerText;
    //    //}



    //}
    //protected void Menu1_DataBound(object sender, EventArgs e)
    //{
    //    //args.Item.ImageUrl = ((SiteMapNode)args.Item.DataItem)["imageUrl"];

    //}

    protected void LnkLogout_Click(object sender, EventArgs e)
    {
        Session.Contents.RemoveAll();
        Session.Abandon();

        //Response.Buffer=<SPAN style="COLOR: blue">true;<o:p></o:p>
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        if (Session["SessionId"] == null)
        {
            Response.Redirect("Loginadmin.aspx");
        }
    }

    protected void lbtnDepLevels_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepLevels.aspx");
    }

    protected void lbtnReportManager_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportManager.aspx");
    }

    protected void lbtnPermissionRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermissionsRequests.aspx");
    }
}