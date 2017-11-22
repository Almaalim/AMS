using System;
using System.Data;
using System.Data.SqlClient;

public class ImportLogSql : DataLayerBase
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
    SqlDbType TxtDB = SqlDbType.Text;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int Import_Log_Insert(ImportLogPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ImportLog_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@IplID"             , IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.IplID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IplType"           , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.IplType));
            Sqlcmd.Parameters.Add(new SqlParameter("@IplRunTodayProcess", IntDB, 2000, IN, false, 0, 0, "", DRV, Pro.IplRunTodayProcess));
            Sqlcmd.Parameters.Add(new SqlParameter("@IplRunProcess"     , IntDB, 2000, IN, false, 0, 0, "", DRV, Pro.IplRunProcess));


            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@IplID"].Value); 
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
    public bool Import_Log_Update_EndDate(string ID)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ImportLog_Update_EndDate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@IplID"     , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute" , IntDB, 10, OU, false, 0, 0, "", DRV, 0));

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
    public int Import_MachineLog_Insert(ImportLogPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ImportMachineLog_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            int len = (!string.IsNullOrEmpty(Pro.ImlErrMsg)) ? Pro.ImlErrMsg.Length : 0;
            Sqlcmd.Parameters.Add(new SqlParameter("@ImlID"        , IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.ImlID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IplID"        , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.IplID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"        , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.MacID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ImlStartDT"   , DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.ImlStartDT));
            Sqlcmd.Parameters.Add(new SqlParameter("@ImlIsImport"  , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.ImlIsImport));
            Sqlcmd.Parameters.Add(new SqlParameter("@ImlTransCount", IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.ImlTransCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@ImlErrMsg"    , TxtDB, len, IN, false, 0, 0, "", DRV, Pro.ImlErrMsg));


            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ImlID"].Value); 
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