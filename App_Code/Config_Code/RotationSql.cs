﻿using System;
using System.Data;
using System.Data.SqlClient;

public class RotationSql : DataLayerBase
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int SANS_RotationWorkTime_Insert(RotationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[SANS_RotationWorkTime_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@oRwtID"               , IntDB, 10  , OU, false, 0, 0, "", DRV, Pro.RwtID));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtNameAr"           , VchDB, 500 , IN, false, 0, 0, "", DRV, Pro.RwtNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtNameEn"           , VchDB, 500 , IN, false, 0, 0, "", DRV, Pro.RwtNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtFromDate"         , DtDB , 10  , IN, false, 0, 0, "", DRV, Pro.RwtFromDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtToDate"           , DtDB , 10  , IN, false, 0, 0, "", DRV, Pro.RwtToDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtWorkDaysCount"    , IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.RwtWorkDaysCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtNotWorkDaysCount" , IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.RwtNotWorkDaysCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtRotationDaysCount", IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.RwtRotationDaysCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtIsActive"         , BitDB, 1   , IN, false, 0, 0, "", DRV, Pro.RwtIsActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtType"             , VchDB, 100 , IN, false, 0, 0, "", DRV, Pro.RwtType));
            Sqlcmd.Parameters.Add(new SqlParameter("@iRwtDesc"             , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.RwtDesc));
        
            Sqlcmd.Parameters.Add(new SqlParameter("@iWktIDs"              , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.WktIDs));        
            Sqlcmd.Parameters.Add(new SqlParameter("@iGrpIDs"              , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.GrpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@iGrpUsers"            , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.GrpUsers));
            Sqlcmd.Parameters.Add(new SqlParameter("@iEmpIDs"              , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.EmpIDs));

            Sqlcmd.Parameters.Add(new SqlParameter("@oIsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@iTransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            int rows = Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@oIsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@oRwtID"].Value); 
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
    public bool SANS_RotationWorkTime_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[SANS_RotationWorkTime_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RwtID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

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
    public bool SANS_RotationWorkTime_AddEmployees(RotationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[SANS_RotationWorkTime_AddEmployees]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RwtID"               , IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.RwtID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RwtAddStartDate"     , DtDB , 10  , IN, false, 0, 0, "", DRV, Pro.RwtAddStartDate));      
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpIDs"              , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.GrpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"              , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.EmpIDs));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            int rows = Sqlcmd.ExecuteNonQuery();
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
    public bool SANS_RotationWorkTime_DeleteEmployees(RotationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[SANS_RotationWorkTime_DeleteEmployees]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RwtID"          , IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.RwtID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RwtAddStartDate", DtDB , 10  , IN, false, 0, 0, "", DRV, Pro.RwtAddStartDate));      
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"         , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.EmpIDs));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            int rows = Sqlcmd.ExecuteNonQuery();
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}