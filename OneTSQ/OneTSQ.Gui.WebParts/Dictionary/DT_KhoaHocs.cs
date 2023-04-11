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
using FlexCel.Report;
using OneTSQ.Core.Model;
using OneTSQ.UploadUtility;

namespace OneTSQ.WebParts
{
    public class DT_KhoaHocs : WebPartTemplate
    {
        class DT_KhoaHocView
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string KHOA { get; set; }
            public string THOILUONG { get; set; }
            public string NGAYKHAIGIANGDUKIEN { get; set; }
            public string HANNOPHOSO { get; set; }
            public int? SOLUONGHOCVIENDUKIEN { get; set; }
            public decimal? HOCPHI { get; set; }
            public string LOAIDAOTAO { get; set; }
            public string LOAIHINHDAOTAO { get; set; }
            public string DOITUONG { get; set; }
            public string LOAIKHOAHOC { get; set; }
            public string DONVIHOTRO_MA { get; set; }
            public string MOTA { get; set; }
            public string TRANGTHAI { get; set; }
        }
        public override string WebPartId
        {
            get
            {
                return "DT_KhoaHocs";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách khóa học";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách khóa học";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_KhoaHocs), Page);
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
            int? nam = string.IsNullOrEmpty(WebEnvironments.Request("Nam")) ? null : (int?)int.Parse(WebEnvironments.Request("Nam"));
            int? loaiKhoaHoc = string.IsNullOrEmpty(WebEnvironments.Request("LoaiKhoaHoc")) ? null : (int?)int.Parse(WebEnvironments.Request("LoaiKhoaHoc"));
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();
                var user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                bool xemPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xem.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool themPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Them.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool suaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Sua.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool xoaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xoa.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool guiDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.GuiDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.PheDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                var trangThais = xemPermission || themPermission || suaPermission || xoaPermission || guiDuyetPermission ? DT_KhoaHocParser.TrangThais : DT_KhoaHocParser.TrangThais.Where(o => o.Key != (int)DT_KhoaHocCls.eTrangThai.Moi);

                string cbbLoaiKhoaHoc = "";
                foreach (var lkh in DT_KhoaHocParser.LoaiKhoaHocs)
                    cbbLoaiKhoaHoc += string.Format("<option value={0} {1}>{2}</option>", lkh.Key, loaiKhoaHoc == lkh.Key ? "selected" : null, lkh.Value);

                string cbbTrangThai = "";
                foreach (var tt in trangThais)
                    cbbTrangThai += "<option value=" + tt.Key + " " + (trangThai == tt.Key ? "selected" : "") + ">" + tt.Value + "</option>";
                string filtemplate = "temp/ImportDT_KHOAHOC.xlsx";
                string tokenURL = WebConfig.GetWebConfig("TokenURL");
                string createRoomURL = WebConfig.GetWebConfig("CreateRoomURL");
                string stringeeServerAddr = WebConfig.GetWebConfig("StringeeServerAddr");

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(

                "<script src=\"../../../../themes/js/plugins/stringee/latest.sdk.bundle.min.js\" type=\"text/javascript\"></script>\r\n" +
                 "<script language=\"javascript\">\r\n" +
                "   var tokenUrl = \"" + tokenURL + "\";\r\n" +
                "   var createRoomURL = \"" + createRoomURL + "\";\r\n" +
                "   var stringeeServerAddr = \"" + stringeeServerAddr + "\";\r\n" +
                "   var stringeeClient;\r\n" +
                "   var loginedStringeeAccount;\r\n" +
                "   var CheckedArr=[];\r\n" +
                "   var checkedTrangThai=-1;\r\n" +
                "   var RenderInfo=CreateRenderInfo();\r\n" +
                "   $(document).ready(function() {\r\n" +
                "       ConnectStringeeServer();\r\n" +
                "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách khóa học") + "';\r\n" +
                "       $('.yearpicker').datetimepicker({ \r\n" +
                "           format: 'YYYY' \r\n" +
                "       }); \r\n" +
                "       cbbLoaiKhoaHocFilter.innerHTML = '" + cbbLoaiKhoaHoc + "';\r\n" +
                "       $('#cbbLoaiKhoaHocFilter').select2({\r\n" +
                "          placeholder: 'Nguồn kinh phí',\r\n" +
                "          allowClear: true\r\n" +
                "       });\r\n" +
                "       $('#cbbLoaiKhoaHocFilter').select2('val', " + (loaiKhoaHoc == null ? "null" : loaiKhoaHoc.ToString()) + ");\r\n" +
                "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                "       $('#cbbTrangThai').select2({\r\n" +
                "          placeholder: 'Trạng thái',\r\n" +
                "          allowClear: true\r\n" +
                "       });\r\n" +
                "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                "       $('#txtKeyword').val('" + keyword + "');\r\n" +
                "       $('#txtNam').val('" + nam + "');\r\n" +
                "   });\r\n" +
                #region Tạo room
                "  function MakeRoom(roomName, uniqueRoomName){\r\n" +
                //make room và create Conversation
                "        var data = {\r\n" +
                "            name : roomName,\r\n" +
                "            uniqueName: uniqueRoomName\r\n" +
                "        }\r\n" +
                "        $.ajax({\r\n" +
                "            type: 'POST',\r\n" +
                "            url: createRoomURL,\r\n" +
                //"            headers: {'X-STRINGEE-AUTH': rest_access_token}, \r\n" +
                "            dataType: 'json',\r\n" +
                "            contentType: 'application/json',\r\n" +
                "            data: JSON.stringify(data),\r\n" +
                "            success: function(response) {\r\n" +
                "                 if(response.r == 0 || response.r == 2)\r\n" +
                "                 {\r\n" +
                "                      console.log('MakeRoom: ', response);\r\n" +
                "                      roomId = response.roomId;\r\n" +
                "	                   khoaHocId = response.uniqueName;\r\n" +
                "                      OneTSQ.WebParts.DT_KhoaHocs.UpdateRoomId(RenderInfo, khoaHocId, roomId).value;\r\n" +
                "                 }\r\n" +
                "                 else{\r\n" +
                "                 }\r\n" +
                "            },\r\n" +
                "            error : function(response) { \r\n" +
                "            } \r\n" +
                "        });\r\n" +
                "       $('#divFormModal').modal('hide');\r\n" +
                "  }\r\n" +
                #endregion
                #region Ket noi den Stringee Server
                "function ConnectStringeeServer() {\r\n" +
                "   stringeeClient = new StringeeClient();\r\n" +
                "   stringeeClient._stringeeServerAddr = stringeeServerAddr;\r\n" +
                "   SettingClientEvents(stringeeClient);\r\n" +
                "   GetAccessTokenAndConnectToStringee(stringeeClient);\r\n" +
                " }\r\n" +
                #endregion
                #region GetAccessTokenAndConnectToStringee để tạo roomToken
                    "function GetAccessTokenAndConnectToStringee(client) {\r\n" +
                    "    $.getJSON(tokenUrl + '?userId=" + user.LoginName + " &rest=true', function(res) {\r\n" +
                    "        access_token = res.access_token;\r\n" +
                    "        rest_access_token = res.rest_access_token;\r\n" +
                    "        client.connect(access_token);\r\n" +
                    "    });\r\n" +
                    "}\r\n" +
                #endregion
                #region Các sự kiện stringee client
                "    function SettingClientEvents(client){\r\n" +
                #region Các sự kiện kết nối stringee
                "        client.on('connect', function () {\r\n" +
                "            console.log('connected');\r\n" +
                "        });\r\n" +

                "        client.on('authen', function (res) {\r\n" +
                "            console.log('authen', res);\r\n" +
                "            loginedStringeeAccount = res.userId;\r\n" +
                "        });\r\n" +

                "        client.on('disconnect', function () {\r\n" +
                "            console.log('disconnected');\r\n" +
                "        });\r\n" +

                "        client.on('requestnewtoken', function () {\r\n" +
                "            console.log('++++++++++++++ requestnewtoken; please get new access_token from YourServer and call client.connect(new_access_token)+++++++++');\r\n" +
                "            //please get new access_token from YourServer and call: \r\n" +
                "            GetAccessTokenAndConnectToStringee(client);\r\n" +
                "        });\r\n" +
                #endregion                
                "    }\r\n" +
                #endregion

                "   var CurrentPageIndex=0;\r\n" +

                "   function NextPage(PageIndex)\r\n" +
                "   {\r\n" +
                "       CurrentPageIndex = PageIndex;\r\n" +
                "       setTimeout('Search()',10);\r\n" +
                "   }\r\n" +

                "   function Search()\r\n" +
                "   {\r\n" +
                "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                "       nam = parseInt(document.getElementById('txtNam').value);\r\n" +
                "       loaiKhoaHoc = parseInt(document.getElementById('cbbLoaiKhoaHocFilter').value);\r\n" +
                "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                "       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, nam, loaiKhoaHoc, trangThai, CheckedArr).value;\r\n" +
                "       document.getElementById('divDT_KhoaHocs').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "   }\r\n" +
                "   function FilterChange(){\r\n" +
                "       CheckedArr=[];\r\n" +
                "       checkedTrangThai=-1;\r\n" +
                "       CurrentPageIndex = 0;\r\n" +
                "       Search();\r\n" +
                "   }\r\n" +
                #region import DT_KhoaHoc
                "   function Import()\r\n" +
                "   {\r\n" +
                "        RenderInfo=CreateRenderInfo();\r\n" +
                "        fileUploadValue=document.getElementById('fileUpload').value;\r\n" +
                "        if(fileUploadValue==''){\r\n" +
                "            callSweetAlert('" + WebLanguage.GetLanguage(OSiteParam, "Chưa xác định tài liệu gắn kèm!") + "');\r\n" +
                "            return;\r\n" +
                "        }\r\n" +      
                "       swal({ \r\n" +
                "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "\", \r\n" +
                "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn import thông tin này không?") + "\", \r\n" +
                "               type: \"warning\", \r\n" +
                "               showCancelButton: true, \r\n" +
                "               confirmButtonColor: \"#DD6B55\", \r\n" +
                "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                "               closeOnConfirm: true \r\n" +
                "           }, function () { \r\n" +
                "        $.LoadingOverlay('show');\r\n" +
                "                  var fd = new FormData();\r\n" +
                "               fd.append(\"fileUploadAvatar\", document.getElementById('fileUpload').files[0]);\r\n" +
                "               var xhr = new XMLHttpRequest();\r\n" +
                "               xhr.addEventListener(\"load\",uploaded, false);\r\n" +
                "               xhr.open(\"POST\", \"" + WebConfig.GetImportHandler(OSiteParam, SessionId, user.OwnerUserId, user.LoginName, (int)ProcessImportHandlerUtility.eFileType.DT_KHOAHOC) + "\");\r\n" +
                "               xhr.send(fd);\r\n" +
                "           }\r\n" +
                "       ); \r\n" +
                "   }\r\n" +
                " function uploaded(evt) {\r\n" +
                "       $.LoadingOverlay('hide');\r\n" +
                "       var parser = new DOMParser();\r\n" +
                "       var xmlDoc = parser.parseFromString(evt.currentTarget.responseText,\"text/xml\"); \r\n" +
                "       var mes = xmlDoc.getElementsByTagName(\"InfoMessage\")[0].childNodes[0].nodeValue;\r\n" +
                "       callSweetAlert(mes);\r\n" +
                "       FilterChange();\r\n" +
                " }\r\n" +
                #endregion
                #region Export dữ liệu khóa học ra file XML
                "   function Export()\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                "       nam = parseInt(document.getElementById('txtNam').value);\r\n" +
                "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                "       AjaxOut  = OneTSQ.WebParts.DT_KhoaHocs.Export(RenderInfo, keyword, nam, trangThai).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       window.open(AjaxOut.RetUrl, 'Download');\r\n" +
                "   }\r\n" +
                #endregion
                #region control
                "function CheckCurrency(e){\r\n" +
                "    if (event.which != 8 && isNaN(String.fromCharCode(e.which))&&event.which != 46){\r\n" +
                "         event.preventDefault();\r\n" +
                "        }\r\n" +
                "    }\r\n" +
                #endregion
                #region Show popup khóa học

                "function ShowPopupKhoaHoc(khoaHocId)\r\n" +
                "{\r\n" +
                "     RenderInfo=CreateRenderInfo();\r\n" +
                "     AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.PopupKhoaHoc(RenderInfo, khoaHocId).value;\r\n" +
                "     if(AjaxOut.Error)\r\n" +
                "     {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "     }\r\n" +
                "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới/cập nhật khóa học") + "</span>';\r\n" +
                "     $('#divFormModal').modal('show');\r\n" +
                "     $('.datepicker').datetimepicker({ \r\n" +
                "         format: 'DD/MM/YYYY' \r\n" +
                "     }); \r\n" +
                //"     $('#cbbLoaiKhoaHoc').selectpicker({ \r\n" +
                //"     }); \r\n" +
                //"     $('#cbbDoiTuong').selectpicker({ \r\n" +
                //"     }); \r\n" +
                "     ValidateIntegerControl('#txtSoLuongHocVienDuKien', 0, 999);\r\n" +
                "       Select2(); \r\n" +
                "}\r\n" +
                #endregion Show popup khóa học
                #region Cập nhật mã khóa học theo thay đổi combobox tên khóa học và textbox khóa
                "   function FillMaKhoaHoc(){\r\n" +
                "       if(cbbTenKhoaHoc.value != '' && txtKhoa.value != '')\r\n" +
                "       {\r\n" +
                "           txtMa.value = cbbTenKhoaHoc.value + txtKhoa.value;\r\n" +
                "       }\r\n" +
                "       else{\r\n" +
                "           txtMa.value = '';\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                #endregion
                #region Cập nhật ngày kết thúc khóa học theo thay đổi textbox thời lượng, combobox Loại thời lượng và textbox ngày khai giảng dự kiến
                "   function FillNgayKetThucKhoaHoc(){\r\n" +
                "       if(txtThoiLuong.value != '' && cbbLoaiThoiLuong.value != '' && dtNgayKhaiGiangDuKien.value != '')\r\n" +
                "       {\r\n" +
                "           AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.GetNgayKetThucKhoaHoc(RenderInfo, dtNgayKhaiGiangDuKien.value, parseInt(txtThoiLuong.value), cbbLoaiThoiLuong.value).value;\r\n" +
                "           if(!AjaxOut.Error)\r\n" +
                "           {\r\n" +
                "               dtNgayBeGiangDuKien.value = AjaxOut.RetExtraParam1;\r\n" +
                "           }\r\n" +
                "       }\r\n" +
                "       else{\r\n" +
                "           dtNgayBeGiangDuKien.value = '';\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                #endregion
                #region Cập nhật hạn nộp hồ sơ theo thay đổi textbox ngày khai giảng dự kiến
                "   function FillHanNopHoSo(){\r\n" +
                "       if(dtNgayKhaiGiangDuKien.value != '')\r\n" +
                "       {\r\n" +
                "           AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.GetNgayNopHoSo(RenderInfo, dtNgayKhaiGiangDuKien.value).value;\r\n" +
                "           if(!AjaxOut.Error)\r\n" +
                "           {\r\n" +
                "               dtHanNopHoSo.value = AjaxOut.RetExtraParam1;\r\n" +
                "               if(AjaxOut.RetBoolean)\r\n" +
                "               {\r\n" +
                "                   $('#dtNgayKhaiGiangDuKien').addClass('clsAlert');\r\n" +
                "               }\r\n" +
                "               else\r\n" +
                "               {\r\n" +
                "                   $('#dtNgayKhaiGiangDuKien').removeClass('clsAlert');\r\n" +
                "               }\r\n" +
                "               if(AjaxOut.RetObject==true)\r\n" +
                "               {\r\n" +
                "                   $('#dtHanNopHoSo').addClass('clsAlert');\r\n" +
                "               }\r\n" +
                "               else\r\n" +
                "               {\r\n" +
                "                   $('#dtHanNopHoSo').removeClass('clsAlert');\r\n" +
                "               }\r\n" +
                "           }\r\n" +
                "       }\r\n" +
                "       else{\r\n" +
                "           dtHanNopHoSo.value = '';\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                #endregion
                #region Bắt sự kiện thay đổi combobox nguồn kinh phí
                "   function cbbLoaiKhoaHoc_onchange(sender){\r\n" +
                "       if(sender.value == " + (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc + ")\r\n" +
                "       {\r\n" +
                "           $('#divDonViHoTro').show();\r\n" +
                "       }\r\n" +
                "       else{\r\n" +
                "           $('#divDonViHoTro').hide();\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                #endregion
                #region Save Thông tin khóa học
                "function SaveKhoaHoc()\r\n" +
                "{\r\n" +
                "     RenderInfo=CreateRenderInfo();\r\n" +
                "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                "     ma = document.getElementById('txtMa').value;\r\n" +
                "     ten = document.getElementById('cbbTenKhoaHoc').value;\r\n" +
                "     khoa = document.getElementById('txtKhoa').value;\r\n" +
                "     thoiLuong = parseInt(document.getElementById('txtThoiLuong').value);\r\n" +
                "     loaiThoiLuong = document.getElementById('cbbLoaiThoiLuong').value;\r\n" +
                "     ngayKhaiGiangDuKien = document.getElementById('dtNgayKhaiGiangDuKien').value;\r\n" +
                "     ngayBeGiangDuKien = document.getElementById('dtNgayBeGiangDuKien').value;\r\n" +
                "     hanNopHoSo = document.getElementById('dtHanNopHoSo').value;\r\n" +
                "     soLuongHocVienDuKien = parseInt(document.getElementById('txtSoLuongHocVienDuKien').value);\r\n" +
                "     hocPhi = parseFloat(document.getElementById('txtHocPhi').value == '' ? '' : parseFloat(document.getElementById('txtHocPhi').value.replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(',', '')).toFixed(2));\r\n" +
                "     loaiDaoTao = parseInt(document.getElementById('cbbLoaiDaoTao').value);\r\n" +
                "     loaiHinhDaoTao = parseInt(document.getElementById('cbbLoaiHinhDaoTao').value);\r\n" +
                "     doiTuong = $('#cbbDoiTuong').val();\r\n" +
                "     loaiKhoaHoc = parseInt(document.getElementById('cbbLoaiKhoaHoc').value);\r\n" +
                "     donViHoTro = document.getElementById('cbbDonViHoTro').value;\r\n" +
                "     tieuChuan = $('#cbbTieuChuan').val();\r\n" +
                "     moTa = document.getElementById('txtMoTa').value;\r\n" +             
                "     AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.SaveKhoaHoc(RenderInfo, khoaHocId, ma, ten, khoa, thoiLuong, loaiThoiLuong, ngayKhaiGiangDuKien, ngayBeGiangDuKien, hanNopHoSo, soLuongHocVienDuKien, hocPhi, loaiDaoTao, loaiHinhDaoTao, doiTuong, loaiKhoaHoc, donViHoTro, tieuChuan, moTa).value;\r\n" +
                "     if(AjaxOut.Error)\r\n" +
                "     {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "     }\r\n" +
                "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                "     {\r\n" +
                "           document.getElementById('hdKhoaHocId').value = AjaxOut.RetExtraParam1;\r\n" +
                "     }\r\n" +
                "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                "     if($('#hdClose').val() == 1)\r\n" +
                "     {\r\n" +
                "          $('#divFormModal').modal('hide');\r\n" +
                "     }\r\n" +
                "     FilterChange();\r\n" +
                "}\r\n" +
                #endregion Save Thông tin khóa học
                #region Xóa Thông tin khóa học
                "function DeleteKhoaHoc(khoaHocId)\r\n" +
                "{\r\n" +
                "   swal({\r\n" +
                "           title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                "           text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắn chắn xóa khóa học này không?") + "\",\r\n" +
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
                "               AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.DeleteKhoaHoc(RenderInfo, khoaHocId).value;\r\n" +
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
                #endregion Xóa Thông tin khóa học

                #region Ẩn hiện các button phê duyệt theo trạng thái khóa học được chọn trên danh sách.
                "   function ShowHideButton(){\r\n" +
                "       $('.clsPheDuyet').hide();\r\n" +
                "       if (checkedTrangThai == " + (int)DT_KhoaHocCls.eTrangThai.Moi + " && '" + guiDuyetPermission + "' == 'True')\r\n" +
                "           $('#btnGuiDuyet').show();\r\n" +
                "       else if (checkedTrangThai == " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ")\r\n" +
                "       {\r\n" +
                "           if ('" + guiDuyetPermission + "' == 'True')\r\n" +
                "               $('#btnThuHoiGuiDuyet').show();\r\n" +
                "           if ('" + pheDuyetPermission + "' == 'True'){\r\n" +
                "               $('#btnDuyet').show();\r\n" +
                "               $('#btnTuChoiDuyet').show();\r\n" +
                "           }\r\n" +
                "       }\r\n" +
                "       else if (checkedTrangThai == " + (int)DT_KhoaHocCls.eTrangThai.Duyet + " && '" + pheDuyetPermission + "' == 'True')\r\n" +
                "           $('#btnThuHoiDuyet').show();\r\n" +
                "       else if (checkedTrangThai == " + (int)DT_KhoaHocCls.eTrangThai.TuChoi + " && '" + pheDuyetPermission + "' == 'True')\r\n" +
                "           $('#btnThuHoiTuChoiDuyet').show();\r\n" +
                "   }\r\n" +
                #endregion Ẩn hiện các button phê duyệt theo trạng thái khóa học được chọn trên danh sách.
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
                    "           KhoaHocNumber = trangThais.length;\r\n" +
                    "           for(i = 0; i < KhoaHocNumber; i++) {\r\n" +
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
                    "               CheckedKhoaHocNumber = CheckedArr.length;\r\n" +
                    "               for(i = 0; i < CheckedKhoaHocNumber; i++) {\r\n" +
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

                #region Chuyển duyệt khóa học
                    "   function ChuyenDuyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần gửi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn gửi duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn gửi duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
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
                #endregion Chuyển duyệt khóa học
                #region Thu hồi chuyển duyệt khóa học
                    "   function ThuHoiChuyenDuyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần thu hồi gửi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi gửi duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi gửi duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.Moi + ").value;\r\n" +
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
                #endregion Thu hồi chuyển duyệt khóa học
                #region Duyệt khóa học
                    "   function Duyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.Duyet + ").value;\r\n" +
                    "                       if(AjaxOut.Error)\r\n" +
                    "                       {\r\n" +
                    "	                        errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "                       }\r\n" +
                    "                       else if(AjaxOut.RetObject == '')\r\n" +
                    "                       {\r\n" +
                    "	                        MakeRoom(AjaxOut.RetObject1, CheckedArr[index]);\r\n" +
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
                #endregion Duyệt khóa học
                #region Thu hồi duyệt khóa học
                    "   function ThuHoiDuyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần thu hồi duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
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
                #endregion Thu hồi duyệt khóa học
                #region Từ chối duyệt khóa học
                    "   function TuChoiDuyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần từ chối duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn từ chối duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn từ chối duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.TuChoi + ").value;\r\n" +
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
                #endregion Từ chối duyệt khóa học
                #region Thu hồi từ chối duyệt khóa học
                    "   function ThuHoiTuChoiDuyet(){\r\n" +
                    "       soLuongKhoaHoc= CheckedArr.length;\r\n" +
                    "       if (soLuongKhoaHoc == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn khóa học cần thu hồi từ chối duyệt") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           swal({ \r\n" +
                    "                   title: (soLuongKhoaHoc > 1 ? \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi từ chối duyệt những khóa học này?") + "\" : \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi từ chối duyệt khóa học này?") + "\"), \r\n" +
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
                    "                   for (var index=0; index < soLuongKhoaHoc; index ++) {\r\n" +
                    "                       AjaxOut = OneTSQ.WebParts.DT_KhoaHocs.UpdateTrangThai(RenderInfo, CheckedArr[index], " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
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
                #endregion Thu hồi từ chối duyệt khóa học
                #region Select2
                    "   function Select2()\r\n" +
                    "   {\r\n" +
                    "     CallInitSelect2('cbbTenKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TenKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbDonViHoTro', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị hỗ trợ") + "');\r\n" +
                    "     CallInitSelect2('cbbTieuChuan', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TieuChuanThamGiaKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn tham gia khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbNhomKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_NhomKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nhóm khóa học") + "');\r\n" +
                    "   }\r\n" +                 
                #endregion
                "</script>\r\n"
                    ) +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                "<style>\r\n" +
                "   .clsAlert{color: red;}\r\n" +
                "</style>\r\n" +

                "<div id=\"divListForm\">\r\n" +
                " <div class=\"ibox float-e-margins\"> \r\n" +
                "     <div class=\"ibox-title\"> \r\n" +
                "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách khóa học") + " </h5> \r\n" +
                "     </div> \r\n" +
                "     <div class=\"ibox-content\"> \r\n" +
                "           <input type='button' id='btnGuiDuyet' title='Gửi duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Gửi duyệt") + "' onclick='javascript:ChuyenDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                "           <input type='button' id='btnThuHoiGuiDuyet' title='Thu hồi gửi duyệt' class='DT_KhoaHocThuHoi btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiChuyenDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                "           <input type='button' id='btnDuyet' title='Duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "' onclick='javascript:Duyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                "           <input type='button' id='btnTuChoiDuyet' title='Từ chối duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Từ Chối") + "' onclick='javascript:TuChoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                "           <input type='button' id='btnThuHoiDuyet' title='Thu hồi duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                "           <input type='button' id='btnThuHoiTuChoiDuyet' title='Thu hồi từ chối duyệt' class='btn btn-primary clsPheDuyet' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiTuChoiDuyet();' style='float:right; margin-left: 5px; display:none;'>\r\n" +
                (themPermission ?
                "           <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\" style=\"\">\r\n" +
                "               <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:Export();\"> " + WebLanguage.GetLanguage(OSiteParam, "Xuất dữ liệu") + "</button>\r\n" +
                "               <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"location.href='" + filtemplate + "'\"><i class=\"fa fa-download\"></i> " + WebLanguage.GetLanguage(OSiteParam, "File mẫu") + "</button>\r\n" +
                "               <button type=\"button\"  class=\"btn btn-sm btn-primary\" onclick=\"javascript:Import();\"> " + WebLanguage.GetLanguage(OSiteParam, "Nhập dữ liệu") + "</button>\r\n" +
                "               <span class=\"btn btn-default btn-file\" ><span class=\"fileinput-new\">Chọn File</span>\r\n" +
                "               <span class=\"fileinput-exists\">Chọn lại</span><input type=\"file\" name=\"...\" id=\"fileUpload\" accept=\".xlsx, .xls\"/></span>\r\n" +
                "               <span class=\"fileinput-filename\"></span>\r\n" +
                "               <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                "           </div> \r\n" : null) +
                "           <div id=\"divDT_KhoaHocs\">" + DT_KhoaHocs.DrawSearchResult(ORenderInfo, pageIndex, keyword, nam, loaiKhoaHoc, trangThai, new string[0]).HtmlContent + "</div>\r\n" +
                "     </div> \r\n" +
                " </div>\r\n" +
                "</div>\r\n" +
                "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n" +
                #region Phần hiển thị popup
                "<div id=\"divFormModal\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                "    <div class=\"modal-dialog\" style='width:90%; height:90%;'>\r\n" +
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
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, int? nam, int? loaiKhoaHoc, int? trangThai, string[] CheckedArr)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);

                DT_KhoaHocFilterCls filter = new DT_KhoaHocFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = keyword,
                    Nam = nam,
                    LoaiKhoaHoc = loaiKhoaHoc,
                    TrangThai = trangThai,
                    DataPermissionQuery = GetDataPermissionQuery(ORenderInfo)
                };
                bool themPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Them.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                if (trangThai == null)
                {
                    bool xemPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xem.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool suaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Sua.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool xoaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xoa.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool guiDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.GuiDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.PheDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    int[] trangThais = xemPermission || themPermission || suaPermission || xoaPermission || guiDuyetPermission ? DT_KhoaHocParser.TrangThais.Keys.ToArray() : DT_KhoaHocParser.TrangThais.Where(o => o.Key != (int)DT_KhoaHocCls.eTrangThai.Moi).Select(o => o.Key).ToArray();
                    filter.TrangThais = trangThais;
                }
                long recordTotal = 0;
                DT_KhoaHocCls[] khoaHocs = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int DT_KhoaHocTotal = khoaHocs.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                themPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Them.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);

                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\"><input type =\"checkbox\" id='ckbAllPage' onclick='ckbAllPage_onclick(this)'></th> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khóa học số") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Thời lượng học") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khai giảng dự kiến") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Hạn nộp hồ sơ") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên dự kiến") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                    "                     <th style='width:70px;'>" + ( "<a href='javascript:ShowPopupKhoaHoc(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới khóa học") + "'><i class='fa fa-plus' style='font-size:20px; color: white;'></i></a>" ) + "</th> \r\n" + //themPermission ?: null
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < DT_KhoaHocTotal; iIndex++)
                {
                    bool suaPermission = user.IsSystemAdmin == 1 || khoaHocs[iIndex].NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Sua.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId, khoaHocs[iIndex].NGUOITAO_ID);
                    bool xoaPermission = user.IsSystemAdmin == 1 || khoaHocs[iIndex].NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xoa.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId, khoaHocs[iIndex].NGUOITAO_ID);
                    var DT_KhoaHocUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_KhoaHoc",
                        new WebParamCls[] { new WebParamCls("id", khoaHocs[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("ChiSoMang", filter.PageIndex * filter.PageSize + iIndex),
                            new WebParamCls("Keyword", keyword),
                            new WebParamCls("nam", nam == null? null : nam.Value.ToString()),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});

                    int ArrIndex = CheckedArr.Count() - 1;
                    while (ArrIndex >= 0 && CheckedArr[ArrIndex] != khoaHocs[iIndex].ID)
                        ArrIndex--;

                    Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-center\" style='text-align: center; vertical-align: middle;'><input type =\"checkbox\" class=\"clsCkb\" id='" + khoaHocs[iIndex].ID + "' " + (ArrIndex >= 0 ? "checked" : "") + " onclick='ckb_onclick(this)'></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + khoaHocs[iIndex].MA + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + khoaHocs[iIndex].TENKHOAHOC + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + khoaHocs[iIndex].KHOA + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + (khoaHocs[iIndex].THOILUONG == null ? null : khoaHocs[iIndex].THOILUONG + " " + DT_KhoaHocParser.LoaiThoiLuongs[khoaHocs[iIndex].LOAITHOILUONG]) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + (khoaHocs[iIndex].NGAYKHAIGIANGDUKIEN == null ? null : khoaHocs[iIndex].NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + (khoaHocs[iIndex].HANNOPHOSO == null ? null : khoaHocs[iIndex].HANNOPHOSO.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_KhoaHocUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở khóa học") + "'>" + khoaHocs[iIndex].SOLUONGHOCVIENDUKIEN + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><input type='hidden' id='hdTrangThai" + khoaHocs[iIndex].ID + "' class='clsTrangThai' value=" + khoaHocs[iIndex].TRANGTHAI + ">" + DT_KhoaHocParser.sColorTrangThai[khoaHocs[iIndex].TRANGTHAI] + "</td> \r\n" +
                            "                     <td style='text-align:center; vertical-align: middle;'>\r\n";
                    if ((khoaHocs[iIndex].TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Moi || khoaHocs[iIndex].TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.TuChoi))
                        Html +=
                            "                   <a id='btnEditThanhVien" + iIndex + "' class='CssDisplayItemThanhVien' href='javascript:ShowPopupKhoaHoc(\"" + khoaHocs[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n";
                    if ((khoaHocs[iIndex].TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Moi || khoaHocs[iIndex].TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.TuChoi))
                        Html +=
                            "                   <a id='btnDeleteThanhVien" + iIndex + "' class='CssDisplayItemThanhVien' href='javascript:DeleteKhoaHoc(\"" + khoaHocs[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n";
                    Html +=
                        "                    </td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + DT_KhoaHocTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut Deletefile(RenderInfoCls ORenderInfo, string filepath)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                File.Delete(filepath);
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
        public static AjaxOut PopupKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? new DT_KhoaHocCls() { LOAIKHOAHOC = (int)DT_KhoaHocCls.eLoaiKhoaHoc.Khac } : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);

                string cbbTenKhoaHoc = "<select class='form-control' id='cbbTenKhoaHoc' required  onchange='FillMaKhoaHoc()' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(khoaHoc.TEN))
                {
                    cbbTenKhoaHoc += string.Format(" <option value='{0}' selected>{1}</option>\r\n", khoaHoc.TEN, khoaHoc.TENKHOAHOC);
                }
                cbbTenKhoaHoc += "</select>\r\n";

                string cbbLoaiKhoaHoc = "<select class='form-control' id='cbbLoaiKhoaHoc' style='font-size: 14px;' onchange='cbbLoaiKhoaHoc_onchange(this)'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiKhoaHocs)
                    cbbLoaiKhoaHoc += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAIKHOAHOC == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiKhoaHoc += "</select>\r\n";

                string cbbLoaiDaoTao = "<select class='form-control' id='cbbLoaiDaoTao' style='font-size: 14px;'>\r\n";
                foreach (var ldt in DT_KhoaHocParser.LoaiDaoTaos)
                    cbbLoaiDaoTao += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, khoaHoc.LOAIDAOTAO == ldt.Key ? "selected" : null, ldt.Value);
                cbbLoaiDaoTao += "</select>\r\n";

                string cbbLoaiHinhDaoTao = "<select class='form-control' id='cbbLoaiHinhDaoTao' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiHinhDaoTaos)
                    cbbLoaiHinhDaoTao += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAIHINHDAOTAO == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiHinhDaoTao += "</select>\r\n";

                string[] doiTuongs;
                if (string.IsNullOrEmpty(khoaHoc.DOITUONG))
                    doiTuongs = new string[0];
                else doiTuongs = khoaHoc.DOITUONG.Split('|');
                string cbbDoiTuong = "<select class='form-control' id='cbbDoiTuong' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.DoiTuongs)
                    cbbDoiTuong += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, doiTuongs.Contains(lkh.Key.ToString()) ? "selected" : null, lkh.Value);
                cbbDoiTuong += "</select>\r\n";

                string cbbLoaiThoiLuong = "<select class='form-control' id='cbbLoaiThoiLuong' onchange='FillNgayKetThucKhoaHoc()' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiThoiLuongs)
                    cbbLoaiThoiLuong += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAITHOILUONG == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiThoiLuong += "</select>\r\n";

                string cbbDonViHoTro = "<select class='form-control' id='cbbDonViHoTro' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(khoaHoc.DONVIHOTRO_MA))
                {
                    var benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), khoaHoc.DONVIHOTRO_MA);                 
                    if (benhVien != null)
                        cbbDonViHoTro += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", benhVien.Ma, benhVien.Ten);
                    else cbbDonViHoTro += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", khoaHoc.DONVIHOTRO_MA, "N/A");
                }
                cbbDonViHoTro += "</select>\r\n";

                string[] tieuChuans;
                if (string.IsNullOrEmpty(khoaHoc.TIEUCHUAN))
                    tieuChuans = new string[0];
                else tieuChuans = khoaHoc.TIEUCHUAN.Split('|');
                string cbbTieuChuan = "<select class='form-control' id='cbbTieuChuan' multiple style='font-size: 14px;'>\r\n";
                foreach (var tieuChuan in tieuChuans)
                {
                    var oTieuChuan = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ORenderInfo, tieuChuan);
                    cbbTieuChuan += string.Format("<option {0} value={1}>{2}</option>\r\n", "selected", tieuChuan, oTieuChuan != null ? oTieuChuan.Ten : tieuChuan);
                }
                cbbTieuChuan += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "   <form action='javascript:SaveKhoaHoc();'> \r\n" +
                "       <input type='hidden' id='hdClose'>\r\n" +
                "       <input type='hidden' id='hdKhoaHocId' value='" + khoaHocId + "'>\r\n" +
                "       <div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                "           <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" + 
                                            WebLanguage.GetLanguage(OSiteParam, "Tên khóa học:") + " <span style='color:red'>*</span>\r\n" +
                                            cbbTenKhoaHoc +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                             WebLanguage.GetLanguage(OSiteParam, "Khóa học số:") + " <span style='color:red'>*</span>\r\n" +
                "                            <input style='font-size: 14px;' type='number' required class='form-control' id='txtKhoa' onchange='FillMaKhoaHoc()' value='" + khoaHoc.KHOA + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Mã khóa học:") + "\r\n" +
                "                           <input type='text' class='form-control' style='font-size: 14px;' disabled = true id='txtMa' value='" + khoaHoc.MA + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Thời lượng học:") +
                "                           <input style='font-size: 14px; z-index: 0;' type='number'  class='form-control' id='txtThoiLuong' onchange='FillNgayKetThucKhoaHoc()' value='" + khoaHoc.THOILUONG + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Loại thời lượng:") +
                                            cbbLoaiThoiLuong +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                             WebLanguage.GetLanguage(OSiteParam, "Thời gian dự kiến:") +
                "                           <input style='font-size: 14px; z-index: 0;' type='text' data-mask='99/99/9999' class='form-control datepicker' id='dtNgayKhaiGiangDuKien' onchange='javascript: FillNgayKetThucKhoaHoc(); FillHanNopHoSo();' value='" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           <br><input style='font-size: 14px; z-index: 0;' type='text' class='form-control datepicker' id='dtNgayBeGiangDuKien' value='" + (khoaHoc.NGAYBEGIANGDUKIEN == null ? null : khoaHoc.NGAYBEGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Hạn nộp hồ sơ:") +
                "                           <input style='font-size: 14px;z-index: 0;' type='text' class='form-control datepicker' id='dtHanNopHoSo' value='" + (khoaHoc.HANNOPHOSO == null ? null : khoaHoc.HANNOPHOSO.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số học viên dự kiến:") +
                "                           <input style='font-size: 14px;z-index: 0;' type='number' class='form-control' id='txtSoLuongHocVienDuKien' value='" + khoaHoc.SOLUONGHOCVIENDUKIEN + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Học phí:") +
                "                           <input style='font-size: 14px; z-index: 0;' type='text' class='form-control' id='txtHocPhi' onkeypress='CheckCurrency(event);' value='" + (khoaHoc.HOCPHI == null ? null : khoaHoc.HOCPHI.Value.ToString("#,##0.00", new CultureInfo("en-US"))) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Loại đào tạo:") +
                                            cbbLoaiDaoTao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Loại hình đào tạo:") +
                                            cbbLoaiHinhDaoTao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đối tượng:") +
                                            cbbDoiTuong +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Nguồn kinh phí:") +
                                            cbbLoaiKhoaHoc +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row' id='divDonViHoTro' style='" + (khoaHoc.LOAIKHOAHOC == (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc ? null : "display:none;") + "'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đơn vị nhận hỗ trợ:") +
                                            cbbDonViHoTro +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn khóa học:") +
                                            cbbTieuChuan +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Mô tả:") +
                "                           <textarea id='txtMoTa' rows=4 class='form-control' style='font-size: 14px;'>" + khoaHoc.MOTA + "</textarea>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "       </div>\r\n" +
                "       <div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                "           <input type='submit' class='popupSaveClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu & đóng") + "' onclick='javascript:$(\"#hdClose\").val(1);' style='float:right; margin: 7px;'> \r\n" +
                "           <input type='submit' class='popupSave btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' onclick='javascript:$(\"#hdClose\").val(0);' style='float:right; margin: 7px;'> \r\n" +
                "       </div>\r\n" +
                "   </form>\r\n" +
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
            bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.PheDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
            //Quyền dữ liệu
            //1. Người tạo được hiển thị ở mọi trạng thái.
            //2. Người được phân quyền phê duyệt khóa học được hiển thị ở những trạng thái khác trạng thái mới.
            //3. Người được phân quyền xem/sửa/xóa/gửi duyệt trên ca bệnh của người khác được hiển thị ở mọi trạng thái.
            return string.Format(" and (kh.NGUOITAO_ID = '{0}' or {7} = 1 " +
                                     (pheDuyetPermission ?
                                    "  OR kh.TRANGTHAI<>{6} " : null) +
                                    "  OR ( " +
                                    "        select count(1) " +
                                    "        from TablePermissionDataAccess join TablePermissionData on ISNULL(TablePermissionDataAccess.AllowAccess,0)=1 and TablePermissionData.frkOwnerUserId=kh.NGUOITAO_ID and PermissionDataId=frkPermissionDataId " +
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
                                                new DT_KhoaHocPermission().PermissionFunctionCode,
                                                DT_KhoaHocCls.ePermission.Xem.ToString(),
                                                DT_KhoaHocCls.ePermission.Sua.ToString(),
                                                DT_KhoaHocCls.ePermission.Xoa.ToString(),
                                                DT_KhoaHocCls.ePermission.GuiDuyet.ToString(),
                                                (int)DT_KhoaHocCls.eTrangThai.Moi,
                                                user.IsSystemAdmin);
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetNgayKetThucKhoaHoc(RenderInfoCls ORenderInfo, string ngayKhaiGiang, int thoiLuong, string loaiThoiLuong)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DateTime dtNgayKhaiGiang = DateTime.ParseExact(ngayKhaiGiang, "dd/MM/yyyy", null);
                if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.D.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddDays(thoiLuong).ToString("dd/MM/yyyy");
                else if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.W.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddDays(thoiLuong * 7).ToString("dd/MM/yyyy");
                else if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.M.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddMonths(thoiLuong).ToString("dd/MM/yyyy");
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
        public static AjaxOut GetNgayNopHoSo(RenderInfoCls ORenderInfo, string ngayKhaiGiang)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DateTime dtNgayKhaiGiang = DateTime.ParseExact(ngayKhaiGiang, "dd/MM/yyyy", null);
                DateTime dtNgayNopHoSo = dtNgayKhaiGiang.AddDays(-7);
                RetAjaxOut.RetExtraParam1 = dtNgayNopHoSo.ToString("dd/MM/yyyy");
                if (dtNgayKhaiGiang.DayOfWeek == DayOfWeek.Saturday || dtNgayKhaiGiang.DayOfWeek == DayOfWeek.Sunday)
                    RetAjaxOut.RetBoolean = true;
                if (dtNgayNopHoSo.DayOfWeek == DayOfWeek.Saturday || dtNgayNopHoSo.DayOfWeek == DayOfWeek.Sunday)
                    RetAjaxOut.RetObject = true;
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
        public static AjaxOut SaveKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId, string ma, string ten, int khoa, int? thoiLuong, string loaiThoiLuong, string ngayKhaiGiangDuKien, string ngayBeGiangDuKien, string hanNopHoSo, int? soLuongHocVienDuKien, decimal? hocPhi, int loaiDaoTao, int loaiHinhDaoTao, string doiTuong, int loaiKhoaHoc, string donViHoTro, string[] tieuChuan, string moTa)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc;
                if (CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Reading(ORenderInfo, new DT_KhoaHocFilterCls() { MaKhoaHoc = ma }).Any(o => o.ID != khoaHocId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã tồn tại khóa học cùng tên và số. Xin nhập tên hoặc số khóa học khác.");
                    return RetAjaxOut;
                }
                if ((string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null)) <= DateTime.Now)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian dự kiến đang nhỏ hơn hoặc bằng ngày hiện tại.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(khoaHocId))
                {
                    khoaHoc = new DT_KhoaHocCls();
                    khoaHoc.ID = System.Guid.NewGuid().ToString();
                    khoaHoc.MA = ma;
                    khoaHoc.TEN = ten;
                    khoaHoc.KHOA = khoa;
                    khoaHoc.THOILUONG = thoiLuong;
                    khoaHoc.LOAITHOILUONG = loaiThoiLuong;
                    khoaHoc.NGAYKHAIGIANGDUKIEN = string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.NGAYBEGIANGDUKIEN = string.IsNullOrEmpty(ngayBeGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayBeGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.HANNOPHOSO = string.IsNullOrEmpty(hanNopHoSo) ? null : (DateTime?)DateTime.ParseExact(hanNopHoSo, "dd/MM/yyyy", null);
                    khoaHoc.SOLUONGHOCVIENDUKIEN = soLuongHocVienDuKien;
                    khoaHoc.HOCPHI = hocPhi;
                    khoaHoc.LOAIDAOTAO = loaiDaoTao;
                    khoaHoc.LOAIHINHDAOTAO = loaiHinhDaoTao;
                    khoaHoc.DOITUONG = doiTuong != null ? string.Join("|", doiTuong) : null;
                    khoaHoc.LOAIKHOAHOC = loaiKhoaHoc;
                    khoaHoc.DONVIHOTRO_MA = donViHoTro;
                    khoaHoc.TIEUCHUAN = tieuChuan != null ? string.Join("|", tieuChuan) : null;
                    khoaHoc.MOTA = moTa;
                    khoaHoc.NGAYTAO = DateTime.Now;
                    khoaHoc.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    khoaHoc.TRANGTHAI = (int)DT_KhoaHocCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Add(ORenderInfo, khoaHoc);
                    RetAjaxOut.RetExtraParam1 = khoaHoc.ID;
                }
                else
                {
                    khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                    khoaHoc.MA = ma;
                    khoaHoc.TEN = ten;
                    khoaHoc.KHOA = khoa;
                    khoaHoc.THOILUONG = thoiLuong;
                    khoaHoc.LOAITHOILUONG = loaiThoiLuong;
                    khoaHoc.NGAYKHAIGIANGDUKIEN = string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.NGAYBEGIANGDUKIEN = string.IsNullOrEmpty(ngayBeGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayBeGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.HANNOPHOSO = string.IsNullOrEmpty(hanNopHoSo) ? null : (DateTime?)DateTime.ParseExact(hanNopHoSo, "dd/MM/yyyy", null);
                    khoaHoc.SOLUONGHOCVIENDUKIEN = soLuongHocVienDuKien;
                    khoaHoc.HOCPHI = hocPhi;
                    khoaHoc.LOAIDAOTAO = loaiDaoTao;
                    khoaHoc.LOAIHINHDAOTAO = loaiHinhDaoTao;
                    khoaHoc.DOITUONG = doiTuong != null ? string.Join("|", doiTuong) : null;
                    khoaHoc.LOAIKHOAHOC = loaiKhoaHoc;
                    khoaHoc.DONVIHOTRO_MA = donViHoTro;
                    khoaHoc.TIEUCHUAN = tieuChuan != null ? string.Join("|", tieuChuan) : null;
                    khoaHoc.MOTA = moTa;
                    khoaHoc.NGAYSUA = DateTime.Now;
                    khoaHoc.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
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
        public static AjaxOut DeleteKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                //Xóa tài liệu đính kèm khóa học
                var taiLieus = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Reading(ORenderInfo, new DT_TaiLieuFilterCls() { KHOAHOC_ID = khoaHocId });
                foreach (var taiLieu in taiLieus)
                {
                    if (System.IO.File.Exists(taiLieu.DUONGDAN))
                    {
                        try
                        {
                            System.IO.File.Delete(taiLieu.DUONGDAN);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Delete(ORenderInfo, khoaHocId);
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string khoaHocId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                DM_TenKhoaHocCls dmTenKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CreateModel(ORenderInfo, khoaHoc.TEN);
                if (khoaHoc == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học") + " " + khoaHoc.MA + " - " + (dmTenKhoaHoc == null ? khoaHoc.TEN : dmTenKhoaHoc.Ten) + " " + WebLanguage.GetLanguage(OSiteParam, "đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                //Nếu khóa học đã có học viên đăng ký được duyệt thì không thể thu hồi duyệt
                if (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && trangThai == (int)DT_KhoaHocCls.eTrangThai.ChoDuyet
                    && CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Reading(ORenderInfo, new DT_KetQuaDaoTaoFilterCls() { KhoaHocDuyet_Id = khoaHoc.ID }).Count() > 0)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học") + " " + khoaHoc.MA + " - " + (dmTenKhoaHoc == null ? khoaHoc.TEN : dmTenKhoaHoc.Ten) + " " + WebLanguage.GetLanguage(OSiteParam, "đã có đăng ký được duyệt nên không thể thu hồi.");
                    return RetAjaxOut;
                }
                khoaHoc.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
                RetAjaxOut.RetObject = khoaHoc.ROOMID;
                RetAjaxOut.RetObject1 = dmTenKhoaHoc == null ? khoaHoc.TEN : dmTenKhoaHoc.Ten;
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
        public static AjaxOut UpdateRoomId(RenderInfoCls ORenderInfo, string khoaHocId, string roomId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                if (khoaHoc == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                khoaHoc.ROOMID = roomId;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
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
        public static AjaxOut Export(RenderInfoCls ORenderInfo, string keyword, int? nam, int? trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                FlexCelReport flexCelReport = new FlexCelReport();

                List<DT_KhoaHocView> Datas = new List<DT_KhoaHocView>();

                SiteParam
                      OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                DT_KhoaHocFilterCls filter = new DT_KhoaHocFilterCls()
                {
                    Keyword = keyword,
                    Nam = nam,
                    TrangThai = trangThai,
                    DataPermissionQuery = GetDataPermissionQuery(ORenderInfo)
                };
                if (trangThai == null)
                {
                    var user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                    bool xemPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xem.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool themPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Them.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool suaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Sua.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool xoaPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xoa.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool guiDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.GuiDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    bool pheDuyetPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.PheDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                    int[] trangThais = xemPermission || themPermission || suaPermission || xoaPermission || guiDuyetPermission ? DT_KhoaHocParser.TrangThais.Keys.ToArray() : DT_KhoaHocParser.TrangThais.Where(o => o.Key != (int)DT_KhoaHocCls.eTrangThai.Moi).Select(o => o.Key).ToArray();
                    filter.TrangThais = trangThais;
                }
                DT_KhoaHocCls[] DT_KhoaHocs = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Reading(ORenderInfo, filter);

                for (int i = 0; i < DT_KhoaHocs.Length; i++)
                {
                    var view = new DT_KhoaHocView();
                    string[] doiTuongs;
                    if (string.IsNullOrEmpty(DT_KhoaHocs[i].DOITUONG))
                        doiTuongs = new string[0];
                    else doiTuongs = DT_KhoaHocs[i].DOITUONG.Split('|');
                    view.DOITUONG = "";
                    foreach (string doiTuong in doiTuongs)
                        view.DOITUONG += DT_KhoaHocParser.DoiTuongs[Int32.Parse(doiTuong)] + ", ";
                    if (!string.IsNullOrEmpty(view.DOITUONG))
                        view.DOITUONG = view.DOITUONG.Substring(0, view.DOITUONG.Length - 2);
                    view.MA = DT_KhoaHocs[i].MA;
                    view.TEN = DT_KhoaHocs[i].TENKHOAHOC;
                    view.KHOA = WebLanguage.GetLanguage(OSiteParam, "Khóa") + " " + DT_KhoaHocs[i].KHOA;
                    view.THOILUONG = DT_KhoaHocs[i].THOILUONG == null ? null : DT_KhoaHocs[i].THOILUONG + " " + DT_KhoaHocParser.LoaiThoiLuongs[DT_KhoaHocs[i].LOAITHOILUONG];
                    view.NGAYKHAIGIANGDUKIEN = DT_KhoaHocs[i].NGAYKHAIGIANGDUKIEN == null ? null : DT_KhoaHocs[i].NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy");
                    view.HANNOPHOSO = DT_KhoaHocs[i].HANNOPHOSO == null ? null : DT_KhoaHocs[i].HANNOPHOSO.Value.ToString("dd/MM/yyyy");
                    view.SOLUONGHOCVIENDUKIEN = DT_KhoaHocs[i].SOLUONGHOCVIENDUKIEN;
                    view.HOCPHI = DT_KhoaHocs[i].HOCPHI;
                    view.LOAIHINHDAOTAO = DT_KhoaHocParser.LoaiHinhDaoTaos[DT_KhoaHocs[i].LOAIHINHDAOTAO];
                    view.LOAIKHOAHOC = DT_KhoaHocParser.LoaiKhoaHocs[DT_KhoaHocs[i].LOAIKHOAHOC];
                    view.DONVIHOTRO_MA = DT_KhoaHocs[i].DONVIHOTRO_MA;
                    view.MOTA = DT_KhoaHocs[i].MOTA;
                    view.TRANGTHAI = DT_KhoaHocParser.TrangThais[DT_KhoaHocs[i].TRANGTHAI];

                    Datas.Add(view);
                }
                flexCelReport.AddTable("KhoaHoc", Datas);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\DT_KHOAHOC.xlsx";

                string fileName = "DT_KhoaHoc_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string fileSavePath = WebConfig.ConvertPathRoot(OSiteParam, Path.Combine(Directoryfile, fileName));
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().ExportEx(ORenderInfo, LoginName, XmlFile, "KhoaHoc", flexCelReport, fileSavePath);
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
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã khóa học, tên khóa học\" class=\"form-control valueForm\" >\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtNam\" placeholder=\"Năm khai giảng dự kiến\" class=\"form-control valueForm yearpicker\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbLoaiKhoaHocFilter\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
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
                   ;
            }
        }
        #endregion
    }
}
