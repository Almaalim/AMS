using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class MailPro
{
    //MailID	int	Unchecked
    //MailCode	varchar(100)	Unchecked
    //VerID	varchar(255)	Unchecked
    //MailType	varchar(100)	Checked
    //MailSendType	varchar(10)	Checked
    //MailRequestType	char(3)	Checked
    //MailNameAr	varchar(500)	Checked
    //MailNameEn	varchar(500)	Checked
    //MailTemp	text	Checked
    //MailViewName	varchar(500)	Checked
    //MailViewCondition	varchar(500)	Checked
    //MailPKList	varchar(2000)	Checked
    //SchType	varchar(20)	Checked
    //SchDays	varchar(100)	Checked
    //SchStartTimeShift1	datetime	Checked
    //SchStartTimeShift2	datetime	Checked
    //SchStartTimeShift3	datetime	Checked
    //SchActive	bit	Checked
    //MailDesc	varchar(MAX)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _MailID;
	public string MailID { get { return _MailID; } set { if (_MailID != value) { _MailID = value; } } }

    string _MailCode;
    public string MailCode { get { return _MailCode; } set { if (_MailCode != value) { _MailCode = value; } } }

    string _VerID;
	public string VerID { get { return _VerID; } set { if (_VerID != value) { _VerID = value; } } }

    string _MailType;
	public string MailType { get { return _MailType; } set { if (_MailType != value) { _MailType = value; } } }

    string _MailSendType;
    public string MailSendType { get { return _MailSendType; } set { if (_MailSendType != value) { _MailSendType = value; } } }
    
    string _MailRequestType;
	public string MailRequestType { get { return _MailRequestType; } set { if (_MailRequestType != value) { _MailRequestType = value; } } }

    string _MailNameAr;
    public string MailNameAr { get { return _MailNameAr; } set { if (_MailNameAr != value) { _MailNameAr = value; } } }

    string _MailNameEn;
    public string MailNameEn { get { return _MailNameEn; } set { if (_MailNameEn != value) { _MailNameEn = value; } } }

    string _MailPKList;
    public string MailPKList { get { return _MailPKList; } set { if (_MailPKList != value) { _MailPKList = value; } } }

    string _SchType;
    public string SchType { get { return _SchType; } set { if (_SchType != value) { _SchType = value; } } }

    string _SchDays;
    public string SchDays { get { return _SchDays; } set { if (_SchDays != value) { _SchDays = value; } } }

    string _SchStartTimeShift1;
    public string SchStartTimeShift1 { get { return _SchStartTimeShift1; } set { if (_SchStartTimeShift1 != value) { _SchStartTimeShift1 = value; } } }

    string _SchStartTimeShift2;
    public string SchStartTimeShift2 { get { return _SchStartTimeShift2; } set { if (_SchStartTimeShift2 != value) { _SchStartTimeShift2 = value; } } }

    string _SchStartTimeShift3;
    public string SchStartTimeShift3 { get { return _SchStartTimeShift3; } set { if (_SchStartTimeShift3 != value) { _SchStartTimeShift3 = value; } } }
    
    string _MailDesc;
    public string MailDesc { get { return _MailDesc; } set { if (_MailDesc != value) { _MailDesc = value; } } }

    bool _SchActive;
    public bool SchActive { get { return _SchActive; } set { if (_SchActive != value) { _SchActive = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}