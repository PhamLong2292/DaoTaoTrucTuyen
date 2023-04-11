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
   
    public class DanTocService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DanTocService";
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
                return "Danh mục dân tộc";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                OneMES3.DM.Model.DanTocFilterCls ODanTocFilter = new OneMES3.DM.Model.DanTocFilterCls();
                ODanTocFilter.Keyword = Keyword;
                ODanTocFilter.PageIndex = PageIndex;
                ODanTocFilter.PageSize = 20;
                ODanTocFilter.HieuLuc = (int)eHieuLuc.Co;

                //var ajaxOut = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().Count(Model.Common.CreateRenderInfo(ORenderInfo), ODanTocFilter);
                //int recordTotal = (int)ajaxOut;
                int recordTotal = 0;
                var DanTocs = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().PageReading(Model.Common.CreateRenderInfo(ORenderInfo), ODanTocFilter, ref recordTotal);
                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DanTocs.Length];
                for (int iIndex = 0; iIndex < DanTocs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DanTocs[iIndex].Ma;
                    ORecord.items[iIndex].text = DanTocs[iIndex].Ten;


                    ORecord.items[iIndex].Code = DanTocs[iIndex].Ma;
                    ORecord.items[iIndex].Name = DanTocs[iIndex].Ten;
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
