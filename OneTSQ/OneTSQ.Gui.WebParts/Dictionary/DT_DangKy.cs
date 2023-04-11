﻿    using Newtonsoft.Json;
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
    public class DT_DangKy : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DT_DangKy";
        public override string WebPartTitle { get { return "Đăng ký học"; } }
        public override string Description { get { return "Đăng ký học"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, DT_DangKy.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_DangKy), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string ketQuaDaoTaoId = WebEnvironments.Request("id");
                DT_KetQuaDaoTaoCls ketQuaDaoTao = string.IsNullOrEmpty(ketQuaDaoTaoId) ? new DT_KetQuaDaoTaoCls() : CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                DT_HocVienCls hocVien = string.IsNullOrEmpty(ketQuaDaoTao.HOCVIEN_ID) ? new DT_HocVienCls() : CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, ketQuaDaoTao.HOCVIEN_ID);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                bool themPermission = string.IsNullOrEmpty(ketQuaDaoTaoId) || ketQuaDaoTao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_DangKyPermission().PermissionFunctionCode, DT_KetQuaDaoTaoCls.ePermission.Them.ToString(), new DT_DangKyPermission().PermissionFunctionCode, DT_DangKyPermission.StaticPermissionFunctionId, userId, ketQuaDaoTao.NGUOITAO_ID);
                bool xemPermission = string.IsNullOrEmpty(ketQuaDaoTaoId) || ketQuaDaoTao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_DangKyPermission().PermissionFunctionCode, DT_KetQuaDaoTaoCls.ePermission.Xem.ToString(), new DT_DangKyPermission().PermissionFunctionCode, DT_DangKyPermission.StaticPermissionFunctionId, userId, ketQuaDaoTao.NGUOITAO_ID);
                bool suaPermission = string.IsNullOrEmpty(ketQuaDaoTaoId) || ketQuaDaoTao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_DangKyPermission().PermissionFunctionCode, DT_KetQuaDaoTaoCls.ePermission.Sua.ToString(), new DT_DangKyPermission().PermissionFunctionCode, DT_DangKyPermission.StaticPermissionFunctionId, userId, ketQuaDaoTao.NGUOITAO_ID);
                bool xoaPermission = string.IsNullOrEmpty(ketQuaDaoTaoId) || ketQuaDaoTao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_DangKyPermission().PermissionFunctionCode, DT_KetQuaDaoTaoCls.ePermission.Xoa.ToString(), new DT_DangKyPermission().PermissionFunctionCode, DT_DangKyPermission.StaticPermissionFunctionId, userId, ketQuaDaoTao.NGUOITAO_ID);

                #region Html
                string html =
                    "<form action='javascript:SaveDangKy();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='button' id='btnClear' title='Mới' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Mới") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px; " + (themPermission ? null : "display:none;") + "'>\r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px; " + (suaPermission ? null : "display:none;") + "'>\r\n" +
                    "           <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteDangKy();' style='float:left; margin-left: 20px; " + (xoaPermission && !string.IsNullOrEmpty(ketQuaDaoTaoId) ? null : "display:none;") + "'>\r\n" +
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-12'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Đăng ký khóa học") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                                        DrawDangKy(ORenderInfo, ketQuaDaoTaoId).HtmlContent +
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
                    "   var KetQuaDaoTaoId='" + ketQuaDaoTaoId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     $('#dtNgaySinh').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +
                    "     $('#dtNgayCapCMND').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +
                    "     CallInitSelect2('cbbKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbDanToc', '" + WebEnvironments.GetRemoteProcessDataUrl(DanTocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Dân tộc") + "');\r\n" +
                    "     CallInitSelect2('cbbNoiSinh', '" + WebEnvironments.GetRemoteProcessDataUrl(TinhThanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi sinh") + "');\r\n" +
                    "     CallInitSelect2('cbbDonViCongTac', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViCongTacService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "');\r\n" +
                    "     InCallInitSelect2('cbbDiaChiHanhChinh', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViHanhChinhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính") + "');\r\n" +
                    "     CallInitSelect2('cbbTrinhDo', '" + WebEnvironments.GetRemoteProcessDataUrl(TrinhDoHocVanService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tốt nghiệp") + "');\r\n" +
                    "     CallInitSelect2('cbbChuyenMon', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenMonService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn") + "');\r\n" +
                    "     CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "       ShowTrangThai();\r\n" +
                    "   });\r\n" +
                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "   if(ketQuaDaoTaoId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin đăng ký trước khi in Đơn đăng ký học.") + "');\r\n" +
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "   return ketQuaDaoTaoId;\r\n" +
                    "}\r\n" +
                #endregion Truyền id đăng ký cho nút print
                #region Show trạng thái đăng ký
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.DT_DangKy.ShowTrangThai(RenderInfo, KetQuaDaoTaoId).value);\r\n" +
                    "}\r\n" +
                #endregion Show trạng thái đăng ký
                //Fill giá trị theo biểu thức vào trường mã bệnh nhân
                "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #region lấy về ảnh được upload và hiển thị lên form 
                    "   function GetHinhAnh(input) \r\n" +
                    "   {\r\n" +
                    "       if (input.files && input.files[0]) \r\n" +
                    "       {\r\n" +
                    "           var filerdr = new FileReader();\r\n" +
                    "           filerdr.onload = function(e) {\r\n" +
                    "               $('#noImg').removeClass('fa fa-user');\r\n" +
                    "               $('#imgNdAnh').attr('src', e.target.result);\r\n" +
                    "           };\r\n" +
                    "           filerdr.readAsDataURL(input.files[0]);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Check ngày sinh
                    "   function onNgaySinhValueChange(s) \r\n" +
                    "   { \r\n" +
                    "       if(s.value != '' && s.value != undefined) \r\n" +
                    "       { \r\n" +
                    "			var sngay = ConvertDate_To_yyyMMdd(s.value);\r\n" +
                    "           var suans = false;\r\n" +
                    "           if(sngay.indexOf(\"undefined\") > -1){\r\n" +
                    "               suans = true;\r\n" +
                    "               $(\"#dtNgaySinh\").val(s.value.replaceAll(\"-\", \"/\"));\r\n" +
                    "               sngay = Convertddmmyyyy_to_yyyymmdd(s.value);\r\n" +
                    "           }\r\n" +
                    "           var dt = new Date(sngay);\r\n" +
                    "           var minDay = new Date(1900,00,01);\r\n" +
                    "           var dn = new Date();\r\n" +
                    "           if(dt <= minDay || dt >= dn)\r\n" +
                    "           {\r\n" +
                    "               swal({ \r\n" +
                    "                       title: \"Cảnh báo\",\r\n" +
                    "                       text: \"Ngày sinh phải nhỏ hơn ngày hiện tại & lớn hơn ngày 01/01/1900\",\r\n" +
                    "                       type: \"warning\",\r\n" +
                    "                       confirmButtonColor: \"#DD6B55\",\r\n" +
                    "                       confirmButtonText: \"OK\",\r\n" +
                    "                       closeOnConfirm: true \r\n" +
                    "                   }, function () { \r\n" +
                    "                       dtNgaySinh.value = '';\r\n" +
                    "                       dtNgaySinh.focus();\r\n" +
                    "               });\r\n" +
                    "               return false;\r\n" +
                    "           }\r\n" +                 
                    "       }\r\n" +
                    "   } \r\n" +
                    "  function ConvertDate_To_yyyMMdd(t)\r\n" +
                    "   {\r\n" +
                    "       if(t != \"\" && t != undefined)\r\n" +
                    "       {\r\n" +
                    "           var arr = t.split('/');\r\n" +
                    "           return arr[2]+'-'+arr[1]+'-'+arr[0];\r\n" +
                    "       }\r\n" +
                    "       else\r\n" +
                    "           return '';\r\n" +
                    "   }\r\n" +
                #endregion
                #region xóa ảnh được upload
                    "   function DeleteHinhAnh() \r\n" +
                    "   {\r\n" +
                    "        $('#imgNdAnh').attr('src', '');\r\n" +
                    "        $('#noImg').addClass('fa fa-user');\r\n" +
                    "   }\r\n" +
                #endregion
                #region Refresh form về trạng thái mới
                    "function Clear()\r\n" +
                    "{\r\n" +
                    "   window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                    "}\r\n" +
                #endregion Refresh form về trạng thái mới
                #region Save Thông tin đăng ký
                    "function SaveDangKy()\r\n" +
                    "{\r\n" +
                    "     var tieuChuan = [];\r\n" +
                    "     $('.clsTieuChuan').each(function(index, element/*this*/) {\r\n" +
                    "         if (element.checked){\r\n" +
                    "             tieuChuan.push(element.id);\r\n" +
                    "         }\r\n" +
                    "     });\r\n" +
                    "     ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "     khoaHocId = document.getElementById('cbbKhoaHoc').value;\r\n" +
                    "     nopHocPhi = ckbNopHocPhi.checked?1:0;\r\n" +
                    "     ma = document.getElementById('txtMa').value;\r\n" +
                    "     hoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "     ngayDangKy = document.getElementById('dtNgayDangKy').value;\r\n" +
                    "     ndAnh = $('#imgNdAnh').attr('src');\r\n" +
                    "     gioiTinh = $(\"input[name='GioiTinh']\").filter(':checked').val();\r\n" +
                    "     danToc = document.getElementById('cbbDanToc').value;\r\n" +
                    "     ngaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "     noiSinh = document.getElementById('cbbNoiSinh').value;\r\n" +
                    "     cmnd = document.getElementById('txtCMND').value;\r\n" +
                    "     ngayCapCMND = document.getElementById('dtNgayCapCMND').value;\r\n" +
                    "     noiCapCMND = document.getElementById('txtNoiCapCMND').value;\r\n" +
                    "     khoaPhongCongTac = document.getElementById('txtKhoaPhongCongTac').value;\r\n" +
                    "     donViCongTac = document.getElementById('cbbDonViCongTac').value;\r\n" +
                    "     chuyenMon = document.getElementById('cbbChuyenMon').value;\r\n" +
                    "     soNamKinhNghiem = parseInt(document.getElementById('txtSoNamKinhNghiem').value);\r\n" +
                    "     diaChiLienHe = document.getElementById('txtDiaChiLienHe').value;\r\n" +
                    "     diaChiHanhChinh = document.getElementById('cbbDiaChiHanhChinh').value;\r\n" +
                    "     dienThoai = document.getElementById('txtDienThoai').value;\r\n" +
                    "     email = document.getElementById('txtEmail').value;\r\n" +
                    "     totNghiep = document.getElementById('cbbTrinhDo').value;\r\n" +
                    "     namTotNghiep = parseInt(document.getElementById('txtNamTotNghiep').value);\r\n" +
                    "     truongCapBang = document.getElementById('txtTruongCapBang').value;\r\n" +
                    "     chuyenNganh = document.getElementById('txtChuyenNganh').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_DangKy.SaveDangKy(RenderInfo, ketQuaDaoTaoId, ngayDangKy, khoaHocId, tieuChuan, nopHocPhi, ndAnh, ma, hoTen, gioiTinh, danToc, ngaySinh, noiSinh, " +
                    "cmnd, ngayCapCMND, noiCapCMND, khoaPhongCongTac, donViCongTac, chuyenMon, soNamKinhNghiem, diaChiLienHe, diaChiHanhChinh, dienThoai, email, totNghiep, namTotNghiep, truongCapBang, chuyenNganh, '" + WebPartTitle + "').value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdKetQuaDaoTaoId').value = ketQuaDaoTaoId = AjaxOut.RetExtraParam1;\r\n" +
                    "           $('#btnXoa').show();\r\n" +
                    "           txtMa.disabled=true;\r\n" +
                    "           AddHistory(ketQuaDaoTaoId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(ketQuaDaoTaoId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin đăng ký
                #region Xóa Thông tin đăng ký
                    "function DeleteDangKy()\r\n" +
                    "{\r\n" +
                    "   RenderInfo = CreateRenderInfo();\r\n" +
                    "   ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "   AjaxOut = OneTSQ.WebParts.DT_DangKy.ChekLopHoc(RenderInfo, ketQuaDaoTaoId).value;\r\n" +
                    "   if(AjaxOut.Error)\r\n" +
                    "   {\r\n" +
                    "       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "       return;\r\n" +
                    "   }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 == 4)\r\n" +
                    "     {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Lớp học đã kết thúc, không thể xóa học viên.") + "');\r\n" +
                    "       return;\r\n" +
                    "     }\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_DangKy.DeleteDangKy(RenderInfo, ketQuaDaoTaoId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(ketQuaDaoTaoId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion Xóa Thông tin đăng ký
                #region Bắt sự kiện thay đổi combobox khóa học
                    "   function cbbKhoaHoc_onchange(sender){\r\n" +
                    "       khoaHocId = sender.value;\r\n" +
                    "       if(khoaHocId == '')\r\n" +
                    "       {\r\n" +
                    "           $('#spThoiLuongKhoaHoc').html('');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_DangKy.GetThoiLuongKhoaHoc(RenderInfo, khoaHocId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#spThoiLuongKhoaHoc').html(AjaxOut.RetObject);\r\n" +
                    "           }\r\n" +
                    "           ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_DangKy.DrawTieuChuanKhoaHoc(RenderInfo, khoaHocId, ketQuaDaoTaoId).value;\r\n" + 
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divTieuChuan').html(AjaxOut.HtmlContent);\r\n" +
                    "           }\r\n" +    
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Bắt sự kiện thay đổi combobox khóa học
                #region Bắt sự kiện thay đổi textbox mã học viên
                    "   function txtMa_onchange(sender){\r\n" +
                    "       ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "       maHocVien = sender.value;\r\n" +
                    "       if(ketQuaDaoTaoId == '' && maHocVien != '')\r\n" +//Nếu đăng ký chưa được tạo thì load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_DangKy.DrawHocVien(RenderInfo, null, maHocVien, ketQuaDaoTaoId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divHocVien').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "                $('#dtNgaySinh').datetimepicker({\r\n" +
                    "                  locale: 'vi',\r\n" +
                    "                  useCurrent: false,\r\n" +
                    "                  format: 'DD/MM/YYYY',\r\n" +
                    "                  maxDate: new Date() \r\n" +
                    "               });\r\n" +
                    "               $('#dtNgayCapCMND').datetimepicker({\r\n" +
                    "                  locale: 'vi',\r\n" +
                    "                  useCurrent: false,\r\n" +
                    "                  format: 'DD/MM/YYYY',\r\n" +
                    "                  maxDate: new Date() \r\n" +
                    "               });\r\n" +
                    "               CallInitSelect2('cbbDanToc', '" + WebEnvironments.GetRemoteProcessDataUrl(DanTocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Dân tộc") + "');\r\n" +
                    "               CallInitSelect2('cbbNoiSinh', '" + WebEnvironments.GetRemoteProcessDataUrl(TinhThanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi sinh") + "');\r\n" +
                    "               CallInitSelect2('cbbDonViCongTac', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViCongTacService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "');\r\n" +
                    "               InCallInitSelect2('cbbDiaChiHanhChinh', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViHanhChinhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính") + "');\r\n" +
                    "               CallInitSelect2('cbbTrinhDo', '" + WebEnvironments.GetRemoteProcessDataUrl(TrinhDoHocVanService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tốt nghiệp") + "');\r\n" +
                    "               CallInitSelect2('cbbChuyenNganh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenNganhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành") + "');\r\n" +
                    "               CallInitSelect2('cbbChuyenMon', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenMonService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn") + "');\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Bắt sự kiện thay đổi textbox mã học viên
                #region Hiển thị popup danh sách học viên theo 4 tiếu chí: Họ tên, ngày sinh, địa chỉ, giới tính
                    "   function ShowPopupHocViens(){\r\n" +
                    "       ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "       hoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "       gioiTinh = $(\"input[name='GioiTinh']\").filter(':checked').val();\r\n" +
                    "       ngaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "       diaChiHanhChinh = document.getElementById('cbbDiaChiHanhChinh').value;\r\n" +
                    "       if(ketQuaDaoTaoId == '' && hoTen != '' && ngaySinh != '' && diaChiHanhChinh != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_DangKy.PopupHocViens(RenderInfo, hoTen, gioiTinh, ngaySinh, diaChiHanhChinh).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           if(AjaxOut.RetObject > 0)\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "               document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách học viên gợi ý") + "</span>';\r\n" +
                    "               $('#divFormModal').modal('show');\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Bắt sự kiện thay đổi textbox mã học viên
                #region Điền thông tin học viên theo mã học viên được chọn trên danh sách học viên gợi ý 
                    "   function FillHocVienInfo(maHocVien){\r\n" +
                    "       ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId').value;\r\n" +
                    "       if(maHocVien != '')\r\n" +//Load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_DangKy.DrawHocVien(RenderInfo, null, maHocVien, ketQuaDaoTaoId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divHocVien').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "               CallInitSelect2('cbbDanToc', '" + WebEnvironments.GetRemoteProcessDataUrl(DanTocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Dân tộc") + "');\r\n" +
                    "               CallInitSelect2('cbbNoiSinh', '" + WebEnvironments.GetRemoteProcessDataUrl(TinhThanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi sinh") + "');\r\n" +
                    "               CallInitSelect2('cbbDonViCongTac', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViCongTacService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "');\r\n" +
                    "               InCallInitSelect2('cbbDiaChiHanhChinh', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViHanhChinhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính") + "');\r\n" +
                    "               CallInitSelect2('cbbTrinhDo', '" + WebEnvironments.GetRemoteProcessDataUrl(TrinhDoHocVanService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tốt nghiệp") + "');\r\n" +
                    "               CallInitSelect2('cbbChuyenNganh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenNganhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành") + "');\r\n" +
                    "               CallInitSelect2('cbbChuyenMon', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenMonService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn") + "');\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       $('#divFormModal').modal('hide');\r\n" +
                    "   }\r\n" +
                #endregion Điền thông tin học viên theo mã học viên được chọn trên danh sách học viên gợi ý
                #region Hiển thị row thêm mới văn bằng
                    "   function ShowAddVanBang(){\r\n" +
                    "       $('.CssEditorItemVanBang').hide();\r\n" +
                    "       $('.CssDisplayItemVanBang').show();\r\n" +
                    "       $('#trAddVanBang').show();\r\n" +
                    "       $('#txtNamCapVanBang').datetimepicker({ \r\n" +
                    "          format: 'YYYY' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị row edit văn bằng
                    "   function ShowEditItemLineVanBang(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItemVanBang').hide();\r\n" +
                    "       $('.CssDisplayItemVanBang').show();\r\n" +
                    "       $('#trAddVanBang').hide();\r\n" +
                    "       document.getElementById('txtTenVanBang'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtDonViCapVanBang'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtNamCapVanBang'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spTenVanBang'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spDonViCapVanBang'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spNamCapVanBang'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveVanBang'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditVanBang'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('btnDeleteVanBang'+rowIndex).style.display='none';\r\n" +

                    "       $('#txtNamCapVanBang'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'YYYY' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật văn bằng
                    "   function SaveVanBang(rowIndex){\r\n" +
                    "       vanBangId = document.getElementById('hdVanBangId'+rowIndex).value;\r\n" +
                    "       tenVanBang = document.getElementById('txtTenVanBang'+rowIndex).value;\r\n" +
                    "       donViCapVanBang = document.getElementById('txtDonViCapVanBang'+rowIndex).value;\r\n" +
                    "       namCapVanBang = parseInt(document.getElementById('txtNamCapVanBang'+rowIndex).value);\r\n" +
                    "       if(tenVanBang=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa nhập tên văn bằng/chứng chỉ.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_DangKy.SaveVanBang(RenderInfo, vanBangId, tenVanBang, donViCapVanBang, namCapVanBang).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divVanBangs').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa văn bằng
                    "   function DeleteVanBang(rowIndex){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa văn bằng này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               vanBangId = document.getElementById('hdVanBangId'+rowIndex).value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_DangKy.DeleteVanBang(RenderInfo, vanBangId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#divVanBangs').html(AjaxOut.HtmlContent);\r\n" +
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
                    "     else if (repo.id == null) markup = '<table style=\"width: 100%;border-bottom: 1px solid black;\"><tr><td style=\"width:20%;padding:4px\"><h3>'+ repo.ShortName+'</h3></td> <td><h3>'+repo.Name+'</h3></td></tr></table>'; \r\n" +
                    "     else markup = '<table style=\"width: 100%;\"><tr><td style=\"color:maroon;font-weight:bold; width:20%;padding:4px\">'+ repo.ShortName+'</td> <td >'+repo.Name+'</td></tr></table>'; \r\n" +
                    "     return markup; \r\n" +
                    "  } \r\n" +

                    " function InformatRepoSelection(repo) { \r\n" +
                    "   if(repo.UnitCode == undefined)\r\n" +
                    "      return repo.text; \r\n" +
                    "   else\r\n" +
                    "      return repo.text + ' ' + '(' + repo.ShortName + ')'; \r\n" +
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
        public static AjaxOut DrawDangKy(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = string.IsNullOrEmpty(ketQuaDaoTaoId) ? new DT_KetQuaDaoTaoCls() : CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(ketQuaDaoTao.KHOAHOCDANGKY_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, ketQuaDaoTao.KHOAHOCDANGKY_ID);
                string cbbKhoaHoc = "<select class='form-control' id='cbbKhoaHoc' required onchange='cbbKhoaHoc_onchange(this)' style='font-size: 20px;'>\r\n";
                if (khoaHoc != null)
                    cbbKhoaHoc += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", khoaHoc.MA, khoaHoc.TENKHOAHOC);
                cbbKhoaHoc += "</select>\r\n";

                string Html =
                "               <input id='hdKetQuaDaoTaoId' type='hidden' value='" + ketQuaDaoTao.ID + "'>\r\n" +
                "               <div class=\"row\">\r\n" +
                "                   <div class=\"col-md-3\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Thời gian đăng ký:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='dtNgayDangKy' required type='text' value='" + (ketQuaDaoTao.NGAYDANGKY == null ? null : ketQuaDaoTao.NGAYDANGKY.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-4\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Khóa học:") + "<span style = 'color:red' > *</span >\r\n" +
                                            cbbKhoaHoc +
                "                       </div> \r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-3\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Thời gian học:") + "<br>\r\n" +
                "                           <span id='spThoiLuongKhoaHoc' class='valueForm'>" + (khoaHoc == null ? null : khoaHoc.THOILUONG + (string.IsNullOrEmpty(khoaHoc.LOAITHOILUONG) ? null : " " + DT_KhoaHocParser.LoaiThoiLuongs[khoaHoc.LOAITHOILUONG])) + "</span>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\" style='margin-top: 20px; '>\r\n" +
                                            "<input type=\"checkbox\" id='ckbNopHocPhi' style='display: inline;' " + (ketQuaDaoTao.NOPHOCPHI == 1 ? "checked" : "") + "> Đã đóng học phí\r\n" +
                "                       </div> \r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class=\"row\">\r\n" +
                "                   <div class=\"col-md-12\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đạt tiêu chuẩn:") + "<br>\r\n" +
                "                           <div id = 'divTieuChuan'>\r\n" +
                                                DrawTieuChuanKhoaHoc(ORenderInfo, ketQuaDaoTao.KHOAHOCDANGKY_ID, ketQuaDaoTaoId).HtmlContent +
                "                           </div> \r\n" +
                "                       </div> \r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class=\"row\" id='divHocVien'>\r\n" +
                                    DrawHocVien(ORenderInfo, ketQuaDaoTao.HOCVIEN_ID, null, ketQuaDaoTao.ID).HtmlContent +
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
        public static AjaxOut DrawHocVien(RenderInfoCls ORenderInfo, string hocVienId, string hocVienMa, string dangKyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_HocVienCls hocVien = null;
                if (!string.IsNullOrEmpty(hocVienId))
                    hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, hocVienId);
                if (!string.IsNullOrEmpty(hocVienMa))
                    hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, hocVienMa);
                if (hocVien == null)
                {
                    hocVien = new DT_HocVienCls();
                    if (string.IsNullOrEmpty(hocVienMa))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        hocVien.MA = boDemValueMaHV.RetExtraParam2;
                    }
                    else hocVien.MA = hocVienMa;
                }
                List<DT_VanBangCls> vangBangs = string.IsNullOrEmpty(hocVien.ID) ? new List<DT_VanBangCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Reading(ORenderInfo, new DT_VanBangFilterCls() { HOCVIEN_ID = hocVien.ID }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "DT_DangKy_VanBangs", vangBangs);

                string cbbDonViCongTac = "<select class='form-control' id='cbbDonViCongTac' style='font-size: 20px;'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.DONVICONGTAC_MA))
                {
                    var donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.DONVICONGTAC_MA);
                    if (donViCongTac != null)
                        cbbDonViCongTac += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", donViCongTac.Ma, donViCongTac.Ten);
                    else cbbDonViCongTac += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", hocVien.DONVICONGTAC_MA, hocVien.DONVICONGTAC_MA, "N/A");
                }
                cbbDonViCongTac += "</select>\r\n";

                string cbbDanToc =
                "<select id = 'cbbDanToc' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.DANTOC_MA))
                {
                    var danToc = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.DANTOC_MA);
                    if (danToc != null)
                        cbbDanToc += string.Format("<option value={0}>{0} - {1}</option>\r\n", danToc.Ma, danToc.Ten);
                    else
                        cbbDanToc += string.Format("<option value={0}>{0}</option>\r\n", hocVien.DANTOC_MA);
                }
                else cbbDanToc += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
                cbbDanToc += "</select>\r\n";

                string cbbNoiSinh =
                "<select id = 'cbbNoiSinh' title = '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.NOISINH_MA))
                {
                    var tinhThanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTinhThanhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.NOISINH_MA);
                    if (tinhThanh != null)
                        cbbNoiSinh += string.Format("<option value={0}>{0} - {1}</option>\r\n", tinhThanh.Ma, tinhThanh.Ten);
                    else
                        cbbNoiSinh += string.Format("<option value={0}>{0}</option>\r\n", hocVien.NOISINH_MA);
                }
                else cbbNoiSinh += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
                cbbNoiSinh += "</select>\r\n";

                string cbbDiaChiHanhChinh =
                "<select id = 'cbbDiaChiHanhChinh' onchange='ShowPopupHocViens()' required title = '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.DIACHIHANHCHINH_MA))
                {
                    var donViHanhChinh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViHanhChinhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.DIACHIHANHCHINH_MA);
                    if (donViHanhChinh != null)
                        cbbDiaChiHanhChinh += string.Format("<option value={0}>{1}</option>\r\n", donViHanhChinh.Ma, donViHanhChinh.Ten);
                    else
                        cbbDiaChiHanhChinh += string.Format("<option value={0}>{0}</option>\r\n", hocVien.DIACHIHANHCHINH_MA);
                }
                else cbbDiaChiHanhChinh += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
                cbbDiaChiHanhChinh += "</select>\r\n";

                string cbbTrinhDo =
                "<select id = 'cbbTrinhDo' style='width: 600px;' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.TOTNGHIEP_MA))
                {
                    var trinhDo = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTrinhDoHocVanProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.TOTNGHIEP_MA);
                    if (trinhDo != null)
                        cbbTrinhDo += string.Format("<option value={0}>{0} - {1}</option>\r\n", trinhDo.Ma, trinhDo.Ten);
                    else
                        cbbTrinhDo += string.Format("<option value={0}>{0}</option>\r\n", hocVien.TOTNGHIEP_MA);
                }
                else cbbTrinhDo += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
                cbbTrinhDo += "</select>\r\n";

                string cbbChuyenMon =
                "<select id = 'cbbChuyenMon' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(hocVien.CHUYENMON_MA))
                {
                    var chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.CHUYENMON_MA);
                    if (chuyenMon != null)
                        cbbChuyenMon += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenMon.Ma, chuyenMon.Ten);
                    else
                        cbbChuyenMon += string.Format("<option value={0}>{0}</option>\r\n", hocVien.CHUYENMON_MA);
                }
                else cbbChuyenMon += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "</option>\r\n";
                cbbChuyenMon += "</select>\r\n";

                string Html =
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "1. Thông tin học viên") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-4\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <div id='divAnh' style='text-align:center;'>\r\n" +
                "                                           <img id='imgNdAnh' height='250px' src='" + (!string.IsNullOrEmpty(hocVien.NOIDUNGANH) ? hocVien.NOIDUNGANH : null) + "'/>\r\n" +
                "                                           <i id='noImg' class='" + (string.IsNullOrEmpty(hocVien.NOIDUNGANH) ? "fa fa-user" : null) + "' style='font-size:250px;'> </i>\r\n" +
                "                                       </div>\r\n" +
                "                                       <div style='text-align:center;'>\r\n " +
                "                                           <button class='btn btn-default btn-file' onkeypress=\"if(event.keyCode==13){document.getElementById('fileUpload').click(); return false;}\"> \r\n " +
                "                                               " + WebLanguage.GetLanguage(OSiteParam, "Chọn ảnh") + "<input type = 'file' id='fileUpload' accept='image/*' onchange='GetHinhAnh(this);'> \r\n " +
                "                                           </button> \r\n " +
                "                                           <button type='button' class='btn btn-sm btn-default'  onclick='DeleteHinhAnh()'>" + WebLanguage.GetLanguage(OSiteParam, "Xóa ảnh") + "</button>\r\n" +
                "                                       </div> \r\n " +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-8\">\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Mã: ") + "<span style = 'color:red' > *</span >\r\n" +
                "                                               <input id='txtMa' type='text' style='z-index: 0;' required value='" + hocVien.MA + "' onchange='txtMa_onchange(this)'  class='form-control valueForm' required " + (string.IsNullOrEmpty(dangKyId) ? null : "disabled=true") + ">\r\n" +
                "                                           </div>\r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Họ tên khai sinh:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                               <input id='txtHoTen' type='text' style='z-index: 0;' required onchange='ShowPopupHocViens()' value='" + hocVien.HOTEN + "' class='form-control valueForm' required>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + "<span style = 'color:red' > *</span ><br>\r\n" +
                "                                               <span class=\"valueForm\" style='float:left; z-index: 0;'><input id='rdoNam' name='GioiTinh' type=\"radio\" onchange='ShowPopupHocViens()' " + (string.IsNullOrEmpty(hocVien.ID) || hocVien.GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nam ? "checked" : null) + " value='" + (int)Common.BenhNhan.eGioiTinh.Nam + "' style=\"float:left;\">&nbsp;Nam&nbsp;&nbsp;&nbsp;&nbsp;</span>\r\n" +
                "                                               <span class=\"valueForm\"><input id='rdoNu' name='GioiTinh' type=\"radio\" onchange='ShowPopupHocViens()' " + (hocVien.GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nu ? "checked" : null) + " value='" + (int)Common.BenhNhan.eGioiTinh.Nu + "' style=\"float:left;z-index: 0;\">&nbsp;Nữ</span>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                               WebLanguage.GetLanguage(OSiteParam, "Dân tộc: ") +
                                                                cbbDanToc +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Ngày, tháng, năm sinh:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                               <input id='dtNgaySinh' required type='text' style='z-index: 0;' onchange='ShowPopupHocViens()' onblur=\"onNgaySinhValueChange(this)\" value='" + (hocVien.NGAYSINH == null ? null : hocVien.NGAYSINH.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm'>\r\n" +
                "                                           </div>\r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Nơi sinh (tỉnh/thành phố ghi trong giấy khai sinh):") +
                                                                cbbNoiSinh +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-4\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Số CMND/ CCCD:") +
                "                                               <input id='txtCMND' type='text' style='z-index: 0;' onkeypress='CheckCurrency(event);' value='" + hocVien.CMTND + "'  maxlength='15' class='form-control valueForm'>\r\n" +
                "                                           </div>\r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-4\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Ngày cấp:") +
                "                                               <input id='dtNgayCapCMND' type='text' style='z-index: 0;' value='" + (hocVien.CMTND_NGAYCAP == null ? null : hocVien.CMTND_NGAYCAP.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm'>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-4\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Nơi cấp:") +
                "                                               <input id='txtNoiCapCMND' type='text' value='" + hocVien.CMTND_NOICAP + "' class='form-control valueForm'>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Nơi công tác(Ghi rõ Khoa/Phòng/Ban; Đơn vị):") +
                "                                               <input id='txtKhoaPhongCongTac' type='text' style='z-index: 0;' value='" + hocVien.KHOAPHONGCONGTAC + "' class='form-control valueForm'>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                               <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác:") + "</span><br>\r\n" +
                                                                cbbDonViCongTac +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"row\">\r\n" +
                "                                       <div class=\"col-md-8\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                "                                               <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn:") + "</span><br>\r\n" +
                                                                cbbChuyenMon +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                       <div class=\"col-md-4\">\r\n" +
                "                                           <div class=\"form-group\">\r\n" +
                                                                WebLanguage.GetLanguage(OSiteParam, "Số năm kinh nghiệm:") +
                "                                               <input id='txtSoNamKinhNghiem' type='number' value='" + hocVien.SONAMKINHNGHIEM + "' class='form-control valueForm'>\r\n" +
                "                                           </div> \r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                               </div> \r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Địa chỉ liên hệ:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='txtDiaChiLienHe' type='text' required style='z-index: 0;' value='" + hocVien.DIACHISONHA + "' class='form-control valueForm' required>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                               <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính: ") + "<span style = 'color:red' > *</span ></span><br>\r\n" +
                                                        cbbDiaChiHanhChinh +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-6\" style=''>\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Điện thoại:") +
                "                                       <input id='txtDienThoai' type='tel' style='z-index: 0;' onkeypress='CheckCurrency(event);' value='" + hocVien.DIENTHOAI + "'  maxlength='10' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Email:") +
                "                                       <input id='txtEmail' type='email' style='z-index: 0;' value='" + hocVien.EMAIL + "' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "2. Thông tin về văn bằng") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Tốt nghiệp:") +
                                                        cbbTrinhDo +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Năm tốt nghiệp:") +
                "                                       <input id='txtNamTotNghiep' type='text' value='" + hocVien.NAMTOTNGHIEP + "' class='form-control yearpicker valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Trường cấp bằng:") +
                "                                       <input id='txtTruongCapBang' type='text' value='" + hocVien.TRUONGCAPBANG + "' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành:") +
                "                                       <input id='txtChuyenNganh' type='text'  style='z-index: 0;' value='" + hocVien.CHUYENNGANH_MA + "' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "3. Các văn bằng, chứng chỉ khác liên quan đến khóa học") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\" id ='divVanBangs'>\r\n" +
                                            DrawVanBangs(ORenderInfo).HtmlContent +
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
        public static AjaxOut DrawVanBangs(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<DT_VanBangCls> vanBangs = WebSessionUtility.GetSession(OSiteParam, "DT_DangKy_VanBangs") as List<DT_VanBangCls>;
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên văn bằng/chứng chỉ") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị cấp văn bằng/chứng chỉ") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Năm") + " </th> \r\n" +
                         "          <th width=60 style='text-align:center;'><a href='javascript:ShowAddVanBang()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm văn bằng") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n" +
                         "      <tr id='trAddVanBang' style='display:none;'> \r\n" +
                         "          <input type='hidden' id='hdVanBangId' value=''>\r\n" +
                         "          <td></td> \r\n" +
                         "          <td><input type='text' class='form-control' id='txtTenVanBang'></td> \r\n" +
                         "          <td><input type='text' class='form-control' id='txtDonViCapVanBang'></td> \r\n" +
                         "          <td><input type='text' class='form-control yearpicker' id='txtNamCapVanBang' data-mask='9999'></td> \r\n" +
                         "          <td style='text-align:center;'><a href='javascript:SaveVanBang(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                         "      </tr> \r\n";
                for (int iIndex = 0; iIndex < vanBangs.Count; iIndex++)
                {
                    html += 
                        "       <tr> \r\n" +
                        "           <input type='hidden' id='hdVanBangId" + iIndex + "' value='" + vanBangs[iIndex].ID + "'>\r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td><input type='text' class='form-control CssEditorItemVanBang' style='display:none;' id='txtTenVanBang" + iIndex + "' value='" + vanBangs[iIndex].TEN + "'><span class='CssDisplayItemVanBang' id='spTenVanBang" + iIndex + "'>" + vanBangs[iIndex].TEN + "</span></td> \r\n" +
                        "           <td><input type='text' class='form-control CssEditorItemVanBang' style='display:none;' id='txtDonViCapVanBang" + iIndex + "' value='" + vanBangs[iIndex].DONVICAP + "'><span class='CssDisplayItemVanBang' id='spDonViCapVanBang" + iIndex + "'>" + vanBangs[iIndex].DONVICAP + "</span></td> \r\n" +
                        "           <td style='text-align: center;'><input type='text' class='form-control CssEditorItemVanBang yearpicker' data-mask='9999' style='display:none; ' id='txtNamCapVanBang" + iIndex + "' value='" + vanBangs[iIndex].NAM + "'><span class='CssDisplayItemVanBang' id='spNamCapVanBang" + iIndex + "'>" + vanBangs[iIndex].NAM + "</span></td> \r\n" +
                        "           <td style='text-align:center;'>\r\n" +
                        "               <a id='btnSaveVanBang" + iIndex + "' class='CssEditorItemVanBang' style='display:none' href='javascript:SaveVanBang(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                        "               <a id='btnEditVanBang" + iIndex + "' class='CssEditorItemVanBang' href='javascript:ShowEditItemLineVanBang(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                        "               <a id='btnDeleteVanBang" + iIndex + "' class='CssEditorItemVanBang' href='javascript:DeleteVanBang(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                        "           </td> \r\n" +
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
        public static AjaxOut PopupHocViens(RenderInfoCls ORenderInfo, string hoTen, int gioiTinh, string ngaySinh, string diaChiHanhChinh)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_HocVienFilterCls filter = new DT_HocVienFilterCls()
                {
                    Keyword = hoTen,
                    GioiTinh = gioiTinh,
                    NgaySinh = DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", null),
                    MaDiaChiHanhChinh = diaChiHanhChinh
                };
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, filter);
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên khai sinh") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Nơi sinh") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số CMND") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + " </th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < hocViens.Count(); iIndex++)
                {
                    TinhThanhCls tinhThanh = null;
                    if (!string.IsNullOrEmpty(hocViens[iIndex].NOISINH_MA))
                    {
                         tinhThanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTinhThanhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocViens[iIndex].NOISINH_MA);                     
                    }
                    DonViCongTacCls donViCongTac = null;
                    if (!string.IsNullOrEmpty(hocViens[iIndex].DONVICONGTAC_MA))
                    {
                        donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocViens[iIndex].DONVICONGTAC_MA);                      
                    }
                    html += 
                        "       <tr> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + hocViens[iIndex].MA + "</td> \r\n" +
                        "           <td style='vertical-align: middle;'><a href='javascript: FillHocVienInfo(\"" + hocViens[iIndex].MA + "\")' title='" + WebLanguage.GetLanguage(OSiteParam, "Chọn học viên") + "'>" + hocViens[iIndex].HOTEN + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (hocViens[iIndex].GIOITINH == null ? null : Common.BenhNhan.GioiTinhs[hocViens[iIndex].GIOITINH.Value]) + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + (hocViens[iIndex].NGAYSINH == null ? null : hocViens[iIndex].NGAYSINH.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "           <td style='vertical-align: middle;'>" + (tinhThanh == null ? null : tinhThanh.Ten) + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + hocViens[iIndex].CMTND + "</td> \r\n" +
                        "           <td style='text-align: center; vertical-align: middle;'>" + hocViens[iIndex].DIENTHOAI + "</td> \r\n" +
                        "           <td style='vertical-align: middle;'>" + (donViCongTac == null ? null : donViCongTac.Ten) + "</td> \r\n" +
                        "       </tr> \r\n";
                }
                html += "   </tbody> \r\n" +
                        "</table> \r\n";
                RetAjaxOut.RetObject = hocViens.Count();
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                if (ketQuaDaoTao != null)
                    return DT_KetQuaDaoTaoParser.sColorTrangThai[ketQuaDaoTao.TRANGTHAI];
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return null;
        }
        #endregion Vẽ giao diện
        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetBoDemValueAndMaHV(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string boDemValue = null;
            string maHV = null;
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "DT_HocVien", FieldName = "Ma" }).FirstOrDefault();
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
        public static AjaxOut AddUser(RenderInfoCls ORenderInfo, string LoginName, string FullName, string Password, string RePasword, string TitleId, string DepartmentId, /*string RoleIds,*/
           /* string MenuTemplateId,*/ string LanguageId, string Mobile, string Email, /*string DashboardId, string OwnerUserText,*/ int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                if (string.IsNullOrEmpty(LoginName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã người dùng không hợp lệ"));
                if (string.IsNullOrEmpty(FullName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên người dùng không hợp lệ"));
                if (string.IsNullOrEmpty(Password)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mật khẩu chưa nhập"));
                if (Password.Equals(RePasword) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mật khẩu nhập lại không phù hợp"));
                //if (string.IsNullOrEmpty(MenuTemplateId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn menu chức năng"));
                //if (string.IsNullOrEmpty(DashboardId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn dashboard chức năng"));


                //string[] ItemRoleIds = FunctionUtility.GetMultiComboboxValue(RoleIds);
                //[] ItemOwnerIds = FunctionUtility.GetMultiComboboxValue(OwnerUserText);

                //if (ItemRoleIds.Length == 0) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Cần chọn ít nhất một vai trò"));
                //if (ItemOwnerIds.Length == 0) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Cần chọn ít nhất 1 đơn vị sử dụng"));

                OwnerUserCls
                    OOwnerUser = new OwnerUserCls();
                OOwnerUser.OwnerUserId = System.Guid.NewGuid().ToString();
                OOwnerUser.LoginName = LoginName;
                OOwnerUser.FullName = FullName;
                OOwnerUser.frkOwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
               // OOwnerUser.frkMenuTemplateId = MenuTemplateId;
                OOwnerUser.Password = Password;
                OOwnerUser.frkTitleId = TitleId;
                OOwnerUser.frkDepartmentId = DepartmentId;
                //OOwnerUser.RoleIds = ItemRoleIds;
                OOwnerUser.frkLanguageId = LanguageId;
                OOwnerUser.Mobile = Mobile;
                OOwnerUser.Email = Email;
                //OOwnerUser.frkDashboardId = DashboardId;
                OOwnerUser.Active = Active;
                //OOwnerUser.ItemOwnerIds = ItemOwnerIds;
                OOwnerUser.WorkingOwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;//quan trong vi xem dang thao tac site nao

                CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Add(ORenderInfo, OOwnerUser);
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
        public static AjaxOut GetThoiLuongKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                RetAjaxOut.RetObject = khoaHoc == null ? null : khoaHoc.THOILUONG + (string.IsNullOrEmpty(khoaHoc.LOAITHOILUONG) ? null : " " + DT_KhoaHocParser.LoaiThoiLuongs[khoaHoc.LOAITHOILUONG]);
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
        public static AjaxOut DrawTieuChuanKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId, string ketQuaDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = string.IsNullOrEmpty(ketQuaDaoTaoId) ? new DT_KetQuaDaoTaoCls() : CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                string[] tieuChuanDats = string.IsNullOrEmpty(ketQuaDaoTao.TIEUCHUAN) ? new string[0] : ketQuaDaoTao.TIEUCHUAN.Split('|');
                if (khoaHoc != null && !string.IsNullOrEmpty(khoaHoc.TIEUCHUAN))
                {
                    string[] tieuChuans = khoaHoc.TIEUCHUAN.Split('|');
                    string html = "";
                    foreach (string tieuChuan in tieuChuans)
                    {
                        DM_TieuChuanThamGiaKhoaHocCls tc = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ORenderInfo, tieuChuan);
                        html += string.Format("<div style='margin-right: 20px; display: inline;'><input type=\"checkbox\" id='{0}' class='clsTieuChuan' style='display: inline;' {1} > {2}</div>\r\n", tieuChuan, tieuChuanDats.Contains(tieuChuan) ? "checked" : null, tc == null ? null : tc.Ten);
                    }
                    RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetHocVien(RenderInfoCls ORenderInfo, string ma)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, ma);
                if (hocVien != null)
                {

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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveDangKy(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId, string ngayDangKy, string khoaHocId, string[] tieuChuan, int? nopHocPhi, string ndAnh, string ma, string hoTen, int? gioiTinh, string danToc, string ngaySinh, string noiSinh,
            string cmnd, string ngayCapCMND, string noiCapCMND, string khoaPhongCongTac, string donViCongTac, string chuyenMon, int? soNamKinhNghiem, string diaChiLienHe, string diaChiHanhChinh, string dienThoai, string email, string totNghiep, int? namTotNghiep, string truongCapBang, string chuyenNganh,
            string functionName)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = new DT_KetQuaDaoTaoCls();
                DT_KhoaHocCls khoaHoc = new DT_KhoaHocCls();
                DT_HocVienCls hocVien = new DT_HocVienCls();
                if (!string.IsNullOrEmpty(ketQuaDaoTaoId))
                    ketQuaDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                if (!string.IsNullOrEmpty(khoaHocId))
                    khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                //Nếu học viên đã được xác định khi đăng ký thì kiểm tra mã trùng trong DB
                if (!string.IsNullOrEmpty(ketQuaDaoTao.HOCVIEN_ID))
                {
                    hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, ketQuaDaoTao.HOCVIEN_ID);
                }
                //Nếu chưa xác định học viên khi đăng ký thì nếu đã tồn tại học viên có mã được nhập thì lấy luôn học viên đó để đăng ký, nếu chưa thì tạo mới học viên và gắn vào đăng ký
                else
                {
                    hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, ma);
                    if (hocVien == null)
                    {
                        hocVien = new DT_HocVienCls() { MA = ma };
                        string roleId = WebConfig.GetWebConfig("HocVienRoleId");
                        //string menuTemplateId = WebConfig.GetWebConfig("HocVienMenuTemplateId");
                        string dashboardId = WebConfig.GetWebConfig("HocVienDashboardId");
                        string ownerId = WebConfig.GetWebConfig("HocVienOwnerId");
                        OwnerCls owner = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, ownerId);
                        hocVien.USERNAME = ma;
                        if (owner != null)
                            hocVien.USERNAME = owner.OwnerCode + "." + ma;
                        AjaxOut addHocVienResult = AddUser(ORenderInfo, hocVien.USERNAME, hoTen, "1", "1", null, null,/* "[" + roleId + "]",*/ /*menuTemplateId,*/ "vi", dienThoai, email, /*dashboardId,*/ /*"[" + ownerId + "]",*/ 1);
                        if (addHocVienResult.Error)
                        {
                            RetAjaxOut.Error = true;
                            RetAjaxOut.InfoMessage = addHocVienResult.InfoMessage;
                            return RetAjaxOut;
                        }
                    }
                    else
                    {
                        if (CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Reading(ORenderInfo, new DT_KetQuaDaoTaoFilterCls() { KhoaHocDangKy_Id = khoaHocId, HocVien_Id = hocVien.ID }).Count() > 0)
                        {
                            RetAjaxOut.Error = true;
                            RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Học viên") + " " + hoTen + " " + WebLanguage.GetLanguage(OSiteParam, "đã đăng ký khóa học này rồi.");
                            return RetAjaxOut;
                        }
                        khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                        if (khoaHoc.NGAYKHAIGIANGDUKIEN != null && khoaHoc.NGAYBEGIANGDUKIEN != null && CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().IsTrungThoiGianHoc(ORenderInfo, hocVien.ID, khoaHoc.NGAYKHAIGIANGDUKIEN.Value, khoaHoc.NGAYBEGIANGDUKIEN.Value, ketQuaDaoTaoId) == true)
                        {
                            RetAjaxOut.Error = true;
                            RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Học viên") + " " + hoTen + " " + WebLanguage.GetLanguage(OSiteParam, "đã đăng ký khóa học khác trùng với thời gian của khóa học này.");
                            return RetAjaxOut;
                        }                      
                    }
                }
                if (DateTime.ParseExact(ngayDangKy, "dd/MM/yyyy", null) > khoaHoc.HANNOPHOSO )
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian đăng ký đã quá hạn nộp hồ sơ?");
                    return RetAjaxOut;
                }
                string ho = "", ten = "";
                if (!string.IsNullOrEmpty(hoTen))
                {
                    hoTen = hoTen.TrimStart().TrimEnd();
                    int index = hoTen.LastIndexOf(' ');
                    if (index > -1 && index < hoTen.Length - 1)
                    {
                        ho = hoTen.Substring(0, index);
                        ten = hoTen.Substring(index + 1);
                    }
                    else ten = hoTen;
                }
                #region Thêm mới/cập nhật học viên
                if (string.IsNullOrEmpty(hocVien.ID))
                {
                    hocVien.ID = System.Guid.NewGuid().ToString();
                    hocVien.NOIDUNGANH = ndAnh;
                    hocVien.TEN = ten;
                    hocVien.HOTEN = hoTen;
                    hocVien.GIOITINH = gioiTinh;
                    hocVien.DANTOC_MA = danToc;
                    hocVien.NGAYSINH = string.IsNullOrWhiteSpace(ngaySinh) ? null : (DateTime?)DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", null);
                    hocVien.NOISINH_MA = noiSinh;
                    hocVien.CMTND = cmnd;
                    hocVien.CMTND_NGAYCAP = string.IsNullOrWhiteSpace(ngayCapCMND) ? null : (DateTime?)DateTime.ParseExact(ngayCapCMND, "dd/MM/yyyy", null);
                    hocVien.CMTND_NOICAP = noiCapCMND;
                    hocVien.KHOAPHONGCONGTAC = khoaPhongCongTac;
                    hocVien.DONVICONGTAC_MA = donViCongTac;
                    hocVien.CHUYENMON_MA = chuyenMon;
                    hocVien.SONAMKINHNGHIEM = soNamKinhNghiem;
                    hocVien.DIACHISONHA = diaChiLienHe;
                    hocVien.DIACHIHANHCHINH_MA = diaChiHanhChinh;
                    hocVien.DIENTHOAI = dienThoai;
                    hocVien.EMAIL = email;
                    hocVien.TOTNGHIEP_MA = totNghiep;
                    hocVien.NAMTOTNGHIEP = namTotNghiep;
                    hocVien.TRUONGCAPBANG = truongCapBang;
                    hocVien.CHUYENNGANH_MA = chuyenNganh;
                    hocVien.NGAYTAO = DateTime.Now;
                    hocVien.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Add(ORenderInfo, hocVien);
                }
                else
                {
                    hocVien.NOIDUNGANH = ndAnh;
                    hocVien.TEN = ten;
                    hocVien.HOTEN = hoTen;
                    hocVien.GIOITINH = gioiTinh;
                    hocVien.DANTOC_MA = danToc;
                    hocVien.NGAYSINH = string.IsNullOrWhiteSpace(ngaySinh) ? null : (DateTime?)DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", null);
                    hocVien.NOISINH_MA = noiSinh;
                    hocVien.CMTND = cmnd;
                    hocVien.CMTND_NGAYCAP = string.IsNullOrWhiteSpace(ngayCapCMND) ? null : (DateTime?)DateTime.ParseExact(ngayCapCMND, "dd/MM/yyyy", null);
                    hocVien.CMTND_NOICAP = noiCapCMND;
                    hocVien.KHOAPHONGCONGTAC = khoaPhongCongTac;
                    hocVien.DONVICONGTAC_MA = donViCongTac;
                    hocVien.CHUYENMON_MA = chuyenMon;
                    hocVien.SONAMKINHNGHIEM = soNamKinhNghiem;
                    hocVien.DIACHISONHA = diaChiLienHe;
                    hocVien.DIACHIHANHCHINH_MA = diaChiHanhChinh;
                    hocVien.DIENTHOAI = dienThoai;
                    hocVien.EMAIL = email;
                    hocVien.TOTNGHIEP_MA = totNghiep;
                    hocVien.NAMTOTNGHIEP = namTotNghiep;
                    hocVien.TRUONGCAPBANG = truongCapBang;
                    hocVien.CHUYENNGANH_MA = chuyenNganh;
                    hocVien.NGAYSUA = DateTime.Now;
                    hocVien.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Save(ORenderInfo, hocVien.ID, hocVien);
                }
                #endregion Thêm mới/cập nhật học viên

                #region Thêm mới/cập nhật đăng ký
                if (string.IsNullOrEmpty(ketQuaDaoTaoId))
                {
                    ketQuaDaoTao.ID = ketQuaDaoTaoId = System.Guid.NewGuid().ToString();
                    ketQuaDaoTao.NGAYDANGKY = string.IsNullOrWhiteSpace(ngayDangKy) ? null : (DateTime?)DateTime.ParseExact(ngayDangKy, "dd/MM/yyyy", null);
                    ketQuaDaoTao.KHOAHOCDANGKY_ID = khoaHocId;
                    ketQuaDaoTao.TIEUCHUAN = string.Join("|", tieuChuan);
                    ketQuaDaoTao.HOCVIEN_ID = hocVien.ID;
                    ketQuaDaoTao.NOPHOCPHI = nopHocPhi;
                    ketQuaDaoTao.TRANGTHAI = (int)DT_KetQuaDaoTaoCls.eTrangThai.Moi;
                    ketQuaDaoTao.NGAYTAO = DateTime.Now;
                    ketQuaDaoTao.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Add(ORenderInfo, ketQuaDaoTao);
                    RetAjaxOut.RetExtraParam1 = ketQuaDaoTao.ID;
                }
                else
                {
                    ketQuaDaoTao.NGAYDANGKY = string.IsNullOrWhiteSpace(ngayDangKy) ? null : (DateTime?)DateTime.ParseExact(ngayDangKy, "dd/MM/yyyy", null);
                    ketQuaDaoTao.KHOAHOCDANGKY_ID = khoaHocId;
                    ketQuaDaoTao.TIEUCHUAN = string.Join("|", tieuChuan);
                    ketQuaDaoTao.HOCVIEN_ID = hocVien.ID;
                    ketQuaDaoTao.NOPHOCPHI = nopHocPhi;
                    ketQuaDaoTao.NGAYSUA = DateTime.Now;
                    ketQuaDaoTao.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Save(ORenderInfo, ketQuaDaoTao.ID, ketQuaDaoTao);
                }
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
                #endregion Thêm mới/cập nhật đăng ký

                #region Thêm mới/cập nhật văn bằng
                List<DT_VanBangCls> newVanBangs = WebSessionUtility.GetSession(OSiteParam, "DT_DangKy_VanBangs") as List<DT_VanBangCls>;
                List<DT_VanBangCls> oldVanBangs = CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Reading(ORenderInfo, new DT_VanBangFilterCls() { HOCVIEN_ID = hocVien.ID }).ToList();
                foreach (var oldVanBang in oldVanBangs)
                {
                    bool isExists = false;
                    foreach (var newVanBang in newVanBangs)
                    {
                        if (newVanBang.ID == oldVanBang.ID)//cập nhật
                        {
                            oldVanBang.HOCVIEN_ID = hocVien.ID;
                            oldVanBang.TEN = newVanBang.TEN;
                            oldVanBang.DONVICAP = newVanBang.DONVICAP;
                            oldVanBang.NAM = newVanBang.NAM;
                            CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Save(ORenderInfo, oldVanBang.ID, oldVanBang);
                            OneTSQ.WebRender.WebScreenProcessBll.ServerSideAddActivityHistory(ORenderInfo, ketQuaDaoTaoId, functionName, WebLanguage.GetLanguage(ORenderInfo, "Cập nhật văn bằng ") + newVanBang.TEN, WebLanguage.GetLanguage(ORenderInfo, "Tác vụ form"));
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Delete(ORenderInfo, oldVanBang.ID);
                        OneTSQ.WebRender.WebScreenProcessBll.ServerSideAddActivityHistory(ORenderInfo, ketQuaDaoTaoId, functionName, WebLanguage.GetLanguage(ORenderInfo, "Xóa văn bằng ") + oldVanBang.TEN, WebLanguage.GetLanguage(ORenderInfo, "Tác vụ form"));
                    }
                }
                var addVanBangs = newVanBangs.Where(o => !oldVanBangs.Any(old => old.ID == o.ID));
                foreach (var addVanBang in addVanBangs)//Thêm mới
                {
                    addVanBang.HOCVIEN_ID = hocVien.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Add(ORenderInfo, addVanBang);
                   OneTSQ.WebRender.WebScreenProcessBll.ServerSideAddActivityHistory(ORenderInfo, ketQuaDaoTaoId, functionName, WebLanguage.GetLanguage(ORenderInfo, "Thêm mới văn bằng ") + addVanBang.TEN, WebLanguage.GetLanguage(ORenderInfo, "Tác vụ form"));
                }
                #endregion Thêm mới/cập nhật văn bằng
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
        public static AjaxOut DeleteDangKy(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls OKQDT = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                if(OKQDT != null)
                {
                    DT_LichThucHanhCls OLichThucHanh = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().CreateModel(ORenderInfo, OKQDT.KHOAHOCDUYET_ID);
                    if(OLichThucHanh != null)
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().DeleteHocVien(ORenderInfo, OLichThucHanh.ID, OKQDT.HOCVIEN_ID);
                }    
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Delete(ORenderInfo, ketQuaDaoTaoId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                string dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_DangKies", new WebParamCls[] { });
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
        public static AjaxOut ChekLopHoc(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls OKQDT = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                if (OKQDT != null)
                {
                    DT_KhoaHocCls OKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, OKQDT.KHOAHOCDUYET_ID);
                    if (OKhoaHoc != null)
                    {
                        RetAjaxOut.RetExtraParam1 = OKhoaHoc.TRANGTHAI.ToString();
                    }
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveVanBang(RenderInfoCls ORenderInfo, string vanBangId, string tenVanBang, string donViCapVanBang, int? namCapVanBang)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_VanBangCls> vanBangs = WebSessionUtility.GetSession(OSiteParam, "DT_DangKy_VanBangs") as List<DT_VanBangCls>;
                if (vanBangs.Any(o => o.TEN == tenVanBang && o.ID != vanBangId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Văn bằng/chứng chỉ này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(vanBangId))//thêm mới
                {
                    vanBangs.Add(new DT_VanBangCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        TEN = tenVanBang,
                        DONVICAP = donViCapVanBang,
                        NAM = namCapVanBang
                    });
                }
                else//cập nhật
                {
                    DT_VanBangCls vanBang = vanBangs.FirstOrDefault(o => o.ID == vanBangId);
                    if (vanBang == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "văn bằng/chứng chỉ này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    vanBang.TEN = tenVanBang;
                    vanBang.DONVICAP = donViCapVanBang;
                    vanBang.NAM = namCapVanBang;
                }
                RetAjaxOut.HtmlContent = DrawVanBangs(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteVanBang(RenderInfoCls ORenderInfo, string vanBangId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_VanBangCls> vanBangs = WebSessionUtility.GetSession(OSiteParam, "DT_DangKy_VanBangs") as List<DT_VanBangCls>;
                DT_VanBangCls vanBang = vanBangs.FirstOrDefault(o => o.ID == vanBangId);
                vanBangs.Remove(vanBang);
                RetAjaxOut.HtmlContent = DrawVanBangs(ORenderInfo).HtmlContent;
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
