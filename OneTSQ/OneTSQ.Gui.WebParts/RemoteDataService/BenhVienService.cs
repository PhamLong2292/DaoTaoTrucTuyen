using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using System;
using Newtonsoft.Json;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{

    public class BenhVienService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "BenhVienService";
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
                return "Danh mục bệnh viện";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.BenhVienFilterCls OBenhVienFilter = new OneMES3.DM.Model.BenhVienFilterCls();
                OBenhVienFilter.Keyword = Keyword;
                OBenhVienFilter.PageIndex = PageIndex;
                OBenhVienFilter.PageSize = 20;
                OBenhVienFilter.HieuLuc = (int)eHieuLuc.Co;
                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OBenhVienFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var BenhViens = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OBenhVienFilter, ref recordTotal);
                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[BenhViens.Length];
                for (int iIndex = 0; iIndex < BenhViens.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = BenhViens[iIndex].Id;
                    ORecord.items[iIndex].text = BenhViens[iIndex].Ten;


                    ORecord.items[iIndex].Code = BenhViens[iIndex].Ma;
                    ORecord.items[iIndex].Name = BenhViens[iIndex].Ten;
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
