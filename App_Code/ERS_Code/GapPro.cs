using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GapPro
{
    
    //GapID	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //EwrID	int	Unchecked
    //ExcID	int	Checked
    //GapDate	datetime	Unchecked
    //GapShift	tinyint	Checked
    //GapStartTime	datetime	Unchecked
    //GapEndTime	datetime	Unchecked
    //GapDuration		Checked
    //GapAlert	char(1)	Checked
    //UsrName	char(10)	Checked
    //GapGraceFlag	char(1)	Checked
    //GapDesc	varchar(255)	Checked
    //GapType	char(2)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _GapID;
	public string GapID { get { return _GapID; } set { if (_GapID != value) { _GapID = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _EwrID;
	public string EwrID { get { return _EwrID; } set { if (_EwrID != value) { _EwrID = value; } } }

    string _ExcID;
	public string ExcID { get { return _ExcID; } set { if (_ExcID != value) { _ExcID = value; } } }

    string _GapDate;
    public string GapDate { get { return _GapDate; } set { if (_GapDate != value) { _GapDate = value; } } }
    
    string _GapShift;
	public string GapShift { get { return _GapShift; } set { if (_GapShift != value) { _GapShift = value; } } }

    string _GapStartTime;
    public string GapStartTime { get { return _GapStartTime; } set { if (_GapStartTime != value) { _GapStartTime = value; } } }

    string _GapEndTime;
    public string GapEndTime { get { return _GapEndTime; } set { if (_GapEndTime != value) { _GapEndTime = value; } } }

    string _GapAlert;
    public string GapAlert { get { return _GapAlert; } set { if (_GapAlert != value) { _GapAlert = value; } } }

    string _UsrName;
    public string UsrName { get { return _UsrName; } set { if (_UsrName != value) { _UsrName = value; } } }

    string _GapGraceFlag;
	public string GapGraceFlag { get { return _GapGraceFlag; } set { if (_GapGraceFlag != value) { _GapGraceFlag = value; } } }

    string _GapDesc;
    public string GapDesc { get { return _GapDesc; } set { if (_GapDesc != value) { _GapDesc = value; } } }

    string _GapType;
    public string GapType { get { return _GapType; } set { if (_GapType != value) { _GapType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}