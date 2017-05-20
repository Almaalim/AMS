using System;
using System.Collections.Generic;
using System.Text;

public class UsersPro
{

    //UsrName	char(10)	Unchecked
    //UsrPassword	varchar(15)	Checked
    //EmpID	varchar(15)	Checked
    //UsrFullName	varchar(100)	Unchecked
    //UsrDesc	varchar(255)	Checked
    //UsrStartDate	datetime	Checked
    //UsrExpireDate	datetime	Checked
    //UsrStatus	bit	Unchecked
    //UsrLanguage	char(2)	Unchecked
    //GrpID	int	Checked
    //ReportGrpID	int	Checked
    //UsrDepartments	varchar(8000)	Checked
    //UsrDeleted	bit	Checked
    //UsrDomainName	varchar(50)	Checked
    //UsrEMailID	varchar(100)	Checked
    //UsrMobileNo	varchar(100)	Checked
    //UsrSendEmailAlert	bit	Checked
    //UsrSendSMSAlert	bit	Checked
    //UsrActingUser	bit	Checked
    //UsrActUserName	char(10)	Checked
    //UsrActPwd	char(10)	Checked
    //UsrActEmpID	varchar(15)	Checked
    //UsrActDomainName	varchar(100)	Checked
    //UsrSalarySystem	bit	Checked
    //UsrAdminMiniLogger	bit	Checked
    //UsrADUser	varchar(100)	Checked
    //UsrADActUser	varchar(100)	Checked
    //UsrActEMailID	varchar(100)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region AppUser

    string _UsrName;
	public string  UsrName { get { return _UsrName; } set { if (_UsrName != value) { _UsrName = value; } } }

    string _UsrPassword;
	public string  UsrPassword { get { return _UsrPassword; } set { if (_UsrPassword != value) { _UsrPassword = value; } } }

    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _UsrFullName;
	public string  UsrFullName { get { return _UsrFullName; } set { if (_UsrFullName != value) { _UsrFullName = value; } } }

    string _UsrDesc;
	public string  UsrDesc { get { return _UsrDesc; } set { if (_UsrDesc != value) { _UsrDesc = value; } } }

    string _UsrStartDate;
    public string UsrStartDate { get { return _UsrStartDate; } set { if (_UsrStartDate != value) { _UsrStartDate = value; } } }

    string _UsrExpireDate;
    public string UsrExpireDate { get { return _UsrExpireDate; } set { if (_UsrExpireDate != value) { _UsrExpireDate = value; } } }

    bool _UsrStatus;
	public bool  UsrStatus { get { return _UsrStatus; } set { if (_UsrStatus != value) { _UsrStatus = value; } } }

    string _UsrLanguage;
	public string  UsrLanguage { get { return _UsrLanguage; } set { if (_UsrLanguage != value) { _UsrLanguage = value; } } }

    string _UsrGrpID;
    public string UsrGrpID { get { return _UsrGrpID; } set { if (_UsrGrpID != value) { _UsrGrpID = value; } } }

    string _UsrReportGrpID;
    public string UsrReportGrpID { get { return _UsrReportGrpID; } set { if (_UsrReportGrpID != value) { _UsrReportGrpID = value; } } }

    string _UsrDepartments;
    public string UsrDepartments { get { return _UsrDepartments; } set { if (_UsrDepartments != value) { _UsrDepartments = value; } } }

    string _UsrDomainName;
    public string UsrDomainName { get { return _UsrDomainName; } set { if (_UsrDomainName != value) { _UsrDomainName = value; } } }
    
    string _UsrEMailID;
    public string UsrEMailID { get { return _UsrEMailID; } set { if (_UsrEMailID != value) { _UsrEMailID = value; } } }

    string _UsrMobileNo;
    public string UsrMobileNo { get { return _UsrMobileNo; } set { if (_UsrMobileNo != value) { _UsrMobileNo = value; } } }

    bool _UsrSendEmailAlert;
    public bool UsrSendEmailAlert { get { return _UsrSendEmailAlert; } set { if (_UsrSendEmailAlert != value) { _UsrSendEmailAlert = value; } } }

    bool _UsrSendSMSAlert;
    public bool UsrSendSMSAlert { get { return _UsrSendSMSAlert; } set { if (_UsrSendSMSAlert != value) { _UsrSendSMSAlert = value; } } }

    bool _UsrAdminMiniLogger;
    public bool UsrAdminMiniLogger { get { return _UsrAdminMiniLogger; } set { if (_UsrAdminMiniLogger != value) { _UsrAdminMiniLogger = value; } } }
    
    string _UsrADUser;
    public string UsrADUser { get { return _UsrADUser; } set { if (_UsrADUser != value) { _UsrADUser = value; } } }
    //////////////////////////////////////////
    bool _UsrActingUser;
    public bool UsrActingUser { get { return _UsrActingUser; } set { if (_UsrActingUser != value) { _UsrActingUser = value; } } }

    string _UsrActUserName;
    public string UsrActUserName { get { return _UsrActUserName; } set { if (_UsrActUserName != value) { _UsrActUserName = value; } } }

    string _UsrActPwd;
    public string UsrActPwd { get { return _UsrActPwd; } set { if (_UsrActPwd != value) { _UsrActPwd = value; } } }

    string _UsrActEmpID;
    public string UsrActEmpID { get { return _UsrActEmpID; } set { if (_UsrActEmpID != value) { _UsrActEmpID = value; } } }

    string _UsrADActUser;
    public string UsrADActUser { get { return _UsrADActUser; } set { if (_UsrADActUser != value) { _UsrADActUser = value; } } }

    string _UsrActEMailID;
    public string UsrActEMailID { get { return _UsrActEMailID; } set { if (_UsrActEMailID != value) { _UsrActEMailID = value; } } }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region PermissionGroup

    string _GrpID;
    public string GrpID { get { return _GrpID; } set { if (_GrpID != value) { _GrpID = value; } } }

    string _GrpNameEn;
	public string GrpNameEn { get { return _GrpNameEn; } set { if (_GrpNameEn != value) { _GrpNameEn = value; } } }

    string _GrpNameAr;
	public string GrpNameAr { get { return _GrpNameAr; } set { if (_GrpNameAr != value) { _GrpNameAr = value; } } }

    string _GrpDescription;
    public string GrpDescription { get { return _GrpDescription; } set { if (_GrpDescription != value) { _GrpDescription = value; } } }

    string _GrpPermissions;
	public string GrpPermissions { get { return _GrpPermissions; } set { if (_GrpPermissions != value) { _GrpPermissions = value; } } }

    string _GrpType;
    public string GrpType { get { return _GrpType; } set { if (_GrpType != value) { _GrpType = value; } } }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ActiveDirectory

    private string _ADError;
    public string ADError { get { return _ADError; } set { _ADError = value; } }

    private bool _ADValid;
    public bool ADValid { get { return _ADValid; } set { _ADValid = value; } }
    
    private ActiveDirectoryFun.ADTypeEnum _ADType;
    public ActiveDirectoryFun.ADTypeEnum ADType { get { return _ADType; } set { _ADType = value; } }

    protected bool _ADEnabled;
    public bool ADEnabled { get { return _ADEnabled; } set { _ADEnabled = value; } }

    private string _ADJoinMethod;
    public string ADJoinMethod { get { return _ADJoinMethod; } set { _ADJoinMethod = value; } }

    private string _ADColName;
    public string ADColName { get { return _ADColName; } set { _ADColName = value; } }

    private string _APPColName;
    public string APPColName { get { return _APPColName; } set { _APPColName = value; } }

    private string _ADDomainName;
    public string ADDomainName { get { return _ADDomainName; } set { _ADDomainName = value; } }

    private string _ADUsrColName;
    public string ADUsrColName { get { return _ADUsrColName; } set { _ADUsrColName = value; } }

    private bool _ADValidation;
    public bool ADValidation { get { return _ADValidation; } set { _ADValidation = value; } }

    private string _ADMsgValidation;
    public string ADMsgValidation { get { return _ADMsgValidation; } set { _ADMsgValidation = value; } }

    private string _ADMsgNotExists;
    public string ADMsgNotExists { get { return _ADMsgNotExists; } set { _ADMsgNotExists = value; } }

    private string _ADVal;
    public string ADVal { get { return _ADVal; } set { _ADVal = value; } }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}