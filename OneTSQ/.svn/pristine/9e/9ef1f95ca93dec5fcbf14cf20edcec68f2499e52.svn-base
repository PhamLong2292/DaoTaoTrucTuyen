
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.WebParts;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCel.Report;
using OneTSQ.Permission;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Utilities;
using OneTSQ.Core.Model;
using OneTSQ.UploadUtility;

namespace OneTSQ.WebParts
{
    public class ChuyenKhoaDaoTaoTt : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "ChuyenKhoaDaoTaoTt";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Chuyên khoa đào tạo trực tuyến";
            }
        }
        public override string Description
        {
            get
            {
                return "Chuyên khoa đào tạo trực tuyến";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ChuyenKhoaDaoTaoTt), Page);
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
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                bool XemPermission = false;
                XemPermission = user.IsSystemAdmin == 1 || PermissionUtility.CheckPermission(ORenderInfo, new ChuyenKhoaDaoTaoTtPermission().PermissionFunctionCode,
                    DM_ChuyenKhoaDaoTaoTtCls.ePermission.Xem.ToString(), new ChuyenKhoaDaoTaoTtPermission().PermissionFunctionCode,
                    ChuyenKhoaDaoTaoTtPermission.StaticPermissionFunctionId, userId, null, false);
                if (!XemPermission)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }

                string SessionId = System.Guid.NewGuid().ToString();
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string filtemplate = WebConfig.GetWebHttpRoot() + "/temp/ImportDM_CHUYENKHOADAOTAOTT.xlsx";
                RetAjaxOut.HtmlContent =
                      WebEnvironments.ProcessHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +

                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa đào tạo trực tuyến") + "</h5> \r\n" +
                        "     </div> \r\n" +

                        "     <div class=\"ibox-content\"> \r\n" +
                        "       <button type=\"button\" style=\"margin-bottom:5px;padding:0px; width:80px;height:34px\" class=\"btn btn-sm btn-primary pull-right\" onclick=\"javascript:ServerSideDrawAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "       <button type=\"button\" style=\"margin-bottom:5px;padding:0px; width:80px;height:34px\" class=\"btn btn-sm btn-primary\" onclick=\"location.href='" + filtemplate + "'\"><i class=\"fa fa-download\"></i> " + WebLanguage.GetLanguage(OSiteParam, "File mẫu") + "</button>\r\n" +
                        "       <button type=\"button\" style=\"margin-bottom:5px;padding:0px; width:80px;height:34px\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallExPortForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Xuất dữ liệu") + "</button>\r\n" +
                        "       <button type=\"button\" style=\"margin-bottom:5px;padding:0px; width:80px;height:34px\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:Import();\"> " + WebLanguage.GetLanguage(OSiteParam, "Nhập dữ liệu") + "</button>\r\n" +
                        "       <div class=\"fileinput fileinput-new\" data-provides=\"fileinput\">\r\n" +
                        "            <span class=\"btn btn-default btn-file\" ><span class=\" fileinput-new\">Chọn File</span>\r\n" +
                        "            <span class=\"  fileinput-exists\">Chọn lại</span><input type=\"file\" name=\"...\" id=\"fileUpload\"/></span>\r\n" +
                        "            <span class=\"fileinput-filename\"></span>\r\n" +
                        "            <a href=\"#\" class=\"close fileinput-exists\" data-dismiss=\"fileinput\" style=\"float: none\" id= \"aexit\" >×</a>\r\n" +
                        "       </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "         <div id=\"divChuyenKhoaDaoTaoTtContent\">" + ServerSideDrawSearchResult(ORenderInfo, "", null, 0).HtmlContent + "</div>\r\n" +
                        "     </div> \r\n" +

                        " </div> \r\n" +
                        "</div>\r\n" +

                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n" +

                        "<div id=\"divFormModal\" class=\"modal immodal\" style=\"overflow: hidden\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"largeModal\" aria-hidden=\"true\" data-backdrop=\"static\">\r\n" +
                        "    <div class=\"modal-dialog\">\r\n" +
                        "       <div class=\"modal-content\">\r\n" +
                        "           <div class=\"panel-heading\">\r\n" +
                        "               <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>\r\n" +
                        "               <h2 class=\"modal-title\" id=\"ModalTitle\"></h2>\r\n" +
                        "           </div> \r\n" +
                        "           <div class=\"modal-body\" id=\"divModalContent\"></div> \r\n" +
                        "        </div> \r\n" +
                        "    </div> \r\n" +
                        "</div> \r\n"
                        ) +

                      WebEnvironments.ProcessJavascript(
                        "<script language=\"javascript\">\r\n" +
                        "   var _currentPageIndex=0;\r\n" +

                        "    $(document).ready(function() \r\n" +
                        "    {\r\n" +
                        "       $(\".hieuluc_select\").select2({\r\n" +
                        "           placeholder: \"" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "\",\r\n" +
                        "           allowClear: true\r\n" +
                        "       });\r\n" +
                        "    });\r\n" +

                        "   function CallExPortForm()\r\n" +
                        "   {\r\n" +
                        "       RenderInfo=CreateRenderInfo();\r\n" +
                        "       AjaxOut  = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSidePrint(CreateRenderInfo()).value;\r\n" +
                        "       if(AjaxOut.Error)\r\n" +
                        "       {\r\n" +
                        "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       window.open(AjaxOut.RetUrl, 'Download');\r\n" +
                        "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Xuất thành công") + "!\");\r\n" +
                        "   }\r\n" +

                        "   function checkCurrency(e){\r\n" +
                        "        if(event.which != 8 && isNaN(String.fromCharCode(e.which))){\r\n" +
                        "           event.preventDefault(); \r\n" +
                        "        }\r\n" +
                        "   }\r\n" +

                        "   function NextPage(PageIndex)\r\n" +
                        "   {\r\n" +
                        "       _currentPageIndex = PageIndex;\r\n" +
                        "       RealCallReading();\r\n" +
                        "   }\r\n" +

                        "   function FilterSearch()\r\n" +
                        "   {\r\n" +
                        "       NextPage(0);\r\n" +
                        "   }\r\n" +

                        "   function RealCallReading()\r\n" +
                        "   {\r\n" +
                        "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                        "       HieuLuc = document.getElementById('cbFilterHieuLuc').value;\r\n" +
                        "       OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideDrawSearchResult(CreateRenderInfo(), Keyword, HieuLuc, _currentPageIndex, CallBackReading);\r\n" +
                        "   }\r\n" +

                        "   function CallBackReading(res)\r\n" +
                        "   {\r\n" +
                        "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                        "       document.getElementById('divChuyenKhoaDaoTaoTtContent').innerHTML = res.value.HtmlContent;\r\n" +
                        "       $('.confirm').focus(); " +
                        "   }\r\n" +

                        "   function CloseModal()\r\n" +
                        "   {\r\n" +
                        "       $('#divFormModal').modal('hide');\r\n" +
                        "   }\r\n" +

                        "   function ServerSideDrawAddForm()\r\n" +
                        "   {\r\n" +
                        "       AjaxOut = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideDrawAddForm(CreateRenderInfo()).value;\r\n" +
                        "       if(AjaxOut.Error) { \r\n" +
                        "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       document.getElementById('ModalTitle').innerHTML = '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, " Thêm mới Chuyên khoa đào tạo trực tuyến") + " </span>';\r\n" +
                        "       document.getElementById('divModalContent').innerHTML = AjaxOut.HtmlContent; \r\n" +
                        "       $('#divFormModal').modal('show');\r\n" +
                        "       document.getElementById('txtMa').focus();\r\n" +
                        "   }\r\n" +

                        "   function ServerSideDrawUpdateForm(Id)\r\n" +
                        "   {\r\n" +
                        "       AjaxOut = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideDrawUpdateForm(CreateRenderInfo(), Id).value;\r\n" +
                        "       if(AjaxOut.Error) { \r\n" +
                        "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       document.getElementById('ModalTitle').innerHTML = '<span class=\"fa fa-pencil-square-o\">" + WebLanguage.GetLanguage(OSiteParam, " Sửa thông tin Chuyên khoa đào tạo trực tuyến") + " </span>';\r\n" +
                        "       document.getElementById('divModalContent').innerHTML = AjaxOut.HtmlContent; \r\n" +
                        "       $('#divFormModal').modal('show');\r\n" +
                        "       document.getElementById('txtMa').focus();\r\n" +
                        "   }\r\n" +

                        "   function ServerSideAdd()\r\n" +
                        "   {\r\n" +
                        "       Ma = document.getElementById('txtMa').value.trim();\r\n" +
                        "       Ten = document.getElementById('txtTen').value.trim();\r\n" +
                        "       TenNgan = document.getElementById('txtTenNgan').value.trim();\r\n" +
                        "       MoTa = document.getElementById('txtMoTa').value.trim() || null;\r\n" +
                        "       HieuLuc = $('#rdHieuLuc input:radio:checked').val();\r\n" +
                        "       Stt = document.getElementById('nbStt').value.trim() || null; \r\n" +
                        "       if(Stt != null) { \r\n" +
                        "           if(Stt == \"0\")\r\n" +
                        "            Stt = 0; \r\n" +
                        "           else\r\n" +
                        "            Stt = parseInt(Stt); \r\n" +
                        "       } \r\n" +
                        "       if(Ma == '' || Ten == '' || Stt == null || Stt < 0 || Stt > 999999999)\r\n" +
                        "           return;\r\n" +

                        "       AjaxOut = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideAdd(CreateRenderInfo(), Ma, Ten, TenNgan, Stt, MoTa, HieuLuc).value;\r\n" +
                        "       if(AjaxOut.Error) {\r\n" +
                        "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới Chuyên khoa đào tạo trực tuyến thành công!") + "\");\r\n" +
                        "       NextPage(_currentPageIndex);\r\n" +
                        "       CloseModal();\r\n" +
                        "   }\r\n" +

                        "   function ServerSideUpdate(Id)\r\n" +
                        "   {\r\n" +
                        "       Ma = document.getElementById('txtMa').value.trim();\r\n" +
                        "       Ten = document.getElementById('txtTen').value.trim();\r\n" +
                        "       TenNgan = document.getElementById('txtTenNgan').value.trim();\r\n" +
                        "       MoTa = document.getElementById('txtMoTa').value.trim() || null;\r\n" +
                        "       HieuLuc = $('#rdHieuLuc input:radio:checked').val();\r\n" +

                        "       Stt = document.getElementById('nbStt').value.trim() || null; \r\n" +
                        "       if(Stt != null) { \r\n" +
                        "           if(Stt == \"0\")\r\n" +
                        "            Stt = 0; \r\n" +
                        "           else\r\n" +
                        "            Stt = parseInt(Stt); \r\n" +
                        "       } \r\n" +

                        "       if(Ma == '' || Ten == '' || Stt == null || Stt < 0 || Stt > 999999999)\r\n" +
                        "           return;\r\n" +

                        "       AjaxOut = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideUpdate(CreateRenderInfo(), Id, Ma, Ten, TenNgan, Stt, MoTa, HieuLuc).value;\r\n" +
                        "       if(AjaxOut.Error) {\r\n" +
                        "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa thông tin Chuyên khoa đào tạo trực tuyến thành công!") + "\");\r\n" +
                        "       NextPage(_currentPageIndex);\r\n" +
                        "       CloseModal();\r\n" +
                        "   }\r\n" +

                        "   function ServerSideDelete(id)\r\n" +
                        "   {\r\n" +
                        "       RenderInfo=CreateRenderInfo();\r\n" +
                        "       swal({ \r\n" +
                        "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                        "               text: \"" + "" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa bản ghi này ra khỏi hệ thống") + "" + "!\", \r\n" +
                        "               type: \"warning\", \r\n" +
                        "               showCancelButton: true, \r\n" +
                        "               confirmButtonColor: \"#DD6B55\", \r\n" +
                        "               confirmButtonText: \"" + "Thực hiện xóa" + "\", \r\n" +
                        "               cancelButtonText: \"" + "Hủy bỏ" + "\", \r\n" +
                        "               closeOnConfirm: false \r\n" +
                        "           }, function () { \r\n" +
                        "           AjaxOut = OneTSQ.WebParts.ChuyenKhoaDaoTaoTt.ServerSideDelete(RenderInfo, id).value;\r\n" +
                        "           if(AjaxOut.Error) { \r\n" +
                        "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                        "               return;\r\n" +
                        "           }\r\n" +
                        "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa thông tin Chuyên khoa đào tạo trực tuyến thành công") + "!\");\r\n" +
                        "       NextPage(_currentPageIndex);\r\n" +
                        "       }); \r\n" +
                        "   }\r\n" +

                #region Import
                        "   function Import()\r\n" +
                        "   {\r\n" +
                        "        RenderInfo=CreateRenderInfo();\r\n" +
                        "        fileUploadValue=document.getElementById('fileUpload').value;\r\n" +
                        "        if(fileUploadValue==''){\r\n" +
                        "            callSweetAlert('" + WebLanguage.GetLanguage(OSiteParam, "Chưa xác định tài liệu gắn kèm!") + "');\r\n" +
                        "            return;\r\n" +
                        "        }\r\n" +
                        "       document.getElementById('divProcessing').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang Import Danh mục") + "';\r\n" +
                        "       swal({ \r\n" +
                        "               title: \"" + WebLanguage.GetLanguage(OSiteParam, "Thông báo") + "\", \r\n" +
                        "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện Import danh mục") + "\", \r\n" +
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
                        "               xhr.open(\"POST\", \"" + WebConfig.GetImportHandler(OSiteParam, SessionId, userId, LoginName, (int)ProcessImportHandlerUtility.eFileType.DM_CHUYENKHOADAOTAOTT) + "\");\r\n" +
                        "               xhr.send(fd);\r\n" +
                        "           }\r\n" +
                        "       ); \r\n" +
                        "   }\r\n" +
                        " function uploaded(evt) {\r\n" +
                        "        $.LoadingOverlay('hide');\r\n" +
                        "       var parser = new DOMParser();\r\n" +
                        "       var xmlDoc = parser.parseFromString(evt.currentTarget.responseText,\"text/xml\"); \r\n" +
                        "       var mes = xmlDoc.getElementsByTagName(\"InfoMessage\")[0].childNodes[0].nodeValue;\r\n" +
                        "       callSweetAlert(mes);\r\n" +
                        "         Reload();\r\n" +
                        " }\r\n" +

                        "   function Reload()\r\n" +
                        "   {\r\n" +
                        "        var a = document.getElementById('aexit');\r\n" +
                        "        a.click();\r\n" +
                        "           NextPage(_currentPageIndex);\r\n" +
                        "   }\r\n" +
                #endregion
            "</script>\r\n");

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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, string Keyword, string HieuLuc, int currentPageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                DM_ChuyenKhoaDaoTaoTtFilterCls OPositionFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls()
                {
                    Keyword = Keyword,
                    HieuLuc = !string.IsNullOrEmpty(HieuLuc) && HieuLuc != null ? int.Parse(HieuLuc) : (int)eSearch.SearchAll,
                    PageIndex = currentPageIndex,
                    PageSize = 20
                };

                DM_ChuyenKhoaDaoTaoTtCls[] Positions = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().ReadingWithPaging(ORenderInfo, OPositionFilter);
                var countPosition = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Count(ORenderInfo, OPositionFilter);
                ReturnPaging RetPaging = WebPaging.GetPaging((int)countPosition, currentPageIndex, OPositionFilter.PageSize, 10, "NextPage");

                var nextpage = string.Empty;
                if (countPosition > 18) nextpage = RetPaging.PagingText;

                var Html = new StringBuilder();
                if (Positions.Length == 0)
                {
                    Html.Append("   <div class=\"search-result-info\"></div>\r\n" +
                     "         <div class=\"table-responsive\"> \r\n" +
                     "             <table id=\"sample\" class=\"table table-striped table-bordered table-hover\"> \r\n" +
                     "                 <thead> \r\n" +
                     "                 <tr> \r\n" +
                     //"                     <th class=\"th-func-20\"></th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mã") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Tên") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Tên ngắn") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Hiệu lực") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày tạo") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày hiệu lực") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày hết hiệu lực") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "STT") + "</th> \r\n" +
                     "                     <th class=\"th-func-20\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
                     //"                     <th class=\"th-func-20\"></th> \r\n" +
                     "                 </tr> \r\n" +
                     "                 </thead> \r\n" +
                     "             </table> \r\n" +
                     "       </div>\r\n");
                }
                else
                {
                    Html.Append(
                        "   <div class=\"search-result-info\"></div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table id=\"sample\" class=\"table table-striped table-bordered table-hover dataTables-autosort\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        //"                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên ngắn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Hiệu lực") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày tạo") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày hiệu lực") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày hết hiệu lực") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "STT") + " </th> \r\n" +
                        "                     <th class = \"unsort\" style=\"width:65px\">" + WebLanguage.GetLanguage(OSiteParam, "Tác vụ") + "</th> \r\n" +
                        //"                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n");
                    for (int iIndex = 0; iIndex < Positions.Length; iIndex++)
                    {
                        Html.Append(
                            "                 <tr> \r\n" +
                            //"                     <td class=\"td-right\">" + (currentPageIndex * 18 + iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].Ma + "\">" + Positions[iIndex].Ma + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].Ten + "\">" + Positions[iIndex].Ten + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].TenNgan + "\">" + Positions[iIndex].TenNgan + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].MoTa + "\" style=\"max-width: 100px; word-wrap: break-word;\">" + Positions[iIndex].MoTa + "</td> \r\n" +
                            "                     <td >" + Model.Common.HieuLucs[Positions[iIndex].HieuLuc] + " </ td > \r\n" +
                            "                     <td title=\"" + Positions[iIndex].NgayTao + "\">" + Positions[iIndex].NgayTao.ToString("dd/MM/yyyy") + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].TuNgay + "\">" + (Positions[iIndex].TuNgay != null ? Positions[iIndex].TuNgay.Value.ToString("dd/MM/yyyy, HH:mm") : string.Empty) + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].DenNgay + "\">" + (Positions[iIndex].DenNgay != null ? Positions[iIndex].DenNgay.Value.ToString("dd/MM/yyyy, HH:mm") : string.Empty) + "</td> \r\n" +
                            "                     <td title=\"" + Positions[iIndex].Stt + "\">" + Positions[iIndex].Stt.ToString("N0") + "</td> \r\n" +
                            "                     <td class=\"td-center\" style=\"text-align:center;\"><a title=\"" + "" + WebLanguage.GetLanguage(OSiteParam, "Sửa Chuyên khoa đào tạo trực tuyến") + "" + "\" href=\"javascript:ServerSideDrawUpdateForm('" + Positions[iIndex].Id + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a> &nbsp; <a title=\"" + "Xóa Chuyên khoa đào tạo trực tuyến" + "\" href=\"javascript:ServerSideDelete('" + Positions[iIndex].Id + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            //"                     <td class=\"td-center\"><a title=\"" + "Xóa Chuyên khoa đào tạo trực tuyến" + "\" href=\"javascript:ServerSideDelete('" + Positions[iIndex].Id + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n");
                    }
                    Html.Append(
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n" +
                        "                   <div class=\"\">\r\n" +
                        "                        <div class=\"col-md-2\" style=\"margin-top:20px;padding-left: 0px;\">\r\n" +
                        "                       <lable>" + WebLanguage.GetLanguage(OSiteParam, "Số bản ghi") + ": " + RetPaging.EndIndex + "" + "/ " + "" + countPosition + "</lable>\r\n" +
                        "                        </div>\r\n" +
                        "                       <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                        "                       " + nextpage + "" +
                        "                       </div>\r\n" +
                        "                   </div>\r\n");
                }
                RetAjaxOut.HtmlContent = Html.ToString();
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        #region showpopup
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo)
        {

            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string Html =
                        "<form id=\"sendmail\" data-async  method=\"post\" onSubmit=\"return false;\" role=\"form\" class=\"contactForm\">\r\n" +

                        "      <div style=\"max-height: calc(100vh - 210px); overflow-y:scroll; white-space: nowrap;\"> \r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label for=\"inputTo\">" + WebLanguage.GetLanguage(OSiteParam, "Mã Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                   <input id=\"txtMa\" type=\"text\" placeholder=\"Mã\" maxlength=\"36\" class=\"form-control\" required>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Tên Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                   <input id=\"txtTen\" type=\"text\" placeholder=\"Tên\" maxlength=\"255\" class=\"form-control\" required>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Tên ngắn Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                   <input id=\"txtTenNgan\" type=\"text\" placeholder=\"Tên ngắn\" maxlength=\"255\" class=\"form-control\" required>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Số thứ tự") + " <span style='color:red'>*</span></label>\r\n" +
                        "                   <input id=\"nbStt\" onkeypress=\"checkCurrency(event);\" type=\"text\" min=\"0\" max=\"999999999\"  class=\"form-control\" required>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\" id=\"rdHieuLuc\">\r\n" +
                        "               <div class=\"form-group\"> " +
                        "                   <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label>\r\n" +
                        "                   <div> \r\n" +
                        "                       <label class=\"radio-inline\"><input  id = \"optradioHl0\" type = \"radio\" name=\"optradioHl\" value=\"" + (int)eHieuLuc.Co + "\" checked>" + Model.Common.HieuLucs[(int)eHieuLuc.Co] + "</label> " +
                        "                       <label class=\"radio-inline\"><input  id = \"optradioHl1\" type = \"radio\" name=\"optradioHl\" value=\"" + (int)eHieuLuc.Khong + "\">" + Model.Common.HieuLucs[(int)eHieuLuc.Khong] + "</label> " +
                        "                   </div> " +
                        "               </div> " +
                        "           </div>\r\n" +

                        "           <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "               <div class=\"form-group\">\r\n" +
                        "                   <label for=\"inputBody\">" + WebLanguage.GetLanguage(OSiteParam, "Mô tả:") + "</label>\r\n" +
                        "                   <textarea class=\"form-control\" id=\"txtMoTa\" maxlength=\"2000\" rows=\"4\"></textarea>\r\n" +
                        "               </div>\r\n" +
                        "           </div>\r\n" +

                        "      </div>\r\n" +

                        "      <div class=\"form-group\" >\r\n" +

                        "           <div class=\"form-group\" style=\"margin-top: 10px; margin-left: 15px;\">\r\n" +
                        "               <button class=\"btn btn-sm btn-primary mr-5px\"  type=\"submit\" onclick=\"javascript:ServerSideAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                        "               <button class=\"btn btn-sm btn-primary\" type=\"button\" onclick=\"javascript:CloseModal();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                        "               <div id='response'></div>\r\n" +
                        "           </div>\r\n" +

                        "      </div>\r\n" +

                        "</form>";

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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                var OPosition = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, Id);
                if (OPosition == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa đào tạo trực tuyến đã bị xóa hoặc không tìm thấy"));

                string Html =
                     "<form id=\"sendmail\" data-async  method=\"post\" onSubmit=\"return false;\" role=\"form\" class=\"contactForm\">\r\n" +

                        "        <div style=\"max-height: calc(100vh - 210px); overflow-y:scroll; white-space: nowrap;\">" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                        "                       <label for=\"inputTo\">" + WebLanguage.GetLanguage(OSiteParam, "Mã Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                       <input id=\"txtMa\" type=\"text\" placeholder=\"Mã\" maxlength=\"36\" class=\"form-control\" value=\"" + OPosition.Ma + "\" required>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                        "                       <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Tên Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                       <input id=\"txtTen\" type=\"text\" placeholder=\"Tên\" maxlength=\"255\" class=\"form-control\" value=\"" + OPosition.Ten + "\" required>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                        "                       <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Tên ngắn Chuyên khoa đào tạo trực tuyến") + " <span style='color:red'>*</span></label>\r\n" +
                        "                       <input id=\"txtTenNgan\" type=\"text\" placeholder=\"Tên ngắn\" maxlength=\"255\" class=\"form-control\" value=\"" + OPosition.Ten + "\" required>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                        "                       <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Số thứ tự") + " <span style='color:red'>*</span></label>\r\n" +
                        "                       <input id=\"nbStt\" onkeypress=\"checkCurrency(event);\" type=\"text\" placeholder=\"Số thứ tự\" class=\"form-control\" value=\"" + OPosition.Stt + "\" required>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\" id=\"rdHieuLuc\">\r\n" +
                        "                   <div class=\"form-group\"> " +
                        "                       <label for=\"inputSubject\">" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label>\r\n" +
                        "                       <div> \r\n" +
                        "                           <label class=\"radio-inline\"><input  id = \"optradioHl0\" type = \"radio\" name=\"optradioHl\" value=\"" + (int)eHieuLuc.Co + "\"" + (OPosition.HieuLuc == (int)eHieuLuc.Co ? "checked" : string.Empty) + ">" + Model.Common.HieuLucs[(int)eHieuLuc.Co] + "</label> " +
                        "                           <label class=\"radio-inline\"><input  id = \"optradioHl1\" type = \"radio\" name=\"optradioHl\" value=\"" + (int)eHieuLuc.Khong + "\"" + (OPosition.HieuLuc == (int)eHieuLuc.Khong ? "checked" : string.Empty) + ">" + Model.Common.HieuLucs[(int)eHieuLuc.Khong] + "</label> " +
                        "                       </div> " +
                        "                   </div> " +
                        "               </div>\r\n" +

                        "               <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n" +
                        "                   <div class=\"form-group\">\r\n" +
                        "                       <label for=\"inputBody\">" + WebLanguage.GetLanguage(OSiteParam, "Mô tả:") + "</label>\r\n" +
                        "                       <textarea class=\"form-control\" id=\"txtMoTa\" maxlength=\"2000\" rows=\"4\">" + OPosition.MoTa + "</textarea>\r\n" +
                        "                   </div>\r\n" +
                        "               </div>\r\n" +

                         "      </div>\r\n" +

                        "      <div class=\"form-group\" >\r\n" +

                        "               <div class=\"form-group\" style=\"margin-top: 10px; margin-left: 15px;\">\r\n" +
                        "                   <button class=\"btn btn-sm btn-primary mr-5px\" type=\"submit\" onclick=\"javascript:ServerSideUpdate('" + Id + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                        "                   <button class=\"btn btn-sm btn-primary\" data-dismiss=\"modal\" type=\"button\" onclick=\"javascript:CloseModal();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button>\r\n" +
                        "                   <div id='response'></div>\r\n" +
                        "               </div>\r\n" +
                        "      </div>\r\n" +
                        "</form>";

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
        #endregion

        #region action
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideAdd(RenderInfoCls ORenderInfo, string Ma, string Ten, string TenNgan, int Stt, string MoTa, string HieuLuc)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                DM_ChuyenKhoaDaoTaoTtCls OPosition = new DM_ChuyenKhoaDaoTaoTtCls();
                OPosition.Id = Guid.NewGuid().ToString();
                OPosition.Ma = Ma;
                OPosition.Ten = Ten;
                OPosition.TenNgan = TenNgan;
                OPosition.MoTa = MoTa;
                OPosition.HieuLuc = int.Parse(HieuLuc);
                OPosition.Stt = Stt;
                OPosition.NgayTao = DateTime.Now;
                if (OPosition.HieuLuc == (int)eHieuLuc.Co) OPosition.TuNgay = DateTime.Now;
                CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Add(ORenderInfo, OPosition);
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
        public static AjaxOut ServerSideUpdate(RenderInfoCls ORenderInfo, string Id, string Ma, string Ten, string TenNgan, int Stt, string MoTa, string HieuLuc)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                var OPositionOld = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, Id);
                if (OPositionOld == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa đào tạo trực tuyến đã bị xóa hoặc không tìm thấy"));

                if (!OPositionOld.Ma.ToUpper().Equals(Ma.ToUpper()))
                    if (CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, Ma) != null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã đã tồn tại không thể sửa được bản ghi!"));

                DM_ChuyenKhoaDaoTaoTtCls OPositionNew = new DM_ChuyenKhoaDaoTaoTtCls();
                OPositionNew.Ma = Ma;
                OPositionNew.Ten = Ten;
                OPositionNew.TenNgan = TenNgan;
                OPositionNew.MoTa = MoTa;
                OPositionNew.HieuLuc = int.Parse(HieuLuc);
                OPositionNew.Stt = Stt;
                OPositionNew.NgayTao = OPositionOld.NgayTao;
                OPositionNew.TuNgay = OPositionOld.TuNgay;
                OPositionNew.DenNgay = OPositionOld.DenNgay;
                OPositionNew.GhiChu = OPositionOld.GhiChu;
                OPositionNew.ChaId = OPositionOld.ChaId;
                CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Save(ORenderInfo, Id, OPositionNew);
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
        public static AjaxOut ServerSideDelete(RenderInfoCls ORenderInfo, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Delete(ORenderInfo, Id);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        public static string ConditionGhiChu(DM_ChuyenKhoaDaoTaoTtCls OPosition, string Ma, string Ten, int Stt, int HieuLuc, ref int ConditionAdd)
        {
            var currentTime = DateTime.Now.ToString("HH:mm dd/MM/yyyy: ");
            var result = new StringBuilder();
            result.Append(currentTime);

            // Xét xem người dùng có thay đổi mã không nếu có ghi new vào biến thông tin
            if (!OPosition.Ma.ToUpper().Replace(" ", "").Equals(Ma.ToUpper().Replace(" ", "")))
            {
                result.Append(string.Format("sửa mã ({0}=>{1})  ", OPosition.Ma, Ma));
            }

            // Xét xem người dùng có thay đổi tên không nếu có ghi new vào biến thông tin
            if (!OPosition.Ten.ToUpper().Replace(" ", "").Equals(Ten.ToUpper().Replace(" ", "")))
            {
                if (!result.ToString().Equals(currentTime))
                    result.Append("; ");
                result.Append(string.Format("sửa tên ({0}=>{1}) ", OPosition.Ten, Ten));
            }
            if (OPosition.HieuLuc != HieuLuc)
            {
                // Nếu người dùng thay đổi mã sang không so hiệu lực thì gán cho biến điều kiện = 0
                if (HieuLuc == (int)eHieuLuc.Khong)
                    ConditionAdd = (int)eHieuLuc.Khong;
            }

            // Xét xem nếu người dùng không thay đổi thông tin thì gán biến result về bằng chuỗi trống
            if (result.ToString().Equals(currentTime))
                result = new StringBuilder();

            return result.ToString();
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSidePrint(RenderInfoCls ORenderInfo)
        {

            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                FlexCelReport flexCelReport = new FlexCelReport();

                List<ChuyenKhoaDaoTaoTtView> Datas = new List<ChuyenKhoaDaoTaoTtView>();

                SiteParam
                      OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTt = new DM_ChuyenKhoaDaoTaoTtFilterCls() { HieuLuc = (int)eSearch.SearchAll };
                DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Reading(ORenderInfo, OChuyenKhoaDaoTaoTt);

                for (int i = 0; i < ChuyenKhoaDaoTaoTts.Length; i++)
                {

                    var view = new ChuyenKhoaDaoTaoTtView();
                    view.Ma = ChuyenKhoaDaoTaoTts[i].Ma;
                    view.Ten = ChuyenKhoaDaoTaoTts[i].Ten;
                    view.TenNgan = ChuyenKhoaDaoTaoTts[i].TenNgan;
                    view.Stt = ChuyenKhoaDaoTaoTts[i].Stt;
                    view.MoTa = ChuyenKhoaDaoTaoTts[i].MoTa;
                    view.HieuLuc = ChuyenKhoaDaoTaoTts[i].HieuLuc.ToString();
                    view.NgayTao = ChuyenKhoaDaoTaoTts[i].NgayTao.ToString("dd/MM/yyyy");
                    view.TuNgay = ChuyenKhoaDaoTaoTts[i].TuNgay.Value.ToString("dd/MM/yyyy");
                    if (!string.IsNullOrEmpty(ChuyenKhoaDaoTaoTts[i].DenNgay.ToString()))
                    {
                        view.DenNgay = ChuyenKhoaDaoTaoTts[i].DenNgay.Value.ToString("dd/MM/yyyy");
                    }
                    view.GhiChu = ChuyenKhoaDaoTaoTts[i].GhiChu;

                    Datas.Add(view);
                }
                flexCelReport.AddTable("ChuyenKhoaDaoTaoTt", Datas);

                //flexCelReport.SetValue("NgayLap", DateTime.Now);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\CHUYENKHOADAOTAOTT.xlsx";

                string Id = "DM_ChuyenKhoaDaoTaoTt_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, Directoryfile + "\\" + Id);
                //FlexCelReport ExcelReport = null;
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                //workbook.SaveToFile(SaveFile, ExcelVersion.Version2007);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().ExportEx(ORenderInfo, LoginName, XmlFile, "DM_ChuyenKhoaDaoTaoTt", flexCelReport, SaveFile);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        public sealed class ChuyenKhoaDaoTaoTtView
        {
            public string Ma { get; set; }
            public string Ten { get; set; }
            public string TenNgan { get; set; }
            public int Stt { get; set; }
            public string MoTa { get; set; }
            public string HieuLuc { get; set; }
            public string NgayTao { get; set; }
            public string TuNgay { get; set; }
            public string DenNgay { get; set; }
            public string GhiChu { get; set; }
        }
        #endregion
        #region divFilter
        public override string divFilter
        {
            get
            {
                return "<div class=\"col-md-3\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm\" onkeypress=\"timkiem(event);\" class=\"form-control\" >\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbFilterHieuLuc\"  class=\"form-control hieuluc_select\">\r\n" +
                       "        <option></option>\r\n" +
                       "        <option value=\"" + (int)eHieuLuc.Co + "\">" + Model.Common.HieuLucs[(int)eHieuLuc.Co] + "</option>\r\n" +
                       "        <option value=\"" + (int)eHieuLuc.Khong + "\">" + Model.Common.HieuLucs[(int)eHieuLuc.Khong] + "</option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-3\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "       <button type=\"button\" style=\"margin-top: 0px; height: 28px;background-color: #e26614;color:white;\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:FilterSearch();\" onkeypress=\"timkiem(event);\"> Tìm kiếm </button>\r\n" +
                       "</div>\r\n";
            }
        }
        #endregion
    }
}
