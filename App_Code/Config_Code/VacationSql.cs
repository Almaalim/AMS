using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class VacationSql : DataLayerBase
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int VacType_Insert(VacationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[VacationType_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"             , IntDB, 10,  OU, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpNameAr"         , VchDB, 100, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpNameEn"         , VchDB, 100, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpInitialEn"      , ChrDB, 2,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpInitialEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpInitialAr"      , ChrDB, 2,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpInitialAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpStatus"         , BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpDesc"           , VchDB, 255, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpMaxDays"        , IntDB, 3,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpMaxDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsReset"        , BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsReset));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsPaid"         , IntDB, 10,  IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsPaid));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsMedicalReport", BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsMedicalReport));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpCategory"       , VchDB, 50,  IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpCategory));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@VtpID"].Value); 
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
    public bool VacType_Update(VacationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[VacationType_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"             , IntDB, 10,  IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpNameAr"         , VchDB, 100, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpNameEn"         , VchDB, 100, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpInitialEn"      , ChrDB, 2,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpInitialEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpInitialAr"      , ChrDB, 2,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpInitialAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpStatus"         , BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpDesc"           , VchDB, 255, IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpMaxDays"        , IntDB, 3,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpMaxDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsReset"        , BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsReset));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsPaid"         , IntDB, 10,  IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsPaid));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpIsMedicalReport", BitDB, 1,   IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpIsMedicalReport));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpCategory"       , VchDB, 50,  IN, false, 0, 0, "", DataRowVersion.Proposed, Pro.VtpCategory));

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
    public bool VacType_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[VacationType_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    public int EmpVacSetting_Insert(VacationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpVacSetting_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EvsID"     , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.EvsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"     , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"     , IntDB, 100, IN, false, 0, 0, "", DRV, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvsMaxDays", IntDB, 100, IN, false, 0, 0, "", DRV, Pro.EvsMaxDays));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@EvsID"].Value); 
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
    public bool EmpVacSetting_Update(VacationPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpVacSetting_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EvsID"     , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EvsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"     , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VtpID"     , IntDB, 100, IN, false, 0, 0, "", DRV, Pro.VtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EvsMaxDays", IntDB, 100, IN, false, 0, 0, "", DRV, Pro.EvsMaxDays));

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
    public bool EmpVacSetting_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpVacSetting_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EvsID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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