using System;
using System.Data;
using System.Data.SqlClient;

public class FingerPrintSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ParameterDirection IN = ParameterDirection.Input;
    ParameterDirection OU = ParameterDirection.Output;
    DataRowVersion DRV = DataRowVersion.Proposed;

    SqlDbType IntDB = SqlDbType.Int;
    SqlDbType VchDB = SqlDbType.VarChar;
    SqlDbType DtDB = SqlDbType.DateTime;
    SqlDbType BitDB = SqlDbType.Bit;
    SqlDbType ChrDB = SqlDbType.Char;
    SqlDbType ImgDB = SqlDbType.Image;
    SqlDbType BinDB = SqlDbType.Binary;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FP_Insert_Employee(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Thumbs_Insert_Employee]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID", VchDB, 20, IN, false, 0, 0, "", DRV, Pro.EmpID));

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
    public bool FP_Update_AddFP(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Thumbs_Update_AddFP]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"     , VchDB, 20,                 IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FPID"      , VchDB, 2,                  IN, false, 0, 0, "", DRV, Pro.FPID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FPImage"   , ImgDB, Pro.FPImage.Length, IN, false, 0, 0, "", DRV, Pro.FPImage));
            Sqlcmd.Parameters.Add(new SqlParameter("@FPTemplate", BinDB, 256,                IN, false, 0, 0, "", DRV, Pro.FPTemplate));
            Sqlcmd.Parameters.Add(new SqlParameter("@ActionID"  , IntDB, 10,                 IN, false, 0, 0, "", DRV, Pro.ActionID));

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
    public bool FP_Update_ClearFP(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Thumbs_Update_ClearFP]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID", VchDB, 20, IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FPID" , VchDB, 2,  IN, false, 0, 0, "", DRV, Pro.FPID));
           
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
    public bool FP_Update_ClearAllFP(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Thumbs_Update_ClearAllFP]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID", VchDB, 20, IN, false, 0, 0, "", DRV, Pro.EmpID));
           
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
    public bool FP_Insert_History(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[ThumbHistory_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"   , VchDB, 20, IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@FPID"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.FPID));
            Sqlcmd.Parameters.Add(new SqlParameter("@ActionID", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.ActionID));

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
    public bool ML_Insert_Transaction(FingerprintPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[TransDump_Insert_ML]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnDate", DtDB, 20, IN, false, 0, 0, "", DRV, Pro.TransDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnTime", DtDB, 20, IN, false, 0, 0, "", DRV, Pro.TransTime));
            Sqlcmd.Parameters.Add(new SqlParameter("@TrnType", ChrDB, 1, IN, false, 0, 0, "", DRV, Pro.TransType));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpID));

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