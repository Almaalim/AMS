using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class DepartmentSql : DataLayerBase
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
    public int Insert(DepartmentPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Department_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"      , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepParentID", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcID"      , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.BrcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepNameAr"  , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.DepNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepNameEn"  , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.DepNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"    , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepDesc"    , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.DepDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepLevel"   , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepLevel));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepStatus"  , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.DepStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            int rows = Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@DepID"].Value); 
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
    public bool Update(DepartmentPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Department_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"      , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepParentID", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcID"      , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.BrcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepNameAr"  , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.DepNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepNameEn"  , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.DepNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"    , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepDesc"    , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.DepDesc));
            //Sqlcmd.Parameters.Add(new SqlParameter("@DepLevel"   , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepLevel));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepStatus"  , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.DepStatus));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Department_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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