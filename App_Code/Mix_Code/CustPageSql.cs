using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class CustPageSql : DataLayerBase
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
    public int CustPage_Insert(string MnuNumber, string UsrName)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CustPage_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CPgID"    , IntDB, 10,   OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"  , VchDB, 15,   IN, false, 0, 0, "", DRV, UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@MnuNumber", IntDB, 1000, IN, false, 0, 0, "", DRV, MnuNumber));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
          
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@CPgID"].Value); 
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
    public bool CustPage_Delete(string ID, string UsrName)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CustPage_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CPgID"  , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int CustRep_Insert(string UsrName, string RepID, string paramName, string paramValue)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CustRep_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CustRepID" , IntDB, 10,      OU, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"   , VchDB, 15,      IN, false, 0, 0, "", DRV, UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"     , ChrDB, 10,      IN, false, 0, 0, "", DRV, RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ParamName" , VchDB, 20,      IN, false, 0, 0, "", DRV, paramName));
            Sqlcmd.Parameters.Add(new SqlParameter("@ParamValue", VchDB, 1000000, IN, false, 0, 0, "", DRV, paramValue));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute", IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@CustRepID"].Value); 
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
    public bool CustRep_Update(string custRepID, string UsrName, string RepID, string paramName, string paramValue)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CustRep_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@CustRepID" , IntDB, 10,      IN, false, 0, 0, "", DRV, 1));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"   , VchDB, 15,      IN, false, 0, 0, "", DRV, UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"     , ChrDB, 10,      IN, false, 0, 0, "", DRV, RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ParamName" , VchDB, 20,      IN, false, 0, 0, "", DRV, paramName));
            Sqlcmd.Parameters.Add(new SqlParameter("@ParamValue", VchDB, 1000000, IN, false, 0, 0, "", DRV, paramValue));
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
    public bool CustRep_Delete(string ID, string UsrName)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[CustRep_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"  , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName", VchDB, 15, IN, false, 0, 0, "", DRV, UsrName));
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
}