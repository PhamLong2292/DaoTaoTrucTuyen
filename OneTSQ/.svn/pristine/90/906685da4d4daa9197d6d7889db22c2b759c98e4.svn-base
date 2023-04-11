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

    public class DT_HocVienService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DT_HocVienService";
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
                return "Danh sách DT_HocVien";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DT_HocVienFilterCls
                    DT_HocVienFilter = new DT_HocVienFilterCls();
                DT_HocVienFilter.Keyword = Keyword;
                DT_HocVienFilter.PageIndex = PageIndex;
                DT_HocVienFilter.PageSize = 20;
                long recordTotal = 0;

                DT_HocVienCls[] DT_HocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().PageReading(ORenderInfo, DT_HocVienFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DT_HocViens.Length];
                for (int iIndex = 0; iIndex < DT_HocViens.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DT_HocViens[iIndex].ID;
                    ORecord.items[iIndex].text = DT_HocViens[iIndex].HOTEN;


                    ORecord.items[iIndex].Code = DT_HocViens[iIndex].MA;
                    ORecord.items[iIndex].Name = DT_HocViens[iIndex].HOTEN;
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
        //Chỉ hiển thị danh sách học viên thuộc mã khóa học trong tham số Filter
        //public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword, string Filter)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();

        //    try
        //    {
        //        DT_HocVienFilterCls
        //            DT_HocVienFilter = new DT_HocVienFilterCls();
        //        DT_HocVienFilter.Keyword = Keyword;
        //        DT_HocVienFilter.PageIndex = PageIndex;
        //        DT_HocVienFilter.PageSize = 20;
        //        DT_HocVienFilter.KhoaHocDuyet_Id = Filter;
        //        long recordTotal = 0;

        //        DT_HocVienCls[] DT_HocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().PageReading(ORenderInfo, DT_HocVienFilter, ref recordTotal);

        //        Record ORecord = new Record();
        //        ORecord.total_count = recordTotal;
        //        ORecord.incomplete_results = true;
        //        ORecord.items = new RecordItemCls[DT_HocViens.Length];
        //        for (int iIndex = 0; iIndex < DT_HocViens.Length; iIndex++)
        //        {
        //            ORecord.items[iIndex] = new RecordItemCls();
        //            ORecord.items[iIndex].id = DT_HocViens[iIndex].ID;
        //            ORecord.items[iIndex].text = DT_HocViens[iIndex].HOTEN;


        //            ORecord.items[iIndex].Code = DT_HocViens[iIndex].MA;
        //            ORecord.items[iIndex].Name = DT_HocViens[iIndex].HOTEN;
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
