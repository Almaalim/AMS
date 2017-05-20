using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ReqApprovalPro
{
    //EraID	int	Unchecked
    //ReqID	int	Checked
    //EmpID	varchar(15)	Checked
    //EraLevelSequenceNo	int	Checked
    //EraLevelCount	int	Checked
    //MgrID	varchar(500)	Checked
    //EraStatus	int	Checked
    //EraApprovalDate	datetime	Checked
    //EraApprovalMgr	varchar(15)	Checked
    //EraComment	varchar(255)	Checked
    //EraManagerAlertMessageCount	int	Checked
    //RetID	char(3)	Unchecked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EraID;
	public string EraID { get { return _EraID; } set { if (_EraID != value) { _EraID = value; } } }

    string _ReqID;
    public string ReqID { get { return _ReqID; } set { if (_ReqID != value) { _ReqID = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _EraLevelSequenceNo;
	public string EraLevelSequenceNo { get { return _EraLevelSequenceNo; } set { if (_EraLevelSequenceNo != value) { _EraLevelSequenceNo = value; } } }

    string _EraLevelCount;
    public string EraLevelCount { get { return _EraLevelCount; } set { if (_EraLevelCount != value) { _EraLevelCount = value; } } }
    
    string _MgrID;
	public string MgrID { get { return _MgrID; } set { if (_MgrID != value) { _MgrID = value; } } }

    string _EraStatus;
    public string EraStatus { get { return _EraStatus; } set { if (_EraStatus != value) { _EraStatus = value; } } }

    //string _EraApprovalDate;
    //public string EraApprovalDate { get { return _EraApprovalDate; } set { if (_EraApprovalDate != value) { _EraApprovalDate = value; } } }

    string _EraApprovalMgr;
    public string EraApprovalMgr { get { return _EraApprovalMgr; } set { if (_EraApprovalMgr != value) { _EraApprovalMgr = value; } } }

    string _EraComment;
    public string EraComment { get { return _EraComment; } set { if (_EraComment != value) { _EraComment = value; } } }

    string _EraManagerAlertMessageCount;
    public string EraManagerAlertMessageCount { get { return _EraManagerAlertMessageCount; } set { if (_EraManagerAlertMessageCount != value) { _EraManagerAlertMessageCount = value; } } }

    string _RetID;
    public string RetID { get { return _RetID; } set { if (_RetID != value) { _RetID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _GapOvtID;
    public string GapOvtID { get { return _GapOvtID; } set { if (_GapOvtID != value) { _GapOvtID = value; } } }

    string _ErqTypeID;
    public string ErqTypeID { get { return _ErqTypeID; } set { if (_ErqTypeID != value) { _ErqTypeID = value; } } }

    string _ErqStartDate;
    public string ErqStartDate { get { return _ErqStartDate; } set { if (_ErqStartDate != value) { _ErqStartDate = value; } } }

    string _ShiftID;
    public string ShiftID { get { return _ShiftID; } set { if (_ShiftID != value) { _ShiftID = value; } } }

    string _ErqStatusTime;
    public string ErqStatusTime { get { return _ErqStatusTime; } set { if (_ErqStatusTime != value) { _ErqStatusTime = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ErqIDs;
    public string ErqIDs { get { return _ErqIDs; } set { if (_ErqIDs != value) { _ErqIDs = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}