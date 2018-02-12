using System;
using System.Data;

namespace LargDataGridView.DAL
{
    public class LargDataGrid
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static DBFun DBCs = new DBFun();

        //private const string CACHE_KEY = "CUSTOMER_DATA";
        //private const string COUNT_CACHE_KEY = "CUSTOMER_COUNT";
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DataTable GetDataSortedPage(int maximumRows, int startRowIndex, string sortExpression, string searchCriteria, string Refresh, string CacheKey, string DataID, string sortID, out DataTable DT)
        {
            string CACHE_KEY = CacheKey + "_DATA";
            string COUNT_CACHE_KEY = CacheKey + "_COUNT";

            if (Refresh == "T") { LargDataGridCache.Remove(CACHE_KEY); }

            if (string.IsNullOrEmpty(sortExpression)) { sortExpression = sortID; }

            try
            {
                if (LargDataGridCache.isRecordsCached(CACHE_KEY))
                {
                    DataTable DT1 = LargDataGridCache.GetData(CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, searchCriteria);
                    DT = DT1;
                    return DT1;
                }
                    
                string sql = "SELECT * FROM " + DataID + " ";
                if (!string.IsNullOrEmpty(searchCriteria)) { sql += " WHERE " + searchCriteria; }

                DataTable DataDT = new DataTable();
                DataDT = DBCs.FetchData(sql);

                //Cache records
                LargDataGridCache.Add(CACHE_KEY, DataDT);
            }
            catch (Exception e) { throw; }

            DataTable DT2 = LargDataGridCache.GetData(CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, null);
            DT = DT2;
            return DT2;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int GetDataCount(string searchCriteria, string Refresh, string CacheKey, string DataID, string sortID, out DataTable DT)
        {
            DT = null;
            int iCount = 0;
            string CACHE_KEY = CacheKey + "_DATA";
            string COUNT_CACHE_KEY = CacheKey + "_COUNT";

            //if (Refresh == "T") { LargDataGridCache.Remove(CACHE_KEY); }

            try
            {
                string sql = " SELECT COUNT(*) iCount FROM " + DataID + " ";
                if (!string.IsNullOrEmpty(searchCriteria)) { sql = sql + " WHERE " + searchCriteria; }
                DataTable DataDT = new DataTable();
                DataDT = DBCs.FetchData(sql);

                if (!DBCs.IsNullOrEmpty(DataDT)) { iCount = Convert.ToInt32(DataDT.Rows[0][0]);}

                if (LargDataGridCache.Get(COUNT_CACHE_KEY) != null)
                {
                    // remove customers data if customers count has changed since first cache
                    if (Convert.ToInt32(LargDataGridCache.Get(COUNT_CACHE_KEY)) != iCount && string.IsNullOrEmpty(searchCriteria))
                    {
                        LargDataGridCache.Remove(CACHE_KEY);
                    }
                }

                if (string.IsNullOrEmpty(searchCriteria)) { LargDataGridCache.Add(COUNT_CACHE_KEY, iCount); }
            }
            catch (Exception e) { throw; }

            return iCount;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DataTable GetAspLogDataSortedPage(int maximumRows, int startRowIndex, string sortExpression, string searchCriteria, string Refresh, string CacheKey, string DataID, string sortID, out DataTable DT)
        {
            string CACHE_KEY = CacheKey + "_DATA";
            string COUNT_CACHE_KEY = CacheKey + "_COUNT";

            if (Refresh == "T") { LargDataGridCache.Remove(CACHE_KEY); }

            if (string.IsNullOrEmpty(sortExpression)) { sortExpression = sortID; }

            try
            {
                if (LargDataGridCache.isRecordsCached(CACHE_KEY))
                {
                    DataTable DT1 = LargDataGridCache.GetData(CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, searchCriteria);
                    DT = DT1;
                    return DT1;
                }
                    
                string sql = "SELECT ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence FROM " + DataID + " ";
                if (!string.IsNullOrEmpty(searchCriteria)) { sql += " WHERE " + searchCriteria; }

                DataTable DataDT = new DataTable();
                DataDT = DBCs.FetchData(sql);

                //Cache records
                LargDataGridCache.Add(CACHE_KEY, DataDT);
            }
            catch (Exception e) { throw; }

            DataTable DT2 = LargDataGridCache.GetData(CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, null);
            DT = DT2;
            return DT2;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
