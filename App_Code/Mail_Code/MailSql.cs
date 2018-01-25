using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class MailSql : DataLayerBase
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
    SqlDbType TxtDB = SqlDbType.Text;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Mail_Request_Insert(string MailCode, string MailJoinID, string MailSendingToList)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Mail_Request_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        { 
            Sqlcmd.Parameters.Add(new SqlParameter("@MailReqID"         , IntDB, 10 , OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailCode"          , VchDB, 100, IN, false, 0, 0, "", DRV, MailCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailJoinID"        , VchDB, 100, IN, false, 0, 0, "", DRV, MailJoinID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendingToList" , VchDB, 500, IN, false, 0, 0, "", DRV, MailSendingToList));
           
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            //return Convert.ToInt32(Sqlcmd.Parameters["@MailReqID"].Value); 
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
    public int MailLog_Request_Insert(string MailCode, string MailReqID, bool MailIsSend, string MailSendingTo, string ShiftID, string ErrText)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[MailLog_Request_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
            
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@LogID"        , IntDB, 100 , OU,  false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailCode"     , VchDB, 100 , IN , false, 0, 0, "", DRV, MailCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailReqID"    , IntDB, 100 , IN , false, 0, 0, "", DRV, MailReqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailIsSend"   , BitDB, 1   , IN , false, 0, 0, "", DRV, MailIsSend));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendingTo", VchDB, 500 , IN , false, 0, 0, "", DRV, MailSendingTo));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErrText"      , TxtDB, 500 , IN , false, 0, 0, "", DRV, ErrText));
                
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            return Convert.ToInt32(Sqlcmd.Parameters["@LogID"].Value); 
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
    public bool Mail_Request_UpdateStatus(string MailReqID)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Mail_Request_UpdateStatus]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
            
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MailReqID", IntDB, 10, IN, false, 0, 0, "", DRV, MailReqID));

            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Int32 Mail_Notification_Insert(string MailCode,string ShiftID, string MailSendMethod)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Mail_Notification_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
            
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MailReqID"      , IntDB, 100, OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailCode"       , VchDB, 100, IN, false, 0, 0, "", DRV, MailCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@ShiftID"        , IntDB, 100, IN, false, 0, 0, "", DRV, ShiftID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendMethod" , VchDB, 10,  IN, false, 0, 0, "", DRV, MailSendMethod));
               
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            return Convert.ToInt32(Sqlcmd.Parameters["@MailReqID"].Value); 
        }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public Int32 MailLog_Notification_Insert(string MailCode, string MailReqID, bool MailIsSend, string MailSendingTo, string ShiftID, string ErrText, string MailSendMethod)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[MailLog_Notification_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@LogID"          , IntDB, 100, OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailCode"       , VchDB, 100, IN, false, 0, 0, "", DRV, MailCode));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailReqID"      , IntDB, 100, IN, false, 0, 0, "", DRV, MailReqID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailIsSend"     , BitDB, 1  , IN, false, 0, 0, "", DRV, MailIsSend));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendingTo"  , VchDB, 500, IN, false, 0, 0, "", DRV, MailSendingTo));
            Sqlcmd.Parameters.Add(new SqlParameter("@ShiftID"        , IntDB, 100, IN, false, 0, 0, "", DRV, ShiftID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ErrText"        , TxtDB, 500, IN, false, 0, 0, "", DRV, ErrText));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendMethod" , VchDB, 10,  IN, false, 0, 0, "", DRV, MailSendMethod));
                  
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            return Convert.ToInt32(Sqlcmd.Parameters["@LogID"].Value); 
        }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Schedule_UpdateLastTime(string SchID)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Schedule_UpdateLastTime]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
            
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@SchID", IntDB, 100, IN, false, 0, 0, "", DRV, SchID));
               
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }
        finally
        {
            MainConnection.Close();
            Sqlcmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Notification_Update(MailPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Notification_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        { 
            Sqlcmd.Parameters.Add(new SqlParameter("@MailID"            , IntDB, 10  , IN, false, 0, 0, "", DRV, Pro.MailID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailNameAr"        , VchDB, 500 , IN, false, 0, 0, "", DRV, Pro.MailNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailNameEn"        , VchDB, 500 , IN, false, 0, 0, "", DRV, Pro.MailNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailDesc"          , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.MailDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchActive"         , BitDB, 1   , IN, false, 0, 0, "", DRV, Pro.SchActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchType"           , VchDB, 20  , IN, false, 0, 0, "", DRV, Pro.SchType));        
            Sqlcmd.Parameters.Add(new SqlParameter("@SchDays"           , VchDB, 100 , IN, false, 0, 0, "", DRV, Pro.SchDays));
            Sqlcmd.Parameters.Add(new SqlParameter("@SchStartTimeShift1", DtDB , 100 , IN, false, 0, 0, "", DRV, Pro.SchStartTimeShift1));
            Sqlcmd.Parameters.Add(new SqlParameter("@MailSendMethod"    , VchDB , 10 , IN, false, 0, 0, "", DRV, Pro.MailSendMethod));
            
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
}