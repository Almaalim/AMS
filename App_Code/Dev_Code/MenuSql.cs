using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class MenuSql : DataLayerBase
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
    public int Menu_Insert(MenuPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"           , IntDB, 10,  OU, false, 0, 0, "", DRV, Pro.MnuNumber));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextEn"           , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuTextEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextAr"           , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuTextAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuDescription"      , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuArabicDescription", VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuArabicDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuServer"           , VchDB, 55,  IN, false, 0, 0, "", DRV, Pro.MnuServer));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuURL"              , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuURL));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuParentID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.MnuParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuVisible"          , VchDB, 5,   IN, false, 0, 0, "", DRV, Pro.MnuVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuType"             , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuType));
            Sqlcmd.Parameters.Add(new SqlParameter("@RgpID"               , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RgpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VerID"               , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.VerID));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@MenuNumber"].Value); 
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
    public bool Menu_Update(MenuPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"           , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.MnuNumber));
            //Sqlcmd.Parameters.Add(new SqlParameter("@MnuID"               , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.MnuID));
            //Sqlcmd.Parameters.Add(new SqlParameter("@MnuPermissionID"     , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.MnuPermissionID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextEn"           , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuTextEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextAr"           , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuTextAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuDescription"      , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuArabicDescription", VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuArabicDescription));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuServer"           , VchDB, 55,  IN, false, 0, 0, "", DRV, Pro.MnuServer));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuURL"              , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.MnuURL));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuParentID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.MnuParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuVisible"          , VchDB, 5,   IN, false, 0, 0, "", DRV, Pro.MnuVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuType"             , VchDB, 50,  IN, false, 0, 0, "", DRV, Pro.MnuType));
            Sqlcmd.Parameters.Add(new SqlParameter("@RgpID"               , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RgpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@VerID"               , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.VerID));
            
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
    public bool Menu_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"    , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    public bool Menu_Update_Order(string MenuIDs, string OrderIDs, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Update_Order]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MenuIDs"      , VchDB, 8000, IN, false, 0, 0, "", DRV, MenuIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@OrderIDs"     , VchDB, 8000, IN, false, 0, 0, "", DRV, OrderIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10,   OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15,   IN, false, 0, 0, "", DRV, TransactionBy));

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
    public int Menu_Insert_Command(string pTextEn, string pTextAr, string pParentID, string pVisible,string pCommandType,string pOrder, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Insert_Command]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"           , IntDB, 10,  OU, false, 0, 0, "", DRV, pParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextEn"           , VchDB, 50,  IN, false, 0, 0, "", DRV, pTextEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuTextAr"           , VchDB, 50,  IN, false, 0, 0, "", DRV, pTextAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuDescription"      , VchDB, 255, IN, false, 0, 0, "", DRV, pTextEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuArabicDescription", VchDB, 255, IN, false, 0, 0, "", DRV, pTextAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuParentID"         , IntDB, 10,  IN, false, 0, 0, "", DRV, pParentID));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuVisible"          , VchDB, 5,   IN, false, 0, 0, "", DRV, pVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuType"             , VchDB, 50,  IN, false, 0, 0, "", DRV, pCommandType));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuOrder"            , IntDB, 10,  IN, false, 0, 0, "", DRV, pOrder));
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@MenuNumber"].Value); 
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
    public bool Menu_Update_Command(string pMenuNumber, string pVisible,string pOrder, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Update_Command]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"    , IntDB, 10, IN, false, 0, 0, "", DRV, pMenuNumber));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuVisible"   , VchDB, 5,  IN, false, 0, 0, "", DRV, pVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuOrder"     , IntDB, 10, IN, false, 0, 0, "", DRV, pOrder));
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
    public bool Menu_Delete_Command(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Menu_Delete_Command]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber"    , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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

