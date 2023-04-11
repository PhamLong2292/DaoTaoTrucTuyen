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

    public class ChucVuService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "ChucVuService";
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
                OneMES3.DM.Model.ChucVuFilterCls OChucVuFilter = new OneMES3.DM.Model.ChucVuFilterCls();
                OChucVuFilter.Keyword = Keyword;
                OChucVuFilter.PageIndex = PageIndex;
                OChucVuFilter.PageSize = 20;
                OChucVuFilter.HieuLuc = (int)eHieuLuc.Co;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OChucVuFilter);
                int recordTotal = (int)ajaxOut;
                var ChucVus = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OChucVuFilter, ref recordTotal);             
                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[ChucVus.Length];
                for (int iIndex = 0; iIndex < ChucVus.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = ChucVus[iIndex].Id;
                    ORecord.items[iIndex].text = ChucVus[iIndex].Ten;

                    ORecord.items[iIndex].Code = ChucVus[iIndex].Ma;
                    ORecord.items[iIndex].Name = ChucVus[iIndex].Ten;
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
