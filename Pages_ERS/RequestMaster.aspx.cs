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
}