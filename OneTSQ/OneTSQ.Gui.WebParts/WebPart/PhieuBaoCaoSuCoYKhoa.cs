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
    public class PhieuBaoCaoSuCoYKhoa : ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "PhieuBaoCaoSuCoYKhoa";
        public override string WebPartTitle { get { return "Phiếu báo cáo sự cố y khoa"; } }
        public override string Description { get { return "Phiếu báo cáo sự cố y khoa"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { StaticWebPartId, PhieuBaoCaoSuCoYKhoa.StaticWebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PhieuBaoCaoSuCoYKhoa), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string BaoCaoSuCoId = WebEnvironments.Request("id");
                PhieuBaoCaoSuCoYKhoaCls BaoCaoSuCo = string.IsNullOrEmpty(BaoCaoSuCoId) ? new PhieuBaoCaoSuCoYKhoaCls() : CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);            
                #region Html
                string html =
                    "<form action='javascript:SaveThongTin();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:Clear();' style='float:left; margin-left: 20px;'>\r\n" +
                    "           <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteThongTin();' style='float:left; margin-left: 20px;" + (!string.IsNullOrEmpty(BaoCaoSuCoId) ? null : "display:none;") + "'>\r\n" +
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
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Chi tiết phiếu báo cáo sự cố ý khoa") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divChitietPhieu'> \r\n" +                   
                                            ChiTietPhieuBaoCao(ORenderInfo, null, BaoCaoSuCoId).HtmlContent +               
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
                    "   var BaoCaoSuCoId='" + BaoCaoSuCoId + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({\r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n" +
                    "        format: 'DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     });\r\n" +
                    "     $('#dtThoiGianXayRaSuCo').datetimepicker({ \r\n" +
                    "        locale: 'vi',\r\n" +
                    "        useCurrent: false,\r\n"+
                    "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                    "        maxDate: new Date() \r\n" +
                    "     }); \r\n" +
                    "     CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "     CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "     ShowTrangThai();\r\n" +                 
                    "     CheckTrangThai();\r\n" +
                    "   });\r\n" +

                #region Truyền id đăng ký cho nút print
                    "function OnWebPartLoad(reportID)\r\n" +
                    "{\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.CheckTrangThai(RenderInfo, BaoCaoSuCoId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   BaoCaoSuCoId = document.getElementById('hdBaoCaoSuCoId').value;\r\n" +
                    "   if(BaoCaoSuCoId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return BaoCaoSuCoId;\r\n" +  
                    "}\r\n" +
                #endregion 

                #region Show trạng thái 
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.ShowTrangThai(RenderInfo, BaoCaoSuCoId).value);\r\n" +        
                    "}\r\n" +            
                    "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #endregion

                #region hiển thị giao diện theo trạng thái
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    BaoCaoSuCoId = document.getElementById('hdBaoCaoSuCoId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.CheckTrangThai(RenderInfo, BaoCaoSuCoId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                    "       {\r\n" +
                    "           $(\"#divChitietPhieu :input\").prop(\"disabled\", true);\r\n" +
                    "           $('#btnThuHoi').show();\r\n" +
                    "           $('#btnLuu').hide();\r\n" +
                    "           $('#btnHoanTat').hide();\r\n" +
                    "           $('#btnXoa').hide();\r\n" +
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divChitietPhieu :input\").prop(\"disabled\", false);\r\n" +
                    "           $('#btnThuHoi').hide();\r\n" +
                    "           $('#btnLuu').show();\r\n" +
                    "           $('#btnHoanTat').show();\r\n" +    
                    "       }\r\n" +
                    "       ShowInput();\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.UpdateTrangThai(RenderInfo, BaoCaoSuCoId, " + (int)PhieuBaoCaoSuCoYKhoaCls.eTrangThai.Moi + ").value;\r\n" +
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
                    "                   AddHistory(BaoCaoSuCoId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   CheckTrangThai();\r\n" +
                    "                   ShowInput();\r\n" +
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
                    "     NgayBaoCao = document.getElementById('dtNgayBaoCao').value;\r\n" +
                    "     if(NgayBaoCao == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày báo cáo không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     MaBenhNhan = document.getElementById('txtMaBN').value;\r\n" +
                    "     if(MaBenhNhan == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường mã bệnh nhân không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     HoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "     if(HoTen == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường họ và tên không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     NgaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "     if(NgaySinh == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày, tháng, năm sinh không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     ChuyenKhoa = document.getElementById('cbbChuyenKhoa').value;\r\n" +
                    "     if(ChuyenKhoa == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường chuyên khoa không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     NoiXayRaSuCo = document.getElementById('txtNoiXayRaSuCo').value;\r\n" +
                    "     if(NoiXayRaSuCo == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường nơi xảy ra sự cố không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     ThoiGianXayRaSuCo = document.getElementById('dtThoiGianXayRaSuCo').value;\r\n" +
                    "     if(ThoiGianXayRaSuCo == '')\r\n" +
                    "     {\r\n" +
                    "         callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường thời gian xảy ra sự cố không được bỏ trống!") + "');\r\n" +
                    "         return true;\r\n" +
                    "     }\r\n" +
                    "     SaveThongTin();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.UpdateTrangThai(RenderInfo, BaoCaoSuCoId, " + (int)PhieuBaoCaoSuCoYKhoaCls.eTrangThai.HoanTat + ").value;\r\n" +
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
                    "           AddHistory(BaoCaoSuCoId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                    "           ShowTrangThai();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           ShowInput();\r\n" +
                    "       }\r\n" + 
                    "   }\r\n" +
                #endregion

                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     BaoCaoSuCoId = document.getElementById('hdBaoCaoSuCoId').value;\r\n" +
                    "     MaSoPhieu = document.getElementById('txtMaSoPhieu').value;\r\n" +
                    "     HinhThuc_ID = document.getElementById('cbbHinhThuc').value;\r\n" +
                    "     NgayBaoCao = document.getElementById('dtNgayBaoCao').value;\r\n" +
                    "     DoiTuongXayRaSuCo = document.getElementById('cbbDoiTuongXayRaSuCo').value;\r\n" +
                    "     MaBenhNhan = document.getElementById('txtMaBN').value;\r\n" +       
                    "     HoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "     NgaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "     gioiTinh = document.getElementById('cbbGioiTinh').value;\r\n" +
                    "     ChuyenKhoa = document.getElementById('cbbChuyenKhoa').value;\r\n" +
                    "     NoiXayRaSuCo = document.getElementById('txtNoiXayRaSuCo').value;\r\n" +
                    "     ThoiGianXayRaSuCo = document.getElementById('dtThoiGianXayRaSuCo').value;\r\n" +
                    "     ViTriXayRaSuCo = document.getElementById('cbbViTriXayRaSuCo').value;\r\n" +
                    "     ViTriCuThe = document.getElementById('txtViTriCuThe').value;\r\n" +
                    "     MoTaNgan = document.getElementById('txtMoTaNgan').value;\r\n" +
                    "     DeXuatGiaiPhap = document.getElementById('txtDeXuatGiaiPhap').value;\r\n" +
                    "     DieuTriXuLiBanDau = document.getElementById('txtXuLiBanDau').value;\r\n" +
                    "     ThongBaoBacSi = $('#rdTBBacSiDT input:radio:checked').val();\r\n" +
                    "     TBChoNguoiNha = $('#rdTBChoNguoiNha input:radio:checked').val();\r\n" +
                    "     GhiNhanHoSo = $('#rdGhiNhanHoSo input:radio:checked').val();\r\n" +
                    "     TBChoNguoiBenh = $('#rdTBChoNguoiBenh input:radio:checked').val();\r\n" +
                    "     PhanLoaiBDSuCo = $('#rdPhanLoaiBDSuCo input:radio:checked').val();\r\n" +
                    "     DanhGiaBanDau = $('#rdDanhGiaBanDau input:radio:checked').val();\r\n" +
                    "     HoTenNguoiBaoCao = document.getElementById('txtHoTenNguoiBaoCao').value;\r\n" +
                    "     SDT = document.getElementById('txtSDT').value;\r\n" +
                    "     Email = document.getElementById('txtEmail').value;\r\n" +
                    "     DoiTuongBaoCao = $('#rdDoiTuongBaoCao input:radio:checked').val();\r\n" +
                    "     GhiChuDieuDuong = document.getElementById('txtDDChucDanh').value;\r\n" +
                    "     GhiChuBaSi = document.getElementById('txtDDBacSi').value;\r\n" +
                    "     GhiChuKhac = document.getElementById('txtGhiChuKhac').value;\r\n" +
                    "     NguoiChungKien1 = document.getElementById('txtNguoiChungKien1').value;\r\n" +
                    "     NguoiChungKien2 = document.getElementById('txtNguoiChungKien2').value;\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.SaveThongTin(RenderInfo, BaoCaoSuCoId, MaSoPhieu, HinhThuc_ID, NgayBaoCao, DoiTuongXayRaSuCo, MaBenhNhan, HoTen, NgaySinh, gioiTinh, ChuyenKhoa, NoiXayRaSuCo, ThoiGianXayRaSuCo, ViTriXayRaSuCo, ViTriCuThe, MoTaNgan, DeXuatGiaiPhap, DieuTriXuLiBanDau, ThongBaoBacSi, TBChoNguoiNha, GhiNhanHoSo, TBChoNguoiBenh, PhanLoaiBDSuCo, DanhGiaBanDau, HoTenNguoiBaoCao, SDT, Email, DoiTuongBaoCao, GhiChuDieuDuong, GhiChuBaSi, GhiChuKhac, NguoiChungKien1, NguoiChungKien2).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdBaoCaoSuCoId').value = BaoCaoSuCoId = AjaxOut.RetExtraParam1;\r\n" +
                    "           $('#btnXoa').show();\r\n" +
                    "           AddHistory(BaoCaoSuCoId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(BaoCaoSuCoId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 

                #region Xóa Thông tin phiếu
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   BaoCaoSuCoId = document.getElementById('hdBaoCaoSuCoId').value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.DeleteThongTin(RenderInfo, BaoCaoSuCoId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(BaoCaoSuCoId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
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

                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       BaoCaoSuCoId = document.getElementById('hdBaoCaoSuCoId').value;\r\n" +
                    "       masophieu = sender.value;\r\n" +
                    "       if(BaoCaoSuCoId == '' && masophieu != '')\r\n" +//Nếu đăng ký chưa được tạo thì load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.ChiTietPhieuBaoCao(RenderInfo, masophieu, BaoCaoSuCoId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divChitietPhieu').html(AjaxOut.HtmlContent);\r\n" +    
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
                    "           ShowInput();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "       }\r\n" +                   
                    "   }\r\n" +
                #endregion

                #region Bắt sự kiện thay đổi textbox mã bn
                    "   function txtMaBN_onchange(sender){\r\n" +             
                    "       mabn = sender.value;\r\n" +
                    "       if(mabn != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.PhieuBaoCaoSuCoYKhoa.OnChangeMaBN(RenderInfo, mabn).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('txtMaBN').value = '';\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion 

                #region Show input
                    "   function ShowInput(){\r\n" +
                    "       DoiTuongBaoCao = $('#rdDoiTuongBaoCao input:radio:checked').val();\r\n"+
                    "       if( DoiTuongBaoCao == 1) \r\n" +
                    "           {\r\n" +
                    "               txtDDChucDanh.disabled=false;\r\n" +
                    "               txtDDBacSi.disabled=true;\r\n" +
                    "               txtGhiChuKhac.disabled=true;\r\n" +
                    "               document.getElementById('txtGhiChuKhac').value = '';\r\n" +
                    "               document.getElementById('txtDDBacSi').value = '';\r\n" +
                    "           }\r\n" +
                    "       else if( DoiTuongBaoCao == 2) \r\n" +
                    "           {\r\n" +
                    "               txtDDChucDanh.disabled=true;\r\n" +
                    "               txtDDBacSi.disabled=false;\r\n" +
                    "               txtGhiChuKhac.disabled=true;\r\n" +
                    "               document.getElementById('txtDDChucDanh').value = '';\r\n" +
                    "               document.getElementById('txtGhiChuKhac').value = '';\r\n" +
                    "           }\r\n" +
                    "       else if( DoiTuongBaoCao == 5) \r\n" +
                    "           {\r\n" +
                    "               txtDDChucDanh.disabled=true;\r\n" +
                    "               txtDDBacSi.disabled=true;\r\n" +
                    "               txtGhiChuKhac.disabled=false;\r\n" +
                    "               document.getElementById('txtDDChucDanh').value = '';\r\n" +
                    "               document.getElementById('txtDDBacSi').value = '';\r\n" +
                    "           }\r\n" +
                    "       else\r\n" +
                    "           {\r\n" +
                    "               txtDDChucDanh.disabled=true;\r\n" +
                    "               txtDDBacSi.disabled=true;\r\n" +
                    "               txtGhiChuKhac.disabled=true;\r\n" +
                    "               document.getElementById('txtGhiChuKhac').value = '';\r\n" +
                    "               document.getElementById('txtDDBacSi').value = '';\r\n" +
                    "               document.getElementById('txtDDChucDanh').value = '';\r\n" +
                    "           }\r\n" +
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
        public static AjaxOut ChiTietPhieuBaoCao(RenderInfoCls ORenderInfo, string MaSoPhieu, string BaoCaoSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);             
                PhieuBaoCaoSuCoYKhoaCls BCSuCoYKhoa = null;
                if (!string.IsNullOrEmpty(BaoCaoSuCoId))
                    BCSuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);
                if (!string.IsNullOrEmpty(MaSoPhieu))
                    BCSuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, MaSoPhieu);
                if (BCSuCoYKhoa == null)
                {
                    BCSuCoYKhoa = new PhieuBaoCaoSuCoYKhoaCls();
                    if (string.IsNullOrEmpty(MaSoPhieu))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        BCSuCoYKhoa.MASOPHIEU = boDemValueMaHV.RetExtraParam2;
                    }
                    else BCSuCoYKhoa.MASOPHIEU = MaSoPhieu;
                }

                string cbbChuyenKhoa = "";
                if (!string.IsNullOrEmpty(BCSuCoYKhoa.KHOAPHONG_ID))
                {
                    var chuyenkhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), BCSuCoYKhoa.KHOAPHONG_ID);
                    if (chuyenkhoa != null)
                    {
                        cbbChuyenKhoa += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenkhoa.Ma, chuyenkhoa.Ten);
                    }
                }

                string cbbDoiTuongXayRaSuCo = "<select class='form-control' id='cbbDoiTuongXayRaSuCo' style='font-size: 14px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Đối tượng xảy ra sự cố") + "'>\r\n";
                foreach (var ldt in PhieuBaoCaoSuCoYKhoaParser.DoiTuongs)
                    cbbDoiTuongXayRaSuCo += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, BCSuCoYKhoa.DOITUONGSUCO == ldt.Key ? "selected" : null, ldt.Value);
                cbbDoiTuongXayRaSuCo += "</select>\r\n";

                string cbbHinhThuc = "<select class='form-control' id='cbbHinhThuc' style='font-size: 14px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Hình thức") + "'>\r\n";
                foreach (var ldt in PhieuBaoCaoSuCoYKhoaParser.HinhThucs)
                    cbbHinhThuc += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, BCSuCoYKhoa.HINHTHUC_ID == ldt.Key ? "selected" : null, ldt.Value);
                cbbHinhThuc += "</select>\r\n";

                string Html =
                "               <input id='hdBaoCaoSuCoId' type='hidden' value='" + BCSuCoYKhoa.ID + "'>\r\n" +
                "               <div class=\"row\" id='divBaoCaoSuCo'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "1. THÔNG TIN CHUNG") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Mã số: ") + "<span style = 'color:red' > *</span >\r\n" +
                "                                        <input id='txtMaSoPhieu' type='text' style='z-index: 0;' value='"+ BCSuCoYKhoa.MASOPHIEU +"' onchange='txtMa_onchange(this)'  class='form-control valueForm' required " + (string.IsNullOrEmpty(BaoCaoSuCoId) ? null : "disabled=true") + ">\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Hình thức: ") + "<span style = 'color:red' > *</span >\r\n"+
                                                       cbbHinhThuc +
                "                                   </div> \r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Ngày báo cáo:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                        <input id='dtNgayBaoCao' type='text' style='z-index: 0;' class='form-control valueForm datepicker' value='" + (BCSuCoYKhoa.NGAYBAOCAO == null ? null : BCSuCoYKhoa.NGAYBAOCAO.Value.ToString("dd/MM/yyyy")) + "' required>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Đối tượng xảy ra sự cố: ") + "<span style = 'color:red' > *</span >\r\n" +
                                                       cbbDoiTuongXayRaSuCo +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +            
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Mã bệnh nhân: ") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='txtMaBN' type='text' style='z-index: 0;' onchange='txtMaBN_onchange(this)' value='" + BCSuCoYKhoa.MABN + "' class='form-control valueForm' required >\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Họ và tên:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='txtHoTen' type='text' style='z-index: 0;' value='" + BCSuCoYKhoa.HOTEN + "' class='form-control valueForm' required>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Ngày, tháng, năm sinh:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='dtNgaySinh' type='text' style='z-index: 0;' value='" + (BCSuCoYKhoa.NGAYSINH == null ? null : BCSuCoYKhoa.NGAYSINH.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker' required>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + "<span style = 'color:red' > *</span ><br>\r\n" +
                "                                       <select id = 'cbbGioiTinh' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + "'>\r\n" +
                "                                           <option " + (string.IsNullOrEmpty(BCSuCoYKhoa.ID) || BCSuCoYKhoa.GIOITINH == (int)PhieuBaoCaoSuCoYKhoaCls.eGioiTinh.Nam ? "checked" : null) + "  value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eGioiTinh.Nam + "'>Nam</option>\r\n" +
                "                                           <option " + (BCSuCoYKhoa.GIOITINH == (int)PhieuBaoCaoSuCoYKhoaCls.eGioiTinh.Nu ? "checked" : null) + "  value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eGioiTinh.Nu + "'>Nữ</option>\r\n" +
                "                                       </select>\r\n" +                
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <select id ='cbbChuyenKhoa' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "' required>\r\n" +
                                                            cbbChuyenKhoa + 
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +               
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                //Thông tin sự cố
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "2. THÔNG TIN SỰ CỐ") + "</h5>\r\n" +
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
                                                        WebLanguage.GetLanguage(OSiteParam, "Nơi xảy ra sự cố") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='txtNoiXayRaSuCo' type='text' style='z-index: 0;' value='" + BCSuCoYKhoa.NOIXAYRA + "' class='form-control valueForm' required >\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thời gian xảy ra sự cố:") + "<span style = 'color:red' > *</span >\r\n" +
                "                                       <input id='dtThoiGianXayRaSuCo' type='text' style='z-index: 0;' value='" + (BCSuCoYKhoa.THOIGIANXAYRA == null ? null : BCSuCoYKhoa.THOIGIANXAYRA.Value.ToString("HH:mm dd/MM/yyyy")) + "' class='form-control valueForm' required>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +                      
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Khoa/Phòng/Vị trí xảy ra sự cố:")+
                "                                       <input id = 'cbbViTriXayRaSuCo' class='form-control valueForm'  value='" + BCSuCoYKhoa.VITRIXAYRA + "' title = '" + WebLanguage.GetLanguage(OSiteParam, "Khoa/Phòng/Vị trí xảy ra sự cố") + "'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Vị trí cụ thể:") +
                "                                       <input id='txtViTriCuThe' type='text' style='z-index: 0;' value='" + BCSuCoYKhoa.VITRICUTHE + "' class='form-control valueForm'>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +             
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn gọn về sự cố:") +
                "                                       <textarea  id='txtMoTaNgan' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + BCSuCoYKhoa.MOTASUCO + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Đề xuất giải pháp ban đầu:") +
                "                                       <textarea  id='txtDeXuatGiaiPhap' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + BCSuCoYKhoa.DEXUATGIAIPHAP + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +             
                "                           <div class='row'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Điều trị/xử lí ban đầu đã được thực hiện:") +
                "                                       <textarea  id='txtXuLiBanDau' type='text' style='z-index: 0; height: 150px;' class='form-control valueForm'>" + BCSuCoYKhoa.DIEUTRIXULYDUOCTHUCHIEN + "</textarea>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row' style='margin-top: 20px;'>\r\n" +
                "                               <div class=\"col-md-3\" id=\"rdTBBacSiDT\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thông báo cho bác sĩ điều trị/Người có trách nhiệm:") + "<br>\r\n"+
                "                                       <label class=\"radio-inline\"><input  id = \"rdbsCo1\" type = \"radio\" name=\"optradioHl1\" " + (BCSuCoYKhoa.THONGBAOCHOBACSI == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co +"'>Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdbsKhong1\" type = \"radio\" name=\"optradioHl1\" " + (BCSuCoYKhoa.THONGBAOCHOBACSI == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong ? "checked" : null) + "  value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong + "'value=\"2\">Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdbsKhongGhiNhan1\" type = \"radio\" name=\"optradioHl1\" " + (BCSuCoYKhoa.THONGBAOCHOBACSI == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan + "'>Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\" id=\"rdTBChoNguoiNha\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thông báo cho người nhà/Người bảo hộ:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdbhCo2\" type = \"radio\" name=\"optradioHl2\" " + (BCSuCoYKhoa.THONGBAOCHONGUOINHA == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co + "'>Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdbhKhong2\" type = \"radio\" name=\"optradioHl2\" " + (BCSuCoYKhoa.THONGBAOCHONGUOINHA == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong + "'>Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdbhKhongGhiNhan2\" type = \"radio\" name=\"optradioHl2\" " + (BCSuCoYKhoa.THONGBAOCHONGUOINHA == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan + "'>Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\" id=\"rdGhiNhanHoSo\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Ghi nhận hồ sơ bệnh án/Giấy tờ liên quan:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqCo3\" type = \"radio\" name=\"optradioHl3\" " + (BCSuCoYKhoa.GHINHANVAOHOSO == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co + "'>Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhong3\" type = \"radio\" name=\"optradioHl3\" " + (BCSuCoYKhoa.GHINHANVAOHOSO == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong + "'>Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdlqKhongGhiNhan3\" type = \"radio\" name=\"optradioHl3\" " + (BCSuCoYKhoa.GHINHANVAOHOSO == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan + "'>Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +
                "                               <div class=\"col-md-3\" id=\"rdTBChoNguoiBenh\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Thông báo cho người bệnh:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdnbCo4\" type = \"radio\" name=\"optradioHl4\" " + (BCSuCoYKhoa.THONGBAOCHONGUOIBENH == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Co + "'>Có</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdnbKhong4\" type = \"radio\" name=\"optradioHl4\" " + (BCSuCoYKhoa.THONGBAOCHONGUOIBENH == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.Khong + "'>Không</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdnbKhongGhiNhan4\" type = \"radio\" name=\"optradioHl4\" " + (BCSuCoYKhoa.THONGBAOCHONGUOIBENH == (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eTraLoi.KhongGhiNhan + "'>Không ghi nhận</label> " +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                 "                           <div class='row' style='margin-top: 20px;'>\r\n" +
                "                               <div class=\"col-md-3\" id=\"rdPhanLoaiBDSuCo\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Phân loại ban đầu về sự cố:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rdplCo5\" type = \"radio\" name=\"optradioHl5\" " + (BCSuCoYKhoa.PHANLOAISUCO == (int)PhieuBaoCaoSuCoYKhoaCls.ePhanLoai.ChuaXayRa ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.ePhanLoai.ChuaXayRa + "'>Chưa xảy ra</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rdplKhong5\" type = \"radio\" name=\"optradioHl5\" " + (BCSuCoYKhoa.PHANLOAISUCO == (int)PhieuBaoCaoSuCoYKhoaCls.ePhanLoai.DaXayRa ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.ePhanLoai.DaXayRa + "'>Đã xảy ra</label> " +              
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-3\" id=\"rdDanhGiaBanDau\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Đánh giá ban đầu về mức độ ảnh hưởng của sự cố:") + "<br>\r\n" +
                "                                       <label class=\"radio-inline\"><input  id = \"rddgCo6\" type = \"radio\" name=\"optradioHl6\" " + (BCSuCoYKhoa.MUCDOANHHUONG == (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.Nang ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.Nang + "'>Nặng</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rddgKhong6\" type = \"radio\" name=\"optradioHl6\" " + (BCSuCoYKhoa.MUCDOANHHUONG == (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.TrungBinh ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.TrungBinh + "'>Trung bình</label> " +
                "                                       <label class=\"radio-inline\"><input  id = \"rddgKhongGhiNhan6\" type = \"radio\" name=\"optradioHl6\" " + (BCSuCoYKhoa.MUCDOANHHUONG == (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.Nhe ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDanhGia.Nhe + "'>Nhẹ</label> " +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                //Thông tin người báo cáo
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-title\">\r\n" +
                "                           <h5>" + WebLanguage.GetLanguage(OSiteParam, "3. THÔNG TIN NGƯỜI BÁO CÁO(ẨN DANH)") + "</h5>\r\n" +
                "                           <div class=\"ibox-tools\">\r\n" +
                "                               <a class=\"collapse-link\">\r\n" +
                "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
                "                               </a>\r\n" +
                "                           </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"ibox-content col-md-12\" id ='divthongtinnguoibao'>\r\n" +
                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-3\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Họ tên: ") +
                "                                        <input id='txtHoTenNguoiBaoCao' type='text' style='z-index: 0;' value='" + BCSuCoYKhoa.HOTENNGUOIBAOCAO + "' class='form-control valueForm'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-3\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "SĐT: ") +
                "                                        <input id='txtSDT' type='text' style='z-index: 0;' onkeypress='CheckCurrency(event);' maxlength='10' value='" + BCSuCoYKhoa.SODIENTHOAI + "' class='form-control valueForm'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-3\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Email: ") +
                "                                        <input id='txtEmail' type='text' style='z-index: 0;' value='" + BCSuCoYKhoa.EMAIL + "' class='form-control valueForm'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row' id ='rdDoiTuongBaoCao'>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <label style='font-weight: bold;'>" + WebLanguage.GetLanguage(OSiteParam, "Đối tượng báo cáo:") + "</label><br>\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class='row'>\r\n" +
                "                                               <div class=\"col-md-3\">\r\n" +
                "                                                   <label class=\"radio-inline\"><input  id = \"rdDDChucDanh\" type = \"radio\" onclick='ShowInput();' name=\"optradioHl7\" " + (BCSuCoYKhoa.DOITUONGBAOCAO == (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.DieuDuong ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.DieuDuong +"'>Điều dưỡng(Chức danh):</label> " +
                "                                               </div> \r\n" +
                "                                               <div class=\"col-md-6\">\r\n" +
                "                                                   <input id='txtDDChucDanh' type='text' value='" + BCSuCoYKhoa.GHICHUDIEUDUONG + "' class='form-control valueForm' disabled></input>\r\n" +
                "                                               </div> \r\n" +
                "                                           </div> \r\n" +
                "                                       </div> \r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class='row'>\r\n" +
                "                                               <div class=\"col-md-3\">\r\n" +
                "                                                   <label class=\"radio-inline\"><input id = \"rdDDBacSi\" type = \"radio\" onclick='ShowInput();' name=\"optradioHl7\" " + (BCSuCoYKhoa.DOITUONGBAOCAO == (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.BacSi ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.BacSi + "'>Bác sĩ(Chức danh):</label> " +
                "                                               </div> \r\n" +
                "                                               <div class=\"col-md-6\">\r\n" +
                "                                                   <input id='txtDDBacSi' type='text' value='" + BCSuCoYKhoa.GHICHUBACSI + "' class='form-control valueForm' disabled></input>\r\n" +
                "                                               </div> \r\n" +
                "                                           </div> \r\n" +              
                "                                       </div> \r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-12\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class='row' style='margin-top:10px;'>\r\n" +
                "                                               <div class=\"col-md-3\">\r\n" +
                "                                                   <label class=\"radio-inline\"><input  id = \"rdNguoiBenh\" type = \"radio\" onclick='ShowInput();' name=\"optradioHl7\" " + (BCSuCoYKhoa.DOITUONGBAOCAO == (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.NguoiBenh ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.NguoiBenh + "'>Người bệnh</label> " +
                "                                               </div> \r\n" +
                "                                               <div class=\"col-md-3\">\r\n" +
                "                                                   <label class=\"radio-inline\"><input  id = \"rdNguoiNhaKhac\" type = \"radio\" onclick='ShowInput();' name=\"optradioHl7\" " + (BCSuCoYKhoa.DOITUONGBAOCAO == (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.NguoiNha ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.NguoiNha + "'>Người nhà/khác đến thăm</label> " +
                "                                               </div> \r\n" +
                "                                           </div> \r\n" +                   
                "                                       </div> \r\n" +
                "                                       <div class=\"col-md-6\">\r\n" +
                "                                           <div class='row' style='margin-top:10px;'>\r\n" +
                "                                               <div class=\"col-md-3\">\r\n" +
                "                                                   <label class=\"radio-inline\"><input id = \"rdGhiChuKhac\" type = \"radio\" onclick='ShowInput();' name=\"optradioHl7\" " + (BCSuCoYKhoa.DOITUONGBAOCAO == (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.Khac ? "checked" : null) + " value='" + (int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.Khac + "'>Khác(Ghi cụ thể):</label> " +
                "                                               </div> \r\n" +
                "                                               <div class=\"col-md-6\">\r\n" +
                "                                                   <input id='txtGhiChuKhac' type='text' value='" + BCSuCoYKhoa.GHICHUKHAC + "' class='form-control valueForm' disabled></input>\r\n" +
                "                                               </div> \r\n" +
                "                                           </div> \r\n" +              
                "                                       </div> \r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class='row' style='margin-bottom: 50px;'>\r\n" +
                "                                <div class=\"col-md-4\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Người chứng kiến 1: ") +
                "                                        <input id='txtNguoiChungKien1' type='text' value='" + BCSuCoYKhoa.NGUOICHUNGKIEN1 + "' class='form-control valueForm'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-4\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Người chứng kiến 2: ") +
                "                                        <input id='txtNguoiChungKien2' type='text' value='" + BCSuCoYKhoa.NGUOICHUNGKIEN2 + "' class='form-control valueForm'>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string BaoCaoSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoSuCoYKhoaCls SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);
                if (SuCoYKhoa != null)
                    return PhieuBaoCaoSuCoYKhoaParser.sColorTrangThai[SuCoYKhoa.TRANGTHAI.Value];
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string BaoCaoSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoSuCoYKhoaCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);
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
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "PhieuBaoCaoSuCoYKhoa", FieldName = "MaSoPhieu" }).FirstOrDefault();
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
        public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string BaoCaoSuCoId, string MaSoPhieu, int HinhThuc_ID, string NgayBaoCao, int DoiTuongXayRaSuCo, string MaBN, string HoTen, string Ngaysinh, int GioiTinh, string ChuyenKhoa, string NoiXayRaSuCo, string ThoiGianXayRaSuCo, string ViTriXayRaSuCo, string ViTriCuThe, string MoTaNgan, string DeXuatGiaiPhap, string DieuTriXuLiBanDau, int ThongBaoBacSi, int TBChoNguoiNha, int GhiNhanHoSo, int TBChoNguoiBenh, int PhanLoaiBDSuCo, int DanhGiaBanDau, string HoTenNguoiBaoCao, string SDT, string Email, int DoiTuongBaoCao, string GhiChuDieuDuong, string GhiChuBaSi, string GhiChuKhac, string NguoiChungKien1, string NguoiChungKien2)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoSuCoYKhoaCls SuCoYKhoa = new PhieuBaoCaoSuCoYKhoaCls();
                if (!string.IsNullOrEmpty(BaoCaoSuCoId))
                {
                    SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);
                }
                else
                {
                    SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, MaSoPhieu);
                    if (SuCoYKhoa == null)
                    {
                        SuCoYKhoa = new PhieuBaoCaoSuCoYKhoaCls() { MASOPHIEU = MaSoPhieu };
                    }
                }
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(SuCoYKhoa.ID))
                {
                    SuCoYKhoa.ID = BaoCaoSuCoId = System.Guid.NewGuid().ToString();
                    SuCoYKhoa.HINHTHUC_ID = HinhThuc_ID;
                    SuCoYKhoa.NGAYBAOCAO = string.IsNullOrWhiteSpace(NgayBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayBaoCao, "dd/MM/yyyy", null);
                    SuCoYKhoa.DOITUONGSUCO = DoiTuongXayRaSuCo;
                    SuCoYKhoa.MABN = MaBN;
                    SuCoYKhoa.HOTEN = HoTen;
                    SuCoYKhoa.NGAYSINH = string.IsNullOrWhiteSpace(Ngaysinh) ? null : (DateTime?)DateTime.ParseExact(Ngaysinh, "dd/MM/yyyy", null); 
                    SuCoYKhoa.GIOITINH = GioiTinh;
                    SuCoYKhoa.KHOAPHONG_ID = ChuyenKhoa;
                    SuCoYKhoa.NOIXAYRA = NoiXayRaSuCo;
                    SuCoYKhoa.THOIGIANXAYRA = string.IsNullOrWhiteSpace(ThoiGianXayRaSuCo) ? null : (DateTime?)DateTime.ParseExact(ThoiGianXayRaSuCo, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                    SuCoYKhoa.VITRIXAYRA = ViTriXayRaSuCo;
                    SuCoYKhoa.VITRICUTHE = ViTriCuThe;
                    SuCoYKhoa.MOTASUCO = MoTaNgan;
                    SuCoYKhoa.DEXUATGIAIPHAP = DeXuatGiaiPhap;
                    SuCoYKhoa.DIEUTRIXULYDUOCTHUCHIEN = DieuTriXuLiBanDau;
                    SuCoYKhoa.THONGBAOCHOBACSI = ThongBaoBacSi;
                    SuCoYKhoa.THONGBAOCHONGUOINHA = TBChoNguoiNha;
                    SuCoYKhoa.GHINHANVAOHOSO = GhiNhanHoSo;
                    SuCoYKhoa.THONGBAOCHONGUOIBENH = TBChoNguoiBenh;
                    SuCoYKhoa.PHANLOAISUCO = PhanLoaiBDSuCo;
                    SuCoYKhoa.MUCDOANHHUONG = DanhGiaBanDau;
                    SuCoYKhoa.HOTENNGUOIBAOCAO = HoTenNguoiBaoCao;
                    SuCoYKhoa.SODIENTHOAI = SDT;
                    SuCoYKhoa.EMAIL = Email;
                    SuCoYKhoa.DOITUONGBAOCAO = DoiTuongBaoCao;
                    if (DoiTuongBaoCao == ((int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.DieuDuong))
                    {
                        SuCoYKhoa.GHICHUDIEUDUONG = GhiChuDieuDuong;
                    }
                    else
                    {
                        SuCoYKhoa.GHICHUDIEUDUONG = "";
                    }
                    if (DoiTuongBaoCao == ((int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.BacSi))
                    {
                        SuCoYKhoa.GHICHUBACSI = GhiChuBaSi;
                    }
                    else
                    {
                        SuCoYKhoa.GHICHUBACSI = "";
                    }
                    if (DoiTuongBaoCao == ((int)PhieuBaoCaoSuCoYKhoaCls.eDoiTuongBC.Khac))
                    {
                        SuCoYKhoa.GHICHUKHAC = GhiChuKhac;
                    }
                    else
                    {
                        SuCoYKhoa.GHICHUKHAC = "";
                    }              
                    SuCoYKhoa.NGUOICHUNGKIEN1 = NguoiChungKien1;
                    SuCoYKhoa.NGUOICHUNGKIEN2 = NguoiChungKien2;
                    SuCoYKhoa.TRANGTHAI = (int)PhieuBaoCaoSuCoYKhoaCls.eTrangThai.Moi;           
                    SuCoYKhoa.NGUOILAP_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Add(ORenderInfo, SuCoYKhoa);
                    RetAjaxOut.RetExtraParam1 = SuCoYKhoa.ID;
                }
                else
                {
                    SuCoYKhoa.HINHTHUC_ID = HinhThuc_ID;
                    SuCoYKhoa.NGAYBAOCAO = string.IsNullOrWhiteSpace(NgayBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayBaoCao, "dd/MM/yyyy", null);
                    SuCoYKhoa.DOITUONGSUCO = DoiTuongXayRaSuCo;
                    SuCoYKhoa.MABN = MaBN;
                    SuCoYKhoa.HOTEN = HoTen;
                    SuCoYKhoa.NGAYSINH = string.IsNullOrWhiteSpace(Ngaysinh) ? null : (DateTime?)DateTime.ParseExact(Ngaysinh, "dd/MM/yyyy", null);
                    SuCoYKhoa.GIOITINH = GioiTinh;
                    SuCoYKhoa.KHOAPHONG_ID = ChuyenKhoa;
                    SuCoYKhoa.NOIXAYRA = NoiXayRaSuCo;
                    SuCoYKhoa.THOIGIANXAYRA = string.IsNullOrWhiteSpace(ThoiGianXayRaSuCo) ? null : (DateTime?)DateTime.ParseExact(ThoiGianXayRaSuCo, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                    SuCoYKhoa.VITRIXAYRA = ViTriXayRaSuCo;
                    SuCoYKhoa.VITRICUTHE = ViTriCuThe;
                    SuCoYKhoa.MOTASUCO = MoTaNgan;
                    SuCoYKhoa.DEXUATGIAIPHAP = DeXuatGiaiPhap;
                    SuCoYKhoa.DIEUTRIXULYDUOCTHUCHIEN = DieuTriXuLiBanDau;
                    SuCoYKhoa.THONGBAOCHOBACSI = ThongBaoBacSi;
                    SuCoYKhoa.THONGBAOCHONGUOINHA = TBChoNguoiNha;
                    SuCoYKhoa.GHINHANVAOHOSO = GhiNhanHoSo;
                    SuCoYKhoa.THONGBAOCHONGUOIBENH = TBChoNguoiBenh;
                    SuCoYKhoa.PHANLOAISUCO = PhanLoaiBDSuCo;
                    SuCoYKhoa.MUCDOANHHUONG = DanhGiaBanDau;
                    SuCoYKhoa.HOTENNGUOIBAOCAO = HoTenNguoiBaoCao;
                    SuCoYKhoa.SODIENTHOAI = SDT;
                    SuCoYKhoa.EMAIL = Email;
                    SuCoYKhoa.DOITUONGBAOCAO = DoiTuongBaoCao;
                    SuCoYKhoa.GHICHUDIEUDUONG = GhiChuDieuDuong;
                    SuCoYKhoa.GHICHUBACSI = GhiChuBaSi;
                    SuCoYKhoa.GHICHUKHAC = GhiChuKhac;
                    SuCoYKhoa.NGUOICHUNGKIEN1 = NguoiChungKien1;
                    SuCoYKhoa.NGUOICHUNGKIEN2 = NguoiChungKien2;
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Save(ORenderInfo, SuCoYKhoa.ID, SuCoYKhoa);
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
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string BaoCaoSuCoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Delete(ORenderInfo, BaoCaoSuCoId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                string dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DanhSachBaoCaoSuCoYKhoa", new WebParamCls[] { });
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string BaoCaoSuCoId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoSuCoYKhoaCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, BaoCaoSuCoId);
                if (BaoCaoSuCo == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phiếu này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                BaoCaoSuCo.TRANGTHAI = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Save(ORenderInfo, BaoCaoSuCo.ID, BaoCaoSuCo);
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
        public static AjaxOut OnChangeMaBN(RenderInfoCls ORenderInfo, string MaBN)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoSuCoYKhoaCls BaoCaoSuCo = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ORenderInfo, MaBN);
                if (BaoCaoSuCo != null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã có phiếu tồn tại mã bệnh nhân này!");
                    return RetAjaxOut;
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
