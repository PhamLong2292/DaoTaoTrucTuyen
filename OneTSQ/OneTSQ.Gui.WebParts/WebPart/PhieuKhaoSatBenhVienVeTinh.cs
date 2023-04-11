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
    public class PhieuKhaoSatBenhVienVeTinh : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "PhieuKhaoSatBenhVienVeTinh";
        public override string WebPartTitle { get { return "Phiếu khảo sát bệnh viện vệ tinh"; } }
        public override string Description { get { return "Phiếu khảo sát bệnh viện vệ tinh"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, PhieuKhaoSatBenhVienVeTinh.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PhieuKhaoSatBenhVienVeTinh), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string PhieuKhaoSatId = WebEnvironments.Request("id");         
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteThongTin();' style='float:left; margin-left: 20px;" + (!string.IsNullOrEmpty(PhieuKhaoSatId) ? null : "display:none;") + "'>\r\n" +
                    "           <input type='button' id='btnThuHoi' title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();' style='float:left; margin-left: 20px; display:none;'>\r\n" +
                    "           <input type='button' id='btnHoanTat' title='Hoàn tất' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "' onclick='HoanTat();' style='float:left; margin-left: 20px;'>\r\n" +
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-12' id='divTTPhieuKhaoSat'>\r\n" +                       
                                    DrawPhieuKhaoSat(ORenderInfo, null, PhieuKhaoSatId).HtmlContent +               
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
                    "   var PhieuKhaoSatId='" + PhieuKhaoSatId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +
                    "     $('#dtThoiGianGui').datetimepicker({ \r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n"+
                    "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     }); \r\n" +
                    "     InCallInitSelect2('cbbTenDonVi', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát") + "');\r\n" +
                    "     CallInitSelect2('cbbNoiDungDaoTao', '" + WebEnvironments.GetRemoteProcessDataUrl(LopHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nội dung đào tạo") + "');\r\n" +              
                    "     CheckTrangThai();\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.CheckTrangThai(RenderInfo, PhieuKhaoSatId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
                    "   if(PhieuKhaoSatId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return PhieuKhaoSatId;\r\n" +  
                    "}\r\n" +
                #endregion 
                #region hiển thị giao diện theo trạng thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.CheckTrangThai(RenderInfo, PhieuKhaoSatId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divTTPhieuKhaoSat :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnHoanTat').hide();\r\n" +
                    "           $('#btnXoa').hide();\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divTTPhieuKhaoSat :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnHoanTat').show();\r\n" +
                    "       }\r\n" +
                    "}\r\n" +
                #endregion
                #region Refresh form về trạng thái mới
                    "function Clear()\r\n" +
                    "{\r\n" +
                    "   window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                    "     CallInitSelect2('cbbTenDonVi', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát") + "');\r\n" +
                    "     InCallInitSelect2('cbbNoiDungDaoTao', '" + WebEnvironments.GetRemoteProcessDataUrl(LopHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nội dung đào tạo") + "');\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.UpdateTrangThai(RenderInfo, PhieuKhaoSatId, " + (int)PhieuKhaoSatBenhVienVeTinhCls.eTrangThai.Moi + ").value;\r\n" +
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
                    "                   AddHistory(PhieuKhaoSatId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region HoanTat
                    "   function HoanTat(){\r\n" +
                    "     MaSoPhieu = document.getElementById('txtMaSoPhieu').value;\r\n" +
                    "     if(MaSoPhieu == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường mã số phiếu không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     ThoiGianGui = document.getElementById('dtThoiGianGui').value;\r\n" +
                    "     if(ThoiGianGui == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường thời gian gửi không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     TenDonVi = document.getElementById('cbbTenDonVi').value;\r\n" +
                    "     if(TenDonVi == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường tên đơn vị khảo sát không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     TenKhoa = document.getElementById('txtTenKhoa').value;\r\n" +
                    "     if(TenKhoa == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường tên khoa/ tổ YHCT không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     NgayThanhLap = document.getElementById('dtNgayThanhLap').value;\r\n" +
                    "     if(NgayThanhLap == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày thành lập không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +                  
                    "     SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.UpdateTrangThai(RenderInfo, PhieuKhaoSatId, " + (int)PhieuKhaoSatBenhVienVeTinhCls.eTrangThai.HoanTat + ").value;\r\n" +
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
                    "           $('#btnXoa').hide();\r\n" +
                    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã hoàn tất!") + "');\r\n" +
                    "           AddHistory(PhieuKhaoSatId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           ShowInput();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
                    "     Ma = document.getElementById('txtMaSoPhieu').value;\r\n" +
                    "     ThoiGianGui = document.getElementById('dtThoiGianGui').value;\r\n" +
                    "     TenDonVi = document.getElementById('cbbTenDonVi').value;\r\n" +
                    "     TenKhoa = document.getElementById('txtTenKhoa').value;\r\n" +
                    "     NgayThanhLap = document.getElementById('dtNgayThanhLap').value;\r\n" +
                    "     SoLuongBacSi = document.getElementById('txtSoLuongBacSi').value;\r\n" +
                    "     SoLuongYSi = document.getElementById('txtSoLuongYSi').value;\r\n" +
                    "     SoLuongDieuDuong = document.getElementById('txtSoLuongDieuDuong').value;\r\n" +
                    "     SoLuongKTV = document.getElementById('txtSoLuongKTV').value;\r\n" +
                    "     SoLuongDuocSi = document.getElementById('txtSoLuongDuocSi').value;\r\n" +
                    "     SoLuongKhac = document.getElementById('txtSoLuongKhac').value;\r\n" +
                    "     SoLanKham = document.getElementById('txtSoLanKham').value;\r\n" +
                    "     SoBenhNhan = document.getElementById('txtSoBenhNhan').value;\r\n" +
                    "     SoLanThuThuat = document.getElementById('txtSoLanThuThuat').value;\r\n" +
                    "     TrangThietBi = document.getElementById('txtTenTrangThietBi').value;\r\n" +
                    "     SoGuongKeHoach = document.getElementById('txtSoGiuongKeHoach').value;\r\n" +
                    "     SoThucTe = document.getElementById('txtSoGiuongThucKe').value;\r\n" +
                    "     SoBuongBenh = document.getElementById('txtSoBuongBenh').value;\r\n" +
                    "     SoPhongKhamTT = document.getElementById('txtSoPhongKhamThuThuat').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.SaveThongTin(RenderInfo, PhieuKhaoSatId, Ma, ThoiGianGui, TenDonVi, TenKhoa, NgayThanhLap, SoLuongBacSi, SoLuongYSi, SoLuongDieuDuong, SoLuongDuocSi, SoLuongKTV, SoLuongKhac, SoLanKham, SoBenhNhan, SoLanThuThuat, TrangThietBi, SoGuongKeHoach, SoThucTe, SoBuongBenh, SoPhongKhamTT).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdPhieuKhaoSatId').value = PhieuKhaoSatId = AjaxOut.RetExtraParam1;\r\n" +
                    "           $('#btnXoa').show();\r\n" +
                    "           AddHistory(PhieuKhaoSatId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(PhieuKhaoSatId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 
                #region Xóa Thông tin phiếu
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.DeleteThongTin(RenderInfo, PhieuKhaoSatId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(PhieuKhaoSatId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion
                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
                    "       masophieu = sender.value;\r\n" +
                    "       if(PhieuKhaoSatId == '' && masophieu != '')\r\n" +//Nếu đăng ký chưa được tạo thì load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.DrawPhieuKhaoSat(RenderInfo, masophieu, PhieuKhaoSatId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divTTPhieuKhaoSat').html(AjaxOut.HtmlContent);\r\n" +    
                    "               $('.datepicker').datetimepicker({\r\n" +
                    "                  locale: 'vi',\r\n" +
                    "                  useCurrent: false,\r\n" +
                    "                  format: 'DD/MM/YYYY',\r\n" +
                    "                  maxDate: new Date() \r\n" +
                    "               });\r\n" +
                    "               $('#dtThoiGianXayRaSuCo').datetimepicker({ \r\n" +
                    "                  locale: 'vi',\r\n" +
                    "                  useCurrent: false,\r\n" +
                    "                  format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "                  maxDate: new Date() \r\n" +
                    "               }); \r\n" +
                    "           }\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           CallInitSelect2('cbbTenDonVi', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát") + "');\r\n" +
                    "           CallInitSelect2('cbbNoiDungDaoTao', '" + WebEnvironments.GetRemoteProcessDataUrl(LopHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nội dung đào tạo") + "');\r\n" +
                    "       }\r\n" +                   
                    "   }\r\n" +
                #endregion
                #region Thêm mới nhu cầu năng lực
                    "   function SaveDaoTaoNhanLuc(){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       PhieuKhaoSatId = document.getElementById('hdPhieuKhaoSatId').value;\r\n" +
                    "       TenKhoaHoc = document.getElementById('cbbNoiDungDaoTao').value;\r\n" +
                    "       SoLuong = document.getElementById('txtSoLuongNguoiHoc').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.SaveDaoTaoNhanLuc(RenderInfo, PhieuKhaoSatId, TenKhoaHoc, SoLuong).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#TabDaoTaoNhanLuc').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa nhu cầu năng lực
                    "   function DeleteDaoTaoNhanLuc(Id){\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuKhaoSatBenhVienVeTinh.DeleteDaoTaoNhanLuc(RenderInfo, Id).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#TabDaoTaoNhanLuc').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Select2
                    " function InCallInitSelect2(Id, svUrl)\r\n" +
                    " {\r\n" +
                    "  $(\"#\"+Id).select2({ \r\n" +
                    "  allowClear: true," +
                    "        ajax: { \r\n" +
                    "            id: function (e) { return e.id + \"|\" + e.text },  \r\n" +
                    "            url: svUrl, \r\n" +
                    "            dataType: 'json', \r\n" +
                    "            delay: 250, \r\n" +
                    "            type: 'POST',  \r\n" +
                    "            data: function (params) { \r\n" +
                    "                return { \r\n" +
                    "                    q: params.term,  \r\n" +
                    "                    page: params.page \r\n" +
                    "                }; \r\n" +
                    "            }, \r\n" +
                    "            processResults: function (data, params) { \r\n" +
                    "                params.page = params.page || 0; \r\n" +
                    "                console.log(params.page); \r\n" +
                    "                return { \r\n" +
                    "                    results: data.items, \r\n" +
                    "                    pagination: { \r\n" +
                    "                        more: (params.page * 10) < data.total_count \r\n" +
                    "                    } \r\n" +
                    "                }; \r\n" +
                    "            }, \r\n" +
                    "            cache: true \r\n" +
                    "        }, \r\n" +
                    "        escapeMarkup: function (markup) { return markup; },  \r\n" +
                    "        minimumInputLength: 0, \r\n" +

                    "        templateResult: InformatRepo,  \r\n" +
                    "        templateSelection: InformatRepoSelection  \r\n" +
                    "    }); \r\n" +
                    " }\r\n" +

                    "  function InformatRepo(repo) { \r\n" +
                    "     var markup = ''; \r\n" +
                    "     if (repo.loading) return repo.text; \r\n" +
                    "     else if (repo.id == null) markup = '<table style=\"width: 415px;border-bottom: 1px solid black;\"><tr><td style=\"width:20%;padding:4px\"><h3>'+ repo.Code+'</h3></td> <td><h3>'+repo.Name+'</h3></td></tr></table>'; \r\n" +
                    "     else markup = '<table style=\"width: 100%;\"><tr><td style=\"color:maroon;font-weight:bold; width:20%;padding:4px\">'+ repo.Code+'</td> <td >'+repo.Name+'</td></tr></table>'; \r\n" +
                    "     return markup; \r\n" +
                    "  } \r\n" +

                    " function InformatRepoSelection(repo) { \r\n" +
                    "   if(repo.UnitCode == undefined)\r\n" +
                    "      return repo.text; \r\n" +
                    "   else\r\n" +
                    "      return repo.text + ' ' + '(' + repo.Name + ')'; \r\n" +
                        " } \r\n" +
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
        public static AjaxOut DrawPhieuKhaoSat(RenderInfoCls ORenderInfo, string MaSoPhieu, string PhieuKhaoSatId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuKhaoSatBenhVienVeTinhCls PhieuKSBV = null;
                if (!string.IsNullOrEmpty(PhieuKhaoSatId))
                    PhieuKSBV = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, PhieuKhaoSatId);
                if (!string.IsNullOrEmpty(MaSoPhieu))
                    PhieuKSBV = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, MaSoPhieu);
                if (PhieuKSBV == null)
                {
                    PhieuKSBV = new PhieuKhaoSatBenhVienVeTinhCls();
                    if (string.IsNullOrEmpty(MaSoPhieu))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        PhieuKSBV.MA = boDemValueMaHV.RetExtraParam2;
                    }
                    else PhieuKSBV.MA = MaSoPhieu;
                }
                List<DaoTaoNhanLucCls> ODaoTaoNL = string.IsNullOrEmpty(PhieuKSBV.ID) ? new List<DaoTaoNhanLucCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Reading(ORenderInfo, new DaoTaoNhanLucFilterCls() { PHIEUKHAOSATBVVT_ID = PhieuKSBV.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "DaoTaoNhanLucs", ODaoTaoNL);
             
                string cbbTenDonVi = "<select class='form-control' id='cbbTenDonVi' style='font-size: 14px; width:415px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát") + "' required>\r\n";
                var BenhViens = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), PhieuKSBV.BENHVIEN_ID);
                if (BenhViens != null)
                {
                    cbbTenDonVi += string.Format(" <option value = {0} selected>{1}</option>\r\n", BenhViens.Id, BenhViens.Ten);
                }
                cbbTenDonVi += "</select>\r\n";

                string cbbNoiDungDaoTao = "<select class='form-control' id='cbbNoiDungDaoTao' style='font-size: 14px; width:405px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Nội dung cần đào tạo") + "'>\r\n";             
                cbbNoiDungDaoTao += "</select>\r\n";

                string Html =
                "               <input id='hdPhieuKhaoSatId' type='hidden' value='"+ PhieuKSBV.ID +"'>\r\n" +
                "               <div class=\"row\">\r\n" +
                "                   <div class=\"col-md-12\">\r\n" +
                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Phiếu đăng ký khảo sát") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-1\">\r\n" +
                "                                        <div class=\"form-group\">\r\n" +
                                                             WebLanguage.GetLanguage(OSiteParam, "Mã phiếu: ") + "<span style = 'color:red' > *</span >\r\n" +
                "                                            <input id='txtMaSoPhieu' type='text' style='z-index: 0;' value='" + PhieuKSBV.MA + "' onchange='txtMa_onchange(this)'  class='form-control valueForm' required " + (string.IsNullOrEmpty(PhieuKhaoSatId) ? null : "disabled=true") + ">\r\n" +
                "                                        </div>\r\n" +
                "                                    </div>\r\n" +
                "                                    <div class=\"col-md-2\">\r\n" +
                "                                        <div class=\"form-group\">\r\n" +
                                                             WebLanguage.GetLanguage(OSiteParam, "Thời gian gửi:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                            <input id='dtThoiGianGui' type='text' style='z-index: 0;' value='" + (PhieuKSBV.THOIGIAN == null ? null : PhieuKSBV.THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm' required>\r\n" +
                "                                        </div>\r\n" +
                "                                    </div>\r\n" +
                "                                   <div class=\"col-md-3\" >\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát: ") + "<span style = 'color:red' > *</span >\r\n" +
                                                           cbbTenDonVi +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-3\" style='margin-left: 15px;'>\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Tên khoa/Tổ YHCT: ") + "<span style = 'color:red' > *</span >\r\n" +
                "                                           <input id='txtTenKhoa' type='text' style='z-index: 0;' value='"+PhieuKSBV.TENKHOA+"' class='form-control valueForm' required >\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-2\" style='margin-left: 15px;'>\r\n" +
                "                                        <div class=\"form-group\">\r\n" +
                                                             WebLanguage.GetLanguage(OSiteParam, "Ngày thành lập:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                            <input id='dtNgayThanhLap' type='text' style='z-index: 0;' value='" + (PhieuKSBV.NGAYTHANHLAP == null ? null : PhieuKSBV.NGAYTHANHLAP.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker' required>\r\n" +
                "                                        </div>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-6\">\r\n" +
                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Nhân lực YHCT") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng bác sĩ:") +
                "                                           <input id = 'txtSoLuongBacSi' class='form-control valueForm' value='" + PhieuKSBV.SOLUONGBACSI + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' style='z-index: 0;' value='' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng bác sĩ") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng y sĩ:") +
                "                                           <input id='txtSoLuongYSi' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOLUONGYSI + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng y sĩ") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng điều dưỡng:") +
                "                                           <input id='txtSoLuongDieuDuong' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOLUONGDIEUDUONG + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng điều dưỡng") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng KTV:") +
                "                                           <input id = 'txtSoLuongKTV' class='form-control valueForm' value='" + PhieuKSBV.SOLUONGKTV + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' style='z-index: 0;' value='' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng KTV") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng dược sĩ:") +
                "                                           <input id='txtSoLuongDuocSi' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOLUONGDUOCSI + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng dược sĩ") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng khác:") +
                "                                           <input id='txtSoLuongKhac' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOLUONGKHAC + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng khác") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +

                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Công tác khám chữa bệnh bằng YHCT") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lần khám bệnh bằng YHCT:") +
                "                                           <input id = 'txtSoLanKham' class='form-control valueForm' style='z-index: 0;' value='" + PhieuKSBV.SOLANKHAMYHCT + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lần khám bệnh bằng YHCT") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số bệnh nhân điều trị nội trú:") +
                "                                           <input id='txtSoBenhNhan' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOBNNOITRU + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số bệnh nhân điều trị nội trú") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lần làm các thủ thuật:") +
                "                                           <input id='txtSoLanThuThuat' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOLANTHUTHUAT + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lần làm các thủ thuật") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +

                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Tên các trang bị") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-12\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Tên trang thiết bị:") +
                "                                           <textarea class=\"form-control\" id=\"txtTenTrangThietBi\" rows=\"5\">"+ PhieuKSBV.TRANGTHIETBI +"</textarea>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +               
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +       
                "                   </div>\r\n" +

                "                   <div class=\"col-md-6\">\r\n" +
                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Cơ sở vật chất") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số giường kế hoạch:") +
                "                                           <input id = 'txtSoGiuongKeHoach' class='form-control valueForm' value='" + PhieuKSBV.SOGIUONGKEHOACH + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số giường kế hoạch") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số giường thực kê:") +
                "                                           <input id='txtSoGiuongThucKe' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOGIUONGTHUCTE + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số giường thực kê") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số buồng bệnh:") +
                "                                           <input id='txtSoBuongBenh' type='text' style='z-index: 0;' value='" + PhieuKSBV.SOBUONGBENH + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số lượng điều dưỡng") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-4\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số phòng khám thủ thuật:") +
                "                                           <input id = 'txtSoPhongKhamThuThuat' class='form-control valueForm' value='" + PhieuKSBV.SOPKTHUTHUAT + "' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' title = '" + WebLanguage.GetLanguage(OSiteParam, "Số phòng khám thủ thuật") + "'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +

                "                       <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                           <div class=\"ibox-title\">\r\n" +
                "                               <h5>" + WebLanguage.GetLanguage(OSiteParam, "Nhu cầu năng lực:") + "</h5>\r\n" +
                "                               <div class=\"ibox-tools\">\r\n" +
                "                                   <a class=\"collapse-link\">\r\n" +
                "                                       <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                                   </a>\r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"ibox-content col-md-12\">\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Nội dung cần đào tạo: ") +
                                                           cbbNoiDungDaoTao +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-3\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Số lượng người học: ") +
                "                                           <input id='txtSoLuongNguoiHoc' type='text' style='z-index: 0;' onkeypress='CheckCurrency(event);' onkeyup='FormatCurrency(this);' maxlength='4' value='' class='form-control valueForm' >\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-3\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <button type=\"button\" class=\"btn btn-sm btn-primary\" style='margin-top: 20px; width: 100px;' onclick ='javascript:SaveDaoTaoNhanLuc();'> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class='row'>\r\n" +
                "                                   <div class=\"col-md-12\" id ='TabDaoTaoNhanLuc'>\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                                                            DrawDaoTaoNhanLuc(ORenderInfo).HtmlContent +
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
        public static AjaxOut DrawDaoTaoNhanLuc(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<DaoTaoNhanLucCls> ODaoTaoNhanLucs = WebSessionUtility.GetSession(OSiteParam, "DaoTaoNhanLucs") as List<DaoTaoNhanLucCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung cần đào tạo") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số lượng học viên") + " </th> \r\n" +
                         "          <th width=80 class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < ODaoTaoNhanLucs.Count; iIndex++)
                {
                    string tenkhoahoc = "";
                    DT_KhoaHocCls TenKhoaHoc = null;
                    if (!string.IsNullOrEmpty(ODaoTaoNhanLucs[iIndex].DM_TENKHOAHOC_ID))
                    {
                        TenKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, ODaoTaoNhanLucs[iIndex].DM_TENKHOAHOC_ID);
                        if (TenKhoaHoc != null)
                        {
                            DM_TenKhoaHocCls TenKhoaHocs = CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CreateModel(ORenderInfo, TenKhoaHoc.TEN);
                            if(TenKhoaHocs != null)
                            {
                                tenkhoahoc = TenKhoaHocs.Ten;
                            }
                        }
                    }                  
                    html +=
                    "       <tr>\r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + tenkhoahoc + "</td> \r\n" +
                    "           <td style='text-align: center; vertical-align: middle;'>" + ODaoTaoNhanLucs[iIndex].SOLUONG + "</td> \r\n" +
                    "           <td style='text-align:center;'><a  title='Xóa' href=\"javascript:DeleteDaoTaoNhanLuc('" + ODaoTaoNhanLucs[iIndex].ID + "');\"><i class='" + WebScreen.GetDeleteGridIcon() + "' style='font-size:20px; margin-top:4px;'></i></a></td>\r\n" +
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string PhieuKhaoSatId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuKhaoSatBenhVienVeTinhCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, PhieuKhaoSatId);
                if (BaoCaoSuCo != null)
                    RetAjaxOut.RetExtraParam1 = BaoCaoSuCo.TRANGTHAI.ToString();
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
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "PhieuKhaoSatBenhVienVeTinh", FieldName = "MaSoPhieu" }).FirstOrDefault();
            if (boMa != null && !string.IsNullOrEmpty(boMa.BIEUTHUC))
            {
                while (maHV == null || CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, maHV) != null)
                {
                    if (maHV != null)
                    {
                        var boDem = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().CreateModel(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC));
                        //if (boDem != null)
                        //{
                        //    CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().UpdateStatus(ORenderInfo, boDem.ID, (int?)int.Parse(boDemValue), 1);
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
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string PhieuKhaoSatId, string Ma, string ThoiGianGui, string TenDonVi, string TenKhoa, string NgayThanhLap, string SoLuongBacSi, string SoLuongYSi, string SoLuongDieuDuong, string SoLuongDuocSi, string SoLuongKTV, string SoLuongKhac, string SoLanKham, string SoBenhNhan, string SoLanThuThuat, string TrangThietBi, string SoGuongKeHoach, string SoThucTe, string SoBuongBenh, string SoPhongKhamTT)
        {           
              AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuKhaoSatBenhVienVeTinhCls PhieuKhaoSat = new PhieuKhaoSatBenhVienVeTinhCls();
                if (!string.IsNullOrEmpty(PhieuKhaoSatId))
                {
                    PhieuKhaoSat = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, PhieuKhaoSatId);
                }
                else
                {
                    PhieuKhaoSat = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, Ma);
                    if (PhieuKhaoSat == null)
                    {
                        PhieuKhaoSat = new PhieuKhaoSatBenhVienVeTinhCls() { MA = Ma };
                    }
                }
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(PhieuKhaoSat.ID))
                {
                    PhieuKhaoSat.ID = PhieuKhaoSatId = System.Guid.NewGuid().ToString();
                    PhieuKhaoSat.THOIGIAN = string.IsNullOrWhiteSpace(ThoiGianGui) ? null : (DateTime?)DateTime.ParseExact(ThoiGianGui, "HH:mm dd/MM/yyyy", null);
                    PhieuKhaoSat.BENHVIEN_ID = TenDonVi;
                    PhieuKhaoSat.TENKHOA = TenKhoa;
                    PhieuKhaoSat.NGAYTHANHLAP = string.IsNullOrWhiteSpace(NgayThanhLap) ? null : (DateTime?)DateTime.ParseExact(NgayThanhLap, "dd/MM/yyyy", null);
                    PhieuKhaoSat.SOLUONGBACSI = string.IsNullOrWhiteSpace(SoLuongBacSi) ? null : (int?)int.Parse(SoLuongBacSi);
                    PhieuKhaoSat.SOLUONGYSI = string.IsNullOrWhiteSpace(SoLuongYSi) ? null : (int?)int.Parse(SoLuongYSi);
                    PhieuKhaoSat.SOLUONGDIEUDUONG = string.IsNullOrWhiteSpace(SoLuongDieuDuong) ? null :(int?)int.Parse(SoLuongDieuDuong);
                    PhieuKhaoSat.SOLUONGDUOCSI = string.IsNullOrWhiteSpace(SoLuongDuocSi) ? null : (int?)int.Parse(SoLuongDuocSi);
                    PhieuKhaoSat.SOLUONGKTV = string.IsNullOrWhiteSpace(SoLuongKTV) ? null : (int?)int.Parse(SoLuongKTV);
                    PhieuKhaoSat.SOLUONGKHAC = string.IsNullOrWhiteSpace(SoLuongKhac) ? null : (int?)int.Parse(SoLuongKhac);
                    PhieuKhaoSat.SOLANKHAMYHCT = string.IsNullOrWhiteSpace(SoLanKham) ? null : (int?)int.Parse(SoLanKham);
                    PhieuKhaoSat.SOBNNOITRU = string.IsNullOrWhiteSpace(SoBenhNhan) ? null : (int?)int.Parse(SoBenhNhan);
                    PhieuKhaoSat.SOLANTHUTHUAT = string.IsNullOrWhiteSpace(SoLanThuThuat) ? null : (int?)int.Parse(SoLanThuThuat);
                    PhieuKhaoSat.TRANGTHIETBI = TrangThietBi;
                    PhieuKhaoSat.SOGIUONGKEHOACH = string.IsNullOrWhiteSpace(SoGuongKeHoach) ? null : (int?)int.Parse(SoGuongKeHoach);
                    PhieuKhaoSat.SOGIUONGTHUCTE = string.IsNullOrWhiteSpace(SoThucTe) ? null : (int?)int.Parse(SoThucTe);
                    PhieuKhaoSat.SOBUONGBENH = string.IsNullOrWhiteSpace(SoBuongBenh) ? null : (int?)int.Parse(SoBuongBenh);
                    PhieuKhaoSat.SOPKTHUTHUAT = string.IsNullOrWhiteSpace(SoPhongKhamTT) ? null : (int?)int.Parse(SoPhongKhamTT);
                    PhieuKhaoSat.TRANGTHAI = (int)PhieuKhaoSatBenhVienVeTinhCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Add(ORenderInfo, PhieuKhaoSat);
                    RetAjaxOut.RetExtraParam1 = PhieuKhaoSat.ID;
                }
                else
                {
                    PhieuKhaoSat.THOIGIAN = string.IsNullOrWhiteSpace(ThoiGianGui) ? null : (DateTime?)DateTime.ParseExact(ThoiGianGui, "HH:mm dd/MM/yyyy", null);
                    PhieuKhaoSat.BENHVIEN_ID = TenDonVi;
                    PhieuKhaoSat.TENKHOA = TenKhoa;
                    PhieuKhaoSat.NGAYTHANHLAP = string.IsNullOrWhiteSpace(NgayThanhLap) ? null : (DateTime?)DateTime.ParseExact(NgayThanhLap, "dd/MM/yyyy", null);
                    PhieuKhaoSat.SOLUONGBACSI = string.IsNullOrWhiteSpace(SoLuongBacSi) ? null : (int?)int.Parse(SoLuongBacSi);
                    PhieuKhaoSat.SOLUONGYSI = string.IsNullOrWhiteSpace(SoLuongYSi) ? null : (int?)int.Parse(SoLuongYSi);
                    PhieuKhaoSat.SOLUONGDIEUDUONG = string.IsNullOrWhiteSpace(SoLuongDieuDuong) ? null : (int?)int.Parse(SoLuongDieuDuong);
                    PhieuKhaoSat.SOLUONGDUOCSI = string.IsNullOrWhiteSpace(SoLuongDuocSi) ? null : (int?)int.Parse(SoLuongDuocSi);
                    PhieuKhaoSat.SOLUONGKTV = string.IsNullOrWhiteSpace(SoLuongKTV) ? null : (int?)int.Parse(SoLuongKTV);
                    PhieuKhaoSat.SOLUONGKHAC = string.IsNullOrWhiteSpace(SoLuongKhac) ? null : (int?)int.Parse(SoLuongKhac);
                    PhieuKhaoSat.SOLANKHAMYHCT = string.IsNullOrWhiteSpace(SoLanKham) ? null : (int?)int.Parse(SoLanKham);
                    PhieuKhaoSat.SOBNNOITRU = string.IsNullOrWhiteSpace(SoBenhNhan) ? null : (int?)int.Parse(SoBenhNhan);
                    PhieuKhaoSat.SOLANTHUTHUAT = string.IsNullOrWhiteSpace(SoLanThuThuat) ? null : (int?)int.Parse(SoLanThuThuat);
                    PhieuKhaoSat.TRANGTHIETBI = TrangThietBi;
                    PhieuKhaoSat.SOGIUONGKEHOACH = string.IsNullOrWhiteSpace(SoGuongKeHoach) ? null : (int?)int.Parse(SoGuongKeHoach);
                    PhieuKhaoSat.SOGIUONGTHUCTE = string.IsNullOrWhiteSpace(SoThucTe) ? null : (int?)int.Parse(SoThucTe);
                    PhieuKhaoSat.SOBUONGBENH = string.IsNullOrWhiteSpace(SoBuongBenh) ? null : (int?)int.Parse(SoBuongBenh);
                    PhieuKhaoSat.SOPKTHUTHUAT = string.IsNullOrWhiteSpace(SoPhongKhamTT) ? null : (int?)int.Parse(SoPhongKhamTT);
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Save(ORenderInfo, PhieuKhaoSat.ID, PhieuKhaoSat);
                }      
                #endregion
                #region Thêm mới/cập nhật tài liệu
                List<DaoTaoNhanLucCls> newNhaLucs = WebSessionUtility.GetSession(OSiteParam, "DaoTaoNhanLucs") as List<DaoTaoNhanLucCls>;
                List<DaoTaoNhanLucCls> oldNhaLucs = CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Reading(ORenderInfo, new DaoTaoNhanLucFilterCls() { PHIEUKHAOSATBVVT_ID = PhieuKhaoSat.ID }).ToList();
                foreach (var oldNhaLuc in oldNhaLucs)
                {
                    bool isExists = false;
                    foreach (var newNhaLuc in newNhaLucs)
                    {
                        if (newNhaLuc.ID == oldNhaLuc.ID)//cập nhật
                        {
                            oldNhaLuc.PHIEUKHAOSATBVVT_ID = PhieuKhaoSat.ID;
                            oldNhaLuc.SOLUONG = newNhaLuc.SOLUONG;
                            oldNhaLuc.DM_TENKHOAHOC_ID = newNhaLuc.DM_TENKHOAHOC_ID;
                            CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Save(ORenderInfo, oldNhaLuc.ID, oldNhaLuc);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Delete(ORenderInfo, oldNhaLuc.ID);
                    }
                }
                var addNhaLucs = newNhaLucs.Where(o => !oldNhaLucs.Any(old => old.ID == o.ID));
                foreach (var addNhaLuc in addNhaLucs)//Thêm mới
                {
                    addNhaLuc.PHIEUKHAOSATBVVT_ID = PhieuKhaoSat.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Add(ORenderInfo, addNhaLuc);
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
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string PhieuKhaoSatId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Delete(ORenderInfo, PhieuKhaoSatId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                string dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DanhSachPhieuKhaoSat", new WebParamCls[] { });
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string PhieuKhaoSatId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuKhaoSatBenhVienVeTinhCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, PhieuKhaoSatId);
                if (BaoCaoSuCo == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phiếu này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                BaoCaoSuCo.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Save(ORenderInfo, BaoCaoSuCo.ID, BaoCaoSuCo);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut OnChangeMaBN(RenderInfoCls ORenderInfo, string MaBN)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSession.CheckSessionTimeOut(ORenderInfo);
        //        PhieuKhaoSatBenhVienVeTinhCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, MaBN);
        //        if (BaoCaoSuCo != null)
        //        {
        //            RetAjaxOut.Error = true;
        //            RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã có phiếu tồn tại mã bệnh nhân này!");
        //            return RetAjaxOut;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveDaoTaoNhanLuc(RenderInfoCls ORenderInfo, string PhieuID, string TenLopHoc, int? SoLuong)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DaoTaoNhanLucCls> HoiDongs = WebSessionUtility.GetSession(OSiteParam, "DaoTaoNhanLucs") as List<DaoTaoNhanLucCls>;
                if (HoiDongs.Any(o => o.DM_TENKHOAHOC_ID == TenLopHoc))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Nội dung đào tạo này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                HoiDongs.Add(new DaoTaoNhanLucCls()
                {
                    ID = Guid.NewGuid().ToString(),
                    DM_TENKHOAHOC_ID = TenLopHoc,
                    SOLUONG = SoLuong,
                });
                RetAjaxOut.HtmlContent = DrawDaoTaoNhanLuc(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteDaoTaoNhanLuc(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DaoTaoNhanLucCls> HoiDongs = WebSessionUtility.GetSession(OSiteParam, "DaoTaoNhanLucs") as List<DaoTaoNhanLucCls>;
                DaoTaoNhanLucCls hoidong = HoiDongs.FirstOrDefault(o => o.ID == Id);
                HoiDongs.Remove(hoidong);
                RetAjaxOut.HtmlContent = DrawDaoTaoNhanLuc(ORenderInfo).HtmlContent;
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
