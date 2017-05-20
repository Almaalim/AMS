using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ConfigPro
{
    //cfgAutoIn	bit	Checked
    //cfgIsTakeAutoIn	bit	Checked
    //cfgAutoOut	bit	Checked
    //cfgIsTakeAutoOut	bit	Checked
    //cfgMaxPercOT	int	Checked
    
    //cfgVacResetDate	        datetime	Checked
    //cfgICApprovalPercent	    int	Checked   
    //cfgRedundantInSelection	char(1)	Checked
    //cfgRedundantOutSelection	char(1)	Checked
    //cfgSessionDuration	    int	Checked
    
    //cfgOTBeginEarlyFlag	bit	Checked
    //cfgOTOutLateFlag	bit	Checked
    //cfgOTOutOfShiftFlag	bit	Checked
    //cfgOTNoShiftFlag	bit	Checked
    //cfgOTBeginEarlyInterval	int	Checked
    //cfgOTOutLateInterval	int	Checked
    //cfgOTOutOfShiftInterval	int	Checked
    //cfgOTNoShiftInterval	int	Checked
    
    //cfgMiddleGapCount	int	Checked
    //cfgDataLang	char(1)	Checked
    //cfgFormReq	varchar(500)	Checked
    //cfgAutoTransaction	bit	Checked
    //cfgApprovalsMonthCount	int	Checked
    //cfgTransInDaysCount	int	Checked
    //cfgFlexHours	int	Checked
    //cfgDaysLimitReqVac	int	Checked
    //cfgOrderTransType	varchar(10)	Checked
    
    //cfgOTInVacInterval	int	Checked
    //cfgOTInVacFlag	bit	Checked
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool _cfgAutoIn;
	public bool  cfgAutoIn { get { return _cfgAutoIn; } set { if (_cfgAutoIn != value) { _cfgAutoIn = value; } } }

    bool _cfgIsTakeAutoIn;
    public bool cfgIsTakeAutoIn { get { return _cfgIsTakeAutoIn; } set { if (_cfgIsTakeAutoIn != value) { _cfgIsTakeAutoIn = value; } } }

    bool _cfgAutoOut;
	public bool  cfgAutoOut { get { return _cfgAutoOut; } set { if (_cfgAutoOut != value) { _cfgAutoOut = value; } } }

    bool _cfgIsTakeAutoOut;
	public bool  cfgIsTakeAutoOut { get { return _cfgIsTakeAutoOut; } set { if (_cfgIsTakeAutoOut != value) { _cfgIsTakeAutoOut = value; } } }

    string _cfgMaxPercOT;
    public string cfgMaxPercOT { get { return _cfgMaxPercOT; } set { if (_cfgMaxPercOT != value) { _cfgMaxPercOT = value; } } }
    
    string _cfgVacResetDate;
	public string  cfgVacResetDate { get { return _cfgVacResetDate; } set { if (_cfgVacResetDate != value) { _cfgVacResetDate = value; } } }

    string _cfgICApprovalPercent;
    public string cfgICApprovalPercent { get { return _cfgICApprovalPercent; } set { if (_cfgICApprovalPercent != value) { _cfgICApprovalPercent = value; } } }

    string _cfgRedundantInSelection;
    public string cfgRedundantInSelection { get { return _cfgRedundantInSelection; } set { if (_cfgRedundantInSelection != value) { _cfgRedundantInSelection = value; } } }

    string _cfgRedundantOutSelection;
    public string cfgRedundantOutSelection { get { return _cfgRedundantOutSelection; } set { if (_cfgRedundantOutSelection != value) { _cfgRedundantOutSelection = value; } } }

    string _cfgSessionDuration;
    public string cfgSessionDuration { get { return _cfgSessionDuration; } set { if (_cfgSessionDuration != value) { _cfgSessionDuration = value; } } }

    bool _cfgOTBeginEarlyFlag;
    public bool cfgOTBeginEarlyFlag { get { return _cfgOTBeginEarlyFlag; } set { if (_cfgOTBeginEarlyFlag != value) { _cfgOTBeginEarlyFlag = value; } } }

    bool _cfgOTOutLateFlag;
    public bool cfgOTOutLateFlag { get { return _cfgOTOutLateFlag; } set { if (_cfgOTOutLateFlag != value) { _cfgOTOutLateFlag = value; } } }

    bool _cfgOTOutOfShiftFlag;
    public bool cfgOTOutOfShiftFlag { get { return _cfgOTOutOfShiftFlag; } set { if (_cfgOTOutOfShiftFlag != value) { _cfgOTOutOfShiftFlag = value; } } }

    bool _cfgOTNoShiftFlag;
    public bool cfgOTNoShiftFlag { get { return _cfgOTNoShiftFlag; } set { if (_cfgOTNoShiftFlag != value) { _cfgOTNoShiftFlag = value; } } }

    bool _cfgOTInVacFlag;
    public bool cfgOTInVacFlag { get { return _cfgOTInVacFlag; } set { if (_cfgOTInVacFlag != value) { _cfgOTInVacFlag = value; } } }

    string _cfgOTBeginEarlyInterval;
    public string cfgOTBeginEarlyInterval { get { return _cfgOTBeginEarlyInterval; } set { if (_cfgOTBeginEarlyInterval != value) { _cfgOTBeginEarlyInterval = value; } } }

    string _cfgOTOutLateInterval;
    public string cfgOTOutLateInterval { get { return _cfgOTOutLateInterval; } set { if (_cfgOTOutLateInterval != value) { _cfgOTOutLateInterval = value; } } }

    string _cfgOTOutOfShiftInterval;
    public string cfgOTOutOfShiftInterval { get { return _cfgOTOutOfShiftInterval; } set { if (_cfgOTOutOfShiftInterval != value) { _cfgOTOutOfShiftInterval = value; } } }

    string _cfgOTNoShiftInterval;
    public string cfgOTNoShiftInterval { get { return _cfgOTNoShiftInterval; } set { if (_cfgOTNoShiftInterval != value) { _cfgOTNoShiftInterval = value; } } }

    string _cfgOTInVacInterval;
    public string cfgOTInVacInterval { get { return _cfgOTInVacInterval; } set { if (_cfgOTInVacInterval != value) { _cfgOTInVacInterval = value; } } }

    string _cfgMiddleGapCount;
    public string cfgMiddleGapCount { get { return _cfgMiddleGapCount; } set { if (_cfgMiddleGapCount != value) { _cfgMiddleGapCount = value; } } }

    string _cfgDataLang;
    public string cfgDataLang { get { return _cfgDataLang; } set { if (_cfgDataLang != value) { _cfgDataLang = value; } } }

    string _cfgFormReq;
    public string cfgFormReq { get { return _cfgFormReq; } set { if (_cfgFormReq != value) { _cfgFormReq = value; } } }

    bool _cfgAutoTransaction;
    public bool cfgAutoTransaction { get { return _cfgAutoTransaction; } set { if (_cfgAutoTransaction != value) { _cfgAutoTransaction = value; } } }
    
    string _cfgApprovalsMonthCount;
    public string cfgApprovalsMonthCount { get { return _cfgApprovalsMonthCount; } set { if (_cfgApprovalsMonthCount != value) { _cfgApprovalsMonthCount = value; } } }

    string _cfgTransInDaysCount;
    public string cfgTransInDaysCount { get { return _cfgTransInDaysCount; } set { if (_cfgTransInDaysCount != value) { _cfgTransInDaysCount = value; } } }

    string _cfgDaysLimitReqVac;
    public string cfgDaysLimitReqVac { get { return _cfgDaysLimitReqVac; } set { if (_cfgDaysLimitReqVac != value) { _cfgDaysLimitReqVac = value; } } }

    string _cfgOrderTransType;
    public string cfgOrderTransType { get { return _cfgOrderTransType; } set { if (_cfgOrderTransType != value) { _cfgOrderTransType = value; } } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
}