using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ExcusePro
{
    //ExcID	int	Unchecked
    //ExcNameAr	varchar(100)	Checked
    //ExcNameEn	varchar(100)	Checked
    //ExcInitialAr	char(2)	Checked
    //ExcInitialEn	char(2)	Checked
    //ExcIsPaid	char(1)	Checked
    //ExcDesc	varchar(255)	Checked
    //ExcStatus	bit	Unchecked
    //ExcMaxHoursPerMonth	int	Checked
    //ExcCategory	varchar(3)	Checked
    //ExcPercentageAllowable	int	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ExcID;
	public string  ExcID { get { return _ExcID; } set { if (_ExcID != value) { _ExcID = value; } } }

    string _ExcNameAr;
    public string ExcNameAr { get { return _ExcNameAr; } set { if (_ExcNameAr != value) { _ExcNameAr = value; } } }

    string _ExcNameEn;
	public string  ExcNameEn { get { return _ExcNameEn; } set { if (_ExcNameEn != value) { _ExcNameEn = value; } } }

    string _ExcInitialAr;
	public string  ExcInitialAr { get { return _ExcInitialAr; } set { if (_ExcInitialAr != value) { _ExcInitialAr = value; } } }

    string _ExcInitialEn;
    public string ExcInitialEn { get { return _ExcInitialEn; } set { if (_ExcInitialEn != value) { _ExcInitialEn = value; } } }
    
    string _ExcIsPaid;
	public string  ExcIsPaid { get { return _ExcIsPaid; } set { if (_ExcIsPaid != value) { _ExcIsPaid = value; } } }

    string _ExcDesc;
    public string ExcDesc { get { return _ExcDesc; } set { if (_ExcDesc != value) { _ExcDesc = value; } } }

    string _ExcMaxHoursPerMonth;
    public string ExcMaxHoursPerMonth { get { return _ExcMaxHoursPerMonth; } set { if (_ExcMaxHoursPerMonth != value) { _ExcMaxHoursPerMonth = value; } } }

    string _ExcCategory;
    public string ExcCategory { get { return _ExcCategory; } set { if (_ExcCategory != value) { _ExcCategory = value; } } }

    string _ExcPercentageAllowable;
    public string ExcPercentageAllowable { get { return _ExcPercentageAllowable; } set { if (_ExcPercentageAllowable != value) { _ExcPercentageAllowable = value; } } }

    bool _ExcStatus;
    public bool ExcStatus { get { return _ExcStatus; } set { if (_ExcStatus != value) { _ExcStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}