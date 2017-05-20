using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

[ValidationProperty("Text")]
public partial class Control_Calendar3 : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    GregorianCalendar Grn = new GregorianCalendar();
    UmAlQuraCalendar  Umq = new UmAlQuraCalendar();

    DTFun DTCs = new DTFun();
    CtrlFun CtrlCs = new CtrlFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _Date;
    public string Date
    {
        get { return _Date; }
        set { if (_Date != value) { _Date = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum CalendarEnum { Gregorian, Hijri, Both, System }
    public CalendarEnum CalendarType
    {
        get { return ((ViewState["CalendarType"] == null) ? CalendarEnum.Both : (CalendarEnum)ViewState["CalendarType"]); }
        set { ViewState["CalendarType"] = value; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ValidationGroup
    {
        get { return ((ViewState["vg"] == null) ? "" : ViewState["vg"].ToString()); }
        set { ViewState["vg"] = value; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool _ValidationRequired = false;
    public bool ValidationRequired
    {
       get { return _ValidationRequired; }
        set { if (_ValidationRequired != value) { _ValidationRequired = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _CalTo;
    public string CalTo
    {
        get { return _CalTo; }
        set { if (_CalTo != value) { _CalTo = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, System.EventArgs e)
    {   
        CalendarEnum CalendarType;
        try { CalendarType = (CalendarEnum)ViewState["CalendarType"]; } catch { CalendarType = CalendarEnum.Both;}

        if      (CalendarType == CalendarEnum.Gregorian) { txtHDate.Visible = tdHCal.Visible = false; } 
        else if (CalendarType == CalendarEnum.Hijri)     { txtGDate.Visible = tdGCal.Visible = false; }
        else if (CalendarType == CalendarEnum.Both)      {  }
        else if (CalendarType == CalendarEnum.System)    {  }
        
         
        if (!Page.IsPostBack) 
        { 
            try 
            {
                if (!string.IsNullOrEmpty(ViewState["vg"].ToString()) && ValidationRequired)
                {
                    string msg = General.Msg("Date is required", "التاريخ مطلوب");
                    rvDate.ErrorMessage = "";
                    rvDate.Text = this.Server.HtmlDecode("&lt;img src='App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='" + msg + "' /&gt;");
                    rvDate.ValidationGroup = ViewState["vg"].ToString();
                    rvDate.Enabled = true;
                }
            } 
            catch { }

            try 
            { 
                if (!string.IsNullOrEmpty(CalTo) && !string.IsNullOrEmpty(ViewState["vg"].ToString()))
                {
                    cvCompareDates.ValidationGroup = ViewState["vg"].ToString(); 
                    cvCompareDates.Enabled = true;
                }
            }
            catch { }

            ddlGYears.Items.Clear();
            ddlGMonths.Items.Clear();
            DTCs.YearGPopulateList(ref ddlGYears);
            DTCs.MonthGPopulateList(ref ddlGMonths);
            Changedate("G");

            ddlHYears.Items.Clear();
            ddlHMonths.Items.Clear();
            DTCs.YearHPopulateList(ref ddlHYears);
            DTCs.MonthHPopulateList(ref ddlHMonths);
            Changedate("H");

            // to show hide the date picker div when user click on textbox or calendar image
            //txtGDate.Attributes.Add("onclick", "showHide('" + this.pnlGCal.ClientID + "');");
            //txtGreg.Attributes.Add("onclick", "showHide('" + this.pnlGCal.ClientID + "');");
            //imgCalendar.Attributes.Add("onclick", "showHide('" + this.pnlGCal.ClientID + "');");

            ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivGCal.ClientID, "document.getElementById('" + this.DivGCal.ClientID + "').style.display = 'none'; ", true); 
            ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivHCal.ClientID, "document.getElementById('" + this.DivHCal.ClientID + "').style.display = 'none'; ", true); 
        }
        else
        {
            var senderAsControl = sender as Control;
            string ParentUCname = senderAsControl.UniqueID;
            string strPostBackControlName = getPostBackControlName(); //To get the potpack control name

            //If the post back triggered from LocaleCalendar dropdown list, year dropdown list or month dropdown list the calendar div 
            //will stay visible but if triggered by other controls the calendar div will be changed to hidden.
            if (strPostBackControlName != ParentUCname + "$" + "ddlYears" && strPostBackControlName != ParentUCname + "$" + "ddlMonths"
                && strPostBackControlName != ParentUCname + "$" + "ddlLocaleChoice")
            {
                //to manage multiple instances of user control postback, incase the postback happend due to culture changeed in current control,
                //the other user contrls culture drop down list to be changed accordingly. Also year and month dropdown lists according to culture 
                if (strPostBackControlName != "" && strPostBackControlName.Substring(strPostBackControlName.LastIndexOf("$")) == "$ddlLocaleChoice")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 
                    ddlGYears.Items.Clear();
                    ddlGMonths.Items.Clear();
                    DTCs.YearGPopulateList(ref ddlGYears);
                    DTCs.MonthGPopulateList(ref ddlGMonths);
                    Changedate("G");

                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-SA"); 
                    ddlHYears.Items.Clear();
                    ddlHMonths.Items.Clear();
                    DTCs.YearHPopulateList(ref ddlHYears);
                    DTCs.MonthHPopulateList(ref ddlHMonths);
                    Changedate("H");
                }
                //To hide the calendar div in case of any postback other than the three controls (Culture ddl, Year ddl, Month ddl)
                ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivGCal.ClientID, "document.getElementById('" + this.DivGCal.ClientID + "').style.display = 'none'; ", true); 
                ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivHCal.ClientID, "document.getElementById('" + this.DivHCal.ClientID + "').style.display = 'none'; ", true); 
            }

            //to keep the selected culture in case the post back triggered by any control    
            //selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
            //System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string getPostBackControlName()
    {
        Control control = null;
        //first we will check the "__EVENTTARGET" because if post back made by the controls
        //which used "_doPostBack" function also available in Request.Form collection.
        string ctrlname = Page.Request.Params["__EVENTTARGET"];
        if (ctrlname != null && ctrlname != String.Empty)
        {
            control = Page.FindControl(ctrlname);
        }

        if (control == null)
        {
            return string.Empty;
        }
        else
        {
            //to catch the control name in case of multiple instances
            return control.UniqueID;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CalDate_SelectionChanged(object sender, System.EventArgs e)
    {
        System.Web.UI.WebControls.Calendar ib = (System.Web.UI.WebControls.Calendar)sender;
        if (ib.ID == "CalHDate") 
        { 
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); 
            this.txtHDate.Text = CalHDate.SelectedDate.ToString("dd/MM/yyyy");
            this.txtGDate.Text = DTCs.HijriToGreg(this.txtHDate.Text,"dd/MM/yyyy");
            CalGDate.SelectedDates.Clear();
        }
        else if (ib.ID == "CalGDate") 
        { 
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 
            this.txtGDate.Text = CalGDate.SelectedDate.ToString("dd/MM/yyyy");
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text,"dd/MM/yyyy");
            CalHDate.SelectedDates.Clear();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e) 
    { 
        DropDownList ib = (DropDownList)sender;
        if (ib.ID == "ddlGYears")
        {
            Changedate("G");
        }
        else if (ib.ID == "ddlHYears")
        {
            Changedate("H");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e) 
    { 
        DropDownList ib = (DropDownList)sender;
        if (ib.ID == "ddlGMonths")
        {
            Changedate("G");
        }
        else if (ib.ID == "ddlHMonths")
        {
            Changedate("H");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Changedate(string DTType)
    {
        try
        {
            if (DTType == "G")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 
                CalGDate.TodaysDate = new DateTime(Convert.ToInt32(ddlGYears.SelectedValue), Convert.ToInt32(ddlGMonths.SelectedValue), 1);
                //CalDate.SelectedDate =new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else if (DTType == "H")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); 
                CalHDate.TodaysDate = new DateTime(Convert.ToInt32(ddlHYears.SelectedValue), Convert.ToInt32(ddlHMonths.SelectedValue), 1, Umq);
                //CalDate.SelectedDate =new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Umq);
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void imgbtnShowCalendar_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton ib = (ImageButton)sender;
        //if (ib.ID == "imgbtnShowGCalendar")
        //{
        //    ViewState["TypeSpecifiedDate"] = "G"; 
        //    ddlYears.Items.Clear();
        //    ddlMonths.Items.Clear();
        //    DTCs.YearPopulateList(ref ddlYears, "Gregorian");
        //    DTCs.MonthPopulateList(ref ddlMonths, "Gregorian");
        //}
        //else if (ib.ID == "imgbtnShowHCalendar")
        //{ 
        //    ViewState["TypeSpecifiedDate"] = "H"; 
        //    ddlYears.Items.Clear();
        //    ddlMonths.Items.Clear();
        //    DTCs.YearPopulateList(ref ddlYears, "Hijri");
        //    DTCs.MonthPopulateList(ref ddlMonths, "Hijri");
        //}

        //Changedate(ViewState["TypeSpecifiedDate"].ToString());
        //CssStyleCollection pnlCalendarStyle = this.pnlGCal.Style;
        //string DISPLAY = pnlCalendarStyle["DISPLAY"];

        //if (DISPLAY == "none") { this.pnlGCal.Attributes.Add("style", "POSITION: absolute"); } else { this.pnlGCal.Attributes.Add("style", "DISPLAY: none; POSITION: absolute"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetEnabled(bool Status)
    {
       if (Status)
        {
            txtGDate.Attributes.Add("onclick", "showHideG('" + this.DivGCal.ClientID + "','" + this.DivHCal.ClientID + "');");
            txtHDate.Attributes.Add("onclick", "showHideH('" + this.DivGCal.ClientID + "','" + this.DivHCal.ClientID + "');");
            imgG.Attributes.Add("onclick", "showHideG('" + this.DivGCal.ClientID + "','" + this.DivHCal.ClientID + "');");
            imgH.Attributes.Add("onclick", "showHideH('"+ this.DivGCal.ClientID + "','" + this.DivHCal.ClientID + "');");
            
            //imgClear.Attributes.Add("onclick", "Clear('" + this.DivCal.ClientID + "','" + this.txtGDate.ClientID + "','" + this.txtHDate.ClientID + "');");
        }
        else
        {
            txtGDate.Attributes.Remove("onclick");
            txtHDate.Attributes.Remove("onclick");
            imgG.Attributes.Remove("onclick");
            imgH.Attributes.Remove("onclick");

            //imgClear.Attributes.Remove("onclick");
        }

        imgbtnClearCalendar.Enabled = Status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetValidationEnabled(bool Status)
    {
        this.rvDate.Enabled         = Status;
        this.cvCompareDates.Enabled = Status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearDate()
    {
        this.txtGDate.Text = "";
        this.txtHDate.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetGDate(string Date)
    {
        if (string.IsNullOrEmpty(Date)) { ClearDate(); }
        else 
        {
            this.txtGDate.Text = Date;
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text,"dd/MM/yyyy");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetGDate(object Date, string format)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (Date != DBNull.Value) 
        { 
            DateTime DT = (DateTime)Date;
            this.txtGDate.Text = DTCs.FormatGreg(DT.ToString("dd/MM/yyyy"), format); 
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text, format);
        } 
        else { ClearDate(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetHDate(string Date)
    {
        if (string.IsNullOrEmpty(Date)) { ClearDate(); }
        else 
        {
            this.txtHDate.Text = Date;
            this.txtGDate.Text = DTCs.HijriToGreg(this.txtHDate.Text,"dd/MM/yyyy");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetTodayDate()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            this.txtGDate.Text = DTCs.GDateNow("dd/MM/yyyy");
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text,"dd/MM/yyyy");
        }
        catch (Exception ex) { ClearDate(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getGDate() { return this.txtGDate.Text; }
    public string getHDate() { return this.txtHDate.Text; }

    public string getGDateDefFormat() { return DTCs.ToDefFormat(this.txtGDate.Text, "Gregorian"); } 
    public string getGDateDBFormat()  { return DTCs.ToDBFormat(this.txtGDate.Text, "Gregorian"); } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    protected void imgbtnClearCalendar_Click(object sender, ImageClickEventArgs e) 
    {  
        //CssStyleCollection pnlCalendarStyle = this.pnlGCal.Style;
        //string DISPLAY = pnlCalendarStyle["DISPLAY"];

        //if (DISPLAY == "none") { } else { this.pnlGCal.Attributes.Add("style", "DISPLAY: none; POSITION: absolute"); }
        //this.txtGDate.Text = this.txtHDate.Text = "";   
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int GetIntGDate()
    {
        if (!string.IsNullOrEmpty(txtGDate.Text))
        {
            string[] Dates = txtGDate.Text.Split('/');
            string Y = Dates[2];
            string M = Dates[1];
            string D = Dates[0];
            return (Convert.ToInt32(Y) * 10000) + (Convert.ToInt32(M) * 100) + Convert.ToInt32(D);
        }

        return 0;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void DateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvCompareDates))
            {
               string DateTo = ((TextBox)this.Parent.Parent.FindControl(CalTo).FindControl("lblCalendar").FindControl("txtGDate")).Text;

                if (!string.IsNullOrEmpty(txtGDate.Text) && !string.IsNullOrEmpty(DateTo))
                {
                    string msg = General.Msg("Start date more than End date", "تاريخ البداية اكبر من تاريخ الإنتهاء");
                    cvCompareDates.ErrorMessage = msg; 
                    cvCompareDates.Text = this.Server.HtmlDecode("&lt;img src='App_Themes/ThemeEn/Images/Validation/message_exclamation.png' title='" + msg + "' /&gt;"); 
                    
                    int iStartDate = DTCs.ConvertDateTimeToInt(txtGDate.Text, "Gregorian");
                    int iEndDate   = DTCs.ConvertDateTimeToInt(DateTo,"Gregorian");
                    if (iStartDate > iEndDate) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}