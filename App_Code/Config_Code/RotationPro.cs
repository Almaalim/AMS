public class RotationPro
{
    //RwtID	                int	Unchecked
    //RwtNameAr          	varchar(500)	Checked
    //RwtNameEn	            varchar(500)	Checked
    //RwtFromDate	        datetime	Unchecked
    //RwtToDate	            datetime	Unchecked
    //RwtWorkDaysCount	    int	Checked
    //RwtNotWorkDaysCount	int	Checked
    //RwtRotationDaysCount	int	Checked
    //RwtIsActive	        bit	Checked
    //RwtType	            varchar(100)	Unchecked
    //RwtDesc	            varchar(8000)	Checked
    //RwtDeleted	        bit	Checked

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _RwtID;
	public string  RwtID { get { return _RwtID; } set { if (_RwtID != value) { _RwtID = value; } } }

    string _RwtNameAr;
    public string RwtNameAr { get { return _RwtNameAr; } set { if (_RwtNameAr != value) { _RwtNameAr = value; } } }

    string _RwtNameEn;
	public string RwtNameEn { get { return _RwtNameEn; } set { if (_RwtNameEn != value) { _RwtNameEn = value; } } }

    string _RwtFromDate;
	public string RwtFromDate { get { return _RwtFromDate; } set { if (_RwtFromDate != value) { _RwtFromDate = value; } } }

    string _RwtToDate;
    public string RwtToDate { get { return _RwtToDate; } set { if (_RwtToDate != value) { _RwtToDate = value; } } }
    
    int _RwtWorkDaysCount;
	public int RwtWorkDaysCount { get { return _RwtWorkDaysCount; } set { if (_RwtWorkDaysCount != value) { _RwtWorkDaysCount = value; } } }

    int _RwtNotWorkDaysCount;
	public int RwtNotWorkDaysCount { get { return _RwtNotWorkDaysCount; } set { if (_RwtNotWorkDaysCount != value) { _RwtNotWorkDaysCount = value; } } }

    int _RwtRotationDaysCount;
	public int RwtRotationDaysCount { get { return _RwtRotationDaysCount; } set { if (_RwtRotationDaysCount != value) { _RwtRotationDaysCount = value; } } }

    bool _RwtIsActive;
	public bool RwtIsActive { get { return _RwtIsActive; } set { if (_RwtIsActive != value) { _RwtIsActive = value; } } }

    string _RwtType;
	public string RwtType { get { return _RwtType; } set { if (_RwtType != value) { _RwtType = value; } } }

    string _RwtDesc;
    public string RwtDesc { get { return _RwtDesc; } set { if (_RwtDesc != value) { _RwtDesc = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _EmpIDs;
    public string EmpIDs { get { return _EmpIDs; } set { if (_EmpIDs != value) { _EmpIDs = value; } } }

    string _WktIDs;
	public string WktIDs { get { return _WktIDs; } set { if (_WktIDs != value) { _WktIDs = value; } } }

    string _GrpIDs;
	public string GrpIDs { get { return _GrpIDs; } set { if (_GrpIDs != value) { _GrpIDs = value; } } }
    
    string _GrpUsers;
	public string GrpUsers { get { return _GrpUsers; } set { if (_GrpUsers != value) { _GrpUsers = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    string _RwtAddStartDate;
    public string RwtAddStartDate { get { return _RwtAddStartDate; } set { if (_RwtAddStartDate != value) { _RwtAddStartDate = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}