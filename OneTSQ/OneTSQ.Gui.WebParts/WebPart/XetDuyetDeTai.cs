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
using System.IO;

namespace OneTSQ.WebParts
{
    public class XetDuyetDeTai : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "XetDuyetDeTai";
        public override string WebPartTitle { get { return "Xét duyệt đề tài"; } }
        public override string Description { get { return "Xét duyệt đề tài"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, XetDuyetDeTai.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(XetDuyetDeTai), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string keyword = WebEnvironments.Request("Keyword");
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);           
                string XetDuyetDeTaiId = WebEnvironments.Request("id");
                string LichXetDuyetId = WebEnvironments.Request("lichxetduyetid");
                //DeTaiCls XetDuyetDeTai = string.IsNullOrEmpty(XetDuyetDeTaiId) ? new DeTaiCls() : CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, XetDuyetDeTaiId);            
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnHoanTat' title='Hoàn tất' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "' onclick='HoanTat();' style='float:left; margin-left: 20px;'>\r\n" +
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-3'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đề tài") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divDrawDanhSachDeTai'> \r\n" +
                                            DrawDanhSachDeTai(ORenderInfo, pageIndex, keyword, LichXetDuyetId).HtmlContent +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "       <div class='col-lg-9'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin đề tài") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divDrawThongTinDeTai'> \r\n" +
                                            DrawThongTinDeTai(ORenderInfo, XetDuyetDeTaiId).HtmlContent +
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
                    "   var XetDuyetDeTaiId='" + XetDuyetDeTaiId + "';\r\n" +
                    "   var _currentPageIndex=0;\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'HH:mm DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CheckTrangThai();\r\n" +
                    "     CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "     CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.CheckTrangThai(RenderInfo, XetDuyetDeTaiId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   XetDuyetDeTaiId = document.getElementById('hdXetDuyetDeTaiId').value;\r\n" +
                    "   if(XetDuyetDeTaiId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return XetDuyetDeTaiId;\r\n" +
                    "}\r\n" +
                #endregion 
                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       _currentPageIndex = PageIndex;\r\n" +
                    "       RealCallReading();\r\n" +
                    "   }\r\n" +
                    "   function RealCallReading()\r\n" +
                    "   {\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OneTSQ.WebParts.XetDuyetDeTai.DrawYKienHoiDong(CreateRenderInfo(), Keyword, _currentPageIndex, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divYKienHoiDong').innerHTML = res.value.HtmlContent;\r\n" +
                    "       $('.confirm').focus(); " +
                    "   }\r\n" +
                #region Check Trạng Thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    XetDuyetDeTaiId = document.getElementById('hdXetDuyetDeTaiId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.CheckTrangThai(RenderInfo, XetDuyetDeTaiId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == "+ (int)DeTaiCls.eTrangThai.HoanTat +")\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawThongTinDeTai :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnHoanTat').hide();\r\n" +
                    "           $(\"#divYKienHoiDong\").css(\"pointer-events\",\"none\");\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawThongTinDeTai :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnHoanTat').show();\r\n" +
                    "           $(\"#divYKienHoiDong\").css(\"pointer-events\",\"auto\");\r\n" +
                    "       }\r\n" +
                    "}\r\n" +
                #endregion
                #region Refresh form về trạng thái mới
                    "function Clear()\r\n" +
                    "{\r\n" +
                    "   window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                    "   CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "}\r\n" +
                #endregion
                #region Hoàn tất
                    "   function HoanTat(){\r\n" +
                    "        Ma = document.getElementById('txtMaDeTai').value\r\n" +
                    "        if(Ma == '')\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường mã đề tài không được bỏ trống!") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "       SaveThongTin()\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       XetDuyetDeTaiId = document.getElementById('hdXetDuyetDeTaiId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.UpdateTrangThai(RenderInfo, XetDuyetDeTaiId, " + (int)DeTaiCls.eTrangThai.HoanTat + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnHoanTat').hide();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã hoàn tất!") + "');\r\n" +
                    "           AddHistory(XetDuyetDeTaiId, '" + WebPartTitle + "', 'Hoàn tất xét duyệt đề tài', 'Tác vụ form');\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thu hồi
                    "   function ThuHoi(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi xét duyệt này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               XetDuyetDeTaiId = document.getElementById('hdXetDuyetDeTaiId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.UpdateTrangThai(RenderInfo, XetDuyetDeTaiId, " + (int)DeTaiCls.eTrangThai.DaLapLich + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   $('#btnLuu').show();\r\n" +
                    "                   $('#btnHoanTat').show();\r\n" +
                    "                   AddHistory(XetDuyetDeTaiId, '" + WebPartTitle + "', 'Thu hồi hoàn tất xét duyệt đề tài', 'Tác vụ form');\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion             
                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     XetDuyetDeTaiId = document.getElementById('hdXetDuyetDeTaiId').value;\r\n" +
                    "     Ma = document.getElementById('txtMaDeTai').value;\r\n" +
                    "     YKienChung = document.getElementById('txtYKienChung').value;\r\n" +
                    "     KetLuan = document.getElementById('cbbKetLuan').value;\r\n" +
                    "     ThoiGianBatDau = document.getElementById('dtThoiGianBatDau').value;\r\n" +
                    "     ThoiGianKetThuc = document.getElementById('dtThoiGianKetThuc').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.SaveThongTin(RenderInfo, XetDuyetDeTaiId, Ma, YKienChung, KetLuan, ThoiGianBatDau, ThoiGianKetThuc).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdXetDuyetDeTaiId').value = XetDuyetDeTaiId = AjaxOut.RetExtraParam1;\r\n" +
                    "           AddHistory(XetDuyetDeTaiId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(XetDuyetDeTaiId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 
                #region Add ý kiến hội đồng xét duyệt

                    "   function CloseModal()\r\n" +
                    "   {\r\n" +
                    "       $('#divFormModal').modal('hide');\r\n" +
                    "   }\r\n" +
                    "   function ServerSideDrawAddForm()\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.ServerSideDrawAddForm(CreateRenderInfo()).value;\r\n" +
                    "       if(AjaxOut.Error) { \r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML = '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, " Đánh giá đề tài") + " </span>';\r\n" +
                    "       document.getElementById('divModalContent').innerHTML = AjaxOut.HtmlContent; \r\n" +
                    "       $('#divFormModal').modal('show');\r\n" +
                    //"       CallInitSelect2('cbbNguoiDanhGia', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "');\r\n" +
                    "   }\r\n" +
                    "   function ServerSideDrawUpdateForm(Id)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.ServerSideDrawUpdateForm(CreateRenderInfo(), Id).value;\r\n" +
                    "       if(AjaxOut.Error) { \r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML = '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, " Sửa thông tin Tên khóa học") + " </span>';\r\n" +
                    "       document.getElementById('divModalContent').innerHTML = AjaxOut.HtmlContent; \r\n" +
                    "       $('#divFormModal').modal('show');\r\n" +
                    //"       CallInitSelect2('cbbNguoiDanhGia', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "');\r\n" +
                    "   }\r\n" +
                    "   function ServerSideAdd()\r\n" +
                    "   {\r\n" +
                    "       DanhGiaID = document.getElementById('hđanhGiaId').value.trim();\r\n" +
                    "       YKienNhanXet = document.getElementById('txtYKienNhanXet').value.trim();\r\n" +
                    "       YKienKhac = document.getElementById('txtYKienKhac').value.trim();\r\n" +
                    "       DiemCham = document.getElementById('txtDiemCham').value.trim() || null;\r\n" +
                    "       NguoiDanhGia = document.getElementById('cbbNguoiDanhGia').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.ServerSideAdd(CreateRenderInfo(), DanhGiaID, YKienNhanXet, YKienKhac, DiemCham, NguoiDanhGia).value;\r\n" +
                    "       if(AjaxOut.Error) {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divYKienHoiDong').html(AjaxOut.HtmlContent);\r\n" +
                    "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã lưu!") + "\");\r\n" +
                    "       CloseModal();\r\n" +
                    "   }\r\n" +
                    "   function ServerSideDelete(Id){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa đánh giá này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.ServerSideDelete(RenderInfo, Id).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#divYKienHoiDong').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region onclickLoadDangKy
                    "   function onclickLoadDangKy(id){\r\n" +
                    "       _xetduyetid = id;\r\n" +
                    "       onLoad();\r\n" +
                    "       txtMaDeTai.disabled=true;\r\n" +
                    "       txtTenDeTai.disabled=true;\r\n" +
                    "       txtChuNhiemDetai.disabled=true;\r\n" +
                    "       cbbCapDeTai.disabled=true;\r\n" +
                    "   }\r\n" +
                    "    function onLoad(){\r\n" +
                    "       if(_xetduyetid == \"\")\r\n" +
                    "           return false;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.DrawThongTinDeTai(RenderInfo, _xetduyetid).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return false;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtMaDeTai').value = AjaxOut.RetExtraParam1;\r\n" +
                    "       document.getElementById('txtTenDeTai').value = AjaxOut.RetExtraParam2;\r\n" +
                    "       cbbCapDeTai.innerHTML = AjaxOut.RetExtraParam3;\r\n" +
                    "       document.getElementById('txtChuNhiemDetai').value = AjaxOut.RetExtraParam4;\r\n" +
                    "       document.getElementById('dtThoiGianBatDau').value = AjaxOut.RetExtraParam5;\r\n" +
                    "       document.getElementById('dtThoiGianKetThuc').value = AjaxOut.RetExtraParam6;\r\n" +
                    "       cbbKetLuan.innerHTML = AjaxOut.RetObject;\r\n" +
                    "       document.getElementById('txtYKienChung').value = AjaxOut.RetObject1;\r\n" +
                    "       document.getElementById('hdXetDuyetDeTaiId').value = AjaxOut.RetObject2;\r\n" +
                    "        AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.DrawTaiLieuDeTai(RenderInfo,_xetduyetid).value;\r\n" +
                    "         if(AjaxOut.Error)\r\n" +
                    "         {\r\n" +
                    "             callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "             return false;\r\n" +
                    "         }\r\n" +
                    "           divTaiLieuDeTai.innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "        AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.DrawHoiDongXetDuyet(RenderInfo).value;\r\n" +
                    "         if(AjaxOut.Error)\r\n" +
                    "         {\r\n" +
                    "             callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "             return false;\r\n" +
                    "         }\r\n" +
                    "           divHoiDongXetDuyet.innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "        AjaxOut = OneTSQ.WebParts.XetDuyetDeTai.DrawYKienHoiDong(RenderInfo).value;\r\n" +
                    "         if(AjaxOut.Error)\r\n" +
                    "         {\r\n" +
                    "             callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "             return false;\r\n" +
                    "         }\r\n" +
                    "           divYKienHoiDong.innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "       CheckTrangThai();\r\n" +
                    "   }\r\n" +
                #endregion
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
        public static AjaxOut DrawDanhSachDeTai(RenderInfoCls ORenderInfo, int PageIndex, string keyword, string LichXetDuyetId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);

                DeTaiFilterCls filter = new DeTaiFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = keyword,
                    LICHXETDUYET_ID = LichXetDuyetId,
                    TrangThais = (int)DeTaiCls.eTrangThai.DaLapLich + "," + (int)DeTaiCls.eTrangThai.HoanTat,
                };
                long recordTotal = 0;
                DeTaiCls[] DeTai = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int dangKyTotal = DeTai.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                Html =
                       "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                       "  <thead> \r\n" +
                       "      <tr> \r\n" +
                       "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                       "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên đề tài") + " </th> \r\n" +
                       "      </tr> \r\n" +
                       "  </thead> \r\n" +
                       "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < dangKyTotal; iIndex++)
                {
                    var DeTaiUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "XetDuyetDeTai",
                        new WebParamCls[] { new WebParamCls("id", DeTai[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),});
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href=\"javascript:onclickLoadDangKy('" + DeTai[iIndex].ID + "');\" title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href=\"javascript:onclickLoadDangKy('" + DeTai[iIndex].ID + "');\" title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + DeTai[iIndex].TENDETAI + "</a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n";
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
        public static AjaxOut DrawThongTinDeTai(RenderInfoCls ORenderInfo, string XetDuyetDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeTaiCls XetDuyetDeTai = string.IsNullOrEmpty(XetDuyetDeTaiId) ? new DeTaiCls() : CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, XetDuyetDeTaiId);
                List<TaiLieuDinhKemCls> OTaiLieuDK = string.IsNullOrEmpty(XetDuyetDeTai.ID) ? new List<TaiLieuDinhKemCls>() : CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ORenderInfo, new TaiLieuDinhKemFilterCls() { DOCUMENT_ID = XetDuyetDeTai.ID }).ToList();
                List<HoiDongXetDuyetCls> OHoiDongXetDuyets = string.IsNullOrEmpty(XetDuyetDeTai.LICHXETDUYET_ID) ? new List<HoiDongXetDuyetCls>() : CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Reading(ORenderInfo, new HoiDongXetDuyetFilterCls() { LICHXETDUYET_ID = XetDuyetDeTai.LICHXETDUYET_ID }).ToList();
                List<DanhGiaDeCuong_DeTaiCls> ODanhGia = string.IsNullOrEmpty(XetDuyetDeTai.ID) ? new List<DanhGiaDeCuong_DeTaiCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Reading(ORenderInfo, new DanhGiaDeCuong_DeTaiFilterCls() { DETAI_ID = XetDuyetDeTai.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "TaiLieuDinhKems", OTaiLieuDK);
                WebSessionUtility.SetSession(OSiteParam, "HoiDongXetDuyets", OHoiDongXetDuyets);
                WebSessionUtility.SetSession(OSiteParam, "DanhGias", ODanhGia);
                RetAjaxOut.RetExtraParam1 = XetDuyetDeTai.MA;
                RetAjaxOut.RetExtraParam2 = XetDuyetDeTai.TENDETAI;
                string cbbCapDeTai = "";
                string chunhiem = "";
                if (XetDuyetDeTai != null)
                {
                    DangKyDeTaiCls DangKyDeTai = null;
                    if (!string.IsNullOrEmpty(XetDuyetDeTai.DANGKYDETAI_ID))
                    {
                        DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, XetDuyetDeTai.DANGKYDETAI_ID);
                        if (DangKyDeTai.CAPDETAI != null)
                        {
                            foreach (var ldt in DangKyDeTaiParser.CapDeTais)
                                cbbCapDeTai += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, DangKyDeTai.CAPDETAI == ldt.Key ? "selected" : null, ldt.Value);
                        }
                        OwnerUserCls chunhiems = null;
                        if (!string.IsNullOrEmpty(DangKyDeTai.CHUNHIEM_ID))
                        {
                            chunhiems = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DangKyDeTai.CHUNHIEM_ID);
                            if (chunhiems != null)
                            {
                                chunhiem = chunhiems.FullName;
                            }
                        }
                    }
                }
                RetAjaxOut.RetExtraParam3 = cbbCapDeTai;
                RetAjaxOut.RetExtraParam4 = chunhiem;
                string cbbKetLuan = "<select class='form-control' id='cbbKetLuan' style='font-size: 14px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Kết luận") + "'>\r\n";
                foreach (var ldt in DeTaiParser.KetLuans)
                    cbbKetLuan += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, XetDuyetDeTai.KETLUAN == ldt.Key ? "selected" : null, ldt.Value);
                cbbKetLuan += "</select>\r\n";
                RetAjaxOut.RetExtraParam5 = XetDuyetDeTai.THOIGIANBATDAU == null ? null : XetDuyetDeTai.THOIGIANBATDAU.Value.ToString("HH:mm dd/MM/yyyy");
                RetAjaxOut.RetExtraParam6 = XetDuyetDeTai.THOIGIANKETTHUC == null ? null : XetDuyetDeTai.THOIGIANKETTHUC.Value.ToString("HH:mm dd/MM/yyyy");
                RetAjaxOut.RetObject = cbbKetLuan;
                RetAjaxOut.RetObject1 = XetDuyetDeTai.YKIENCHUNG;
                RetAjaxOut.RetObject2 = XetDuyetDeTai.ID;
                string Html =
                "               <input id='hdXetDuyetDeTaiId' type='hidden' value='" + XetDuyetDeTai.ID + "'>\r\n" +
                "               <div class=\"row\" id='divXetDuyetDeTai'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Mã đề tài: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtMaDeTai' type='text' style='z-index: 0;' required value='" + XetDuyetDeTai.MA + "'  class='form-control valueForm' disabled>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                                <div class=\"col-md-6\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề tài: ") +
                "                                        <input id='txtTenDeTai' type='text' style='z-index: 0;' value='" + XetDuyetDeTai.TENDETAI + "' class='form-control valueForm' disabled>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài: ") +
                "                                       <input id='txtChuNhiemDetai' type='text' style='z-index: 0;' value='" + chunhiem + "' class='form-control valueForm' disabled>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài: ") +
                "                                      <select class='form-control' id='cbbCapDeTai' style='font-size: 14px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài") + "' disabled>\r\n" +
                                                          cbbCapDeTai +
                "                                      </select>\r\n" +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top:20px;'>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, " Tài liệu đề tài") +
                                                    DrawTaiLieuDeTai(ORenderInfo, XetDuyetDeTai.ID).HtmlContent +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, " Hội đồng xét duyệt") +
                                                    DrawHoiDongXetDuyet(ORenderInfo).HtmlContent +
                "                               </div> \r\n" +
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top:20px;'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, " Ý kiến của hội đồng xét duyệt") +
                                                    DrawYKienHoiDong(ORenderInfo).HtmlContent +
                "                               </div> \r\n" +
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top:20px;'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Ý kiến chung của hội đồng") +
                "                                       <textarea  id='txtYKienChung'  maxlength=\"2000\" rows=\"4\" type='text' style='z-index: 0; height: 120px;' class='form-control valueForm'>" + XetDuyetDeTai.YKIENCHUNG + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\" id='txtKetLuan'>\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Kết luận: ") +
                                                        cbbKetLuan +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian bắt đầu:") +
                "                                       <input id='dtThoiGianBatDau' type='text' style='z-index: 0;' value='" + (XetDuyetDeTai.THOIGIANBATDAU == null ? null : XetDuyetDeTai.THOIGIANBATDAU.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian kết thúc:") +
                "                                       <input id='dtThoiGianKetThuc' type='text' style='z-index: 0;' value='" + (XetDuyetDeTai.THOIGIANKETTHUC == null ? null : XetDuyetDeTai.THOIGIANKETTHUC.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +

                "                       </div>\r\n" +
                "                   </div>\r\n" +
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
        public static AjaxOut DrawTaiLieuDeTai(RenderInfoCls ORenderInfo, string DeTaiID)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<TaiLieuDinhKemCls> OTaiLieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\" id='divTaiLieuDeTai'> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên tài liệu") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "File đính kèm") + " </th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < OTaiLieus.Count; iIndex++)
                {
                    string pathToFile = WebConfig.GetWebHttpRoot() + "/" + WebConfig.GetWebConfig("UploadedTaiLieuPath");
                    string taiLieuUrl = Path.Combine(pathToFile, OTaiLieus[iIndex].TENTAILIEU);
                    string[] fileNameArray = OTaiLieus[iIndex].TENTAILIEU.Split('.');
                    string fileType = fileNameArray[fileNameArray.Length - 1];
                    string aTag = "";
                    if (new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx" }.Contains(fileType))//Nếu là các file thuộc office thì mở nhờ vào http://docs.google.com/gview
                    {
                        if (!string.IsNullOrEmpty(WebConfig.GetWebConfig("GoogleViewerUrl")))
                        {
                            taiLieuUrl = WebConfig.GetWebConfig("GoogleViewerUrl") + taiLieuUrl;
                            aTag = "<a href = '" + taiLieuUrl + "' target='_blank'><i class='fa fa-eye'></i> " + (OTaiLieus[iIndex].TENHIENTHI == null ? null : OTaiLieus[iIndex].TENHIENTHI) + "</a>";
                        }
                    }
                    else if (new string[] { "pdf", "bmp", "jpg", "jpeg", "jpe", "jfif", "gif", "tif", "tiff", "png", "heic", "mp3", "mp4", "mov", "mpeg4", "avi", "wmv", "mpegps", "flv", "3gpp" }.Contains(fileType))
                        aTag = "<a href = '" + taiLieuUrl + "' target='_blank'><i class='fa fa-eye'></i>  " + (OTaiLieus[iIndex].TENHIENTHI == null ? null : OTaiLieus[iIndex].TENHIENTHI) + "</a>";
                    html +=
                        "       <tr>\r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td style='text-align: left; vertical-align: middle;'>" + OTaiLieus[iIndex].GHICHU + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + aTag + "</td> \r\n" +
                        "       </tr> \r\n";
                }
                html += "   </tbody> \r\n" +
                        "</table> \r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
                html += "   </tbody> \r\n" +
                        "</table> \r\n";
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawHoiDongXetDuyet(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<HoiDongXetDuyetCls> OHoiDongXetDuyets = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\" id='divHoiDongXetDuyet'> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + " </th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < OHoiDongXetDuyets.Count; iIndex++)
                {
                    string chucvu = "";
                    string tennguoidung = "";
                    string manguoidung = "";
                    ChucVuCls chucvus = null;
                    if (!string.IsNullOrEmpty(OHoiDongXetDuyets[iIndex].CHUCVU_ID))
                    {
                        chucvus = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), OHoiDongXetDuyets[iIndex].CHUCVU_ID);
                        if (chucvus != null)
                        {
                            chucvu = chucvus.Ten;
                        }
                    }
                    OwnerUserCls nguoidungs = null;
                    if (!string.IsNullOrEmpty(OHoiDongXetDuyets[iIndex].NGUOIDUNG_ID))
                    {
                        nguoidungs = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, OHoiDongXetDuyets[iIndex].NGUOIDUNG_ID);
                        if (nguoidungs != null)
                        {
                            tennguoidung = nguoidungs.FullName;
                            manguoidung = nguoidungs.LoginName;
                        }
                    }
                    html +=
                    "       <tr>\r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + manguoidung + "</td> \r\n" +
                    "           <td style='text-align: left; vertical-align: middle;'>" + tennguoidung + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + chucvu + "</td> \r\n" +
                    "       </tr> \r\n";
                }
                html += "   </tbody> \r\n" +
                        "</table> \r\n";
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawYKienHoiDong(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<DanhGiaDeCuong_DeTaiCls> ODanhGias = WebSessionUtility.GetSession(OSiteParam, "DanhGias") as List<DanhGiaDeCuong_DeTaiCls>;
                string html =
                      "             <table class=\"table table-striped table-bordered table-hover dataTables-autosort\" id='divYKienHoiDong'> \r\n" +
                      "                 <thead> \r\n" +
                      "                 <tr> \r\n" +
                      "                      <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                      "                      <th width=350 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ý kiến nhận xét đánh giá") + " </th> \r\n" +
                      "                      <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Điểm chấm") + " </th> \r\n" +
                      "                      <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + " </th> \r\n" +
                      "                      <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian đánh giá") + " </th> \r\n" +
                      "                      <th width=60 style='text-align:center;'><a id=\"btnThemDangGia\" onclick='javascript:ServerSideDrawAddForm();' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "'><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                      "                  </tr> \r\n" +
                      "              </thead> \r\n" +
                      "              <tbody> \r\n";
                for (int iIndex = 0; iIndex < ODanhGias.Count; iIndex++)
                {
                    string tennguoidung = "";
                    OwnerUserCls nguoidungs = null;
                    if (!string.IsNullOrEmpty(ODanhGias[iIndex].NGUOIDANHGIA_ID))
                    {
                        nguoidungs = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, ODanhGias[iIndex].NGUOIDANHGIA_ID);
                        if (nguoidungs != null)
                        {
                            tennguoidung = nguoidungs.FullName;
                        }
                    }
                    html +=
                      "                 <tr> \r\n" +
                      "                     <input type='hidden' id='hdDanhGiaNhanXetId" + iIndex + "' value='" + ODanhGias[iIndex].ID + "'>\r\n" +
                      "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                      "                     <td title=\"" + ODanhGias[iIndex].DANHGIA + "\">" + ODanhGias[iIndex].DANHGIA + "</td> \r\n" +
                      "                     <td style='text-align: center; vertical-align: middle;' title=\"" + ODanhGias[iIndex].DIEM + "\">" + ODanhGias[iIndex].DIEM + "</td> \r\n" +
                      "                     <td title=\"" + tennguoidung + "\">" + tennguoidung + "</td> \r\n" +
                      "                     <td title=\"" + ODanhGias[iIndex].NGAYTAO + "\">" + ODanhGias[iIndex].NGAYTAO.Value.ToString("HH:mm dd/MM/yyyy") + "</td> \r\n" +
                      "                     <td class=\"td-center\" style=\"text-align:center;\"><a id=\"btnCapNhatDangGia\" title=\"" + "" + WebLanguage.GetLanguage(OSiteParam, "Sửa Tên khóa học") + "" + "\" href=\"javascript:ServerSideDrawUpdateForm('" + ODanhGias[iIndex].ID + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a> &nbsp; <a id=\"btnXoaDangGia\" title=\"" + "Xóa Tên khóa học" + "\" href=\"javascript:ServerSideDelete('" + ODanhGias[iIndex].ID + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                      "                 </tr> \r\n";
                }
                html +=
                   "                 </tbody> \r\n" +
                   "             </table> \r\n";
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string XetDuyetDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeTaiCls XetDuyetDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, XetDuyetDeTaiId);
                if (XetDuyetDeTai != null)
                    RetAjaxOut.RetExtraParam1 = XetDuyetDeTai.TRANGTHAI.ToString();
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {

                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<HoiDongXetDuyetCls> OHoiDongXetDuyets = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                string cbbNguoiDanhGia = "";
                for (int iIndex = 0; iIndex < OHoiDongXetDuyets.Count; iIndex++)
                {
                    OwnerUserCls nguoidungs = null;
                    if (!string.IsNullOrEmpty(OHoiDongXetDuyets[iIndex].NGUOIDUNG_ID))
                    {
                        nguoidungs = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, OHoiDongXetDuyets[iIndex].NGUOIDUNG_ID);
                        if (nguoidungs != null)
                        {
                            cbbNguoiDanhGia += string.Format("<option value={0}>{1}</option>\r\n", nguoidungs.OwnerUserId, nguoidungs.FullName);
                        }
                    }
                }
                string Html =
                        "<form id=\"sendmail\" data-async  method=\"post\" onSubmit=\"javascript:ServerSideAdd(); return false;\" role=\"form\" class=\"contactForm\">\r\n" +
                        "       <input id='hđanhGiaId' type='hidden' value=''>\r\n" +
                        "      <div style=\"max-height: calc(100vh - 210px); overflow-y:scroll; white-space: nowrap;\"> \r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Ý kiến nhận xét đánh giá ") + "</label></br>\r\n" +
                        "                   <textarea id=\"txtYKienNhanXet\" style='z-index: 0; height: 300px;' maxlength=\"2000\" rows=\"4\" type=\"text\" placeholder=\"Ý kiến nhận xét đánh giá\" class=\"form-control\"></textarea>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label>" + WebLanguage.GetLanguage(OSiteParam, "Ý kiến khác ") + "</label></br>\r\n" +
                        "                   <textarea id=\"txtYKienKhac\" style='z-index: 0; height: 150px;' maxlength=\"2000\" rows=\"4\" type=\"text\" placeholder=\"Ý kiến khác\" class=\"form-control\"></textarea>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class='row'>\r\n" +
                        "                   <div class=\"col-md-2\">\r\n" +
                        "                       <div class=\"form-group\">\r\n" +
                        "                           <label>" + WebLanguage.GetLanguage(OSiteParam, "Điểm chấm") + "</label>\r\n</ br >\r\n" +
                        "                           <input class='form-control' id='txtDiemCham' type='number' step='0.1' onkeypress='CheckCurrency(event);' maxlength=\"3\" title = '" + WebLanguage.GetLanguage(OSiteParam, "Điểm chấm") + "'></input>\r\n" +
                        "                       </div> \r\n" +
                        "                   </div>\r\n" +
                        "                   <div class=\"col-md-2\">\r\n" +
                        "                       <div class=\"form-group\">\r\n" +
                        "                           <label>" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "</label>\r\n</br>\r\n" +
                        "                           <select class='form-control' id='cbbNguoiDanhGia' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "'>\r\n" +
                                                        cbbNguoiDanhGia +
                        "                           </select>\r\n" +
                        "                       </div> \r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "      </div>\r\n" +
                        "      <div class=\"form-group\" >\r\n" +

                        "           <div class=\"form-group\" style=\"margin-top: 10px; margin-left: 1500px;\">\r\n" +
                        "               <button class=\"btn btn-sm btn-primary mr-5px\" type=\"submit\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "</strong></button> \r\n" +
                        "               <div id='response'></div>\r\n" +
                        "           </div>\r\n" +

                        "      </div>\r\n" +

                        "</form>";

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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string ID)
        {
            DanhGiaDeCuong_DeTaiCls DanhGiaDeTai = string.IsNullOrEmpty(ID) ? new DanhGiaDeCuong_DeTaiCls() : CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().CreateModel(ORenderInfo, ID);
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DanhGiaDeCuong_DeTaiCls> ODanhGias = WebSessionUtility.GetSession(OSiteParam, "DanhGias") as List<DanhGiaDeCuong_DeTaiCls>;
                string Html = "";
                for (int iIndex = 0; iIndex < ODanhGias.Count; iIndex++)
                {
                    string cbbNguoiDanhGia = "";
                    if (!string.IsNullOrEmpty(ODanhGias[iIndex].NGUOIDANHGIA_ID))
                    {
                        var nguodanhgia = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, ODanhGias[iIndex].NGUOIDANHGIA_ID);
                        if (nguodanhgia != null)
                            cbbNguoiDanhGia += string.Format(" <option value ={0} selected>{1}</option>\r\n", nguodanhgia.OwnerUserId, nguodanhgia.FullName);
                    }
                    Html =
                       "<form id=\"sendmail\" data-async  method=\"post\" onSubmit=\"javascript:ServerSideAdd(); return false;\" role=\"form\" class=\"contactForm\">\r\n" +
                       "       <input id='hđanhGiaId' type='hidden' value='" + ODanhGias[iIndex].ID + "'>\r\n" +
                       "      <div style=\"max-height: calc(100vh - 210px); overflow-y:scroll; white-space: nowrap;\"> \r\n" +

                       "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                       "               <div class=\"form-group\">\r\n" +
                       "                   <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Ý kiến nhận xét đánh giá ") + "</label></br>\r\n" +
                       "                   <textarea id=\"txtYKienNhanXet\" style='z-index: 0; height: 300px;' maxlength=\"2000\" rows=\"4\" type=\"text\" placeholder=\"Ý kiến nhận xét đánh giá\" class=\"form-control\">" + ODanhGias[iIndex].DANHGIA + "</textarea>\r\n" +
                       "               </div>\r\n" +
                       "           </div>\r\n" +

                       "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                       "               <div class=\"form-group\">\r\n" +
                       "                   <label>" + WebLanguage.GetLanguage(OSiteParam, "Ý kiến khác ") + "</label></br>\r\n" +
                       "                   <textarea id=\"txtYKienKhac\" style='z-index: 0; height: 150px;' maxlength=\"2000\" rows=\"4\" type=\"text\" placeholder=\"Ý kiến khác\" class=\"form-control\">" + ODanhGias[iIndex].YKIENKHAC + "</textarea>\r\n" +
                       "               </div>\r\n" +
                       "           </div>\r\n" +

                       "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                       "               <div class='row'>\r\n" +
                       "                   <div class=\"col-md-2\">\r\n" +
                       "                       <div class=\"form-group\">\r\n" +
                       "                           <label>" + WebLanguage.GetLanguage(OSiteParam, "Điểm chấm") + "</label>\r\n</ br >\r\n" +
                       "                           <input class='form-control' id='txtDiemCham'  maxlength=\"2\" title = '" + WebLanguage.GetLanguage(OSiteParam, "Điểm chấm") + "' value='" + ODanhGias[iIndex].DIEM + "'></input>\r\n" +
                       "                       </div> \r\n" +
                       "                   </div>\r\n" +
                       "                   <div class=\"col-md-2\">\r\n" +
                       "                       <div class=\"form-group\">\r\n" +
                       "                           <label>" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "</label>\r\n</br>\r\n" +
                       "                           <select class='form-control' id='cbbNguoiDanhGia' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người đánh giá") + "'>\r\n" +
                                                       cbbNguoiDanhGia +
                       "                           </select>\r\n" +
                       "                       </div> \r\n" +
                       "                   </div>\r\n" +
                       "               </div>\r\n" +
                       "           </div>\r\n" +

                       "      </div>\r\n" +
                       "      <div class=\"form-group\" >\r\n" +

                       "           <div class=\"form-group\" style=\"margin-top: 10px; margin-left: 1500px;\">\r\n" +
                       "               <button class=\"btn btn-sm btn-primary mr-5px\" type=\"submit\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "</strong></button> \r\n" +
                       "               <div id='response'></div>\r\n" +
                       "           </div>\r\n" +

                       "      </div>\r\n" +

                       "</form>";
                }

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
        #endregion Vẽ giao diện

        #region Xử lý nghiệp vụ          
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string XetDuyetDeTaiId, string MaDeTai, string YKienChung, int KetLuan, string ThoiGianBatDau, string ThoiGianKetThuc)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeTaiCls ODeTai = new DeTaiCls();
                if (!string.IsNullOrEmpty(XetDuyetDeTaiId))
                {
                    ODeTai = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, XetDuyetDeTaiId);
                }
                else
                {
                    ODeTai = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, MaDeTai);
                    if (ODeTai == null)
                    {
                        ODeTai = new DeTaiCls() { MA = MaDeTai };
                    }
                }
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(ODeTai.ID))
                {

                }
                else
                {
                    ODeTai.YKIENCHUNG = YKienChung;
                    ODeTai.KETLUAN = KetLuan;
                    ODeTai.THOIGIANBATDAU = string.IsNullOrWhiteSpace(ThoiGianBatDau) ? null : (DateTime?)DateTime.ParseExact(ThoiGianBatDau, "HH:mm dd/MM/yyyy", null);
                    ODeTai.THOIGIANKETTHUC = string.IsNullOrWhiteSpace(ThoiGianKetThuc) ? null : (DateTime?)DateTime.ParseExact(ThoiGianKetThuc, "HH:mm dd/MM/yyyy", null);
                    CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Save(ORenderInfo, ODeTai.ID, ODeTai);
                }
                #endregion Thêm mới/cập nhật đăng ký
                #region Thêm mới/cập nhật tài liệu
                List<DanhGiaDeCuong_DeTaiCls> newDanhGias = WebSessionUtility.GetSession(OSiteParam, "DanhGias") as List<DanhGiaDeCuong_DeTaiCls>;
                List<DanhGiaDeCuong_DeTaiCls> oldDanhGias = CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Reading(ORenderInfo, new DanhGiaDeCuong_DeTaiFilterCls() { DETAI_ID = ODeTai.ID }).ToList();
                foreach (var oldDanhGia in oldDanhGias)
                {
                    bool isExists = false;
                    foreach (var newHoiDong in newDanhGias)
                    {
                        if (newHoiDong.ID == oldDanhGia.ID)//cập nhật
                        {
                            oldDanhGia.DETAI_ID = ODeTai.ID;
                            oldDanhGia.DANHGIA = newHoiDong.DANHGIA;
                            oldDanhGia.YKIENKHAC = newHoiDong.YKIENKHAC;
                            oldDanhGia.DIEM = newHoiDong.DIEM;
                            oldDanhGia.NGUOIDANHGIA_ID = newHoiDong.NGUOIDANHGIA_ID;
                            oldDanhGia.NGAYTAO = newHoiDong.NGAYTAO;
                            CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Save(ORenderInfo, oldDanhGia.ID, oldDanhGia);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Delete(ORenderInfo, oldDanhGia.ID);
                    }
                }
                var addDanhGias = newDanhGias.Where(o => !oldDanhGias.Any(old => old.ID == o.ID));
                foreach (var addDanhGia in addDanhGias)//Thêm mới
                {
                    addDanhGia.DETAI_ID = ODeTai.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Add(ORenderInfo, addDanhGia);
                }
                #endregion
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
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
        public static AjaxOut ServerSideAdd(RenderInfoCls ORenderInfo, string DanhGiaID, string YKienDanhGia, string YKienKhac, string DiemCham, string NguoiDanhGia)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DanhGiaDeCuong_DeTaiCls> DanhGias = WebSessionUtility.GetSession(OSiteParam, "DanhGias") as List<DanhGiaDeCuong_DeTaiCls>;
                if (string.IsNullOrEmpty(DanhGiaID))//thêm mới
                {
                    DanhGias.Add(new DanhGiaDeCuong_DeTaiCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        DANHGIA = YKienDanhGia,
                        YKIENKHAC = YKienDanhGia,
                        DIEM = decimal.Parse(DiemCham),
                        NGUOIDANHGIA_ID = NguoiDanhGia,
                        NGAYTAO = DateTime.Now
                    });
                }
                else//cập nhật
                {
                    DanhGiaDeCuong_DeTaiCls DanhGia = DanhGias.FirstOrDefault(o => o.ID == DanhGiaID);
                    if (DanhGia == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đánh giá này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    DanhGia.DANHGIA = YKienDanhGia;
                    DanhGia.YKIENKHAC = YKienDanhGia;
                    DanhGia.DIEM = decimal.Parse(DiemCham);
                    DanhGia.NGUOIDANHGIA_ID = NguoiDanhGia;
                    DanhGia.NGAYTAO = DateTime.Now;
                }
                RetAjaxOut.HtmlContent = DrawYKienHoiDong(ORenderInfo).HtmlContent;
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
        public static AjaxOut ServerSideDelete(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DanhGiaDeCuong_DeTaiCls> DanhGias = WebSessionUtility.GetSession(OSiteParam, "DanhGias") as List<DanhGiaDeCuong_DeTaiCls>;
                DanhGiaDeCuong_DeTaiCls danhgia = DanhGias.FirstOrDefault(o => o.ID == Id);
                DanhGias.Remove(danhgia);
                RetAjaxOut.HtmlContent = DrawYKienHoiDong(ORenderInfo).HtmlContent;
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeTaiId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeTaiCls ThemMoiDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ORenderInfo, ThemMoiDeTaiId);
                if (ThemMoiDeTai == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thông tin bị thiếu chưa thể hoàn tất.");
                    return RetAjaxOut;
                }
                ThemMoiDeTai.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Save(ORenderInfo, ThemMoiDeTai.ID, ThemMoiDeTai);
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
