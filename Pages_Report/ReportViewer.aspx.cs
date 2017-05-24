﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.WebFx;
using Stimulsoft.Report.Print;
using System.Text;
using System.Globalization;
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

    string appDirectory = HttpContext.Current.Server.MapPath(string.Empty);
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

            if (Session["RepProCs"] == null) { Response.Redirect(@"~/Pages_Report/Reports.aspx"); }
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
            StiReport Rep = new StiReport();
            RepParametersPro RepProCs = new RepParametersPro();
            RepProCs = (RepParametersPro)Session["RepProCs"];

            ViewState["RepID"] = RepProCs.RepID;
            ViewState["RgpID"] = RepProCs.RgpID;

            Rep = RepCs.CreateReport(RepProCs);

            ////// View report
            //Rep.Render();
            StiWebViewerFx1.Report = Rep;
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
    //protected void StiWebViewerFx1_PreInit(object sender, StiWebViewerFx.StiPreInitEventArgs e)
    //{
    //    e.WebDesigner.Localization = "en";        
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected void StiWebViewerFx1_PreInit(object sender, Stimulsoft.Report.WebFx.StiWebViewerFx.StiPreInitEventArgs e)
    //{
    //    StiWebViewerFx1.Localization = Session["Language"].ToString();
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}