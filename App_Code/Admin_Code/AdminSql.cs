using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class AdminSql : DataLayerBase
{
    //ProCs.TransactionBy = pgCs.LoginID;
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
    public bool ADConfig_InsertUpdate(AdminPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ADConfig_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@ADEnabled"    , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.ADEnabled));
            Sqlcmd.Parameters.Add(new SqlParameter("@ADJoinMethod" , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.ADJoinMethod));
            Sqlcmd.Parameters.Add(new SqlParameter("@AMSUsrColName", VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.AMSUsrColName));
            Sqlcmd.Parameters.Add(new SqlParameter("@AMSEmpColName", VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.AMSEmpColName));
            Sqlcmd.Parameters.Add(new SqlParameter("@ADUsrColName" , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.ADUsrColName));
            Sqlcmd.Parameters.Add(new SqlParameter("@ADDomainName" , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.ADDomainName));
            Sqlcmd.Parameters.Add(new SqlParameter("@ADColName"    , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.ADColName));

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
    public bool EmailConfig_InsertUpdate(AdminPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmailConfig_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlServerID"      , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmlServerID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlPortNo"        , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.EmlPortNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlSenderEmail"   , VchDB, 200, IN, false, 0, 0, "", DRV, Pro.EmlSenderEmail));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlSenderName"    , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.EmlSenderName));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlSenderPassword", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.EmlSenderPassword));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlSsl"           , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmlSsl));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmlCredential"    , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.EmlCredential));

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
    public bool RequestType_Update_Status(AdminPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RequestType_Update_Status]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RetIDs"   , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.RetIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@StatusIDs", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.StatusIDs));

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
    public bool SMSConfig_InsertUpdate(AdminPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[SMSConfig_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SmsGateway" , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SmsGateway));
            Sqlcmd.Parameters.Add(new SqlParameter("@SmsSenderID", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SmsSenderID));
            Sqlcmd.Parameters.Add(new SqlParameter("@SmsSenderNo", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SmsSenderNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@SmsUser"    , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SmsUser));
            Sqlcmd.Parameters.Add(new SqlParameter("@SmsPass"    , VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.SmsPass));

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
    public bool DepLevel_InsertUpdate(AdminPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[DepLevel_InsertUpdate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            //Sqlcmd.Parameters.Add(new SqlParameter("@DplIDs"    , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.DplIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@DplNameArs", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.DplNameArs));
            Sqlcmd.Parameters.Add(new SqlParameter("@DplNameEns", VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.DplNameEns));

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
}