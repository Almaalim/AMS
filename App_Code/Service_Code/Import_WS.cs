using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class Import_WS : System.Web.Services.WebService
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();

    ImportLogPro ProCs = new ImportLogPro();
    ImportLogSql SqlCs = new ImportLogSql();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool CheckUser(string WS, out string Err)
    {
        Err = "";

        try
        {
            DataTable DT = DBCs.FetchData("SELECT * FROM WSUsers WHERE WSStatus = 'True' AND WSName = @P1", new string[] { "WSImport" });
            if (DBCs.IsNullOrEmpty(DT)) { Err = "WS_UValid Message #1: The login data is incorrect"; /**/ return false; }
            else
            {
                string UWS  = WS.Split(',')[0];
                string PWS  = WS.Split(',')[1];
                string User = CryptorEngine.Decrypt(DT.Rows[0]["WSUsr"].ToString(), true);
                string Pass = CryptorEngine.Decrypt(DT.Rows[0]["WSPassword"].ToString(), true);

                if (Pass == PWS && User == UWS) { return true; } else { Err = "WS_UValid Message #2: The login data is incorrect"; /**/ return false; }
            }
        }
        catch (Exception ex) { Err = String.Format("WS_UValid Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool DBConnectValidate(string WS, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return false; }

            bool isConnect = DBCs.isConnect();
            if (!isConnect) { Err = "WS_DBValid Message :no connection"; }

            return isConnect;
        }
        catch (Exception ex) { Err = String.Format("WS_DBValid Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool LicValidate(string WS, out string Err)
    {
        Err = "";

        return true;
        //if (!CheckUser(WS)) { return false; }

        //string Lic = LicDf.FetchLic("ML");
        //if (Lic == "1") { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FetchMacInfo(string WS, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return null; }

            StringBuilder SQ = new StringBuilder();
            SQ.Append(" SELECT MacID, MtpName, MacLocationAr, MacLocationEn, MacIP, MacPort, MacNo, ISNULL(MacUseKey,'False') MacUseKey, MacInKeys, MacOutKeys");
            SQ.Append(" FROM MachineInfoView ");
            SQ.Append(" WHERE MacStatus = 'True' AND MacVirtualType IS NULL AND ISNULL(MacDeleted,0) = 0 ");
            //SQ.Append(" AND MacID IN (16,17) ");
            SQ.Append(" ORDER BY MtpID ");

            DataSet DS = DBCs.GetData(SQ.ToString(), null);

            if (DS == null) { Err = "WS_Fetch_Mac Message :There are no data"; }

            return DS;
        }
        catch (Exception ex) { Err = String.Format("WS_Fetch_Mac Error: {0}", ex.Message); /**/ return null; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FetchSettingInfo(string WS, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return null; }

            StringBuilder SQ = new StringBuilder();
            SQ.Append(" SELECT * FROM ImportSetting");

            DataSet DS = DBCs.GetData(SQ.ToString(), null);

            if (DS == null) { Err = "WS_Fetch_Setting Message :There are no data"; }

            return DS;
        }
        catch (Exception ex) { Err = String.Format("WS_Fetch_Setting Error: {0}", ex.Message); /**/ return null; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet ProcessInfo(string WS, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return null; }

            StringBuilder SQ = new StringBuilder();
            SQ.Append(" SELECT * FROM ImportSetting");

            DataSet DS = DBCs.GetData(SQ.ToString(), null);

            if (DS == null) { Err = "WS_Fetch_Setting Message :There are no data"; }

            return DS;
        }
        catch (Exception ex) { Err = String.Format("WS_Fetch_Setting Error: {0}", ex.Message); /**/ return null; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool Trans_Insert(string WS, DataSet DS, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return false; }
            if (DS == null) { Err = "WS_Trans_Insert Message: There are no data"; return false; }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DBCs.CopyDataToTransDump(DS.Tables[0]);

            return true;
        }
        catch (Exception ex) { Err = String.Format("WS_Trans_Insert Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool Import_Log_Insert(out int LogID, string WS, string[] Params, out string Err)
    {
        Err = "";
        LogID = 0;

        try
        {
            
            if (!CheckUser(WS, out Err)) { return false; }
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.IplType            = Params[0]; // M = Manual, A = automatic
            ProCs.IplRunTodayProcess = Params[1]; // 0 = false, 1 = true , 2 = undefined
            ProCs.IplRunProcess      = Params[2]; // 0 = false, 1 = true , 2 = undefined

            LogID = SqlCs.Import_Log_Insert(ProCs);

            return true;
        }
        catch (Exception ex) { Err = String.Format("WS_Import_Log_Insert Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool Import_Log_Update_EndDate(string WS, string[] Params, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return false; }
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            SqlCs.Import_Log_Update_EndDate(Params[0]);

            return true;
        }
        catch (Exception ex) { Err = String.Format("WS_Import_Log_Update_EndDate Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool Import_MachineLog_Insert(string WS, string[] Params, out string Err)
    {
        Err = "";

        try
        {
            if (!CheckUser(WS, out Err)) { return false; }
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.IplID         = Params[0];
            ProCs.MacID         = Params[1];
            ProCs.ImlStartDT    = Params[2];
            ProCs.ImlIsImport   = Params[3];
            ProCs.ImlTransCount = Params[4];
            ProCs.ImlErrMsg     = Params[5];

            if (string.IsNullOrEmpty(ProCs.ImlErrMsg)) { ProCs.ImlErrMsg = null; }
            SqlCs.Import_MachineLog_Insert(ProCs);

            return true;
        }
        catch (Exception ex) { Err = String.Format("WS_Import_MachineLog_Insert Error: {0}", ex.Message); /**/ return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool StartProcess(string WS, out string Err)
    {
        SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(General.ConnString)
        {
            ConnectTimeout = 4000,
            AsynchronousProcessing = true
        };

        SqlConnection conn = new SqlConnection(connectionBuilder.ConnectionString);
        SqlCommand cmd = new SqlCommand(" EXECUTE spAttendanceProcess ", conn);

        try
        {
            conn.Open();

            //The actual T-SQL execution happens in a separate work thread.
            cmd.BeginExecuteReader(new AsyncCallback(ProcessCallbackFunction), cmd);

            Err = "";
            return true;
        }
        catch (SqlException se)
        {
            Err = String.Format("WS_StartProcess Error: {0}", se.Message);
            return false;
        }
        catch (Exception ex)
        {
            Err = String.Format("WS_StartProcess Error: {0}", ex.Message);
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    private void ProcessCallbackFunction(IAsyncResult asyncResult)
    {
        try
        {
            ////un-box the AsynState back to the SqlCommand
            //SqlCommand cmd = (SqlCommand)asyncResult.AsyncState;
            //SqlDataReader reader = cmd.EndExecuteReader(asyncResult);
            //while (reader.Read())
            //{
            //    Dispatcher.BeginInvoke(new delegateAddTextToListbox(AddTextToListbox),
            //                            reader.GetString(0));
            //}
            //if (cmd.Connection.State.Equals(ConnectionState.Open))
            //{
            //    cmd.Connection.Close();
            //}
        }
        catch (Exception ex)
        {
            //ToDo : Swallow exception log
        }
    }   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod]
    public bool StartTodayProcess(string WS, out string Err)
    {
        SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(General.ConnString)
        {
            ConnectTimeout = 4000,
            AsynchronousProcessing = true
        };

        SqlConnection conn = new SqlConnection(connectionBuilder.ConnectionString);
        SqlCommand cmd = new SqlCommand(" EXECUTE spTodayStateProcess ", conn);

        try
        {
            conn.Open();

            //The actual T-SQL execution happens in a separate work thread.
            cmd.BeginExecuteReader(new AsyncCallback(TodayProcessCallbackFunction), cmd);

            Err = "";
            return true;
        }
        catch (SqlException se)
        {
            Err = String.Format("WS_StartTodayProcess Error: {0}", se.Message);
            return false;
        }
        catch (Exception ex)
        {
            Err = String.Format("WS_StartTodayProcess Error: {0}", ex.Message);
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    private void TodayProcessCallbackFunction(IAsyncResult asyncResult)
    {
        try
        {
            ////un-box the AsynState back to the SqlCommand
            //SqlCommand cmd = (SqlCommand)asyncResult.AsyncState;
            //SqlDataReader reader = cmd.EndExecuteReader(asyncResult);
            //while (reader.Read())
            //{
            //    Dispatcher.BeginInvoke(new delegateAddTextToListbox(AddTextToListbox),
            //                            reader.GetString(0));
            //}
            //if (cmd.Connection.State.Equals(ConnectionState.Open))
            //{
            //    cmd.Connection.Close();
            //}
        }
        catch (Exception ex)
        {
            //ToDo : Swallow exception log
        }
    }   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}
