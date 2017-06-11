using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class EmpExcRelSql : DataLayerBase
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
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Period_Insert_WithUpdateSummary(EmpExcRelPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpExcRel_Period_InsertWithUpdateSummary]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"        , txtDB, 1000000,  IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ExcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrStartDate"  , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrEndDate"    , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrStartTime"  , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrStartTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrEndTime"    , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrEndTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrDesc"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ExrDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrISOvernight", BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExrISOvernight));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrIsOverTime" , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExrIsOverTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrIsStopped"  , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExrIsOverTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrAddBy"      , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExrAddBy));

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
    public bool Period_Delete_WithUpdateSummary(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpExcRel_Period_DeleteWithUpdateSummary]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int Day_Insert_WithUpdateSummary(EmpExcRelPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpExcRel_InsertWithUpdateSummary]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrID"         , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.ExrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ExcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"         , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrStartDate"  , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrEndDate"    , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrStartTime"  , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrStartTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrEndTime"    , DtDB,  20,  IN, false, 0, 0, "", DRV, Pro.ExrEndTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrDesc"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ExrDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrAddBy"      , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExrAddBy));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ExrID"].Value); 
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
    public bool Day_Delete_WithUpdateSummary(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpExcRel_DeleteWithUpdateSummary]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExrID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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