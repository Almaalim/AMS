using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DepartmentPro
{
    //DepID	int	Unchecked
    //DepParentID	int	Checked
    //BrcID	int	Checked
    //DepNameAr	varchar(100)	Checked
    //DepNameEn	varchar(100)	Checked
    //DepDesc	varchar(255)	Checked
    //UsrName	char(10)	Checked
    //DepStatus	bit	Checked
    //DepLevel	int	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _DepID;
	public string  DepID { get { return _DepID; } set { if (_DepID != value) { _DepID = value; } } }

    string _DepParentID;
	public string  DepParentID { get { return _DepParentID; } set { if (_DepParentID != value) { _DepParentID = value; } } }
    
    string _BrcID;
	public string  BrcID { get { return _BrcID; } set { if (_BrcID != value) { _BrcID = value; } } }

    string _DepNameAr;
    public string DepNameAr { get { return _DepNameAr; } set { if (_DepNameAr != value) { _DepNameAr = value; } } }

    string _DepNameEn;
	public string  DepNameEn { get { return _DepNameEn; } set { if (_DepNameEn != value) { _DepNameEn = value; } } }

    string _UsrName;
	public string  UsrName { get { return _UsrName; } set { if (_UsrName != value) { _UsrName = value; } } }

    string _DepDesc;
    public string DepDesc { get { return _DepDesc; } set { if (_DepDesc != value) { _DepDesc = value; } } }
    
    string _DepLevel;
	public string  DepLevel { get { return _DepLevel; } set { if (_DepLevel != value) { _DepLevel = value; } } }

    bool _DepStatus;
    public bool DepStatus { get { return _DepStatus; } set { if (_DepStatus != value) { _DepStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}