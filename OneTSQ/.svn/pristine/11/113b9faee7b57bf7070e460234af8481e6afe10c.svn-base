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

    public class KyThuatChuyenGiaoService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "KyThuatChuyenGiaoService";
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
                return "Service danh mục kỹ thuật chuyển giao";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter = new DM_KyThuatChuyenGiaoFilterCls();
                OKyThuatChuyenGiaoFilter.Keyword = Keyword;
                OKyThuatChuyenGiaoFilter.PageIndex = PageIndex;
                OKyThuatChuyenGiaoFilter.PageSize = 20;
                OKyThuatChuyenGiaoFilter.HieuLuc = (int)eHieuLuc.Co;
                int recordTotal = 0;
                var kyThuatChuyenGiaos = CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().PageReading(ORenderInfo, OKyThuatChuyenGiaoFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[kyThuatChuyenGiaos.Length];
                
                for (int iIndex = 0; iIndex < kyThuatChuyenGiaos.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = kyThuatChuyenGiaos[iIndex].Ma;
                    ORecord.items[iIndex].text = kyThuatChuyenGiaos[iIndex].Ten;

                    ORecord.items[iIndex].Code = kyThuatChuyenGiaos[iIndex].Ma;
                    ORecord.items[iIndex].Name = kyThuatChuyenGiaos[iIndex].Ten;
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
