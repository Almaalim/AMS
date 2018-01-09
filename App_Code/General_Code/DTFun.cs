using System;
using System.Web;
using System.Diagnostics;
using System.Globalization;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Elmah;

public class DTFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	static PageFun pgCs = new PageFun();
    
    private HttpContext cur;
	private const int startGreg = 1900;
	private const int endGreg = 2100; //"MM/dd/yyyy",
    private string[] allFormats = {"yyyy/MM/dd","yyyy/M/d","dd/MM/yyyy","d/M/yyyy","dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd","yyyy-M-d","dd-MM-yyyy","d-M-yyyy","dd-M-yyyy","d-MM-yyyy","yyyy MM dd","yyyy M d","dd MM yyyy","d M yyyy","dd M yyyy","d MM yyyy"};
    private string[] allFormats2 = { "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy" };
    private CultureInfo arCul;
	private CultureInfo enCul;

    private UmAlQuraCalendar Umq;
	private GregorianCalendar Grn;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
    public enum CalenderType { G, H };

    string[] GMEn = { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    string[] GMAr = { "", "يناير", "فبراير", "مارس", "ابريل", "مايو", "يونيو", "يوليو", "اغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
        
    string[] HMEn = { "", "Muharram", "Safar", "Rabii I", "Rabii II", "Jumada I", "Jumada II", "Rajab", "Sha'aban", "Ramadan", "Shawwal", "Dhu al-Qa'aida", "Dhual-Hijja" };
    string[] HMAr = { "", "محرم", "صفر", "ربيع اول", "ربيع ثاني", "جمادى الأول", "جمادى الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذو القعدة", "ذو الحجة" };
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public CalenderType CurrentCalander()
    //{
    //    return DateTime.Now.ToString(format,enCul.DateTimeFormat);
    //} 
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	public DTFun()
	{
		cur   = HttpContext.Current;
		arCul = new CultureInfo("ar-SA");
		enCul = new CultureInfo("en-US");
        Umq   = new UmAlQuraCalendar();
		Grn   = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
		arCul.DateTimeFormat.Calendar = Umq;	
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
	/// Check if string is hijri date and then return true 
	/// </summary>
	/// <param name="hijri"></param>
	/// <returns></returns>
	public bool IsHijri(string hijri)
	{
		if (hijri.Length<=0)
		{
			cur.Trace.Warn("IsHijri Error: Date String is Empty");
			return false;
		}
		try
		{	
			DateTime tempDate=DateTime.ParseExact(hijri,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			if (tempDate.Year>=startGreg && tempDate.Year<=endGreg)
				return true;
			else
				return false;
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("IsHijri Error :"+hijri.ToString()+"\n"+ex.Message);
			return false;
		}

	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
    /// <summary>
	/// Check if string is Gregorian date and then return true 
	/// </summary>
	/// <param name="greg"></param>
	/// <returns></returns>
	public bool IsGreg(string greg)
	{
		if (greg.Length<=0)
		{
			cur.Trace.Warn("IsGreg :Date String is Empty");
			return false;
		}
		try
		{	
			DateTime tempDate=DateTime.ParseExact(greg,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			if (tempDate.Year>=startGreg && tempDate.Year<=endGreg)
				return true;
			else
				return false;
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("IsGreg Error :"+greg.ToString()+"\n"+ex.Message);
			return false;
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Return Formatted Hijri date string 
	/// </summary>
	/// <param name="date"></param>
	/// <param name="format"></param>
	/// <returns></returns>
	public string FormatHijri(string date ,string format)
	{
		if (date.Length<=0)
		{
			cur.Trace.Warn("Format :Date String is Empty");
			return "";
		}
		try
		{					   
			DateTime tempDate = DateTime.ParseExact(date,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString(format,arCul.DateTimeFormat);
							
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("Date :\n"+ex.Message);
			return "";
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
	/// Returned Formatted Gregorian date string
	/// </summary>
	/// <param name="date"></param>
	/// <param name="format"></param>
	/// <returns></returns>
	public string FormatGreg(string date ,string format)
	{
		if (date.Length<=0)
		{	
			cur.Trace.Warn("Format :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate = DateTime.ParseExact(date,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString(format,enCul.DateTimeFormat);
							
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("Date :\n"+ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Return Today Gregorian date and return it in yyyy/MM/dd format
	/// </summary>
	/// <returns></returns>
	public string GDateNow()
	{
		try
		{
			return DateTime.Now.ToString("yyyy/MM/dd",enCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("GDateNow :\n" + ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Return formatted today Gregorian date based on your format
	/// </summary>
	/// <param name="format"></param>
	/// <returns></returns>
	public string GDateNow(string format)
	{
		try
		{
			return DateTime.Now.ToString(format,enCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("GDateNow :\n"+ ex.Message);
			return "";
		}
	} 
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	/// <summary>
	/// Return Today Hijri date and return it in yyyy/MM/dd format
	/// </summary>
	/// <returns></returns>
	public string HDateNow()
	{
		try
		{
			return DateTime.Now.ToString("yyyy/MM/dd",arCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("HDateNow :\n"+ex.Message);
			return "";
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
    /// <summary>
	/// Return formatted today hijri date based on your format
	/// </summary>
	/// <param name="format"></param>
	/// <returns></returns>
	public string HDateNow(string format)
	{
		try
		{
			return DateTime.Now.ToString(format,arCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("HDateNow :\n"+ex.Message);
			return "";
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	/// <summary>
	/// Convert Hijri Date to it's equivalent Gregorian Date
	/// </summary>
	/// <param name="hijri"></param>
	/// <returns></returns>
	public string HijriToGreg(string hijri)
	{
			
		if (hijri.Length<=0)
		{
			cur.Trace.Warn("HijriToGreg :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate=DateTime.ParseExact(hijri,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString("yyyy/MM/dd",enCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("HijriToGreg :"+hijri.ToString()+"\n" + ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Convert Hijri Date to it's equivalent Gregorian Date and return it in specified format
	/// </summary>
	/// <param name="hijri"></param>
	/// <param name="format"></param>
	/// <returns></returns>
	public string HijriToGreg(string hijri,string format)
	{	
		if (hijri.Length <= 0)
		{
			cur.Trace.Warn("HijriToGreg :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate=DateTime.ParseExact(hijri,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString(format,enCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("HijriToGreg :"+hijri.ToString()+"\n"+ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Convert Gregoian Date to it's equivalent Hijir Date
	/// </summary>
	/// <param name="greg"></param>
	/// <returns></returns>
	public string GregToHijri(string greg)
	{
		if (greg.Length<=0)
		{
				
			cur.Trace.Warn("GregToHijri :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate=DateTime.ParseExact(greg,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString("yyyy/MM/dd",arCul.DateTimeFormat);
				
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("GregToHijri :"+greg.ToString()+"\n"+ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Convert Hijri Date to it's equivalent Gregorian Date and return it in specified format
	/// </summary>
	/// <param name="greg"></param>
	/// <param name="format"></param>
	/// <returns></returns>
	public string GregToHijri(string greg, string format)
	{
		if (greg.Length<=0)
		{
			cur.Trace.Warn("GregToHijri :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate = DateTime.ParseExact(greg,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return tempDate.ToString(format,arCul.DateTimeFormat);
				
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("GregToHijri :"+greg.ToString()+"\n"+ex.Message);
			return "";
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	/// <summary>
	/// Return Gregrian Date Time as digit stamp
	/// </summary>
	/// <returns></returns>
	public string GTimeStamp()
	{
		return GDateNow("yyyyMMddHHmmss");
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Return Hijri Date Time as digit stamp
	/// </summary>
	/// <returns></returns>
	public string HTimeStamp()
	{
		return HDateNow("yyyyMMddHHmmss");
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////			
	/// <summary>
	/// Compare two instaces of string date and return indication of thier values 
	/// </summary>
	/// <param name="d1"></param>
	/// <param name="d2"></param>
	/// <returns>positive d1 is greater than d2,negative d1 is smaller than d2, 0 both are equal</returns>
	public int Compare(string d1,string d2)
	{
		try
		{
			DateTime date1 = DateTime.ParseExact(d1,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			DateTime date2 = DateTime.ParseExact(d2,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
			return DateTime.Compare(date1,date2);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("Compare :"+"\n"+ex.Message);
			return -1;
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void YearPopulateList(ref DropDownList ddl, string pDateType, bool IsAll)
    {
        if (IsAll) { ListItem _li = new ListItem(General.Msg("All", "الكل"), "0"); /**/  ddl.Items.Add(_li); }

        if (pDateType == "Gregorian")
        {
            Int32 Y = Convert.ToInt32(FindCurrentYear("Gregorian"));

            int minYear = 1997; //DateTime.Now.Year - 5;
            int maxYear = Y + 10; //DateTime.Now.Year

            for (int i = maxYear; i >= minYear; i--) { ddl.Items.Add(i.ToString()); }

            ddl.Items.FindByValue(Y.ToString()).Selected = true;
        }
        else if (pDateType == "Hijri")
        {
            //string date = HDateNow("dd/MM/yyyy"); //GregToHijri(DateTime.Now.ToString("dd/MM/yyyy"));

            //string[] arrDate = date.Split('/');
            //Int32 Y = Convert.ToInt32(arrDate[0]);
            //Int32 M = Convert.ToInt32(arrDate[1]);
            //Int32 D = Convert.ToInt32(arrDate[2]);

            Int32 Y = Convert.ToInt32(FindCurrentYear("Hijri"));

            int minYear = 1400; //Y - 5;
            int maxYear = 1450;

            for (int i = maxYear; i >= minYear; i--) { ddl.Items.Add(i.ToString()); }

            ddl.Items.FindByValue(Y.ToString()).Selected = true;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void YearGPopulateList(ref DropDownList ddl)
    {
        YearPopulateList(ref ddl, "Gregorian", false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void YearHPopulateList(ref DropDownList ddl)
    {
        YearPopulateList(ref ddl, "Hijri", false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void YearPopulateList(ref DropDownList ddl)
    {
        pgCs.FillDTSession();
        YearPopulateList(ref ddl, pgCs.DateType, false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MonthPopulateList(ref DropDownList ddl, string pDateType, bool IsAll)
    {
        ListItem item = new ListItem();
        ddl.Items.Clear();

        if (IsAll) { ListItem _li = new ListItem(General.Msg("All", "الكل"), "0"); /**/  ddl.Items.Add(_li); }

        for (int i = 1; i < 13; i++)
        {
            item = new ListItem();

            if (pDateType == "Gregorian") { item.Text = General.Msg(GMEn[i],GMAr[i]); } else if (pDateType == "Hijri") { item.Text = General.Msg(HMEn[i],HMAr[i]); }
            item.Value = i.ToString();
            ddl.Items.Add(item);
        }
        
        if (pDateType == "Gregorian")
        {
            if (IsAll) { ddl.SelectedIndex = DateTime.Now.Month; } else { ddl.SelectedIndex = DateTime.Now.Month - 1; }
        }
        else if (pDateType == "Hijri")
        {
            string date = HDateNow("dd/MM/yyyy");// GregToHijri(DateTime.Now.ToString("dd/MM/yyyy"));
            string[] arrDate = date.Split('/');
            Int32 M = Convert.ToInt32(arrDate[1]);
            if (IsAll) { ddl.SelectedIndex = M; } else { ddl.SelectedIndex = M - 1; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public void MonthGPopulateList(ref DropDownList ddl)
    {
        MonthPopulateList(ref ddl, "Gregorian", false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MonthHPopulateList(ref DropDownList ddl)
    {
        MonthPopulateList(ref ddl, "Hijri", false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MonthPopulateList(ref DropDownList ddl)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        pgCs.FillDTSession();

        MonthPopulateList(ref ddl, pgCs.DateType, false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MonthPopulateList(ref CheckBoxList _cbl, string DateType)
    {
        ListItem item = new ListItem();

        for (int i = 1; i < 13; i++)
        {
            item = new ListItem();

            if (DateType == "Gregorian") { item.Text = General.Msg(GMEn[i], GMAr[i]); } else if (DateType == "Hijri") { item.Text = General.Msg(HMEn[i], HMAr[i]); }
            item.Value = i.ToString();
            _cbl.Items.Add(item);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public string FindCurrentDate(string DateFormat)
    {
        try
        {
            pgCs.FillDTSession();
            if (pgCs.DateType == "Gregorian") { return GDateNow(DateFormat); } else if (pgCs.DateType == "Hijri") { return HDateNow(DateFormat); }

            return "0";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public string FindCurrentDate()
    {
        pgCs.FillDTSession();
        return FindCurrentDate(pgCs.DateFormat);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCurrentYear(string DateType)
    {
        try
        {
            if (DateType == "Gregorian") { return GDateNow("yyyy"); } else if (DateType == "Hijri") { return HDateNow("yyyy"); }
            return "0";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCurrentYear()
    {
        pgCs.FillDTSession();
        return FindCurrentYear(pgCs.DateType);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCurrentMonth(string DateType)
    {
        try
        {
            if (DateType == "Gregorian") { return GDateNow("MM"); } else if (DateType == "Hijri") { return HDateNow("MM"); }
            return "0";
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCurrentMonth()
    {
        pgCs.FillDTSession();
        return FindCurrentMonth(pgCs.DateType);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindMonthDates(string DateType, string Year, string Month,out DateTime StartDate, out DateTime EndDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        DateTime SDate = DateTime.Now;
        DateTime EDate = DateTime.Now;
        int iYear  = Convert.ToInt16(Year);
        int iMonth = Convert.ToInt16(Month);
        
        if (DateType == "Gregorian")
        {
            int LastDay = Grn.GetDaysInMonth(iYear, iMonth);
            SDate = new DateTime(iYear, iMonth, 1);
            EDate = new DateTime(iYear, iMonth, LastDay);
        }
        else
        {
            int LastDay = Umq.GetDaysInMonth(iYear, iMonth);
            string HStartDate = "1"     + "/" + iMonth + "/" + iYear;
            string HEndDate   = LastDay + "/" + iMonth + "/" + iYear;
            SDate = ConvertToDatetime(HijriToGreg(HStartDate), "Gregorian");
            EDate = ConvertToDatetime(HijriToGreg(HEndDate), "Gregorian");
        }

        StartDate = SDate;
        EndDate   = EDate;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public void FindMonthDates(string Year, string Month,out DateTime StartDate, out DateTime EndDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        pgCs.FillDTSession();

        DateTime SDate = DateTime.Now;
        DateTime EDate = DateTime.Now;
        FindMonthDates(pgCs.DateType, Year, Month,out SDate, out EDate);
        
        StartDate = SDate;
        EndDate   = EDate;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int ConvertDateTimeToInt(string pDate, string pDateType)
    {
        string[] Dates = (ToDefFormat(pDate, pDateType)).Split('/');
        string Y = Dates[2];
        string M = Dates[1];
        string D = Dates[0];
        return (Convert.ToInt32(Y) * 10000) + (Convert.ToInt32(M) * 100) + Convert.ToInt32(D);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ToDefFormat(string pDate, string pDateType)
    {
        if (pDateType == "Gregorian") { return FormatGreg(pDate, "dd/MM/yyyy"); } else { return FormatHijri(pDate, "dd/MM/yyyy"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ToDBFormat(string pDate, string pDateType)
    {
        if (pDateType == "Gregorian") { return FormatGreg(pDate, "MM/dd/yyyy"); } else { return FormatHijri(pDate, "MM/dd/yyyy"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetGDatePart(string part, string date, string format) // part = D,M,Y
	{
		if (date.Length <= 0)
		{	
			cur.Trace.Warn("Format :Date String is Empty");
			return "";
		}
		try
		{
			DateTime tempDate = DateTime.ParseExact(date,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
            if      (part == "D") { return tempDate.Day.ToString(); }
            else if (part == "M") { return tempDate.Month.ToString(); }
            else if (part == "Y") { return tempDate.Year.ToString(); }
            
            return "";
							
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("Date :\n"+ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetHDatePart(string part, string date, string format) // part = D,M,Y
	{
		if (date.Length <= 0)
		{	
			cur.Trace.Warn("Format :Date String is Empty");
			return "";
		}
		try
		{
            string HD = GregToHijri(date, format);

            string[] Dates = HD.Split('/');
            string Y = Dates[2];
            string M = Dates[1];
            string D = Dates[0];

			//DateTime tempDate = DateTime.ParseExact(date,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
            if      (part == "D") { return Dates[0]; }
            else if (part == "M") { return Dates[1]; }
            else if (part == "Y") { return Dates[2]; }
            
            return "";
							
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("Date :\n"+ex.Message);
			return "";
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DateTime ConvertToDatetime(string Date, string DateType)
	{
        //////System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        if (DateType == "Gregorian")
        {
            return DateTime.ParseExact(Date,allFormats,enCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
        }
        else if (DateType == "Hijri")
        {
            return DateTime.ParseExact(Date,allFormats,arCul.DateTimeFormat,DateTimeStyles.AllowWhiteSpaces);
        }

        return new DateTime();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DateTime ConvertToDatetime2(string Date, string DateType)
    {
        //////System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        if (DateType == "Gregorian")
        {
            return DateTime.ParseExact(Date, allFormats2, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
        }
        else if (DateType == "Hijri")
        {
            return DateTime.ParseExact(Date, allFormats2, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
        }

        return new DateTime();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetGregDateFromCurrDateType(string pDate, string pDateFormat)
    {
        if (HttpContext.Current.Session["DateType"].ToString() == "Gregorian") {return FormatGreg(pDate, pDateFormat); } else { return HijriToGreg(pDate, pDateFormat); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowDBDate(object Date, string format)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (Date != DBNull.Value) 
        {
            DateTime DT = (DateTime)Date;
            
            if (HttpContext.Current.Session["DateType"].ToString() == "Gregorian") 
            {
                
                return FormatGreg(DT.ToString("dd/MM/yyyy"), format);
            } 
            else 
            {
                string GD = FormatGreg(Date.ToString(), "dd/MM/yyyy");
                return GregToHijri(Convert.ToDateTime(DT).ToString("dd/MM/yyyy"), format);
            }
        }

        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowDBDate(object Date, string DateType, string format)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (Date != DBNull.Value)
        {
            DateTime DT = (DateTime)Date;

            if (DateType == "Gregorian")
            {
                string GD = FormatGreg(Date.ToString(), "dd/MM/yyyy");
                return FormatGreg(DT.ToString("dd/MM/yyyy"), format);
            }
            else
            {
                string GD = FormatGreg(Date.ToString(), "dd/MM/yyyy");
                return GregToHijri(Convert.ToDateTime(DT).ToString("dd/MM/yyyy"), format);
            }
        }

        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowDBTime(object Date)
    {
        try
        {
            DateTime DT = Convert.ToDateTime(Date);
            return string.Format("{0:hh\\:mm\\:ss}", DT);
            
            //if (!string.IsNullOrEmpty(Date.ToString()))
            //{
            //    string[] Parts = Date.ToString().Split(' ');
            //    return Parts[1];
            //}
        }
        catch (Exception e1) { }

        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindLastDay(int pMonth, int pYear)
    {
        if (HttpContext.Current.Session["DateType"].ToString() == "Gregorian") { return Grn.GetDaysInMonth(pYear, pMonth); } else { return Umq.GetDaysInMonth(pYear, pMonth); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DateTime FindDateFromPart(int pDay, int pMonth, int pYear, string DateType)
    {
        if (DateType == "Gregorian") { return new DateTime(pYear, pMonth, pDay, Grn); } else { return new DateTime(pYear, pMonth, pDay, Umq); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public DateTime FindDateFromPart(int iDay, int iMonth, int iYear)
    {
        return FindDateFromPart(iDay, iMonth, iYear, HttpContext.Current.Session["DateType"].ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ShowDurtion(int seconds)
    {
        if (seconds == 0) { return " "; }

        TimeSpan ts = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:hh\\:mm\\:ss}", ts);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindDuration(int[] TimeFrom, int[] TimeTo) //int[3] { H, MI, S }
    {
        TimeSpan tsFrom = new TimeSpan(TimeFrom[0], TimeFrom[1], TimeFrom[2]);
        TimeSpan tsTo   = new TimeSpan(TimeTo[0], TimeTo[1], TimeTo[2]);

        if (tsTo.TotalSeconds >= tsFrom.TotalSeconds) { return Convert.ToInt32(tsTo.TotalSeconds - tsFrom.TotalSeconds); }
        else
        {
            if (TimeFrom[0] == 24)
            {
                tsFrom = new TimeSpan(0, TimeFrom[1], TimeFrom[2]);
                return Convert.ToInt32(tsTo.TotalSeconds - tsFrom.TotalSeconds);
            }
            else
            {
                TimeSpan ts24 = new TimeSpan(24, 0, 0);
                return Convert.ToInt32((ts24.TotalSeconds - tsFrom.TotalSeconds) + tsTo.TotalSeconds); 
            }
        } 
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GDateYesterday(string format)
	{
		try
		{
			return DateTime.Today.AddDays(-1).ToString(format,enCul.DateTimeFormat);
		}
		catch (Exception ex)
		{
			cur.Trace.Warn("GDateNow :\n"+ ex.Message);
			return "";
		}
	} 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsGregDateType(object DateType)
	{
		if (DateType.ToString() == "Gregorian") { return true; } else { return false; }
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetMonthName(string DateType, string Lang)
    {
        int M = Convert.ToInt32(FindCurrentMonth(DateType));
        return General.Msg(Lang,GMEn[M],GMAr[M]);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string TimeNow()
    {
        return GDateNow("HH:mm:ss");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int iTimeNow()
    {
        string H = GDateNow("HH");
        string M = GDateNow("mm");
        string S = GDateNow("ss");

        return Convert.ToInt32(H + M + S);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

