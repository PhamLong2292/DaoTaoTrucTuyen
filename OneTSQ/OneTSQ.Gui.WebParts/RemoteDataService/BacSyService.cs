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
   
    public class BacSyService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "BacSyService";
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
                return "Danh sách BacSy";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                BacSyFilterCls
                    BacSyFilter = new BacSyFilterCls();
                BacSyFilter.Keyword = Keyword;
                BacSyFilter.PageIndex = PageIndex;
                BacSyFilter.PageSize = 20;
                long recordTotal = 0;

                BacSyCls[] BacSys = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().PageReading(ORenderInfo, BacSyFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[BacSys.Length];
                for (int iIndex = 0; iIndex < BacSys.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = BacSys[iIndex].ID;
                    ORecord.items[iIndex].text = BacSys[iIndex].HOTEN;


                    ORecord.items[iIndex].Code = BacSys[iIndex].MA;
                    ORecord.items[iIndex].Name = BacSys[iIndex].HOTEN;
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
        //Chỉ hiển thị danh sách bác sỹ thuộc mã đơn vị trong tham số Filter
        //public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword, string Filter)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();

        //    try
        //    {
        //        BacSyFilterCls
        //            BacSyFilter = new BacSyFilterCls();
        //        BacSyFilter.Keyword = Keyword;
        //        BacSyFilter.PageIndex = PageIndex;
        //        BacSyFilter.PageSize = 20;
        //        BacSyFilter.DONVIMA = Filter;
        //        long recordTotal = 0;

        //        BacSyCls[] BacSys = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().PageReading(ORenderInfo, BacSyFilter, ref recordTotal);

        //        Record ORecord = new Record();
        //        ORecord.total_count = recordTotal;
        //        ORecord.incomplete_results = true;
        //        ORecord.items = new RecordItemCls[BacSys.Length];
        //        for (int iIndex = 0; iIndex < BacSys.Length; iIndex++)
        //        {
        //            ORecord.items[iIndex] = new RecordItemCls();
        //            ORecord.items[iIndex].id = BacSys[iIndex].ID;
        //            ORecord.items[iIndex].text = BacSys[iIndex].HOTEN;


        //            ORecord.items[iIndex].Code = BacSys[iIndex].MA;
        //            ORecord.items[iIndex].Name = BacSys[iIndex].HOTEN;
        //        }

        //        string json = JsonConvert.SerializeObject(ORecord, Formatting.Indented);
        //        RetAjaxOut.HtmlContent = json;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}
    }
}
