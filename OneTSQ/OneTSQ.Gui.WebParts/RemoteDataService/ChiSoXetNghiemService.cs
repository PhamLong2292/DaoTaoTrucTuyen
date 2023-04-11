﻿//using OneTSQ.Call.Bussiness.Utility;
//using OneTSQ.Model;
//using OneTSQ.Bussiness.Utility;
//using OneTSQ.Utility;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace OneTSQ.WebParts
//{
   
//    public class ChiSoXetNghiemService : RemoteDataTemplate
//    {
//        public static string StaticServiceId
//        {
//            get
//            {
//                return "ChiSoXetNghiemService";
//            }
//        }
//        public override string ServiceId
//        {
//            get
//            {
//                return StaticServiceId;
//            }
//        }

//        public override string ServiceName
//        {
//            get
//            {
//                return "Danh mục chỉ số xét nghiệm";
//            }
//        }
//        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
//        {
//            AjaxOut RetAjaxOut = new AjaxOut();

//            try
//            {
//                OneMES3.DM.Model.ChiSoXetNghiemFilterCls OChiSoXetNghiemFilter = new OneMES3.DM.Model.ChiSoXetNghiemFilterCls();
//                OChiSoXetNghiemFilter.Keyword = Keyword;
//                OChiSoXetNghiemFilter.PageIndex = PageIndex;
//                OChiSoXetNghiemFilter.PageSize = 20;
//                OChiSoXetNghiemFilter.HieuLuc = (int)Common.eHieuLuc.Co;

//                OneMES3.SYS.Core.Model.XmlCls OXml = ChiSoXetNghiemFilterParser.GetXml(OChiSoXetNghiemFilter);
//                XmlCls XmlCommand = OneTSQ.DmProcessWebServiceUtility.Utility.CreateCommand("chisoxetnghiem.count", ORenderInfo, "", OXml);
//                var ajaxOut = OneTSQ.DmProcessWebServiceUtility.Utility.ProcessComand("ChiSoXetNghiemWsProcessCommand", XmlCommand.XmlData, XmlCommand.XmlDataSchema);
//                long recordTotal = (long)ajaxOut.RetDecimal;

//                XmlCommand = OneTSQ.DmProcessWebServiceUtility.Utility.CreateCommand("chisoxetnghiem.readingwithpaging", ORenderInfo, "", OXml);
//                ajaxOut = OneTSQ.DmProcessWebServiceUtility.Utility.ProcessComand("ChiSoXetNghiemWsProcessCommand", XmlCommand.XmlData, XmlCommand.XmlDataSchema);
//                var ChiSoXetNghiems = ChiSoXetNghiemParser.ParseFromMultiXml(ajaxOut.XmlDataResult, ajaxOut.XmlDataResultSchema);

//                Record ORecord = new Record();
//                ORecord.total_count = recordTotal;
//                ORecord.incomplete_results = true;
//                ORecord.items = new RecordItemCls[ChiSoXetNghiems.Length];
//                for (int iIndex = 0; iIndex < ChiSoXetNghiems.Length; iIndex++)
//                {
//                    ORecord.items[iIndex] = new RecordItemCls();
//                    ORecord.items[iIndex].id = ChiSoXetNghiems[iIndex].Ma;
//                    ORecord.items[iIndex].text = ChiSoXetNghiems[iIndex].Ten;


//                    ORecord.items[iIndex].Code = ChiSoXetNghiems[iIndex].Ma;
//                    ORecord.items[iIndex].Name = ChiSoXetNghiems[iIndex].Ten;
//                }

//                string json = JsonConvert.SerializeObject(ORecord, Formatting.Indented);
//                RetAjaxOut.HtmlContent = json;
//            }
//            catch (Exception ex)
//            {
//                RetAjaxOut.Error = true;
//                RetAjaxOut.InfoMessage = ex.Message.ToString();
//            }
//            return RetAjaxOut;
//        }
//    }
//}
