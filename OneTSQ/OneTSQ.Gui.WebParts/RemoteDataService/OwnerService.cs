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
   
    public class OwnerService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "OwnerService";
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
                return "Danh mục owner";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OwnerFilterCls
                    OwnerFilter = new OwnerFilterCls();
                OwnerFilter.Keyword = Keyword;
                OwnerFilter.PageIndex = PageIndex;

                OwnerCls[]
                    Owners = Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().Reading(ORenderInfo, OwnerFilter);

                Record ORecord = new Record();
                ORecord.total_count = Owners.Length;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[Owners.Length];
                for (int iIndex = 0; iIndex < Owners.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = Owners[iIndex].OwnerId;
                    ORecord.items[iIndex].text = Owners[iIndex].OwnerName;


                    ORecord.items[iIndex].Code = Owners[iIndex].OwnerCode;
                    ORecord.items[iIndex].Name = Owners[iIndex].OwnerName;
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
