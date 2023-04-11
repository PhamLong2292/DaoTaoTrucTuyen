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
using System.IO;
using System.Web;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_LopHoc : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DT_LopHoc";
        public override string WebPartTitle { get { return "Lớp học"; } }
        public override string Description { get { return "Lớp học"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                DT_LopHocs lopHoc = new DT_LopHocs();
                return new string[] { StaticWebPartId, lopHoc.WebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_LopHoc), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string maDonViTuVanDefault = WebConfig.GetWebConfig("HospitalCode");
                string khoaHocId = WebEnvironments.Request("id");
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                if (khoaHoc == null)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Lớp học này vừa bị xóa khỏi hệ thống."), false);
                    return RetAjaxOut;
                }
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHoc.ID });
                string SessionId = System.Guid.NewGuid().ToString();
                bool qlKeHoachPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.QuanLyKeHoach.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool qlTaiLieuPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.QuanLyTaiLieu.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool diemDanhPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.DiemDanh.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool nhapKqDtPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.NhapKetQuaDaoTao.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool ketThucPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.KetThuc.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);

                OneMES3.DM.Model.BenhVienCls donViHoTro = null;
                if (!string.IsNullOrEmpty(khoaHoc.DONVIHOTRO_MA))
                {
                    donViHoTro = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), khoaHoc.DONVIHOTRO_MA);
                }
                var DT_DaoTaoTrucTuyenUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_DaoTaoTrucTuyen", new WebParamCls[] { new WebParamCls("id", khoaHocId) });
                string doiTuong = "";
                string[] doiTuongs;
                if (string.IsNullOrEmpty(khoaHoc.DOITUONG))
                    doiTuongs = new string[0];
                else doiTuongs = khoaHoc.DOITUONG.Split('|');
                foreach (string dt in doiTuongs)
                    doiTuong += DT_KhoaHocParser.DoiTuongs[Int32.Parse(dt)] + ", ";
                if (!string.IsNullOrEmpty(doiTuong))
                    doiTuong = doiTuong.Substring(0, doiTuong.Length - 2);
                #region Html
                string html =
                    "<div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "   <div style='float:left; padding:0;' class='col-md-11'> \r\n" +
                    "    <input type='button' id='btnKetThuc' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Kết thúc") + "' onclick='javascript:KetThuc();' style='float:left; margin-left: 20px; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet /*&& ketThucPermission*/ ? null : "display:none;") + "'>\r\n" +
                    "    <input type='button' id='btnThuHoiKetThuc' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi kết thúc") + "' onclick='javascript:ThuHoiKetThuc();' style='float:left; margin-left: 20px; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.KetThuc /*&& ketThucPermission */? null : "display:none;") + "'>\r\n" +
                    "    <input type='button' id='btnVaoLop' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Vào lớp") + "' onclick=\"javascript:window.open('" + DT_DaoTaoTrucTuyenUrl + "');\" style='float:left; margin-left: 20px;" + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet ? null : "display:none;") + "'>\r\n" +
                    "   </div> \r\n" +
                    "   <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-1' > \r\n" +
                    "   </div> \r\n" +
                    "</div>\r\n" +
                    "<div class='row'>\r\n" +
                    "   <input type='hidden' id='hdKhoaHocId' value='" + khoaHocId + "'>\r\n" +
                    "   <div class='col-lg-12'>\r\n" +
                    "      <div class='ibox float-e-margins'>\r\n" +
                    "          <div class='ibox-title'>\r\n" +
                    "              <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin lớp học") + "</h5>\r\n" +
                    "          </div>\r\n" +
                    "          <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Mã lớp học:") + " \r\n" +
                    "                          <span id='spMa' class=\"valueForm\">" + khoaHoc.MA + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Tên lớp học:") + " \r\n" +
                    "                          <span id='spTen' class=\"valueForm\">" + khoaHoc.TENKHOAHOC + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Thời lượng học:") + " \r\n" +
                    "                          <span id='spThoiLuong' class=\"valueForm\">" + (khoaHoc.THOILUONG + (string.IsNullOrEmpty(khoaHoc.LOAITHOILUONG) ? null : " " + DT_KhoaHocParser.LoaiThoiLuongs[khoaHoc.LOAITHOILUONG])) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Ngày khai giảng dự kiến:") + " \r\n" +
                    "                          <span id='spNgayKhaiGiangDuKien' class=\"valueForm\">" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Số lượng học viên:") + " \r\n" +
                    "                          <span id='spSoLuongHocVienDuKien' class=\"valueForm\">" + hocViens.Count() + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Học phí:") + " \r\n" +
                    "                          <span id='spHocPhi' class=\"valueForm\">" + (khoaHoc.HOCPHI == null ? null : khoaHoc.HOCPHI.Value.ToString("#,##0.00", new CultureInfo("en-US"))) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Loại khóa học:") + " \r\n" +
                    "                          <span id='spLoaiKhoaHoc' class=\"valueForm\">" + DT_KhoaHocParser.LoaiKhoaHocs[khoaHoc.LOAIKHOAHOC] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Đơn vị nhận hỗ trợ:") + " \r\n" +
                    "                          <span id='spDonViHoTro' class=\"valueForm\">" + (donViHoTro == null ? khoaHoc.DONVIHOTRO_MA : donViHoTro.Ten) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Loại đào tạo:") + " \r\n" +
                    "                          <span id='spLoaiDaoTao' class=\"valueForm\">" + DT_KhoaHocParser.LoaiDaoTaos[khoaHoc.LOAIDAOTAO] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Loại hình đào tạo:") + " \r\n" +
                    "                          <span id='spLoaiHinhDaoTao' class=\"valueForm\">" + DT_KhoaHocParser.LoaiHinhDaoTaos[khoaHoc.LOAIHINHDAOTAO] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                               WebLanguage.GetLanguage(OSiteParam, "Đối tượng:") + " \r\n" +
                    "                          <span id='spDoiTuong' class=\"valueForm\">" + doiTuong + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='tabs-container'>\r\n" +
                    "                   <ul class='nav nav-tabs'>\r\n" +
                    "                       <li class='active'><a data-toggle='tab' href='#tab-1'>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách học viên") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-2'>" + WebLanguage.GetLanguage(OSiteParam, "Kế hoạch đào tạo") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-3'>" + WebLanguage.GetLanguage(OSiteParam, "Lịch học lý thuyết") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-4'>" + WebLanguage.GetLanguage(OSiteParam, "Lịch học thực hành") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-5'>" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh lý thuyết") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-6'>" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh thực hành") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-7'>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu") + "</a></li>\r\n" +
                    "                   </ul>\r\n" +
                    "                   <div class='tab-content'>\r\n" +
                    "                       <div id='tab-1' class='tab-pane active'>\r\n" +
                    "                           <div class='row' id='btnNhapKetQuaDaoTao' style='text-align: center; font-weight:bold; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && nhapKqDtPermission ? null : "display:none;") + "'><a href='javascript:PopupNhapKetQuaDaoTao();' title='" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật kết quả đào tạo.") + "'>" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật kết quả đào tạo") + "</a></div>\r\n" +
                    "                           <div class='row' id='divHocViens'>\r\n" +
                                                    DrawHocViens(ORenderInfo, khoaHoc.ID, false).HtmlContent +
                    "                           </div>\r\n" +
                    "                       </div>\r\n" +
                    "                       <div id='tab-2' class='tab-pane'>\r\n" +
                    "                           <div class='row' id='btnSuaKeHoachLop' style='text-align: center; font-weight:bold; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && qlKeHoachPermission ? null : "display:none;") + "'><a href='javascript:PopupKeHoachLop();' title='" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật kế hoạch đào tạo.") + "'>" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật") + "</a></div>\r\n" +
                    "                           <div class='row' id='divKeHoachLop'>\r\n" +
                                                    DrawKeHoachLop(ORenderInfo, khoaHoc.ID).HtmlContent +
                    "                           </div>\r\n" +
                    "                       </div>\r\n" +
                    "                       <div id='tab-3' class='tab-pane'>\r\n" +
                    "                           <div class='row' id='btnSuaLichHocLt' style='text-align: center; font-weight:bold; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && qlKeHoachPermission ? null : "display:none;") + "'><a href='javascript:PopupLichHocLt();' title='" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật lịch học lý thuyết.") + "'>" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật") + "</a></div>\r\n" +
                    "                           <div class='row' id='divLichHocLt'>\r\n" +
                                                    DrawLichHocLt(ORenderInfo, khoaHoc.ID).HtmlContent +
                    "                           </div>\r\n" +
                    "                       </div>\r\n" +
                    "                       <div id='tab-4' class='tab-pane'>\r\n" +
                                                DrawLichThucHanhs(ORenderInfo, khoaHoc.ID).HtmlContent +
                    "                       </div>\r\n" +
                    "                       <div id='tab-5' class='tab-pane'>\r\n" +
                    "                           <div class='row' id='btnDiemDanhLt' style='text-align: center; font-weight:bold; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && diemDanhPermission ? null : "display:none;") + "'><a href='javascript:PopupDiemDanhLt();' title='" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật bảng điểm danh lý thuyết.") + "'>" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật") + "</a></div>\r\n" +
                    "                           <div class='row' id='divDiemDanhLt'>\r\n" +
                                                    DrawDiemDanhLt(ORenderInfo, khoaHoc.ID).HtmlContent +
                    "                           </div>\r\n" +
                    "                       </div>\r\n" +
                    "                       <div id='tab-6' class='tab-pane'>\r\n" +
                                                DrawDiemDanhTh(ORenderInfo, khoaHoc.ID).HtmlContent +
                    "                       </div>\r\n" +
                    "                       <div id='tab-7' class='tab-pane'>\r\n" +
                    "                           <div class='row' id='btnQuanLyTaiLieu' style='text-align: center; font-weight:bold; " + (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && qlTaiLieuPermission ? null : "display:none;") + "'><a href='javascript:PopupTaiLieu();' title='" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật tài liệu.") + "'>" + WebLanguage.GetLanguage(ORenderInfo, "Cập nhật") + "</a></div>\r\n" +
                    "                           <div class='row' id='divTaiLieus'>\r\n" +
                                                    DrawTaiLieus(ORenderInfo, khoaHoc.ID, false).HtmlContent +
                    "                           </div>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n" +
                    "</div>\r\n" +
                #region Phần hiển thị popup
                    "<div id=\"divFormModal\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                    "    <div class=\"modal-dialog\" style='width:90%; height:90%;'>\r\n" +
                    "       <div class=\"modal-content\" style='width:100%; height:100%; position: relative;'>\r\n" +
                    "           <div class=\"panel-heading\">\r\n" +
                    "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\" onclick='ClosePopup()'>&times;</button>\r\n" +
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
                    "   var khoaHocId='" + khoaHocId + "';\r\n" +
                    "   var currentPopup = '';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "       $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "   });\r\n" +
                #region Truyền id biên bản hội chẩn cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   return khoaHocId;\r\n" +
                    "}\r\n" +
                #endregion Truyền id biên bản hội chẩn cho nút print
                #region Kết thúc khóa học
                    "   function KetThuc(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn kết thúc khóa học này?\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.UpdateTrangThaiKhoaHoc(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.KetThuc + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã kết thúc khóa học!") + "');\r\n" +
                    "                   $('#btnKetThuc').hide();\r\n" +
                    "                   $('#btnThuHoiKetThuc').show();\r\n" +
                    "                   if('" + qlKeHoachPermission + "' == 'True'){\r\n" +
                    "                       $('#btnSuaKeHoachLop').hide();\r\n" +
                    "                       $('#btnSuaLichHocLt').hide();\r\n" +
                    //"                       $('#btnSuaLichHocTh').hide();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + diemDanhPermission + "' == 'True'){\r\n" +
                    "                       $('#btnDiemDanhLt').hide();\r\n" +
                    //"                       $('#btnDiemDanhTh').hide();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + nhapKqDtPermission + "' == 'True'){\r\n" +
                    "                       $('#btnNhapKetQuaDaoTao').hide();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + qlTaiLieuPermission + "' == 'True'){\r\n" +
                    "                       $('#btnQuanLyTaiLieu').hide();\r\n" +
                    "                   }\r\n" +
                    "                   $('#btnVaoLop').hide();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion Kết thúc khóa học
                #region Thu hồi kết thúc khóa học
                    "   function ThuHoiKetThuc(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi kết thúc khóa học này?\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.UpdateTrangThaiKhoaHoc(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.Duyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi kết thúc khóa học!") + "');\r\n" +
                    "                   $('#btnKetThuc').show();\r\n" +
                    "                   $('#btnThuHoiKetThuc').hide();\r\n" +
                    "                   if('" + qlKeHoachPermission + "' == 'True'){\r\n" +
                    "                       $('#btnSuaKeHoachLop').show();\r\n" +
                    "                       $('#btnSuaLichHocLt').show();\r\n" +
                    //"                       $('#btnSuaLichHocTh').show();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + diemDanhPermission + "' == 'True'){\r\n" +
                    "                       $('#btnDiemDanhLt').show();\r\n" +
                    //"                       $('#btnDiemDanhTh').show();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + nhapKqDtPermission + "' == 'True'){\r\n" +
                    "                       $('#btnNhapKetQuaDaoTao').show();\r\n" +
                    "                   }\r\n" +
                    "                   if('" + qlTaiLieuPermission + "' == 'True'){\r\n" +
                    "                       $('#btnQuanLyTaiLieu').show();\r\n" +
                    "                   }\r\n" +
                    "                   $('#btnVaoLop').show();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion Thu hồi kết thúc khóa học
                #region Show popup cập nhật kế hoạch đào tạo
                    "function PopupKeHoachLop()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupKeHoachLop(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật kế hoạch đào tạo") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datetimepicker').datetimepicker({ \r\n" +
                    "         format: 'HH:mm DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     $('.timepicker').datetimepicker({ \r\n" +
                    "         format: 'HH:mm' \r\n" +
                    "     }); \r\n" +
                    "     ValidateIntegerControl('#txtSoLuongNhomTh', 0, 99);\r\n" +
                    "     ValidateIntegerControl('#txtSoHvTrongNhomTh', 0, 99);\r\n" +
                    "     CallInitSelect2('cbbLanhDao','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "}\r\n" +
                #endregion Show popup cập nhật kế hoạch đào tạo
                #region Save Thông tin kế hoạch đào tạo
                    "function SaveKeHoachLop()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     batDau = document.getElementById('dtBatDau').value;\r\n" +
                    "     ketThuc = document.getElementById('dtKetThuc').value;\r\n" +
                    "     thoiGianTiepDon = document.getElementById('dtThoiGianTiepDon').value;\r\n" +
                    "     diaDiemTiepDon = document.getElementById('txtDiaDiemTiepDon').value;\r\n" +
                    "     batDauLt = document.getElementById('dtBatDauLt').value;\r\n" +
                    "     ketThucLt = document.getElementById('dtKetThucLt').value;\r\n" +
                    "     diaDiemLt = document.getElementById('txtDiaDiemLt').value;\r\n" +
                    "     batDauTh = document.getElementById('dtBatDauTh').value;\r\n" +
                    "     ketThucTh = document.getElementById('dtKetThucTh').value;\r\n" +
                    "     diaDiemTh = document.getElementById('txtDiaDiemTh').value;\r\n" +
                    "     soLuongNhomTh = parseInt(document.getElementById('txtSoLuongNhomTh').value);\r\n" +
                    "     soHvTrongNhomTh = parseInt(document.getElementById('txtSoHvTrongNhomTh').value);\r\n" +
                    "     thoiGianDanhGiaTdt = document.getElementById('dtThoiGianDanhGiaTdt').value;\r\n" +
                    "     diaDiemDanhGiaTdt = document.getElementById('txtDiaDiemDanhGiaTdt').value;\r\n" +
                    "     thoiGianGiaiDapThacMac = document.getElementById('dtThoiGianGiaiDapThacMac').value;\r\n" +
                    "     diaDiemGiaiDapThacMac = document.getElementById('txtDiaDiemGiaiDapThacMac').value;\r\n" +
                    "     batDauThiLt = document.getElementById('dtBatDauThiLt').value;\r\n" +
                    "     ketThucThiLt = document.getElementById('dtKetThucThiLt').value;\r\n" +
                    "     diaDiemThiLt = document.getElementById('txtDiaDiemThiLt').value;\r\n" +
                    "     batDauThiVd = document.getElementById('dtBatDauThiVd').value;\r\n" +
                    "     ketThucThiVd = document.getElementById('dtKetThucThiVd').value;\r\n" +
                    "     diaDiemThiVd = document.getElementById('txtDiaDiemThiVd').value;\r\n" +
                    "     batDauThiTh = document.getElementById('dtBatDauThiTh').value;\r\n" +
                    "     ketThucThiTh = document.getElementById('dtKetThucThiTh').value;\r\n" +
                    "     diaDiemThiTh = document.getElementById('txtDiaDiemThiTh').value;\r\n" +
                    "     thoiGianBeGiang = document.getElementById('dtThoiGianBeGiang').value;\r\n" +
                    "     diaDiemBeGiang = document.getElementById('txtDiaDiemBeGiang').value;\r\n" +
                    "     lanhDao = document.getElementById('cbbLanhDao').value;\r\n" +
                    "     nguoiLap = document.getElementById('txtNguoiLap').value;\r\n" +
                    "     if(batDauThiLt != '' && ketThucThiLt == '')\r\n" +
                    "     {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa nhập thời gian kết thúc thi lý thuyêt.") + "');\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(batDauThiVd != '' && ketThucThiVd == '')\r\n" +
                    "     {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa nhập thời gian kết thúc thi vấn đáp.") + "');\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(batDauThiTh != '' && ketThucThiTh == '')\r\n" +
                    "     {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa nhập thời gian kết thúc thi thực hành.") + "');\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveKeHoachLop(RenderInfo, khoaHocId, batDau, ketThuc, thoiGianTiepDon, diaDiemTiepDon, batDauLt, ketThucLt, diaDiemLt, batDauTh, ketThucTh, diaDiemTh, soLuongNhomTh, soHvTrongNhomTh, thoiGianDanhGiaTdt, " +
                    "diaDiemDanhGiaTdt, thoiGianGiaiDapThacMac, diaDiemGiaiDapThacMac, batDauThiLt, ketThucThiLt, diaDiemThiLt, batDauThiVd, ketThucThiVd, diaDiemThiVd, batDauThiTh, ketThucThiTh, diaDiemThiTh, thoiGianBeGiang, diaDiemBeGiang, lanhDao, nguoiLap).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawKeHoachLop(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#divKeHoachLop').html(AjaxOut.HtmlContent);\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin kế hoạch đào tạo

                #region Show popup cập nhật lịch học lý thuyết
                    "function PopupLichHocLt()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupLichHocLt(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật lịch học lý thuyết") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbPtChuyenMon','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "     CallInitSelect2('cbbLanhDao','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "}\r\n" +
                #endregion Show popup cập nhật lịch học lý thuyết
                #region Save Thông tin lịch học lý thuyết
                    "function SaveLichHocLt()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     diaDiem = document.getElementById('txtDiaDiem').value;\r\n" +
                    "     batDau = document.getElementById('dtBatDau').value;\r\n" +
                    "     ketThuc = document.getElementById('dtKetThuc').value;\r\n" +
                    "     ptChuyenMon = document.getElementById('cbbPtChuyenMon').value;\r\n" +
                    "     lanhDao = document.getElementById('cbbLanhDao').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveLichLyThuyet(RenderInfo, khoaHocId, diaDiem, batDau, ketThuc, ptChuyenMon, lanhDao).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawLichHocLt(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#divLichHocLt').html(AjaxOut.HtmlContent);\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawDiemDanhLt(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#divDiemDanhLt').html(AjaxOut.HtmlContent);\r\n" +
                    //"          $('#divTableDiemDanhLt').freezeTable({'columnNum' : 3, 'scrollable': true,});\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin lịch học lý thuyết
                #region Hiển thị row thêm mới lịch học lý thuyết chi tiết
                    "   function ShowLichLyThuyetChiTiet(){\r\n" +
                    "       $('.CssEditorItemLltct').hide();\r\n" +
                    "       $('.CssEditorItemBacSy').hide();\r\n" +
                    "       $('.CssDisplayItemLltct').show();\r\n" +
                    "       $('#trAddLichLyThuyetChiTiet').show();\r\n" +

                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.CbbBacSy(RenderInfo, null, null).value;\r\n" +
                    "       $('#divCbbGiangVien').html(AjaxOut);\r\n" +
                    "       CallInitSelect2('cbbBacSy', '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + "');\r\n" +
                    "       $('#txtNgay').datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGian').datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGianKetThuc').datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị row edit lịch học lý thuyết chi tiết
                    "   function ShowEditItemLineLltct(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItemLltct').hide();\r\n" +
                    "       $('.CssDisplayItemLltct').show();\r\n" +
                    "       $('#trAddLichLyThuyetChiTiet').hide();\r\n" +
                    "       document.getElementById('txtNgay'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtThoiGian'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtThoiGianKetThuc'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtNoiDung'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('divCbbGiangVien'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('cbbHinhThucHoc'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtGhiChu'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spNgay'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spThoiGian'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spThoiGianKetThuc'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spNoiDung'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spGiangVien'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spHinhThucHoc'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spGhiChu'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveLichLyThuyetChiTiet'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditLichLyThuyetChiTiet'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('btnDeleteLichLyThuyetChiTiet'+rowIndex).style.display='none';\r\n" +

                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.CbbBacSy(RenderInfo, rowIndex, $('#hdGiangVienId'+rowIndex).val()).value;\r\n" +
                    "       $('#divCbbGiangVien'+rowIndex).html(AjaxOut);\r\n" +
                    "       CallInitSelect2('cbbBacSy'+rowIndex, '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + "');\r\n" +
                    "       $('#txtNgay'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGian'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGianKetThuc'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật lịch học lý thuyết chi tiết
                    "   function SaveLichLyThuyetChiTiet(rowIndex){\r\n" +
                    "       khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "       lichLyThuyetChiTietId = document.getElementById('hdLichLyThuyetChiTietId'+rowIndex).value;\r\n" +
                    "       ngay = document.getElementById('txtNgay'+rowIndex).value;\r\n" +
                    "       thoiGian = document.getElementById('txtThoiGian'+rowIndex).value;\r\n" +
                    "       thoiGianKetThuc = document.getElementById('txtThoiGianKetThuc'+rowIndex).value;\r\n" +
                    "       noiDung = document.getElementById('txtNoiDung'+rowIndex).value;\r\n" +
                    "       bacSy = document.getElementById('cbbBacSy'+rowIndex).value;\r\n" +
                    "       hinhThucHoc = parseInt(document.getElementById('cbbHinhThucHoc'+rowIndex).value);\r\n" +
                    "       ghiChu = document.getElementById('txtGhiChu'+rowIndex).value;\r\n" +
                    "       if(ngay=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(thoiGian=='' && thoiGianKetThuc != '')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn từ giờ.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(thoiGian!='' && thoiGianKetThuc == '')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn đến giờ.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveLichLyThuyetChiTiet(RenderInfo, khoaHocId, lichLyThuyetChiTietId, ngay, thoiGian, thoiGianKetThuc, noiDung, bacSy, hinhThucHoc, ghiChu).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divLichLyThuyetChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa lịch học lý thuyết chi tiết
                    "   function DeleteLichLyThuyetChiTiet(rowIndex){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa lịch học lý thuyết này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               lichLyThuyetChiTietId = document.getElementById('hdLichLyThuyetChiTietId'+rowIndex).value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.DeleteLichLyThuyetChiTiet(RenderInfo, khoaHocId, lichLyThuyetChiTietId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#divLichLyThuyetChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +                 
                    "   }\r\n" +
                #endregion

                #region Show popup cập nhật lịch học thực hành
                    "function PopupLichHocTh(lichThucHanhId, isEditor)\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupLichHocTh(RenderInfo, khoaHocId, lichThucHanhId, isEditor).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật lịch học thực hành") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbPtChuyenMon','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "     CallInitSelect2('cbbLanhDao','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "}\r\n" +
                #endregion Show popup cập nhật lịch học thực hành
                #region Save Thông tin lịch học thực hành
                    "function SaveLichHocTh()\r\n" +
                    "{\r\n" +
                    "     lichThucHanhId = document.getElementById('hdLichThucHanhId').value;\r\n" +
                    "     diaDiem = document.getElementById('txtDiaDiem').value;\r\n" +
                    "     batDau = document.getElementById('dtBatDau').value;\r\n" +
                    "     ketThuc = document.getElementById('dtKetThuc').value;\r\n" +
                    "     nhom = document.getElementById('txtNhom').value;\r\n" +
                    "     ptChuyenMon = document.getElementById('cbbPtChuyenMon').value;\r\n" +
                    "     lanhDao = document.getElementById('cbbLanhDao').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveLichThucHanh(RenderInfo, khoaHocId, lichThucHanhId, diaDiem, batDau, ketThuc, nhom, ptChuyenMon, lanhDao).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawLichThucHanhs(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#tab-4').html(AjaxOut.HtmlContent);\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawDiemDanhTh(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#tab-6').html(AjaxOut.HtmlContent);\r\n" +
                    //"          $('#divTableDiemDanhTh').freezeTable({'columnNum' : 3, 'scrollable': true,});\r\n" +
                    "     }\r\n" +
                    "     else if (AjaxOut.RetExtraParam1 != ''){\r\n" +
                    "       document.getElementById('hdLichThucHanhId').value = AjaxOut.RetExtraParam1;\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin lịch học thực hành
                #region Xóa lịch học thực hành
                    "   function DeleteLichHocTh(lichThucHanhId){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa lịch học thực hành này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "                RenderInfo=CreateRenderInfo();\r\n" +
                    "                AjaxOut = OneTSQ.WebParts.DT_LopHoc.DeleteLichThucHanh(RenderInfo, khoaHocId, lichThucHanhId).value;\r\n" +
                    "                if(AjaxOut.Error)\r\n" +
                    "                {\r\n" +
                    "                    callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                    return;\r\n" +
                    "                }\r\n" +
                    "                $('#tab-4').html(AjaxOut.HtmlContent);\r\n" +
                    "                AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawDiemDanhTh(RenderInfo, khoaHocId).value;\r\n" +
                    "                if(AjaxOut.Error)\r\n" +
                    "                {\r\n" +
                    "                    callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                    return;\r\n" +
                    "                }\r\n" +
                    "                $('#tab-6').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Hiển thị row thêm mới lịch học thực hành chi tiết
                    "   function ShowLichThucHanhChiTiet(){\r\n" +
                    "       $('.CssEditorItemLthct').hide();\r\n" +
                    "       $('.CssEditorItemBacSy').hide();\r\n" +
                    "       $('.CssDisplayItemLthct').show();\r\n" +
                    "       $('#trAddLichThucHanhChiTiet').show();\r\n" +

                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.CbbBacSy(RenderInfo, null, null).value;\r\n" +
                    "       $('#divCbbGiangVien').html(AjaxOut);\r\n" +
                    "       CallInitSelect2('cbbBacSy', '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + "');\r\n" +
                    "       $('#txtNgay').datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGian').datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGianKetThuc').datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị row edit lịch học thực hành chi tiết
                    "   function ShowEditItemLineLthct(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItemLthct').hide();\r\n" +
                    "       $('.CssDisplayItemLthct').show();\r\n" +
                    "       $('#trAddLichThucHanhChiTiet').hide();\r\n" +
                    "       document.getElementById('txtNgay'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtThoiGian'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtThoiGianKetThuc'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtNoiDung'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('divCbbGiangVien'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtGhiChu'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spNgay'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spThoiGian'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spThoiGianKetThuc'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spNoiDung'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spGiangVien'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spGhiChu'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveLichThucHanhChiTiet'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditLichThucHanhChiTiet'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('btnDeleteLichThucHanhChiTiet'+rowIndex).style.display='none';\r\n" +

                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.CbbBacSy(RenderInfo, rowIndex, $('#hdGiangVienId'+rowIndex).val()).value;\r\n" +
                    "       $('#divCbbGiangVien'+rowIndex).html(AjaxOut);\r\n" +
                    "       CallInitSelect2('cbbBacSy'+rowIndex, '" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + "');\r\n" +
                    "       $('#txtNgay'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGian'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "       $('#txtThoiGianKetThuc'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'HH:mm' \r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật lịch học thực hành chi tiết
                    "   function SaveLichThucHanhChiTiet(rowIndex){\r\n" +
                    "       lichThucHanhId = document.getElementById('hdLichThucHanhId'+rowIndex).value;\r\n" +
                    "       lichThucHanhChiTietId = document.getElementById('hdLichThucHanhChiTietId'+rowIndex).value;\r\n" +
                    "       ngay = document.getElementById('txtNgay'+rowIndex).value;\r\n" +
                    "       thoiGian = document.getElementById('txtThoiGian'+rowIndex).value;\r\n" +
                    "       thoiGianKetThuc = document.getElementById('txtThoiGianKetThuc'+rowIndex).value;\r\n" +
                    "       noiDung = document.getElementById('txtNoiDung'+rowIndex).value;\r\n" +
                    "       bacSy = document.getElementById('cbbBacSy'+rowIndex).value;\r\n" +
                    "       ghiChu = document.getElementById('txtGhiChu'+rowIndex).value;\r\n" +
                    "       if(ngay=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(thoiGian=='' && thoiGianKetThuc != '')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn từ giờ.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(thoiGian!='' && thoiGianKetThuc == '')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn đến giờ.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveLichThucHanhChiTiet(RenderInfo, khoaHocId, lichThucHanhId, lichThucHanhChiTietId, ngay, thoiGian, thoiGianKetThuc, noiDung, bacSy, ghiChu).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#tabLichThucHanhChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa lịch học thực hành chi tiết
                    "   function DeleteLichThucHanhChiTiet(rowIndex){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa lịch học thực hành này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               lichThucHanhId = document.getElementById('hdLichThucHanhId'+rowIndex).value;\r\n" +
                    "               lichThucHanhChiTietId = document.getElementById('hdLichThucHanhChiTietId'+rowIndex).value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.DeleteLichThucHanhChiTiet(RenderInfo, khoaHocId, lichThucHanhId, lichThucHanhChiTietId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#tabLichThucHanhChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Hiển thị thông tin học viên khi thay đổi giá trị chọn trong combobox học viên trong nhóm
                    "   function cbbHocVien_onchange(sender){\r\n" +
                    "       if(sender.value == ''){\r\n" +
                    "           $('#tdMa').html('');\r\n" +
                    "           $('#tdGioiTinh').html('');\r\n" +
                    "           $('#tdNgaySinh').html('');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.GetThongTinHocVien(RenderInfo, sender.value).value;\r\n" +
                    //"       if(AjaxOut.JsonData != ''){\r\n" +
                    //"           var hocVien = JSON.parse(AjaxOut.JsonData);\r\n" +
                    //"           $('#tdMa').html(hocVien.Ma);\r\n" +
                    //"           $('#tdGioiTinh').html(hocVien.GioiTinh);\r\n" +
                    //"           $('#tdNgaySinh').html(hocVien.NgaySinh);\r\n" +
                    //"       }\r\n" +
                    //"       else{\r\n" +
                    //"           $('#tdMa').html('');\r\n" +
                    //"           $('#tdGioiTinh').html('');\r\n" +
                    //"           $('#tdNgaySinh').html('');\r\n" +
                    //"       }\r\n" +
                    "   }\r\n" +
                #endregion Hiển thị thông tin học viên khi thay đổi giá trị chọn trong combobox học viên trong nhóm
                #region Hiển thị row thêm mới học viên trong nhóm
                    "   function ShowHocVienTrongNhom(){\r\n" +
                    "       $('#trAddHocVienTrongNhom').show();\r\n" +
                    "       $(\"#cbbHocVien\").select2({\r\n" +
                    "           placeholder: \"Học viên\",\r\n" +
                    "           allowClear: true\r\n" +
                    "       });\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới học viên trong nhóm
                    "   function SaveHocVienTrongNhom(rowIndex){\r\n" +
                    "       lichThucHanhId = document.getElementById('hdLichThucHanhId'+rowIndex).value;\r\n" +
                    "       hocVienId = document.getElementById('cbbHocVien'+rowIndex).value;\r\n" +
                    "       if(hocVienId=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn học viên.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveHocVienTrongNhom(RenderInfo, khoaHocId, lichThucHanhId, hocVienId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#tabHocVienTrongNhom').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa học viên trong nhóm
                    "   function DeleteHocVienTrongNhom(rowIndex){\r\n" +                  
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn muốn xóa học viên này không?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               lichThucHanhId = document.getElementById('hdLichThucHanhId'+rowIndex).value;\r\n" +
                    "               hocVienId = document.getElementById('hdHocVienId'+rowIndex).value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.DeleteHocVienTrongNhom(RenderInfo, khoaHocId, lichThucHanhId, hocVienId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#tabHocVienTrongNhom').html(AjaxOut.HtmlContent);\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Show popup điểm danh lý thuyết
                    "function PopupDiemDanhLt()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupDiemDanhLt(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh lý thuyết (Tích vào ô check nếu học viên vắng mặt)") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    //"     $('#divTablePopupDiemDanhLt').freezeTable({'columnNum' : 3, 'scrollable': true,});\r\n" +
                    "     currentPopup = 'DiemDanhLt';\r\n" +
                    "}\r\n" +
                #endregion Show popup điểm danh lý thuyết
                #region Save điểm danh lý thuyết
                    "function SaveDiemDanhLyThuyet(sender)\r\n" +
                    "{\r\n" +
                    "     coDiemDanh = sender.checked ? 1 : 0;\r\n" +
                    "     hocVienId = sender.id.split('_')[0];\r\n" +
                    "     lichLyThuyetChiTietId = sender.id.split('_')[1];\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveDiemDanhLyThuyet(RenderInfo, hocVienId, lichLyThuyetChiTietId, coDiemDanh).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    //"     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion Save điểm danh lý thuyết

                #region Show popup điểm danh thực hành
                    "function PopupDiemDanhTh(lichThucHanhId, tenNhom)\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupDiemDanhTh(RenderInfo, khoaHocId, lichThucHanhId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh nhóm thực hành") + " ' + tenNhom + ' " + WebLanguage.GetLanguage(OSiteParam, "(Tích vào ô check nếu học viên vắng mặt)") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    //"     $('#divTablePopupDiemDanhTh').freezeTable({'columnNum' : 3, 'scrollable': true,});\r\n" +
                    "     currentPopup = 'DiemDanhTh';\r\n" +
                    "}\r\n" +
                #endregion Show popup điểm danh thực hành
                #region Save điểm danh thực hành
                    "function SaveDiemDanhThucHanh(sender, hocVienId, lichThucHanhChiTietId)\r\n" +
                    "{\r\n" +
                    "     coDiemDanh = sender.checked ? 1 : 0;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveDiemDanhThucHanh(RenderInfo, hocVienId, lichThucHanhChiTietId, coDiemDanh).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    //"     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion Save điểm danh thực hành

                #region Show popup nhập kết quả đào tạo
                    "function PopupNhapKetQuaDaoTao()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupKetQuaDaoTao(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Kết quả đào tạo") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "}\r\n" +
                #endregion Show popup nhập kết quả đào tạo
                #region Hiển thị row edit kết quả đào tạo
                    "   function ShowEditItemLineKqdt(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItemKqdt').hide();\r\n" +
                    "       $('.CssDisplayItemKqdt').show();\r\n" +
                    "       document.getElementById('txtDiemThiLyThuyet'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtDiemThiThucHanh'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spDiemThiLyThuyet'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spDiemThiThucHanh'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveKetQuaDaoTao'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditKetQuaDaoTao'+rowIndex).style.display='none';\r\n" +
                    "       currentPopup = 'KetQuaDaoTao';\r\n" +
                    "   }\r\n" +
                #endregion Hiển thị row edit kết quả đào tạo
                #region Hiển thị row edit kết quả đào tạo
                    "   function ShowXepLoai(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       diemThiLyThuyet = parseFloat(document.getElementById('txtDiemThiLyThuyet'+rowIndex).value);\r\n" +
                    "       diemThiThucHanh = parseFloat(document.getElementById('txtDiemThiThucHanh'+rowIndex).value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.GetXepLoai(RenderInfo, diemThiLyThuyet, diemThiThucHanh).value;\r\n" +
                    "       if(!AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           $('#spXepLoai'+rowIndex).text(AjaxOut.RetExtraParam1);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Hiển thị row edit kết quả đào tạo
                #region Cập nhật kết quả đào tạo
                    "   function SaveKetQuaDaoTao(rowIndex){\r\n" +
                    "       ketQuaDaoTaoId = document.getElementById('hdKetQuaDaoTaoId'+rowIndex).value;\r\n" +
                    "       diemThiLyThuyet = parseFloat(document.getElementById('txtDiemThiLyThuyet'+rowIndex).value);\r\n" +
                    "       diemThiThucHanh = parseFloat(document.getElementById('txtDiemThiThucHanh'+rowIndex).value);\r\n" +
                    "       xepLoai = $('#spXepLoai'+rowIndex).text();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_LopHoc.SaveKetQuaDaoTao(RenderInfo, ketQuaDaoTaoId, diemThiLyThuyet, diemThiThucHanh, xepLoai).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divHocVienEditor').html(AjaxOut.HtmlContent);\r\n" +
                    "       toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +
                #endregion Cập nhật kết quả đào tạo

                #region Show popup quản lý tài liệu
                    "function PopupTaiLieu()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_LopHoc.PopupTaiLieu(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Quản lý tài liệu") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "}\r\n" +
                #endregion Show popup nhập quản lý tài liệu

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
                #region Upload tài liệu
                    "   function UploadTaiLieus(){\r\n" +
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
                    "        $('#btnUploadTaiLieu').prop('disabled', true);\r\n" +
                    "        $('#divProgress').show();\r\n" +
                    "        var data = new FormData();\r\n" +
                    "        var files = fileUploadElement.files;\r\n" +
                    "        for (i = 0; i < files.length; i++) {\r\n" +
                    "            data.append(''+i, files[i]);\r\n" +
                    "        }\r\n" +
                    "       var ajaxRequest = $.ajax({\r\n" +
                    "           type: 'POST',\r\n" +
                    "           url: '" + GetUploadHandler(OSiteParam, SessionId, user.OwnerUserId, user.LoginName, "UploadedDaoTaoFilePath") + "',\r\n" +
                    "           contentType: false,\r\n" +
                    "           processData: false,\r\n" +
                    "           data: data,\r\n" +
                    "           xhr: function() {\r\n" +
                    "               var xhr = $.ajaxSettings.xhr();\r\n" +
                    "               xhr.upload.onprogress = function(e) {\r\n" +
                    "                   if (e.lengthComputable)\r\n" +
                    "                   {\r\n" +
                    "                       var completedPercent = Math.floor(e.loaded / e.total * 100);\r\n" +
                    "                       $('#spCompletedPercent').html(completedPercent);\r\n" +
                    "                       $('#divProgressBar').css('width', completedPercent + '%');\r\n" +
                    "                   }\r\n" +
                    "               };\r\n" +
                    "               return xhr;\r\n" +
                    "           },\r\n" +
                    "           success: function(xmlResult) {\r\n" +
                    "               khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "               ghiChu = document.getElementById('txtMoTaTaiLieu').value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.UploadTaiLieu(RenderInfo, khoaHocId, ghiChu, xmlResult).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               $('#fileUploadTaiLieu')[0].value='';\r\n" +
                    "               $('#txtMoTaTaiLieu').val('');\r\n" +
                    "               $('#divTenTaiLieuChon').html('');\r\n" +
                    //Hiển thị lại danh sách tài liệu trên popup
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawTaiLieus(RenderInfo, khoaHocId, true).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   $('#btnUploadTaiLieu').prop('disabled', false);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#divTaiLieuList').html(AjaxOut.HtmlContent);\r\n" +
                    //Hiển thị lại danh sách tài liệu trên form
                    "               AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawTaiLieus(RenderInfo, khoaHocId, false).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   $('#btnUploadTaiLieu').prop('disabled', false);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               $('#divTaiLieus').html(AjaxOut.HtmlContent);\r\n" +
                    //"               $.LoadingOverlay('hide');\r\n" +
                    "               $('#btnUploadTaiLieu').prop('disabled', false);\r\n" +
                    "           },\r\n" +
                    "           error: function(result) {\r\n" +
                    //"               $.LoadingOverlay('hide');\r\n" +
                    "               $('#btnUploadTaiLieu').prop('disabled', false);\r\n" +
                    "           },\r\n" +
                    "       });\r\n" +
                    "   }\r\n" +
                #endregion
                #region Delete tài liệu
                    "   function DeleteTaiLieu(taiLieuId){\r\n" +
                    "       swal({\r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn xóa tài liệu này không?") + "\",\r\n" +
                    "               type: \"warning\",\r\n" +
                    "               showCancelButton: true,\r\n" +
                    "               confirmButtonClass: \"btn-danger\",\r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\",\r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\",\r\n" +
                    "               closeOnConfirm: true,\r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           },\r\n" +
                    "           function(isConfirm) {\r\n" +
                    "               if (isConfirm) {\r\n" +
                    "                   AjaxOut = OneTSQ.WebParts.DT_LopHoc.DeleteTaiLieu(RenderInfo, taiLieuId).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    //Hiển thị lại danh sách tài liệu trên popup
                    "                   AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawTaiLieus(RenderInfo, khoaHocId, true).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    "                   $('#divTaiLieuList').html(AjaxOut.HtmlContent);\r\n" +
                    //Hiển thị lại danh sách tài liệu trên form`
                    "                   AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawTaiLieus(RenderInfo, khoaHocId, false).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    "                   $('#divTaiLieus').html(AjaxOut.HtmlContent);\r\n" +
                    "               }\r\n " +
                    "           });\r\n" +
                    "   }\r\n" +
                #endregion

                #region Bắt sự kiện đóng popup
                    "function ClosePopup()\r\n" +
                    "{\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     if(currentPopup == 'DiemDanhLt')\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawDiemDanhLt(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#divDiemDanhLt').html(AjaxOut.HtmlContent);\r\n" +
                    "     }\r\n" +
                    "     else if(currentPopup == 'DiemDanhTh')\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawDiemDanhTh(RenderInfo, khoaHocId).value;\r\n" +
                    "          $('#tab-6').html(AjaxOut.HtmlContent);\r\n" +
                    "     }\r\n" +
                    "     else if(currentPopup == 'KetQuaDaoTao')\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_LopHoc.DrawHocViens(RenderInfo, khoaHocId, false).value;\r\n" +
                    "          $('#divHocViens').html(AjaxOut.HtmlContent);\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion Bắt sự kiện đóng popup
                #region Select2
                    "   function Select2()\r\n" +
                    "   {\r\n" +
                    "     CallInitSelect2('cbbPtChuyenMon','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
                    "     CallInitSelect2('cbbLanhDao','" + WebEnvironments.GetRemoteProcessDataUrl(BacSyService.StaticServiceId, maDonViTuVanDefault) + "',\"\"/*,'false'*/);\r\n" +
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
        public static AjaxOut DrawHocViens(RenderInfoCls ORenderInfo, string khoaHocId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId, });
                DT_KetQuaDaoTaoCls[] ketQuaDaoTaos = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().ReadingDiemDanh(ORenderInfo, new DT_KetQuaDaoTaoFilterCls() { KhoaHocDuyet_Id = khoaHocId });
                double soBuoiHocLT10Percent = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Count(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = khoaHocId });
                int hocVienTotal = hocViens.Count();
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Cơ quan công tác") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh lý thuyết") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điểm danh thực hành") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điểm thi lý thuyết") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điểm thi thực hành") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Xếp loại") + " </th> \r\n" +
                    (isEditor ? "         <th>" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + " </th> \r\n" : null) +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < hocVienTotal; iIndex++)
                {
                    OneMES3.DM.Model.DonViCongTacCls donViCongTac = null;
                    if (!string.IsNullOrEmpty(hocViens[iIndex].DONVICONGTAC_MA))
                    {
                        donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocViens[iIndex].DONVICONGTAC_MA);
                    }
                    double soBuoiHocTH10Percent = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Count(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { KHOAHOC_ID = khoaHocId, HOCVIEN_ID = hocViens[iIndex].ID });
                    DT_KetQuaDaoTaoCls ketQuaDaoTao = ketQuaDaoTaos.FirstOrDefault(o => o.HOCVIEN_ID == hocViens[iIndex].ID);
                    Html +=
                    "                 <tr> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + hocViens[iIndex].MA + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + hocViens[iIndex].HOTEN + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (hocViens[iIndex].NGAYSINH == null ? null : hocViens[iIndex].NGAYSINH.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (hocViens[iIndex].GIOITINH == null ? null : Common.BenhNhan.GioiTinhs[hocViens[iIndex].GIOITINH.Value]) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + hocViens[iIndex].KHOAPHONGCONGTAC + ", " + (donViCongTac == null ? null : donViCongTac.Ten) + "</a></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (ketQuaDaoTao.DIEMDANHLYTHUYET_TH + "/" + soBuoiHocLT10Percent) + "</a></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (ketQuaDaoTao.DIEMDANHTHUCHANH_TH + "/" + soBuoiHocTH10Percent) + "</td> \r\n" +
                    (
                        isEditor ?
                    "                     <input type='hidden' id='hdKetQuaDaoTaoId" + iIndex + "' value='" + ketQuaDaoTao.ID + "'>\r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'><input type='number' step='0.1' class='form-control CssEditorItemKqdt' style='display:none; ' id='txtDiemThiLyThuyet" + iIndex + "' value='" + ketQuaDaoTao.DIEMTHILYTHUYET + "' onchange='ShowXepLoai(" + iIndex + ");'><span class='CssDisplayItemKqdt' id='spDiemThiLyThuyet" + iIndex + "'>" + ketQuaDaoTao.DIEMTHILYTHUYET + "</span></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'><input type='number' step='0.1' class='form-control CssEditorItemKqdt' style='display:none; ' id='txtDiemThiThucHanh" + iIndex + "' value='" + ketQuaDaoTao.DIEMTHITHUCHANH + "' onchange='ShowXepLoai(" + iIndex + ");'><span class='CssDisplayItemKqdt' id='spDiemThiThucHanh" + iIndex + "'>" + ketQuaDaoTao.DIEMTHITHUCHANH + "</span></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'><span id='spXepLoai" + iIndex + "'>" + ketQuaDaoTao.XEPLOAI + "</span></td> \r\n" +
                    "                     <td style='text-align:center;'>\r\n" +
                    "                         <a id='btnSaveKetQuaDaoTao" + iIndex + "' class='CssEditorItemKqdt' style='display:none' href='javascript:SaveKetQuaDaoTao(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                    "                         <a id='btnEditKetQuaDaoTao" + iIndex + "' class='CssEditorItemKqdt' href='javascript:ShowEditItemLineKqdt(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                    "                     </td> \r\n"
                        :
                    "                     <td style='text-align: center; vertical-align: middle;'>" + ketQuaDaoTao.DIEMTHILYTHUYET + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + ketQuaDaoTao.DIEMTHITHUCHANH + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + ketQuaDaoTao.XEPLOAI + "</td> \r\n"
                    ) +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
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
        public static AjaxOut PopupKetQuaDaoTao(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string html =
                "<div class='col-md-12' id='divHocVienEditor' style='height: 100%; margin-top: 15px; padding:5px; overflow-y:auto;'>\r\n" +
                    DrawHocViens(ORenderInfo, khoaHocId, true).HtmlContent +
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawKeHoachLop(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_KeHoachLopCls keHoachLop = CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().CreateModel(ORenderInfo, khoaHocId);
                if (keHoachLop == null)
                {
                    keHoachLop = new DT_KeHoachLopCls();
                }
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                BacSyCls lanhDao = null;
                if (!string.IsNullOrEmpty(keHoachLop.LANHDAO_ID))
                {
                    lanhDao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, keHoachLop.LANHDAO_ID);
                }

                string html =
                    "   <center> \r\n" +
                    "       <div style='width: 800px; padding: 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "          <div class='row'>" +
                    "              <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                    "                  " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH") + "<span style='border-bottom:solid 1px;'> " + WebLanguage.GetLanguage(OSiteParam, "VIỆN YHCT NGHỆ") + "</span> " + WebLanguage.GetLanguage(OSiteParam, "AN") + " </b>\r\n" +
                    "              </div>\r\n" +
                    "              <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM") + "</b><br>\r\n" +
                    "                  <b><span style='border-bottom:solid 1px;'>" + WebLanguage.GetLanguage(OSiteParam, "Độc lập - Tự do - Hạnh phúc") + "</span></b><br><br>\r\n" +
                    //"                  <i>" + string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "Hà Nội, ngày"), keHoachLop.NGAYTAO.Day, WebLanguage.GetLanguage(OSiteParam, "tháng"), keHoachLop.NGAYTAO.Month, WebLanguage.GetLanguage(OSiteParam, "năm"), keHoachLop.NGAYTAO.Year) + "</i><br>\r\n" +
                    "                  <i>" + string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "Hà Nội, ngày"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", WebLanguage.GetLanguage(OSiteParam, "tháng"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", WebLanguage.GetLanguage(OSiteParam, "năm"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "</i><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center; margin-top:50px;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "KẾ HOẠCH") + "</b><br><br>\r\n" +
                    "                  <b>" + string.Format("{0} \"{1} - K {2}\"", WebLanguage.GetLanguage(OSiteParam, "Tổ chức khóa đào tạo"), khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "</b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>1. " + WebLanguage.GetLanguage(OSiteParam, "Thời gian tổ chức:") + "</b></span>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Từ") + ":\r\n" +
                    "                   <span id='spBatDau' class=\"valueForm\">" + (keHoachLop.BATDAU == null ? null : keHoachLop.BATDAU.Value.ToString("dd/MM/yyyy")) + "</span> - \r\n" +
                    "                   <span id='spKetThuc' class=\"valueForm\">" + (keHoachLop.KETTHUC == null ? null : keHoachLop.KETTHUC.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>2. " + WebLanguage.GetLanguage(OSiteParam, "Thời gian và địa điểm tiếp đón học viên") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                    "                      - " + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                      <span id='spThoiGianTiepDon_Gio' class=\"valueForm\">" + (keHoachLop.THOIGIANTIEPDON == null ? null : keHoachLop.THOIGIANTIEPDON.Value.ToString("HH") + "h" + keHoachLop.THOIGIANTIEPDON.Value.ToString("mm")) + "</span> \r\n" +
                    "                      <span id='spThoiGianTiepDon_Thu' class=\"valueForm\">" + (keHoachLop.THOIGIANTIEPDON == null ? null : GetDayOfWeek(OSiteParam, keHoachLop.THOIGIANTIEPDON.Value.DayOfWeek)) + "</span>, ngày \r\n" +
                    "                      <span id='spThoiGianTiepDon' class=\"valueForm\">" + (keHoachLop.THOIGIANTIEPDON == null ? null : keHoachLop.THOIGIANTIEPDON.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                    "                      - " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ": \r\n" +
                    "                      <span id='spDiaDiemTiepDon' class=\"valueForm\">" + keHoachLop.DIADIEMTIEPDON + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Tổ chức đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3.1. " + WebLanguage.GetLanguage(OSiteParam, "Lý thuyết") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       - " + WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + " \r\n" +
                    "                       <span id='spBatDauLt' class=\"valueForm\">" + (keHoachLop.BATDAULT == null ? null : keHoachLop.BATDAULT.Value.ToString("dd/MM/yyyy")) + "</span> - \r\n" +
                    "                       <span id='spKetThucLt' class=\"valueForm\">" + (keHoachLop.KETTHUCLT == null ? null : keHoachLop.KETTHUCLT.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            " - " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                       <span id='spDiaDiemLt' class=\"valueForm\">" + keHoachLop.DIADIEMLT + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           "- " + WebLanguage.GetLanguage(OSiteParam, "Hình thức: Học tập trung") + "\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3.2. " + WebLanguage.GetLanguage(OSiteParam, "Thực hành") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                                            " - " + WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + " \r\n" +
                    "                       <span id='spBatDauTh' class=\"valueForm\">" + (keHoachLop.BATDAUTH == null ? null : keHoachLop.BATDAUTH.Value.ToString("dd/MM/yyyy")) + "</span> - \r\n" +
                    "                       <span id='spKetThucTh' class=\"valueForm\">" + (keHoachLop.KETTHUCTH == null ? null : keHoachLop.KETTHUCTH.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                       <span id='spDiaDiemTh' class=\"valueForm\">" + keHoachLop.DIADIEMTH + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            "- " + WebLanguage.GetLanguage(OSiteParam, "Hình thức: Học viên được chia thành") + " \r\n" +
                    "                       <span id='spSoLuongNhomTh' class=\"valueForm\">" + keHoachLop.SOLUONGNHOMTH + "</span> \r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "nhóm, mỗi nhóm") + "\r\n" +
                    "                       <span id='spSoHvTrongNhomTh' class=\"valueForm\">" + keHoachLop.SOHVTRONGNHOMTH + "</span> \r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "học viên và học luân phiên tại các địa điểm trên.") + "\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4. " + WebLanguage.GetLanguage(OSiteParam, "Tổ chức đánh giá") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.1. " + WebLanguage.GetLanguage(OSiteParam, "Đánh giá trước khóa đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                       <span id='spThoiGianDanhGiaTdt_Gio' class=\"valueForm\">" + (keHoachLop.THOIGIANDANHGIATDT == null ? null : keHoachLop.THOIGIANDANHGIATDT.Value.ToString("HH") + "h" + keHoachLop.THOIGIANDANHGIATDT.Value.ToString("mm")) + "</span>, ngày \r\n" +
                    "                       <span id='spThoiGianDanhGiaTdt' class=\"valueForm\">" + (keHoachLop.THOIGIANDANHGIATDT == null ? null : keHoachLop.THOIGIANDANHGIATDT.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           " - " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                      <span id='spDiaDiemDanhGiaTdt' class=\"valueForm\">" + keHoachLop.DIADIEMDANHGIATDT + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.2. " + WebLanguage.GetLanguage(OSiteParam, "Giải đáp thắc mắc") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ": \r\n" +
                    "                       <span id='spThoiGianGiaiDapThacMac_Gio' class=\"valueForm\">" + (keHoachLop.THOIGIANGIAIDAPTHACMAC == null ? null : keHoachLop.THOIGIANGIAIDAPTHACMAC.Value.ToString("HH") + "h" + keHoachLop.THOIGIANGIAIDAPTHACMAC.Value.ToString("mm")) + "</span>, ngày \r\n" +
                    "                       <span id='spThoiGianGiaiDapThacMac' class=\"valueForm\">" + (keHoachLop.THOIGIANGIAIDAPTHACMAC == null ? null : keHoachLop.THOIGIANGIAIDAPTHACMAC.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                      <span id='spDiaDiemGiaiDapThacMac' class=\"valueForm\">" + keHoachLop.DIADIEMGIAIDAPTHACMAC + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3. " + WebLanguage.GetLanguage(OSiteParam, "Đánh giá kết thúc khóa đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.1. " + WebLanguage.GetLanguage(OSiteParam, "Thi lý thuyết") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ": \r\n" +
                    "                      <span id='spBatDauThiLt' class=\"valueForm\">" + (keHoachLop.BATDAUTHILT == null ? null : keHoachLop.BATDAUTHILT.Value.ToString("HH") + "h" + keHoachLop.BATDAUTHILT.Value.ToString("mm")) + "</span> - \r\n" +
                    "                      <span id='spKetThucThiLt_Gio' class=\"valueForm\">" + (keHoachLop.KETTHUCTHILT == null ? null : keHoachLop.KETTHUCTHILT.Value.ToString("HH") + "h" + keHoachLop.KETTHUCTHILT.Value.ToString("mm")) + "</span>, ngày \r\n" +
                    "                      <span id='spKetThucThiLt' class=\"valueForm\">" + (keHoachLop.KETTHUCTHILT == null ? null : keHoachLop.KETTHUCTHILT.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                      <span id='spDiaDiemThiLt' class=\"valueForm\">" + keHoachLop.DIADIEMTHILT + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.2. " + WebLanguage.GetLanguage(OSiteParam, "Thi vấn đáp (nếu có)") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ": \r\n" +
                    "                      <span id='spBatDauThiVd' class=\"valueForm\">" + (keHoachLop.BATDAUTHIVD == null ? null : keHoachLop.BATDAUTHIVD.Value.ToString("HH") + "h" + keHoachLop.BATDAUTHIVD.Value.ToString("mm")) + "</span> - \r\n" +
                    "                      <span id='spKetThucThiVd_Gio' class=\"valueForm\">" + (keHoachLop.KETTHUCTHIVD == null ? null : keHoachLop.KETTHUCTHIVD.Value.ToString("HH") + "h" + keHoachLop.KETTHUCTHIVD.Value.ToString("mm")) + "</span>, ngày \r\n" +
                    "                      <span id='spKetThucThiVd' class=\"valueForm\">" + (keHoachLop.KETTHUCTHIVD == null ? null : keHoachLop.KETTHUCTHIVD.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                                          "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                     <span id='spDiaDiemThiVd' class=\"valueForm\">" + keHoachLop.DIADIEMTHIVD + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.3. " + WebLanguage.GetLanguage(OSiteParam, "Thi thực hành") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-6 col-xs-6'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                                          "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ": \r\n" +
                    "                     <span id='spBatDauThiTh' class=\"valueForm\">" + (keHoachLop.BATDAUTHITH == null ? null : keHoachLop.BATDAUTHITH.Value.ToString("HH") + "h" + keHoachLop.BATDAUTHITH.Value.ToString("mm")) + "</span> - \r\n" +
                    "                     <span id='spKetThucThiTh_Gio' class=\"valueForm\">" + (keHoachLop.KETTHUCTHITH == null ? null : keHoachLop.KETTHUCTHITH.Value.ToString("HH") + "h" + keHoachLop.KETTHUCTHITH.Value.ToString("mm")) + "</span>, ngày \r\n" +
                    "                     <span id='spKetThucThiTh' class=\"valueForm\">" + (keHoachLop.KETTHUCTHITH == null ? null : keHoachLop.KETTHUCTHITH.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                                          "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                     <span id='spDiaDiemThiTh' class=\"valueForm\">" + keHoachLop.DIADIEMTHITH + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>5. " + WebLanguage.GetLanguage(OSiteParam, "Bế giảng và trao chứng chỉ") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            "- " + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                       <span id='spThoiGianBeGiang_Gio' class=\"valueForm\">" + (keHoachLop.THOIGIANBEGIANG == null ? null : keHoachLop.THOIGIANBEGIANG.Value.ToString("HH") + "h" + keHoachLop.THOIGIANBEGIANG.Value.ToString("mm")) + "</span>, ngày \r\n" +
                                            "<span id='spThoiGianBeGiang' class=\"valueForm\">" + (keHoachLop.THOIGIANBEGIANG == null ? null : keHoachLop.THOIGIANBEGIANG.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <div class=\"form-group\">\r\n" +
                                          "- " + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                     <span id='spDiaDiemBeGiang' class=\"valueForm\">" + keHoachLop.DIADIEMBEGIANG + "</span>\r\n" +
                    "                  </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:50px;'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                    "                       <div style='width:100%; text-align:center;'>" + keHoachLop.NGUOILAP + "</div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                    "                       <div style='width:100%; text-align:center;'>" + (lanhDao == null ? null : lanhDao.HOTEN) + "</div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "   </center> \r\n";
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
        public static AjaxOut PopupKeHoachLop(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_KeHoachLopCls keHoachLop = CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().CreateModel(ORenderInfo, khoaHocId);
                if (keHoachLop == null)
                {
                    keHoachLop = new DT_KeHoachLopCls() { NGAYTAO = DateTime.Now, NGUOITAO_ID = user.OwnerUserId };
                }
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);

                string cbbLanhDao = "<select id = 'cbbLanhDao' class='form-control'>\r\n";
                if (!string.IsNullOrEmpty(keHoachLop.LANHDAO_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, keHoachLop.LANHDAO_ID);
                    if (bacSy != null)
                        cbbLanhDao += string.Format("<option value={0}>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbLanhDao += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "<form action='javascript:SaveKeHoachLop();'> \r\n" +
                    "<input type='hidden' id='hdClose'>\r\n" +
                    "<div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                    "   <center> \r\n" +
                    "       <div style='width: 800px; padding: 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                #region Thông tin kế hoạch lớp
                    "          <div class='row'>" +
                    "              <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                    "                  " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH") + "<span style='border-bottom:solid 1px;'> " + WebLanguage.GetLanguage(OSiteParam, "VIỆN YHCT NGHỆ") + "</span> " + WebLanguage.GetLanguage(OSiteParam, "AN") + " </b>\r\n" +
                    "              </div>\r\n" +
                    "              <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM") + "</b><br>\r\n" +
                    "                  <b><span style='border-bottom:solid 1px;'>" + WebLanguage.GetLanguage(OSiteParam, "Độc lập - Tự do - Hạnh phúc") + "</span></b><br><br>\r\n" +
                    //"                  <i>" + string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "Hà Nội, ngày"), keHoachLop.NGAYTAO.Day, WebLanguage.GetLanguage(OSiteParam, "tháng"), keHoachLop.NGAYTAO.Month, WebLanguage.GetLanguage(OSiteParam, "năm"), keHoachLop.NGAYTAO.Year) + "</i><br>\r\n" +
                    "                  <i>" + string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "Hà Nội, ngày"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", WebLanguage.GetLanguage(OSiteParam, "tháng"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", WebLanguage.GetLanguage(OSiteParam, "năm"), "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "</i><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center; margin-top:50px;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "KẾ HOẠCH") + "</b><br><br>\r\n" +
                    "                  <b>" + string.Format("{0} \"{1} - K {2}\"", WebLanguage.GetLanguage(OSiteParam, "Tổ chức khóa đào tạo"), khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "</b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>1. " + WebLanguage.GetLanguage(OSiteParam, "Thời gian tổ chức:") + "</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Từ") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999' style='z-index: 0;' class='datepicker form-control' id='dtBatDau' value='" + (keHoachLop.BATDAU == null ? null : keHoachLop.BATDAU.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999'  ='z-index: 0;' class='datepicker form-control' id='dtKetThuc' value='" + (keHoachLop.KETTHUC == null ? null : keHoachLop.KETTHUC.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>2. " + WebLanguage.GetLanguage(OSiteParam, "Thời gian và địa điểm tiếp đón học viên") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtThoiGianTiepDon' value='" + (keHoachLop.THOIGIANTIEPDON == null ? null : keHoachLop.THOIGIANTIEPDON.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                  <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemTiepDon' value='" + keHoachLop.DIADIEMTIEPDON + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Tổ chức đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3.1. " + WebLanguage.GetLanguage(OSiteParam, "Lý thuyết") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='z-index: 0;' id='dtBatDauLt' value='" + (keHoachLop.BATDAULT == null ? null : keHoachLop.BATDAULT.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='z-index: 0;' id='dtKetThucLt' value='" + (keHoachLop.KETTHUCLT == null ? null : keHoachLop.KETTHUCLT.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                  <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemLt' value='" + keHoachLop.DIADIEMLT + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Hình thức: Học tập trung") + "\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3.2. " + WebLanguage.GetLanguage(OSiteParam, "Thực hành") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='z-index: 0;' id='dtBatDauTh' value='" + (keHoachLop.BATDAUTH == null ? null : keHoachLop.BATDAUTH.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='z-index: 0;' id='dtKetThucTh' value='" + (keHoachLop.KETTHUCTH == null ? null : keHoachLop.KETTHUCTH.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                                       "<input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemTh' value='" + keHoachLop.DIADIEMTH + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Hình thức: Học viên được chia thành") + " \r\n" +
                    "                  <input type='number' class='form-control' id='txtSoLuongNhomTh' style='z-index: 0; width:50px; display:inline;' value='" + keHoachLop.SOLUONGNHOMTH + "'> " +
                                       WebLanguage.GetLanguage(OSiteParam, "nhóm, mỗi nhóm") + " \r\n" +
                    "                  <input type='number' class='form-control' id='txtSoHvTrongNhomTh' style='z-index: 0; width:50px; display:inline;' value='" + keHoachLop.SOHVTRONGNHOMTH + "'> " +
                                       WebLanguage.GetLanguage(OSiteParam, "học viên và học luân phiên tại các địa điểm trên.") + "\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4. " + WebLanguage.GetLanguage(OSiteParam, "Tổ chức đánh giá") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.1. " + WebLanguage.GetLanguage(OSiteParam, "Đánh giá trước khóa đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtThoiGianDanhGiaTdt' value='" + (keHoachLop.THOIGIANDANHGIATDT == null ? null : keHoachLop.THOIGIANDANHGIATDT.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                  <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemDanhGiaTdt' value='" + keHoachLop.DIADIEMDANHGIATDT + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.2. " + WebLanguage.GetLanguage(OSiteParam, "Giải đáp thắc mắc") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtThoiGianGiaiDapThacMac' value='" + (keHoachLop.THOIGIANGIAIDAPTHACMAC == null ? null : keHoachLop.THOIGIANGIAIDAPTHACMAC.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                 <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemGiaiDapThacMac' value='" + keHoachLop.DIADIEMGIAIDAPTHACMAC + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3. " + WebLanguage.GetLanguage(OSiteParam, "Đánh giá kết thúc khóa đào tạo") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.1. " + WebLanguage.GetLanguage(OSiteParam, "Thi lý thuyết") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                   <input type='text' data-mask='99:99' class='timepicker form-control' style='z-index: 0;' id='dtBatDauThiLt' value='" + (keHoachLop.BATDAUTHILT == null ? null : keHoachLop.BATDAUTHILT.Value.ToString("HH:mm")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtKetThucThiLt' value='" + (keHoachLop.KETTHUCTHILT == null ? null : keHoachLop.KETTHUCTHILT.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                  <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemThiLt' value='" + keHoachLop.DIADIEMTHILT + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.2. " + WebLanguage.GetLanguage(OSiteParam, "Thi vấn đáp (nếu có)") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99' style='z-index: 0;' class='timepicker form-control' id='dtBatDauThiVd' value='" + (keHoachLop.BATDAUTHIVD == null ? null : keHoachLop.BATDAUTHIVD.Value.ToString("HH:mm")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtKetThucThiVd' value='" + (keHoachLop.KETTHUCTHIVD == null ? null : keHoachLop.KETTHUCTHIVD.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                      <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemThiVd' value='" + keHoachLop.DIADIEMTHIVD + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4.3.3. " + WebLanguage.GetLanguage(OSiteParam, "Thi thực hành") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99' class='timepicker form-control' style='z-index: 0;' id='dtBatDauThiTh' value='" + (keHoachLop.BATDAUTHITH == null ? null : keHoachLop.BATDAUTHITH.Value.ToString("HH:mm")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtKetThucThiTh' value='" + (keHoachLop.KETTHUCTHITH == null ? null : keHoachLop.KETTHUCTHITH.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                  <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemThiTh' value='" + keHoachLop.DIADIEMTHITH + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>5. " + WebLanguage.GetLanguage(OSiteParam, "Bế giảng và trao chứng chỉ") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Thời gian") + ":\r\n" +
                    "                  <input type='text' data-mask='99:99 99/99/9999' style='z-index: 0;' class='datetimepicker form-control' id='dtThoiGianBeGiang' value='" + (keHoachLop.THOIGIANBEGIANG == null ? null : keHoachLop.THOIGIANBEGIANG.Value.ToString("HH:mm dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "           <div class='col-md-12 col-xs-12'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                                       WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + ":\r\n" +
                    "                   <input type='text' class='form-control' style='z-index: 0;' id='txtDiaDiemBeGiang' value='" + keHoachLop.DIADIEMBEGIANG + "'>" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:50px;'>" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                    "                   <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                    "                   <div style='width:100%; text-align:center;'><input type='text' class='form-control' id='txtNguoiLap' value='" + keHoachLop.NGUOILAP + "'></div>\r\n" +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "           <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\">\r\n" +
                    "                   <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                                       cbbLanhDao +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                #endregion
                    "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "   </center> \r\n" +
                    "</div>\r\n" +
                    "<div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                        "   <input type='button' class='popupClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Đóng") + "' onclick=\"$('#divFormModal').modal('hide');\" style='float:right; margin-right: 14px; margin-top: 7px;'> \r\n" +
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawLichHocLt(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_LichLyThuyetCls lichLyThuyet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().CreateModel(ORenderInfo, khoaHocId);
                OwnerUserCls nguoiTao = null;
                if (lichLyThuyet == null)
                    lichLyThuyet = new DT_LichLyThuyetCls() { ID = khoaHocId };
                else nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, lichLyThuyet.NGUOITAO_ID);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                BacSyCls ptChuyenMon = string.IsNullOrEmpty(lichLyThuyet.PTCHUYENMON_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.PTCHUYENMON_ID);
                BacSyCls lanhDao = string.IsNullOrEmpty(lichLyThuyet.LANHDAO_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.LANHDAO_ID);

                string html =
                    "   <center> \r\n" +
                    "       <div style='width: 1200px; padding: 0 50px 50px 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "          <div class='row' style='margin-bottom: 20px;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='border:solid 1px; text-align:center; width: 180px; float:right;'>BM.09.QT.03.ĐT</div>" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>" +
                    "          <div class='row'>" +
                    "              <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                    "                  " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH VIỆN YHCT NGHỆ AN") + " </b>\r\n" +
                    "              </div>\r\n" +
                    "              <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "LỊCH HỌC LÝ THUYẾT") + "</b><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:30px;'>" +
                    "               <div class='col-md-3 col-xs-3'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Tên khóa đào tạo") + "\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-9 col-xs-9'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <span id='spThoiLuong' class=\"valueForm\">" + string.Format(": {0} - <b>K</b> {1}", khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-3 col-xs-3'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + "\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-9 col-xs-9'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <span id='spThoiLuong' class=\"valueForm\">: " + lichLyThuyet.DIADIEM + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-3 col-xs-3'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian") + "\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "              <div class='col-md-9 col-xs-9'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <span id='spThoiLuong' class=\"valueForm\">" +
                    "                          : <b>" + WebLanguage.GetLanguage(OSiteParam, "Từ") + "</b>\r\n" +
                                               (lichLyThuyet.BATDAU == null ? null : lichLyThuyet.BATDAU.Value.ToString("dd/MM/yyyy")) + " - " +
                                               (lichLyThuyet.KETTHUC == null ? null : lichLyThuyet.KETTHUC.Value.ToString("dd/MM/yyyy")) +
                    "                       </span>\r\n" +
                    "                   </div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                                    DrawLichLyThuyetChiTiets(ORenderInfo, lichLyThuyet.ID, false).HtmlContent +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:50px;'>" +
                    "               <div class='col-md-4 col-xs-4'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                    "                       <div style='width:100%; text-align:center;'>" + (nguoiTao != null ? nguoiTao.FullName : null) + "</div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-4 col-xs-4'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "PHỤ TRÁCH CHUYÊN MÔN") + "</div>\r\n" +
                    "                       <div style='width:100%; text-align:center;'>" + (ptChuyenMon == null ? null : ptChuyenMon.HOTEN) + "</div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-4 col-xs-4'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                    "                       <div style='width:100%; text-align:center;'>" + (lanhDao == null ? null : lanhDao.HOTEN) + "</div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "   </center> \r\n";
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
        public static AjaxOut PopupLichHocLt(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_LichLyThuyetCls lichLyThuyet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().CreateModel(ORenderInfo, khoaHocId);
                string nguoiTao = null;
                if (lichLyThuyet == null)
                {
                    lichLyThuyet = new DT_LichLyThuyetCls() { ID = khoaHocId, NGAYTAO = DateTime.Now, NGUOITAO_ID = user.OwnerUserId };
                    nguoiTao = user.FullName;
                }
                else
                {
                    nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, lichLyThuyet.NGUOITAO_ID).FullName;
                }
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);

                string cbbPtChuyenMon = "<select id = 'cbbPtChuyenMon' class='form-control'>\r\n";
                if (!string.IsNullOrEmpty(lichLyThuyet.PTCHUYENMON_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.PTCHUYENMON_ID);
                    if (bacSy != null)
                        cbbPtChuyenMon += string.Format("<option value={0}>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbPtChuyenMon += "</select>\r\n";

                string cbbLanhDao = "<select id = 'cbbLanhDao' class='form-control'>\r\n";
                if (!string.IsNullOrEmpty(lichLyThuyet.LANHDAO_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.LANHDAO_ID);
                    if (bacSy != null)
                        cbbLanhDao += string.Format("<option value={0}>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbLanhDao += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "<form action='javascript:SaveLichHocLt();'> \r\n" +
                "<input type='hidden' id='hdClose'>\r\n" +
                "<div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                "   <center> \r\n" +
                "       <div style='width: 100%; padding: 0 50px 50px 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                "          <div class='row' style='margin-bottom: 20px;'>" +
                "               <div class='col-md-12 col-xs-12'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                "                       <div style='border:solid 1px; text-align:center; width: 180px; float:right;'>BM.09.QT.03.ĐT</div>" +
                                    "</div>\r\n" +
                "              </div>\r\n" +
                "          </div>" +
                "          <div class='row'>" +
                "              <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                "                  " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH VIỆN YHCT NGHỆ AN") + " </b>\r\n" +
                "              </div>\r\n" +
                "              <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "LỊCH HỌC LÝ THUYẾT") + "</b><br>\r\n" +
                "              </div>\r\n" +
                "          </div>\r\n" +
                "          <div class='row' style='margin-top:30px;'>" +
                "             <div class='col-md-3 col-xs-3'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                                   WebLanguage.GetLanguage(OSiteParam, "Tên khóa đào tạo") + "\r\n" +
                "               </div>\r\n" +
                "             </div>\r\n" +
                "             <div class='col-md-9 col-xs-9'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                                 string.Format(": {0} - <b>K</b> {1}", khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "\r\n" +
                "               </div>\r\n" +
                "             </div>\r\n" +
                "          </div>\r\n" +
                "          <div class='row'>" +
                "           <div class='col-md-3 col-xs-3'>\r\n" +
                "             <div class=\"form-group\">\r\n" +
                                 WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + "\r\n" +
                "             </div>\r\n" +
                "           </div>\r\n" +
                "           <div class='col-md-9 col-xs-9'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                "                   : <input type='text' class='form-control' style='display: inline; width: 99%; z-index: 0;' id='txtDiaDiem' value='" + lichLyThuyet.DIADIEM + "'>" +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "          </div>\r\n" +
                "          <div class='row'>" +
                "           <div class='col-md-3 col-xs-3'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                                   WebLanguage.GetLanguage(OSiteParam, "Thời gian") + "\r\n" +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "           <div class='col-md-9 col-xs-9'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                "                   : <b>" + WebLanguage.GetLanguage(OSiteParam, "Từ") + "</b>\r\n" +
                "                   <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='width: 100px; display:inline;' id='dtBatDau' value='" + (lichLyThuyet.BATDAU == null ? null : lichLyThuyet.BATDAU.Value.ToString("dd/MM/yyyy")) + "'> - " +
                "                   <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='width: 100px; display:inline;' id='dtKetThuc' value='" + (lichLyThuyet.KETTHUC == null ? null : lichLyThuyet.KETTHUC.Value.ToString("dd/MM/yyyy")) + "'>" +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "          </div>\r\n" +
                "          <div class='row'>" +
                "              <div class='col-md-12 col-xs-12' id='divLichLyThuyetChiTiet'>\r\n" +
                                DrawLichLyThuyetChiTiets(ORenderInfo, lichLyThuyet.ID, true).HtmlContent +
                "              </div>\r\n" +
                "          </div>\r\n" +
                "          <div class='row' style='margin-top:50px;'>" +
                "           <div class='col-md-4 col-xs-4'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                "                   <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                "                   <div style='width:100%; text-align:center;'>" + nguoiTao + "</div>\r\n" +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "           <div class='col-md-4 col-xs-4'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                "                  <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "PHỤ TRÁCH CHUYÊN MÔN") + "</div>\r\n" +
                                   cbbPtChuyenMon +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "           <div class='col-md-4 col-xs-4'>\r\n" +
                "               <div class=\"form-group\">\r\n" +
                "                  <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                                   cbbLanhDao +
                "               </div>\r\n" +
                "           </div>\r\n" +
                "          </div>\r\n" +
                "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                "   </center> \r\n" +
                "</div>\r\n" +
                "<div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                "   <input type='button' class='popupClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Đóng") + "' onclick=\"$('#divFormModal').modal('hide');\" style='float:right; margin-right: 14px; margin-top: 7px;'> \r\n" +
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawLichLyThuyetChiTiets(RenderInfoCls ORenderInfo, string lichLyThuyetId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string html = "";

                if (isEditor)
                {
                    string cbbHinhThucHoc = "    <select id=\"cbbHinhThucHoc\" class=\"form-control\" style=\"border-radius: 4px;\">\r\n";
                    foreach (var hinhThucHoc in DT_LichLyThuyetChiTietParser.HinhThucHocs)
                        cbbHinhThucHoc += string.Format("<option value={0}>{1}</option>", hinhThucHoc.Key, hinhThucHoc.Value);
                    cbbHinhThucHoc += "    </select>\r\n";

                    List<DT_LichLyThuyetChiTietCls> lichLyThuyetChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichLyThuyetChiTiets" + lichLyThuyetId) as List<DT_LichLyThuyetChiTietCls>;
                    html =
                         "<table width=100% class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày") + " </th> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Từ giờ") + " </th> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đến giờ") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Hình thức học") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                                    "<th width=60 style='text-align:center;'><a href='javascript:ShowLichLyThuyetChiTiet()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n" +
                                "<tr id='trAddLichLyThuyetChiTiet' style='display:none;'> \r\n" +
                                    "<input type='hidden' id='hdLichLyThuyetChiTietId' value=''>\r\n" +
                                    "<td><input type='text' data-mask='99/99/9999' class='form-control datepicker' id='txtNgay'></td> \r\n" +
                                    "<td><input type='text' data-mask='99:99' class='form-control timepicker' id='txtThoiGian'></td> \r\n" +
                                    "<td><input type='text' data-mask='99:99' class='form-control timepicker' id='txtThoiGianKetThuc'></td> \r\n" +
                                    "<td><input type='text' class='form-control' id='txtNoiDung' style ='z-index:0;'></td> \r\n" +
                                    "<td><div id='divCbbGiangVien'></div></td> \r\n" +
                                    "<td>\r\n" + cbbHinhThucHoc + "</td> \r\n" +
                                    "<td><input type='text' class='form-control' id='txtGhiChu'></td> \r\n" +
                                    "<td style='text-align:center;'><a href='javascript:SaveLichLyThuyetChiTiet(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                                "</tr> \r\n";
                    for (int iIndex = 0; iIndex < lichLyThuyetChiTiets.Count; iIndex++)
                    {
                        cbbHinhThucHoc = "    <select id=\"cbbHinhThucHoc" + iIndex + "\" class=\"form-control CssEditorItemLltct\" style=\"border-radius: 4px; display:none;\">\r\n";
                        foreach (var hinhThucHoc in DT_LichLyThuyetChiTietParser.HinhThucHocs)
                            cbbHinhThucHoc += string.Format("<option value={0} {1}>{2}</option>", hinhThucHoc.Key, lichLyThuyetChiTiets[iIndex].HINHTHUCHOC == hinhThucHoc.Key ? "checked" : null, hinhThucHoc.Value);
                        cbbHinhThucHoc += "    </select>\r\n";

                        BacSyCls giangVien = string.IsNullOrEmpty(lichLyThuyetChiTiets[iIndex].GIANGVIEN_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyetChiTiets[iIndex].GIANGVIEN_ID);
                        html += "<tr> \r\n" +
                                    "<input type='hidden' id='hdLichLyThuyetChiTietId" + iIndex + "' value='" + lichLyThuyetChiTiets[iIndex].ID + "'>\r\n" +
                                    "<input type='hidden' id='hdGiangVienId" + iIndex + "' value='" + lichLyThuyetChiTiets[iIndex].GIANGVIEN_ID + "'>\r\n" +
                                    "<td style='text-align: center;'><input type='text' class='form-control CssEditorItemLltct' style='display:none;' id='txtNgay" + iIndex + "' value='" + lichLyThuyetChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "'><span class='CssDisplayItemLltct' id='spNgay" + iIndex + "'>" + lichLyThuyetChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "</span></td> \r\n" +
                                    "<td style='text-align: center;'><input type='text' class='form-control CssEditorItemLltct' style='display:none;' id='txtThoiGian" + iIndex + "' value='" + (lichLyThuyetChiTiets[iIndex].THOIGIAN == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "'><span class='CssDisplayItemLltct' id='spThoiGian" + iIndex + "'>" + (lichLyThuyetChiTiets[iIndex].THOIGIAN == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "</span></td> \r\n" +
                                    "<td style='text-align: center;'><input type='text' class='form-control CssEditorItemLltct' style='display:none;' id='txtThoiGianKetThuc" + iIndex + "' value='" + (lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH:mm")) + "'><span class='CssDisplayItemLltct' id='spThoiGianKetThuc" + iIndex + "'>" + (lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH:mm")) + "</span></td> \r\n" +
                                    "<td><input type='text' class='form-control CssEditorItemLltct' style='display:none; ' id='txtNoiDung" + iIndex + "' value='" + lichLyThuyetChiTiets[iIndex].NOIDUNG + "'><span class='CssDisplayItemLltct' id='spNoiDung" + iIndex + "'>" + lichLyThuyetChiTiets[iIndex].NOIDUNG + "</span></td> \r\n" +
                                    "<td><div id='divCbbGiangVien" + iIndex + "' class='CssEditorItemLltct' style='display:none'></div><span class='CssDisplayItemLltct' id='spGiangVien" + iIndex + "'>" + (giangVien != null ? giangVien.HOTEN : null) + "</span></td> \r\n" +
                                    "<td>\r\n" + cbbHinhThucHoc + "<span class='CssDisplayItemLltct' id='spHinhThucHoc" + iIndex + "'>" + DT_LichLyThuyetChiTietParser.HinhThucHocs[lichLyThuyetChiTiets[iIndex].HINHTHUCHOC] + "</span></td> \r\n" +
                                    "<td><input type='text' class='form-control CssEditorItemLltct' style='display:none; ' id='txtGhiChu" + iIndex + "' value='" + lichLyThuyetChiTiets[iIndex].GHICHU + "'><span class='CssDisplayItemLltct' id='spGhiChu" + iIndex + "'>" + lichLyThuyetChiTiets[iIndex].GHICHU + "</span></td> \r\n" +
                                    "<td style='text-align:center;'>\r\n" +
                                        "<a id='btnSaveLichLyThuyetChiTiet" + iIndex + "' class='CssEditorItemLltct' style='display:none' href='javascript:SaveLichLyThuyetChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                                        "<a id='btnEditLichLyThuyetChiTiet" + iIndex + "' class='CssEditorItemLltct' href='javascript:ShowEditItemLineLltct(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                                        "<a id='btnDeleteLichLyThuyetChiTiet" + iIndex + "' class='CssEditorItemLltct' href='javascript:DeleteLichLyThuyetChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                    "</td> \r\n" +
                                "</tr> \r\n";
                    }
                    html += "<tr style='font-weight:bold; text-align:center;'> \r\n" +
                                "<td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                                "<td>" + lichLyThuyetChiTiets.Count + " " + WebLanguage.GetLanguage(OSiteParam, "buổi") + "</td> \r\n" +
                                "<td></td> \r\n" +
                                "<td></td> \r\n" +
                                "<td></td> \r\n" +
                                "<td></td> \r\n" +
                            "</tr> \r\n";
                    html += "</tbody> \r\n" +
                        "</table> \r\n";
                }
                else
                {
                    List<DT_LichLyThuyetChiTietCls> lichLyThuyetChiTiets = string.IsNullOrEmpty(lichLyThuyetId) ? new List<DT_LichLyThuyetChiTietCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = lichLyThuyetId }).ToList();
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichLyThuyetChiTiets" + lichLyThuyetId, lichLyThuyetChiTiets);

                    html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thứ") + " </th> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày") + " </th> \r\n" +
                                    "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Hình thức học") + " </th> \r\n" +
                                    "<th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                    for (int iIndex = 0; iIndex < lichLyThuyetChiTiets.Count; iIndex++)
                    {
                        BacSyCls giangVien = string.IsNullOrEmpty(lichLyThuyetChiTiets[iIndex].GIANGVIEN_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyetChiTiets[iIndex].GIANGVIEN_ID);
                        html += "<tr> \r\n" +
                                    "<td style='text-align: center;'>" + GetDayOfWeek(OSiteParam, lichLyThuyetChiTiets[iIndex].NGAY.DayOfWeek) + "</td> \r\n" +
                                    "<td style='text-align: center;'>" + lichLyThuyetChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "</td> \r\n" +
                                    "<td style='text-align: center;'>" + (lichLyThuyetChiTiets[iIndex].THOIGIAN == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "-" + (lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH:mm")) + "</td> \r\n" +
                                    "<td>" + lichLyThuyetChiTiets[iIndex].NOIDUNG + "</td> \r\n" +
                                    "<td>" + (giangVien != null ? giangVien.HOTEN : null) + "</td> \r\n" +
                                    "<td>" + DT_LichLyThuyetChiTietParser.HinhThucHocs[lichLyThuyetChiTiets[iIndex].HINHTHUCHOC] + "</td> \r\n" +
                                    "<td>" + lichLyThuyetChiTiets[iIndex].GHICHU + "</td> \r\n" +
                                "</tr> \r\n";
                    }
                    html += "<tr style='font-weight:bold; text-align:center;'> \r\n" +
                                    "<td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                                    "<td>" + lichLyThuyetChiTiets.Count + " " + WebLanguage.GetLanguage(OSiteParam, "buổi") + "</td> \r\n" +
                                    "<td></td> \r\n" +
                                    "<td></td> \r\n" +
                                    "<td></td> \r\n" +
                                "</tr> \r\n";
                    html += "</tbody> \r\n" +
                        "</table> \r\n";
                }
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
        public static AjaxOut DrawLichHocTh(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_LichThucHanhCls lichThucHanh = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().CreateModel(ORenderInfo, khoaHocId);
                OwnerUserCls nguoiTao = null;
                if (lichThucHanh == null)
                    lichThucHanh = new DT_LichThucHanhCls() { ID = khoaHocId };
                else nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, lichThucHanh.NGUOITAO_ID);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                BacSyCls ptChuyenMon = string.IsNullOrEmpty(lichThucHanh.PTCHUYENMON_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanh.PTCHUYENMON_ID);
                BacSyCls lanhDao = string.IsNullOrEmpty(lichThucHanh.LANHDAO_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanh.LANHDAO_ID);

                string html =
                    "   <center> \r\n" +
                    "       <div style='width: 1200px; padding: 0 50px 50px 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "          <div class='row' style='margin-bottom: 20px;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                                        "<div class=\"form-group\">\r\n" +
                    "                       <div style='border:solid 1px; text-align:center; width: 180px; float:right;'>BM.09.QT.03.ĐT</div>" +
                                        "</div>\r\n" +
                    "              </div>\r\n" +
                    "          </div>" +
                    "          <div class='row'>" +
                    "              <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                    "                  " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH VIỆN YHCT NGHỆ AN") + " </b>\r\n" +
                    "              </div>\r\n" +
                    "              <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "LỊCH HỌC THỰC HÀNH") + "</b><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:30px;'>" +
                                "<div class='col-md-3 col-xs-3'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Tên khóa đào tạo") + "\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-9 col-xs-9'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<span id='spThoiLuong' class=\"valueForm\">" + string.Format(": {0} - <b>K</b> {1}", khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "</span>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                                "<div class='col-md-3 col-xs-3'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + "\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-9 col-xs-9'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<span id='spThoiLuong' class=\"valueForm\">: " + lichThucHanh.DIADIEM + "</span>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                                "<div class='col-md-3 col-xs-3'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian") + "\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-9 col-xs-9'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<span id='spThoiLuong' class=\"valueForm\">" +
                                            ": <b>" + WebLanguage.GetLanguage(OSiteParam, "Từ") + "</b>\r\n" +
                                            (lichThucHanh.BATDAU == null ? null : lichThucHanh.BATDAU.Value.ToString("dd/MM/yyyy")) + " - " +
                                            (lichThucHanh.KETTHUC == null ? null : lichThucHanh.KETTHUC.Value.ToString("dd/MM/yyyy")) +
                                        "</span>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                                "<div class='col-md-3 col-xs-3'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Nhóm/đối tượng") + "\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-9 col-xs-9'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<span id='spNhom' class=\"valueForm\">: " + lichThucHanh.NHOM + "</span>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                                    DrawLichThucHanhChiTiets(ORenderInfo, khoaHocId, lichThucHanh.ID, false).HtmlContent +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin-top:50px;'>" +
                                "<div class='col-md-4 col-xs-4'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                                        "<div style='width:100%; text-align:center;'>" + (nguoiTao != null ? nguoiTao.FullName : null) + "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-4 col-xs-4'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "PHỤ TRÁCH CHUYÊN MÔN") + "</div>\r\n" +
                                        "<div style='width:100%; text-align:center;'>" + (ptChuyenMon == null ? null : ptChuyenMon.HOTEN) + "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                                "<div class='col-md-4 col-xs-4'>\r\n" +
                                    "<div class=\"form-group\">\r\n" +
                                        "<div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                                        "<div style='width:100%; text-align:center;'>" + (lanhDao == null ? null : lanhDao.HOTEN) + "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n" +
                    "          </div>\r\n" +
                    "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "   </center> \r\n";
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
        public static AjaxOut DrawLichThucHanhs(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                bool qlKeHoachPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.QuanLyKeHoach.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool isEditor = khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && qlKeHoachPermission;
                string html = "";
                //List<DT_LichThucHanhCls> lichThucHanhs = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichThucHanhs" + khoaHocId) as List<DT_LichThucHanhCls>;
                DT_LichThucHanhCls[] lichThucHanhs = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Reading(ORenderInfo, new DT_LichThucHanhFilterCls() { KhoaHocId = khoaHocId });
                html =
                        "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                            "<thead> \r\n" +
                                "<tr> \r\n" +
                                "<th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhóm") + " </th> \r\n" +
                                "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + " </th> \r\n" +
                                "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + " </th> \r\n" +
                                "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + " </th> \r\n" +
                                "<th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + " </th> \r\n" +
                                (isEditor ?
                                "<th width=60 style='text-align:center;'><a href='javascript:PopupLichHocTh(\"\", true)' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" : null) +
                                "</tr> \r\n" +
                            "</thead> \r\n" +
                            "<tbody> \r\n";
                for (int iIndex = 0; iIndex < lichThucHanhs.Length; iIndex++)
                {
                    int hocVienQuantity = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocVienQuantity(ORenderInfo, lichThucHanhs[iIndex].ID);
                    html += "<tr> \r\n" +
                                "<input type='hidden' id='hdLichThucHanhId" + iIndex + "' value='" + lichThucHanhs[iIndex].ID + "'>\r\n" +
                                "<td style='text-align: center;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td><a href='javascript:PopupLichHocTh(\"" + lichThucHanhs[iIndex].ID + "\", false);'>" + lichThucHanhs[iIndex].NHOM + "</a></td> \r\n" +
                                "<td style='text-align: center;'>" + (lichThucHanhs[iIndex].BATDAU == null ? null : lichThucHanhs[iIndex].BATDAU.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                                "<td style='text-align: center;'>" + (lichThucHanhs[iIndex].KETTHUC == null ? null : lichThucHanhs[iIndex].KETTHUC.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                                "<td>" + lichThucHanhs[iIndex].DIADIEM + "</td> \r\n" +
                                "<td style='text-align: center;'>" + hocVienQuantity + "</td> \r\n" +
                                (isEditor ?
                                "<td style='text-align:center;'>\r\n" +
                                    "<a id='btnEditLichThucHanh" + iIndex + "' href='javascript:PopupLichHocTh(\"" + lichThucHanhs[iIndex].ID + "\", true);' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                                    "<a id='btnDeleteLichThucHanh" + iIndex + "' href='javascript:DeleteLichHocTh(\"" + lichThucHanhs[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                "</td> \r\n" : null) +
                            "</tr> \r\n";
                }
                html += "</tbody> \r\n" +
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
        public static AjaxOut PopupLichHocTh(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_LichThucHanhCls lichThucHanh = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().CreateModel(ORenderInfo, lichThucHanhId);
                string nguoiTao = null;
                if (lichThucHanh == null)
                {
                    lichThucHanh = new DT_LichThucHanhCls() { ID = lichThucHanhId, NGAYTAO = DateTime.Now, NGUOITAO_ID = user.OwnerUserId };
                    nguoiTao = user.FullName;
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + khoaHocId, new List<DT_HocVienCls>());
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + khoaHocId, new List<DT_LichThucHanhChiTietCls>());
                }
                else
                {
                    nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, lichThucHanh.NGUOITAO_ID).FullName;
                    List<DT_HocVienCls> hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocViens(ORenderInfo, lichThucHanhId).ToList();
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + lichThucHanh.ID, hocViens);
                    DT_LichThucHanhChiTietCls[] lichThucHanhChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { LICHTHUCHANH_ID = lichThucHanh.ID });
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + lichThucHanh.ID, lichThucHanhChiTiets.ToList());
                }
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);

                string cbbPtChuyenMon = "<select id = 'cbbPtChuyenMon' class='form-control'>\r\n";
                if (!string.IsNullOrEmpty(lichThucHanh.PTCHUYENMON_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanh.PTCHUYENMON_ID);
                    if (bacSy != null)
                        cbbPtChuyenMon += string.Format("<option value={0}>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbPtChuyenMon += "</select>\r\n";

                string cbbLanhDao = "<select id = 'cbbLanhDao' class='form-control'>\r\n";
                if (!string.IsNullOrEmpty(lichThucHanh.LANHDAO_ID))
                {
                    BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanh.LANHDAO_ID);
                    if (bacSy != null)
                        cbbLanhDao += string.Format("<option value={0}>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
                }
                cbbLanhDao += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "   <form action='javascript:SaveLichHocTh();'> \r\n" +
                "       <input type='hidden' id='hdClose'>\r\n" +
                "       <input type='hidden' id='hdLichThucHanhId' value='" + lichThucHanh.ID + "'>\r\n" +
                "       <div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                "          <center> \r\n" +
                "              <div style='width: 100%; padding: 0 50px 50px 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                "                 <div class='row' style='margin-bottom: 20px;'>" +
                "                      <div class='col-md-12 col-xs-12'>\r\n" +
                "                           <div class=\"form-group\">\r\n" +
                "                              <div style='border:solid 1px; text-align:center; width: 180px; float:right;'>BM.09.QT.03.ĐT</div>" +
                "                           </div>\r\n" +
                "                     </div>\r\n" +
                "                 </div>" +
                "                 <div class='row'>" +
                "                     <div class='col-md-5 col-xs-5' style='text-align:center;'>\r\n" +
                "                         " + WebLanguage.GetLanguage(OSiteParam, "BỘ Y TẾ") + "<br>\r\n" +
                "                         <b>" + WebLanguage.GetLanguage(OSiteParam, "BỆNH VIỆN YHCT NGHỆ AN") + " </b>\r\n" +
                "                     </div>\r\n" +
                "                     <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                "                         <b>" + WebLanguage.GetLanguage(OSiteParam, "LỊCH HỌC THỰC HÀNH") + "</b><br>\r\n" +
                "                     </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row' style='margin-top:30px;'>" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Tên khóa đào tạo") + "\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-9 col-xs-9'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                             string.Format(": {0} - <b>K</b> {1}", khoaHoc.TENKHOAHOC, khoaHoc.KHOA) + "\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row'>" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + "\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-9 col-xs-9'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           : <input type='text' class='form-control' style='display: inline; width: 99%;' id='txtDiaDiem' value='" + lichThucHanh.DIADIEM + "'>" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row'>" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian") + "\r\n" +
                "                       </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='col-md-9 col-xs-9'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           : <b>" + WebLanguage.GetLanguage(OSiteParam, "Từ") + "</b>\r\n" +
                "                           <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='width: 100px; display:inline;' id='dtBatDau' value='" + (lichThucHanh.BATDAU == null ? null : lichThucHanh.BATDAU.Value.ToString("dd/MM/yyyy")) + "'> - " +
                "                           <input type='text' data-mask='99/99/9999' class='datepicker form-control' style='width: 100px; display:inline;' id='dtKetThuc' value='" + (lichThucHanh.KETTHUC == null ? null : lichThucHanh.KETTHUC.Value.ToString("dd/MM/yyyy")) + "'>" +
                "                       </div>\r\n" +
                "                 </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row'>" +
                "                      <div class='col-md-3 col-xs-3'>\r\n" +
                "                          <div class=\"form-group\">\r\n" +
                                              WebLanguage.GetLanguage(OSiteParam, "Nhóm/đối tượng") + "\r\n" +
                "                          </div>\r\n" +
                "                      </div>\r\n" +
                "                      <div class='col-md-9 col-xs-9'>\r\n" +
                "                          <div class=\"form-group\">\r\n" +
                "                              : <input type='text' class='form-control' style='display: inline; width: 99%; z-index: 0;' id='txtNhom' value='" + lichThucHanh.NHOM + "'>" +
                "                          </div>\r\n" +
                "                      </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row'>" +
                "                      <div class='tabs-container'>\r\n" +
                "                          <ul class='nav nav-tabs'>\r\n" +
                "                              <li class='active'><a data-toggle='tab' href='#tabHocVienTrongNhom'>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách học viên") + "</a></li>\r\n" +
                "                              <li class=''><a data-toggle='tab' href='#tabLichThucHanhChiTiet'>" + WebLanguage.GetLanguage(OSiteParam, "Lịch học thực hành chi tiết") + "</a></li>\r\n" +
                "                          </ul>\r\n" +
                "                          <div class='tab-content'>\r\n" +
                "                              <div id='tabHocVienTrongNhom' class='tab-pane active'>\r\n" +
                                                   DrawHocVienTrongNhoms(ORenderInfo, khoaHocId, lichThucHanh.ID, isEditor).HtmlContent +
                "                              </div>\r\n" +
                "                              <div id='tabLichThucHanhChiTiet' class='tab-pane'>\r\n" +
                                                   DrawLichThucHanhChiTiets(ORenderInfo, khoaHocId, lichThucHanh.ID, isEditor).HtmlContent +
                "                              </div>\r\n" +
                "                          </div>\r\n" +
                "                      </div>\r\n" +
                "                 </div>\r\n" +
                "                 <div class='row' style='margin-top:50px;'>" +
                "                   <div class='col-md-4 col-xs-4'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "NGƯỜI LẬP") + "</div>\r\n" +
                "                           <div style='width:100%; text-align:center;'>" + nguoiTao + "</div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-4 col-xs-4'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "PHỤ TRÁCH CHUYÊN MÔN") + "</div>\r\n" +
                                           cbbPtChuyenMon +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-4 col-xs-4'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           <div style='width:100%; text-align:center; font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "TUQ.GIÁM ĐỐC") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "KT.GIÁM ĐỐC TT ĐT & CĐT") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "PHÓ GIÁM ĐỐC TT ĐT & CĐT") + "</div>\r\n" +
                                           cbbLanhDao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                 </div>\r\n" +
                "              </div> \r\n" +//set chiều rộng tương đương khổ giấy A4 nằm ngang
                "          </center> \r\n" +
                "       </div>\r\n" +
                "       <div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                "           <input type='button' class='popupClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Đóng") + "' onclick=\"$('#divFormModal').modal('hide');\" style='float:right; margin-right: 14px; margin-top: 7px;'> \r\n" +
                (       isEditor ?
                "           <input type='submit' class='popupSaveClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu & đóng") + "' onclick='javascript:$(\"#hdClose\").val(1);' style='float:right; margin: 7px;'> \r\n" +
                "           <input type='submit' class='popupSave btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' onclick='javascript:$(\"#hdClose\").val(0);' style='float:right; margin: 7px;'> \r\n" : null) +
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawLichThucHanhChiTiets(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string html = "";
                if (isEditor)
                {
                    List<DT_LichThucHanhChiTietCls> lichThucHanhChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_LichThucHanhChiTietCls>;
                    html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Từ giờ") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đến giờ") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                         "          <th width=60 style='text-align:center;'><a href='javascript:ShowLichThucHanhChiTiet()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n" +
                         "      <tr id='trAddLichThucHanhChiTiet' style='display:none;'> \r\n" +
                         "          <input type='hidden' id='hdLichThucHanhChiTietId' value=''>\r\n" +
                         "          <input type='hidden' id='hdLichThucHanhId' value=''>\r\n" +
                         "          <td><input type='text' data-mask='99/99/9999' class='form-control datepicker' id='txtNgay'></td> \r\n" +
                         "          <td><input type='text' data-mask='99:99' class='form-control timepicker' id='txtThoiGian'></td> \r\n" +
                         "          <td><input type='text' data-mask='99:99' class='form-control timepicker' id='txtThoiGianKetThuc'></td> \r\n" +
                         "          <td><input type='text' class='form-control' id='txtNoiDung' style ='z-index: 0;'></td> \r\n" +
                         "          <td><div id='divCbbGiangVien'></div></td> \r\n" +
                         "          <td><input type='text' class='form-control' id='txtGhiChu'></td> \r\n" +
                         "          <td style='text-align:center;'><a href='javascript:SaveLichThucHanhChiTiet(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                         "      </tr> \r\n";
               for (int iIndex = 0; iIndex < lichThucHanhChiTiets.Count; iIndex++)
                    {
                        BacSyCls giangVien = string.IsNullOrEmpty(lichThucHanhChiTiets[iIndex].GIANGVIEN_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanhChiTiets[iIndex].GIANGVIEN_ID);
                html += "<tr> \r\n" +
                        "   <input type='hidden' id='hdLichThucHanhChiTietId" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].ID + "'>\r\n" +
                        "   <input type='hidden' id='hdLichThucHanhId" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].LICHTHUCHANH_ID + "'>\r\n" +
                        "   <input type='hidden' id='hdGiangVienId" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].GIANGVIEN_ID + "'>\r\n" +
                        "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemLthct' style='display:none;' id='txtNgay" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "'><span class='CssDisplayItemLthct' id='spNgay" + iIndex + "'>" + lichThucHanhChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "</span></td> \r\n" +
                        "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemLthct' style='display:none;' id='txtThoiGian" + iIndex + "' value='" + (lichThucHanhChiTiets[iIndex].THOIGIAN == null ? null : lichThucHanhChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "'><span class='CssDisplayItemLthct' id='spThoiGian" + iIndex + "'>" + (lichThucHanhChiTiets[iIndex].THOIGIAN == null ? null : lichThucHanhChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "</span></td> \r\n" +
                        "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemLthct' style='display:none;' id='txtThoiGianKetThuc" + iIndex + "' value='" + (lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH:mm")) + "'><span class='CssDisplayItemLthct' id='spThoiGianKetThuc" + iIndex + "'>" + (lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH:mm")) + "</span></td> \r\n" +
                        "   <td><input type='text' class='form-control CssEditorItemLthct' style='display:none; ' id='txtNoiDung" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].NOIDUNG + "'><span class='CssDisplayItemLthct' id='spNoiDung" + iIndex + "'>" + lichThucHanhChiTiets[iIndex].NOIDUNG + "</span></td> \r\n" +
                        "   <td><div id='divCbbGiangVien" + iIndex + "' class='CssEditorItemLthct' style='display:none'></div><span class='CssDisplayItemLthct' id='spGiangVien" + iIndex + "'>" + (giangVien != null ? giangVien.HOTEN : null) + "</span></td> \r\n" +
                        "   <td><input type='text' class='form-control CssEditorItemLthct' style='display:none; ' id='txtGhiChu" + iIndex + "' value='" + lichThucHanhChiTiets[iIndex].GHICHU + "'><span class='CssDisplayItemLthct' id='spGhiChu" + iIndex + "'>" + lichThucHanhChiTiets[iIndex].GHICHU + "</span></td> \r\n" +
                        "   <td style='text-align:center;'>\r\n" +
                        "       <a id='btnSaveLichThucHanhChiTiet" + iIndex + "' class='CssEditorItemLthct' style='display:none' href='javascript:SaveLichThucHanhChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                        "       <a id='btnEditLichThucHanhChiTiet" + iIndex + "' class='CssEditorItemLthct' href='javascript:ShowEditItemLineLthct(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                        "       <a id='btnDeleteLichThucHanhChiTiet" + iIndex + "' class='CssEditorItemLthct' href='javascript:DeleteLichThucHanhChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                        "   </td> \r\n" +
                        "</tr> \r\n";
                    }
                html += "<tr style='font-weight:bold; text-align:center;'> \r\n" +
                        "   <td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                        "   <td>" + lichThucHanhChiTiets.Count + " " + WebLanguage.GetLanguage(OSiteParam, "buổi") + "</td> \r\n" +
                        "   <td></td> \r\n" +
                        "   <td></td> \r\n" +
                        "   <td></td> \r\n" +
                        "</tr> \r\n";
                html += "</tbody> \r\n" +
                        "</table> \r\n";
                }
                else
                {
                    List<DT_LichThucHanhChiTietCls> lichThucHanhChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { LICHTHUCHANH_ID = lichThucHanhId }).ToList();
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + lichThucHanhId, lichThucHanhChiTiets);
                    html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thứ") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Giảng viên") + " </th> \r\n" +
                         "          <th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                for (int iIndex = 0; iIndex < lichThucHanhChiTiets.Count; iIndex++)
                {
                BacSyCls giangVien = string.IsNullOrEmpty(lichThucHanhChiTiets[iIndex].GIANGVIEN_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichThucHanhChiTiets[iIndex].GIANGVIEN_ID);
                html += "<tr> \r\n" +
                        "   <td style='text-align: center;'>" + GetDayOfWeek(OSiteParam, lichThucHanhChiTiets[iIndex].NGAY.DayOfWeek) + "</td> \r\n" +
                        "   <td style='text-align: center;'>" + lichThucHanhChiTiets[iIndex].NGAY.ToString("dd/MM/yyyy") + "</td> \r\n" +
                        "   <td style='text-align: center;'>" + (lichThucHanhChiTiets[iIndex].THOIGIAN == null ? null : lichThucHanhChiTiets[iIndex].THOIGIAN.Value.ToString("HH:mm")) + "-" + (lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC == null ? null : lichThucHanhChiTiets[iIndex].THOIGIANKETTHUC.Value.ToString("HH: mm")) + "</td> \r\n" +
                        "   <td>" + lichThucHanhChiTiets[iIndex].NOIDUNG + "</td> \r\n" +
                        "   <td>" + (giangVien != null ? giangVien.HOTEN : null) + "</td> \r\n" +
                        "   <td>" + lichThucHanhChiTiets[iIndex].GHICHU + "</td> \r\n" +
                        "</tr> \r\n";
                }
                html += "<tr style='font-weight:bold; text-align:center;'> \r\n" +
                        "   <td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                        "   <td>" + lichThucHanhChiTiets.Count + " " + WebLanguage.GetLanguage(OSiteParam, "buổi") + "</td> \r\n" +
                        "   <td></td> \r\n" +
                        "   <td></td> \r\n" +
                        "</tr> \r\n";
                html += "</tbody> \r\n" +
                        "</table> \r\n";
                    }
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
        public static AjaxOut DrawHocVienTrongNhoms(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;

                string html = "";
                if (isEditor)
                {
                    List<DT_HocVienCls> hocViens = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_HocVienCls>;
                    var hocVienComboboxs = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId })
                        .Where(o => !hocViens.Any(hv => hv.ID == o.ID));
                    string cbbHocVien = "<select id = 'cbbHocVien' onchange='cbbHocVien_onchange(this)'>";
                    foreach (var hv in hocVienComboboxs)
                        cbbHocVien += string.Format("<option value='{0}'>{1} - {2}</option>", hv.ID, hv.MA, hv.HOTEN);
                    cbbHocVien += "</select>\r\n";
                    html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=50 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh") + " </th> \r\n" +
                         "          <th width=60 style='text-align:center;'><a href='javascript:ShowHocVienTrongNhom()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n" +
                         "      <tr id='trAddHocVienTrongNhom' style='display:none;'> \r\n" +
                         "          <input type='hidden' id='hdHocVienId' value=''>\r\n" +
                         "          <input type='hidden' id='hdLichThucHanhId' value=''>\r\n" +
                         "          <td></td> \r\n" +
                         "          <td>" + cbbHocVien + "</td> \r\n" +
                         "          <td id='tdMa'  style='text-align: center;'></td> \r\n" +
                         "          <td id='tdGioiTinh'  style='text-align: center;'></td> \r\n" +
                         "          <td id='tdNgaySinh'  style='text-align: center;'></td> \r\n" +
                         "          <td style='text-align:center;'><a href='javascript:SaveHocVienTrongNhom(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                         "      </tr> \r\n";
                    for (int iIndex = 0; iIndex < hocViens.Count; iIndex++)
                    {
                    html += 
                        "<tr> \r\n" +
                        "   <input type='hidden' id='hdHocVienId" + iIndex + "' value='" + hocViens[iIndex].ID + "'>\r\n" +
                        "   <input type='hidden' id='hdLichThucHanhId" + iIndex + "' value='"+ lichThucHanhId +"'>\r\n" +
                        "   <td style='text-align: center;'>" + (iIndex + 1) + "</td> \r\n" +
                        "   <td>" + hocViens[iIndex].HOTEN + "</td> \r\n" +
                        "   <td style='text-align: center;'>" + hocViens[iIndex].MA + "</td> \r\n" +
                        "   <td style='text-align: center;'>" + (hocViens[iIndex].GIOITINH == null ? null : Common.BenhNhan.GioiTinhs[hocViens[iIndex].GIOITINH.Value]) + "</td> \r\n" +
                        "   <td style='text-align: center;'>" + (hocViens[iIndex].NGAYSINH == null ? null : hocViens[iIndex].NGAYSINH.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "   <td style='text-align:center;'>\r\n" +
                        "       <a id='btnDeleteHocVienTrongNhom" + iIndex + "' href='javascript:DeleteHocVienTrongNhom(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                        "   </td> \r\n" +
                        "</tr> \r\n";
                    }
                    html += 
                        "<tr style='font-weight:bold; text-align:center;'> \r\n" +
                        "   <td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                        "   <td colspan=2>" + hocViens.Count + " " + WebLanguage.GetLanguage(OSiteParam, "học viên") + "</td> \r\n" +
                        "   <td></td> \r\n" +
                        "</tr> \r\n";
                    html +=
                        "</tbody> \r\n" +
                        "</table> \r\n";
                }
                else
                {
                    List<DT_HocVienCls> hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocViens(ORenderInfo, lichThucHanhId).ToList();
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + lichThucHanhId, hocViens);
                    html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                         "  <thead> \r\n" +
                         "      <tr> \r\n" +
                         "          <th width=50 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                         "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + " </th> \r\n" +
                         "          <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh") + " </th> \r\n" +
                         "      </tr> \r\n" +
                         "  </thead> \r\n" +
                         "  <tbody> \r\n";
                    for (int iIndex = 0; iIndex < hocViens.Count; iIndex++)
                    {
                        html += 
                        "       <tr> \r\n" +
                        "           <td style='text-align: center;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td>" + hocViens[iIndex].HOTEN + "</td> \r\n" +
                        "           <td style='text-align: center;'>" + hocViens[iIndex].MA + "</td> \r\n" +
                        "           <td style='text-align: center;'>" + (hocViens[iIndex].GIOITINH == null ? null : Common.BenhNhan.GioiTinhs[hocViens[iIndex].GIOITINH.Value]) + "</td> \r\n" +
                        "           <td style='text-align: center;'>" + (hocViens[iIndex].NGAYSINH == null ? null : hocViens[iIndex].NGAYSINH.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "       </tr> \r\n";
                    }
                    html += 
                        "       <tr style='font-weight:bold; text-align:center;'> \r\n" +
                        "           <td colspan=3>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                        "           <td colspan=2>" + hocViens.Count + " " + WebLanguage.GetLanguage(OSiteParam, "học viên") + "</td> \r\n" +
                        "       </tr> \r\n";
                    html +=
                        "</tbody> \r\n" +
                        "</table> \r\n";
                }
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
        public static string CbbBacSy(RenderInfoCls ORenderInfo, int? id, string value)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string cbbBacSy =
                string.Format("<select id = 'cbbBacSy{0}' style='display:{1}; font-size: 20px;' {2}>\r\n", id, id != null ? "none" : "block", id != null ? "class='CssEditorItemBacSy'" : null);
            if (!string.IsNullOrEmpty(value))
            {
                BacSyCls bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, value);
                if (bacSy != null)
                    cbbBacSy += string.Format("<option value={0} selected>{1} - {2}</option>\r\n", bacSy.ID, bacSy.MA, bacSy.HOTEN);
            }
            cbbBacSy += "</select>\r\n";
            return cbbBacSy;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawDiemDanhLt(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichLyThuyetChiTietCls[] lichLyThuyetChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = khoaHocId });
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId });
                DT_DiemDanhLyThuyetCls[] diemDanhs = CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Reading(ORenderInfo, new DT_DiemDanhLyThuyetFilterCls() { KhoaHocId = khoaHocId });
                int lichLyThuyetChiTietQuantity = lichLyThuyetChiTiets.Count();
                int hocVienQuantity = hocViens.Count();
                string html =
                "<div class='table-responsive' id='divTableDiemDanhLt'> \r\n" +
                "   <table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                "       <thead> \r\n" +
                "           <tr> \r\n" +
                "               <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                "               <th width=100>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                "               <th width=200>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n";
                for (int i = 0; i < lichLyThuyetChiTietQuantity; i++)
                {
                    string columnTitle = lichLyThuyetChiTiets[i].NGAY.ToString("dd/MM/yyyy");
                    if (lichLyThuyetChiTiets[i].THOIGIAN != null)
                        columnTitle = lichLyThuyetChiTiets[i].THOIGIAN.Value.ToString("HH:mm") + " " + columnTitle;
                    html += 
                "               <th width=120 style='text-align: center;'>" + columnTitle + " </th> \r\n";
                }
                    html +=
                "           </tr> \r\n" +
                "       </thead> \r\n" +
                "       <tbody> \r\n";
                for (int iIndex = 0; iIndex < hocVienQuantity; iIndex++)
                {
                     html += 
                "           <tr> \r\n" +
                "               <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                "               <td>" + hocViens[iIndex].MA + "</td> \r\n" +
                "               <td>" + hocViens[iIndex].HOTEN + "</td> \r\n";
                    for (int i = 0; i < lichLyThuyetChiTietQuantity; i++)
                    {
                        bool coDiemDanh = diemDanhs.Any(o => o.HOCVIEN_ID == hocViens[iIndex].ID && o.LICHLYTHUYETCHITIET_ID == lichLyThuyetChiTiets[i].ID);
                        html += string.Format("<td><center>{0}</center></td> \r\n", coDiemDanh ? "<i class='fa fa-times' style='color: red;'></i>" : null);
                    }
                    html += "</tr> \r\n";
                }
                html += 
                "       </tbody> \r\n" +
                "   </table> \r\n" +
                "</div> \r\n";
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
        public static AjaxOut PopupDiemDanhLt(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichLyThuyetChiTietCls[] lichLyThuyetChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = khoaHocId });
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ORenderInfo, new DT_HocVienFilterCls() { KhoaHocDuyet_Id = khoaHocId });
                DT_DiemDanhLyThuyetCls[] diemDanhs = CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Reading(ORenderInfo, new DT_DiemDanhLyThuyetFilterCls() { KhoaHocId = khoaHocId });
                int lichLyThuyetChiTietQuantity = lichLyThuyetChiTiets.Count();
                int hocVienQuantity = hocViens.Count();
                string html =
                "<div class='table-responsive' id='divTablePopupDiemDanhLt' style='height:100%; overflow-y:auto;'> \r\n" +
                "   <table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                "       <thead> \r\n" +
                "           <tr> \r\n" +
                "               <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                "               <th width=100>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                "               <th width=200>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n";
                for (int i = 0; i < lichLyThuyetChiTietQuantity; i++)
                {
                    string columnTitle = lichLyThuyetChiTiets[i].NGAY.ToString("dd/MM/yyyy");
                    if (lichLyThuyetChiTiets[i].THOIGIAN != null)
                        columnTitle = lichLyThuyetChiTiets[i].THOIGIAN.Value.ToString("HH:mm") + " " + columnTitle;
                    html += 
                "               <th width=120 style='text-align: center;'>" + columnTitle + " </th> \r\n";
                }
                html +=
                 "          </tr> \r\n" +
                 "      </thead> \r\n" +
                 "      <tbody> \r\n";
                for (int iIndex = 0; iIndex < hocVienQuantity; iIndex++)
                {
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].MA + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].HOTEN + "</td> \r\n";
                    for (int i = 0; i < lichLyThuyetChiTietQuantity; i++)
                    {
                        bool coDiemDanh = diemDanhs.Any(o => o.HOCVIEN_ID == hocViens[iIndex].ID && o.LICHLYTHUYETCHITIET_ID == lichLyThuyetChiTiets[i].ID);
                        html += string.Format("<td style='text-align: center;'><center><input type ='checkbox' id='{0}_{1}' {2} onchange='SaveDiemDanhLyThuyet(this)'></center></td> \r\n", hocViens[iIndex].ID, lichLyThuyetChiTiets[i].ID, coDiemDanh ? "checked" : null);
                    }
                    html += "</tr> \r\n";
                }
                html += "</tbody> \r\n" +
                    "</table> \r\n" +
                "</div> \r\n";
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
        public static AjaxOut DrawDiemDanhTh(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichThucHanhCls[] lichThucHanhs = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Reading(ORenderInfo, new DT_LichThucHanhFilterCls() { KhoaHocId = khoaHocId });
                string html =
                        "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                        "   <thead> \r\n" +
                        "       <tr> \r\n" +
                        "       <th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                        "       <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhóm") + " </th> \r\n" +
                        "       <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + " </th> \r\n" +
                        "       <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + " </th> \r\n" +
                        "       <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + " </th> \r\n" +
                        "       <th width=100 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + " </th> \r\n" +
                        "       </tr> \r\n" +
                        "   </thead> \r\n" +
                        "   <tbody> \r\n";
                for (int iIndex = 0; iIndex < lichThucHanhs.Length; iIndex++)
                {
                    int hocVienQuantity = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocVienQuantity(ORenderInfo, lichThucHanhs[iIndex].ID);

                    html += 
                        "       <tr> \r\n" +
                        "           <input type='hidden' id='hdLichThucHanhId" + iIndex + "' value='" + lichThucHanhs[iIndex].ID + "'>\r\n" +
                        "           <td style='text-align: center;'>" + (iIndex + 1) + "</td> \r\n" +
                        "           <td><a href='javascript:PopupDiemDanhTh(\"" + lichThucHanhs[iIndex].ID + "\", \"" + lichThucHanhs[iIndex].NHOM + "\");' title = '" + WebLanguage.GetLanguage(ORenderInfo, "Bảng điểm danh của nhóm") + "'>" + lichThucHanhs[iIndex].NHOM + "</a></td> \r\n" +
                        "           <td style='text-align: center;'>" + (lichThucHanhs[iIndex].BATDAU == null ? null : lichThucHanhs[iIndex].BATDAU.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "           <td style='text-align: center;'>" + (lichThucHanhs[iIndex].KETTHUC == null ? null : lichThucHanhs[iIndex].KETTHUC.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                        "           <td>" + lichThucHanhs[iIndex].DIADIEM + "</td> \r\n" +
                        "           <td style='text-align: center;'>" + hocVienQuantity + "</td> \r\n" +
                        "       </tr> \r\n";
                }
                html += "</tbody> \r\n" +
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
        public static AjaxOut PopupDiemDanhTh(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                bool diemDanhPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new DT_LopHocPermission().PermissionFunctionCode, DT_KhoaHocCls.eLopHocPermission.DiemDanh.ToString(), new DT_LopHocPermission().PermissionFunctionCode, DT_LopHocPermission.StaticPermissionFunctionId, user.OwnerUserId);
                bool isEditor = khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && diemDanhPermission;
                DT_LichThucHanhChiTietCls[] lichThucHanhChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { LICHTHUCHANH_ID = lichThucHanhId });
                DT_HocVienCls[] hocViens = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocViens(ORenderInfo, lichThucHanhId);
                DT_DiemDanhThucHanhCls[] diemDanhs = CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Reading(ORenderInfo, new DT_DiemDanhThucHanhFilterCls() { LichThucHanhId = lichThucHanhId });
                int lichThucHanhChiTietQuantity = lichThucHanhChiTiets.Length;
                int hocVienQuantity = hocViens.Count();
                string html =
                "<div class='table-responsive' id='divTablePopupDiemDanhTh'> \r\n" +
                      "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                          "<thead> \r\n" +
                              "<tr> \r\n" +
                                 "<th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                 "<th width=100>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                                 "<th width=200>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n";
                for (int i = 0; i < lichThucHanhChiTietQuantity; i++)
                {
                    string columnTitle = lichThucHanhChiTiets[i].NGAY.ToString("dd/MM/yyyy");
                    if (lichThucHanhChiTiets[i].THOIGIAN != null)
                        columnTitle = lichThucHanhChiTiets[i].THOIGIAN.Value.ToString("HH:mm") + " " + columnTitle;
                    html += "<th width=120 style='text-align: center;'>" + columnTitle + " </th> \r\n";
                }
                html +=
                             "</tr> \r\n" +
                         "</thead> \r\n" +
                         "<tbody> \r\n";
                for (int iIndex = 0; iIndex < hocVienQuantity; iIndex++)
                {
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].MA + "</td> \r\n" +
                                "<td>" + hocViens[iIndex].HOTEN + "</td> \r\n";
                    if (isEditor)
                        for (int i = 0; i < lichThucHanhChiTietQuantity; i++)
                        {
                            bool coDiemDanh = diemDanhs.Any(o => o.HOCVIEN_ID == hocViens[iIndex].ID && o.LICHTHUCHANHCHITIET_ID == lichThucHanhChiTiets[i].ID);
                            html += string.Format("<td style='text-align: center;'><center><input type ='checkbox' {0} onchange='SaveDiemDanhThucHanh(this, \"" + hocViens[iIndex].ID + "\", \"" + lichThucHanhChiTiets[i].ID + "\")'></center></td> \r\n", coDiemDanh ? "checked" : null);
                        }
                    else
                        for (int i = 0; i < lichThucHanhChiTietQuantity; i++)
                        {
                            bool coDiemDanh = diemDanhs.Any(o => o.HOCVIEN_ID == hocViens[iIndex].ID && o.LICHTHUCHANHCHITIET_ID == lichThucHanhChiTiets[i].ID);
                            html += string.Format("<td><center>{0}</center></td> \r\n", coDiemDanh ? "<i class='fa fa-times' style='color: red;'></i>" : null);
                        }
                    html += "</tr> \r\n";
                }
                html += "</tbody> \r\n" +
                    "</table> \r\n" +
                "</div> \r\n";
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
        public static AjaxOut DrawTaiLieus(RenderInfoCls ORenderInfo, string khoaHocId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_TaiLieuCls[] taiLieus = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Reading(ORenderInfo, new DT_TaiLieuFilterCls() { KHOAHOC_ID = khoaHocId });
                int taiLieuQuantity = taiLieus.Count();
                string html =
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th style='width:500px;'>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                    "                     <th style='width:200px;'>" + WebLanguage.GetLanguage(OSiteParam, "Người tạo") + " </th> \r\n" +
                    "                     <th style='width:130px;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian tạo") + " </th> \r\n" +
                    (!isEditor ?
                    ("                     <th style='width:50px; text-align:center;'>" + WebLanguage.GetLanguage(OSiteParam, "Xem") + " </th> \r\n" +
                    "                     <th style='width:50px; text-align:center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tải") + " </th> \r\n") :
                    "                     <th style='width:70px;'>" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "</th> \r\n") +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < taiLieuQuantity; iIndex++)
                {
                    bool xoaPermission = isEditor && (user.OwnerUserId == taiLieus[iIndex].NGUOITAO_ID || user.IsSystemAdmin == 1);
                    string pathToFile = WebConfig.GetWebHttpRoot() + "/" + WebConfig.GetWebConfig("UploadedDaoTaoFilePath");
                    string taiLieuUrl = Path.Combine(pathToFile, taiLieus[iIndex].TENTEP);

                    OwnerUserCls nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, taiLieus[iIndex].NGUOITAO_ID);
                    string[] fileNameArray = taiLieus[iIndex].TENTEP.Split('.');
                    string fileType = fileNameArray[fileNameArray.Length - 1];
                    string aTag = "";
                    if (new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx" }.Contains(fileType))//Nếu là các file thuộc office thì mở nhờ vào http://docs.google.com/gview
                    {
                        if (!string.IsNullOrEmpty(WebConfig.GetWebConfig("GoogleViewerUrl")))
                        {
                            taiLieuUrl = WebConfig.GetWebConfig("GoogleViewerUrl") + taiLieuUrl;
                            aTag = "<a href = '" + taiLieuUrl + "' target='_blank'><i class='fa fa-eye'></i></a>";
                        }
                    }
                    else if (new string[] { "pdf", "bmp", "jpg", "jpeg", "jpe", "jfif", "gif", "tif", "tiff", "png", "heic", "mp3", "mp4", "mov", "mpeg4", "avi", "wmv", "mpegps", "flv", "3gpp" }.Contains(fileType))
                        aTag = "<a href = '" + taiLieuUrl + "' target='_blank'><i class='fa fa-eye'></i></a>";

                    html +=
                    "                 <tr> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + taiLieus[iIndex].TENHIENTHI + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + taiLieus[iIndex].GHICHU + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + (nguoiTao == null ? null : nguoiTao.FullName) + "</td> \r\n" +
                    "                     <td style='text-align:center; vertical-align: middle;'>" + taiLieus[iIndex].NGAYTAO.ToString("HH:mm dd/MM/yyyy") + "</td> \r\n" +
                    (!isEditor ?
                    ("                     <td style='vertical-align: middle;text-align: center;'>" + (File.Exists(taiLieus[iIndex].DUONGDAN) ? aTag : null) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;text-align: center;'>" + (File.Exists(taiLieus[iIndex].DUONGDAN) ? "<a href='" + pathToFile + "' download='" + taiLieus[iIndex].TENTEP + "'><i class='fa fa-download'></i></a>" : null) + "</td> \r\n") :
                    "                     <td style='text-align:center; vertical-align: middle;'>\r\n" +
                        (xoaPermission ? "  <a href='javascript:DeleteTaiLieu(\"" + taiLieus[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" : null) +
                    "                    </td> \r\n") +
                    "                 </tr> \r\n";
                }
                html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n" +
                    "<style>\r\n" +
                    "table th{text-align: center; vertical-align: middle;}\r\n" +
                    "</style>\r\n"
                    ;
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
        public static AjaxOut PopupTaiLieu(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                #region Danh sách tài liệu
                "   <div class='col-md-12 col-xs-12' id = 'divAddTaiLieus'>\r\n" +
                "       <div class='col-md-1 col-xs-2'>\r\n" +
                "           <input id='fileUploadTaiLieu' type='file' multiple onchange='ShowSelectedFileNames(this);' style='display:none;'/>\r\n" +
                "           <button class='btn btn-primary' type='button' style='margin-top: -3px;' onclick='$(\"#fileUploadTaiLieu\").click(); $(\"#divTenTaiLieuChon\").html(\"\");'>" + WebLanguage.GetLanguage(OSiteParam, "Chọn file") + "</button>\r\n" +
                "       </div>\r\n" +
                "       <div class='col-md-10 col-xs-12'>\r\n" +
                "           <span class=\"valueForm\"><input type='text' class='form-control' id='txtMoTaTaiLieu' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Nhập ghi chú tài liệu tại đây") + "'></span>\r\n" +
                "       </div>\r\n" +
                "       <div class='col-md-1 col-xs-2'>\r\n" +
                "           <button id='btnUploadTaiLieu' style='margin-top: -3px;' onclick=\"javascript:UploadTaiLieus();\" type=\"button\" class=\"btn btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Tải") + "</button>\r\n" +
                "       </div>\r\n" +
                "   </div>\r\n" +
                "   <div class='col-md-12 col-xs-12' id='divTenTaiLieuChon' style='min-height: 30px;'>\r\n" +
                "   </div>\r\n" +
                "   <div id='divProgress' class='col-md-12 col-xs-12 progress progress-striped active' style='background-color: transparent; display:none;'>\r\n" +
                "       <div id='divProgressBar' style = 'width: 0%; background-color: green;' aria-valuemax='100' aria-valuemin='0' aria-valuenow='0' role='progressbar' class='progress-bar progress-bar-danger'>\r\n" +
                "           <span id='spCompletedPercent'>0</span>% đã được tải.\r\n" +
                "       </div>\r\n" +
                "   </div>\r\n" +
                "   <div class='col-md-12 col-xs-12' id = 'divTaiLieuList' style='margin-top:20px;'>\r\n" +
                       DrawTaiLieus(ORenderInfo, khoaHocId, true).HtmlContent +
                "   </div>\r\n" +
                #endregion
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
        #endregion Vẽ giao diện

        #region Xử lý nghiệp vụ
        private static string GetDayOfWeek(SiteParam oSiteParam, DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ hai");
            else if (dayOfWeek == DayOfWeek.Tuesday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ ba");
            else if (dayOfWeek == DayOfWeek.Wednesday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ tư");
            else if (dayOfWeek == DayOfWeek.Thursday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ năm");
            else if (dayOfWeek == DayOfWeek.Friday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ sáu");
            else if (dayOfWeek == DayOfWeek.Saturday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ bảy");
            else if (dayOfWeek == DayOfWeek.Sunday)
                return WebLanguage.GetLanguage(oSiteParam, "Chủ nhật");
            return null;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetXepLoai(RenderInfoCls ORenderInfo, decimal? diemThiLyThuyet, decimal? diemThiThucHanh)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                RetAjaxOut.RetExtraParam1 = DT_KhoaHocCls.XepLoai(diemThiLyThuyet, diemThiThucHanh);
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
        public static AjaxOut GetThongTinHocVien(RenderInfoCls ORenderInfo, string hocVienId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, hocVienId);

                RetAjaxOut.RetExtraParam6 = JsonConvert.SerializeObject(new
                {
                    Ma = hocVien.MA,
                    GioiTinh = hocVien.GIOITINH == null ? null : Common.BenhNhan.GioiTinhs[hocVien.GIOITINH.Value],
                    NgaySinh = hocVien.NGAYSINH == null ? null : hocVien.NGAYSINH.Value.ToString("dd/MM/yyyy")
                });
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
        public static AjaxOut UpdateTrangThaiKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId, int trangThai)
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
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "lớp học này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                khoaHoc.TRANGTHAI = trangThai;
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
        public static AjaxOut SaveKeHoachLop(RenderInfoCls ORenderInfo, string khoaHocId, string batDau, string ketThuc, string thoiGianTiepDon, string diaDiemTiepDon, string batDauLt, string ketThucLt, string diaDiemLt, string batDauTh, string ketThucTh, string diaDiemTh, int? soLuongNhomTh, int? soHvTrongNhomTh, string thoiGianDanhGiaTdt,
            string diaDiemDanhGiaTdt, string thoiGianGiaiDapThacMac, string diaDiemGiaiDapThacMac, string batDauThiLt, string ketThucThiLt, string diaDiemThiLt, string batDauThiVd, string ketThucThiVd, string diaDiemThiVd, string batDauThiTh, string ketThucThiTh, string diaDiemThiTh, string thoiGianBeGiang, string diaDiemBeGiang, string lanhDao, string nguoiLap)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (!string.IsNullOrEmpty(batDauThiLt) && !string.IsNullOrEmpty(ketThucThiLt))
                    batDauThiLt = batDauThiLt + " " + ketThucThiLt.Split(' ')[1];
                if (!string.IsNullOrEmpty(batDauThiVd) && !string.IsNullOrEmpty(ketThucThiVd))
                    batDauThiVd = batDauThiVd + " " + ketThucThiVd.Split(' ')[1];
                if (!string.IsNullOrEmpty(batDauThiTh) && !string.IsNullOrEmpty(ketThucThiTh))
                    batDauThiTh = batDauThiTh + " " + ketThucThiTh.Split(' ')[1];
                DT_KeHoachLopCls keHoachLop = CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().CreateModel(ORenderInfo, khoaHocId);
                if (keHoachLop == null)
                {
                    keHoachLop = new DT_KeHoachLopCls();
                    keHoachLop.ID = khoaHocId;
                    keHoachLop.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    keHoachLop.THOIGIANTIEPDON = string.IsNullOrEmpty(thoiGianTiepDon) ? null : (DateTime?)DateTime.ParseExact(thoiGianTiepDon, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTIEPDON = diaDiemTiepDon;
                    keHoachLop.BATDAULT = string.IsNullOrEmpty(batDauLt) ? null : (DateTime?)DateTime.ParseExact(batDauLt, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUCLT = string.IsNullOrEmpty(ketThucLt) ? null : (DateTime?)DateTime.ParseExact(ketThucLt, "dd/MM/yyyy", null);
                    keHoachLop.DIADIEMLT = diaDiemLt;
                    keHoachLop.BATDAUTH = string.IsNullOrEmpty(batDauTh) ? null : (DateTime?)DateTime.ParseExact(batDauTh, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTH = string.IsNullOrEmpty(ketThucTh) ? null : (DateTime?)DateTime.ParseExact(ketThucTh, "dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTH = diaDiemTh;
                    keHoachLop.SOLUONGNHOMTH = soLuongNhomTh;
                    keHoachLop.SOHVTRONGNHOMTH = soHvTrongNhomTh;
                    keHoachLop.THOIGIANDANHGIATDT = string.IsNullOrEmpty(thoiGianDanhGiaTdt) ? null : (DateTime?)DateTime.ParseExact(thoiGianDanhGiaTdt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMDANHGIATDT = diaDiemDanhGiaTdt;
                    keHoachLop.THOIGIANGIAIDAPTHACMAC = string.IsNullOrEmpty(thoiGianGiaiDapThacMac) ? null : (DateTime?)DateTime.ParseExact(thoiGianGiaiDapThacMac, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMGIAIDAPTHACMAC = diaDiemGiaiDapThacMac;
                    keHoachLop.BATDAUTHILT = string.IsNullOrEmpty(batDauThiLt) ? null : (DateTime?)DateTime.ParseExact(batDauThiLt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHILT = string.IsNullOrEmpty(ketThucThiLt) ? null : (DateTime?)DateTime.ParseExact(ketThucThiLt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHILT = diaDiemThiLt;
                    keHoachLop.BATDAUTHIVD = string.IsNullOrEmpty(batDauThiVd) ? null : (DateTime?)DateTime.ParseExact(batDauThiVd, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHIVD = string.IsNullOrEmpty(ketThucThiVd) ? null : (DateTime?)DateTime.ParseExact(ketThucThiVd, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHIVD = diaDiemThiVd;
                    keHoachLop.BATDAUTHITH = string.IsNullOrEmpty(batDauThiTh) ? null : (DateTime?)DateTime.ParseExact(batDauThiTh, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHITH = string.IsNullOrEmpty(ketThucThiTh) ? null : (DateTime?)DateTime.ParseExact(ketThucThiTh, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHITH = diaDiemThiTh;
                    keHoachLop.THOIGIANBEGIANG = string.IsNullOrEmpty(thoiGianBeGiang) ? null : (DateTime?)DateTime.ParseExact(thoiGianBeGiang, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMBEGIANG = diaDiemBeGiang;
                    keHoachLop.LANHDAO_ID = lanhDao;
                    keHoachLop.NGUOILAP = nguoiLap;
                    keHoachLop.NGAYTAO = DateTime.Now;
                    keHoachLop.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Add(ORenderInfo, keHoachLop);
                }
                else
                {
                    keHoachLop.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    keHoachLop.THOIGIANTIEPDON = string.IsNullOrEmpty(thoiGianTiepDon) ? null : (DateTime?)DateTime.ParseExact(thoiGianTiepDon, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTIEPDON = diaDiemTiepDon;
                    keHoachLop.BATDAULT = string.IsNullOrEmpty(batDauLt) ? null : (DateTime?)DateTime.ParseExact(batDauLt, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUCLT = string.IsNullOrEmpty(ketThucLt) ? null : (DateTime?)DateTime.ParseExact(ketThucLt, "dd/MM/yyyy", null);
                    keHoachLop.DIADIEMLT = diaDiemLt;
                    keHoachLop.BATDAUTH = string.IsNullOrEmpty(batDauTh) ? null : (DateTime?)DateTime.ParseExact(batDauTh, "dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTH = string.IsNullOrEmpty(ketThucTh) ? null : (DateTime?)DateTime.ParseExact(ketThucTh, "dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTH = diaDiemTh;
                    keHoachLop.SOLUONGNHOMTH = soLuongNhomTh;
                    keHoachLop.SOHVTRONGNHOMTH = soHvTrongNhomTh;
                    keHoachLop.THOIGIANDANHGIATDT = string.IsNullOrEmpty(thoiGianDanhGiaTdt) ? null : (DateTime?)DateTime.ParseExact(thoiGianDanhGiaTdt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMDANHGIATDT = diaDiemDanhGiaTdt;
                    keHoachLop.THOIGIANGIAIDAPTHACMAC = string.IsNullOrEmpty(thoiGianGiaiDapThacMac) ? null : (DateTime?)DateTime.ParseExact(thoiGianGiaiDapThacMac, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMGIAIDAPTHACMAC = diaDiemGiaiDapThacMac;
                    keHoachLop.BATDAUTHILT = string.IsNullOrEmpty(batDauThiLt) ? null : (DateTime?)DateTime.ParseExact(batDauThiLt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHILT = string.IsNullOrEmpty(ketThucThiLt) ? null : (DateTime?)DateTime.ParseExact(ketThucThiLt, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHILT = diaDiemThiLt;
                    keHoachLop.BATDAUTHIVD = string.IsNullOrEmpty(batDauThiVd) ? null : (DateTime?)DateTime.ParseExact(batDauThiVd, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHIVD = string.IsNullOrEmpty(ketThucThiVd) ? null : (DateTime?)DateTime.ParseExact(ketThucThiVd, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHIVD = diaDiemThiVd;
                    keHoachLop.BATDAUTHITH = string.IsNullOrEmpty(batDauThiTh) ? null : (DateTime?)DateTime.ParseExact(batDauThiTh, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.KETTHUCTHITH = string.IsNullOrEmpty(ketThucThiTh) ? null : (DateTime?)DateTime.ParseExact(ketThucThiTh, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMTHITH = diaDiemThiTh;
                    keHoachLop.THOIGIANBEGIANG = string.IsNullOrEmpty(thoiGianBeGiang) ? null : (DateTime?)DateTime.ParseExact(thoiGianBeGiang, "HH:mm dd/MM/yyyy", null);
                    keHoachLop.DIADIEMBEGIANG = diaDiemBeGiang;
                    keHoachLop.LANHDAO_ID = lanhDao;
                    keHoachLop.NGUOILAP = nguoiLap;
                    keHoachLop.NGAYSUA = DateTime.Now;
                    keHoachLop.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Save(ORenderInfo, keHoachLop.ID, keHoachLop);
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
        public static AjaxOut SaveLichLyThuyetChiTiet(RenderInfoCls ORenderInfo, string lichLyThuyetId, string lichLyThuyetChiTietId, string ngay, string thoiGian, string thoiGianKetThuc, string noiDung, string bacSy, int hinhThucHoc, string ghiChu)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_LichLyThuyetChiTietCls> lichLyThuyetChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichLyThuyetChiTiets" + lichLyThuyetId) as List<DT_LichLyThuyetChiTietCls>;
                DateTime dtNgay = DateTime.ParseExact(ngay, "dd/MM/yyyy", null);
                DateTime? dtThoiGian = string.IsNullOrEmpty(thoiGian) ? null : (DateTime?)DateTime.ParseExact(ngay + " " + thoiGian, "dd/MM/yyyy HH:mm", null);
                DateTime? dtThoiGianKetThuc = string.IsNullOrEmpty(thoiGianKetThuc) ? null : (DateTime?)DateTime.ParseExact(ngay + " " + thoiGianKetThuc, "dd/MM/yyyy HH:mm", null);
                if (lichLyThuyetChiTiets.Any(o => o.NGAY == dtNgay && (o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)) && o.ID != lichLyThuyetChiTietId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian bị trùng.\nXin chọn thời gian khác.");
                    return RetAjaxOut;
                }
                if (CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { LICHTHUCHANH_ID = lichLyThuyetId, NGAY = dtNgay })
                    .Any(o => o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Lịch này trùng thời gian với lịch thực hành.\nXin chọn thời gian khác.");
                    return RetAjaxOut;
                }
                if (!string.IsNullOrEmpty(bacSy) && (CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { NGAY = dtNgay, GIANGVIEN_ID = bacSy, ISKHOAHOCKETTHUC = false })
                    .Any(o => o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN))
                        || CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { NGAY = dtNgay, GIANGVIEN_ID = bacSy, ISKHOAHOCKETTHUC = false })
                            .Any(o => o.ID != lichLyThuyetChiTietId && (o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)))))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Giảng viên này đã có lịch khác trùng thời gian với lịch này.\nXin chọn giảng viên khác.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(lichLyThuyetChiTietId))//thêm mới
                {
                    lichLyThuyetChiTiets.Add(new DT_LichLyThuyetChiTietCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LICHLYTHUYET_ID = lichLyThuyetId,
                        NGAY = dtNgay,
                        THOIGIAN = dtThoiGian,
                        THOIGIANKETTHUC = dtThoiGianKetThuc,
                        NOIDUNG = noiDung,
                        GIANGVIEN_ID = bacSy,
                        HINHTHUCHOC = hinhThucHoc,
                        GHICHU = ghiChu
                    });
                }
                else//cập nhật
                {
                    DT_LichLyThuyetChiTietCls lichLyThuyetChiTiet = lichLyThuyetChiTiets.FirstOrDefault(o => o.ID == lichLyThuyetChiTietId);
                    if (lichLyThuyetChiTiet == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ngày này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    lichLyThuyetChiTiet.NGAY = dtNgay;
                    lichLyThuyetChiTiet.THOIGIAN = dtThoiGian;
                    lichLyThuyetChiTiet.THOIGIANKETTHUC = dtThoiGianKetThuc;
                    lichLyThuyetChiTiet.NOIDUNG = noiDung;
                    lichLyThuyetChiTiet.GIANGVIEN_ID = bacSy;
                    lichLyThuyetChiTiet.HINHTHUCHOC = hinhThucHoc;
                    lichLyThuyetChiTiet.GHICHU = ghiChu;
                }
                RetAjaxOut.HtmlContent = DrawLichLyThuyetChiTiets(ORenderInfo, lichLyThuyetId, true).HtmlContent;
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
        public static AjaxOut DeleteLichLyThuyetChiTiet(RenderInfoCls ORenderInfo, string lichLyThuyetId, string lichLyThuyetChiTietId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_LichLyThuyetChiTietCls> lichLyThuyetChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichLyThuyetChiTiets" + lichLyThuyetId) as List<DT_LichLyThuyetChiTietCls>;
                DT_LichLyThuyetChiTietCls lichLyThuyetChiTiet = lichLyThuyetChiTiets.FirstOrDefault(o => o.ID == lichLyThuyetChiTietId);
                lichLyThuyetChiTiets.Remove(lichLyThuyetChiTiet);
                RetAjaxOut.HtmlContent = DrawLichLyThuyetChiTiets(ORenderInfo, lichLyThuyetChiTiet.LICHLYTHUYET_ID, true).HtmlContent;
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
        public static AjaxOut SaveLichLyThuyet(RenderInfoCls ORenderInfo, string khoaHocId, string diaDiem, string batDau, string ketThuc, string ptChuyenMon, string lanhDao)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichLyThuyetCls lichLyThuyet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().CreateModel(ORenderInfo, khoaHocId);
                if (lichLyThuyet == null)
                {
                    lichLyThuyet = new DT_LichLyThuyetCls();
                    lichLyThuyet.ID = khoaHocId;
                    lichLyThuyet.DIADIEM = diaDiem;
                    lichLyThuyet.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichLyThuyet.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichLyThuyet.PTCHUYENMON_ID = ptChuyenMon;
                    lichLyThuyet.LANHDAO_ID = lanhDao;
                    lichLyThuyet.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichLyThuyet.NGAYTAO = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Add(ORenderInfo, lichLyThuyet);
                }
                else
                {
                    lichLyThuyet.DIADIEM = diaDiem;
                    lichLyThuyet.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichLyThuyet.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichLyThuyet.PTCHUYENMON_ID = ptChuyenMon;
                    lichLyThuyet.LANHDAO_ID = lanhDao;
                    lichLyThuyet.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichLyThuyet.NGAYSUA = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Save(ORenderInfo, lichLyThuyet.ID, lichLyThuyet);
                }
                #region Cập nhật lịch lý thuyết chi tiết
                List<DT_LichLyThuyetChiTietCls> newLichLyThuyetChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichLyThuyetChiTiets" + khoaHocId) as List<DT_LichLyThuyetChiTietCls>;
                List<DT_LichLyThuyetChiTietCls> oldLichLyThuyetChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = lichLyThuyet.ID }).ToList();
                foreach (var oldLichLyThuyetChiTiet in oldLichLyThuyetChiTiets)
                {
                    bool isExists = false;
                    foreach (var newLichLyThuyetChiTiet in newLichLyThuyetChiTiets)
                    {
                        if (newLichLyThuyetChiTiet.ID == oldLichLyThuyetChiTiet.ID)//cập nhật
                        {
                            CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Save(ORenderInfo, oldLichLyThuyetChiTiet.ID, newLichLyThuyetChiTiet);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Delete(ORenderInfo, oldLichLyThuyetChiTiet.ID);
                    }
                }
                var addLichLyThuyetChiTiets = newLichLyThuyetChiTiets.Where(o => !oldLichLyThuyetChiTiets.Any(old => old.ID == o.ID));
                foreach (var addLichLyThuyetChiTiet in addLichLyThuyetChiTiets)//Thêm mới
                {
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Add(ORenderInfo, addLichLyThuyetChiTiet);
                }
                #endregion
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
        public static AjaxOut SaveLichThucHanhChiTiet(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, string lichThucHanhChiTietId, string ngay, string thoiGian, string thoiGianKetThuc, string noiDung, string bacSy, string ghiChu)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_LichThucHanhChiTietCls> lichThucHanhChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_LichThucHanhChiTietCls>;
                DateTime dtNgay = DateTime.ParseExact(ngay, "dd/MM/yyyy", null);
                DateTime? dtThoiGian = string.IsNullOrEmpty(thoiGian) ? null : (DateTime?)DateTime.ParseExact(ngay + " " + thoiGian, "dd/MM/yyyy HH:mm", null);
                DateTime? dtThoiGianKetThuc = string.IsNullOrEmpty(thoiGianKetThuc) ? null : (DateTime?)DateTime.ParseExact(ngay + " " + thoiGianKetThuc, "dd/MM/yyyy HH:mm", null);
                if (lichThucHanhChiTiets.Any(o => o.NGAY == dtNgay && (o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)) && o.ID != lichThucHanhChiTietId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian bị trùng.\nXin chọn thời gian khác.");
                    return RetAjaxOut;
                }
                if (CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = lichThucHanhId, NGAY = dtNgay })
                    .Any(o => o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Lịch này trùng thời gian với lịch lý thuyết.\nXin chọn thời gian khác.");
                    return RetAjaxOut;
                }
                if (!string.IsNullOrEmpty(bacSy) && (CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { NGAY = dtNgay, GIANGVIEN_ID = bacSy, ISKHOAHOCKETTHUC = false })
                    .Any(o => o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN))
                         || CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { NGAY = dtNgay, GIANGVIEN_ID = bacSy, ISKHOAHOCKETTHUC = false })
                         .Any(o => o.ID != lichThucHanhChiTietId && (o.THOIGIAN == null || dtThoiGian == null || (dtThoiGian <= o.THOIGIANKETTHUC && dtThoiGianKetThuc >= o.THOIGIAN)))))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Giảng viên này đã có lịch khác trùng thời gian với lịch này.\nXin chọn giảng viên khác.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(lichThucHanhChiTietId))//thêm mới
                {
                    lichThucHanhChiTiets.Add(new DT_LichThucHanhChiTietCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LICHTHUCHANH_ID = lichThucHanhId,
                        NGAY = dtNgay,
                        THOIGIAN = dtThoiGian,
                        THOIGIANKETTHUC = dtThoiGianKetThuc,
                        NOIDUNG = noiDung,
                        GIANGVIEN_ID = bacSy,
                        GHICHU = ghiChu
                    });
                }
                else//cập nhật
                {
                    DT_LichThucHanhChiTietCls lichThucHanhChiTiet = lichThucHanhChiTiets.FirstOrDefault(o => o.ID == lichThucHanhChiTietId);
                    if (lichThucHanhChiTiet == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ngày này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    lichThucHanhChiTiet.NGAY = dtNgay;
                    lichThucHanhChiTiet.THOIGIAN = dtThoiGian;
                    lichThucHanhChiTiet.THOIGIANKETTHUC = dtThoiGianKetThuc;
                    lichThucHanhChiTiet.NOIDUNG = noiDung;
                    lichThucHanhChiTiet.GIANGVIEN_ID = bacSy;
                    lichThucHanhChiTiet.GHICHU = ghiChu;
                }
                RetAjaxOut.HtmlContent = DrawLichThucHanhChiTiets(ORenderInfo, khoaHocId, lichThucHanhId, true).HtmlContent;
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
        public static AjaxOut DeleteHocVienTrongNhom(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, string hocVienId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_HocVienCls> HocVienTrongNhoms = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_HocVienCls>;
                DT_HocVienCls HocVienTrongNhom = HocVienTrongNhoms.FirstOrDefault(o => o.ID == hocVienId);
                HocVienTrongNhoms.Remove(HocVienTrongNhom);
                RetAjaxOut.HtmlContent = DrawHocVienTrongNhoms(ORenderInfo, khoaHocId, lichThucHanhId, true).HtmlContent;
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
        public static AjaxOut SaveHocVienTrongNhom(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, string hocVienId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_HocVienCls> HocVienTrongNhoms = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_HocVienCls>;
                if (HocVienTrongNhoms.Any(o => o.ID == hocVienId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Học viên này đã có trong nhóm.\nXin chọn học viên khác.");
                    return RetAjaxOut;
                }
                DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, hocVienId);
                HocVienTrongNhoms.Add(hocVien);
                RetAjaxOut.HtmlContent = DrawHocVienTrongNhoms(ORenderInfo, khoaHocId, lichThucHanhId, true).HtmlContent;
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
        public static AjaxOut DeleteLichThucHanhChiTiet(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, string lichThucHanhChiTietId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_LichThucHanhChiTietCls> lichThucHanhChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_LichThucHanhChiTietCls>;
                DT_LichThucHanhChiTietCls lichThucHanhChiTiet = lichThucHanhChiTiets.FirstOrDefault(o => o.ID == lichThucHanhChiTietId);
                lichThucHanhChiTiets.Remove(lichThucHanhChiTiet);
                RetAjaxOut.HtmlContent = DrawLichThucHanhChiTiets(ORenderInfo, khoaHocId, lichThucHanhChiTiet.LICHTHUCHANH_ID, true).HtmlContent;
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
        public static AjaxOut SaveLichThucHanh(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId, string diaDiem, string batDau, string ketThuc, string nhom, string ptChuyenMon, string lanhDao)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichThucHanhCls lichThucHanh = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().CreateModel(ORenderInfo, lichThucHanhId);
                if (lichThucHanh == null)
                {
                    lichThucHanh = new DT_LichThucHanhCls();
                    lichThucHanh.ID = Guid.NewGuid().ToString();
                    lichThucHanh.KHOAHOC_ID = khoaHocId;
                    lichThucHanh.DIADIEM = diaDiem;
                    lichThucHanh.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichThucHanh.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichThucHanh.NHOM = nhom;
                    lichThucHanh.PTCHUYENMON_ID = ptChuyenMon;
                    lichThucHanh.LANHDAO_ID = lanhDao;
                    lichThucHanh.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichThucHanh.NGAYTAO = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Add(ORenderInfo, lichThucHanh);
                    RetAjaxOut.RetExtraParam1 = lichThucHanh.ID;
                }
                else
                {
                    lichThucHanh.DIADIEM = diaDiem;
                    lichThucHanh.BATDAU = string.IsNullOrEmpty(batDau) ? null : (DateTime?)DateTime.ParseExact(batDau, "dd/MM/yyyy", null);
                    lichThucHanh.KETTHUC = string.IsNullOrEmpty(ketThuc) ? null : (DateTime?)DateTime.ParseExact(ketThuc, "dd/MM/yyyy", null);
                    lichThucHanh.NHOM = nhom;
                    lichThucHanh.PTCHUYENMON_ID = ptChuyenMon;
                    lichThucHanh.LANHDAO_ID = lanhDao;
                    lichThucHanh.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichThucHanh.NGAYSUA = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Save(ORenderInfo, lichThucHanh.ID, lichThucHanh);
                }
                #region Cập nhật lịch thực hành chi tiết
                List<DT_LichThucHanhChiTietCls> newLichThucHanhChiTiets = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_LichThucHanhChiTietCls>;
                List<DT_LichThucHanhChiTietCls> oldLichThucHanhChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ORenderInfo, new DT_LichThucHanhChiTietFilterCls() { LICHTHUCHANH_ID = lichThucHanh.ID }).ToList();
                foreach (var oldLichThucHanhChiTiet in oldLichThucHanhChiTiets)
                {
                    bool isExists = false;
                    foreach (var newLichThucHanhChiTiet in newLichThucHanhChiTiets)
                    {
                        if (newLichThucHanhChiTiet.ID == oldLichThucHanhChiTiet.ID)//cập nhật
                        {
                            CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Save(ORenderInfo, oldLichThucHanhChiTiet.ID, newLichThucHanhChiTiet);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Delete(ORenderInfo, oldLichThucHanhChiTiet.ID);
                    }
                }
                var addLichThucHanhChiTiets = newLichThucHanhChiTiets.Where(o => !oldLichThucHanhChiTiets.Any(old => old.ID == o.ID));
                foreach (var addLichThucHanhChiTiet in addLichThucHanhChiTiets)//Thêm mới
                {
                    addLichThucHanhChiTiet.LICHTHUCHANH_ID = lichThucHanh.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Add(ORenderInfo, addLichThucHanhChiTiet);
                }
                if (string.IsNullOrEmpty(lichThucHanhId))
                {
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + khoaHocId, new List<DT_LichThucHanhChiTietCls>());
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_LichThucHanhChiTiets" + lichThucHanh.ID, newLichThucHanhChiTiets);
                }
                #endregion

                #region Cập nhật học viên trong nhóm
                List<DT_HocVienCls> newHocViens = WebSessionUtility.GetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + (string.IsNullOrEmpty(lichThucHanhId) ? khoaHocId : lichThucHanhId)) as List<DT_HocVienCls>;
                List<DT_HocVienCls> oldHocViens = string.IsNullOrEmpty(lichThucHanhId) ? new List<DT_HocVienCls>() : CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocViens(ORenderInfo, lichThucHanhId).ToList();
                List<string> hocVienIdRemove = new List<string>();

                string[] removeHocVienIds = oldHocViens.Where(o => !newHocViens.Any(n => n.ID == o.ID)).Select(o => o.ID).ToArray();
                DT_LichThucHanhHocVienCls[] addHocViens = newHocViens.Where(o => !oldHocViens.Any(old => old.ID == o.ID))
                    .Select(o => new DT_LichThucHanhHocVienCls() { LICHTHUCHANH_ID = lichThucHanh.ID, HOCVIEN_ID = o.ID }).ToArray();
                if (removeHocVienIds.Length > 0)//remove
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().RemoveHocViens(ORenderInfo, lichThucHanh.ID, removeHocVienIds);
                if (addHocViens.Length > 0)//add
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().AddHocViens(ORenderInfo, addHocViens);

                if (string.IsNullOrEmpty(lichThucHanhId))
                {
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + khoaHocId, new List<DT_HocVienCls>());
                    WebSessionUtility.SetSession(OSiteParam, "DT_LopHoc_HocVienTrongNhoms" + lichThucHanh.ID, newHocViens);
                }
                #endregion

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
        public static AjaxOut DeleteLichThucHanh(RenderInfoCls ORenderInfo, string khoaHocId, string lichThucHanhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Delete(ORenderInfo, lichThucHanhId);
                RetAjaxOut.HtmlContent = DrawLichThucHanhs(ORenderInfo, khoaHocId).HtmlContent;
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
        public static AjaxOut SaveDiemDanhLyThuyet(RenderInfoCls ORenderInfo, string hocVienId, string lichLyThuyetChiTietId, int coDiemDanh)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_DiemDanhLyThuyetCls diemDanhLyThuyet = CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Reading(ORenderInfo, new DT_DiemDanhLyThuyetFilterCls() { HocVienId = hocVienId, LichLyThuyetChiTietId = lichLyThuyetChiTietId }).FirstOrDefault();
                if (diemDanhLyThuyet == null)
                {
                    if (coDiemDanh == 1)//học viên có mặt trong buổi học
                    {
                        diemDanhLyThuyet = new DT_DiemDanhLyThuyetCls();
                        diemDanhLyThuyet.HOCVIEN_ID = hocVienId;
                        diemDanhLyThuyet.LICHLYTHUYETCHITIET_ID = lichLyThuyetChiTietId;
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Add(ORenderInfo, diemDanhLyThuyet);
                    }
                }
                else
                {
                    if (coDiemDanh == 0)//học viên vắng mặt trong buổi học
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Delete(ORenderInfo, diemDanhLyThuyet.ID);
                    }
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
        public static AjaxOut SaveDiemDanhThucHanh(RenderInfoCls ORenderInfo, string hocVienId, string lichThucHanhChiTietId, int coDiemDanh)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_DiemDanhThucHanhCls diemDanhThucHanh = CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Reading(ORenderInfo, new DT_DiemDanhThucHanhFilterCls() { HocVienId = hocVienId, LichThucHanhChiTietId = lichThucHanhChiTietId }).FirstOrDefault();
                if (diemDanhThucHanh == null)
                {
                    if (coDiemDanh == 1)//học viên có mặt trong buổi học
                    {
                        diemDanhThucHanh = new DT_DiemDanhThucHanhCls();
                        diemDanhThucHanh.HOCVIEN_ID = hocVienId;
                        diemDanhThucHanh.LICHTHUCHANHCHITIET_ID = lichThucHanhChiTietId;
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Add(ORenderInfo, diemDanhThucHanh);
                    }
                }
                else
                {
                    if (coDiemDanh == 0)//học viên vắng mặt trong buổi học
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Delete(ORenderInfo, diemDanhThucHanh.ID);
                    }
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
        public static AjaxOut SaveKetQuaDaoTao(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId, decimal? diemThiLyThuyet, decimal? diemThiThucHanh, string xepLoai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                ketQuaDaoTao.DIEMTHILYTHUYET = diemThiLyThuyet;
                ketQuaDaoTao.DIEMTHITHUCHANH = diemThiThucHanh;
                ketQuaDaoTao.XEPLOAI = xepLoai;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Save(ORenderInfo, ketQuaDaoTao.ID, ketQuaDaoTao);
                RetAjaxOut.HtmlContent = DrawHocViens(ORenderInfo, ketQuaDaoTao.KHOAHOCDUYET_ID, true).HtmlContent;
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
        public static AjaxOut UploadTaiLieu(RenderInfoCls ORenderInfo, string khoaHocId, string ghiChu, string xmlUploadFileResult)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
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
                    //string fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedDaoTaoFilePath"]), file.Name);
                    //file.MoveTo(fileSavePath);
                    DT_TaiLieuCls taiLieu = new DT_TaiLieuCls
                    {
                        ID = uploadHandlerResult.Id,
                        KHOAHOC_ID = khoaHocId,
                        TENTEP = file.Name,
                        TENHIENTHI = uploadHandlerResult.UploadFileName,
                        GHICHU = ghiChu,
                        NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId,
                        NGAYTAO = DateTime.Now
                    };
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Add(ORenderInfo, taiLieu);
                }
                if (multiUploadHandlerResult.Error)
                {
                    if (uploadHandlerResults.Count() > 0)
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Upload file ") + string.Join(", ", uploadHandlerResults.Select(o => o.UploadFileName)) + WebLanguage.GetLanguage(OSiteParam, " thành công.\nCác file còn lại thất bại do lỗi:\n") + multiUploadHandlerResult.InfoMessage;
                    else
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = multiUploadHandlerResult.InfoMessage;
                    }
                }
                else
                {
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Upload file thành công.");
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
        public static AjaxOut DeleteTaiLieu(RenderInfoCls ORenderInfo, string taiLieuId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_TaiLieuCls taiLieu = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().CreateModel(ORenderInfo, taiLieuId);
                if (File.Exists(taiLieu.DUONGDAN))
                    File.Delete(taiLieu.DUONGDAN);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Delete(ORenderInfo, taiLieu.ID);
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
