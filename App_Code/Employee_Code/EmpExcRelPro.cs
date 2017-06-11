using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpExcRelPro
{
    //ExrID	int	Unchecked
    //ExcID	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //ExrStartDate	datetime	Checked
    //ExrEndDate	datetime	Checked
    //ExrStartTime	datetime	Checked
    //ExrEndTime	datetime	Checked
    //ExrDesc	varchar(250)	Checked
    //WktID	int	Checked
    //ExrISOvernight	bit	Checked
    //ExrIsOverTime	bit	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ExrID;
	public string ExrID { get { return _ExrID; } set { if (_ExrID != value) { _ExrID = value; } } }

    string _ExcID;
    public string ExcID { get { return _ExcID; } set { if (_ExcID != value) { _ExcID = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _ExrStartDate;
	public string ExrStartDate { get { return _ExrStartDate; } set { if (_ExrStartDate != value) { _ExrStartDate = value; } } }

    string _ExrEndDate;
    public string ExrEndDate { get { return _ExrEndDate; } set { if (_ExrEndDate != value) { _ExrEndDate = value; } } }
    
    string _ExrStartTime;
	public string ExrStartTime { get { return _ExrStartTime; } set { if (_ExrStartTime != value) { _ExrStartTime = value; } } }

    string _ExrEndTime;
    public string ExrEndTime { get { return _ExrEndTime; } set { if (_ExrEndTime != value) { _ExrEndTime = value; } } }

    string _ExrDesc;
    public string ExrDesc { get { return _ExrDesc; } set { if (_ExrDesc != value) { _ExrDesc = value; } } }

    string _WktID;
    public string WktID { get { return _WktID; } set { if (_WktID != value) { _WktID = value; } } }

    bool _ExrISOvernight;
    public bool ExrISOvernight { get { return _ExrISOvernight; } set { if (_ExrISOvernight != value) { _ExrISOvernight = value; } } }

    bool _ExrIsOverTime;
    public bool ExrIsOverTime { get { return _ExrIsOverTime; } set { if (_ExrIsOverTime != value) { _ExrIsOverTime = value; } } }

    bool _ExrIsStopped;
    public bool ExrIsStopped { get { return _ExrIsStopped; } set { if (_ExrIsStopped != value) { _ExrIsStopped = value; } } }

    string _ExrAddBy;
    public string ExrAddBy { get { return _ExrAddBy; } set { if (_ExrAddBy != value) { _ExrAddBy = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpIDs;
	public string EmpIDs { get { return _EmpIDs; } set { if (_EmpIDs != value) { _EmpIDs = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}