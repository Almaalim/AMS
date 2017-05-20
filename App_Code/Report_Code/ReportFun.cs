using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System.Text;
using System.Data.SqlClient;

public class ReportFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    DTFun DTCs = new DTFun();

    FavoriteFormsPro FavProCs = new FavoriteFormsPro();
    FavoriteFormsSql FavSqlCs = new FavoriteFormsSql();

    string RepOrientation = "V";
    string RepLang        = "EN";
    string RepUser        = "";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public StiReport CreateReport(RepParametersPro RepProCs)
    {
        try
        {
            StiReport Rep = new StiReport();
            //// if (RepParametersPro.ProcedureName.Length > 0) { RepParametersPro.UsrName = pgCs.LoginID; }

            if (!string.IsNullOrEmpty(RepProCs.ProcedureName))
            {
                ////ExecuteProc(RepProCs.DateFrom, RepProCs.DateTo, RepProCs.EmpID, RepProCs.RepUser, RepProCs.DepID, RepProCs.ProcedureName, RepProCs.ProcedureParameters);
            }

            RepUser        = RepProCs.RepUser;
            RepLang        = RepProCs.RepLang;
            RepOrientation = RepProCs.RepOrientation;

            Rep.LoadFromString(RepProCs.RepTemp);
            Rep.Dictionary.Databases.Clear();
            Rep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Connection", General.ConnString));
            //(report.GetComponentByName("ReportTitleBand1") as StiReportTitleBand).PrintOn = StiPrintOnType.AllPages;
            Rep.GetSubReport += new StiGetSubReportEventHandler(rep_GetSubReport);
            Rep.Dictionary.Synchronize();
            Rep.Compile();

            /////// Fill Parameters to Report
            if (!string.IsNullOrEmpty(RepProCs.Date))        { Rep["ParamDate"]      = DTCs.ConvertToDatetime(RepProCs.Date,"Gregorian"); }
            if (!string.IsNullOrEmpty(RepProCs.DateFrom))    { Rep["ParamDateFrom"]  = DTCs.ConvertToDatetime(RepProCs.DateFrom,"Gregorian"); }
            if (!string.IsNullOrEmpty(RepProCs.DateTo))      { Rep["ParamDateTo"]    = DTCs.ConvertToDatetime( RepProCs.DateTo,"Gregorian"); }
            if (!string.IsNullOrEmpty(RepProCs.MonthDate))   { Rep["ParamMonthDate"] = RepProCs.MonthDate; }
            if (!string.IsNullOrEmpty(RepProCs.YearDate))    { Rep["ParamYearDate"]  = RepProCs.YearDate; } 
            if (!string.IsNullOrEmpty(RepProCs.WktID))       { Rep["WktID"]          = RepProCs.WktID; }
            if (!string.IsNullOrEmpty(RepProCs.MacID))       { Rep["MacID"]          = RepProCs.MacID; }
            if (!string.IsNullOrEmpty(RepProCs.EmpID))       { Rep["EmpID"]          = RepProCs.EmpID; }
            if (!string.IsNullOrEmpty(RepProCs.DepID))       { Rep["DepID"]          = RepProCs.DepID; }
            if (!string.IsNullOrEmpty(RepProCs.CatID))       { Rep["CatID"]          = RepProCs.CatID; }
            if (!string.IsNullOrEmpty(RepProCs.UsrName))     { Rep["UsrName"]        = RepProCs.UsrName; } ////else { Rep["UsrName"] = RepProCs.RepUser; }
            if (!string.IsNullOrEmpty(RepProCs.VtpID))       { Rep["VtpID"]          = RepProCs.VtpID; }
            if (!string.IsNullOrEmpty(RepProCs.ExcID))       { Rep["ExcID"]          = RepProCs.ExcID; }
            if (!string.IsNullOrEmpty(RepProCs.DaysCount))   { Rep["DaysCount"]      = RepProCs.DaysCount; }
            if (!string.IsNullOrEmpty(RepProCs.Permissions))
            {
                Rep["Permission"] = RepProCs.Permissions;
                ////(Rep.GetComponentByName("Text2") as StiText).Text = RepProCs.RepID;
            }

            Rep.Render(false);

            return Rep;
        }
        catch (Exception ex) { return null; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getTemplate_HeaderFooter(string RepType)
    {
        string RepTmp  = "";
        string Ver = LicDf.FindActiveVersion();
        string Version = (Ver == "General" ? "0" : Ver);

        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT * FROM Report WHERE RepType = @P1 AND RepOrientation = @P2 ");
        sb.Append(" AND ( CHARINDEX('General',VerID) > 0 OR CHARINDEX(@P3, VerID) > 0) ");
        sb.Append(" AND RepID NOT IN ( SELECT ISNULL(RepGeneralID,'0') FROM Report WHERE VerID = @P3) ");

        DataTable DT = DBCs.FetchData(sb.ToString(), new string[] { RepType, RepOrientation, Version });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            RepTmp = General.Msg(RepLang, DT.Rows[0]["RepTempEn"].ToString(), DT.Rows[0]["RepTempAr"].ToString());
        }

        return RepTmp;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void rep_GetSubReport(object sender, StiGetSubReportEventArgs e)
    {
        StiReport rep = new StiReport();
        string RepHeader = getTemplate_HeaderFooter("Header");
        string RepFooter = getTemplate_HeaderFooter("Footer");

        if (e.SubReportName == "SubReport1") { rep.LoadFromString(RepHeader); }
        if (e.SubReportName == "SubReport2") { rep.LoadFromString(RepFooter); }

        (rep.GetComponentByName("txtUserPrint") as StiText).Text = RepUser;

        rep.Dictionary.Databases.Clear();
        rep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Connection", General.ConnString));
        e.Report = rep;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public RepParametersPro GetReportInfo(RepParametersPro RepProCs)
    {
        try
        {
            DataTable DT = DBCs.FetchData(" SELECT * FROM Report WHERE RepID = @P1 ", new string[] { RepProCs.RepID });
            if (!DBCs.IsNullOrEmpty(DT))
            {
                RepProCs.RgpID = DT.Rows[0]["RgpID"].ToString();
            
                RepProCs.RepName = General.Msg(RepProCs.RepLang, DT.Rows[0]["RepNameEn"].ToString(), DT.Rows[0]["RepNameAr"].ToString());
                RepProCs.RepTemp = General.Msg(RepProCs.RepLang, DT.Rows[0]["RepTempEn"].ToString(), DT.Rows[0]["RepTempAr"].ToString());
                RepProCs.RepDesc = General.Msg(RepProCs.RepLang, DT.Rows[0]["RepDescEn"].ToString(),DT.Rows[0]["RepDescAr"].ToString());

                if (DT.Rows[0]["RepOrientation"] != DBNull.Value) { RepProCs.RepOrientation = DT.Rows[0]["RepOrientation"].ToString(); } else { RepProCs.RepOrientation = "V"; }
                RepProCs.RepPanels = DT.Rows[0]["RepPanels"].ToString();

                RepProCs.ProcedureName = DT.Rows[0]["RepProcedureName"].ToString();
                RepProCs.ProcedureParameters = DT.Rows[0]["RepParametersProc"].ToString();
            }
        }
        catch (Exception ex) { return null; }

        return RepProCs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    public RepParametersPro FillFavReportParam(string FavID, string RepID, string LoginID, string Lang, string CalendarType)
    {
        RepParametersPro RepProCs = new RepParametersPro();
        RepProCs.RepID   = RepID;
        RepProCs.RepUser = LoginID;
        RepProCs.RepLang = Lang;

        RepProCs = GetReportInfo(RepProCs);

        DataTable DT = DBCs.FetchData(" SELECT * FROM FavoriteParameters WHERE FavID = @P1 ", new string[] { FavID });
        foreach (DataRow DR in DT.Rows)
        {
            string ParmID  = DR["FParPanel"].ToString();
            string ParmVal = DR["FParValue"].ToString();

            if (ParmID == "1")    { RepProCs.Date = DTCs.GDateNow("dd/MM/yyyy"); }
            if (ParmID == "2")    { RepProCs.DateFrom  = GetFirstDay(CalendarType); /**/ RepProCs.DateTo = GetLastDay(CalendarType); }
            if (ParmID == "4")    { RepProCs.WktID = ParmVal; }
            if (ParmID == "8")    { RepProCs.MacID = ParmVal; }
            if (ParmID == "16")   { RepProCs.EmpID = ParmVal; }
            if (ParmID == "32")   { RepProCs.DepID = ParmVal; }
            if (ParmID == "64")   { RepProCs.CatID = ParmVal; }
            if (ParmID == "128")  { RepProCs.UsrName = ParmVal; }
            if (ParmID == "256")  
            { 
                RepProCs.DateFrom  = GetFirstDay(CalendarType);
                RepProCs.DateTo    = GetLastDay(CalendarType); 
                RepProCs.MonthDate = DTCs.FindCurrentMonth(CalendarType);
                RepProCs.YearDate  = DTCs.FindCurrentYear(CalendarType);
            } 
            if (ParmID == "512")  { RepProCs.Date = DTCs.GDateNow("dd/MM/yyyy"); }
            if (ParmID == "1024") { RepProCs.VtpID = ParmVal; }
            if (ParmID == "2048") { RepProCs.ExcID = ParmVal; }
            if (ParmID == "4096") { RepProCs.DaysCount = ParmVal; }
        }
        
        return RepProCs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public RepParametersPro FillMailReportParam(string RepID, string UserName, string Lang, string CalendarType)
    {           
        RepParametersPro RepProCs = new RepParametersPro();
        RepProCs.RepID   = RepID;
        RepProCs.RepUser = UserName;
        RepProCs.RepLang = Lang;

        RepProCs = GetReportInfo(RepProCs);
        int RepPanels = Convert.ToInt32(RepProCs.RepPanels);

        if (CheckBitWise(RepPanels, 1))    { RepProCs.Date     = DTCs.GDateYesterday("dd/MM/yyyy"); }
        if (CheckBitWise(RepPanels, 2))    { RepProCs.DateFrom = GetFirstDay(CalendarType); /**/ RepProCs.DateTo = GetLastDay(CalendarType); }
        if (CheckBitWise(RepPanels, 16))   { RepProCs.EmpID    = getEmployees(UserName); }
        if (CheckBitWise(RepPanels, 32))   { RepProCs.DepID    = getDepartments(UserName); }
        if (CheckBitWise(RepPanels, 128))  { RepProCs.UsrName  = UserName; }
        if (CheckBitWise(RepPanels, 256))  
        { 
            RepProCs.DateFrom  = GetFirstDay(CalendarType);
            RepProCs.DateTo    = GetLastDay(CalendarType); 
            RepProCs.MonthDate = DTCs.FindCurrentMonth(CalendarType);
            RepProCs.YearDate  = DTCs.FindCurrentYear(CalendarType);
        } 
        if (CheckBitWise(RepPanels, 512))  { RepProCs.Date     = DTCs.GDateNow("dd/MM/yyyy"); }
        if (CheckBitWise(RepPanels, 1024)) { RepProCs.VtpID    = VacationType(); }
        if (CheckBitWise(RepPanels, 2048)) { RepProCs.ExcID    = ExcuseType(); }

        return RepProCs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool CheckBitWise(int panelPermission, int permission)
    {
        if ((panelPermission | permission) == panelPermission) { return true; } else { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string GetFirstDay(string CalendarType)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (CalendarType == "Gregorian")
        {
            DateTime baseDate = DateTime.Today;
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            return thisMonthStart.ToString("dd/MM/yyyy");
        }
        else
        {
            DataTable HDT = DBCs.FetchData(new SqlCommand("SELECT GStartDate FROM HijriDates WHERE GStartDate <= GETDATE() AND GEndDate >= GETDATE()"));
            var baseDate = DateTime.Now;
            if (!DBCs.IsNullOrEmpty(HDT)) { baseDate = Convert.ToDateTime(HDT.Rows[0]["GStartDate"]); }
            return baseDate.ToString("dd/MM/yyyy");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string GetLastDay(string CalendarType)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (CalendarType == "Gregorian")
        {
            DateTime baseDate = DateTime.Today;
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            return thisMonthEnd.ToString("dd/MM/yyyy");
        }
        else
        {
            DataTable HDT = DBCs.FetchData(new SqlCommand("SELECT GEndDate FROM HijriDates WHERE GETDATE() BETWEEN GStartDate AND GEndDate "));
            var baseDate = DateTime.Now;
            if (!DBCs.IsNullOrEmpty(HDT)) { baseDate = Convert.ToDateTime(HDT.Rows[0]["GEndDate"]); }
            return baseDate.ToString("dd/MM/yyyy");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getDepartments(string UserName)
    {
        string DepList = "";
            
        DataTable DT = DBCs.FetchData(" SELECT UsrDepartments FROM AppUser WHERE UsrName = @P1 ", new string[] { UserName });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            if (DT.Rows[0]["UsrDepartments"] != DBNull.Value) 
            {
                DepList = DT.Rows[0]["UsrDepartments"].ToString();
                //DepList = CryptorEngine.Decrypt(DT.Rows[0]["UsrDepartments"].ToString(), true);
            }
        }

        return DepList;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getEmployees(string UserName)
    {
        string EmpIDs = "";

        string depIDs = getDepartments(UserName);
        if (!string.IsNullOrEmpty(depIDs))
        {
            DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT EmpID FROM Employee WHERE DepID IN (" + depIDs + ") "));
            if (!DBCs.IsNullOrEmpty(DT)) { EmpIDs = GenCs.CreateIDsString("EmpID", DT); }
        }

        return EmpIDs;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string VacationType()
    {
        return "0";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string ExcuseType()
    {
        return "0";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // mail
    protected void ExecuteProc(DateTime dt1, DateTime dt2, string EmpID, string Username, string DepID, string procName, string parProcedure) 
    {
        //string[] listParmeters = parProcedure.Split(',');

        //string iEmp = string.Empty;
        //string iDep = string.Empty;

        //SqlConnection con = new SqlConnection(Setting.DBConn);
        //SqlDataAdapter da = new SqlDataAdapter();
        //DataSet ds = new DataSet();

        //da.SelectCommand = new SqlCommand();
        //da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //da.SelectCommand.CommandText = procName;
        //da.SelectCommand.Connection = con;
        //da.SelectCommand.CommandTimeout = 6000; // second

        //foreach (string parameter in listParmeters)
        //{
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    if (parameter == "@ipFromDate") { da.SelectCommand.Parameters.Add(new SqlParameter("@ipFromDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dt1)); }
        //    if (parameter == "@ipToDate")   { da.SelectCommand.Parameters.Add(new SqlParameter("@ipToDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dt2)); }
        //    if (parameter == "@ipEmpIDSTR") 
        //    { 
        //        // remove quotes from string
        //        iEmp = EmpID.Replace("\"", string.Empty).Replace("'", string.Empty);
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipEmpIDSTR", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iEmp)); 
        //    }
        //    if (parameter == "@ipUsrName") { da.SelectCommand.Parameters.Add(new SqlParameter("@ipUsrName", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Username)); }

        //    if (parameter == "@ipDepIDSTR") 
        //    { 
        //        // remove quotes from string
        //        iDep = DepID.Replace("\"", string.Empty).Replace("'", string.Empty);
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipDepIDSTR", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iDep)); 
        //    }
        //}
        //con.Open();
        //da.SelectCommand.ExecuteNonQuery();
        //con.Close();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Rep
    public void ExecuteProc(string dt1, string dt2, string EmpID, string Username, string DepID, string procName, string parProcedure)
    {
        //string[] listParmeters = parProcedure.Split(',');

        //string iEmp = string.Empty;
        //string iDep = string.Empty;

        //SqlConnection con = new SqlConnection(General.ConnString);
        //SqlDataAdapter da = new SqlDataAdapter();
        //DataSet ds = new DataSet();

        //da.SelectCommand                = new SqlCommand();
        //da.SelectCommand.CommandType    = CommandType.StoredProcedure;
        //da.SelectCommand.CommandText    = procName;
        //da.SelectCommand.Connection     = con;
        //da.SelectCommand.CommandTimeout = 0; // second

        //foreach (string parameter in listParmeters)
        //{
        //    if (parameter == "@ipFromDate")
        //    {
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipFromDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dt1));
        //    }

        //    if (parameter == "@ipToDate")
        //    {
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipToDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dt2));
        //    }

        //    if (parameter == "@ipEmpIDSTR")
        //    {
        //        // remove quotes from string
        //        iEmp = EmpID.Replace("\"", string.Empty).Replace("'", string.Empty);
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipEmpIDSTR", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iEmp));
        //    }

        //    if (parameter == "@ipUsrName")
        //    {
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipUsrName", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Username));
        //    }

        //    if (parameter == "@ipDepIDSTR")
        //    {
        //        // remove quotes from string
        //        iDep = DepID.Replace("\"", string.Empty).Replace("'", string.Empty);
        //        da.SelectCommand.Parameters.Add(new SqlParameter("@ipDepIDSTR", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iDep));
        //    }
        //}

        //con.Open();
        //da.SelectCommand.ExecuteNonQuery();
        //con.Close();
    } 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FavReportInsert(string RepID, string LoginID, string NameEn, string NameAr, RepParametersPro RepProCs)
    {
        FavProCs.FavFormID  = RepID;
        FavProCs.FavNameAr  = NameAr;
        FavProCs.FavNameEn  = NameEn;
        FavProCs.FavUsrName = LoginID;
        FavProCs.FavType    = "R";

        FavProCs.FParPanel = "";
        FavProCs.FParValue = "";

        int RepPanels = Convert.ToInt32(RepProCs.RepPanels);
        if (CheckBitWise(RepPanels, 1))    { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "1",    ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, "D", "-"); }//Date
        if (CheckBitWise(RepPanels, 2))    { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "2",    ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, "D", "-"); }//DateFrom,DateTo
        if (CheckBitWise(RepPanels, 4))    { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "4",    ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.WktID, "-"); }
        if (CheckBitWise(RepPanels, 8))    { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "8",    ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.MacID, "-"); }
        if (CheckBitWise(RepPanels, 16))   { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "16",   ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.EmpID, "-"); }
        if (CheckBitWise(RepPanels, 32))   { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "32",   ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.DepID, "-"); }
        if (CheckBitWise(RepPanels, 64))   { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "64",   ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.CatID, "-"); }
        if (CheckBitWise(RepPanels, 128))  { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "128",  ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.UsrName, "-"); }
        if (CheckBitWise(RepPanels, 256))  { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "256",  ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, "D", "-"); } //MonthDate,YearDate
        if (CheckBitWise(RepPanels, 512))  { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "512",  ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, "D", "-"); } //TodayDate
        if (CheckBitWise(RepPanels, 1024)) { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "1024", ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.VtpID, "-"); }
        if (CheckBitWise(RepPanels, 2048)) { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "2048", ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.ExcID, "-"); }
        if (CheckBitWise(RepPanels, 4096)) { FavProCs.FParPanel = GenCs.AddString(FavProCs.FParPanel, "4096", ","); /**/ FavProCs.FParValue = GenCs.AddString(FavProCs.FParValue, RepProCs.DaysCount, "-"); }
        //if (!string.IsNullOrEmpty(RepProCs.Permissions))

        FavSqlCs.Insert(FavProCs);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}