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
   
    public class DeTaiDangKyService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DeTaiDangKyService";
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
                return "Danh sách DeTaiDangKy";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DangKyDeTaiFilterCls
                    DeTaiDangKyFilter = new DangKyDeTaiFilterCls();
                DeTaiDangKyFilter.Keyword = Keyword;
                DeTaiDangKyFilter.PageIndex = PageIndex;
                DeTaiDangKyFilter.PageSize = 20;
                DeTaiDangKyFilter.TrangThai = (int)DangKyDeTaiCls.eTrangThai.DaXetDuyet;
                long recordTotal = 0;

                DangKyDeTaiCls[] DeTaiDangKys = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().PageReading(ORenderInfo, DeTaiDangKyFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DeTaiDangKys.Length];
                for (int iIndex = 0; iIndex < DeTaiDangKys.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DeTaiDangKys[iIndex].ID;
                    ORecord.items[iIndex].text = DeTaiDangKys[iIndex].TENDETAI;


                    ORecord.items[iIndex].Code = DeTaiDangKys[iIndex].MA;
                    ORecord.items[iIndex].Name = DeTaiDangKys[iIndex].TENDETAI;
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
