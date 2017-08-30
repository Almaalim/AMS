using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class ConfigSql : DataLayerBase
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
    public bool InsertUpdate(ConfigPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Configuration_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOrderTransType"       , VchDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOrderTransType));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgAutoIn"               , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgAutoIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgIsTakeAutoIn"         , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgIsTakeAutoIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgAutoOut"              , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgAutoOut));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgIsTakeAutoOut"        , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgIsTakeAutoOut));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgRedundantInSelection" , ChrDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgRedundantInSelection));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgRedundantOutSelection", ChrDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgRedundantOutSelection));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgICApprovalPercent"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgICApprovalPercent));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgMiddleGapCount"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgMiddleGapCount));

            Sqlcmd.Parameters.Add(new SqlParameter("@cfgMaxPercOT"           , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgMaxPercOT));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTBeginEarlyFlag"    , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgOTBeginEarlyFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTOutLateFlag"       , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgOTOutLateFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTOutOfShiftFlag"    , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgOTOutOfShiftFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTNoShiftFlag"       , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgOTNoShiftFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTInVacFlag"         , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.cfgOTInVacFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTBeginEarlyInterval", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOTBeginEarlyInterval));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTOutLateInterval"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOTOutLateInterval));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTOutOfShiftInterval", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOTOutOfShiftInterval));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTNoShiftInterval"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOTNoShiftInterval));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgOTInVacInterval"     , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.cfgOTInVacInterval));
            
            
            if (!string.IsNullOrEmpty(Pro.cfgVacResetDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@cfgVacResetDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.cfgVacResetDate)); }
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgFormReq"             , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.cfgFormReq));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgApprovalsMonthCount" , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.cfgApprovalsMonthCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgTransInDaysCount"    , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.cfgTransInDaysCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@cfgDaysLimitReqVac"     , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.cfgDaysLimitReqVac));
            Sqlcmd.Parameters.Add(new SqlParameter("@CfgPeriodDifferenceInInspectionTours", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.CfgPeriodDifferenceInInspectionTours));

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
}