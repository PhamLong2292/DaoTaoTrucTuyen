using OneTSQ.Model;
using System;
using Newtonsoft.Json;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{

    public class ChuyenKhoaService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "ChuyenKhoaService";
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
                return "Service chuyên khoa";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.ChuyenKhoaFilterCls
                    OChuyenKhoaFilter = new OneMES3.DM.Model.ChuyenKhoaFilterCls();
                OChuyenKhoaFilter.Keyword = Keyword;
                OChuyenKhoaFilter.PageIndex = PageIndex;
                OChuyenKhoaFilter.PageSize = 20;
                OChuyenKhoaFilter.HieuLuc = (int)eHieuLuc.Co;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OChuyenKhoaFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var ChuyenKhoas = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OChuyenKhoaFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[ChuyenKhoas.Length];
                for (int iIndex = 0; iIndex < ChuyenKhoas.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = ChuyenKhoas[iIndex].Ma;
                    ORecord.items[iIndex].text = ChuyenKhoas[iIndex].Ten;


                    ORecord.items[iIndex].Code = ChuyenKhoas[iIndex].Ma;
                    ORecord.items[iIndex].Name = ChuyenKhoas[iIndex].Ten;
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
