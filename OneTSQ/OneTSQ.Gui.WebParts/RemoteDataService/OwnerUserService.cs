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
   
    public class OwnerUserService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "OwnerUserService";
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
                return "Danh mục OwnerUser";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OwnerUserFilterCls
                    OwnerUserFilter = new OwnerUserFilterCls();
                OwnerUserFilter.Keyword = Keyword;
                OwnerUserFilter.PageIndex = PageIndex;

                OwnerUserCls[] OwnerUsers = Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Reading(ORenderInfo, OwnerUserFilter);

                Record ORecord = new Record();
                ORecord.total_count = OwnerUsers.Length;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[OwnerUsers.Length];
                for (int iIndex = 0; iIndex < OwnerUsers.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = OwnerUsers[iIndex].OwnerUserId;
                    ORecord.items[iIndex].text = OwnerUsers[iIndex].FullName;


                    ORecord.items[iIndex].Code = OwnerUsers[iIndex].LoginName;
                    ORecord.items[iIndex].Name = OwnerUsers[iIndex].FullName;
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
