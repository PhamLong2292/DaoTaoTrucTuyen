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
   
    public class DmDichVuService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DmDichVuService";
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
