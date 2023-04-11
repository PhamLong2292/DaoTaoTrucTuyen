﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneMES3.DM.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class PhieuPhanTichNguyenNhanSuCo : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "PhieuPhanTichNguyenNhanSuCo";
        public override string WebPartTitle { get { return "Phiếu phân tích nguyên nhân sự cố"; } }
        public override string Description { get { return "Phiếu phân tích nguyên nhân sự cố"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, PhieuPhanTichNguyenNhanSuCo.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PhieuPhanTichNguyenNhanSuCo), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string NguyenNhanSuCoId = WebEnvironments.Request("id");
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = string.IsNullOrEmpty(NguyenNhanSuCoId) ? new PhieuPhanTichNguyenNhanSuCoCls() : CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);              
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;' >\r\n" +
                    "           <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteThongTin();' style='float:left; margin-left: 20px;" + (!string.IsNullOrEmpty(NguyenNhanSuCoId) ? null : "display:none;") + "'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnHoanTat' title='Hoàn tất' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "' onclick='HoanTat();' style='float:left; margin-left: 20px;'>\r\n" +
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-12'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Chi tiết phiếu phân tích nguyên nhân sự cố") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id ='divThongtinPhieu'> \r\n" +
                                            DrawDangKy(ORenderInfo, NguyenNhanSuCoId, null).HtmlContent +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n" +
                    "</form>\r\n" +
                #region Phần hiển thị popup
                    "<div id=\"divFormModal\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                    "    <div class=\"modal-dialog\" style='width:90%; height:90%;'>\r\n" +
                    "       <div class=\"modal-content\" style='width:100%; height:100%; position: relative;'>\r\n" +
                    "           <div class=\"panel-heading\">\r\n" +
                    "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>\r\n" +
                    "               <h2 class=\"modal-title\" id=\"ModalTitle\"></h2>\r\n" +
                    "           </div> \r\n" +
                    "           <div class=\"modal-body\" id=\"divModalContent\" style='width:100%; padding-top:0; position: absolute; top:50px; bottom:0px;'></div> \r\n" +
                    "        </div> \r\n" +
                    "    </div> \r\n" +
                    "</div> \r\n";
                #endregion
                #endregion Html
                #region javaScript
                string js =
                    "<script language=\"javascript\">\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "   var NguyenNhanSuCoId='" + NguyenNhanSuCoId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "       CheckTrangThai();\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +
                    "     CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "     CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                    "       ShowTrangThai();\r\n" +          
                    "       ShowSelect2();\r\n" +                  
                    "   });\r\n" +

                #region checkbox
                    "   $(\"input:checkbox\").on('click', function() {\r\n" +
                    "   var $box = $(this);\r\n" +
                    "   if ($box.is(\":checked\"))\r\n" +
                    "       {\r\n" +
                    "           var group = \"input:checkbox[name='\" + $box.attr(\"name\") + \"']\";\r\n" +
                    "           $(group).prop(\"checked\", false);\r\n" +
                    "            $box.prop(\"checked\", true);\r\n" +
                    "       }\r\n" +
                    "   else \r\n" +
                    "       {\r\n" +
                    "           $box.prop(\"checked\", false);\r\n" +
                    "       }\r\n" +
                    " });\r\n" +
                #endregion

                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.CheckTrangThai(RenderInfo, NguyenNhanSuCoId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   NguyenNhanSuCoId = document.getElementById('hdNguyenNhanSuCoId').value;\r\n" +
                    "   if(NguyenNhanSuCoId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "   if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "   return NguyenNhanSuCoId;\r\n" +
                    "}\r\n" +                               
                #endregion

                #region Show trạng thái 
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.ShowTrangThai(RenderInfo, NguyenNhanSuCoId).value);\r\n" +
                    "}\r\n" +
                    "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #endregion

                #region Disbale draw
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    NguyenNhanSuCoId = document.getElementById('hdNguyenNhanSuCoId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.CheckTrangThai(RenderInfo, NguyenNhanSuCoId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                    "   {\r\n" +
                    "       $(\"#divNguyenNhan :input\").prop(\"disabled\", true);\r\n" +
                    "       $(\"#divThongTin :input\").prop(\"disabled\", true);\r\n" +
                    "       $('#btnThuHoi').show();\r\n" +
                    "       $('#btnLuu').hide();\r\n" +
                    "       $('#btnHoanTat').hide();\r\n" +
                    "           $('#btnXoa').hide();\r\n" +
                    "   }\r\n" +
                    "   else \r\n" +
                    "   {\r\n" +
                    "       $(\"#divNguyenNhan :input\").prop(\"disabled\", false);\r\n" +
                    "       $(\"#divThongTin :input\").prop(\"disabled\", false);\r\n" +
                    "       $('#btnThuHoi').hide();\r\n" +
                    "       $('#btnLuu').show();\r\n" +
                    "       $('#btnHoanTat').show();\r\n" +
                    "   }\r\n" +
                    "}\r\n" +
                #endregion

                #region ShowSelect2
                    "function ShowSelect2(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +                  
                    "    AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.ShowSelect2(RenderInfo, NguyenNhanSuCoId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(!$('#cbbNguoiLap').val()){\r\n" +
                    "           $('#cbbNguoiLap').val(null).trigger('change');\r\n" +
                    "           $('#cbbNguoiLap').append(AjaxOut.RetExtraParam1);\r\n" +
                    "           $('#cbbNguoiLap').trigger('change');\r\n" +
                    "       }\r\n" +
                    "}\r\n" +

                #endregion

                #region Refresh form về trạng thái mới
                    "function Clear()\r\n" +
                    "{\r\n" +
                    "   window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                    "}\r\n" +
                #endregion

                #region Thu hồi
                    "   function ThuHoi(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi phiếu này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +             
                    "               AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.UpdateTrangThai(RenderInfo, NguyenNhanSuCoId, " + (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.Moi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $(\"#divNguyenNhan :input\").prop(\"disabled\", false);\r\n" +
                    "                   $(\"#divThongTin :input\").prop(\"disabled\", false);\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   $('#btnLuu').show();\r\n" +
                    "                   $('#btnHoanTat').show();\r\n" +
                    "                   AddHistory(NguyenNhanSuCoId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region HoanTat
                    "   function HoanTat(){\r\n" +
                    "       SOBAOCAO = document.getElementById('txtSoBaoCao').value.trim();\r\n" +
                    "        if(SOBAOCAO == '')\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường số báo cáo không được bỏ trống!") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "       NGUOILAP_ID = document.getElementById('cbbNguoiLap').value.trim();\r\n" +
                    "        if(NGUOILAP_ID == '')\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường người lập không được bỏ trống!") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "       THOIGIANLAP = document.getElementById('dtThoiGianLap').value.trim();\r\n"+
                    "        if(THOIGIANLAP == '')\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường thời gian lập không được bỏ trống!") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n"+
                    "       SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.UpdateTrangThai(RenderInfo, NguyenNhanSuCoId, " + (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.HoanTat + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divNguyenNhan :input\").prop(\"disabled\", true);\r\n" +
                    "           $(\"#divThongTin :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnHoanTat').hide();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnXoa').hide();\r\n" +                    
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã hoàn tất!") + "');\r\n" +
                    "           AddHistory(NguyenNhanSuCoId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion

                #region Save Thông tin phiếu
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +                  
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     NguyenNhanSuCoId = document.getElementById('hdNguyenNhanSuCoId').value;\r\n" +
                    "     SOBAOCAO = document.getElementById('txtSoBaoCao').value.trim();\r\n" +
                    "     NGUOILAP_ID = document.getElementById('cbbNguoiLap').value.trim();\r\n" +
                    "     CHUCDANH_ID = document.getElementById('cbbChucDanh').value.trim();\r\n" +
                    "     THOIGIANLAP = document.getElementById('dtThoiGianLap').value.trim();\r\n" +
                    "     MOTASUCO = document.getElementById('txtMoTaChiTiet').value.trim();\r\n" +
                    "     THUCHIENQTKT = $('#cbThucHienQTKT input:checkbox:checked');\r\n" +
                    "       var tongTHUCHIENQTKT = 0;\r\n" +
                    "       for(var i = 0; i < THUCHIENQTKT.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTHUCHIENQTKT = tongTHUCHIENQTKT + parseInt(THUCHIENQTKT[i].value);\r\n" +
                    "        }\r\n" +
                    "     NHIEMKHUANBENHVIEN = $('#cbNhiemKhuanBV input:checkbox:checked');\r\n" +
                    "       var tongNHIEMKHUANBENHVIEN = 0;\r\n" +
                    "       for(var i = 0; i < NHIEMKHUANBENHVIEN.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNHIEMKHUANBENHVIEN = tongNHIEMKHUANBENHVIEN + parseInt(NHIEMKHUANBENHVIEN[i].value);\r\n" +
                    "        }\r\n" +
                    "     THUOCDICHTRUYEN  = $('#cbThuocVaTruyenDich input:checkbox:checked');\r\n" +
                    "       var tongTHUOCDICHTRUYEN = 0;\r\n" +
                    "       for(var i = 0; i < THUOCDICHTRUYEN.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTHUOCDICHTRUYEN = tongTHUOCDICHTRUYEN + parseInt(THUOCDICHTRUYEN[i].value);\r\n" +
                    "        }\r\n" +
                    "     CHEPHAMMAU = $('#cbMauVaChePham input:checkbox:checked');\r\n" +
                    "       var tongCHEPHAMMAU = 0;\r\n" +
                    "       for(var i = 0; i < CHEPHAMMAU.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongCHEPHAMMAU = tongCHEPHAMMAU + parseInt(CHEPHAMMAU[i].value);\r\n" +
                    "        }\r\n" +
                    "     THIETBIYTE = $('#cbThietBiYTe input:checkbox:checked');\r\n" +
                    "       var tongTHIETBIYTE = 0;\r\n" +
                    "       for(var i = 0; i < THIETBIYTE.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTHIETBIYTE = tongTHIETBIYTE + parseInt(THIETBIYTE[i].value);\r\n" +
                    "        }\r\n" +
                    "     HANHVI  = $('#cbHanhVi input:checkbox:checked');\r\n" +
                    "       var tongHANHVI = 0;\r\n" +
                    "       for(var i = 0; i < HANHVI.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongHANHVI = tongHANHVI + parseInt(HANHVI[i].value);\r\n" +
                    "        }\r\n" +
                    "     TAINANNGUOIBENH  = $('#cbTaiNanNguoiBenh input:checkbox:checked');\r\n" +
                    "       var tongTAINANNGUOIBENH = 0;\r\n" +
                    "       for(var i = 0; i < TAINANNGUOIBENH.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTAINANNGUOIBENH = tongTAINANNGUOIBENH + parseInt(TAINANNGUOIBENH[i].value);\r\n" +
                    "        }\r\n" +
                    "     HATANGCOSO  = $('#cbCoSoHaTang input:checkbox:checked');\r\n" +
                    "       var tongHATANGCOSO = 0;\r\n" +
                    "       for(var i = 0; i < HATANGCOSO.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongHATANGCOSO = tongHATANGCOSO + parseInt(HATANGCOSO[i].value);\r\n" +
                    "        }\r\n" +
                    "     QLNGUONLUCTC  = $('#cbQuanLyNguonLuc input:checkbox:checked');\r\n" +
                    "       var tongQLNGUONLUCTC = 0;\r\n" +
                    "       for(var i = 0; i < QLNGUONLUCTC.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongQLNGUONLUCTC = tongQLNGUONLUCTC + parseInt(QLNGUONLUCTC[i].value);\r\n" +
                    "        }\r\n" +
                    "     HSTHUTUCHANHCHINH  = $('#cbHSThuTucHanhChinh input:checkbox:checked');\r\n"+
                    "       var tongHSTHUTUCHANHCHINH = 0;\r\n" +
                    "       for(var i = 0; i < HSTHUTUCHANHCHINH.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongHSTHUTUCHANHCHINH = tongHSTHUTUCHANHCHINH + parseInt(HSTHUTUCHANHCHINH[i].value);\r\n" +
                    "        }\r\n"+
                    "     KHAC = $('#cbKhac input:checkbox:checked');\r\n" +
                    "       var tongKHAC = 0;\r\n" +
                    "       for(var i = 0; i < KHAC.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongKHAC = tongKHAC + parseInt(KHAC[i].value);\r\n" +
                    "        }\r\n" +
                    "     DTYLDUOCTHUCHIEN = document.getElementById('txtDTYLDuocThucHien').value.trim();\r\n" +
                    "     NNNHANVIEN  = $('#cbNhanVien input:checkbox:checked');\r\n" +
                    "       var tongNNNHANVIEN = 0;\r\n" +
                    "       for(var i = 0; i < NNNHANVIEN.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNNHANVIEN = tongNNNHANVIEN + parseInt(NNNHANVIEN[i].value);\r\n" +
                    "        }\r\n" +
                    "     NNNGUOIBENH  = $('#cbNguoiBenh input:checkbox:checked');\r\n" +
                    "       var tongNNNGUOIBENH= 0;\r\n" +
                    "       for(var i = 0; i < NNNGUOIBENH.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNNGUOIBENH = tongNNNGUOIBENH + parseInt(NNNGUOIBENH[i].value);\r\n" +
                    "        }\r\n" +
                    "     NNMOITRUONGLAMVIEC  = $('#cbMoiTruongLamViec input:checkbox:checked');\r\n" +
                    "       var tongNNMOITRUONGLAMVIEC = 0;\r\n" +
                    "       for(var i = 0; i < NNMOITRUONGLAMVIEC.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNMOITRUONGLAMVIEC = tongNNMOITRUONGLAMVIEC + parseInt(NNMOITRUONGLAMVIEC[i].value);\r\n" +
                    "        }\r\n" +
                    "     NNTOCHUCDICHVU  = $('#cbToChucDichVu input:checkbox:checked');\r\n" +
                    "       var tongNNTOCHUCDICHVU = 0;\r\n" +
                    "       for(var i = 0; i < NNTOCHUCDICHVU.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNTOCHUCDICHVU = tongNNTOCHUCDICHVU + parseInt(NNTOCHUCDICHVU[i].value);\r\n" +
                    "        }\r\n" +
                    "     NNYEUTOBENNGOAI  = $('#cbYeuToBenNgoai input:checkbox:checked');\r\n" +
                    "       var tongNNYEUTOBENNGOAI = 0;\r\n" +
                    "       for(var i = 0; i < NNYEUTOBENNGOAI.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNYEUTOBENNGOAI = tongNNYEUTOBENNGOAI + parseInt(NNYEUTOBENNGOAI[i].value);\r\n" +
                    "        }\r\n" +
                    "     NNKHAC  = $('#cbNNKhac input:checkbox:checked');\r\n" +
                    "       var tongNNKHAC = 0;\r\n" +
                    "       for(var i = 0; i < NNKHAC.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongNNKHAC = tongNNKHAC + parseInt(NNKHAC[i].value);\r\n" +
                    "        }\r\n" +
                    "     KHACPHUCSUCO = document.getElementById('txtKhacPhucSuCo').value.trim();\r\n" +
                    "     DEXUATKHUYENCAO = document.getElementById('txtDeXuatKhuyenCao').value.trim();\r\n" +
                    "     MOTAKETQUA = document.getElementById('txtMoTaKetQua').value.trim();\r\n" +
                    "     THAOLUANKHUYENCAO = $('#rdThaoLuanKhuyenCao input:radio:checked').val();\r\n" +
                    "     PHUHOPKHUYENCAO = $('#rdPhuHopKhuyenCao input:radio:checked').val();\r\n" +                
                    "     TONGTHUONGNBNC0 = $('#cbNC0 input:checkbox:checked');\r\n" +
                    "       var tongTONGTHUONGNBNC0 = 0;\r\n" +
                    "       for(var i = 0; i < TONGTHUONGNBNC0.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTONGTHUONGNBNC0 = tongTONGTHUONGNBNC0 + parseInt(TONGTHUONGNBNC0[i].value);\r\n" +
                    "        }\r\n" +
                    "     TONGTHUONGNBNC1 = $('#cbNC1 input:checkbox:checked');\r\n" +
                    "       var tongTONGTHUONGNBNC1 = 0;\r\n" +
                    "       for(var i = 0; i < TONGTHUONGNBNC1.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTONGTHUONGNBNC1 = tongTONGTHUONGNBNC1 + parseInt(TONGTHUONGNBNC1[i].value);\r\n" +
                    "        }\r\n" +
                    "     TONGTHUONGNBNC2 = $('#cbNC2 input:checkbox:checked');\r\n" +
                    "       var tongTONGTHUONGNBNC2 = 0;\r\n" +
                    "       for(var i = 0; i < TONGTHUONGNBNC2.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTONGTHUONGNBNC2 = tongTONGTHUONGNBNC2 + parseInt(TONGTHUONGNBNC2[i].value);\r\n" +
                    "        }\r\n" +
                    "     TONGTHUONGNBNC3 = $('#cbNC3 input:checkbox:checked');\r\n" +
                    "       var tongTONGTHUONGNBNC3 = 0;\r\n" +
                    "       for(var i = 0; i < TONGTHUONGNBNC3.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongTONGTHUONGNBNC3 = tongTONGTHUONGNBNC3 + parseInt(TONGTHUONGNBNC3[i].value);\r\n" +
                    "        }\r\n" +
                    "     DANHGIATRENTOCHUC = $('#cbTrenToChuc input:checkbox:checked');\r\n" +
                    "       var tongDANHGIATRENTOCHUC = 0;\r\n" +
                    "       for(var i = 0; i < DANHGIATRENTOCHUC.length; i++)\r\n" +
                    "        {\r\n" +
                    "           tongDANHGIATRENTOCHUC = tongDANHGIATRENTOCHUC + parseInt(DANHGIATRENTOCHUC[i].value);\r\n" +
                    "        }\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.SaveThongTin(RenderInfo, NguyenNhanSuCoId, SOBAOCAO, NGUOILAP_ID, CHUCDANH_ID, THOIGIANLAP, MOTASUCO, tongTHUCHIENQTKT, tongNHIEMKHUANBENHVIEN, tongTHUOCDICHTRUYEN, tongCHEPHAMMAU, tongTHIETBIYTE, tongHANHVI, tongTAINANNGUOIBENH, tongHATANGCOSO, tongQLNGUONLUCTC, tongHSTHUTUCHANHCHINH, tongKHAC, DTYLDUOCTHUCHIEN, tongNNNHANVIEN, tongNNNGUOIBENH, tongNNMOITRUONGLAMVIEC, tongNNTOCHUCDICHVU, tongNNYEUTOBENNGOAI, tongNNKHAC, KHACPHUCSUCO, DEXUATKHUYENCAO, MOTAKETQUA, THAOLUANKHUYENCAO, PHUHOPKHUYENCAO, tongTONGTHUONGNBNC0, tongTONGTHUONGNBNC1, tongTONGTHUONGNBNC2, tongTONGTHUONGNBNC3, tongDANHGIATRENTOCHUC).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdNguyenNhanSuCoId').value = NguyenNhanSuCoId = AjaxOut.RetExtraParam1;\r\n" +
                    "           $('#btnXoa').show();\r\n" +
                    "           AddHistory(NguyenNhanSuCoId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(NguyenNhanSuCoId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin phiếu

                #region Xóa Thông tin phiếu
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   NguyenNhanSuCoId = document.getElementById('hdNguyenNhanSuCoId').value;\r\n" +
                    "   swal({\r\n" +
                    "           title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "           text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắn chắn xóa phiếu này không?") + "\",\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.DeleteThongTin(RenderInfo, NguyenNhanSuCoId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(NguyenNhanSuCoId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion Xóa Thông tin đăng ký
            
                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       NguyenNhanSuCoId = document.getElementById('hdNguyenNhanSuCoId').value;\r\n" +
                    "       SoBaoCao = sender.value;\r\n" +
                    "       if(NguyenNhanSuCoId == '' && SoBaoCao != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.PhieuPhanTichNguyenNhanSuCo.DrawDangKy(RenderInfo, SoBaoCao, NguyenNhanSuCoId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divThongtinPhieu').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'HH:mm DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "           }\r\n" +
                    "           $(\"input:checkbox\").on('click', function() {\r\n" +
                    "                var $box = $(this);\r\n" +
                    "                if ($box.is(\":checked\")) {\r\n" +
                    "                    var group = \"input:checkbox[name='\" + $box.attr(\"name\") + \"']\";\r\n" +
                    "                    $(group).prop(\"checked\", false);\r\n" +
                    "                    $box.prop(\"checked\", true);\r\n" +
                    "                }else {\r\n" +
                    "                    $box.prop(\"checked\", false);\r\n" +
                    "                }\r\n" +
                    "            });\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "           CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Bắt sự kiện thay đổi textbox mã học viên
             
                    "</script>\r\n";
                #endregion javaScript
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html) + WebEnvironments.ProcessJavascript(js);

            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawDangKy(RenderInfoCls ORenderInfo, string SoBaoCao, string NguyenNhanSuCoId )
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);               
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = null;
                if (!string.IsNullOrEmpty(NguyenNhanSuCoId))
                    NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                if (!string.IsNullOrEmpty(SoBaoCao))
                    NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, SoBaoCao);
                if (NguyenNhanSuCo == null)
                {
                    NguyenNhanSuCo = new PhieuPhanTichNguyenNhanSuCoCls();
                    if (string.IsNullOrEmpty(SoBaoCao))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        NguyenNhanSuCo.SOBAOCAO = boDemValueMaHV.RetExtraParam2;
                    }
                    else NguyenNhanSuCo.SOBAOCAO = SoBaoCao;
                }
                string cbbChucDanh = "";
                if (!string.IsNullOrEmpty(NguyenNhanSuCo.CHUCDANH_ID))
                {
                    var chucdanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), NguyenNhanSuCo.CHUCDANH_ID);
                    if (chucdanh != null)
                    {
                        cbbChucDanh += string.Format("<option value={0}>{0} - {1}</option>\r\n", chucdanh.Ma, chucdanh.Ten);
                    }
                }
                string nguoilap = "";
                if (!string.IsNullOrEmpty(NguyenNhanSuCo.NGUOILAP_ID))
                {
                    OwnerUserCls Users = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, NguyenNhanSuCo.NGUOILAP_ID);
                    if (Users != null)
                    {
                        nguoilap += string.Format("<option value='"+ Users.OwnerUserId + "'>{0} - {1}</option>\r\n", Users.OwnerCode, Users.FullName);
                    }
                }
                string Html =
                "               <input id='hdNguyenNhanSuCoId' type='hidden' value='" + NguyenNhanSuCo.ID + "'>\r\n" +
                "               <div class=\"row\" style='margin-left: 10px;' id='divThongTin'>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số báo cáo/ Mã số sự cố:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtSoBaoCao' type='text' value='" + NguyenNhanSuCo.SOBAOCAO + "' onchange='txtMa_onchange(this)' value='' class='form-control valueForm' required>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Người lập:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <select id = 'cbbNguoiLap' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "' required>\r\n" +
                                                nguoilap +
                "                           </select>\r\n" +
                "                       </div> \r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Chức danh:") +
                "                           <select id = 'cbbChucDanh' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "'>\r\n" +
                                                cbbChucDanh+
                "                           </select>\r\n" +
                "                       </div> \r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Thời gian lập:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='dtThoiGianLap' required type='text' value='" + (NguyenNhanSuCo.THOIGIANLAP == null ? null : NguyenNhanSuCo.THOIGIANLAP.Value.ToString(" HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +           
                "               <div class=\"row\" id='divNguyenNhan'>\r\n" +
                                    ChiTietPhieuBaoCao(ORenderInfo, NguyenNhanSuCo.ID).HtmlContent +
                "               </div>\r\n";
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
        public static AjaxOut ChiTietPhieuBaoCao(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = string.IsNullOrEmpty(NguyenNhanSuCoId) ? new PhieuPhanTichNguyenNhanSuCoCls() : CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                string Html =
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "A. DÀNH CHO NHÂN VIÊN CHUYÊN TRÁCH") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "1.Mô tả chi tiết sự cố: ") + "</label></br>\r\n" +
                "                                       <textarea  id='txtMoTaChiTiet' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + NguyenNhanSuCo.MOTASUCO + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "II.Phân loại sự cố theo nhóm sự cố(Incident type") + "</label>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id = 'cbThucHienQTKT'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "1.Thực hiện quy trình kỹ thuật, thủ") + "</label>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl0\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.KhongCoSuDongYCuaNB] + "\" " + (NguyenNhanSuCo.KhongCoSuDongYCuaNB == true ? "checked" : string.Empty) + " >Không có sự đồng ý của người bệnh/người nhà(đối với những kỹ thuật, thủ thuật quy định phải ký cam kết) </label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl1\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.KhongTHKhiCoCD] + "\" " + (NguyenNhanSuCo.KhongTHKhiCoCD == true ? "checked" : string.Empty) + ">Không thực hiện khi có chỉ định</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl2\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.THSaiNB] + "\" " + (NguyenNhanSuCo.THSaiNB == true ? "checked" : string.Empty) + ">Thực hiện sai người bệnh</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl3\" type = \"checkbox\" name=\"ThucHienQTKT\"  value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.THSaiTT] + "\" " + (NguyenNhanSuCo.THSaiTT == true ? "checked" : string.Empty) + ">Thực hiện sai thủ thuật/quy trình/phương pháp điều trị</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl4\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.THSaiVTPhauThuat] + "\"  " + (NguyenNhanSuCo.THSaiVTPhauThuat == true ? "checked" : string.Empty) + ">Thực hiện sai vị trí phẫu thuật/thủ thuật</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl5\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.BoSotDungCu] + "\" " + (NguyenNhanSuCo.BoSotDungCu == true ? "checked" : string.Empty) + ">Bỏ sót dụng cụ, vật tư tiêu hao trong quá trình phẫu thuật</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl6\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TVTrongThaiKy] + "\" " + (NguyenNhanSuCo.TVTrongThaiKy == true ? "checked" : string.Empty) + ">Tử vong trong thai kỳ</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl7\" type = \"checkbox\" name=\"ThucHienQTKT\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TVKhiSinh] + "\" " + (NguyenNhanSuCo.TVKhiSinh == true ? "checked" : string.Empty) + ">Tử vong khi sinh</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl8\" type = \"checkbox\" name=\"ThucHienQTKT\"  value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TVSoSinh] + "\" " + (NguyenNhanSuCo.TVSoSinh == true ? "checked" : string.Empty) + ">Tử vong sơ sinh</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id = 'cbNhiemKhuanBV'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "2.Nhiễm khuẩn bệnh viện:") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl9\" type = \"checkbox\" name=\"NhiemKhuanBenhVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NKHuyet] + "\" " + (NguyenNhanSuCo.NKHuyet == true ? "checked" : string.Empty) + ">Nhiễm khuẩn huyết </label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl10\" type = \"checkbox\" name=\"NhiemKhuanBenhVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ViemPhoi] + "\" " + (NguyenNhanSuCo.ViemPhoi == true ? "checked" : string.Empty) + ">Viêm phổi</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl11\" type = \"checkbox\" name=\"NhiemKhuanBenhVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacLoaiNKKhac] + "\" " + (NguyenNhanSuCo.CacLoaiNKKhac == true ? "checked" : string.Empty) + ">Các loại nhiễm khuẩn khác</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl12\" type = \"checkbox\" name=\"NhiemKhuanBenhVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NKVetMo] + "\" " + (NguyenNhanSuCo.NKVetMo == true ? "checked" : string.Empty) + ">Nhiễm khuẩn vết mổ</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl13\" type = \"checkbox\" name=\"NhiemKhuanBenhVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NKTietNieu] + "\" " + (NguyenNhanSuCo.NKTietNieu == true ? "checked" : string.Empty) + ">Nhiễm khuẩn tiết niệu</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +    
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id = 'cbThuocVaTruyenDich'>\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "3.Thuốc và dịch truyền:") + "</label></br>\r\n" +
                "                                   <div class='row'>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl14\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CPSaiThuocTD] + "\" " + (NguyenNhanSuCo.CPSaiThuocTD == true ? "checked" : string.Empty) + ">Cấp phát sai thuốc dịch truyền</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl15\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThieuThuoc] + "\" " + (NguyenNhanSuCo.ThieuThuoc == true ? "checked" : string.Empty) + ">Thiếu thuốc</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl16\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiLieuHamLuong] + "\" " + (NguyenNhanSuCo.SaiLieuHamLuong == true ? "checked" : string.Empty) + ">Sai liều, sai hàm lượng</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl17\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiThoiGian] + "\" " + (NguyenNhanSuCo.SaiThoiGian == true ? "checked" : string.Empty) + ">Sai thời gian</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl18\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiYLenh] + "\" " + (NguyenNhanSuCo.SaiYLenh == true ? "checked" : string.Empty) + ">Sai y lệnh</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\" >\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl19\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.BoSotThuoc] + "\" " + (NguyenNhanSuCo.BoSotThuoc == true ? "checked" : string.Empty) + ">Bỏ sót thuốc/liều thuốc</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl20\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiThuoc] + "\" " + (NguyenNhanSuCo.SaiThuoc == true ? "checked" : string.Empty) + ">Sai thuốc</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl21\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiNguoiBenh] + "\" " + (NguyenNhanSuCo.SaiNguoiBenh == true ? "checked" : string.Empty) + ">Sai người bệnh</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl22\" type = \"checkbox\" name=\"ThuocVaTruyenDich\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SaiDuongDung] + "\" " + (NguyenNhanSuCo.SaiDuongDung == true ? "checked" : string.Empty) + ">Sai đường dùng</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id = 'cbMauVaChePham'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "4.Máu và các chế phẩm máu: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl23\" type = \"checkbox\" name=\"ChePhamMau\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.PhanUngPhu] + "\" " + (NguyenNhanSuCo.PhanUngPhu == true ? "checked" : string.Empty) + ">Phản ứng phụ, tai biến khi truyền máu</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl24\" type = \"checkbox\" name=\"ChePhamMau\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TruyenMauNham] + "\" " + (NguyenNhanSuCo.TruyenMauNham == true ? "checked" : string.Empty) + ">Truyền máu nhầm, chế phẩm máu</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl25\" type = \"checkbox\" name=\"ChePhamMau\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TruyenSaiLieu] + "\" " + (NguyenNhanSuCo.TruyenSaiLieu == true ? "checked" : string.Empty) + ">Truyền sai liều, sai thời điểm</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbThietBiYTe'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "5.Thiết bị y tế: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl26\" type = \"checkbox\" name=\"ThietBiYTe\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThieuThongTinHD] + "\" " + (NguyenNhanSuCo.ThieuThongTinHD == true ? "checked" : string.Empty) + ">Thiếu thông tin hướng dẫn sử dụng</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl27\" type = \"checkbox\" name=\"ThietBiYTe\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.LoiThietBi] + "\" " + (NguyenNhanSuCo.LoiThietBi == true ? "checked" : string.Empty) + ">Lỗi thiết bị</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl28\" type = \"checkbox\" name=\"ThietBiYTe\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThieuHoacKhongPH] + "\" " + (NguyenNhanSuCo.ThieuHoacKhongPH == true ? "checked" : string.Empty) + ">Thiếu thiết bị hoặc không phù hợp</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbHanhVi'>\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "6.Hành vi: ") + "</label></br>\r\n" +
                "                                   <div class='row'>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +                         
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl29\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TuGayHai] + "\" " + (NguyenNhanSuCo.TuGayHai == true ? "checked" : string.Empty) + ">Khuynh hướng tự gây gại, tự tử</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl30\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.QRTDBoiNV] + "\" " + (NguyenNhanSuCo.QRTDBoiNV == true ? "checked" : string.Empty) + ">Quấy rối tình dục bởi nhân viên</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl31\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.QRTDBoiNB] + "\" " + (NguyenNhanSuCo.QRTDBoiNB == true ? "checked" : string.Empty) + ">Quấy rối tình dục bởi người bệnh/khách đến thăm</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div> \r\n" +
                 "                                      <div class=\"col-md-6\">\r\n" +
                "                                          <div class=\"form-group\">\r\n" +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl32\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.XamHaiCoTheBoiNB] + "\" " + (NguyenNhanSuCo.XamHaiCoTheBoiNB == true ? "checked" : string.Empty) + ">Xâm hại cơ thể bởi người bệnh/khác đến thăm</label></br> " +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl33\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CoHDTuTu] + "\" " + (NguyenNhanSuCo.CoHDTuTu == true ? "checked" : string.Empty) + ">Có hành động tử tự</label></br> " +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl34\" type = \"checkbox\" name=\"HanhVi\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TronVien] + "\" " + (NguyenNhanSuCo.TronVien == true ? "checked" : string.Empty) + ">Trốn viện</label></br> " +
                "                                          </div> \r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbTaiNanNguoiBenh'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "7.Tai nạn đối với người bệnh: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl35\" type = \"checkbox\" name=\"TaiNan\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TaiNan] + "\" " + (NguyenNhanSuCo.TaiNan == true ? "checked" : string.Empty) + ">Té ngã</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbCoSoHaTang'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "8.Hạ tầng cơ sở: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl36\" type = \"checkbox\" name=\"HaTangCoSo\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.BiHuHong] + "\" " + (NguyenNhanSuCo.BiHuHong == true ? "checked" : string.Empty) + ">Bị hư hỏng, bị lỗi</label> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl37\" type = \"checkbox\" name=\"HaTangCoSo\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThieuHoacKhongPH] + "\" " + (NguyenNhanSuCo.ThieuHoacKhongPH == true ? "checked" : string.Empty) + ">Thiếu hoặc không phù hợp</label>" +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbQuanLyNguonLuc'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "9.Quản lý nguồn lực, tổ chức: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl38\" type = \"checkbox\" name=\"QLNLToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TPHDDCuaDVKB] + "\" " + (NguyenNhanSuCo.TPHDDCuaDVKB == true ? "checked" : string.Empty) + ">Tính phù hợp, đầy đủ của dịch vụ khám bệnh, chữa bệnh</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl39\" type = \"checkbox\" name=\"QLNLToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TPHDDCuaNL] + "\" " + (NguyenNhanSuCo.TPHDDCuaNL == true ? "checked" : string.Empty) + ">Tính phù hợp, đầy đủ của nguồn lực</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl40\" type = \"checkbox\" name=\"QLNLToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TPHDDCuaCS] + "\" " + (NguyenNhanSuCo.TPHDDCuaCS == true ? "checked" : string.Empty) + ">Tính phù hợp, đầy đủ của chính sách, quy định, quy trình, hướng dẫn chuyên môn</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbHSThuTucHanhChinh'>\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "10.Hồ sơ, tài liệu, thủ tục hành chính: ") + "</label></br>\r\n" +
                "                                   <div class='row'>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl41\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TaiLieuThieu] + "\" " + (NguyenNhanSuCo.TaiLieuThieu == true ? "checked" : string.Empty) + ">Tài liệu thiếu</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl42\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TaiLieuKhongRoRang] + "\" " + (NguyenNhanSuCo.TaiLieuKhongRoRang == true ? "checked" : string.Empty) + ">Tài liệu không rõ ràng, không hoàn chỉnh</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl43\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TGChoDoiKeoDai] + "\" " + (NguyenNhanSuCo.TGChoDoiKeoDai == true ? "checked" : string.Empty) + ">Thời gian chờ đợi khéo dài</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl44\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CCHSTLCham] + "\" " + (NguyenNhanSuCo.CCHSTLCham == true ? "checked" : string.Empty) + ">Cung cấp hồ sơ tài liệu chậm</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl45\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NhamHSTL] + "\" " + (NguyenNhanSuCo.NhamHSTL == true ? "checked" : string.Empty) + ">Nhầm hồ sơ tài liệu</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl46\" type = \"checkbox\" name=\"HSTaiLieu\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TTHCPhucTap] + "\" " + (NguyenNhanSuCo.TTHCPhucTap == true ? "checked" : string.Empty) + ">Thủ tục hành chính phức tạp</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbKhac'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "11.Khác: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl47\" type = \"checkbox\" name=\"CacSuCoKhongDuocDeCap\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacSCKhongDuocDeCap] + "\" " + (NguyenNhanSuCo.CacSCKhongDuocDeCap == true ? "checked" : string.Empty) + ">Các sự cố không được đề cập từ mục 1 - 10</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +              
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "III.Điều trị/y lệnh đã được thực hiện") + "</label>\r\n" +
                "                                       <textarea  id='txtDTYLDuocThucHien' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + NguyenNhanSuCo.DTYLDUOCTHUCHIEN + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "IV.Phân loại sự cố theo nhóm nguyên nhân gây ra sự cố(*)") + "</label>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbNhanVien'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "1.Nhân viên: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl48\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NhanThucNV] + "\" " + (NguyenNhanSuCo.NhanThucNV == true ? "checked" : string.Empty) + ">Nhận thức(kiến thức, hiểu biết, quan niệm)</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl49\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThucHanhNV] + "\" " + (NguyenNhanSuCo.ThucHanhNV == true ? "checked" : string.Empty) + ">Thực hành(kỹ năng thực hành không đúng quy định, hướng dẫn chuẩn hoặc thực hành theo quy định, hướng dẫn sai)</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl50\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThaiDoNV] + "\" " + (NguyenNhanSuCo.ThaiDoNV == true ? "checked" : string.Empty) + ">Thái độ, hành vi, cảm xúc</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl51\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.GiaoTiepNV] + "\" " + (NguyenNhanSuCo.GiaoTiepNV == true ? "checked" : string.Empty) + ">Giao tiếp</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl52\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TamSinhLyNV] + "\" " + (NguyenNhanSuCo.TamSinhLyNV == true ? "checked" : string.Empty) + ">Tâm sinh lý, thể chất, bệnh lý</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl53\" type = \"checkbox\" name=\"NhanVien\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacYTXHNV] + "\" " + (NguyenNhanSuCo.CacYTXHNV == true ? "checked" : string.Empty) + ">Các yếu tố xã hội</label></br> " +                       
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbNguoiBenh'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "2.Người bệnh: ") + "</label></br>\r\n" +           
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl54\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NhanThucNB] + "\" " + (NguyenNhanSuCo.NhanThucNB == true ? "checked" : string.Empty) + ">Nhận thức(kiến thức, hiểu biết, quan niệm) </label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl55\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThucHanhNB] + "\" " + (NguyenNhanSuCo.ThucHanhNB == true ? "checked" : string.Empty) + ">Thực hành(kỹ năng thực hành không đúng quy định, hướng dẫn chuẩn hoặc thực hành theo quy định, hướng dẫn sai)</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl56\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.ThaiDoNB] + "\" " + (NguyenNhanSuCo.ThaiDoNB == true ? "checked" : string.Empty) + ">Thái độ, hành vi, cảm xúc</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl57\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.GiaoTiepNB] + "\" " + (NguyenNhanSuCo.GiaoTiepNB == true ? "checked" : string.Empty) + ">Giao tiếp</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl58\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TamSinhLyNB] + "\" " + (NguyenNhanSuCo.TamSinhLyNB == true ? "checked" : string.Empty) + ">Tâm sinh lý, thể chất, bệnh lý</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl59\" type = \"checkbox\" name=\"NguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacYTXHNB] + "\" " + (NguyenNhanSuCo.CacYTXHNB == true ? "checked" : string.Empty) + ">Các yếu tố xã hội</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbMoiTruongLamViec'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "3.Môi trường làm việc: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl60\" type = \"checkbox\" name=\"MTLamViec\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CoSovatChat] + "\" " + (NguyenNhanSuCo.CoSovatChat == true ? "checked" : string.Empty) + ">Cơ sở vật chất, hạ tầng, trang thiết bị</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl61\" type = \"checkbox\" name=\"MTLamViec\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.KhoangCach] + "\" " + (NguyenNhanSuCo.KhoangCach == true ? "checked" : string.Empty) + ">Khoảng cách đến nơi làm việc quá xa</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl62\" type = \"checkbox\" name=\"MTLamViec\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.DGVeDoAnToan] + "\" " + (NguyenNhanSuCo.DGVeDoAnToan == true ? "checked" : string.Empty) + ">Đánh giá về độ an toàn, các nguy cơ rủi ro của môi trường làm việc</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl63\" type = \"checkbox\" name=\"MTLamViec\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.NoiQuy] + "\" " + (NguyenNhanSuCo.NoiQuy == true ? "checked" : string.Empty) + ">Nội quy, quy định và đặc tính kĩ thuật</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbToChucDichVu' >\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "4.Tổ chức/dịch vụ: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl64\" type = \"checkbox\" name=\"ToChucDV\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacChinhSach] + "\" " + (NguyenNhanSuCo.CacChinhSach == true ? "checked" : string.Empty) + ">Các chính sách, quy trình, hướng dẫn chuyên môn</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl65\" type = \"checkbox\" name=\"ToChucDV\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TuanThuQT] + "\" " + (NguyenNhanSuCo.TuanThuQT == true ? "checked" : string.Empty) + ">Tuân thủ quy trình thực hành chuẩn</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl66\" type = \"checkbox\" name=\"ToChucDV\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.VanHoaToChuc] + "\" " + (NguyenNhanSuCo.VanHoaToChuc == true ? "checked" : string.Empty) + ">Văn hóa tổ chức</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl67\" type = \"checkbox\" name=\"ToChucDV\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.LamViecNhom] + "\" " + (NguyenNhanSuCo.LamViecNhom == true ? "checked" : string.Empty) + ">Làm việc nhóm</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbYeuToBenNgoai'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "5.Yếu tố bên ngoài: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl68\" type = \"checkbox\" name=\"YTBenNgoai\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.MoiTruongTuNhien] + "\" " + (NguyenNhanSuCo.MoiTruongTuNhien == true ? "checked" : string.Empty) + ">Môi trường tự nhiên</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl69\" type = \"checkbox\" name=\"YTBenNgoai\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.SPCNCSHT] + "\" " + (NguyenNhanSuCo.SPCNCSHT == true ? "checked" : string.Empty) + ">Sản phẩm, công nghệ và cơ sở hạ tầng</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl70\" type = \"checkbox\" name=\"YTBenNgoai\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.QTHeThongDV] + "\" " + (NguyenNhanSuCo.QTHeThongDV == true ? "checked" : string.Empty) + ">Quy trình, hệ thống dịch vụ</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id = 'cbNNKhac'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "6.khác: ") + "</label></br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioH71\" type = \"checkbox\" name=\"CacYeuToKhongDeCap\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CacYTKhongDeCap] + "\" " + (NguyenNhanSuCo.CacYTKhongDeCap == true ? "checked" : string.Empty) + ">Các yếu tố không đề cập trong các mục từ 1 - 5</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "V.Hành động khắc phục sự cố") + "</label>\r\n" +
                "                                       <textarea  id='txtKhacPhucSuCo' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + NguyenNhanSuCo.KHACPHUCSUCO + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "VI.Đề xuất khuyến cáo phòng ngừa sự cố") + "</label>\r\n" +
                "                                       <textarea  id='txtDeXuatKhuyenCao' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + NguyenNhanSuCo.DEXUATKHUYENCAO + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                //Thông tin sự cố
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "B. DÀNH CHO CẤP QUẢN LÝ") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "I.Đánh giá trưởng nhóm chuyên gia") + "</label></br>\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Mô tả kết quả phát hiện được(không lặp lại các mô tả sự cố)") + "</br>\r\n" +
                "                                       <textarea  id='txtMoTaKetQua' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + NguyenNhanSuCo.MOTAKETQUA + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='rdThaoLuanKhuyenCao'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Đã thảo luận đưa ra khuyến cáo/hướng xử lý với người báo cáo:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqCo1\" type = \"radio\" name=\"optradioHl1\"value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Co + "' " + (NguyenNhanSuCo.THAOLUANKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Co ? "checked" : null) + ">Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhong1\" type = \"radio\" name=\"optradioHl1\" value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Khong + "' " + (NguyenNhanSuCo.THAOLUANKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Khong ? "checked" : null) + ">Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhongGhiNhan1\" type = \"radio\" name=\"optradioHl1\" value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.KhongGhiNhan + "' " + (NguyenNhanSuCo.THAOLUANKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.KhongGhiNhan ? "checked" : null) + ">Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-6\" id = 'rdPhuHopKhuyenCao'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Phù hợp với các khuyến cáo chính thức được ban hành (Ghi cụ thể khuyến cáo):") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqCo\" type = \"radio\" name=\"optradioHl2\" value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Co + "' " + (NguyenNhanSuCo.PHUHOPKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Co ? "checked" : null) + ">Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhong\" type = \"radio\" name=\"optradioHl2\" value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Khong + "' " + (NguyenNhanSuCo.PHUHOPKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Khong ? "checked" : null) + ">Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhongGhiNhan\" type = \"radio\" name=\"optradioHl2\" value='" + (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.KhongGhiNhan + "' " + (NguyenNhanSuCo.PHUHOPKHUYENCAO == (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.KhongGhiNhan ? "checked" : null) + ">Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "II.Đánh mức độ tổn thương") + "</label></br>\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "1.Trên người bệnh: ") + "</label></br>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbNC0'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Chưa xảy ra (NC0) ") + "</br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl72\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.A] + "\"  " + (NguyenNhanSuCo.A == true ? "checked" : string.Empty) + ">A Tình huống có nguy cơ gây ra sự cố (near miss)</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbNC1'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Tổn thương nhẹ(NC1) ") + "</br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl73\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.B] + "\" " + (NguyenNhanSuCo.B == true ? "checked" : string.Empty) + ">B Sự cố đã xảy ra chưa tác động đến người bệnh</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl74\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.C] + "\" " + (NguyenNhanSuCo.C == true ? "checked" : string.Empty) + ">C Sự cố đã xảy ra tác động trực tiếp đến người bệnh, chưa gây nguy hại</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl75\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.D] + "\" " + (NguyenNhanSuCo.D == true ? "checked" : string.Empty) + ">D Sự cố đã xảy ra tác động trực tiếp đến người bệnh, phải cần theo dõi hoặc đã can thiệp điều trị kịp thời nên không gây nguy hại</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbNC2'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Tổn thương trung bình(NC2) ") + "</br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl76\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.E] + "\" " + (NguyenNhanSuCo.E == true ? "checked" : string.Empty) + ">E Sự cố đã xảy ra nguy hại tạm thời và cần phải can thiệp điều trị</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl77\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.F] + "\" " + (NguyenNhanSuCo.F == true ? "checked" : string.Empty) + ">F Sự cố đã xảy ra nguy hại tạm thời, cần phải can thiệp điều trị và kéo dài thời gian nằm viện</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\" id ='cbNC3'>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Tổn thương nặng (NC3)") + "</br>\r\n" +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl78\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.G] + "\" " + (NguyenNhanSuCo.G == true ? "checked" : string.Empty) + ">G Sự cố đã xảy nguy hại kéo dài, để lại di chứng</label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl79\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.H] + "\" " + (NguyenNhanSuCo.H == true ? "checked" : string.Empty) + ">H Sự cố đã xảy nguy hại cần phải hồi sức tích cực </label></br> " +
                "                                      <label class=\"checkbox-inline\"><input  id = \"optradioHl80\" type = \"checkbox\" name=\"TrenNguoiBenh\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.I] + "\" " + (NguyenNhanSuCo.I == true ? "checked" : string.Empty) + ">I Sự cố đã xảy ra có ảnh hưởng hoặc trực tiếp gây tử vong</label></br> " +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row' style='margin-bottom: 50px;'>\r\n" +
                "                               <div class=\"col-md-6\" id ='cbTrenToChuc'>\r\n" +
                "                                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "2.Trên tổ chức: ") + "</label></br>\r\n" +
                "                                   <div class='row'>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +                         
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl81\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TonHaiTaiSan] + "\" " + (NguyenNhanSuCo.TonHaiTaiSan == true ? "checked" : string.Empty) + ">Tổn hại tài sản</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl82\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TangNLPVChoBV] + "\" " + (NguyenNhanSuCo.TangNLPVChoBV == true ? "checked" : string.Empty) + ">Tăng nguồn lực phục vụ cho bệnh viện</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl83\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.QTCuaTT] + "\" " + (NguyenNhanSuCo.QTCuaTT == true ? "checked" : string.Empty) + ">Quan tâm của truyền thông</label></br> " +
                "                                              <label class=\"checkbox-inline\"><input  id = \"optradioHl84\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.KhieuNaiCuaNB] + "\" " + (NguyenNhanSuCo.KhieuNaiCuaNB == true ? "checked" : string.Empty) + ">Khiếu nại của người bệnh</label></br> " +
                "                                           </div> \r\n" +
                "                                       </div> \r\n" +
                "                                      <div class=\"col-md-6\">\r\n" +
                "                                          <div class=\"form-group\">\r\n" +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl85\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.TonHaiDanhTieng] + "\" " + (NguyenNhanSuCo.TonHaiDanhTieng == true ? "checked" : string.Empty) + ">Tổn hại danh tiếng</label></br> " +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl86\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.CanThiepCuaPL] + "\" " + (NguyenNhanSuCo.CanThiepCuaPL == true ? "checked" : string.Empty) + ">Can thiệp của pháp luật</label></br> " +
                "                                             <label class=\"checkbox-inline\"><input  id = \"optradioHl87\" type = \"checkbox\" name=\"TrenToChuc\" value=\"" + PhieuPhanTichNguyenNhanSuCoCls.BitArray[(int)PhieuPhanTichNguyenNhanSuCoCls.eLoai.Khac] + "\" " + (NguyenNhanSuCo.Khac == true ? "checked" : string.Empty) + ">Khác</label></br> " +
                "                                          </div> \r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n";
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                if (NguyenNhanSuCo != null)
                    return PhieuPhanTichNguyenNhanSuCoParser.sColorTrangThai[NguyenNhanSuCo.TRANGTHAI.Value];
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return null;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                if (NguyenNhanSuCo != null)
                        RetAjaxOut.RetExtraParam1 = NguyenNhanSuCo.TRANGTHAI.ToString() ;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #endregion Vẽ giao diện

        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetBoDemValueAndMaHV(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string boDemValue = null;
            string maHV = null;
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "PhieuPhanTichNguyenNhanSuCo", FieldName = "SoBaoCao" }).FirstOrDefault();
            if (boMa != null && !string.IsNullOrEmpty(boMa.BIEUTHUC))
            {
                while (maHV == null || CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, maHV) != null)
                {
                    if (maHV != null)
                    {
                        var boDem = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().CreateModel(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC));
                        //if (boDem != null)
                        //{
                        //    CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().UpdateStatus(ORenderInfo, boDem.ID, int.Parse(boDemValue), 1);
                        //}
                    }
                    boDemValue = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().GetValue(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC), Model.Common.GetValueFormat(boMa.BIEUTHUC));
                    maHV = Model.Common.GetDisplayPart(boMa.BIEUTHUC) + boDemValue;
                }
            }
            RetAjaxOut.RetExtraParam1 = boDemValue;
            RetAjaxOut.RetExtraParam2 = maHV;
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId, string SOBAOCAO, string NGUOILAP_ID, string CHUCDANH_ID, string THOIGIANLAP, string MOTASUCO, int THUCHIENQTKT, int NHIEMKHUANBENHVIEN, int THUOCDICHTRUYEN, int CHEPHAMMAU, int THIETBIYTE, int HANHVI, int TAINANNGUOIBENH, int HATANGCOSO, int QLNGUONLUCTC, int HSTHUTUCHANHCHINH, int KHAC, string DTYLDUOCTHUCHIEN, int NNNHANVIEN, int NNNGUOIBENH, int NNMOITRUONGLAMVIEC, int NNTOCHUCDICHVU, int NNYEUTOBENNGOAI, int NNKHAC, string KHACPHUCSUCO, string DEXUATKHUYENCAO, string MOTAKETQUA, int THAOLUANKHUYENCAO, int PHUHOPKHUYENCAO, int TONGTHUONGNBNC0, int TONGTHUONGNBNC1, int TONGTHUONGNBNC2, int TONGTHUONGNBNC3, int DANHGIATRENTOCHUC)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = new PhieuPhanTichNguyenNhanSuCoCls();
                if (!string.IsNullOrEmpty(NguyenNhanSuCoId))
                {
                    NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                }
                else
                {
                    NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, SOBAOCAO);
                    if (NguyenNhanSuCo == null)
                    {
                        NguyenNhanSuCo = new PhieuPhanTichNguyenNhanSuCoCls() { SOBAOCAO = SOBAOCAO };
                    }
                }
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(NguyenNhanSuCo.ID))
                {
                    NguyenNhanSuCo.ID = NguyenNhanSuCoId = System.Guid.NewGuid().ToString();
                    NguyenNhanSuCo.NGUOILAP_ID = NGUOILAP_ID;
                    NguyenNhanSuCo.CHUCDANH_ID = CHUCDANH_ID;
                    NguyenNhanSuCo.THOIGIANLAP = string.IsNullOrWhiteSpace(THOIGIANLAP) ? null : (DateTime?)DateTime.ParseExact(THOIGIANLAP, "HH:mm dd/MM/yyyy", null);           
                    NguyenNhanSuCo.MOTASUCO = MOTASUCO;
                    NguyenNhanSuCo.THUCHIENQTKT = THUCHIENQTKT;
                    NguyenNhanSuCo.NHIEMKHUANBENHVIEN = NHIEMKHUANBENHVIEN;
                    NguyenNhanSuCo.THUOCDICHTRUYEN = THUOCDICHTRUYEN;
                    NguyenNhanSuCo.CHEPHAMMAU = CHEPHAMMAU;
                    NguyenNhanSuCo.THIETBIYTE = THIETBIYTE;
                    NguyenNhanSuCo.HANHVI = HANHVI;
                    NguyenNhanSuCo.TAINANNGUOIBENH = TAINANNGUOIBENH;
                    NguyenNhanSuCo.HATANGCOSO = HATANGCOSO;
                    NguyenNhanSuCo.QLNGUONLUCTC = QLNGUONLUCTC;
                    NguyenNhanSuCo.HSTHUTUCHANHCHINH = HSTHUTUCHANHCHINH;
                    NguyenNhanSuCo.KHAC = KHAC;
                    NguyenNhanSuCo.DTYLDUOCTHUCHIEN = DTYLDUOCTHUCHIEN;
                    NguyenNhanSuCo.NNNHANVIEN = NNNHANVIEN;
                    NguyenNhanSuCo.NNNGUOIBENH = NNNGUOIBENH;
                    NguyenNhanSuCo.NNMOITRUONGLAMVIEC = NNMOITRUONGLAMVIEC;
                    NguyenNhanSuCo.NNTOCHUCDICHVU = NNTOCHUCDICHVU;
                    NguyenNhanSuCo.NNYEUTOBENNGOAI = NNYEUTOBENNGOAI;
                    NguyenNhanSuCo.NNKHAC = NNKHAC;
                    NguyenNhanSuCo.KHACPHUCSUCO = KHACPHUCSUCO;
                    NguyenNhanSuCo.DEXUATKHUYENCAO = DEXUATKHUYENCAO;
                    NguyenNhanSuCo.MOTAKETQUA = MOTAKETQUA;
                    NguyenNhanSuCo.THAOLUANKHUYENCAO = THAOLUANKHUYENCAO;
                    NguyenNhanSuCo.PHUHOPKHUYENCAO = PHUHOPKHUYENCAO;
                    NguyenNhanSuCo.TONGTHUONGNBNC0 = TONGTHUONGNBNC0;
                    NguyenNhanSuCo.TONGTHUONGNBNC1 = TONGTHUONGNBNC1;
                    NguyenNhanSuCo.TONGTHUONGNBNC2 = TONGTHUONGNBNC2;
                    NguyenNhanSuCo.TONGTHUONGNBNC3 = TONGTHUONGNBNC3;
                    NguyenNhanSuCo.DANHGIATRENTOCHUC = DANHGIATRENTOCHUC;
                    NguyenNhanSuCo.TRANGTHAI = (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.Moi;
                    //NguyenNhanSuCo.NGUOILAP_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Add(ORenderInfo, NguyenNhanSuCo);
                    RetAjaxOut.RetExtraParam1 = NguyenNhanSuCo.ID;
                }
                else
                {
                    NguyenNhanSuCo.NGUOILAP_ID = NGUOILAP_ID;
                    NguyenNhanSuCo.CHUCDANH_ID = CHUCDANH_ID;
                    NguyenNhanSuCo.THOIGIANLAP = string.IsNullOrWhiteSpace(THOIGIANLAP) ? null : (DateTime?)DateTime.ParseExact(THOIGIANLAP, "HH:mm dd/MM/yyyy", null);
                    NguyenNhanSuCo.MOTASUCO = MOTASUCO;
                    NguyenNhanSuCo.THUCHIENQTKT = THUCHIENQTKT;
                    NguyenNhanSuCo.NHIEMKHUANBENHVIEN = NHIEMKHUANBENHVIEN;
                    NguyenNhanSuCo.THUOCDICHTRUYEN = THUOCDICHTRUYEN;
                    NguyenNhanSuCo.CHEPHAMMAU = CHEPHAMMAU;
                    NguyenNhanSuCo.THIETBIYTE = THIETBIYTE;
                    NguyenNhanSuCo.HANHVI = HANHVI;
                    NguyenNhanSuCo.TAINANNGUOIBENH = TAINANNGUOIBENH;
                    NguyenNhanSuCo.HATANGCOSO = HATANGCOSO;
                    NguyenNhanSuCo.QLNGUONLUCTC = QLNGUONLUCTC;
                    NguyenNhanSuCo.HSTHUTUCHANHCHINH = HSTHUTUCHANHCHINH;
                    NguyenNhanSuCo.KHAC = KHAC;
                    NguyenNhanSuCo.DTYLDUOCTHUCHIEN = DTYLDUOCTHUCHIEN;
                    NguyenNhanSuCo.NNNHANVIEN = NNNHANVIEN;
                    NguyenNhanSuCo.NNNGUOIBENH = NNNGUOIBENH;
                    NguyenNhanSuCo.NNMOITRUONGLAMVIEC = NNMOITRUONGLAMVIEC;
                    NguyenNhanSuCo.NNTOCHUCDICHVU = NNTOCHUCDICHVU;
                    NguyenNhanSuCo.NNYEUTOBENNGOAI = NNYEUTOBENNGOAI;
                    NguyenNhanSuCo.NNKHAC = NNKHAC;
                    NguyenNhanSuCo.KHACPHUCSUCO = KHACPHUCSUCO;
                    NguyenNhanSuCo.DEXUATKHUYENCAO = DEXUATKHUYENCAO;
                    NguyenNhanSuCo.MOTAKETQUA = MOTAKETQUA;
                    NguyenNhanSuCo.THAOLUANKHUYENCAO = THAOLUANKHUYENCAO;
                    NguyenNhanSuCo.PHUHOPKHUYENCAO = PHUHOPKHUYENCAO;
                    NguyenNhanSuCo.TONGTHUONGNBNC0 = TONGTHUONGNBNC0;
                    NguyenNhanSuCo.TONGTHUONGNBNC1 = TONGTHUONGNBNC1;
                    NguyenNhanSuCo.TONGTHUONGNBNC2 = TONGTHUONGNBNC2;
                    NguyenNhanSuCo.TONGTHUONGNBNC3 = TONGTHUONGNBNC3;
                    NguyenNhanSuCo.DANHGIATRENTOCHUC = DANHGIATRENTOCHUC;
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Save(ORenderInfo, NguyenNhanSuCo.ID, NguyenNhanSuCo);
                }
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
                #endregion Thêm mới/cập nhật đăng ký
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
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Delete(ORenderInfo, NguyenNhanSuCoId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                string dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DanhSachPhanTichNguyenNhanSuCo", new WebParamCls[] { });
                RetAjaxOut.RetUrl = dangKyUrl;
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);                      
                PhieuPhanTichNguyenNhanSuCoCls NguyenNhanSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ORenderInfo, NguyenNhanSuCoId);
                if (NguyenNhanSuCo == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phiếu này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                NguyenNhanSuCo.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Save(ORenderInfo, NguyenNhanSuCo.ID, NguyenNhanSuCo);
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
        public static AjaxOut ShowSelect2(RenderInfoCls ORenderInfo, string NguyenNhanSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OwnerUserCls Users = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, ID);
                if (Users != null)
                {
                    RetAjaxOut.RetExtraParam1 += string.Format("<option value='" + Users.OwnerUserId + "'>{0} - {1}</option>\r\n", Users.OwnerCode, Users.FullName);
                }
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
    }
}
