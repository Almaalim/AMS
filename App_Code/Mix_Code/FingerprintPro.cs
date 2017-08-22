using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FingerprintPro
{
    //ID varchar(20) Unchecked
    //Image1  image Checked
    //Template1 binary(256) Checked
    //Image2  image Checked
    //Template2 binary(256) Checked
    //Image3  image Checked
    //Template3 binary(256) Checked
    //Image4  image Checked
    //Template4 binary(256) Checked
    //Image5  image Checked
    //Template5 binary(256) Checked
    //Image6  image Checked
    //Template6 binary(256) Checked
    //Image7  image Checked
    //Template7 binary(256) Checked
    //Image8  image Checked
    //Template8 binary(256) Checked
    //Image9  image Checked
    //Template9 binary(256) Checked
    //Image10 image Checked
    //Template10 binary(256) Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpID;
    public string EmpID { get { return _EmpID; } set { if (_EmpID != value) { _EmpID = value; } } }

    byte[] _FPImage;
    public byte[] FPImage { get { return _FPImage; } set { if (_FPImage != value) { _FPImage = value; } } }

    byte[] _FPTemplate;
    public byte[] FPTemplate { get { return _FPTemplate; } set { if (_FPTemplate != value) { _FPTemplate = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _ActionID;
    public string ActionID { get { return _ActionID; } set { if (_ActionID != value) { _ActionID = value; } } }

    string _FPID;
    public string FPID { get { return _FPID; } set { if (_FPID != value) { _FPID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _TransType;
    public string TransType { get { return _TransType; } set { if (_TransType != value) { _TransType = value; } } }

    DateTime _TransDate;
    public DateTime TransDate { get { return _TransDate; } set { if (_TransDate != value) { _TransDate = value; } } }

    string _TransTime;
    public string TransTime { get { return _TransTime; } set { if (_TransTime != value) { _TransTime = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}