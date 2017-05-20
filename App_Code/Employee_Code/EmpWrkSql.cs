using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class EmpWrkSql : DataLayerBase
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int Wkt_Insert(EmpWrkPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRel_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
    
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"       , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.EwrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"       , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrStartDate", DtDB,  14, IN, false, 0, 0, "", DRV, Pro.EwrStartDate));

            if (!String.IsNullOrEmpty(Pro.EwrEndDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EwrEndDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.EwrEndDate)); }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrFri));
           
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@EwrID"].Value); 
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
    public bool Wkt_Update(EmpWrkPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRel_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.EwrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"       , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrStartDate", DtDB,  14, IN, false, 0, 0, "", DRV, Pro.EwrStartDate));

            if (!String.IsNullOrEmpty(Pro.EwrEndDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EwrEndDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.EwrEndDate)); }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrFri));
           
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
    public bool Wkt_Insert_WithMoveBack(EmpWrkPro Pro, out string SaveIDs, out string NotSaveIDs)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRel_InsertWithMoveBack]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"      , TxtDB, 1000000, IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrStartDate", DtDB,  14, IN, false, 0, 0, "", DRV, Pro.EwrStartDate));

            if (!String.IsNullOrEmpty(Pro.EwrEndDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EwrEndDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.EwrEndDate)); }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrFri));
           
            Sqlcmd.Parameters.Add(new SqlParameter("@SaveIDs"   , VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));
            Sqlcmd.Parameters.Add(new SqlParameter("@NotSaveIDs", VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }

            SaveIDs    = Sqlcmd.Parameters["@SaveIDs"].Value.ToString();
            NotSaveIDs = Sqlcmd.Parameters["@NotSaveIDs"].Value.ToString();
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
    public bool Wkt_Update_WithMoveBack(EmpWrkPro Pro, out string SaveIDs, out string NotSaveIDs)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRel_UpdateWithMoveBack]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.EwrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpID"       , VchDB, 15, IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"       , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrStartDate", DtDB,  14, IN, false, 0, 0, "", DRV, Pro.EwrStartDate));

            if (!String.IsNullOrEmpty(Pro.EwrEndDate)) { Sqlcmd.Parameters.Add(new SqlParameter("@EwrEndDate", DtDB, 14, IN, false, 0, 0, "", DRV, Pro.EwrEndDate)); }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri", BitDB, 1, IN, false, 0, 0, "", DRV, Pro.EwrFri));
           
            Sqlcmd.Parameters.Add(new SqlParameter("@SaveIDs"   , VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));
            Sqlcmd.Parameters.Add(new SqlParameter("@NotSaveIDs", VchDB, 8000, OU, false, 0, 0, "", DRV, "1"));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }

            SaveIDs    = Sqlcmd.Parameters["@SaveIDs"].Value.ToString();
            NotSaveIDs = Sqlcmd.Parameters["@NotSaveIDs"].Value.ToString();
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
    public bool Wkt_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRel_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
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
    public int DefaultWkt_Insert(EmpWrkPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[EmpWrkRelDefault_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
    
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrID"              , IntDB, 10, OU, false, 0, 0, "", DRV, Pro.EwrID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"              , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri"             , BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrFri));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWrkDefault"      , BitDB, 1,  IN, false, 0, 0, "", DRV, true));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWrkDefaultForAll", BitDB, 1,  IN, false, 0, 0, "", DRV, Pro.EwrWrkDefaultForAll));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@EwrID"].Value); 
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
    public bool UnstyleWkt_Insert(EmpWrkPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[UnstyleShift_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs"      , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.EmpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktIDs"      , VchDB, 8000, IN, false, 0, 0, "", DRV, Pro.WktIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@FirstDayDate", DtDB,  20,   IN, false, 0, 0, "", DRV, Pro.FirstDayDate));
            Sqlcmd.Parameters.Add(new SqlParameter("@DayCount"    , IntDB, 10,   IN, false, 0, 0, "", DRV, Pro.DayCount));
            
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
    public int RotationWkt_Insert(WorkTimePro WPro,EmpWrkPro EWPro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RotationWkt_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
    
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"              , IntDB, 10 , OU, false, 0, 0, "", DRV, WPro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameAr"          , VchDB, 100, IN, false, 0, 0, "", DRV, WPro.WktNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameEn"          , VchDB, 100, IN, false, 0, 0, "", DRV, WPro.WktNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1From"      , DtDB , 10,  IN, false, 0, 0, "", DRV, WPro.WktShift1From));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1To"        , DtDB , 10,  IN, false, 0, 0, "", DRV, WPro.WktShift1To));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1Duration"  , IntDB, 10,  IN, false, 0, 0, "", DRV, WPro.WktShift1Duration));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1IsOptional", BitDB, 1 ,  IN, false, 0, 0, "", DRV, WPro.WktShift1IsOptional));

            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSat" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrSat));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrSun" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrSun));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrMon" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrMon));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrTue" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrTue));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrWed" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrWed));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrThu" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrThu));
            Sqlcmd.Parameters.Add(new SqlParameter("@EwrFri" , BitDB, 1,   IN, false, 0, 0, "", DRV, EWPro.EwrFri));

            Sqlcmd.Parameters.Add(new SqlParameter("@DateLen", IntDB, 10,   IN, false, 0, 0, "", DRV, EWPro.DateLen));
            Sqlcmd.Parameters.Add(new SqlParameter("@SDates" , VchDB, 8000, IN, false, 0, 0, "", DRV, EWPro.SDates));
            Sqlcmd.Parameters.Add(new SqlParameter("@EDates" , VchDB, 8000, IN, false, 0, 0, "", DRV, EWPro.EDates));
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpLen" , IntDB, 10,   IN, false, 0, 0, "", DRV, EWPro.GrpLen));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktIDs" , VchDB, 8000, IN, false, 0, 0, "", DRV, EWPro.WktIDs));        
            Sqlcmd.Parameters.Add(new SqlParameter("@GrpIDs" , VchDB, 8000, IN, false, 0, 0, "", DRV, EWPro.GrpIDs));
            Sqlcmd.Parameters.Add(new SqlParameter("@EmpIDs" , VchDB, 8000,  IN, false, 0, 0, "", DRV, EWPro.EmpIDs));

            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 15, IN, false, 0, 0, "", DRV, EWPro.TransactionBy));
            
            MainConnection.Open();
            Sqlcmd.ExecuteNonQuery();
            if (Convert.ToInt32(Sqlcmd.Parameters["@IsExecute"].Value) == -1) { throw new Exception(General.ProcedureMsg(), null); }
            return Convert.ToInt32(Sqlcmd.Parameters["@WktID"].Value); 
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