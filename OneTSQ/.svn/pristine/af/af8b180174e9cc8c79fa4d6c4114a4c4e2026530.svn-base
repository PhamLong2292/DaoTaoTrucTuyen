using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.Web;
using OneTSQ.Core.Model;
//using System.Runtime.Caching;

namespace OneTSQ.WebParts
{
    public class LopHocService : RemoteDataTemplate
    {
        public static string StaticServiceId { get { return "LopHocService"; } }
        public override string ServiceId { get { return StaticServiceId; } }
        public override string ServiceName { get { return "Danh sách khóa học"; } }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                long recordTotal = 0;
                DT_KhoaHocFilterCls OLopHocFilter = new DT_KhoaHocFilterCls();
                OLopHocFilter.Keyword = Keyword;
                OLopHocFilter.TrangThai = (int)DT_KhoaHocCls.eLopHocTrangThai.Moi;
                DT_KhoaHocCls[] khoaHocs = new List<DT_KhoaHocCls>().ToArray();
                khoaHocs = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().LopHocPageReading(ORenderInfo, OLopHocFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[khoaHocs.Length];
                for (int iIndex = 0; iIndex < khoaHocs.Length; iIndex++)
                {
                    DM_TenKhoaHocCls dmTenKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CreateModel(ORenderInfo, khoaHocs[iIndex].TEN);
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = khoaHocs[iIndex].ID;
                    ORecord.items[iIndex].text = dmTenKhoaHoc == null ? khoaHocs[iIndex].TEN : dmTenKhoaHoc.Ten;


                    ORecord.items[iIndex].Code = khoaHocs[iIndex].MA;
                    ORecord.items[iIndex].ShortName = khoaHocs[iIndex].MA;
                    ORecord.items[iIndex].Name = dmTenKhoaHoc == null ? khoaHocs[iIndex].TEN : dmTenKhoaHoc.Ten;
                }
                string json = JsonConvert.SerializeObject(ORecord, Formatting.Indented);
                RetAjaxOut.HtmlContent = json;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }    
    }
}
