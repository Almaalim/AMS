using System;
using Stimulsoft.Report;
using Elmah;

public partial class ReportViewer : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun   pgCs   = new PageFun();
    General   GenCs  = new General();
    DBFun     DBCs   = new DBFun();
    CtrlFun   CtrlCs = new CtrlFun();
    DTFun     DTCs   = new DTFun();
    ReportFun RepCs  = new ReportFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        //StiWebViewerFxOptions.Toolbar.ShowOpenButton = false;
        //StiWebViewerFxOptions.Toolbar.ShowSaveButton = false;
 
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            /*** Fill Session ************************************/
            if (Request.QueryString["ID"] != null)
            {
                string ID = Request.QueryString["ID"].ToString();
                if (!string.IsNullOrEmpty(ID))
                {
                    string[] IDs = ID.Split('_');
                    string FavID = IDs[0];
                    string RepID = IDs[1];
                    RepParametersPro RepProCs = RepCs.FillFavReportParam(FavID, RepID, pgCs.LoginID, pgCs.Lang, pgCs.DateType);
                    Session["RepProCs"] = RepProCs;
                }
            }

            if (Session["RepProCs"] == null)
            { Response.Redirect(@"~/Pages_Report/Reports.aspx"); }
            ShowReport();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowReport()
    {
        if (Session["RepProCs"] != null)
        {
            StiLoadLic();

            StiReport Rep = new StiReport();
            RepParametersPro RepProCs = new RepParametersPro();
            RepProCs = (RepParametersPro)Session["RepProCs"];

            ViewState["RepID"] = RepProCs.RepID;
            ViewState["RgpID"] = RepProCs.RgpID;

            Rep = RepCs.CreateReport(RepProCs);
            ////// View report
            //Rep.Render();
            //string Lang = "En"; 
            string Lang = Convert.ToString(Session["Language"]) == "AR" ? "Ar" : "En"; 
            StiWebViewer1.Localization = String.Format("Localization/{0}.xml", Lang);

            StiWebViewer1.RightToLeft = false;
            StiWebViewer1.Report = Rep;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static object GetPropValue(object src, string propName)
    {
        return src.GetType().GetProperty(propName).GetValue(src, null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnBackToReportsPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Pages_Report/Reports.aspx?ID=" + ViewState["RgpID"].ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StiLoadLic()
    {
        Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHm+XCVQB8hoG7iOXPruVpVJgjD1niOe/z/B02rxea2pCghjtE" + 
                                         "thRKe83MO1s8+wUNJdZCpotZRpcukV/TdUJdp5V9LeTx0vSpfuZAABGJVWbRgsnN2YMB5CSgTSuf3HzvQPWTNjSkMY" + 
                                         "cQN7CWZMWIwmfDmSSpIIhpc5BeObqD43b1tCPCbcUq170lg41mhY7871drNs+o0sRn2/4s9Sx+1BLQnJmDK1gRaO7B" + 
                                         "fRXRraf8croLQkHGQ4VsxNMEQCalwmMSScvzDnd7k05m6mSAtGPR5qvgzHpTQgujWC9PRmSieTumvltoRvI7aPQjLq" + 
                                         "EF1t5CD3zr2A/wKV4pJuq5gFx2HD1Lr8rhDr5YHvI4MHzZuD5LmH1y7W2ljb/zsQscSDy3/AEfuVe2mZoCupNagb4O" + 
                                         "JFcKCYP+Tv11BKJmfQ3C8bwUgfYcpra6r25fTsDwKyARaxNARg1dIwjAPbyW5yxL91SfXz5b7Ng/ALSrSPOczT7sK/" + 
                                         "iyZzc/adT3XYfflVvaX0HnUUFiJ43QoNXfn+";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected void StiWebViewer1_GetReport(object sender, Stimulsoft.Report.Web.StiReportDataEventArgs e)
    //{
    //    if (Request.QueryString["ID"] != null)
    //    {
    //        string ID = Request.QueryString["ID"].ToString();
    //        if (!string.IsNullOrEmpty(ID))
    //        {
    //            string[] IDs = ID.Split('_');
    //            string FavID = IDs[0];
    //            string RepID = IDs[1];
    //            RepParametersPro RepProCs = RepCs.FillFavReportParam(FavID, RepID, pgCs.LoginID, pgCs.Lang, pgCs.DateType);
    //            Session["RepProCs"] = RepProCs;
    //        }
    //    }

    //    if (Session["RepProCs"] == null) { Response.Redirect(@"~/Pages_Report/Reports.aspx"); }
    //    ShowReport();
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}