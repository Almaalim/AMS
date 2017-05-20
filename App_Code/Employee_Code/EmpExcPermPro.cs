using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpExcPermPro
{
    //ExprID	int	Unchecked
    //ExprType	varchar(3)	Unchecked
    //EmpID	varchar(15)	Unchecked
    //ExprDate	datetime	Checked
    //ExcID	int	Checked
    //VtpID	int	Checked
    //ShiftID	int	Checked
    //ExprDesc	varchar(250)	Checked
    //ExprVacIfNoPresent	bit	Checked
    //ExprStartDate	datetime	Checked
    //ExprEndDate	datetime	Checked
    //ExcType	varchar(2)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ExprID;
	public string ExprID { get { return _ExprID; } set { if (_ExprID != value) { _ExprID = value; } } }

    string _ExprType;
    public string ExprType { get { return _ExprType; } set { if (_ExprType != value) { _ExprType = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _ExcID;
    public string ExcID { get { return _ExcID; } set { if (_ExcID != value) { _ExcID = value; } } }
    
    string _VtpID;
	public string VtpID { get { return _VtpID; } set { if (_VtpID != value) { _VtpID = value; } } }

    string _ShiftID;
    public string ShiftID { get { return _ShiftID; } set { if (_ShiftID != value) { _ShiftID = value; } } }

    string _ExprDesc;
    public string ExprDesc { get { return _ExprDesc; } set { if (_ExprDesc != value) { _ExprDesc = value; } } }

    bool _ExprVacIfNoPresent;
    public bool ExprVacIfNoPresent { get { return _ExprVacIfNoPresent; } set { if (_ExprVacIfNoPresent != value) { _ExprVacIfNoPresent = value; } } }

    string _ExprStartDate;
    public string ExprStartDate { get { return _ExprStartDate; } set { if (_ExprStartDate != value) { _ExprStartDate = value; } } }

    string _ExprEndDate;
    public string ExprEndDate { get { return _ExprEndDate; } set { if (_ExprEndDate != value) { _ExprEndDate = value; } } }

    string _ExcType;
    public string ExcType { get { return _ExcType; } set { if (_ExcType != value) { _ExcType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}