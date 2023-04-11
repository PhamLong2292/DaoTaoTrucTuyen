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

    public class DM_TieuChuanThamGiaKhoaHocService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DM_TieuChuanThamGiaKhoaHocService";
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
                return "Service danh mục Tiêu chuẩn tham gia khóa học";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DM_TieuChuanThamGiaKhoaHocFilterCls ODM_TieuChuanThamGiaKhoaHocFilter = new DM_TieuChuanThamGiaKhoaHocFilterCls();
                ODM_TieuChuanThamGiaKhoaHocFilter.Keyword = Keyword;
                ODM_TieuChuanThamGiaKhoaHocFilter.PageIndex = PageIndex;
                ODM_TieuChuanThamGiaKhoaHocFilter.PageSize = 20;
                ODM_TieuChuanThamGiaKhoaHocFilter.HieuLuc = (int)OneTSQ.Model.eHieuLuc.Co;
                int recordTotal = 0;
                var DM_TieuChuanThamGiaKhoaHocs = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().PageReading(ORenderInfo, ODM_TieuChuanThamGiaKhoaHocFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DM_TieuChuanThamGiaKhoaHocs.Length];

                for (int iIndex = 0; iIndex < DM_TieuChuanThamGiaKhoaHocs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DM_TieuChuanThamGiaKhoaHocs[iIndex].Ma;
                    ORecord.items[iIndex].text = DM_TieuChuanThamGiaKhoaHocs[iIndex].Ten;

                    ORecord.items[iIndex].Code = DM_TieuChuanThamGiaKhoaHocs[iIndex].Ma;
                    ORecord.items[iIndex].ShortName = DM_TieuChuanThamGiaKhoaHocs[iIndex].Ma;
                    ORecord.items[iIndex].Name = DM_TieuChuanThamGiaKhoaHocs[iIndex].Ten;
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
