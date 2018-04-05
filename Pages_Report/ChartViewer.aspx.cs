using System;
using Elmah;
using System.Data;
using System.Text;
using System.Web.UI;
using FusionCharts.Charts;
using System.Data.SqlClient;

public partial class Pages_Report_ChartViewer : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    PageFun   pgCs   = new PageFun();
    General   GenCs  = new General();
    DBFun     DBCs   = new DBFun();
    CtrlFun   CtrlCs = new CtrlFun();
    DTFun     DTCs   = new DTFun();
    ReportFun RepCs  = new ReportFun();

    DateTime Param_Date;
    DateTime Param_FromDate;
    DateTime Param_ToDate;
    string Param_MonthDate = "";
    string Param_YearDate  = "";
    string Param_DepIDs    = "";
    string Lang            = "En"; 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*** Fill Session ************************************/
            pgCs.FillSession();
            /*** Fill Session ************************************/
            //if (Request.QueryString["ID"] != null)
            //{
            //    string ID = Request.QueryString["ID"].ToString();
            //    if (!string.IsNullOrEmpty(ID))
            //    {
            //        string[] IDs = ID.Split('_');
            //        string FavID = IDs[0];
            //        string RepID = IDs[1];
            //        RepParametersPro RepProCs = RepCs.FillFavReportParam(FavID, RepID, pgCs.LoginID, pgCs.Lang, pgCs.DateType);
            //        Session["RepProCs"] = RepProCs;
            //    }
            //}

            if (Session["RepProCs"] == null) { Response.Redirect(@"~/Pages_Report/Reports.aspx"); }
            ShowChart();
        }
        catch (Exception ex) { ErrorSignal.FromCurrentContext().Raise(ex); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnBackToReportsPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Pages_Report/Reports.aspx?ID=" + ViewState["RgpID"].ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowChart()
    {
        if (Session["RepProCs"] != null)
        {
            RepParametersPro RepProCs = new RepParametersPro();
            RepProCs = (RepParametersPro)Session["RepProCs"];

            ViewState["RepID"] = RepProCs.RepID;
            ViewState["RgpID"] = RepProCs.RgpID;

            string RepUser = RepProCs.RepUser;
            string RepLang = RepProCs.RepLang;
            //RepOrientation = RepProCs.RepOrientation;

            /////// Fill Parameters to Report
            Param_Date      = !string.IsNullOrEmpty(RepProCs.Date)      ? DTCs.ConvertToDatetime(RepProCs.Date,     "Gregorian") : new DateTime();         
            

            if (!string.IsNullOrEmpty(RepProCs.MonthDate))
            {
                Param_MonthDate = !string.IsNullOrEmpty(RepProCs.MonthDate) ? RepProCs.MonthDate : "";
                Param_YearDate  = !string.IsNullOrEmpty(RepProCs.YearDate)  ? RepProCs.YearDate  : "";
                Param_FromDate  = !string.IsNullOrEmpty(RepProCs.DateFrom)  ? DTCs.ConvertToDatetime2(RepProCs.DateFrom, "Gregorian") : new DateTime();  
                Param_ToDate    = !string.IsNullOrEmpty(RepProCs.DateTo)    ? DTCs.ConvertToDatetime2(RepProCs.DateTo,   "Gregorian") : new DateTime();
            }
            else
            {
                Param_FromDate  = !string.IsNullOrEmpty(RepProCs.DateFrom)  ? DTCs.ConvertToDatetime(RepProCs.DateFrom, "Gregorian") : new DateTime();  
                Param_ToDate    = !string.IsNullOrEmpty(RepProCs.DateTo)    ? DTCs.ConvertToDatetime(RepProCs.DateTo,   "Gregorian") : new DateTime();
            }

            Param_DepIDs    = !string.IsNullOrEmpty(RepProCs.DepID)     ? RepProCs.DepID     : "";
            Lang = Convert.ToString(Session["Language"]) == "AR" ? "Ar" : "En";

            SelectChart(RepProCs.RepID);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SelectChart(string ChartID)
    {
		switch (ChartID)
		{
			case "Rep160101": FillChart01();     break;
			case "Rep160102": FillChart02("BL"); break;
            case "Rep160103": FillChart03();     break;
            case "Rep160104": FillChart04();     break;
            case "Rep160105": FillChart05();     break;           
            case "Rep160106": FillChart06();     break;
            case "Rep160107": FillChart07();     break;
            case "Rep160108": FillChart08();     break;
            case "Rep160109": FillChart09();     break;
            case "Rep160110": FillChart10();     break;
            case "Rep160111": FillChart11();     break;
            case "Rep160112": FillChart12();     break;
		}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart01()
    {
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

         Q.Append(" SELECT D.DepID AS DepID, D.DepNameAr AS DepNameAr, D.DepNameEn AS DepNameEn "); 
         Q.Append(" , ISNULL(C.PCount,0) PCount ");
         Q.Append(" , ISNULL(C.ACount,0) ACount ");
         Q.Append(" , ISNULL(C.VCount,0) VCount ");
         Q.Append(" , ISNULL(C.CCount,0) CCount ");
         Q.Append(" , ISNULL(C.JCount,0) JCount ");  
         Q.Append(" ,SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0)) SumReqDays ");  
         Q.Append(" ,ISNULL(CAST(ISNULL(C.PCount,0) AS DECIMAL)/NULLIF((SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0))),0) * 100,0) PRate ");  
         Q.Append(" ,ISNULL(CAST(ISNULL(C.ACount,0) AS DECIMAL)/NULLIF((SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0))),0) * 100,0) ARate ");  
         Q.Append(" ,ISNULL(CAST(ISNULL(C.VCount,0) AS DECIMAL)/NULLIF((SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0))),0) * 100,0) VRate ");  
         Q.Append(" ,ISNULL(CAST(ISNULL(C.CCount,0) AS DECIMAL)/NULLIF((SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0))),0) * 100,0) CRate ");  
         Q.Append(" ,ISNULL(CAST(ISNULL(C.JCount,0) AS DECIMAL)/NULLIF((SUM(ISNULL(C.PCount,0) + ISNULL(C.ACount,0) + ISNULL(C.VCount,0) + ISNULL(C.CCount,0) + ISNULL(C.JCount,0))),0) * 100,0) JRate ");  
         Q.Append(" FROM Department D LEFT JOIN  (SELECT DepID, COUNT(CASE WHEN (DsmStatus = 'P') THEN DepID ELSE NULL END) AS PCount "); 
         Q.Append("                                , COUNT(CASE WHEN (DsmStatus = 'A') THEN DepID ELSE NULL END) AS ACount ");               
		 Q.Append("                                , COUNT(CASE WHEN (DsmStatus IN ('UE','PE')) THEN DepID ELSE NULL END) AS VCount ");              
		 Q.Append("                                , COUNT(CASE WHEN (DsmStatus = 'CM') THEN DepID ELSE NULL END) AS CCount ");               
		 Q.Append("                                , COUNT(CASE WHEN (DsmStatus = 'JB') THEN DepID ELSE NULL END) AS JCount ");  
		 Q.Append("                           FROM Rep_EmployeeDaySummaryInfoView  WHERE DsmDate BETWEEN @Param_FromDate AND @Param_ToDate GROUP BY DepID) C  ON D.DepID = C.DepID ");  
         Q.Append(" WHERE D.DepID IN (" + Param_DepIDs + ") ");
         Q.Append(" GROUP BY D.DepID, D.DepNameAr, D.DepNameEn, ISNULL(C.PCount,0), ISNULL(C.ACount,0), ISNULL(C.VCount,0), ISNULL(C.CCount,0), ISNULL(C.JCount,0) ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            string titel = General.Msg("Attendance, absence, vacations, Commissions and Jobs Assignment Rate By Department", "نسبة الحضور و الغياب و الإجازات و الانتدابات و مهام العمل بالقسم");

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' rotateYAxisName='0' showValues='1' decimals='2' numberPrefix='%' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Departments", "الأقسام"), General.Msg("Rate", "النسبة"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();
            StringBuilder strDS3 = new StringBuilder();
            StringBuilder strDS4 = new StringBuilder();
            StringBuilder strDS5 = new StringBuilder();

            foreach(DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("DepNameEn", "DepNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}'/>", DR["PRate"].ToString());
                strDS2.AppendFormat("<set value='{0}'/>", DR["ARate"].ToString());
                strDS3.AppendFormat("<set value='{0}'/>", DR["VRate"].ToString());
                strDS4.AppendFormat("<set value='{0}'/>", DR["CRate"].ToString());
                strDS5.AppendFormat("<set value='{0}'/>", DR["JRate"].ToString());
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Attendance", "الحضور")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Vacations", "الإجازات")); /**/ strXML.Append(strDS3.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("absence", "الغياب"));    /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Commissions", "الانتدابات"));    /**/ strXML.Append(strDS4.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Jobs Assignment", "مهام العمل"));    /**/ strXML.Append(strDS5.ToString()); /**/ strXML.AppendFormat("</dataset>");

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("stackedcolumn3d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart02(string Type)
    {
        string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT D.DepID AS DepID, D.DepNameAr AS DepNameAr, D.DepNameEn AS DepNameEn, ISNULL(C.PercenBLTotal,0) PercenBLTotal, ISNULL(C.PercenBLCount,0) PercenBLCount, ISNULL(C.PercenOETotal,0) PercenOETotal, ISNULL(C.PercenOECount,0) PercenOECount ");
	    Q.Append(" FROM Department D LEFT JOIN ");
		Q.Append(" 	            (SELECT G.DepID, A.AllBLCount, A.AllBLTotal, A.AllOECount, A.AllOETotal ");
		Q.Append(" 			            ,(SUM(CASE WHEN (G.GapType = 'BL') THEN G.GapDuration ELSE 0 END) / NULLIF(CAST(A.AllBLTotal AS DECIMAL),0) * 100) PercenBLTotal "); 
		Q.Append(" 			            ,(COUNT(CASE WHEN (G.GapType = 'BL') THEN G.GapID ELSE NULL END) / NULLIF(CAST(A.AllBLCount AS DECIMAL),0) * 100) AS PercenBLCount ");
		Q.Append(" 			            ,(SUM(CASE WHEN (G.GapType = 'OE') THEN G.GapDuration ELSE 0 END) / NULLIF(CAST(A.AllOETotal AS DECIMAL),0) * 100) PercenOETotal "); 
		Q.Append(" 			            ,(COUNT(CASE WHEN (G.GapType = 'OE') THEN G.GapID ELSE NULL END) / NULLIF(CAST(A.AllOECount AS DECIMAL),0) * 100) AS PercenOECount ");
		Q.Append(" 	             FROM Rep_EmployeeGapInfoView G CROSS JOIN ");
		Q.Append(" 							(SELECT COUNT(CASE WHEN (GapType = 'BL') THEN GapID       ELSE NULL END) AS AllBLCount ");
		Q.Append(" 								   ,SUM(  CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0    END) AS AllBLTotal ");
		Q.Append(" 								   ,COUNT(CASE WHEN (GapType = 'OE') THEN GapID       ELSE NULL END) AS AllOECount ");
		Q.Append(" 								   ,SUM(  CASE WHEN (GapType = 'OE') THEN GapDuration ELSE 0    END) AS AllOETotal ");
		Q.Append(" 							 FROM Rep_EmployeeGapInfoView ");
		Q.Append(" 							 WHERE GapDate BETWEEN @Param_FromDate AND @Param_ToDate) A ");
		Q.Append(" 	             WHERE G.GapDate BETWEEN @Param_FromDate AND @Param_ToDate AND G.DepID IN (" + Param_DepIDs + ") ");
		Q.Append(" 	             GROUP BY G.DepID, A.AllBLCount, A.AllBLTotal, A.AllOECount, A.AllOETotal) C ");
		Q.Append("      ON D.DepID = C.DepID ");
        Q.Append(" WHERE D.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            //switch(Type)
            //{
            //    case "BL": titel = General.Msg("Percentage of Late attendance of departments", "نسبة الحضور المتأخر للأقسام"); break;
            //    case "OE": titel = General.Msg("Percentage of early Out of departments", "نسبة الخروج المبكر للأقسام"); break;
            //}

            titel = General.Msg("Percentage of late attendance and early out of departments", "نسبة الحضور المتأخر و الخروج المبكر للأقسام");
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' syaxisname='{3}' syaxisvaluesdecimals='2' rotateYAxisName='0' showValues='1' decimals='2' numberPrefix='%' sNumberSuffix='%' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1' plotspacepercent='0' >", titel,  General.Msg("Departments", "الأقسام"), General.Msg("Percentage of total hours", "نسبة مجموع ساعات"), General.Msg("Percentage of times", "نسبة عدد المرات"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();
            StringBuilder strDS3 = new StringBuilder();
            StringBuilder strDS4 = new StringBuilder();

            foreach (DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("DepNameEn", "DepNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}'/>", DR["PercenBLTotal"].ToString());
                strDS2.AppendFormat("<set value='{0}'/>", DR["PercenBLCount"].ToString());
                strDS3.AppendFormat("<set value='{0}'/>", DR["PercenOETotal"].ToString());
                strDS4.AppendFormat("<set value='{0}'/>", DR["PercenOECount"].ToString());
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Percentage of total hours of late attendance", "نسبة مجموع ساعات الحضور المتأخر")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}' parentyaxis='S' renderas='Line' >", General.Msg("Percentage of times of late attendance", "نسبة عدد مرات الحضور المتأخر")); /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Percentage of total hours of early out", "نسبة مجموع ساعات الخروج المبكر")); /**/ strXML.Append(strDS3.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}' parentyaxis='S' renderas='Line' >", General.Msg("Percentage of times of early out", "نسبة عدد مرات الخروج المبكر")); /**/ strXML.Append(strDS4.ToString()); /**/ strXML.AppendFormat("</dataset>");       

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("mscolumn3dlinedy", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart03()
    {
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT COUNT(CASE WHEN (A.EmpPercentWork <= 25.00) THEN D.DepID ELSE NULL END) AS Work25 "); 
        Q.Append(" ,COUNT(CASE WHEN (A.EmpPercentWork > 25.00 AND A.EmpPercentWork <= 50.00) THEN D.DepID ELSE NULL END) AS Work50 "); 
	    Q.Append(" ,COUNT(CASE WHEN (A.EmpPercentWork > 50.00 AND A.EmpPercentWork <= 75.00) THEN D.DepID ELSE NULL END) AS Work75 "); 
	    Q.Append(" ,COUNT(CASE WHEN (A.EmpPercentWork > 75.00 AND A.EmpPercentWork <= 100.00) THEN D.DepID ELSE NULL END) AS Work100 "); 
        Q.Append(" FROM Department D LEFT JOIN "); 
		Q.Append("         (SELECT DepID, (CASE WHEN MsmWorkDuration = 0 THEN NULL ELSE (CAST(MsmWorkDuration AS DECIMAL) / MsmShiftDuration * 100) END) AS EmpPercentWork	"); 					 
		Q.Append("          FROM            dbo.MonthSummaryInfoView "); 
		Q.Append("          WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) "); 
		Q.Append(" 	        AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 
		Q.Append(" 	        AND DepID IN (" + Param_DepIDs + ")) A "); 
	    Q.Append("     ON D.DepID = A.DepID "); 
        Q.Append(" WHERE D.DepID IN (" + Param_DepIDs + ") "); 

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            string titel = General.Msg("Percentage of Employees work", "نسبة عمل الموظفين");

            StringBuilder strXML = new StringBuilder();
            //strXML.AppendFormat("<chart caption='{0}' palette='4' exportEnabled='1' rotateYAxisName='0' showValues='1' showLabels='0' decimals='0' formatNumberScale='0' showborder='0' showZeroPies='1' showLegend='1' pieRadius='350' theme='fint'>", titel);
            //plottooltext=Age group : $label<br>Total visit : $datavalue' showpercentvalues='1'
            strXML.AppendFormat("<chart caption='{0}' startingangle='120' showValues='1' showlabels='1' showlegend='1' enablemultislicing='0' slicingdistance='15'  showpercentintooltip='0' pieRadius='350' theme='fint'>", titel);
            strXML.AppendFormat("<set label='{0}' value='{1}'/>", "Percentage of Employees work Smaller than 25%", ChartDT.Rows[0]["Work25"].ToString());
            strXML.AppendFormat("<set label='{0}' value='{1}'/>", "Percentage of Employees work between 25% and 50%", ChartDT.Rows[0]["Work50"].ToString());
            strXML.AppendFormat("<set label='{0}' value='{1}'/>", "Percentage of Employees work between 50% and 75%", ChartDT.Rows[0]["Work75"].ToString());
            strXML.AppendFormat("<set label='{0}' value='{1}'/>", "Percentage of Employees work between 75% and 100%", ChartDT.Rows[0]["Work100"].ToString());

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("pie3d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart04()
    {
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT D.DepID AS DepID, D.DepNameAr AS DepNameAr, D.DepNameEn AS DepNameEn, ISNULL(A.DepPercentWork,0) DepPercentWork "); 
        Q.Append(" FROM Department D LEFT JOIN "); 
	    Q.Append("       (SELECT DepID, (SUM(MsmWorkDuration) / NULLIF(CAST(SUM(MsmShiftDuration) AS DECIMAL),0) * 100) DepPercentWork ");
		Q.Append("          FROM            dbo.MonthSummaryInfoView "); 
		Q.Append("          WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) "); 
		Q.Append(" 	        AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 
		Q.Append(" 	        AND DepID IN (" + Param_DepIDs + ") "); 
		Q.Append("          GROUP BY DepID) A "); 
	    Q.Append("     ON D.DepID = A.DepID "); 
        Q.Append(" WHERE D.DepID IN (" + Param_DepIDs + ") "); 

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            string titel = General.Msg("Percentage of Departments work", "نسبة عمل الأقسام");

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' rotateYAxisName='0' showValues='1' decimals='2' numberPrefix='%' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Departments", "الأقسام"), General.Msg("Percentage of work", "نسبة العمل"));

            foreach (DataRow DR in ChartDT.Rows)
            {
                strXML.AppendFormat("<set label='{0}' value='{1}'/>", DR[General.Msg("DepNameEn", "DepNameAr")].ToString(), DR["DepPercentWork"].ToString());
            }
            
            strXML.Append("</chart>");

            //<set label="Jul" value="150000" tooltext="Occupancy: <b>67%</b>{br}Revenue: <b>$150,000{br}3 conferences hosted!</b>" /> 

            string StrChart = "";
            Chart ChartObj = new Chart("column2d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart05()
    {
        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT D.DepID AS DepID, D.DepNameAr AS DepNameAr, D.DepNameEn AS DepNameEn, ISNULL(A.DepWorkDuration,0) DepWorkDuration, ISNULL(A.DepShiftDuration,0) DepShiftDuration, ISNULL(A.DepPercentWork,0) DepPercentWork "); 
        Q.Append(" FROM Department D LEFT JOIN "); 
	    Q.Append("       (SELECT DepID, SUM(MsmWorkDuration) / 60 / 60 DepWorkDuration, SUM(MsmShiftDuration) / 60 / 60 DepShiftDuration, (SUM(MsmWorkDuration) / NULLIF(CAST(SUM(MsmShiftDuration) AS DECIMAL),0) * 100) DepPercentWork ");
		Q.Append("          FROM            dbo.MonthSummaryInfoView "); 
		Q.Append("          WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) "); 
		Q.Append(" 	        AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 
		Q.Append(" 	        AND DepID IN (" + Param_DepIDs + ") "); 
		Q.Append("          GROUP BY DepID) A "); 
	    Q.Append("     ON D.DepID = A.DepID "); 
        Q.Append(" WHERE D.DepID IN (" + Param_DepIDs + ") "); 

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            string titel = General.Msg("Percentage of Departments work", "نسبة عمل الأقسام");

            StringBuilder strXML = new StringBuilder();
            //strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' rotateYAxisName='0' showValues='1' decimals='2' numberPrefix='%' showborder='0' formatNumberScale='0' llabelDisplay='rotate' slantLabel='1' showLegend='1'>", titel,  General.Msg("Departments", "الأقسام"), General.Msg("Percentage of work", "نسبة العمل"));
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' syaxisname='{3}' syaxisvaluesdecimals='2' rotateYAxisName='0' showValues='1' decimals='2' numberPrefix='%' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Departments", "الأقسام"), General.Msg("Percentage of work", "نسبة العمل"), General.Msg("Total hours", "مجموع ساعات"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();
            StringBuilder strDS3 = new StringBuilder();

            foreach (DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("DepNameEn", "DepNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}'/>", DR["DepPercentWork"].ToString());

                strDS2.AppendFormat("<set value='{0}'/>", DR["DepShiftDuration"].ToString());
                strDS3.AppendFormat("<set value='{0}'/>", DR["DepWorkDuration"].ToString());
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Percentage of Department work", "نسبة عمل القسم")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}' parentyaxis='S' renderas='Line' >", General.Msg("Total hours of work required", "مجموع ساعات العمل المطلوبة")); /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}' parentyaxis='S' renderas='Line' >", General.Msg("Total actual working hours", "مجموع ساعات العمل الفعلية")); /**/ strXML.Append(strDS3.ToString()); /**/ strXML.AppendFormat("</dataset>");
           
            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("mscolumn3dlinedy", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart06()
    {
        string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn ");
		Q.Append("     , ISNULL(G.EmpBLCount,0) EmpBLCount, ISNULL(G.EmpBLTotalHours,0) EmpBLTotalHours ");
		Q.Append("     , ISNULL(G.EmpOECount,0) EmpOECount, ISNULL(G.EmpOETotalHours,0) EmpOETotalHours ");
		Q.Append("     , ISNULL(G.EmpMGCount,0) EmpMGCount, ISNULL(G.EmpMGTotalHours,0) EmpMGTotalHours ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("         ( SELECT EmpID, COUNT(CASE WHEN (GapType = 'BL') THEN GapID       ELSE NULL END) AS EmpBLCount ");
		Q.Append(" 	                     , SUM(  CASE WHEN (GapType = 'BL') THEN GapDuration ELSE 0    END) / 60 /60 AS EmpBLTotalHours ");
		Q.Append(" 	                     , COUNT(CASE WHEN (GapType = 'OE') THEN GapID       ELSE NULL END) AS EmpOECount ");
		Q.Append(" 	                     , SUM(  CASE WHEN (GapType = 'OE') THEN GapDuration ELSE 0    END) / 60 /60 AS EmpOETotalHours ");
		Q.Append(" 	                     , COUNT(CASE WHEN (GapType = 'MG') THEN GapID       ELSE NULL END) AS EmpMGCount ");
		Q.Append(" 	                     , SUM(  CASE WHEN (GapType = 'MG') THEN GapDuration ELSE 0    END) / 60 /60 AS EmpMGTotalHours ");
        Q.Append("          FROM Rep_EmployeeGapInfoView ");
        Q.Append("          WHERE GapDate BETWEEN @Param_FromDate AND @Param_ToDate AND DepID IN (" + Param_DepIDs + ") ");
        Q.Append("          GROUP BY EmpID) G ");
	    Q.Append("     ON E.EmpID = G.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            titel = General.Msg("Total hours of employee gaps", "مجموع ساعات الثغرات للموظف");
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' rotateYAxisName='0' showValues='1' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Employees", "الموظفون"), General.Msg("Total of hours", "مجموع الساعات"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();
            StringBuilder strDS3 = new StringBuilder();
            StringBuilder strDS4 = new StringBuilder();

            foreach (DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("EmpNameEn", "EmpNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}'/>", DR["EmpBLTotalHours"].ToString());
                strDS2.AppendFormat("<set value='{0}'/>", DR["EmpOETotalHours"].ToString());
                strDS3.AppendFormat("<set value='{0}'/>", DR["EmpMGTotalHours"].ToString());
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Total hours of late attendance", "مجموع ساعات الحضور المتأخر")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Total hours of early out", "مجموع ساعات الخروج المبكر")); /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Total hours of middle gaps", "مجموع ساعات الثغرات الوسطى")); /**/ strXML.Append(strDS3.ToString()); /**/ strXML.AppendFormat("</dataset>");

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("mscolumn2d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart07()
    {
        string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn ");
		Q.Append("      , ISNULL(EXC.EmpExcCount,0) EmpExcCount, ISNULL(EXC.EmpExcTotal,0) EmpExcTotal ");
		Q.Append("      , ISNULL(VAC.EmpVacCount,0) EmpVacCount, ISNULL(VAC.EmpVacDaysCount,0) EmpVacDaysCount ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("          (SELECT EmpID, COUNT(CASE WHEN ExcID IS NOT NULL THEN GapID ELSE NULL END) AS EmpExcCount ");
		Q.Append(" 			             , SUM(CASE WHEN ExcID IS NOT NULL THEN GapDuration ELSE 0 END) / 60 /60 AS EmpExcTotal ");
		Q.Append("           FROM Rep_EmployeeGapInfoView ");
		Q.Append("           WHERE GapDate BETWEEN @Param_FromDate AND @Param_ToDate AND DepID IN (" + Param_DepIDs + ") GROUP BY EmpID) EXC ");
	    Q.Append("      ON E.EmpID = EXC.EmpID LEFT JOIN ");
		Q.Append("          (SELECT EmpID, COUNT(*) AS EmpVacCount ");
		Q.Append(" 			             , SUM( CASE WHEN (EvrStartDate BETWEEN @Param_FromDate AND @Param_ToDate AND EvrEndDate BETWEEN @Param_FromDate AND @Param_ToDate) THEN DATEDIFF(D,EvrStartDate,EvrEndDate) + 1 ");
		Q.Append(" 						             WHEN (EvrStartDate BETWEEN @Param_FromDate AND @Param_ToDate AND EvrEndDate NOT BETWEEN @Param_FromDate AND @Param_ToDate) THEN DATEDIFF(D,EvrStartDate,@Param_ToDate) + 1 ");
		Q.Append(" 						             WHEN (EvrStartDate NOT BETWEEN @Param_FromDate AND @Param_ToDate AND EvrEndDate BETWEEN @Param_FromDate AND @Param_ToDate) THEN DATEDIFF(D,@Param_FromDate,EvrEndDate) + 1 ");
		Q.Append(" 						             WHEN (@Param_FromDate BETWEEN EvrStartDate AND EvrEndDate AND @Param_ToDate BETWEEN EvrStartDate AND EvrEndDate) THEN DATEDIFF(D,@Param_FromDate,@Param_ToDate) + 1 ");
		Q.Append(" 				           END) EmpVacDaysCount ");
		Q.Append("           FROM Rep_EmployeeVactionRelInfoView ");
		Q.Append("           WHERE @Param_ToDate >= EvrStartDate AND EvrEndDate >= @Param_FromDate AND DepID IN (" + Param_DepIDs + ") AND VtpCategory = 'VAC' GROUP BY EmpID) VAC ");
	    Q.Append("      ON E.EmpID = VAC.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            titel = General.Msg("Number of employee Excuses and Vacations", "عدد الإستئذانات و الإجازات للموظف");
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' syaxisname='{3}' syaxisvaluesdecimals='0' sformatNumberScale='0' rotateYAxisName='1' showValues='1' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Employees", "الموظفون"), General.Msg("Number of Excuses", "عدد الإستئذانات"), General.Msg("Number of Vacations", "عدد الإجازات"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();

            foreach (DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("EmpNameEn", "EmpNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}'/>", DR["EmpExcCount"].ToString());
                strDS2.AppendFormat("<set value='{0}'/>", DR["EmpVacCount"].ToString());
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset parentyaxis='P' seriesname='{0}'>", General.Msg("Number of Excuses", "عدد الإستئذانات")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset parentyaxis='S' renderas='Column' seriesname='{0}'>", General.Msg("Number of Vacations", "عدد الإجازات")); /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("mscombidy2d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart08()
    {
        //string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn, ISNULL(M.EmpPercentWork,0) EmpPercentWork ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("         (SELECT EmpID, (CASE WHEN MsmWorkDuration = 0 THEN NULL ELSE (CAST(MsmWorkDuration AS DECIMAL) / MsmShiftDuration * 100) END) AS EmpPercentWork ");				 
		Q.Append("         FROM            dbo.MonthSummaryInfoView ");
		Q.Append("         WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");
		Q.Append("         AND DepID IN (" + Param_DepIDs + ")) M ");
	    Q.Append("    ON E.EmpID = M.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            //titel = General.Msg("Percentage of actual work from required work of employees", "نسبة الدوام الفعلي من المطلوب للموظفين"); titel,  
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' exportEnabled='1' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' numbersuffix='%' chartbottommargin='25' theme='fint'>", ChartDT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), General.Msg("actual work vs required work", "الدوام الفعلي مقابل الدوام المطلوب"));
            strXML.Append("<colorrange>");
            strXML.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00' alpha='25'/>");
            strXML.Append("<color minvalue='25' maxvalue='50'  code='#e44a00' alpha='25'/>");
            strXML.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19' alpha='25'/>");
            strXML.Append("<color minvalue='75' maxvalue='100' code='#6baa01' alpha='25'/>");
            strXML.Append("</colorrange>");
            strXML.AppendFormat("<value>{0}</value>", ChartDT.Rows[0]["EmpPercentWork"].ToString());
            strXML.Append("<target>100</target>");
            strXML.Append("</chart>");
           
            //foreach (DataRow DR in ChartDT.Rows)
            //{
            //    strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("EmpNameEn", "EmpNameAr")].ToString());
            //    strDS1.AppendFormat("<set value='{0}'/>", DR["EmpExcCount"].ToString());
            //    strDS2.AppendFormat("<set value='{0}'/>", DR["EmpVacCount"].ToString());
            //}
            
           
            string StrChart = "";
            Chart ChartObj = new Chart("hbullet", " ", "100%", "100", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart09()//////////////
    {
        //string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn, ISNULL(M.EmpPercentWork,0) EmpPercentWork ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("         (SELECT EmpID, (CASE WHEN MsmWorkDuration = 0 THEN NULL ELSE (CAST(MsmWorkDuration AS DECIMAL) / MsmShiftDuration * 100) END) AS EmpPercentWork ");				 
		Q.Append("         FROM            dbo.MonthSummaryInfoView ");
		Q.Append("         WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) ");
		Q.Append("         AND DepID IN (" + Param_DepIDs + ")) M ");
	    Q.Append("    ON E.EmpID = M.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            //titel = General.Msg("Percentage of actual work from required work of employees", "نسبة الدوام الفعلي من المطلوب للموظفين"); titel,  
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' exportEnabled='1' lowerlimit='0' upperlimit='100' subcaptionfontsize='11' numbersuffix='%' chartbottommargin='25' theme='fint'>", ChartDT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), General.Msg("actual work vs required work", "الدوام الفعلي مقابل الدوام المطلوب"));
            strXML.Append("<colorrange>");
            strXML.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00' alpha='25'/>");
            strXML.Append("<color minvalue='25' maxvalue='50'  code='#e44a00' alpha='25'/>");
            strXML.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19' alpha='25'/>");
            strXML.Append("<color minvalue='75' maxvalue='100' code='#6baa01' alpha='25'/>");
            strXML.Append("</colorrange>");
            strXML.AppendFormat("<value>{0}</value>", ChartDT.Rows[0]["EmpPercentWork"].ToString());
            strXML.Append("<target>100</target>");
            strXML.Append("</chart>");
           
            //foreach (DataRow DR in ChartDT.Rows)
            //{
            //    strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("EmpNameEn", "EmpNameAr")].ToString());
            //    strDS1.AppendFormat("<set value='{0}'/>", DR["EmpExcCount"].ToString());
            //    strDS2.AppendFormat("<set value='{0}'/>", DR["EmpVacCount"].ToString());
            //}
            
           
            string StrChart = "";
            Chart ChartObj = new Chart("hbullet", " ", "100%", "100", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart10()
    {
        //string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn, D.DsmDate AS DsmDate, ISNULL(D.EmpActualWork,0) EmpActualWork, ISNULL(D.EmpActualWorkSecond,0) EmpActualWorkSecond ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("         (SELECT EmpID, DsmDate, DsmWorkDuration AS EmpActualWorkSecond, CAST(DsmWorkDuration AS DECIMAL)/ 60 / 60 AS EmpActualWork ");
		Q.Append("          FROM dbo.DaySummaryInfoView ");
		Q.Append("          WHERE DsmDate BETWEEN @Param_FromDate AND @Param_ToDate AND DsmStatus NOT IN ('WE','N') ");
		Q.Append("          AND DepID IN (" + Param_DepIDs + ")) D ");
	    Q.Append("     ON E.EmpID = D.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");
        Q.Append(" ORDER BY E.EmpID,D.DsmDate ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            //titel = General.Msg("Percentage of actual work from required work of employees", "نسبة الدوام الفعلي من المطلوب للموظفين"); titel,  
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' subcaption='{1}' palette='1' exportEnabled='1' decimals='2' showborder='0' showOpenValue='1' showCloseValue='1' numberPrefix='' bgcolor='#ffffff' theme='fint'>", ChartDT.Rows[0][General.Msg("EmpNameEn", "EmpNameAr")].ToString(), General.Msg("actual work", "الدوام الفعلي"));
            strXML.Append("<dataset>");

            DataRow[] EDRs = ChartDT.Select("EmpID = '" + ChartDT.Rows[0]["EmpID"].ToString() + "'");
            foreach (DataRow DR in EDRs)
            {
                strXML.AppendFormat("<set value='{0}' displayValue='{1}'/>", DR["EmpActualWork"].ToString(), DisplayFun.GrdDisplayDuration(DR["EmpActualWorkSecond"].ToString()));
            }

            strXML.Append("</dataset>");
            strXML.Append("</chart>");
           
            string StrChart = "";
            Chart ChartObj = new Chart("sparkline", " ", "100%", "100", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart11()
    {
        string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT E.EmpID AS EmpID, E.EmpNameAr AS EmpNameAr, E.EmpNameEn AS EmpNameEn ");
		Q.Append("         , ISNULL(D.EmpExtraTime_BeginEarly,0) AS EmpExtraTime_BeginEarly, ISNULL(D.EmpOverTime_BeginEarly,0) AS EmpOverTime_BeginEarly ");
		Q.Append("         , ISNULL(D.EmpExtraTime_OutLate,0)    AS EmpExtraTime_OutLate,    ISNULL(D.EmpOverTime_OutLate,0) AS EmpOverTime_OutLate ");
		Q.Append("         , ISNULL(D.EmpExtraTime_OutOfShift,0) AS EmpExtraTime_OutOfShift, ISNULL(D.EmpOverTime_OutOfShift,0) AS EmpOverTime_OutOfShift ");
		Q.Append("         , ISNULL(D.EmpExtraTime_NoShift,0)    AS EmpExtraTime_NoShift,    ISNULL(D.EmpOverTime_NoShift,0) AS EmpOverTime_NoShift ");
		Q.Append("         , ISNULL(D.EmpExtraTime_InVac,0)      AS EmpExtraTime_InVac,      ISNULL(D.EmpOverTime_InVac,0) AS EmpOverTime_InVac ");
        Q.Append(" FROM spActiveEmployeeView E LEFT JOIN ");
		Q.Append("         (SELECT EmpID, SUM(ISNULL(DsmExtraTimeDur_BeginEarly,0)) AS EmpExtraTime_BeginEarly, SUM(ISNULL(DsmOverTimeDur_BeginEarly,0))  AS EmpOverTime_BeginEarly ");
		Q.Append(" 			         , SUM(ISNULL(DsmExtraTimeDur_OutLate,0)) AS EmpExtraTime_OutLate, SUM(ISNULL(DsmOverTimeDur_OutLate,0))  AS EmpOverTime_OutLate ");
		Q.Append(" 			         , SUM(ISNULL(DsmExtraTimeDur_OutOfShift,0)) AS EmpExtraTime_OutOfShift, SUM(ISNULL(DsmOverTimeDur_OutOfShift,0))  AS EmpOverTime_OutOfShift ");
		Q.Append(" 			         , SUM(ISNULL(DsmExtraTimeDur_NoShift,0)) AS EmpExtraTime_NoShift, SUM(ISNULL(DsmOverTimeDur_NoShift,0))  AS EmpOverTime_NoShift ");
		Q.Append(" 			         , SUM(ISNULL(DsmExtraTimeDur_InVac,0)) AS EmpExtraTime_InVac, SUM(ISNULL(DsmOverTimeDur_InVac,0))  AS EmpOverTime_InVac ");
		Q.Append("          FROM dbo.DaySummaryInfoView ");
		Q.Append("          WHERE DsmDate BETWEEN @Param_FromDate AND @Param_ToDate AND DepID IN (" + Param_DepIDs + ") ");
		Q.Append("          GROUP BY EmpID) D ");
	    Q.Append("     ON E.EmpID = D.EmpID ");
        Q.Append(" WHERE E.DepID IN (" + Param_DepIDs + ") ");

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            titel = General.Msg("Employee Overtime", "الدوام الإضافي للموظف");
            
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' xAxisName='{1}' yAxisName='{2}' syaxisvaluesdecimals='0' sformatNumberScale='0' rotateYAxisName='1' showValues='1' showborder='0' formatNumberScale='0' showLegend='1' labelDisplay='rotate' slantLabel='1'>", titel,  General.Msg("Employees", "الموظفون"), General.Msg("Number of hours", "عدد الساعات"));

            StringBuilder strCat = new StringBuilder();
            StringBuilder strDS1 = new StringBuilder();
            StringBuilder strDS2 = new StringBuilder();

            foreach (DataRow DR in ChartDT.Rows)
            {
                strCat.AppendFormat("<category label='{0}'/>", DR[General.Msg("EmpNameEn", "EmpNameAr")].ToString());
                strDS1.AppendFormat("<set value='{0}' displayValue='{1}'/>", DisplayDuration(DR["EmpExtraTime_BeginEarly"].ToString()), DisplayFun.GrdDisplayDuration(DR["EmpExtraTime_BeginEarly"].ToString()));
                strDS2.AppendFormat("<set value='{0}' displayValue='{1}'/>", DisplayDuration(DR["EmpExtraTime_OutLate"].ToString()), DisplayFun.GrdDisplayDuration(DR["EmpExtraTime_OutLate"].ToString()));
            }
            
            strXML.Append("<categories>"); /**/ strXML.Append(strCat.ToString()); /**/ strXML.Append("</categories>");

            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("Before start work", "قبل بداية الدوام")); /**/ strXML.Append(strDS1.ToString()); /**/ strXML.AppendFormat("</dataset>");
            strXML.AppendFormat("<dataset seriesname='{0}'>", General.Msg("After end work", "بعد نهاية الدوام")); /**/ strXML.Append(strDS2.ToString()); /**/ strXML.AppendFormat("</dataset>");

            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("mscolumn2d", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillChart12()
    {
        string titel = "";

        SqlCommand cmd = new SqlCommand();
        StringBuilder Q = new StringBuilder();

        Q.Append(" SELECT SUM(ISNULL(CAST(MsmWorkDuration AS DECIMAL),0)) / SUM(ISNULL(MsmShiftDuration,0)) * 100 AS PercentWork ");		 
        Q.Append(" FROM dbo.MonthSummaryInfoView ");
        Q.Append(" WHERE CONVERT(VARCHAR(12),MsmStartDate,103) = CONVERT(VARCHAR(12),@Param_FromDate,103) ");
 	    Q.Append(" AND CONVERT(VARCHAR(12),MsmEndDate,103) = CONVERT(VARCHAR(12),@Param_ToDate,103) "); 

        cmd.Parameters.AddWithValue("@Param_FromDate", Param_FromDate);
        cmd.Parameters.AddWithValue("@Param_ToDate"  , Param_ToDate);
       
        cmd.CommandText = Q.ToString();

        DataTable ChartDT = new DataTable();
        ChartDT = DBCs.FetchData(cmd);
        if (!DBCs.IsNullOrEmpty(ChartDT))
        {
            titel = General.Msg("General measure of the work of the establishment", "المقياس العام لدوام المنشأة");

            //gaugeFillMix='{dark-30},{light-60},{dark-10}' gaugeFillRatio='15' 
            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<chart caption='{0}' palette='1' exportEnabled='1' lowerlimit='0' upperlimit='100' showValue='1' valueBelowPivot='1' theme='fint'>", titel);
            strXML.Append("<colorrange>");
            strXML.Append("<color minvalue='0'  maxvalue='25'  code='#e44a00'/>");
            strXML.Append("<color minvalue='25' maxvalue='50'  code='#e44a00'/>");
            strXML.Append("<color minvalue='50' maxvalue='75'  code='#f8bd19'/>");
            strXML.Append("<color minvalue='75' maxvalue='100' code='#6baa01'/>");
            strXML.Append("</colorrange>");
            strXML.AppendFormat("<dials><dial value='{0}'/></dials>", ChartDT.Rows[0]["PercentWork"].ToString());
            strXML.Append("</chart>");

            string StrChart = "";
            Chart ChartObj = new Chart("angulargauge", " ", "100%", "600", "xml", strXML.ToString());
            StrChart = ChartObj.Render();
            //pnlChartViewer.Controls.Clear();
            //pnlChartViewer.Controls.Add(new LiteralControl(StrChart));
            litChartViewer.Text = StrChart;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string DisplayDuration(object pDuration)
    {
        try
        {
            if (pDuration == DBNull.Value) { return "0"; }
            if (string.IsNullOrEmpty(pDuration.ToString())) { return "0"; }

            TimeSpan tsGen = TimeSpan.FromSeconds(Convert.ToDouble(pDuration));
            int Hours = tsGen.Hours;
            if (tsGen.Days > 0) { Hours = (tsGen.Days * 24) + tsGen.Hours; }
            return Hours.ToString("00");// +tsGen.Minutes.ToString("00") + tsGen.Seconds.ToString("00");
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "00:00:00";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}