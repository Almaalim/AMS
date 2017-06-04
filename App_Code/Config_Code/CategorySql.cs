using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class CategorySql : DataLayerBase
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
    public int Insert(CategoryPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Category_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CatID"    , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.CatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatNameAr", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.CatNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatNameEn", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.CatNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatDesc"  , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.CatDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@Catstatus", BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.CatStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@CatID"].Value); 
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
    public bool Update(CategoryPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Category_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CatID"    , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.CatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatNameAr", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.CatNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatNameEn", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.CatNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatDesc"  , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.CatDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@Catstatus", BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.CatStatus));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Category_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CatID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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