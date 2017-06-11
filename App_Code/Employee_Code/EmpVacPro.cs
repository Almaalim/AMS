using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpVacPro
{
    //EvrID	int	Unchecked
    //VtpID	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //EvrStartDate	datetime	Unchecked
    //EvrEndDate	datetime	Unchecked
    //EvrDesc	varchar(255)	Checked
    //EvrPhone	varchar(20)	Checked
    //EvrAvailability	varchar(255)	Checked
    //EvrHospitalType	varchar(50)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EvrID;
	public string EvrID { get { return _EvrID; } set { if (_EvrID != value) { _EvrID = value; } } }

    string _VtpID;
    public string VtpID { get { return _VtpID; } set { if (_VtpID != value) { _VtpID = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _EvrStartDate;
	public string EvrStartDate { get { return _EvrStartDate; } set { if (_EvrStartDate != value) { _EvrStartDate = value; } } }

    string _EvrEndDate;
    public string EvrEndDate { get { return _EvrEndDate; } set { if (_EvrEndDate != value) { _EvrEndDate = value; } } }
    
    string _EvrDesc;
	public string EvrDesc { get { return _EvrDesc; } set { if (_EvrDesc != value) { _EvrDesc = value; } } }

    string _EvrPhone;
    public string EvrPhone { get { return _EvrPhone; } set { if (_EvrPhone != value) { _EvrPhone = value; } } }

    string _EvrAvailability;
    public string EvrAvailability { get { return _EvrAvailability; } set { if (_EvrAvailability != value) { _EvrAvailability = value; } } }

    string _EvrHospitalType;
    public string EvrHospitalType { get { return _EvrHospitalType; } set { if (_EvrHospitalType != value) { _EvrHospitalType = value; } } }

    string _EvrAddBy;
    public string EvrAddBy { get { return _EvrAddBy; } set { if (_EvrAddBy != value) { _EvrAddBy = value; } } }
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