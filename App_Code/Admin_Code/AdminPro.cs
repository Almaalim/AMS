using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AdminPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* ADConfig */
    string _ADType;
	public string ADType { get { return _ADType; } set { if (_ADType != value) { _ADType = value; } } }

    string _ADEnabled;
    public string ADEnabled { get { return _ADEnabled; } set { if (_ADEnabled != value) { _ADEnabled = value; } } }

    string _ADJoinMethod;
	public string ADJoinMethod { get { return _ADJoinMethod; } set { if (_ADJoinMethod != value) { _ADJoinMethod = value; } } }

    string _ADColName;
	public string ADColName { get { return _ADColName; } set { if (_ADColName != value) { _ADColName = value; } } }

    string _AMSUsrColName;
    public string AMSUsrColName { get { return _AMSUsrColName; } set { if (_AMSUsrColName != value) { _AMSUsrColName = value; } } }
    
    string _AMSEmpColName;
    public string AMSEmpColName { get { return _AMSEmpColName; } set { if (_AMSEmpColName != value) { _AMSEmpColName = value; } } }
    
    string _ADDomainName;
	public string ADDomainName { get { return _ADDomainName; } set { if (_ADDomainName != value) { _ADDomainName = value; } } }

    string _ADUsrColName;
    public string ADUsrColName { get { return _ADUsrColName; } set { if (_ADUsrColName != value) { _ADUsrColName = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* EmailConfig */
    string _EmlServerID;
    public string EmlServerID { get { return _EmlServerID; } set { if (_EmlServerID != value) { _EmlServerID = value; } } }

    string _EmlPortNo;
    public string EmlPortNo { get { return _EmlPortNo; } set { if (_EmlPortNo != value) { _EmlPortNo = value; } } }

    string _EmlSenderEmail;
    public string EmlSenderEmail { get { return _EmlSenderEmail; } set { if (_EmlSenderEmail != value) { _EmlSenderEmail = value; } } }

    string _EmlSenderName;
    public string EmlSenderName { get { return _EmlSenderName; } set { if (_EmlSenderName != value) { _EmlSenderName = value; } } }
    
    string _EmlSenderPassword;
    public string EmlSenderPassword { get { return _EmlSenderPassword; } set { if (_EmlSenderPassword != value) { _EmlSenderPassword = value; } } }

    bool _EmlSsl;
    public bool EmlSsl { get { return _EmlSsl; } set { if (_EmlSsl != value) { _EmlSsl = value; } } }

    bool _EmlCredential;
    public bool EmlCredential { get { return _EmlCredential; } set { if (_EmlCredential != value) { _EmlCredential = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* RequestType */
    string _RetID;
    public string RetID { get { return _RetID; } set { if (_RetID != value) { _RetID = value; } } }

    string _RetNameEn;
    public string RetNameEn { get { return _RetNameEn; } set { if (_RetNameEn != value) { _RetNameEn = value; } } }

    string _RetNameAr;
    public string RetNameAr { get { return _RetNameAr; } set { if (_RetNameAr != value) { _RetNameAr = value; } } }

    string _RetOrder;
    public string RetOrder { get { return _RetOrder; } set { if (_RetOrder != value) { _RetOrder = value; } } }

    string _EmpReqStatus;
    public string EmpReqStatus { get { return _EmpReqStatus; } set { if (_EmpReqStatus != value) { _EmpReqStatus = value; } } }

    string _RetIDs;
    public string RetIDs { get { return _RetIDs; } set { if (_RetIDs != value) { _RetIDs = value; } } }

    string _StatusIDs;
    public string StatusIDs { get { return _StatusIDs; } set { if (_StatusIDs != value) { _StatusIDs = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* SMSConfig */
    string _SmsGateway;
    public string SmsGateway { get { return _SmsGateway; } set { if (_SmsGateway != value) { _SmsGateway = value; } } }

    string _SmsSenderID;
    public string SmsSenderID { get { return _SmsSenderID; } set { if (_SmsSenderID != value) { _SmsSenderID = value; } } }

    string _SmsSenderNo;
    public string SmsSenderNo { get { return _SmsSenderNo; } set { if (_SmsSenderNo != value) { _SmsSenderNo = value; } } }

    string _SmsUser;
    public string SmsUser { get { return _SmsUser; } set { if (_SmsUser != value) { _SmsUser = value; } } }

    string _SmsPass;
    public string SmsPass { get { return _SmsPass; } set { if (_SmsPass != value) { _SmsPass = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* DepLevel */
    string _DplIDs;
    public string DplIDs { get { return _DplIDs; } set { if (_DplIDs != value) { _DplIDs = value; } } }

    string _DplNameArs;
    public string DplNameArs { get { return _DplNameArs; } set { if (_DplNameArs != value) { _DplNameArs = value; } } }

    string _DplNameEns;
    public string DplNameEns { get { return _DplNameEns; } set { if (_DplNameEns != value) { _DplNameEns = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     /* ImportSetting */
    bool _IpsRunProcess;
    public bool IpsRunProcess { get { return _IpsRunProcess; } set { if (_IpsRunProcess != value) { _IpsRunProcess = value; } } }

    bool _IpsRunTodayProcess;
    public bool IpsRunTodayProcess { get { return _IpsRunTodayProcess; } set { if (_IpsRunTodayProcess != value) { _IpsRunTodayProcess = value; } } }

    bool _IpsSaveTransInFile;
    public bool IpsSaveTransInFile { get { return _IpsSaveTransInFile; } set { if (_IpsSaveTransInFile != value) { _IpsSaveTransInFile = value; } } }

    bool _IpsEncryptTransInFile;
    public bool IpsEncryptTransInFile { get { return _IpsEncryptTransInFile; } set { if (_IpsEncryptTransInFile != value) { _IpsEncryptTransInFile = value; } } }

    string _IpsImportScheduleTimes;
    public string IpsImportScheduleTimes { get { return _IpsImportScheduleTimes; } set { if (_IpsImportScheduleTimes != value) { _IpsImportScheduleTimes = value; } } }

    string _IpsProcessScheduleTimes;
    public string IpsProcessScheduleTimes { get { return _IpsProcessScheduleTimes; } set { if (_IpsProcessScheduleTimes != value) { _IpsProcessScheduleTimes = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}