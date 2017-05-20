using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Globalization;

public class ReportSql : DataLayerBase
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
    public bool Rep_Insert(ReportPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Report_Import_New]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"            , VchDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RgpID"            , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RgpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepNameAr"        , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepNameEn"        , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepDescAr"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepDescAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepDescEn"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepDescEn)); 
            Sqlcmd.Parameters.Add(new SqlParameter("@RepType"          , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepType));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepOrientation"   , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepOrientation));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepPanels"        , IntDB, 20,  IN, false, 0, 0, "", DRV, Pro.RepPanels));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepForSchedule"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepForSchedule));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepVisible"       , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@VerID"            , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.VerID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepGeneralID"     , ChrDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepGeneralID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepProcedureName" , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepProcedureName));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepParametersProc", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepParametersProc));

            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempAr"   , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempEn"   , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempDefAr", txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempDefAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempDefEn", txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempDefEn));
            
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
    public bool Rep_Update(ReportPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Report_Import_Update]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;

        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"            , VchDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RgpID"            , IntDB, 10,  IN, false, 0, 0, "", DRV, Pro.RgpID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepNameAr"        , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepNameAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepNameEn"        , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepNameEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepDescAr"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepDescAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepDescEn"        , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepDescEn)); 
            Sqlcmd.Parameters.Add(new SqlParameter("@RepType"          , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.RepType));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepOrientation"   , ChrDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepOrientation));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepPanels"        , IntDB, 20,  IN, false, 0, 0, "", DRV, Pro.RepPanels));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepForSchedule"   , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepForSchedule));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepVisible"       , BitDB, 1,   IN, false, 0, 0, "", DRV, Pro.RepVisible));
            Sqlcmd.Parameters.Add(new SqlParameter("@VerID"            , VchDB, 255, IN, false, 0, 0, "", DRV, Pro.VerID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepGeneralID"     , ChrDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepGeneralID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepProcedureName" , VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepProcedureName));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepParametersProc", VchDB, 100, IN, false, 0, 0, "", DRV, Pro.RepParametersProc));

            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempAr"   , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempEn"   , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempEn));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempDefAr", txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempDefAr));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTempDefEn", txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTempDefEn));
            
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
    public bool Report_Update_Template(ReportPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Report_Update_Template]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"        , VchDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTemp"      , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTemp));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepLang"      , VchDB, 2,  IN, false, 0, 0, "", DRV, Pro.RepLang));

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
    public bool Report_Update_DefTemplate(ReportPro Pro)
    {
        SqlCommand Sqlcmd = new SqlCommand("dbo.[Report_Update_DefTemplate]", MainConnection);
        Sqlcmd.CommandType = CommandType.StoredProcedure;
        
        try
        {
            Sqlcmd.Parameters.Add(new SqlParameter("@RepID"        , VchDB, 10,  IN, false, 0, 0, "", DRV, Pro.RepID));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepTemp"      , txtDB, 3000000, IN, false, 0, 0, "", DRV, Pro.RepTemp));
            Sqlcmd.Parameters.Add(new SqlParameter("@RepLang"      , VchDB, 2,  IN, false, 0, 0, "", DRV, Pro.RepLang));

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