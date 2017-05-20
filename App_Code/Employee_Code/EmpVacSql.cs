using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public class EmpVacSql : DataLayerBase
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
    public bool Insert_WithUpdateSummary(EmpVacPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpVacRel_InsertWithUpdateSummary]",MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"         , txtDB, 1000000,  IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"          , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrStartDate"   , DtDB,  14,  IN, false, 0, 0, "", DRV, Pro.EvrStartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrEndDate"     , DtDB,  14,  IN, false, 0, 0, "", DRV, Pro.EvrEndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrDesc"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EvrDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrPhone"       , VchDB, 20,  IN, false, 0, 0, "", DRV, Pro.EvrPhone));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrAvailability", VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EvrAvailability));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrHospitalType", VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EvrHospitalType));

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
    public bool Delete_WithUpdateSummary(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpVacRel_DeleteWithUpdateSummary]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EvrID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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