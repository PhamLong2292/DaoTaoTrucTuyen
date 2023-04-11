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
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_ChuyenGiao : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DT_ChuyenGiao";
        public override string WebPartTitle { get { return "Chuyển giao"; } }
        public override string Description { get { return "Chuyển giao"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                DT_LichChuyenGiaos LichChuyenGiao = new DT_LichChuyenGiaos();
                return new string[] { StaticWebPartId, LichChuyenGiao.WebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_ChuyenGiao), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string lichChuyenGiaoId = WebEnvironments.Request("id");
                DT_LichChuyenGiaoCls lichChuyenGiao = string.IsNullOrEmpty(lichChuyenGiaoId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (lichChuyenGiao == null)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Lịch chuyển giao này vừa bị xóa khỏi hệ thống."), false);
                    return RetAjaxOut;
                }
                DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiao.ID });

                DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);
                OneMES3.DM.Model.BenhVienCls benhVien = null;
                if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
                {
                    benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), lichChuyenGiao.BENHVIEN_MA);

                }
                BacSyCls canBoChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.BACSY_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);
                BacSyOwnerUserCls[] bacSyOwnerUsers = canBoChuyenGiao == null ? new BacSyOwnerUserCls[0] : CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = canBoChuyenGiao.ID });

                string[] giayTos;
                if (string.IsNullOrEmpty(lichChuyenGiao.GIAYTO))
                    giayTos = new string[0];
                else giayTos = lichChuyenGiao.GIAYTO.Split('|');
                string tenGiayTos = "";
                foreach (var giayTo in giayTos)
                {
                    var oGiayTo = CallBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().CreateModel(ORenderInfo, giayTo);
                    tenGiayTos += (oGiayTo != null ? oGiayTo.Ten : giayTo) + ", ";
                }
                if (tenGiayTos != "")
                    tenGiayTos = tenGiayTos.Substring(0, tenGiayTos.Length - 2);

                bool isCanBoChuyenGiao = bacSyOwnerUsers.Any(o => o.OWNERUSERID == user.OwnerUserId);
                #region Html
                string html =
                    "<div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "    <input type='button' id='btnHoanTatLich' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "' onclick='javascript:HoanTat();' style='float:left; margin-left: 20px; " + (lichChuyenGiao.TRANGTHAI == (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet && (isCanBoChuyenGiao || lichChuyenGiao.NGUOITAO_ID == user.OwnerUserId) ? null : "display:none;") + "'>\r\n" +
                    "    <input type='button' id='btnThuHoiHoanTatLich' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiHoanTat();' style='float:left; margin-left: 20px; " + (lichChuyenGiao.TRANGTHAI == (int)DT_LichChuyenGiaoCls.eTrangThai.HoanTat && (isCanBoChuyenGiao || lichChuyenGiao.NGUOITAO_ID == user.OwnerUserId) ? null : "display:none;") + "'>\r\n" +
                    "    <input type='button' id='btnQuanLyTaiLieu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Quản lý tài liệu") + "' onclick='javascript:PopupTaiLieu();' style='float:left; margin-left: 20px; " + (lichChuyenGiao.TRANGTHAI == (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet && (isCanBoChuyenGiao || lichChuyenGiao.NGUOITAO_ID == user.OwnerUserId) ? null : "display:none;") + "'>\r\n" +
                    "    <input type='button' id='btnSuaBaoCao' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Sửa báo cáo") + "' onclick='javascript:PopupKetQuaChuyenGiao();' style='float:left; margin-left: 20px; " + (lichChuyenGiao.TRANGTHAI == (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet && (isCanBoChuyenGiao || lichChuyenGiao.NGUOITAO_ID == user.OwnerUserId) ? null : "display:none;") + "'>\r\n" +
                    "    <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' ></div> \r\n" +
                    "</div>\r\n" +
                    "<div class='row'>\r\n" +
                    "   <input type='hidden' id='hdLichChuyenGiaoId' value='" + lichChuyenGiaoId + "'>\r\n" +
                    "   <div class='col-lg-12'>\r\n" +
                    "      <div class='ibox float-e-margins'>\r\n" +
                    "          <div class='ibox-title'>\r\n" +
                    "              <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin Chuyển giao") + "</h5>\r\n" +
                    "          </div>\r\n" +
                    "          <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Tên kỹ thuật:") + " \r\n" +
                    "                           <span id='spMa' class=\"valueForm\">" + (kyThuatChuyenGiao == null ? lichChuyenGiao.KYTHUAT_MA : kyThuatChuyenGiao.Ten) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-3 col-xs-3'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Từ ngày:") + " \r\n" +
                    "                           <span id='spTen' class=\"valueForm\">" + lichChuyenGiao.BATDAU.ToString("dd/MM/yyyy") + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-3 col-xs-3'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                 WebLanguage.GetLanguage(OSiteParam, "Đến ngày:") + " \r\n" +
                    "                           <span id='spTen' class=\"valueForm\">" + lichChuyenGiao.KETTHUC.ToString("dd/MM/yyyy") + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao:") + " \r\n" +
                    "                           <span id='spThoiLuong' class=\"valueForm\">" + (canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Bệnh viện nhận chuyển giao:") + " \r\n" +
                    "                           <span id='spNgayKhaiGiangDuKien' class=\"valueForm\">" + (benhVien == null ? null : benhVien.Ten) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-12 col-xs-12'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Giấy tờ:") + " \r\n" +
                    "                           <span id='spGiayTo' class=\"valueForm\">" + tenGiayTos + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='tabs-container'>\r\n" +
                    "                   <ul class='nav nav-tabs'>\r\n" +
                    "                       <li class='active'><a data-toggle='tab' href='#tab-LichChuyenGiaoChiTiet'>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách chuyển giao") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-KetQuaChuyenGiao'>" + WebLanguage.GetLanguage(OSiteParam, "Kết quả chuyển giao") + "</a></li>\r\n" +
                    "                       <li class=''><a data-toggle='tab' href='#tab-TaiLieu'>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu") + "</a></li>\r\n" +
                    "                   </ul>\r\n" +
                    "                   <div class='tab-content'>\r\n" +
                    "                       <div id='tab-LichChuyenGiaoChiTiet' class='tab-pane active'>\r\n" +
                                                DrawLichChuyenGiaoChiTiets(ORenderInfo, lichChuyenGiao.ID).HtmlContent +
                    "                       </div>\r\n" +
                    "                       <div id='tab-KetQuaChuyenGiao' class='tab-pane'>\r\n" +
                                                DrawKetQuaChuyenGiao(ORenderInfo, lichChuyenGiao.ID).HtmlContent +
                    "                       </div>\r\n" +
                    "                       <div id='tab-TaiLieu' class='tab-pane'>\r\n" +
                                                DrawTaiLieus(ORenderInfo, lichChuyenGiao.ID, false).HtmlContent +
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
                    "   var lichChuyenGiaoId='" + lichChuyenGiaoId + "';\r\n" +
                    "   var currentPopup = '';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "       $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "   });\r\n" +
                #region Truyền id phiếu cho nút print
                    "function OnWebPartLoad()\r\n" +
                    "{\r\n" +
                    "     return document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "}\r\n" +
                #endregion Truyền id phiếu cho nút print
                #region Show popup cập nhật kết quả chuyển giao
                    "function PopupKetQuaChuyenGiao()\r\n" +
                    "{\r\n" +
                    "     lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.PopupKetQuaChuyenGiao(RenderInfo, lichChuyenGiaoId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Báo cáo kết quả chuyển giao kỹ thuật") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    "     ValidateIntegerControl('#txtSoLuotBenhNhan', 0, 999);\r\n" +
                    "     ValidateIntegerControl('#txtSoCaHuongDanTh', 0, 999);\r\n" +
                    "     ValidateIntegerControl('#txtSoCaHoTroTh', 0, 999);\r\n" +
                    "     ValidateIntegerControl('#txtSoGioThamGia', 0, 999999);\r\n" +
                    "}\r\n" +
                #endregion Show popup cập nhật kết quả chuyển giao
                #region Save Thông tin báo cáo kết quả chuyển giao
                    "function SaveKetQuaChuyenGiao()\r\n" +
                    "{\r\n" +
                    "     lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "     soQD = document.getElementById('txtSoQD').value;\r\n" +
                    "     ngayQD = document.getElementById('dtNgayQD').value;\r\n" +
                    "     tuNgay = document.getElementById('dtTuNgay').value;\r\n" +
                    "     denNgay = document.getElementById('dtDenNgay').value;\r\n" +
                    "     soLuotBenhNhan = parseInt(document.getElementById('txtSoLuotBenhNhan').value);\r\n" +
                    "     soCaHuongDan = parseInt(document.getElementById('txtSoCaHuongDanTh').value);\r\n" +
                    "     soCaHoTro = parseInt(document.getElementById('txtSoCaHoTroTh').value);\r\n" +
                    "     soGioThamGia = parseInt(document.getElementById('txtSoGioThamGia').value);\r\n" +
                    "     chapHanhThoiGian = document.getElementById('txtChapHanhThoiGian').value;\r\n" +
                    "     chapHanhQuyChe = document.getElementById('txtChapHanhQuyChe').value;\r\n" +
                    "     phoiHop = document.getElementById('txtPhoiHop').value;\r\n" +
                    "     htNhiemVu = $('input[name=\"rdoHtNhiemVuCg\"]').filter(':checked').val();\r\n" +
                    "     deXuatThoiGian = document.getElementById('txtDeXuatThoiGian').value;\r\n" +
                    "     deXuatCheDo = document.getElementById('txtDeXuatCheDo').value;\r\n" +
                    "     deXuatDieuKien = document.getElementById('txtDeXuatDieuKien').value;\r\n" +
                    "     thoiGianBaoCao = document.getElementById('dtThoiGianBaoCao').value;\r\n" +
                    "     nxTinhThanThaiDoYThuc = document.getElementById('txtNxTinhThanThaiDoYThuc').value;\r\n" +
                    "     nxKhaNangThucHienDocLap = $('input[name=\"rdoNxKhaNangThucHienDocLapKt\"]').filter(':checked').val();\r\n" +
                    "     nxDungYcDeXuat = $('input[name=\"rdoNxDungYcDeXuatDv\"]').filter(':checked').val();\r\n" +
                    "     nxMucDoHtNhiemVu = $('input[name=\"rdoNxMucDoHtNhiemVuCg\"]').filter(':checked').val();\r\n" +
                    "     deXuatGiaiPhap = document.getElementById('txtDeXuatGiaiPhap').value;\r\n" +
                    "     noiNhanXet = document.getElementById('txtNoiNhanXet').value;\r\n" +
                    "     ngayNhanXet = document.getElementById('dtNgayNhanXet').value;\r\n" +
                    "     nguoiNhanXet = document.getElementById('txtNguoiNhanXet').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.SaveKetQuaChuyenGiao(RenderInfo, lichChuyenGiaoId, soQD, ngayQD, tuNgay, denNgay, soLuotBenhNhan, soCaHuongDan, soCaHoTro, soGioThamGia, chapHanhThoiGian, chapHanhQuyChe, phoiHop, " +
                    "htNhiemVu, deXuatThoiGian, deXuatCheDo, deXuatDieuKien, thoiGianBaoCao, nxTinhThanThaiDoYThuc, nxKhaNangThucHienDocLap, nxDungYcDeXuat, nxMucDoHtNhiemVu, deXuatGiaiPhap, noiNhanXet, ngayNhanXet, nguoiNhanXet).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawKetQuaChuyenGiao(RenderInfo, lichChuyenGiaoId).value;\r\n" +
                    "          $('#tab-KetQuaChuyenGiao').html(AjaxOut.HtmlContent);\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion Save Thông tin báo cáo kết quả chuyển giao
                #region Bắt sự kiện đóng popup
                    "function ClosePopup()\r\n" +
                    "{\r\n" +
                    "     lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawKetQuaChuyenGiao(RenderInfo, lichChuyenGiaoId).value;\r\n" +
                    "     $('#tab-KetQuaChuyenGiao').html(AjaxOut.HtmlContent);\r\n" +
                    "}\r\n" +
                #endregion Bắt sự kiện đóng popup

                #region Hiển thị row thêm mới lịch chuyển giao chi tiết
                    "   function ShowLichChuyenGiaoChiTiet(){\r\n" +
                    "       $('.CssEditorItem').hide();\r\n" +
                    "       $('.CssDisplayItem').show();\r\n" +
                    "       $('#trAddLichChuyenGiaoChiTiet').show();\r\n" +
                    "       $('#txtThoiGian').datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       ValidateIntegerControl('#txtSoCaHuongDan', 0, 999);\r\n" +
                    "       ValidateIntegerControl('#txtSoCaHoTro', 0, 999);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị row edit lịch chuyển giao chi tiết
                    "   function ShowEditItemLine(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItem').hide();\r\n" +
                    "       $('.CssDisplayItem').show();\r\n" +
                    "       $('#trAddLichChuyenGiaoChiTiet').hide();\r\n" +
                    "       document.getElementById('txtThoiGian'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtNoiDung'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtSoCaHuongDan'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtSoCaHoTro'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtCanBos'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spThoiGian'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spNoiDung'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spSoCaHuongDan'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spSoCaHoTro'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spCanBos'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveLichChuyenGiaoChiTiet'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditLichChuyenGiaoChiTiet'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('btnDeleteLichChuyenGiaoChiTiet'+rowIndex).style.display='none';\r\n" +

                    "       $('#txtThoiGian'+rowIndex).datetimepicker({ \r\n" +
                    "          format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       ValidateIntegerControl('#txtSoCaHuongDan'+rowIndex, 0, 999);\r\n" +
                    "       ValidateIntegerControl('#txtSoCaHoTro'+rowIndex, 0, 999);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật lịch chuyển giao chi tiết
                    "   function SaveLichChuyenGiaoChiTiet(rowIndex){\r\n" +
                    "       lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "       lichChuyenGiaoChiTietId = document.getElementById('hdLichChuyenGiaoChiTietId'+rowIndex).value;\r\n" +
                    "       thoiGian = document.getElementById('txtThoiGian'+rowIndex).value;\r\n" +
                    "       noiDung = document.getElementById('txtNoiDung'+rowIndex).value;\r\n" +
                    "       soCaHuongDan = parseInt(document.getElementById('txtSoCaHuongDan'+rowIndex).value);\r\n" +
                    "       soCaHoTro = parseInt(document.getElementById('txtSoCaHoTro'+rowIndex).value);\r\n" +
                    "       canBos = document.getElementById('txtCanBos'+rowIndex).value;\r\n" +
                    "       if(thoiGian=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn thời gian.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.SaveLichChuyenGiaoChiTiet(RenderInfo, lichChuyenGiaoId, lichChuyenGiaoChiTietId, thoiGian, noiDung, soCaHuongDan, soCaHoTro, canBos).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#tab-LichChuyenGiaoChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa lịch chuyển giao chi tiết
                    "   function DeleteLichChuyenGiaoChiTiet(rowIndex){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn xóa ngày chuyển giao này không?\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Xóa" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "               lichChuyenGiaoChiTietId = document.getElementById('hdLichChuyenGiaoChiTietId'+rowIndex).value;\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DeleteLichChuyenGiaoChiTiet(RenderInfo, lichChuyenGiaoId, lichChuyenGiaoChiTietId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa!") + "');\r\n" +
                    "                   $('#tab-LichChuyenGiaoChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Hoàn tất lịch chuyển giao
                    "   function HoanTat(){\r\n" +
                    "       lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "       swal({ \r\n" +
                    "               title: (\"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn hoàn tất lịch chuyển giao này?") + "\"), \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.UpdateTrangThai(RenderInfo, lichChuyenGiaoId, " + (int)DT_LichChuyenGiaoCls.eTrangThai.HoanTat + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "	                callGallAlert(errorInfo);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "	            toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã hoàn tất!") + "');\r\n" +
                    "               $('#btnHoanTatLich').hide();\r\n" +
                    "               $('#btnSuaBaoCao').hide();\r\n" +
                    "               $('#btnThuHoiHoanTatLich').show();\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion Hoàn tất lịch chuyển giao
                #region Thu hồi hoàn tất lịch chuyển giao
                    "   function ThuHoiHoanTat(){\r\n" +
                    "       lichChuyenGiaoId = document.getElementById('hdLichChuyenGiaoId').value;\r\n" +
                    "       swal({ \r\n" +
                    "               title: (\"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi hoàn tất lịch chuyển giao này?") + "\"), \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.UpdateTrangThai(RenderInfo, lichChuyenGiaoId, " + (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "	                callGallAlert(errorInfo);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "	            toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!") + "');\r\n" +
                    "               $('#btnHoanTatLich').show();\r\n" +
                    "               $('#btnSuaBaoCao').show();\r\n" +
                    "               $('#btnThuHoiHoanTatLich').hide();\r\n" +
                    "           }); \r\n" +
                    "   }\r\n" +
                #endregion Thu hồi hoàn tất lịch chuyển giao

                #region Show popup quản lý tài liệu
                    "function PopupTaiLieu()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.PopupTaiLieu(RenderInfo, lichChuyenGiaoId).value;\r\n" +
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
                #region Lấy về mảng byte của tài liệu được upload
                    "   var taiLieuInfos = [];\r\n" +
                    "   function GetFilesByteArray(sender) \r\n" +
                    "   {\r\n" +
                    "       taiLieuInfos = [];\r\n" +
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
                    "           GetFileByteArrayRecursive(0, sender.files)\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                    "   function GetFileByteArrayRecursive(taiLieuIndex, taiLieus) \r\n" +
                    "   {\r\n" +
                    "       if (taiLieuIndex < taiLieus.length) \r\n" +
                    "       {\r\n" +
                    "           var taiLieuInfo = new Object();\r\n" +
                    "           taiLieuInfo.TaiLieuFile = taiLieus[taiLieuIndex];\r\n" +
                    "           var filerdr = new FileReader();\r\n" +
                    "           filerdr.readAsArrayBuffer(taiLieus[taiLieuIndex]);\r\n" +
                    "           filerdr.onload = function(e) {\r\n" +
                    "               array = new Uint8Array(filerdr.result);\r\n" +
                    "               taiLieuByteArray = [];\r\n" +
                    "               for (var i = 0; i < array.length; i++) {\r\n" +
                    "                   taiLieuByteArray.push(array[i]);\r\n" +
                    "               }\r\n" +
                    "               taiLieuInfo.TaiLieuByteArray = taiLieuByteArray;\r\n" +
                    "               taiLieuInfos.push(taiLieuInfo);\r\n" +
                    "               GetFileByteArrayRecursive(taiLieuIndex + 1, taiLieus)\r\n" +
                    "           };\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion
                #region Upload tài liệu
                    "   function UploadTaiLieus(){\r\n" +
                    "       if (taiLieuInfos.length == 0) {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn file cần upload.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       ghiChu = document.getElementById('txtMoTaTaiLieu').value;\r\n" +
                    "       for (i = 0; i < taiLieuInfos.length; i++) {\r\n" +
                    "           fileName = taiLieuInfos[i].TaiLieuFile.name;\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.UploadTaiLieu(RenderInfo, lichChuyenGiaoId, fileName, ghiChu, taiLieuInfos[i].TaiLieuByteArray).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       $('#fileUploadTaiLieu')[0].value='';\r\n" +
                    "       $('#txtMoTaTaiLieu').val('');\r\n" +
                    "       $('#divTenTaiLieuChon').html('');\r\n" +
                    //Hiển thị lại danh sách tài liệu trên popup
                    "       AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawTaiLieus(RenderInfo, lichChuyenGiaoId, true).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divTaiLieuList').html(AjaxOut.HtmlContent);\r\n" +
                    //Hiển thị lại danh sách tài liệu trên form
                    "       AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawTaiLieus(RenderInfo, lichChuyenGiaoId, false).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#tab-TaiLieu').html(AjaxOut.HtmlContent);\r\n" +
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
                    "                   AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DeleteTaiLieu(RenderInfo, taiLieuId).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    //Hiển thị lại danh sách tài liệu trên popup
                    "                   AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawTaiLieus(RenderInfo, lichChuyenGiaoId, true).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    "                   $('#divTaiLieuList').html(AjaxOut.HtmlContent);\r\n" +
                    //Hiển thị lại danh sách tài liệu trên form`
                    "                   AjaxOut = OneTSQ.WebParts.DT_ChuyenGiao.DrawTaiLieus(RenderInfo, lichChuyenGiaoId, false).value;\r\n" +
                    "                   if(AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                       return;\r\n" +
                    "                   }\r\n" +
                    "                   $('#tab-TaiLieu').html(AjaxOut.HtmlContent);\r\n" +
                    "               }\r\n " +
                    "           });\r\n" +
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
        public static AjaxOut DrawLichChuyenGiaoChiTiets(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId });
                int lichChuyenGiaoChiTietTotal = lichChuyenGiaoChiTiets.Count();
                bool isEdit = lichChuyenGiao.TRANGTHAI == (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet &&
                    CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { OWNERUSERID = user.OwnerUserId, BACSYID = lichChuyenGiao.BACSY_ID }).Count() > 0;
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th width=100px>" + WebLanguage.GetLanguage(OSiteParam, "Ngày") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Số ca/KT đã hướng dẫn") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Số ca/KT đã hỗ trợ học viên thực hiện") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên cán bộ nhận chuyển giao") + " </th> \r\n" +
                    (isEdit ? "         <th><a href='javascript:ShowLichChuyenGiaoChiTiet();' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới thông tin chuyển giao ") + "'><i class='fa fa-plus' style='font-size:20px; color: white;'></i></a></th> \r\n" : null) +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n" +
                    "                 <tr id='trAddLichChuyenGiaoChiTiet' style='display:none;'> \r\n" +
                    "                    <input type='hidden' id='hdLichChuyenGiaoChiTietId' value=''>\r\n" +
                    "                    <td></td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='text' class='form-control' id='txtThoiGian' value=''></td> \r\n" +
                    "                    <td style='vertical-align: middle;'><input type='text' class='form-control' id='txtNoiDung' value=''></td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='number' class='form-control' id='txtSoCaHuongDan' value=''></td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='number' class='form-control' id='txtSoCaHoTro' value=''></td> \r\n" +
                    "                    <td style='vertical-align: middle;'><input type='text' class='form-control' id='txtCanBos' value=''></td> \r\n" +
                    "                    <td style='text-align:center;'><a href='javascript:SaveLichChuyenGiaoChiTiet(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                    "                 </tr> \r\n";
                for (                 int iIndex = 0; iIndex < lichChuyenGiaoChiTietTotal; iIndex++)
                {
                    Html +=
                    "                 <tr> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='text' class='form-control CssEditorItem' style='display:none; ' id='txtThoiGian" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].THOIGIAN.ToString("dd/MM/yyyy") + "'><span class='CssDisplayItem' id='spThoiGian" + iIndex + "'>" + lichChuyenGiaoChiTiets[iIndex].THOIGIAN.ToString("dd/MM/yyyy") + "</span></td> \r\n" +
                    "                    <td style='vertical-align: middle;'><input type='text' class='form-control CssEditorItem' style='display:none; ' id='txtNoiDung" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].NOIDUNG + "'><span class='CssDisplayItem' id='spNoiDung" + iIndex + "'>" + lichChuyenGiaoChiTiets[iIndex].NOIDUNG + "</span></td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='number' class='form-control CssEditorItem' style='display:none; ' id='txtSoCaHuongDan" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].SOCAHUONGDAN + "'><span class='CssDisplayItem' id='spSoCaHuongDan" + iIndex + "'>" + lichChuyenGiaoChiTiets[iIndex].SOCAHUONGDAN + "</span></td> \r\n" +
                    "                    <td style='text-align: center; vertical-align: middle;'><input type='number' class='form-control CssEditorItem' style='display:none; ' id='txtSoCaHoTro" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].SOCAHOTRO + "'><span class='CssDisplayItem' id='spSoCaHoTro" + iIndex + "'>" + lichChuyenGiaoChiTiets[iIndex].SOCAHOTRO + "</span></td> \r\n" +
                    "                    <td style='vertical-align: middle;'><input type='text' class='form-control CssEditorItem' style='display:none; ' id='txtCanBos" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].CANBOS + "'><span class='CssDisplayItem' id='spCanBos" + iIndex + "'>" + lichChuyenGiaoChiTiets[iIndex].CANBOS + "</span></td> \r\n" +
                    (
                        isEdit ?
                    "                    <input type='hidden' id='hdLichChuyenGiaoChiTietId" + iIndex + "' value='" + lichChuyenGiaoChiTiets[iIndex].ID + "'>\r\n" +
                    "                    <td style='text-align:center;'>\r\n" +
                    "                        <a id='btnSaveLichChuyenGiaoChiTiet" + iIndex + "' class='CssEditorItem' style='display:none' href='javascript:SaveLichChuyenGiaoChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                    "                        <a id='btnEditLichChuyenGiaoChiTiet" + iIndex + "' class='CssEditorItem' href='javascript:ShowEditItemLine(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                    "                        <a id='btnDeleteLichChuyenGiaoChiTiet" + iIndex + "' class='CssEditorItem' href='javascript:DeleteLichChuyenGiaoChiTiet(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                    "                    </td> \r\n"
                        : null) +
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
        public static AjaxOut DrawKetQuaChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId });
                DT_KetQuaChuyenGiaoCls ketQuaChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (ketQuaChuyenGiao == null)
                    ketQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls()
                    {
                        SOCAHUONGDAN = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHUONGDAN),
                        SOCAHOTRO = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHOTRO)
                    };
                DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);

                OneMES3.DM.Model.BenhVienCls benhVien = null;
                if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
                {
                    benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), lichChuyenGiao.BENHVIEN_MA);
                }

                BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);

                OneMES3.DM.Model.ChuyenMonCls chuyenMon = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
                {
                    chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENMONMA);
                }

                OneMES3.DM.Model.ChuyenNganhCls chuyenNganh = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
                {
                    chuyenNganh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenNganhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENNGANHMA);
                }

                OneMES3.DM.Model.DonViCongTacCls donViCongTac = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.DONVIMA))
                {
                    donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.DONVIMA);
                }
                string html =
                    "   <center> \r\n" +
                    "       <div style='width: 800px; padding: 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif; font-size:18px;' > \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM") + "</b><br>\r\n" +
                    "                  <b><span style='border-bottom:solid 1px;'>" + WebLanguage.GetLanguage(OSiteParam, "Độc lập - Tự do - Hạnh phúc") + "</span></b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center; margin-top:50px;'>\r\n" +
                    "                  <b style='font-size:16px;'>" + WebLanguage.GetLanguage(OSiteParam, "BÁO CÁO") + "</b><br><br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "KẾT QUẢ CỦA CÁN BỘ HỖ TRỢ CHUYỂN GIAO KỸ THUẬT") + "</b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>I. " + WebLanguage.GetLanguage(OSiteParam, "Phần chung:") + "</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Họ và tên") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Trình độ chuyên môn") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (chuyenMon == null ? null : chuyenMon.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (chuyenNganh == null ? null : chuyenNganh.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (donViCongTac == null ? null : donViCongTac.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Được cử đi công tác tại") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (benhVien == null ? null : benhVien.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Theo quyết định số") + " \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.SOQD + "</span> " +
                                            WebLanguage.GetLanguage(OSiteParam, "ngày") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.NGAYQD == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYQD.Value.Day.ToString()) + "</span> " +
                                            WebLanguage.GetLanguage(OSiteParam, "tháng") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.NGAYQD == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYQD.Value.Month.ToString()) + "</span> " +
                                            WebLanguage.GetLanguage(OSiteParam, "năm") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.NGAYQD == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYQD.Value.Year.ToString()) + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                             WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Day.ToString()) + "</span> " +
                                             WebLanguage.GetLanguage(OSiteParam, "tháng") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Month.ToString()) + "</span> " +
                                             WebLanguage.GetLanguage(OSiteParam, "năm") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Year.ToString()) + "</span> " +
                                             WebLanguage.GetLanguage(OSiteParam, "đến ngày") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Day.ToString()) + "</span> " +
                                             WebLanguage.GetLanguage(OSiteParam, "tháng") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Month.ToString()) + "</span> " +
                                             WebLanguage.GetLanguage(OSiteParam, "năm") + " <span class=\"valueForm\">" + (ketQuaChuyenGiao.TUNGAY == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.TUNGAY.Value.Year.ToString()) + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>II. " + WebLanguage.GetLanguage(OSiteParam, "Tự nhận xét, đánh giá kết quả đi chuyển giao kỹ thuật") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>1. " + WebLanguage.GetLanguage(OSiteParam, "Kết quả thực hiện chuyên môn") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Tên kỹ thuật chuyển giao cho đơn vị") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số lượt bệnh nhân đã khám, điều trị") + ":\r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.SOLUOTBENHNHAN + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số ca đã hướng dẫn thực hiện kỹ thuật") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.SOCAHUONGDAN + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số ca hỗ trợ cho cán bộ nhận kỹ thuật chuyển giao tự thực hiện kỹ thuật") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.SOCAHOTRO + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số giờ tham gia tập huấn, đào tạo") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (ketQuaChuyenGiao.SOGIOTHAMGIA == null ? null : ketQuaChuyenGiao.SOGIOTHAMGIA.Value.ToString("#,##0")) + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>2. " + WebLanguage.GetLanguage(OSiteParam, "Tinh thần, thái độ, ý thức tổ chức kỷ luật") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Việc chấp hành thời gian đến chuyển giao") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.CHAPHANHTHOIGIAN + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Việc chấp hành các quy chế chuyên môn và các quy định của đơn vị") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.CHAPHANHQUYCHE + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Sự phối hợp, quan hệ với đồng nghiệp nơi đến chuyển giao") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + ketQuaChuyenGiao.PHOIHOP + "</span>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>3. " + WebLanguage.GetLanguage(OSiteParam, "Tự đánh giá mức độ hoàn thành nhiệm vụ") + ":</b> (" +
                                       WebLanguage.GetLanguage(OSiteParam, "Tự chấm theo thang điểm 100") + "): <i>" + WebLanguage.GetLanguage(OSiteParam, "Đánh dấu x vào ô trống thích hợp") + "</i></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <table>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                     WebLanguage.GetLanguage(OSiteParam, "Hoàn thành xuất sắc nhiệm vụ (Đạt >= 90 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                    string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                     WebLanguage.GetLanguage(OSiteParam, "Hoàn thành tốt nhiệm vụ (Đạt 70 – 89 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                    string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Hoàn thành nhiệm vụ (Đạt 50 – 69 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                    string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Không hoàn thành nhiệm vụ (Đạt < 50 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                    string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>III. " + WebLanguage.GetLanguage(OSiteParam, "Những đề xuất, kiến nghị") + ": </b>" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>1. " + WebLanguage.GetLanguage(OSiteParam, "Về thời gian hỗ trợ chuyển giao kỹ thuật") + ":</b>\r\n" +
                    "                       <div class=\"valueForm\">" + (ketQuaChuyenGiao.DEXUATTHOIGIAN == null ? null : ketQuaChuyenGiao.DEXUATTHOIGIAN.Replace("\n", "<br>")) + "</div>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>2. " + WebLanguage.GetLanguage(OSiteParam, "Về chế độ, chính sách đối với cán bộ đi chuyển giao") + ":</b>\r\n" +
                    "                       <div class=\"valueForm\">" + (ketQuaChuyenGiao.DEXUATCHEDO == null ? null : ketQuaChuyenGiao.DEXUATCHEDO.Replace("\n", "<br>")) + "</div>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Về điều kiện, cơ sở vật chất của đơn vị đến chuyển giao") + ":</b>\r\n" +
                    "                       <div class=\"valueForm\">" + (ketQuaChuyenGiao.DEXUATDIEUKIEN == null ? null : ketQuaChuyenGiao.DEXUATDIEUKIEN.Replace("\n", "<br>")) + "</div>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin: 20px 0 100px 0'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='col-md-6 col-xs-6'>\r\n" +
                    "               <div class=\"form-group\" style='text-align:center;'>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Hà Nội, ngày") + " " + (ketQuaChuyenGiao.THOIGIANBAOCAO == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.Day.ToString()) + " " +
                                        WebLanguage.GetLanguage(OSiteParam, "tháng") + " " + (ketQuaChuyenGiao.THOIGIANBAOCAO == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.Month.ToString()) + " " +
                                        WebLanguage.GetLanguage(OSiteParam, "năm") + " " + (ketQuaChuyenGiao.THOIGIANBAOCAO == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.Year.ToString()) +
                    "                   <br><b>" + WebLanguage.GetLanguage(OSiteParam, "Cán bộ chuyển giao") + "</b><br>\r\n" +
                                        (canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN) +
                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>IV. " + WebLanguage.GetLanguage(OSiteParam, "Nhận xét, đánh giá của thủ trưởng đơn vị nơi cán bộ đến chuyển giao") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>1. " + WebLanguage.GetLanguage(OSiteParam, "Về tinh thần, thái độ, ý thức tổ chức kỷ luật") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div class=\"valueForm\">" + (ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC == null ? null : ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC.Replace("\n", "<br>")) + "</div>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>2. " + WebLanguage.GetLanguage(OSiteParam, "Về khả năng chuyên môn của cán bộ đi chuyển giao kỹ thuật tại đơn vị") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <table width='500px'>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td style='width: 100px; text-align:center;'>\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Có") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td style='width: 100px; text-align:center;'>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Không") + "\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Khả năng thực hiện độc lập kỹ thuật") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                        string.Format("<input type='radio' value=1 name='rdoNxKhaNangThucHienDocLap' {0}>\r\n", ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP == 1 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                           <center>\r\n" +
                                                   string.Format("<input type='radio' value=2 name='rdoNxKhaNangThucHienDocLap' {0}>\r\n", ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP == 2 ? "checked" : null) +
                    "                           </center>\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Đúng với yêu cầu đề xuất của đơn vị") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=1 name='rdoNxDungYcDeXuat' {0}>\r\n", ketQuaChuyenGiao.NXDUNGYCDEXUAT == 1 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=2 name='rdoNxDungYcDeXuat' {0}>\r\n", ketQuaChuyenGiao.NXDUNGYCDEXUAT == 2 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Về mức độ hoàn thành nhiệm vụ") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <table>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành xuất sắc nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành tốt nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Không hoàn thành nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                    string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVu' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4. " + WebLanguage.GetLanguage(OSiteParam, "Đề xuất giải pháp nâng cao hiệu quả và duy trì tính bền vững của hoạt động đào tạo, chuyển giao kỹ thuật?") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <div class=\"valueForm\">" + (ketQuaChuyenGiao.DEXUATGIAIPHAP == null ? null : ketQuaChuyenGiao.DEXUATGIAIPHAP.Replace("\n", "<br>")) + "</div>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='margin: 20px 0 0px 0'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\" style='text-align:center;'>\r\n" +
                                            ketQuaChuyenGiao.NOINHANXET + ", " +
                                            WebLanguage.GetLanguage(OSiteParam, "ngày") + " " + (ketQuaChuyenGiao.NGAYNHANXET == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYNHANXET.Value.Day.ToString()) + " " +
                                            WebLanguage.GetLanguage(OSiteParam, "tháng") + " " + (ketQuaChuyenGiao.NGAYNHANXET == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYNHANXET.Value.Month.ToString()) + " " +
                                            WebLanguage.GetLanguage(OSiteParam, "năm") + " " + (ketQuaChuyenGiao.NGAYNHANXET == null ? "&nbsp;&nbsp;&nbsp;&nbsp;" : ketQuaChuyenGiao.NGAYNHANXET.Value.Year.ToString()) +
                    "                       <br><b>" + WebLanguage.GetLanguage(OSiteParam, "Lãnh đạo đơn vị nhận chuyển giao") + "</b><br>\r\n" +
                                            ketQuaChuyenGiao.NGUOINHANXET +
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
        public static AjaxOut PopupKetQuaChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId });
                DT_KetQuaChuyenGiaoCls ketQuaChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (ketQuaChuyenGiao == null)
                    ketQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls()
                    {
                        SOCAHUONGDAN = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHUONGDAN),
                        SOCAHOTRO = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHOTRO)
                    };
                DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);

                OneMES3.DM.Model.BenhVienCls benhVien = null;
                if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
                {
                    benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), lichChuyenGiao.BENHVIEN_MA);
                }

                BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);

                OneMES3.DM.Model.ChuyenMonCls chuyenMon = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
                {
                    chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENMONMA);
                }

                OneMES3.DM.Model.ChuyenNganhCls chuyenNganh = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
                {     
                    chuyenNganh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenNganhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENNGANHMA);
                }

                OneMES3.DM.Model.DonViCongTacCls donViCongTac = null;
                if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.DONVIMA))
                {
                    donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.DONVIMA);
                }
                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "<form action='javascript:SaveKetQuaChuyenGiao();'> \r\n" +
                    "<input type='hidden' id='hdClose'>\r\n" +
                    "<div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                    "   <center> \r\n" +
                    "       <div style='width: 800px; padding: 50px; border:solid 1px; text-align:left; font-family: \"Times New Roman\", Times, serif; font-size:18px;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                #region Thông tin kế hoạch lớp
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center;'>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM") + "</b><br>\r\n" +
                    "                  <b><span style='border-bottom:solid 1px;'>" + WebLanguage.GetLanguage(OSiteParam, "Độc lập - Tự do - Hạnh phúc") + "</span></b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12' style='text-align:center; margin-top:50px;'>\r\n" +
                    "                  <b style='font-size:16px;'>" + WebLanguage.GetLanguage(OSiteParam, "BÁO CÁO") + "</b><br><br>\r\n" +
                    "                  <b>" + WebLanguage.GetLanguage(OSiteParam, "KẾT QUẢ CỦA CÁN BỘ HỖ TRỢ CHUYỂN GIAO KỸ THUẬT") + "</b><br><br>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>I. " + WebLanguage.GetLanguage(OSiteParam, "Phần chung:") + "</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Họ và tên") + ": \r\n" +
                    "                      <span class=\"valueForm\">" + (canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Trình độ chuyên môn") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (chuyenMon == null ? null : chuyenMon.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (chuyenNganh == null ? null : chuyenNganh.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (donViCongTac == null ? null : donViCongTac.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Được cử đi công tác tại") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (benhVien == null ? null : benhVien.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Theo quyết định số") + ":\r\n" +
                    "                       <input type='text' class='form-control' id='txtSoQD' style='display:inline; width: 150px;' value='" + ketQuaChuyenGiao.SOQD + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "ngày") + ":\r\n" +
                    "                       <input type='text' data-mask='99/99/9999' class='datepicker form-control' id='dtNgayQD' style='display:inline; width: 100px;' value='" + (ketQuaChuyenGiao.NGAYQD == null ? null : ketQuaChuyenGiao.NGAYQD.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Thời gian: Từ") + ":\r\n" +
                    "                       <input type='text' data-mask='99/99/9999' class='datepicker form-control' id='dtTuNgay' style='display:inline; width: 100px;' value='" + (ketQuaChuyenGiao.TUNGAY == null ? null : ketQuaChuyenGiao.TUNGAY.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Đến") + ":\r\n" +
                    "                       <input type='text' data-mask='99/99/9999' class='datepicker form-control' id='dtDenNgay' style='display:inline; width: 100px;' value='" + (ketQuaChuyenGiao.TUNGAY == null ? null : ketQuaChuyenGiao.DENNGAY.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "               </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>II. " + WebLanguage.GetLanguage(OSiteParam, "Tự nhận xét, đánh giá kết quả đi chuyển giao kỹ thuật") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>1. " + WebLanguage.GetLanguage(OSiteParam, "Kết quả thực hiện chuyên môn") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Tên kỹ thuật chuyển giao cho đơn vị") + ": \r\n" +
                    "                       <span class=\"valueForm\">" + (kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten) + "</span>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số lượt bệnh nhân đã khám, điều trị") + ":\r\n" +
                    "                       <input type='number' class='form-control' id='txtSoLuotBenhNhan' style='display:inline; width: 80px;' value='" + ketQuaChuyenGiao.SOLUOTBENHNHAN + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số ca đã hướng dẫn thực hiện kỹ thuật") + ": \r\n" +
                    "                       <input type='number' class='form-control' id='txtSoCaHuongDanTh' style='display:inline; width: 80px;' value='" + ketQuaChuyenGiao.SOCAHUONGDAN + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số ca hỗ trợ cho cán bộ nhận kỹ thuật chuyển giao tự thực hiện kỹ thuật") + ": \r\n" +
                    "                       <input type='number' class='form-control' id='txtSoCaHoTroTh' style='display:inline; width: 80px;' value='" + ketQuaChuyenGiao.SOCAHOTRO + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số giờ tham gia tập huấn, đào tạo") + ": \r\n" +
                    "                       <input type='number' class='form-control' id='txtSoGioThamGia' style='display:inline; width: 80px;' value='" + ketQuaChuyenGiao.SOGIOTHAMGIA + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>2. " + WebLanguage.GetLanguage(OSiteParam, "Tinh thần, thái độ, ý thức tổ chức kỷ luật") + ":</b></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Việc chấp hành thời gian đến chuyển giao") + ": \r\n" +
                    "                       <input type='text' class='form-control' id='txtChapHanhThoiGian' width=80px value='" + ketQuaChuyenGiao.CHAPHANHTHOIGIAN + "'>" +
                    "                   x</div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Việc chấp hành các quy chế chuyên môn và các quy định của đơn vị") + ": \r\n" +
                    "                       <input type='text' class='form-control' id='txtChapHanhQuyChe' width=80px value='" + ketQuaChuyenGiao.CHAPHANHQUYCHE + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Sự phối hợp, quan hệ với đồng nghiệp nơi đến chuyển giao") + ": \r\n" +
                    "                       <input type='text' class='form-control' id='txtPhoiHop' width=80px value='" + ketQuaChuyenGiao.PHOIHOP + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>3. " + WebLanguage.GetLanguage(OSiteParam, "Tự đánh giá mức độ hoàn thành nhiệm vụ") + ":</b> (" +
                                       WebLanguage.GetLanguage(OSiteParam, "Tự chấm theo thang điểm 100") + "): <i>" + WebLanguage.GetLanguage(OSiteParam, "Đánh dấu x vào ô trống thích hợp") + "</i></span>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <table>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành xuất sắc nhiệm vụ (Đạt >= 90 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành tốt nhiệm vụ (Đạt 70 – 89 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành nhiệm vụ (Đạt 50 – 69 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Không hoàn thành nhiệm vụ (Đạt < 50 điểm)") + " \r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh, ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <span style='float:left; margin-right:5px;'><b>III. " + WebLanguage.GetLanguage(OSiteParam, "Những đề xuất, kiến nghị") + ": </b>" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>1. " + WebLanguage.GetLanguage(OSiteParam, "Về thời gian hỗ trợ chuyển giao kỹ thuật") + ":</b>\r\n" +
                    "                       <textarea rows=4 class='form-control' id='txtDeXuatThoiGian' >" + ketQuaChuyenGiao.DEXUATTHOIGIAN + "</textarea>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>2. " + WebLanguage.GetLanguage(OSiteParam, "Về chế độ, chính sách đối với cán bộ đi chuyển giao") + ":</b>\r\n" +
                    "                       <textarea rows=4 class='form-control' id='txtDeXuatCheDo' >" + ketQuaChuyenGiao.DEXUATCHEDO + "</textarea>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Về điều kiện, cơ sở vật chất của đơn vị đến chuyển giao") + ":</b>\r\n" +
                    "                       <textarea rows=4 class='form-control' id='txtDeXuatDieuKien' >" + ketQuaChuyenGiao.DEXUATDIEUKIEN + "</textarea>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Ngày báo cáo") + ":\r\n" +
                    "                       <input type='text' data-mask='99/99/9999' class='datepicker form-control' id='dtThoiGianBaoCao' style='display:inline; width: 100px;' value='" + (ketQuaChuyenGiao.THOIGIANBAOCAO == null ? null : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>IV. " + WebLanguage.GetLanguage(OSiteParam, "Nhận xét, đánh giá của thủ trưởng đơn vị nơi cán bộ đến chuyển giao") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>1. " + WebLanguage.GetLanguage(OSiteParam, "Về tinh thần, thái độ, ý thức tổ chức kỷ luật") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <textarea rows=4 class='form-control' id='txtNxTinhThanThaiDoYThuc' >" + ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC + "</textarea>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>2. " + WebLanguage.GetLanguage(OSiteParam, "Về khả năng chuyên môn của cán bộ đi chuyển giao kỹ thuật tại đơn vị") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <table width='500px'>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td style='width: 100px; text-align:center;'>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Có") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td style='width: 100px; text-align:center;'>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Không") + "\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Khả năng thực hiện độc lập kỹ thuật") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=1 name='rdoNxKhaNangThucHienDocLapKt' {0}>\r\n", ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP == 1 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=2 name='rdoNxKhaNangThucHienDocLapKt' {0}>\r\n", ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP == 2 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Đúng với yêu cầu đề xuất của đơn vị") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=1 name='rdoNxDungYcDeXuatDv' {0}>\r\n", ketQuaChuyenGiao.NXDUNGYCDEXUAT == 1 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                    "                               <center>\r\n" +
                                                       string.Format("<input type='radio' value=2 name='rdoNxDungYcDeXuatDv' {0}>\r\n", ketQuaChuyenGiao.NXDUNGYCDEXUAT == 2 ? "checked" : null) +
                    "                               </center>\r\n" +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>3. " + WebLanguage.GetLanguage(OSiteParam, "Về mức độ hoàn thành nhiệm vụ") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-6 col-xs-6'>\r\n" +
                    "                   <table>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành xuất sắc nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành tốt nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Hoàn thành nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                       <tr>\r\n" +
                    "                           <td>\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Không hoàn thành nhiệm vụ") + "\r\n" +
                    "                           </td>\r\n" +
                    "                           <td>\r\n" +
                                                   string.Format("<input type='radio' style='display: inline;' value={0} name='rdoNxMucDoHtNhiemVuCg' {1}>\r\n", (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh, ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "checked" : null) +
                    "                           </td>\r\n" +
                    "                       </tr>\r\n" +
                    "                   </table>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "              <div class='col-md-12 col-xs-12'>\r\n" +
                    "                  <b>4. " + WebLanguage.GetLanguage(OSiteParam, "Đề xuất giải pháp nâng cao hiệu quả và duy trì tính bền vững của hoạt động đào tạo, chuyển giao kỹ thuật?") + ":</b>\r\n" +
                    "              </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                    "                       <textarea rows=4 class='form-control' id='txtDeXuatGiaiPhap' >" + ketQuaChuyenGiao.DEXUATGIAIPHAP + "</textarea>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-3 col-xs-3'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Nơi nhận xét") + ":\r\n" +
                    "                       <input type='text' class='form-control' id='txtNoiNhanXet' value='" + ketQuaChuyenGiao.NOINHANXET + "'>" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "                   <div class='col-md-3 col-xs-3'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Ngày nhận xét") + ":\r\n" +
                    "                           <input type='text' data-mask='99/99/9999' class='datepicker form-control' id='dtNgayNhanXet' value='" + (ketQuaChuyenGiao.NGAYNHANXET == null ? null : ketQuaChuyenGiao.NGAYNHANXET.Value.ToString("dd/MM/yyyy")) + "'>" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-6'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +
                                                 WebLanguage.GetLanguage(OSiteParam, "Người nhận xét") + ":\r\n" +
                    "                           <input type='text' class='form-control' id='txtNguoiNhanXet' value='" + ketQuaChuyenGiao.NGUOINHANXET + "'>" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "          </div>\r\n" +
                #endregion
                    "       </div> \r\n" +//set chiều rộng tương đương khổ giấy A4
                    "   </center> \r\n" +
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawTaiLieus(RenderInfoCls ORenderInfo, string khoaHocId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                DT_TaiLieuChuyenGiaoCls[] taiLieus = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Reading(ORenderInfo, new DT_TaiLieuChuyenGiaoFilterCls() { LICHCHUYENGIAO_ID = khoaHocId });
                int taiLieuQuantity = taiLieus.Count();
                string html =
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tài liệu") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Người tạo") + " </th> \r\n" +
                    "                     <th  style='width:130px;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian tạo") + " </th> \r\n" +
                    (isEditor ? "         <th style='width:70px;'>" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "</th> \r\n" : null) +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < taiLieuQuantity; iIndex++)
                {
                    bool xoaPermission = isEditor && (user.OwnerUserId == taiLieus[iIndex].NGUOITAO_ID || user.IsSystemAdmin == 1);
                    string taiLieuUrl = Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedDaoTaoFilePath"], taiLieus[iIndex].TENTEP);
                    OwnerUserCls nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, taiLieus[iIndex].NGUOITAO_ID);
                    html +=
                    "                 <tr> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + (File.Exists(taiLieus[iIndex].DUONGDAN) ? "<a href='" + taiLieuUrl + "'>" + taiLieus[iIndex].TENHIENTHI + "</a>" : taiLieus[iIndex].TENHIENTHI) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + taiLieus[iIndex].GHICHU + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + (nguoiTao == null ? null : nguoiTao.FullName) + "</td> \r\n" +
                    "                     <td style='text-align:center; vertical-align: middle;'>" + taiLieus[iIndex].NGAYTAO.ToString("HH:mm dd/MM/yyyy") + "</td> \r\n" +
                    (isEditor ? "         <td style='text-align:center; vertical-align: middle;'>\r\n" +
                        (xoaPermission ? "  <a href='javascript:DeleteTaiLieu(\"" + taiLieus[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" : null) +
                    "                    </td> \r\n" : null) +
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
                "           <input id='fileUploadTaiLieu' type='file' multiple onchange='GetFilesByteArray(this);' style='display:none;'/>\r\n" +
                "           <button class='btn btn-primary' type='button' style='margin-top: -3px;' onclick='$(\"#fileUploadTaiLieu\").click(); $(\"#divTenTaiLieuChon\").html(\"\");'>" + WebLanguage.GetLanguage(OSiteParam, "Chọn file") + "</button>\r\n" +
                "       </div>\r\n" +
                "       <div class='col-md-10 col-xs-12'>\r\n" +
                "           <span class=\"valueForm\"><input type='text' class='form-control' id='txtMoTaTaiLieu' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Nhập ghi chú tài liệu tại đây") + "'></span>\r\n" +
                "       </div>\r\n" +
                "       <div class='col-md-1 col-xs-2'>\r\n" +
                "           <button style='margin-top: -3px;' onclick=\"javascript:UploadTaiLieus();\" type=\"button\" class=\"btn btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Tải") + "</button>\r\n" +
                "       </div>\r\n" +
                "   </div>\r\n" +
                "   <div class='col-md-12 col-xs-12' id='divTenTaiLieuChon'>\r\n" +
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveKeHoachLop(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string batDau, string ketThuc, string thoiGianTiepDon, string diaDiemTiepDon, string batDauLt, string ketThucLt, string diaDiemLt, string batDauTh, string ketThucTh, string diaDiemTh, int? soLuongNhomTh, int? soHvTrongNhomTh, string thoiGianDanhGiaTdt,
            string diaDiemDanhGiaTdt, string thoiGianGiaiDapThacMac, string diaDiemGiaiDapThacMac, string batDauThiLt, string ketThucThiLt, string diaDiemThiLt, string batDauThiVd, string ketThucThiVd, string diaDiemThiVd, string batDauThiTh, string ketThucThiTh, string diaDiemThiTh, string thoiGianBeGiang, string diaDiemBeGiang, string lanhDao)
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
                DT_KeHoachLopCls keHoachLop = CallBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (keHoachLop == null)
                {
                    keHoachLop = new DT_KeHoachLopCls();
                    keHoachLop.ID = lichChuyenGiaoId;
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
        public static AjaxOut SaveLichChuyenGiaoChiTiet(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string lichChuyenGiaoChiTietId, string thoiGian, string noiDung, int? soCaHuongDan, int? soCaHoTro, string canBos)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId });
                DateTime dtThoiGian = DateTime.ParseExact(thoiGian, "dd/MM/yyyy", null);
                if (lichChuyenGiaoChiTiets.Any(o => o.THOIGIAN == dtThoiGian && o.ID != lichChuyenGiaoChiTietId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian này đã có trong danh sách.\nXin chọn thời gian khác.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(lichChuyenGiaoChiTietId))//thêm mới
                {
                    DT_LichChuyenGiaoChiTietCls lichChuyenGiaoChiTiet = new DT_LichChuyenGiaoChiTietCls();
                    lichChuyenGiaoChiTiet.ID = Guid.NewGuid().ToString();
                    lichChuyenGiaoChiTiet.LICHCHUYENGIAO_ID = lichChuyenGiaoId;
                    lichChuyenGiaoChiTiet.THOIGIAN = dtThoiGian;
                    lichChuyenGiaoChiTiet.NOIDUNG = noiDung;
                    lichChuyenGiaoChiTiet.SOCAHUONGDAN = soCaHuongDan;
                    lichChuyenGiaoChiTiet.SOCAHOTRO = soCaHoTro;
                    lichChuyenGiaoChiTiet.CANBOS = canBos;
                    lichChuyenGiaoChiTiet.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichChuyenGiaoChiTiet.NGAYTAO = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Add(ORenderInfo, lichChuyenGiaoChiTiet);
                }
                else//cập nhật
                {
                    DT_LichChuyenGiaoChiTietCls lichChuyenGiaoChiTiet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().CreateModel(ORenderInfo, lichChuyenGiaoChiTietId);
                    if (lichChuyenGiaoChiTiet == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ngày chuyển giao này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    lichChuyenGiaoChiTiet.THOIGIAN = dtThoiGian;
                    lichChuyenGiaoChiTiet.NOIDUNG = noiDung;
                    lichChuyenGiaoChiTiet.SOCAHUONGDAN = soCaHuongDan;
                    lichChuyenGiaoChiTiet.SOCAHOTRO = soCaHoTro;
                    lichChuyenGiaoChiTiet.CANBOS = canBos;
                    lichChuyenGiaoChiTiet.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    lichChuyenGiaoChiTiet.NGAYSUA = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Save(ORenderInfo, lichChuyenGiaoChiTiet.ID, lichChuyenGiaoChiTiet);
                }
                RetAjaxOut.HtmlContent = DrawLichChuyenGiaoChiTiets(ORenderInfo, lichChuyenGiaoId).HtmlContent;
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
        public static AjaxOut DeleteLichChuyenGiaoChiTiet(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string lichChuyenGiaoChiTietId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Delete(ORenderInfo, lichChuyenGiaoChiTietId);
                RetAjaxOut.HtmlContent = DrawLichChuyenGiaoChiTiets(ORenderInfo, lichChuyenGiaoId).HtmlContent;
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
        public static AjaxOut SaveKetQuaChuyenGiao(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string soQD, string ngayQD, string tuNgay, string denNgay, int? soLuotBenhNhan, int? soCaHuongDan, int? soCaHoTro, int? soGioThamGia, string chapHanhThoiGian, string chapHanhQuyChe, string phoiHop,
            int? htNhiemVu, string deXuatThoiGian, string deXuatCheDo, string deXuatDieuKien, string thoiGianBaoCao, string nxTinhThanThaiDoYThuc, int? nxKhaNangThucHienDocLap, int? nxDungYcDeXuat, int? nxMucDoHtNhiemVu, string deXuatGiaiPhap, string noiNhanXet, string ngayNhanXet, string nguoiNhanXet)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaChuyenGiaoCls ketQuaChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
                if (ketQuaChuyenGiao == null)
                {
                    ketQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls();
                    ketQuaChuyenGiao.ID = lichChuyenGiaoId;
                    ketQuaChuyenGiao.SOQD = soQD;
                    ketQuaChuyenGiao.NGAYQD = string.IsNullOrEmpty(ngayQD) ? null : (DateTime?)DateTime.ParseExact(ngayQD, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.TUNGAY = string.IsNullOrEmpty(tuNgay) ? null : (DateTime?)DateTime.ParseExact(tuNgay, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.DENNGAY = string.IsNullOrEmpty(denNgay) ? null : (DateTime?)DateTime.ParseExact(denNgay, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.SOLUOTBENHNHAN = soLuotBenhNhan;
                    ketQuaChuyenGiao.SOCAHUONGDAN = soCaHuongDan;
                    ketQuaChuyenGiao.SOCAHOTRO = soCaHoTro;
                    ketQuaChuyenGiao.SOGIOTHAMGIA = soGioThamGia;
                    ketQuaChuyenGiao.CHAPHANHTHOIGIAN = chapHanhThoiGian;
                    ketQuaChuyenGiao.CHAPHANHQUYCHE = chapHanhQuyChe;
                    ketQuaChuyenGiao.PHOIHOP = phoiHop;
                    ketQuaChuyenGiao.HTNHIEMVU = htNhiemVu;
                    ketQuaChuyenGiao.DEXUATTHOIGIAN = deXuatThoiGian;
                    ketQuaChuyenGiao.DEXUATCHEDO = deXuatCheDo;
                    ketQuaChuyenGiao.DEXUATDIEUKIEN = deXuatDieuKien;
                    ketQuaChuyenGiao.THOIGIANBAOCAO = string.IsNullOrEmpty(thoiGianBaoCao) ? null : (DateTime?)DateTime.ParseExact(thoiGianBaoCao, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC = nxTinhThanThaiDoYThuc;
                    ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP = nxKhaNangThucHienDocLap;
                    ketQuaChuyenGiao.NXDUNGYCDEXUAT = nxDungYcDeXuat;
                    ketQuaChuyenGiao.NXMUCDOHTNHIEMVU = nxMucDoHtNhiemVu;
                    ketQuaChuyenGiao.DEXUATGIAIPHAP = deXuatGiaiPhap;
                    ketQuaChuyenGiao.NOINHANXET = noiNhanXet;
                    ketQuaChuyenGiao.NGAYNHANXET = string.IsNullOrEmpty(ngayNhanXet) ? null : (DateTime?)DateTime.ParseExact(ngayNhanXet, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.NGUOINHANXET = nguoiNhanXet;
                    ketQuaChuyenGiao.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    ketQuaChuyenGiao.NGAYTAO = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Add(ORenderInfo, ketQuaChuyenGiao);
                }
                else
                {
                    ketQuaChuyenGiao.SOQD = soQD;
                    ketQuaChuyenGiao.NGAYQD = string.IsNullOrEmpty(ngayQD) ? null : (DateTime?)DateTime.ParseExact(ngayQD, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.TUNGAY = string.IsNullOrEmpty(tuNgay) ? null : (DateTime?)DateTime.ParseExact(tuNgay, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.DENNGAY = string.IsNullOrEmpty(denNgay) ? null : (DateTime?)DateTime.ParseExact(denNgay, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.SOLUOTBENHNHAN = soLuotBenhNhan;
                    ketQuaChuyenGiao.SOCAHUONGDAN = soCaHuongDan;
                    ketQuaChuyenGiao.SOCAHOTRO = soCaHoTro;
                    ketQuaChuyenGiao.SOGIOTHAMGIA = soGioThamGia;
                    ketQuaChuyenGiao.CHAPHANHTHOIGIAN = chapHanhThoiGian;
                    ketQuaChuyenGiao.CHAPHANHQUYCHE = chapHanhQuyChe;
                    ketQuaChuyenGiao.PHOIHOP = phoiHop;
                    ketQuaChuyenGiao.HTNHIEMVU = htNhiemVu;
                    ketQuaChuyenGiao.DEXUATTHOIGIAN = deXuatThoiGian;
                    ketQuaChuyenGiao.DEXUATCHEDO = deXuatCheDo;
                    ketQuaChuyenGiao.DEXUATDIEUKIEN = deXuatDieuKien;
                    ketQuaChuyenGiao.THOIGIANBAOCAO = string.IsNullOrEmpty(thoiGianBaoCao) ? null : (DateTime?)DateTime.ParseExact(thoiGianBaoCao, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC = nxTinhThanThaiDoYThuc;
                    ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP = nxKhaNangThucHienDocLap;
                    ketQuaChuyenGiao.NXDUNGYCDEXUAT = nxDungYcDeXuat;
                    ketQuaChuyenGiao.NXMUCDOHTNHIEMVU = nxMucDoHtNhiemVu;
                    ketQuaChuyenGiao.DEXUATGIAIPHAP = deXuatGiaiPhap;
                    ketQuaChuyenGiao.NOINHANXET = noiNhanXet;
                    ketQuaChuyenGiao.NGAYNHANXET = string.IsNullOrEmpty(ngayNhanXet) ? null : (DateTime?)DateTime.ParseExact(ngayNhanXet, "dd/MM/yyyy", null);
                    ketQuaChuyenGiao.NGUOINHANXET = nguoiNhanXet;
                    ketQuaChuyenGiao.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    ketQuaChuyenGiao.NGAYSUA = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Save(ORenderInfo, ketQuaChuyenGiao.ID, ketQuaChuyenGiao);
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
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Lịch chuyển giao có id là") + " " + lichChuyenGiaoId + " " + WebLanguage.GetLanguage(OSiteParam, "đã bị xóa bởi người dùng khác.");
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut UploadTaiLieu(RenderInfoCls ORenderInfo, string lichChuyenGiaoId, string fileName, string ghiChu, byte[] ndTep)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string tenTep = System.Guid.NewGuid().ToString() + fileName.Substring(fileName.LastIndexOf('.'));
                string fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedDaoTaoFilePath"]), tenTep);
                File.WriteAllBytes(fileSavePath, ndTep);
                DT_TaiLieuChuyenGiaoCls taiLieu = new DT_TaiLieuChuyenGiaoCls
                {
                    ID = System.Guid.NewGuid().ToString(),
                    LICHCHUYENGIAO_ID = lichChuyenGiaoId,
                    TENTEP = tenTep,
                    TENHIENTHI = fileName,
                    GHICHU = ghiChu,
                    NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId,
                    NGAYTAO = DateTime.Now
                };
                CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Add(ORenderInfo, taiLieu);
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
                DT_TaiLieuChuyenGiaoCls taiLieu = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().CreateModel(ORenderInfo, taiLieuId);
                if (File.Exists(taiLieu.DUONGDAN))
                    File.Delete(taiLieu.DUONGDAN);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Delete(ORenderInfo, taiLieu.ID);
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
        #region CustomActionControls       
        #endregion

    }
}
