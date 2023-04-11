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
    public class LichXetDuyetDeCuong : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "LichXetDuyetDeCuong";
        public override string WebPartTitle { get { return "Lập lịch xét duyệt đề cương"; } }
        public override string Description { get { return "Lập lịch xét duyệt đề cương"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, LichXetDuyetDeCuong.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(LichXetDuyetDeCuong), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string LichXetDuyetDeCuongId = WebEnvironments.Request("id");
                //LichXetDuyetDeCuongCls LichXetDuyetDeCuongId = string.IsNullOrEmpty(LichXetDuyetDeCuongId) ? new LichXetDuyetDeCuongCls() : CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetDeCuongProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuongId);            
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnLapLich' title='Lập lịch' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lập lịch") + "' onclick='LapLich();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnXetDuyet' title='Xét duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xét duyệt") + "' onclick='OpenXetDuyet();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
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
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divlapLichXetDuyet'> \r\n" +                   
                                            ThongTinDangKy(ORenderInfo, LichXetDuyetDeCuongId).HtmlContent +               
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
                    "   var LichXetDuyetDeCuongId='"+ LichXetDuyetDeCuongId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "      format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "     }); \r\n" +
                    "     $('.datepicker1').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +            
                    "     CallInitSelect2('cbbNguoiDuyet', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người duyệt") + "');\r\n" +
                    "     CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                    "     CallInitSelect2('cbbChuTri', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chủ trì") + "');\r\n" +
                    "     CallInitSelect2('cbbThuKy', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Thư ký") + "');\r\n" +
                    "     CallInitSelect2('cbbNguoiDung', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Họ và tên") + "');\r\n" +
                    "     CallInitSelect2('cbbTenDeCuong', '" + WebEnvironments.GetRemoteProcessDataUrl(DeCuongService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đề cương") + "');\r\n" +
                    "     CallInitSelect2('cbbChucVu', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucVuService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + "');\r\n" +
                    "     CheckTrangThai();\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.CheckTrangThai(RenderInfo, LichXetDuyetDeCuongId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   LichXetDuyetDeCuongId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "   if(LichXetDuyetDeCuongId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return LichXetDuyetDeCuongId;\r\n" +  
                    "}\r\n" +
                #endregion
                #region hiện thị giao diện theo trạng thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    lapLichXetDuyetId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.CheckTrangThai(RenderInfo, lapLichXetDuyetId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 3)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divlapLichXetDuyet :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnLapLich').hide();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnXetDuyet').show();\r\n" +
                    "       }\r\n" +                 
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divlapLichXetDuyet :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnLapLich').show();\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnXetDuyet').hide();\r\n" +
                    "       }\r\n" +
                    "}\r\n" +
                #endregion
                #region Refresh form về trạng thái mới
                    "function Clear()\r\n" +
                    "{\r\n" +
                    "   window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                    "   CallInitSelect2('cbbnguoilap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "}\r\n" +
                #endregion
                #region Thu hồi
                    "   function ThuHoi(){\r\n" +
                    "       LichXetDuyetDeCuongId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.CheckTrangThai(RenderInfo, LichXetDuyetDeCuongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam2 == 4)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Đã có đề cương được hoàn tất, không thể thu hồi!") + "');\r\n" +
                    "           return true;\r\n" +
                    "       }\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.UpdateTrangThai(RenderInfo, LichXetDuyetDeCuongId, " + (int)LichXetDuyetCls.eTrangThai.Moi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                    "                   $('#btnThuHoi').hide();\r\n" +
                    "                   $('#btnXetDuyet').hide();\r\n" +
                    "                   $('#btnLuu').show();\r\n" +
                    "                   $('#btnLapLich').show();\r\n" +
                    "                   AddHistory(LichXetDuyetDeCuongId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Lập lịch
                    "   function LapLich(){\r\n" +
                    "       SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       lapLichXetDuyetId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.UpdateTrangThai(RenderInfo, LichXetDuyetDeCuongId, " + (int)LichXetDuyetCls.eTrangThai.DaLapLich + ").value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnLapLich').hide();\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnXetDuyet').show();\r\n" +
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã lập lịch!") + "');\r\n" +
                    "           AddHistory(lapLichXetDuyetId, '" + WebPartTitle + "', 'Đã lập lịch', 'Tác vụ form');\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xét duyệt
                     "   function OpenXetDuyet(){\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               lapLichXetDuyetId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.GetCaBenhUrl(RenderInfo, lapLichXetDuyetId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "               }\r\n" +
                    "   }\r\n" +               
                #endregion              
                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     LichXetDuyetDeCuongId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
                    "     NguoiLap = document.getElementById('cbbNguoiLap').value;\r\n" +
                    "     ThoiGianLap = document.getElementById('dtThoiGianLap').value;\r\n" +
                    "     NguoiDuyet = document.getElementById('cbbNguoiDuyet').value;\r\n" +
                    "     ThoiGianDuyet = document.getElementById('dtThoiGianDuyet').value;\r\n" +
                    "     ChuTri = document.getElementById('cbbChuTri').value;\r\n" +
                    "     ThuKy = document.getElementById('cbbThuKy').value;\r\n" +
                    "     ThoiGianBatDau = document.getElementById('dtThoiGianBatDau').value;\r\n" +
                    "     ThoiGianKetThuc = document.getElementById('dtThoiGianKetThuc').value;\r\n" +
                    "     DiaDiem = document.getElementById('txtDiaDiem').value;\r\n" +
                    "     GhiChu = document.getElementById('txtGhiChu').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.SaveThongTin(RenderInfo, LichXetDuyetDeCuongId, NguoiLap, ThoiGianLap, NguoiDuyet, ThoiGianDuyet, ChuTri, ThuKy, ThoiGianBatDau, ThoiGianKetThuc, DiaDiem, GhiChu).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdLichXetDuyetDeCuongId').value = LichXetDuyetDeCuongId = AjaxOut.RetExtraParam1;\r\n" +         
                    "           AddHistory(LichXetDuyetDeCuongId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(LichXetDuyetDeCuongId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 

                #region Xóa Thông tin đăng ký
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   LichXetDuyetDeCuongId = document.getElementById('hdLichXetDuyetDeCuongId').value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.DeleteThongTin(RenderInfo, LichXetDuyetDeCuongId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(LichXetDuyetDeCuongId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion

                #region Thêm mới hội dồng cét duyệt
                    "   function SaveHoiDongXetDuyet(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       HoTen = document.getElementById('cbbNguoiDung').value;\r\n" +
                    "       ChucVu = document.getElementById('cbbChucVu').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.SaveHoiDongXetDuyet(RenderInfo, HoTen, ChucVu).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#TabHoiDongXetDuyet').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion

                #region Xóa hội dồng xét duyệt
                    "   function DeleteHoiDongXetDuyet(Id){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.DeleteHoiDongXetDuyet(RenderInfo, Id).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#TabHoiDongXetDuyet').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Thêm mới đề cương
                    "   function SaveDeCuong(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       TenDeCuong = document.getElementById('cbbTenDeCuong').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.SaveDeCuong(RenderInfo, TenDeCuong).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#TabDeCuong').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion

                #region Xóa đề cương
                    "   function DeleteDeCuong(Id){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.LichXetDuyetDeCuong.DeleteDeCuong(RenderInfo, Id).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#TabDeCuong').html(AjaxOut.HtmlContent);\r\n" +
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
        public static AjaxOut ThongTinDangKy(RenderInfoCls ORenderInfo, string LichXetDuyetDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                LichXetDuyetCls LichXetDuyetDeCuong = string.IsNullOrEmpty(LichXetDuyetDeCuongId) ? new LichXetDuyetCls() : CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuongId);
                string cbbNguoiLap = "";
                string cbbChuTri = "";
                string cbbNguoiDuyet = "";
                string cbbThuKi = "";
                List<HoiDongXetDuyetCls> OHoiDongXetDuyets = string.IsNullOrEmpty(LichXetDuyetDeCuong.ID) ? new List<HoiDongXetDuyetCls>() : CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Reading(ORenderInfo, new HoiDongXetDuyetFilterCls() { LICHXETDUYET_ID = LichXetDuyetDeCuong.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "HoiDongXetDuyets", OHoiDongXetDuyets);

                List<DeCuongCls> ODeCuongs = string.IsNullOrEmpty(LichXetDuyetDeCuong.ID) ? new List<DeCuongCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Reading(ORenderInfo, new DeCuongFilterCls() { LICHXETDUYET_ID = LichXetDuyetDeCuong.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "DeCuongs", ODeCuongs);

                if (!string.IsNullOrEmpty(LichXetDuyetDeCuong.NGUOILAP_ID))
                {
                    var nguoilap = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuong.NGUOILAP_ID);
                    if (nguoilap != null)
                    {
                        cbbNguoiLap += string.Format("<option value={0}>{1}</option>\r\n", nguoilap.OwnerUserId, nguoilap.FullName);
                    }
                }
                if (!string.IsNullOrEmpty(LichXetDuyetDeCuong.NGUOILAP_ID))
                {
                    var nguoiduyet = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuong.NGUOIDUYET_ID);
                    if (nguoiduyet != null)
                        cbbNguoiDuyet += string.Format(" <option value ={0} selected>{1}</option>\r\n", nguoiduyet.OwnerUserId, nguoiduyet.FullName);
                }       
                if (!string.IsNullOrEmpty(LichXetDuyetDeCuong.NGUOILAP_ID))
                {
                    var chutri = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuong.CHUTRI_ID);
                    if (chutri != null)
                        cbbChuTri += string.Format(" <option value ={0} selected>{1}</option>\r\n", chutri.OwnerUserId, chutri.FullName);
                }        
                if (!string.IsNullOrEmpty(LichXetDuyetDeCuong.NGUOILAP_ID))
                {
                    var thuki = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuong.THUKY_ID);
                    if (thuki != null)
                        cbbThuKi += string.Format(" <option value ={0} selected>{1}</option>\r\n", thuki.OwnerUserId, thuki.FullName);
                }
            
                string Html =
                "               <input id='hdLichXetDuyetDeCuongId' type='hidden' value='"+ LichXetDuyetDeCuong.ID + "'>\r\n" +
                "               <div class=\"row\" id='divLichXetDuyetDeCuongId'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Người lập: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbNguoiLap' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "' required>\r\n" +
                                                            cbbNguoiLap +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                                  <div class=\"col-md-3\">\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian lập lịch:") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                          <input id='dtThoiGianLap' required type='text' style='z-index: 0;' value='" + (LichXetDuyetDeCuong.THOIGIAN == null ? null : LichXetDuyetDeCuong.THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker1' required>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Người duyệt: ") +
                "                                       <select id = 'cbbNguoiDuyet' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người duyệt") + "'>\r\n" +
                                                            cbbNguoiDuyet +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                                  <div class=\"col-md-3\">\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian duyệt:") +
                "                                          <input id='dtThoiGianDuyet' type='text' style='z-index: 0;' value='"+ (LichXetDuyetDeCuong.THOIGIANDUYET == null ? null : LichXetDuyetDeCuong.THOIGIANDUYET.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Chủ trì: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbChuTri' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chủ trì") + "' required>\r\n" +
                                                            cbbChuTri +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Thư ký: ") +
                "                                       <select id = 'cbbThuKy' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Thư ký") + "'>\r\n" +
                                                            cbbThuKi +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian bắt đầu:") +
                "                                       <input id='dtThoiGianBatDau' type='text' style='z-index: 0;'  value='" + (LichXetDuyetDeCuong.THOIGIANBATDAU == null ? null : LichXetDuyetDeCuong.THOIGIANBATDAU.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian kết thúc:") +
                "                                       <input id='dtThoiGianKetThuc' type='text' style='z-index: 0;'  value='" + (LichXetDuyetDeCuong.THOIGIANKETTHUC == null ? null : LichXetDuyetDeCuong.THOIGIANKETTHUC.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                              <div class=\"col-md-6\">\r\n" +
                "                                  <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm thực hiện: ")+ "<span style = 'color:red' >(*)</span >\r\n" +
                "                                      <input id='txtDiaDiem' type='text' style='z-index: 0;'  value='" + LichXetDuyetDeCuong.DIADIEMTHUCHIEN + "' class='form-control valueForm' required>\r\n" +
                "                                  </div>\r\n" +
                "                              </div>\r\n" +
                "                              <div class=\"col-md-6\">\r\n" +
                "                                  <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Ghi chú: ") +
                "                                      <input id='txtGhiChu' type='text' style='z-index: 0;'  value='" + LichXetDuyetDeCuong.GHICHU + "' class='form-control valueForm'>\r\n" +
                "                                  </div>\r\n" +
                "                              </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +              
                "               </div>\r\n"+
                "               <div class='row'>\r\n" +
                "                   <div class='col-lg-6'>\r\n" +
                "                       <div class='ibox float-e-margins'>\r\n" +
                "                           <div class='ibox-title'>\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Hội đồng xét duyệt") + "</h5>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content\" style=\"border: 0; \" id='divHoiDongXetDuyet'> \r\n" +
                "                               <div class='row' style ='margin-top: 20px;'>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                             WebLanguage.GetLanguage(OSiteParam, "Họ tên:") +
                "                                            <select id='cbbNguoiDung' type='text' style='z-index: 0;' value='' class='form-control valueForm'>\r\n" +
                "                                            <select>\r\n" +
                "                                       </div> \r\n" +
                "                                        </div>\r\n" +
                "                                   <div class=\"col-md-3\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Chức vụ: ") +
                "                                           <select id = 'cbbChucVu' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + "'>\r\n" +
                "                                           </select>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div> \r\n" +
                "                                   <div class=\"col-md-2\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <button style ='margin-top:20px;' onclick='javascript:SaveHoiDongXetDuyet();' type=\"button\" class=\"btn btn-sm btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class='row' style ='margin-top: 20px;'>\r\n" +
                "                                   <div class=\"col-md-12\" id ='TabHoiDongXetDuyet'>\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            DrawHoiDongXetDuyet(ORenderInfo).HtmlContent +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +              
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-lg-6'>\r\n" +
                "                       <div class='ibox float-e-margins'>\r\n" +
                "                           <div class='ibox-title'>\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đề cương") + "</h5>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content\" style=\"border: 0; \" id='divDeCuong'> \r\n" +
                "                               <div class='row' style ='margin-top:20px;'>\r\n" +
                "                                   <div class=\"col-md-8\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                             WebLanguage.GetLanguage(OSiteParam, "Tên đề cương: ") +
                "                                            <select id='cbbTenDeCuong' type='text' style='z-index: 0;' value='' class='form-control valueForm'>\r\n" +
                "                                            </select>\r\n"+
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-2\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <button style ='margin-top:20px;'  onclick='javascript:SaveDeCuong();' type=\"button\" class=\"btn btn-sm btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Chọn") + "</button>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class='row' style ='margin-top:20px;'>\r\n" +
                "                                   <div class=\"col-md-12\" id ='TabDeCuong'>\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            DrawDeCuong(ORenderInfo).HtmlContent +
                "                                       </div> \r\n" +
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
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + " </th> \r\n" +
                         "          <th width=80 class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
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
                            manguoidung = nguoidungs.OwnerCode;

                        }
                    }
                        html +=
                        "       <tr>\r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + manguoidung + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + tennguoidung + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + chucvu + "</td> \r\n" +                      
                        "           <td style='text-align:center;'><a  title='Xóa' href=\"javascript:DeleteHoiDongXetDuyet('" + OHoiDongXetDuyets[iIndex].ID + "');\"><i class='" + WebScreen.GetDeleteGridIcon() + "' style='font-size:20px; margin-top:4px;'></i></a></td>\r\n" +
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
        public static AjaxOut DrawDeCuong(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<DeCuongCls> ODeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên đề cương") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài") + " </th> \r\n" +
                         "          <th width=80 class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < ODeCuongs.Count; iIndex++)
                {
                    string tendecuong = "";
                    string chunhiem = "";
                    DeCuongCls DeCuong = null;
                    if (!string.IsNullOrEmpty(ODeCuongs[iIndex].ID))
                        DeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ODeCuongs[iIndex].ID);
                    if (DeCuong != null)
                    {
                        tendecuong = DeCuong.TENDECUONG;
                        DangKyDeTaiCls DangKyDeTai = null;
                        if (!string.IsNullOrEmpty(DeCuong.DANGKYDETAI_ID))
                        {
                            DangKyDeTai = CallBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ORenderInfo, DeCuong.DANGKYDETAI_ID);
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
                    html +=
                    "       <tr>\r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + tendecuong + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + chunhiem + "</td> \r\n" +
                    "           <td style='text-align:center;'><a  title='Xóa' href=\"javascript:DeleteDeCuong('" + ODeCuongs[iIndex].ID + "');\"><i class='" + WebScreen.GetDeleteGridIcon() + "' style='font-size:20px; margin-top:4px;'></i></a></td>\r\n" +
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string LichXetDuyetDeCuongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DeCuongCls> ODeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                for (int iIndex = 0; iIndex < ODeCuongs.Count; iIndex++)
                {
                    if (!string.IsNullOrEmpty(ODeCuongs[iIndex].ID))
                    {
                        DeCuongCls ThemMoiDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ODeCuongs[iIndex].ID);
                        if (ThemMoiDeCuong != null)
                            RetAjaxOut.RetExtraParam2 = ThemMoiDeCuong.TRANGTHAI.ToString();
                    }
                }
                LichXetDuyetCls LichXetDuyetDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuongId);
                if (LichXetDuyetDeCuong != null)
                    RetAjaxOut.RetExtraParam1 = LichXetDuyetDeCuong.TRANGTHAI.ToString();
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string lapLichXetDuyetId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);               
                LichXetDuyetCls lapLichXetDuyet = CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().CreateModel(ORenderInfo, lapLichXetDuyetId);
                if (string.IsNullOrEmpty(lapLichXetDuyetId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn cần điền đầy đủ thông tin trước khi chuyển duyệt.");
                    return RetAjaxOut;
                }
                lapLichXetDuyet.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Save(ORenderInfo, lapLichXetDuyet.ID, lapLichXetDuyet);
                List<DeCuongCls> ODeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                for (int iIndex = 0; iIndex < ODeCuongs.Count; iIndex++)
                {
                    DeCuongCls ODeCuong = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().CreateModel(ORenderInfo, ODeCuongs[iIndex].ID);
                    ODeCuong.TRANGTHAI = trangThai;
                    CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Save(ORenderInfo, ODeCuongs[iIndex].ID, ODeCuong);
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
        #endregion Vẽ giao diện

        #region Xử lý nghiệp vụ         
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string LichXetDuyetDeCuongId, string NguoiLap, string ThoiGianLap, string NguoiDuyet, string ThoiGianDuyet, string ChuTri, string ThuKy, string ThoiGianBatDau, string ThoiGianKetThuc, string DiaDiem, string GhiChu)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                LichXetDuyetCls LichXetDuyetDeCuong = new LichXetDuyetCls();
                if (!string.IsNullOrEmpty(LichXetDuyetDeCuongId))
                {
                    LichXetDuyetDeCuong = CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().CreateModel(ORenderInfo, LichXetDuyetDeCuongId);
                }
                List<DeCuongCls> ODeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                if(ODeCuongs.Count <= 0)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn đề cương để lập lịch.");
                    return RetAjaxOut;
                }
                List<HoiDongXetDuyetCls> OHoiDongXetDuyets = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                if (OHoiDongXetDuyets.Count <= 0)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn hội đồng xét duyệt.");
                    return RetAjaxOut;
                }
                #region Thêm mới/cập nhật 
                if (string.IsNullOrEmpty(LichXetDuyetDeCuong.ID))
                {
                    LichXetDuyetDeCuong.ID = LichXetDuyetDeCuongId = System.Guid.NewGuid().ToString();
                    LichXetDuyetDeCuong.NGUOILAP_ID = NguoiLap;
                    LichXetDuyetDeCuong.THOIGIAN = string.IsNullOrWhiteSpace(ThoiGianLap) ? null : (DateTime?)DateTime.ParseExact(ThoiGianLap, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.NGUOIDUYET_ID = NguoiDuyet;
                    LichXetDuyetDeCuong.THOIGIANDUYET = string.IsNullOrWhiteSpace(ThoiGianDuyet) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDuyet, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.CHUTRI_ID = ChuTri;
                    LichXetDuyetDeCuong.THUKY_ID = ThuKy;
                    LichXetDuyetDeCuong.THOIGIANBATDAU = string.IsNullOrWhiteSpace(ThoiGianBatDau) ? null : (DateTime?)DateTime.ParseExact(ThoiGianBatDau, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.THOIGIANKETTHUC = string.IsNullOrWhiteSpace(ThoiGianKetThuc) ? null : (DateTime?)DateTime.ParseExact(ThoiGianKetThuc, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.DIADIEMTHUCHIEN = DiaDiem;
                    LichXetDuyetDeCuong.GHICHU = GhiChu;
                    LichXetDuyetDeCuong.TRANGTHAI = (int)DeCuongCls.eTrangThai.LapLich;
                    CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Add(ORenderInfo, LichXetDuyetDeCuong);
                    RetAjaxOut.RetExtraParam1 = LichXetDuyetDeCuong.ID;
                }
                else
                {
                    LichXetDuyetDeCuong.NGUOILAP_ID = NguoiLap;
                    LichXetDuyetDeCuong.THOIGIAN = string.IsNullOrWhiteSpace(ThoiGianLap) ? null : (DateTime?)DateTime.ParseExact(ThoiGianLap, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.NGUOIDUYET_ID = NguoiDuyet;
                    LichXetDuyetDeCuong.THOIGIANDUYET = string.IsNullOrWhiteSpace(ThoiGianDuyet) ? null : (DateTime?)DateTime.ParseExact(ThoiGianDuyet, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.CHUTRI_ID = ChuTri;
                    LichXetDuyetDeCuong.THUKY_ID = ThuKy;
                    LichXetDuyetDeCuong.THOIGIANBATDAU = string.IsNullOrWhiteSpace(ThoiGianBatDau) ? null : (DateTime?)DateTime.ParseExact(ThoiGianBatDau, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.THOIGIANKETTHUC = string.IsNullOrWhiteSpace(ThoiGianKetThuc) ? null : (DateTime?)DateTime.ParseExact(ThoiGianKetThuc, "HH:mm dd/MM/yyyy", null);
                    LichXetDuyetDeCuong.DIADIEMTHUCHIEN = DiaDiem;
                    LichXetDuyetDeCuong.GHICHU = GhiChu;
                    CallBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Save(ORenderInfo, LichXetDuyetDeCuong.ID, LichXetDuyetDeCuong);
                }
                #endregion

                #region Thêm mới/cập nhật hội đồng xét duyệt
                List<HoiDongXetDuyetCls> newHoiDongs = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                List<HoiDongXetDuyetCls> oldHoiDongs = CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Reading(ORenderInfo, new HoiDongXetDuyetFilterCls() { LICHXETDUYET_ID = LichXetDuyetDeCuong.ID }).ToList();
                foreach (var oldHoiDong in oldHoiDongs)
                {
                    bool isExists = false;
                    foreach (var newHoiDong in newHoiDongs)
                    {
                        if (newHoiDong.ID == oldHoiDong.ID)//cập nhật
                        {
                            oldHoiDong.LICHXETDUYET_ID = LichXetDuyetDeCuong.ID;
                            oldHoiDong.NGUOIDUNG_ID = newHoiDong.NGUOIDUNG_ID;
                            oldHoiDong.CHUCVU_ID = newHoiDong.CHUCVU_ID;
                            CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Save(ORenderInfo, oldHoiDong.ID, oldHoiDong);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Delete(ORenderInfo, oldHoiDong.ID);
                    }
                }
                var addHoiDongs = newHoiDongs.Where(o => !oldHoiDongs.Any(old => old.ID == o.ID));
                foreach (var addHoiDong in addHoiDongs)//Thêm mới
                {
                    addHoiDong.LICHXETDUYET_ID = LichXetDuyetDeCuong.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Add(ORenderInfo, addHoiDong);
                }
                #endregion
                #region Thêm mới/cập nhật Đề cương
                List<DeCuongCls> newDeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                List<DeCuongCls> oldDeCuongs = CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().Reading(ORenderInfo, new DeCuongFilterCls() { LICHXETDUYET_ID = LichXetDuyetDeCuong.ID }).ToList();
                foreach (var oldDeCuong in oldDeCuongs)
                {
                    bool isExists = false;
                    foreach (var newDeCuong in newDeCuongs)
                    {
                        if (newDeCuong.ID == oldDeCuong.ID)//cập nhật
                        {
                            string ExecuteQuery = " UPDATE NCKH_DECUONG SET LICHXETDUYET_ID ='" + LichXetDuyetDeCuong.ID + "'where ID = '" + oldDeCuong.ID + "'";
                            CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().UpdateLichXetDuyetID(ORenderInfo, ExecuteQuery);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        string ExecuteQuery = " UPDATE NCKH_DECUONG SET LICHXETDUYET_ID = '' where ID ='" + oldDeCuong.ID + "'";
                        CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().UpdateLichXetDuyetID(ORenderInfo, ExecuteQuery);
                    }
                }
                var adddeCuongs = newDeCuongs.Where(o => !oldDeCuongs.Any(old => old.ID == o.ID));
                foreach (var adddecuong in adddeCuongs)//Thêm mới
                {
                    string ExecuteQuery = " UPDATE NCKH_DECUONG SET LICHXETDUYET_ID = '" + LichXetDuyetDeCuong.ID + "' where ID = '" + adddecuong.ID + "'";
                    CallBussinessUtility.CreateBussinessProcess().CreateDeCuongProcess().UpdateLichXetDuyetID(ORenderInfo, ExecuteQuery);
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
        public static AjaxOut SaveHoiDongXetDuyet(RenderInfoCls ORenderInfo, string HoTen, string ChucVu)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<HoiDongXetDuyetCls> HoiDongs = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                if (HoiDongs.Any(o => o.NGUOIDUNG_ID == HoTen))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Người này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                HoiDongs.Add(new HoiDongXetDuyetCls()
                {
                    ID = Guid.NewGuid().ToString(),
                    NGUOIDUNG_ID = HoTen,
                    CHUCVU_ID = ChucVu,
                });
                RetAjaxOut.HtmlContent = DrawHoiDongXetDuyet(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteHoiDongXetDuyet(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<HoiDongXetDuyetCls> HoiDongs = WebSessionUtility.GetSession(OSiteParam, "HoiDongXetDuyets") as List<HoiDongXetDuyetCls>;
                HoiDongXetDuyetCls hoidong = HoiDongs.FirstOrDefault(o => o.ID == Id);
                HoiDongs.Remove(hoidong);
                RetAjaxOut.HtmlContent = DrawHoiDongXetDuyet(ORenderInfo).HtmlContent;
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
        public static AjaxOut SaveDeCuong(RenderInfoCls ORenderInfo, string TenDeCuong)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DeCuongCls> DeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                if (DeCuongs.Any(o => o.ID == TenDeCuong))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đề cương này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                DeCuongs.Add(new DeCuongCls()
                {
                    DeCuongLapLichID = Guid.NewGuid().ToString(),
                    ID = TenDeCuong,
                });
                RetAjaxOut.HtmlContent = DrawDeCuong(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteDeCuong(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DeCuongCls> DeCuongs = WebSessionUtility.GetSession(OSiteParam, "DeCuongs") as List<DeCuongCls>;
                DeCuongCls decuong = DeCuongs.FirstOrDefault(o => o.ID == Id);
                DeCuongs.Remove(decuong);
                RetAjaxOut.HtmlContent = DrawDeCuong(ORenderInfo).HtmlContent;
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetCaBenhUrl(RenderInfoCls ORenderInfo, string lichXetDuyetId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                var user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                var DeCuongUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "XetDuyetDeCuong",
                        new WebParamCls[] { new WebParamCls("lichxetduyetid", lichXetDuyetId),});
                RetAjaxOut.RetUrl = DeCuongUrl;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
    }
}
