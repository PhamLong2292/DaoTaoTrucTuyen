﻿using OneTSQ.Call.Bussiness.Utility;
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

    public class ChucDanhService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "ChucDanhService";
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
                return "Service Chức danh";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.ChucDanhFilterCls OChucDanhFilter = new OneMES3.DM.Model.ChucDanhFilterCls();
                OChucDanhFilter.Keyword = Keyword;
                OChucDanhFilter.PageIndex = PageIndex;
                OChucDanhFilter.PageSize = 20;
                OChucDanhFilter.HieuLuc = (int)eHieuLuc.Co;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OChucDanhFilter);
                int recordTotal = (int)ajaxOut;
                var ChucDanhs = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OChucDanhFilter, ref recordTotal);             
                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[ChucDanhs.Length];
                for (int iIndex = 0; iIndex < ChucDanhs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = ChucDanhs[iIndex].Id;
                    ORecord.items[iIndex].text = ChucDanhs[iIndex].Ten;

                    ORecord.items[iIndex].Code = ChucDanhs[iIndex].Ma;
                    ORecord.items[iIndex].Name = ChucDanhs[iIndex].Ten;
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
