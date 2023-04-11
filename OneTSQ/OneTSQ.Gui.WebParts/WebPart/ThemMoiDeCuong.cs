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
    public class ThemMoiDeCuong : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "ThemMoiDeCuong";
        public override string WebPartTitle { get { return "Thêm mới đề cương"; } }
        public override string Description { get { return "Thêm mới đề cương"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, ThemMoiDeCuong.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ThemMoiDeCuong), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string SessionId = System.Guid.NewGuid().ToString();
                string ThemMoiDeCuongId = WebEnvironments.Request("id");     
                #region Html
                string html =
                    "<form action='javascript:SaveDeCuong();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnChuyenDuyet' title='Chuyển duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Chuyển duyệt") + "' onclick='ChuyenDuyet();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnDuyet' title='Duyệt lịch' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Duyệt lịch") + "' onclick='Duyet();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnThuHoiDuyet' title='Thu hồi duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi duyệt") + "' onclick='ThuHoiDuyet();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-12'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin đăng ký đề tài") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divDeCuong'> \r\n" +                   
                                            DrawDeCuong(ORenderInfo, null, ThemMoiDeCuongId).HtmlContent +               
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
                    "   var ThemMoiDeCuongId='"+ ThemMoiDeCuongId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'HH:mm DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbChuNhiemDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + "');\r\n" +
                    "     CallInitSelect2('cbbNguoiGui', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người gửi") + "');\r\n" +
                    "     CallInitSelect2('cbbTenDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(DeTaiDangKyService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đề tài") + "');\r\n" +
                    "     ShowTrangThai();\r\n" +
                    "     CheckTrangThai();\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "   if(ThemMoiDeCuongId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return ThemMoiDeCuongId;\r\n" +  
                    "}\r\n" +
                #endregion 
                #region Show trạng thái 
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.ThemMoiDeCuong.ShowTrangThai(RenderInfo, ThemMoiDeCuongId).value);\r\n" +        
                    "}\r\n" +
                    "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Check trạng thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDeCuong :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnDuyet').show();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "       }\r\n" +
                    "       else if(AjaxOut.RetExtraParam1 == 2)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDeCuong :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       }\r\n" +
                    "       else if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDeCuong :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       }\r\n" +
                    "       else if(AjaxOut.RetExtraParam1 == 4)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDeCuong :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divDeCuong :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnChuyenDuyet').show();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnDuyet').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
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
                #region Thu hồi
                    "   function ThuHoi(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được lập lịch xét duyệt, không thể thu hồi!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được hoàn tất, không thể thu hồi!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi chuyển duyệt đề cương này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongId, " + (int)DeCuongCls.eTrangThai.Moi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   $('#btnLuu').show();\r\n" +
                    "                   $('#btnChuyenDuyet').show();\r\n" +
                    "                   $('#btnThuHoiDuyet').hide();\r\n" +
                    "                   $('#btnDuyet').hide();\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Thu hồi
                    "   function ThuHoiDuyet(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được lập lịch xét duyệt, không thể thu hồi!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 4)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được Hoàn tất, không thể thu hồi!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi duyệt đề cương này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongId, " + (int)DeCuongCls.eTrangThai.ChoXetDuyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   $('#btnLuu').hide();\r\n" +
                    "                   $('#btnChuyenDuyet').hide();\r\n" +
                    "                   $('#btnThuHoiDuyet').hide();\r\n" +
                    "                   $('#btnDuyet').show();\r\n" +
                    "                   $('#btnThuHoi').show();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Thu hồi duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Chuyển duyệt
                    "   function ChuyenDuyet(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được lập lịch xét duyệt, không thể chuyển duyệt!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 4)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được hoàn tất, không thể chuyển duyệt!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       SaveDeCuong();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongId, " + (int)DeCuongCls.eTrangThai.ChoXetDuyet + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnDuyet').show();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +        
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã chuyển duyệt!") + "');\r\n" +
                    "           AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Duyệt
                    "   function Duyet(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được lập lịch xét duyệt, không thể duyệt!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 4)\r\n" +
                    "       {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được hoàn tất, không thể duyệt!") + "');\r\n" +
                    "       return true;\r\n" +
                    "       }\r\n" +
                    "       SaveDeCuong();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongId, " + (int)DeCuongCls.eTrangThai.LapLich+ ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnChuyenDuyet').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyet').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã xét duyệt!") + "');\r\n" +
                    "           AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Save Thông tin
                    "function SaveDeCuong()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đề cương đã được hoàn tất!") + "');\r\n" +
                    "           return true;\r\n" +
                    "       }\r\n" +
                    "     MaDeCuong = document.getElementById('txtMaDeCuong').value;\r\n" +
                    "     TenDeTai = document.getElementById('cbbTenDeTai').value;\r\n" +
                    "     TenDeCuong = document.getElementById('txtTenDeCuong').value;\r\n" +
                    "     ThoiGianGui = document.getElementById('dtThoiGianGui').value;\r\n" +
                    "     NguoiGui = document.getElementById('cbbNguoiGui').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.SaveDeCuong(RenderInfo, ThemMoiDeCuongId, MaDeCuong, TenDeTai, TenDeCuong, ThoiGianGui, NguoiGui).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdThemMoiDeCuongId').value = ThemMoiDeCuongId = AjaxOut.RetExtraParam1;\r\n" +         
                    "           AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(ThemMoiDeCuongId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 
                #region Xóa Thông tin đăng ký
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "   swal({\r\n" +
                    "           title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "           text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắn chắn xóa đăng ký này không?") + "\",\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.DeleteThongTin(RenderInfo, ThemMoiDeCuongId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(ThemMoiDeCuongId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion 
                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "       MaDeCuong = sender.value;\r\n" +
                    "       if( MaDeCuong != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.DrawDeCuong(RenderInfo, MaDeCuong, null).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divDeCuong').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'HH:mm DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "           }\r\n" +
                    "           CallInitSelect2('cbbChuNhiemDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + "');\r\n" +
                    "           CallInitSelect2('cbbNguoiGui', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người gửi") + "');\r\n" +
                    "           CallInitSelect2('cbbTenDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(DeTaiDangKyService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đề tài") + "');\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +                   
                    "   }\r\n" +
                #endregion
                #region Bắt sự kiện thay đổi Combobox tên đề tài
                    "   function cbbTenDeTai_onchange(sender){\r\n" +
                    "       var id= $(sender).val(); \r\n" +
                    "       if(id != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.ShowTTDeTaiDangKy(RenderInfo, id).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           qc= AjaxOut.RetObject;\r\n" +
                    "           if(qc.CHUNHIEM_ID != null && qc.CHUNHIEM_ID != ''){\r\n" +
                    "               $('#cbbChuNhiemDeTai').val(null).trigger('change');\r\n" +
                    "               $('#cbbChuNhiemDeTai').html(''); \r\n" +
                    "               $('#cbbChuNhiemDeTai').append(qc.CHUNHIEM_ID);\r\n" +
                    "               $('#cbbChuNhiemDeTai').trigger('change');\r\n" +
                    "           }\r\n" +
                    "       if(!$('#cbbCapDeTai').val()){\r\n" +
                    "           $('#cbbCapDeTai').val(null).trigger('change');\r\n" +
                    "           $('#cbbCapDeTai').append(qc.CAPDETAI);\r\n" +
                    "           $('#cbbCapDeTai').trigger('change');\r\n" +
                    "       }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Kiểm tra định dạng file
                    "   function CheckFileTaiLieuType(files) \r\n" +
                    "   {\r\n" +
                    "       if (files.length > 0) \r\n" +
                    "       {\r\n" +
                    "           for(var i = 0; i < files.length; i++){\r\n" +
                    "               fileExtension = files[i].name.split('.').pop().toLowerCase();\r\n" +
                    "               if(fileExtension == 'bmp' || fileExtension == 'jpg' || fileExtension == 'jpeg' || fileExtension == 'jpe' || fileExtension == 'jfif' || fileExtension == 'gif' || fileExtension == 'tif' || fileExtension == 'tiff' || fileExtension == 'png' || fileExtension == 'heic'\r\n" +
                    "                   || fileExtension == 'zip' || fileExtension == 'rar' || fileExtension == 'doc' || fileExtension == 'docx' || fileExtension == 'xls' || fileExtension == 'xlsx' || fileExtension == 'ppt' || fileExtension == 'pptx' || fileExtension == 'pdf' " +
                    "                   || fileExtension == 'mp3' || fileExtension == 'mp4' || fileExtension == 'mov' || fileExtension == 'mpeg4' || fileExtension == 'avi' || fileExtension == 'wmv' || fileExtension == 'mpegps' || fileExtension == 'flv' || fileExtension == '3gpp')\r\n" +
                    "               {\r\n" +
                    "                   return true;\r\n" +
                    "               }\r\n" +
                    "           };\r\n" +
                    "           return false;\r\n" +
                    "       }\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị tên của tài liệu được upload
                    "   function ShowSelectedFileNames(sender) \r\n" +
                    "   {\r\n" +
                    "       if (!CheckFileTaiLieuType(sender.files)) \r\n" +
                    "       {\r\n" +
                    "           callGallAlert('Định dạng file không được hỗ trợ. Xin chọn file có định dạng khác.');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if (sender.files.length > 0) \r\n" +
                    "       {\r\n" +
                    "           var tenfile = \"\";\r\n" +
                    "           for(var i = 0; i < sender.files.length; i++){\r\n" +
                    "               tenfile += sender.files[i].name + '; ';\r\n" +
                    "           };\r\n" +
                    "           tenfile = tenfile.substring(0, tenfile.length - 2);\r\n" +
                    "           $('#divTenTaiLieuChon').html(tenfile);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới tài liệu
                    "   function SaveTaiLieu(){\r\n" +
                    "       fileUploadElement=document.getElementById('fileUploadTaiLieu');\r\n" +
                    "       if (!CheckFileTaiLieuType(fileUploadElement.files)) \r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Định dạng file không được hỗ trợ. Xin chọn file có định dạng khác.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        fileUploadValue=fileUploadElement.value;\r\n" +
                    "        if(fileUploadValue==''){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn file cần upload.") + "');\r\n" +
                    "           return;\r\n" +
                    "        }\r\n" +
                    "        var data = new FormData();\r\n" +
                    "        var files = fileUploadElement.files;\r\n" +
                    "        for (i = 0; i < files.length; i++) {\r\n" +
                    "            data.append(''+i, files[i]);\r\n" +
                    "        }\r\n" +
                    "       var ajaxRequest = $.ajax({\r\n" +
                    "           type: 'POST',\r\n" +
                    "           url: '" + GetUploadHandler(OSiteParam, SessionId, user.OwnerUserId, user.LoginName, "UploadedTaiLieuPath") + "',\r\n" +
                    "           contentType: false,\r\n" +
                    "           processData: false,\r\n" +
                    "           data: data,\r\n" +
                    "           success: function(xmlResult) {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           ThemMoiDeCuongId = document.getElementById('hdThemMoiDeCuongId').value;\r\n" +
                    "           GhiChu = document.getElementById('txtTaiLieuDinhKem').value;\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.SaveTaiLieu(RenderInfo, ThemMoiDeCuongId, GhiChu, xmlResult).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           $('#TabTaiLieu').html(AjaxOut.HtmlContent);\r\n" +
                    "           },\r\n" +
                    "           error: function(result) {\r\n" +
                    "           },\r\n" +
                    "       });\r\n" +                  
                    "   }\r\n" +
                #endregion
                #region Xóa tài liệu
                    "   function DeleteTaiLieu(Id){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa tài liệu này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.DeleteTaiLieu(RenderInfo, Id).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#TabTaiLieu').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
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
        public static AjaxOut DrawDeCuong(RenderInfoCls ORenderInfo, string MaDeCuong, string ThemMoiDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeCuongCls ThemMoiDeCuong = null;
                if (!string.IsNullOrEmpty(ThemMoiDeCuongId))
                    ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongId);
                if (!string.IsNullOrEmpty(MaDeCuong))
                    ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, MaDeCuong);
                if (ThemMoiDeCuong == null)
                {
                    ThemMoiDeCuong = new DeCuongCls();
                    if (string.IsNullOrEmpty(MaDeCuong))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        ThemMoiDeCuong.MA = boDemValueMaHV.RetExtraParam2;
                    }
                    else ThemMoiDeCuong.MA = MaDeCuong;
                }
                List<TaiLieuDinhKemCls> OTaiLieuDK = string.IsNullOrEmpty(ThemMoiDeCuong.ID) ? new List<TaiLieuDinhKemCls>() : CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ORenderInfo, new TaiLieuDinhKemFilterCls() { DOCUMENT_ID = ThemMoiDeCuong.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "TaiLieuDinhKems", OTaiLieuDK);            
                string cbbNguoiGui = "";
                if (!string.IsNullOrEmpty(ThemMoiDeCuong.NGUOIGUI_ID))
                {
                    var nguoigui = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, ThemMoiDeCuong.NGUOIGUI_ID);
                    if (nguoigui != null)
                    {
                        cbbNguoiGui += string.Format("<option value={0}>{1}</option>\r\n", nguoigui.OwnerUserId, nguoigui.FullName);
                    }
                }

                string cbbTenDeTai = "";
                if (!string.IsNullOrEmpty(ThemMoiDeCuong.DANGKYDETAI_ID))
                {
                    var Detaidangky = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, ThemMoiDeCuong.DANGKYDETAI_ID);
                    if (Detaidangky != null)
                    {
                        cbbTenDeTai += string.Format("<option value={0}>{1} - {2}</option>\r\n", Detaidangky.ID, Detaidangky.MA, Detaidangky.TENDETAI);
                    }
                }
                string cbbChuNhiemDeTai = "";
                string cbbCapDeTai = "";
                if (!string.IsNullOrEmpty(ThemMoiDeCuong.DANGKYDETAI_ID))
                {
                    var Detaidangky = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, ThemMoiDeCuong.DANGKYDETAI_ID);                  
                    if (Detaidangky != null)
                    {
                        var chunhiem = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, Detaidangky.CHUNHIEM_ID);
                        if (chunhiem != null)
                        {
                            cbbChuNhiemDeTai += string.Format("<option value={0}>{1}</option>\r\n", chunhiem.OwnerUserId, chunhiem.FullName);
                        }
                        foreach (var ldt in DangKyDeTaiParser.CapDeTais)
                            cbbCapDeTai += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, Detaidangky.CAPDETAI == ldt.Key ? "selected" : null, ldt.Value);
                    }
                }
                string Html =
                "               <input id='hdThemMoiDeCuongId' type='hidden' value='"+ ThemMoiDeCuong.ID +"'>\r\n" +
                "               <div class=\"row\" id='divThemMoiDeCuongId'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Mã đề cương: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtMaDeCuong' type='text' style='z-index: 0;' onchange='txtMa_onchange(this)' value='" + ThemMoiDeCuong.MA +"' class='form-control valueForm' required>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề tài: ") +
                "                                        <select id='cbbTenDeTai' type='text' style='z-index: 0;' onchange='cbbTenDeTai_onchange(this)' class='form-control'>\r\n" +
                                                            cbbTenDeTai +
                "                                        </select>\r\n"+                                         
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbChuNhiemDeTai' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm") + "' required>\r\n" +
                                                            cbbChuNhiemDeTai +
                "                                       </select>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài: ") +
                "                                      <select id = 'cbbCapDeTai' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài") + "'>\r\n" +
                                                            cbbCapDeTai +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +            
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-8\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề cương: ") +
                "                                        <input id='txtTenDeCuong' type='text' style='z-index: 0;' value='"+ ThemMoiDeCuong.TENDECUONG +"' class='form-control valueForm' >\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                  <div class=\"col-md-2\">\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian gửi đề cương:") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                          <input id='dtThoiGianGui' required type='text' style='z-index: 0;' value='" + (ThemMoiDeCuong.THOIGIANGUI == null ? null : ThemMoiDeCuong.THOIGIANGUI.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +          
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Người gửi: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbNguoiGui' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người gửi") + "'>\r\n" +
                                                            cbbNguoiGui+
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +             
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top: 20px;'>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tài liệu đính kèm: ") +
                "                                        <input id='txtTaiLieuDinhKem' type='text' style='z-index: 0;' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\" style ='margin-top: 12px;'>\r\n" +
                "                                            <span class=\"btn btn-default btn-file\" ><span class=\"fileinput-new\">Chọn tập tin</span>\r\n" +
                "                                            <span class=\"fileinput-exists\">Chọn lại</span><input onchange='ShowSelectedFileNames(this);' type=\"file\" name=\"...\" id=\"fileUploadTaiLieu\"/></span>\r\n" +
                "                                            <span class=\"fileinput-filename\"></span>\r\n" +
                "                                       <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                "                                       </div> \r\n" +
                "                                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick='javascript:SaveTaiLieu();'> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top: 10px;'>\r\n" +
                "                               <div class=\"col-md-12\" id ='TabTaiLieu'>\r\n" +
                                                    DrawTaiLieu(ORenderInfo).HtmlContent +
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
        public static AjaxOut DrawTaiLieu(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<TaiLieuDinhKemCls> OTaiLieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "File đính kèm") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Người tạo") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + " </th> \r\n" +
                         "          <th style='text-align: center; width = 150' class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < OTaiLieus.Count; iIndex++)
                {
                    OwnerUserCls nguoitaos = null;
                    if (!string.IsNullOrEmpty(OTaiLieus[iIndex].NGUOITAO_ID))
                    {
                        string nguoitao = "";
                        nguoitaos = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, OTaiLieus[iIndex].NGUOITAO_ID);
                        if (nguoitaos != null)
                        {
                            nguoitao = nguoitaos.DepartmentName;
                        }
                    }
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
                        "           <td style='text-align: center; vertical-align: middle;'>" + OTaiLieus[iIndex].GHICHU + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + aTag + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (nguoitaos == null ? null : nguoitaos.LoginName) + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (OTaiLieus[iIndex].NGAYTAO == null ? null : OTaiLieus[iIndex].NGAYTAO.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "           <td style='text-align:center;'><a  title='Xóa' href=\"javascript:DeleteTaiLieu('" + OTaiLieus[iIndex].ID + "');\"><i class='" + WebScreen.GetDeleteGridIcon() + "' style='font-size:20px; margin-top:4px;'></i></a></td>\r\n" +
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
        #endregion Vẽ giao diện

        #region Xử lý nghiệp vụ   
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetBoDemValueAndMaHV(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string boDemValue = null;
            string maHV = null;
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "ThemMoiDeCuong", FieldName = "MaDeCuong" }).FirstOrDefault();
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
        public static AjaxOut SaveDeCuong(RenderInfoCls ORenderInfo, string ThemMoiDeCuongId, string MaDeCuong, string TenDeTai, string TenDeCuong, string ThoiGianGui, string NguoiGui_ID)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeCuongCls ThemMoiDeCuong = new DeCuongCls();
                if (!string.IsNullOrEmpty(ThemMoiDeCuongId))
                {
                    ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongId);
                }
                else
                {
                    ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, MaDeCuong);
                    if (ThemMoiDeCuong == null)
                    {
                        ThemMoiDeCuong = new DeCuongCls() { MA = MaDeCuong };
                    }
                }
                #region Thêm mới/cập nhật dề cương
                if (string.IsNullOrEmpty(ThemMoiDeCuong.ID))
                {
                    ThemMoiDeCuong.ID = ThemMoiDeCuongId = System.Guid.NewGuid().ToString();
                    ThemMoiDeCuong.DANGKYDETAI_ID = TenDeTai;
                    ThemMoiDeCuong.MA = MaDeCuong;
                    ThemMoiDeCuong.TENDECUONG = TenDeCuong;
                    ThemMoiDeCuong.NGUOIGUI_ID = NguoiGui_ID;
                    ThemMoiDeCuong.THOIGIANGUI = string.IsNullOrWhiteSpace(ThoiGianGui) ? null : (DateTime?)DateTime.ParseExact(ThoiGianGui, "HH:mm dd/MM/yyyy", null);
                    ThemMoiDeCuong.TRANGTHAI = (int)DeCuongCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Add(ORenderInfo, ThemMoiDeCuong);
                    RetAjaxOut.RetExtraParam1 = ThemMoiDeCuong.ID;
                }
                else
                {
                    ThemMoiDeCuong.DANGKYDETAI_ID = TenDeTai;
                    ThemMoiDeCuong.MA = MaDeCuong;
                    ThemMoiDeCuong.NGUOIGUI_ID = NguoiGui_ID;
                    ThemMoiDeCuong.TENDECUONG = TenDeCuong;
                    ThemMoiDeCuong.THOIGIANGUI = string.IsNullOrWhiteSpace(ThoiGianGui) ? null : (DateTime?)DateTime.ParseExact(ThoiGianGui, "HH:mm dd/MM/yyyy", null);
                    ThemMoiDeCuong.TRANGTHAI = (int)DeCuongCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Save(ORenderInfo, ThemMoiDeCuong.ID, ThemMoiDeCuong);
                }       
                #endregion Thêm mới/cập nhật đăng ký
                #region Thêm mới/cập nhật tài liệu
                List<TaiLieuDinhKemCls> newTaiLieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                List<TaiLieuDinhKemCls> oldTaiLieus = CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ORenderInfo, new TaiLieuDinhKemFilterCls() { DOCUMENT_ID = ThemMoiDeCuong.ID }).ToList();
                foreach (var oldTaiLieu in oldTaiLieus)
                {
                    bool isExists = false;
                    foreach (var newThuocADR in newTaiLieus)
                    {
                        if (newThuocADR.ID == oldTaiLieu.ID)//cập nhật
                        {
                            oldTaiLieu.DOCUMENT_ID = ThemMoiDeCuong.ID;
                            oldTaiLieu.TENTAILIEU = newThuocADR.TENTAILIEU;
                            oldTaiLieu.TENHIENTHI = newThuocADR.TENHIENTHI;
                            oldTaiLieu.DUONGDAN = newThuocADR.DUONGDAN;
                            oldTaiLieu.GHICHU = newThuocADR.GHICHU;
                            oldTaiLieu.NGUOITAO_ID = newThuocADR.NGUOITAO_ID;
                            oldTaiLieu.NGAYTAO = newThuocADR.NGAYTAO;
                            CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Save(ORenderInfo, oldTaiLieu.ID, oldTaiLieu);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Delete(ORenderInfo, oldTaiLieu.ID);
                    }
                }
                var addTaiLieus = newTaiLieus.Where(o => !oldTaiLieus.Any(old => old.ID == o.ID));
                foreach (var addTaiLieu in addTaiLieus)//Thêm mới
                {
                    addTaiLieu.DOCUMENT_ID = ThemMoiDeCuong.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Add(ORenderInfo, addTaiLieu);
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
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string ThemMoiDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Delete(ORenderInfo, ThemMoiDeCuongId);
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeCuongCls ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongId);
                if (string.IsNullOrEmpty(ThemMoiDeCuongId))
                {                  
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn cần điền đầy đủ thông tin trước khi chuyển duyệt.");
                    return RetAjaxOut;
                }
                ThemMoiDeCuong.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Save(ORenderInfo, ThemMoiDeCuong.ID, ThemMoiDeCuong);
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DeCuongCls ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongId);
                if (ThemMoiDeCuong != null)
                    return DeCuongParser.sColorTrangThai[ThemMoiDeCuong.TRANGTHAI.Value];
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if(!string.IsNullOrEmpty(ThemMoiDeCuongId))
                {
                    DeCuongCls ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongId);
                    if (ThemMoiDeCuong != null)
                        RetAjaxOut.RetExtraParam1 = ThemMoiDeCuong.TRANGTHAI.ToString();
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
        public class QCGiaoDienDeCuongNew
        {
            public string CHUNHIEM_ID { get; set; }
            public string CAPDETAI { get; set; }

        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ShowTTDeTaiDangKy(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QCGiaoDienDeCuongNew qd = new QCGiaoDienDeCuongNew();
                if (!string.IsNullOrEmpty(Id))
                {
                    DangKyDeTaiCls DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, Id);

                    if (DangKyDeTai != null)
                    {
                        var chunhiemdetai = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DangKyDeTai.CHUNHIEM_ID);
                        if (chunhiemdetai != null)
                        {
                            qd.CHUNHIEM_ID += string.Format("<option value={0}>{1}</option>\r\n", chunhiemdetai.OwnerUserId, chunhiemdetai.FullName);
                        }
                        foreach (var ldt in DangKyDeTaiParser.CapDeTais)
                            qd.CAPDETAI += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, DangKyDeTai.CAPDETAI == ldt.Key ? "selected" : null, ldt.Value);
                    }
                }
                RetAjaxOut.RetObject = qd;
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
        public static AjaxOut SaveTaiLieu(RenderInfoCls ORenderInfo, string DeCuongId, string GhiChu, string xmlUploadFileResult)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                MultiUploadHandlerResultCls multiUploadHandlerResult = MultiUploadHandlerResultParser.ParseFromXml(xmlUploadFileResult);
                if (multiUploadHandlerResult.Error)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = multiUploadHandlerResult.InfoMessage;
                    return RetAjaxOut;
                }
                UploadHandlerResultCls[] uploadHandlerResults = UploadHandlerResultParser.ParseFromXmls(xmlUploadFileResult);
                foreach (UploadHandlerResultCls uploadHandlerResult in uploadHandlerResults)
                {
                    FileInfo file = new FileInfo(uploadHandlerResult.SaveFile);
                    WebSession.CheckSessionTimeOut(ORenderInfo);
                    List<TaiLieuDinhKemCls> TaiLieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                    if (TaiLieus.Any(o => o.GHICHU == GhiChu))
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Tài liệu này đã có trong danh sách.");
                        return RetAjaxOut;
                    }
                    TaiLieus.Add(new TaiLieuDinhKemCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        TENTAILIEU = file.Name,
                        TENHIENTHI = uploadHandlerResult.UploadFileName,
                        NGAYTAO = DateTime.Now,
                        NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId,
                    });
                }
                RetAjaxOut.HtmlContent = DrawTaiLieu(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteTaiLieu(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<TaiLieuDinhKemCls> tailieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                TaiLieuDinhKemCls tailieu = tailieus.FirstOrDefault(o => o.ID == Id);
                tailieus.Remove(tailieu);
                RetAjaxOut.HtmlContent = DrawTaiLieu(ORenderInfo).HtmlContent;
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
        public static string GetUploadHandler(SiteParam OSiteParam, string SessionId, string UserId, string LoginName, string DestinationPathKey = null)
        {
            string link = WebConfig.GetWebConfig("UploadHandlerUrl") + "?SiteId=" + OSiteParam.SiteId + "&SessionId=" + SessionId + "&UserId=" + UserId + "&LoginName=" + LoginName;
            if (!string.IsNullOrEmpty(DestinationPathKey))
                link += "&DestinationPathKey=" + DestinationPathKey;
            return link;
        }
    }
}
