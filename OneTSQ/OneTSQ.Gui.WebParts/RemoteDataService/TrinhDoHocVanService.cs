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

    public class TrinhDoHocVanService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "TrinhDoService";
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
                return "Service Trình độ học vấn";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.TrinhDoHocVanFilterCls OTrinhDoHocVanFilter = new OneMES3.DM.Model.TrinhDoHocVanFilterCls();
                OTrinhDoHocVanFilter.Keyword = Keyword;
                OTrinhDoHocVanFilter.PageIndex = PageIndex;
                OTrinhDoHocVanFilter.PageSize = 20;
                OTrinhDoHocVanFilter.HieuLuc = (int)eHieuLuc.Co;

                var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTrinhDoHocVanProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), OTrinhDoHocVanFilter);
                long recordTotal = (long)ajaxOut;
                //long recordTotal = 0;
                var trinhDoHocVans = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTrinhDoHocVanProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), OTrinhDoHocVanFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[trinhDoHocVans.Length];
                for (int iIndex = 0; iIndex < trinhDoHocVans.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = trinhDoHocVans[iIndex].Ma;
                    ORecord.items[iIndex].text = trinhDoHocVans[iIndex].Ten;

                    ORecord.items[iIndex].Code = trinhDoHocVans[iIndex].Ma;
                    ORecord.items[iIndex].Name = trinhDoHocVans[iIndex].Ten;
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
