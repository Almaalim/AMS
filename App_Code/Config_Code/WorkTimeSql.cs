using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public class WorkTimeSql : DataLayerBase
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
    public int Wkt_Insert(WorkTimePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[WorkingTime_Insert]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"        , IntDB, 10 , OU, false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameAr"    , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.WktNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameEn"    , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.WktNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktDesc"      , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.WktDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@WtpID"        , IntDB, 10 , IN, false, 0, 0, "", DRV, Pro.WtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktIsActive"  , BitDB, 1  , IN, false, 0, 0, "", DRV, Pro.WktIsActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktAddPercent", IntDB, 10 , IN, false, 0, 0, "", DRV, Pro.WktAddPercent));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShiftCount", IntDB, 10 , IN, false, 0, 0, "", DRV, Pro.WktShiftCount));

            //shift1
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift1From));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift1To));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift1NameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift1NameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1Grace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1EndGrace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1Duration));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1MiddleGrace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift1IsOverNight));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift1IsOptional));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1FTHours));
            
            //shift2
            if (Convert.ToInt16(Pro.WktShiftCount) > 1)
            { 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift2From));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift2To)); 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift2NameAr));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift2NameEn));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2Grace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2Duration));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2MiddleGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2EndGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift2IsOverNight));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift2IsOptional));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2FTHours));
            }

            //shift3
            if (Convert.ToInt16(Pro.WktShiftCount) > 2)
            { 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift3From));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift3To));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift3NameAr));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift3NameEn));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3Grace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3Duration));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3MiddleGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift3IsOverNight));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift3IsOptional));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3EndGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3FTHours));
            }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
            MainConnection.Open();
            int rows = Sqlcmd.ExecuteNonQuery();
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
    public bool Wkt_Update(WorkTimePro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[WorkingTime_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"        , IntDB, 10 , IN , false, 0, 0, "", DRV, Pro.WktID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameAr"    , VchDB, 100, IN , false, 0, 0, "", DRV, Pro.WktNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktNameEn"    , VchDB, 100, IN , false, 0, 0, "", DRV, Pro.WktNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktDesc"      , VchDB, 255, IN , false, 0, 0, "", DRV, Pro.WktDesc));
            Sqlcmd.Parameters.Add(new SqlParameter("@WtpID"        , IntDB, 10 , IN , false, 0, 0, "", DRV, Pro.WtpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktIsActive"  , BitDB, 1  , IN , false, 0, 0, "", DRV, Pro.WktIsActive));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktAddPercent", IntDB, 10 , IN , false, 0, 0, "", DRV, Pro.WktAddPercent));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShiftCount", IntDB, 10 , IN , false, 0, 0, "", DRV, Pro.WktShiftCount));

            //shift1
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift1From));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift1To));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift1NameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift1NameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1Grace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1EndGrace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1Duration));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1MiddleGrace));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift1IsOverNight));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift1IsOptional));
            Sqlcmd.Parameters.Add(new SqlParameter("@WktShift1FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift1FTHours));
            
            //shift2
            if (Convert.ToInt16(Pro.WktShiftCount) > 1)
            { 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift2From));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift2To)); 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift2NameAr));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift2NameEn));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2Grace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2Duration));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2MiddleGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2EndGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift2IsOverNight));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift2IsOptional));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift2FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift2FTHours));
            }

            //shift3
            if (Convert.ToInt16(Pro.WktShiftCount) > 2)
            { 
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3From"       , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift3From));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3To"         , DtDB , 10, IN, false, 0, 0, "", DRV, Pro.WktShift3To));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3NameAr"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift3NameAr));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3NameEn"     , VchDB, 50, IN, false, 0, 0, "", DRV, Pro.WktShift3NameEn));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3Grace"      , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3Grace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3Duration"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3Duration));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3MiddleGrace", IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3MiddleGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3IsOverNight", BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift3IsOverNight));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3IsOptional" , BitDB, 1 , IN, false, 0, 0, "", DRV, Pro.WktShift3IsOptional));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3EndGrace"   , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3EndGrace));
                Sqlcmd.Parameters.Add(new SqlParameter("@WktShift3FTHours"    , IntDB, 10, IN, false, 0, 0, "", DRV, Pro.WktShift3FTHours));
            }
            
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, Pro.TransactionBy));
            
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
    public bool Wkt_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[WorkingTime_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

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
    public bool RotationWkt_Delete(string ID, string TransactionBy)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[RotationWkt_Delete]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@WktID"        , IntDB, 10, IN, false, 0, 0, "", DRV, ID));
            Sqlcmd.Parameters.Add(new SqlParameter("@IsExecute"    , IntDB, 10, OU, false, 0, 0, "", DRV, 0));
            Sqlcmd.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 10, IN, false, 0, 0, "", DRV, TransactionBy));

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