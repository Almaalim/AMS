using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using Elmah;

public partial class CurrentUsers : BasePage
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
        /*** Fill Session ************************************/
        pgCs.FillSession(); 
        CtrlCs.RefreshGridEmpty(ref grdData);
        /*** Fill Session ************************************/
        
        if (!IsPostBack)
        {
             /*** Common Code ************************************/
            /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
            /*** get Permission    ***/ ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);            
            FillGrid();
            /*** Common Code ************************************/
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        DateTime now = DateTime.Now;
        DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
        DateTime End   = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);

        DataTable DT = DBCs.FetchData(" SELECT UsrName,HostName,max(LogInEvent) maxLogInEvent FROM InOutLog WHERE (LogInEvent BETWEEN @P1 AND @P2) AND LogOutEvent IS NULL GROUP BY UsrName,HostName ", new string[] { start.ToString(), End.ToString() });
        grdData.DataSource = (DataTable)DT;
        grdData.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
