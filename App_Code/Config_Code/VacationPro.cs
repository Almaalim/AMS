using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class VacationPro
{
    //VtpID	int	Unchecked
    //VtpNameAr	varchar(100)	Checked
    //VtpNameEn	varchar(100)	Checked
    //VtpInitialAr	char(2)	Checked
    //VtpInitialEn	char(2)	Checked
    //VtpIsPaid	int	Checked
    //VtpDesc	varchar(255)	Checked
    //VtpStatus	bit	Unchecked
    //VtpMaxDays	int	Checked
    //VtpIsReset	bit	Checked
    //VtpIsMedicalReport	bit	Checked
    //VtpCategory	varchar(50)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _VtpID;
	public string  VtpID { get { return _VtpID; } set { if (_VtpID != value) { _VtpID = value; } } }

    string _VtpNameAr;
    public string VtpNameAr { get { return _VtpNameAr; } set { if (_VtpNameAr != value) { _VtpNameAr = value; } } }

    string _VtpNameEn;
	public string VtpNameEn { get { return _VtpNameEn; } set { if (_VtpNameEn != value) { _VtpNameEn = value; } } }

    string _VtpInitialAr;
	public string  VtpInitialAr { get { return _VtpInitialAr; } set { if (_VtpInitialAr != value) { _VtpInitialAr = value; } } }

    string _VtpInitialEn;
    public string VtpInitialEn { get { return _VtpInitialEn; } set { if (_VtpInitialEn != value) { _VtpInitialEn = value; } } }
    
    string _VtpIsPaid;
	public string  VtpIsPaid { get { return _VtpIsPaid; } set { if (_VtpIsPaid != value) { _VtpIsPaid = value; } } }

    string _VtpDesc;
    public string VtpDesc { get { return _VtpDesc; } set { if (_VtpDesc != value) { _VtpDesc = value; } } }

    string _VtpMaxDays;
    public string VtpMaxDays { get { return _VtpMaxDays; } set { if (_VtpMaxDays != value) { _VtpMaxDays = value; } } }

    string _VtpCategory;
    public string VtpCategory { get { return _VtpCategory; } set { if (_VtpCategory != value) { _VtpCategory = value; } } }

    bool _VtpIsReset;
    public bool VtpIsReset { get { return _VtpIsReset; } set { if (_VtpIsReset != value) { _VtpIsReset = value; } } }

    bool _VtpIsMedicalReport;
    public bool VtpIsMedicalReport { get { return _VtpIsMedicalReport; } set { if (_VtpIsMedicalReport != value) { _VtpIsMedicalReport = value; } } }

    bool _VtpStatus;
    public bool VtpStatus { get { return _VtpStatus; } set { if (_VtpStatus != value) { _VtpStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //EvsID	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //VtpID	int	Unchecked
    //EvsMaxDays	int	Unchecked
    
    string _EvsID;
	public string  EvsID { get { return _EvsID; } set { if (_EvsID != value) { _EvsID = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }
    
    //string _VtpID;
    //public string  VtpID { get { return _VtpID; } set { if (_VtpID != value) { _VtpID = value; } } }

    string _EvsMaxDays;
    public string EvsMaxDays { get { return _EvsMaxDays; } set { if (_EvsMaxDays != value) { _EvsMaxDays = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}