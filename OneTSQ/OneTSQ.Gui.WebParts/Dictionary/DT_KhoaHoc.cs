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
    public class DT_KhoaHoc : WebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "DT_KhoaHoc";
        public override string WebPartTitle { get { return "Khóa học"; } }
        public override string Description { get { return "Khóa học"; } }
        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                //return new string[] { StaticWebPartId, DT_KhoaHoc.StaticWebPartId };
                DT_KhoaHocs dts = new DT_KhoaHocs();
                return new string[] { dts.WebPartId, dts.WebPartId };
            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_KhoaHoc), Page);
        }
        #region Vẽ giao diện
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string khoaHocId = WebEnvironments.Request("Id");
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                if (khoaHoc == null)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Khóa học này vừa bị xóa khỏi hệ thống."), false);
                    return RetAjaxOut;
                }
                string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                bool xemPermission = khoaHoc.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xem.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, UserId, khoaHoc.NGUOITAO_ID);
                bool suaPermission = khoaHoc.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Sua.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, UserId, khoaHoc.NGUOITAO_ID);
                bool xoaPermission = khoaHoc.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.Xoa.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, UserId, khoaHoc.NGUOITAO_ID);
                bool guiDuyetPermission = khoaHoc.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.GuiDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, UserId, khoaHoc.NGUOITAO_ID);
                bool pheDuyetPermission = khoaHoc.NGUOITAO_ID == user.OwnerUserId || PermissionUtility.CheckPermission(ORenderInfo, new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocCls.ePermission.PheDuyet.ToString(), new DT_KhoaHocPermission().PermissionFunctionCode, DT_KhoaHocPermission.StaticPermissionFunctionId, UserId, khoaHoc.NGUOITAO_ID);

                DM_TenKhoaHocCls dmTenKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CreateModel(ORenderInfo, khoaHoc.TEN);
                string doiTuong = "";
                string[] doiTuongs;
                if (string.IsNullOrEmpty(khoaHoc.DOITUONG))
                    doiTuongs = new string[0];
                else doiTuongs = khoaHoc.DOITUONG.Split('|');
                foreach (string dt in doiTuongs)
                    doiTuong += DT_KhoaHocParser.DoiTuongs[Int32.Parse(dt)] + ", ";
                if (!string.IsNullOrEmpty(doiTuong))
                    doiTuong = doiTuong.Substring(0, doiTuong.Length - 2);

                OneMES3.DM.Model.BenhVienCls donViHoTro = null;
                if (!string.IsNullOrEmpty(khoaHoc.DONVIHOTRO_MA))
                {

                     donViHoTro = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), khoaHoc.DONVIHOTRO_MA);                   
                }

                string[] tieuChuans;
                if (string.IsNullOrEmpty(khoaHoc.TIEUCHUAN))
                    tieuChuans = new string[0];
                else tieuChuans = khoaHoc.TIEUCHUAN.Split('|');
                string tenTieuChuans = "";
                foreach (var tieuChuan in tieuChuans)
                {
                    var oTieuChuan = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ORenderInfo, tieuChuan);
                    tenTieuChuans += (oTieuChuan != null ? oTieuChuan.Ten : tieuChuan) + ", ";
                }
                if (tenTieuChuans != "")
                    tenTieuChuans = tenTieuChuans.Substring(0, tenTieuChuans.Length - 2);

                string tokenURL = WebConfig.GetWebConfig("TokenURL");
                string createRoomURL = WebConfig.GetWebConfig("CreateRoomURL");
                string stringeeServerAddr = WebConfig.GetWebConfig("StringeeServerAddr");
                #region Html
                string html =
                    "<style>\r\n" +
                    "   .clsAlert{color: red;}\r\n" +
                    "</style>\r\n" +
                    "     <div id=\"divTopButton\" class='col-lg-12' style='margin-top: 10px; padding-bottom: 10px;'> \r\n" +
                     "       <input type='button' id='btnSua' title='Sửa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' onclick='javascript:ShowPopupKhoaHoc(\"" + khoaHocId + "\");' style='float:left; margin-left: 14px; " + (suaPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Moi ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnXoa' title='Xóa' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteKhoaHoc(\"" + khoaHocId + "\");' style='float:left; margin-left: 14px; " + (xoaPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Moi ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnGuiDuyet' title='Gửi duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Gửi duyệt") + "' onclick='javascript:ChuyenDuyet();' style='float:left; margin-left: 14px; " + (guiDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Moi ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnThuHoiGuiDuyet' title='Thu hồi gửi duyệt' class='DT_KhoaHocThuHoi btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiChuyenDuyet();' style='float:left; margin-left: 14px; " + (guiDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.ChoDuyet ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnDuyet' title='Duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "' onclick='javascript:Duyet();' style='float:left; margin-left: 14px;  " + (pheDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.ChoDuyet ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnTuChoiDuyet' title='Từ chối duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Từ Chối") + "' onclick='javascript:TuChoiDuyet();' style='float:left; margin-left: 14px; " + (pheDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.ChoDuyet ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnThuHoiDuyet' title='Thu hồi duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiDuyet();' style='float:left; margin-left: 14px; " + (pheDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet ? null : "display:none;") + "'>\r\n" +
                    "       <input type='button' id='btnThuHoiTuChoiDuyet' title='Thu hồi từ chối duyệt' class='btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Thu hồi") + "' onclick='javascript:ThuHoiTuChoiDuyet();' style='float:left; margin-left: 14px; " + (pheDuyetPermission && khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.TuChoi ? null : "display:none;") + "'>\r\n" +
                    "     </div>\r\n" +
                    "<div class='row'>\r\n" +
                    "   <div class='col-lg-12'>\r\n" +
                    "      <div class='ibox float-e-margins'>\r\n" +
                    "          <div class='ibox-title'>\r\n" +
                    "              <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin khóa học") + "</h5>\r\n" +
                    "          </div>\r\n" +
                    "          <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-3 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Mã khóa học:") + " \r\n" +
                    "                           <span id='spMa' class=\"valueForm\">" + khoaHoc.MA + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-3 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +WebLanguage.GetLanguage(OSiteParam, "Khóa học số:") + " \r\n" +
                    "                           <span id='spKhoa' class=\"valueForm\">" + khoaHoc.KHOA + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học:") + " \r\n" +
                    "                          <span id='spTen' class=\"valueForm\">" + (dmTenKhoaHoc == null ? khoaHoc.TEN : dmTenKhoaHoc.Ten) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Thời lượng học:") + " \r\n" +
                    "                          <span id='spThoiLuong' class=\"valueForm\">" + khoaHoc.THOILUONG + "</span>\r\n" +
                    "                          <span id='spLoaiThoiLuong' class=\"valueForm\">" + (string.IsNullOrEmpty(khoaHoc.LOAITHOILUONG) ? khoaHoc.LOAITHOILUONG : DT_KhoaHocParser.LoaiThoiLuongs[khoaHoc.LOAITHOILUONG]) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Số học viên dự kiến:") + " \r\n" +
                    "                          <span id='spSoLuongHocVienDuKien' class=\"valueForm\">" + khoaHoc.SOLUONGHOCVIENDUKIEN + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Học phí:") + " \r\n" +
                    "                          <span id='spHocPhi' class=\"valueForm\">" + (khoaHoc.HOCPHI == null ? null : khoaHoc.HOCPHI.Value.ToString("#,##0.00", new CultureInfo("en-US"))) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-8 col-xs-8'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +WebLanguage.GetLanguage(OSiteParam, "Thời gian dự kiến:") + " \r\n" +
                    "                         <span id='spNgayKhaiGiangDuKien' class=\"valueForm\">" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                         <span id='spNgayBeGiangDuKien' class=\"valueForm\">" + (khoaHoc.NGAYBEGIANGDUKIEN == null ? null : khoaHoc.NGAYBEGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Hạn nộp hồ sơ:") + " \r\n" +
                    "                          <span id='spHanNopHoSo' class=\"valueForm\">" + (khoaHoc.HANNOPHOSO == null ? null : khoaHoc.HANNOPHOSO.Value.ToString("dd/MM/yyyy")) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Loại đào tạo:") + " \r\n" +
                    "                           <span id='spLoaiDaoTao' class=\"valueForm\">" + DT_KhoaHocParser.LoaiDaoTaos[khoaHoc.LOAIDAOTAO] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Loại hình đào tạo:") + " \r\n" +
                    "                           <span id='spLoaiHinhDaoTao' class=\"valueForm\">" + DT_KhoaHocParser.LoaiHinhDaoTaos[khoaHoc.LOAIHINHDAOTAO] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-4 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Đối tượng:") + " \r\n" +
                    "                          <span id='spDoiTuong' class=\"valueForm\">" + doiTuong + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-6 col-xs-4'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" +WebLanguage.GetLanguage(OSiteParam, "Nguồn kinh phí:") + " \r\n" +
                    "                           <span id='spLoaiKhoaHoc' class=\"valueForm\">" + DT_KhoaHocParser.LoaiKhoaHocs[khoaHoc.LOAIKHOAHOC] + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "                   <div class='col-md-6 col-xs-8' id='divDonViHoTroViewer' style='" + (khoaHoc.LOAIKHOAHOC == (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc ? null : "display:none;") + "'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị nhận hỗ trợ:") + " \r\n" +
                    "                           <span id='spDonViHoTro' class=\"valueForm\">" + (donViHoTro == null ? khoaHoc.DONVIHOTRO_MA : donViHoTro.Ten) + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-12 col-xs-12'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn khóa học:") + " \r\n" +
                    "                           <span id='spTieuChuan' class=\"valueForm\">" + tenTieuChuans + "</span>\r\n" +
                    "                       </div>\r\n" +
                    "                   </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class='row'>" +
                    "                   <div class='col-md-12 col-xs-12'>\r\n" +
                    "                       <div class=\"form-group\">\r\n" + WebLanguage.GetLanguage(OSiteParam, "Mô tả:") + " \r\n" +
                    "                           <textarea id='txtMoTa' disabled rows=4 class='form-control'>" + khoaHoc.MOTA + "</textarea>\r\n" +
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
                    "<script src=\"../../../../themes/js/plugins/stringee/latest.sdk.bundle.min.js\" type=\"text/javascript\"></script>\r\n" +
                    "<script language=\"javascript\">\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "   var khoaHocId='" + khoaHocId + "';\r\n" +
                    "   var tokenUrl = \"" + tokenURL + "\";\r\n" +
                    "   var createRoomURL = \"" + createRoomURL + "\";\r\n" +
                    "   var stringeeServerAddr = \"" + stringeeServerAddr + "\";\r\n" +
                    "	var access_token;\r\n" +
                    "	var rest_access_token;\r\n" +
                    "   var stringeeClient;\r\n" +
                    "   var loginedStringeeAccount;\r\n" +
                    "   var roomId='" + khoaHoc.ROOMID + "';\r\n" +
                    "   $(document).ready(function() \r\n" +
                    "   {\r\n" +
                    "       ConnectStringeeServer();\r\n" +
                    "       ShowTrangThai();\r\n" +
                    "   });\r\n" +

                #region Show trạng thái khóa học
                    "function ShowTrangThai(){\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     $('#aTrangThai').html(OneTSQ.WebParts.DT_KhoaHoc.ShowTrangThai(RenderInfo, khoaHocId).value);\r\n" +
                    "}\r\n" +
                #endregion Show trạng thái khóa học

                #region Tạo room
                    "  function MakeRoom(){\r\n" +
                    //make room và create Conversation
                    "        var data = {\r\n" +
                    "            name : '" + khoaHoc.TEN + "',\r\n" +
                    "            uniqueName: '" + khoaHoc.ID + "'\r\n" +
                    "        }\r\n" +
                    "        $.ajax({\r\n" +
                    "            type: 'POST',\r\n" +
                    "            url: createRoomURL,\r\n" +
                   // "            headers: {'X-STRINGEE-AUTH': rest_access_token}, \r\n" +
                    "            dataType: 'json',\r\n" +
                    "            contentType: 'application/json',\r\n" +
                    "            data: JSON.stringify(data),\r\n" +
                    "            success: function(response) {\r\n" +
                    "                 if(response.r == 0 || response.r == 2)\r\n" +
                    "                 {\r\n" +
                    "                      console.log('MakeRoom: ', response);\r\n" +
                    "                      roomId = response.roomId;\r\n" +
                    "                      OneTSQ.WebParts.DT_KhoaHoc.UpdateRoomId(RenderInfo, khoaHocId, roomId).value;\r\n" +
                    "                 }\r\n" +
                    "                 else{\r\n" +
                    "                 }\r\n" +
                    "            },\r\n" +
                    "            error : function(response) { \r\n" +
                    "            } \r\n" +
                    "        });\r\n" +
                    "       $('#divFormModal').modal('hide');\r\n" +
                    "  }\r\n" +
                #endregion

                #region Ket noi den Stringee Server
                    "function ConnectStringeeServer() {\r\n" +
                    "   stringeeClient = new StringeeClient();\r\n" +
                    "   stringeeClient._stringeeServerAddr = stringeeServerAddr;\r\n" +
                    "   SettingClientEvents(stringeeClient);\r\n" +
                    "   GetAccessTokenAndConnectToStringee(stringeeClient);\r\n" +
                    " }\r\n" +
                #endregion

                #region GetAccessTokenAndConnectToStringee để tạo roomToken
                    "function GetAccessTokenAndConnectToStringee(client) {\r\n" +
                    "    $.getJSON(tokenUrl + '?userId=" + user.LoginName + " &rest=true', function(res) {\r\n" +
                    "        access_token = res.access_token;\r\n" +
                    "        rest_access_token = res.rest_access_token;\r\n" +
                    "        client.connect(access_token);\r\n" +
                    "    });\r\n" +
                    "}\r\n" +
                #endregion

                #region control
                    "function ValidateIntegerControl(control, min, max)\r\n" +
                    "    { \r\n" +
                    "        $(control).on('keydown keyup blur', function(e){\r\n" +
                    "                            if (($(this).val() > max || $(this).val() < min) && e.keyCode !== 46 && e.keyCode !== 8){\r\n" +
                    "                               e.preventDefault();\r\n" +
                    "                $(this).val(min);\r\n" +
                    "                           }\r\n" +
                    "            else if (e.keyCode != 8 && e.keyCode != 0 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40 && e.keyCode != 9 && e.keyCode != 13 && (e.keyCode < 48 || e.keyCode > 57) && !(e.keyCode <= 105 && e.keyCode >= 96))\r\n" +
                    "                                e.preventDefault();\r\n" +
                    //        TH: Nhập 1. không đúng định dạng, click chuột sang control mới xét lại giá trị của nó về rỗng
                    "                            else if (e.bubbles == false && e.type == 'blur' && !this.checkValidity())\r\n" +
                    "                $(this).val('');\r\n" +
                    "                       });" +
                    "    }\r\n" +
                #endregion

                #region Các sự kiện stringee client
                    "    function SettingClientEvents(client){\r\n" +
                #region Các sự kiện kết nối stringee
                    "        client.on('connect', function () {\r\n" +
                    "            console.log('connected');\r\n" +
                    "        });\r\n" +

                    "        client.on('authen', function (res) {\r\n" +
                    "            console.log('authen', res);\r\n" +
                    "            loginedStringeeAccount = res.userId;\r\n" +
                    "        });\r\n" +

                    "        client.on('disconnect', function () {\r\n" +
                    "            console.log('disconnected');\r\n" +
                    "        });\r\n" +

                    "        client.on('requestnewtoken', function () {\r\n" +
                    "            console.log('++++++++++++++ requestnewtoken; please get new access_token from YourServer and call client.connect(new_access_token)+++++++++');\r\n" +
                    "            //please get new access_token from YourServer and call: \r\n" +
                    "            GetAccessTokenAndConnectToStringee(client);\r\n" +
                    "        });\r\n" +
                #endregion                
                    "    }\r\n" +
                #endregion

                #region Show popup khóa học
                    "function ShowPopupKhoaHoc(khoaHocId)\r\n" +
                    "{\r\n" +
                    "     RenderInfo=CreateRenderInfo();\r\n" +
                    "     AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.PopupKhoaHoc(RenderInfo, khoaHocId).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "     document.getElementById('ModalTitle').innerHTML= '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới/cập nhật khóa học") + "</span>';\r\n" +
                    "     $('#divFormModal').modal('show');\r\n" +
                    "     $('.datepicker').datetimepicker({ \r\n" +
                    "         format: 'DD/MM/YYYY' \r\n" +
                    "     }); \r\n" +
                    //"     $('#cbbLoaiKhoaHoc').selectpicker({ \r\n" +
                    //"     }); \r\n" +
                    //"     $('#cbbDoiTuong').selectpicker({ \r\n" +
                    //"     }); \r\n" +
                    "     ValidateIntegerControl('#txtSoLuongHocVienDuKien', 0, 999);\r\n" +
                    "     CallInitSelect2('cbbTenKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TenKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbDonViHoTro', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị hỗ trợ") + "');\r\n" +
                    "     CallInitSelect2('cbbTieuChuan', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TieuChuanThamGiaKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn tham gia khóa học") + "');\r\n" +
                    "}\r\n" +
                #endregion 

                #region Cập nhật mã khóa học theo thay đổi combobox tên khóa học và textbox khóa
                    "   function FillMaKhoaHoc(){\r\n" +
                    "       if(cbbTenKhoaHoc.value != '' && txtKhoa.value != '')\r\n" +
                    "       {\r\n" +
                    "           txtMa.value = cbbTenKhoaHoc.value + txtKhoa.value;\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           txtMa.value = '';\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion

                #region Cập nhật ngày kết thúc khóa học theo thay đổi textbox thời lượng, combobox Loại thời lượng và textbox ngày khai giảng dự kiến
                    "   function FillNgayKetThucKhoaHoc(){\r\n" +
                    "       if(txtThoiLuong.value != '' && cbbLoaiThoiLuong.value != '' && dtNgayKhaiGiangDuKien.value != '')\r\n" +
                    "       {\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.GetNgayKetThucKhoaHoc(RenderInfo, dtNgayKhaiGiangDuKien.value, parseInt(txtThoiLuong.value), cbbLoaiThoiLuong.value).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               dtNgayBeGiangDuKien.value = AjaxOut.RetExtraParam1;\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           dtNgayBeGiangDuKien.value = '';\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion

                #region Cập nhật hạn nộp hồ sơ theo thay đổi textbox ngày khai giảng dự kiến
                    "   function FillHanNopHoSo(){\r\n" +
                    "       if(dtNgayKhaiGiangDuKien.value != '')\r\n" +
                    "       {\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.GetNgayNopHoSo(RenderInfo, dtNgayKhaiGiangDuKien.value).value;\r\n" +
                    "           if(!AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               dtHanNopHoSo.value = AjaxOut.RetExtraParam1;\r\n" +
                    "               if(AjaxOut.RetBoolean)\r\n" +
                    "               {\r\n" +
                    "                   $('#dtNgayKhaiGiangDuKien').addClass('clsAlert');\r\n" +
                    "               }\r\n" +
                    "               else\r\n" +
                    "               {\r\n" +
                    "                   $('#dtNgayKhaiGiangDuKien').removeClass('clsAlert');\r\n" +
                    "               }\r\n" +
                    "               if(AjaxOut.RetObject==true)\r\n" +
                    "               {\r\n" +
                    "                   $('#dtHanNopHoSo').addClass('clsAlert');\r\n" +
                    "               }\r\n" +
                    "               else\r\n" +
                    "               {\r\n" +
                    "                   $('#dtHanNopHoSo').removeClass('clsAlert');\r\n" +
                    "               }\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           dtHanNopHoSo.value = '';\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion

                #region Bắt sự kiện thay đổi combobox nguồn kinh phí
                    "   function cbbLoaiKhoaHoc_onchange(sender){\r\n" +
                    "       if(sender.value == " + (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc + ")\r\n" +
                    "       {\r\n" +
                    "           $('#divDonViHoTroEditor').show();\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           $('#divDonViHoTroEditor').hide();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion

                #region Save Thông tin khóa học
                    "function SaveKhoaHoc()\r\n" +
                    "{\r\n" +
                    "     RenderInfo = CreateRenderInfo();\r\n" +
                    "     khoaHocId = document.getElementById('hdKhoaHocId').value;\r\n" +
                    "     ma = document.getElementById('txtMa').value;\r\n" +
                    "     ten = document.getElementById('cbbTenKhoaHoc').value;\r\n" +
                    "     khoa = document.getElementById('txtKhoa').value;\r\n" +
                    "     thoiLuong = parseInt(document.getElementById('txtThoiLuong').value);\r\n" +
                    "     loaiThoiLuong = document.getElementById('cbbLoaiThoiLuong').value;\r\n" +
                    "     ngayKhaiGiangDuKien = document.getElementById('dtNgayKhaiGiangDuKien').value;\r\n" +                  
                    "     ngayBeGiangDuKien = document.getElementById('dtNgayBeGiangDuKien').value;\r\n" +
                    "     hanNopHoSo = document.getElementById('dtHanNopHoSo').value;\r\n" +
                    "     soLuongHocVienDuKien = parseInt(document.getElementById('txtSoLuongHocVienDuKien').value);\r\n" +
                    "     hocPhi = parseFloat(document.getElementById('txtHocPhi').value == '' ? '' : parseFloat(document.getElementById('txtHocPhi').value.replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(',', '')).toFixed(2));\r\n" +
                    "     loaiDaoTao = parseInt(document.getElementById('cbbLoaiDaoTao').value);\r\n" +
                    "     loaiHinhDaoTao = parseInt(document.getElementById('cbbLoaiHinhDaoTao').value);\r\n" +
                    "     doiTuong = $('#cbbDoiTuong').val();\r\n" +
                    "     loaiKhoaHoc = parseInt(document.getElementById('cbbLoaiKhoaHoc').value);\r\n" +
                    "     donViHoTro = document.getElementById('cbbDonViHoTro').value;\r\n" +
                    "     tieuChuan = $('#cbbTieuChuan').val();\r\n" +
                    "     moTa = document.getElementById('txtMoTa').value;\r\n" +           
                    "     AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.SaveKhoaHoc(RenderInfo, khoaHocId, ma, ten, khoa, thoiLuong, loaiThoiLuong, ngayKhaiGiangDuKien, ngayBeGiangDuKien, hanNopHoSo, soLuongHocVienDuKien, hocPhi, loaiDaoTao, loaiHinhDaoTao, doiTuong, loaiKhoaHoc, donViHoTro, tieuChuan, moTa).value;\r\n" +
                    "     if(AjaxOut.Error)\r\n" +
                    "     {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "     }\r\n" +
                    "     if(AjaxOut.RetExtraParam1 != '')\r\n" +
                    "     {\r\n" +
                    "           document.getElementById('hdKhoaHocId').value = khoaHocId = AjaxOut.RetExtraParam1;\r\n" +
                    "           AddHistory(khoaHocId, '" + WebPartTitle + "', 'Thêm mới', 'Tác vụ form');\r\n" +
                    "     }\r\n" +
                    "     else\r\n" +
                    "           AddHistory(khoaHocId, '" + WebPartTitle + "', 'Cập nhật', 'Tác vụ form');\r\n" +
                    "     toastr.info(AjaxOut.InfoMessage);\r\n" +
                    "     if($('#hdClose').val() == 1)\r\n" +
                    "     {\r\n" +
                    "          $('#divFormModal').modal('hide');\r\n" +
                    "     }\r\n" +
                    "     $('#spMa').html(ma);\r\n" +
                    "     $('#spKhoa').html(khoa);\r\n" +
                    "     $('#spTen').html(cbbTenKhoaHoc.options[cbbTenKhoaHoc.options.selectedIndex].text);\r\n" +
                    "     $('#spThoiLuong').html(thoiLuong + ' ' + cbbLoaiThoiLuong.options[cbbLoaiThoiLuong.options.selectedIndex].text);\r\n" +
                    "     $('#spSoLuongHocVienDuKien').html(document.getElementById('txtSoLuongHocVienDuKien').value);\r\n" +
                    "     $('#spHocPhi').html(document.getElementById('txtHocPhi').value == '' ? '' : GetFormatCurrency(parseFloat(document.getElementById('txtHocPhi').value.replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(',', '')).toFixed(2)));\r\n" +
                    "     $('#spNgayKhaiGiangDuKien').html(ngayKhaiGiangDuKien);\r\n" +
                    "     $('#spNgayBeGiangDuKien').html(ngayBeGiangDuKien);\r\n" +
                    "     $('#spHanNopHoSo').html(hanNopHoSo);\r\n" +
                    "     $('#spLoaiDaoTao').html(cbbLoaiDaoTao.options[cbbLoaiDaoTao.options.selectedIndex].text);\r\n" +
                    "     $('#spLoaiHinhDaoTao').html(cbbLoaiHinhDaoTao.options[cbbLoaiHinhDaoTao.options.selectedIndex].text);\r\n" +
                    "     $('#spDoiTuong').html($('#cbbDoiTuong').prev().prev().children().html());\r\n" +
                    "     $('#spLoaiKhoaHoc').html(cbbLoaiKhoaHoc.options[cbbLoaiKhoaHoc.options.selectedIndex].text);\r\n" +
                    "     if(loaiKhoaHoc==" + (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc + "){\r\n" +
                    "           $('#spDonViHoTro').html(cbbDonViHoTro.options.selectedIndex >= 0 ? cbbDonViHoTro.options[cbbDonViHoTro.options.selectedIndex].text : null);\r\n" +
                    "           $('#divDonViHoTroViewer').show();\r\n" +
                    "     }\r\n" +
                    "     else{\r\n" +
                    "           $('#divDonViHoTroViewer').hide();\r\n" +
                    "     }\r\n" +
                    "     $('#spTieuChuan').html(AjaxOut.RetExtraParam2);\r\n" +
                    "}\r\n" +
                #endregion

                #region Xóa Thông tin khóa học
                    "function DeleteKhoaHoc(khoaHocId)\r\n" +
                    "{\r\n" +
                    "   swal({\r\n" +
                    "           title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "           text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắn chắn xóa khóa học này không?") + "\",\r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.DeleteKhoaHoc(RenderInfo, khoaHocId).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               DeleteHistory(khoaHocId);\r\n" +
                    "               window.location.href = AjaxOut.RetUrl;\r\n" +
                    "           }\r\n " +
                    "       });\r\n" +
                    "}\r\n" +
                #endregion

                #region Chuyển duyệt khóa học
                    "   function ChuyenDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn gửi duyệt khóa học này?\", \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã gửi duyệt!") + "');\r\n" +
                    "                   $('#btnGuiDuyet').hide();\r\n" +
                    "                   $('#btnThuHoiGuiDuyet').show();\r\n" +
                    "                   if('" + suaPermission + "' == 'True')\r\n" +
                    "                       $('#btnSua').hide();\r\n" +
                    "                   if('" + xoaPermission + "' == 'True')\r\n" +
                    "                       $('#btnXoa').hide();\r\n" +
                    "                   if('" + pheDuyetPermission + "' == 'True')\r\n" +
                    "                   {\r\n" +
                    "                       $('#btnDuyet').show();\r\n" +
                    "                       $('#btnTuChoiDuyet').show();\r\n" +
                    "                   }\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Gửi duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Thu hồi chuyển duyệt khóa học
                    "   function ThuHoiChuyenDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi gửi duyệt khóa học này?\", \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.Moi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã Thu hồi gửi duyệt!") + "');\r\n" +
                    "                   $('#btnGuiDuyet').show();\r\n" +
                    "                   $('#btnThuHoiGuiDuyet').hide();\r\n" +
                    "                   if('" + suaPermission + "' == 'True')\r\n" +
                    "                       $('#btnSua').show();\r\n" +
                    "                   if('" + xoaPermission + "' == 'True')\r\n" +
                    "                       $('#btnXoa').show();\r\n" +
                    "                   if('" + pheDuyetPermission + "' == 'True')\r\n" +
                    "                   {\r\n" +
                    "                       $('#btnDuyet').hide();\r\n" +
                    "                       $('#btnTuChoiDuyet').hide();\r\n" +
                    "                   }\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Thu hồi gửi duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Duyệt khóa học
                    "   function Duyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn duyệt khóa học này?\", \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.Duyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã duyệt!") + "');\r\n" +
                    "                   $('#btnDuyet').hide();\r\n" +
                    "                   $('#btnTuChoiDuyet').hide();\r\n" +
                    "                   $('#btnThuHoiDuyet').show();\r\n" +
                    "                   if('" + guiDuyetPermission + "' == 'True')\r\n" +
                    "                       $('#btnThuHoiGuiDuyet').hide();\r\n" +
                    //Nếu chưa tạo room thì tạo
                    "                   if(roomId == '')\r\n" +
                    "                       MakeRoom();\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Thu hồi duyệt khóa học
                    "   function ThuHoiDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi chuyển duyệt khóa học này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: false, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã Thu hồi duyệt!") + "');\r\n" +
                    "                   $('#btnDuyet').show();\r\n" +
                    "                   $('#btnTuChoiDuyet').show();\r\n" +
                    "                   $('#btnThuHoiDuyet').hide();\r\n" +
                    "                   if('" + guiDuyetPermission + "' == 'True')\r\n" +
                    "                       $('#btnThuHoiGuiDuyet').show();\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Thu hồi duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "               swal.close();\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Từ chối duyệt khóa học
                    "   function TuChoiDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn Từ chối duyệt khóa học này?\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + "Thực hiện" + "\", \r\n" +
                    "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                    "               closeOnConfirm: true, \r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.TuChoi + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã Từ chối duyệt!") + "');\r\n" +
                    "                   $('#btnDuyet').hide();\r\n" +
                    "                   $('#btnTuChoiDuyet').hide();\r\n" +
                    "                   $('#btnThuHoiTuChoiDuyet').show();\r\n" +
                    "                   if('" + guiDuyetPermission + "' == 'True')\r\n" +
                    "                       $('#btnThuHoiGuiDuyet').hide();\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Từ chối duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Thu hồi từ chối duyệt khóa học
                    "   function ThuHoiTuChoiDuyet(){\r\n" +
                    "       swal({ \r\n" +
                    "               title: \"Bạn có chắc chắn thu hồi từ chối duyệt khóa học này?\", \r\n" +
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
                    "               AjaxOut = OneTSQ.WebParts.DT_KhoaHoc.UpdateTrangThai(RenderInfo, khoaHocId, " + (int)DT_KhoaHocCls.eTrangThai.ChoDuyet + ").value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã thu hồi từ chối duyệt!") + "');\r\n" +
                    "                   $('#btnDuyet').show();\r\n" +
                    "                   $('#btnTuChoiDuyet').show();\r\n" +
                    "                   $('#btnThuHoiTuChoiDuyet').hide();\r\n" +
                    "                   if('" + guiDuyetPermission + "' == 'True')\r\n" +
                    "                       $('#btnThuHoiGuiDuyet').show();\r\n" +
                    "                   AddHistory(khoaHocId, '" + WebPartTitle + "', 'Thu hồi từ chối duyệt', 'Tác vụ form');\r\n" +
                    "                   ShowTrangThai();\r\n" +
                    "               }\r\n" +
                    "       }); \r\n" +
                    "   }\r\n" +
                #endregion

                #region Select2
                    "   function Select2()\r\n" +
                    "   {\r\n" +
                    "     CallInitSelect2('cbbTenKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TenKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbDonViHoTro', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị hỗ trợ") + "');\r\n" +
                    "     CallInitSelect2('cbbTieuChuan', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TieuChuanThamGiaKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn tham gia khóa học") + "');\r\n" +
                    "     CallInitSelect2('cbbNhomKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_NhomKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nhóm khóa học") + "');\r\n" +
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
        public static AjaxOut PopupKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(khoaHocId) ? new DT_KhoaHocCls() { LOAIKHOAHOC = (int)DT_KhoaHocCls.eLoaiKhoaHoc.Khac } : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);

                string cbbTenKhoaHoc = "<select class='form-control' id='cbbTenKhoaHoc' required  onchange='FillMaKhoaHoc()' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(khoaHoc.TEN))
                {
                    cbbTenKhoaHoc += string.Format(" <option value='{0}' selected>{1}</option>\r\n", khoaHoc.TEN, khoaHoc.TENKHOAHOC);
                }
                cbbTenKhoaHoc += "</select>\r\n";

                string cbbLoaiKhoaHoc = "<select class='form-control' id='cbbLoaiKhoaHoc' style='font-size: 14px;' onchange='cbbLoaiKhoaHoc_onchange(this)'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiKhoaHocs)
                    cbbLoaiKhoaHoc += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAIKHOAHOC == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiKhoaHoc += "</select>\r\n";

                string cbbLoaiDaoTao = "<select class='form-control' id='cbbLoaiDaoTao' style='font-size: 14px;'>\r\n";
                foreach (var ldt in DT_KhoaHocParser.LoaiDaoTaos)
                    cbbLoaiDaoTao += string.Format("<option value={0} {1}>{2}</option>\r\n", ldt.Key, khoaHoc.LOAIDAOTAO == ldt.Key ? "selected" : null, ldt.Value);
                cbbLoaiDaoTao += "</select>\r\n";

                string cbbLoaiHinhDaoTao = "<select class='form-control' id='cbbLoaiHinhDaoTao' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiHinhDaoTaos)
                    cbbLoaiHinhDaoTao += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAIHINHDAOTAO == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiHinhDaoTao += "</select>\r\n";

                string[] doiTuongs;
                if (string.IsNullOrEmpty(khoaHoc.DOITUONG))
                    doiTuongs = new string[0];
                else doiTuongs = khoaHoc.DOITUONG.Split('|');
                string cbbDoiTuong = "<select class='chosen-select form-control' id='cbbDoiTuong' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.DoiTuongs)
                    cbbDoiTuong += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, doiTuongs.Contains(lkh.Key.ToString()) ? "selected" : null, lkh.Value);
                cbbDoiTuong += "</select>\r\n";

                string cbbLoaiThoiLuong = "<select class='form-control' id='cbbLoaiThoiLuong' onchange='FillNgayKetThucKhoaHoc()' style='font-size: 14px;'>\r\n";
                foreach (var lkh in DT_KhoaHocParser.LoaiThoiLuongs)
                    cbbLoaiThoiLuong += string.Format("<option value={0} {1}>{2}</option>\r\n", lkh.Key, khoaHoc.LOAITHOILUONG == lkh.Key ? "selected" : null, lkh.Value);
                cbbLoaiThoiLuong += "</select>\r\n";

                string cbbDonViHoTro = "<select class='form-control' id='cbbDonViHoTro' style='font-size: 14px;'>\r\n";
                if (!string.IsNullOrEmpty(khoaHoc.DONVIHOTRO_MA))
                {
                    var benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), khoaHoc.DONVIHOTRO_MA);                
                    if (benhVien != null)
                        cbbDonViHoTro += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", benhVien.Ma, benhVien.Ten);
                    else cbbDonViHoTro += string.Format(" <option value='{0}' selected>{0} - {1}</option>\r\n", khoaHoc.DONVIHOTRO_MA, "N/A");
                }
                cbbDonViHoTro += "</select>\r\n";

                string[] tieuChuans;
                if (string.IsNullOrEmpty(khoaHoc.TIEUCHUAN))
                    tieuChuans = new string[0];
                else tieuChuans = khoaHoc.TIEUCHUAN.Split('|');
                string cbbTieuChuan = "<select class='form-control' id='cbbTieuChuan' multiple style='font-size: 14px;'>\r\n";
                foreach (var tieuChuan in tieuChuans)
                {
                    var oTieuChuan = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ORenderInfo, tieuChuan);
                    cbbTieuChuan += string.Format("<option {0} value={1}>{2}</option>\r\n", "selected", tieuChuan, oTieuChuan != null ? oTieuChuan.Ten : tieuChuan);
                }
                cbbTieuChuan += "</select>\r\n";

                string html =
                "<div class='col-md-12' style='height: 100%; margin-top: 15px;'>\r\n" +
                "   <form action='javascript:SaveKhoaHoc();'> \r\n" +
                "       <input type='hidden' id='hdClose'>\r\n" +
                "       <input type='hidden' id='hdKhoaHocId' value='" + khoaHocId + "'>\r\n" +
                "       <div style='position: absolute; top:0px; right:5px; bottom:40px; left:5px; overflow-y:auto;'>\r\n" +
                "            <div class=\"ibox-content\" style=\"border: 0; \"> \r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Tên khóa học:") + " <span style='color:red'>*</span>\r\n" +
                                           cbbTenKhoaHoc +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Khóa học số:") + " <span style='color:red'>*</span>\r\n" +
                "                          <input style='font-size: 14px;' type='number' required class='form-control' id='txtKhoa' onchange='FillMaKhoaHoc()' value='" + khoaHoc.KHOA + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-3 col-xs-3'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Mã khóa học:") + "\r\n" +
                "                          <input type='text' class='form-control' style='font-size: 14px;' disabled = true id='txtMa' value='" + khoaHoc.MA + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Thời lượng học:") +
                "                          <input style='font-size: 14px;' type='number'  class='form-control' id='txtThoiLuong' onchange='FillNgayKetThucKhoaHoc()' value='" + khoaHoc.THOILUONG + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Loại thời lượng:") +
                                           cbbLoaiThoiLuong +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Thời gian dự kiến:") +
                "                          <input style='font-size: 14px; z-index: 0;' type='text' data-mask='99/99/9999' class='form-control datepicker' id='dtNgayKhaiGiangDuKien' onchange='javascript: FillNgayKetThucKhoaHoc(); FillHanNopHoSo();' value='" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                "                           <br><input style='font-size: 14px;' type='text' class='form-control datepicker' id='dtNgayBeGiangDuKien' value='" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Hạn nộp hồ sơ:") +
                "                          <input style='font-size: 14px;z-index: 0;' type='text' class='form-control datepicker' id='dtHanNopHoSo' value='" + (khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : khoaHoc.NGAYKHAIGIANGDUKIEN.Value.ToString("dd/MM/yyyy")) + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Số học viên dự kiến:") +
                "                          <input style='font-size: 14px; z-index: 0;' type='number' class='form-control' id='txtSoLuongHocVienDuKien' value='" + khoaHoc.SOLUONGHOCVIENDUKIEN + "'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Học phí:") +
                "                          <input style='font-size: 14px;' type='text' class='form-control' id='txtHocPhi' onkeypress='CheckCurrency(event);' value='" + (khoaHoc.HOCPHI == null ? null : khoaHoc.HOCPHI.Value.ToString("#,##0.00", new CultureInfo("en-US"))) + "' onkeyup='FormatCurrency(this);'>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Loại đào tạo:") +
                                           cbbLoaiDaoTao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                   <div class='col-md-6 col-xs-6'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Loại hình đào tạo:") +
                                           cbbLoaiHinhDaoTao +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Đối tượng:") +
                                           cbbDoiTuong +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "               <div class='col-md-12 col-xs-12'>\r\n" +
                "                    <div class=\"form-group\">\r\n" +
                                        WebLanguage.GetLanguage(OSiteParam, "Nguồn kinh phí:") +
                                        cbbLoaiKhoaHoc +
                "                    </div>\r\n" +
                "               </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row' id='divDonViHoTro' style='" + (khoaHoc.LOAIKHOAHOC == (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc ? null : "display:none;") + "'>" +
                "                    <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Đơn vị nhận hỗ trợ:") +
                                           cbbDonViHoTro +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                 </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                        <div class=\"form-group\">\r\n" +
                                            WebLanguage.GetLanguage(OSiteParam, "Tiêu chuẩn khóa học:") +
                                            cbbTieuChuan +
                "                        </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "                <div class='row'>" +
                "                   <div class='col-md-12 col-xs-12'>\r\n" +
                "                       <div class=\"form-group\">\r\n" +
                                           WebLanguage.GetLanguage(OSiteParam, "Mô tả:") +
                "                          <textarea id='txtMoTa' rows=4 class='form-control' style='font-size: 14px;'>" + khoaHoc.MOTA + "</textarea>\r\n" +
                "                       </div>\r\n" +
                "                   </div>\r\n" +
                "                </div>\r\n" +
                "              </div>\r\n" +
                "       </div>\r\n" +
                "       <div style='position: absolute; bottom:0px; right:0px;' class='popupButton'>\r\n" +
                "           <input type='submit' class='popupSaveClose btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu & đóng") + "' onclick='javascript:$(\"#hdClose\").val(1);' style='float:right; margin: 7px;'> \r\n" +
                "           <input type='submit' class='popupSave btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' onclick='javascript:$(\"#hdClose\").val(0);' style='float:right; margin: 7px;'> \r\n" +
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
        public static string ShowTrangThai(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                if (khoaHoc != null)
                    return DT_KhoaHocParser.sColorTrangThai[khoaHoc.TRANGTHAI];
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
        public static AjaxOut GetNgayKetThucKhoaHoc(RenderInfoCls ORenderInfo, string ngayKhaiGiang, int thoiLuong, string loaiThoiLuong)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DateTime dtNgayKhaiGiang = DateTime.ParseExact(ngayKhaiGiang, "dd/MM/yyyy", null);
                if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.D.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddDays(thoiLuong).ToString("dd/MM/yyyy");
                else if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.W.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddDays(thoiLuong * 7).ToString("dd/MM/yyyy");
                else if (loaiThoiLuong == DT_KhoaHocCls.eLoaiThoiLuong.M.ToString())
                    RetAjaxOut.RetExtraParam1 = dtNgayKhaiGiang.AddMonths(thoiLuong).ToString("dd/MM/yyyy");
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
        public static AjaxOut GetNgayNopHoSo(RenderInfoCls ORenderInfo, string ngayKhaiGiang)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DateTime dtNgayKhaiGiang = DateTime.ParseExact(ngayKhaiGiang, "dd/MM/yyyy", null);
                DateTime dtNgayNopHoSo = dtNgayKhaiGiang.AddDays(-7);
                RetAjaxOut.RetExtraParam1 = dtNgayNopHoSo.ToString("dd/MM/yyyy");
                if (dtNgayKhaiGiang.DayOfWeek == DayOfWeek.Saturday || dtNgayKhaiGiang.DayOfWeek == DayOfWeek.Sunday)
                    RetAjaxOut.RetBoolean = true;
                if (dtNgayNopHoSo.DayOfWeek == DayOfWeek.Saturday || dtNgayNopHoSo.DayOfWeek == DayOfWeek.Sunday)
                    RetAjaxOut.RetObject = true;
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
        public static AjaxOut SaveKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId, string ma, string ten, int khoa, int? thoiLuong, string loaiThoiLuong, string ngayKhaiGiangDuKien, string ngayBeGiangDuKien, string hanNopHoSo, int? soLuongHocVienDuKien, decimal? hocPhi, int loaiDaoTao, int loaiHinhDaoTao, string doiTuong, int loaiKhoaHoc, string donViHoTro, string[] tieuChuan, string moTa)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc;
                if (CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Reading(ORenderInfo, new DT_KhoaHocFilterCls() { MaKhoaHoc = ma }).Any(o => o.ID != khoaHocId))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã tồn tại khóa học cùng tên và số. Xin nhập tên hoặc số khóa học khác.");
                    return RetAjaxOut;
                }
                if ((string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null)) <= DateTime.Now)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thời gian dự kiến đang nhỏ hơn hoặc bằng ngày hiện tại.");
                    return RetAjaxOut;
                }
                if (string.IsNullOrEmpty(khoaHocId))
                {
                    khoaHoc = new DT_KhoaHocCls();
                    khoaHoc.ID = System.Guid.NewGuid().ToString();
                    khoaHoc.MA = ma;
                    khoaHoc.TEN = ten;
                    khoaHoc.KHOA = khoa;
                    khoaHoc.THOILUONG = thoiLuong;
                    khoaHoc.LOAITHOILUONG = loaiThoiLuong;
                    khoaHoc.NGAYKHAIGIANGDUKIEN = string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.NGAYBEGIANGDUKIEN = string.IsNullOrEmpty(ngayBeGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayBeGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.HANNOPHOSO = string.IsNullOrEmpty(hanNopHoSo) ? null : (DateTime?)DateTime.ParseExact(hanNopHoSo, "dd/MM/yyyy", null);
                    khoaHoc.SOLUONGHOCVIENDUKIEN = soLuongHocVienDuKien;
                    khoaHoc.HOCPHI = hocPhi;
                    khoaHoc.LOAIDAOTAO = loaiDaoTao;
                    khoaHoc.LOAIHINHDAOTAO = loaiHinhDaoTao;
                    khoaHoc.DOITUONG = doiTuong != null ? string.Join("|", doiTuong) : null;
                    khoaHoc.LOAIKHOAHOC = loaiKhoaHoc;
                    khoaHoc.DONVIHOTRO_MA = donViHoTro;
                    khoaHoc.TIEUCHUAN = tieuChuan != null ? string.Join("|", tieuChuan) : null;
                    khoaHoc.MOTA = moTa;
                    khoaHoc.NGAYTAO = DateTime.Now;
                    khoaHoc.NGUOITAO_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    khoaHoc.TRANGTHAI = (int)DT_KhoaHocCls.eTrangThai.Moi;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Add(ORenderInfo, khoaHoc);
                    RetAjaxOut.RetExtraParam1 = khoaHoc.ID;
                }
                else
                {
                    khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
                    khoaHoc.MA = ma;
                    khoaHoc.TEN = ten;
                    khoaHoc.KHOA = khoa;
                    khoaHoc.THOILUONG = thoiLuong;
                    khoaHoc.LOAITHOILUONG = loaiThoiLuong;
                    khoaHoc.NGAYKHAIGIANGDUKIEN = string.IsNullOrEmpty(ngayKhaiGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayKhaiGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.NGAYBEGIANGDUKIEN = string.IsNullOrEmpty(ngayBeGiangDuKien) ? null : (DateTime?)DateTime.ParseExact(ngayBeGiangDuKien, "dd/MM/yyyy", null);
                    khoaHoc.HANNOPHOSO = string.IsNullOrEmpty(hanNopHoSo) ? null : (DateTime?)DateTime.ParseExact(hanNopHoSo, "dd/MM/yyyy", null);
                    khoaHoc.SOLUONGHOCVIENDUKIEN = soLuongHocVienDuKien;
                    khoaHoc.HOCPHI = hocPhi;
                    khoaHoc.LOAIDAOTAO = loaiDaoTao;
                    khoaHoc.LOAIHINHDAOTAO = loaiHinhDaoTao;
                    khoaHoc.DOITUONG = doiTuong != null ? string.Join("|", doiTuong) : null;
                    khoaHoc.LOAIKHOAHOC = loaiKhoaHoc;
                    khoaHoc.DONVIHOTRO_MA = donViHoTro;
                    khoaHoc.TIEUCHUAN = tieuChuan != null ? string.Join("|", tieuChuan) : null;
                    khoaHoc.MOTA = moTa;
                    khoaHoc.NGAYSUA = DateTime.Now;
                    khoaHoc.NGUOISUA_ID = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
                }

                string[] tieuChuans;
                if (string.IsNullOrEmpty(khoaHoc.TIEUCHUAN))
                    tieuChuans = new string[0];
                else tieuChuans = khoaHoc.TIEUCHUAN.Split('|');
                string tenTieuChuans = "";
                foreach (var tc in tieuChuans)
                {
                    var oTieuChuan = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ORenderInfo, tc);
                    tenTieuChuans += (oTieuChuan != null ? oTieuChuan.Ten : tc) + ", ";
                }
                if (tenTieuChuans != "")
                    tenTieuChuans = tenTieuChuans.Substring(0, tenTieuChuans.Length - 2);
                RetAjaxOut.RetExtraParam2 = tenTieuChuans;
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
        public static AjaxOut DeleteKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                //Xóa tài liệu đính kèm khóa học
                var taiLieus = CallBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Reading(ORenderInfo, new DT_TaiLieuFilterCls() { KHOAHOC_ID = khoaHocId });
                foreach (var taiLieu in taiLieus)
                {
                    if (System.IO.File.Exists(taiLieu.DUONGDAN))
                    {                       
                        try
                        {
                            System.IO.File.Delete(taiLieu.DUONGDAN);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Delete(ORenderInfo, khoaHocId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã xóa.");
                string khoaHocsUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_KhoaHocs", new WebParamCls[] { });
                RetAjaxOut.RetUrl = khoaHocsUrl;
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
        public static AjaxOut UpdateTrangThai(RenderInfoCls ORenderInfo, string khoaHocId, int trangThai)
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
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                //Nếu khóa học đã có học viên đăng ký được duyệt thì không thể thu hồi duyệt
                if (khoaHoc.TRANGTHAI == (int)DT_KhoaHocCls.eTrangThai.Duyet && trangThai == (int)DT_KhoaHocCls.eTrangThai.ChoDuyet
                    && CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Reading(ORenderInfo, new DT_KetQuaDaoTaoFilterCls() { KhoaHocDuyet_Id = khoaHoc.ID }).Count() > 0)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học này đã có đăng ký được duyệt nên không thể thu hồi.");
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
        public static AjaxOut UpdateRoomId(RenderInfoCls ORenderInfo, string khoaHocId, string roomId)
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
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "khóa học này đã bị xóa bởi người dùng khác.");
                    return RetAjaxOut;
                }
                khoaHoc.ROOMID = roomId;
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
        #endregion     

    }
}
