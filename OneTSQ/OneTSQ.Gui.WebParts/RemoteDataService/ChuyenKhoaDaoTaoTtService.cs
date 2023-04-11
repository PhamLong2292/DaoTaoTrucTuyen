using OneTSQ.Model;
using System;
using Newtonsoft.Json;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{

    public class ChuyenKhoaDaoTaoTtService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "ChuyenKhoaDaoTaoTtService";
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
                return "Service chuyên khoa đào tạo trực tuyến";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
                OChuyenKhoaDaoTaoTtFilter.Keyword = Keyword;
                OChuyenKhoaDaoTaoTtFilter.PageIndex = PageIndex;
                OChuyenKhoaDaoTaoTtFilter.PageSize = 20;
                OChuyenKhoaDaoTaoTtFilter.HieuLuc = (int)eHieuLuc.Co;                
                int recordTotal = 0;
                var ChuyenKhoaDaoTaoTts = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().PageReading(ORenderInfo, OChuyenKhoaDaoTaoTtFilter, ref recordTotal);
                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[ChuyenKhoaDaoTaoTts.Length];
                for (int iIndex = 0; iIndex < ChuyenKhoaDaoTaoTts.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = ChuyenKhoaDaoTaoTts[iIndex].Ma;
                    ORecord.items[iIndex].text = ChuyenKhoaDaoTaoTts[iIndex].Ten;


                    ORecord.items[iIndex].Code = ChuyenKhoaDaoTaoTts[iIndex].Ma;
                    ORecord.items[iIndex].Name = ChuyenKhoaDaoTaoTts[iIndex].Ten;
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
