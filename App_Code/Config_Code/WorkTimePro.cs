using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class WorkTimePro
{
    //WktID	int	Unchecked
    //WktNameAr	varchar(100)	Checked
    //WktNameEn	varchar(100)	Checked
    //WktDesc	varchar(255)	Checked
    //WtpID	int	Checked
    //WktShiftCount	int	Checked
    //WktShift1NameAr	varchar(50)	Checked
    //WktShift1NameEn	varchar(50)	Checked
    //WktShift1From	datetime	Checked
    //WktShift1To	datetime	Checked
    //WktShift1Duration	int	Checked
    //WktShift1Grace	int	Checked
    //WktShift1MiddleGrace	int	Checked
    //WktShift1EndGrace	int	Checked
    //WktShift1IsOverNight	bit	Checked
    //WktShift1IsOptional	bit	Checked
    //WktShift1FTHours	int	Checked
    //WktShift1AddPercentOverNight	int	Checked
    //WktShift2NameAr	varchar(50)	Checked
    //WktShift2NameEn	varchar(50)	Checked
    //WktShift2From	datetime	Checked
    //WktShift2To	datetime	Checked
    //WktShift2Duration	int	Checked
    //WktShift2Grace	int	Checked
    //WktShift2MiddleGrace	int	Checked
    //WktShift2EndGrace	int	Checked
    //WktShift2IsOverNight	bit	Checked
    //WktShift2IsOptional	bit	Checked
    //WktShift2FTHours	int	Checked
    //WktShift2AddPercentOverNight	int	Checked
    //WktShift3NameAr	varchar(50)	Checked
    //WktShift3NameEn	varchar(50)	Checked
    //WktShift3From	datetime	Checked
    //WktShift3To	datetime	Checked
    //WktShift3Duration	int	Checked
    //WktShift3Grace	int	Checked
    //WktShift3MiddleGrace	int	Checked
    //WktShift3EndGrace	int	Checked
    //WktShift3IsOverNight	bit	Checked
    //WktShift3IsOptional	bit	Checked
    //WktShift3FTHours	int	Checked
    //WktShift3AddPercentOverNight	int	Checked
    //WktIsActive	bit	Checked
    //WktAddPercent	int	Checked
    //WktShift1FTSet	int	Checked
    //WktShift2FTSet	int	Checked
    //WktShift3FTSet	int	Checked

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string _WktID;
	public string  WktID { get { return _WktID; } set { if (_WktID != value) { _WktID = value; } } }

    string _WktNameAr;
    public string WktNameAr { get { return _WktNameAr; } set { if (_WktNameAr != value) { _WktNameAr = value; } } }

    string _WktNameEn;
	public string WktNameEn { get { return _WktNameEn; } set { if (_WktNameEn != value) { _WktNameEn = value; } } }

    string _WktDesc;
	public string WktDesc { get { return _WktDesc; } set { if (_WktDesc != value) { _WktDesc = value; } } }

    string _WtpID;
    public string WtpID { get { return _WtpID; } set { if (_WtpID != value) { _WtpID = value; } } }
    
    string _WktShiftCount;
	public string WktShiftCount { get { return _WktShiftCount; } set { if (_WktShiftCount != value) { _WktShiftCount = value; } } }

    bool _WktIsActive;
	public bool WktIsActive { get { return _WktIsActive; } set { if (_WktIsActive != value) { _WktIsActive = value; } } }

    string _WktAddPercent;
	public string WktAddPercent { get { return _WktAddPercent; } set { if (_WktAddPercent != value) { _WktAddPercent = value; } } }
    //////////////////////////////////////
    string _WktShift1NameAr;
    public string WktShift1NameAr { get { return _WktShift1NameAr; } set { if (_WktShift1NameAr != value) { _WktShift1NameAr = value; } } }

    string _WktShift1NameEn;
    public string WktShift1NameEn { get { return _WktShift1NameEn; } set { if (_WktShift1NameEn != value) { _WktShift1NameEn = value; } } }

    string _WktShift1From;
    public string WktShift1From { get { return _WktShift1From; } set { if (_WktShift1From != value) { _WktShift1From = value; } } }

    string _WktShift1To;
    public string WktShift1To { get { return _WktShift1To; } set { if (_WktShift1To != value) { _WktShift1To = value; } } }

    string _WktShift1Duration;
    public string WktShift1Duration { get { return _WktShift1Duration; } set { if (_WktShift1Duration != value) { _WktShift1Duration = value; } } }

    string _WktShift1Grace;
    public string WktShift1Grace { get { return _WktShift1Grace; } set { if (_WktShift1Grace != value) { _WktShift1Grace = value; } } }

    string _WktShift1MiddleGrace;
    public string WktShift1MiddleGrace { get { return _WktShift1MiddleGrace; } set { if (_WktShift1MiddleGrace != value) { _WktShift1MiddleGrace = value; } } }

    string _WktShift1EndGrace;
    public string WktShift1EndGrace { get { return _WktShift1EndGrace; } set { if (_WktShift1EndGrace != value) { _WktShift1EndGrace = value; } } }

    bool _WktShift1IsOverNight;
    public bool WktShift1IsOverNight { get { return _WktShift1IsOverNight; } set { if (_WktShift1IsOverNight != value) { _WktShift1IsOverNight = value; } } }

    bool _WktShift1IsOptional;
    public bool WktShift1IsOptional { get { return _WktShift1IsOptional; } set { if (_WktShift1IsOptional != value) { _WktShift1IsOptional = value; } } }

    string _WktShift1FTHours;
    public string WktShift1FTHours { get { return _WktShift1FTHours; } set { if (_WktShift1FTHours != value) { _WktShift1FTHours = value; } } }

    //string _WktShift1AddPercentOverNight;
    //public string WktShift1AddPercentOverNight { get { return _WktShift1AddPercentOverNight; } set { if (_WktShift1AddPercentOverNight != value) { _WktShift1AddPercentOverNight = value; } } }

    string _WktShift1FTSet;
    public string WktShift1FTSet { get { return _WktShift1FTSet; } set { if (_WktShift1FTSet != value) { _WktShift1FTSet = value; } } }
    //////////////////////////////////////
    string _WktShift2NameAr;
    public string WktShift2NameAr { get { return _WktShift2NameAr; } set { if (_WktShift2NameAr != value) { _WktShift2NameAr = value; } } }

    string _WktShift2NameEn;
    public string WktShift2NameEn { get { return _WktShift2NameEn; } set { if (_WktShift2NameEn != value) { _WktShift2NameEn = value; } } }

    string _WktShift2From;
    public string WktShift2From { get { return _WktShift2From; } set { if (_WktShift2From != value) { _WktShift2From = value; } } }

    string _WktShift2To;
    public string WktShift2To { get { return _WktShift2To; } set { if (_WktShift2To != value) { _WktShift2To = value; } } }

    string _WktShift2Duration;
    public string WktShift2Duration { get { return _WktShift2Duration; } set { if (_WktShift2Duration != value) { _WktShift2Duration = value; } } }

    string _WktShift2Grace;
    public string WktShift2Grace { get { return _WktShift2Grace; } set { if (_WktShift2Grace != value) { _WktShift2Grace = value; } } }

    string _WktShift2MiddleGrace;
    public string WktShift2MiddleGrace { get { return _WktShift2MiddleGrace; } set { if (_WktShift2MiddleGrace != value) { _WktShift2MiddleGrace = value; } } }

    string _WktShift2EndGrace;
    public string WktShift2EndGrace { get { return _WktShift2EndGrace; } set { if (_WktShift2EndGrace != value) { _WktShift2EndGrace = value; } } }

    bool _WktShift2IsOverNight;
    public bool WktShift2IsOverNight { get { return _WktShift2IsOverNight; } set { if (_WktShift2IsOverNight != value) { _WktShift2IsOverNight = value; } } }

    bool _WktShift2IsOptional;
    public bool WktShift2IsOptional { get { return _WktShift2IsOptional; } set { if (_WktShift2IsOptional != value) { _WktShift2IsOptional = value; } } }

    string _WktShift2FTHours;
    public string WktShift2FTHours { get { return _WktShift2FTHours; } set { if (_WktShift2FTHours != value) { _WktShift2FTHours = value; } } }

    //string _WktShift2AddPercentOverNight;
    //public string WktShift2AddPercentOverNight { get { return _WktShift2AddPercentOverNight; } set { if (_WktShift2AddPercentOverNight != value) { _WktShift2AddPercentOverNight = value; } } }

    string _WktShift2FTSet;
    public string WktShift2FTSet { get { return _WktShift2FTSet; } set { if (_WktShift2FTSet != value) { _WktShift2FTSet = value; } } }
    //////////////////////////////////////
    string _WktShift3NameAr;
    public string WktShift3NameAr { get { return _WktShift3NameAr; } set { if (_WktShift3NameAr != value) { _WktShift3NameAr = value; } } }

    string _WktShift3NameEn;
    public string WktShift3NameEn { get { return _WktShift3NameEn; } set { if (_WktShift3NameEn != value) { _WktShift3NameEn = value; } } }

    string _WktShift3From;
    public string WktShift3From { get { return _WktShift3From; } set { if (_WktShift3From != value) { _WktShift3From = value; } } }

    string _WktShift3To;
    public string WktShift3To { get { return _WktShift3To; } set { if (_WktShift3To != value) { _WktShift3To = value; } } }

    string _WktShift3Duration;
    public string WktShift3Duration { get { return _WktShift3Duration; } set { if (_WktShift3Duration != value) { _WktShift3Duration = value; } } }

    string _WktShift3Grace;
    public string WktShift3Grace { get { return _WktShift3Grace; } set { if (_WktShift3Grace != value) { _WktShift3Grace = value; } } }

    string _WktShift3MiddleGrace;
    public string WktShift3MiddleGrace { get { return _WktShift3MiddleGrace; } set { if (_WktShift3MiddleGrace != value) { _WktShift3MiddleGrace = value; } } }

    string _WktShift3EndGrace;
    public string WktShift3EndGrace { get { return _WktShift3EndGrace; } set { if (_WktShift3EndGrace != value) { _WktShift3EndGrace = value; } } }

    bool _WktShift3IsOverNight;
    public bool WktShift3IsOverNight { get { return _WktShift3IsOverNight; } set { if (_WktShift3IsOverNight != value) { _WktShift3IsOverNight = value; } } }

    bool _WktShift3IsOptional;
    public bool WktShift3IsOptional { get { return _WktShift3IsOptional; } set { if (_WktShift3IsOptional != value) { _WktShift3IsOptional = value; } } }

    string _WktShift3FTHours;
    public string WktShift3FTHours { get { return _WktShift3FTHours; } set { if (_WktShift3FTHours != value) { _WktShift3FTHours = value; } } }

    //string _WktShift3AddPercentOverNight;
    //public string WktShift3AddPercentOverNight { get { return _WktShift3AddPercentOverNight; } set { if (_WktShift3AddPercentOverNight != value) { _WktShift3AddPercentOverNight = value; } } }

    string _WktShift3FTSet;
    public string WktShift3FTSet { get { return _WktShift3FTSet; } set { if (_WktShift3FTSet != value) { _WktShift3FTSet = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}