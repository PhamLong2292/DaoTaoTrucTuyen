using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using OneTSQ.Permission;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Common;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_BcTongKetCongTacDaoTao : WebPartTemplate
    {
        public sealed class DaoTaoView
        {
            public string TenNhom { set; get; }
            public string TenKhoaHoc { set; get; }
            public DateTime? BatDau { set; get; }
            public DateTime? KetThuc { set; get; }
            public int TongSoHocVien { set; get; }
            public int LoaiDaoTao { set; get; }

        }
        public override string WebPartId
        {
            get
            {
                return "DT_BcTongKetCongTacDaoTao";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Báo cáo tổng kết công tác đào tạo";
            }
        }

        public override string Description
        {
            get
            {
                return "Báo cáo tổng kết công tác đào tạo";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_BcTongKetCongTacDaoTao), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = PermissionUtility.CheckPermission(ORenderInfo, new ThamVanCaBenhPermission().PermissionFunctionCode, OneTSQ.Common.ThamVanCaBenh.ePermission.Them.ToString(), new CaBenhPermission().PermissionFunctionCode, CaBenhPermission.StaticPermissionFunctionId, UserId);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                //bool permission = CheckPermission(ORenderInfo).RetBoolean;
                //if (!permission)
                //{
                //    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                //    return RetAjaxOut;
                //}
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string bcTongKetCongTacDaoTaoId = WebEnvironments.Request("id");
                bool isNewTime;
                DT_BcTongKetCongTacDaoTaoCls baoCao = null;
                string tenNguoiTao = null;
                if (string.IsNullOrEmpty(bcTongKetCongTacDaoTaoId))
                {
                    baoCao = new DT_BcTongKetCongTacDaoTaoCls()
                    {
                        NAM = DateTime.Today.Year,
                        TUNGAY = DateTime.Today,
                        DENNGAY = DateTime.Today,
                        NGAYTAO = DateTime.Now
                    };
                    isNewTime = true;
                    tenNguoiTao = user.FullName;
                }
                else
                {
                    baoCao = CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().CreateModel(ORenderInfo, bcTongKetCongTacDaoTaoId);
                    isNewTime = false;
                    var nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, baoCao.NGUOITAO_ID);
                    tenNguoiTao = nguoiTao == null ? null : nguoiTao.FullName;
                }
                //bool themPermission = PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Them.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, user.OwnerUserId);
                //bool xemPermission = baoCao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Xem.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, user.OwnerUserId, baoCao.NGUOITAO_ID);
                //bool suaPermission = (string.IsNullOrEmpty(baoCao.ID) && themPermission) || baoCao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Sua.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, user.OwnerUserId, baoCao.NGUOITAO_ID);
                //bool xoaPermission = baoCao.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Xoa.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, user.OwnerUserId, baoCao.NGUOITAO_ID);
                RetAjaxOut.HtmlContent =
                #region javascript
                    WebEnvironments.ProcessJavascript(
                    "<script language=javascript>\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Báo cáo tổng kết công tác đào tạo") + "';\r\n" +
                    //Set Height nội dung
                    "       $('.yearpicker').datetimepicker({ \r\n" +
                    "           format: 'YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('.datepicker').datetimepicker({ \r\n" +
                    "           format: 'DD/MM' \r\n" +
                    "       }); \r\n" +
                    (string.IsNullOrEmpty(bcTongKetCongTacDaoTaoId) ? null :
                    "       $('#divPrintButton').html(DrawPrintButton());\r\n") +
                    "   });\r\n" +
                #region Truyền id phiếu cho nút print
                    "function GetObjectId()\r\n" +
                    "{\r\n" +
                    "   return document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "}\r\n" +
                #endregion Truyền id phiếu cho nút print

                #region Refresh form về trạng thái mới
                    "   function Clear(){\r\n" +
                    "       now = new Date();\r\n" +
                    "       nam = now.getFullYear();\r\n" +
                    "       document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value = '';\r\n" +
                    "       document.getElementById('dtNam').value = nam;\r\n" +
                    "       document.getElementById('dtTuNgay').value = '01/01';\r\n" +
                    "       document.getElementById('dtDenNgay').value = '31/12';\r\n" +
                    "       $('#spNamKeHoach').html(nam + 1);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.DrawTongKetDaoTao(ORenderInfo, null, nam, tuNgay, denNgay, true).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divTongKetDaoTao').html(AjaxOut.HtmlContent);\r\n" +
                    "       $('#btnSave').show();\r\n" +
                    "       $('#btnDelete').hide();\r\n" +
                    "       $('#divPrintButton').hide();\r\n" +
                    "   }\r\n" +
                #endregion
                #region Load lại nội dung báo cáo theo sự thay đổi của thời gian
                    "   function LoadReportByTime(){\r\n" +
                    "       id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "       nam = parseInt(document.getElementById('dtNam').value);\r\n" +
                    "       tuNgay = document.getElementById('dtTuNgay').value;\r\n" +
                    "       denNgay = document.getElementById('dtDenNgay').value;\r\n" +
                    "       if(isNaN(nam))\r\n" +
                    "       {\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.DrawTongKetDaoTao(ORenderInfo, id, nam, tuNgay, denNgay, true).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divTongKetDaoTao').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion

                #region Thêm mới/Cập nhật phiếu đánh giá chất lượng đào tạo
                    "   function SaveDT_BcTongKetCongTacDaoTao(){\r\n" +
                    "       id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "       nam = parseInt(document.getElementById('dtNam').value);\r\n" +
                    "       tuNgay = document.getElementById('dtTuNgay').value;\r\n" +
                    "       denNgay = document.getElementById('dtDenNgay').value;\r\n" +
                    "       if(isNaN(nam))\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn năm báo cáo.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       dtLienTuc = document.getElementById('txtDtLienTuc').value;\r\n" +
                    "       dtTheoKeHoach = document.getElementById('txtDtTheoKeHoach').value;\r\n" +
                    "       dtNangCao = document.getElementById('txtDtNangCao').value;\r\n" +
                    "       dtTheoNhuCauXaHoi = document.getElementById('txtDtTheoNhuCauXaHoi').value;\r\n" +
                    "       dtVienTruong = document.getElementById('txtDtVienTruong').value;\r\n" +
                    "       chuongTrinhTaiLieu = document.getElementById('txtChuongTrinhTaiLieu').value;\r\n" +
                    "       thuanLoi = document.getElementById('txtThuanLoi').value;\r\n" +
                    "       khoKhan = document.getElementById('txtKhoKhan').value;\r\n" +
                    "       khacPhuc = document.getElementById('txtKhacPhuc').value;\r\n" +
                    "       phuongHuongKeHoach = document.getElementById('txtPhuongHuongKeHoach').value;\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.SaveDT_BcTongKetCongTacDaoTao(RenderInfo, id, nam, tuNgay, denNgay, dtLienTuc, dtTheoKeHoach, dtNangCao, dtTheoNhuCauXaHoi, dtVienTruong, chuongTrinhTaiLieu, thuanLoi, khoKhan, khacPhuc, phuongHuongKeHoach).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "       {\r\n" +
                    "           id = AjaxOut.RetExtraParam1;\r\n" +
                    "           document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value = id;\r\n" +
                    "           $('#btnDelete').show();\r\n" +
                    "           $('#divPrintButton').show();\r\n" +
                    "       }\r\n" +
                    "       toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã lưu") + ".');\r\n" +
                    "   }\r\n" +
                #endregion Thêm mới/Cập nhật phiếu đánh giá chất lượng đào tạo
                #region Xóa phiếu đánh giá chất lượng đào tạo
                    "function DeleteDT_BcTongKetCongTacDaoTao()\r\n" +
                    "{\r\n" +
                    "     id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.DeleteDT_BcTongKetCongTacDaoTao(RenderInfo, id).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     Clear();\r\n" +
                    "}\r\n" +
                #endregion Xóa phiếu đánh giá chất lượng đào tạo

                #region Show popup cập nhật đào tạo viện trường
                    "function ShowPopupDtVienTruong()\r\n" +
                    "{\r\n" +
                    "     id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.PopupDtVienTruong(RenderInfo, id).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật danh sách đào tạo phối hợp Viện-Trường") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "}\r\n" +
                #endregion Show popup cập nhật đào tạo viện trường
                #region Hiển thị row thêm mới đào tạo viện trường
                    "   function ShowAddItemLineDtvt(){\r\n" +
                    "       $('.CssEditorItemDtvt').hide();\r\n" +
                    "       $('.CssDisplayItemDtvt').show();\r\n" +
                    "       $('#trAddDtvt').show();\r\n" +
                    "   }\r\n" +
                #endregion
                #region Hiển thị row edit đào tạo viện trường
                    "   function ShowEditItemLineDtvt(rowIndex)\r\n" +
                    "   {\r\n" +
                    "       $('.CssEditorItemDtvt').hide();\r\n" +
                    "       $('.CssDisplayItemDtvt').show();\r\n" +
                    "       $('#trAddDtvt').hide();\r\n" +
                    "       document.getElementById('txtTruong'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('txtSoHocVien'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('ckNguyenTac'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('ckChiTiet'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('spTruong'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spSoHocVien'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spNguyenTac'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('spChiTiet'+rowIndex).style.display='none';\r\n" +

                    "       document.getElementById('btnSaveDaoTaoVienTruong'+rowIndex).style.display='block';\r\n" +
                    "       document.getElementById('btnEditDaoTaoVienTruong'+rowIndex).style.display='none';\r\n" +
                    "       document.getElementById('btnDeleteDaoTaoVienTruong'+rowIndex).style.display='none';\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật đào tạo viện trường
                    "   function SaveDaoTaoVienTruong(rowIndex){\r\n" +
                    "       id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "       daoTaoVienTruongId = document.getElementById('hdDaoTaoVienTruongId'+rowIndex).value;\r\n" +
                    "       truong = document.getElementById('txtTruong'+rowIndex).value;\r\n" +
                    "       soHocVien = parseInt(document.getElementById('txtSoHocVien'+rowIndex).value);\r\n" +
                    "       nguyenTac = document.getElementById('ckNguyenTac'+rowIndex).checked ? 1 : 0;\r\n" +
                    "       chiTiet = document.getElementById('ckChiTiet'+rowIndex).checked ? 1 : 0;\r\n" +
                    "       if(truong=='')\r\n" +
                    "       {\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa nhập tên trường.") + "');\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.SaveDaoTaoVienTruong(RenderInfo, id, daoTaoVienTruongId, truong, soHocVien, nguyenTac, chiTiet).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divUpdateDtVienTruongs').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa đào tạo viện trường
                    "   function DeleteDaoTaoVienTruong(rowIndex){\r\n" +
                    "       id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "       daoTaoVienTruongId = document.getElementById('hdDaoTaoVienTruongId'+rowIndex).value;\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.DeleteDaoTaoVienTruong(RenderInfo, id, daoTaoVienTruongId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       $('#divUpdateDtVienTruongs').html(AjaxOut.HtmlContent);\r\n" +
                    "   }\r\n" +
                # endregion
                #region Refresh danh sách phối hợp đào tạo Viện-Trường
                    "function ClosePopup()\r\n" +
                    "{\r\n" +
                    "     id = document.getElementById('hdDT_BcTongKetCongTacDaoTaoId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTao.DrawDtVienTruongs(RenderInfo, id, false).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     $('#divDtVienTruongs').html(AjaxOut.HtmlContent);\r\n" +
                    "}\r\n" +
                #endregion Refresh danh sách phối hợp đào tạo Viện-Trường
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                        "<style>\r\n" +
                        "   table, th, td {border: solid 1px;}\r\n" +
                        "   #divContent{font-family: 'Times New Roman', Times, serif;}\r\n" +
                        "</style>\r\n" +
                        "<center>\r\n" +
                        "<form action='javascript:SaveDT_BcTongKetCongTacDaoTao();'> \r\n" +
                        "     <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                        "       <input type='button' id='btnClear' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Mới") + "' onclick='javascript:Clear();' style='float:left;'>\r\n" + //" + (themPermission ? null : "display:none;") + "
                        "       <input type='submit' id = 'btnSave' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 20px;'>\r\n" +//" + (suaPermission ? null : "display:none;") + "
                        "       <input type='button' id='btnDelete' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteDT_BcTongKetCongTacDaoTao();' style='float:left; margin-left: 20px; " + (string.IsNullOrEmpty(bcTongKetCongTacDaoTaoId) /*|| !xoaPermission ? "display:none;" : null*/) + "'>\r\n" +
                        "       <div id='divPrintButton' style='margin-top:5px; float:right; padding:0; " + (string.IsNullOrEmpty(bcTongKetCongTacDaoTaoId) /*|| !xemPermission ? "display:none;" : null*/) + "' class='col-md-2' ></div> \r\n" +
                        "     </div> \r\n" +
                        //Phần hiển thị nội dung
                        "     <input type='hidden' id='hdDT_BcTongKetCongTacDaoTaoId' value='" + baoCao.ID + "'>\r\n" +
                        "     <div class=\"ibox-content\" id='divContent' style='width: 800px; padding: 50px; text-align:left;'> \r\n" +//set chiều rộng tương đương khổ giấy A4
                #region Tiêu đề báo cáo
                        "          <div class='row'>" +
                        "               <div class='col-md-5 col-xs-5'>\r\n" +
                        "                   <div class='form-group' style='text-align:center;'>" +
                        "                       <span>" + WebLanguage.GetLanguage(OSiteParam, "TRUNG TÂM ĐÀO TẠO") + "</span><br>\r\n" +
                        "                       <span>" + WebLanguage.GetLanguage(OSiteParam, "VÀ CHỈ ĐẠO TUYẾN") + "</span><br>\r\n" +
                        "                       <span style='font-weight:bold;'>" + WebLanguage.GetLanguage(OSiteParam, "PHÒNG ĐÀO TẠO") + "</span>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +
                        "               <div class='col-md-7 col-xs-7' style='text-align:center;'>\r\n" +
                        "                   <div class='form-group' style='font-weight:bold;'>" +
                        "                       <span>" + WebLanguage.GetLanguage(OSiteParam, "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM") + "</span><br>\r\n" +
                        "                       <span>" + WebLanguage.GetLanguage(OSiteParam, "Độc lập - Tự do - Hạnh phúc") + "</span><br>\r\n" +
                        "                   </div>\r\n" +
                        "                   <i style='margin-top: 10px;'>" + WebLanguage.GetLanguage(OSiteParam, "Hà Nội") + ", " + WebLanguage.GetLanguage(OSiteParam, "ngày") + " " + baoCao.NGAYTAO.Day + " " + WebLanguage.GetLanguage(OSiteParam, "tháng") + " " + baoCao.NGAYTAO.Month + " " + WebLanguage.GetLanguage(OSiteParam, "năm") + " " + baoCao.NGAYTAO.Year + "</i>\r\n" +
                        "               </div>\r\n" +
                        "          </div>\r\n" +
                        "          <div class='row' style='font-weight:bold; font-size: 20px; margin: 10px 0 20px 0; text-align: center; '>" +
                        "               <div class='col-md-12 col-xs-12'>\r\n" +
                                             WebLanguage.GetLanguage(OSiteParam, "BÁO CÁO TỔNG KẾT CÔNG TÁC ĐÀO TẠO NĂM") +
                        "                   <input type='text' data-mask='9999' class='form-control yearpicker' style='width:60px; display:inline;' id='dtNam' required onchange='if(this.value == \"\") {$(\"#spNamKeHoach\").html(\"\"); $(\"#dtTuNgay\").val(\"\"); $(\"#dtDenNgay\").val(\"\");} else {$(\"#spNamKeHoach\").html(parseInt(this.value) + 1); $(\"#dtTuNgay\").val(\"01/01\"); $(\"#dtDenNgay\").val(\"31/01\");} LoadReportByTime();' value='" + baoCao.NAM + "'>\r\n" +
                        "               </div>\r\n" +
                        "               <div class='col-md-12 col-xs-12' style='padding: 0 100px;'>\r\n" +
                                             WebLanguage.GetLanguage(OSiteParam, "PHƯƠNG HƯỚNG HOẠT ĐỘNG NĂM") +
                        "                   <span id='spNamKeHoach'>" + (baoCao.NAM + 1) + "</span><br>\r\n" +
                        "               </div>\r\n" +
                        "               <div class='col-md-12 col-xs-12' style='padding: 0 100px;'>\r\n" +
                        "                   (<input type='text' required style='width: 60px; display:inline;' class='form-control datepicker' id='dtTuNgay' onchange='LoadReportByTime()' data-mask='99/99' value='" + baoCao.TUNGAY.ToString("dd/MM") + "'>" +
                        "                    - <input type='text' required style='width: 60px; display:inline;' class='form-control datepicker' id='dtDenNgay' onchange='LoadReportByTime()' data-mask='99/99' value='" + baoCao.DENNGAY.ToString("dd/MM") + "'>)\r\n" +
                        "               </div>\r\n" +
                        "          </div>\r\n" +
                #endregion
                        "          <div id=\"divTongKetDaoTao\">\r\n" +
                                        DrawTongKetDaoTao(ORenderInfo, bcTongKetCongTacDaoTaoId, baoCao.NAM, baoCao.TUNGAY.ToString("dd/MM"), baoCao.DENNGAY.ToString("dd/MM"), isNewTime).HtmlContent +
                        "          </div>\r\n" +
                        "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0; text-align: center;'>" +
                        "               <div class='col-md-6 col-xs-6'>\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "PHÒNG ĐÀO TẠO") +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +
                        "               <div class='col-md-6 col-xs-6'>\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "LÃNH ĐẠO TRUNG TÂM") +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +
                        "          </div>\r\n" +
                        "     </div>\r\n" +
                        "</form> \r\n" +
                        "</center>\r\n" +
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
                        "</div> \r\n"
                #endregion
                        );
                #endregion

            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        #region Vẽ giao diện
        /// <summary>
        /// Nếu isNewTime = true thì load dữ liệu theo sự thay đổi của tuNgay/denNgay:
        /// - bcTongKetCongTacDaoTaoId = null thì load lại toàn bộ dữ liệu theo giá trị tuNgay/denNgay.
        /// - bcTongKetCongTacDaoTaoId != null thì chỉ load lại các grid theo giá trị tuNgay/denNgay.
        /// Nếu isNewTime = false thì load dữ liệu theo đúng báo cáo có id = bcTongKetCongTacDaoTaoId.
        /// </summary>
        /// <param name="ORenderInfo"></param>
        /// <param name="bcTongKetCongTacDaoTaoId"></param>
        /// <param name="tuNgay"></param>
        /// <param name="denNgay"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawTongKetDaoTao(RenderInfoCls ORenderInfo, string bcTongKetCongTacDaoTaoId, int nam, string tuNgay, string denNgay, bool isNewTime)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                WebSession.CheckSessionTimeOut(ORenderInfo);
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                DT_BcTongKetCongTacDaoTaoCls bcTongKetCongTacDaoTao;
                if (!string.IsNullOrEmpty(bcTongKetCongTacDaoTaoId))
                {
                    bcTongKetCongTacDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().CreateModel(ORenderInfo, bcTongKetCongTacDaoTaoId);
                    WebSessionUtility.SetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId, CallBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Reading(ORenderInfo, new DT_DaoTaoVienTruongFilterCls() { BCTONGKETCONGTACDAOTAO_ID = bcTongKetCongTacDaoTaoId }).ToList());
                }
                else
                {
                    bcTongKetCongTacDaoTao = new DT_BcTongKetCongTacDaoTaoCls();
                    WebSessionUtility.SetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId, new List<DT_DaoTaoVienTruongCls>());
                }
                if (isNewTime)
                {
                    bcTongKetCongTacDaoTao.NAM = nam;
                    bcTongKetCongTacDaoTao.TUNGAY = DateTime.ParseExact((string.IsNullOrEmpty(tuNgay) ? "01/01" : tuNgay) + "/" + nam, "dd/MM/yyyy", null);
                    bcTongKetCongTacDaoTao.DENNGAY = DateTime.ParseExact((string.IsNullOrEmpty(denNgay) ? "31/12" : denNgay) + "/" + nam, "dd/MM/yyyy", null);
                    string query = string.Format("select hv.ChuyenMon_Ma " +
                                        "   from DT_KHOAHOC kh inner join DT_KeHoachLop khl on khl.id = kh.id " +
                                        "   inner join DT_KetQuaDaoTao kqdt on kqdt.KHOAHOCDUYET_ID = kh.id " +
                                        "   inner join DT_HocVien hv on hv.id = kqdt.HOCVIEN_ID " +
                                        "   where kh.LOAIDAOTAO = {0} " +
                                        "       and ((kh.TRANGTHAI = {1} and TRY_CONVERT(DATETIME,'{3}') <= khl.BatDau and TRY_CONVERT(DATETIME,'{4}') > khl.BatDau) " +
                                        "           or (kh.TRANGTHAI = {2} and TRY_CONVERT(DATETIME,'{3}') <= khl.KetThuc and TRY_CONVERT(DATETIME,'{4}') > khl.KetThuc))  ",
                                        (int)DT_KhoaHocCls.eLoaiDaoTao.NangCaoNV,
                                        (int)DT_KhoaHocCls.eTrangThai.Duyet,
                                        (int)DT_KhoaHocCls.eTrangThai.KetThuc,
                                        bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"),
                                        bcTongKetCongTacDaoTao.DENNGAY.AddDays(1).ToString("dd/MM/yyyy"));
                    var dtResult = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ORenderInfo, query);
                    int recordTotal = dtResult.Rows.Count;
                    int soBacSy = 0, soYsDdKtv = 0;
                    for (int i = 0; i < recordTotal; i++)
                    {
                        string chuyenMonMa = CoreXmlUtility.GetString(dtResult.Rows[i], "ChuyenMon_Ma", true);
                        if (!string.IsNullOrEmpty(chuyenMonMa))
                        {
                            if (chuyenMonMa.Contains("BS"))
                                soBacSy++;
                            else if (chuyenMonMa.Contains("YS") || chuyenMonMa.Contains("ĐD") || chuyenMonMa.Contains("KTV"))
                                soYsDdKtv++;
                        }
                    }
                    long soKhoaDt = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Count(ORenderInfo, new DT_KhoaHocFilterCls() { TuNgay = bcTongKetCongTacDaoTao.TUNGAY, DenNgay = bcTongKetCongTacDaoTao.DENNGAY, TrangThai = (int)DT_KhoaHocCls.eTrangThai.KetThuc, LoaiDaoTao = (int)DT_KhoaHocCls.eLoaiDaoTao.KeHoach });
                    long soHocVienDt = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Count(ORenderInfo, new DT_KetQuaDaoTaoFilterCls() { TuNgay = bcTongKetCongTacDaoTao.TUNGAY, DenNgay = bcTongKetCongTacDaoTao.DENNGAY, TrangThaiKhoaHoc = (int)DT_KhoaHocCls.eTrangThai.KetThuc, LoaiDaoTao = (int)DT_KhoaHocCls.eLoaiDaoTao.KeHoach });
                    bcTongKetCongTacDaoTao.DTLIENTUC = string.Format(WebLanguage.GetLanguage(ORenderInfo, "\tThực hiện Thông tư 22/2013/TT - BYT ngày 09/8/2013 của Bộ Y tế về hướng dẫn công tác đào tạo liên tục trong lĩnh vực y tế. Từ {0} đến {1}, Trung tâm Đào tạo và Chỉ đạo tuyến đã triển khai nhiều hình thức đào tạo liên tục như: nâng cao nghiệp vụ, đào tạo theo nhu cầu xã hội, đào tạo, chuyển giao kỹ thuật theo các Đề án, Dự án…"), bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.ToString("dd/MM/yyyy"));
                    bcTongKetCongTacDaoTao.DTTHEOKEHOACH = string.Format(WebLanguage.GetLanguage(ORenderInfo, "\tBệnh VIỆN YHCT NGHỆ AN là trung tâm Ngoại khoa, Gây mê hồi sức và Chẩn đoán hình ảnh lớn của cả nước. Được sự cho phép của Bộ Y Tế, sự chỉ đạo sát sao của Ban giám đốc bệnh viện, trung tâm Đào tạo và Chỉ đạo tuyến không ngừng mở rộng các khóa đào tạo và từng bước nâng cao chất lượng đào tạo trong các lĩnh vực được giao nhiệm vụ.\n" +
                        "\tTừ {0} đến {1} Trung tâm tổ chức thành công {2} khóa đào tạo, tập huấn với {3} học viên cụ thể"), bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.ToString("dd/MM/yyyy"), soKhoaDt, soHocVienDt);
                    bcTongKetCongTacDaoTao.DTNANGCAO = string.Format(WebLanguage.GetLanguage(ORenderInfo, "- Với mục tiêu trang bị, cập nhật kiến thức, kỹ năng, phương pháp điều trị mới, tiên tiến cho các cán bộ y tế, Trung tâm Đào tạo và Chỉ đạo tuyến đã phối hợp với các chuyên khoa hoàn thiện tiêu chuẩn, nội dung thực hành và chỉ tiêu đầu ra cho từng đối tượng học viên các chuyên khoa.\n" +
                    "- Tăng cường, mở rộng đào tạo nâng cao nghiệp vụ cho các học viên tự do, các học viên của các cơ sở y tế tư nhân, công lập phục vụ cấp chứng chỉ hành nghề.\n" +
                    "- Từ {0} đến {1} Trung tâm đã tiếp nhận và đào tạo {2} học viên, gồm:\n" +
                    "\t+ Bác sỹ: {3}\n" +
                    "\t+ Y sỹ/Điều dưỡng/Kỹ thuật viên: {4}"), bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.ToString("dd/MM/yyyy"), soBacSy + soYsDdKtv, soBacSy, soYsDdKtv);
                    bcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI = WebLanguage.GetLanguage(ORenderInfo, "\tPhát huy thế mạnh của Bệnh viện là Trung tâm ngoại khoa hàng đầu cả nước, phát triển các kỹ thuật chuyên sâu, Trung tâm Đào tạo và Chỉ đạo tuyến đã tăng cường truyền thông, mở rộng các loại hình đào tạo, chuyển giao các gói dịch vụ kỹ thuật cho các đơn vị y tế có nhu cầu như:");
                    bcTongKetCongTacDaoTao.DTVIENTRUONG = WebLanguage.GetLanguage(ORenderInfo, "- Thực hiện Nghị định 111/2017/NĐ-CP ngày 05/10/2017 của Chính phủ Quy định về tổ chức đào tạo thực hành trong đào tạo khối ngành sức khỏe, Trung tâm đã rà soát, triển khai xây dựng nội dung giảng thực hành phù hợp với từng đối tượng, trình độ cũng như cập nhật, bổ sung đầy đủ các điều kiện… và được Cục Khoa học Công nghệ và Đào tạo, Bộ Y tế thẩm định, công nhận là đơn vị đủ điều kiện đáp ứng là cơ sở thực hành trong đào tạo khối ngành sức khỏe.\n" +
                        "- Tổ chức ký kết hợp đồng “Nguyên tắc” và “Chi tiết” với các cơ sở giáo dục khối ngành sức khỏe, gồm:");
                    bcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU = WebLanguage.GetLanguage(ORenderInfo, "\tHoạt động xây dựng chương trình, tài liệu đào tạo cũng như thẩm định tài liệu và hướng đến xuất bản sách luôn là một mục tiêu và nhiệm vụ quan trọng mà Trung tâm đặt ra nhằm từng bước chuẩn hóa, nâng cao chất lượng đào tạo hướng tới thực hiện đúng yêu cầu của Nghị định 111/2017/NĐ–CP ngày 05/10/2017 của Thủ tướng Chính phủ Hướng dẫn về tổ chức đào tạo thực hành trong đào tạo khối ngành sức khỏe và Thông tư 22/2013/TT-BYT ngày 09/8/2013 của Bộ Y tế về hướng dẫn đào tạo liên tục trong lĩnh vực y tế, cụ thể:\n" +
                        "\t- Rà soát, cập nhật chỉnh sửa bổ sung 150 chương trình, tài liệu.\n" +
                        "\t- Xây dựng mới 10 chương trình, tài liệu.\n" +
                        "\t- Tổ chức Hội đồng thẩm định cấp cơ sở chương trình, tài liệu “Tạo hình thẩm mỹ cơ bản”, “Chẩn đoán hình ảnh cơ bản”.");
                    bcTongKetCongTacDaoTao.THUANLOI = WebLanguage.GetLanguage(ORenderInfo, "- Nhận được sự chỉ đạo chặt chẽ của lãnh đạo Bộ Y tế và Cục khoa học đào tạo Bộ Y tế.\n" +
                    "- Ban giám đốc bệnh viện chỉ đạo trực tiếp mọi hoạt động của Trung tâm.\n" +
                    "- Bệnh viện là trung tâm lớn của cả nước về ngoại khoa, gây mê hồi sức và chẩn đoán hình ảnh, có đầy đủ trang thiết bị hỗ trợ tốt nhất cho công tác đào tạo chuyên môn.\n" +
                    "- Bệnh viện cũng là cơ sở thực hành của các cơ sở giáo dục Y – Dược, có đội ngũ các giảng viên ưu tú, các thầy thuốc đầu ngành trong nhiều lĩnh vực, có nhiều kinh nghiệm giảng dạy lý thuyết và lâm sàng.\n" +
                    "- Tập thể trung tâm đoàn kết, nỗ lực vì sự phát triển chung.\n");
                    bcTongKetCongTacDaoTao.KHOKHAN = WebLanguage.GetLanguage(ORenderInfo, "- Trung tâm Đào tạo và chỉ đạo tuyến đang trên con đường từng bước hoàn thiện và chuẩn hóa chức năng, nhiệm vụ. Do vậy trong quá trình hoạt động vẫn còn gặp nhiều khó khăn.\n" +
                    "- Nhân lực: Còn thiếu, mỗi thành viên trong trung tâm luôn phải cùng lúc phụ trách nhiều khóa đào tạo, nhiều công việc khác nhau, áp lực công việc lớn do đó không có nhiều thời gian nghiên cứu cải tiến phương pháp quản lý đào tạo để phù hợp nhất với học viên.\n");
                    bcTongKetCongTacDaoTao.KHACPHUC = WebLanguage.GetLanguage(ORenderInfo, "- Các thành viên không ngừng học hỏi, nâng cao trình độ quản lý tổ chức các lớp học.");
                    bcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH = WebLanguage.GetLanguage(ORenderInfo, "- Tiếp tục triển khai các hoạt động đào tạo theo Thông tư số 22/2013/TT-BYT ngày 09/8/2013 của Bộ Y tế và Thông tư số 26/2020/TT-BYT sửa đổi, bổ sung một số điều của Thông tư 22/2013/TT-BYT, tăng cường mở rộng đào tạo nâng cao nghiệp vụ và phát triển các chương trình đào tạo mới như chuyên khoa Giải phẫu bệnh, chuyên khoa Chẩn đoán hình ảnh, chuyên khoa sâu cho đối tượng điều dưỡng, kỹ thuật viên…\n" +
                        "- Đảm bảo 100% chương  trình, tài liệu đào tạo được thẩm định cấp cơ sở.\n" +
                        "- Triển khai thực hiện Nghị định 111/NĐ–TTg ngày 05/10/2017 của Thủ tướng Chính phủ về tổ chức đào tạo thực hành trong đào tạo khối ngành sức khỏe.\n" +
                        "- Mở rộng và phát triển các chương trình đào tạo mới bao gồm đào tạo trực tuyến./.");
                }
                string html =
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   I. " + WebLanguage.GetLanguage(OSiteParam, "KẾT QUẢ:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   1. " + WebLanguage.GetLanguage(OSiteParam, "Đào tạo liên tục:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtDtLienTuc'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.DTLIENTUC) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <i class='fa fa-star'></i> " + WebLanguage.GetLanguage(OSiteParam, "Đào tạo theo kế hoạch:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=10 class='form-control' id='txtDtTheoKeHoach'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.DTTHEOKEHOACH) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                                         DrawTongKetDaoTaoTheoKeHoachs(ORenderInfo, bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.AddDays(1).ToString("dd/MM/yyyy")).HtmlContent +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <i class='fa fa-star'></i> " + WebLanguage.GetLanguage(OSiteParam, "Đào tạo nâng cao nghiệp vụ:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtDtNangCao'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.DTNANGCAO) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <i class='fa fa-star'></i> " + string.Format(WebLanguage.GetLanguage(OSiteParam, "Đào tạo theo Đề án, Dự án và Hoạt động Chỉ đạo tuyến từ {0} đến {1}"), bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.ToString("dd/MM/yyyy")) +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                               DrawTkDtDeAnDuAnHdChiDaoTuyen(ORenderInfo, bcTongKetCongTacDaoTao.TUNGAY.ToString("dd/MM/yyyy"), bcTongKetCongTacDaoTao.DENNGAY.AddDays(1).ToString("dd/MM/yyyy")).HtmlContent +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <i class='fa fa-star'></i> " + WebLanguage.GetLanguage(OSiteParam, "Đào tạo theo nhu cầu xã hội:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtDtTheoNhuCauXaHoi'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   2. " + WebLanguage.GetLanguage(OSiteParam, "Đào tạo phối hợp Viện - Trường") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtDtVienTruong'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.DTVIENTRUONG) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <a class='clsUpdateChuyenGiaList' title='" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật danh sách đào tạo phối hợp Viện-Trường") + "' href='javascript:ShowPopupDtVienTruong();'>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</a>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' id='divDtVienTruongs'>" +
                                    DrawDtVienTruongs(ORenderInfo, bcTongKetCongTacDaoTaoId, false).HtmlContent +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   3. " + WebLanguage.GetLanguage(OSiteParam, "Chương trình, tài liệu đào tạo") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=10 class='form-control' id='txtChuongTrinhTaiLieu'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   II. " + WebLanguage.GetLanguage(OSiteParam, "THUẬN LỢI VÀ KHÓ KHĂN VÀ BIỆN PHÁP KHẮC PHỤC") +
                    "               </div>\r\n" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Thuận lợi:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtThuanLoi'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.THUANLOI) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Khó khăn:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtKhoKhan'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.KHOKHAN) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Biện pháp khắc phục:") +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=4 class='form-control' id='txtKhacPhuc'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.KHACPHUC) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   III. " + WebLanguage.GetLanguage(OSiteParam, "PHƯƠNG HƯỚNG VÀ KẾ HOẠCH HOẠT ĐỘNG NĂM") + " " + (bcTongKetCongTacDaoTao.NAM + 1) +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row'>" +
                    "               <div class='col-md-12 col-xs-12' style='margin-top: 20px; text-align: left;'>\r\n" +
                    "                   <div class=\"form-group\">\r\n" +
                                            string.Format("<textarea rows=8 class='form-control' id='txtPhuongHuongKeHoach'>{0}</textarea>\r\n", bcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH) +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "          </div>\r\n";
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
        public static AjaxOut DrawTongKetDaoTaoTheoKeHoachs(RenderInfoCls ORenderInfo, string tuNgay, string denNgay)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string query = string.Format("select nkh.TEN TenNhom, tkh.TEN TenKhoaHoc, kh.Khoa, khl.BatDau, khl.KetThuc, count(1) SoHocVien" +
                                            "   from DT_KHOAHOC kh inner join DM_TENKHOAHOC tkh on kh.TEN = tkh.MA " +
                                            "   left join DM_NHOMKHOAHOC nkh on nkh.MA = tkh.NHOMKHOAHOC_MA " +
                                            "   inner join DT_KeHoachLop khl on khl.id = kh.id " +
                                            "   inner join DT_KetQuaDaoTao kqdt on kqdt.KHOAHOCDUYET_ID = kh.id " +
                                            "   group by nkh.TEN, tkh.TEN, kh.KHOA, khl.BatDau, khl.KetThuc, kh.TRANGTHAI, kh.LOAIDAOTAO " +
                                            "   having kh.LOAIDAOTAO = {0} " +
                                            "       and ((kh.TRANGTHAI = {1} and TRY_CONVERT(DATETIME,'{3}') <= khl.BatDau and TRY_CONVERT(DATETIME,'{4}') > khl.BatDau) " +
                                            "           or (kh.TRANGTHAI = {2} and TRY_CONVERT(DATETIME,'{3}') <= khl.KetThuc and TRY_CONVERT(DATETIME,'{4}') > khl.KetThuc))  ",
                                            (int)DT_KhoaHocCls.eLoaiDaoTao.KeHoach,
                                            (int)DT_KhoaHocCls.eTrangThai.Duyet,
                                            (int)DT_KhoaHocCls.eTrangThai.KetThuc,
                                            tuNgay,
                                            denNgay);
                var dtResult = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ORenderInfo, query);
                int recordTotal = dtResult.Rows.Count;
                List<DaoTaoView> daoTaoLienTucs = new List<DaoTaoView>();
                for (int i = 0; i < recordTotal; i++)
                {
                    DaoTaoView daoTaoLienTuc = new DaoTaoView();
                    daoTaoLienTuc.TenNhom = CoreXmlUtility.GetString(dtResult.Rows[i], "TenNhom", true);
                    daoTaoLienTuc.TenKhoaHoc = CoreXmlUtility.GetString(dtResult.Rows[i], "TenKhoaHoc", true) + " - K" + CoreXmlUtility.GetString(dtResult.Rows[i], "Khoa", true);
                    daoTaoLienTuc.BatDau = CoreXmlUtility.GetDateOrNull(dtResult.Rows[i], "BatDau", true);
                    daoTaoLienTuc.KetThuc = CoreXmlUtility.GetDateOrNull(dtResult.Rows[i], "KetThuc", true);
                    daoTaoLienTuc.TongSoHocVien = CoreXmlUtility.GetInt(dtResult.Rows[i], "SoHocVien", true);
                    daoTaoLienTucs.Add(daoTaoLienTuc);
                }
                var nhomKhoaHocs = daoTaoLienTucs.Select(o => o.TenNhom).Distinct();
                string Html =
                    "         <div class=\"table-responsive col-xs-12\" style='margin-top: 5px;'> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khóa đào tạo") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + " </th> \r\n" +
                    "                     <th width=50>" + WebLanguage.GetLanguage(OSiteParam, "Số HV") + " </th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                foreach (var nhomKhoaHoc in nhomKhoaHocs)
                {
                    Html +=
                    "                 <tr> \r\n" +
                    "                     <td colspan = 5 style='font-weight: bold;'>" + nhomKhoaHoc + "</td> \r\n" +
                    "                 </tr> \r\n";
                    var khoaHocs = daoTaoLienTucs.Where(o => o.TenNhom == nhomKhoaHoc).ToArray();
                    int khoHocTotal = khoaHocs.Count();
                    for (int iIndex = 0; iIndex < khoHocTotal; iIndex++)
                    {
                        Html +=
                    "                 <tr style='font-weight: normal;'> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + khoaHocs[iIndex].TenKhoaHoc + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (khoaHocs[iIndex].BatDau == null ? null : khoaHocs[iIndex].BatDau.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (khoaHocs[iIndex].KetThuc == null ? null : khoaHocs[iIndex].KetThuc.Value.ToString("dd/MM/yyyy")) + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + khoaHocs[iIndex].TongSoHocVien + "</td> \r\n" +
                    "                 </tr> \r\n";
                    }
                }
                Html +=
                    "                 <tr> \r\n" +
                    "                     <td></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + ":</td> \r\n" +
                    "                     <td></td> \r\n" +
                    "                     <td></td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + daoTaoLienTucs.Sum(o => o.TongSoHocVien) + "</td> \r\n" +
                    "                 </tr> \r\n";
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
        public static AjaxOut DrawTkDtDeAnDuAnHdChiDaoTuyen(RenderInfoCls ORenderInfo, string tuNgay, string denNgay)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string query = string.Format("select tkh.TEN TenKhoaHoc, kh.Khoa, kh.LoaiDaoTao, count(1) SoHocVien" +
                                            "   from DT_KHOAHOC kh inner join DM_TENKHOAHOC tkh on kh.TEN = tkh.MA " +
                                            "   inner join DT_KeHoachLop khl on khl.id = kh.id " +
                                            "   inner join DT_KetQuaDaoTao kqdt on kqdt.KHOAHOCDUYET_ID = kh.id " +
                                            "   group by tkh.TEN, kh.KHOA, khl.KetThuc, kh.TRANGTHAI, kh.LOAIDAOTAO " +
                                            "   having (kh.LOAIDAOTAO = {0} or kh.LOAIDAOTAO = {1} or kh.LOAIDAOTAO = {2}) " +
                                            "       and ((kh.TRANGTHAI = {3} and TRY_CONVERT(DATETIME,'{5}', 'DD/MM/YYYY') <= khl.BatDau and TRY_CONVERT(DATETIME,'{6}', 'DD/MM/YYYY') > khl.BatDau) " +
                                            "           or (kh.TRANGTHAI = {4} and TRY_CONVERT(DATETIME,'{5}', 'DD/MM/YYYY') <= khl.KetThuc and TRY_CONVERT(DATETIME,'{6}', 'DD/MM/YYYY') > khl.KetThuc))  ",
                                            (int)DT_KhoaHocCls.eLoaiDaoTao.DeAn1816,
                                            (int)DT_KhoaHocCls.eLoaiDaoTao.DuAnBVVT,
                                            (int)DT_KhoaHocCls.eLoaiDaoTao.CongTacCDT,
                                            (int)DT_KhoaHocCls.eTrangThai.Duyet,
                                            (int)DT_KhoaHocCls.eTrangThai.KetThuc,
                                            tuNgay,
                                            denNgay);
                var dtResult = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ORenderInfo, query);
                int recordTotal = dtResult.Rows.Count;
                List<DaoTaoView> khoaHocs = new List<DaoTaoView>();
                for (int i = 0; i < recordTotal; i++)
                {
                    DaoTaoView khoaHoc = new DaoTaoView();
                    khoaHoc.TenKhoaHoc = CoreXmlUtility.GetString(dtResult.Rows[i], "TenKhoaHoc", true) + " - K" + CoreXmlUtility.GetString(dtResult.Rows[i], "Khoa", true);
                    khoaHoc.TongSoHocVien = CoreXmlUtility.GetInt(dtResult.Rows[i], "SoHocVien", true);
                    khoaHoc.LoaiDaoTao = CoreXmlUtility.GetInt(dtResult.Rows[i], "LoaiDaoTao", true);
                    khoaHocs.Add(khoaHoc);
                }
                #region Draw đào tạo theo đề án 1816
                DaoTaoView[] dtDeAn1816 = khoaHocs.Where(o => o.LoaiDaoTao == (int)DT_KhoaHocCls.eLoaiDaoTao.DeAn1816).ToArray();
                int dtDeAn1816Total = dtDeAn1816.Count();
                string Html =
                    "   <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "        <div class='col-md-12 col-xs-12'>\r\n" +
                                "* " + string.Format(WebLanguage.GetLanguage(OSiteParam, "Đề án 1816: {0} kỹ thuật, {1} học viên"), dtDeAn1816Total, dtDeAn1816.Sum(o => o.TongSoHocVien)) +
                    "        </div>\r\n" +
                    "   </div>\r\n" +
                    "   <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "       <div class='col-md-12 col-xs-12'>\r\n" +
                    "         <div class=\"table-responsive col-xs-12\" style='margin-top: 5px;'> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Gói kỹ thuật") + " </th> \r\n" +
                    "                     <th width=80px>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + " </th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < dtDeAn1816Total; iIndex++)
                {
                    Html +=
                    "                 <tr style='font-weight: normal;'> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                     <td style='vertical-align: middle;'>" + dtDeAn1816[iIndex].TenKhoaHoc + "</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + dtDeAn1816[iIndex].TongSoHocVien + "</td> \r\n" +
                    "                 </tr> \r\n";
                }
                Html +=
                    "                 <tr> \r\n" +
                    "                     <td colspan=2 style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + ":</td> \r\n" +
                    "                     <td style='text-align: center; vertical-align: middle;'>" + dtDeAn1816.Sum(o => o.TongSoHocVien) + "</td> \r\n" +
                    "                 </tr> \r\n";
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n" +
                    "          </div>\r\n";
                #endregion Draw đào tạo theo đề án 1816
                #region Draw đào tạo theo Dự án bệnh viện vệ tinh
                DaoTaoView[] dtDuAnBvvt = khoaHocs.Where(o => o.LoaiDaoTao == (int)DT_KhoaHocCls.eLoaiDaoTao.DuAnBVVT).ToArray();
                int dtDuAnBvvtTotal = dtDuAnBvvt.Count();
                Html +=
                    "  <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "       <div class='col-md-12 col-xs-12'>\r\n" +
                    "           * " + string.Format(WebLanguage.GetLanguage(OSiteParam, "Dự án bệnh viện vệ tinh: {0} kỹ thuật, {1} học viên"), dtDuAnBvvtTotal, dtDuAnBvvt.Sum(o => o.TongSoHocVien)) +
                    "       </div>\r\n" +
                    "  </div>\r\n" +
                    "  <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "       <div class='col-md-12 col-xs-12'>\r\n" +
                    "             <div class=\"table-responsive col-xs-12\" style='margin-top: 5px;'> \r\n" +
                    "                 <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                     <thead> \r\n" +
                    "                     <tr> \r\n" +
                    "                         <th class=\"th-func-20\">STT</th> \r\n" +
                    "                         <th>" + WebLanguage.GetLanguage(OSiteParam, "Gói kỹ thuật") + " </th> \r\n" +
                    "                         <th width=80px>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + " </th> \r\n" +
                    "                     </tr> \r\n" +
                    "                     </thead> \r\n" +
                    "                       <tbody> \r\n";
                for (int iIndex = 0; iIndex < dtDuAnBvvtTotal; iIndex++)
                {
                    Html +=
                    "                    <tr style='font-weight: normal;'> \r\n" +
                    "                        <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                        <td style='vertical-align: middle;'>" + dtDuAnBvvt[iIndex].TenKhoaHoc + "</td> \r\n" +
                    "                        <td style='text-align: center; vertical-align: middle;'>" + dtDuAnBvvt[iIndex].TongSoHocVien + "</td> \r\n" +
                    "                    </tr> \r\n";
                }
                Html +=
                    "                     <tr> \r\n" +
                    "                         <td colspan=2 style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + ":</td> \r\n" +
                    "                         <td style='text-align: center; vertical-align: middle;'>" + dtDuAnBvvt.Sum(o => o.TongSoHocVien) + "</td> \r\n" +
                    "                     </tr> \r\n";
                Html +=
                    "                     </tbody> \r\n" +
                    "                 </table> \r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n";
                #endregion Draw đào tạo theo Dự án bệnh viện vệ tinh
                #region Draw đào tạo theo Công tác Chỉ đạo tuyến
                DaoTaoView[] dtCongTacCdt = khoaHocs.Where(o => o.LoaiDaoTao == (int)DT_KhoaHocCls.eLoaiDaoTao.CongTacCDT).ToArray();
                int dtCongTacCdtTotal = dtCongTacCdt.Count();
                Html +=
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "               * " + string.Format(WebLanguage.GetLanguage(OSiteParam, "Công tác Chỉ đạo tuyến: {0} kỹ thuật, {1} học viên"), dtCongTacCdtTotal, dtCongTacCdt.Sum(o => o.TongSoHocVien)) +
                    "               </div>\r\n" +
                    "          </div>\r\n" +
                    "          <div class='row' style='font-weight:bold; margin: 10px 0 5px 0;'>" +
                    "               <div class='col-md-12 col-xs-12'>\r\n" +
                    "                   <div class=\"table-responsive col-xs-12\" style='margin-top: 5px;'> \r\n" +
                    "                       <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                           <thead> \r\n" +
                    "                           <tr> \r\n" +
                    "                               <th class=\"th-func-20\">STT</th> \r\n" +
                    "                               <th>" + WebLanguage.GetLanguage(OSiteParam, "Gói kỹ thuật") + " </th> \r\n" +
                    "                               <th width=80px>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + " </th> \r\n" +
                    "                           </tr> \r\n" +
                    "                           </thead> \r\n" +
                    "                           <tbody> \r\n";
                for (int iIndex = 0; iIndex < dtCongTacCdtTotal; iIndex++)
                {
                    Html +=
                    "                            <tr style='font-weight: normal;'> \r\n" +
                    "                                <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                    "                                <td style='vertical-align: middle;'>" + dtCongTacCdt[iIndex].TenKhoaHoc + "</td> \r\n" +
                    "                                <td style='text-align: center; vertical-align: middle;'>" + dtCongTacCdt[iIndex].TongSoHocVien + "</td> \r\n" +
                    "                            </tr> \r\n";
                }
                Html +=
                    "                            <tr> \r\n" +
                    "                                <td colspan=2 style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + ":</td> \r\n" +
                    "                                <td style='text-align: center; vertical-align: middle;'>" + dtCongTacCdt.Sum(o => o.TongSoHocVien) + "</td> \r\n" +
                    "                            </tr> \r\n";
                Html +=
                    "                            </tbody> \r\n" +
                    "                       </table> \r\n" +
                    "                </div>\r\n" +
                    "           </div>\r\n" +
                    "      </div>\r\n";
                #endregion Draw đào tạo theo Công tác Chỉ đạo tuyến
                Html +=
                    "<style>\r\n" +
                        "table th{text-align: center; vertical-align: middle;}\r\n" +
                    "</style>\r\n";
                Html = WebEnvironments.ProcessHtml(Html);
                RetAjaxOut.HtmlContent = Html;
                RetAjaxOut.RetObject = khoaHocs.Count();
                RetAjaxOut.RetObject1 = khoaHocs.Sum(o => o.TongSoHocVien);
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
        public static AjaxOut DrawDtVienTruongs(RenderInfoCls ORenderInfo, string bcTongKetCongTacDaoTaoId, bool isEditor)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_DaoTaoVienTruongCls> daoTaoVienTruongs = WebSessionUtility.GetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId) as List<DT_DaoTaoVienTruongCls>;
                int daoTaoVienTruongCount = daoTaoVienTruongs.Count();
                string html =
                    "             <table class=\"table table-bordered\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th rowspan=2 class=\"th-func-20\" style='vertical-align: middle;'>Stt</th> \r\n" +
                    "                     <th rowspan=2 style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Viện-Trường") + " </th> \r\n" +
                    "                     <th rowspan=2  style='text-align: center; vertical-align: middle; width: 80px;'>" + WebLanguage.GetLanguage(OSiteParam, "Số học viên") + "</th> \r\n" +
                    "                     <th colspan=2 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Hợp đồng") + "<br>" + WebLanguage.GetLanguage(OSiteParam, "Đào tạo thực hành") + " </th> \r\n" +
                    (isEditor ? "         <th rowspan=2 style='text-align:center; vertical-align: middle; width:60px;'><a href='javascript:ShowAddItemLineDtvt()' title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "' ><i class='fa fa-plus' style='font-size:24px; color: white;'></i></a></th> \r\n" : null) +
                    "                 </tr> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th style='text-align: center; width: 100px;'>" + WebLanguage.GetLanguage(OSiteParam, "Nguyên tắc") + " </th> \r\n" +
                    "                     <th style='text-align: center; width: 100px;'>" + WebLanguage.GetLanguage(OSiteParam, "Chi tiết") + "</th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                if (isEditor)
                {
                    html +=
                    "<tr id='trAddDtvt' style='display:none;'> \r\n" +
                        "<input type='hidden' id='hdDaoTaoVienTruongId' value=''>\r\n" +
                        "<td></td> \r\n" +
                        "<td><input type='text' class='form-control' id='txtTruong'></td> \r\n" +
                        "<td><input type='number' class='form-control number' step=1 id='txtSoHocVien'></td> \r\n" +
                        "<td><center><input type='checkbox' id='ckNguyenTac'></center></td> \r\n" +
                        "<td><center><input type='checkbox' id='ckChiTiet'></center></td> \r\n" +
                        "<td style='text-align:center; vertical-align: middle;'><a href='javascript:SaveDaoTaoVienTruong(\"\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i></a></td> \r\n" +
                    "</tr> \r\n";
                    for (int i = 0; i <= daoTaoVienTruongCount - 1; i++)
                    {
                        html += "<tr> \r\n" +
                                    "<input type='hidden' id='hdDaoTaoVienTruongId" + i + "' value='" + daoTaoVienTruongs[i].ID + "'>\r\n" +
                                    "<td style='text-align: center; vertical-align: middle;'>" + (i + 1) + "</td> \r\n" +
                                    "<td><input type='text' class='form-control CssEditorItemDtvt' style='display:none;' id='txtTruong" + i + "' value='" + daoTaoVienTruongs[i].TRUONG + "'><span class='CssDisplayItemDtvt' id='spTruong" + i + "'>" + daoTaoVienTruongs[i].TRUONG + "</span></td> \r\n" +
                                    "<td style='text-align: center; vertical-align: middle; color: red;'><input type='number' class='form-control CssEditorItemDtvt' style='display:none;' id='txtSoHocVien" + i + "' value='" + daoTaoVienTruongs[i].SOHOCVIEN + "'><span class='CssDisplayItemDtvt' id='spSoHocVien" + i + "'>" + daoTaoVienTruongs[i].SOHOCVIEN + "</span></td> \r\n" +
                                    "<td style='text-align: center; vertical-align: middle;'><center><input type='checkbox' class='CssEditorItemDtvt' style='display:none;' id='ckNguyenTac" + i + "' " + (daoTaoVienTruongs[i].NGUYENTAC == 1 ? "checked" : null) + "></center><span class='CssDisplayItemDtvt' id='spNguyenTac" + i + "'>" + (daoTaoVienTruongs[i].NGUYENTAC == 1 ? "x" : null) + "</span></td> \r\n" +
                                    "<td style='text-align: center; vertical-align: middle;'><center><input type='checkbox' class='CssEditorItemDtvt' style='display:none; ' id='ckChiTiet" + i + "' " + (daoTaoVienTruongs[i].CHITIET == 1 ? "checked" : null) + "></center><span class='CssDisplayItemDtvt' id='spChiTiet" + i + "'>" + (daoTaoVienTruongs[i].CHITIET == 1 ? "x" : null) + "</span></td> \r\n" +
                                    "<td style='text-align:center;'>\r\n" +
                                        "<a id='btnSaveDaoTaoVienTruong" + i + "' class='CssEditorItemLltct' style='display:none' href='javascript:SaveDaoTaoVienTruong(" + i + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' ><i class='fa fa-save' style='font-size:20px; margin-top:4px;'></i><span class='text-muted'></span></a>\r\n" +
                                        "<a id='btnEditDaoTaoVienTruong" + i + "' class='CssEditorItemLltct' href='javascript:ShowEditItemLineDtvt(" + i + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                                        "<a id='btnDeleteDaoTaoVienTruong" + i + "' class='CssEditorItemLltct' href='javascript:DeleteDaoTaoVienTruong(" + i + ");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                                    "</td> \r\n" +
                                "</tr> \r\n";
                    }
                }
                else
                {
                    for (int i = 0; i <= daoTaoVienTruongCount - 1; i++)
                    {
                        html +=
                            "                 <tr> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + (i + 1) + "</td> \r\n" +
                            "                     <td>" + daoTaoVienTruongs[i].TRUONG + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle; color: red;'>" + daoTaoVienTruongs[i].SOHOCVIEN + "</td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + (daoTaoVienTruongs[i].NGUYENTAC == 1 ? "x" : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + (daoTaoVienTruongs[i].CHITIET == 1 ? "x" : null) + "</a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                }
                html +=
                    "                 <tr style='font-weight:bold;'> \r\n" +
                    "                     <td colspan=2 style='text-align: center; vertical-align: middle;'>" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + "</td> \r\n" +
                    "                     <td style='text-align: center; color: red;'>" + daoTaoVienTruongs.Sum(o => o.SOHOCVIEN) + "</a></td> \r\n" +
                    "                     <td colspan=" + (isEditor ? 3 : 2) + "></td> \r\n" +
                    "                 </tr> \r\n";
                html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n";
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
        public static AjaxOut PopupDtVienTruong(RenderInfoCls ORenderInfo, string bcTongKetCongTacDaoTaoId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                List<DT_DaoTaoVienTruongCls> daoTaoVienTruongs = WebSessionUtility.GetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId) as List<DT_DaoTaoVienTruongCls>;
                int daoTaoVienTruongCount = daoTaoVienTruongs.Count();
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(
                "   <div id='divUpdateDtVienTruongs'>\r\n" +
                    DrawDtVienTruongs(ORenderInfo, bcTongKetCongTacDaoTaoId, true).HtmlContent +
                "   </div>\r\n"
                    );
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

        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetOwnerAddress(RenderInfoCls ORenderInfo, string ownerId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var benhVienThamVan = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, ownerId);
                if (benhVienThamVan == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bệnh viện đã bị xóa");
                    return RetAjaxOut;
                }
                RetAjaxOut.RetExtraParam1 = benhVienThamVan.OwnerAddress;
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
        public static AjaxOut SaveDT_BcTongKetCongTacDaoTao(RenderInfoCls ORenderInfo, string id, int nam, string tuNgay, string denNgay, string dtLienTuc, string dtTheoKeHoach, string dtNangCao, string dtTheoNhuCauXaHoi, string dtVienTruong, string chuongTrinhTaiLieu, string thuanLoi, string khoKhan, string khacPhuc, string phuongHuongKeHoach)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_BcTongKetCongTacDaoTaoCls bcTongKetCongTacDaoTao = new DT_BcTongKetCongTacDaoTaoCls();
                if (string.IsNullOrEmpty(id))
                {
                    bcTongKetCongTacDaoTao.ID = System.Guid.NewGuid().ToString();
                    bcTongKetCongTacDaoTao.NAM = nam;
                    bcTongKetCongTacDaoTao.TUNGAY = DateTime.ParseExact((string.IsNullOrEmpty(tuNgay) ? "01/01" : tuNgay) + "/" + nam, "dd/MM/yyyy", null);
                    bcTongKetCongTacDaoTao.DENNGAY = DateTime.ParseExact((string.IsNullOrEmpty(denNgay) ? "31/12" : denNgay) + "/" + nam, "dd/MM/yyyy", null);
                    bcTongKetCongTacDaoTao.DTLIENTUC = dtLienTuc;
                    bcTongKetCongTacDaoTao.DTTHEOKEHOACH = dtTheoKeHoach;
                    bcTongKetCongTacDaoTao.DTNANGCAO = dtNangCao;
                    bcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI = dtTheoNhuCauXaHoi;
                    bcTongKetCongTacDaoTao.DTVIENTRUONG = dtVienTruong;
                    bcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU = chuongTrinhTaiLieu;
                    bcTongKetCongTacDaoTao.THUANLOI = thuanLoi;
                    bcTongKetCongTacDaoTao.KHOKHAN = khoKhan;
                    bcTongKetCongTacDaoTao.KHACPHUC = khacPhuc;
                    bcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH = phuongHuongKeHoach;
                    bcTongKetCongTacDaoTao.NGAYTAO = DateTime.Now;
                    bcTongKetCongTacDaoTao.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Add(ORenderInfo, bcTongKetCongTacDaoTao);
                    RetAjaxOut.RetExtraParam1 = bcTongKetCongTacDaoTao.ID;
                }
                else
                {
                    bcTongKetCongTacDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().CreateModel(ORenderInfo, id);
                    bcTongKetCongTacDaoTao.NAM = nam;
                    bcTongKetCongTacDaoTao.TUNGAY = DateTime.ParseExact((string.IsNullOrEmpty(tuNgay) ? "01/01" : tuNgay) + "/" + nam, "dd/MM/yyyy", null);
                    bcTongKetCongTacDaoTao.DENNGAY = DateTime.ParseExact((string.IsNullOrEmpty(denNgay) ? "31/12" : denNgay) + "/" + nam, "dd/MM/yyyy", null);
                    bcTongKetCongTacDaoTao.DTLIENTUC = dtLienTuc;
                    bcTongKetCongTacDaoTao.DTTHEOKEHOACH = dtTheoKeHoach;
                    bcTongKetCongTacDaoTao.DTNANGCAO = dtNangCao;
                    bcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI = dtTheoNhuCauXaHoi;
                    bcTongKetCongTacDaoTao.DTVIENTRUONG = dtVienTruong;
                    bcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU = chuongTrinhTaiLieu;
                    bcTongKetCongTacDaoTao.THUANLOI = thuanLoi;
                    bcTongKetCongTacDaoTao.KHOKHAN = khoKhan;
                    bcTongKetCongTacDaoTao.KHACPHUC = khacPhuc;
                    bcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH = phuongHuongKeHoach;
                    bcTongKetCongTacDaoTao.NGAYSUA = DateTime.Now;
                    bcTongKetCongTacDaoTao.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Save(ORenderInfo, bcTongKetCongTacDaoTao.ID, bcTongKetCongTacDaoTao);
                }

                #region Cập nhật đào tạo viện trường
                List<DT_DaoTaoVienTruongCls> newDaoTaoVienTruongs = WebSessionUtility.GetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + id) as List<DT_DaoTaoVienTruongCls>;
                List<DT_DaoTaoVienTruongCls> oldDaoTaoVienTruongs = CallBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Reading(ORenderInfo, new DT_DaoTaoVienTruongFilterCls() { BCTONGKETCONGTACDAOTAO_ID = id }).ToList();
                foreach (var oldDaoTaoVienTruong in oldDaoTaoVienTruongs)
                {
                    bool isExists = false;
                    foreach (var newDaoTaoVienTruong in newDaoTaoVienTruongs)
                    {
                        if (newDaoTaoVienTruong.ID == oldDaoTaoVienTruong.ID)//cập nhật
                        {
                            CallBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Save(ORenderInfo, oldDaoTaoVienTruong.ID, newDaoTaoVienTruong);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)//Delete
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Delete(ORenderInfo, oldDaoTaoVienTruong.ID);
                    }
                }
                var addLichLyThuyetChiTiets = newDaoTaoVienTruongs.Where(o => !oldDaoTaoVienTruongs.Any(old => old.ID == o.ID));
                foreach (var addLichLyThuyetChiTiet in addLichLyThuyetChiTiets)//Thêm mới
                {
                    addLichLyThuyetChiTiet.BCTONGKETCONGTACDAOTAO_ID = bcTongKetCongTacDaoTao.ID;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Add(ORenderInfo, addLichLyThuyetChiTiet);
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
        public static AjaxOut DeleteDT_BcTongKetCongTacDaoTao(RenderInfoCls ORenderInfo, string id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Delete(ORenderInfo, id);
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
        public static AjaxOut SaveDaoTaoVienTruong(RenderInfoCls ORenderInfo, string bcTongKetCongTacDaoTaoId, string daoTaoVienTruongId, string truong, int? soHocVien, int? nguyenTac, int? chiTiet)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_DaoTaoVienTruongCls> daoTaoVienTruongs = WebSessionUtility.GetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId) as List<DT_DaoTaoVienTruongCls>;
                if (string.IsNullOrEmpty(daoTaoVienTruongId))//thêm mới
                {
                    daoTaoVienTruongs.Add(new DT_DaoTaoVienTruongCls()
                    {
                        ID = Guid.NewGuid().ToString(),
                        TRUONG = truong,
                        SOHOCVIEN = soHocVien,
                        NGUYENTAC = nguyenTac,
                        CHITIET = chiTiet
                    });
                }
                else//Cập nhật
                {
                    DT_DaoTaoVienTruongCls daoTaoVienTruong = daoTaoVienTruongs.FirstOrDefault(o => o.ID == daoTaoVienTruongId);
                    if (daoTaoVienTruong == null)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Trường cần cập nhật đã bị xóa bởi người dùng khác.");
                        return RetAjaxOut;
                    }
                    daoTaoVienTruong.TRUONG = truong;
                    daoTaoVienTruong.SOHOCVIEN = soHocVien;
                    daoTaoVienTruong.NGUYENTAC = nguyenTac;
                    daoTaoVienTruong.CHITIET = chiTiet;
                }
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu.");
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            RetAjaxOut.HtmlContent = DrawDtVienTruongs(ORenderInfo, bcTongKetCongTacDaoTaoId, true).HtmlContent;
            return RetAjaxOut;
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DeleteDaoTaoVienTruong(RenderInfoCls ORenderInfo, string bcTongKetCongTacDaoTaoId, string daoTaoVienTruongId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                List<DT_DaoTaoVienTruongCls> daoTaoVienTruongs = WebSessionUtility.GetSession(OSiteParam, "BCTKCTDaoTao_DtVienTruongs" + bcTongKetCongTacDaoTaoId) as List<DT_DaoTaoVienTruongCls>;

                DT_DaoTaoVienTruongCls daoTaoVienTruong = daoTaoVienTruongs.FirstOrDefault(o => o.ID == daoTaoVienTruongId);
                if (daoTaoVienTruong != null)
                {
                    daoTaoVienTruongs.Remove(daoTaoVienTruong);
                }
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            RetAjaxOut.HtmlContent = DrawDtVienTruongs(ORenderInfo, bcTongKetCongTacDaoTaoId, true).HtmlContent;
            return RetAjaxOut;
        }
        #endregion
    }
}
