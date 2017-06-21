using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Elmah;

public class UsersSql : DataLayerBase
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
    #region AppUser

    public bool AppUser_Insert(UsersPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrPassword"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrPassword));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"         , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrFullName"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrFullName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDesc"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.UsrDesc));
            if (!string.IsNullOrEmpty(Pro.UsrStartDate))  { Sqlcmd.Parameters.Add(new SqlParameter("@UsrStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.UsrStartDate)); }
            if (!string.IsNullOrEmpty(Pro.UsrExpireDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@UsrExpireDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.UsrExpireDate)); }

            Sqlcmd.Parameters.Add(new SqlParameter("@UsrStatus"         , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrLanguage"       , ChrDB, 2,    IN, false, 0, 0, "", DRV, Pro.UsrLanguage));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpID"             , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.UsrGrpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReportGrpID"       , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.UsrReportGrpID));
            //Sqlcmd.Parameters.Add(new SqlParameter("@UsrDepartments"    , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.UsrDepartments));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDomainName"     , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.UsrDomainName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrMobileNo"       , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrMobileNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrEMailID"        , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrEMailID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrSendEmailAlert" , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrSendEmailAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrSendSMSAlert"   , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrSendSMSAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActingUser"     , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrActingUser));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrAdminMiniLogger", BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrAdminMiniLogger));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrADUser"         , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrADUser));
            
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
    public bool AppUser_Update(UsersPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrPassword"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrPassword));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"         , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrFullName"   , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrFullName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDesc"       , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.UsrDesc));
            if (!string.IsNullOrEmpty(Pro.UsrStartDate))  { Sqlcmd.Parameters.Add(new SqlParameter("@UsrStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.UsrStartDate)); }
            if (!string.IsNullOrEmpty(Pro.UsrExpireDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@UsrExpireDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.UsrExpireDate)); }

            Sqlcmd.Parameters.Add(new SqlParameter("@UsrStatus"         , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrStatus));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrLanguage"       , ChrDB, 2,    IN, false, 0, 0, "", DRV, Pro.UsrLanguage));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpID"             , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.UsrGrpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ReportGrpID"       , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.UsrReportGrpID));
            //Sqlcmd.Parameters.Add(new SqlParameter("@UsrDepartments"    , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.UsrDepartments));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDomainName"     , VchDB, 50,   IN, false, 0, 0, "", DRV, Pro.UsrDomainName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrMobileNo"       , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrMobileNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrEMailID"        , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrEMailID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrSendEmailAlert" , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrSendEmailAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrSendSMSAlert"   , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrSendSMSAlert));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActingUser"     , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrActingUser));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrAdminMiniLogger", BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.UsrAdminMiniLogger));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrADUser"         , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.UsrADUser));
            
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
    public bool AppUser_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 15, IN, false, 0, 0, "", DRV, ID));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"     , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy" , VchDB, 15, IN, false, 0, 0, "", DRV, TransactionBy));

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
    public bool AppUser_Update_DomainName(string ID, string DomainName, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update_DomainName]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDomainName" , VchDB, 50, IN, false, 0, 0, "", DRV, DomainName));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"     , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy" , VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

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
    //public DataTable SelectByPrimaryKey(string UsrName)
    //{
    //    SqlCommand sqlCommand = new SqlCommand();
    //    try
    //    {
    //        dt = new DataTable();


    //        sqlCommand.CommandText = "AMSAppUser_SelectByPrimaryKey";
    //        sqlCommand.CommandType = CommandType.StoredProcedure;
    //        sqlCommand.Connection = MainConnection;

    //        SqlDataReader dr;

    //        param = new SqlParameter();
    //        param.ParameterName = "@UsrName";
    //        param.SqlDbType = SqlDbType.Char;
    //        param.Value = UsrName;
    //        param.Size = 10;
    //        param.Direction = IN;
    //        sqlCommand.Parameters.Add(param);

    //        MainConnection.Open();

    //        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);


    //        dt.Load(dr);
    //        dr.Close();

    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("AMSAppUser::SelectByPrimaryKey::Error occured.", ex);
    //    }
    //    finally
    //    {
    //        MainConnection.Close();
    //        sqlCommand.Dispose();

    //    }

    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool AppUser_Update_AD(string ID,string ADUser, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update_AD]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"      , VchDB, 15,  IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrADUser"    , VchDB, 100, IN, false, 0, 0, "", DRV, ADUser));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15,  IN, false, 0, 0, "", DRV, TransactionBy));

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
    public bool AppUser_Update_Pass(string UsrName,string Pass)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update_Pass]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"    , VchDB, 15,  IN, false, 0, 0, "", DRV, UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrPassword", VchDB, 100, IN, false, 0, 0, "", DRV, Pass));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"  , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
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
    public bool AppUser_Update_Acting(UsersPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update_Acting]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActingUser" , BitDB, 1 ,  IN, false, 0, 0, "", DRV, Pro.UsrActingUser));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActUserName", VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrActUserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActPwd"     , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrActPwd));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActEmpID"   , VchDB, 15,  IN, false, 0, 0, "", DRV, Pro.UsrActEmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrADActUser"  , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrADActUser));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrActEMailID" , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.UsrActEMailID)); 
            
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
    public bool AppUser_Update_DepList(string UsrName, string DepList, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[AppUser_Update_DepList]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"       , VchDB, 15,   IN, false, 0, 0, "", DRV, UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrDepartments", VchDB, 8000, IN, false, 0, 0, "", DRV, DepList));

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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region PermissionGroup

    public int PermissionGroup_Insert(UsersPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[PermissionGroup_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpID"         , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.GrpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpNameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpNameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpDescription", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpPermissions", VchDB, 2147483647, IN, false, 0, 0, "", DRV, Pro.GrpPermissions));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpType"       , VchDB, 10, IN, false, 0, 0, "", DRV, Pro.GrpType));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }  
            return Convert.ToInt32(Sqlcmd.Parameters["@GrpID"].Value); 
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
    public bool PermissionGroup_Update(UsersPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[PermissionGroup_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpID"         , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.GrpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpNameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpNameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpDescription", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.GrpDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpPermissions", VchDB, 2147483647, IN, false, 0, 0, "", DRV, Pro.GrpPermissions));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpType"       , VchDB, 10, IN, false, 0, 0, "", DRV, Pro.GrpType));

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
    public bool PermissionGroup_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[PermissionGroup_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
  
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region InOutLogSql

    public bool InOutLog_Insert(string ID, string HostName, string HostIP)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InOutLog_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName" , VchDB, 15,  IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@HostName", VchDB, 100, IN, false, 0, 0, "", DRV, HostName));
            Sqlcmd.Parameters.Add(new SqlParameter("@HostIP"  , VchDB, 100, IN, false, 0, 0, "", DRV, HostIP));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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
    public bool InOutLog_Update(string ID)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[InOutLog_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName", VchDB, 15,  IN, false, 0, 0, "", DRV, ID));
           
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region UserDepartmentRel

    public bool UsrDepRel_Insert(string UserName, string DepID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[UsrDepRel_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"      , VchDB, 10, IN, false, 0, 0, "", DRV, UserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"        , IntDB, 10, IN, false, 0, 0, "", DRV, DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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
    public bool UsrDepRel_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[UsrDepRel_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UdrID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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
    public bool UsrDepRel_Insert_ByMgr(string UserName, string DepID, string TransactionBy, string activeMgr)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[UsrDepRel_InsertByMgr]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UdrID"       , IntDB, 10, OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"     , VchDB, 10, IN, false, 0, 0, "", DRV, UserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"       , IntDB, 4,  IN, false, 0, 0, "", DRV, DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UdrActiveMgr", VchDB, 10, IN, false, 0, 0, "", DRV, activeMgr));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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
    public bool UsrDepRel_Update_ByMgr(string UdrID, string UserName, string DepID, string TransactionBy, string activeMgr)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[UsrDepRel_UpdateByMgr]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@UdrID"       , IntDB, 10, IN, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"     , VchDB, 10, IN, false, 0, 0, "", DRV, UserName));
            Sqlcmd.Parameters.Add(new SqlParameter("@DepID"       , IntDB, 4,  IN, false, 0, 0, "", DRV, DepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UdrActiveMgr", VchDB, 10, IN, false, 0, 0, "", DRV, activeMgr));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
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

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
