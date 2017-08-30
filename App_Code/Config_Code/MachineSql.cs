using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class MachineSql : DataLayerBase
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
    public int Insert(MachinesPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Machine_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"        , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.MacID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MtpID"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacNo"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacNo));            
            Sqlcmd.Parameters.Add(new SqlParameter("@MacLocationAr", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.MacLocationAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacLocationEn", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.MacLocationEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacIP"        , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.MacIP));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacPort"      , VchDB, 5,  IN, false, 0, 0, "", DRV, Pro.MacPort));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacTransactionType", VchDB, 5,  IN, false, 0, 0, "", DRV, Pro.MacTransactionType));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacInstallDate", DtDB,  20, IN, false, 0, 0, "", DRV, Pro.MacInstallDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacInOutType"  , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacInOutType));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacStatus"     , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.MacStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@MacID"].Value); 
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
    public bool Update(MachinesPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Machine_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MtpID"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacNo"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacNo));            
            Sqlcmd.Parameters.Add(new SqlParameter("@MacLocationAr", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.MacLocationAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacLocationEn", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.MacLocationEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacIP"        , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.MacIP));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacPort"      , VchDB, 5,  IN, false, 0, 0, "", DRV, Pro.MacPort));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacTransactionType", VchDB, 5, IN, false, 0, 0, "", DRV, Pro.MacTransactionType));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacInstallDate", DtDB,  20, IN, false, 0, 0, "", DRV, Pro.MacInstallDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacInOutType"  , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacInOutType));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacStatus"     , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.MacStatus));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Machine_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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