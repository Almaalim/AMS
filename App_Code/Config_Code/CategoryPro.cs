﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CategoryPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _CatID;
	public string  CatID { get { return _CatID; } set { if (_CatID != value) { _CatID = value; } } }

    string _CatNameAr;
    public string CatNameAr { get { return _CatNameAr; } set { if (_CatNameAr != value) { _CatNameAr = value; } } }

    string _CatNameEn;
	public string  CatNameEn { get { return _CatNameEn; } set { if (_CatNameEn != value) { _CatNameEn = value; } } }

    string _CatDesc;
	public string  CatDesc { get { return _CatDesc; } set { if (_CatDesc != value) { _CatDesc = value; } } }

    bool _CatStatus;
    public bool CatStatus { get { return _CatStatus; } set { if (_CatStatus != value) { _CatStatus = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}