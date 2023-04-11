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
   
    public class DeCuongService : RemoteDataTemplate
    {
        public static string StaticServiceId
        {
            get
            {
                return "DeCuongService";
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
                return "Danh sách đề cương";
            }
        }
        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            try
            {
                DeCuongFilterCls
                    DeCuongFilter = new DeCuongFilterCls();
                DeCuongFilter.Keyword = Keyword;
                DeCuongFilter.PageIndex = PageIndex;
                DeCuongFilter.LICHXETDUYET_ID = "";
                DeCuongFilter.TrangThai = (int)DeCuongCls.eTrangThai.LapLich;
                DeCuongFilter.PageSize = 20;
                long recordTotal = 0;
                DeCuongCls[] DeCuongs = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().PageReading(ORenderInfo, DeCuongFilter, ref recordTotal);

                Record ORecord = new Record();
                ORecord.total_count = recordTotal;
                ORecord.incomplete_results = true;
                ORecord.items = new RecordItemCls[DeCuongs.Length];
                for (int iIndex = 0; iIndex < DeCuongs.Length; iIndex++)
                {
                    ORecord.items[iIndex] = new RecordItemCls();
                    ORecord.items[iIndex].id = DeCuongs[iIndex].ID;
                    ORecord.items[iIndex].text = DeCuongs[iIndex].TENDECUONG;


                    ORecord.items[iIndex].Code = DeCuongs[iIndex].MA;
                    ORecord.items[iIndex].Name = DeCuongs[iIndex].TENDECUONG;
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
