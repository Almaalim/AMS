using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class FavoriteFormsSql : DataLayerBase
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
    public int Insert(FavoriteFormsPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[FavoriteForms_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@FavID"     , IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.FavID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavFormID" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.FavFormID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavNameEn" , VchDB, 2000, IN, false, 0, 0, "", DRV, Pro.FavNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavNameAr" , VchDB, 2000, IN, false, 0, 0, "", DRV, Pro.FavNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavUsrName", VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.FavUsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavType"   , ChrDB, 1,    IN, false, 0, 0, "", DRV, Pro.FavType));
            Sqlcmd.Parameters.Add(new SqlParameter("@FParPanel" , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.FParPanel));
            Sqlcmd.Parameters.Add(new SqlParameter("@FParValue" , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.FParValue));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@FavID"].Value); 
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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[FavoriteForms_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@FavID"     , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute" , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@FavUsrName", VchDB, 15, IN, false, 0, 0, "", DRV, TransactionBy));

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