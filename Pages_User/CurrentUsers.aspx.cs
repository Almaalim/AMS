using System;
using System.Data;

public partial class CurrentUsers : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    DTFun   DTCs   = new DTFun();
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
        string Start = DTCs.GDateNow("MM/dd/yyyy 00:00:00");
        string End   = DTCs.GDateNow("MM/dd/yyyy HH:mm:ss");

        DataTable DT = DBCs.FetchData(" SELECT UsrName,HostName,HostIP,max(LogInEvent) maxLogInEvent FROM InOutLog WHERE (LogInEvent BETWEEN @P1 AND @P2) AND LogOutEvent IS NULL GROUP BY UsrName,HostName,HostIP ", new string[] { Start.ToString(), End.ToString() });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            grdData.DataSource = (DataTable)DT;
            grdData.DataBind();
        }
        else
        {
            CtrlCs.FillGridEmpty(ref grdData, 50);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
