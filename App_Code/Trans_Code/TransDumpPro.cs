using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TransDumpPro
{
    //TrnDate	datetime	Unchecked
    //TrnTime	datetime	Unchecked
    //EmpID	varchar(15)	Unchecked
    //MacID	int	Unchecked
    //EwrID	int	Checked
    //TrnType	char(1)	Checked
    //TrnShift	tinyint	Checked
    //TrnFlagOT	char(2)	Checked
    //UsrName	char(10)	Checked
    //TrnDesc	nvarchar(255)	Checked
    //TrnIgnore	bit	Checked
    //TrnAddBy	varchar(3)	Checked
    //TrnIgnoredBy	char(10)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _TrnDate;
	public string TrnDate { get { return _TrnDate; } set { if (_TrnDate != value) { _TrnDate = value; } } }

    string _TrnTime;
    public string TrnTime { get { return _TrnTime; } set { if (_TrnTime != value) { _TrnTime = value; } } }

    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _MacID;
	public string MacID { get { return _MacID; } set { if (_MacID != value) { _MacID = value; } } }

    string _EwrID;
    public string EwrID { get { return _EwrID; } set { if (_EwrID != value) { _EwrID = value; } } }

    string _TrnType;
	public string TrnType { get { return _TrnType; } set { if (_TrnType != value) { _TrnType = value; } } }

    string _TrnShift;
    public string TrnShift { get { return _TrnShift; } set { if (_TrnShift != value) { _TrnShift = value; } } }

    string _TrnFlagOT;
    public string TrnFlagOT { get { return _TrnFlagOT; } set { if (_TrnFlagOT != value) { _TrnFlagOT = value; } } }

    string _UsrName;
    public string UsrName { get { return _UsrName; } set { if (_UsrName != value) { _UsrName = value; } } }

    string _TrnDesc;
    public string TrnDesc { get { return _TrnDesc; } set { if (_TrnDesc != value) { _TrnDesc = value; } } }

    bool _TrnIgnore;
    public bool TrnIgnore { get { return _TrnIgnore; } set { if (_TrnIgnore != value) { _TrnIgnore = value; } } }

    string _TrnAddBy;
    public string TrnAddBy { get { return _TrnAddBy; } set { if (_TrnAddBy != value) { _TrnAddBy = value; } } }

    string _TrnIgnoredBy;
    public string TrnIgnoredBy { get { return _TrnIgnoredBy; } set { if (_TrnIgnoredBy != value) { _TrnIgnoredBy = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TrnReason;
    public string TrnReason { get { return _TrnReason; } set { _TrnReason = value; } }

    private string _TrnFilePath;
    public string TrnFilePath { get { return _TrnFilePath; } set { _TrnFilePath = value; } }

    private HttpPostedFile _PostedFile;
    public HttpPostedFile PostedFile { get { return _PostedFile; } set { _PostedFile = value; } }
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