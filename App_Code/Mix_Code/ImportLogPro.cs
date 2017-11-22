using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ImportLogPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _IplID;
	public string IplID { get { return _IplID; } set { if (_IplID != value) { _IplID = value; } } }

    string _IplDate;
    public string IplDate { get { return _IplDate; } set { if (_IplDate != value) { _IplDate = value; } } }

    string _IplStartTime;
	public string IplStartTime { get { return _IplStartTime; } set { if (_IplStartTime != value) { _IplStartTime = value; } } }

    string _IplEndTime;
	public string IplEndTime { get { return _IplEndTime; } set { if (_IplEndTime != value) { _IplEndTime = value; } } }

    string _IplType;
    public string IplType { get { return _IplType; } set { if (_IplType != value) { _IplType = value; } } }
    
    string _IplRunTodayProcess;
	public string IplRunTodayProcess { get { return _IplRunTodayProcess; } set { if (_IplRunTodayProcess != value) { _IplRunTodayProcess = value; } } }

    string _IplRunProcess;
	public string IplRunProcess { get { return _IplRunProcess; } set { if (_IplRunProcess != value) { _IplRunProcess = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ImlID;
    public string ImlID { get { return _ImlID; } set { if (_ImlID != value) { _ImlID = value; } } }

    string _MacID;
    public string MacID { get { return _MacID; } set { if (_MacID != value) { _MacID = value; } } }

    string _MacIP;
    public string MacIP { get { return _MacIP; } set { if (_MacIP != value) { _MacIP = value; } } }

    string _MtpName;
    public string MtpName { get { return _MtpName; } set { if (_MtpName != value) { _MtpName = value; } } }

    string _ImlStartDT;
    public string ImlStartDT { get { return _ImlStartDT; } set { if (_ImlStartDT != value) { _ImlStartDT = value; } } }

    string _ImlEndDT;
    public string ImlEndDT { get { return _ImlEndDT; } set { if (_ImlEndDT != value) { _ImlEndDT = value; } } }

    string _ImlIsImport;
    public string ImlIsImport { get { return _ImlIsImport; } set { if (_ImlIsImport != value) { _ImlIsImport = value; } } }

    string _ImlTransCount;
    public string ImlTransCount { get { return _ImlTransCount; } set { if (_ImlTransCount != value) { _ImlTransCount = value; } } }

    string _ImlErrMsg;
    public string ImlErrMsg { get { return _ImlErrMsg; } set { if (_ImlErrMsg != value) { _ImlErrMsg = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
}