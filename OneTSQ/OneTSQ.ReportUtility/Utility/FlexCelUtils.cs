using FlexCel.Report;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    public static class FlexCelUtils
    {
        #region Dump

        private static readonly SynchronizedDictionary<Type, Func<object, object>[]> GetterMap = new SynchronizedDictionary<Type, Func<object, object>[]>(10);

        public static Func<object, object>[] GetGetters<T>(string[] fields)
        {
            return FlexCelUtils.GetterMap.GetValueOrAdd(typeof(T), k => fields.Select(field => DynamicDelegates.CreateObjectPropertyGet<Func<object, object>>(k, field)).ToArray());
        }

        /// <summary>
        /// Chuyển toàn bộ các trường của View (view xác định kiểu) vào report dưới dạng SetValue
        /// Với từng trường theo tên mẫu [TênView]_[TênTrường]
        /// Nếu view bị null thì các trường của view vẫn được đưa vào dưới giá trị NULL mặc định
        /// phục vụ việc chấp nhận giá trị rỗng ở trong báo cáo
        /// </summary>
        public static DbDataRecord DumpObject(this FlexCelReport report, string objectname, IList<DbDataRecord> objectquery)
        {
            var rc = objectquery.SingleOrDefault();
            if (rc != null)
            {
                objectname = objectname + "_";
                for (int i = 0, l = rc.FieldCount; i < l; i++)
                    report.SetValue(objectname + rc.GetName(i), rc.GetValue(i));
            }
            return rc;
        }

        /// <summary>
        /// Chuyển toàn bộ các trường của View (view xác định kiểu) vào report dưới dạng SetValue
        /// Với từng trường theo tên mẫu [TênView]_[TênTrường]
        /// Nếu view bị null thì các trường của view vẫn được đưa vào dưới giá trị NULL mặc định
        /// phục vụ việc chấp nhận giá trị rỗng ở trong báo cáo
        /// </summary>
        public static T DumpObject<T>(this FlexCelReport report, string objectname, IList<T> objectquery)
        {
            var t = objectquery.SingleOrDefault();
            if (t != null)
            {
                var columnNames = t.GetType().GetMembers().ToList().OfType<PropertyInfo>().Select(m => m.Name).ToArray();
                var getters = FlexCelUtils.GetGetters<T>(columnNames);
                objectname = objectname + "_";
                for (var i = 0; i < columnNames.Length; i++)
                    report.SetValue(objectname + columnNames[i], getters[i](t));
            }
            return t;
        }

        /// <summary>
        /// Chuyển toàn bộ các trường của View (view xác định kiểu) vào report dưới dạng SetValue
        /// Với từng trường theo tên mẫu [TênView]_[TênTrường]
        /// Nếu view bị null thì các trường của view vẫn được đưa vào dưới giá trị NULL mặc định
        /// phục vụ việc chấp nhận giá trị rỗng ở trong báo cáo
        /// </summary>
        public static T DumpObjectOrDefault<T>(this FlexCelReport report, string objectname, IList<T> objectquery)
        {
            var t = objectquery.SingleOrDefault();
            var columnNames = t.GetType().GetMembers().ToList().OfType<PropertyInfo>().Select(m => m.Name).ToArray();
            var getters = FlexCelUtils.GetGetters<T>(columnNames);
            objectname = objectname + "_";
            if (t != null)
            {
                for (var i = 0; i < columnNames.Length; i++)
                    report.SetValue(objectname + columnNames[i], getters[i](t));
            }
            else
            {
                for (var i = 0; i < columnNames.Length; i++)
                    report.SetValue(objectname + columnNames[i], null);
            }
            return t;
        }

        public static System.Drawing.Bitmap ExportImageNext(this global::FlexCel.Render.FlexCelImgExport flexCelImgExport, ref global::FlexCel.Render.TImgExportInfo exportInfo, float xCrop = 0F, float yCrop = 0F, float? zoom = null, System.Drawing.Color? backgroundColor = null, System.Drawing.Drawing2D.InterpolationMode? interpolationMode = null, System.Drawing.Drawing2D.SmoothingMode? smoothingMode = null)
        {
            var pagebounds = exportInfo.ActiveSheet.PageBounds;
            System.Drawing.Bitmap bitmap;
            if (zoom == null)
            {
                bitmap = global::FlexCel.Draw.GdipBitmapConstructor.CreateBitmap((int)(pagebounds.Width - xCrop), (int)(pagebounds.Height - yCrop));
                bitmap.SetResolution(96, 96);
            }
            else
            {
                bitmap = global::FlexCel.Draw.GdipBitmapConstructor.CreateBitmap((int)((pagebounds.Width - xCrop) * zoom.Value), (int)((pagebounds.Height - yCrop) * zoom.Value));
                bitmap.SetResolution(96 * zoom.Value, 96 * zoom.Value);
            }

            try
            {
                using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    graphics.Clear(backgroundColor ?? System.Drawing.Color.White);
                    if (interpolationMode != null)
                        graphics.InterpolationMode = interpolationMode.Value;
                    if (smoothingMode != null)
                        graphics.SmoothingMode = smoothingMode.Value;
                    flexCelImgExport.ExportNext(graphics, ref exportInfo);
                }
                return bitmap;
            }
            catch
            {
                bitmap.Dispose();
                throw;
            }
        }
        #endregion
    }
}
