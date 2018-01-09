using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpWrkPro
{
    //EwrID	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //WktID	int	Unchecked
    //EwrStartDate	datetime	Unchecked
    //EwrEndDate	datetime	Checked
    //EwrSat	bit	Unchecked
    //EwrSun	bit	Unchecked
    //EwrMon	bit	Unchecked
    //EwrTue	bit	Unchecked
    //EwrWed	bit	Unchecked
    //EwrThu	bit	Unchecked
    //EwrFri	bit	Unchecked
    //GrpName	varchar(50)	Checked
    //RotID	int	Checked
    //SwpID	int	Checked
    //EwrWrkDefault	bit	Checked
    //EwrWrkDefaultForAll	bit	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EwrID;
	public string EwrID { get { return _EwrID; } set { if (_EwrID != value) { _EwrID = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _WktID;
	public string WktID { get { return _WktID; } set { if (_WktID != value) { _WktID = value; } } }

    string _EwrStartDate;
	public string EwrStartDate { get { return _EwrStartDate; } set { if (_EwrStartDate != value) { _EwrStartDate = value; } } }

    string _EwrEndDate;
    public string EwrEndDate { get { return _EwrEndDate; } set { if (_EwrEndDate != value) { _EwrEndDate = value; } } }
    
    bool _EwrSat;
	public bool EwrSat { get { return _EwrSat; } set { if (_EwrSat != value) { _EwrSat = value; } } }

    bool _EwrSun;
    public bool EwrSun { get { return _EwrSun; } set { if (_EwrSun != value) { _EwrSun = value; } } }

    bool _EwrMon;
    public bool EwrMon { get { return _EwrMon; } set { if (_EwrMon != value) { _EwrMon = value; } } }

    bool _EwrTue;
    public bool EwrTue { get { return _EwrTue; } set { if (_EwrTue != value) { _EwrTue = value; } } }

    bool _EwrWed;
    public bool EwrWed { get { return _EwrWed; } set { if (_EwrWed != value) { _EwrWed = value; } } }

    bool _EwrThu;
    public bool EwrThu { get { return _EwrThu; } set { if (_EwrThu != value) { _EwrThu = value; } } }

    bool _EwrFri;
    public bool EwrFri { get { return _EwrFri; } set { if (_EwrFri != value) { _EwrFri = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _GrpName;
	public string GrpName { get { return _GrpName; } set { if (_GrpName != value) { _GrpName = value; } } }

    string _RotID;
	public string RotID { get { return _RotID; } set { if (_RotID != value) { _RotID = value; } } }

    string _SwpID;
    public string SwpID { get { return _SwpID; } set { if (_SwpID != value) { _SwpID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool _EwrWrkDefault;
    public bool EwrWrkDefault { get { return _EwrWrkDefault; } set { if (_EwrWrkDefault != value) { _EwrWrkDefault = value; } } }

    bool _EwrWrkDefaultForAll;
    public bool EwrWrkDefaultForAll { get { return _EwrWrkDefaultForAll; } set { if (_EwrWrkDefaultForAll != value) { _EwrWrkDefaultForAll = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpIDs;
    public string EmpIDs { get { return _EmpIDs; } set { if (_EmpIDs != value) { _EmpIDs = value; } } }

    string _WktIDs;
	public string WktIDs { get { return _WktIDs; } set { if (_WktIDs != value) { _WktIDs = value; } } }

    string _DayNos;
	public string DayNos { get { return _DayNos; } set { if (_DayNos != value) { _DayNos = value; } } }

    string _FirstDayDate;
	public string FirstDayDate { get { return _FirstDayDate; } set { if (_FirstDayDate != value) { _FirstDayDate = value; } } }

    string _DayCount;
    public string DayCount { get { return _DayCount; } set { if (_DayCount != value) { _DayCount = value; } } }    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _DateLen;
	public string DateLen { get { return _DateLen; } set { if (_DateLen != value) { _DateLen = value; } } }
    
    string _SDates;
	public string SDates { get { return _SDates; } set { if (_SDates != value) { _SDates = value; } } }

    string _EDates;
	public string EDates { get { return _EDates; } set { if (_EDates != value) { _EDates = value; } } }

    string _GrpLen;
	public string GrpLen { get { return _GrpLen; } set { if (_GrpLen != value) { _GrpLen = value; } } }

    string _GrpIDs;
	public string GrpIDs { get { return _GrpIDs; } set { if (_GrpIDs != value) { _GrpIDs = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}