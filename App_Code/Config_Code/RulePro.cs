using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class RulePro
{
    //RltID	int	Unchecked
    //RlsID	int	Checked
    //RltType	varchar(50)	Checked
    //RltRuleMeasureIn	char(7)	Checked
    //RltUnits	int	Checked
    //RltFrequencyFlag	bit	Checked
    //RltFrequency	int	Checked
    //RltActionMeasureIn	varchar(10)	Checked
    //RltActionUnits	int	Unchecked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RltID;
	public string  RltID { get { return _RltID; } set { if (_RltID != value) { _RltID = value; } } }

    string _RlsID;
    public string RlsID { get { return _RlsID; } set { if (_RlsID != value) { _RlsID = value; } } }

    string _RltType;
	public string  RltType { get { return _RltType; } set { if (_RltType != value) { _RltType = value; } } }

    string _RltRuleMeasureIn;
	public string  RltRuleMeasureIn { get { return _RltRuleMeasureIn; } set { if (_RltRuleMeasureIn != value) { _RltRuleMeasureIn = value; } } }

    string _RltUnits;
    public string RltUnits { get { return _RltUnits; } set { if (_RltUnits != value) { _RltUnits = value; } } }
    
    string _RltFrequency;
	public string  RltFrequency { get { return _RltFrequency; } set { if (_RltFrequency != value) { _RltFrequency = value; } } }

    string _RltActionMeasureIn;
    public string RltActionMeasureIn { get { return _RltActionMeasureIn; } set { if (_RltActionMeasureIn != value) { _RltActionMeasureIn = value; } } }

    string _RltActionUnits;
    public string RltActionUnits { get { return _RltActionUnits; } set { if (_RltActionUnits != value) { _RltActionUnits = value; } } }

    bool _RltFrequencyFlag;
    public bool RltFrequencyFlag { get { return _RltFrequencyFlag; } set { if (_RltFrequencyFlag != value) { _RltFrequencyFlag = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //RlsID	int	Unchecked
    //RlsNameEn	varchar(50)	Unchecked
    //RlsNameAr	varchar(50)	Checked
    //RlsStatus	bit	Checked
    
    //string _RlsID;
    //public string  RlsID { get { return _RlsID; } set { if (_RlsID != value) { _RlsID = value; } } }

    string _RlsNameEn;
    public string RlsNameEn { get { return _RlsNameEn; } set { if (_RlsNameEn != value) { _RlsNameEn = value; } } }

    string _RlsNameAr;
    public string RlsNameAr { get { return _RlsNameAr; } set { if (_RlsNameAr != value) { _RlsNameAr = value; } } }

    bool _RlsStatus;
    public bool RlsStatus { get { return _RlsStatus; } set { if (_RlsStatus != value) { _RlsStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}