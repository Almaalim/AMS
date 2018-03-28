using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class EmpRequestSql : DataLayerBase
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
    public int Insert(EmpRequestPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpRequest_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqID"           , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.ErqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RetID"           , ChrDB, 3,   IN, false, 0, 0, "", DRV, Pro.RetID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqTypeID"       , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ErqTypeID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VacHospitalType" , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.VacHospitalType));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"           , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@GapOvtID"        , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.GapOvtID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID2"          , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID2));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqEmp2ReqStatus", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ErqEmp2ReqStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqReason"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ErqReason));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqAvailability" , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ErqAvailability));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqPhone"        , VchDB, 20,  IN, false, 0, 0, "", DRV, Pro.ErqPhone));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqReqStatus"    , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ErqReqStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqReqFilePath"  , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.ErqReqFilePath));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"           , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ShiftID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.ShiftID));

            Sqlcmd.Parameters.Add(new SqlParameter("@ErqStatusTime", SqlDbType.Char, 1, IN, false, 0, 0, "", DRV, Pro.ErqStatusTime));

            if (!string.IsNullOrEmpty(Pro.ErqStartDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@ErqStartDate", DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqStartDate)); }
            if (!string.IsNullOrEmpty(Pro.ErqEndDate))   { Sqlcmd.Parameters.Add(new SqlParameter("@ErqEndDate"  , DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqEndDate)); }
            
            if (!string.IsNullOrEmpty(Pro.ErqStartDate2))
            {
                Sqlcmd.Parameters.Add(new SqlParameter("@ErqStartDate2", DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqStartDate2));
                Sqlcmd.Parameters.Add(new SqlParameter("@ErqEndDate2"  , DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqEndDate2));
            }

            if (!string.IsNullOrEmpty(Pro.ErqStartTime))
            {
                Sqlcmd.Parameters.Add(new SqlParameter("@ErqStartTime", DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqStartTime));
                Sqlcmd.Parameters.Add(new SqlParameter("@ErqEndTime"  , DtDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqEndTime));
            }

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            //Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@ErqID"].Value); 
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
    public bool Swap_EmpApproveRejected(EmpRequestPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpRequest_Swap_EmpApproveRejected]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqID"           , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RetID"           , ChrDB, 3,  IN, false, 0, 0, "", DRV, Pro.RetID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"           , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqEmp2ReqStatus", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ErqEmp2ReqStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));

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
    public bool isWorkDays(DateTime WorkDate, string EmpID) // return true = work, return false = no work, holiday, weekend
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CheckIsWorkDay]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@iDateDay", DtDB , 100, IN, false, 0, 0, "", DRV, WorkDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@iEmpID"  , VchDB, 15,  IN, false, 0, 0, "", DRV, EmpID));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@oWorkStatus", BitDB, 1,  OU, false, 0, 0, "", DRV, null));
            Sqlcmd.Parameters.Add(new SqlParameter("@oDayStatus" , VchDB, 2,  OU, false, 0, 0, "", DRV, null));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }

            return Convert.ToBoolean(Sqlcmd.Parameters["@oWorkStatus"].Value);
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
    public bool isWorkDuration(DateTime StartDate, DateTime EndDate, string EmpID, string Type) 
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CheckIsWorkDuration]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@iStartDate"   , DtDB , 100, IN, false, 0, 0, "", DRV, StartDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@iEndDate"     , DtDB , 100, IN, false, 0, 0, "", DRV, EndDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@iEmpID"       , VchDB, 15,  IN, false, 0, 0, "", DRV, EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@iDurationType", VchDB, 15,  IN, false, 0, 0, "", DRV, Type));

            Sqlcmd.Parameters.Add(new SqlParameter("@oWorkStatus", BitDB, 1,  OU, false, 0, 0, "", DRV, null));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"  , IntDB, 10, OU, false, 0, 0, "", DRV, 0));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }

            return Convert.ToBoolean(Sqlcmd.Parameters["@oWorkStatus"].Value);
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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpReq_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ErqID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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