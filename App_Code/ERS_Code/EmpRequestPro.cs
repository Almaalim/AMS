using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpRequestPro
{
    //ErqID	int	Unchecked
    //RetID	char(3)	Checked
    //ErqTypeID	int	Checked
    //EmpID	varchar(15)	Checked
    //GapOvtID	int	Checked
    //ErqStartDate	datetime	Checked
    //ErqEndDate	datetime	Checked
    //ErqStartTime	datetime	Checked
    //ErqEndTime	datetime	Checked
    //ErqReason	varchar(255)	Checked
    //ErqAvailability	varchar(255)	Checked
    //ErqPhone	varchar(20)	Checked
    //ErqReqStatus	int	Checked
    //ErqReqDate	datetime	Checked
    //ErqReqFilePath	varchar(255)	Checked
    //EmpID2	varchar(15)	Checked
    //ErqStartDate2	datetime	Checked
    //ErqEndDate2	datetime	Checked
    //ErqEmp2ReqStatus	int	Checked
    //WktID	int	Checked
    //ShiftID	int	Checked
    //ErqStatusTime	varchar(1)	Checked
    //VacHospitalType	varchar(50)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ErqID;
	public string ErqID { get { return _ErqID; } set { if (_ErqID != value) { _ErqID = value; } } }

    string _RetID;
    public string RetID { get { return _RetID; } set { if (_RetID != value) { _RetID = value; } } }

    string _ErqTypeID;
	public string ErqTypeID { get { return _ErqTypeID; } set { if (_ErqTypeID != value) { _ErqTypeID = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _GapOvtID;
    public string GapOvtID { get { return _GapOvtID; } set { if (_GapOvtID != value) { _GapOvtID = value; } } }
    
    string _ErqStartDate;
	public string ErqStartDate { get { return _ErqStartDate; } set { if (_ErqStartDate != value) { _ErqStartDate = value; } } }

    string _ErqEndDate;
    public string ErqEndDate { get { return _ErqEndDate; } set { if (_ErqEndDate != value) { _ErqEndDate = value; } } }

    string _ErqStartTime;
    public string ErqStartTime { get { return _ErqStartTime; } set { if (_ErqStartTime != value) { _ErqStartTime = value; } } }

    string _ErqEndTime;
    public string ErqEndTime { get { return _ErqEndTime; } set { if (_ErqEndTime != value) { _ErqEndTime = value; } } }

    string _ErqReason;
    public string ErqReason { get { return _ErqReason; } set { if (_ErqReason != value) { _ErqReason = value; } } }

    string _ErqAvailability;
    public string ErqAvailability { get { return _ErqAvailability; } set { if (_ErqAvailability != value) { _ErqAvailability = value; } } }

    string _ErqPhone;
    public string ErqPhone { get { return _ErqPhone; } set { if (_ErqPhone != value) { _ErqPhone = value; } } }

    string _ErqReqStatus;
    public string ErqReqStatus { get { return _ErqReqStatus; } set { if (_ErqReqStatus != value) { _ErqReqStatus = value; } } }

    //string _ErqReqDate;
    //public string ErqReqDate { get { return _ErqReqDate; } set { if (_ErqReqDate != value) { _ErqReqDate = value; } } }

    string _ErqReqFilePath;
    public string ErqReqFilePath { get { return _ErqReqFilePath; } set { if (_ErqReqFilePath != value) { _ErqReqFilePath = value; } } }

    string _ErqStatusTime;
    public string ErqStatusTime { get { return _ErqStatusTime; } set { if (_ErqStatusTime != value) { _ErqStatusTime = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpID2;
    public string EmpID2 { get { return _EmpID2; } set { if (_EmpID2 != value) { _EmpID2 = value; } } }

    string _ErqStartDate2;
    public string ErqStartDate2 { get { return _ErqStartDate2; } set { if (_ErqStartDate2 != value) { _ErqStartDate2 = value; } } }

    string _ErqEndDate2;
    public string ErqEndDate2 { get { return _ErqEndDate2; } set { if (_ErqEndDate2 != value) { _ErqEndDate2 = value; } } }

    string _ErqEmp2ReqStatus;
    public string ErqEmp2ReqStatus { get { return _ErqEmp2ReqStatus; } set { if (_ErqEmp2ReqStatus != value) { _ErqEmp2ReqStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _WktID;
    public string WktID { get { return _WktID; } set { if (_WktID != value) { _WktID = value; } } }

    string _ShiftID;
    public string ShiftID { get { return _ShiftID; } set { if (_ShiftID != value) { _ShiftID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _VacHospitalType;
    public string VacHospitalType { get { return _VacHospitalType; } set { if (_VacHospitalType != value) { _VacHospitalType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //string _MailCode;
    //public string MailCode { get { return _MailCode; } set { if (_MailCode != value) { _MailCode = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}