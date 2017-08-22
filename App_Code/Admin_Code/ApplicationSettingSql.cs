using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class ApplicationSettingSql : DataLayerBase
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
    public bool InsertUpdate(ApplicationSettingPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ApplicationSetup_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@AppCompany" , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppCompany));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppDisplay" , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppDisplay));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppCountry" , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppCountry));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppCity"    , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppCity));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppAddress1", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.AppAddress1));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppAddress2", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.AppAddress2));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppTelNo1"  , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppTelNo1));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppTelNo2"  , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppTelNo2));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppPOBox"   , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.AppPOBox));

            Sqlcmd.Parameters.Add(new SqlParameter("@AppCalendar", VchDB, 1, IN, false, 0, 0, "", DRV, Pro.AppCalendar));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppDateFormat", VchDB, 20, IN, false, 0, 0, "", DRV, Pro.AppDateFormat));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppDataLangRequired", ChrDB, 1,  IN, false, 0, 0, "", DRV, Pro.AppDataLangRequired));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppMiniLogger_VerificationMethod", VchDB, 2,  IN, false, 0, 0, "", DRV, Pro.AppMiniLogger_VerificationMethod));
            Sqlcmd.Parameters.Add(new SqlParameter("@AppSessionDuration", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.AppSessionDuration));

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