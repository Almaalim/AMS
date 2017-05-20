using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Progress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Language"].ToString() == "AR") { lblUpdateProgress.Text = "...الرجاء الإنتظار"; } else { lblUpdateProgress.Text = "Please Wait..."; }
    }
}