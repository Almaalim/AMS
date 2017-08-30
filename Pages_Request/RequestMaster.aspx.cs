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
using System.Data.SqlClient;

public partial class RequestMaster : BasePage
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
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession(); 
            /*** Fill Session ************************************/
            
            if (!IsPostBack)
            {
                /*** Common Code ************************************/                
                /*** Check ERS License ***/ pgCs.CheckERSLicense();
                /*** Common Code ************************************/

                SetPageTitel();

                if (Request.QueryString["Type"] != null)
                {
                    string Type = Request.QueryString["Type"].ToString();
                    //////////////////////
                    if (Type == "VAC") { this.iFrame.Attributes.Add("src", "VacationRequest2.aspx?Type=VAC"); }
                    if (Type == "COM") { this.iFrame.Attributes.Add("src", "VacationRequest2.aspx?Type=COM"); }
                    if (Type == "JOB") { this.iFrame.Attributes.Add("src", "VacationRequest2.aspx?Type=JOB"); }
                    if (Type == "LIC") { this.iFrame.Attributes.Add("src", "VacationRequest2.aspx?Type=LIC"); }

                    if (Type == "EXC") { this.iFrame.Attributes.Add("src", "ExcuseRequest2.aspx"); }
                    if (Type == "ESH") { this.iFrame.Attributes.Add("src", "ShiftExcuseRequest2.aspx"); }
                    if (Type == "SWP") { this.iFrame.Attributes.Add("src", "ShiftSwapRequest2.aspx"); }
                }
            }
        }
        catch (Exception ex) { }     
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetPageTitel()
    {
        try
        {
            System.IO.FileInfo PageFileInfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string QS = (Request.QueryString.Count != 0) ? Request.QueryString.ToString() : "";
            string PageName = PageFileInfo.Name + (!string.IsNullOrEmpty(QS) ? "?" + QS : "");

            DataTable DT = new DataTable();

            if (Session["MenuTitelDT"] == null)
            {
                DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Menu "));
                Session["MenuTitelDT"] = DT;
            }
            else
            {
                DT = (DataTable)Session["MenuTitelDT"];
            }

            if (!DBCs.IsNullOrEmpty(DT))
            {
                DataRow[] DRs = DT.Select("MnuURL LIKE '" + PageName + "' OR MnuURL LIKE '" + PageFileInfo.Name + "'");
                foreach (DataRow DR in DRs)
                {
                    if (DR["MnuTextEn"] != DBNull.Value) { Page.Title = General.Msg(DR["MnuTextEn"].ToString(), DR["MnuTextAr"].ToString()); }
                }
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}