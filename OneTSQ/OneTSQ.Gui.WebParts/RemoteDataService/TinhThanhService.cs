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

    public class TinhThanhService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "TinhThanhService";
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
                return "Danh mục tỉnh thành";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.TinhThanhFilterCls
                    OTinhThanhFilter = new OneMES3.DM.Model.TinhThanhFilterCls();
                OTinhThanhFilter.Keyword = Keyword;
                OTinhThanhFilter.PageIndex = PageIndex;
                OTinhThanhFilter.PageSize = 20;
                OTinhThanhFilter.HieuLuc = (int)eHieuLuc.Co;

                long recordTotal = 0;
                var tinhThanhs = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTinhThanhProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OTinhThanhFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[tinhThanhs.Length];
                for (int iIndex = 0; iIndex < tinhThanhs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = tinhThanhs[iIndex].Ma;
                    ORecord.items[iIndex].text = tinhThanhs[iIndex].Ten;


                    ORecord.items[iIndex].Code = tinhThanhs[iIndex].Ma;
                    ORecord.items[iIndex].Name = tinhThanhs[iIndex].Ten;
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
