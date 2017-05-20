using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class MenuPro
{
    //MnuNumber	int	Unchecked
    //MnuID	int	Checked
    //MnuPermissionID	int	Checked
    //MnuTextEn	varchar(50)	Checked
    //MnuDescription	varchar(255)	Checked
    //MnuTextAr	varchar(50)	Checked
    //MnuArabicDescription	varchar(255)	Checked
    //MnuServer	varchar(50)	Checked
    //MnuURL	varchar(255)	Checked
    //MnuParentID	int	Checked
    //MnuVisible	varchar(5)	Checked
    //MnuImageURL	varchar(50)	Checked
    //MnuOrder	int	Checked
    //MnuType	varchar(50)	Checked
    //RgpID	int	Checked
    //VerID	varchar(255)	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _MnuNumber;
	public string MnuNumber { get { return _MnuNumber; } set { if (_MnuNumber != value) { _MnuNumber = value; } } }

    string _MnuID;
    public string MnuID { get { return _MnuID; } set { if (_MnuID != value) { _MnuID = value; } } }

    string _MnuPermissionID;
	public string MnuPermissionID { get { return _MnuPermissionID; } set { if (_MnuPermissionID != value) { _MnuPermissionID = value; } } }

    string _MnuTextEn;
	public string MnuTextEn { get { return _MnuTextEn; } set { if (_MnuTextEn != value) { _MnuTextEn = value; } } }

    string _MnuTextAr;
    public string MnuTextAr { get { return _MnuTextAr; } set { if (_MnuTextAr != value) { _MnuTextAr = value; } } }
    
    string _MnuDescription;
	public string MnuDescription { get { return _MnuDescription; } set { if (_MnuDescription != value) { _MnuDescription = value; } } }

    string _MnuArabicDescription;
    public string MnuArabicDescription { get { return _MnuArabicDescription; } set { if (_MnuArabicDescription != value) { _MnuArabicDescription = value; } } }

    string _MnuServer;
    public string MnuServer { get { return _MnuServer; } set { if (_MnuServer != value) { _MnuServer = value; } } }

    string _MnuURL;
    public string MnuURL { get { return _MnuURL; } set { if (_MnuURL != value) { _MnuURL = value; } } }

    string _MnuParentID;
    public string MnuParentID { get { return _MnuParentID; } set { if (_MnuParentID != value) { _MnuParentID = value; } } }

    string _MnuVisible;
    public string MnuVisible { get { return _MnuVisible; } set { if (_MnuVisible != value) { _MnuVisible = value; } } }

    string _MnuImageURL;
    public string MnuImageURL { get { return _MnuImageURL; } set { if (_MnuImageURL != value) { _MnuImageURL = value; } } }

    string _MnuOrder;
    public string MnuOrder { get { return _MnuOrder; } set { if (_MnuOrder != value) { _MnuOrder = value; } } }

    string _MnuType;
    public string MnuType { get { return _MnuType; } set { if (_MnuType != value) { _MnuType = value; } } }

    string _RgpID;
    public string RgpID { get { return _RgpID; } set { if (_RgpID != value) { _RgpID = value; } } }

    string _VerID;
    public string VerID { get { return _VerID; } set { if (_VerID != value) { _VerID = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}