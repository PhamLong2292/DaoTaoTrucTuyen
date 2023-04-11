using Newtonsoft.Json;
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
               // string ThemMoiDeCuongIdId = WebEnvironments.Request("id");
                //ThemMoiDeCuongCls ThemMoiDeCuongId = string.IsNullOrEmpty(ThemMoiDeCuongIdId) ? new ThemMoiDeCuongCls() : CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);            
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
                    "       <div class='col-lg-12'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin đăng ký đề tài") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id='divChitietPhieu'> \r\n" +                   
                                            ThongTinDangKy(ORenderInfo, null, null).HtmlContent +               
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
                    "   var ThemMoiDeCuongIdId='';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "     $('#divPrintButton').html(DrawPrintButton());\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
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
                    "    AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongIdId).value;\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "   ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
                    "   if(ThemMoiDeCuongIdId == '')\r\n" +
                    "   {\r\n" +
                    "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +        
                    "       return true;\r\n" +
                    "   }\r\n" +
                    "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                    "        {\r\n" +
                    "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                    "            return true;\r\n" +
                    "        }\r\n" +
                    "   return ThemMoiDeCuongIdId;\r\n" +  
                    "}\r\n" +
                #endregion 
                #region Show trạng thái 
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.ThemMoiDeCuong.ShowTrangThai(RenderInfo, ThemMoiDeCuongIdId).value);\r\n" +        
                    "}\r\n" +
                //Fill giá trị theo biểu thức vào trường mã 
                    "   function txtMa_onkeydown(value){\r\n" +
                    "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Disbale draw
                    "function CheckTrangThai(){\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
                    "    AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.CheckTrangThai(RenderInfo, ThemMoiDeCuongIdId).value;\r\n" +
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
                    "       }\r\n" +
                    "       else \r\n" +
                    "       {\r\n" +
                    "           $(\"#divChitietPhieu :input\").prop(\"disabled\", false);\r\n" +
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
                    "   CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "}\r\n" +
                #endregion                   
                #region Thu hồi
                //    "   function ThuHoi(){\r\n" +
                //    "       swal({ \r\n" +
                //    "               title: \"Bạn có chắc chắn thu hồi phiếu này?\", \r\n" +
                //    "               type: \"warning\", \r\n" +
                //    "               showCancelButton: true, \r\n" +
                //    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                //    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                //    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                //    "               closeOnConfirm: true, \r\n" +
                //    "               closeOnCancel: true\r\n" +
                //    "           }, function () { \r\n" +
                //    "               RenderInfo=CreateRenderInfo();\r\n" +
                //    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongIdId, " + (int)ThemMoiDeCuongCls.eTrangThai.Moi + ").value;\r\n" +
                //    "               if(AjaxOut.Error)\r\n" +
                //    "               {\r\n" +
                //    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                //    "                   return;\r\n" +
                //    "               }\r\n" +
                //    "               else \r\n" +
                //    "               {\r\n" +
                //    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi!!") + "');\r\n" +
                //    "                   $('#btnThuHoi').hide();\r\n" +
                //    "                   $('#btnLuu').show();\r\n" +
                //    "                   $('#btnHoanTat').show();\r\n" +      
                //    "                   AddHistory(ThemMoiDeCuongIdId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                //    "                   ShowTrangThai();\r\n" +
                //    "                   CheckTrangThai();\r\n" +
                //    "               }\r\n" +
                //    "       }); \r\n" +
                //    "   }\r\n" +
                #endregion
                #region HoanTat
                //    "   function HoanTat(){\r\n" +              
                //    "       SaveThongTin();\r\n" +
                //    "       RenderInfo=CreateRenderInfo();\r\n" +
                //    "       AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.UpdateTrangThai(RenderInfo, ThemMoiDeCuongIdId, " + (int)ThemMoiDeCuongCls.eTrangThai.HoanTat + ").value;\r\n" +
                //    "       if(AjaxOut.Error)\r\n" +
                //    "       {\r\n" +
                //    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                //    "           return;\r\n" +
                //    "       }\r\n" +
                //    "       else \r\n" +
                //    "       {\r\n" +               
                //    "           $('#btnLuu').hide();\r\n" +
                //    "           $('#btnHoanTat').hide();\r\n" +
                //    "           $('#btnThuHoi').show();\r\n" +
                //    "           toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã hoàn tất!") + "');\r\n" +
                //    "           AddHistory(ThemMoiDeCuongIdId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                //    "           ShowTrangThai();\r\n" +
                //    "           CheckTrangThai();\r\n" +
                //    "       }\r\n" + 
                //    "   }\r\n" +
                #endregion
                #region Save Thông tin
                    "function SaveThongTin()\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
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
                    "     AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.SaveThongTin(RenderInfo, ThemMoiDeCuongIdId, MaSoPhieu, HinhThuc_ID, NgayBaoCao, DoiTuongXayRaSuCo, MaBenhNhan, HoTen, NgaySinh, gioiTinh, ChuyenKhoa, NoiXayRaSuCo, ThoiGianXayRaSuCo, ViTriXayRaSuCo, ViTriCuThe, MoTaNgan, DeXuatGiaiPhap, DieuTriXuLiBanDau, ThongBaoBacSi, TBChoNguoiNha, GhiNhanHoSo, TBChoNguoiBenh, PhanLoaiBDSuCo, DanhGiaBanDau, HoTenNguoiBaoCao, SDT, Email, DoiTuongBaoCao, GhiChuDieuDuong, GhiChuBaSi, GhiChuKhac, NguoiChungKien1, NguoiChungKien2).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdThemMoiDeCuongIdId').value = ThemMoiDeCuongIdId = AjaxOut.RetExtraParam1;\r\n" +         
                    "           AddHistory(ThemMoiDeCuongIdId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(ThemMoiDeCuongIdId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "}\r\n" +
                #endregion 
                #region Xóa Thông tin đăng ký
                    "function DeleteThongTin()\r\n" +
                    "{\r\n" +
                    "   ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.DeleteThongTin(RenderInfo, ThemMoiDeCuongIdId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(ThemMoiDeCuongIdId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion 
                #region Bắt sự kiện thay đổi textbox mã số
                    "   function txtMa_onchange(sender){\r\n" +
                    "       ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
                    "       masophieu = sender.value;\r\n" +
                    "       if(ThemMoiDeCuongIdId == '' && masophieu != '')\r\n" +//Nếu đăng ký chưa được tạo thì load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.ThongTinDangKy(RenderInfo, masophieu, ThemMoiDeCuongIdId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divChitietPhieu').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "           }\r\n" +
                    "           ShowInput();\r\n" +
                    "           CheckTrangThai();\r\n" +
                    "           CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "       }\r\n" +                   
                    "   }\r\n" +
                #endregion 
                #region Điền thông tin học viên theo mã học viên được chọn trên danh sách học viên gợi ý 
                    "   function FillHocVienInfo(masophieu){\r\n" +
                    "       ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
                    "       if(masophieu != '')\r\n" +//Load động thông tin học viên theo mã được nhập
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.ThongTinDangKy(RenderInfo, masophieu, ThemMoiDeCuongIdId).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               $('#divChitietPhieu').html(AjaxOut.HtmlContent);\r\n" +
                    "               $('.datepicker').datetimepicker({ \r\n" +
                    "                   format: 'DD/MM/YYYY' \r\n" +
                    "               }); \r\n" +
                    "               CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       $('#divFormModal').modal('hide');\r\n" +
                    "   }\r\n" +
                #endregion 
                #region Hiển thị popup danh sách theo 4 tiếu chí: Họ tên, ngày sinh, địa chỉ, giới tính
                    "   function ShowPopupThemMoiDeCuongIds(){\r\n" +
                    "       ThemMoiDeCuongIdId = document.getElementById('hdThemMoiDeCuongIdId').value;\r\n" +
                    "       hoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "       GioiTinh = document.getElementById('cbbGioiTinh').value;\r\n" +
                    "       ngaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "       if(ThemMoiDeCuongIdId == '' && hoTen != '' && ngaySinh != '')\r\n" +
                    "       {\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.ThemMoiDeCuong.PopupBenhNhans(RenderInfo, hoTen, gioiTinh, ngaySinh).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           if(AjaxOut.RetObject > 0)\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "               document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách gợi ý") + "</span>';\r\n" +
                    "               $('#divFormModal').modal('show');\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
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
        public static AjaxOut ThongTinDangKy(RenderInfoCls ORenderInfo, string MaSoPhieu, string ThemMoiDeCuongIdId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);             
                //ThemMoiDeCuongCls BCSuCoYKhoa = null;
                //if (!string.IsNullOrEmpty(ThemMoiDeCuongIdId))
                //    BCSuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);
                //if (!string.IsNullOrEmpty(MaSoPhieu))
                //    BCSuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, MaSoPhieu);
                //if (BCSuCoYKhoa == null)
                //{
                //    BCSuCoYKhoa = new ThemMoiDeCuongCls();
                //    if (string.IsNullOrEmpty(MaSoPhieu))
                //    {
                //        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                //        BCSuCoYKhoa.MASOPHIEU = boDemValueMaHV.RetExtraParam2;
                //    }
                //    else BCSuCoYKhoa.MASOPHIEU = MaSoPhieu;
                //}

                //string cbbChuyenKhoa = "";
                //if (!string.IsNullOrEmpty(BCSuCoYKhoa.KHOAPHONG_ID))
                //{
                //    var chuyenkhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), BCSuCoYKhoa.KHOAPHONG_ID);
                //    if (chuyenkhoa != null)
                //    {
                //        cbbChuyenKhoa += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenkhoa.Ma, chuyenkhoa.Ten);
                //    }
                //}
                string Html =
                "               <input id='hdThemMoiDeCuongIdId' type='hidden' value=''>\r\n" +
                "               <div class=\"row\" id='divThemMoiDeCuongId'>\r\n" +
                "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                       <div class=\"ibox-content col-md-12\">\r\n" +
                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-2\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Mã đề cương: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtMaDeCuong' type='text' style='z-index: 0;' value='' class='form-control valueForm' required>\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                <div class=\"col-md-6\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề tài: ") +
                "                                        <input id='txtTenDeTai' type='text' style='z-index: 0;' value='' class='form-control valueForm' >\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                        WebLanguage.GetLanguage(OSiteParam, "Chủ nhiệm đề tài: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <input id='txtChuNhiemDetai' type='text' style='z-index: 0;' value='' class='form-control valueForm' required >\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài: ") +
                "                                      <select id = 'cbbCapDeTai' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Cấp đề tài") + "'>\r\n" +
                "                                           <option value='' >Cơ sở</option>\r\n" +
                "                                           <option value='' >Trung Ương </option>\r\n" +
                "                                       </select>\r\n" +
                "                                   </div> \r\n"+
                "                                </div>\r\n" +            
                "                           </div>\r\n" +

                "                           <div class='row'>\r\n" +
                "                                <div class=\"col-md-8\">\r\n" +
                "                                    <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tên đề cương: ") +
                "                                        <input id='txtTenDeCuong' type='text' style='z-index: 0;' value='' class='form-control valueForm' >\r\n" +
                "                                    </div>\r\n" +
                "                                </div>\r\n" +
                "                                  <div class=\"col-md-2\">\r\n" +
                "                                      <div class=\"form-group\">\r\n" +
                                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian gửi đề cương:") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                          <input id='dtThoiGianDangKy' required type='text' style='z-index: 0;' value='' class='form-control valueForm datepicker'>\r\n" +
                "                                      </div>\r\n" +
                "                                  </div>\r\n" +          
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                       WebLanguage.GetLanguage(OSiteParam, "Người gửi: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                       <select id = 'cbbNguoiGui' class='form-control valueForm' title = '" + WebLanguage.GetLanguage(OSiteParam, "Người gửi") + "'>\r\n" +
                "                                       </select>\r\n" +
                "                                   </div> \r\n" +
                "                               </div> \r\n" +             
                "                           </div>\r\n" +


                "                           <div class='row' style ='margin-top: 20px;'>\r\n" +
                "                               <div class=\"col-md-6\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                                                         WebLanguage.GetLanguage(OSiteParam, "Tài liệu đính kèm: ") + "<span style = 'color:red' >(*)</span >\r\n" +
                "                                        <input id='txtTaiLieuDinhKem' type='text' style='z-index: 0;' value='' class='form-control valueForm' required>\r\n" +
                "                                   </div> \r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"col-md-2\">\r\n" +
                "                                   <div class=\"form-group\">\r\n" +
                "                                       <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\" style ='margin-top: 12px;'>\r\n" +
                "                                            <span class=\"btn btn-default btn-file\" ><span class=\"fileinput-new\">Chọn tập tin</span>\r\n" +
                "                                            <span class=\"fileinput-exists\">Chọn lại</span><input type=\"file\" name=\"...\" id=\"fileUpload\"/></span>\r\n" +
                "                                            <span class=\"fileinput-filename\"></span>\r\n" +
                "                                       <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                "                                       </div> \r\n" +
                "                                       <button type=\"button\" class=\"btn btn-sm btn-primary\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                "                                   </div> \r\n" +
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongIdId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                //ThemMoiDeCuongCls SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);
                //if (SuCoYKhoa != null)
                //    return ThemMoiDeCuongParser.sColorTrangThai[SuCoYKhoa.TRANGTHAI.Value];
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongIdId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                //ThemMoiDeCuongCls ThemMoiDeCuongId = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);
                //if (ThemMoiDeCuongId != null)
                //    RetAjaxOut.RetExtraParam1 = ThemMoiDeCuongId.TRANGTHAI.ToString();
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
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut GetBoDemValueAndMaHV(RenderInfoCls ORenderInfo)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    string boDemValue = null;
        //    string maHV = null;
        //    BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "ThemMoiDeCuong", FieldName = "MaSoPhieu" }).FirstOrDefault();
        //    if (boMa != null && !string.IsNullOrEmpty(boMa.BIEUTHUC))
        //    {
        //        while (maHV == null || CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, maHV) != null)
        //        {
        //            if (maHV != null)
        //            {
        //                var boDem = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().CreateModel(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC));
        //                //if (boDem != null)
        //                //{
        //                //    CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().UpdateStatus(ORenderInfo, boDem.ID, int.Parse(boDemValue), 1);
        //                //}
        //            }
        //            boDemValue = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoDemProcess().GetValue(ORenderInfo, Model.Common.GetMaBoDem(boMa.BIEUTHUC), Model.Common.GetValueFormat(boMa.BIEUTHUC));
        //            maHV = Model.Common.GetDisplayPart(boMa.BIEUTHUC) + boDemValue;
        //        }
        //    }
        //    RetAjaxOut.RetExtraParam1 = boDemValue;
        //    RetAjaxOut.RetExtraParam2 = maHV;
        //    return RetAjaxOut;
        //}   
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut SaveThongTin(RenderInfoCls ORenderInfo, string ThemMoiDeCuongIdId, string MaSoPhieu, int HinhThuc_ID, string NgayBaoCao, int DoiTuongXayRaSuCo, string MaBN, string HoTen, string Ngaysinh, int GioiTinh, string ChuyenKhoa, string NoiXayRaSuCo, string ThoiGianXayRaSuCo, string ViTriXayRaSuCo, string ViTriCuThe, string MoTaNgan, string DeXuatGiaiPhap, string DieuTriXuLiBanDau, int ThongBaoBacSi, int TBChoNguoiNha, int GhiNhanHoSo, int TBChoNguoiBenh, int PhanLoaiBDSuCo, int DanhGiaBanDau, string HoTenNguoiBaoCao, string SDT, string Email, int DoiTuongBaoCao, string GhiChuDieuDuong, string GhiChuBaSi, string GhiChuKhac, string NguoiChungKien1, string NguoiChungKien2)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSession.CheckSessionTimeOut(ORenderInfo);
        //        ThemMoiDeCuongCls SuCoYKhoa = new ThemMoiDeCuongCls();
        //        if (!string.IsNullOrEmpty(ThemMoiDeCuongIdId))
        //        {
        //            SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);
        //        }
        //        else
        //        {
        //            SuCoYKhoa = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, MaSoPhieu);
        //            if (SuCoYKhoa == null)
        //            {
        //                SuCoYKhoa = new ThemMoiDeCuongCls() { MASOPHIEU = MaSoPhieu };
        //            }
        //        }
        //        #region Thêm mới/cập nhật phiếu
        //        if (string.IsNullOrEmpty(SuCoYKhoa.ID))
        //        {
        //            SuCoYKhoa.ID = ThemMoiDeCuongIdId = System.Guid.NewGuid().ToString();
        //            SuCoYKhoa.HINHTHUC_ID = HinhThuc_ID;
        //            SuCoYKhoa.NGAYBAOCAO = string.IsNullOrWhiteSpace(NgayBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayBaoCao, "dd/MM/yyyy", null);
        //            SuCoYKhoa.DOITUONGSUCO = DoiTuongXayRaSuCo;
        //            SuCoYKhoa.MABN = MaBN;
        //            SuCoYKhoa.HOTEN = HoTen;
        //            SuCoYKhoa.NGAYSINH = string.IsNullOrWhiteSpace(Ngaysinh) ? null : (DateTime?)DateTime.ParseExact(Ngaysinh, "dd/MM/yyyy", null); 
        //            SuCoYKhoa.GIOITINH = GioiTinh;
        //            SuCoYKhoa.KHOAPHONG_ID = ChuyenKhoa;
        //            SuCoYKhoa.NOIXAYRA = NoiXayRaSuCo;
        //            SuCoYKhoa.THOIGIANXAYRA = string.IsNullOrWhiteSpace(ThoiGianXayRaSuCo) ? null : (DateTime?)DateTime.ParseExact(ThoiGianXayRaSuCo, "dd/MM/yyyy", null);
        //            SuCoYKhoa.VITRIXAYRA = ViTriXayRaSuCo;
        //            SuCoYKhoa.VITRICUTHE = ViTriCuThe;
        //            SuCoYKhoa.MOTASUCO = MoTaNgan;
        //            SuCoYKhoa.DEXUATGIAIPHAP = DeXuatGiaiPhap;
        //            SuCoYKhoa.DIEUTRIXULYDUOCTHUCHIEN = DieuTriXuLiBanDau;
        //            SuCoYKhoa.THONGBAOCHOBACSI = ThongBaoBacSi;
        //            SuCoYKhoa.THONGBAOCHONGUOINHA = TBChoNguoiNha;
        //            SuCoYKhoa.GHINHANVAOHOSO = GhiNhanHoSo;
        //            SuCoYKhoa.THONGBAOCHONGUOIBENH = TBChoNguoiBenh;
        //            SuCoYKhoa.PHANLOAISUCO = PhanLoaiBDSuCo;
        //            SuCoYKhoa.MUCDOANHHUONG = DanhGiaBanDau;
        //            SuCoYKhoa.HOTENNGUOIBAOCAO = HoTenNguoiBaoCao;
        //            SuCoYKhoa.SODIENTHOAI = SDT;
        //            SuCoYKhoa.EMAIL = Email;
        //            SuCoYKhoa.DOITUONGBAOCAO = DoiTuongBaoCao;
        //            SuCoYKhoa.GHICHUDIEUDUONG = GhiChuDieuDuong;
        //            SuCoYKhoa.GHICHUBACSI = GhiChuBaSi;
        //            SuCoYKhoa.GHICHUKHAC = GhiChuKhac;
        //            SuCoYKhoa.NGUOICHUNGKIEN1 = NguoiChungKien1;
        //            SuCoYKhoa.NGUOICHUNGKIEN2 = NguoiChungKien2;
        //            SuCoYKhoa.TRANGTHAI = (int)ThemMoiDeCuongCls.eTrangThai.Moi;           
        //            SuCoYKhoa.NGUOILAP_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
        //            CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().Add(ORenderInfo, SuCoYKhoa);
        //            RetAjaxOut.RetExtraParam1 = SuCoYKhoa.ID;
        //        }
        //        else
        //        {
        //            SuCoYKhoa.HINHTHUC_ID = HinhThuc_ID;
        //            SuCoYKhoa.NGAYBAOCAO = string.IsNullOrWhiteSpace(NgayBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayBaoCao, "dd/MM/yyyy", null);
        //            SuCoYKhoa.DOITUONGSUCO = DoiTuongXayRaSuCo;
        //            SuCoYKhoa.MABN = MaBN;
        //            SuCoYKhoa.HOTEN = HoTen;
        //            SuCoYKhoa.NGAYSINH = string.IsNullOrWhiteSpace(Ngaysinh) ? null : (DateTime?)DateTime.ParseExact(Ngaysinh, "dd/MM/yyyy", null);
        //            SuCoYKhoa.GIOITINH = GioiTinh;
        //            SuCoYKhoa.KHOAPHONG_ID = ChuyenKhoa;
        //            SuCoYKhoa.NOIXAYRA = NoiXayRaSuCo;
        //            SuCoYKhoa.THOIGIANXAYRA = string.IsNullOrWhiteSpace(ThoiGianXayRaSuCo) ? null : (DateTime?)DateTime.ParseExact(ThoiGianXayRaSuCo, "dd/MM/yyyy", null);
        //            SuCoYKhoa.VITRIXAYRA = ViTriXayRaSuCo;
        //            SuCoYKhoa.VITRICUTHE = ViTriCuThe;
        //            SuCoYKhoa.MOTASUCO = MoTaNgan;
        //            SuCoYKhoa.DEXUATGIAIPHAP = DeXuatGiaiPhap;
        //            SuCoYKhoa.DIEUTRIXULYDUOCTHUCHIEN = DieuTriXuLiBanDau;
        //            SuCoYKhoa.THONGBAOCHOBACSI = ThongBaoBacSi;
        //            SuCoYKhoa.THONGBAOCHONGUOINHA = TBChoNguoiNha;
        //            SuCoYKhoa.GHINHANVAOHOSO = GhiNhanHoSo;
        //            SuCoYKhoa.THONGBAOCHONGUOIBENH = TBChoNguoiBenh;
        //            SuCoYKhoa.PHANLOAISUCO = PhanLoaiBDSuCo;
        //            SuCoYKhoa.MUCDOANHHUONG = DanhGiaBanDau;
        //            SuCoYKhoa.HOTENNGUOIBAOCAO = HoTenNguoiBaoCao;
        //            SuCoYKhoa.SODIENTHOAI = SDT;
        //            SuCoYKhoa.EMAIL = Email;
        //            SuCoYKhoa.DOITUONGBAOCAO = DoiTuongBaoCao;
        //            SuCoYKhoa.GHICHUDIEUDUONG = GhiChuDieuDuong;
        //            SuCoYKhoa.GHICHUBACSI = GhiChuBaSi;
        //            SuCoYKhoa.GHICHUKHAC = GhiChuKhac;
        //            SuCoYKhoa.NGUOICHUNGKIEN1 = NguoiChungKien1;
        //            SuCoYKhoa.NGUOICHUNGKIEN2 = NguoiChungKien2;
        //            SuCoYKhoa.TRANGTHAI = (int)ThemMoiDeCuongCls.eTrangThai.Moi;
        //            CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().Save(ORenderInfo, SuCoYKhoa.ID, SuCoYKhoa);
        //        }
        //        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
        //        #endregion Thêm mới/cập nhật đăng ký
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string ThemMoiDeCuongIdId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSession.CheckSessionTimeOut(ORenderInfo);
        //        CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().Delete(ORenderInfo, ThemMoiDeCuongIdId);
        //        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");              
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string ThemMoiDeCuongIdId, int trangThai)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSession.CheckSessionTimeOut(ORenderInfo);
        //        ThemMoiDeCuongCls ThemMoiDeCuongId = CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().CreateModel(ORenderInfo, ThemMoiDeCuongIdId);
        //        if (ThemMoiDeCuongId == null)
        //        {
        //            RetAjaxOut.Error = true;
        //            RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phiếu này đã bị xóa bởi người dùng khác.");
        //            return RetAjaxOut;
        //        }
        //        ThemMoiDeCuongId.TRANGTHAI = trangThai;
        //        CallBussinessUtility.CreateBussinessProcess().CreateThemMoiDeCuongProcess().Save(ORenderInfo, ThemMoiDeCuongId.ID, ThemMoiDeCuongId);
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}
        #endregion    
    }
}
