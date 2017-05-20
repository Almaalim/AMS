using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EmployeePro
{
    //EmpID	varchar(15)	Unchecked
    //EmpCardID	varchar(15)	Unchecked
    //EmpNationalID	varchar(15)	Checked
    //DepID	int	Unchecked
    //CatID	int	Checked
    //EmpNameAr	varchar(100)	Checked
    //EmpNameEn	varchar(100)	Checked
    //EmpJoinDate	datetime	Unchecked
    //EmpLeaveDate	datetime	Checked
    //EmpStatus	bit	Unchecked
    //EmpPWD	varchar(15)	Checked
    //EmpAutoOut	bit	Checked
    //EmpAutoIn	bit	Checked
    //EmpEmailID	varchar(100)	Checked
    //EmpMobileNo	varchar(100)	Checked
    //NatID	int	Unchecked
    //EtpID	int	Checked
    //EmpJobTitleAr	varchar(100)	Checked
    //EmpJobTitleEn	varchar(100)	Checked
    //EmpBlood	varchar(3)	Checked
    //EmpGender	char(1)	Checked
    //EmpSendEmailAlert	bit	Checked
    //EmpSendSMSAlert	bit	Checked
    //EmpDescription	varchar(255)	Checked
    //EmpDomainUserName	varchar(50)	Checked
    //EmpMaxOvertimePercent	bigint	Checked
    //RlsID	int	Checked
    //EmpLanguage	char(2)	Checked
    //EmpIntegrationID	varchar(100)	Checked
    //EmpRankTitel	varchar(500)	Checked
    //EmpRankDesc	varchar(500)	Checked
    //EmpADUser	varchar(100)	Checked
    //EmpRankCode	varchar(100)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpID;
	public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    string _EmpCardID;
    public string EmpCardID { get { return _EmpCardID; } set { if (_EmpCardID != value) { _EmpCardID = value; } } }

    string _EmpNationalID;
	public string EmpNationalID { get { return _EmpNationalID; } set { if (_EmpNationalID != value) { _EmpNationalID = value; } } }

    string _DepID;
	public string DepID { get { return _DepID; } set { if (_DepID != value) { _DepID = value; } } }

    string _CatID;
    public string CatID { get { return _CatID; } set { if (_CatID != value) { _CatID = value; } } }
    
    string _EmpNameAr;
	public string EmpNameAr { get { return _EmpNameAr; } set { if (_EmpNameAr != value) { _EmpNameAr = value; } } }

    string _EmpNameEn;
    public string EmpNameEn { get { return _EmpNameEn; } set { if (_EmpNameEn != value) { _EmpNameEn = value; } } }

    string _EmpJoinDate;
    public string EmpJoinDate { get { return _EmpJoinDate; } set { if (_EmpJoinDate != value) { _EmpJoinDate = value; } } }

    string _EmpLeaveDate;
    public string EmpLeaveDate { get { return _EmpLeaveDate; } set { if (_EmpLeaveDate != value) { _EmpLeaveDate = value; } } }

    bool _EmpStatus;
    public bool EmpStatus { get { return _EmpStatus; } set { if (_EmpStatus != value) { _EmpStatus = value; } } }

    string _EmpPWD;
	public string EmpPWD { get { return _EmpPWD; } set { if (_EmpPWD != value) { _EmpPWD = value; } } }

    bool _EmpAutoOut;
	public bool EmpAutoOut { get { return _EmpAutoOut; } set { if (_EmpAutoOut != value) { _EmpAutoOut = value; } } }

    bool _EmpAutoIn;
    public bool EmpAutoIn { get { return _EmpAutoIn; } set { if (_EmpAutoIn != value) { _EmpAutoIn = value; } } }
    
    string _EmpEmailID;
	public string EmpEmailID { get { return _EmpEmailID; } set { if (_EmpEmailID != value) { _EmpEmailID = value; } } }

    string _EmpMobileNo;
    public string EmpMobileNo { get { return _EmpMobileNo; } set { if (_EmpMobileNo != value) { _EmpMobileNo = value; } } }

    string _NatID;
    public string NatID { get { return _NatID; } set { if (_NatID != value) { _NatID = value; } } }

    string _EtpID;
    public string EtpID { get { return _EtpID; } set { if (_EtpID != value) { _EtpID = value; } } }

    string _EmpJobTitleAr;
    public string EmpJobTitleAr { get { return _EmpJobTitleAr; } set { if (_EmpJobTitleAr != value) { _EmpJobTitleAr = value; } } }

    string _EmpJobTitleEn;
    public string EmpJobTitleEn { get { return _EmpJobTitleEn; } set { if (_EmpJobTitleEn != value) { _EmpJobTitleEn = value; } } }

    string _EmpBlood;
    public string EmpBlood { get { return _EmpBlood; } set { if (_EmpBlood != value) { _EmpBlood = value; } } }

    string _EmpGender;
    public string EmpGender { get { return _EmpGender; } set { if (_EmpGender != value) { _EmpGender = value; } } }

    bool _EmpSendEmailAlert;
    public bool EmpSendEmailAlert { get { return _EmpSendEmailAlert; } set { if (_EmpSendEmailAlert != value) { _EmpSendEmailAlert = value; } } }

    bool _EmpSendSMSAlert;
    public bool EmpSendSMSAlert { get { return _EmpSendSMSAlert; } set { if (_EmpSendSMSAlert != value) { _EmpSendSMSAlert = value; } } }

    string _EmpDescription;
    public string EmpDescription { get { return _EmpDescription; } set { if (_EmpDescription != value) { _EmpDescription = value; } } }

    string _EmpDomainUserName;
    public string EmpDomainUserName { get { return _EmpDomainUserName; } set { if (_EmpDomainUserName != value) { _EmpDomainUserName = value; } } }

    string _EmpMaxOvertimePercent;
    public string EmpMaxOvertimePercent { get { return _EmpMaxOvertimePercent; } set { if (_EmpMaxOvertimePercent != value) { _EmpMaxOvertimePercent = value; } } }

    string _RlsID;
    public string RlsID { get { return _RlsID; } set { if (_RlsID != value) { _RlsID = value; } } }

    string _EmpLanguage;
    public string EmpLanguage { get { return _EmpLanguage; } set { if (_EmpLanguage != value) { _EmpLanguage = value; } } }

    string _EmpADUser;
    public string EmpADUser { get { return _EmpADUser; } set { if (_EmpADUser != value) { _EmpADUser = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpRankTitel;
    public string EmpRankTitel { get { return _EmpRankTitel; } set { if (_EmpRankTitel != value) { _EmpRankTitel = value; } } }

    string _EmpRankDesc;
    public string EmpRankDesc { get { return _EmpRankDesc; } set { if (_EmpRankDesc != value) { _EmpRankDesc = value; } } }

    string _EmpRankCode;
    public string EmpRankCode { get { return _EmpRankCode; } set { if (_EmpRankCode != value) { _EmpRankCode = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpIDs;
	public string EmpIDs { get { return _EmpIDs; } set { if (_EmpIDs != value) { _EmpIDs = value; } } }

    string _EmpAutoOutWizard;
	public string EmpAutoOutWizard { get { return _EmpAutoOutWizard; } set { if (_EmpAutoOutWizard != value) { _EmpAutoOutWizard = value; } } }

    string _EmpAutoInWizard;
    public string EmpAutoInWizard { get { return _EmpAutoInWizard; } set { if (_EmpAutoInWizard != value) { _EmpAutoInWizard = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}