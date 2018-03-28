using System;
using Stimulsoft.Report;
using Elmah;
using System.Data;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;

public partial class Pages_Report_ReportDesigner : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReportPro ProCs = new ReportPro();
    ReportSql SqlCs = new ReportSql();

    PageFun   pgCs   = new PageFun();
    General   GenCs  = new General();
    DBFun     DBCs   = new DBFun();
    CtrlFun   CtrlCs = new CtrlFun();
    DTFun     DTCs   = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            /*** Fill Session ************************************/
            if (!string.IsNullOrEmpty(Convert.ToString(Session["RDRepID"])) && !string.IsNullOrEmpty(Convert.ToString(Session["RDRgpID"])))
            {
                StiLoadLic();
                LoadReport(Convert.ToString(Session["RDRepID"]));
            }
            else { Response.Redirect(@"~/Pages_Report/Reports.aspx?ID=" + Convert.ToString(Session["RDRgpID"])); }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void LoadReport(string RepID)
    {
        try
        {
            string RepTemp = "";
            DataTable DT = DBCs.FetchData("SELECT * FROM Report WHERE RepID = @P1 ", new string[] { RepID });
            if (!DBCs.IsNullOrEmpty(DT)) { RepTemp = General.Msg(DT.Rows[0]["RepTempEn"].ToString(), DT.Rows[0]["RepTempAr"].ToString()); }

            if (string.IsNullOrEmpty(RepTemp)) { return; }

            StiReport Rep = new StiReport();
            Rep.LoadFromString(RepTemp);
            Rep.Dictionary.Databases.Clear();
            Rep.Dictionary.Databases.Add(new StiSqlDatabase("Connection", General.ConnString));
            Rep.Dictionary.Synchronize();
            Rep.Compile();

            StiWebDesigner1.ShowFileMenuNew    = false;
            StiWebDesigner1.ShowFileMenuOpen   = false;
            StiWebDesigner1.ShowFileMenuSaveAs = false;

            string Lang = Convert.ToString(Session["Language"]) == "AR" ? "Ar" : "En"; 
            StiWebDesigner1.Localization = string.Format("Localization/{0}.xml", Lang);
            StiWebDesigner1.Report = Rep;
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void StiWebDesigner1_SaveReport(object sender, StiSaveReportEventArgs e)
    {
        StiReport Rep = e.Report;
        string RepTemp = e.Report.SaveToString().ToString();

        ProCs.RepID = Convert.ToString(Session["RDRepID"]);
        ProCs.RepTemp = RepTemp;
        ProCs.RepLang = Session["Language"].ToString();
        ProCs.TransactionBy = Session["UserName"].ToString();

        try
        {
            SqlCs.Report_Update_Template(ProCs);
            CtrlCs.ShowMsg(this, CtrlFun.TypeMsg.Success, General.Msg("Report Updated Successfully", "تم تحديث التقرير بنجاح"));
            //Response.Redirect(@"~/Pages_Report/Reports.aspx");
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            CtrlCs.ShowAdminMsg(this, ex.Message.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnBackToReportsPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Pages_Report/Reports.aspx?ID=" + Convert.ToString(Session["RDRgpID"]));
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
}