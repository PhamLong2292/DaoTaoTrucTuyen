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
   
    public class DeTaiService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DeTaiService";
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
                return "Danh sách DeTai";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DeTaiFilterCls
                    DeTaiFilter = new DeTaiFilterCls();
                DeTaiFilter.Keyword = Keyword;
                DeTaiFilter.PageIndex = PageIndex;
                DeTaiFilter.LICHXETDUYET_ID = "";
                DeTaiFilter.TrangThai = (int)DeTaiCls.eTrangThai.LapLich;
                DeTaiFilter.PageSize = 20;
                long recordTotal = 0;

                DeTaiCls[] DeTais = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().PageReading(ORenderInfo, DeTaiFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DeTais.Length];
                for (int iIndex = 0; iIndex < DeTais.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DeTais[iIndex].ID;
                    ORecord.items[iIndex].text = DeTais[iIndex].TENDETAI;


                    ORecord.items[iIndex].Code = DeTais[iIndex].MA;
                    ORecord.items[iIndex].Name = DeTais[iIndex].TENDETAI;
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
