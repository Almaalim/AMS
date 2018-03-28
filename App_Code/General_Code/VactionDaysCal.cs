using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class VactionDaysCal
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static DBFun DBCs = new DBFun();
    static DTFun DTCs = new DTFun();
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public VactionDaysCal() { }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindMaxDays(string pEmpID, string pVacType)
    {
        int MaxDays = 0;
        DataTable EmDT = DBCs.FetchData(" SELECT EvsMaxDays FROM EmpVacSetting WHERE EmpID = @P1 AND VtpID = @P1 ", new string[] { pEmpID, pVacType });
        if (!DBCs.IsNullOrEmpty(EmDT)) { MaxDays = Convert.ToInt32(EmDT.Rows[0]["EvsMaxDays"].ToString()); }
        else
        {
            DataTable DT = DBCs.FetchData(" SELECT VtpMaxDays FROM VacationType WHERE VtpID = @P1 ", new string[] { pVacType });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["VtpMaxDays"] != DBNull.Value) { MaxDays = Convert.ToInt32(DT.Rows[0]["VtpMaxDays"].ToString()); }
            }
        }
        return MaxDays;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool VacIsReset(string pVacType)
    {
        bool VtpIsReset = false;
        DataTable DT = DBCs.FetchData(" SELECT VtpIsReset FROM VacationType WHERE VtpID = @P1 ", new string[] { pVacType });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["VtpIsReset"] != DBNull.Value) { VtpIsReset = Convert.ToBoolean(DT.Rows[0]["VtpIsReset"].ToString()); }
        }
        return VtpIsReset;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindVacResetDates(string DateType, out DateTime StartDate, out DateTime EndDate)
    {
        StartDate = EndDate = DateTime.Now;
        bool Found = false;
        
        try
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT cfgVacResetDate FROM Configuration "));
            if (!DBCs.IsNullOrEmpty(DT))
            {
                if (DT.Rows[0]["cfgVacResetDate"] != DBNull.Value)
                {
                    Found = true;
                    int iYear = Convert.ToInt32(DTCs.FindCurrentYear(DateType));
                    DateTime CDate = DTCs.ConvertToDatetime(DTCs.FindCurrentDate(), DateType);
                    string VacDT = DTCs.ShowDBDate(DT.Rows[0]["cfgVacResetDate"],"dd/MM/yyyy");
                    int iMonth = Convert.ToInt32((VacDT.Split('/'))[1]);
                    int iDay   = Convert.ToInt32((VacDT.Split('/'))[0]);
                    
                    DateTime SDate = DTCs.FindDateFromPart(iDay, iMonth, iYear, DateType);
                    DateTime EDate = DTCs.FindDateFromPart(iDay, iMonth, iYear + 1, DateType);
                    EDate = EDate.AddDays(-1);

                    StartDate = SDate;
                    EndDate   = EDate;

                    if (CDate < StartDate)
                    {
                        StartDate = DTCs.FindDateFromPart(iDay, iMonth, iYear - 1, DateType);
                        EndDate   = DTCs.FindDateFromPart(iDay, iMonth, iYear, DateType);
                        EndDate   = EndDate.AddDays(-1);
                    }
                }
            }
        }
        catch (Exception ex) { Found = false; }

        return Found;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindVacDays(string DateType,string EmpID, string VacType,bool pAll)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            DateTime SDate;
            DateTime EDate;
            bool FoundDate = FindVacResetDates(DateType, out SDate, out EDate);
            
            bool IsReset = VacIsReset(VacType);
            int VavDays = 0;

            if (FoundDate)
            {
                if (!IsReset)
                {
                    if (pAll)
                    {
                        DataTable ErqDT = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,ErqStartDate, ErqEndDate) + 1 ) SumDays FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID = 'VAC' AND ErqTypeID = @P2 ", new string[] { EmpID, VacType });
                        if (!DBCs.IsNullOrEmpty(ErqDT)) { if (ErqDT.Rows[0]["SumDays"] != DBNull.Value) { VavDays = Convert.ToInt32(ErqDT.Rows[0]["SumDays"]); } }
                    }
                    
                    DataTable EvrDT = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,EvrStartDate, EvrEndDate) + 1 ) SumDays FROM EmpVacRel WHERE EmpID = @P1 AND VtpID = @P2 AND ISNULL(EvrDeleted,0) = 0 ", new string[] { EmpID, VacType });
                    if (!DBCs.IsNullOrEmpty(EvrDT)) { if (EvrDT.Rows[0]["SumDays"] != DBNull.Value) { VavDays = Convert.ToInt32(EvrDT.Rows[0]["SumDays"]); } }
                }
                else
                {
                    if (pAll)
                    {
                        DataTable ErqDT1 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,ErqStartDate,ErqEndDate) + 1) SumDays FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID = 'VAC' AND ErqTypeID = @P2 AND ErqStartDate BETWEEN @P3 AND @P4 AND ErqEndDate BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                        if (!DBCs.IsNullOrEmpty(ErqDT1)) { if (ErqDT1.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(ErqDT1.Rows[0]["SumDays"]); } }

                        DataTable ErqDT2 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,ErqStartDate, @P4) + 1) SumDays FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID = 'VAC' AND ErqTypeID = @P2 AND ErqStartDate BETWEEN @P3 AND @P4 AND ErqEndDate NOT BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                        if (!DBCs.IsNullOrEmpty(ErqDT2)) { if (ErqDT2.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(ErqDT2.Rows[0]["SumDays"]); } }

                        DataTable ErqDT3 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY, @P3, ErqEndDate) + 1) SumDays FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID = 'VAC' AND ErqTypeID = @P2 AND ErqStartDate NOT BETWEEN @P3 AND @P4 AND ErqEndDate BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                        if (!DBCs.IsNullOrEmpty(ErqDT3)) { if (ErqDT3.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(ErqDT3.Rows[0]["SumDays"]); } }

                        DataTable ErqDT4 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY, @P3, @P4) + 1) SumDays FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID = 'VAC' AND ErqTypeID = @P2 AND @P3 BETWEEN ErqStartDate AND ErqEndDate AND @P4 BETWEEN ErqStartDate AND ErqEndDate ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                        if (!DBCs.IsNullOrEmpty(ErqDT4)) { if (ErqDT4.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(ErqDT4.Rows[0]["SumDays"]); } }  
                    }

                    DataTable EvrDT1 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,EvrStartDate, EvrEndDate) + 1) SumDays FROM EmpVacRel WHERE EmpID = @P1 AND VtpID = @P2 AND ISNULL(EvrDeleted,0) = 0 AND EvrStartDate BETWEEN @P3 AND @P4 AND EvrEndDate BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                    if (!DBCs.IsNullOrEmpty(EvrDT1)) { if (EvrDT1.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(EvrDT1.Rows[0]["SumDays"]); } }

                    DataTable EvrDT2 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY,EvrStartDate, @P4) + 1) SumDays FROM EmpVacRel WHERE EmpID = @P1 AND VtpID = @P2 AND ISNULL(EvrDeleted,0) = 0 AND EvrStartDate BETWEEN @P3 AND @P4 AND EvrEndDate NOT BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                    if (!DBCs.IsNullOrEmpty(EvrDT2)) { if (EvrDT2.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(EvrDT2.Rows[0]["SumDays"]); } } 

                    DataTable EvrDT3 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY, @P3, EvrEndDate) + 1) SumDays FROM EmpVacRel WHERE EmpID = @P1 AND VtpID = @P2 AND ISNULL(EvrDeleted,0) = 0 AND EvrStartDate NOT BETWEEN @P3 AND @P4 AND EvrEndDate BETWEEN @P3 AND @P4 ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                    if (!DBCs.IsNullOrEmpty(EvrDT3)) { if (EvrDT3.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(EvrDT3.Rows[0]["SumDays"]); } }

                    DataTable EvrDT4 = DBCs.FetchData(" SELECT SUM (DATEDIFF(DAY, @P3, @P4 ) + 1) SumDays FROM EmpVacRel WHERE EmpID = @P1 AND VtpID = @P2 AND ISNULL(EvrDeleted,0) = 0 AND @P3 BETWEEN EvrStartDate AND EvrEndDate AND @P4 BETWEEN EvrStartDate AND EvrEndDate ", new string[] { EmpID, VacType, SDate.ToString(), EDate.ToString() });
                    if (!DBCs.IsNullOrEmpty(EvrDT4)) { if (EvrDT4.Rows[0]["SumDays"] != DBNull.Value) { VavDays += Convert.ToInt32(EvrDT4.Rows[0]["SumDays"]); } }
                }
                return VavDays;
            }
            else { return -1; }
        }
        catch (Exception ex) { return -1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindVacDaysRequest(string DateType, string StartDate, string EndDate)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime SDate = DTCs.ConvertToDatetime(StartDate, DateType); 
            DateTime EDate = DTCs.ConvertToDatetime(EndDate, DateType); 
            int VavDays = Convert.ToInt32((EDate - SDate).TotalDays + 1);   
            return VavDays;
        }
        catch (Exception ex) { return -1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindNestingDates(string DateType, string StartDate, string EndDate, string EmpID)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime SDate = DTCs.ConvertToDatetime(StartDate, DateType); 
            DateTime EDate = DTCs.ConvertToDatetime(EndDate, DateType); 
            DateTime Date  = SDate;
            int Days = Convert.ToInt32((Date - SDate).TotalDays + 1);

            StringBuilder Q = new StringBuilder();
            Q.Append(" SELECT EmpID FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID IN ('VAC','COM','JOB') AND @P2 BETWEEN ErqStartDate AND ErqEndDate ");
            Q.Append(" UNION ALL ");
            Q.Append(" SELECT EmpID FROM EmpVacRel WHERE EmpID = @P1 AND @P2 BETWEEN EvrStartDate AND EvrEndDate AND ISNULL(EvrDeleted,0) = 0 ");

            for (int i = 0; i < Days; i++)
            {
                Date = SDate.AddDays(i);
                
                DataTable DT = DBCs.FetchData(Q.ToString(), new string[] { EmpID, Date.ToString() });
                if (!DBCs.IsNullOrEmpty(DT)) { return true; }
            }
            return false;
        }
        catch (Exception ex) { return true; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindNestingDates2(string DateType, string StartDate, string EndDate, string EmpID, out string VacType)
    {
        VacType = "";

        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime SDate = DTCs.ConvertToDatetime(StartDate, DateType); 
            DateTime EDate = DTCs.ConvertToDatetime(EndDate, DateType); 

            //'R' + 
            StringBuilder Q = new StringBuilder();
            Q.Append(" SELECT EmpID, RetID, ErqStartDate, ErqEndDate FROM EmpRequest WHERE ErqReqStatus IN (0) AND EmpID = @P1 AND RetID IN ('VAC','COM','JOB') AND @P3 >= ErqStartDate AND ErqEndDate >= @P2 ");
            Q.Append(" UNION ALL ");
            Q.Append(" SELECT EmpID, 'VAC' AS RetID, EvrStartDate, EvrEndDate FROM EmpVacRel WHERE EmpID = @P1 AND ISNULL(EvrDeleted,0) = 0 AND @P3 >= EvrStartDate AND EvrEndDate >= @P2 AND VtpID IN (SELECT VtpID FROM VacationType WHERE VtpCategory IN ('VAC')) ");
            Q.Append(" UNION ALL ");
            Q.Append(" SELECT EmpID, 'LIC' AS RetID, EvrStartDate, EvrEndDate FROM EmpVacRel WHERE EmpID = @P1 AND ISNULL(EvrDeleted,0) = 0 AND @P3 >= EvrStartDate AND EvrEndDate >= @P2 AND VtpID IN (SELECT VtpID FROM VacationType WHERE VtpCategory IN ('LIC')) ");
            Q.Append(" UNION ALL ");
            Q.Append(" SELECT EmpID, 'COM' AS RetID, EvrStartDate, EvrEndDate FROM EmpVacRel WHERE EmpID = @P1 AND ISNULL(EvrDeleted,0) = 0 AND @P3 >= EvrStartDate AND EvrEndDate >= @P2 AND VtpID IN (SELECT VtpID FROM VacationType WHERE VtpCategory IN ('COM', 'TRA')) ");
            Q.Append(" UNION ALL ");
            Q.Append(" SELECT EmpID, 'JOB' AS RetID, EvrStartDate, EvrEndDate FROM EmpVacRel WHERE EmpID = @P1 AND ISNULL(EvrDeleted,0) = 0 AND @P3 >= EvrStartDate AND EvrEndDate >= @P2 AND VtpID IN (SELECT VtpID FROM VacationType WHERE VtpCategory IN ('JOB')) ");

            DataTable DT = DBCs.FetchData(Q.ToString(), new string[] { EmpID, SDate.ToString(), EDate.ToString() });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                VacType = Convert.ToString(DT.Rows[0]["RetID"]);
                return true;
            }

            return false;
        }
        catch (Exception ex) { return true; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getMsgVac(string VacType)
    {
        string Type = "";

        if      (VacType == "VAC") { Type = General.Msg("Vacation", "إجازة"); }
        else if (VacType == "LIC") { Type = General.Msg("license" ,"رخصة"); }
        else if (VacType == "COM") { Type = General.Msg("Commission" ,"انتداب"); }
        else if (VacType == "JOB") { Type = General.Msg("Job Assignment" ,"مهمة عمل"); }

        return General.Msg("There are other " + Type + " on the date specified Please choose another date", "يوجد " + Type + " أخرى في التاريخ المحدد الرجاء اختيار تاريخ آخر");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}