using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    public static class Utilities
    {
        public static DataTable PivotToDataSource<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> rowSelector, Func<TSource, TSecondKey> columnSelector, Func<IEnumerable<TSource>, TValue> valueSelector, string rowHeader)
        {
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add(rowHeader, typeof(string));

            var columns = source.ToLookup(columnSelector);
            var colType = (((Delegate)(valueSelector)).Method).ReturnType;
            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                colType = colType.GetGenericArguments()[0];
            }

            foreach (var c in columns)
            {
                returnTable.Columns.Add(c.Key.ToString(), colType);
            }

            var rows = source.ToLookup(rowSelector);
            foreach (var r in rows)
            {
                DataRow drReturn = returnTable.NewRow();
                drReturn[0] = r.Key;

                var subColumns = r.ToLookup(columnSelector);
                foreach (var sc in subColumns)
                {
                    drReturn[sc.Key.ToString()] = valueSelector(sc);
                }
                returnTable.Rows.Add(drReturn);
            }
            return returnTable;
        }

        public static byte[] ToData(this System.Drawing.Image image, System.Drawing.Imaging.ImageFormat imageFormat = null)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                image.Save(stream, imageFormat ?? System.Drawing.Imaging.ImageFormat.Bmp);
                return stream.ToArray();
            }
        }
    }
}
