using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FavoriteFormsPro
{
    //FavID	int	Unchecked
    //FavFormID	varchar(100)	Unchecked
    //FavNameEn	varchar(2000)	Checked
    //FavNameAr	varchar(2000)	Checked
    //FavUsrName	varchar(15)	Unchecked
    //FavType	char(1)	Unchecked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _FavID;
	public string FavID { get { return _FavID; } set { if (_FavID != value) { _FavID = value; } } }

    string _FavFormID;
    public string FavFormID { get { return _FavFormID; } set { if (_FavFormID != value) { _FavFormID = value; } } }

    string _FavNameEn;
	public string FavNameEn { get { return _FavNameEn; } set { if (_FavNameEn != value) { _FavNameEn = value; } } }

    string _FavNameAr;
	public string FavNameAr { get { return _FavNameAr; } set { if (_FavNameAr != value) { _FavNameAr = value; } } }

    string _FavUsrName;
    public string FavUsrName { get { return _FavUsrName; } set { if (_FavUsrName != value) { _FavUsrName = value; } } }
    
    string _FavType;
	public string FavType { get { return _FavType; } set { if (_FavType != value) { _FavType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _FParPanel;
    public string FParPanel { get { return _FParPanel; } set { if (_FParPanel != value) { _FParPanel = value; } } }

    string _FParValue;
    public string FParValue { get { return _FParValue; } set { if (_FParValue != value) { _FParValue = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}