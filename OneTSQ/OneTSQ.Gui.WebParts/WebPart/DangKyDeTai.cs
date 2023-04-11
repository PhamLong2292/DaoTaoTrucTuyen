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
    public class DangKyDeTai : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DangKyDeTai";
        public override string WebPartTitle { get { return "Đăng ký đề tài sáng kiến"; } }
        public override string Description { get { return "Đăng ký đề tài sáng kiến"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, DangKyDeTai.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DangKyDeTai), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string SessionId = System.Guid.NewGuid().ToString();
                string DangKyDeTaiId = WebEnvironments.Request("id");             
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnGuiDangKy' title='Gửi đăng ký' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Gửi đăng ký") + "' onclick='javascript:GuiDangKy()' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnDuyetDangKy' title='Duyệt đăng ký' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Duyệt đăng ký") + "' onclick='javascript:DuyetDangKy()' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnTuChoi' title='Từ chối' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Từ chối") + "' onclick='javascript:TuChoiDuyet()' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnThuHoiDuyet' title='Thu hồi duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi duyệt") + "' onclick='javascript:ThuHoiDuyet()' style='float:left; margin-left: 20px; display:none;'>\r\n" +
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
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divDrawDangKyDeTai'> \r\n" +                   
                                            DrawDangKyDeTai(ORenderInfo, null, DangKyDeTaiId).HtmlContent +               
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
                    "   var DangKyDeTaiId = '" + DangKyDeTaiId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucVuService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh, nghề nghiệp") + "');\r\n" +
                    "     CallInitSelect2('cbbNguoiDuyet', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người duyệt") + "');\r\n" +
                    "     CallInitSelect2('cbbChuNhiemDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + "');\r\n" +
                    "     CallInitSelect2('cbbCongTacVien', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Cộng tác viên đề tài") + "');\r\n" +
                    "     ShowTrangThai();\r\n" +
                    "     CheckTrangThai();\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.DangKyDeTai.CheckTrangThai(RenderInfo, DangKyDeTaiId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "   if(DangKyDeTaiId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return DangKyDeTaiId;\r\n" +  
                    "}\r\n" +
                #endregion 
                #region Show trạng thái 
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.DangKyDeTai.ShowTrangThai(RenderInfo, DangKyDeTaiId).value);\r\n" +        
                    "}\r\n" +
                    "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #endregion
                #region check trạng thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.DangKyDeTai.CheckTrangThai(RenderInfo, DangKyDeTaiId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawDangKyDeTai :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnGuiDangKy').hide();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnDuyetDangKy').show();\r\n" +
                    "           $('#btnTuChoi').show();\r\n" +
                    "       }\r\n" +
                    "       else if(AjaxOut.RetExtraParam1 == 2) \r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawDangKyDeTai :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnGuiDangKy').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyetDangKy').hide();\r\n" +
                    "           $('#btnTuChoi').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       }\r\n" +
                    "       else if(AjaxOut.RetExtraParam1 == 3) \r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawDangKyDeTai :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnGuiDangKy').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyetDangKy').hide();\r\n" +
                    "           $('#btnTuChoi').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divDrawDangKyDeTai :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyetDangKy').hide();\r\n" +
                    "           $('#btnTuChoi').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnGuiDangKy').show();\r\n" +       
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
                #region Thu hồi về mới
                    "   function ThuHoi(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi đăng ký này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DangKyDeTai.UpdateTrangThai(RenderInfo, DangKyDeTaiId, " + (int)DangKyDeTaiCls.eTrangThai.Moi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   $('#btnThuHoiDuyet').hide();\r\n" +
                    "                   $('#btnDuyetDangKy').hide();\r\n" +
                    "                   $('#btnTuChoi').hide();\r\n" +
                    "                   $('#btnLuu').show();\r\n" +
                    "                   $('#btnGuiDangKy').show();\r\n" +
                    "                   AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Thu hồi về duyệt
                    "   function ThuHoiDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi duyệt đăng ký này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DangKyDeTai.UpdateTrangThai(RenderInfo, DangKyDeTaiId, " + (int)DangKyDeTaiCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $('#btnThuHoi').show();\r\n" +
                    "                   $('#btnDuyetDangKy').show();\r\n" +
                    "                   $('#btnTuChoi').show();\r\n" +
                    "                   $('#btnLuu').hide();\r\n" +
                    "                   $('#btnGuiDangKy').hide();\r\n" +
                    "                   AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Thu hồi duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Gửi đăng ký
                    "   function GuiDangKy(){\r\n" +
                    "       SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DangKyDeTai.UpdateTrangThai(RenderInfo, DangKyDeTaiId, " + (int)DangKyDeTaiCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').hide();\r\n" +
                    "           $('#btnGuiDangKy').hide();\r\n" +             
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnDuyetDangKy').show();\r\n" +
                    "           $('#btnTuChoi').show();\r\n" +    
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã gửi đăng ký!") + "');\r\n" +
                    "           AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Duyệt đăng ký
                    "   function DuyetDangKy(){\r\n" +
                    "       SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DangKyDeTai.UpdateTrangThai(RenderInfo, DangKyDeTaiId, " + (int)DangKyDeTaiCls.eTrangThai.DaXetDuyet + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnGuiDangKy').hide();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnDuyetDangKy').hide();\r\n" +
                    "           $('#btnTuChoi').hide();\r\n" +
                    "           $('#btnThuHoiDuyet').show();\r\n" +
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã gửi đăng ký!") + "');\r\n" +
                    "           AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Từ chối duyệt
                    "   function TuChoiDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn từ chối duyệt đăng ký này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DangKyDeTai.UpdateTrangThai(RenderInfo, DangKyDeTaiId, " + (int)DangKyDeTaiCls.eTrangThai.TuChoi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   $('#btnDuyetDangKy').hide();\r\n" +
                    "                   $('#btnTuChoi').hide();\r\n" +
                    "                   $('#btnLuu').hide();\r\n" +
                    "                   $('#btnGuiDangKy').hide();\r\n" +
                    "                   $('#btnThuHoiDuyet').show();\r\n" +
                    "                   AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Thu hồi từ chối', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "     LoaiHinh = document.getElementById('cbbLoaiHinh').value;\r\n" +
                    "     Ma = document.getElementById('txtMa').value;\r\n" +
                    "     TenDeTai = document.getElementById('txtTenDeTai').value;\r\n" +
                    "     CapDeTai = document.getElementById('cbbCapDeTai').value;\r\n" +
                    "     ChuNhiemDeTai = document.getElementById('cbbChuNhiemDeTai').value;\r\n" +
                    "     ChucDanh = document.getElementById('cbbChucDanh').value;\r\n" +
                    "     DienThoai = document.getElementById('txtDienThoai').value;\r\n" +
                    "     Email = document.getElementById('txtEmail').value;\r\n" +
                    "     ThoiGianDK = document.getElementById('dtThoiGianDangKy').value;\r\n" +
                    "     ThoiGianDuyet = document.getElementById('dtThoiGianDuyet').value;\r\n" +
                    "     NguoiDuyet = document.getElementById('cbbNguoiDuyet').value;\r\n" +
                    "     CongTacVien = $('#cbbCongTacVien').val();\r\n" +
                    "     KinhPhi = document.getElementById('txtKinhPhi').value;\r\n" +
                    "     YNghiaThucTien = document.getElementById('txtYNghiaThucTien').value;\r\n" +
                    "     YNghiaKhoaHoc = document.getElementById('txtYNghiaKhoaHoc').value;\r\n" +
                    "     TinhKhaThi = document.getElementById('txtTinhKhaThi').value;\r\n" +
                    "     MucTieu = document.getElementById('txtMucTieu').value;\r\n" +
                    "     NoiDungChuYeu = document.getElementById('txtNoiDungChuYeu').value;\r\n" +
                    "     PhuongPhapNghienCuu = document.getElementById('txtPhuongPhapNghienCuu').value;\r\n" +
                    "     KetQuaDatDuoc = document.getElementById('txtKetQuaDatDuoc').value;\r\n" +
                    "     KhaNang = document.getElementById('txtKhaNang').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DangKyDeTai.SaveThongTin(RenderInfo, DangKyDeTaiId, LoaiHinh, Ma, TenDeTai, CapDeTai, ChuNhiemDeTai, ChucDanh, DienThoai, Email, ThoiGianDK, ThoiGianDuyet, NguoiDuyet, CongTacVien, KinhPhi, YNghiaThucTien, YNghiaKhoaHoc, TinhKhaThi, MucTieu, NoiDungChuYeu, PhuongPhapNghienCuu, KetQuaDatDuoc, KhaNang).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdDangKyDeTaiId').value = DangKyDeTaiId = AjaxOut.RetExtraParam1;\r\n" +         
                    "           AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(DangKyDeTaiId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 
                #region Xóa Thông tin đăng ký
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DangKyDeTai.DeleteThongTin(RenderInfo, DangKyDeTaiId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(DangKyDeTaiId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion 
                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "       maso = sender.value;\r\n" +
                    "       if( maso != '')\r\n" +//Nếu đăng ký chưa được tạo thì load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DangKyDeTai.DrawDangKyDeTai(RenderInfo, maso, DangKyDeTaiId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divDrawDangKyDeTai').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "           }\r\n" +       
                    "           CheckTrangThai();\r\n" +
                    "           CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucVuService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh, nghề nghiệp") + "');\r\n" +
                    "           CallInitSelect2('cbbNguoiDuyet', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người duyệt") + "');\r\n" +
                    "           CallInitSelect2('cbbChuNhiemDeTai', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + "');\r\n" +
                    "           CallInitSelect2('cbbCongTacVien', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Cộng tác viên đề tài") + "');\r\n" +
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
                    "           DangKyDeTaiId = document.getElementById('hdDangKyDeTaiId').value;\r\n" +
                    "           TenTaiLieu = document.getElementById('txtTaiLieuDinhKem').value;\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DangKyDeTai.SaveTaiLieu(RenderInfo, DangKyDeTaiId, TenTaiLieu, xmlResult).value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DangKyDeTai.DeleteTaiLieu(RenderInfo, Id).value;\r\n" +
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
        public static AjaxOut DrawDangKyDeTai(RenderInfoCls ORenderInfo, string maso, string DangKyDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DangKyDeTaiCls DangKyDeTai = null;
                if (!string.IsNullOrEmpty(DangKyDeTaiId))
                    DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DangKyDeTaiId);
                if (!string.IsNullOrEmpty(maso))
                    DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, maso);
                if (DangKyDeTai == null)
                {
                    DangKyDeTai = new DangKyDeTaiCls();
                    if (string.IsNullOrEmpty(maso))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        DangKyDeTai.MA = boDemValueMaHV.RetExtraParam2;
                    }
                    else DangKyDeTai.MA = maso;
                }
     
                List<TaiLieuDinhKemCls> OTaiLieuDK = string.IsNullOrEmpty(DangKyDeTai.ID) ? new List<TaiLieuDinhKemCls>() : CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ORenderInfo, new TaiLieuDinhKemFilterCls() { DOCUMENT_ID = DangKyDeTai.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "TaiLieuDinhKems", OTaiLieuDK);
                string cbbChucDanh = "";
                if (!string.IsNullOrEmpty(DangKyDeTai.CHUCDANH_ID))
                {
                    var chucdanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), DangKyDeTai.CHUCDANH_ID);
                    if (chucdanh != null)
                    {
                        cbbChucDanh += string.Format("<option value={0}>{1}</option>\r\n", chucdanh.Id, chucdanh.Ten);
                    }
                }
                string cbbNguoiDuyet = "";
                if (!string.IsNullOrEmpty(DangKyDeTai.CHUCDANH_ID))
                {
                    var nguoiduyet = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DangKyDeTai.NGUOIDUYET_ID);
                    if (nguoiduyet != null)
                    {
                        cbbNguoiDuyet += string.Format("<option value={0}>{1}</option>\r\n", nguoiduyet.OwnerUserId, nguoiduyet.FullName);
                    }
                }
                string cbbChuNhiemDeTai = "";
                if (!string.IsNullOrEmpty(DangKyDeTai.CHUCDANH_ID))
                {
                    var chunhiem = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DangKyDeTai.CHUNHIEM_ID);
                    if (chunhiem != null)
                    {
                        cbbChuNhiemDeTai += string.Format("<option value={0}>{1}</option>\r\n", chunhiem.OwnerUserId, chunhiem.FullName);
                    }
                }
                string cbbCongTacVien = "";
                string DangKyDeTaiID = "0";
                if(!string.IsNullOrEmpty(DangKyDeTai.ID))
                {
                     DangKyDeTaiID = DangKyDeTai.ID;
                }
                CongTacVienDeTaiCls[] ConTacVienDeTais = CallBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Reading(ORenderInfo, new CongTacVienDeTaiFilterCls() { DANGKYDETAI_ID = DangKyDeTaiID});
                if(ConTacVienDeTais != null)
                {
                    foreach (CongTacVienDeTaiCls ConTacVienDeTai in ConTacVienDeTais)
                    {
                        var congtacvien = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, ConTacVienDeTai.NGUOIDUNG_ID);
                        if (congtacvien != null)
                            cbbCongTacVien += string.Format(" <option value ={0} selected>{1}</option>\r\n", congtacvien.OwnerUserId, congtacvien.FullName);
                    }
                }
                string cbbLoaiDaoTao = "<select class='form-control' id='cbbLoaiHinh' style='font-size: 14px;'>\r\n";
                foreach (var ldt in DangKyDeTaiParser.LoaiHinhs)
                    cbbLoaiDaoTao += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, DangKyDeTai.LOAIHINH == ldt.Key ? "selected" : null, ldt.Value);
                cbbLoaiDaoTao += "</select>\r\n";

                string cbbCapDeTai = "<select class='form-control' id='cbbCapDeTai' style='font-size: 14px;'>\r\n";
                foreach (var ldt in DangKyDeTaiParser.CapDeTais)
                    cbbCapDeTai += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, DangKyDeTai.CAPDETAI == ldt.Key ? "selected" : null, ldt.Value);
                cbbCapDeTai += "</select>\r\n";


                string Html =
                "               <input id='hdDangKyDeTaiId' type='hidden' value='"+ DangKyDeTai.ID +"'>\r\n" +
                "               <div class=\"row\" id='divDangKyDeTaiId'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Loại hình: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                                                         cbbLoaiDaoTao +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Mã: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtMa' type='text' style='z-index: 0;' value='" + DangKyDeTai.MA + "' onchange='txtMa_onchange(this)' class='form-control valueForm' required " + (string.IsNullOrEmpty(DangKyDeTaiId) ? null : "disabled=true") + ">\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề tài, sáng kiến: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtTenDeTai' type='text' style='z-index: 0;' required value='"+ DangKyDeTai.TENDETAI +"' class='form-control valueForm' required >\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài: ") +
                                                       cbbCapDeTai +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +             
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
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
                                                       WebLanguage.GetLanguage(OSiteParam, "Chức danh nghề nghiệp: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbChucDanh' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "' required>\r\n" +
                                                            cbbChucDanh +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                                  <div class=\"col-md-1\"  style ='max-width:12.3%; position: relative; width:100%; padding-right:15px; padding-left:15px;'>\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Điện thoại:") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                          <input id='txtDienThoai' type='text' style='z-index: 0;' onkeypress='CheckCurrency(event);' maxlength='10' value='"+ DangKyDeTai.DIENTHOAI +"' class='form-control valueForm' required>\r\n" +
                "                                      </div> \r\n" +
                "                                  </div>\r\n" +
                "                                  <div class=\"col-md-1\" style ='max-width:12.3%; position: relative; width:100%; padding-right:15px; padding-left:15px;'>\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Email:") +
                "                                          <input id='txtEmail' type='text' style='z-index: 0;' value='" + DangKyDeTai.EMAIL + "' value='' class='form-control valueForm'>\r\n" +
                "                                      </div> \r\n" +
                "                                  </div>\r\n" +
                "                                  <div class=\"col-md-1\" style ='max-width:12.6%; position: relative; width:100%; padding-right:15px; padding-left:15px;'>\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian đăng ký:") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                          <input id='dtThoiGianDangKy' required type='text' style='z-index: 0;' value='" + (DangKyDeTai.THOIGIANDANGKY == null ? null : DangKyDeTai.THOIGIANDANGKY.Value.ToString(" dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +
                "                                  <div class=\"col-md-1\" style ='max-width:12.7%; position: relative; width:100%; padding-right:15px; padding-left:15px;'>\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian duyệt đăng ký:") +
                "                                          <input id='dtThoiGianDuyet' type='text' style='z-index: 0;' value='" + (DangKyDeTai.THOIGIANDUYETDK == null ? null : DangKyDeTai.THOIGIANDUYETDK.Value.ToString(" dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Người duyệt: ") +
                "                                       <select id = 'cbbNguoiDuyet' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người duyệt") + "'>\r\n" +
                                                            cbbNguoiDuyet +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +             
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-8\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Cộng tác viên: ") +
                "                                        <select class='form-control' id='cbbCongTacVien' multiple style='width:100%;font-size: 14px;'>\r\n" +
                                                            cbbCongTacVien +
                "                                       </select>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-4\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Kinh phí dự kiến: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtKinhPhi' type='text' style='z-index: 0;' class='form-control valueForm' required onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' value='" + (DangKyDeTai.KINHPHIDUKIEN.ToString() == null ? null : DangKyDeTai.KINHPHIDUKIEN.ToString("#,##0.00", new CultureInfo("en-US"))) + "'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +            
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Giải trình về tính cấp thiết:") + "</label><br>\r\n" +                                  
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Ý nghĩa thực tiễn:") + "</label><br>\r\n" +
                "                                       <textarea  id='txtYNghiaThucTien' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.YNGHIATHUCTIEN + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +                                    
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Ý nghĩa khoa học:") + "</label><br>\r\n" +
                "                                       <textarea  id='txtYNghiaKhoaHoc' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.YNGHIAKHOAHOC + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n"+
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Tính khả thi") + "</label><br>\r\n" +
                "                                       <textarea  id='txtTinhKhaThi' type='text' style='z-index: 0; height: 100px;' value='" + DangKyDeTai.TINHKHATHI + "' class='form-control valueForm'></textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +                                                  
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Mục tiêu") + "</label><br>\r\n" +
                "                                       <textarea  id='txtMucTieu' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.MUCTIEU + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +         
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung chủ yếu") + "</label><br>\r\n" +
                "                                       <textarea  id='txtNoiDungChuYeu' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.NOIDUNGCHUYEU + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +                                                     
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Phương pháp nghiên cứu") + "</label><br>\r\n" +
                "                                       <textarea  id='txtPhuongPhapNghienCuu' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.PHUONGPHAPNGHIENCUU + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Dự kiến kết quả đạt được") + "</label><br>\r\n" +
                "                                       <textarea  id='txtKetQuaDatDuoc' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.DUKIENKETQUA + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +                                
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Khả năng và địa chỉ áp dụng") + "</label><br>\r\n" +
                "                                       <textarea  id='txtKhaNang' type='text' style='z-index: 0; height: 100px;' class='form-control valueForm'>" + DangKyDeTai.KHANANGDIACHIAPDUNG + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row' style ='margin-top: 20px;'>\r\n" +              
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +                                       
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu đính kèm:") + "</label><br>\r\n" +
                "                                        <input id='txtTaiLieuDinhKem' type='text' style='z-index: 0;' value='' class='form-control valueForm'>\r\n" +
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
                "                                       <button type=\"button\" onclick='javascript:SaveTaiLieu();' class=\"btn btn-sm btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +

                "                           <div class='row' style ='margin-top: 10px;'>\r\n" +
                "                               <div class=\"col-md-12\" id='TabTaiLieu'>\r\n" +
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
                         "          <th width=80 class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
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
                        if(nguoitaos != null)
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
                        "           <td style='text-align: center; vertical-align: middle;'>" +  aTag + "</td> \r\n" +
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
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "DangKyDeTai", FieldName = "maso" }).FirstOrDefault();
            if (boMa != null && !string.IsNullOrEmpty(boMa.BIEUTHUC))
            {
                while (maHV == null || CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, maHV) != null)
                {
                    //if (maHV != null)
                    //{
                    //    var boDem = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().CreateModel(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC));
                    //    if (boDem != null)
                    //    {
                    //        CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().UpdateStatus(ORenderInfo, boDem.ID, int.Parse(boDemValue), 1);
                    //    }
                    //}
                    boDemValue = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().GetValue(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC), Model.Common.GetValueFormat(boMa.BIEUTHUC));
                    maHV = Model.Common.GetDisplayPart(boMa.BIEUTHUC) + boDemValue;
                }
            }
            RetAjaxOut.RetExtraParam1 = boDemValue;
            RetAjaxOut.RetExtraParam2 = maHV;
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string DangKyDeTaiId, int LoaiHinh, string Ma, string TenDeTai, int CapDeTai, string ChuNhiemDeTai, string ChucDanh, string DienThoai, string Email, string ThoiGianDK, string ThoiGianDuyet, string NguoiDuyet, string[] CongTacVien, string KinhPhi, string YNghiaThucTien, string YNghiaKhoaHoc, string TinhKhaThi, string MucTieu, string NoiDungChuYeu, string PhuongPhapNghienCuu, string KetQuaDatDuoc, string KhaNang)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DangKyDeTaiCls DangKyDeTai = new DangKyDeTaiCls();
                if (!string.IsNullOrEmpty(DangKyDeTaiId))
                {
                    DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DangKyDeTaiId);
                }  
                else
                {
                    DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, Ma);
                    if (DangKyDeTai == null)
                    {
                        DangKyDeTai = new DangKyDeTaiCls() { MA = Ma };
                    }
                }       
                if(!string.IsNullOrEmpty(DangKyDeTai.ID))
                {
                    CallBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Delete(ORenderInfo, DangKyDeTai.ID);
                }      
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(DangKyDeTai.ID))
                {
                    DangKyDeTai.ID = DangKyDeTaiId = System.Guid.NewGuid().ToString();
                    DangKyDeTai.LOAIHINH = LoaiHinh;
                    DangKyDeTai.MA = Ma;              
                    DangKyDeTai.TENDETAI = TenDeTai;
                    DangKyDeTai.CAPDETAI = CapDeTai;
                    DangKyDeTai.CHUNHIEM_ID = ChuNhiemDeTai;
                    DangKyDeTai.CHUCDANH_ID = ChucDanh;
                    DangKyDeTai.DIENTHOAI = DienThoai;
                    DangKyDeTai.EMAIL = Email;
                    DangKyDeTai.THOIGIANDANGKY = string.IsNullOrWhiteSpace(ThoiGianDK) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDK, "dd/MM/yyyy", null);
                    DangKyDeTai.THOIGIANDUYETDK = string.IsNullOrWhiteSpace(ThoiGianDuyet) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDuyet, "dd/MM/yyyy", null);
                    DangKyDeTai.NGUOIDUYET_ID = NguoiDuyet;
                    DangKyDeTai.KINHPHIDUKIEN = decimal.Parse(KinhPhi);
                    DangKyDeTai.YNGHIATHUCTIEN = YNghiaThucTien;
                    DangKyDeTai.YNGHIAKHOAHOC = YNghiaKhoaHoc;
                    DangKyDeTai.TINHKHATHI = TinhKhaThi;
                    DangKyDeTai.MUCTIEU = MucTieu;
                    DangKyDeTai.NOIDUNGCHUYEU = NoiDungChuYeu;
                    DangKyDeTai.PHUONGPHAPNGHIENCUU = PhuongPhapNghienCuu;
                    DangKyDeTai.DUKIENKETQUA = KetQuaDatDuoc;
                    DangKyDeTai.KHANANGDIACHIAPDUNG = KhaNang;        
                    DangKyDeTai.TRANGTHAI = (int)DangKyDeTaiCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Add(ORenderInfo, DangKyDeTai);
                    RetAjaxOut.RetExtraParam1 = DangKyDeTai.ID;                
                }
                else
                {
                    DangKyDeTai.LOAIHINH = LoaiHinh;
                    DangKyDeTai.MA = Ma;
                    DangKyDeTai.TENDETAI = TenDeTai;
                    DangKyDeTai.CAPDETAI = CapDeTai;
                    DangKyDeTai.CHUNHIEM_ID = ChuNhiemDeTai;
                    DangKyDeTai.CHUCDANH_ID = ChucDanh;
                    DangKyDeTai.DIENTHOAI = DienThoai;
                    DangKyDeTai.EMAIL = Email;
                    DangKyDeTai.THOIGIANDANGKY = string.IsNullOrWhiteSpace(ThoiGianDK) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDK, "dd/MM/yyyy", null);
                    DangKyDeTai.THOIGIANDUYETDK = string.IsNullOrWhiteSpace(ThoiGianDuyet) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDuyet, "dd/MM/yyyy", null);
                    DangKyDeTai.NGUOIDUYET_ID = NguoiDuyet;
                    DangKyDeTai.KINHPHIDUKIEN = decimal.Parse(KinhPhi);
                    DangKyDeTai.YNGHIATHUCTIEN = YNghiaThucTien;
                    DangKyDeTai.YNGHIAKHOAHOC = YNghiaKhoaHoc;
                    DangKyDeTai.TINHKHATHI = TinhKhaThi;
                    DangKyDeTai.MUCTIEU = MucTieu;
                    DangKyDeTai.NOIDUNGCHUYEU = NoiDungChuYeu;
                    DangKyDeTai.PHUONGPHAPNGHIENCUU = PhuongPhapNghienCuu;
                    DangKyDeTai.DUKIENKETQUA = KetQuaDatDuoc;
                    DangKyDeTai.KHANANGDIACHIAPDUNG = KhaNang;
                    CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Save(ORenderInfo, DangKyDeTai.ID, DangKyDeTai);                
                }
                #endregion
                #region Thêm/ cập nhật cộng tác viên
                if(CongTacVien != null && CongTacVien.Length > 0)
                {
                    foreach (var item in CongTacVien)
                    {
                        CongTacVienDeTaiCls CongTacVienDeTai = new CongTacVienDeTaiCls();
                        CongTacVienDeTai.ID = System.Guid.NewGuid().ToString();
                        CongTacVienDeTai.DANGKYDETAI_ID = DangKyDeTai.ID;
                        CongTacVienDeTai.NGUOIDUNG_ID = item;
                        CallBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Add(ORenderInfo, CongTacVienDeTai);
                    }
                }
                #endregion
                #region Thêm mới/cập nhật tài liệu
                List<TaiLieuDinhKemCls> newTaiLieus = WebSessionUtility.GetSession(OSiteParam, "TaiLieuDinhKems") as List<TaiLieuDinhKemCls>;
                List<TaiLieuDinhKemCls> oldTaiLieus = CallBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ORenderInfo, new TaiLieuDinhKemFilterCls() { DOCUMENT_ID = DangKyDeTai.ID }).ToList();
                foreach (var oldTaiLieu in oldTaiLieus)
                {
                    bool isExists = false;
                    foreach (var newTaiLieu in newTaiLieus)
                    {
                        if (newTaiLieu.ID == oldTaiLieu.ID)//cập nhật
                        {
                            oldTaiLieu.DOCUMENT_ID = DangKyDeTai.ID;
                            oldTaiLieu.TENTAILIEU = newTaiLieu.TENTAILIEU;
                            oldTaiLieu.TENHIENTHI = newTaiLieu.TENHIENTHI;
                            oldTaiLieu.GHICHU = newTaiLieu.GHICHU;
                            oldTaiLieu.NGUOITAO_ID = newTaiLieu.NGUOITAO_ID;
                            oldTaiLieu.NGAYTAO = newTaiLieu.NGAYTAO;
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
                    addTaiLieu.DOCUMENT_ID = DangKyDeTai.ID;
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
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Delete(ORenderInfo, DangKyDeTaiId);
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string DangKyDeTaiId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DangKyDeTaiCls DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DangKyDeTaiId);
                if (DangKyDeTai == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn cần điền đầy đủ thông tin trước khi gửi đăng ký.");
                    return RetAjaxOut;
                }
                DangKyDeTai.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Save(ORenderInfo, DangKyDeTai.ID, DangKyDeTai);
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DangKyDeTaiCls DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DangKyDeTaiId);
                if (DangKyDeTai != null)
                    return DangKyDeTaiParser.sColorTrangThai[DangKyDeTai.TRANGTHAI.Value];
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DangKyDeTaiCls DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DangKyDeTaiId);
                if (DangKyDeTai != null)
                    RetAjaxOut.RetExtraParam1 = DangKyDeTai.TRANGTHAI.ToString();
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
        public static AjaxOut SaveTaiLieu(RenderInfoCls ORenderInfo, string DangKyID, string TenTaiLieu, string xmlUploadFileResult)
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
                    if (TaiLieus.Any(o => o.GHICHU == TenTaiLieu))
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Tài liệu này đã có trong danh sách.");
                        return RetAjaxOut;
                    }
                    TaiLieus.Add(new TaiLieuDinhKemCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        GHICHU = TenTaiLieu,
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
