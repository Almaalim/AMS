using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

public class DBFun : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General Gen = new General();
    SqlDataAdapter da = new SqlDataAdapter();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool isConnect()
    {
        string Conn = General.ConnString;
        System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(Conn);
        try
        {
            using (sqlConn)
            {
                sqlConn.Open();
                sqlConn.Close();
                sqlConn.Dispose();
                return true;
            }
        }
        catch (Exception ex)
        {
            sqlConn.Close();
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OpenCon()
    {
        try
        {
            if (MainConnection.State != ConnectionState.Open) { MainConnection.Open(); }
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CloseCon()
    {
        try
        {
            if (MainConnection.State == ConnectionState.Open) { MainConnection.Close(); }
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public DataTable FetchData(SqlCommand cmd)
    {
        try
        {
            OpenCon();
            cmd.CommandType = CommandType.Text;
            cmd.Connection  = MainConnection;
            
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            //CloseCon();
            return FDT;
        }
        catch (Exception ex) 
        { 
            string s = cmd.CommandText;
            throw ex; 
        }
        finally 
        { 
            CloseCon(); 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchData(string Query, string[] Param)
    {
        try
        {
            if (string.IsNullOrEmpty(Query) || Param.Length <= 0) { return new DataTable(); }

            SqlCommand cmd = new SqlCommand(Query);     
            for (int i = 0; i < Param.Length; i++) { cmd.Parameters.AddWithValue("@P" + (i+1).ToString(), Param[i]); }
            
            OpenCon();
            cmd.CommandType = CommandType.Text;
            cmd.Connection  = MainConnection;
            
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            return FDT;
        }
        catch (Exception ex) { throw ex; }
        finally 
        { 
            CloseCon(); 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchData(string Query, string[] Param, string[] AParam)
    {
        try
        {
            if (string.IsNullOrEmpty(Query) || AParam.Length <= 0) { return new DataTable(); }

            SqlCommand cmd = new SqlCommand(Query);     
            for (int i = 0; i < Param.Length; i++) { cmd.Parameters.AddWithValue("@P" + (i+1).ToString(), Param[i]); }

            cmd.AddArrayParameters(AParam, "@AP");

            OpenCon();
            cmd.CommandType = CommandType.Text;
            cmd.Connection  = MainConnection;
            
            string s = cmd.CommandText;

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            return FDT;
        }
        catch (Exception ex) { throw ex; }
        finally 
        { 
            CloseCon(); 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchDataQuery(string pQuery) //
    {
       try
        {
            if (string.IsNullOrEmpty(pQuery)) { return new DataTable(); }
            if (pQuery.Contains(';'))         { return new DataTable(); }
            
            da.SelectCommand = new SqlCommand(pQuery, MainConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.SelectCommand.Parameters.Add(param);
            
            OpenCon();

            SqlDataReader dr = da.SelectCommand.ExecuteReader(); //CommandBehavior.CloseConnection
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            dr.Dispose();
            CloseCon();
            return FDT;
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchData(string pQuery)
    {
        try
        {
            if (string.IsNullOrEmpty(pQuery)) { return new DataTable(); }
            if (pQuery.Contains(';'))         { return new DataTable(); }
            
            da.SelectCommand = new SqlCommand("FetchData", MainConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.SelectCommand.Parameters.Add(param);
            
            OpenCon();

            SqlDataReader dr = da.SelectCommand.ExecuteReader(); //CommandBehavior.CloseConnection
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            dr.Dispose();
            CloseCon();
            return FDT;
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int ExecuteData(string pQuery)
    {
        try
        {
            if (string.IsNullOrEmpty(pQuery)) { return 0; }

            da.InsertCommand = new SqlCommand("ExecuteData", MainConnection);
            da.InsertCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.InsertCommand.Parameters.Add(param);

            OpenCon();
            int rowsAffected = da.InsertCommand.ExecuteNonQuery();
            CloseCon();
            return rowsAffected;
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchProcedureData(string pProcName)
    {
        try
        {
            da.SelectCommand = new SqlCommand(pProcName, MainConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            OpenCon();
            SqlDataReader dr = da.SelectCommand.ExecuteReader(); //CommandBehavior.CloseConnection
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            dr.Dispose();
            CloseCon();
            return FDT;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataTable FetchProcedureData(string pProcName, string[] ParamName, string[] ParamValue)
    {
        try
        {
            da.SelectCommand = new SqlCommand(pProcName, MainConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < ParamName.Length; i++) { da.SelectCommand.Parameters.AddWithValue(ParamName[i], ParamValue[i]); }
            OpenCon();
            SqlDataReader dr = da.SelectCommand.ExecuteReader(); //CommandBehavior.CloseConnection
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            dr.Dispose();
            CloseCon();
            return FDT;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet GetData(string Query, string[] Param)
    {
        DataSet   DS = new DataSet();
        DataTable DT = new DataTable();
        try
        {
            if (Param == null) { DT = FetchData(new SqlCommand(Query)); } else { DT = FetchData(Query, Param); }
            
            if (!IsNullOrEmpty(DT))
            {
                DS.Tables.Add(DT);
                DS.Tables[0].TableName = "Data";
            }
            else
            {
                DS = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message, ex); }

        return DS;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsNullOrEmpty(DataTable dt)
    {
        if (dt == null)         { return true; }
        if (dt.Rows.Count == 0) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchMenuData(string pQuery)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(pQuery, MainConnection);

        da.Fill(ds);
        da.Dispose();

        ds.DataSetName = "Menus";
        ds.Tables[0].TableName = "Menu";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Menu"].Columns["MnuID"], ds.Tables["Menu"].Columns["MnuParentID"], false);
        relation.Nested = true;
        ds.Relations.Add(relation);

        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchReportData(string pQuery)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(pQuery, MainConnection);
        da.Fill(ds);
        da.Dispose();
        ds.DataSetName = "Menus";
        ds.Tables[0].TableName = "Menu";
        DataRelation relation = new DataRelation("ParentChild",ds.Tables["Menu"].Columns["RepID"],ds.Tables["Menu"].Columns["RgpID"],false);

        relation.Nested = true;
        ds.Relations.Add(relation);

        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchDepartmentData(string pQuery)
    {
        DataSet ds = new DataSet();

        SqlDataAdapter da = new SqlDataAdapter(pQuery, MainConnection);
        da.Fill(ds);
        da.Dispose();
        ds.DataSetName = "Departments";
        ds.Tables[0].TableName = "Department";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Department"].Columns["DepID"], ds.Tables["Department"].Columns["DepParentID"], false);

        relation.Nested = true;
        ds.Relations.Add(relation);

        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CopyDataToTransDump(DataTable DT)
    {
        SqlBulkCopy bulkcopy = new SqlBulkCopy(MainConnection);
        OpenCon();
        
        bulkcopy.DestinationTableName = "dbo.TransDump";
        bulkcopy.ColumnMappings.Add("TrnDate", "TrnDate");
        bulkcopy.ColumnMappings.Add("TrnTime", "TrnTime");
        bulkcopy.ColumnMappings.Add("EmpID"  , "EmpID");
        bulkcopy.ColumnMappings.Add("MacID"  , "MacID");
        bulkcopy.ColumnMappings.Add("TrnType", "TrnType");

        try { bulkcopy.WriteToServer(DT); } catch (Exception ex) { throw new Exception(ex.Message, ex); }

        bulkcopy.Close();
        CloseCon();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}