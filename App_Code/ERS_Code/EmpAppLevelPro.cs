using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmpAppLevelPro
{
    //EalID	int	Unchecked
    //EalLevelSequenceNo	int	Unchecked
    //EalMgrID	varchar(500)	Unchecked
    //EalLevelCount	int	Unchecked
    //EmpID	varchar(15)	Unchecked
    //RetID	char(3)	Unchecked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EalID;
	public string EalID { get { return _EalID; } set { if (_EalID != value) { _EalID = value; } } }

    string _EalLevelSequenceNo;
    public string EalLevelSequenceNo { get { return _EalLevelSequenceNo; } set { if (_EalLevelSequenceNo != value) { _EalLevelSequenceNo = value; } } }

    string _EalMgrID;
	public string EalMgrID { get { return _EalMgrID; } set { if (_EalMgrID != value) { _EalMgrID = value; } } }

    string _EalLevelCount;
	public string EalLevelCount { get { return _EalLevelCount; } set { if (_EalLevelCount != value) { _EalLevelCount = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }
    
    string _RetID;
	public string RetID { get { return _RetID; } set { if (_RetID != value) { _RetID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpIDs;
    public string EmpIDs { get { return _EmpIDs; } set { if (_EmpIDs != value) { _EmpIDs = value; } } }

    string _EalMgrIDs;
	public string EalMgrIDs { get { return _EalMgrIDs; } set { if (_EalMgrIDs != value) { _EalMgrIDs = value; } } }
    
    string _EalLevelIDs;
    public string EalLevelIDs { get { return _EalLevelIDs; } set { if (_EalLevelIDs != value) { _EalLevelIDs = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}