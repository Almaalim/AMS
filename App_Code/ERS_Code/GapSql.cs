using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class GapSql : DataLayerBase
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
    public int Insert(GapPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Gaps_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@GapID"       , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.GapID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"       , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"       , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EwrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapDate"     , DtDB,  12,  IN, false, 0, 0, "", DRV, Pro.GapDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapShift"    , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.GapShift));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapStartTime", DtDB,  12,  IN, false, 0, 0, "", DRV, Pro.GapStartTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapEndTime"  , DtDB,  12,  IN, false, 0, 0, "", DRV, Pro.GapEndTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapAlert"    , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.GapAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"     , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapGraceFlag", ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.GapGraceFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapDesc"     , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.GapDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapType"     , ChrDB, 2,   IN, false, 0, 0, "", DRV, Pro.GapType));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@GapID"].Value); 
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
    public bool Update(GapPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Gaps_Update_Time]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@GapID"       , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.GapID));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapStartTime", DtDB,  12, IN, false, 0, 0, "", DRV, Pro.GapStartTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapEndTime"  , DtDB,  12, IN, false, 0, 0, "", DRV, Pro.GapEndTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"     , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.UsrName));
           
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