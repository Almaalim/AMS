using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class InspectionToursSql : DataLayerBase
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
    public int Insert(InspectionToursPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InspectionToursMaster_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmID",          IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.ItmID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmNameEn",      VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.ItmNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmNameAr",      VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.ItmNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmDate",        DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.ItmDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmTimeFrom",    DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.ItmTimeFrom));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmTimeTo",      DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.ItmTimeTo));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmMacType",     IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.ItmMacType));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmMacIDs",      VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.ItmMacIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmIsSend",      BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.ItmIsSend));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmIsProcess",   IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.ItmIsProcess));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmEmpIDs",      VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.ItmEmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmDescription", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.ItmDescription));

            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ItmID"].Value);
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
    public bool Update_IsSend(InspectionToursPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InspectionToursMaster_Update_IsSend]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmID", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ItmID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmIsSend", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.ItmIsSend));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
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
    public bool Update_IsProcess(InspectionToursPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InspectionToursMaster_Update_IsProcess]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmID", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ItmID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmIsProcess", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ItmIsProcess));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InspectionToursMaster_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ItmID", IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
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

