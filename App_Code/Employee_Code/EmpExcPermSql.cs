using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class EmpExcPermSql : DataLayerBase
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
    public int Insert(EmpExcPermPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpExcPermRel_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprID"            , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.ExprID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprType"          , VchDB, 3,   IN, false, 0, 0, "", DRV, Pro.ExprType));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"             , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcID"             , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ExcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"             , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ShiftID"           , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ShiftID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprDesc"          , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ExprDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprVacIfNoPresent", BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.ExprVacIfNoPresent));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprStartDate"     , DtDB , 14,  IN, false, 0, 0, "", DRV, Pro.ExprStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExprEndDate"       , DtDB , 14,  IN, false, 0, 0, "", DRV, Pro.ExprEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ExcType"           , VchDB, 2,   IN, false, 0, 0, "", DRV, Pro.ExcType));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ExprID"].Value); 
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