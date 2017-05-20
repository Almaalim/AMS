using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class TransDumpSql : DataLayerBase
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
    SqlDbType txtDB = SqlDbType.Text;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(TransDumpPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[TransDump_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnDate", DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.TrnDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnTime", DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.TrnTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"  , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"  , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.MacID));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnType", ChrDB, 1,    IN, false, 0, 0, "", DRV, Pro.TrnType));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName", VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnDesc", VchDB, 2000, IN, false, 0, 0, "", DRV, Pro.TrnDesc));

            Sqlcmd.Parameters.Add(new SqlParameter("@TrnIgnore"   , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.TrnIgnore));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnAddBy"    , VchDB, 3,    IN, false, 0, 0, "", DRV, Pro.TrnAddBy));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnIgnoredBy", VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.TrnIgnoredBy));

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
    public bool Wizard_Trans_WithAttachment(TransDumpPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Wizard_Trans_WithAttachment]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"  , txtDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpIDs));

            Sqlcmd.Parameters.Add(new SqlParameter("@TrnDate", DtDB,  20, IN, false, 0, 0, "", DRV, Pro.TrnDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnTime", DtDB,  20, IN, false, 0, 0, "", DRV, Pro.TrnTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@MacID"  , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.MacID));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnType", ChrDB, 1,  IN, false, 0, 0, "", DRV, Pro.TrnType));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.UsrName));

            Sqlcmd.Parameters.Add(new SqlParameter("@TrnDesc"     , VchDB, 2000, IN, false, 0, 0, "", DRV, Pro.TrnDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnIgnore"   , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.TrnIgnore));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnAddBy"    , VchDB, 3,    IN, false, 0, 0, "", DRV, Pro.TrnAddBy));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnIgnoredBy", VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.TrnIgnoredBy));

            Sqlcmd.Parameters.Add(new SqlParameter("@TrnFilePath", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.TrnFilePath));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnReason"  , VchDB, 2000, IN, false, 0, 0, "", DRV, Pro.TrnReason));

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

