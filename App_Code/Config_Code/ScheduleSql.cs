using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class ScheduleSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ParameterDirection IN  = ParameterDirection.Input;
    ParameterDirection OU  = ParameterDirection.Output;
    DataRowVersion     DRV = DataRowVersion.Proposed;

    SqlDbType IntDB = SqlDbType.Int;
    SqlDbType VchDB = SqlDbType.VarChar;
    SqlDbType DtDB  = SqlDbType.DateTime;
    SqlDbType BitDB = SqlDbType.Bit;
    SqlDbType ChrDB = SqlDbType.Char;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int Insert(SchedulePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Schedule_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SchID"              , IntDB, 4,    OU, false, 0, 0, "", DRV, Pro.SchID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchNameAr"          , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchNameEn"          , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchIsActive"        , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchIsActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchType"            , VchDB, 20,   IN, false, 0, 0, "", DRV, Pro.SchType));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchDescription"     , VchDB, 255,  IN, false, 0, 0, "", DRV, Pro.SchDescription));
            if (!string.IsNullOrEmpty(Pro.SchStartDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@SchStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.SchStartDate)); }
            if (!string.IsNullOrEmpty(Pro.SchEndDate))   { Sqlcmd.Parameters.Add(new SqlParameter("@SchEndDate"  , DtDB, 14, IN, false, 0, 0, "", DRV, Pro.SchEndDate)); }
            Sqlcmd.Parameters.Add(new SqlParameter("@SchStartHour"       , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchStartHour));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchStartMin"        , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchStartMin));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEveryHour"       , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchEveryHour));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEveryMinute"     , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchEveryMinute));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchWeekOfMonth"     , ChrDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchWeekOfMonth));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchRepeatDays"      , IntDB, 4,    IN, false, 0, 0, "", DRV, Pro.SchRepeatDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchRepeatWeeks"     , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchRepeatWeeks));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReportID"           , VchDB, 10,   IN, false, 0, 0, "", DRV, Pro.ReportID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchUsers"           , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchUsers));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchReportFormat"    , ChrDB, 10,   IN, false, 0, 0, "", DRV, Pro.SchReportFormat));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEmailBodyContent", VchDB, 500,  IN, false, 0, 0, "", DRV, Pro.SchEmailBodyContent));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEmailSubject"    , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchEmailSubject));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchCalendar"        , VchDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchCalendar));

            Sqlcmd.Parameters.Add(new SqlParameter("@SchDays"            , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchMonths"          , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchMonths));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchWeekDays"        , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchWeekDays));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@SchID"].Value);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Update(SchedulePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Schedule_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SchID"              , IntDB, 4,    IN, false, 0, 0, "", DRV, Pro.SchID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchNameAr"          , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchNameEn"          , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchIsActive"        , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchIsActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchType"            , VchDB, 20,   IN, false, 0, 0, "", DRV, Pro.SchType));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchDescription"     , VchDB, 255,  IN, false, 0, 0, "", DRV, Pro.SchDescription));
            if (!string.IsNullOrEmpty(Pro.SchStartDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@SchStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.SchStartDate)); }
            if (!string.IsNullOrEmpty(Pro.SchEndDate))   { Sqlcmd.Parameters.Add(new SqlParameter("@SchEndDate"  , DtDB, 14, IN, false, 0, 0, "", DRV, Pro.SchEndDate)); }
            Sqlcmd.Parameters.Add(new SqlParameter("@SchStartHour"       , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchStartHour));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchStartMin"        , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchStartMin));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEveryHour"       , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchEveryHour));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEveryMinute"     , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchEveryMinute));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchWeekOfMonth"     , ChrDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchWeekOfMonth));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchRepeatDays"      , IntDB, 4,    IN, false, 0, 0, "", DRV, Pro.SchRepeatDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchRepeatWeeks"     , IntDB, 2,    IN, false, 0, 0, "", DRV, Pro.SchRepeatWeeks));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReportID"           , VchDB, 10,   IN, false, 0, 0, "", DRV, Pro.ReportID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchUsers"           , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchUsers));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchReportFormat"    , ChrDB, 10,   IN, false, 0, 0, "", DRV, Pro.SchReportFormat));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEmailBodyContent", VchDB, 500,  IN, false, 0, 0, "", DRV, Pro.SchEmailBodyContent));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchEmailSubject"    , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.SchEmailSubject));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchCalendar"        , VchDB, 1,    IN, false, 0, 0, "", DRV, Pro.SchCalendar));

            Sqlcmd.Parameters.Add(new SqlParameter("@SchDays"    , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchMonths"  , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchMonths));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchWeekDays", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SchWeekDays));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Schedule_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SchID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}