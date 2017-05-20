using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class HolidaySql : DataLayerBase
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
    public int Insert(HolidayPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Holiday_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@HolID"       , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.HolID));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolNameAr"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.HolNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolNameEn"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.HolNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolStartDate", DtDB,  100, IN, false, 0, 0, "", DRV, Pro.HolStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolEndDate"  , DtDB,  100, IN, false, 0, 0, "", DRV, Pro.HolEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolDesc"     , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.HolDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@Holstatus"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.HolStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@HolID"].Value); 
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
    public bool Update(HolidayPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Holiday_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@HolID"       , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.HolID));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolNameAr"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.HolNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolNameEn"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.HolNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolStartDate", DtDB,  100, IN, false, 0, 0, "", DRV, Pro.HolStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolEndDate"  , DtDB,  100, IN, false, 0, 0, "", DRV, Pro.HolEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@HolDesc"     , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.HolDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@Holstatus"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.HolStatus));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Holiday_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@HolID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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