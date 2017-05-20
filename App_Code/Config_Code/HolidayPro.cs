using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class HolidayPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _HolID;
	public string  HolID { get { return _HolID; } set { if (_HolID != value) { _HolID = value; } } }

    string _HolNameAr;
    public string HolNameAr { get { return _HolNameAr; } set { if (_HolNameAr != value) { _HolNameAr = value; } } }

    string _HolNameEn;
	public string  HolNameEn { get { return _HolNameEn; } set { if (_HolNameEn != value) { _HolNameEn = value; } } }

    string _HolDesc;
	public string  HolDesc { get { return _HolDesc; } set { if (_HolDesc != value) { _HolDesc = value; } } }

    string _HolStartDate;
    public string HolStartDate { get { return _HolStartDate; } set { if (_HolStartDate != value) { _HolStartDate = value; } } }
    
    string _HolEndDate;
	public string  HolEndDate { get { return _HolEndDate; } set { if (_HolEndDate != value) { _HolEndDate = value; } } }

    bool _HolStatus;
    public bool HolStatus { get { return _HolStatus; } set { if (_HolStatus != value) { _HolStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}