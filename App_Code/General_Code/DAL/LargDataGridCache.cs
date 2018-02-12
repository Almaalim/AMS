using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Data;

namespace LargDataGridView.DAL
{
    public class LargDataGridCache
    {
        public static bool isRecordsCached(string cacheKey)
        {
            Cache dbCache = HttpContext.Current.Cache;

            if (dbCache[cacheKey] == null)
                return false;

            return true;
        }

        public static void Add(string key, object value)
        {
            Cache dbCache = HttpContext.Current.Cache;

            dbCache.Add(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

        }
        public static object Get(string key)
        {
            Cache dbCache = HttpContext.Current.Cache;

            return dbCache[key];
        }

        public static object Remove(string key)
        {
            Cache dbCache = HttpContext.Current.Cache;

            return dbCache.Remove(key);
        }

        public static DataTable GetData(string cacheKey, int startRowIndex, int maximumRowNumber, string sortExpression, string searchCriteria)
        {
            Cache dbCache = HttpContext.Current.Cache;

            if (dbCache[cacheKey] != null)
            {
                DataTable dtble = dbCache[cacheKey] as DataTable;
                DataTable dtblNew = dtble.Clone();

                DataRow[] rows = dtble.Select(searchCriteria, sortExpression);

                    if (rows != null)
                    {
                        if (rows.Count() > 0)
                        {
                            if (startRowIndex > rows.Count())
                            {
                                startRowIndex = rows.Count() - maximumRowNumber;

                                if (startRowIndex < 0)
                                {
                                    startRowIndex = 1;
                                    maximumRowNumber = rows.Count();
                                }
                            }

                            for (int i = startRowIndex - 1; i < (startRowIndex + maximumRowNumber - 1); i++)
                            {
                                if (i < rows.Count())
                                    dtblNew.ImportRow(rows[i]);
                            }

                            return dtblNew;
                        }
                    }

                    return dtblNew;
               
            }

            return null;
        }
    }
}
