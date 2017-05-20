using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ReportPro
{
    //RepID	char(10)	Unchecked
    //RepParentID	int	Checked
    //RepNameAr	varchar(100)	Checked
    //RepNameEn	varchar(100)	Unchecked
    //RepForSchedule	bit	Checked
    //RepURL	varchar(100)	Checked
    //RepDefaultURL	varchar(100)	Checked
    //RepDescAr	varchar(255)	Checked
    //RepDescEn	varchar(255)	Checked
    //RepVisible	bit	Checked
    //RgpID	int	Checked
    //RepTempAr	varchar(MAX)	Checked
    //RepTempEn	varchar(MAX)	Checked
    //RepPanels	bigint	Checked
    //RepStringDef	varchar(MAX)	Checked
    //RepStringEdited	varchar(MAX)	Checked
    //RepProcedureName	varchar(100)	Checked
    //RepTempDefEn	varchar(MAX)	Checked
    //RepTempDefAr	varchar(MAX)	Checked
    //RepParametersProc	varchar(100)	Checked
    //RepOrientation	char(1)	Checked
    //VerID	varchar(255)	Checked
    //RepType	varchar(255)	Checked
    //RepGeneralID	char(10)	Checked
    //RepHeaderType	char(1)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RepID;
	public string RepID { get { return _RepID; } set { if (_RepID != value) { _RepID = value; } } }

    string _RepParentID;
    public string RepParentID { get { return _RepParentID; } set { if (_RepParentID != value) { _RepParentID = value; } } }

    string _RgpID;
    public string RgpID { get { return _RgpID; } set { if (_RgpID != value) { _RgpID = value; } } }

    string _RepNameAr;
	public string RepNameAr { get { return _RepNameAr; } set { if (_RepNameAr != value) { _RepNameAr = value; } } }

    string _RepNameEn;
	public string RepNameEn { get { return _RepNameEn; } set { if (_RepNameEn != value) { _RepNameEn = value; } } }
    
    string _RepDescAr;
    public string RepDescAr { get { return _RepDescAr; } set { if (_RepDescAr != value) { _RepDescAr = value; } } }

    string _RepDescEn;
    public string RepDescEn { get { return _RepDescEn; } set { if (_RepDescEn != value) { _RepDescEn = value; } } }
    
    string _RepType;
    public string RepType { get { return _RepType; } set { if (_RepType != value) { _RepType = value; } } }

    string _RepOrientation;
    public string RepOrientation { get { return _RepOrientation; } set { if (_RepOrientation != value) { _RepOrientation = value; } } }

    string _RepPanels;
    public string RepPanels { get { return _RepPanels; } set { if (_RepPanels != value) { _RepPanels = value; } } }

    bool _RepForSchedule;
    public bool RepForSchedule { get { return _RepForSchedule; } set { if (_RepForSchedule != value) { _RepForSchedule = value; } } }
    
    bool _RepVisible;
    public bool RepVisible { get { return _RepVisible; } set { if (_RepVisible != value) { _RepVisible = value; } } }

    string _VerID;
    public string VerID { get { return _VerID; } set { if (_VerID != value) { _VerID = value; } } }
    
    string _RepGeneralID;
    public string RepGeneralID { get { return _RepGeneralID; } set { if (_RepGeneralID != value) { _RepGeneralID = value; } } }    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RepTempAr;
    public string RepTempAr { get { return _RepTempAr; } set { if (_RepTempAr != value) { _RepTempAr = value; } } }

    string _RepTempEn;
    public string RepTempEn { get { return _RepTempEn; } set { if (_RepTempEn != value) { _RepTempEn = value; } } }

    string _RepTempDefAr;
    public string RepTempDefAr { get { return _RepTempDefAr; } set { if (_RepTempDefAr != value) { _RepTempDefAr = value; } } }

    string _RepTempDefEn;
    public string RepTempDefEn { get { return _RepTempDefEn; } set { if (_RepTempDefEn != value) { _RepTempDefEn = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RepProcedureName;
	public string RepProcedureName { get { return _RepProcedureName; } set { if (_RepProcedureName != value) { _RepProcedureName = value; } } }

    string _RepParametersProc;
    public string RepParametersProc { get { return _RepParametersProc; } set { if (_RepParametersProc != value) { _RepParametersProc = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RepLang;
    public string RepLang { get { return _RepLang; } set { if (_RepLang != value) { _RepLang = value; } } }
    
    string _RepTemp;
    public string RepTemp { get { return _RepTemp; } set { if (_RepTemp != value) { _RepTemp = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}