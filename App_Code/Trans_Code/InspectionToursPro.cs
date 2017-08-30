using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class InspectionToursPro
{
    //ItmID int Unchecked
    //ItmNameEn varchar(1000)   Checked
    //ItmNameAr   varchar(1000)   Checked
    //ItmDate datetime Unchecked
    //ItmTimeFrom datetime    Unchecked
    //ItmTimeTo   datetime Unchecked
    //ItmMacType int Checked
    //ItmMacIDs varchar(1000)   Checked
    //ItmIsSend   bit Checked
    //ItmEmpIDs varchar(8000)   Unchecked
    //ItmDescription  varchar(8000)   Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ItmID;
	public string ItmID { get { return _ItmID; } set { if (_ItmID != value) { _ItmID = value; } } }

    string _ItmNameEn;
    public string ItmNameEn { get { return _ItmNameEn; } set { if (_ItmNameEn != value) { _ItmNameEn = value; } } }

    string _ItmNameAr;
	public string ItmNameAr { get { return _ItmNameAr; } set { if (_ItmNameAr != value) { _ItmNameAr = value; } } }

    string _ItmDate;
	public string ItmDate { get { return _ItmDate; } set { if (_ItmDate != value) { _ItmDate = value; } } }

    string _ItmTimeFrom;
    public string ItmTimeFrom { get { return _ItmTimeFrom; } set { if (_ItmTimeFrom != value) { _ItmTimeFrom = value; } } }

    string _ItmTimeTo;
	public string ItmTimeTo { get { return _ItmTimeTo; } set { if (_ItmTimeTo != value) { _ItmTimeTo = value; } } }

    string _ItmMacType;
    public string ItmMacType { get { return _ItmMacType; } set { if (_ItmMacType != value) { _ItmMacType = value; } } }

    string _ItmMacIDs;
    public string ItmMacIDs { get { return _ItmMacIDs; } set { if (_ItmMacIDs != value) { _ItmMacIDs = value; } } }

    bool _ItmIsSend;
    public bool ItmIsSend { get { return _ItmIsSend; } set { if (_ItmIsSend != value) { _ItmIsSend = value; } } }

    string _ItmEmpIDs;
    public string ItmEmpIDs { get { return _ItmEmpIDs; } set { if (_ItmEmpIDs != value) { _ItmEmpIDs = value; } } }

    string _ItmDescription;
    public string ItmDescription { get { return _ItmDescription; } set { if (_ItmDescription != value) { _ItmDescription = value; } } }

    string _ItmIsProcess;
    public string ItmIsProcess { get { return _ItmIsProcess; } set { if (_ItmIsProcess != value) { _ItmIsProcess = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}