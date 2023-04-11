﻿using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using System.IO;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.Net.Mail;
using System.Net;
using OneTSQ.Common;
using System.Collections.Generic;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class TuVanCaBenh : WebPartTemplate
    {
        public class SlideProperty
        {
            public int stt;
            public int type;
            public string value;
        }
        private enum eSlideType
        {
            Start = 0,
            ThongTinHanhChinh = 1,
            HoiBenh = 2,
            KhamBenhVaoVien = 3,
            ChanDoanCanLamSang = 4,
            XetNghiem = 5,
            ChanDoanHinhanh = 6,
            HinhAnh = 7,
            ChanDoanPhauThuat = 8,
            DieuTri = 9,
            CauHoi = 10,
            End = 11
        }
        public override string WebPartId
        {
            get
            {
                return "TuVanCaBenh";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Ca bệnh tư vấn";
            }
        }
        public override string Description
        {
            get
            {
                return "Ca bệnh tư vấn";
            }
        }
        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(TuVanCaBenh), Page);
        }
        public override bool NeedCheckPermission
        {
            get
            {
                return false;
            }
        }
        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string caBenhId = WebEnvironments.Request("CaBenhId");
            long recordTotal = long.Parse(WebEnvironments.Request("RecordTotal"));
            int chiSoMang = int.Parse(WebEnvironments.Request("ChiSoMang"));
            string keyword = WebEnvironments.Request("Keyword");
            string cskcbYeuCau = WebEnvironments.Request("CskcbYeuCau");
            string chuyenKhoa = WebEnvironments.Request("ChuyenKhoa");
            byte loaiThoiGian = Byte.Parse(WebEnvironments.Request("LoaiThoiGian"));
            string tuNgay = WebEnvironments.Request("TuNgay");
            string denNgay = WebEnvironments.Request("DenNgay");
            int trangThai = int.Parse(WebEnvironments.Request("TrangThai"));

            if (string.IsNullOrEmpty(caBenhId))
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn ca bệnh để xem.");
                return RetAjaxOut;
            }
            CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
            OwnerCls OOwner = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITHAMVANID);
            string lang = (string)WebEnvironments.Request("lang");
            if (string.IsNullOrEmpty(lang))
            {
                lang = "vi";
            }
            try
            {
                RetAjaxOut.HtmlContent =
                #region javascript
                    WebEnvironments.ProcessJavascript(
                    "<script language=javascript>\r\n" +
                    "   var currentSlideType;\r\n" +
                    "   var currentSlideIndex=0;\r\n" +
                    "   var slidePropertys;\r\n" +
                    "   var currentSlideIndex=0;\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Chi tiết ca bệnh tư vấn") + "';\r\n" +
                    "       GetSlideProperties();\r\n" +
                    "       $('#divSlide').on('shown.bs.modal', function() {\r\n" +//Sửa lỗi slide đầu tiên bị lệch (không fill được hết màn hình)
                                                                                   // do something when the modal is shown
                    "           $('#divSlide').css('padding',0);\r\n" +
                    "       });\r\n" +
                    "       ShowTrangThai();\r\n" +
                    "       $('#txtThongTinDieuTriView').summernote({\r\n" +
                    "         toolbar: [\r\n" +
                    "         ],\r\n" +
                    "         placeholder: 'Toolbar for font style...',\r\n" +
                    //Nếu hệ thống hiển thị tiếng việt, thì việt hóa phần màu nền màu chữ của control Summernote
                    (lang == "vi" ? "         lang: 'vi-VN'\r\n" : null) +
                    "       });\r\n" +
                    "       $('.note-editable').css('border', '1px solid #F5F5F5');\r\n" +
                    "   });\r\n" +
                    "   function OnWebPartLoad(){\r\n" +
                    "       return document.getElementById('hdCaBenhId').value;\r\n" +
                    "   }\r\n" +
                #region Show trạng thái ca bệnh
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.TuVanCaBenh.ShowTrangThai(RenderInfo, caBenhId).value);\r\n" +
                    "}\r\n" +
                #endregion Show trạng thái ca bệnh
                #region Mở tóm tắt bệnh án
                    "function TomTatBenhAn()\r\n" +
                    "{\r\n" +
                    "     caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.TuVanCaBenh.GetUrlTomTatBenhAn(RenderInfo, caBenhId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     window.location.href = AjaxOut.RetUrl;\r\n" +
                    "}\r\n" +
                #endregion

                #region Bắt sự kiện click row lịch hội chẩn
                    "   function LichHoiChanRow_OnClick(lichHoiChanRowId, lichHoiChanId)\r\n" +
                    "   {\r\n" +
                    "       $('#divLichHoiChansListContent .seletedRow').removeClass('seletedRow');\r\n" +
                    "       $('#' + lichHoiChanRowId).addClass('seletedRow');\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.TuVanCaBenh.DrawLichHoiChans_Detail(RenderInfo, caBenhId, lichHoiChanId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divLichHoiChansDetail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                #endregion

                #region Từ chối tiếp nhận ca bệnh
                    "function TuChoiTiepNhan()\r\n" +
                    "{\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn từ chối tiếp nhận ca bệnh này?") + "\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +
                    "               caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "               recordTotal = parseInt(document.getElementById('hdRecordTotal').value);\r\n" +
                    "               chiSoMang = parseInt(document.getElementById('hdChiSoMang').value) + 1;\r\n" +
                    "               keyword = document.getElementById('hdKeyword').value;\r\n" +
                    "               cskcbYeuCau = document.getElementById('hdCskcbYeuCau').value;\r\n" +
                    "               chuyenKhoaMa = document.getElementById('hdChuyenKhoaMa').value;\r\n" +
                    "               loaiThoiGian = parseInt(document.getElementById('hdLoai').value);\r\n" +
                    "               tuNgay = document.getElementById('hdTuNgay').value;\r\n" +
                    "               denNgay = document.getElementById('hdDenNgay').value;\r\n" +
                    "               trangThai = parseInt(document.getElementById('hdTrangThai').value);\r\n" +
                    "               butPhe = document.getElementById('txtButPhe').value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.TuVanCaBenh.CapNhatTrangThaiCaBenh(RenderInfo, caBenhId, recordTotal, chiSoMang, keyword, cskcbYeuCau, chuyenKhoaMa, trangThai, loaiThoiGian, tuNgay, denNgay, " + (int)Common.TuVanCaBenh.eTacVu.TuChoiTiepNhan + ", butPhe).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   swal.close();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Từ chối tiếp nhận ca bệnh thành công") + "');\r\n" +
                    //"                   window.location.href = AjaxOut.RetUrl;\r\n" +
                    "                   $('#btnTuChoiTiepNhan').hide();\r\n" +
                    "                   $('#btnChuyenLapLich').hide();\r\n" +
                    "                   $('#btnChuyenHoiChan').hide();\r\n" +
                    "                   $('#btnThuHoiTiepNhan').show();\r\n" +
                    "                   $('#txtButPhe').hide();\r\n" +
                    "                   if(butPhe != '')\r\n" +
                    "                       $('#divButPhe').html(AjaxOut.HtmlContent);\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   AddHistory(caBenhId, '" + WebPartTitle + "', 'Từ chối tiếp nhận', 'Tác vụ form');\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "}\r\n" +
                #endregion
                #region Chuyển lập lịch ca bệnh
                    "function ChuyenLapLich()\r\n" +
                    "{\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn chuyển lập lịch hội chẩn ca bệnh này?") + "\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +
                    "               caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "               recordTotal = parseInt(document.getElementById('hdRecordTotal').value);\r\n" +
                    "               chiSoMang = parseInt(document.getElementById('hdChiSoMang').value) + 1;\r\n" +
                    "               keyword = document.getElementById('hdKeyword').value;\r\n" +
                    "               cskcbYeuCau = document.getElementById('hdCskcbYeuCau').value;\r\n" +
                    "               chuyenKhoaMa = document.getElementById('hdChuyenKhoaMa').value;\r\n" +
                    "               loaiThoiGian = parseInt(document.getElementById('hdLoai').value);\r\n" +
                    "               tuNgay = document.getElementById('hdTuNgay').value;\r\n" +
                    "               denNgay = document.getElementById('hdDenNgay').value;\r\n" +
                    "               trangThai = parseInt(document.getElementById('hdTrangThai').value);\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.TuVanCaBenh.CapNhatTrangThaiCaBenh(RenderInfo, caBenhId, recordTotal, chiSoMang, keyword, cskcbYeuCau, chuyenKhoaMa, trangThai, loaiThoiGian, tuNgay, denNgay, " + (int)Common.TuVanCaBenh.eTacVu.ChuyenLapLich + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   swal.close();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Chuyển lập lịch hội chẩn thành công") + "' );\r\n" +
                    //"                   window.location.href = AjaxOut.RetUrl;\r\n" +
                    "                   $('#btnTuChoiTiepNhan').hide();\r\n" +
                    "                   $('#btnChuyenLapLich').hide();\r\n" +
                    "                   $('#btnChuyenHoiChan').hide();\r\n" +
                    "                   $('#btnThuHoiTiepNhan').show();\r\n" +
                    "                   $('#txtButPhe').hide();\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   AddHistory(caBenhId, '" + WebPartTitle + "', 'Chuyển lập lịch', 'Tác vụ form');\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "}\r\n" +
                #endregion
                #region Chuyển hội chẩn ca bệnh
                    "function ChuyenHoiChan()\r\n" +
                    "{\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn chuyển hội chẩn ca bệnh này?") + "\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +
                    "               caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "               recordTotal = parseInt(document.getElementById('hdRecordTotal').value);\r\n" +
                    "               chiSoMang = parseInt(document.getElementById('hdChiSoMang').value) + 1;\r\n" +
                    "               keyword = document.getElementById('hdKeyword').value;\r\n" +
                    "               cskcbYeuCau = document.getElementById('hdCskcbYeuCau').value;\r\n" +
                    "               chuyenKhoaMa = document.getElementById('hdChuyenKhoaMa').value;\r\n" +
                    "               loaiThoiGian = parseInt(document.getElementById('hdLoai').value);\r\n" +
                    "               tuNgay = document.getElementById('hdTuNgay').value;\r\n" +
                    "               denNgay = document.getElementById('hdDenNgay').value;\r\n" +
                    "               trangThai = parseInt(document.getElementById('hdTrangThai').value);\r\n" +
                    "               butPhe = document.getElementById('txtButPhe').value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.TuVanCaBenh.CapNhatTrangThaiCaBenh(RenderInfo, caBenhId, recordTotal, chiSoMang, keyword, cskcbYeuCau, chuyenKhoaMa, trangThai, loaiThoiGian, tuNgay, denNgay, " + (int)Common.TuVanCaBenh.eTacVu.ChuyenHoiChan + ", butPhe).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   swal.close();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Chuyển hội chẩn ca bệnh thành công") + "');\r\n" +
                    //"                   window.location.href = AjaxOut.RetUrl;\r\n" +
                    "                   $('#btnTuChoiTiepNhan').hide();\r\n" +
                    "                   $('#btnChuyenLapLich').hide();\r\n" +
                    "                   $('#btnChuyenHoiChan').hide();\r\n" +
                    "                   $('#btnThuHoiTiepNhan').show();\r\n" +
                    "                   if(butPhe != '')\r\n" +
                    "                       $('#divButPhe').html(AjaxOut.HtmlContent);\r\n" +
                    //Refresh danh sách lịch hội chẩn
                    "                   AjaxOut = OneTSQ.WebParts.TuVanCaBenh.DrawLichHoiChans_Tab(RenderInfo, caBenhId);\r\n" +
                    "                   if(!AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       $('#tab-2').html(AjaxOut.HtmlContent);\r\n" +
                    "                   }\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   AddHistory(caBenhId, '" + WebPartTitle + "', 'Chuyển hội chẩn', 'Tác vụ form');\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "}\r\n" +
                #endregion

                #region Thu hồi tiếp nhận
                    "function ThuHoi()\r\n" +
                    "{\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn thu hồi tiếp nhận ca bệnh này?") + "\", \r\n" +
                    //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +
                    "               caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "               recordTotal = parseInt(document.getElementById('hdRecordTotal').value);\r\n" +
                    "               chiSoMang = parseInt(document.getElementById('hdChiSoMang').value) + 1;\r\n" +
                    "               keyword = document.getElementById('hdKeyword').value;\r\n" +
                    "               cskcbYeuCau = document.getElementById('hdCskcbYeuCau').value;\r\n" +
                    "               chuyenKhoaMa = document.getElementById('hdChuyenKhoaMa').value;\r\n" +
                    "               loaiThoiGian = parseInt(document.getElementById('hdLoai').value);\r\n" +
                    "               tuNgay = document.getElementById('hdTuNgay').value;\r\n" +
                    "               denNgay = document.getElementById('hdDenNgay').value;\r\n" +
                    "               trangThai = parseInt(document.getElementById('hdTrangThai').value);\r\n" +
                    "               butPhe = document.getElementById('txtButPhe').value;\r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.TuVanCaBenh.CapNhatTrangThaiCaBenh(RenderInfo, caBenhId, recordTotal, chiSoMang, keyword, cskcbYeuCau, chuyenKhoaMa, trangThai, loaiThoiGian, tuNgay, denNgay, " + (int)Common.TuVanCaBenh.eTacVu.ThuHoiTiepNhan + ", butPhe).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   swal.close();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi tiếp nhận ca bệnh thành công") + "');\r\n" +
                    //"                   window.location.href = AjaxOut.RetUrl;\r\n" +
                    "                   $('#btnTuChoiTiepNhan').show();\r\n" +
                    "                   $('#btnChuyenLapLich').show();\r\n" +
                    "                   $('#btnChuyenHoiChan').show();\r\n" +
                    "                   $('#btnThuHoiTiepNhan').hide();\r\n" +
                    "                   if(butPhe != '')\r\n" +
                    "                       $('#divButPhe').html(AjaxOut.HtmlContent);\r\n" +
                    //Refresh danh sách lịch hội chẩn
                    "                   AjaxOut = OneTSQ.WebParts.TuVanCaBenh.DrawLichHoiChans_Tab(RenderInfo, caBenhId);\r\n" +
                    "                   if(!AjaxOut.Error)\r\n" +
                    "                   {\r\n" +
                    "                       $('#tab-2').html(AjaxOut.HtmlContent);\r\n" +
                    "                   }\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "                   AddHistory(caBenhId, '" + WebPartTitle + "', 'Thu hồi tiếp nhận', 'Tác vụ form');\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "}\r\n" +
                #endregion

                #region Trình chiếu
                    "function ShowSlide(slideIndex)\r\n" +
                    "{\r\n" +
                    "     currentSlideIndex=slideIndex;\r\n" +
                    "     caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.TuVanCaBenh.PopupSlide(RenderInfo, caBenhId, slideIndex, slidePropertys).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divSlideContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     $('#divSlide').modal('show');\r\n" +
                    "     start = new Date();\r\n" +
                    "     $('#divSlide').css('padding',0);\r\n" +
                    "     document.addEventListener('keydown', Slide_OnKeyDown);\r\n" +
                    "}\r\n" +
                "function HideSlide()\r\n" +
                    "{\r\n" +
                    "     $('#divSlide').modal('hide');\r\n" +
                    "     $('#divSlideContent').html('');\r\n" +
                    "     document.removeEventListener(\"keydown\", Slide_OnKeyDown);\r\n" +
                    "}\r\n" +
                #endregion
                #region Bắt sự kiện keydown chuyển slide
                "function Slide_OnKeyDown(keyboardEvent)\r\n" +
                    "{\r\n" +
                    "     if(keyboardEvent.key == 'ArrowLeft' || keyboardEvent.key == 'ArrowUp')\r\n" +
                    "     {\r\n" +
                    "       if(currentSlideIndex > 0){\r\n" +
                    "           ShowSlide(currentSlideIndex-1)\r\n" +
                    "       }\r\n" +
                    "     }\r\n" +
                    "     else if(keyboardEvent.key == 'ArrowRight' || keyboardEvent.key == 'ArrowDown')\r\n" +
                    "     {\r\n" +
                    "       if(currentSlideIndex < slidePropertys.length-1){\r\n" +
                    "           ShowSlide(currentSlideIndex+1)\r\n" +
                    "       }\r\n" +
                    "     }\r\n" +
                    "     else if(keyboardEvent.key == 'Escape')\r\n" +
                    "     {\r\n" +
                    "       HideSlide();\r\n" +
                    "     }\r\n" +
                    "}\r\n" +
                #endregion
                #region Lấy về thuộc tính của slide
                "function GetSlideProperties()\r\n" +
                    "{\r\n" +
                    "     caBenhId = document.getElementById('hdCaBenhId').value;\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.TuVanCaBenh.GetSlideProperties(RenderInfo, caBenhId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     slidePropertys = AjaxOut.RetObject;\r\n" +
                    "}\r\n" +
                #endregion Lấy về thuộc tính của slide
                #region Bắt sự kiện click row kết quả xét nghiệm trên slide 
                    "function SlideKetQuaXNRow_OnClick(ketQuaXNRowId, ketQuaXetNghiemId)\r\n" +
                    "{\r\n" +
                    "   $('#divSlideKetQuaXetNghiem .seletedRow').removeClass('seletedRow');\r\n" +
                    "   $('#' + ketQuaXNRowId).addClass('seletedRow');\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "   AjaxOut = OneTSQ.WebParts.TuVanCaBenh.DrawSlideKetQuaXetNghiemChiTiets(RenderInfo,ketQuaXetNghiemId).value;\r\n" +
                    "   if(AjaxOut.Error)\r\n" +
                    "   {\r\n" +
                    "       callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "       return;\r\n" +
                    "   }\r\n" +
                    "   $('#divSlideKetQuaXetNghiemChiTiet').html(AjaxOut.HtmlContent);\r\n" +
                    "}\r\n" +
                #endregion
                #region Show popup chỉ số xét nghiệm
                    "function ShowPopupChiSoXetNghiem(ketQuaXetNghiemId, tenDichVu)\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.TuVanCaBenh.DrawKetQuaXetNghiemChiTiets(RenderInfo, ketQuaXetNghiemId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContentCsxn').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitleCsxn').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Kết quả") + " ' + tenDichVu + '</span>';\r\n" +
                    "     $('#divFormModalCsxn').modal('show');\r\n" +
                    "}\r\n" +
                #endregion Show popup chỉ số xét nghiệm
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                        "<div id=\"divCaBenh\">\r\n" +
                         DrawCaBenh(ORenderInfo, caBenhId, recordTotal, chiSoMang, keyword, cskcbYeuCau, chuyenKhoa, trangThai < 0 ? null : (int?)trangThai, loaiThoiGian, tuNgay, denNgay).HtmlContent +
                        "</div>\r\n" +
                #region Phần hiển thị popup hình ảnh
                        "<div id=\"divFormModal\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                        "    <div class=\"modal-dialog\" style='width:90%; height:90%;'>\r\n" +
                        "       <div class=\"modal-content\" style='width:100%; height:100%; position: relative;'>\r\n" +
                        "           <div class=\"panel-heading\">\r\n" +
                        "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\" >&times;</button>\r\n" +
                        "               <h2 class=\"modal-title\" id=\"ModalTitle\"></h2>\r\n" +
                        "           </div> \r\n" +
                        "           <div class=\"modal-body\" id=\"divModalContent\" style='width:100%; padding-top:0; position: absolute; top:50px; bottom:0px;'></div> \r\n" +
                        "        </div> \r\n" +
                        "    </div> \r\n" +
                        "</div> \r\n" +
                #endregion
                #region Phần hiển thị popup Slide
                        "<div id=\"divSlide\" class=\"modal fade\" style=\"overflow: hidden; padding-right:0 !important;\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                        "    <div class=\"modal-dialog\" style=\"width:100%; height:100%; margin:0; padding:0; background: url('/themes/img/cabenh/back_ground_slide_VietDuc.png') no-repeat bottom fixed; -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover;\">\r\n" +
                        "       <div class=\"modal-content\" style=\"width:100%; height:100%; background: url('/themes/img/cabenh/LogoBV.jpg') no-repeat top left fixed;\">\r\n" +
                        "           <div class=\"modal-content\" id=\"divSlideContent\" style=\"width:100%; height:100%; position: relative; no-repeat top right fixed;\">\r\n" +// background: url('" + (OOwner != null && !string.IsNullOrEmpty(GetOOwner(OOwner).LOGO) ? OOwner.LOGO : null) + "')
                        "           </div> \r\n" +
                        "        </div> \r\n" +
                        "    </div> \r\n" +
                        "</div> \r\n" +
                #endregion
                #region Phần hiển thị popup chỉ số xét nghiệm
                        "<div id=\"divFormModalCsxn\" class=\"modal fade\" style=\"overflow: hidden\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                        "    <div class=\"modal-dialog\" style='width:90%; height:90%; font-size: 20px;'>\r\n" +
                        "       <div class=\"modal-content\" style='width:100%; height:100%; position: relative;'>\r\n" +
                        "           <div class=\"panel-heading\">\r\n" +
                        "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\" onclick='ClosePopup();'>&times;</button>\r\n" +
                        "               <h2 class=\"modal-title\" id=\"ModalTitleCsxn\"></h2>\r\n" +
                        "           </div> \r\n" +
                        "           <div class=\"modal-body\" id=\"divModalContentCsxn\" style='width:100%; padding-top:0; position: absolute; top:50px; bottom:0px; overflow-y: auto;'></div> \r\n" +
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

        private static OwnerCls GetOOwner(OwnerCls OOwner)
        {
            return OOwner;
        }
        #region Vẽ giao diện
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawCaBenh(RenderInfoCls ORenderInfo, string caBenhId, long recordTotal, int chiSoMang, string keyword, string cskcbYeuCau, string chuyenKhoaMa, int? trangThai, byte loaiThoiGian, string tuNgay, string denNgay)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string userId = user.OwnerUserId;
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã có ca bệnh bị xóa khỏi hệ thống.");
                    return RetAjaxOut;
                }
                //bool beLongOwner = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().ReadingOwners(ORenderInfo, userId).Any(o => o.OwnerId == caBenh.DONVITUVANID);
                //bool tiepNhanPermission = (beLongOwner && PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, Common.TuVanCaBenh.ePermission.TiepNhan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId))
                //    || PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, Common.TuVanCaBenh.ePermission.TiepNhan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId, caBenh.TAOBOI);
                //bool duyetHcPermission = (beLongOwner && PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, Common.TuVanCaBenh.ePermission.DuyetHoiChan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId))
                //    || PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, Common.TuVanCaBenh.ePermission.DuyetHoiChan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId, caBenh.TAOBOI);
                OwnerCls donViThamVan = string.IsNullOrEmpty(caBenh.DONVITHAMVANID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITHAMVANID);
                OwnerCls donViTuVan = string.IsNullOrEmpty(caBenh.DONVITUVANID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITUVANID);
                DM_ChuyenKhoaDaoTaoTtCls chuyenKhoa = null;
                if (!string.IsNullOrEmpty(caBenh.CHUYENKHOAMA))
                {
                    chuyenKhoa = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, caBenh.CHUYENKHOAMA);
                }

                bool isChuTri = false, isThuKy = false;
                //Lấy về biên bản được lập sau khi duyệt hội chẩn (thành phần tham dự hội chẩn thực tế)
                BienBanHoiChanCls bienBanHoiChanHienHanh = CallBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Reading(ORenderInfo, new BienBanHoiChanFilterCls() { CABENHID = caBenh.ID }).FirstOrDefault(o => o.TAOVAO > caBenh.DUYETHOICHANVAO);
                if (bienBanHoiChanHienHanh != null)
                {
                    if (CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bienBanHoiChanHienHanh.CHUTRIHOICHAN, OWNERUSERID = user.OwnerUserId }).Any())
                        isChuTri = true;
                    if (CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bienBanHoiChanHienHanh.THUKY, OWNERUSERID = user.OwnerUserId }).Any())
                        isThuKy = true;
                }
                else//Nếu không tồn tại biên bản thì lấy lịch hội chẩn đang được áp dụng (thành phần tham dự hội chẩn theo kế hoạch)
                {
                    LichHoiChanCls lichHoiChanHienHanh = string.IsNullOrEmpty(caBenh.LICHHOICHANID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().CreateModel(ORenderInfo, caBenh.LICHHOICHANID);
                    if (lichHoiChanHienHanh != null)
                    {
                        if (CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = lichHoiChanHienHanh.CHUTRI, OWNERUSERID = user.OwnerUserId }).Any())
                            isChuTri = true;
                        if (CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = lichHoiChanHienHanh.THUKY, OWNERUSERID = user.OwnerUserId }).Any())
                            isThuKy = true;
                    }
                }
                var caBenhsUrl = WebScreen.BuildUrl(OSiteParam, user.OwnerCode, "TuVanCaBenhs", new WebParamCls[] {
                    new WebParamCls("PageIndex", chiSoMang/20),
                    new WebParamCls("Keyword", keyword),
                    new WebParamCls("CskcbYeuCau", cskcbYeuCau),
                    new WebParamCls("ChuyenKhoa", chuyenKhoaMa),
                    new WebParamCls("LoaiThoiGian", loaiThoiGian),
                    new WebParamCls("TuNgay", tuNgay),
                    new WebParamCls("DenNgay", denNgay),
                    new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                var imageListUrl = WebScreen.BuildUrl(OSiteParam, user.OwnerCode, "HinhAnhs", new WebParamCls[] { new WebParamCls("CaBenhId", caBenh.ID) });
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string sturl =
                    "   <div class='row'>\r\n" +
                    "       <div style='float:left; padding:0; margin-top: 5px; margin-left: 10px;' class='col-sx-12'> \r\n" +
                    "           <ul class='ul-css' style='list-style:none;padding: 0;'> \r\n" +
                    "               <li><a class='btn btn-primary' href='" + caBenhsUrl + "'><i class='fa fa-mail-reply-all' style='font-size:18px;'></i> " + WebLanguage.GetLanguage(OSiteParam, "Về danh sách") + "</a></li> \r\n" +
                    "               <li id='btnTuChoiTiepNhan' " + (caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoTiepNhan ? "style='display: none;'" : null) + " ><a class='btn btn-primary' href='javascript:TuChoiTiepNhan();'><i class='fa fa-mail-reply-all' style='font-size:18px;'></i>" + WebLanguage.GetLanguage(OSiteParam, "Từ chối tiếp nhận") + "</a></li> \r\n" + //|| !tiepNhanPermission
                    "               <li id='btnChuyenLapLich' " + (caBenh.CAPCUU == 1 || (caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoTiepNhan && caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.KetThuc) ? "style='display: none;'" : null) + " ><a class='btn btn-primary' href='javascript:ChuyenLapLich();'><i class='fa fa-mail-forward' style='font-size:24px;'></i>" + WebLanguage.GetLanguage(OSiteParam, "Chuyển lập lịch") + " </a></li> \r\n" +
                    "               <li id='btnChuyenHoiChan' " + ((caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoTiepNhan && caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.KetThuc) ? "style='display: none;'" : null) + " ><a class='btn btn-primary' href='javascript:ChuyenHoiChan();'><i class='fa fa-mail-forward' style='font-size:24px;'></i>" + WebLanguage.GetLanguage(OSiteParam, "Chuyển hội chẩn") + " </a></li> \r\n" +
                    "               <li id='btnThuHoiTiepNhan' " + ((caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoLapLich && caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoHoiChan) ? "style='display: none;'" : null) + " ><a class='btn btn-primary' href='javascript:ThuHoi();'><i class='fa fa-mail-reply' style='font-size:24px;'></i>" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + " </a></li> \r\n" +
                    "               <li><a class='btn btn-primary' href='javascript:ShowSlide(0);'><i class='fa fa-desktop' style='font-size:15px;'></i> " + WebLanguage.GetLanguage(OSiteParam, "Show Slide") + "</a></li> \r\n" +
                    "           </ul> \r\n" +
                    "       </div>\r\n" +
                    "     </div> \r\n" +
                    "<style>\r\n" +
                    "   ul.ul-css li{display:inline-block; padding:0 7px;}\r\n" +
                    "</style>\r\n"
                ;

                string html = "     <div id=\"divTopButton\"> \r\n" +
                    sturl +
                    "     </div> \r\n" +
                //Phần hiển thị nội dung
                "   <input type='hidden' id='hdCaBenhId' value='" + caBenh.ID + "'>\r\n" +
                    "   <input type='hidden' id='hdRecordTotal' value='" + recordTotal + "'>\r\n" +
                    "   <input type='hidden' id='hdChiSoMang' value='" + chiSoMang + "'>\r\n" +
                    "   <input type='hidden' id='hdKeyword' value='" + keyword + "'>\r\n" +
                    "   <input type='hidden' id='hdCskcbYeuCau' value='" + cskcbYeuCau + "'>\r\n" +
                    "   <input type='hidden' id='hdChuyenKhoaMa' value='" + chuyenKhoaMa + "'>\r\n" +
                    "   <input type='hidden' id='hdLoai' value=" + loaiThoiGian + ">\r\n" +
                    "   <input type='hidden' id='hdTuNgay' value='" + tuNgay + "'>\r\n" +
                    "   <input type='hidden' id='hdDenNgay' value='" + denNgay + "'>\r\n" +
                    "   <input type='hidden' id='hdTrangThai' value='" + trangThai + "'>\r\n" +
                    "     <div id='divContent'> \r\n" +
                            "<div class='tabs-container'>\r\n" +
                            "    <ul class='nav nav-tabs'>\r\n" +
                            "        <li class='active' " + (isThuKy ? "onclick='$(\".caBenhSave\").hide();'" : null) + "><a data-toggle='tab' href='#tab-1'>" + WebLanguage.GetLanguage(OSiteParam, "Ca bệnh") + "</a></li>\r\n" +
                            "        <li class=''><a data-toggle='tab' href='#tab-2'>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách lịch hội chẩn") + "</a></li>\r\n" +
                            "        <li class=''><a data-toggle='tab' href='#tab-3'>" + WebLanguage.GetLanguage(OSiteParam, "Bút phê") + "</a></li>\r\n" +
                            "    </ul>\r\n" +
                            "    <div class='tab-content' style='overflow-y:auto;'>\r\n" +
                            "        <div id='tab-1' class='tab-pane active'>\r\n" +
                #region Nội dung ca bệnh
                #region Thông tin hội chẩn
                         " <div class=\"ibox float-e-margins\"> \r\n" +
                         "     <div class=\"ibox-title\" style='background-color:#4682B4;'> \r\n" +
                         "         <h5 style='color:white;'>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin hội chẩn") + "</h5> \r\n" +
                         "     </div> \r\n" +
                         "     <div class=\"ibox-content\"> \r\n" +
                         "          <div class='row'>" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<div class='form-group'>" +
                                                WebLanguage.GetLanguage(OSiteParam, "Loại hội chẩn:") + "\r\n" +
                                                "<span id='spLoaiHoiChan' class=\"valueString\">" + CaBenhParser.LoaiHoichans[caBenh.LOAIHOICHAN] + "</span>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<input type=\"checkbox\" id='ckbCapCuuView' disabled style='display: inline;' " + (caBenh.CAPCUU == 1 ? "checked" : "") + "> Cấp cứu\r\n" +
                                        "</div>\r\n" +
                         "          </div>\r\n" +
                         "          <div class='row'>" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Đơn vị tham vấn:") + " \r\n" +
                                                "<span id='spCskcbYeuCau' class=\"valueString\">" + (donViThamVan == null ? null : donViThamVan.OwnerName) + "</span>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Đơn vị tư vấn:") + " \r\n" +
                                                "<span id='spCskcbTuVan' class=\"valueString\">" + (donViTuVan == null ? null : donViTuVan.OwnerName) + "</span>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa:") + "\r\n" +
                                                "<span id='spChuyenKhoa' class=\"valueString\">" + (chuyenKhoa == null ? caBenh.CHUYENKHOAMA : chuyenKhoa.Ten) + "</span>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class='col-md-3 col-xs-6'>\r\n" +
                                            "<div class=\"form-group\">\r\n" +
                                                WebLanguage.GetLanguage(OSiteParam, "Ngày HC mong muốn:") + " \r\n" +
                                                "<span id='spNgayHoiChanDeNghi' class=\"valueString\">" + (caBenh.NGAYHOICHANDENGHI == null ? null : caBenh.NGAYHOICHANDENGHI.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                        "           </div>\r\n" +
                        "       </div>\r\n" +
                        "   </div>\r\n" +
                #endregion
                #region Thông tin ca bệnh
                         " <div class=\"ibox float-e-margins\"> \r\n" +
                         "     <div class=\"ibox-title\" style='background-color:#4682B4;'> \r\n" +
                         "         <h5 style='color:white;'>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin ca bệnh") + "</h5> \r\n" +
                         "     </div> \r\n" +
                         "     <div class=\"ibox-content\" id = 'divCaBenhContent'> \r\n" +
                                    DrawCaBenhContent(ORenderInfo, caBenhId).HtmlContent +
                        "      </div>\r\n" +
                        "  </div>\r\n" +
                #endregion

                #endregion
                                "        </div>\r\n" +
                            "        <div id='tab-2' class='tab-pane'>\r\n" +
                #region Danh sách lịch hội chẩn
                                            DrawLichHoiChans_Tab(ORenderInfo, caBenh.ID).HtmlContent +
                #endregion
                                "        </div>\r\n" +
                            "        <div id='tab-3' class='tab-pane'>\r\n" +
                #region Bút phê
                                            "<textarea id='txtButPhe' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Nhập bút phê tại đây!") + "' rows=4 style='width:100%; " + (caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.ChoTiepNhan && caBenh.TRANGTHAI != (int)CaBenhCls.eTrangThai.DaLapLich ? "display: none;" : null) + "'></textarea>\r\n" +
                                        "<div id='divButPhe' width='100%'>\r\n" +
                                        DrawButPheList(ORenderInfo, caBenh.ID).HtmlContent +
                                        "</div>\r\n" +
                #endregion
                                "        </div>\r\n" +
                            "    </div>\r\n" +
                            "</div>\r\n" +
                    "     </div> \r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
                RetAjaxOut.RetExtraParam1 = caBenh.TRANGTHAI.ToString();
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
        public static AjaxOut DrawCaBenhContent(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                string diaChi = "";
                if (!string.IsNullOrEmpty(caBenh.DIACHI))
                {              
                   diaChi = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViHanhChinhProcess().GetTen(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.DIACHI);
                }
                string ngheNghiep = "";
                if (!string.IsNullOrEmpty(caBenh.NGHENGHIEPMA))
                {
                    ngheNghiep = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateNgheNghiepProcess().GetTen(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.NGHENGHIEPMA);                   
                }
                OneMES3.DM.Model.DanTocCls danToc = null;
                if (!string.IsNullOrEmpty(caBenh.DANTOCMA))
                {
                    danToc = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.DANTOCMA);
                }
                OneMES3.DM.Model.IcdCls chanDoan = null;
                if (!string.IsNullOrEmpty(caBenh.CHANDOANBANDAUMA))
                {
                    chanDoan = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateIcdProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.CHANDOANBANDAUMA);
                }
                string html = "";
                if (caBenh.LOAIHOICHAN == (int)CaBenhCls.eLoaiHoichan.TuVanKCB || caBenh.LOAIHOICHAN == (int)CaBenhCls.eLoaiHoichan.PhauThuat || caBenh.LOAIHOICHAN == (int)CaBenhCls.eLoaiHoichan.CanLamSang)
                {
                    html =
                             "          <div class='row'>" +
                    #region Thông tin hành chính
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "1. Thông tin hành chính") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtHanhChinh' style='height:280px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Họ tên BN:") + " \r\n" +
                                                            "<span id='spHoTenBN' class=\"valueForm\">" + caBenh.HOTENBN + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Tuổi:") + " \r\n" +
                                                            "<span id='spTuoi' class=\"valueForm\">" + BenhNhan.TinhTuoi(caBenh.NGAYSINH, caBenh.NGAYNHAPVIEN, caBenh.NGAYXUATVIEN) + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + " \r\n" +
                                                            "<span id='spGioiTinh' class=\"valueForm\">" + (caBenh.GIOITINH == null ? null : BenhNhan.GioiTinhs[caBenh.GIOITINH.Value]) + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Dân tộc:") + " \r\n" +
                                                            "<span id='spDanToc' class=\"valueForm\">" + (danToc != null ? danToc.Ten : caBenh.DANTOCMA) + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Nghề nghiệp:") + " \r\n" +
                                                            "<span id='spNgheNghiep' class=\"valueForm\">" + ngheNghiep + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Địa chỉ:") + " \r\n" +
                                                            "<span id='spDiaChi' class=\"valueForm\">" + diaChi + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Ngày vào viện:") + " \r\n" +
                                                            "<span id='spNgayNhapVien' class=\"valueString\">" + (caBenh.NGAYNHAPVIEN == null ? null : caBenh.NGAYNHAPVIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Thông tin hành chính
                    #region Hỏi bệnh
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "2. Thông tin hỏi bệnh") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtHoiBenh' style='height:280px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Lý do vào viện:") + "<br> \r\n" +
                                                            "<input type='text' id='spLyDoVaoVien' class=\"form-control\" disabled value='" + caBenh.LYDOVAOVIEN + "'>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Bệnh sử:") + "<br> \r\n" +
                                                            "<textarea id='divBenhSu' class=\"form-control\" rows=4 disabled>" + caBenh.BENHSU + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Tiền sử:") + "<br> \r\n" +
                                                            "<textarea id='divTienSuBenh' class=\"form-control\" rows=4 disabled>" + caBenh.TIENSUBENH + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Hỏi bệnh
                             "          </div>\r\n" +
                             "          <div class='row'>" +
                    #region Khám bệnh vào viện
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "3. Khám bệnh vào viện") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtKhamBenhVaoVien' style='height:300px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Toàn thân:") + "<br> \r\n" +
                                                            "<textarea id='divToanThan' class=\"form-control\" rows=4 disabled>" + caBenh.TOANTHAN + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Các cơ quan:") + "<br> \r\n" +
                                                            "<textarea id='divBoPhan' class=\"form-control\" rows=8 disabled>" + caBenh.BOPHAN + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Khám bệnh vào viện
                    #region Chẩn đoán sơ bộ, cận lâm sàng đã thực hiện
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "4. Chẩn đoán sơ bộ, cận lâm sàng đã thực hiện") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtChanDoanCanLamSang' style='height:300px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Chẩn đoán sơ bộ:") + "<br> \r\n" +
                                                            "<input type='text' id='spChanDoanSoBo' class=\"form-control\" disabled value='" + caBenh.CHANDOANSOBO + "'>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Cận lâm sàng đã thực hiện:") + "<br> \r\n" +
                                                            "<textarea id='divCanLamSang' class=\"form-control\" rows=11 disabled>" + caBenh.CANLAMSANG + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Chẩn đoán sơ bộ, cận lâm sàng đã thực hiện
                             "          </div>\r\n" +
                             "          <div class='row'>" +
                    #region Kết quả xét nghiệm
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "5. Kết quả xét nghiệm") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtKetQuaXN' style='height:400px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Nhận xét:") + "<br> \r\n" +
                                                            //"<div id='divNxKetQuaXN' class=\"form-control\" style='height: 150px; overflow-y:auto;'>" + caBenh.NXKETQUAXN + "</div>\r\n" +
                                                            "<textarea id='divNxKetQuaXN' class=\"form-control\"  rows=8; style='overflow-y: auto;' disabled>" + caBenh.NXKETQUAXN + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12' id='divChiSoXetNghiems'>\r\n" +
                                                        DrawKetQuaXetNghiems(ORenderInfo, caBenh.ID).HtmlContent +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Kết quả xét nghiệm
                    #region Kết quả chẩn đoán hình ảnh
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "6. Kết quả chẩn đoán hình ảnh") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtKetQuaCDHA' style='height:400px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Kết quả:") + "<br> \r\n" +
                                                            "<textarea id='divKetQuaCDHA' class=\"form-control\"  rows=11; style='overflow-y: auto;' disabled>" + caBenh.KETQUACDHA + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Danh sách hình ảnh:") + " \r\n" +
                                                            "<div id='divHinhAnhs' style='overflow-x: auto;'>" + DrawHinhAnhs(ORenderInfo, caBenh.ID).HtmlContent + "</div>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Kết quả chẩn đoán hình ảnh
                             "          </div>\r\n" +

                             "          <div class='row'>" +
                    #region Chẩn đoán xác định, phẫu thuật
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "7. Chẩn đoán xác định, phẫu thuật, thủ thuật") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divChanDoanXacDinhPhauThuat' style='height:280px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Chẩn đoán:") + "<br> \r\n" +
                                                            "<input type='text' id='spChanDoanXacDinh' class=\"form-control\" disabled value='" + caBenh.CHANDOANXACDINH + "'>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Phẫu thuật:") + "<br> \r\n" +
                                                            "<textarea id='divPhauThuat' class=\"form-control\" rows=8 disabled>" + caBenh.PHAUTHUAT + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<div class=\"form-group\">\r\n" +
                                                            WebLanguage.GetLanguage(OSiteParam, "Thủ thuật:") + "<br> \r\n" +
                                                            "<textarea id='divThuThuat' class=\"form-control\" rows=4 disabled>" + caBenh.THUTHUAT + "</textarea>\r\n" +
                                                        "</div>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Chẩn đoán xác định, phẫu thuật
                    #region Thông tin điều trị
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "8. Thông tin điều trị") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divTtDieuTri' style='height:280px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<textarea id='txtThongTinDieuTriView' rows=13 class=\"form-control\" disabled>" + caBenh.THONGTINDIEUTRI + "</textarea>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Thông tin điều trị
                             "          </div>\r\n" +

                             "          <div class='row'>" +
                    #region Câu hỏi thảo luận
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "9. Câu hỏi thảo luận") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divCauHoiThaoLuan' style='height:150px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    "<div class='col-md-12 col-xs-12'>\r\n" +
                                                        "<textarea id='divCauHoi' class=\"form-control\" rows=7 disabled>" + caBenh.CAUHOI + "</textarea>\r\n" +
                                                    "</div>\r\n" +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Câu hỏi thảo luận
                    #region Danh sách hồ sơ
                             "              <div class='float-e-margins col-md-6 col-xs-12'>\r\n" +
                             "                  <div class='ibox-title'>\r\n" +
                             "                      <h5>" + WebLanguage.GetLanguage(OSiteParam, "10. Danh sách hồ sơ") + "</h5> \r\n" +
                             "                  </div>\r\n" +
                             "                  <div class='ibox-content' id = 'divHoSos' style='height:150px; border:solid 1px #DBEEF3; overflow-y:auto;'>\r\n" +
                                                    DrawHoSoList(ORenderInfo, caBenh.ID).HtmlContent +
                             "                  </div>\r\n" +
                             "              </div>\r\n" +
                    #endregion Danh sách hồ sơ
                             "          </div>\r\n";
                }
                else if (caBenh.LOAIHOICHAN == (int)CaBenhCls.eLoaiHoichan.CanLamSang)
                {
                    html =
                    #region Thông tin hành chính
                    "          <div class=\"ibox float-e-margins\"> \r\n" +
                    "              <div class=\"ibox-title\"> \r\n" +
                    "                  <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin hành chính") + "</h5> \r\n" +
                    "              </div> \r\n" +
                    "              <div class=\"ibox-content\"> \r\n" +
                    "                  <div class='row'>" +
                                            "<div class='col-md-3 col-xs-6'>\r\n" +
                                                "<div class=\"form-group\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Mã BN:") + " \r\n" +
                                                    "<span id='spMaBN' class=\"valueForm\">" + caBenh.MABN + "</span>\r\n" +
                                                "</div>\r\n" +
                                            "</div>\r\n" +
                                            "<div class='col-md-3 col-xs-6'>\r\n" +
                                                "<div class=\"form-group\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Họ tên BN:") + " \r\n" +
                                                    "<span id='spHoTenBN' class=\"valueForm\">" + caBenh.HOTENBN + "</span>\r\n" +
                                                "</div>\r\n" +
                                            "</div>\r\n" +
                                            "<div class='col-md-3 col-xs-6'>\r\n" +
                                                "<div class=\"form-group\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Tuổi:") + " \r\n" +
                                                    "<span id='spTuoi' class=\"valueForm\">" + BenhNhan.TinhTuoi(caBenh.NGAYSINH, caBenh.NGAYNHAPVIEN, caBenh.NGAYXUATVIEN) + "</span>\r\n" +
                                                "</div>\r\n" +
                                            "</div>\r\n" +
                                            "<div class='col-md-3 col-xs-6'>\r\n" +
                                                "<div class=\"form-group\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + " \r\n" +
                                                    "<span id='spGioiTinh' class=\"valueForm\">" + (caBenh.GIOITINH == 1 ? "Nam" : caBenh.GIOITINH == 2 ? "Nữ" : null) + "</span>\r\n" +
                                                "</div>\r\n" +
                                            "</div>\r\n" +
                    "                  </div>" +
                    "              </div> \r\n" +
                    "          </div> \r\n" +
                    #endregion
                    #region Thông tin khám bệnh, điều trị
                         "          <div class=\"ibox float-e-margins\"> \r\n" +
                    "              <div class=\"ibox-title\"> \r\n" +
                    "                  <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin khám bệnh, điều trị") + "</h5> \r\n" +
                    "              </div> \r\n" +
                    "              <div class=\"ibox-content\"> \r\n" +
                    "                  <div class='row'>" +
                                           "<div class='col-md-4 col-xs-6'>\r\n" +
                                               "<div class=\"form-group\">\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Ngày vào viện:") + " \r\n" +
                                                   "<span id='spNgayNhapVien' class=\"valueString\">" + (caBenh.NGAYNHAPVIEN == null ? null : caBenh.NGAYNHAPVIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                                               "</div>\r\n" +
                                           "</div>\r\n" +
                                           "<div class='col-md-4 col-xs-6'>\r\n" +
                                               "<div class=\"form-group\">\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Vào Viện/Trung tâm/Khoa:") + " \r\n" +
                                                   "<span id='spNhapVienTai' class=\"valueString\">" + caBenh.NHAPVIENTAI + "</span>\r\n" +
                                               "</div>\r\n" +
                                           "</div>\r\n" +
                    "                  </div>" +
                    "                  <div class='row'>" +
                                            "<div class='col-md-12 col-xs-12'>\r\n" +
                                                "<div class=\"form-group\">\r\n" +
                                                    WebLanguage.GetLanguage(OSiteParam, "Chẩn đoán:") + " \r\n" +
                                                    "<span id='spChanDoan' class=\"valueForm\">" + (chanDoan == null ? caBenh.CHANDOANBANDAUMA : chanDoan.Ten) + (string.IsNullOrEmpty(caBenh.CHANDOANBANDAU) ? null : " (" + caBenh.CHANDOANBANDAU + ")") + "</span>\r\n" +
                                                "</div>\r\n" +
                                            "</div>\r\n" +
                    "                  </div>" +
                    "                  <div class='row'>" +
                                           "<div class='col-md-12 col-xs-12'>\r\n" +
                                               "<div class=\"form-group\">\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Lý do hội chẩn:") + " \r\n" +
                                                   "<span id='spLyDoYeuCau' class=\"valueForm\">" + caBenh.LYDOYEUCAU + "</span>\r\n" +
                                               "</div>\r\n" +
                                           "</div>\r\n" +
                    "                  </div>" +
                    "                  <div class='row'>" +
                                           "<div class='col-md-12 col-xs-12'>\r\n" +
                                               "<div class=\"form-group\">\r\n" +
                                                   WebLanguage.GetLanguage(OSiteParam, "Tóm tắt quá trình diễn biến bệnh, quá trình điều trị và chăm sóc người bệnh") +
                                                   "<span id=\"spMoTa\" class=\"valueForm\">" + caBenh.MOTA + "</span>\r\n" +
                                               "</div>\r\n" +
                                           "</div>\r\n" +
                    "                  </div>\r\n" +
                    "              </div> \r\n" +
                    "          </div> \r\n";
                    #endregion
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
        public static AjaxOut DrawLichHoiChans_Tab(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác."));
                }
                int[] trangThais = new int[] { (int)LichHoiChanCls.eTrangThai.ChoDuyet, (int)LichHoiChanCls.eTrangThai.DaDuyet, (int)LichHoiChanCls.eTrangThai.TuChoiDuyet };
                LichHoiChanCls[] lichHoiChans = CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Reading(ORenderInfo, new LichHoiChanFilterCls() { CaBenhId = caBenhId, TrangThais = trangThais });
                string lichHoiChanId = lichHoiChans.Count() > 0 ? lichHoiChans.FirstOrDefault().ID : null;

                string html = "<table width='100%'>\r\n" +
                                "<tr'>\r\n" +
                                    "<td style='width:300px; vertical-align: top; border:1px solid;'>\r\n" +

                                        "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                                            "<thead> \r\n" +
                                                "<tr> \r\n" +
                                                    "<th width=150 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian HC") + " </th> \r\n" +
                                                    "<th width=150 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                                                "</tr> \r\n" +
                                            "</thead> \r\n" +
                                            "<tbody id = 'divLichHoiChansListContent'> \r\n" +
                                            DrawLichHoiChans_List(ORenderInfo, caBenhId, lichHoiChanId).HtmlContent +
                                            "</tbody> \r\n" +
                                        "</table> \r\n" +

                                    "</td>\r\n" +
                                    "<td id='divLichHoiChansDetail' width='80%'>\r\n" +
                                        DrawLichHoiChans_Detail(ORenderInfo, caBenhId, lichHoiChanId).HtmlContent +
                                    "</td>\r\n" +
                                "</tr>\r\n" +
                            "</table>\r\n";
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
        public static AjaxOut DrawLichHoiChans_List(RenderInfoCls ORenderInfo, string caBenhId, string lichHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác."));
                }
                int[] trangThais = new int[] { (int)LichHoiChanCls.eTrangThai.ChoDuyet, (int)LichHoiChanCls.eTrangThai.DaDuyet, (int)LichHoiChanCls.eTrangThai.TuChoiDuyet };
                LichHoiChanCls[] lichHoiChans = CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Reading(ORenderInfo, new LichHoiChanFilterCls() { CaBenhId = caBenhId, TrangThais = trangThais });
                if (string.IsNullOrEmpty(lichHoiChanId))
                    lichHoiChanId = lichHoiChans.Select(o => o.ID).FirstOrDefault();

                string html = "";
                for (int iIndex = 0; iIndex < lichHoiChans.Length; iIndex++)
                {
                    html += "<tr style='cursor:pointer' onclick=\"LichHoiChanRow_OnClick('LichHoiChanRow" + iIndex + "', '" + lichHoiChans[iIndex].ID + "');\" id='LichHoiChanRow" + iIndex + "' " + (lichHoiChans[iIndex].ID == lichHoiChanId ? "class='seletedRow'" : null) + "> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'><span>" + lichHoiChans[iIndex].BATDAU.Value.ToString("HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'><span>" + LichHoiChanParser.sColorTrangThai[lichHoiChans[iIndex].TRANGTHAI] + "</span></td> \r\n" +
                            "</tr> \r\n";
                }
                RetAjaxOut.RetExtraParam1 = lichHoiChanId;
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
        public static AjaxOut DrawLichHoiChans_Detail(RenderInfoCls ORenderInfo, string caBenhId, string lichHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                LichHoiChanCls lichHoiChan = string.IsNullOrEmpty(lichHoiChanId) ? new LichHoiChanCls() : CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().CreateModel(ORenderInfo, lichHoiChanId);
                if (lichHoiChan == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Lịch hội chẩn này đã bị xóa bởi người dùng khác."));
                }
                BacSyCls chuTriHoiChan = string.IsNullOrEmpty(lichHoiChan.CHUTRI) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichHoiChan.CHUTRI);
                BacSyCls thuKy = string.IsNullOrEmpty(lichHoiChan.THUKY) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichHoiChan.THUKY);

                string html =
                    "<table width='100%'>\r\n" +
                        "<tr>\r\n" +
                            "<td colspan=4>\r\n" +
                                "<span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Địa điểm hội chẩn:") + "</span>\r\n" +
                                "<span class=\"valueString\">" + lichHoiChan.DIADIEM + "</span>\r\n" +
                            "</td>\r\n" +
                        "</tr>\r\n" +
                        "<tr>\r\n" +
                            "<td width=25%>\r\n" +
                                "<span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Hội chẩn từ:") + "</span>\r\n" +
                                "<span class=\"valueString\">" + (string.IsNullOrEmpty(lichHoiChan.ID) ? null : lichHoiChan.BATDAU.Value.ToString("HH:mm dd/MM/yyyy")) + "</span>\r\n" +
                            "</td>\r\n" +
                            "<td width=25%>\r\n" +
                                "<span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Đến:") + "</span>\r\n" +
                                "<span class=\"valueString\">" + (lichHoiChan.KETTHUC == null ? null : lichHoiChan.KETTHUC.Value.ToString("HH:mm dd/MM/yyyy")) + "</span>\r\n" +
                            "</td>\r\n" +
                            "<td width=25%>\r\n" +
                                "<span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Chủ trì hội chẩn:") + "</span>\r\n" +
                                "<span class=\"valueString\">" + (chuTriHoiChan != null ? chuTriHoiChan.MA + " - " + chuTriHoiChan.HOTEN : null) + "</span>\r\n" +
                            "</td>\r\n" +
                            "<td width=25%>\r\n" +
                                "<span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Thư ký:") + "</span>\r\n" +
                                "<span class=\"valueString\">" + (thuKy != null ? thuKy.MA + " - " + thuKy.HOTEN : null) + "</span>\r\n" +
                            "</td>\r\n" +
                        "</tr>\r\n" +
                        "<tr>\r\n" +
                            "<td colspan=2 style='vertical-align:top;'>\r\n" +
                                "<span class=\"rightLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách tài liệu đính kèm") + "</span>\r\n" +
                                "<div id='divTaiLieu'>" + DrawTaiLieus(ORenderInfo, caBenhId, lichHoiChan.ID).HtmlContent + "</div>\r\n" +
                            "</td>\r\n" +
                            "<td colspan=2 style='vertical-align:top;'>\r\n" +
                                "<span class=\"rightLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách thành viên tham gia hội chẩn") + "</span>\r\n" +
                                "<div id='divThanhVienHoiChan'>" + DrawThanhVienHoichans(ORenderInfo, caBenhId, lichHoiChan.ID).HtmlContent + "</div>\r\n" +
                            "</td>\r\n" +
                        "</tr>\r\n" +
                    "</table>\r\n";
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
        public static AjaxOut DrawTaiLieus(RenderInfoCls ORenderInfo, string caBenhId, string lichHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                LapLichTepDinhKemCls[] lapLichTepDinhKems = string.IsNullOrEmpty(lichHoiChanId) ? new LapLichTepDinhKemCls[0] : CallBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Reading(ORenderInfo, new LapLichTepDinhKemFilterCls() { LICHHOICHANID = lichHoiChanId });
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\" style='width: 100%;'> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=10% style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên tài liệu") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n" +
                                 "<tr id='trAddTepTin' style='display:none;'> \r\n" +
                                     "<td></td> \r\n" +
                                     "<td><input id='fileUpload' type='file' class='form-control'/></td> \r\n" +
                                 "</tr> \r\n";
                for (int iIndex = 0; iIndex < lapLichTepDinhKems.Length; iIndex++)
                {
                    string fileType = lapLichTepDinhKems[iIndex].TENTEP.Substring(lapLichTepDinhKems[iIndex].TENTEP.LastIndexOf('.') + 1);
                    string link, icon = "";
                    if (new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx" }.Contains(fileType))//Nếu là các file thuộc office thì mở nhờ vào http://docs.google.com/viewer
                    {
                        //Tạm thời chưa có tên miền nên người dùng tự download về
                        link = "<a href='./" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedTaiLieuPath"] + "/" + lapLichTepDinhKems[iIndex].TENTEP + "'>" + lapLichTepDinhKems[iIndex].TENHIENTHI + "</a>\r\n";
                        //Mở nhờ vào http://docs.google.com/viewer
                        //if (fileType == "doc" || fileType == "docx")
                        //    icon = "fa fa-file-word-o";
                        //else if (fileType == "xls" || fileType == "xlsx")
                        //    icon = "fa fa-file-excel-o";
                        //else if (fileType == "ppt" || fileType == "pptx")
                        //    icon = "fa fa-file-powerpoint-o";
                        //link = "<a href='http://docs.google.com/gview?url=http://" + System.Web.Configuration.WebConfigurationManager.AppSettings["DomainName"] + "/" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedTaiLieuPath"] + "/" + lapLichTepDinhKems[iIndex].TENTEP + "' target='_blank'><i class='" + icon + "' style='font-size:50px;'></i>&nbsp;&nbsp;&nbsp;" + lapLichTepDinhKems[iIndex].TENHIENTHI + "</a>\r\n";
                    }
                    else
                    {
                        //"<a href='" + exampleUrl + "'>embedded document view !!</a>&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                        //"<object><embed height ='500' src='./" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedTuVanFilePath"] + "/" + tepDinhKem.TENTEP + "' type='application/pdf' width='650'></object>" +
                        if (fileType == "rar" || fileType == "zip")
                            icon = "fa fa-file-archive-o";
                        else if (fileType == "pdf")
                            icon = "fa fa-file-pdf-o";
                        else if (new string[] { "jpg", "png", "jpeg", "bmp", "gif", "tga" }.Contains(fileType))
                            icon = "fa fa-file-photo-o";
                        else if (new string[] { "mp3", "wma", "wav", "flac", "aac", "ogg", "aiff", "alac" }.Contains(fileType))
                            icon = "fa fa-file-sound-o";
                        else if (new string[] { "avi", "mp4", "mkv", "wmv", "vob", "flv", "dlvx" }.Contains(fileType))
                            icon = "fa fa-file-video-o";
                        else
                            icon = "fa fa-delicious";
                        link = "<a href='./" + System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedTaiLieuPath"] + "/" + lapLichTepDinhKems[iIndex].TENTEP + "' target='_blank'><i class='" + icon + "' style='font-size:40px;'></i>&nbsp;&nbsp;&nbsp;" + lapLichTepDinhKems[iIndex].TENHIENTHI + "</a>\r\n";
                    }
                    html += "<tr> \r\n" +
                                "<input type='hidden' id='hdLapLichTepDinhKemId" + iIndex + "' value='" + lapLichTepDinhKems[iIndex].ID + "'>\r\n" +
                                "<input type='hidden' id='hdTenTep" + iIndex + "' value='" + lapLichTepDinhKems[iIndex].TENTEP + "'>\r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td><input id='fileUpload" + iIndex + "' type='file' class='form-control CssEditorItemTepTin' style='display:none'><span class='CssDisplayItemTepTin' width=100% id='spTenTep" + iIndex + "'>" + link + "</span></td> \r\n" +
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
        public static AjaxOut DrawThanhVienHoichans(RenderInfoCls ORenderInfo, string caBenhId, string lichHoiChanId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                LapLichThanhVienHoiChanCls[] lapLichThanhVienHoiChans = string.IsNullOrEmpty(lichHoiChanId) ? new LapLichThanhVienHoiChanCls[0] : CallBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Reading(ORenderInfo, new LapLichThanhVienHoiChanFilterCls() { LICHHOICHANID = lichHoiChanId });

                string html =
                         "<table class=\"table table-striped table-bordered table-hover\" style='width: 100%;'> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=10% style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < lapLichThanhVienHoiChans.Length; iIndex++)
                {
                    BacSyCls thanhVien = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lapLichThanhVienHoiChans[iIndex].BACSYID);
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td style='vertical-align: middle;'><span id='spThanhVienHoiChan" + iIndex + "'>" + (thanhVien == null ? null : thanhVien.HOTEN) + "</span></td> \r\n" +
                                "<td style='vertical-align: middle;'><span id='spDonVi" + iIndex + "'>" + lapLichThanhVienHoiChans[iIndex].DONVICONGTAC + "</span></td> \r\n" +
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
        public static AjaxOut DrawButPheList(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                int[] hanhDongs = new int[] { (int)Common.TuVanCaBenh.eTacVu.TuChoiTiepNhan, (int)Common.TuVanCaBenh.eTacVu.ChuyenLapLich, (int)Common.TuVanCaBenh.eTacVu.ChuyenHoiChan };
                var butPhes = CallBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Reading(ORenderInfo, new ButPheFilterCls() { CABENHID = caBenhId, HanhDongs = hanhDongs });
                string html =
                        "<table class=\"table table-striped table-bordered table-hover\" style='width: 100%;'> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=20px style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center; width: 150px;'>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + " </th> \r\n" +
                                    "<th style='text-align: center; width: 20%'>" + WebLanguage.GetLanguage(OSiteParam, "Người phê") + " </th> \r\n" +
                                    "<th style='text-align: center; width: 15%'>" + WebLanguage.GetLanguage(OSiteParam, "Thao tác") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                int sodong = 0;
                foreach (var butPhe in butPhes)
                {
                    sodong++;
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (sodong) + "</td> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'><span>" + butPhe.THOIGIAN.ToString("HH:mm dd/MM/yyyy") + "</span></td> \r\n" +
                                "<td style='vertical-align: middle;'><span>" + butPhe.NGUOIPHE + "</span></td> \r\n" +
                                "<td style='vertical-align: middle;'><span>" + Common.CaBenh.HanhDongs[butPhe.HANHDONG] + "</span></td> \r\n" +
                                "<td style='vertical-align: middle;'><span>" + butPhe.NOIDUNG + "</span></td> \r\n" +
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
        public static AjaxOut DrawKetQuaXetNghiems(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                var ketQuaXetNghiems = CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Reading(ORenderInfo, new KetQuaXetNghiemFilterCls() { CABENHID = caBenhId }).ToList();
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ") + " </th> \r\n" +
                                    "<th width=150 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày kết quả") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < ketQuaXetNghiems.Count; iIndex++)
                {
                    string tenDichVu = ketQuaXetNghiems[iIndex].DICHVUTEN;
                    if (string.IsNullOrEmpty(tenDichVu))
                    {
                        OneMES3.DM.Model.DichVuCls dichVu = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDichVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), ketQuaXetNghiems[iIndex].DICHVUMA);
                        if (dichVu != null)
                            tenDichVu = dichVu.Ten;
                        else tenDichVu = ketQuaXetNghiems[iIndex].DICHVUMA;
                    }
                    html += "<tr style='cursor:pointer;' onclick='ShowPopupChiSoXetNghiem(\"" + ketQuaXetNghiems[iIndex].ID + "\", \"" + tenDichVu + "\");'> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + tenDichVu + "</td> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (ketQuaXetNghiems[iIndex].THOIGIAN == null ? null : ketQuaXetNghiems[iIndex].THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy")) + "</td> \r\n" +
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
        public static AjaxOut DrawKetQuaXetNghiemChiTiets(RenderInfoCls ORenderInfo, string ketQuaXetNghiemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                var ketQuaXetNghiems = string.IsNullOrEmpty(ketQuaXetNghiemId) ? new List<KetQuaXetNghiemChiTietCls>() : CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Reading(ORenderInfo, new KetQuaXetNghiemChiTietFilterCls() { KETQUAXETNGHIEMID = ketQuaXetNghiemId }).ToList();
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=30 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chỉ số") + " </th> \r\n" +
                                    "<th width=150 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Giá trị") + " </th> \r\n" +
                                    "<th width=80 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "ĐVT") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < ketQuaXetNghiems.Count; iIndex++)
                {
                    string tenChiSo = ketQuaXetNghiems[iIndex].CHISOTEN;
                    string donViTinh = "";
                    if (string.IsNullOrEmpty(tenChiSo))
                    {       
                        OneMES3.DM.Model.ChiSoXetNghiemCls chiSoXN = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChiSoXetNghiemProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), ketQuaXetNghiems[iIndex].CHISOMA);                     
                        if (chiSoXN != null)
                        {
                            tenChiSo = chiSoXN.Ten;
                            donViTinh = chiSoXN.DonViDo;
                        }
                        else tenChiSo = ketQuaXetNghiems[iIndex].CHISOMA;
                    }
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + tenChiSo + "</td> \r\n" +
                                "<td style='text-align: center;'>" + ketQuaXetNghiems[iIndex].GIATRI + "</td> \r\n" +
                                "<td style='text-align: center;'>" + donViTinh + "</td> \r\n" +
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
        public static AjaxOut DrawHinhAnhs(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                HinhAnhCls[] hinhAnhs = CallBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Reading(ORenderInfo, new HinhAnhFilterCls() { CABENHID = caBenhId });
                string html = "";
                foreach (var hinhAnh in hinhAnhs)
                {
                    if (hinhAnh.TYPE == (int)HinhAnh.eType.DICOM)//Anh DICOM
                    {
                        html += "<div style = 'float: left; margin-right:50px; max-width:300px; height:130px;'>\r\n" +
                                    "<center><a href='" + hinhAnh.LINK + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Xem ảnh") + "' target='_blank'>" + (string.IsNullOrEmpty(hinhAnh.MODALITY) ? null : hinhAnh.MODALITY) + " | " + (hinhAnh.TIMEEX == null ? null : hinhAnh.TIMEEX.Value.ToString("HH:mm dd/MM/yyyy")) + "</a>\r\n" +
                                        "<br><span class=\"valueString\">" + hinhAnh.MOTA + "</span></center>\r\n" +
                                "</div>\r\n";
                    }
                    else if (hinhAnh.TYPE == (int)HinhAnh.eType.Video)
                    {
                        if (System.IO.File.Exists(hinhAnh.DUONGDAN))
                        {
                            html += "<div style = 'float: left; margin-right:50px; height:130px;'>\r\n" +
                                         string.Format("<center><a href='{0}' title='" + WebLanguage.GetLanguage(OSiteParam, "Xem video") + "' target='_blank'><div style='height: 100px; width: 180px;'><video height='100%' controls><source src='{0}'></video></div></a>\r\n", Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"], hinhAnh.TENTEP)) +
                                         "<br><span class=\"valueString\">" + hinhAnh.MOTA + "</span></center>\r\n" +
                                    "</div>\r\n";
                        }
                    }
                    else if (hinhAnh.TYPE == (int)HinhAnh.eType.NonDICOM)
                    {
                        if (System.IO.File.Exists(hinhAnh.DUONGDAN))
                        {
                            html += "<div style = 'float: left; margin-right:50px; height:130px;'>\r\n" +
                                         string.Format("<center><a href='{0}' title='" + WebLanguage.GetLanguage(OSiteParam, "Xem ảnh") + "' target='_blank'><img style = 'height: 100px;' src='{0}'></a>\r\n", Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"], hinhAnh.TENTEP)) +
                                         "<br><span class=\"valueString\">" + hinhAnh.MOTA + "</span></center>\r\n" +
                                    "</div>\r\n";
                        }
                    }
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
        public static AjaxOut DrawHoSoList(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                TepTinCls[] tepTins = CallBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Reading(ORenderInfo, new TepTinFilterCls() { CABENHID = caBenhId });
                string html = "<ul>";
                foreach (var tepTin in tepTins)
                {
                    if (System.IO.File.Exists(tepTin.DUONGDAN))
                    {
                        html += "<li style='float:left; width:98%;'>\r\n";
                        string[] fileNameArray = tepTin.TENTEP.Split('.');
                        string fileType = fileNameArray[fileNameArray.Length - 1];
                        if (new string[] { "doc", "docx", "xls", "xlsx", "ppt", "pptx" }.Contains(fileType))//Nếu là các file thuộc office thì mở nhờ vào http://docs.google.com/viewer
                        {
                            //Tạm thời chưa có tên miền nên người dùng tự download về
                            html += "<a href='" + Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedThamVanFilePath"], tepTin.TENTEP) + "'>" + tepTin.TENHIENTHI + "</a>\r\n";

                        }
                        else
                        {
                            html += "<a href='" + Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedThamVanFilePath"], tepTin.TENTEP) + "' target='_blank'>" + tepTin.TENHIENTHI + "</a>\r\n";
                        }
                        html += "</li>\r\n";
                    }
                }
                html += "</ul>";
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
        public static AjaxOut PopupSlide(RenderInfoCls ORenderInfo, string caBenhId, int slideIndex, SlideProperty[] slidePropertys)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                string diaChi = "";
                if (!string.IsNullOrEmpty(caBenh.DIACHI))
                {
                     diaChi = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViHanhChinhProcess().GetTen(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.DIACHI);
                }
                string ngheNghiep = "";
                if (!string.IsNullOrEmpty(caBenh.NGHENGHIEPMA))
                {
                    ngheNghiep = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateNgheNghiepProcess().GetTen(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.NGHENGHIEPMA);
                }
                OneMES3.DM.Model.DanTocCls danToc = null;
                if (!string.IsNullOrEmpty(caBenh.DANTOCMA))
                {
                    danToc = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.DANTOCMA);
                }
                int slideTotal = slidePropertys.Count();
                string html =
                "<div style='height:100%; width:100%; position: relative;'>\r\n" +
                    "<div style='width:100%; padding:0 10%; position: absolute; top:0px; bottom:100px;'>\r\n";
                switch (slidePropertys[slideIndex].type)
                {
                    #region Start Slide
                    case (int)eSlideType.Start:
                        DateTime? ngayHoiChan = null;
                        BienBanHoiChanToanLichCls bienBanHoiChanToanLich = CallBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().CreateModel(ORenderInfo, caBenh.LICHHOICHANID);
                        if (bienBanHoiChanToanLich != null)
                            ngayHoiChan = bienBanHoiChanToanLich.BATDAU;
                        else
                        {
                            LichHoiChanCls lichHoiChan = CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().CreateModel(ORenderInfo, caBenh.LICHHOICHANID);
                            BienBanHoiChanCls bienBanHoiChan = CallBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Reading(ORenderInfo, new BienBanHoiChanFilterCls() { CABENHID = caBenhId }).FirstOrDefault();
                            if (bienBanHoiChan != null && (lichHoiChan == null || bienBanHoiChan.TAOVAO > lichHoiChan.TAOVAO))
                                ngayHoiChan = bienBanHoiChan.BATDAUHOICHANVAO;
                            else
                            {
                                if (lichHoiChan != null)
                                    ngayHoiChan = lichHoiChan.BATDAU;
                                else if (caBenh.TRANGTHAI == (int)CaBenhCls.eTrangThai.DangHoiChan)
                                    ngayHoiChan = DateTime.Today;
                            }
                        }
                        OwnerCls donViThamVan = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITHAMVANID);
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:10%; text-align:center; color:#B71E42;'>\r\n" +
                                "<p style='font-size:60px; font-weight:bold; '>" + WebLanguage.GetLanguage(OSiteParam, "HỘI CHẨN TƯ VẤN Y HỌC TRỰC TUYẾN") + "<p>\r\n" +
                                "<p style='font-size: 50px;'>" + (donViThamVan != null ? donViThamVan.OwnerName : null) + "</p>\r\n" +
                            "</div>\r\n" +
                            "<p style='font-size: 40px; position: absolute; bottom:10px; right:30px;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày hội chẩn") + ": " + (ngayHoiChan != null ? ngayHoiChan.Value.ToString("dd/MM/yyyy") : null) + "</p>\r\n";
                        break;
                    #endregion Start Slide
                    #region Thông tin hành chính
                    case (int)eSlideType.ThongTinHanhChinh:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". THÔNG TIN HÀNH CHÍNH") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px; padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Họ tên BN:") + " \r\n" +
                                        "<span id='spHoTenBN' class=\"valueForm\">" + caBenh.HOTENBN + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Tuổi:") + " \r\n" +
                                        "<span id='spTuoi' class=\"valueForm\">" + BenhNhan.TinhTuoi(caBenh.NGAYSINH, caBenh.NGAYNHAPVIEN, caBenh.NGAYXUATVIEN) + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + " \r\n" +
                                        "<span id='spGioiTinh' class=\"valueForm\">" + (caBenh.GIOITINH == null ? null : BenhNhan.GioiTinhs[caBenh.GIOITINH.Value]) + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Dân tộc:") + " \r\n" +
                                        "<span id='spDanToc' class=\"valueForm\">" + (danToc != null ? danToc.Ten : caBenh.DANTOCMA) + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Nghề nghiệp:") + " \r\n" +
                                        "<span id='spNgheNghiep' class=\"valueForm\">" + ngheNghiep + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Địa chỉ:") + " \r\n" +
                                        "<span id='spDiaChi' class=\"valueForm\">" + diaChi + "</span>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Ngày vào viện:") + " \r\n" +
                                        "<span id='spNgayNhapVien' class=\"valueString\">" + (caBenh.NGAYNHAPVIEN == null ? null : caBenh.NGAYNHAPVIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Thông tin hành chính
                    #region Hỏi bệnh
                    case (int)eSlideType.HoiBenh:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". HỎI BỆNH") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px;  padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Lý do vào viện:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + caBenh.LYDOVAOVIEN + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Bệnh sử:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.BENHSU) + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Tiền sử:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.TIENSUBENH) + "</div>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Hỏi bệnh
                    #region Khám bệnh vào viện
                    case (int)eSlideType.KhamBenhVaoVien:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". KHÁM BỆNH VÀO VIỆN") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px; padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Toàn thân:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.TOANTHAN) + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Các cơ quan:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.BOPHAN) + "</div>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Khám bệnh vào viện
                    #region Chẩn đoán sơ bộ, cận lâm sàng đã thực hiện
                    case (int)eSlideType.ChanDoanCanLamSang:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". CHẨN ĐOÁN SƠ BỘ, CẬN LÂM SÀNG ĐÃ THỰC HIỆN") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px; padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Chẩn đoán sơ bộ:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + caBenh.CHANDOANSOBO + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Cận lâm sàng đã thực hiện:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.CANLAMSANG) + "</div>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Chẩn đoán sơ bộ, cận lâm sàng đã thực hiện
                    #region Xét nghiệm
                    case (int)eSlideType.XetNghiem:
                        KetQuaXetNghiemCls ketQuaXetNghiem = CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Reading(ORenderInfo, new KetQuaXetNghiemFilterCls() { CABENHID = caBenh.ID }).FirstOrDefault();
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". XÉT NGHIỆM") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px; padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Nhận xét:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.NXKETQUAXN_ENCODED) + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Kết quả xét nghiệm:") + "</b><br> \r\n" +
                                        "<div class='row' style='font-size:24px;'>\r\n" +
                                        "   <div class='col-md-6 col-xs-6' id='divSlideKetQuaXetNghiem' style='overflow:auto;'>\r\n" +
                                            DrawSlideKetQuaXetNghiems(ORenderInfo, caBenhId, ketQuaXetNghiem == null ? null : ketQuaXetNghiem.ID).HtmlContent +
                                        "   </div>\r\n" +
                                        "   <div class='col-md-6 col-xs-6' id='divSlideKetQuaXetNghiemChiTiet' style='overflow:auto;'>\r\n" +
                                            DrawSlideKetQuaXetNghiemChiTiets(ORenderInfo, ketQuaXetNghiem == null ? null : ketQuaXetNghiem.ID).HtmlContent +
                                        "   </div>\r\n" +
                                        "</div>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Xét nghiệm
                    #region Chẩn đoán hình ảnh
                    case (int)eSlideType.ChanDoanHinhanh:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". CHẨN ĐOÁN HÌNH ẢNH") + "</p>\r\n" +
                                "<div class='col-md-12 col-xs-12' style='font-size:30px;'>\r\n" +
                                    ToHtml(caBenh.KETQUACDHA) +
                                "</div>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Chẩn đoán hình ảnh
                    #region Ảnh gắn với ca bệnh
                    case (int)eSlideType.HinhAnh:
                        HinhAnhCls hinhAnh = CallBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().CreateModel(ORenderInfo, slidePropertys[slideIndex].value);
                        if (hinhAnh == null)
                        {
                            RetAjaxOut.Error = true;
                            RetAjaxOut.HtmlContent = WebLanguage.GetLanguage(OSiteParam, "Hình ảnh này đã bị xóa bởi người dùng khác.");
                            return RetAjaxOut;
                        }
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; text-align:center;  height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; border-bottom: solid 2px; color:#B71E42;'>" + (string.IsNullOrEmpty(hinhAnh.MOTA) ? hinhAnh.TENHIENTHI : hinhAnh.MOTA) + "</p>\r\n" +
                                "<p class='col-md-12 col-xs-12' style='height: 90%;'>\r\n";
                        if (hinhAnh.TYPE == (int)HinhAnh.eType.DICOM)//Anh DICOM
                        {
                            //html += "<iframe src='" + hinhAnh.LINK + "' width='100%' height='100%'></iframe>\r\n";
                            html += "<a href='" + hinhAnh.LINK + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Xem ảnh") + "' target='_blank' style='font-size: 30px;'>" + (string.IsNullOrEmpty(hinhAnh.MODALITY) ? null : hinhAnh.MODALITY) + " | " + (hinhAnh.TIMEEX == null ? null : hinhAnh.TIMEEX.Value.ToString("HH:mm dd/MM/yyyy")) + "</a>\r\n";
                        }
                        else if (hinhAnh.TYPE == (int)HinhAnh.eType.Video)
                        {
                            if (System.IO.File.Exists(hinhAnh.DUONGDAN))
                            {
                                html += "<video height='100%' controls><source src='" + Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"], hinhAnh.TENTEP) + "'></video>\r\n";
                            }
                        }
                        else if (hinhAnh.TYPE == (int)HinhAnh.eType.NonDICOM)
                        {
                            if (System.IO.File.Exists(hinhAnh.DUONGDAN))
                            {
                                html += "<img height='100%' src='" + Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"], hinhAnh.TENTEP) + "'>\r\n";
                            }
                        }
                        html +=
                            "</p>\r\n" +
                        "</div>\r\n";
                        break;
                    #endregion Ảnh gắn với ca bệnh
                    #region Chẩn đoán xác định, phẫu thuật, thủ thuật
                    case (int)eSlideType.ChanDoanPhauThuat:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". CHẨN ĐOÁN XÁC ĐỊNH, PHẪU THUẬT") + "</p>\r\n" +
                                "<ul class='col-md-12 col-xs-12' style='font-size: 30px; padding-left: 30px;'>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Chẩn đoán xác định:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + caBenh.CHANDOANXACDINH + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Phẫu thuật:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.PHAUTHUAT) + "</div>\r\n" +
                                    "</li>\r\n" +
                                    "<li>\r\n" +
                                        "<b>" + WebLanguage.GetLanguage(OSiteParam, "Thủ thuật:") + "</b><br> \r\n" +
                                        "<div style='width:100%;'>" + ToHtml(caBenh.THUTHUAT) + "</div>\r\n" +
                                    "</li>\r\n" +
                                "</ul>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Chẩn đoán xác định, phẫu thuật
                    #region Điều trị
                    case (int)eSlideType.DieuTri:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". ĐIỀU TRỊ") + "</p>\r\n" +
                                "<div class='col-md-12 col-xs-12' style='font-size:30px; padding-left: 30px;'>\r\n" +//hidden
                                    ToHtml(caBenh.THONGTINDIEUTRI_ENCODED) +
                                "</div>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Điều trị
                    #region Câu hỏi, thảo luận
                    case (int)eSlideType.CauHoi:
                        html +=
                            "<div class='col-md-12 col-xs-12' style='margin-top:50px; height: 95%; overflow: auto;'>\r\n" +
                                "<p style='font-size:40px; font-weight:bold; text-align:center; border-bottom: solid 2px; color:#B71E42;'>" + slidePropertys[slideIndex].stt + WebLanguage.GetLanguage(OSiteParam, ". CÂU HỎI, THẢO LUẬN") + "</p>\r\n" +
                                "<div class='col-md-12 col-xs-12' style='font-size:30px;'>\r\n" +
                                    ToHtml(caBenh.CAUHOI) +
                                "</div>\r\n" +
                            "</div>\r\n";
                        break;
                    #endregion Câu hỏi, thảo luận
                    #region End Slide
                    case (int)eSlideType.End:
                        html += "<p style='font-size:60px; font-weight:bold; margin-top: 20%; text-align:center; color:#B71E42;'>" + WebLanguage.GetLanguage(OSiteParam, "Trân trọng cảm ơn!") + "</p>\r\n";
                        break;
                        #endregion End Slide
                }

                html +=
                    "</div>\r\n" +
                    "<div style='width:100%; text-align:center; position: absolute; bottom:0px;'>\r\n";
                if (slideIndex > 0)
                    html += string.Format("<a href='javascript:ShowSlide({0});'><i class='fa fa-chevron-circle-left' title='Slide trước' style='font-size:50px; color: #4682B4; margin:0 10px;'></i></a> \r\n", slideIndex - 1);
                if (slideIndex < slideTotal - 1)
                    html += string.Format("<a href='javascript:ShowSlide({0});'><i class='fa fa-chevron-circle-right' title='Slide sau' style='font-size:50px; color: #4682B4; margin:0 10px;'></i></a> \r\n", slideIndex + 1);
                html += "<a href='javascript:HideSlide();'><i class='fa fa-stop' title='Kết thúc' style='font-size:50px; color: #4682B4; margin:0 10px;'></i></a> \r\n" +
                    "</div>\r\n" +
                "</div>\r\n";
                RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(html);
                RetAjaxOut.RetExtraParam1 = "Tiêu đề slide";
                RetAjaxOut.RetNumber = slidePropertys[slideIndex].type;
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
        public static AjaxOut GetSlideProperties(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);

                #region Thiết lập thuộc tính của danh sách slide
                int slideSequence = 0;
                List<SlideProperty> slideProperties = new List<SlideProperty>();
                slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.Start });
                slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.ThongTinHanhChinh });
                if (!string.IsNullOrEmpty(caBenh.LYDOVAOVIEN) || !string.IsNullOrEmpty(caBenh.BENHSU) || !string.IsNullOrEmpty(caBenh.TIENSUBENH))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.HoiBenh });
                if (!string.IsNullOrEmpty(caBenh.TOANTHAN) || !string.IsNullOrEmpty(caBenh.BOPHAN))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.KhamBenhVaoVien });
                if (!string.IsNullOrEmpty(caBenh.CHANDOANSOBO) || !string.IsNullOrEmpty(caBenh.CANLAMSANG))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.ChanDoanCanLamSang });
                if (!string.IsNullOrEmpty(caBenh.NXKETQUAXN) || CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Reading(ORenderInfo, new KetQuaXetNghiemFilterCls() { CABENHID = caBenh.ID }).Count() > 0)
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.XetNghiem });
                if (!string.IsNullOrEmpty(caBenh.KETQUACDHA))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.ChanDoanHinhanh });
                HinhAnhCls[] hinhAnhs = CallBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Reading(ORenderInfo, new HinhAnhFilterCls() { CABENHID = caBenh.ID });
                for (int i = 0; i < hinhAnhs.Count(); i++)
                {
                    if (hinhAnhs[i].TYPE == (int)HinhAnh.eType.DICOM || System.IO.File.Exists(hinhAnhs[i].DUONGDAN))
                        slideProperties.Add(new SlideProperty { stt = slideSequence, type = (int)eSlideType.HinhAnh, value = hinhAnhs[i].ID });
                }
                if (!string.IsNullOrEmpty(caBenh.CHANDOANXACDINH) || !string.IsNullOrEmpty(caBenh.PHAUTHUAT) || !string.IsNullOrEmpty(caBenh.THUTHUAT))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.ChanDoanPhauThuat });
                if (!string.IsNullOrEmpty(caBenh.THONGTINDIEUTRI))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.DieuTri });
                if (!string.IsNullOrEmpty(caBenh.CAUHOI))
                    slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.CauHoi });
                slideProperties.Add(new SlideProperty { stt = slideSequence++, type = (int)eSlideType.End });
                RetAjaxOut.RetObject = slideProperties.ToArray();
                #endregion
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
        public static AjaxOut DrawSlideKetQuaXetNghiems(RenderInfoCls ORenderInfo, string caBenhId, string ketQuaXetNghiemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                var ketQuaXetNghiems = CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Reading(ORenderInfo, new KetQuaXetNghiemFilterCls() { CABENHID = caBenhId }).ToList();
                WebSessionUtility.SetSession(OSiteParam, "ThamVanCaBenh_KetQuaXetNghiems", ketQuaXetNghiems);
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=40 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ") + " </th> \r\n" +
                                    "<th width=250 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Ngày kết quả") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < ketQuaXetNghiems.Count; iIndex++)
                {
                    string tenDichVu = ketQuaXetNghiems[iIndex].DICHVUTEN;
                    if (string.IsNullOrEmpty(tenDichVu))
                    {                       
                        OneMES3.DM.Model.DichVuCls dichVu = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDichVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), ketQuaXetNghiems[iIndex].DICHVUMA);                      
                        if (dichVu != null)
                            tenDichVu = dichVu.Ten;
                        else tenDichVu = ketQuaXetNghiems[iIndex].DICHVUMA;
                    }
                    html += "<tr style='cursor:pointer' id='KetQuaXNRow_" + iIndex + "' " + (ketQuaXetNghiems[iIndex].ID == ketQuaXetNghiemId ? "class='seletedRow'" : null) + " onclick=\"SlideKetQuaXNRow_OnClick('KetQuaXNRow_" + iIndex + "', '" + ketQuaXetNghiems[iIndex].ID + "');\" > \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + tenDichVu + "</td> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (ketQuaXetNghiems[iIndex].THOIGIAN == null ? null : ketQuaXetNghiems[iIndex].THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy")) + "</td> \r\n" +
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
        public static AjaxOut DrawSlideKetQuaXetNghiemChiTiets(RenderInfoCls ORenderInfo, string ketQuaXetNghiemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                var ketQuaXetNghiemChiTiets = string.IsNullOrEmpty(ketQuaXetNghiemId) ? new List<KetQuaXetNghiemChiTietCls>() : CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Reading(ORenderInfo, new KetQuaXetNghiemChiTietFilterCls() { KETQUAXETNGHIEMID = ketQuaXetNghiemId }).ToList();
                string html =
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=40 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chỉ số") + " </th> \r\n" +
                                    "<th width=200 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Giá trị") + " </th> \r\n" +
                                    "<th width=80 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "ĐVT") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < ketQuaXetNghiemChiTiets.Count; iIndex++)
                {
                    string tenChiSo = ketQuaXetNghiemChiTiets[iIndex].CHISOTEN;
                    string donViTinh = "";
                    if (string.IsNullOrEmpty(tenChiSo))
                    {                       
                        OneMES3.DM.Model.ChiSoXetNghiemCls chiSoXN = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChiSoXetNghiemProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), ketQuaXetNghiemChiTiets[iIndex].CHISOMA);                      
                        if (chiSoXN != null)
                        {
                            tenChiSo = chiSoXN.Ten;
                            donViTinh = chiSoXN.DonViDo;
                        }
                        else tenChiSo = ketQuaXetNghiemChiTiets[iIndex].CHISOMA;
                    }
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td>" + tenChiSo + "</td> \r\n" +
                                "<td style='text-align: center;'>" + ketQuaXetNghiemChiTiets[iIndex].GIATRI + "</td> \r\n" +
                                "<td style='text-align: center;'>" + donViTinh + "</td> \r\n" +
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh != null && caBenh.TRANGTHAI != null)
                    return CaBenhParser.sColorTrangThai[caBenh.TRANGTHAI.Value];
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return null;
        }
        //Hàm chuyển đổi chuỗi tạo ra từ textarea sang chuỗi html
        public static string ToHtml(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return inputString;
            return inputString.Replace("\n", "<br>");
        }
        #endregion
        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetUrlTomTatBenhAn(RenderInfoCls ORenderInfo, string caBenhId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(caBenh.KHAMCHUABENHID))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này không có tóm tắt bệnh án.");
                    return RetAjaxOut;
                }
                var tomTatBenhAnUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "TomTatBenhAn", new WebParamCls[] { new WebParamCls("cbid", caBenh.ID) });
                RetAjaxOut.RetUrl = tomTatBenhAnUrl;
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
        public static AjaxOut CapNhatTrangThaiCaBenh(RenderInfoCls ORenderInfo, string caBenhId, long recordTotal, int chiSoMang, string keyword, string cskcbYeuCau, string chuyenKhoaMa, int? trangThai, byte loaiThoiGian, string tuNgay, string denNgay, int tacVu, string butPhe = null)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CaBenhCls caBenh = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ORenderInfo, caBenhId);
                if (caBenh == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Ca bệnh này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                caBenh.TRANGTHAI = tacVu;
                caBenh.LICHHOICHANID = null;
                //Cập nhật trạng thái chưa xem của người lập lịch 
                if (caBenh.TRANGTHAI == (int)CaBenhCls.eTrangThai.ChoLapLich)
                {
                    caBenh.LAPLICHDAXEM = 0;
                }
                //Nếu chuyển hội chẩn ca bệnh thì cập nhật người duyệt và thời gian duyệt vào ca bệnh.
                if (tacVu == (int)OneTSQ.Common.TuVanCaBenh.eTacVu.ChuyenHoiChan)
                {
                    caBenh.DUYETHOICHANBOI = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    caBenh.DUYETHOICHANVAO = DateTime.Now;
                }
                if (caBenh.TRANGTHAI == (int)CaBenhCls.eTrangThai.ChoTiepNhan)
                {
                    caBenh.DUYETHOICHANBOI = null;
                    caBenh.DUYETHOICHANVAO = null;
                }
                CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Save(ORenderInfo, caBenhId, caBenh);
                //Luu noi dung but phe
                if (!string.IsNullOrWhiteSpace(butPhe))
                {
                    var user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                    ButPheCls oButPhe = new ButPheCls();
                    oButPhe.CABENHID = caBenhId;
                    oButPhe.THOIGIAN = DateTime.Now;
                    oButPhe.NGUOIPHE = user.FullName + " (" + user.LoginName + ")";
                    oButPhe.HANHDONG = tacVu;
                    oButPhe.NOIDUNG = butPhe;
                    CallBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Add(ORenderInfo, oButPhe);
                    RetAjaxOut.HtmlContent = DrawButPheList(ORenderInfo, caBenhId).HtmlContent;
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
        private static bool SendThongBaoGiayMoi(RenderInfoCls ORenderInfo, CaBenhCls caBenh, LichHoiChanCls lichHoiChan)
        {
            OwnerCls donViThamVan = string.IsNullOrEmpty(caBenh.DONVITHAMVANID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITHAMVANID);
            OwnerCls donViTuVan = string.IsNullOrEmpty(caBenh.DONVITUVANID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenh.DONVITUVANID);
            OneMES3.DM.Model.IcdCls chanDoan = null;
            if (!string.IsNullOrEmpty(caBenh.CHANDOANBANDAUMA))
            {
                chanDoan = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateIcdProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), caBenh.CHANDOANBANDAUMA);
            }
            string emailSubject, emailBody;

            #region SendThongBaoLichHoiChan
            var nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, caBenh.TAOBOI);
            if (nguoiTao != null && !string.IsNullOrEmpty(nguoiTao.Email))
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                emailSubject = WebLanguage.GetLanguage(OSiteParam, "THÔNG BÁO LỊCH HỘI CHẨN BỆNH NHÂN");
                emailBody = "           <table style='max-width:800px;'>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2 style='text-align:center;'>\r\n" +
                                                    "<h2><b>" + WebLanguage.GetLanguage(OSiteParam, "LỊCH HỘI CHẨN BỆNH NHÂN") + "</b></h2>\r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2 style='text-align:center;'>\r\n" +
                                                    "<b><u><i>" + WebLanguage.GetLanguage(OSiteParam, "Kính gửi:") + "</i></u></b> \r\n" +
                                                    (donViThamVan != null ? donViThamVan.OwnerName : null) + "<br><br> \r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2 style='text-indent:40px;'>\r\n" +
                                                    "<b>" + (donViTuVan != null ? donViTuVan.OwnerName : null) + WebLanguage.GetLanguage(OSiteParam, " trân trọng thông báo đến quý bệnh viện lịch hội chẩn bệnh nhân:") + " </b><br>\r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "- Mã:") + " </b>\r\n" +
                                                    caBenh.MABN + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Họ và tên: ") + " </b>\r\n" +
                                                    caBenh.HOTENBN + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Tuổi: ") + " </b>\r\n" +
                                                    BenhNhan.TinhTuoi(caBenh.NGAYSINH, caBenh.NGAYNHAPVIEN, caBenh.NGAYXUATVIEN) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính: </b>\r\n" +
                                                    (caBenh.GIOITINH == null ? null : BenhNhan.GioiTinhs[caBenh.GIOITINH.Value]) + " \r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "- Giường:") + " </b>\r\n" +
                                                    caBenh.GIUONG + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Phòng:") + " </b>\r\n" +
                                                    caBenh.PHONG + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Viện/Trung tâm/Khoa:") + " </b>\r\n" +
                                                    caBenh.NHAPVIENTAI + " \r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "- Chẩn đoán:") + " </b>\r\n" +
                                                    (chanDoan != null ? chanDoan.Ma + " - " + chanDoan.Ten : null) +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "- Lý do hội chẩn:") + " </b>\r\n" +
                                                    caBenh.LYDOYEUCAU +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian:") + " </b>\r\n" +
                                                    lichHoiChan.BATDAU.Value.ToString("HH") + WebLanguage.GetLanguage(OSiteParam, " giờ ") + lichHoiChan.BATDAU.Value.ToString("mm") + WebLanguage.GetLanguage(OSiteParam, " phút, ngày ") + lichHoiChan.BATDAU.Value.ToString("dd/MM/yyyy") +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Địa điểm:") + " </b>\r\n" +
                                                    lichHoiChan.DIADIEM +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td colspan=2 style='text-indent:40px;'>\r\n" +
                                                    "<b>" + WebLanguage.GetLanguage(OSiteParam, "Xin trân trọng cảm ơn.") + "/.</b>\r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "               <tr>\r\n" +
                            "                   <td width='50%'>\r\n" +
                            "                   </td>\r\n" +
                            "                   <td width='50%' style='text-align:center;'>\r\n" +
                                                    "<br><br><b>" + WebLanguage.GetLanguage(OSiteParam, "GIÁM ĐỐC BỆNH VIỆN") + "</b><br><br>\r\n" +
                            "                   </td>\r\n" +
                            "               </tr>\r\n" +
                            "           </table>\r\n");
                SendEmail(nguoiTao.Email, emailSubject, emailBody);
            }

            #endregion

            #region SendGiayMoiHoiChan
            SiteParam
                   OSiteParam2 = WebEnvironments.CreateSiteParam(ORenderInfo);
            emailSubject = WebLanguage.GetLanguage(OSiteParam2, "GIẤY MỜI HỘI CHẨN");
            emailBody = "           <table style='max-width:600px;'>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2 style='text-align:center;'>\r\n" +
                                                "<h2><b>" + WebLanguage.GetLanguage(OSiteParam2, "GIẤY MỜI HỘI CHẨN") + "</b></h2>\r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2 style='text-align:center;'>\r\n" +
                                                "<b><u><i>" + WebLanguage.GetLanguage(OSiteParam2, "Kính gửi:") + "</i></u></b> " + WebLanguage.GetLanguage(OSiteParam2, "Ông(bà)" + " {0}<br><br>\r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2 style='text-indent:40px;'>\r\n" +
                                                "<b>" + (donViTuVan != null ? donViTuVan.OwnerName : null) + WebLanguage.GetLanguage(OSiteParam2, " trân trọng kính mời Ông(bà)") + " </b><br> " + WebLanguage.GetLanguage(OSiteParam2, "hội chẩn bệnh nhân:") + "\r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "- Họ và tên:") + " </b>\r\n" +
                                                caBenh.HOTENBN + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Tuổi:") + " </b>\r\n" +
                                                BenhNhan.TinhTuoi(caBenh.NGAYSINH, caBenh.NGAYNHAPVIEN, caBenh.NGAYXUATVIEN) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Giới tính:") + " </b>\r\n" +
                                                (caBenh.GIOITINH == null ? null : BenhNhan.GioiTinhs[caBenh.GIOITINH.Value]) + " \r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "- Giường:") + " </b>\r\n" +
                                                caBenh.GIUONG + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Phòng:") + " </b>\r\n" +
                                                caBenh.PHONG + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Viện/Trung tâm/Khoa:") + " </b>\r\n" +
                                                caBenh.NHAPVIENTAI + " \r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "- Chẩn đoán:") + " </b>\r\n" +
                                                (chanDoan != null ? chanDoan.Ma + " - " + chanDoan.Ten : null) +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "- Lý do hội chẩn:") + " </b>\r\n" +
                                                caBenh.LYDOYEUCAU +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Thời gian:") + " </b>\r\n" +
                                                lichHoiChan.BATDAU.Value.ToString("HH") + WebLanguage.GetLanguage(OSiteParam2, " giờ ") + lichHoiChan.BATDAU.Value.ToString("mm") + WebLanguage.GetLanguage(OSiteParam2, " phút, ngày ") + lichHoiChan.BATDAU.Value.ToString("dd/MM/yyyy") +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Địa điểm:") + " </b>\r\n" +
                                                lichHoiChan.DIADIEM +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td colspan=2 style='text-indent:40px;'>\r\n" +
                                                "<b>" + WebLanguage.GetLanguage(OSiteParam2, "Xin trân trọng cảm ơn.") + "/.</b>\r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "               <tr>\r\n" +
                        "                   <td width='50%'>\r\n" +
                        "                   </td>\r\n" +
                        "                   <td width='50%' style='text-align:center;'>\r\n" +
                                                "<br><br><b>" + WebLanguage.GetLanguage(OSiteParam2, "GIÁM ĐỐC BỆNH VIỆN") + "</b><br><br>\r\n" +
                        "                   </td>\r\n" +
                        "               </tr>\r\n" +
                        "           </table>\r\n");
            LapLichThanhVienHoiChanCls[] lapLichThanhVienHoiChans = CallBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Reading(ORenderInfo, new LapLichThanhVienHoiChanFilterCls() { LICHHOICHANID = lichHoiChan.ID });
            foreach (LapLichThanhVienHoiChanCls lapLichThanhVienHoiChan in lapLichThanhVienHoiChans)
            {
                var thanhVienHoiChan = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lapLichThanhVienHoiChan.BACSYID);
                if (thanhVienHoiChan != null && !string.IsNullOrEmpty(thanhVienHoiChan.EMAIL))
                {
                    var personalEmailBody = string.Format(emailBody, thanhVienHoiChan != null ? thanhVienHoiChan.HOTEN : null);
                    SendEmail(thanhVienHoiChan.EMAIL, emailSubject, personalEmailBody);
                }
            }
            #endregion
            return true;
        }
        private static bool SendEmail(string emailTo, string subject, string body)
        {
            try
            {
                string smtpAddress = "smtp.gmail.com";//gmail
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmail"];
                string password = System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmailPass"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;// Can set to false, if you are sending pure text.

                SmtpClient SmtpServer = new SmtpClient(smtpAddress, portNumber);
                SmtpServer.Credentials = new NetworkCredential(emailFrom, password);
                SmtpServer.EnableSsl = enableSSL;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
