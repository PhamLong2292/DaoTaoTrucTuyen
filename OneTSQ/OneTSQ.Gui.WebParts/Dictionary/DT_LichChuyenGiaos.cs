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
using OneTSQ.UploadUtility;

namespace OneTSQ.WebParts
{
    public class DT_LichChuyenGiaos : WebPartTemplate
    {
        class DT_LichChuyenGiaoView
        {
            public string KYTHUAT { get; set; }
            public string KHOAHOC { get; set; }
            public string BENHVIEN { get; set; }
            public string BACSY { get; set; }
            public string BATDAU { get; set; }
            public string KETTHUC { get; set; }
            public string TRANGTHAI { get; set; }
        }
        public override string WebPartId
        {
            get
            {
                return "DT_LichChuyenGiaos";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách lịch chuyển giao";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách lịch chuyển giao";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_LichChuyenGiaos), Page);
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
            OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string keyword = WebEnvironments.Request("Keyword");
            int? nam = string.IsNullOrEmpty(WebEnvironments.Request("Nam")) ? null : (int?)int.Parse(WebEnvironments.Request("Nam"));
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();
                string maDonViTuVanDefault = WebConfig.GetWebConfig("HospitalCode");
                string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                bool xemPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Xem.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                bool themPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Them.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                bool suaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Sua.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                bool xoaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Xoa.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                bool guiDuyetPermission = user.IsSystemAdmin == 1 || user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.GuiDuyet.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.PheDuyet.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, UserId, user.OwnerUserId);
                var trangThais = xemPermission || themPermission || suaPermission || xoaPermission || guiDuyetPermission ? DT_LichChuyenGiaoParser.TrangThais : DT_LichChuyenGiaoParser.TrangThais.Where(o => o.Key != (int)DT_LichChuyenGiaoCls.eTrangThai.Moi);

                string cbbTrangThai = "";
                foreach (var tt in trangThais)
                    cbbTrangThai += "<option value=" + tt.Key + " " + (trangThai == tt.Key ? "selected" : "") + ">" + tt.Value + "</option>";
               // string filtemplate = WebConfig.GetWebHttpRoot() + "/temp/NhapDT_LichChuyenGiao.xml";
                string filtemplate = "temp/ImportDT_LICHCHUYENGIAO.xlsx";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   var CheckedArr=[];\r\n" +
                    "   var checkedTrangThai=-1;\r\n" +                 
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách lịch chuyển giao") + "';\r\n" +
                    "       $('.yearpicker').datetimepicker({ \r\n" +
                    "           format: 'YYYY' \r\n" +
                    "       }); \r\n" +
                    "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                    "       $('#cbbTrangThai').select2({\r\n" +
                    "          placeholder: 'Trạng thái',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                    "       $('#txtNam').val('" + nam + "');\r\n" +
                    "       CallInitSelect2('cbbKyThuatChuyenGiaoFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(KyThuatChuyenGiaoService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Kỹ thuật chuyển giao") + "');\r\n" +
                    "       CallInitSelect2('cbbKhoaHocFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Khóa học") + "');\r\n" +
                    "       CallInitSelect2('cbbBenhVienFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Bệnh viện nhận chuyển giao") + "');\r\n" +
                    "       CallInitSelect2('cbbBacSyFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao") + "');\r\n" +
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
                    "       nam = parseInt(document.getElementById('txtNam').value);\r\n" +
                    "       kyThuatChuyenGiao = document.getElementById('cbbKyThuatChuyenGiaoFilter').value;\r\n" +
                    "       benhVien = document.getElementById('cbbBenhVienFilter').value;\r\n" +
                    "       bacSy = document.getElementById('cbbBacSyFilter').value;\r\n" +
                    "       khoaHoc = document.getElementById('cbbKhoaHocFilter').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.DrawSearchResult(RenderInfo, CurrentPageIndex, nam, kyThuatChuyenGiao, benhVien, bacSy, khoaHoc, trangThai, CheckedArr).value;\r\n" +
                    "       document.getElementById('divDT_LichChuyenGiaos').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CheckedArr=[];\r\n" +
                    "       checkedTrangThai=-1;\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                #region import DT_LichChuyenGiaoXML
                    "   function Import()\r\n" +
                    "   {\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        fileUploadValue=document.getElementById('fileUpload').value;\r\n" +
                    "        if(fileUploadValue==''){\r\n" +
                    "            callGallAlert('Chưa xác định file chứa dữ liệu nhập!');\r\n" +
                    "            return false;\r\n" +
                    "        }\r\n" +
                    //"       document.getElementById('divProcessing').innerHTML='Đang Import Danh mục';\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn import thông tin này không ?") + "\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Có") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Không") + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "                   $.LoadingOverlay('show');\r\n" +
                    "                   var fd = new FormData();\r\n" +
                    "                   fd.append(\"fileUploadAvatar\", document.getElementById('fileUpload').files[0]);\r\n" +
                    "                   var xhr = new XMLHttpRequest();\r\n" +
                    "                   xhr.addEventListener(\"load\",uploaded, false);\r\n" +
                    "                   xhr.open(\"POST\", \"" + WebConfig.GetImportHandler(OSiteParam, SessionId, UserId, LoginName, (int)ProcessImportHandlerUtility.eFileType.DT_LICHCHUYENGIAO) + "\");\r\n" +
                    "                   xhr.send(fd);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                    " function uploaded(evt) {\r\n" +
                    "       $.LoadingOverlay('hide');\r\n" +
                    "       var parser = new DOMParser();\r\n" +
                    "       var xmlDoc = parser.parseFromString(evt.currentTarget.responseText,\"text/xml\"); \r\n" +
                    "       var mes = xmlDoc.getElementsByTagName(\"InfoMessage\")[0].childNodes[0].nodeValue;\r\n" +
                    "       callSweetAlert(mes);\r\n" +
                    "       $('.fileinput-filename').html('');\r\n" +
                    "       FilterChange();\r\n" +
                    " }\r\n" +
                #endregion
                #region Export dữ liệu lịch chuyển giao ra file XML
                    "   function Export()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       nam = parseInt(document.getElementById('txtNam').value);\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut  = OneTSQ.WebParts.DT_LichChuyenGiaos.Export(RenderInfo, /*keyword, */nam, trangThai).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl, 'Download');\r\n" +
                    "   }\r\n" +
                #endregion
                #region Show popup lịch chuyển giao
                    "function ShowPopupLichChuyenGiao(LichChuyenGiaoId)\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.PopupLichChuyenGiao(RenderInfo, LichChuyenGiaoId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới/cập nhật lịch chuyển giao") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbKyThuatChuyenGiao', '" + WebEnvironments.GetRemoteProcessDataUrl(KyThuatChuyenGiaoService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Kỹ thuật chuyển giao") + "');\r\n" +
                    "     CallInitSelect2('cbbKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbBenhVien', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Bệnh viện nhận chuyển giao") + "');\r\n" +
                    "     CallInitSelect2('cbbBacSy', '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao") + "');\r\n" +
                    "     CallInitSelect2('cbbGiayTo', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_GiayToDiChuyenGiaoService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Giấy tờ đi chuyển giao") + "');\r\n" +
                    "}\r\n" +
                #endregion Show popup lịch chuyển giao
                #region Save Thông tin lịch chuyển giao
                    "function SaveLichChuyenGiao()\r\n" +
                    "{\r\n" +
                    "     lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "     kyThuatChuyenGiao = document.getElementById('cbbKyThuatChuyenGiao').value;\r\n" +
                    "     khoaHoc = document.getElementById('cbbKhoaHoc').value;\r\n" +
                    "     benhVien = document.getElementById('cbbBenhVien').value;\r\n" +
                    "     bacSy = document.getElementById('cbbBacSy').value;\r\n" +
                    "     batDau = document.getElementById('dtBatDau').value;\r\n" +
                    "     ketThuc = document.getElementById('dtKetThuc').value;\r\n" +
                    "     giayTo = $('#cbbGiayTo').val();\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.SaveLichChuyenGiao(RenderInfo, lichChuyenGiaoId, kyThuatChuyenGiao, khoaHoc, benhVien, bacSy, batDau, ketThuc, giayTo).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdLichChuyenGiaoId').value = AjaxOut.RetExtraParam1;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "     }\r\n" +
                    "     FilterChange();\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin lịch chuyển giao
                #region Xóa Thông tin lịch chuyển giao
                    "function DeleteLichChuyenGiao(lichChuyenGiaoId)\r\n" +
                    "{\r\n" +
                    "   swal({\r\n" +
                    "           title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "           text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắn chắn xóa lịch chuyển giao này không?") + "\",\r\n" +
                    "           type: \"warning\",\r\n" +
                    "           showCancelButton: true,\r\n" +
                    "           confirmButtonClass: \"btn-danger\",\r\n" +
                    "           confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\",\r\n" +
                    "           cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\",\r\n" +
                    "           closeOnConfirm: true,\r\n" +
                    "           closeOnCancel: true\r\n" +
                    "       },\r\n" +
                    "       function(isConfirm) {\r\n" +
                    "           if (isConfirm) {\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.DeleteLichChuyenGiao(RenderInfo, lichChuyenGiaoId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "               FilterChange();\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion Xóa Thông tin lịch chuyển giao
                #region Ẩn hiện các button phê duyệt theo trạng thái lịch được chọn trên danh sách.
                    "   function ShowHideButton(){\r\n" +
                    "       $('.clsPheDuyet').hide();\r\n" +
                    "       if (checkedTrangThai == " + (int)DT_LichChuyenGiaoCls.eTrangThai.Moi + " && '" + guiDuyetPermission + "' == 'True')\r\n" +
                    "           $('#btnGuiDuyet').show();\r\n" +
                    "       else if (checkedTrangThai == " + (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet + ")\r\n" +
                    "       {\r\n" +
                    "           if ('" + guiDuyetPermission + "' == 'True')\r\n" +
                    "               $('#btnThuHoiGuiDuyet').show();\r\n" +
                    "           if ('" + pheDuyetPermission + "' == 'True'){\r\n" +
                    "               $('#btnDuyet').show();\r\n" +
                    "               $('#btnTuChoiDuyet').show();\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       else if (checkedTrangThai == " + (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet + " && '" + pheDuyetPermission + "' == 'True')\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       else if (checkedTrangThai == " + (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi + " && '" + pheDuyetPermission + "' == 'True')\r\n" +
                    "           $('#btnThuHoiTuChoiDuyet').show();\r\n" +
                    "   }\r\n" +
                #endregion Ẩn hiện các button phê duyệt theo trạng thái lịch được chọn trên danh sách.
                #region ckbAllPage_onclick
                    "   function ckbAllPage_onclick(sender){\r\n" +
                    "       checkedTrangThai = -1;\r\n" +
                    "       if (sender.checked){\r\n" +
                    "           $('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "               if (!element.checked){\r\n" +
                    "                   element.checked=true;\r\n" +
                    "                   CheckedArr.push(element.id);\r\n" +
                    "               }\r\n" +
                    "           });\r\n" +
                    "           trangThais = $('.clsTrangThai');\r\n" +
                    "           lichChuyenGiaoNumber = trangThais.length;\r\n" +
                    "           for(i = 0; i < lichChuyenGiaoNumber; i++) {\r\n" +
                    "               if (checkedTrangThai == -1){\r\n" +
                    "                   checkedTrangThai=trangThais[i].value;\r\n" +
                    "               }\r\n" +
                    "               else if (trangThais[i].value != checkedTrangThai){\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   break;\r\n" +
                    "               }\r\n" +
                    "           }\r\n" +
                    "           ShowHideButton();\r\n" +
                    "       }\r\n" +
                    "       else {\r\n" +
                    "           $('.clsPheDuyet').hide();\r\n" +
                    "           $('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "               if (element.checked){\r\n" +
                    "                   element.checked=false;\r\n" +
                    "                   for (var i=0; i < CheckedArr.length; i++) {\r\n" +
                    "                       if(CheckedArr[i] == element.id){\r\n" +
                    "	                        CheckedArr.splice(i,1);\r\n" +
                    "	                        break;\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "               }\r\n" +
                    "           });\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion ckbAllPage_onclick
                #region ckb_onclick
                    "   function ckb_onclick(sender){\r\n" +
                    "       if (sender.checked){\r\n" +
                    "           CheckedArr.push(sender.id);\r\n" +
                    "           CheckCheckAllPage();\r\n" +
                    "           if (checkedTrangThai == -1){\r\n" +
                    "               CheckedLichChuyenGiaoNumber = CheckedArr.length;\r\n" +
                    "               for(i = 0; i < CheckedLichChuyenGiaoNumber; i++) {\r\n" +
                    "                   if (checkedTrangThai == -1){\r\n" +
                    "                       checkedTrangThai=$('#hdTrangThai' + CheckedArr[i]).val();\r\n" +
                    "                   }\r\n" +
                    "                   else if ($('#hdTrangThai' + CheckedArr[i]).val() != checkedTrangThai){\r\n" +
                    "                       checkedTrangThai = -1;\r\n" +
                    "                       break;\r\n" +
                    "                   }\r\n" +
                    "               }\r\n" +
                    "               ShowHideButton();\r\n" +
                    "           }\r\n" +
                    "           else if (checkedTrangThai != $('#hdTrangThai' + sender.id).val()){\r\n" +
                    "               checkedTrangThai = -1;\r\n" +
                    "               $('.clsPheDuyet').hide();\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           for (var index=0; index < CheckedArr.length; index ++) {\r\n" +
                    "               if(CheckedArr[index] == sender.id){\r\n" +
                    "	                CheckedArr.splice(index,1);\r\n" +
                    "	                break;\r\n" +
                    "               }\r\n" +
                    "           }\r\n" +
                    "           ckbAllPage.checked=false;\r\n" +
                    "           if (CheckedArr.length == 0) {\r\n" +
                    "               checkedTrangThai = -1;\r\n" +
                    "           }\r\n" +
                    //Kiểm tra lại danh sách check, nếu toàn danh sách cùng 1 trạng thái thì cập nhật lại checkedTrangThai theo trạng thái của lịch chuyển giao trong danh sách.
                    "           if (checkedTrangThai == -1) {\r\n" +
                    "               for (var index=0; index < CheckedArr.length; index ++) {\r\n" +
                    "                   if (checkedTrangThai == -1){\r\n" +
                    "                       checkedTrangThai=$('#hdTrangThai' + CheckedArr[index]).val();\r\n" +
                    "                   }\r\n" +
                    "                   else if ($('#hdTrangThai' + CheckedArr[index]).val() != checkedTrangThai){\r\n" +
                    "                       checkedTrangThai = -1;\r\n" +
                    "                       break;\r\n" +
                    "                   }\r\n" +
                    "               }\r\n" +
                    "           }\r\n" +
                    "           if (checkedTrangThai == -1) {\r\n" +
                    "               $('.clsPheDuyet').hide();\r\n" +
                    "           }\r\n" +
                    "           else {\r\n" +
                    "               ShowHideButton();\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion ckb_onclick
                #region Check CheckAllPage: kiểm tra nếu tất cả checkbox trên row được check thì check checkbox all
                    "   function CheckCheckAllPage(){\r\n" +
                    "       checkAll=true;\r\n" +
                    "       $('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "           if (!element.checked){\r\n" +
                    "               checkAll=false;\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "       });\r\n" +
                    "       ckbAllPage.checked=checkAll;\r\n" +
                    "   }\r\n" +
                #endregion Check CheckAllPage: kiểm tra nếu tất cả checkbox trên row được check thì check checkbox all
                #region Chuyển duyệt lịch chuyển giao
                    "   function ChuyenDuyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần gửi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn gửi duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn gửi duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã gửi duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Chuyển duyệt lịch chuyển giao
                #region Thu hồi chuyển duyệt lịch chuyển giao
                    "   function ThuHoiChuyenDuyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần thu hồi gửi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi gửi duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi gửi duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.Moi + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi gửi duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Thu hồi chuyển duyệt lịch chuyển giao
                #region Duyệt lịch chuyển giao
                    "   function Duyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Duyệt lịch chuyển giao
                #region Thu hồi duyệt lịch chuyển giao
                    "   function ThuHoiDuyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần thu hồi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Thu hồi duyệt lịch chuyển giao
                #region Từ chối duyệt lịch chuyển giao
                    "   function TuChoiDuyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần từ chối duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn từ chối duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn từ chối duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã từ chối duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Từ chối duyệt lịch chuyển giao
                #region Thu hồi từ chối duyệt lịch chuyển giao
                    "   function ThuHoiTuChoiDuyet(){\r\n" +
                    "       soLuongLich= CheckedArr.length;\r\n" +
                    "       if (soLuongLich == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn lịch cần thu hồi từ chối duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongLich > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi từ chối duyệt những lịch chuyển giao này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi từ chối duyệt lịch chuyển giao này?") + "\"), \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "                   type: \"warning\", \r\n" +
                    "                   showCancelButton: true, \r\n" +
                    "                   confirmButtonColor: \"#DD6B55\", \r\n" +
                    "                   confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "                   cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "                   closeOnConfirm: true, \r\n" +
                    "                   closeOnCancel: true\r\n" +
                    "               }, function () { \r\n" +
                    "                   RenderInfo=CreateRenderInfo();\r\n" +
                    "                   errorInfo = '';\r\n" +
                    "                   for (var index=0; index < soLuongLich; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_LichChuyenGiaos.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "                   if(errorInfo != '')\r\n" +
                    "	                    callGallAlert(errorInfo);\r\n" +
                    "                   else\r\n" +
                    "	                    toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi từ chối duyệt!") + "');\r\n" +
                    "                   CheckedArr=[];\r\n" +
                    "                   Search();\r\n" +
                    "                   checkedTrangThai = -1;\r\n" +
                    "                   $('.clsPheDuyet').hide();\r\n" +
                    "               }); \r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Thu hồi từ chối duyệt lịch chuyển giao
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách lịch chuyển giao") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <input type='button' id='btnGuiDuyet' title='Gửi duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Gửi duyệt") + "' onclick='javascript:ChuyenDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    "           <input type='button' id='btnThuHoiGuiDuyet' title='Thu hồi gửi duyệt' class='DT_KhoaHocThuHoi btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiChuyenDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    "           <input type='button' id='btnDuyet' title='Duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "' onclick='javascript:Duyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    "           <input type='button' id='btnTuChoiDuyet' title='Từ chối duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Từ Chối") + "' onclick='javascript:TuChoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    "           <input type='button' id='btnThuHoiDuyet' title='Thu hồi duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    "           <input type='button' id='btnThuHoiTuChoiDuyet' title='Thu hồi từ chối duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiTuChoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                    (themPermission ?
                    "           <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\" style=\"margin-left: 5px;\">\r\n" +
                    "               <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:Export();\"> " + WebLanguage.GetLanguage(OSiteParam, "Xuất dữ liệu") + "</button>\r\n" +
                    "               <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"location.href='" + filtemplate + "'\"><i class=\"fa fa-download\"></i> " + WebLanguage.GetLanguage(OSiteParam, "File mẫu") + "</button>\r\n" +
                    "               <button type=\"button\"  class=\"btn btn-sm btn-primary\" onclick=\"javascript:Import();\"> " + WebLanguage.GetLanguage(OSiteParam, "Nhập dữ liệu") + "</button>\r\n" +
                    "               <span class=\"btn btn-default btn-file\" ><span class=\"fileinput-new\">Chọn File</span>\r\n" +
                    "               <span class=\"fileinput-exists\">Chọn lại</span><input type=\"file\" name=\"...\" id=\"fileUpload\" accept=\".xlsx, .xls\"/></span>\r\n" +
                    "               <span class=\"fileinput-filename\"></span>\r\n" +
                    "               <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                    "           </div> \r\n" : null) +
                    "           <div id=\"divDT_LichChuyenGiaos\">" + DT_LichChuyenGiaos.DrawSearchResult(ORenderInfo, pageIndex, nam, null, null, null, null, trangThai, new string[0]).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
                    " </div>\r\n" +
                    "</div>\r\n" +
                    "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n" +
                #region Phần hiển thị popup
                    "<div id=\"divFormModal\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                    "    <div class=\"modal-dialog\" style='width:750px; height:550px; font-size: 14px;'>\r\n" +
                    "       <div class=\"modal-content\" style='width:100%; height:100%; position: relative;'>\r\n" +
                    "           <div class=\"panel-heading\">\r\n" +
                    "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\"'>&times;</button>\r\n" +
                    "               <h2 class=\"modal-title\" id=\"ModalTitle\"></h2>\r\n" +
                    "           </div> \r\n" +
                    "           <div class=\"modal-body\" id=\"divModalContent\" style='width:100%; padding-top:0; position: absolute; top:50px; bottom:0px;'></div> \r\n" +
                    "        </div> \r\n" +
                    "    </div> \r\n" +
                    "</div> \r\n"
                #endregion
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
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, int? nam, string kyThuatChuyenGiao_Ma, string benhVien_Ma, string bacSy_Id, string khoaHoc_Id, int? trangThai, string[] CheckedArr)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                DT_LichChuyenGiaoFilterCls filter = new DT_LichChuyenGiaoFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Nam = nam,
                    KYTHUAT_MA = kyThuatChuyenGiao_Ma,
                    BENHVIEN_MA = benhVien_Ma,
                    BACSY_ID = bacSy_Id,
                    KHOAHOC_ID = khoaHoc_Id,
                    TrangThai = trangThai,
                    DataPermissionQuery = GetDataPermissionQuery(ORenderInfo)
                };
                long recordTotal = 0;
                DT_LichChuyenGiaoCls[] lichChuyenGiaos = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int DT_LichChuyenGiaoTotal = lichChuyenGiaos.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\"><input type =\"checkbox\" id='ckbAllPage' onclick='ckbAllPage_onclick(this)'></th> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên kỹ thuật") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Bệnh viện nhận chuyển giao") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khóa học") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Bắt đầu") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Kết thúc") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                    "                     <th style='width:70px;'><a href='javascript:ShowPopupLichChuyenGiao(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới lịch chuyển giao") + "'><i class='fa fa-plus' style='font-size:14px; color: white;'></i></a></th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < DT_LichChuyenGiaoTotal; iIndex++)
                {
                    bool suaPermission = lichChuyenGiaos[iIndex].NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Sua.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, user.OwnerUserId, lichChuyenGiaos[iIndex].NGUOITAO_ID);
                    bool xoaPermission = lichChuyenGiaos[iIndex].NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.Xoa.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, user.OwnerUserId, lichChuyenGiaos[iIndex].NGUOITAO_ID);
                    var DT_LichChuyenGiaoUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_ChuyenGiao",
                        new WebParamCls[] { new WebParamCls("id", lichChuyenGiaos[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("ChiSoMang", filter.PageIndex * filter.PageSize + iIndex),
                            new WebParamCls("nam", nam == null? null : nam.Value.ToString()),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                    DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaos[iIndex].KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaos[iIndex].KYTHUAT_MA);
                    OneMES3.DM.Model.BenhVienCls benhVien = null;
                    if (!string.IsNullOrEmpty(lichChuyenGiaos[iIndex].BENHVIEN_MA))
                    {
                        benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(sRenderInfo, lichChuyenGiaos[iIndex].BENHVIEN_MA);
                    }
                    BacSyCls canBoChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaos[iIndex].BACSY_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiaos[iIndex].BACSY_ID);
                    DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(lichChuyenGiaos[iIndex].KHOAHOC_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, lichChuyenGiaos[iIndex].KHOAHOC_ID);

                    int ArrIndex = CheckedArr.Count() - 1;
                    while (ArrIndex >= 0 && CheckedArr[ArrIndex] != lichChuyenGiaos[iIndex].ID)
                        ArrIndex--;
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td class=\"td-center\" style='text-align: center; vertical-align: middle;'><input type =\"checkbox\" class=\"clsCkb\" id='" + lichChuyenGiaos[iIndex].ID + "' " + (ArrIndex >= 0 ? "checked" : "") + " onclick='ckb_onclick(this)'></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + (kyThuatChuyenGiao == null ? lichChuyenGiaos[iIndex].KYTHUAT_MA : kyThuatChuyenGiao.Ten) + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + (benhVien == null ? lichChuyenGiaos[iIndex].BENHVIEN_MA : benhVien.Ten) + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + (canBoChuyenGiao == null ? lichChuyenGiaos[iIndex].BACSY_ID : canBoChuyenGiao.HOTEN) + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + (khoaHoc == null ? lichChuyenGiaos[iIndex].KHOAHOC_ID : khoaHoc.TENKHOAHOC) + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + lichChuyenGiaos[iIndex].BATDAU.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_LichChuyenGiaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở lịch chuyển giao") + "'>" + lichChuyenGiaos[iIndex].KETTHUC.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><input type='hidden' id='hdTrangThai" + lichChuyenGiaos[iIndex].ID + "' class='clsTrangThai' value=" + lichChuyenGiaos[iIndex].TRANGTHAI + ">" + DT_LichChuyenGiaoParser.sColorTrangThai[lichChuyenGiaos[iIndex].TRANGTHAI] + "</td> \r\n" +
                        "                     <td style='text-align:center; vertical-align: middle;'>\r\n";
                    if (suaPermission)
                        Html +=
                            "                   <a id='btnEdit" + iIndex + "' class='CssDisplayItemThanhVien' href='javascript:ShowPopupLichChuyenGiao(\"" + lichChuyenGiaos[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:14px;'></i></a>\r\n";
                    if (xoaPermission)
                        Html +=
                            "                   <a id='btnDelete" + iIndex + "' class='CssDisplayItemThanhVien' href='javascript:DeleteLichChuyenGiao(\"" + lichChuyenGiaos[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:14px;'></i></a>\r\n";
                    Html +=
                        "                    </td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + DT_LichChuyenGiaoTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "              </div>\r\n" +
                    "             <div class=\"col-md-10\" style=\"margin-top:14px;\">\r\n" +
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut PopupLichChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoCls lichChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaoId) ? new DT_LichChuyenGiaoCls() : CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                string cbbKyThuatChuyenGiao = "<select class='form-control' id='cbbKyThuatChuyenGiao' required style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA))
                {
                    DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);
                    if (kyThuatChuyenGiao != null)
                        cbbKyThuatChuyenGiao += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", kyThuatChuyenGiao.Ma, kyThuatChuyenGiao.Ten);
                    else
                        cbbKyThuatChuyenGiao += string.Format(" <option value='{0}' selected>{0}</option>\r\n", lichChuyenGiao.KYTHUAT_MA);
                }
                cbbKyThuatChuyenGiao += "</select>\r\n";

                string cbbKhoaHoc = "<select class='form-control' id='cbbKhoaHoc' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(lichChuyenGiao.KHOAHOC_ID))
                {
                    DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, lichChuyenGiao.KHOAHOC_ID);
                    if (khoaHoc != null)
                        cbbKhoaHoc += string.Format(" <option value='{0}' selected>{1} - {2}</option>\r\n", khoaHoc.ID, khoaHoc.MA, khoaHoc.TENKHOAHOC);
                }
                cbbKhoaHoc += "</select>\r\n";

                string cbbBenhVien = "<select class='form-control' id='cbbBenhVien' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
                {
                    OneMES3.DM.Model.BenhVienCls benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(sRenderInfo, lichChuyenGiao.BENHVIEN_MA);
                    if (benhVien != null)
                        cbbBenhVien += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", benhVien.Ma, benhVien.Ten);
                    else cbbBenhVien += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", lichChuyenGiao.BENHVIEN_MA, lichChuyenGiao.BENHVIEN_MA, "N/A");
                }
                cbbBenhVien += "</select>\r\n";

                string cbbBacSy = "<select class='form-control' id='cbbBacSy' required style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(lichChuyenGiao.BACSY_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);
                    if (bacSy != null)
                        cbbBacSy += string.Format(" <option value='{0}' selected>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbBacSy += "</select>\r\n";

                string[] giayTos;
                if (string.IsNullOrEmpty(lichChuyenGiao.GIAYTO))
                    giayTos = new string[0];
                else giayTos = lichChuyenGiao.GIAYTO.Split('|');
                string cbbGiayTo = "<select id = 'cbbGiayTo' class='form-control' multiple>\r\n";
                foreach (var giayTo in giayTos)
                {
                    var oGiayTo = CallBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().CreateModel(ORenderInfo, giayTo);
                    cbbGiayTo += string.Format("<option {0} value={1}>{2}</option>\r\n", "selected", giayTo, oGiayTo != null ? oGiayTo.Ten : giayTo);
                }
                cbbGiayTo += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "<form action='javascript:SaveLichChuyenGiao();'> \r\n" +
                    "<input type='hidden' id='hdClose'>\r\n" +
                    "<input type='hidden' id='hdLichChuyenGiaoId' value='" + lichChuyenGiaoId + "'>\r\n" +
                    "<div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                    "     <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Kỹ thuật chuyển giao:") + " <span style='color:red'>*</span>\r\n" +
                                       cbbKyThuatChuyenGiao +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Khóa học:") + "\r\n" +
                                       cbbKhoaHoc +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Bệnh viện nhận chuyển giao:") +
                                       cbbBenhVien +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao:") + " <span style='color:red'>*</span>\r\n" +
                                       cbbBacSy +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Bắt đầu chuyển giao:") + " <span style='color:red'>*</span>\r\n" +
                    "                  <input style='font-size: 14px;' type='text' data-mask='99/99/9999' required class='form-control datepicker' id='dtBatDau' value='" + (string.IsNullOrEmpty(lichChuyenGiao.ID) ? null : lichChuyenGiao.BATDAU.ToString("dd/MM/yyyy")) + "'>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Kết thúc chuyển giao:") + " <span style='color:red'>*</span>\r\n" +
                    "                  <input style='font-size: 14px;' type='text' data-mask='99/99/9999' required class='form-control datepicker' id='dtKetThuc' value='" + (string.IsNullOrEmpty(lichChuyenGiao.ID) ? null : lichChuyenGiao.KETTHUC.ToString("dd/MM/yyyy")) + "'>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "         <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Giấy tờ:") + "\r\n" +
                                       cbbGiayTo +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "         </div>\r\n" +
                    "       </div>\r\n" +
                    "</div>\r\n" +
                    "<div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                    "   <input type='submit' class='popupSaveClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu & đóng") + "' onclick='javascript:$(\"#hdClose\").val(1);' style='float:right; margin: 7px;'> \r\n" +
                    "   <input type='submit' class='popupSave btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' onclick='javascript:$(\"#hdClose\").val(0);' style='float:right; margin: 7px;'> \r\n" +
                    "</div>\r\n" +
                    "</form>\r\n" +
                    "</div>\r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
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
        #region Xử lý nghiệp vụ
        private static string GetDataPermissionQuery(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
            BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = user.OwnerUserId });
            string bacSyIds = "";
            foreach (var bsou in bacSyOwnerUsers)
                bacSyIds += string.Format("'{0}',", bsou.BACSYID);
            if (!string.IsNullOrEmpty(bacSyIds))
                bacSyIds = bacSyIds.Substring(0, bacSyIds.Length - 1);
            bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoCls.ePermission.PheDuyet.ToString(), new DT_LichChuyenGiaoPermission().PermissionFunctionCode, DT_LichChuyenGiaoPermission.StaticPermissionFunctionId, user.OwnerUserId);
            //Quyền dữ liệu
            //1. Người tạo được hiển thị ở mọi trạng thái.
            //2. Cán bộ chuyển giao của lịch được xem lịch ở trạng thái đã duyệt và hoàn tất.
            //3. Người được phân quyền phê duyệt lịch chuyển giao được hiển thị ở những trạng thái khác trạng thái mới.
            //4. Người được phân quyền xem/sửa/xóa/gửi duyệt trên lịch chuyển giao của người khác được hiển thị ở mọi trạng thái.
            return string.Format(" and (DT_LichChuyenGiao.NGUOITAO_ID = '{0}' or {9} = 1 " +
                                    (!string.IsNullOrEmpty(bacSyIds) ?
                                    "   OR (DT_LichChuyenGiao.BACSY_ID in (" + bacSyIds + ") and (DT_LichChuyenGiao.TRANGTHAI = {7} or DT_LichChuyenGiao.TRANGTHAI = {8}))" : null) +
                                    (pheDuyetPermission ?
                                    "  OR DT_LichChuyenGiao.TRANGTHAI<>{6} " : null) +
                                    "  OR ( " +
                                    "        select count(1) " +
                                    "        from TablePermissionDataAccess join TablePermissionData on ISNULL(TablePermissionDataAccess.AllowAccess,0)=1 and TablePermissionData.frkOwnerUserId=DT_LichChuyenGiao.NGUOITAO_ID and PermissionDataId=frkPermissionDataId " +
                                    "                                        join TablePermissionFunction on PermissionFunctionId=TablePermissionData.frkPermissionFunctionId " +
                                    "                                        join TablePermissionFunctionItem on TablePermissionFunctionItem.frkPermissionFunctionId=PermissionFunctionId and PermissionFunctionItemId=frkPermissionFunctionItemId " +
                                    "        where PermissionFunctionCode='{1}' " +
                                    "              and (PermissionFunctionItemCode='{2}' or PermissionFunctionItemCode='{3}' or PermissionFunctionItemCode='{4}' or PermissionFunctionItemCode='{5}') " +
                                    "              and ( " +
                                    "                   TablePermissionDataAccess.frkOwnerUserId='{0}'" +//user
                                    "                   or TablePermissionDataAccess.frkRoleId in (select frkRoleId from TableOwnerUserBelongRole where frkOwnerUserId='{0}') " +//--role
                                    "                   or TablePermissionDataAccess.frkGroupRoleId in " +//GroupRole
                                    "                      ( " +
                                    "                           select frkGroupRoleId from TableRoleBelongGroupRole " +
                                    "                           where frkRoleId in  " +
                                    "                               ( " +
                                    "                                   select frkRoleId from TableOwnerUserBelongRole  " +
                                    "                                   where frkOwnerUserId='{0}' " +
                                    "                               ) " +
                                    "                      ) " +
                                    "              ) " +
                                    "  ) > 0) ", user.OwnerUserId,
                                                new DT_LichChuyenGiaoPermission().PermissionFunctionCode,
                                                DT_LichChuyenGiaoCls.ePermission.Xem.ToString(),
                                                DT_LichChuyenGiaoCls.ePermission.Sua.ToString(),
                                                DT_LichChuyenGiaoCls.ePermission.Xoa.ToString(),
                                                DT_LichChuyenGiaoCls.ePermission.GuiDuyet.ToString(),
                                                (int)DT_LichChuyenGiaoCls.eTrangThai.Moi,
                                                (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet,
                                                (int)DT_LichChuyenGiaoCls.eTrangThai.HoanTat,
                                                user.IsSystemAdmin);
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveLichChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string kyThuatChuyenGiao, string khoaHoc, string benhVien, string bacSy, string batDau, string ketThuc, string[] giayTo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoCls lichChuyenGiao;
                if (string.IsNullOrEmpty(lichChuyenGiaoId))
                {
                    lichChuyenGiao = new DT_LichChuyenGiaoCls();
                    lichChuyenGiao.ID = System.Guid.NewGuid().ToString();
                    lichChuyenGiao.KYTHUAT_MA = kyThuatChuyenGiao;
                    lichChuyenGiao.KHOAHOC_ID = khoaHoc;
                    lichChuyenGiao.BENHVIEN_MA = benhVien;
                    //lichChuyenGiao.LANHDAOBENHVIEN_ID = lanhDaoBenhVien;
                    lichChuyenGiao.BACSY_ID = bacSy;
                    lichChuyenGiao.BATDAU = DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichChuyenGiao.KETTHUC = DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichChuyenGiao.GIAYTO = giayTo != null ? string.Join("|", giayTo) : null;
                    lichChuyenGiao.NGAYTAO = DateTime.Now;
                    lichChuyenGiao.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichChuyenGiao.TRANGTHAI = (int)DT_LichChuyenGiaoCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Add(ORenderInfo, lichChuyenGiao);
                    RetAjaxOut.RetExtraParam1 = lichChuyenGiao.ID;
                }
                else
                {
                    lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                    lichChuyenGiao.KYTHUAT_MA = kyThuatChuyenGiao;
                    lichChuyenGiao.KHOAHOC_ID = khoaHoc;
                    lichChuyenGiao.BENHVIEN_MA = benhVien;
                    //lichChuyenGiao.LANHDAOBENHVIEN_ID = lanhDaoBenhVien;
                    lichChuyenGiao.BACSY_ID = bacSy;
                    lichChuyenGiao.BATDAU = DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichChuyenGiao.KETTHUC = DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichChuyenGiao.GIAYTO = giayTo != null ? string.Join("|", giayTo) : null;
                    lichChuyenGiao.NGAYSUA = DateTime.Now;
                    lichChuyenGiao.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Save(ORenderInfo, lichChuyenGiao.ID, lichChuyenGiao);
                }
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu.");
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DeleteLichChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Delete(ORenderInfo, lichChuyenGiaoId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut Export(RenderInfoCls ORenderInfo, int? nam, int? trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                FlexCelReport flexCelReport = new FlexCelReport();

                List<DT_LichChuyenGiaoView> Datas = new List<DT_LichChuyenGiaoView>();

                SiteParam
                      OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                string keyword = WebEnvironments.Request("Keyword");
                DT_LichChuyenGiaoFilterCls filter = new DT_LichChuyenGiaoFilterCls()
                {
                    Keyword = keyword,
                    Nam = nam,
                    TrangThai = trangThai,
                    DataPermissionQuery = GetDataPermissionQuery(ORenderInfo)
                };
                DT_LichChuyenGiaoCls[] lichChuyenGiaos = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Reading(ORenderInfo, filter);

                for (int i = 0; i < lichChuyenGiaos.Length; i++)
                {
                    DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaos[i].KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaos[i].KYTHUAT_MA);
                    OneMES3.DM.Model.BenhVienCls benhVien = null;
                    if (!string.IsNullOrEmpty(lichChuyenGiaos[i].BENHVIEN_MA))
                    {
                        benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(sRenderInfo, lichChuyenGiaos[i].BENHVIEN_MA);
                    }
                    BacSyCls canBoChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaos[i].BACSY_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiaos[i].BACSY_ID);
                    DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(lichChuyenGiaos[i].KHOAHOC_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, lichChuyenGiaos[i].KHOAHOC_ID);

                    var view = new DT_LichChuyenGiaoView();
                    view.KYTHUAT = kyThuatChuyenGiao == null ? lichChuyenGiaos[i].KYTHUAT_MA : kyThuatChuyenGiao.Ten;
                    view.KHOAHOC = khoaHoc == null ? null : khoaHoc.TEN;
                    view.BENHVIEN = benhVien == null ? lichChuyenGiaos[i].BENHVIEN_MA : benhVien.Ten;
                    view.BACSY = canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN;
                    view.BATDAU = lichChuyenGiaos[i].BATDAU.ToString("dd/MM/yyyy");
                    view.KETTHUC = lichChuyenGiaos[i].KETTHUC.ToString("dd/MM/yyyy");
                    view.TRANGTHAI = DT_LichChuyenGiaoParser.TrangThais[lichChuyenGiaos[i].TRANGTHAI];

                    Datas.Add(view);
                }
                flexCelReport.AddTable("LichChuyenGiao", Datas);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\DT_LICHCHUYENGIAO.xlsx";

                string fileName = "DT_LichChuyenGiao_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                //string fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + System.Web.Configuration.WebConfigurationManager.AppSettings["TemporaryFilePath"]), fileName);
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string fileSavePath = WebConfig.ConvertPathRoot(OSiteParam, Path.Combine(Directoryfile, fileName));
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().ExportEx(ORenderInfo, LoginName, XmlFile, "LichChuyenGiao", flexCelReport, fileSavePath);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (lichChuyenGiao == null)
                {
                    DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);
                    BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Lịch chuyển giao") + " " + (kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten) + " " + "bắt đầu ngày " + " " + lichChuyenGiao.BATDAU.ToString("dd/MM/yyyy") + " " + WebLanguage.GetLanguage(OSiteParam, "đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                if ((trangThai == (int)DT_LichChuyenGiaoCls.eTrangThai.Moi && lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet)
                || (trangThai == (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet && (lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.Moi && lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet && lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi))
                || (trangThai == (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet && lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet)
                || (trangThai == (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi && lichChuyenGiao.TRANGTHAI != (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet))
                {
                    DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);
                    BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Lịch chuyển giao") + " " + (kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten) + " " +
                        WebLanguage.GetLanguage(OSiteParam, "bắt đầu") + " " + lichChuyenGiao.BATDAU.ToString("dd/MM/yyyy") + " " +
                        WebLanguage.GetLanguage(OSiteParam, "kết thúc") + " " + lichChuyenGiao.KETTHUC.ToString("dd/MM/yyyy") + " " +
                        WebLanguage.GetLanguage(OSiteParam, "do") + " " + (canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN) + " " +
                        WebLanguage.GetLanguage(OSiteParam, "phụ trách chuyển giao không được phép thực hiện tác vụ này.");
                    return RetAjaxOut;
                }
                lichChuyenGiao.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Save(ORenderInfo, lichChuyenGiao.ID, lichChuyenGiao);
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
                return "<div class=\"col-md-1\" style=\"padding-top:12px; padding-bottom: 12px; min-width: 180px;\">\r\n" +
                       "    <input id=\"txtNam\" placeholder=\"Năm\" class=\"form-control valueForm yearpicker\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; width: 250px;\">\r\n" +
                       "    <select id=\"cbbKyThuatChuyenGiaoFilter\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; width: 250px;\">\r\n" +
                       "    <select id=\"cbbBenhVienFilter\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; width: 250px;\">\r\n" +
                       "    <select id=\"cbbBacSyFilter\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top:12px; width: 250px;\">\r\n" +
                       "    <select id=\"cbbKhoaHocFilter\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                        "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px;\">\r\n" +
                       "    <button id = 'btnTimKiem' class='btn btn-sm  mr-10px' onclick='FilterChange()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Tìm kiếm</strong></button>\r\n" +
                       "</div>\r\n"

                   ;
            }
        }
        #endregion
    }
}
