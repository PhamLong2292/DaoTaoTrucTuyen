using Newtonsoft.Json;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Model;
using System;

namespace OneTSQ.WebParts
{
    public class DmDichVuXNService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DmDichVuXNService";
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
                return "Danh mục dịch vụ";
            }
        }

        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.DichVuFilterCls ODmDichVuFilter = new OneMES3.DM.Model.DichVuFilterCls();
                ODmDichVuFilter.Keyword = Keyword;
                ODmDichVuFilter.PageIndex = PageIndex;
                ODmDichVuFilter.PageSize = 20;
                ODmDichVuFilter.HieuLuc = (int)eHieuLuc.Co;
                ODmDichVuFilter.NhomLopDichVu = (int)eNhomLopDichVu.XetNghiem;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDichVuProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), ODmDichVuFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var DmDichVus = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDichVuProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), ODmDichVuFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DmDichVus.Length];
                for (int iIndex = 0; iIndex < DmDichVus.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DmDichVus[iIndex].Ma;
                    ORecord.items[iIndex].text = DmDichVus[iIndex].Ten;

                    ORecord.items[iIndex].Code = DmDichVus[iIndex].Ma;
                    ORecord.items[iIndex].Name = DmDichVus[iIndex].Ten;
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