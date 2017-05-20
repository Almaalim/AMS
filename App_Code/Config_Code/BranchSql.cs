using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class BranchSql : DataLayerBase
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
    public int Insert(BranchPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Branch_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcID"     , IntDB, 10,   OU, false, 0, 0, "", DRV, Pro.BrcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameAr" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameEn" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"   , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcAddress", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.BrcAddress));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcCity"   , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCity));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcCountry", VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCountry));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcPOBox"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcPOBox));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcTelNo"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcTelNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcEmail"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcEmail));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcStatus" , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.BrcStatus));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@BrcID"].Value); 
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
    public bool Update(BranchPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Branch_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcID"     , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.BrcID));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameAr" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameEn" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"   , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.UsrName));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcAddress", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.BrcAddress));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcCity"   , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCity));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcCountry", VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCountry));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcPOBox"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcPOBox));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcTelNo"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcTelNo));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcEmail"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcEmail));
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcStatus" , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.BrcStatus));

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
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Branch_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@BrcID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    //public int InsertUpdate(BranchPro Pro)
    //{
    //    SqlCommand Sqlcmd = new SqlCommand("dbo.[Branch_InsertUpdate]", MainConnection);
    //    Sqlcmd.CommandType = CommandType.StoredProcedure;
        
    //    try
    //    {
    //        Sqlcmd.Parameters.Add(new SqlParameter("@oBrcID"    , IntDB, 10,   OU, false, 0, 0, "", DRV, 0));

    //        Sqlcmd.Parameters.Add(new SqlParameter("@iBrcID"    , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.BrcID));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameAr" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameAr));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcNameEn" , VchDB, 100,  IN, false, 0, 0, "", DRV, Pro.BrcNameEn));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@UsrName"   , VchDB, 15,   IN, false, 0, 0, "", DRV, Pro.UsrName));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcAddress", VchDB, 1000, IN, false, 0, 0, "", DRV, Pro.BrcAddress));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcCity"   , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCity));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcCountry", VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcCountry));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcPOBox"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcPOBox));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcTelNo"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcTelNo));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcEmail"  , VchDB, 200,  IN, false, 0, 0, "", DRV, Pro.BrcEmail));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@BrcStatus" , BitDB, 1,    IN, false, 0, 0, "", DRV, Pro.BrcStatus));

    //        Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
    //        Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
    //        MainConnection.Open();
    //        Sqlcmd.ExecuteNonQuery();
    //        if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
    //        return Convert.ToInt32(Sqlcmd.Parameters["@iBrcID"].Value); 
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
}