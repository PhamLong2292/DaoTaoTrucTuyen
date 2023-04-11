﻿using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using System.Data;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Web;
using OneTSQ.Common;
using FlexCel.Report;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{   
    public class DanhSachDangKyDeTai : WebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DanhSachDangKyDeTai";
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách đề tài, sáng kiến";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách đề tài, sáng kiến";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DanhSachDangKyDeTai), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string keyword = WebEnvironments.Request("Keyword");
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            int? LoaiHinh = string.IsNullOrEmpty(WebEnvironments.Request("LoaiHinh")) ? null : (int?)int.Parse(WebEnvironments.Request("LoaiHinh"));
            int? CapDeTai = string.IsNullOrEmpty(WebEnvironments.Request("CapDeTai")) ? null : (int?)int.Parse(WebEnvironments.Request("CapDeTai"));
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();              
                string cbbTrangThai = "";
                foreach (var tt in DangKyDeTaiParser.TrangThais)
                    cbbTrangThai += "<option value=" + tt.Key + " " + (trangThai == tt.Key ? "selected" : "") + ">" + tt.Value + "</option>";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách đề tài, sáng kiến") + "';\r\n" +                  
                    "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                    "       $('#cbbTrangThai').select2({\r\n" +
                    "          placeholder: 'Trạng thái',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                    "       $('#cbbLoaiHinh').select2({\r\n" +
                    "          placeholder: 'Loại hình',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbbCapDeTai').select2({\r\n" +
                    "          placeholder: 'Cấp đề tài',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       LoaiHinh = document.getElementById('cbbLoaiHinh').value;\r\n" +
                    "       CapDeTai = parseInt(document.getElementById('cbbCapDeTai').value);\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DanhSachDangKyDeTai.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, LoaiHinh, CapDeTai, trangThai).value;\r\n" +
                    "       document.getElementById('divdangkydetai').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +

                    "   function ExPort()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       LoaiHinh = document.getElementById('cbbLoaiHinh').value;\r\n" +
                    "       CapDeTai = document.getElementById('cbbCapDeTai').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut  = OneTSQ.WebParts.DanhSachDangKyDeTai.Print(RenderInfo, keyword, LoaiHinh, CapDeTai, trangThai).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl, 'Download');\r\n" +
                    "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Xuất thành công") + "!\");\r\n" +
                    "   }\r\n" +
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đề tài, sáng kiến") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <div id=\"divdangkydetai\">" + DanhSachDangKyDeTai.DrawSearchResult(ORenderInfo, pageIndex, keyword, LoaiHinh, CapDeTai, trangThai).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
                    " </div>\r\n" +
                    "</div>\r\n"
                        );
                #endregion
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #region Vẽ giao diện
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, int? LoaiHinh, int? CapDeTai, int? trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);

                DangKyDeTaiFilterCls filter = new DangKyDeTaiFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = keyword,
                    LOAIHINH = LoaiHinh,
                    CAPDETAI = CapDeTai,
                    TrangThai = trangThai,
                };
                long recordTotal = 0;
                DangKyDeTaiCls[] dangkydetai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int dangKyTotal = dangkydetai.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã đề tài") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên đề tài đăng ký") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Dự kiến kinh phí") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày nhiệm thu") + " </th> \r\n" +             
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < dangKyTotal; iIndex++)
                {
                    var DangKyDeTaiUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DangKyDeTai",
                        new WebParamCls[] { new WebParamCls("id", dangkydetai[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                    string chunhiem = null;
                    OwnerUserCls Users = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, dangkydetai[iIndex].CHUNHIEM_ID);                        
                    if (Users != null)
                    {
                        chunhiem = Users.FullName;
                    }
                        Html +=
                        "                 <tr> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + dangkydetai[iIndex].MA + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + dangkydetai[iIndex].TENDETAI + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (dangkydetai[iIndex] == null || dangkydetai[iIndex].LOAIHINH.ToString() == null ? null : DangKyDeTaiParser.LoaiHinhs[dangkydetai[iIndex].LOAIHINH]) + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (dangkydetai[iIndex] == null || dangkydetai[iIndex].CAPDETAI == null ? null : DangKyDeTaiParser.CapDeTais[dangkydetai[iIndex].CAPDETAI.Value]) + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + chunhiem + "</a></td> \r\n" +
                        "                     <td style='text-align:right;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + dangkydetai[iIndex].KINHPHIDUKIEN.ToString("#,##0") + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DangKyDeTaiUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (dangkydetai[iIndex].THOIGIANDANGKY == null ? null : dangkydetai[iIndex].THOIGIANDANGKY.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'>" + DangKyDeTaiParser.sColorTrangThai[dangkydetai[iIndex].TRANGTHAI.Value] + "</td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + dangKyTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "              </div>\r\n" +
                    "             <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                    RetPaging.PagingText +
                    "             </div>\r\n" +
                    "         </div>\r\n" +
                    "       </div>\r\n" +
                    "<style>\r\n" +
                    "table th{text-align: center; vertical-align: middle;}\r\n" +
                    "</style>\r\n"
                    ;
                Html = WebEnvironments.ProcessHtml(Html);
                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #endregion     
        #region Override Func Core
        public override string divFilter
        {
            get
            {
                return "<div class=\"col-md-3\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã, tên đề tài/sáng kiến \" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbLoaiHinh\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "        <option value=\"" + (int)DangKyDeTaiCls.eLoaiHinh.DeTai + "\">Đề tài</option>\r\n" +
                       "        <option value=\"" + (int)DangKyDeTaiCls.eLoaiHinh.SangKien + "\">Sáng kiến</option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbCapDeTai\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "        <option value=\"" + (int)DangKyDeTaiCls.eCapDeTai.CoSo + "\">Cơ sở</option>\r\n" +
                       "        <option value=\"" + (int)DangKyDeTaiCls.eCapDeTai.Tinh + "\">Tỉnh</option>\r\n" +
                       "        <option value=\"" + (int)DangKyDeTaiCls.eCapDeTai.NhaNuoc + "\">Nhà nước</option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <button id = 'btnTimKiem' class='btn btn-sm  mr-10px' onclick='FilterChange()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Tìm kiếm</strong></button>\r\n" +
                       "</div>\r\n" 
                       //"<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       //"    <button id = 'btnExport' class='btn btn-sm  mr-10px' onclick='ExPort()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Xuất dữ liệu</strong></button>\r\n" +
                       //"</div>\r\n"
                   ;
            }
        }
        #endregion
    }
}
