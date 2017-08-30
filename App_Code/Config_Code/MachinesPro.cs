using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class MachinesPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _MacID;
	public string  MacID { get { return _MacID; } set { if (_MacID != value) { _MacID = value; } } }
    
    string _MtpID;
	public string  MtpID { get { return _MtpID; } set { if (_MtpID != value) { _MtpID = value; } } }

    string _MacLocationAr;
    public string MacLocationAr { get { return _MacLocationAr; } set { if (_MacLocationAr != value) { _MacLocationAr = value; } } }

    string _MacLocationEn;
	public string  MacLocationEn { get { return _MacLocationEn; } set { if (_MacLocationEn != value) { _MacLocationEn = value; } } }

    string _MacInstallDate;
	public string  MacInstallDate { get { return _MacInstallDate; } set { if (_MacInstallDate != value) { _MacInstallDate = value; } } }

    string _MacIP;
    public string MacIP { get { return _MacIP; } set { if (_MacIP != value) { _MacIP = value; } } }
    
    string _MacPort;
	public string  MacPort { get { return _MacPort; } set { if (_MacPort != value) { _MacPort = value; } } }

    string _MacNo;
    public string MacNo { get { return _MacNo; } set { if (_MacNo != value) { _MacNo = value; } } }

    string _MacLastConDate;
    public string MacLastConDate { get { return _MacLastConDate; } set { if (_MacLastConDate != value) { _MacLastConDate = value; } } }

    string _MacRecCount;
    public string MacRecCount { get { return _MacRecCount; } set { if (_MacRecCount != value) { _MacRecCount = value; } } }

    string _MacVirtualType;
    public string MacVirtualType { get { return _MacVirtualType; } set { if (_MacVirtualType != value) { _MacVirtualType = value; } } }

    string _MacTransactionType;
    public string MacTransactionType { get { return _MacTransactionType; } set { if (_MacTransactionType != value) { _MacTransactionType = value; } } }

    bool _MacInOutType;
    public bool MacInOutType { get { return _MacInOutType; } set { if (_MacInOutType != value) { _MacInOutType = value; } } }

    bool _MacStatus;
    public bool MacStatus { get { return _MacStatus; } set { if (_MacStatus != value) { _MacStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}