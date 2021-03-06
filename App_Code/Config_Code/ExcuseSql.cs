﻿using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class ExcuseSql : DataLayerBase
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
    public int Insert(ExcusePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Excuse_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"       , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.ExcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcNameAr"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.ExcNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcNameEn"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.ExcNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcInitialAr", ChrDB, 2,   IN, false, 0, 0, "", DRV, Pro.ExcInitialAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcInitialEn", ChrDB, 2,   IN, false, 0, 0, "", DRV, Pro.ExcInitialEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcIsPaid"   , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExcIsPaid));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcDesc"     , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ExcDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcStatus"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExcStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcMaxHoursPerMonth"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ExcMaxHoursPerMonth));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcPercentageAllowable", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ExcPercentageAllowable));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ExcID"].Value); 
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
    public bool Update(ExcusePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Excuse_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"       , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ExcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcNameAr"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.ExcNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcNameEn"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.ExcNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcInitialAr", ChrDB, 2,   IN, false, 0, 0, "", DRV, Pro.ExcInitialAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcInitialEn", ChrDB, 2,   IN, false, 0, 0, "", DRV, Pro.ExcInitialEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcIsPaid"   , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExcIsPaid));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcDesc"     , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ExcDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcStatus"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExcStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcMaxHoursPerMonth"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ExcMaxHoursPerMonth));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcPercentageAllowable", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ExcPercentageAllowable));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Excuse_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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