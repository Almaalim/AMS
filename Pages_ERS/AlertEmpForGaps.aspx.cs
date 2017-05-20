using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using System.Data;
using System.Collections;
using System.Text;
using System.Globalization;

public partial class AlertEmpForGaps : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun pgCs   = new PageFun();
    General GenCs  = new General();
    DBFun   DBCs   = new DBFun();
    CtrlFun CtrlCs = new CtrlFun();
    DTFun   DTCs   = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            /*** Fill Session ************************************/
            
            divAbs.Attributes["class"] = General.Msg("AlertGapsAttendanceEN", "AlertGapsAttendanceAR");
            divGaps.Attributes["class"] = General.Msg("AlertGapsAttendanceEN", "AlertGapsAttendanceAR");

            if (!IsPostBack)
            {
                /*** Common Code ************************************/
                /*** Check AMS License ***/ pgCs.CheckAMSLicense();  
                /*** get Permission    ***/ //ViewState["ht"] = pgCs.getPerm(Request.Url.AbsolutePath);  
                Populate();
                ViewState["CommandName"] = "";
                /*** Common Code ************************************/
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Populate()
    {
        try
        {
            int TotalGaps   = Convert.ToInt32(Request.QueryString["Gaps"].ToString());
            int TotalAbsent = Convert.ToInt32(Request.QueryString["Abs"].ToString());

            if (TotalGaps > 0)
            {
                divGaps.Visible = true;

                string msgTypTotalAr = "ساعات";
                string msgTypTotalEn = "Hours";
                if (TotalGaps < 3600) { msgTypTotalAr = "دقيقة"; msgTypTotalEn = "Minutes"; }

                lblMsgWrgGaps.Text = General.Msg("There you have Gaps Without Excuse of Total: " + DisplayFun.GrdDisplayDuration(TotalGaps) + " " + msgTypTotalEn, "يوجد لديك مجموع ثغرات بدون استئذان: " + DisplayFun.GrdDisplayDuration(TotalGaps) + " " + msgTypTotalAr);
            }

            if (TotalAbsent > 0)
            {
                divAbs.Visible = true;
                lblMsgWrgAbs.Text = General.Msg("There you have days Absent Without Excuse: " + TotalAbsent.ToString() + " Day/Days ", "يوجد لديك أيام غياب بدون استئذان عددها: " + TotalAbsent.ToString() + " يوم/أيام");
            }
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}