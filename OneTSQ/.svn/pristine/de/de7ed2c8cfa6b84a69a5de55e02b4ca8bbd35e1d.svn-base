using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using System.Data;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Web;
using OneTSQ.Common;
using FlexCel.Report;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_TongHopDangKy : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "DT_TongHopDangKy";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Tổng hợp đăng ký khóa học";
            }
        }
        public override string Description
        {
            get
            {
                return "Tổng hợp đăng ký khóa học";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_TongHopDangKy), Page);
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

            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string khoaHocDangKyId = WebEnvironments.Request("khoaHocDangKyId");
            string khoaHocDuyetId = WebEnvironments.Request("khoaHocDuyetId");
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();
                string cbbKhoaHocDangKy = "";
                if (!string.IsNullOrEmpty(khoaHocDangKyId))
                {
                    DT_KhoaHocCls oKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocDangKyId);
                    if (oKhoaHoc != null)
                    {
                        cbbKhoaHocDangKy += string.Format("<option value={0}>{1}</option>", oKhoaHoc.MA, oKhoaHoc.TENKHOAHOC);
                    }
                }
                string cbbKhoaHocDuyet = "";
                if (!string.IsNullOrEmpty(khoaHocDuyetId))
                {
                    DT_KhoaHocCls oKhoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocDuyetId);
                    if (oKhoaHoc != null)
                    {
                        cbbKhoaHocDuyet += string.Format("<option value={0}>{1}</option>", oKhoaHoc.MA, oKhoaHoc.TENKHOAHOC);
                    }
                }
                string cbbTrangThai = "";
                foreach (var tt in DT_KetQuaDaoTaoParser.TrangThais)
                    cbbTrangThai += "<option value=" + tt.Key + " " + (trangThai == tt.Key ? "selected" : "") + ">" + tt.Value + "</option>";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   RenderInfo=CreateRenderInfo();\r\n" +
                    "   var CheckedArr=[];\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Tổng hợp đăng ký khóa học") + "';\r\n" +
                    "       $('#cbbKhoaHocDangKy').html('" + cbbKhoaHocDangKy + "');\r\n" +
                    "       CallInitSelect2('cbbKhoaHocDangKy', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "','Khóa học đăng ký');\r\n" +
                    "       $('#cbbKhoaHocDuyet').html('" + cbbKhoaHocDuyet + "');\r\n" +
                    "       CallInitSelect2('cbbKhoaHocDuyet', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "','Khóa học duyệt');\r\n" +
                    "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                    "       $('#cbbTrangThai').select2({\r\n" +
                    "          placeholder: 'Trạng thái',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       SetSelect2ForCbbDatTieuChuan();\r\n" +
                    "       SetSelect2ForCbbKhoaHocDuyet();\r\n" +
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       if (CheckedArr.length > 0){\r\n" +
                    "           swal({\r\n" +
                    "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "?\",\r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Bạn có muốn lưu thông tin tổng hợp vừa tạo không?") + "\",\r\n" +
                    "               type: \"warning\",\r\n" +
                    "               showCancelButton: true,\r\n" +
                    "               confirmButtonClass: \"btn-danger\",\r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Có") + "\",\r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Không") + "\",\r\n" +
                    "               closeOnConfirm: true,\r\n" +
                    "               closeOnCancel: true\r\n" +
                    "           },\r\n" +
                    "           function(isConfirm) {\r\n" +
                    "               if (isConfirm) {\r\n" +
                    "                   SaveTongHop();\r\n" +
                    "               }\r\n " +
                    "           });\r\n" +
                    "       }\r\n" +
                    "       CheckedArr=[];\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       khoaHocDangKyId = document.getElementById('cbbKhoaHocDangKy').value;\r\n" +
                    "       khoaHocDuyetId = document.getElementById('cbbKhoaHocDuyet').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_TongHopDangKy.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, khoaHocDangKyId, khoaHocDuyetId, trangThai, CheckedArr).value;\r\n" +
                    "       document.getElementById('divTongHopDangKy').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('.clsDatTieuChuan').select2({\r\n" +
                    "          placeholder: 'Đạt tiêu chuẩn?',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       SetSelect2ForCbbDatTieuChuan();\r\n" +
                    "       SetSelect2ForCbbKhoaHocDuyet();\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       CheckedArr=[];\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                    "   function SetSelect2ForCbbDatTieuChuan(){\r\n" +
                    "       $('.clsDatTieuChuan').each(function(index, element/*this*/){\r\n" +
                    "           $('#' + element.id).select2({\r\n" +
                    "               placeholder: '...',\r\n" +
                    "               allowClear: true,\r\n" +
                    "           });\r\n" +
                    "           if($('#hdDatTieuChuan' + element.id.split('_')[1]).val() == '')\r\n" +
                    "               $('#' + element.id).select2('val', null);\r\n" +
                    "       });\r\n" +
                    "   }\r\n" +
                    "   function SetSelect2ForCbbKhoaHocDuyet(){\r\n" +
                    "       $('.clsKhoaHocDuyet').each(function(index, element/*this*/){\r\n" +
                    "           CallInitSelect2(element.id, '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "...") + "');\r\n" +
                    "       });\r\n" +
                    "   }\r\n" +
                #region Cập nhật lại nội dung marquee khi thay đổi combobox khóa học đăng ký
                    "   function cbbKhoaHocDangKy_onchange(sender){\r\n" +
                    "       maKhoaHoc = sender.value;\r\n" +
                    "       if(sender.value=='')\r\n" +
                    "           $('#divTieuChiKhoaHoc').html('');\r\n" +
                    "       else{\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_TongHopDangKy.GetTieuChiKhoaHoc(RenderInfo, maKhoaHoc).value;\r\n" +
                    "           $('#divTieuChiKhoaHoc').html('<marquee>Tiêu chuẩn: ' + AjaxOut.RetExtraParam1 + '</marquee>');\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Cập nhật lại nội dung marquee khi thay đổi combobox khóa học đăng ký
                #region Save cập nhật đạt tiêu chuẩn của học viên đối với khóa học
                    "   function cbbDatTieuChuan_onchange(sender){\r\n" +
                    "       ketQuaDaoTaoId = sender.id.split('_')[1];\r\n" +
                    "       if(sender.value != $('#hdDatTieuChuan' + ketQuaDaoTaoId).val())\r\n" +
                    "       {\r\n" +
                    "           AjaxOut = OneTSQ.WebParts.DT_TongHopDangKy.SaveDatTieuChuan(RenderInfo, ketQuaDaoTaoId, parseInt(sender.value)).value;\r\n" +
                    "           if(AjaxOut.Error){\r\n" +
                    "	            callGallAlert(AjaxOut.Error);\r\n" +
                    "               $('#' + sender.id).select2('val', $('#hdDatTieuChuan' + ketQuaDaoTaoId).val());\r\n" +
                    "           }\r\n" +
                    "           else{\r\n" +
                    "               $('#hdDatTieuChuan' + ketQuaDaoTaoId).val(sender.value);\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Save cập nhật đạt tiêu chuẩn của học viên đối với khóa học
                #region ckbAllPage_onclick
                    "   function ckbAllPage_onclick(sender){\r\n" +
                    "       if (sender.checked){\r\n" +
                    "           $('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "               if (!element.checked){\r\n" +
                    "                   element.checked=true;\r\n" +
                    "                   CheckedArr.push(element.id);\r\n" +
                    "               }\r\n" +
                    "           });\r\n" +
                    "       }\r\n" +
                    "       else {\r\n" +
                                "$('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "               if (element.checked){\r\n" +
                    "                   element.checked=false;\r\n" +
                    "                   for (var i=0; i < CheckedArr.length; i++) {\r\n" +
                    "                       if(CheckedArr[i] == element.id){\r\n" +
                    "	                        CheckedArr.splice(i,1);\r\n" +
                    "	                        break;\r\n" +
                    "                       }\r\n" +
                    "                   }\r\n" +
                    "               }\r\n" +
                    "           });\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion ckbAllPage_onclick
                #region ckb_onclick
                    "   function ckb_onclick(sender){\r\n" +
                    "       if (sender.checked){\r\n" +
                    "           CheckedArr.push(sender.id);\r\n" +
                    "           CheckCheckAllPage();\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "               for (var index=0; index < CheckedArr.length; index ++) {\r\n" +
                    "                   if(CheckedArr[index] == sender.id){\r\n" +
                    "	                    CheckedArr.splice(index,1);\r\n" +
                    "	                    break;\r\n" +
                    "                   }\r\n" +
                    "               }\r\n" +
                    "           ckbAllPage.checked=false;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion ckb_onclick
                #region Check CheckAllPage: kiểm tra nếu tất cả checkbox trên row được check thì check checkbox all
                    "   function CheckCheckAllPage(){\r\n" +
                    "       checkAll=true;\r\n" +
                    "       $('.clsCkb').each(function(index, element/*this*/) {\r\n" +
                    "           if (!element.checked){\r\n" +
                    "               checkAll=false;\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "       });\r\n" +
                    "       ckbAllPage.checked=checkAll;\r\n" +
                    "   }\r\n" +
                #endregion Check CheckAllPage: kiểm tra nếu tất cả checkbox trên row được check thì check checkbox all

                #region Lưu thông tin tổng hợp
                    "   function SaveTongHop(){\r\n" +
                    "       soLuongDkTongHop = CheckedArr.length;\r\n" +
                    "       if (soLuongDkTongHop == 0){\r\n" +
                    "           callGallAlert('" + WebLanguage.GetLanguage(OSiteParam, "Bạn chưa chọn đăng ký cần tổng hợp") + "');\r\n" +
                    "       }\r\n" +
                    "       else{\r\n" +
                    "           RenderInfo=CreateRenderInfo();\r\n" +
                    "           errorInfo = '';\r\n" +
                    "           for (var index=0; index < soLuongDkTongHop; index ++) {\r\n" +
                    "               AjaxOut = OneTSQ.WebParts.DT_TongHopDangKy.TongHopDangKy(RenderInfo, CheckedArr[index], $('#cbbKhoaHocDuyet' + CheckedArr[index]).val()).value;\r\n" +
                    "               if(AjaxOut.Error){\r\n" +
                    "	                errorInfo += AjaxOut.InfoMessage + '\\n';\r\n" +
                    "               }\r\n" +
                    "           }\r\n" +
                    "           if(errorInfo != '')\r\n" +
                    "	            callGallAlert(errorInfo);\r\n" +
                    "           else\r\n" +
                    "	            toastr.info('" + WebLanguage.GetLanguage(OSiteParam, "Đã tổng hợp!") + "');\r\n" +
                    "           CheckedArr=[];\r\n" +
                    "           Search();\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +
                #endregion Lưu thông tin tổng hợp
                #region Select2
                    "   function Select2()\r\n" +
                    "   {\r\n" +
                    "       CallInitSelect2('cbbKhoaHocDangKy', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "','Khóa học đăng ký');\r\n" +
                    "       CallInitSelect2('cbbKhoaHocDuyet', '" + WebEnvironments.GetRemoteProcessDataUrl(DT_KhoaHocService.StaticServiceId) + "','Khóa học duyệt');\r\n" +
                    "   }\r\n" +               
                #endregion
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div id='divTieuChiKhoaHoc' style='margin-top: 10px; width: 100%; font-size:20px; color: red;'> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đăng ký khóa học") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <div id=\"divTongHopDangKy\">" + DT_TongHopDangKy.DrawSearchResult(ORenderInfo, pageIndex, null, khoaHocDangKyId, khoaHocDangKyId, trangThai, new string[0]).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
                    " </div>\r\n" +
                    "</div>\r\n"
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, string khoaHocDangKyId, string khoaHocDuyetId, int? trangThai, string[] CheckedArr)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);

                DT_KetQuaDaoTaoFilterCls filter = new DT_KetQuaDaoTaoFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    KhoaHocDangKy_Id = khoaHocDangKyId,
                    KhoaHocDuyet_Id = khoaHocDuyetId,
                    TrangThai = trangThai,
                    Keyword = keyword
                };
                long recordTotal = 0;
                DT_KetQuaDaoTaoCls[] dangKies = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int dangKyTotal = dangKies.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\"><input type =\"checkbox\" id='ckbAllPage' onclick='ckbAllPage_onclick(this)'></th> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sinh") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trình độ chuyên môn") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Cơ quan công tác") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Số năm kinh nghiệm") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày đăng ký") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khóa học đăng ký") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đạt tiêu chuẩn") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khóa học duyệt") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < dangKyTotal; iIndex++)
                {
                    var dangKyUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_DangKy",
                        new WebParamCls[] { new WebParamCls("id", dangKies[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("ChiSoMang", filter.PageIndex * filter.PageSize + iIndex),
                            new WebParamCls("khoaHocDangKyId", khoaHocDangKyId),
                            new WebParamCls("khoaHocDuyetId", khoaHocDuyetId),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                    DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, dangKies[iIndex].HOCVIEN_ID);
                    OneMES3.DM.Model.ChuyenMonCls chuyenMon = null;
                    if (hocVien != null && !string.IsNullOrEmpty(hocVien.CHUYENMON_MA))
                    {                       
                        chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.CHUYENMON_MA);                       
                    }
                    OneMES3.DM.Model.DonViCongTacCls donViCongTac = null;
                    if (hocVien != null && !string.IsNullOrEmpty(hocVien.DONVICONGTAC_MA))
                    {
                        donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.DONVICONGTAC_MA);
                    }
                    DT_KhoaHocCls khoaHocDangKy = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, dangKies[iIndex].KHOAHOCDANGKY_ID);
                    DT_KhoaHocCls khoaHocDuyet = string.IsNullOrEmpty(dangKies[iIndex].KHOAHOCDUYET_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, dangKies[iIndex].KHOAHOCDUYET_ID);
                    string cbbKhoaHocDuyet = "<select class='form-control clsKhoaHocDuyet' id='cbbKhoaHocDuyet" + dangKies[iIndex].ID + "'>\r\n";
                    if (khoaHocDuyet != null)
                    {
                        cbbKhoaHocDuyet += string.Format(" <option value='{0}' selected>{1}</option>\r\n", khoaHocDuyet.ID, khoaHocDuyet.TENKHOAHOC);
                    }
                    cbbKhoaHocDuyet += "</select>\r\n";

                    string cbbDatTieuChuan = "<select class='form-control clsDatTieuChuan' onchange='cbbDatTieuChuan_onchange(this)' id='cbbDatTieuChuan_" + dangKies[iIndex].ID + "'>\r\n";
                    foreach (var datTieuChuan in DT_KetQuaDaoTaoParser.DatTieuChuans)
                        cbbDatTieuChuan += string.Format(" <option value='{0}' {1}>{2}</option>\r\n", datTieuChuan.Key, datTieuChuan.Key == dangKies[iIndex].DATTIEUCHUAN ? "selected" : null, datTieuChuan.Value);
                    cbbDatTieuChuan += "</select>\r\n";

                    int ArrIndex = CheckedArr.Count() - 1;
                    while (ArrIndex >= 0 && CheckedArr[ArrIndex] != dangKies[iIndex].ID)
                        ArrIndex--;
                    Html +=
                            "                 <tr> \r\n" +
                            "                     <input type = 'hidden' id = 'hdDatTieuChuan" + dangKies[iIndex].ID + "' value = '" + dangKies[iIndex].DATTIEUCHUAN + "'>\r\n" +
                            "                     <td class=\"td-center\" style='text-align: center; vertical-align: middle;'><input type =\"checkbox\" class=\"clsCkb\" id='" + dangKies[iIndex].ID + "' " + (ArrIndex >= 0 ? "checked" : "") + " onclick='ckb_onclick(this)'></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (hocVien == null ? null : hocVien.MA) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='javascript: $(\"#trVanBang" + dangKies[iIndex].ID + "\").toggle(\"slow\", \"linear\");' title='" + WebLanguage.GetLanguage(OSiteParam, "Đóng/Mở thông tin văn bằng của học viên") + "'>" + (hocVien == null ? null : hocVien.HOTEN) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (hocVien == null || hocVien.NGAYSINH == null ? null : hocVien.NGAYSINH.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (hocVien == null || hocVien.GIOITINH == null ? null : BenhNhan.GioiTinhs[hocVien.GIOITINH.Value]) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (hocVien == null ? null : hocVien.DIENTHOAI) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (chuyenMon == null ? null : chuyenMon.Ten) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (donViCongTac == null ? null : donViCongTac.Ten) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (hocVien == null ? null : hocVien.SONAMKINHNGHIEM) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (dangKies[iIndex].NGAYDANGKY == null ? null : dangKies[iIndex].NGAYDANGKY.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + dangKyUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở đăng ký khóa học") + "'>" + (khoaHocDangKy == null ? null : khoaHocDangKy.TENKHOAHOC) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'>" + cbbDatTieuChuan + "</td> \r\n" +
                            "                     <td style='vertical-align: middle;'>" + cbbKhoaHocDuyet + "</td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + DT_KetQuaDaoTaoParser.sColorTrangThai[dangKies[iIndex].TRANGTHAI] + "</td> \r\n" +
                            "                 </tr> \r\n" +
                            "                 <tr id = 'trVanBang" + dangKies[iIndex].ID + "' style='display:none; background-color: #F3F4D5'> \r\n" +
                            "                     <td colspan='15'>\r\n" +
                                                    "<center style='font-weight: bold; color: blue;'>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin văn bằng của học viên") + "</center>" +
                                                    DrawVanBangs(ORenderInfo, dangKies[iIndex].HOCVIEN_ID).HtmlContent +
                            "                     </td> \r\n" +
                            "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + dangKyTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "              </div>\r\n" +
                    "             <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                    RetPaging.PagingText +
                    "             </div>\r\n" +
                    "         </div>\r\n" +
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
        public static AjaxOut DrawVanBangs(RenderInfoCls ORenderInfo, string hocVienId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, hocVienId);
                if (hocVien == null)
                {
                    RetAjaxOut.Error = true;
                    RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Học viên không tồn tại");
                    return RetAjaxOut;
                }
                DT_VanBangCls[] vanBangs = CallBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Reading(ORenderInfo, new DT_VanBangFilterCls() { HOCVIEN_ID = hocVienId });
                OneMES3.DM.Model.TrinhDoHocVanCls trinhDo = null;
                if (!string.IsNullOrEmpty(hocVien.TOTNGHIEP_MA))
                {
                    trinhDo = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateTrinhDoHocVanProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.TOTNGHIEP_MA);
                }

                OneMES3.DM.Model.ChuyenNganhCls chuyenNganh = null;
                if (!string.IsNullOrEmpty(hocVien.CHUYENNGANH_MA))
                {
                    chuyenNganh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenNganhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), hocVien.CHUYENNGANH_MA);
                }
                string html =
                         "<p style='text-decoration: underline;'>" + WebLanguage.GetLanguage(OSiteParam, "1. Thông tin về văn bằng") + "</p>\r\n" +
                        "<div class='row'>" +
                            "<div class='col-md-3 col-xs-3'>\r\n" +
                                "<div class=\"form-group\">\r\n" +
                                    WebLanguage.GetLanguage(OSiteParam, "Tốt nghiệp:") + " \r\n" +
                                    "<span id='spMa' class=\"valueForm\">" + (trinhDo == null ? hocVien.TOTNGHIEP_MA : trinhDo.Ten) + "</span>\r\n" +
                                "</div>\r\n" +
                            "</div>\r\n" +
                            "<div class='col-md-3 col-xs-3'>\r\n" +
                                "<div class=\"form-group\">\r\n" +
                                    WebLanguage.GetLanguage(OSiteParam, "Năm tốt nghiệm:") + " \r\n" +
                                    "<span id='spKhoa' class=\"valueForm\">" + hocVien.NAMTOTNGHIEP + "</span>\r\n" +
                                "</div>\r\n" +
                            "</div>\r\n" +
                            "<div class='col-md-3 col-xs-3'>\r\n" +
                                "<div class=\"form-group\">\r\n" +
                                    WebLanguage.GetLanguage(OSiteParam, "Trường cấp bằng:") + " \r\n" +
                                    "<span id='spTen' class=\"valueForm\">" + hocVien.TRUONGCAPBANG + "</span>\r\n" +
                                "</div>\r\n" +
                            "</div>\r\n" +
                            "<div class='col-md-3 col-xs-3'>\r\n" +
                                "<div class=\"form-group\">\r\n" +
                                    WebLanguage.GetLanguage(OSiteParam, "Chuyên ngành:") + " \r\n" +
                                    "<span id='spTen' class=\"valueForm\">" + (chuyenNganh == null ? hocVien.CHUYENMON_MA : chuyenNganh.Ten) + "</span>\r\n" +
                                "</div>\r\n" +
                            "</div>\r\n" +
                        "</div>\r\n" +
                         "<p style='text-decoration: underline;'>" + WebLanguage.GetLanguage(OSiteParam, "2. Các văn bằng, chứng chỉ khác liên quan đến khóa học") + "</p>\r\n" +
                         "<table class=\"table table-striped table-bordered table-hover\"> \r\n" +
                             "<thead> \r\n" +
                                 "<tr> \r\n" +
                                    "<th width=30 style='text-align: center; background-color:#F3F4D5; color: black;'>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                                    "<th style='text-align: center; background-color:#F3F4D5; color: black;'>" + WebLanguage.GetLanguage(OSiteParam, "Tên văn bằng/chứng chỉ") + " </th> \r\n" +
                                    "<th style='text-align: center; background-color:#F3F4D5; color: black;'>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị cấp văn bằng/chứng chỉ") + " </th> \r\n" +
                                    "<th width=100 style='text-align: center; background-color:#F3F4D5; color: black;'>" + WebLanguage.GetLanguage(OSiteParam, "Năm") + " </th> \r\n" +
                                 "</tr> \r\n" +
                             "</thead> \r\n" +
                             "<tbody> \r\n";
                for (int iIndex = 0; iIndex < vanBangs.Count(); iIndex++)
                {
                    html += "<tr> \r\n" +
                                "<td style='text-align: center; vertical-align: middle; background-color:#F3F4D5;'>" + (iIndex + 1) + "</td> \r\n" +
                                "<td style='background-color:#F3F4D5;'>" + vanBangs[iIndex].TEN + "</td> \r\n" +
                                "<td style='background-color:#F3F4D5;'>" + vanBangs[iIndex].DONVICAP + "</td> \r\n" +
                                "<td style='text-align: center; background-color:#F3F4D5;'>" + vanBangs[iIndex].NAM + "</td> \r\n" +
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

        #endregion
        #region Xử lý nghiệp vụ
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut GetTieuChiKhoaHoc(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
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
                RetAjaxOut.RetExtraParam1 = tenTieuChuans;
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
        public static AjaxOut SaveDatTieuChuan(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId, int? datTieuChuan)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                ketQuaDaoTao.DATTIEUCHUAN = datTieuChuan;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Save(ORenderInfo, ketQuaDaoTao.ID, ketQuaDaoTao);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã lưu!");
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
        public static AjaxOut TongHopDangKy(RenderInfoCls ORenderInfo, string ketQuaDaoTaoId, string khoaHocDuyetId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                DT_KetQuaDaoTaoCls ketQuaDaoTao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ORenderInfo, ketQuaDaoTaoId);
                if (!string.IsNullOrEmpty(khoaHocDuyetId))
                {
                    DT_HocVienCls hocVien = CallBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ORenderInfo, ketQuaDaoTao.HOCVIEN_ID);
                    DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocDuyetId);
                    if (khoaHoc.NGAYKHAIGIANGDUKIEN != null && khoaHoc.NGAYBEGIANGDUKIEN != null && CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().IsTrungThoiGianHoc(ORenderInfo, hocVien.ID, khoaHoc.NGAYKHAIGIANGDUKIEN.Value, khoaHoc.NGAYBEGIANGDUKIEN.Value, ketQuaDaoTaoId) == true)
                    {
                        RetAjaxOut.Error = true;
                        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Học viên") + " " + hocVien.HOTEN + " " + WebLanguage.GetLanguage(OSiteParam, "đã đăng ký khóa học khác trùng với thời gian của khóa học được duyệt.");
                        return RetAjaxOut;
                    }
                }
                ketQuaDaoTao.KHOAHOCDUYET_ID = string.IsNullOrEmpty(khoaHocDuyetId) ? ketQuaDaoTao.KHOAHOCDANGKY_ID : khoaHocDuyetId;
                ketQuaDaoTao.TRANGTHAI = (int)DT_KetQuaDaoTaoCls.eTrangThai.DaTongHop;
                CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Save(ORenderInfo, ketQuaDaoTao.ID, ketQuaDaoTao);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đã tổng hợp!");
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
                return "<div class=\"col-md-3\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã học viên, họ tên học viên\" onkeypress=\"FilterChange(event);\" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbKhoaHocDangKy\" class=\"form-control valueForm\" onchange='cbbKhoaHocDangKy_onchange(this)'>\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbKhoaHocDuyet\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <button id = 'btnTimKiem' class='btn btn-sm  mr-10px' onclick='FilterChange()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Tìm kiếm</strong></button>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <button id = 'btnLuu' class='btn btn-sm  mr-10px' onclick='SaveTongHop()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Tổng hợp</strong></button>\r\n" +
                       "</div>\r\n"

                   ;
            }
        }
        #endregion
    }
}
