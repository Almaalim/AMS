using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SwapPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _SwpID;
	public string SwpID { get { return _SwpID; } set { if (_SwpID != value) { _SwpID = value; } } }

    string _SwpEmpID1;
    public string SwpEmpID1 { get { return _SwpEmpID1; } set { if (_SwpEmpID1 != value) { _SwpEmpID1 = value; } } }

    string _SwpStartDate1;
	public string SwpStartDate1 { get { return _SwpStartDate1; } set { if (_SwpStartDate1 != value) { _SwpStartDate1 = value; } } }

    string _SwpEndDate;
	public string SwpEndDate { get { return _SwpEndDate; } set { if (_SwpEndDate != value) { _SwpEndDate = value; } } }

    string _SwpEmpID2;
    public string SwpEmpID2 { get { return _SwpEmpID2; } set { if (_SwpEmpID2 != value) { _SwpEmpID2 = value; } } }
    
    string _SwpStartDate2;
	public string SwpStartDate2 { get { return _SwpStartDate2; } set { if (_SwpStartDate2 != value) { _SwpStartDate2 = value; } } }

    string _SwpType;
    public string SwpType { get { return _SwpType; } set { if (_SwpType != value) { _SwpType = value; } } }

    string _SwpDesc;
    public string SwpDesc { get { return _SwpDesc; } set { if (_SwpDesc != value) { _SwpDesc = value; } } }

    string _SwpAddBy;
    public string SwpAddBy { get { return _SwpAddBy; } set { if (_SwpAddBy != value) { _SwpAddBy = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}