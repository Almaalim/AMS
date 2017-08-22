using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class EmployeeSql : DataLayerBase
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
    public bool Insert(EmployeePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Employee_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"                , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpCardID"            , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpCardID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNationalID"        , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpNationalID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.CatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNameAr"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNameEn"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJoinDate"          , DtDB , 14,  IN, false, 0, 0, "", DRV, Pro.EmpJoinDate));
            if (!string.IsNullOrEmpty(Pro.EmpLeaveDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EmpLeaveDate", DtDB, 14, IN, false, 0, 0, "", DRV,Pro.EmpLeaveDate)); }
            Sqlcmd.Parameters.Add(new SqlParameter("@NatID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.NatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpStatus"            , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpPWD"               , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpPWD));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoOut"           , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpAutoOut));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoIn"            , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpAutoIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EtpID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJobTitleAr"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpJobTitleAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJobTitleEn"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpJobTitleEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpBlood"             , VchDB, 3,   IN, false, 0, 0, "", DRV, Pro.EmpBlood));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpGender"            , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpGender));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpSendEmailAlert"    , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpSendEmailAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpSendSMSAlert"      , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpSendSMSAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpDescription"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpDomainUserName"    , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.EmpDomainUserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpMaxOvertimePercent", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EmpMaxOvertimePercent));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpEmailID"           , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpEmailID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpMobileNo"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpMobileNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RlsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpLanguage"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpLanguage));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpADUser"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpADUser));

            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankCode"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpRankCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankTitel"         , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.EmpRankTitel));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankDesc"          , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.EmpRankDesc));

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
    public bool Update(EmployeePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Employee_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"                , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpCardID"            , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpCardID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNationalID"        , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpNationalID));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@CatID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.CatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNameAr"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpNameEn"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJoinDate"          , DtDB , 14,  IN, false, 0, 0, "", DRV, Pro.EmpJoinDate));
            if (!string.IsNullOrEmpty(Pro.EmpLeaveDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EmpLeaveDate", DtDB, 14, IN, false, 0, 0, "", DRV,Pro.EmpLeaveDate)); }
            Sqlcmd.Parameters.Add(new SqlParameter("@NatID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.NatID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpStatus"            , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpStatus));
            //Sqlcmd.Parameters.Add(new SqlParameter("@EmpPWD"               , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpPWD));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoOut"           , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpAutoOut));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoIn"            , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpAutoIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EtpID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJobTitleAr"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpJobTitleAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpJobTitleEn"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpJobTitleEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpBlood"             , VchDB, 3,   IN, false, 0, 0, "", DRV, Pro.EmpBlood));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpGender"            , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpGender));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpSendEmailAlert"    , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpSendEmailAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpSendSMSAlert"      , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmpSendSMSAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpDescription"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.EmpDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpDomainUserName"    , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.EmpDomainUserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpMaxOvertimePercent", IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EmpMaxOvertimePercent));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpEmailID"           , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpEmailID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpMobileNo"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpMobileNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"                , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RlsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpLanguage"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpLanguage));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpADUser"            , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpADUser));

            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankCode"          , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpRankCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankTitel"         , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.EmpRankTitel));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpRankDesc"          , VchDB, 500, IN, false, 0, 0, "", DRV, Pro.EmpRankDesc));

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
    public bool Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Employee_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"        , VchDB, 15, IN, false, 0, 0, "", DRV, ID));
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
    public bool Employee_Update_Password(string ID, string Pass, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Employee_Update_Password]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"        , VchDB, 15,  IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpPWD"       , VchDB, 100, IN, false, 0, 0, "", DRV, Pass));
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
    public bool Employee_Update_ADUser(EmployeePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Employee_Update_ADUser]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"        , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpADUser"    , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmpADUser));
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public bool Wizard_AutoInOut(EmployeePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Wizard_AutoInOut]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"       , txtDB, 1000000,  IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoIn"    , ChrDB, 1,        IN, false, 0, 0, "", DRV, Pro.EmpAutoInWizard));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpAutoOut"   , ChrDB, 1,        IN, false, 0, 0, "", DRV, Pro.EmpAutoOutWizard));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10,       OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15,       IN, false, 0, 0, "", DRV, Pro.TransactionBy));

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