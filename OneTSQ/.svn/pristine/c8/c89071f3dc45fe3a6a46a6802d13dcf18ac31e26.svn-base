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
   
    public class QuocGiaService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "QuocGiaService";
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
                return "Danh mục quốc gia";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.QuocGiaFilterCls OQuocGiaFilter = new OneMES3.DM.Model.QuocGiaFilterCls();
                OQuocGiaFilter.Keyword = Keyword;
                OQuocGiaFilter.PageIndex = PageIndex;
                OQuocGiaFilter.PageSize = 20;
                OQuocGiaFilter.HieuLuc = (int)eHieuLuc.Co;
                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateQuocGiaProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OQuocGiaFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var QuocGias = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateQuocGiaProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OQuocGiaFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[QuocGias.Length];
                for (int iIndex = 0; iIndex < QuocGias.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = QuocGias[iIndex].Ma;
                    ORecord.items[iIndex].text = QuocGias[iIndex].Ten;


                    ORecord.items[iIndex].Code = QuocGias[iIndex].Ma;
                    ORecord.items[iIndex].Name = QuocGias[iIndex].Ten;
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
