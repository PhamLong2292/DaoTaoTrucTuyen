﻿using OneMES3.DM.Model;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.WebParts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OneTSQ.WebParts
{
    public class PhieuADR: ObjectWebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "PhieuADR";
        public override string WebPartTitle
        {
            get
            {
                return "Lập phiếu ADR";
            }
        }
        public override string Description
        {
            get
            {
                return "Lập phiếu ADR";
            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PhieuADR), Page);
        }
        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {           
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string maDonViTuVanDefault = WebConfig.GetWebConfig("HospitalCode");
                string PhieuADRId = WebEnvironments.Request("id");
                #region Html
                string html =
                    "<form    method=\"post\"  action='javascript:SavePhieuADR();'> \r\n" +
                    "    <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                    "       <div style='float:left; padding:0;' class='col-md-10'> \r\n" +
                    "           <input type='submit' id='btnLuu' title='Lưu' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 14px; '>\r\n" +
                    "           <input type='button' id='btnTai' title='Tải' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Tải") + "' onclick='javascript:TaiPhieu();'  style='float:left; margin-left: 14px;'>\r\n" +
                    "           <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteThongTin();' style='float:left; margin-left: 14px;" + (!string.IsNullOrEmpty(PhieuADRId) ? null : "display:none;") + "'>\r\n" +
                    "           <input type='button' id='btnThuHoi'  title='Thu hồi' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoi();'style='float:left; margin-left: 14px; '>\r\n" +
                    "           <input type='button' id='btnHoanTat' title='Hoàn tất' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Hoàn tất") + "' onclick='javascript:HoanTat();'style='float:left; margin-left: 14px;'>\r\n" +                 
                    "       </div> \r\n" +
                    "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0;' class='col-md-2' > \r\n" +
                    "       </div> \r\n" +
                    "   </div>\r\n" +
                    "   <div class='row'>\r\n" +
                    "       <div class='col-lg-12'>\r\n" +
                    "           <div class='ibox float-e-margins'>\r\n" +
                    "               <div class='ibox-title'>\r\n" +
                    "                   <h5>" + WebLanguage.GetLanguage(OSiteParam, "Chi tiết báo cáo phản ứng có hại của thuốc") + "</h5>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"ibox-content\" style=\"border: 0; \" id ='divPhieuADRChiTiet'>\r\n" +
                                        DrawPhieuADR(ORenderInfo, PhieuADRId, null).HtmlContent +
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
                #region JavaScript
                string js =
                "<script language=\"javascript\">\r\n" +
                "   RenderInfo=CreateRenderInfo();\r\n" +
                "   var PhieuADRId='" + PhieuADRId + "';\r\n" +
                "    $(document).ready(function() \r\n" +
                "       {\r\n" +
                "       $('#divPrintButton').html(DrawPrintButton());\r\n" +
                "       $('.datepicker').datetimepicker({\r\n" +
                "          locale: 'vi',\r\n" +
                "          useCurrent: false,\r\n" +
                "          format: 'DD/MM/YYYY',\r\n" +
                "          maxDate: new Date() \r\n" +
                "       });\r\n" +
                "       $('#btnHuy').hide()\r\n" +
                "       $('#btnThuHoi').hide()\r\n" +
                "       CallInitSelect2('cbbKhoaPhong', '" + WebEnvironments.GetRemoteProcessDataUrl(DepartmentService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo") + "');\r\n" +
                "       CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                "       CallInitSelect2('cbbChucVu', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucVuService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + "');\r\n" +
                "       ShowTrangThai();\r\n" +
                "       CheckTrangThai();\r\n" +
                "   });\r\n" +

                #region Checked
                "   $(\"input:checkbox\").on('click', function() {\r\n" +
                "       var $box = $(this);\r\n" +
                "       if ($box.is(\":checked\")) {\r\n" +
                "           var group = \"input:checkbox[name='\" + $box.attr(\"name\") + \"']\";\r\n" +
                "           $(group).prop(\"checked\", false);\r\n" +
                "           $box.prop(\"checked\", true);\r\n" +
                "       }else {\r\n" +
                "           $box.prop(\"checked\", false);\r\n" +
                "       }\r\n" +
                "   });\r\n" +
                #endregion

                #region Truyền id đăng ký cho nút print
                "function OnWebPartLoad(reportID)\r\n" +
                "{\r\n" +
                "   RenderInfo=CreateRenderInfo();\r\n" +
                "    AjaxOut = OneTSQ.WebParts.PhieuADR.CheckTrangThai(RenderInfo, PhieuADRId).value;\r\n" +
                "    if(AjaxOut.Error)\r\n" +
                "    {\r\n" +
                "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "        return;\r\n" +
                "    }\r\n" +
                "   PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
                "   if(PhieuADRId == '')\r\n" +
                "   {\r\n" +
                "       callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần lưu lại thông tin trước khi in phiếu.") + "');\r\n" +
                "       return true;\r\n" +
                "   }\r\n" +
                "        if(AjaxOut.RetExtraParam1 != 1)\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn cần hoàn tất phiếu trước khi in") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "   return PhieuADRId;\r\n" +
                "}\r\n" +
                #endregion

                #region Show trạng thái 
                "function ShowTrangThai(){\r\n" +
                "     RenderInfo=CreateRenderInfo();\r\n" +
                "     $('#aTrangThai').html(OneTSQ.WebParts.PhieuADR.ShowTrangThai(RenderInfo, PhieuADRId).value);\r\n" +
                "}\r\n" +
                "   function txtMa_onkeydown(value){\r\n" +
                "       if($('#txtMa').val() == '') $('#txtMa').val(value);\r\n" +
                "   }\r\n" +
                #endregion

                #region hiển thị giao diện theo trạng thái
                "function CheckTrangThai(){\r\n" +
                "    RenderInfo=CreateRenderInfo();\r\n"+
                "    PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n"+
                "    AjaxOut = OneTSQ.WebParts.PhieuADR.CheckTrangThai(RenderInfo, PhieuADRId).value;\r\n" +
                "    if(AjaxOut.Error)\r\n" +
                "    {\r\n" +
                "        callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "        return;\r\n" +
                "    }\r\n" +
                "       if(AjaxOut.RetExtraParam1 == 1)\r\n" +
                "       {\r\n" +
                "           $(\"#divPhieuADRChiTiet :input\").prop(\"disabled\", true);\r\n" +
                "           $('#btnThuHoi').show();\r\n" +
                "           $('#btnLuu').hide();\r\n" +
                "           $('#btnHoanTat').hide();\r\n" +
                "           $(\"#TabThuocADR\").css(\"pointer-events\",\"none\");\r\n" +
                "           $(\"#TabThuocDongThoi\").css(\"pointer-events\",\"none\");\r\n" +
                "           $('#btnXoa').hide();\r\n" +
                "       }\r\n" +
                "       else \r\n" +
                "       {\r\n" +
                "           $(\"#divPhieuADRChiTiet :input\").prop(\"disabled\", false);\r\n" +
                "           $('#btnThuHoi').hide();\r\n" +
                "           $('#btnLuu').show();\r\n" +
                "           $('#btnHoanTat').show();\r\n" +
                "           $(\"#TabThuocADR\").css(\"pointer-events\",\"auto\");\r\n" +
                "           $(\"#TabThuocDongThoi\").css(\"pointer-events\",\"auto\");\r\n" +
                "       }\r\n" +
                "}\r\n" +
                #endregion

                #region Hoàn tất
                "   function HoanTat(){\r\n" +
                "        SoBCDonVi = document.getElementById('txtSoBaoCaoCuaDonVi').value;\r\n" +
                "        if(SoBCDonVi == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường số báo cáo đơn vị không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        SoBCCuaTTQG = document.getElementById('txtSoBaoCaoCuaTTQG').value;\r\n" +
                "        if(SoBCCuaTTQG == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường số báo cáo của TT Quốc Gia không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        KhoaId = document.getElementById('cbbKhoaPhong').value;\r\n" +
                "        if(KhoaId == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường nơi báo cáo khoa không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        NguoiLapId = document.getElementById('cbbNguoiLap').value\r\n" +
                "        if(NguoiLapId == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường người lập không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        ChucVuId = document.getElementById('cbbChucVu').value\r\n" +
                "        if(ChucVuId == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường chức vụ không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        DienThoai = document.getElementById('txtDienThoai').value\r\n" +
                "        if(DienThoai == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường điện thoại không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        Email = document.getElementById('txtEmail').value\r\n" +
                "        if(Email == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường email không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        DangBaoCao = document.getElementById('cbbDangBaoCao').value\r\n" +
                "        if(DangBaoCao == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường dạng báo cáo không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        NgayLapBaoCao = document.getElementById('txtNgayLapBaoCao').value\r\n" +
                "        if(NgayLapBaoCao == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày lập báo cáo không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        MaBN = document.getElementById('txtMaBN').value\r\n" +
                "        if(MaBN == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường mã bệnh nhân không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        TenBN = document.getElementById('txtTenBN').value\r\n" +
                "        if(TenBN == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường tên bệnh nhân không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        NgaySinh = document.getElementById('txtNgaySinh').value\r\n" +
                "        if(NgaySinh == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày sinh không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        CanNang = document.getElementById('txtCanNang').value\r\n" +
                "        if(CanNang == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường cân nặng không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        NgayXuatHienPU = document.getElementById('txtNgayXuatHienPU').value\r\n" +
                "        if(NgayXuatHienPU == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường ngày xuất hiện phản ứng không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        PhanUngXuatHienSau = document.getElementById('txtPhanUngXuatHienSau').value\r\n" +
                "        if(PhanUngXuatHienSau == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường phản ứng xuất hiện sau bao lâu không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        MoTaBieuHieuADR = document.getElementById('txtMoTaBieuHienADR').value\r\n" +
                "        if(MoTaBieuHieuADR == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường mô tả biểu hiện ADR không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        XetNghiemLQPU = document.getElementById('txtXetNghiemLQPU').value\r\n" +
                "        if(XetNghiemLQPU == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường xét nghiệm liên quan tới phản ứng không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        TienSuBenhSu = document.getElementById('txtTienSuBenh').value\r\n" +
                "        if(TienSuBenhSu == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường tiền sử bệnh không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "        CachXuTri = document.getElementById('txtXuTri').value\r\n" +
                "        if(CachXuTri == '')\r\n" +
                "        {\r\n" +
                "            callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Trường cách xử trí không được bỏ trống!") + "');\r\n" +
                "            return true;\r\n" +
                "        }\r\n" +
                "       SavePhieuADR()\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.UpdateTrangThai(RenderInfo, PhieuADRId, " + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eTrangThai.HoanTat + ").value;\r\n" +
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
                "           AddHistory(PhieuADRId, '" + WebPartTitle + "', 'Hoàn tất', 'Tác vụ form');\r\n" +
                "           ShowTrangThai();\r\n" +
                "           CheckTrangThai();\r\n" +
                "       }\r\n" +
                "   }\r\n" +
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
                "               PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
                "               AjaxOut = OneTSQ.WebParts.PhieuADR.UpdateTrangThai(RenderInfo, PhieuADRId, " + (int)PhieuBaoCaoSuCoYKhoaCls.eTrangThai.Moi + ").value;\r\n" +
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
                "                   AddHistory(PhieuADRId, '" + WebPartTitle + "', 'Thu hồi', 'Tác vụ form');\r\n" +
                "                   ShowTrangThai();\r\n" +
                "                   CheckTrangThai();\r\n" +
                "               }\r\n" +
                "       }); \r\n" +
                "   }\r\n" +
                #endregion

                #region Lưu phiếu
                "   function SavePhieuADR()\r\n" +
                "   {\r\n" +
                "        RenderInfo=CreateRenderInfo();\r\n" +
                "        PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
                "        SoBCDonVi = document.getElementById('txtSoBaoCaoCuaDonVi').value;\r\n" +
                "        SoBCCuaTTQG = document.getElementById('txtSoBaoCaoCuaTTQG').value;\r\n" +
                "        KhoaId = document.getElementById('cbbKhoaPhong').value;\r\n" +
                "        NguoiLapId = document.getElementById('cbbNguoiLap').value\r\n" +
                "        ChucVuId = document.getElementById('cbbChucVu').value\r\n" +
                "        DienThoai = document.getElementById('txtDienThoai').value\r\n" +
                "        Email = document.getElementById('txtEmail').value\r\n" +
                "        DangBaoCao = document.getElementById('cbbDangBaoCao').value\r\n" +
                "        NgayLapBaoCao = document.getElementById('txtNgayLapBaoCao').value\r\n" +
                "        MaBN = document.getElementById('txtMaBN').value\r\n" +
                "        TenBN = document.getElementById('txtTenBN').value\r\n" +
                "        NgaySinh = document.getElementById('txtNgaySinh').value\r\n" +
                "        GioiTinh = document.getElementById('cbbGioiTinh').value\r\n" +
                "        CanNang = document.getElementById('txtCanNang').value\r\n" +
                "        NgayXuatHienPU = document.getElementById('txtNgayXuatHienPU').value\r\n" +
                "        PhanUngXuatHienSau = document.getElementById('txtPhanUngXuatHienSau').value\r\n" +
                "        MoTaBieuHieuADR = document.getElementById('txtMoTaBieuHienADR').value\r\n" +
                "        XetNghiemLQPU = document.getElementById('txtXetNghiemLQPU').value\r\n" +
                "        TienSuBenhSu = document.getElementById('txtTienSuBenh').value\r\n" +
                "        CachXuTri = document.getElementById('txtXuTri').value\r\n" +
                "        MucDoNghiemTrong = $('#cbkMucDoNTCuaPU input:checkbox:checked').val();\r\n" +
                "        KQXuTri = $('#cbkKqSauXuTri input:checkbox:checked').val();\r\n" +
                "        ThamDinhADR =  $('#cbkThuocVaADR input:checkbox:checked').val();\r\n" +
                "        ThangThamDinhADR =  $('#cbkThangThamDinhADR input:checkbox:checked').val();\r\n" +
                "        thangDinhADRKhac = document.getElementById('txtThangKhac').value\r\n" +
                "        thamDinhADRKhac = document.getElementById('txtKhac').value\r\n" +
                "        BinhLuanCBYTe = document.getElementById('txtBinhLuanCBYTe').value\r\n" +                
                "        AjaxOut = OneTSQ.WebParts.PhieuADR.SavePhieuADRADR(RenderInfo, PhieuADRId ,SoBCDonVi, SoBCCuaTTQG, KhoaId, ChucVuId, NguoiLapId, DienThoai, Email, DangBaoCao, NgayLapBaoCao, MaBN, TenBN, NgaySinh, GioiTinh, CanNang, NgayXuatHienPU, PhanUngXuatHienSau, MoTaBieuHieuADR, XetNghiemLQPU, TienSuBenhSu, CachXuTri, MucDoNghiemTrong, KQXuTri, ThamDinhADR, thamDinhADRKhac, ThangThamDinhADR, thangDinhADRKhac, BinhLuanCBYTe).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "         {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "         }\r\n" +
                "         if(AjaxOut.RetExtraParam1 != '')\r\n" +
                "         {\r\n" +
                "               document.getElementById('phieuBCPUCoHaiADRId').value = PhieuADRId = AjaxOut.RetExtraParam1;\r\n" +
                "               $('#btnXoa').show();\r\n" +
                "               AddHistory(PhieuADRId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                "         }\r\n" +
                "         else\r\n" +
                "               AddHistory(PhieuADRId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                "         toastr.info(AjaxOut.InfoMessage);\r\n" +     
                "   }\r\n" +
                #endregion

                #region Xóa Thông tin phiếu
                "function DeleteThongTin()\r\n" +
                "{\r\n" +
                "   PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
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
                "               AjaxOut = OneTSQ.WebParts.PhieuADR.DeleteThongTin(RenderInfo, PhieuADRId).value;\r\n" +
                "               if(AjaxOut.Error)\r\n" +
                "               {\r\n" +
                "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "                   return;\r\n" +
                "               }\r\n" +
                "               DeleteHistory(PhieuADRId);\r\n" +
                "               window.location.href = AjaxOut.RetUrl;\r\n" +
                "           }\r\n " +
                "       });\r\n" +
                "}\r\n" +
                #endregion

                #region Tải phiếu
                "   function TaiPhieu()\r\n" +
                "      {\r\n" +
                "         window.location.href = '" + WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, StaticWebPartId, new WebParamCls[] { }) + "';\r\n" +
                "      }\r\n" +
                #endregion

                #region Bắt sự kiện thay đổi textbox mã số
                "   function txtMa_onchange(sender){\r\n" +
                "       PhieuADRId = document.getElementById('phieuBCPUCoHaiADRId').value;\r\n" +
                "       sobaocao = sender.value;\r\n" +
                "       if(PhieuADRId == '' && sobaocao != '')\r\n" +
                "       {\r\n" +
                "           RenderInfo=CreateRenderInfo();\r\n" +
                "           AjaxOut = OneTSQ.WebParts.PhieuADR.DrawPhieuADR(RenderInfo, PhieuADRId, sobaocao).value;\r\n" +
                "           if(!AjaxOut.Error)\r\n" +
                "           {\r\n" +
                "               $('#divPhieuADRChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                "               $('.datepicker').datetimepicker({\r\n" +
                "                  locale: 'vi',\r\n" +
                "                  useCurrent: false,\r\n" +
                "                  format: 'DD/MM/YYYY',\r\n" +
                "                  maxDate: new Date() \r\n" +
                "               });\r\n" +
                "           }\r\n" +
                "           $(\"input:checkbox\").on('click', function() {\r\n" +
                "                var $box = $(this);\r\n" +
                "                if ($box.is(\":checked\")) {\r\n" +
                "                    var group = \"input:checkbox[name='\" + $box.attr(\"name\") + \"']\";\r\n" +
                "                    $(group).prop(\"checked\", false);\r\n" +
                "                    $box.prop(\"checked\", true);\r\n" +
                "                }else {\r\n" +
                "                    $box.prop(\"checked\", false);\r\n" +
                "                }\r\n" +
                "            });\r\n" +
                "           CheckTrangThai();\r\n" +
                "           CallInitSelect2('cbbKhoaPhong', '" + WebEnvironments.GetRemoteProcessDataUrl(DepartmentService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo") + "');\r\n" +
                "           CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                "           CallInitSelect2('cbbChucVu', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucVuService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức vụ") + "');\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                #endregion

                #region Bắt sự kiện thay đổi textbox mã bn
                "   function txtMaBN_onchange(sender){\r\n" +
                "       mabn = sender.value;\r\n" +
                "       if(mabn != '')\r\n" +
                "       {\r\n" +
                "           RenderInfo=CreateRenderInfo();\r\n" +
                "           AjaxOut = OneTSQ.WebParts.PhieuADR.OnChangeMaBN(RenderInfo, mabn).value;\r\n" +
                "           if(AjaxOut.Error)\r\n" +
                "           {\r\n" +
                "               document.getElementById('txtMaBN').value = '';\r\n" +
                "               callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "               return;\r\n" +
                "           }\r\n" +
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

                #region Thuốc ADR
                #region Hiển thị row thêm mới thuốc nghi ngờ ADR
                "   function ShowThuocADR(){\r\n" +
                "       $('.CssEditorItemThuocADR').hide();\r\n" +
                "       $('.CssEditorItemHang').hide();\r\n" +
                "       $('.CssDisplayItemThuocADR').show();\r\n" +
                "       $('#trAddThuocADR').show();\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.cbbThuocADR(RenderInfo, null, null).value;\r\n" +
                "       $('#divCbbThuocADR').html(AjaxOut);\r\n" +
                "       CallInitSelect2('cbbThuocADR', '" + WebEnvironments.GetRemoteProcessDataUrl(DmThuocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Thuốc") + "');\r\n" +   
                "       $('#dtNgayVao').datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "       $('#dtNgayRa').datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "   }\r\n" +
                #endregion

                #region Hiển thị row edit thuốc adr
                "   function ShowEditItemLineThuocADR(rowIndex)\r\n" +
                "   {\r\n" +
                "       $('.CssEditorItemThuocADR').hide();\r\n" +
                "       $('.CssDisplayItemThuocADR').show();\r\n" +
                "       $('#trAddThuocADR').hide();\r\n" +
                "       document.getElementById('divCbbThuocADR'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtDangBaoChe'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtNhaSanXuat'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtSoLoSX'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtLieuDung1Lan'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtSoLanDung'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtDuongDung'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('dtNgayVao'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('dtNgayRa'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtLyDo'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('cbbPhanUngCaiThien'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('cbbPhanUngXuatHien'+rowIndex).style.display='block';\r\n" +

                "       document.getElementById('spThuocADR'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spDangBaoChe'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spNhaSanXuat'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spSoLoSX'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spLieuDung1Lan'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spSoLanDung'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spDuongDung'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spNgayVao'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spNgayRa'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spLyDo'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spPhanUngCaiThien'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spPhanUngXuatHien'+rowIndex).style.display='none';\r\n" +

                "       document.getElementById('btnSaveThuocADR'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('btnEditThuocAdR'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('btnDeleteThuocADR'+rowIndex).style.display='none';\r\n" +

                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.cbbThuocADR(RenderInfo, rowIndex, $('#hdHangId'+rowIndex).val()).value;\r\n" +
                "       $('#divCbbThuocADR'+rowIndex).html(AjaxOut);\r\n" +    
                "       CallInitSelect2('cbbThuocADR'+rowIndex, '" + WebEnvironments.GetRemoteProcessDataUrl(DmThuocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Thuốc") + "');\r\n" +
           
                "       $('#dtNgayVao'+rowIndex).datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "       $('#dtNgayRa'+rowIndex).datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "   }\r\n" +
                #endregion

                #region Thêm mới/Cập nhật thuốc
                "   function SaveThuocADR(rowIndex){\r\n" +
                "       ThuocADRId = document.getElementById('hdThuocADRId'+rowIndex).value;\r\n" +
                "       Hang_ID = document.getElementById('cbbThuocADR'+rowIndex).value;\r\n" +
                "       DangBaoChe = document.getElementById('txtDangBaoChe'+rowIndex).value;\r\n" + 
                "       NhaSanXuat = document.getElementById('txtNhaSanXuat'+rowIndex).value;\r\n" +
                "       SoLoSX = document.getElementById('txtSoLoSX'+rowIndex).value;\r\n" +
                "       LieuDung1Lan = document.getElementById('txtLieuDung1Lan'+rowIndex).value;\r\n" +
                "       SoLanDung = document.getElementById('txtSoLanDung'+rowIndex).value;\r\n" +
                "       DuongDung = document.getElementById('txtDuongDung'+rowIndex).value;\r\n" +
                "       NgayVao = document.getElementById('dtNgayVao'+rowIndex).value;\r\n" +
                "       NgayRa = document.getElementById('dtNgayRa'+rowIndex).value;\r\n" +
                "       LyDo = document.getElementById('txtLyDo'+rowIndex).value;\r\n" +
                "       CoCaiThien = document.getElementById('cbbPhanUngCaiThien'+rowIndex).value;\r\n" +
                "       CoPhanUng = document.getElementById('cbbPhanUngXuatHien'+rowIndex).value;\r\n" +
                "       if(NgayVao=='')\r\n" +
                "       {\r\n" +
                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày vào.") + "');\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       if(NgayRa =='')\r\n" +
                "       {\r\n" +
                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày ra.") + "');\r\n" +
                "           return;\r\n" +
                "       }\r\n" +                  
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.SaveThuocADR(RenderInfo, ThuocADRId, Hang_ID, DangBaoChe, NhaSanXuat, SoLoSX, LieuDung1Lan, SoLanDung, DuongDung, NgayVao, NgayRa, LyDo, CoCaiThien, CoPhanUng).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       $('#TabThuocADR').html(AjaxOut.HtmlContent);\r\n" +
                "   }\r\n" +
                #endregion

                #region Xóa thuốc nghi ngờ ADR
                "   function DeleteThuocADR(rowIndex){\r\n" +
                "       ThuocADRId = document.getElementById('hdThuocADRId'+rowIndex).value;\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.DeleteThuocADR(RenderInfo, ThuocADRId).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       $('#TabThuocADR').html(AjaxOut.HtmlContent);\r\n" +
                "   }\r\n" +
                #endregion
                #endregion

                #region Thuốc Dùng Đồng Thời
                #region Hiển thị row thêm mới thuốc đông thời
                "   function ShowThuocDungDongThoi(){\r\n" +
                "       $('.CssEditorItemThuocDongThoi').hide();\r\n" +
                "       $('.CssEditorItemHang1').hide();\r\n" +
                "       $('.CssDisplayItemThuocDongThoi').show();\r\n" +
                "       $('#trAddThuocDongThoi').show();\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.cbbThuocADRDT(RenderInfo, null, null).value;\r\n" +
                "       $('#divCbbThuocADRDT').html(AjaxOut);\r\n" +
                "       CallInitSelect2('cbbThuocADRDT', '" + WebEnvironments.GetRemoteProcessDataUrl(DmThuocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Thuốc") + "');\r\n" +
                "       $('#dtNgayVao1').datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "       $('#dtNgayRa1').datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "   }\r\n" +
                #endregion

                #region Hiển thị row edit thuốc adr đồng thời
                "   function ShowEditItemLineThuocDongThoi(rowIndex)\r\n" +
                "   {\r\n" +
                "       $('.CssEditorItemThuocDongThoi').hide();\r\n" +
                "       $('.CssDisplayItemThuocDongThoi').show();\r\n" +
                "       $('#trAddThuocDongThoi').hide();\r\n" +
                "       document.getElementById('divCbbThuocADRDT'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('txtDangBaoChe1'+rowIndex).style.display='block';\r\n" +    
                "       document.getElementById('dtNgayVao1'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('dtNgayRa1'+rowIndex).style.display='block';\r\n" +      

                "       document.getElementById('spThuocADR1'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spDangBaoChe1'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spNgayVao1'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('spNgayRa1'+rowIndex).style.display='none';\r\n" +

                "       document.getElementById('btnSaveThuocDongThoi'+rowIndex).style.display='block';\r\n" +
                "       document.getElementById('btnEditThuocDongThoi'+rowIndex).style.display='none';\r\n" +
                "       document.getElementById('btnDeleteThuocDongThoi'+rowIndex).style.display='none';\r\n" +

                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.cbbThuocADRDT(RenderInfo, rowIndex, $('#hdHang1Id'+rowIndex).val()).value;\r\n" +
                "       $('#divCbbThuocADRDT'+rowIndex).html(AjaxOut);\r\n" +
                "       CallInitSelect2('cbbThuocADRDT'+rowIndex, '" + WebEnvironments.GetRemoteProcessDataUrl(DmThuocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Thuốc") + "');\r\n" +

                "       $('#dtNgayVao1'+rowIndex).datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "       $('#dtNgayRa1'+rowIndex).datetimepicker({ \r\n" +
                "        format: 'HH:mm DD/MM/YYYY',\r\n" +
                "       }); \r\n" +
                "   }\r\n" +
                #endregion

                #region Thêm mới/Cập nhật thuốc
                "   function SaveThuocDongThoi(rowIndex){\r\n" +
                "       ThuocADRId = document.getElementById('hdThuocADR1Id'+rowIndex).value;\r\n" +
                "       Hang_ID = document.getElementById('cbbThuocADRDT'+rowIndex).value;\r\n" +
                "       DangBaoChe = document.getElementById('txtDangBaoChe1'+rowIndex).value;\r\n" +                
                "       NgayVao = document.getElementById('dtNgayVao1'+rowIndex).value;\r\n" +
                "       NgayRa = document.getElementById('dtNgayRa1'+rowIndex).value;\r\n" +                  
                "       if(NgayVao=='')\r\n" +
                "       {\r\n" +
                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày vào.") + "');\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       if(NgayRa =='')\r\n" +
                "       {\r\n" +
                "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ngày ra.") + "');\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.SaveThuocDongThoi(RenderInfo, ThuocADRId, Hang_ID, DangBaoChe, NgayVao, NgayRa).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       $('#TabThuocDongThoi').html(AjaxOut.HtmlContent);\r\n" +
                "   }\r\n" +
                #endregion

                #region Xóa thuốc nghi ngờ ADR
                "   function DeleteThuocDongThoi(rowIndex){\r\n" +
                "       ThuocADRId = document.getElementById('hdThuocADR1Id'+rowIndex).value;\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OneTSQ.WebParts.PhieuADR.DeleteThuocDongThoi(RenderInfo, ThuocADRId).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       $('#TabThuocDongThoi').html(AjaxOut.HtmlContent);\r\n" +
                "   }\r\n" +
                #endregion
                #endregion             
                "</script>\r\n";
                #endregion
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
        public static AjaxOut DrawPhieuADR(RenderInfoCls ORenderInfo, string PhieuADRId, string SoBaoCao)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBCPUCoHaiADR = null;
                if (!string.IsNullOrEmpty(PhieuADRId))
                    OPhieuBCPUCoHaiADR = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
                if (!string.IsNullOrEmpty(SoBaoCao))
                    OPhieuBCPUCoHaiADR = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, SoBaoCao);
                if (OPhieuBCPUCoHaiADR == null)
                {
                    OPhieuBCPUCoHaiADR = new PhieuBaoCaoPhanUngCoHaiADRCls();
                    if (string.IsNullOrEmpty(SoBaoCao))
                    {
                        var boDemValueMaHV = GetBoDemValueAndMaHV(ORenderInfo);
                        OPhieuBCPUCoHaiADR.SoBcDonVi = boDemValueMaHV.RetExtraParam2;
                    }
                    else OPhieuBCPUCoHaiADR.SoBcDonVi = SoBaoCao;
                }
                List<ThuocADRCls> ThuocADRS = string.IsNullOrEmpty(OPhieuBCPUCoHaiADR.Id) ? new List<ThuocADRCls>() : CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Reading(ORenderInfo, new ThuocADRFilterCls() { Phieu_ID = OPhieuBCPUCoHaiADR.Id }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "PhieuADR_Thuocs", ThuocADRS);

                DepartmentCls OKhoaPhong = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateDepartmentProcess().CreateModel(ORenderInfo, OPhieuBCPUCoHaiADR.NoiBaoCao_Id);             
                string cbbKhoaPhong= "<select class='form-control' id='cbbKhoaPhong' style='font-size: 14px;' required>\r\n";
                if (OKhoaPhong != null)
                    cbbKhoaPhong += string.Format(" <option value ={0} selected>{0} - {1}</option>\r\n", OKhoaPhong.DepartmentCode, OKhoaPhong.DepartmentName);
                cbbKhoaPhong += "</select>\r\n";

                OwnerUserCls ONguoiLap = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, OPhieuBCPUCoHaiADR.NguoiLap_Id);
                string cbbNguoiLap = "<select class='form-control' id='cbbNguoiLap' style='font-size: 14px;' required>\r\n";
                if (ONguoiLap != null)
                    cbbNguoiLap += string.Format(" <option value={0} selected>{0} - {1}</option>\r\n", ONguoiLap.OwnerCode, ONguoiLap.OwnerName);
                cbbNguoiLap += "</select>\r\n";

                ChucVuCls OChucVu = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), OPhieuBCPUCoHaiADR.ChucVu_Id);
                string cbbChucVu = "<select class='form-control' id='cbbChucVu' style='font-size: 14px;' required>\r\n";
                if (OChucVu != null)
                    cbbChucVu += string.Format(" <option value = {0} selected>{0} - {1}</option>\r\n", OChucVu.Ma, OChucVu.Ten);
                cbbChucVu += "</select>\r\n";

                string cbbDangBaoCao = "<select class='form-control' id='cbbDangBaoCao' style='font-size: 14px;'  title = '" + WebLanguage.GetLanguage(OSiteParam, "Dạng báo cáo") + "'>\r\n";
                foreach (var ldt in PhieuBaoCaoPhanUngCoHaiADRCls.DangBaoCaos)
                    cbbDangBaoCao += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, OPhieuBCPUCoHaiADR.DangBaoCao == ldt.Key ? "selected" : null, ldt.Value);
                cbbDangBaoCao += "</select>\r\n";

                string Html =
                "               <input id='phieuBCPUCoHaiADRId' type='hidden' value='"+ OPhieuBCPUCoHaiADR.Id+ "'>\r\n" +
                "               <div class=\"row\">\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số báo cáo của đơn vị:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtSoBaoCaoCuaDonVi' required type='text' value='"+ OPhieuBCPUCoHaiADR.SoBcDonVi + "' onchange='txtMa_onchange(this)' class='form-control valueForm' " + (string.IsNullOrEmpty(PhieuADRId) ? null : "disabled=true") + ">\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Số báo cáo của TT Quốc Gia:") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtSoBaoCaoCuaTTQG' required type='text' value='" + OPhieuBCPUCoHaiADR.SoBCQuocGia + "' class='form-control valueForm  '>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo khoa ") + "<span style = 'color:red' > *</span >\r\n" +
                                            cbbKhoaPhong +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Người lập ") + "<span style = 'color:red' > *</span >\r\n" +
                                        cbbNguoiLap +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Chức vụ ") + "<span style = 'color:red' > *</span >\r\n" +
                                        cbbChucVu +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Điện thoại ") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtDienThoai' required type='text' onkeypress='CheckCurrency(event);' maxlength='10' value='" + OPhieuBCPUCoHaiADR.DienThoai + "' class='form-control valueForm  '>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Email") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtEmail' required type='text' value='" + OPhieuBCPUCoHaiADR.Email + "' class='form-control valueForm' style='z-index:0'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Dạng báo cáo") + "<span style = 'color:red' > *</span >\r\n" +
                                            cbbDangBaoCao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +

                "                   <div class=\"col-md-2\">\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Ngày lập báo cáo ") + "<span style = 'color:red' > *</span >\r\n" +
                "                           <input id='txtNgayLapBaoCao' required type='text' value='" + (OPhieuBCPUCoHaiADR.NgayLap == null ? null : OPhieuBCPUCoHaiADR.NgayLap.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker' style ='z-index:0'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "               </div>\r\n" +
                "               <div class=\"row\">\r\n" +
                                    DrawTTPhieuADR(ORenderInfo, OPhieuBCPUCoHaiADR.Id).HtmlContent +
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
        public static AjaxOut DrawTTPhieuADR(RenderInfoCls ORenderInfo, string PhieuADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBCPUCoHaiADR = string.IsNullOrEmpty(PhieuADRId) ? new PhieuBaoCaoPhanUngCoHaiADRCls() : CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
              
              string Html = 
              "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
              "                       <div class=\"ibox-title\">\r\n" +
              "                           <h5 style = \"color: black\">" + WebLanguage.GetLanguage(OSiteParam, "A. THÔNG TIN BỆNH NHÂN") + "</h5>\r\n" +
              "                           <div class=\"ibox-tools\">\r\n" +
              "                               <a class=\"collapse-link\">\r\n" +
              "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
              "                               </a>\r\n" +
              "                           </div>\r\n" +
              "                       </div>\r\n" +
              "                       <div class=\"ibox-content col-md-12\">\r\n" +
              "                           <div class='row'>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Mã BN: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtMaBN' type='text' style='z-index: 0;' onchange='txtMaBN_onchange(this)' value='" + OPhieuBCPUCoHaiADR.MaBN +"'  class='form-control valueForm' required>\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Họ và tên: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtTenBN' type='text' style='z-index: 0;' value='"+ OPhieuBCPUCoHaiADR.HoTen + "' class='form-control valueForm' required>\r\n" +
              "                                   </div> \r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Ngày sinh: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtNgaySinh' type='text' style='z-index: 0;' onblur=\"onNgaySinhValueChange(this)\" value='" + (OPhieuBCPUCoHaiADR.NgaySinh == null ? null : OPhieuBCPUCoHaiADR.NgaySinh.Value.ToString(" dd/MM/yyyy")) + "' class='form-control valueForm datepicker' required>\r\n" +
              "                                   </div> \r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Giới tính: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                        <select id='cbbGioiTinh' class=\"form-control valueForm\" >" +
              "                                           <option value='" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nam + "' " + (string.IsNullOrEmpty(OPhieuBCPUCoHaiADR.Id) || OPhieuBCPUCoHaiADR.GioiTinh == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nam ? "checked" : null) + ">Nam</option>" +
              "                                           <option value='" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nu + "' " + (OPhieuBCPUCoHaiADR.GioiTinh == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nu ? "checked" : null) + ">Nữ</option>" +
              "                                        </select>" +
              "                                   </div> \r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Cân nặng: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtCanNang' type='text' style='z-index: 0;' value='"+ OPhieuBCPUCoHaiADR.CanNang +"' class='form-control valueForm' required>\r\n" +
              "                                   </div> \r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +          
              "                      </div>\r\n" +
              "                   </div>\r\n" +

              "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
              "                       <div class=\"ibox-title\">\r\n" +
              "                           <h5 style = \"color: black\">" + WebLanguage.GetLanguage(OSiteParam, "B. THÔNG TIN VỀ PHẢN ỨNG CÓ HẠI (ADR)") + "</h5>\r\n" +
              "                           <div class=\"ibox-tools\">\r\n" +
              "                               <a class=\"collapse-link\">\r\n" +
              "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
              "                               </a>\r\n" +
              "                           </div>\r\n" +
              "                       </div>\r\n" +
              "                       <div class=\"ibox-content col-md-12\">\r\n" +
              "                           <div class='row'>\r\n" +
              "                               <div class=\"col-md-2\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Ngày xuất hiện phản ứng: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtNgayXuatHienPU' type='text' style='z-index: 0;' value='" + (OPhieuBCPUCoHaiADR.NgayXuatHienPU == null ? null : OPhieuBCPUCoHaiADR.NgayXuatHienPU.Value.ToString(" dd/MM/yyyy")) + "' class='form-control valueForm datepicker' required >\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-10\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Phản ứng xuất hiện sau bao lâu (tính từ lần dùng cuối của thuốc nghi ngờ): ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtPhanUngXuatHienSau' type='text' style='z-index: 0;' value='" + OPhieuBCPUCoHaiADR.ThoiGianPhanUng + "' class='form-control valueForm' required>\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +
              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Mô tả biểu hiện ADR: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtMoTaBieuHienADR' type='text' style='z-index: 0;' value='" + OPhieuBCPUCoHaiADR.MoTaADR + "' class='form-control valueForm' required >\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +
              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Xét nghiệm liên quan tới phản ứng: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtXetNghiemLQPU' type='text' style='z-index: 0;' value='" + OPhieuBCPUCoHaiADR.XetNghiemLienQuan + "' class='form-control valueForm' required >\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +       
              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Tiền sử (dị ứng, thai nghén, nghiện thuốc lá, nghiện rượu, bệnh gan, bệnh thận...): ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtTienSuBenh' type='text' style='z-index: 0;' value='" + OPhieuBCPUCoHaiADR.TienSuBenhSu + "' class='form-control valueForm' required >\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +

              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Cách xử trí phản ứng: ") + "<span style = 'color:red' > *</span >\r\n" +
              "                                       <input id='txtXuTri' type='text' style='z-index: 0;' value='" + OPhieuBCPUCoHaiADR.XuTri + "' class='form-control valueForm' required>\r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +

              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <h3></h3>" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Mức độ nghiêm trọng của phản ứng:") +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\" id ='cbkMucDoNTCuaPU'>\r\n" +
              "                                   <div class = 'row'>\r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbTuVong\" type = \"checkbox\" name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVong + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVong ? "checked" : null) + ">Tử vong</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbNhapVienKeoDai\" type = \"checkbox\" name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.NhapVien + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.NhapVien ? "checked" : null) + ">Nhập viện kéo dài</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbDiTatThaiNhi\" type = \"checkbox\"  style='z-index: 0;' name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DiTatThaiNhi + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DiTatThaiNhi ? "checked" : null) + ">Dị tật thai nhi</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                                   <div class = 'row'>\r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbDeDoaTinhMang\" type = \"checkbox\" name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DeDoaTinhMang + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DeDoaTinhMang ? "checked" : null) + ">Đe dọa tính mạng</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbTanTat\" type = \"checkbox\" name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TanTat + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TanTat ? "checked" : null) + ">Tàn tật vĩnh viễn/nặng nề</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-3\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbKhongNghiemTrong\" type = \"checkbox\"  style='z-index: 0;' name=\"ckbmotapu\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongNghiemTrong + "\" " + (OPhieuBCPUCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongNghiemTrong ? "checked" : null) + ">không nghiêm trọng</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +

              "                           <div class = 'row' >\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <h3></h3>" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Kết quả sau khi xử trí phản ứng: ") +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\" id ='cbkKqSauXuTri'>\r\n" +
              "                                  <div class = 'row'  >\r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbTuVongADR\" type = \"checkbox\" name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongDoADR + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongDoADR ? "checked" : null) + ">Từ vong do ADR</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbChuaHoiPhuc\" type = \"checkbox\" name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaHoiPhuc + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaHoiPhuc ? "checked" : null) + ">Chưa hồi phục</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbHoiPhucCoDiChung\" type = \"checkbox\"  style='z-index: 0;' name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucCoDiChung + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucCoDiChung ? "checked" : null) + ">Hồi phục có di chứng</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                  </div>\r\n" +
              "                                  <div class = 'row'>\r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbTuVongKhongLQThuoc\" type = \"checkbox\" name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongKhongLqThuoc + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongKhongLqThuoc ? "checked" : null) + ">Tử vong không liên quan đến thuốc</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbDangHoiPhuc\" type = \"checkbox\" name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DangHoiPhuc + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DangHoiPhuc ? "checked" : null) + ">Đang hồi phục</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbHoiPhucKhongDiChung\" type = \"checkbox\"  style='z-index: 0;' name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucKhongDiChung + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucKhongDiChung ? "checked" : null) + ">Hồi phục không di chứng</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-3\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                             <label class=\"checkbox-inline\"><input id =\"ckbKhongRo\" type = \"checkbox\" name=\"ckbKQXuTri\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongRo + "\" " + (OPhieuBCPUCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongRo ? "checked" : null) + ">Không rõ</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                  </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +             
              "                       </div>\r\n" +
              "                   </div>\r\n" +

              "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
              "                       <div class=\"ibox-title\">\r\n" +
              "                           <h5 style = \"color: black\">" + WebLanguage.GetLanguage(OSiteParam, "C. THÔNG TIN VỀ THUỐC NGHI NGỜ GÂY ADR") + "</h5>\r\n" +
              "                           <div class=\"ibox-tools\">\r\n" +
              "                               <a class=\"collapse-link\">\r\n" +
              "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
              "                               </a>\r\n" +
              "                           </div>\r\n" +
              "                       </div>\r\n" +
              "                       <div class=\"ibox-content col-md-12\" id ='divThuocADRs'>\r\n" +
              "                          <div id='TabThuocADR' class='tab-pane'>\r\n" +
                                               DrawTTVeThuoc(ORenderInfo).HtmlContent +
              "                          </div>\r\n" +
              "                          <div id='TabThuocDongThoi' class='tab-pane' style ='margin-top: 15px; width: 1050px;'>\r\n" +                      
                                               DrawThuocDungDongThoi(ORenderInfo).HtmlContent +
              "                          </div>\r\n" +
              "                       </div>\r\n" +
              "                   </div>\r\n" +


              "                   <div class=\"ibox float-e-margins col-md-12\">\r\n" +
              "                       <div class=\"ibox-title\">\r\n" +
              "                           <h5 style = \"color: black\">" + WebLanguage.GetLanguage(OSiteParam, "D. PHẦN THẨM ĐỊNH ADR CỦA ĐƠN VỊ") + "</h5>\r\n" +
              "                           <div class=\"ibox-tools\">\r\n" +
              "                               <a class=\"collapse-link\">\r\n" +
              "                                   <i class=\"fa fa-chevron-up\"></i>\r\n" +
              "                               </a>\r\n" +
              "                           </div>\r\n" +
              "                       </div>\r\n" +
              "                       <div class=\"ibox-content col-md-12\" >\r\n" +
              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "1. Đánh giá mối liên quan giữa thuốc và ADR ") +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\" id ='cbkThuocVaADR'>\r\n" +
              "                                   <div class = 'row' >\r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbChacChan\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChacChan + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChacChan ? "checked" : null) + ">Chắc chắn</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbKhongChacChan\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongChacChan + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongChacChan ? "checked" : null) + ">Không chắc chắn</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                                   <div class = 'row'>\r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <label class=\"checkbox-inline\"><input id =\"ckbCoKhaNang\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoKhaNang + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoKhaNang ? "checked" : null) + ">Có khả năng</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                               <label class=\"checkbox-inline\"><input id =\"ckbChuaPhanLoai\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaPhanLoai + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaPhanLoai ? "checked" : null) + ">Chưa phân loại</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                                   <div class = 'row '>\r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                               <label class=\"checkbox-inline\"><input id =\"ckbCoThe\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoThe + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoThe ? "checked" : null) + ">Có thể</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                               <label class=\"checkbox-inline\"><input id =\"ckbKhongPhanLoai\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongThePhanLoai + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongThePhanLoai ? "checked" : null) + ">Không phân loại</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                                   <div class = 'row'>\r\n" +
              "                                       <div class=\"col-md-2\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                               <label class=\"checkbox-inline\"><input id =\"ckbKhac\" type = \"checkbox\" name=\"ckbThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac + "\" " + (OPhieuBCPUCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac ? "checked" : null) + " >khác</label></br> " +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                       <div class=\"col-md-8\">\r\n" +
              "                                           <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                               <input id='txtKhac' type='text' style='z-index: 0;' class='form-control valueForm' value ='" + OPhieuBCPUCoHaiADR.MoTaThuocVaADR + "' disabled>\r\n" +
              "                                           </div>\r\n" +
              "                                       </div> \r\n" +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +

              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "2. Đơn vị thẩm định ADR theo thang nào?") +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                               <div class=\"col-md-12\" id ='cbkThangThamDinhADR'>\r\n" +
              "                                  <div class = 'row'>\r\n" +
              "                                      <div class=\"col-md-1\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                                <label class=\"checkbox-inline\"><input id = \"ckbThangWHO\" type = \"checkbox\" name=\"ckbThangThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangWHO + "\" " + (OPhieuBCPUCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangWHO ? "checked" : null) + ">Thang WHO</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-1\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                                <label class=\"checkbox-inline\"><input id = \"ckbThangNaranjo\" type = \"checkbox\" name=\"ckbThangThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangWHO + "\" " + (OPhieuBCPUCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangNaranjo ? "checked" : null) + ">Thang Naranjo</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-1\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                                <label class=\"checkbox-inline\"><input id =\"ckbThangKhac\" type = \"checkbox\" name=\"ckbThangThamDinhADR\" value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangKhac + "\" " + (OPhieuBCPUCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangKhac ? "checked" : null) + "  >Thang khác</label></br> " +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                      <div class=\"col-md-9\">\r\n" +
              "                                          <div class=\"form-group\" style='margin-top: 14px; '>\r\n" +
              "                                              <input id='txtThangKhac' type='text' style='z-index: 0;' class='form-control valueForm' value ='"+ OPhieuBCPUCoHaiADR.MoTaThangThamDinh + "' disabled>\r\n" +
              "                                          </div>\r\n" +
              "                                      </div> \r\n" +
              "                                 </div>\r\n" +
              "                             </div>\r\n" +
              "                         </div>\r\n" +

              "                           <div class = 'row'>\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <div class=\"form-group\">\r\n" +
                                                      WebLanguage.GetLanguage(OSiteParam, "Phần bình luận của cán bộ y tế (nếu có)") +
              "                                   </div>\r\n" +
              "                               </div>\r\n" +
              "                           </div>\r\n" +

              "                           <div class=\"row\">\r\n" +
              "                               <div class=\"col-md-12\">\r\n" +
              "                                   <textarea class=\"form-control\" id=\"txtBinhLuanCBYTe\" rows=\"5\">"+ OPhieuBCPUCoHaiADR.BinhLuan +"</textarea>\r\n" +
              "                               </div>\r\n" +
              "                           </div>" +
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
        public static AjaxOut DrawTTVeThuoc(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;

                string cbbPhanUngCaiThien = "<select id=\"cbbPhanUngCaiThien\" class=\"form-control\" style=\"border-radius: 4px;\">\r\n";
                foreach (var CaiThien in ThuocADRParser.TraLois)
                    cbbPhanUngCaiThien += string.Format("<option value={0}>{1}</option>", CaiThien.Key, CaiThien.Value);
                cbbPhanUngCaiThien += "</select>\r\n";

                string cbbPhanUngXuatHien = "<select id=\"cbbPhanUngXuatHien\" class=\"form-control\" style=\"border-radius: 4px;\">\r\n";
                foreach (var PhanUng in ThuocADRParser.TraLois)
                    cbbPhanUngXuatHien += string.Format("<option value={0}>{1}</option>", PhanUng.Key, PhanUng.Value);
                cbbPhanUngXuatHien += "</select>\r\n";

                string html = "";
                List<ThuocADRCls> OThuocADR = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                html =
                     "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                     "  <thead> \r\n" +
                     "      <tr> \r\n" +
                     "          <th width=30> " + WebLanguage.GetLanguage(OSiteParam, "STT") + "</th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thuốc(Tên gốc và tên thương mại") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Dạng bào chế, hàm lượng") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Nhà sản xuất") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số lô SX") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Liều dùng 1 lần") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Số lần dùng trong ngày/tuần/tháng") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đường dùng") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày vào") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày ra") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Lý do dùng thuốc ") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "có cải thiện sau khi giảm liều?") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "có phản ứng khi tái sử dụng?") + " </th> \r\n" +
                     "          <th width=60 style='text-align:center;'><a href='javascript:ShowThuocADR()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                     "      </tr> \r\n" +
                     "  </thead> \r\n" +
                     "  <tbody> \r\n" +
                     "      <tr id='trAddThuocADR' style='display:none;'> \r\n" +
                     "          <input type='hidden' id='hdThuocADRId' value=''>\r\n" +
                     "          <td></td> \r\n" +
                     "          <td><div id='divCbbThuocADR'></div></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtDangBaoChe'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtNhaSanXuat'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtSoLoSX'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtLieuDung1Lan'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtSoLanDung'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtDuongDung'></td> \r\n" +
                     "          <td><input type='text' style='z-index: 0;' class='form-control ' id='dtNgayVao'></td> \r\n" +
                     "          <td><input type='text' style='z-index: 0;' class='form-control' id='dtNgayRa'></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtLyDo'></td> \r\n" +
                     "          <td>\r\n" + cbbPhanUngCaiThien + "</td> \r\n" +
                     "          <td>\r\n" + cbbPhanUngXuatHien + "</td> \r\n" +
                     "          <td style='text-align:center;'><a href='javascript:SaveThuocADR(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "'><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                     "      </tr> \r\n";
                for (int iIndex = 0; iIndex < OThuocADR.Count; iIndex++)
                {
                    if (OThuocADR[iIndex].LOAITHUOC == 1)
                    {

                        cbbPhanUngCaiThien = "<select id=\"cbbPhanUngCaiThien" + iIndex + "\" class=\"form-control CssEditorItemThuocADR\" style=\"border-radius: 4px; display:none;\">\r\n";
                        foreach (var CaiThien in ThuocADRParser.TraLois)
                            cbbPhanUngCaiThien += string.Format("<option value={0} {1}>{2}</option>", CaiThien.Key, OThuocADR[iIndex].PHANUNGCAITHIEN == CaiThien.Key ? "checked" : null, CaiThien.Value);
                        cbbPhanUngCaiThien += "</select>\r\n";

                        cbbPhanUngXuatHien = "<select id=\"cbbPhanUngXuatHien" + iIndex + "\" class=\"form-control CssEditorItemThuocADR\" style=\"border-radius: 4px; display:none;\">\r\n";
                        foreach (var PhanUng in ThuocADRParser.TraLois)
                            cbbPhanUngXuatHien += string.Format("<option value={0} {1}>{2}</option>", PhanUng.Key, OThuocADR[iIndex].PHANUNGXUATHIEN == PhanUng.Key ? "checked" : null, PhanUng.Value);
                        cbbPhanUngXuatHien += "</select>\r\n";

                        HangCls hang = string.IsNullOrEmpty(OThuocADR[iIndex].HANG_ID) ? null : OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), OThuocADR[iIndex].HANG_ID);
                        html += "<tr> \r\n" +
                                "   <input type='hidden' id='hdThuocADRId" + iIndex + "' value='" + OThuocADR[iIndex].ID + "'>\r\n" +
                                "   <input type='hidden' id='hdHangId" + iIndex + "' value='" + OThuocADR[iIndex].HANG_ID + "'>\r\n" +
                                "   <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "    <td><div id='divCbbThuocADR" + iIndex + "' class='CssEditorItemThuocADR' style='display:none'></div><span class='CssDisplayItemThuocADR' id='spThuocADR" + iIndex + "'>" + (hang != null ? hang.Ten : null) + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtDangBaoChe" + iIndex + "' value='" + OThuocADR[iIndex].DANGBAOCHE + "'><span class='CssDisplayItemThuocADR' id='spDangBaoChe" + iIndex + "'>" + OThuocADR[iIndex].DANGBAOCHE + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtNhaSanXuat" + iIndex + "' value='" + OThuocADR[iIndex].NHASANXUAT + "'><span class='CssDisplayItemThuocADR' id='spNhaSanXuat" + iIndex + "'>" + OThuocADR[iIndex].NHASANXUAT + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtSoLoSX" + iIndex + "' value='" + OThuocADR[iIndex].SOLOSX + "'><span class='CssDisplayItemThuocADR' id='spSoLoSX" + iIndex + "'>" + OThuocADR[iIndex].SOLOSX + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtLieuDung1Lan" + iIndex + "' value='" + OThuocADR[iIndex].LIEUDUNG1LAN + "'><span class='CssDisplayItemThuocADR' id='spLieuDung1Lan" + iIndex + "'>" + OThuocADR[iIndex].LIEUDUNG1LAN + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtSoLanDung" + iIndex + "' value='" + OThuocADR[iIndex].SOLANDUNG + "'><span class='CssDisplayItemThuocADR' id='spSoLanDung" + iIndex + "'>" + OThuocADR[iIndex].SOLANDUNG + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; ' id='txtDuongDung" + iIndex + "' value='" + OThuocADR[iIndex].DUONGDUNG + "'><span class='CssDisplayItemThuocADR' id='spDuongDung" + iIndex + "'>" + OThuocADR[iIndex].DUONGDUNG + "</span></td> \r\n" +
                                "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; z-index: 0;' id='dtNgayVao" + iIndex + "' value='" + OThuocADR[iIndex].NGAYVAOVIEN.Value.ToString("HH:mm dd/MM/yyyy") + "'><span class='CssDisplayItemThuocADR' id='spNgayVao" + iIndex + "'>" + OThuocADR[iIndex].NGAYVAOVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemThuocADR' style='display:none; z-index: 0;' id='dtNgayRa" + iIndex + "' value='" + OThuocADR[iIndex].NGAYRAVIEN.Value.ToString("HH:mm dd/MM/yyyy") + "'><span class='CssDisplayItemThuocADR' id='spNgayRa" + iIndex + "'>" + OThuocADR[iIndex].NGAYRAVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "   <td><input type='text' class='form-control CssEditorItemThuocADR' style='display:none;z-index: 0;' id='txtLyDo" + iIndex + "' value='" + OThuocADR[iIndex].LYDODUNGTHUOC + "'><span class='CssDisplayItemThuocADR' id='spLyDo" + iIndex + "'>" + OThuocADR[iIndex].LYDODUNGTHUOC + "</span></td> \r\n" +
                                "   <td>\r\n" + cbbPhanUngCaiThien + "<span class='CssEditorItemThuocADR' id='spPhanUngCaiThien" + iIndex + "'>" + (OThuocADR[iIndex] == null || OThuocADR[iIndex].PHANUNGCAITHIEN == null ? null : ThuocADRParser.TraLois[OThuocADR[iIndex].PHANUNGCAITHIEN.Value]) + "</span></td> \r\n" +
                                "   <td>\r\n" + cbbPhanUngXuatHien + "<span class='CssEditorItemThuocADR' id='spPhanUngXuatHien" + iIndex + "'>" + (OThuocADR[iIndex] == null || OThuocADR[iIndex].PHANUNGXUATHIEN == null ? null : ThuocADRParser.TraLois[OThuocADR[iIndex].PHANUNGXUATHIEN.Value]) + "</span></td> \r\n" +
                                "   <td style='text-align:center;'>\r\n" +
                                "       <a id='btnSaveThuocADR" + iIndex + "' class='CssEditorItemThuocADR' style='display:none' href='javascript:SaveThuocADR(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                                "       <a id='btnEditThuocAdR" + iIndex + "' class='CssEditorItemThuocADR' href='javascript:ShowEditItemLineThuocADR(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                                "       <a id='btnDeleteThuocADR" + iIndex + "' class='CssEditorItemThuocADR' href='javascript:DeleteThuocADR(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                "   </td> \r\n" +
                                "</tr> \r\n";
                    }
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
        public static AjaxOut DrawThuocDungDongThoi(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string html = "";
                List<ThuocADRCls> OThuocDongThoi = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                html =
                     WebLanguage.GetLanguage(OSiteParam, "Cách dùng thuốc đông thời(Ngoại trừ các thuốc dùng điều trị/khắc phục hậu quả của ADR)") +
                     "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                     "  <thead> \r\n" +
                     "      <tr> \r\n" +
                     "          <th width=30> " + WebLanguage.GetLanguage(OSiteParam, "STT") + "</th> \r\n" +
                     "          <th  width=200 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên thuốc") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Dạng bào chế, hàm lượng") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày vào") + " </th> \r\n" +
                     "          <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày ra") + " </th> \r\n" +
                     "          <th width=60 style='text-align:center;'><a href='javascript:ShowThuocDungDongThoi()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" +
                     "      </tr> \r\n" +
                     "  </thead> \r\n" +
                     "  <tbody> \r\n" +
                     "      <tr id='trAddThuocDongThoi' style='display:none;'> \r\n" +
                     "          <input type='hidden' id='hdThuocADR1Id' value=''>\r\n" +
                     "          <td></td> \r\n" +
                     "          <td><div id ='divCbbThuocADRDT'></div></td> \r\n" +
                     "          <td><input type='text' class='form-control' id='txtDangBaoChe1'></td> \r\n" +
                     "          <td><input type='text' class='form-control' style='z-index: 0;' id='dtNgayVao1'></td> \r\n" +
                     "          <td><input type='text' class='form-control' style='z-index: 0;' id='dtNgayRa1'></td> \r\n" +
                     "          <td style='text-align:center;'><a href='javascript:SaveThuocDongThoi(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "'><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                     "      </tr> \r\n";
                for (int iIndex = 0; iIndex < OThuocDongThoi.Count; iIndex++)
                {
                    string defaultSave = "";
                    string defaultDelete = "";
                    defaultSave = " style ='pointer-events: auto; display:none' ";
                    defaultDelete = " style ='pointer-events: auto; font-size:14px;' ";
                    if (OThuocDongThoi[iIndex].LOAITHUOC == 2)
                    {
                        HangCls hang = string.IsNullOrEmpty(OThuocDongThoi[iIndex].HANG_ID) ? null : OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), OThuocDongThoi[iIndex].HANG_ID);
                        html += "<tr> \r\n" +
                                "   <input type='hidden' id='hdThuocADR1Id" + iIndex + "' value='" + OThuocDongThoi[iIndex].ID + "'>\r\n" +
                                "   <input type='hidden' id='hdHang1Id" + iIndex + "' value='" + OThuocDongThoi[iIndex].HANG_ID + "'>\r\n" +
                                "   <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "   <td><div id='divCbbThuocADRDT" + iIndex + "' class='CssEditorItemThuocDongThoi' style='display:none'></div><span class='CssDisplayItemThuocDongThoi' id='spThuocADR1" + iIndex + "'>" + (hang != null ? hang.Ten : null) + "</span></td> \r\n" +                             
                                "   <td><input type='text' class='form-control CssEditorItemThuocDongThoi' style='display:none; ' id='txtDangBaoChe1" + iIndex + "' value='" + OThuocDongThoi[iIndex].DANGBAOCHE + "'><span class='CssDisplayItemThuocDongThoi' id='spDangBaoChe1" + iIndex + "'>" + OThuocDongThoi[iIndex].DANGBAOCHE + "</span></td> \r\n" +
                                "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemThuocDongThoi' style='display:none; z-index: 0;' id='dtNgayVao1" + iIndex + "' value='" + OThuocDongThoi[iIndex].NGAYVAOVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "'><span class='CssDisplayItemThuocDongThoi' id='spNgayVao1" + iIndex + "'>" + OThuocDongThoi[iIndex].NGAYVAOVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "   <td style='text-align: center;'><input type='text' class='form-control CssEditorItemThuocDongThoi' style='display:none; z-index: 0;' id='dtNgayRa1" + iIndex + "' value='" + OThuocDongThoi[iIndex].NGAYRAVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "'><span class='CssDisplayItemThuocDongThoi' id='spNgayRa1" + iIndex + "'>" + OThuocDongThoi[iIndex].NGAYRAVIEN.Value.ToString(" HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "   <td style='text-align:center;'>\r\n" +
                                "       <a id='btnSaveThuocDongThoi" + iIndex + "' class='CssEditorItemThuocDongThoi'  " + defaultSave + " href='javascript:SaveThuocDongThoi(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                                "       <a id='btnEditThuocDongThoi" + iIndex + "' class='CssEditorItemThuocDongThoi' href='javascript:ShowEditItemLineThuocDongThoi(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                                "       <a id='btnDeleteThuocDongThoi" + iIndex + "' class='CssEditorItemThuocDongThoi' href='javascript:DeleteThuocDongThoi(" + iIndex + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' "+ defaultDelete + "><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                "   </td> \r\n" +
                                "</tr> \r\n";
                    }               
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
        public static AjaxOut GetBoDemValueAndMaHV(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string boDemValue = null;
            string maHV = null;
            BoMaCls boMa = CoreCallBussinessUtility.CreateBussinessProcess().CreateBoMaProcess().Reading(ORenderInfo, new BoMaFilterCls() { TableName = "PhieuADR", FieldName = "SoBaoCao" }).FirstOrDefault();
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
        public static AjaxOut SavePhieuADRADR(RenderInfoCls ORenderInfo, string PhieuADRId, string SoBCDonVi, string SoBCCuaTTQG, string KhoaId, string ChucVuId, string NguoiLapId, string DienThoai, string Email, string DangBaoCao, string NgayLapBaoCao, string MaBN, string TenBN, string NgaySinh, string GioiTinh, int CanNang, string NgayXuatHienPU, string PhanUngXuatHienSau, string MoTaBieuHieuADR, string XetNghiemLQPU, string TienSuBenhSu, string CachXuTri, string MucDoNghiemTrong, string KQXuTri, string ThamDinhADR, string thamDinhADRKhac, string ThangThamDinhADR, string thangDinhADRKhac, string BinhLuanCBYTe)
        {
            AjaxOut ajaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoPhanUngCoHaiADRCls OPhieu = new PhieuBaoCaoPhanUngCoHaiADRCls();
                if (!string.IsNullOrEmpty(PhieuADRId))
                {
                    OPhieu = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
                }
                else
                {
                    OPhieu = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, SoBCDonVi);
                    if (OPhieu == null)
                    {
                        OPhieu = new PhieuBaoCaoPhanUngCoHaiADRCls() { SoBcDonVi = SoBCDonVi };
                    }
                }
                #region Thêm mới/cập nhật phiếu
                if (string.IsNullOrEmpty(OPhieu.Id))
                {
                    OPhieu.Id = Guid.NewGuid().ToString();
                    OPhieu.SoBcDonVi = SoBCDonVi;
                    OPhieu.SoBCQuocGia = SoBCCuaTTQG;
                    OPhieu.NoiBaoCao_Id = KhoaId;
                    OPhieu.NguoiLap_Id = NguoiLapId;
                    OPhieu.ChucVu_Id = ChucVuId;
                    OPhieu.DienThoai = DienThoai;
                    OPhieu.Email = Email;
                    OPhieu.DangBaoCao = Convert.ToInt32(DangBaoCao);
                    OPhieu.MaBN = MaBN;
                    OPhieu.HoTen = TenBN;
                    OPhieu.NgaySinh = string.IsNullOrWhiteSpace(NgaySinh) ? null : (DateTime?)DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", null);
                    OPhieu.GioiTinh = Convert.ToInt32(GioiTinh);
                    OPhieu.CanNang = CanNang;
                    OPhieu.NgayXuatHienPU = string.IsNullOrWhiteSpace(NgayXuatHienPU) ? null : (DateTime?)DateTime.ParseExact(NgayXuatHienPU, "dd/MM/yyyy", null);
                    OPhieu.NgayLap = string.IsNullOrWhiteSpace(NgayLapBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayLapBaoCao, "dd/MM/yyyy", null);
                    OPhieu.ThoiGianPhanUng = PhanUngXuatHienSau;
                    OPhieu.MoTaADR = MoTaBieuHieuADR;
                    OPhieu.XetNghiemLienQuan = XetNghiemLQPU;
                    OPhieu.TienSuBenhSu = TienSuBenhSu;
                    OPhieu.XuTri = CachXuTri;
                    OPhieu.MucDoNghiemTrong = Convert.ToInt32(MucDoNghiemTrong);
                    OPhieu.KetQuaSauXuTri = Convert.ToInt32(KQXuTri);
                    OPhieu.ThangThamDinhADR = Convert.ToInt32(ThangThamDinhADR);
                    if (ThangThamDinhADR == ((int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac).ToString())
                    {
                        OPhieu.MoTaThangThamDinh = thangDinhADRKhac;
                    }
                    else
                    {
                        OPhieu.ThangThamDinhADR = Convert.ToInt32(ThangThamDinhADR);
                    }
                    if (ThamDinhADR == ((int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac).ToString())
                    {
                        OPhieu.MoTaThuocVaADR = thamDinhADRKhac;
                    }
                    else
                    {
                        OPhieu.ThuocVaADR = Convert.ToInt32(ThamDinhADR);
                    }
                    OPhieu.BinhLuan = BinhLuanCBYTe;
                    OPhieu.TrangThai = (int)PhieuBaoCaoPhanUngCoHaiADRCls.eTrangThai.Moi;
                    Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Add(ORenderInfo, OPhieu);
                    ajaxOut.RetExtraParam1 = OPhieu.Id;
                }
                else
                {
                    OPhieu.SoBcDonVi = SoBCDonVi;
                    OPhieu.SoBCQuocGia = SoBCCuaTTQG;
                    OPhieu.NoiBaoCao_Id = KhoaId;
                    OPhieu.NguoiLap_Id = NguoiLapId;
                    OPhieu.ChucVu_Id = ChucVuId;
                    OPhieu.DienThoai = DienThoai;
                    OPhieu.Email = Email;
                    OPhieu.DangBaoCao = Convert.ToInt32(DangBaoCao);
                    OPhieu.MaBN = MaBN;
                    OPhieu.HoTen = TenBN;
                    OPhieu.NgaySinh = string.IsNullOrWhiteSpace(NgaySinh) ? null : (DateTime?)DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", null);
                    OPhieu.GioiTinh = Convert.ToInt32(GioiTinh);
                    OPhieu.CanNang = CanNang;
                    OPhieu.NgayXuatHienPU = string.IsNullOrWhiteSpace(NgayXuatHienPU) ? null : (DateTime?)DateTime.ParseExact(NgayXuatHienPU, "dd/MM/yyyy", null);
                    OPhieu.NgayLap = string.IsNullOrWhiteSpace(NgayLapBaoCao) ? null : (DateTime?)DateTime.ParseExact(NgayLapBaoCao, "dd/MM/yyyy", null);
                    OPhieu.ThoiGianPhanUng = PhanUngXuatHienSau;
                    OPhieu.MoTaADR = MoTaBieuHieuADR;
                    OPhieu.XetNghiemLienQuan = XetNghiemLQPU;
                    OPhieu.TienSuBenhSu = TienSuBenhSu;
                    OPhieu.XuTri = CachXuTri;
                    OPhieu.MucDoNghiemTrong = Convert.ToInt32(MucDoNghiemTrong);
                    OPhieu.KetQuaSauXuTri = Convert.ToInt32(KQXuTri);
                    OPhieu.ThangThamDinhADR = Convert.ToInt32(ThangThamDinhADR);
                    if (ThangThamDinhADR == ((int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangKhac).ToString())
                    {
                        OPhieu.MoTaThangThamDinh = thangDinhADRKhac;
                    }
                    else
                    {
                        OPhieu.ThangThamDinhADR = Convert.ToInt32(ThangThamDinhADR);
                    }
                    OPhieu.ThuocVaADR = Convert.ToInt32(ThamDinhADR);
                    if (ThamDinhADR == ((int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac).ToString())
                    {
                        OPhieu.MoTaThuocVaADR = thamDinhADRKhac;
                    }
                    else
                    {
                        OPhieu.ThuocVaADR = Convert.ToInt32(ThamDinhADR);
                    }
                    OPhieu.BinhLuan = BinhLuanCBYTe;
                    Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Save(ORenderInfo, PhieuADRId, OPhieu);
                }
                #endregion

                #region Thêm mới/cập nhật thuốc
                List<ThuocADRCls> newThuocADRs = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                List<ThuocADRCls> oldThuocADRs = CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Reading(ORenderInfo, new ThuocADRFilterCls() { Phieu_ID = OPhieu.Id }).ToList();
                foreach (var oldThuocADR in oldThuocADRs)
                {
                    bool isExists = false;
                    foreach (var newThuocADR in newThuocADRs)
                    {
                        if (newThuocADR.ID == oldThuocADR.ID)//cập nhật
                        {
                            oldThuocADR.PHIEU_ID = OPhieu.Id;
                            oldThuocADR.HANG_ID = newThuocADR.HANG_ID;
                            oldThuocADR.DANGBAOCHE = newThuocADR.DANGBAOCHE;
                            oldThuocADR.NHASANXUAT = newThuocADR.NHASANXUAT;
                            oldThuocADR.SOLOSX = newThuocADR.SOLOSX;
                            oldThuocADR.LIEUDUNG1LAN = newThuocADR.LIEUDUNG1LAN;
                            oldThuocADR.SOLANDUNG = newThuocADR.SOLANDUNG;
                            oldThuocADR.DUONGDUNG = newThuocADR.DUONGDUNG;
                            oldThuocADR.NGAYVAOVIEN = newThuocADR.NGAYVAOVIEN;
                            oldThuocADR.NGAYRAVIEN = newThuocADR.NGAYRAVIEN;
                            oldThuocADR.LYDODUNGTHUOC = newThuocADR.LYDODUNGTHUOC;
                            oldThuocADR.PHANUNGCAITHIEN = newThuocADR.PHANUNGCAITHIEN;
                            oldThuocADR.PHANUNGXUATHIEN = newThuocADR.PHANUNGXUATHIEN;
                            oldThuocADR.LOAITHUOC = newThuocADR.LOAITHUOC;
                            CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Save(ORenderInfo, oldThuocADR.ID, oldThuocADR);                          
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Delete(ORenderInfo, oldThuocADR.ID);
                    }
                }
                var addThuocADRs = newThuocADRs.Where(o => !oldThuocADRs.Any(old => old.ID == o.ID));
                foreach (var addThuocADR in addThuocADRs)//Thêm mới
                {
                    addThuocADR.PHIEU_ID = OPhieu.Id;
                    CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Add(ORenderInfo, addThuocADR);                
                }
                #endregion

                ajaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
            }
            catch (Exception ex)
            {
                ajaxOut.Error = true;
                ajaxOut.InfoMessage = ex.Message.ToString();
                ajaxOut.HtmlContent = ex.Message.ToString();
            }
            return ajaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DeleteThongTin(RenderInfoCls ORenderInfo, string PhieuADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                    CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Delete(ORenderInfo, PhieuADRId);           
                    CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Delete(ORenderInfo, PhieuADRId);
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                    string dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "danhsachphieuadr", new WebParamCls[] { });
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string PhieuADRId, int trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoPhanUngCoHaiADRCls PhanUngCoHai = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
                if (PhanUngCoHai == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phiếu này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                PhanUngCoHai.TrangThai = trangThai;
                CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Save(ORenderInfo, PhanUngCoHai.Id, PhanUngCoHai);             
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
        public static AjaxOut SaveThuocADR(RenderInfoCls ORenderInfo, string ThuocADRId, string Hang_ID, string DangBaoChe, string NhaSanXuat, string SoLoSX, string LieuDung1Lan, string SoLanDung, string DuongDung, string NgayVao, string NgayRa, string LyDo, int CoCaiThien, int CoPhanUng)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<ThuocADRCls> newThuocADRs = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                if (newThuocADRs.Any(o => o.HANG_ID == Hang_ID && o.ID != ThuocADRId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thuốc này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(ThuocADRId))//thêm mới
                {
                    newThuocADRs.Add(new ThuocADRCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        HANG_ID = Hang_ID,
                        DANGBAOCHE = DangBaoChe,
                        NHASANXUAT = NhaSanXuat,
                        SOLOSX = SoLoSX,
                        LIEUDUNG1LAN = LieuDung1Lan,
                        SOLANDUNG = SoLanDung,
                        DUONGDUNG = DuongDung,
                        NGAYVAOVIEN = string.IsNullOrWhiteSpace(NgayVao) ? null : (DateTime?)DateTime.ParseExact(NgayVao, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture),
                        NGAYRAVIEN = string.IsNullOrWhiteSpace(NgayRa) ? null : (DateTime?)DateTime.ParseExact(NgayRa, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture),
                        LYDODUNGTHUOC = LyDo,
                        PHANUNGCAITHIEN = CoCaiThien,
                        PHANUNGXUATHIEN = CoPhanUng,
                        LOAITHUOC = 1,
                    });
                }
                else//cập nhật
                {
                    ThuocADRCls newThuocADR = newThuocADRs.FirstOrDefault(o => o.ID == ThuocADRId);
                    if (newThuocADR == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thuốc ADR này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    newThuocADR.HANG_ID = Hang_ID;
                    newThuocADR.DANGBAOCHE = DangBaoChe;
                    newThuocADR.NHASANXUAT = NhaSanXuat;
                    newThuocADR.SOLOSX = SoLoSX;
                    newThuocADR.LIEUDUNG1LAN = LieuDung1Lan;
                    newThuocADR.SOLANDUNG = SoLanDung;
                    newThuocADR.DUONGDUNG = DuongDung;
                    newThuocADR.NGAYVAOVIEN = string.IsNullOrWhiteSpace(NgayVao) ? null : (DateTime?)DateTime.ParseExact(NgayVao, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                    newThuocADR.NGAYRAVIEN = string.IsNullOrWhiteSpace(NgayRa) ? null : (DateTime?)DateTime.ParseExact(NgayRa, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                    newThuocADR.LYDODUNGTHUOC = LyDo;
                    newThuocADR.PHANUNGCAITHIEN = CoCaiThien;
                    newThuocADR.PHANUNGXUATHIEN = CoPhanUng;
                    newThuocADR.LOAITHUOC = 1;
                }
                RetAjaxOut.HtmlContent = DrawTTVeThuoc(ORenderInfo).HtmlContent;
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
        public static AjaxOut SaveThuocDongThoi(RenderInfoCls ORenderInfo, string ThuocADRId, string Hang_ID, string DangBaoChe, string NgayVao, string NgayRa)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<ThuocADRCls> newThuocADRs = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                if (newThuocADRs.Any(o => o.HANG_ID == Hang_ID && o.ID != ThuocADRId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thuốc này đã có trong danh sách.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(ThuocADRId))//thêm mới
                {
                    newThuocADRs.Add(new ThuocADRCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        HANG_ID = Hang_ID,
                        DANGBAOCHE = DangBaoChe,
                        NGAYVAOVIEN = string.IsNullOrWhiteSpace(NgayVao) ? null : (DateTime?)DateTime.ParseExact(NgayVao, "HH:mm dd/MM/yyyy", null),
                        NGAYRAVIEN = string.IsNullOrWhiteSpace(NgayRa) ? null : (DateTime?)DateTime.ParseExact(NgayRa, "HH:mm dd/MM/yyyy", null),
                        LOAITHUOC = 2,
                    });
                }
                else//cập nhật
                {
                    ThuocADRCls newThuocADR = newThuocADRs.FirstOrDefault(o => o.ID == ThuocADRId);
                    if (newThuocADR == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thuốc ADR này đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    newThuocADR.HANG_ID = Hang_ID;
                    newThuocADR.DANGBAOCHE = DangBaoChe;
                    newThuocADR.NGAYVAOVIEN = string.IsNullOrWhiteSpace(NgayVao) ? null : (DateTime?)DateTime.ParseExact(NgayVao, "HH:mm dd/MM/yyyy", null);
                    newThuocADR.NGAYRAVIEN = string.IsNullOrWhiteSpace(NgayRa) ? null : (DateTime?)DateTime.ParseExact(NgayRa, "HH:mm dd/MM/yyyy", null);
                    newThuocADR.LOAITHUOC = 2;
                }
                RetAjaxOut.HtmlContent = DrawThuocDungDongThoi(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteThuocADR(RenderInfoCls ORenderInfo, string newThuocADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<ThuocADRCls> newThuocADRs = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                ThuocADRCls newThuocADR = newThuocADRs.FirstOrDefault(o => o.ID == newThuocADRId);
                newThuocADRs.Remove(newThuocADR);
                RetAjaxOut.HtmlContent = DrawTTVeThuoc(ORenderInfo).HtmlContent;
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
        public static AjaxOut DeleteThuocDongThoi(RenderInfoCls ORenderInfo, string newThuocADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<ThuocADRCls> newThuocADRs = WebSessionUtility.GetSession(OSiteParam, "PhieuADR_Thuocs") as List<ThuocADRCls>;
                ThuocADRCls newThuocADR = newThuocADRs.FirstOrDefault(o => o.ID == newThuocADRId);
                newThuocADRs.Remove(newThuocADR);
                RetAjaxOut.HtmlContent = DrawThuocDungDongThoi(ORenderInfo).HtmlContent;
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
        public static string cbbThuocADR(RenderInfoCls ORenderInfo, int? id, string value)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string cbbThuocADR =
                string.Format("<select id = 'cbbThuocADR{0}' style='display:{1}; font-size: 14px;' {2}>\r\n", id, id != null ? "none" : "block", id != null ? "class='CssEditorItemHang'" : null);
            if (!string.IsNullOrEmpty(value))
            {
                HangCls Ohang = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), value);
                if (Ohang != null)
                    cbbThuocADR += string.Format("<option value={0} selected>{1} - {2}</option>\r\n", Ohang.Id, Ohang.Ma, Ohang.Ten);
            }
            cbbThuocADR += "</select>\r\n";
            return cbbThuocADR;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static string cbbThuocADRDT(RenderInfoCls ORenderInfo, int? id, string value)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string cbbThuocADRDT =
                string.Format("<select id = 'cbbThuocADRDT{0}' style='display:{1}; font-size: 14px;' {2}>\r\n", id, id != null ? "none" : "block", id != null ? "class='CssEditorItemHang'" : null);
            if (!string.IsNullOrEmpty(value))
            {
                HangCls Ohang = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), value);
                if (Ohang != null)
                    cbbThuocADRDT += string.Format("<option value={0} selected>{1} - {2}</option>\r\n", Ohang.Id, Ohang.Ma, Ohang.Ten);
            }
            cbbThuocADRDT += "</select>\r\n";
            return cbbThuocADRDT;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut OnChagecbbThuocADR(RenderInfoCls ORenderInfo, string value)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                //HangCls Ohang = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), value);
                //RetAjaxOut.RetExtraParam1 = Ohang.;
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string PhieuADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoPhanUngCoHaiADRCls PhanUngCoHai = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
                if (PhanUngCoHai != null)
                    return PhieuBaoCaoPhanUngCoHaiADRCls.sColorTrangThais[PhanUngCoHai.TrangThai.Value];
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
        public static AjaxOut CheckTrangThai(RenderInfoCls ORenderInfo, string PhieuADRId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                PhieuBaoCaoPhanUngCoHaiADRCls PhanUngCoHai = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhieuADRId);
                if (PhanUngCoHai != null)
                    RetAjaxOut.RetExtraParam1 = PhanUngCoHai.TrangThai.ToString();
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
                PhieuBaoCaoPhanUngCoHaiADRCls PhieuADR = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, MaBN);
                if (PhieuADR != null)
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
    }
}
