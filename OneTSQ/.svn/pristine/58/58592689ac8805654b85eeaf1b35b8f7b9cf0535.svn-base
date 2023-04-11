using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
   
    public class YKienBenhVienService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "YKienBenhVienService";
            }
        }
        public override string ServiceId
        {
            get
            {
                return StaticServiceId;
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Danh sách ý kiến bệnh viện";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DM_YKienBenhVienFilterCls
                    YKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
                YKienBenhVienFilter.Keyword = Keyword;
                YKienBenhVienFilter.PageIndex = PageIndex;
                YKienBenhVienFilter.PageSize = 20;
                YKienBenhVienFilter.HieuLuc = (int)eHieuLuc.Co;
                int recordTotal = 0;
                DM_YKienBenhVienCls[] YKienBenhViens = CallBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().PageReading(ORenderInfo, YKienBenhVienFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[YKienBenhViens.Length];
                for (int iIndex = 0; iIndex < YKienBenhViens.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = YKienBenhViens[iIndex].Ma;
                    ORecord.items[iIndex].text = YKienBenhViens[iIndex].Ten;


                    ORecord.items[iIndex].Code = YKienBenhViens[iIndex].Ma;
                    ORecord.items[iIndex].Name = YKienBenhViens[iIndex].Ten;
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
