using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SwapPro
{
    //SwpID	int	Unchecked
    //EmpID	varchar(15)	Checked
    //SwpStartDate	datetime	Checked
    //SwpEndDate	datetime	Checked
    //EmpID2	varchar(15)	Checked
    //SwpStartDate2	datetime	Checked
    //SwpEndDate2	datetime	Checked
    //WktID	int	Checked
    //SwpType	varchar(15)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _SwpID;
	public string SwpID { get { return _SwpID; } set { if (_SwpID != value) { _SwpID = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _SwpStartDate;
	public string SwpStartDate { get { return _SwpStartDate; } set { if (_SwpStartDate != value) { _SwpStartDate = value; } } }

    string _SwpEndDate;
	public string SwpEndDate { get { return _SwpEndDate; } set { if (_SwpEndDate != value) { _SwpEndDate = value; } } }

    string _EmpID2;
    public string EmpID2 { get { return _EmpID2; } set { if (_EmpID2 != value) { _EmpID2 = value; } } }
    
    string _SwpStartDate2;
	public string SwpStartDate2 { get { return _SwpStartDate2; } set { if (_SwpStartDate2 != value) { _SwpStartDate2 = value; } } }

    string _SwpEndDate2;
    public string SwpEndDate2 { get { return _SwpEndDate2; } set { if (_SwpEndDate2 != value) { _SwpEndDate2 = value; } } }

    string _WktID;
    public string WktID { get { return _WktID; } set { if (_WktID != value) { _WktID = value; } } }

    string _SwpType;
    public string SwpType { get { return _SwpType; } set { if (_SwpType != value) { _SwpType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}