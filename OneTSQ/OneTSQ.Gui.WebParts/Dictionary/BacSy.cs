﻿using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.WebParts;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.UploadUtility;

namespace OneTSQ.WebParts
{
    public class BacSys : WebPartTemplate
    {
        public override string WebPartId { get { return StaticWebPartId; } }
        public static string StaticWebPartId = "BacSys";
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách bác sỹ";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách bác sỹ";

            }
        }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(BacSys), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;// WebPermissionUtility.CheckPermission(OSiteParam, DictionaryPermission.StaticPermissionFunctionId, "Access", DictionaryPermission.StaticPermissionFunctionCode, DictionaryPermission.StaticPermissionFunctionId, UserId, false);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;

            try
            {
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(ORenderInfo);
                string SessionId = System.Guid.NewGuid().ToString();
                var user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                string filtemplate = "temp/ImportBacSy.xlsx";

                if (RetAjaxOutCheckPermission.RetBoolean == false)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }
                string cbbDonViFilter =
                "<select id = 'cbbDonViFilter' title = '" + WebLanguage.GetLanguage(OSiteParam, "--Đơn vị--") + "'>\r\n" +
                    "<option value=''>" + WebLanguage.GetLanguage(OSiteParam, "--Đơn vị--") + "</option>\r\n" +
                "</select>\r\n";

                string cbbChuyenKhoaFilter =
                "<select id = 'cbbChuyenKhoaFilter' title = '" + WebLanguage.GetLanguage(OSiteParam, "--Chuyên khoa--") + "'>\r\n" +
                    "<option value=''>" + WebLanguage.GetLanguage(OSiteParam, "--Chuyên khoa--") + "</option>\r\n" +
                "</select>\r\n";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách bác sỹ") + "';\r\n" +
                    //"       $('.selectpicker').selectpicker({ \r\n" +
                    //"       }); \r\n" +
                    "       Select2();\r\n"+
                    "       CallInitSelect2('cbbDonViFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViCongTacService.StaticServiceId) + "', '--Đơn vị--');\r\n" +
                    "       CallInitSelect2('cbbChuyenKhoaFilter', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '--Chuyên khoa--');\r\n" +                 
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +
                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       donVi = document.getElementById('cbbDonViFilter').value;\r\n" +
                    "       chuyenKhoa = document.getElementById('cbbChuyenKhoaFilter').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BacSys.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, donVi, chuyenKhoa).value;\r\n" +
                    "       document.getElementById('divBacSys').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +                 
                #region Refresh danh sách bác sỹ
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                #endregion
                #region Draw div danh sách bác sỹ
                    "   function ShowDivListForm(){\r\n" +
                    "       FilterChange();\r\n" +
                    "       $('#divActionForm').hide();\r\n" +
                    "       $('#divListForm').show();\r\n" +
                    "   }\r\n" +
                #endregion
                #region CallInitSelect2
                  "   function Select2()\r\n" +
                    "   {\r\n" +
                    "       CallInitSelect2('cbbQuocGia', '" + WebEnvironments.GetRemoteProcessDataUrl(QuocGiaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Quốc gia") + "');\r\n" +
                    "       CallInitSelect2('cbbDanToc', '" + WebEnvironments.GetRemoteProcessDataUrl(DanTocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Dân tộc") + "');\r\n" +
                    "       CallInitSelect2('cbbDiaChiHanhChinh', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViHanhChinhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính") + "');\r\n" +
                    "       CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + "');\r\n" +
                    "       CallInitSelect2('cbbDonVi', '" + WebEnvironments.GetRemoteProcessDataUrl(DonViCongTacService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "');\r\n" +
                    "       CallInitSelect2('cbbChuyenMon', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenMonService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn") + "');\r\n" +
                    "       CallInitSelect2('cbbCapBac', '" + WebEnvironments.GetRemoteProcessDataUrl(CapBacService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Cấp bậc") + "');\r\n" +
                    "       CallInitSelect2('cbbChuyenNganh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenNganhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành") + "');\r\n" +
                    "       CallInitSelect2('cbbTrinhDo', '" + WebEnvironments.GetRemoteProcessDataUrl(TrinhDoHocVanService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Trình độ học vấn") + "');\r\n" +
                    "       CallInitSelect2('cbbChucDanh', '" + WebEnvironments.GetRemoteProcessDataUrl(ChucDanhService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Chức danh") + "');\r\n" +
                    "   }\r\n" +
                #endregion
                #region Draw div thêm mới, cập nhật bác sỹ
                    "   function DrawDivActionForm(bacSyId){\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BacSys.DrawDivActionForm(RenderInfo, bacSyId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('.datepicker').datetimepicker({ \r\n" +
                    "           format: 'DD/MM/YYYY' \r\n" +
                    "       }); \r\n" +
                    "       $('#smQtCongTac').summernote({\r\n" +
                    "         toolbar: [\r\n" +
                    "           ['style', ['bold', 'italic', 'underline', 'clear']],\r\n" +
                    "           ['font', ['fontname']],\r\n" +
                    "           ['fontsize', ['fontsize']],\r\n" +
                    "           ['color', ['color']],\r\n" +
                    "           ['para', ['ul', 'ol', 'paragraph']],\r\n" +
                    "           ['height', ['height']]\r\n" +
                    "         ],\r\n" +
                    "         placeholder: 'Toolbar for font style...'\r\n" +
                    "       });\r\n" +
                    "       $('.chosen-select').chosen({width: '100%'});\r\n" +                 
                    "       $('#divListForm').hide();\r\n" +
                    "       $('#divActionForm').show();\r\n" +
                    "       $(\".note-editable\").css(\"border\", \"1px solid black\");\r\n" +
                    "       Select2();\r\n" +
                    "   }\r\n" +
                #endregion
                #region Refresh div thêm mới, cập nhật bác sỹ về trạng thái mới
                    "   function ClearBacSy(){\r\n" +
                    "       document.getElementById('hdBacSyId').value='';\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BacSys.CbbOwnerUser(RenderInfo, null).value;\r\n" +
                    "       if(!AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           $('#divCbbOwnerUser').html(AjaxOut.HtmlContent);\r\n" +
                    "           $('.chosen-select').chosen({width: '100%'});\r\n" +
                    "       }\r\n" +
                    "       $('#imgNdAnh').attr('src', '');\r\n" +
                    "       document.getElementById('txtMa').value='';\r\n" +
                    "       document.getElementById('txtHoTen').value='';\r\n" +
                    "       document.getElementById('txtTenThuongGoi').value='';\r\n" +
                    "       document.getElementById('txtBiDanh').value='';\r\n" +
                    "       document.getElementById('dtNgaySinh').value='';\r\n" +
                    "       document.getElementById('rdoNam').checked = true;\r\n" +
                    "       document.getElementById('rdoNu').checked = false;\r\n" +
                    "       document.getElementById('txtDienThoai').value='';\r\n" +
                    "       document.getElementById('txtEmail').value='';\r\n" +
                    "       document.getElementById('txtDiaChiSoNha').value='';\r\n" +
                    "       document.getElementById('cbbDiaChiHanhChinh').value='';\r\n" +
                    "       $('#select2-cbbDiaChiHanhChinh-container').html('');\r\n" +
                    "       document.getElementById('txtCMND').value='';\r\n" +
                    "       document.getElementById('dtNgayCapCMND').value='';\r\n" +
                    "       document.getElementById('txtNoiCapCMND').value='';\r\n" +
                    "       document.getElementById('cbbChuyenMon').value='';\r\n" +
                    "       $('#select2-cbbChuyenMon-container').html('');\r\n" +
                    "       document.getElementById('cbbCapBac').value = '';\r\n" +
                    "       $('#select2-cbbCapBac-container').html('');\r\n" +
                    "       document.getElementById('cbbChuyenNganh').value = '';\r\n" +
                    "       $('#select2-cbbChuyenNganh-container').html('');\r\n" +
                    "       document.getElementById('cbbTrinhDo').value = '';\r\n" +
                    "       $('#select2-cbbTrinhDo-container').html('');\r\n" +
                    "       document.getElementById('cbbChucDanh').value = '';\r\n" +
                    "       $('#select2-cbbChucDanh-container').html('');\r\n" +
                    "       document.getElementById('cbbChuyenKhoa').value = '';\r\n" +
                    "       $('#select2-cbbChuyenKhoa-container').html('');\r\n" +
                    "       document.getElementById('cbbDonVi').value = '';\r\n" +
                    "       $('#select2-cbbDonVi-container').html('');\r\n" +
                    "       document.getElementById('cbbDanToc').value = '';\r\n" +
                    "       $('#select2-cbbDanToc-container').html('');\r\n" +
                    "       document.getElementById('cbbQuocGia').value = '';\r\n" +
                    "       $('#select2-cbbQuocGia-container').html('');\r\n" +
                    "       $('#smQtCongTac').next().children('.note-editable').html('<p><br></p>');\r\n" +
                    "       $('.caBenhDelete').hide();\r\n" +
                    "   }\r\n" +                  
                #endregion
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
                #region xóa ảnh được upload
                    "   function DeleteHinhAnh() \r\n" +
                    "   {\r\n" +
                    "        $('#imgNdAnh').attr('src', '');\r\n" +
                    "        $('#noImg').addClass('fa fa-user');\r\n" +
                    "   }\r\n" +
                #endregion
                #region Thêm mới/Cập nhật bác sỹ
                    "   function SaveBacSy(){\r\n" +
                    "       bacSyId = document.getElementById('hdBacSyId').value;\r\n" +
                    "       ownerUserIds = $('#cbbOwnerUser').val();\r\n" +
                    "       ndAnh = $('#imgNdAnh').attr('src');\r\n" +
                    "       ma = document.getElementById('txtMa').value;\r\n" +
                    "       hoTen = document.getElementById('txtHoTen').value;\r\n" +
                    "       tenThuongGoi = document.getElementById('txtTenThuongGoi').value;\r\n" +
                    "       biDanh = document.getElementById('txtBiDanh').value;\r\n" +
                    "       ngaySinh = document.getElementById('dtNgaySinh').value;\r\n" +
                    "       gioiTinh = $(\"input[name='GioiTinh']\").filter(':checked').val();\r\n" +
                    "       dienThoai = document.getElementById('txtDienThoai').value;\r\n" +
                    "       email = document.getElementById('txtEmail').value;\r\n" +
                    "       diaChiSoNha = document.getElementById('txtDiaChiSoNha').value;\r\n" +
                    "       diaChiHanhChinh = document.getElementById('cbbDiaChiHanhChinh').value;\r\n" +
                    "       cmnd = document.getElementById('txtCMND').value;\r\n" +
                    "       ngayCapCMND = document.getElementById('dtNgayCapCMND').value;\r\n" +
                    "       noiCapCMND = document.getElementById('txtNoiCapCMND').value;\r\n" +
                    "       chuyenMon = document.getElementById('cbbChuyenMon').value;\r\n" +
                    "       capBac = document.getElementById('cbbCapBac').value;\r\n" +
                    "       chuyenNganh = document.getElementById('cbbChuyenNganh').value;\r\n" +
                    "       trinhDo = document.getElementById('cbbTrinhDo').value;\r\n" +
                    "       chucDanh = document.getElementById('cbbChucDanh').value;\r\n" +
                    "       chuyenKhoa = document.getElementById('cbbChuyenKhoa').value;\r\n" +
                    "       donVi = document.getElementById('cbbDonVi').value;\r\n" +
                    "       danToc = document.getElementById('cbbDanToc').value;\r\n" +
                    "       quocGia = document.getElementById('cbbQuocGia').value;\r\n" +
                    "       quaTrinhCongTac = $('#smQtCongTac').next().children('.note-editable').html();\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BacSys.SaveBacSy(RenderInfo, bacSyId, ownerUserIds, ndAnh, ma, hoTen, tenThuongGoi, biDanh, ngaySinh, gioiTinh, dienThoai, email, diaChiSoNha, diaChiHanhChinh, cmnd, ngayCapCMND, noiCapCMND, chuyenMon, capBac, chuyenNganh, trinhDo, chucDanh, chuyenKhoa, donVi, danToc, quocGia, quaTrinhCongTac).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã lưu.") + "');\r\n" +
                    "       if(AjaxOut.RetExtraParam1!='')\r\n" +
                    "       {\r\n" +
                    "           $('#hdBacSyId').val(AjaxOut.RetExtraParam1);\r\n" +
                    "       }\r\n" +
                    "       $('.caBenhDelete').show();\r\n" +
                    "   }\r\n" +
                #endregion
                #region Xóa bác sỹ
                    "function DeleteBacSy(bacSyId)\r\n" +
                    "{\r\n" +
                   "       swal({ \r\n" +
                   "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                   //"               text: \"" + "Đang thực hiện xóa bệnh án này ra khỏi hệ thống" + "!\", \r\n" +
                   "               type: \"warning\", \r\n" +
                   "               showCancelButton: true, \r\n" +
                   "               confirmButtonColor: \"#DD6B55\", \r\n" +
                   "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                   "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                   "               closeOnConfirm: false \r\n" +
                   "           }, function () { \r\n" +
                    "               RenderInfo=CreateRenderInfo();\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.BacSys.DeleteBacSy(RenderInfo, bacSyId != '' ? bacSyId : document.getElementById('hdBacSyId').value).value;\r\n" +
                    "               if(AjaxOut.Error)\r\n" +
                    "               {\r\n" +
                    "                   callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "                   return;\r\n" +
                    "               }\r\n" +
                    "               else \r\n" +
                    "               {\r\n" +
                    "                   swal.close();\r\n" +
                    "                   toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "');\r\n" +
                    "                   window.location.href = AjaxOut.RetUrl;\r\n" +
                    "               }\r\n" +
                    "               if(bacSyId != '')\r\n" +
                    "                   FilterChange();\r\n" +
                    "               else ShowDivListForm();\r\n" +
                    "       }); \r\n" +

                    "}\r\n" +
                #endregion

                #region import danh sách bác sỹ từ file Excel
                "   function Import()\r\n" +
                "   {\r\n" +
                "        RenderInfo=CreateRenderInfo();\r\n" +
                "        fileUploadValue=document.getElementById('fileUpload').value;\r\n" +
                "        if(fileUploadValue==''){\r\n" +
                "            callSweetAlert('" + WebLanguage.GetLanguage(OSiteParam, "Chưa xác định tài liệu gắn kèm!") + "');\r\n" +
                "            return;\r\n" +
                "        }\r\n" +
                "       swal({ \r\n" +
                "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "\", \r\n" +
                "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có chắc chắn import thông tin này không?") + "\", \r\n" +
                "               type: \"warning\", \r\n" +
                "               showCancelButton: true, \r\n" +
                "               confirmButtonColor: \"#DD6B55\", \r\n" +
                "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "\", \r\n" +
                "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                "               closeOnConfirm: false \r\n" +
                "           }, function () { \r\n" +
                "        $.LoadingOverlay('show');\r\n" +
                "                  var fd = new FormData();\r\n" +
                "               fd.append(\"fileUploadAvatar\", document.getElementById('fileUpload').files[0]);\r\n" +
                "               var xhr = new XMLHttpRequest();\r\n" +
                "               xhr.addEventListener(\"load\",uploaded, false);\r\n" +
                "               xhr.open(\"POST\", \"" + WebConfig.GetImportHandler(OSiteParam, SessionId, user.OwnerUserId, user.LoginName, (int)ProcessImportHandlerUtility.eFileType.BACSY) + "\");\r\n" +
                "               xhr.send(fd);\r\n" +
                "           }\r\n" +
                "       ); \r\n" +
                "   }\r\n" +
                " function uploaded(evt) {\r\n" +
                "       $.LoadingOverlay('hide');\r\n" +
                "       var parser = new DOMParser();\r\n" +
                "       var xmlDoc = parser.parseFromString(evt.currentTarget.responseText,\"text/xml\"); \r\n" +
                "       var mes = xmlDoc.getElementsByTagName(\"InfoMessage\")[0].childNodes[0].nodeValue;\r\n" +
                "       callSweetAlert(mes);\r\n" +
                "       FilterChange();\r\n" +
                " }\r\n" +
                #endregion
                    "</script>\r\n") +
                #endregion
                #region html
                WebEnvironments.ProcessHtml(
                "<div id=\"divListForm\">\r\n" +
                "           <div class=\"ibox float-e-margins\">\r\n" +
                "               <div class=\"ibox-title\"> \r\n" +
                "                 <div class=\"col-md-12\">" +
                "                   <div class=\"row\">" +
                "                       <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách bác sỹ") + "</h5> \r\n" +
                "                   </div> \r\n" +
                "                 </div> \r\n" +
                "               </div> \r\n" +
                "               <div class=\"ibox-content\"> \r\n" +
                "           <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\" style=\"\">\r\n" +
                "               <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"location.href='" + filtemplate + "'\"><i class=\"fa fa-download\"></i> " + WebLanguage.GetLanguage(OSiteParam, "File mẫu") + "</button>\r\n" +
                "               <button type=\"button\"  class=\"btn btn-sm btn-primary\" onclick=\"javascript:Import();\"> " + WebLanguage.GetLanguage(OSiteParam, "Nhập dữ liệu") + "</button>\r\n" +
                "               <span class=\"btn btn-default btn-file\" ><span class=\"fileinput-new\">Chọn File</span>\r\n" +
                "               <span class=\"fileinput-exists\">Chọn lại</span><input type=\"file\" name=\"...\" id=\"fileUpload\" accept=\".xlsx, .xls\"/></span>\r\n" +
                "               <span class=\"fileinput-filename\"></span>\r\n" +
                "               <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                "           </div> \r\n" +
                "       <div id=\"divBacSys\" style='text-align: center;'>" + BacSys.DrawSearchResult(ORenderInfo, 0, null, null, null).HtmlContent + "</div>\r\n" +
                "    </div> \r\n" +
                " </div> \r\n" +
                "</div>\r\n" +
                "</div>\r\n" +
                "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n");
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, string donViMa, string chuyenKhoaMa)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var sRenderInfo = OneTSQ.Model.Common.CreateRenderInfo(ORenderInfo);
                BacSyFilterCls filter = new BacSyFilterCls() { PageIndex = PageIndex, PageSize = 20, Keyword = keyword, DONVIMA = donViMa, CHUYENKHOAMA = chuyenKhoaMa };
                long recordTotal = 0;
                BacSyCls[] BacSys = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int bacSyQuanlity = BacSys.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                int sodong = 0;
                Html += "<div class='row'>\r\n" +
                        "   <div class='col-md-12'>\r\n";
                Html +=
                        "   <div style='width:100%; text-align: center; overflow: hidden' class=\"table-responsive\">\r\n" +
                        "       <table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                        "           <thead> \r\n" +
                        "               <tr> \r\n" +
                        "                   <th width=120 style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                        "                   <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                        "                   <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên") + " </th> \r\n" +
                        "                   <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị") + " </th> \r\n" +
                        "                   <th style='text-align: center;'>" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + " </th> \r\n" +
                        "                   <th style='text-align: center;'>" +
                        "                      <a title='" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới bác sỹ") + "' href='javascript:DrawDivActionForm(\"\");'><i class='fa fa-plus' style='font-size:20px; color: white;'></i></a>\r\n" +
                        "                    </th> \r\n" +
                        "               </tr> \r\n" +
                        "           </thead> \r\n" +
                        "           <tbody> \r\n"
                        ;
                for (int iIndex = 0; iIndex < bacSyQuanlity; iIndex++)
                {
                    sodong++;
                    OneMES3.DM.Model.DonViCongTacCls donVi = null;
                    if (!string.IsNullOrEmpty(BacSys[iIndex].DONVIMA))
                    {
                        var ODonViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(sRenderInfo,BacSys[iIndex].DONVIMA);
                        donVi = ODonViCongTac;
                    }
                    OneMES3.DM.Model.ChuyenKhoaCls chuyenKhoa = null;
                    if (!string.IsNullOrEmpty(BacSys[iIndex].CHUYENKHOAMA))
                    {
                        var OChuyenKhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(sRenderInfo, BacSys[iIndex].CHUYENKHOAMA);
                        chuyenKhoa = OChuyenKhoa;
                    }
                    string title = string.Format("" + WebLanguage.GetLanguage(OSiteParam, "Mã:") + " {0} \n" + WebLanguage.GetLanguage(OSiteParam, "Họ tên:") + " {1} \n" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh:") +
                        " {2} \n" + WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + " {3} \n" +
                        WebLanguage.GetLanguage(OSiteParam, "Điện thoại:") + " {4} \n" +
                        WebLanguage.GetLanguage(OSiteParam, "Email:") + " {5} \n" +
                        WebLanguage.GetLanguage(OSiteParam, "Đơn vị:") + " {6} \n" +
                        WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa:") + " {7}", BacSys[iIndex].MA,
                                                                                    BacSys[iIndex].HOTEN,
                                                                                    (BacSys[iIndex].NGAYSINH != null ? BacSys[iIndex].NGAYSINH.Value.ToString("dd/MM/yyyy") : null),
                                                                                    (BacSys[iIndex].GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nam ? WebLanguage.GetLanguage(OSiteParam, "Nam") : BacSys[iIndex].GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nu ? WebLanguage.GetLanguage(OSiteParam, "Nữ") : null),
                                                                                    BacSys[iIndex].DIENTHOAI,
                                                                                    BacSys[iIndex].EMAIL,
                                                                                    (donVi != null ? donVi.Ten : BacSys[iIndex].DONVIMA),
                                                                                    (chuyenKhoa != null ? chuyenKhoa.Ten : BacSys[iIndex].CHUYENKHOAMA));
                    Html += "<tr title='" + title + "'>\r\n" +
                        "   <td style='text-align: center; vertical-align: middle;'>" + (iIndex + 1) + "</td>\r\n" +
                        "   <td style='text-align: center; vertical-align: middle;'>" + BacSys[iIndex].MA + "</td>\r\n" +
                        "   <td style='text-align: left;'>" + BacSys[iIndex].HOTEN + "</td>\r\n" +
                        "   <td style='text-align: left;'>" + (donVi != null ? donVi.Ten : BacSys[iIndex].DONVIMA) + "</td>\r\n" +
                        "   <td style='text-align: left;'>" + (chuyenKhoa != null ? chuyenKhoa.Ten : BacSys[iIndex].CHUYENKHOAMA) + "</td>\r\n" +
                        "   <td style='text-align: center; vertical-align: middle;'>\r\n" +
                        "        <a id='btnEdit" + iIndex + "' href='javascript:DrawDivActionForm(\"" + BacSys[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "' ><i class='fa fa-pencil-square-o' style='font-size:20px;'></i></a>\r\n" +
                        "        <a id='btnDelete" + iIndex + "' href='javascript:DeleteBacSy(\"" + BacSys[iIndex].ID + "\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' ><i class='fa fa-times' style='font-size:20px;'></i></a>\r\n" +
                        "   </td>\r\n" +
                        "</tr>\r\n"
                        ;
                }
                Html +=
                    "             </tbody>" +
                    "         </table>" +
                    "         <div class='row'>" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "             <div class=\"col-md-2 col-xs-3\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + sodong).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "             </div>\r\n" +
                    "             <div class=\"col-md-10 col-xs-9\" style=\"margin-top:20px;\">\r\n" +
                    RetPaging.PagingText +
                    "             </div>\r\n" +
                    "         </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>" +
                    "</div>";
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
        public static AjaxOut DrawDivActionForm(RenderInfoCls ORenderInfo, string bacSyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                BacSyCls bacSy = string.IsNullOrEmpty(bacSyId) ? new BacSyCls() : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyId);

                string cbbQuocGia =
                "<select id = 'cbbQuocGia' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Quốc gia--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.QUOCTICHMA))
                {
                    var quocGia = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateQuocGiaProcess().CreateModel(sRenderInfo, bacSy.QUOCTICHMA);
                    if (quocGia != null)
                        cbbQuocGia += string.Format("<option value={0}>{0} - {1}</option>\r\n", quocGia.Ma, quocGia.Ten);
                    else
                        cbbQuocGia += string.Format("<option value={0}>{0}</option>\r\n", bacSy.QUOCTICHMA);
                }
                else cbbQuocGia += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Quốc gia--") + "</option>\r\n";
                cbbQuocGia += "</select>\r\n";

                string cbbDanToc =
                "<select id = 'cbbDanToc' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Dân tộc--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.DANTOCMA))
                {
                    var danToc = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDanTocProcess().CreateModel(sRenderInfo, bacSy.DANTOCMA);
                    if (danToc != null)
                        cbbDanToc += string.Format("<option value={0}>{0} - {1}</option>\r\n", danToc.Ma, danToc.Ten);
                    else
                        cbbDanToc += string.Format("<option value={0}>{0}</option>\r\n", bacSy.DANTOCMA);
                }
                else cbbDanToc += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Dân tộc--") + "</option>\r\n";
                cbbDanToc += "</select>\r\n";
                   
                string cbbDiaChiHanhChinh =
                "<select id = 'cbbDiaChiHanhChinh' title = '" + WebLanguage.GetLanguage(OSiteParam, "--Địa chỉ hành chính--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.DIACHIHANHCHINHMA))
                {
                    var donViHanhChinh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViHanhChinhProcess().CreateModel(sRenderInfo, bacSy.DIACHIHANHCHINHMA);
                    if (donViHanhChinh != null)
                        cbbDiaChiHanhChinh += string.Format("<option value={0}>{0} - {1}</option>\r\n", donViHanhChinh.Ma, donViHanhChinh.Ten);
                    else
                        cbbDiaChiHanhChinh += string.Format("<option value={0}>{0}</option>\r\n", bacSy.DIACHIHANHCHINHMA);
                }
                else cbbDiaChiHanhChinh += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Địa chỉ hành chính--") + "</option>\r\n";
                cbbDiaChiHanhChinh += "</select>\r\n";

                string cbbChuyenMon =
                "<select id = 'cbbChuyenMon' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Chuyên môn--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.CHUYENMONMA))
                {
                    var chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(sRenderInfo, bacSy.CHUYENMONMA);
                    if (chuyenMon != null)
                        cbbChuyenMon += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenMon.Ma, chuyenMon.Ten);
                    else
                        cbbChuyenMon += string.Format("<option value={0}>{0}</option>\r\n", bacSy.CHUYENMONMA);
                }
                else cbbChuyenMon += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Chuyên môn--") + "</option>\r\n";
                cbbChuyenMon += "</select>\r\n";

                string cbbCapBac =
                "<select id = 'cbbCapBac' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Cấp bậc--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.CAPBACMA))
                {
                    var capBac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateCapBacProcess().CreateModel(sRenderInfo, bacSy.CAPBACMA);
                    if (capBac != null)
                        cbbCapBac += string.Format("<option value={0}>{0} - {1}</option>\r\n", capBac.Ma, capBac.Ten);
                    else
                        cbbCapBac += string.Format("<option value={0}>{0}</option>\r\n", bacSy.CAPBACMA);
                }
                else cbbCapBac += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Cấp bậc--") + "</option>\r\n";
                cbbCapBac += "</select>\r\n";

                string cbbChuyenNganh =
                "<select id = 'cbbChuyenNganh' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Chuyên ngành--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.CHUYENNGANHMA))
                {
                    var chuyenNganh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenNganhProcess().CreateModel(sRenderInfo, bacSy.CHUYENNGANHMA);
                    if (chuyenNganh != null)
                        cbbChuyenNganh += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenNganh.Ma, chuyenNganh.Ten);
                    else
                        cbbChuyenNganh += string.Format("<option value={0}>{0}</option>\r\n", bacSy.CHUYENNGANHMA);
                }
                else cbbChuyenNganh += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Chuyên ngành--") + "</option>\r\n";
                cbbChuyenNganh += "</select>\r\n";

                string cbbTrinhDo =
                "<select id = 'cbbTrinhDo' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Trình độ--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.TRINHDOMA))
                {
                    var trinhDo = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTrinhDoHocVanProcess().CreateModel(sRenderInfo, bacSy.TRINHDOMA);
                    if (trinhDo != null)
                        cbbTrinhDo += string.Format("<option value={0}>{0} - {1}</option>\r\n", trinhDo.Ma, trinhDo.Ten);
                    else
                        cbbTrinhDo += string.Format("<option value={0}>{0}</option>\r\n", bacSy.TRINHDOMA);
                }
                else cbbTrinhDo += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Trình độ--") + "</option>\r\n";
                cbbTrinhDo += "</select>\r\n";

                string cbbChucDanh =
                "<select id = 'cbbChucDanh' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Chức danh--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.CHUCDANHMA))
                {
                    var chucDanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().CreateModel(sRenderInfo, bacSy.CHUCDANHMA);
                    if (chucDanh != null)
                        cbbChucDanh += string.Format("<option value={0}>{0} - {1}</option>\r\n", chucDanh.Ma, chucDanh.Ten);
                    else
                        cbbChucDanh += string.Format("<option value={0}>{0}</option>\r\n", bacSy.CHUCDANHMA);
                }
                else cbbChucDanh += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Chức danh--") + "</option>\r\n";
                cbbChucDanh += "</select>\r\n";

                string cbbChuyenKhoa =
                "<select id = 'cbbChuyenKhoa' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Chuyên khoa--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.CHUYENKHOAMA))
                {
                    var chuyenKhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(sRenderInfo, bacSy.CHUYENKHOAMA);
                    if (chuyenKhoa != null)
                        cbbChuyenKhoa += string.Format("<option value={0}>{0} - {1}</option>\r\n", chuyenKhoa.Ma, chuyenKhoa.Ten);
                    else
                        cbbChuyenKhoa += string.Format("<option value={0}>{0}</option>\r\n", bacSy.CHUYENKHOAMA);
                }
                else cbbChuyenKhoa += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Chuyên khoa--") + "</option>\r\n";
                cbbChuyenKhoa += "</select>\r\n";

                string cbbDonVi =
                "<select id = 'cbbDonVi' title =  '" + WebLanguage.GetLanguage(OSiteParam, "--Đơn vị--") + "'>\r\n";
                if (!string.IsNullOrEmpty(bacSy.DONVIMA))
                {
                    var donVi = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(sRenderInfo, bacSy.DONVIMA);
                    if (donVi != null)
                        cbbDonVi += string.Format("<option value={0}>{0} - {1}</option>\r\n", donVi.Ma, donVi.Ten);
                    else
                        cbbDonVi += string.Format("<option value={0}>{0}</option>\r\n", bacSy.DONVIMA);
                }
                else cbbDonVi += "<option value=''> " + WebLanguage.GetLanguage(OSiteParam, "--Đơn vị--") + "</option>\r\n";
                cbbDonVi += "</select>\r\n";
                string Html =
                "<form action='javascript:SaveBacSy();'> \r\n" +
                "     <div id=\"divTopButton\" style='float:left; padding:0; margin-top: 5px; margin-left: 10px; margin-bottom: -15px;' class='col-sx-12'> \r\n" +
                "       <input type='button' class='caBenhNew btn btn-primary' value= '" + WebLanguage.GetLanguage(OSiteParam, "Mới") + "' onclick='javascript:ClearBacSy();' style='float:left; margin-top: 7px;'> \r\n" +
                "       <input type='submit' class='caBenhSave btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Lưu") + "' style='float:left; margin-left: 7px; margin-top: 7px;'></li> \r\n" +
                "       <input type='button' class='caBenhDelete btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "' onclick='javascript:DeleteBacSy(\"\");' style='float:left; margin-left: 7px; margin-top: 7px; " + (string.IsNullOrEmpty(bacSyId) ? "display:none;" : null) + "'> \r\n" +
                "       <input type='button' class='caBenhBack btn btn-primary' value='" + WebLanguage.GetLanguage(OSiteParam, "Trở về") + "' onclick='javascript:ShowDivListForm();' style='float:left; margin-left: 7px; margin-top: 7px;'> \r\n" +
                "     </div> \r\n" +
                "   </div>\r\n" +
                "<style>\r\n" +
                "   ul.ul-css li{display:inline-block; padding:0 7px;}\r\n" +
                "</style>\r\n" +
                "            <div class=\"row\">\r\n" +
                "                <div class=\"ibox float-e-margins col-md-12\">\r\n" +
                "                    <div class=\"ibox-title\">\r\n" +
                "                        <h5>" + WebLanguage.GetLanguage(OSiteParam, "HỒ SƠ BÁC SĨ") + "</h5>\r\n" +
                "                        <div class=\"ibox-tools\">\r\n" +
                "                            <a class=\"collapse-link\">\r\n" +
                "                                <i class=\"fa fa-chevron-up\"></i>\r\n" +
                //"                                <i class=\"fa fa-chevron-down\"></i>\r\n" +
                "                            </a>\r\n" +
                "                        </div>\r\n" +
                "                    </div>\r\n" +
                "                    <input id='hdBacSyId' type='hidden' value='" + bacSy.ID + "'>\r\n" +
                "                    <div class=\"ibox-content col-md-12\">\r\n" +
                "                       <div class='row'>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <div id='divAnh' style='text-align:center;'>\r\n" +
                "                                       <img id='imgNdAnh' height='250px' src='" + (!string.IsNullOrEmpty(bacSy.NDANH) ? bacSy.NDANH : null) + "'/>\r\n" +
                "                                       <i id='noImg' class='" + (string.IsNullOrEmpty(bacSy.NDANH) ? "fa fa-user" : null) + "' style='font-size:250px;'> </i>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div style='text-align:center;'>\r\n " +
                "                                       <button class='btn btn-default btn-file' onkeypress=\"if(event.keyCode==13){document.getElementById('fileUpload').click(); return false;}\"> \r\n " +
                "                                           " + WebLanguage.GetLanguage(OSiteParam, "Chọn ảnh") + "<input type = 'file' id='fileUpload' accept='image/*' onchange='GetHinhAnh(this);'> \r\n " +
                "                                       </button> \r\n " +
                "                                       <button type='button' class='btn btn-sm btn-default'  onclick='DeleteHinhAnh()'>" + WebLanguage.GetLanguage(OSiteParam, "Xóa ảnh") + "</button>\r\n" +
                "                                   </div> \r\n " +
                "                               </div>\r\n" +
                "                           </div>\r\n" +
                "                           <div class=\"col-md-8\">\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản đăng nhập hệ thống:") + "</span><br>\r\n" +
                "                                           <div id='divCbbOwnerUser'>" + CbbOwnerUser(ORenderInfo, bacSyId).HtmlContent + "</div>\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Mã:") + " <span style='color:red; z-index:0'>*</span></span><br>\r\n" +
                "                                           <input id='txtMa' type='text' value='" + bacSy.MA + "' class='form-control valueForm' required style = 'z-index:0'>\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Họ tên:") + " <span style='color:red'>*</span></span><br>\r\n" +
                "                                           <input id='txtHoTen' type='text' value='" + bacSy.HOTEN + "' class='form-control valueForm' required>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Tên thường gọi:") + "</span><br>\r\n" +
                "                                           <input id='txtTenThuongGoi' type='text' value='" + bacSy.TENTHUONGGOI + "' class='form-control valueForm' style='z-index:0'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Bí danh:") + "</span><br>\r\n" +
                "                                           <input id='txtBiDanh' type='text' value='" + bacSy.BIDANH + "' class='form-control valueForm'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh:") + "</span><br>\r\n" +
                "                                           <input id='dtNgaySinh' type='text' value='" + (bacSy.NGAYSINH == null ? null : bacSy.NGAYSINH.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker' style='z-index:0'>\r\n" +
                "                                       </div>\r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Giới tính:") + "</span><br>\r\n" +
                "                                           <span class=\"valueForm\" style='float:left;'><input id='rdoNam' name='GioiTinh' type=\"radio\" " + (string.IsNullOrEmpty(bacSyId) || bacSy.GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nam ? "checked" : null) + " value='" + (int)Common.BenhNhan.eGioiTinh.Nam + "' style=\"float:left;\">&nbsp;Nam&nbsp;&nbsp;&nbsp;&nbsp;</span>\r\n" +
                "                                           <span class=\"valueForm\"><input id='rdoNu' name='GioiTinh' type=\"radio\" " + (bacSy.GIOITINH == (int)Common.BenhNhan.eGioiTinh.Nu ? "checked" : null) + " value='" + (int)Common.BenhNhan.eGioiTinh.Nu + "' style=\"float:left;\">&nbsp;Nữ</span>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\" style=''>\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại:") + "</span><br>\r\n" +
                "                                           <input id='txtDienThoai' type='tel' value='" + bacSy.DIENTHOAI + "' onkeypress='CheckCurrency(event);' maxlength='10' class='form-control valueForm' style='z-index:0'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Email:") + "</span><br>\r\n" +
                "                                           <input id='txtEmail' type='email' value='" + bacSy.EMAIL + "' class='form-control valueForm'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                               <div class=\"row\">\r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Thôn, xóm, số nhà:") + "</span><br>\r\n" +
                "                                           <input id='txtDiaChiSoNha' type='text' value='" + bacSy.DIACHISONHA + "' class='form-control valueForm' style='z-index:0'>\r\n" +
                "                                       </div> \r\n" +
                "                                   </div> \r\n" +
                "                                   <div class=\"col-md-6\">\r\n" +
                "                                       <div class=\"form-group\">\r\n" +
                "                                           <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hành chính:") + "</span><br>\r\n" +
                                                            cbbDiaChiHanhChinh +
                "                                       </div> \r\n" +
                "                                   </div>\r\n" +
                "                               </div>\r\n" +
                "                           </div> \r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"row\">\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Số CMND:") + "</span><br>\r\n" +
                "                                   <input id='txtCMND' type='text' value='" + bacSy.CMND + "' onkeypress='CheckCurrency(event);' maxlength='15' class='form-control valueForm'>\r\n" +
                "                               </div>\r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Ngày cấp:") + "</span><br>\r\n" +
                "                                   <input id='dtNgayCapCMND' type='text' style ='z-index: 0;' value='" + (bacSy.NGAYCAPCMND == null ? null : bacSy.NGAYCAPCMND.Value.ToString("dd/MM/yyyy")) + "' class='form-control valueForm datepicker'>\r\n" +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Nơi cấp:") + "</span><br>\r\n" +
                "                                   <input id='txtNoiCapCMND' type='text' value='" + bacSy.NOICAPCMND + "' class='form-control valueForm'>\r\n" +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"row\">\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyên môn:") + "</span><br>\r\n" +
                                                    cbbChuyenMon +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Cấp bậc:") + "</span><br>\r\n" +
                                                    cbbCapBac +
                "                               </div>\r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành: ") + "</span><br>\r\n" +
                                                    cbbChuyenNganh +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"row\">\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Trình độ:") + "</span><br>\r\n" +
                                                    cbbTrinhDo +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Chức danh:") + "</span><br>\r\n" +
                                                    cbbChucDanh +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa:") + "</span><br>\r\n" +
                                                    cbbChuyenKhoa +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"row\">\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Dân tộc: ") + "</span><br>\r\n" +
                                                    cbbDanToc +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Quốc tịch: ") + "</span><br>\r\n" +
                                                    cbbQuocGia +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                           <div class=\"col-md-4\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"leftLabel\">" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác: ") + "</span><br>\r\n" +
                                                    cbbDonVi +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                       </div>\r\n" +
                "                       <div class=\"row\">\r\n" +
                "                           <div class=\"col-md-12\">\r\n" +
                "                               <div class=\"form-group\">\r\n" +
                "                                   <span class=\"labelForm\">" + WebLanguage.GetLanguage(OSiteParam, "Quá trình công tác:") + "</span><br>\r\n" +
                "                                   <div id=\"smQtCongTac\" style='vertical-align: middle;'>" + bacSy.QUATRINHCONGTAC + "</div>\r\n" +
                "                               </div> \r\n" +
                "                            </div>\r\n" +
                "                       </div>\r\n" +
                "                    </div>\r\n" +
                "                </div>\r\n" +
                "            </div>\r\n" +
                "</form>\r\n";
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
        public static AjaxOut CbbOwnerUser(RenderInfoCls ORenderInfo, string bacSyId)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            BacSyCls bacSy = string.IsNullOrEmpty(bacSyId) ? new BacSyCls() : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyId);
            string[] bacSyOwnerUsers = string.IsNullOrEmpty(bacSyId) ? new string[0] : CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bacSyId }).Select(o => o.OWNERUSERID).ToArray();
            OwnerUserCls[] ownerUsers = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Reading(ORenderInfo, new OwnerUserFilterCls());
            string cbbOwnerUser = "<select id = 'cbbOwnerUser' data-placeholder='" + WebLanguage.GetLanguage(OSiteParam, "--chọn--") + "' class='chosen-select' multiple=''>\r\n";
            foreach (var ownerUser in ownerUsers)
                cbbOwnerUser += string.Format("<option value='{0}' {1}>{2}</option>\r\n", ownerUser.OwnerUserId, bacSyOwnerUsers.Contains(ownerUser.OwnerUserId) ? "selected" : null, ownerUser.LoginName);
            cbbOwnerUser += "</select>\r\n";
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(cbbOwnerUser);
            return RetAjaxOut;
        }
        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut SaveBacSy(RenderInfoCls ORenderInfo, string bacSyId, string[] ownerUserIds, string ndAnh, string ma, string hoTen, string tenThuongGoi, string biDanh, string ngaySinh, int? gioiTinh, string dienThoai, string email, string diaChiSoNha, string diaChiHanhChinh, string cmnd, string ngayCapCMND, string noiCapCMND, string chuyenMon, string capBac, string chuyenNganh, string trinhDo, string chucDanh, string chuyenKhoa, string donVi, string danToc, string quocGia, string quaTrinhCongTac)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Reading(ORenderInfo, new BacSyFilterCls()).Any(o => o.ID != bacSyId && o.MA == ma))
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Mã bác sỹ bị trùng, xin nhập mã khác.");
                    return RetAjaxOut;
                }
                BacSyCls bacSy = new BacSyCls();
                if (!string.IsNullOrEmpty(bacSyId))
                    bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyId);
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
                if (string.IsNullOrEmpty(bacSyId))
                {
                    bacSy.ID = System.Guid.NewGuid().ToString();
                    bacSy.NDANH = ndAnh;
                    bacSy.MA = ma;
                    bacSy.HO = ho;
                    bacSy.TEN = ten;
                    bacSy.HOTEN = hoTen;
                    bacSy.TENTHUONGGOI = tenThuongGoi;
                    bacSy.BIDANH = biDanh;
                    bacSy.NGAYSINH = string.IsNullOrWhiteSpace(ngaySinh) ? null : (DateTime?)DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", null);
                    bacSy.GIOITINH = gioiTinh;
                    bacSy.DIENTHOAI = dienThoai;
                    bacSy.EMAIL = email;
                    bacSy.DIACHISONHA = diaChiSoNha;
                    bacSy.DIACHIHANHCHINHMA = diaChiHanhChinh;
                    bacSy.CMND = cmnd;
                    bacSy.NGAYCAPCMND = string.IsNullOrWhiteSpace(ngayCapCMND) ? null : (DateTime?)DateTime.ParseExact(ngayCapCMND, "dd/MM/yyyy", null);
                    bacSy.NOICAPCMND = noiCapCMND;
                    bacSy.CHUYENMONMA = chuyenMon;
                    bacSy.CAPBACMA = capBac;
                    bacSy.CHUYENNGANHMA = chuyenNganh;
                    bacSy.TRINHDOMA = trinhDo;
                    bacSy.CHUCDANHMA = chucDanh;
                    bacSy.CHUYENKHOAMA = chuyenKhoa;
                    bacSy.DONVIMA = donVi;
                    bacSy.DANTOCMA = danToc;
                    bacSy.QUOCTICHMA = quocGia;
                    bacSy.QUATRINHCONGTAC = quaTrinhCongTac;
                    CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Add(ORenderInfo, bacSy);
                    RetAjaxOut.RetExtraParam1 = bacSy.ID;
                    //Gắn thêm tài khoản đăng nhập hệ thống (quan hệ với bảng OwnerUser)
                    if (ownerUserIds != null)
                    {
                        foreach (var ownerUserId in ownerUserIds)
                        {
                            BacSyOwnerUserCls bacSyOwnerUser = new BacSyOwnerUserCls();
                            bacSyOwnerUser.BACSYID = bacSy.ID;
                            bacSyOwnerUser.OWNERUSERID = ownerUserId;
                            CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Add(ORenderInfo, bacSyOwnerUser);
                        }
                    }
                }
                else
                {
                    bacSy.NDANH = ndAnh;
                    bacSy.MA = ma;
                    bacSy.HO = ho;
                    bacSy.TEN = ten;
                    bacSy.HOTEN = hoTen;
                    bacSy.TENTHUONGGOI = tenThuongGoi;
                    bacSy.BIDANH = biDanh;
                    bacSy.NGAYSINH = string.IsNullOrWhiteSpace(ngaySinh) ? null : (DateTime?)DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", null);
                    bacSy.GIOITINH = gioiTinh;
                    bacSy.DIENTHOAI = dienThoai;
                    bacSy.EMAIL = email;
                    bacSy.DIACHISONHA = diaChiSoNha;
                    bacSy.DIACHIHANHCHINHMA = diaChiHanhChinh;
                    bacSy.CMND = cmnd;
                    bacSy.NGAYCAPCMND = string.IsNullOrWhiteSpace(ngayCapCMND) ? null : (DateTime?)DateTime.ParseExact(ngayCapCMND, "dd/MM/yyyy", null);
                    bacSy.NOICAPCMND = noiCapCMND;
                    bacSy.CHUYENMONMA = chuyenMon;
                    bacSy.CAPBACMA = capBac;
                    bacSy.CHUYENNGANHMA = chuyenNganh;
                    bacSy.TRINHDOMA = trinhDo;
                    bacSy.CHUCDANHMA = chucDanh;
                    bacSy.CHUYENKHOAMA = chuyenKhoa;
                    bacSy.DONVIMA = donVi;
                    bacSy.DANTOCMA = danToc;
                    bacSy.QUOCTICHMA = quocGia;
                    bacSy.QUATRINHCONGTAC = quaTrinhCongTac;
                    CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Save(ORenderInfo, bacSy.ID, bacSy);
                    //Cập nhật danh sách tài khoản đăng nhập hệ thống (quan hệ với bảng OwnerUser)
                    BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bacSyId });
                    //Xóa tài khoản đăng nhập hệ thống (quan hệ với bảng OwnerUser)
                    var bacSyOwnerUsersForDelete = ownerUserIds == null ? bacSyOwnerUsers : bacSyOwnerUsers.Where(o => !ownerUserIds.Contains(o.OWNERUSERID));
                    foreach (var bacSyOwnerUserForDelete in bacSyOwnerUsersForDelete)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Delete(ORenderInfo, bacSyOwnerUserForDelete.ID);
                    }
                    //Gắn thêm tài khoản đăng nhập hệ thống (quan hệ với bảng OwnerUser)
                    var ownerUserIdsForAdd = ownerUserIds == null ? new string[0] : ownerUserIds.Where(o => !bacSyOwnerUsers.Any(k => o == k.OWNERUSERID));
                    foreach (var ownerUserIdForAdd in ownerUserIdsForAdd)
                    {
                        BacSyOwnerUserCls bacSyOwnerUser = new BacSyOwnerUserCls();
                        bacSyOwnerUser.BACSYID = bacSy.ID;
                        bacSyOwnerUser.OWNERUSERID = ownerUserIdForAdd;
                        CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Add(ORenderInfo, bacSyOwnerUser);
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
        public static AjaxOut DeleteBacSy(RenderInfoCls ORenderInfo, string bacSyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                //Xóa quan hệ bác sỹ và người dùng trong bảng bacsyowneruser
                BacSyOwnerUserCls[] bacSyOwnerUsers = CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ORenderInfo, new BacSyOwnerUserFilterCls() { BACSYID = bacSyId });
                foreach (BacSyOwnerUserCls bacSyOwnerUser in bacSyOwnerUsers)
                    CallBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Delete(ORenderInfo, bacSyOwnerUser.ID);
                //Xóa bác sỹ
                CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Delete(ORenderInfo, bacSyId);
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
        #region Override Func Core
        public override string divFilter
        {
            get
            {
                return "<div class=\"col-md-3\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã, họ tên bác sỹ\" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbDonViFilter\" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbChuyenKhoaFilter\" class=\"form-control khoaphong_select valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1 divFilter\" style=\"padding-top: 12px; width: 107px; padding-left: 0px;\">\r\n" +
                       "    <div class=\"form-group\">\r\n" +
                       "        <button id=\"btnTimKiem\" class=\"btn btn-sm  mr-10px\" onclick=\"Search()\" style=\"margin-top: 0px; height: 28px;background-color: #e26614;color:white;\"><strong>Tìm kiếm</strong></button>\r\n" +
                       "    </div>\r\n" +
                       "</div>"
                ;
            }
        }
        #endregion
    }
}
