using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SchedulePro
{
    //SchID	int	Unchecked
    //SchNameEn	varchar(50)	Unchecked
    //SchNameAr	varchar(50)	Checked
    //SchIsActive	bit	Checked
    //SchType	varchar(20)	Unchecked
    //SchDescription	varchar(255)	Checked
    //SchStartDate	smalldatetime	Checked
    //SchEndDate	smalldatetime	Checked
    //SchStartHour	smallint	Checked
    //SchStartMin	smallint	Checked
    //SchEveryHour	smallint	Checked
    //SchEveryMinute	smallint	Checked
    //SchWeekOfMonth	char(1)	Checked
    //SchRepeatDays	int	Checked
    //SchRepeatWeeks	smallint	Checked
    //ReportID	varchar(10)	Unchecked
    //SchUsers	varchar(1000)	Checked
    //SchReportFormat	char(10)	Checked
    //SchEmailBodyContent	varchar(500)	Checked
    //SchEmailSubject	varchar(50)	Checked
    //SchLastRunTime	datetime	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _SchID;
	public string SchID { get { return _SchID; } set { if (_SchID != value) { _SchID = value; } } }

    string _SchNameAr;
    public string SchNameAr { get { return _SchNameAr; } set { if (_SchNameAr != value) { _SchNameAr = value; } } }

    string _SchNameEn;
	public string SchNameEn { get { return _SchNameEn; } set { if (_SchNameEn != value) { _SchNameEn = value; } } }

    bool _SchIsActive;
	public bool SchIsActive { get { return _SchIsActive; } set { if (_SchIsActive != value) { _SchIsActive = value; } } }

    string _SchType;
    public string SchType { get { return _SchType; } set { if (_SchType != value) { _SchType = value; } } }
    
    string _SchDescription;
	public string SchDescription { get { return _SchDescription; } set { if (_SchDescription != value) { _SchDescription = value; } } }

    string _SchStartDate;
    public string SchStartDate { get { return _SchStartDate; } set { if (_SchStartDate != value) { _SchStartDate = value; } } }

    string _SchEndDate;
    public string SchEndDate { get { return _SchEndDate; } set { if (_SchEndDate != value) { _SchEndDate = value; } } }

    string _SchStartHour;
    public string SchStartHour { get { return _SchStartHour; } set { if (_SchStartHour != value) { _SchStartHour = value; } } }

    string _SchStartMin;
    public string SchStartMin { get { return _SchStartMin; } set { if (_SchStartMin != value) { _SchStartMin = value; } } }

    string _SchEveryHour;
    public string SchEveryHour { get { return _SchEveryHour; } set { if (_SchEveryHour != value) { _SchEveryHour = value; } } }

    string _SchEveryMinute;
    public string SchEveryMinute { get { return _SchEveryMinute; } set { if (_SchEveryMinute != value) { _SchEveryMinute = value; } } }

    string _SchWeekOfMonth;
    public string SchWeekOfMonth { get { return _SchWeekOfMonth; } set { if (_SchWeekOfMonth != value) { _SchWeekOfMonth = value; } } }

    string _SchRepeatDays;
    public string SchRepeatDays { get { return _SchRepeatDays; } set { if (_SchRepeatDays != value) { _SchRepeatDays = value; } } }

    string _SchRepeatWeeks;
    public string SchRepeatWeeks { get { return _SchRepeatWeeks; } set { if (_SchRepeatWeeks != value) { _SchRepeatWeeks = value; } } }

    string _ReportID;
    public string ReportID { get { return _ReportID; } set { if (_ReportID != value) { _ReportID = value; } } }

    string _SchUsers;
    public string SchUsers { get { return _SchUsers; } set { if (_SchUsers != value) { _SchUsers = value; } } }

    string _SchReportFormat;
    public string SchReportFormat { get { return _SchReportFormat; } set { if (_SchReportFormat != value) { _SchReportFormat = value; } } }

    string _SchEmailBodyContent;
    public string SchEmailBodyContent { get { return _SchEmailBodyContent; } set { if (_SchEmailBodyContent != value) { _SchEmailBodyContent = value; } } }

    string _SchEmailSubject;
    public string SchEmailSubject { get { return _SchEmailSubject; } set { if (_SchEmailSubject != value) { _SchEmailSubject = value; } } }

    string _SchLastRunTime;
    public string SchLastRunTime { get { return _SchLastRunTime; } set { if (_SchLastRunTime != value) { _SchLastRunTime = value; } } }

    string _SchCalendar;
    public string SchCalendar { get { return _SchCalendar; } set { if (_SchCalendar != value) { _SchCalendar = value; } } }

    string _SchDays;
    public string SchDays { get { return _SchDays; } set { if (_SchDays != value) { _SchDays = value; } } }

    string _SchMonths;
    public string SchMonths { get { return _SchMonths; } set { if (_SchMonths != value) { _SchMonths = value; } } }

    string _SchWeekDays;
    public string SchWeekDays { get { return _SchWeekDays; } set { if (_SchWeekDays != value) { _SchWeekDays = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}