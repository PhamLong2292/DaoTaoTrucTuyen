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
   
    public class DmThuocService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DmThuocService";
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
                return "Danh mục thuốc";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.ThuocFilterCls ODmThuocFilter = new OneMES3.DM.Model.ThuocFilterCls();
                ODmThuocFilter.Keyword = Keyword;
                ODmThuocFilter.PageIndex = PageIndex;
                ODmThuocFilter.PageSize = 20;
                ODmThuocFilter.HieuLuc = (int)eHieuLuc.Co;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateThuocProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), ODmThuocFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var DmThuocs = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateThuocProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), ODmThuocFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DmThuocs.Length];
                for (int iIndex = 0; iIndex < DmThuocs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DmThuocs[iIndex].Id;
                    ORecord.items[iIndex].text = DmThuocs[iIndex].Ten;


                    ORecord.items[iIndex].Code = DmThuocs[iIndex].Ma;
                    ORecord.items[iIndex].Name = DmThuocs[iIndex].Ten;
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
