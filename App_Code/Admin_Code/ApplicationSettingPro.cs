using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ApplicationSettingPro
{
    //AppCompany varchar(1000)   Checked
    //AppDisplay  varchar(1000)   Checked
    //AppCountry  varchar(1000)   Checked
    //AppCity varchar(1000)   Checked
    //AppAddress1 varchar(8000)   Checked
    //AppAddress2 varchar(8000)   Checked
    //AppTelNo1   varchar(1000)   Checked
    //AppTelNo2   varchar(1000)   Checked
    //AppPOBox    varchar(1000)   Checked
    //AppCalendar varchar(1)  Checked
    //AppDateFormat   varchar(20) Checked
    //AppDataLangRequired char(1)	Checked
    //AppMiniLogger_VerificationMethod    varchar(2)  Checked
    //AppSessionDuration  int Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _AppCompany;
	public string AppCompany { get { return _AppCompany; } set { if (_AppCompany != value) { _AppCompany = value; } } }

    string _AppDisplay;
    public string AppDisplay { get { return _AppDisplay; } set { if (_AppDisplay != value) { _AppDisplay = value; } } }

    string _AppCountry;
	public string AppCountry { get { return _AppCountry; } set { if (_AppCountry != value) { _AppCountry = value; } } }

    string _AppCity;
	public string AppCity { get { return _AppCity; } set { if (_AppCity != value) { _AppCity = value; } } }

    string _AppAddress1;
    public string AppAddress1 { get { return _AppAddress1; } set { if (_AppAddress1 != value) { _AppAddress1 = value; } } }
    
    string _AppAddress2;
    public string AppAddress2 { get { return _AppAddress2; } set { if (_AppAddress2 != value) { _AppAddress2 = value; } } }
    
    string _AppTelNo1;
	public string AppTelNo1 { get { return _AppTelNo1; } set { if (_AppTelNo1 != value) { _AppTelNo1 = value; } } }

    string _AppTelNo2;
    public string AppTelNo2 { get { return _AppTelNo2; } set { if (_AppTelNo2 != value) { _AppTelNo2 = value; } } }

    string _AppPOBox;
    public string AppPOBox { get { return _AppPOBox; } set { if (_AppPOBox != value) { _AppPOBox = value; } } }

    string _AppCalendar;
    public string AppCalendar { get { return _AppCalendar; } set { if (_AppCalendar != value) { _AppCalendar = value; } } }

    string _AppDateFormat;
    public string AppDateFormat { get { return _AppDateFormat; } set { if (_AppDateFormat != value) { _AppDateFormat = value; } } }

    string _AppDataLangRequired;
    public string AppDataLangRequired { get { return _AppDataLangRequired; } set { if (_AppDataLangRequired != value) { _AppDataLangRequired = value; } } }

    string _AppMiniLogger_VerificationMethod;
    public string AppMiniLogger_VerificationMethod { get { return _AppMiniLogger_VerificationMethod; } set { if (_AppMiniLogger_VerificationMethod != value) { _AppMiniLogger_VerificationMethod = value; } } }

    string _AppSessionDuration;
    public string AppSessionDuration { get { return _AppSessionDuration; } set { if (_AppSessionDuration != value) { _AppSessionDuration = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}