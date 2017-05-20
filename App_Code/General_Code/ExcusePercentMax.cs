using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;

public class ExcusePercentMax
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static DBFun DBCs = new DBFun();
    static DTFun DTCs = new DTFun();
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public ExcusePercentMax() { }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int GetSecondsShiftDuration(string pWrkID)
    {
        int ScndsShith = 0;
        
        DataTable DT = DBCs.FetchData(" SELECT WktShift1Duration FROM WorkingTime WHERE WktID = @P1 ", new string[] { pWrkID });
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            if (DT.Rows[0]["WktShift1Duration"] != DBNull.Value)
            {
                int SecondsShift = Convert.ToInt32(DT.Rows[0]["WktShift1Duration"].ToString());
                ScndsShith = SecondsShift;
            }
        }
        return ScndsShith;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int GetSecondsShiftDuration(string pEmpID, string pDateType, string pDateReq)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
        int MintsShith = 0;

        DateTime StartDate = DTCs.ConvertToDatetime(pDateReq, pDateType);

        DataTable WDT = DBCs.FetchData(" SELECT TOP 1 WktID FROM EmpWrkRel WHERE EmpID = @P1 AND @P2 BETWEEN EwrStartDate AND EwrEndDate AND ISNULL(EwrDeleted,0) = 0  ORDER BY EwrID DESC ", new string[] { pEmpID,StartDate.ToString()  });
        if (!DBCs.IsNullOrEmpty(WDT)) 
        {
            string pWktID = WDT.Rows[0]["WktID"].ToString();

             DataTable DT = DBCs.FetchData(" SELECT WktShift1Duration FROM WorkingTime WHERE WktID = @P1 ", new string[] { pWktID });
             if (!DBCs.IsNullOrEmpty(DT))
             {
                if (DT.Rows[0]["WktShift1Duration"] != DBNull.Value)
                {
                    int SecondsShift = Convert.ToInt32(DT.Rows[0]["WktShift1Duration"].ToString());
                    MintsShith = SecondsShift;
                }
             }
        }
        return MintsShith;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int GetPercentMaxExc(string pExcID)
    {
        int PercentExc = 0;
        DataTable DT = DBCs.FetchData(" SELECT ExcPercentageAllowable FROM ExcuseType WHERE ExcID = @P1 ", new string[] { pExcID });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["ExcPercentageAllowable"] != DBNull.Value)
            {
                PercentExc = Convert.ToInt32(DT.Rows[0]["ExcPercentageAllowable"].ToString());
            }
        }
        return PercentExc;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int GetMaxSeconds(int pShiftDurMints, int pPercentMaxExc)
    {
        int MaxSeconds = (pShiftDurMints / 100) * pPercentMaxExc;

        return MaxSeconds;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int GetTotalRequestedExc(string pEmpID, string pDateType, string pDateRequest, string pExcID)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
        int TotalRequseted = 0;

        DateTime StartDate = DTCs.ConvertToDatetime(pDateRequest, pDateType);

        /////////////////////// Get Totla time Requested From EmpExcRel/////////////////////////////
        DataTable DT1 = DBCs.FetchData(" SELECT DATEDIFF(SECOND,ExrStartTime,ExrEndTime) AS SumSec FROM EmpExcRel WHERE ExcID = @P1 AND EmpID = @P2 AND @P3 BETWEEN ExrStartDate AND ExrEndDate AND ISNULL(ExrDeleted,0) = 0 ", new string[] { pExcID, pEmpID, StartDate.ToString() });
        if (!DBCs.IsNullOrEmpty(DT1))
        {
            foreach (DataRow Exrrow in DT1.Rows)
            {
                TotalRequseted += Convert.ToInt32(Exrrow["SumSec"].ToString());
            }
        }

        ///////////////////// Get Totla time Requested From EmpRequest/////////////////////////////////
        DataTable DT2 = DBCs.FetchData(" SELECT DATEDIFF(SECOND,ErqStartTime,ErqEndTime) AS SumSec FROM EmpRequest WHERE RetID = 'EXC' AND ErqTypeID = @P1 AND EmpID = @P2 AND ErqStartDate = @P3 AND ErqReqStatus IN ('0','1') ", new string[] { pExcID, pEmpID, StartDate.ToString() });
        if (!DBCs.IsNullOrEmpty(DT2))
        {
            foreach (DataRow Exrrow in DT2.Rows)
            {
                TotalRequseted += Convert.ToInt32(Exrrow["SumSec"].ToString());
            }
        }

        return TotalRequseted;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}