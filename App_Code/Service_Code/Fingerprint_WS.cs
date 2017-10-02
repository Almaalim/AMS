using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class Fingerprint_WS : System.Web.Services.WebService
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();

    FingerprintPro ProCs = new FingerprintPro();
    FingerPrintSql SqlCs = new FingerPrintSql();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool CheckUser(string UWS, string PWS)
    {
        try
        {
            DataTable DT = DBCs.FetchData("SELECT * FROM WSUsers WHERE WSStatus = 'True' AND WSName = @P1", new string[] { "WSFingerprint" });
            if (DBCs.IsNullOrEmpty(DT)) { return false; }
            else
            {
                string User = CryptorEngine.Decrypt(DT.Rows[0]["WSUsr"].ToString(), true);
                string Pass = CryptorEngine.Decrypt(DT.Rows[0]["WSPassword"].ToString(), true);

                if (Pass == PWS && User == UWS) { return true; } else { return false; }
            }
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool DBConnectValidate(string UWS, string PWS)
    {
        if (!CheckUser(UWS, PWS)) { return false; }

        return DBCs.isConnect();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool LicValidate(string UWS, string PWS)
    {
        if (!CheckUser(UWS, PWS)) { return false; }

        string Lic = LicDf.FetchLic("ML");
        if (Lic == "1") { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool FPValidate(string UWS, string PWS, string EmpID)
    {
        if (!CheckUser(UWS, PWS)) { return false; }

        StringBuilder Q = new StringBuilder();
        Q.Append("SELECT ID FROM Thumbs WHERE ID = @P1 ");
        Q.Append(" AND ( Template1 IS NOT NULL OR Template2  IS NOT NULL OR Template3  IS NOT NULL OR Template4  IS NOT NULL OR Template5  IS NOT NULL ");
        Q.Append(" OR Template6  IS NOT NULL OR Template7  IS NOT NULL OR Template8  IS NOT NULL OR Template9  IS NOT NULL OR Template10 IS NOT NULL )");

        DataTable DT = DBCs.FetchData(Q.ToString(), new string[] { EmpID });
        if (!DBCs.IsNullOrEmpty(DT)) { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool ML_PassValidate(string UWS, string PWS, string UsrID, string Pass)
    {
        if (!CheckUser(UWS, PWS)) { return false; }

        bool isLogin   = false;
        bool AD_Active = ActiveDirectoryFun.ADEnabled();

        if (AD_Active)
        {
            string AD_Domain = ActiveDirectoryFun.GetDomainFromDB(ActiveDirectoryFun.ADTypeEnum.USR);
            if (ActiveDirectoryFun.Authenticate(UsrID, Pass, AD_Domain)) { isLogin = true; }
        }
        else
        {
            DataTable DT = DBCs.FetchData(" SELECT UsrPass,UsrName FROM LoginView WHERE UsrName = @P1 ", new string[] { UsrID });

            string DecPass = CryptorEngine.Decrypt(DT.Rows[0]["UsrPass"].ToString(), true);
            if (DecPass == Pass && DT.Rows[0]["UsrName"].ToString().Trim() == UsrID) { isLogin = true; }
        }

        return isLogin;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FP_FetchFP(string UWS, string PWS, string EmpID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        return DBCs.GetData(" SELECT * FROM Thumbs WHERE ID = @P1 ", new string[] { EmpID });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FP_FetchEmployeeInfo(string UWS, string PWS, string UsrID, string EmpID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        string DepList = "0";
        DataTable DT = DBCs.FetchData(" SELECT UsrDepartments FROM LoginView WHERE UsrName = @P1 ", new string[] { UsrID });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) { DepList = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(), true); }
        }

        DataTable DT1 = DBCs.FetchData(new SqlCommand(" SELECT EmpID FROM Employee WHERE DepID IN (" + DepList + ")"));
        if (!DBCs.IsNullOrEmpty(DT1))
        {
            ProCs.EmpID = EmpID;
            ProCs.TransactionBy = UsrID;

            SqlCs.FP_Insert_Employee(ProCs);

            return DBCs.GetData(" SELECT * FROM PersonThumbInfoView WHERE ID = @P1 ", new string[] { EmpID });
        }

        return null;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod]
    public DataSet FP_FetchPermission(string UWS, string PWS, string UsrID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        string GrpPermissions = "";

        DataTable DT = DBCs.FetchData(" SELECT * FROM LoginView WHERE UsrName = @P1 ", new string[] { UsrID });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["PGrpPermissions"] != DBNull.Value) { GrpPermissions = CryptorEngine.Decrypt(DT.Rows[0]["PGrpPermissions"].ToString(), true); }
            if (!string.IsNullOrEmpty(GrpPermissions))
            {
                string Q = "SELECT MnuTextEn FROM Menu WHERE MnuVisible = 'True' AND MnuType = 'Command' AND MnuPermissionID = 552 AND MnuPermissionID IN (" + GrpPermissions + ")";
                return DBCs.GetData(Q, null);
            }
        }

        return new DataSet();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FP_FetchImage(string UWS, string PWS, string EmpID, string FPID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        return DBCs.GetData(" SELECT Image" + FPID + " AS FingerImage FROM Thumbs WHERE ID = @P1 ", new string[] { EmpID });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet FP_FetchTemplate(string UWS, string PWS, string EmpID, string FPID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        return DBCs.GetData(" SELECT Template" + FPID + " AS FingerTemplate FROM Thumbs WHERE ID = @P1 ", new string[] { EmpID });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet ML_FetchSetting(string UWS, string PWS)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        return DBCs.GetData(" SELECT AppMiniLogger_VerificationMethod FROM ApplicationSetup ", null);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public DataSet ML_FetchPermission(string UWS, string PWS, string UsrID)
    {
        if (!CheckUser(UWS, PWS)) { return null; }

        string GrpPermissions = "";

        DataTable DT = DBCs.FetchData(" SELECT * FROM LoginView WHERE UsrName = @P1 ", new string[] { UsrID });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["PGrpPermissions"] != DBNull.Value) { GrpPermissions = CryptorEngine.Decrypt(DT.Rows[0]["PGrpPermissions"].ToString(), true); }
            if (!string.IsNullOrEmpty(GrpPermissions))
            {
                string Q = "SELECT MnuTextEn FROM Menu WHERE MnuVisible = 'True' AND MnuType = 'Command' AND MnuPermissionID = 621 AND MnuPermissionID IN (" + GrpPermissions + ")";
                return DBCs.GetData(Q, null);
            }
        }

        return new DataSet();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool FP_AddUpdate(string UWS, string PWS, string UsrID, string EmpID, string ActionID, string FPID, byte[] FPImage, byte[] FPTemplate)
    {
        try
        {
            if (!CheckUser(UWS, PWS)) { return false; }

            //string Bmp = "0x" + BitConverter.ToString(FPImage).Replace("-", string.Empty);
            //string Tmp = "0x" + BitConverter.ToString(FPTemplate).Replace("-", string.Empty);

            ProCs.EmpID         = EmpID;
            ProCs.FPID          = FPID;
            ProCs.FPImage       = FPImage;
            ProCs.FPTemplate    = FPTemplate;
            ProCs.ActionID      = ActionID;
            ProCs.TransactionBy = UsrID;

            SqlCs.FP_Update_AddFP(ProCs);

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool FP_Clear(string UWS, string PWS, string UsrID, string EmpID, string FPID)
    {
        try
        {
            if (!CheckUser(UWS, PWS)) { return false; }

            ProCs.EmpID         = EmpID;
            ProCs.FPID          = FPID;
            ProCs.TransactionBy = UsrID;

            SqlCs.FP_Update_ClearFP(ProCs);

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool FP_ClearAll(string UWS, string PWS, string UsrID, string EmpID)
    {
        try
        {
            if (!CheckUser(UWS, PWS)) { return false; }

            ProCs.EmpID         = EmpID;
            ProCs.TransactionBy = UsrID;

            SqlCs.FP_Update_ClearAllFP(ProCs);

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool FP_AddHistory(string UWS, string PWS, string UsrID, string EmpID, string ActionID, string FPID)
    {
        try
        {
            if (!CheckUser(UWS, PWS)) { return false; }

            ProCs.EmpID         = EmpID;
            ProCs.FPID          = FPID;
            ProCs.ActionID      = ActionID;
            ProCs.TransactionBy = UsrID;

            SqlCs.FP_Insert_History(ProCs);

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod]
    public bool ML_AddTransaction(string UWS, string PWS, string UsrID, string EmpID, string TransType, string TransDate, string TransTime)
    {
        try
        {
            if (!CheckUser(UWS, PWS)) { return false; }

            ProCs.EmpID = EmpID;
            ProCs.TransType = TransType;
            ProCs.TransDate = DTCs.ConvertToDatetime(TransDate, "Gregorian");
            ProCs.TransTime = TransTime;
            ProCs.TransactionBy = UsrID;

            SqlCs.ML_Insert_Transaction(ProCs);

            return true;
        }
        catch (Exception ex) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}
