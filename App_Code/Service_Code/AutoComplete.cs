using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    
    string departmentList = string.Empty;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public AutoComplete() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmployeeIDList(string prefixText, int count)
    {
        if (Session["DepartmentList"] != null) { departmentList = Session["DepartmentList"].ToString(); } else { departmentList = "0"; }
        string lang = "En";
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "AR") { lang = "Ar"; } else { lang = "En"; } }

        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpID + '-' + EmpName" + lang +" EmpName ");
        Q.Append(" FROM Employee ");
        Q.Append(" WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
        Q.Append(" AND EmpID LIKE '" + prefixText + "%'");
        Q.Append(" AND DepID IN (" + departmentList + ") ");

        List<string> items = new List<string>(count);
        DataTable DT = DBCs.FetchData(new SqlCommand(Q.ToString()));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            for (int i = 0; i < DT.Rows.Count; i++) { items.Add(DT.Rows[i]["EmpName"].ToString()); }
        }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmployeeIDListWithCon(string prefixText, int count)
    {
        if (Session["DepartmentList"] != null) { departmentList = Session["DepartmentList"].ToString(); } else { departmentList = "0"; }
        string lang = "En";
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "AR") { lang = "Ar"; } else { lang = "En"; } }

        string EmpCon = "";
        if (Session["EmpConSelect"] != null) { if (!string.IsNullOrEmpty(Session["EmpConSelect"].ToString())) { EmpCon = Session["EmpConSelect"].ToString(); } } 

        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpID + '-' + EmpName" + lang +" EmpName ");
        Q.Append(" FROM Employee ");
        Q.Append(" WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
        Q.Append(" AND EmpID LIKE '" + prefixText + "%'");
        Q.Append(" AND DepID IN (" + departmentList + ") ");
        Q.Append(" " + EmpCon + " ");

        List<string> items = new List<string>(count);
        DataTable DT = DBCs.FetchData(new SqlCommand(Q.ToString()));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            for (int i = 0; i < DT.Rows.Count; i++) { items.Add(DT.Rows[i]["EmpName"].ToString()); }
        }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmployeeNameList(string prefixText, int count)
    {
        if (Session["DepartmentList"] != null) { departmentList = Session["DepartmentList"].ToString(); } else { departmentList = "0"; }
        string lang = "En";
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "AR") { lang = "Ar"; } else { lang = "En"; } }

        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpName" + lang +" + '-' + EmpID EmpName ");
        Q.Append(" FROM Employee ");
        Q.Append(" WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
        Q.Append(" AND EmpName" + lang +" LIKE '%" + prefixText + "%'");
        Q.Append(" AND DepID IN (" + departmentList + ") ");

        List<string> items = new List<string>(count);
        DataTable DT = DBCs.FetchData(new SqlCommand(Q.ToString()));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            for (int i = 0; i < DT.Rows.Count; i++) { items.Add(DT.Rows[i]["EmpName"].ToString()); }
        }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmployeeNameListWithCon(string prefixText, int count)
    {
        if (Session["DepartmentList"] != null) { departmentList = Session["DepartmentList"].ToString(); } else { departmentList = "0"; }
        string lang = "En";
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "AR") { lang = "Ar"; } else { lang = "En"; } }

        string EmpCon = "";
        if (Session["EmpConSelect"] != null) { if (!string.IsNullOrEmpty(Session["EmpConSelect"].ToString())) { EmpCon = Session["EmpConSelect"].ToString(); } } 

        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpName" + lang +" + '-' + EmpID EmpName ");
        Q.Append(" FROM Employee ");
        Q.Append(" WHERE EmpStatus ='True' AND ISNULL(EmpDeleted, 0) = 0 ");
        Q.Append(" AND EmpName" + lang +" LIKE '%" + prefixText + "%'");
        Q.Append(" AND DepID IN (" + departmentList + ") ");
        Q.Append(" " + EmpCon + " ");

        List<string> items = new List<string>(count);
        DataTable DT = DBCs.FetchData(new SqlCommand(Q.ToString()));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            for (int i = 0; i < DT.Rows.Count; i++) { items.Add(DT.Rows[i]["EmpName"].ToString()); }
        }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetDepNameList(string prefixText, int count)
    {
        if (Session["DepartmentList"] != null) { departmentList = Session["DepartmentList"].ToString(); } else { departmentList = "0"; }
        string lang = "En";
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "AR") { lang = "Ar"; } else { lang = "En"; } }

        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT DepName" + lang +" + '-' + CONVERT(VARCHAR(100),DepID) DepName FROM Department WHERE ISNULL(DepDeleted,0) = 0 AND DepID IN (" + departmentList + ") AND DepName" + lang +" LIKE '%" + prefixText + "%' ");

        List<string> items = new List<string>(count);
        DataTable DT = DBCs.FetchData(new SqlCommand(Q.ToString()));
        if (!DBCs.IsNullOrEmpty(DT)) 
        {
            for (int i = 0; i < DT.Rows.Count; i++) { items.Add(DT.Rows[i]["DepName"].ToString()); }
        }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
