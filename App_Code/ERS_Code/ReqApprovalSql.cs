using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class ReqApprovalSql : DataLayerBase
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
    public int Insert(ReqApprovalPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ReqApproval_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EraID"             , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.EraID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EraLevelSequenceNo", IntDB, 100, IN, false, 0, 0, "", DRV, Pro.EraLevelSequenceNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@MgrID"             , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.MgrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EraLevelCount"     , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EraLevelCount));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"             , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RetID"             , ChrDB, 3,   IN, false, 0, 0, "", DRV, Pro.RetID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EraStatus"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EraStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReqID"             , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ReqID));
            //Sqlcmd.Parameters.Add(new SqlParameter("@EraApprovalDate" , DtDB,  14,  IN, false, 0, 0, "", DRV, Pro.EraApprovalDate));
            //Sqlcmd.Parameters.Add(new SqlParameter("@EraComment"      , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EraComment));    
            //Sqlcmd.Parameters.Add(new SqlParameter("@EraManagerAlertMessageCount", IntDB, 10, IN, false, 0, 0, "",DRV, Pro.EraManagerAlertMessageCount));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@EraID"].Value); 
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
    public bool Update(ReqApprovalPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ReqApproval_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RetID"    , ChrDB, 3,  IN, false, 0, 0, "", DRV, Pro.RetID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReqID"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ReqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MgrID"    , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.MgrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EraStatus", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.EraStatus));
            
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
    public bool Delete(string ErqID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ReqApproval_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ReqID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ErqID));
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
    //public bool Update_ReqStatus(ReqApprovalPro Pro)
    //{
    //    SqlCommand Sqlcmd = new SqlCommand("dbo.[ReqApproval_UpdateReqStatus]", MainConnection);
    //    Sqlcmd.CommandType = CommandType.StoredProcedure;

    //    try
    //    {
    //        Sqlcmd.Parameters.Add(new SqlParameter("@RetID"        , ChrDB, 3,  IN, false, 0, 0, "", DRV, Pro.RetID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@ErqID"        , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ReqID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@EraStatus"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.EraStatus));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@GapOvtID"     , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.GapOvtID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@ErqTypeID"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqTypeID));
            
    //        Sqlcmd.Parameters.Add(new SqlParameter("@ErqStartDate" , DtDB,  20, IN, false, 0, 0, "", DRV, Pro.ErqStartDate));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"        , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@ShiftID"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ShiftID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@ErqStatusTime", VchDB, 1,  IN, false, 0, 0, "", DRV, Pro.ErqStatusTime));

    //        Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
    //        MainConnection.Open();
    //        Sqlcmd.ExecuteNonQuery();
    //        if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message, ex);
    //    }
    //    finally
    //    {
    //        MainConnection.Close();
    //        Sqlcmd.Dispose();
    //    }
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool ApprovalOrRejected(ReqApprovalPro Pro)
    {
        /* Status = -1 > Error */
        /* Status = 0 > Wait, the level is NOT last & Approve Request */
        /* Status = 1 > Approve Request */
        /* Status = 2 > Rejected Request */

        SqlCommand Sqlcmd = new SqlCommand("dbo.[Request_ApprovalOrRejected]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@Status"   , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@NextLevel", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqID"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ReqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RetID"    , ChrDB, 3 , IN, false, 0, 0, "", DRV, Pro.RetID));            
            Sqlcmd.Parameters.Add(new SqlParameter("@MgrID"    , VchDB, 10, IN, false, 0, 0, "", DRV, Pro.MgrID));        
            Sqlcmd.Parameters.Add(new SqlParameter("@EraStatus", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.EraStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
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
    public bool ApprovalOrRejected_Multi(ReqApprovalPro Pro, out string SaveIDs, out string NotSaveIDs)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Request_ApprovalOrRejected_Multi]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqIDs"   , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.ErqIDs));           
            Sqlcmd.Parameters.Add(new SqlParameter("@MgrID"    , VchDB, 500,  IN, false, 0, 0, "", DRV, Pro.MgrID));        
            Sqlcmd.Parameters.Add(new SqlParameter("@EraStatus", IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.EraStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@SaveIDs"   , VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));
            Sqlcmd.Parameters.Add(new SqlParameter("@NotSaveIDs", VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            SaveIDs    = Sqlcmd.Parameters["@SaveIDs"].Value.ToString();
            NotSaveIDs = Sqlcmd.Parameters["@NotSaveIDs"].Value.ToString();
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