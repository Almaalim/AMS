using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class RuleSql : DataLayerBase
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
    public int RuleType_Insert(RulePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RuleType_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RltID"             , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.RltID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"             , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.RlsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltType"           , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.RltType));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltRuleMeasureIn"  , ChrDB, 7,  IN, false, 0, 0, "", DRV, Pro.RltRuleMeasureIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltUnits"          , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.RltUnits));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltFrequencyFlag"  , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.RltFrequencyFlag));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltFrequency"      , IntDB, 4,  IN, false, 0, 0, "", DRV, Pro.RltFrequency));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltActionMeasureIn", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.RltActionMeasureIn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RltActionUnits"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.RltActionUnits));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@RltID"].Value); 
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
    public bool RuleType_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RuleType_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RltID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    public int RuleSet_Insert(RulePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RuleSet_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"    , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.RlsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsNameAr", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.RlsNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsNameEn", VchDB, 50, IN, false, 0, 0, "", DRV, Pro.RlsNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsStatus", BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.RlsStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@RlsID"].Value); 
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
    public bool RuleSet_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RuleSet_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    public bool Wizard_RuleSet(string EmpIDs, string RlsID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Wizard_RuleSet]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"       , txtDB, 1000000, IN, false, 0, 0, "", DRV, EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@RlsID"        , IntDB, 10,      IN, false, 0, 0, "", DRV, RlsID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10,      OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15,      IN, false, 0, 0, "", DRV, TransactionBy));

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