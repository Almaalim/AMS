using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BranchPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _BrcID;
	public string BrcID { get { return _BrcID; } set { if (_BrcID != value) { _BrcID = value; } } }

    string _BrcNameAr;
    public string BrcNameAr { get { return _BrcNameAr; } set { if (_BrcNameAr != value) { _BrcNameAr = value; } } }

    string _BrcNameEn;
	public string BrcNameEn { get { return _BrcNameEn; } set { if (_BrcNameEn != value) { _BrcNameEn = value; } } }

    string _UsrName;
	public string UsrName { get { return _UsrName; } set { if (_UsrName != value) { _UsrName = value; } } }

    string _BrcAddress;
    public string BrcAddress { get { return _BrcAddress; } set { if (_BrcAddress != value) { _BrcAddress = value; } } }
    
    string _BrcCity;
	public string BrcCity { get { return _BrcCity; } set { if (_BrcCity != value) { _BrcCity = value; } } }

    string _BrcCountry;
    public string BrcCountry { get { return _BrcCountry; } set { if (_BrcCountry != value) { _BrcCountry = value; } } }

    string _BrcPOBox;
    public string BrcPOBox { get { return _BrcPOBox; } set { if (_BrcPOBox != value) { _BrcPOBox = value; } } }

    string _BrcTelNo;
    public string BrcTelNo { get { return _BrcTelNo; } set { if (_BrcTelNo != value) { _BrcTelNo = value; } } }

    string _BrcEmail;
    public string BrcEmail { get { return _BrcEmail; } set { if (_BrcEmail != value) { _BrcEmail = value; } } }

    bool _BrcStatus;
    public bool BrcStatus { get { return _BrcStatus; } set { if (_BrcStatus != value) { _BrcStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}