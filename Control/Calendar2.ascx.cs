using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

[ValidationProperty("Text")]
public partial class Control_Calendar2 : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    GregorianCalendar Grn = new GregorianCalendar();
    UmAlQuraCalendar Umq = new UmAlQuraCalendar();

    private CultureInfo HCulture = new CultureInfo("ar-SA");
    private CultureInfo GCulture = new CultureInfo("en-US");

    private CultureInfo selected_culture;

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

    public enum DefaultCalendarCultureEnum { Gregorian, Hijri }
    public DefaultCalendarCultureEnum DefaultCalendarCulture
    {
        get { return ((ViewState["DefaultCalendarCulture"] == null) ? DefaultCalendarCultureEnum.Gregorian : (DefaultCalendarCultureEnum)ViewState["DefaultCalendarCulture"]); }
        set { ViewState["DefaultCalendarCulture"] = value; }
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
    private bool _InitialValue = false;
    public bool InitialValue
    {
        get { return _InitialValue; }
        set { if (_InitialValue != value) { _InitialValue = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, System.EventArgs e)
    {
        CalendarEnum CalendarType;
        try { CalendarType = (CalendarEnum)ViewState["CalendarType"]; } catch { CalendarType = CalendarEnum.Both; }

        if (CalendarType == CalendarEnum.Gregorian) { txtHDate.Visible = tdHCal.Visible = false; ddlLocaleChoice.Enabled = false; }
        else if (CalendarType == CalendarEnum.Hijri) { txtGDate.Visible = tdGCal.Visible = false; ddlLocaleChoice.Enabled = false; }
        else if (CalendarType == CalendarEnum.Both) { ddlLocaleChoice.Enabled = true; }
        else if (CalendarType == CalendarEnum.System) { }

        DefaultCalendarCulture = DefaultCalendarCultureEnum.Gregorian;
        if (HttpContext.Current.Session["DateType"] != null)
        {
            string DateType = HttpContext.Current.Session["DateType"].ToString();
            if (DateType == "Gregorian") { DefaultCalendarCulture = DefaultCalendarCultureEnum.Gregorian; }
            else if (DateType == "Hijri") DefaultCalendarCulture = DefaultCalendarCultureEnum.Hijri;
        }

        if (DefaultCalendarCulture == DefaultCalendarCultureEnum.Gregorian)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }
        else if (DefaultCalendarCulture == DefaultCalendarCultureEnum.Hijri)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
        }

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

            if (DefaultCalendarCulture == DefaultCalendarCultureEnum.Gregorian)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(GCulture.Name);
                ddlLocaleChoice.SelectedValue = GCulture.Name;
                ddlYears.Items.Clear();
                ddlMonths.Items.Clear();
                DTCs.YearGPopulateList(ref ddlYears);
                DTCs.MonthGPopulateList(ref ddlMonths);

                Changedate("G");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(HCulture.Name);
                ddlLocaleChoice.SelectedValue = HCulture.Name;
                ddlYears.Items.Clear();
                ddlMonths.Items.Clear();
                DTCs.YearHPopulateList(ref ddlYears);
                DTCs.MonthHPopulateList(ref ddlMonths);

                Changedate("H");
            }

            if (InitialValue) { SetTodayDate(); }

            selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
            System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;

            ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivCal.ClientID, "document.getElementById('" + this.DivCal.ClientID + "').style.display = 'none'; ", true);
        }
        else
        {
            try
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

                    string str = "";
                    int i = strPostBackControlName.LastIndexOf("$");
                    if (i >= 0)
                    {
                        str = strPostBackControlName.Substring(strPostBackControlName.LastIndexOf("$"));
                    }

                    if (strPostBackControlName != "" && str == "$ddlLocaleChoice")
                    {
                        if (ddlLocaleChoice.SelectedValue == HCulture.Name)
                        {
                            ddlLocaleChoice.SelectedValue = GCulture.Name;
                            ddlYears.Items.Clear();
                            ddlMonths.Items.Clear();
                            DTCs.YearGPopulateList(ref ddlYears);
                            DTCs.MonthGPopulateList(ref ddlMonths);
                            Page.Culture = GCulture.Name;
                            Changedate("G");

                        }
                        else if (ddlLocaleChoice.SelectedValue == GCulture.Name)
                        {
                            ddlLocaleChoice.SelectedValue = HCulture.Name;
                            ddlMonths.Items.Clear();
                            ddlYears.Items.Clear();
                            DTCs.YearHPopulateList(ref ddlYears);
                            DTCs.MonthHPopulateList(ref ddlMonths);
                            Page.Culture = HCulture.Name;
                            Changedate("H");
                        }
                    }
                    //To hide the calendar div in case of any postback other than the three controls (Culture ddl, Year ddl, Month ddl)
                    ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivCal.ClientID, "document.getElementById('" + this.DivCal.ClientID + "').style.display = 'none';", true);
                }

                //to keep the selected culture in case the post back triggered by any control    
                selected_culture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
                System.Threading.Thread.CurrentThread.CurrentCulture = selected_culture;
            }
            catch (Exception ex) { }
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
    protected void CalDate_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            e.Cell.BackColor = System.Drawing.Color.White;
        }

        if (e.Day.Date.DayOfWeek == DayOfWeek.Friday || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
        {
            e.Cell.BackColor = System.Drawing.Color.LightSteelBlue;
            
            //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#EE8A1D");
        }        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CalDate_SelectionChanged(object sender, System.EventArgs e)
    {
        if (ddlLocaleChoice.SelectedValue == HCulture.Name)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
            this.txtHDate.Text = CalDate.SelectedDate.ToString("dd/MM/yyyy");
            this.txtGDate.Text = DTCs.HijriToGreg(this.txtHDate.Text, "dd/MM/yyyy");
            CalDate.SelectedDates.Clear();
        }
        else if (ddlLocaleChoice.SelectedValue == GCulture.Name)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
            this.txtGDate.Text = CalDate.SelectedDate.ToString("dd/MM/yyyy");
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text, "dd/MM/yyyy");
            CalDate.SelectedDates.Clear();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocaleChoice.SelectedValue == HCulture.Name) { Changedate("H"); } else if (ddlLocaleChoice.SelectedValue == GCulture.Name) { Changedate("G"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocaleChoice.SelectedValue == HCulture.Name) { Changedate("H"); } else if (ddlLocaleChoice.SelectedValue == GCulture.Name) { Changedate("G"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Changedate(string DTType)
    {
        try
        {
            if (DTType == "G")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);

                if (ddlYears.SelectedValue == DTCs.GDateNow("yyyy") && Convert.ToInt32(ddlMonths.SelectedValue) == Convert.ToInt32(DTCs.GDateNow("MM")))
                {
                    CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), Convert.ToInt32(DTCs.GDateNow("dd")));
                }
                else
                {
                    CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), 1);
                }
            }
            else if (DTType == "H")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlLocaleChoice.SelectedValue);
                
                if (ddlYears.SelectedValue == DTCs.HDateNow("yyyy") && Convert.ToInt32(ddlMonths.SelectedValue) == Convert.ToInt32(DTCs.HDateNow("MM")))
                {
                    CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), Convert.ToInt32(DTCs.HDateNow("dd")), Umq);
                }
                else
                {
                    CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), 1, Umq);
                }
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlLocaleChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMonths.Items.Clear();
        ddlYears.Items.Clear();
        if (ddlLocaleChoice.SelectedValue == HCulture.Name)
        {
            DTCs.YearHPopulateList(ref ddlYears);
            DTCs.MonthHPopulateList(ref ddlMonths);
            Changedate("H");
        }
        else if (ddlLocaleChoice.SelectedValue == GCulture.Name)
        {
            DTCs.YearGPopulateList(ref ddlYears);
            DTCs.MonthGPopulateList(ref ddlMonths);
            Changedate("G");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(CalendarUpdatePanel, typeof(string), "ShowPopup" + this.DivCal.ClientID, "document.getElementById('" + this.DivCal.ClientID + "').style.display = 'none';", true);
        this.txtGDate.Text = this.txtHDate.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region public Events

    public void SetVisible(bool Status)
    {
        lblCalendar.Visible = Status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetEnabled(bool Status)
    {
        if (Status)
        {
            txtGDate.Attributes.Add("onclick", "showHide('" + this.DivCal.ClientID + "');");
            txtHDate.Attributes.Add("onclick", "showHide('" + this.DivCal.ClientID + "');");
            imgG.Attributes.Add("onclick", "showHide('" + this.DivCal.ClientID + "');");
            imgH.Attributes.Add("onclick", "showHide('" + this.DivCal.ClientID + "');");
            imgCal.Attributes.Add("onclick", "showHide('" + this.DivCal.ClientID + "');");

            string GDate = (InitialValue) ? DTCs.GDateNow("dd/MM/yyyy") : "";
            string HDate = (InitialValue) ? DTCs.GregToHijri(GDate, "dd/MM/yyyy") : "";

            imgClear.Attributes.Add("onclick", "Clear('" + this.DivCal.ClientID + "','" + this.txtGDate.ClientID + "','" + this.txtHDate.ClientID + "','" + GDate + "','" + HDate + "');");
        }
        else
        {
            txtGDate.Attributes.Remove("onclick");
            txtHDate.Attributes.Remove("onclick");
            imgG.Attributes.Remove("onclick");
            imgH.Attributes.Remove("onclick");
            imgCal.Attributes.Remove("onclick");

            imgClear.Attributes.Remove("onclick");
        }

        //imgbtnClear.Enabled = Status;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetValidationEnabled(bool Status)
    {
        this.rvDate.Enabled = Status;
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
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text, "dd/MM/yyyy");
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
            this.txtGDate.Text = DTCs.HijriToGreg(this.txtHDate.Text, "dd/MM/yyyy");
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
            this.txtHDate.Text = DTCs.GregToHijri(this.txtGDate.Text, "dd/MM/yyyy");
        }
        catch (Exception ex) { ClearDate(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getGDate() { return this.txtGDate.Text; }
    public string getHDate() { return this.txtHDate.Text; }

    public string getGDateDefFormat() { return DTCs.ToDefFormat(this.txtGDate.Text, "Gregorian"); }
    public string getGDateDBFormat() { return DTCs.ToDBFormat(this.txtGDate.Text, "Gregorian"); }
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

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
                    int iEndDate = DTCs.ConvertDateTimeToInt(DateTo, "Gregorian");
                    if (iStartDate > iEndDate) { e.IsValid = false; }
                }
            }
        }
        catch (Exception ex) { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}