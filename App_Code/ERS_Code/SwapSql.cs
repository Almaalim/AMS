using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class SwapSql : DataLayerBase
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
    public int Insert(SwapPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmployeeSwap_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpID"        , IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.SwpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpType"      , IntDB, 1,    IN, false, 0, 0, "", DRV, Pro.SwpType));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpEmpID1"    , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.SwpEmpID1));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpStartDate1", DtDB,  10,   IN, false, 0, 0, "", DRV, Pro.SwpStartDate1));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpEmpID2"    , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.SwpEmpID2));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpStartDate2", DtDB,  10,   IN, false, 0, 0, "", DRV, Pro.SwpStartDate2));          
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpDesc"      , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.SwpDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpAddBy"     , ChrDB, 3,    IN, false, 0, 0, "", DRV, Pro.SwpAddBy));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@SwpID"].Value); 
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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmployeeSwap_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SwpID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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