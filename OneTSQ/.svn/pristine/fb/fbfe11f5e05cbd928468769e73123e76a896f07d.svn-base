using OnlineTour.Bussiness.Utility;
using OnlineTour.Model;
using OnlineTour.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTour.WebParts
{
    public class City : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "City";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục thành phố";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục thành phố";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(City),Page);
        }

        public override AjaxOut CheckPermission(SiteParam OSiteParam)
        {
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;// WebPermissionUtility.CheckPermission(OSiteParam, DictionaryPermission.StaticPermissionFunctionId, "Access", DictionaryPermission.StaticPermissionFunctionCode, DictionaryPermission.StaticPermissionFunctionId, UserId, false);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(OSiteParam);
                if (RetAjaxOutCheckPermission.RetBoolean == false)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }
                string CountryId = (string)WebEnvironments.Request("CountryId");
                if (string.IsNullOrEmpty(CountryId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số không hợp lệ"));
                }

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Country().WebPartId);

                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.City.ServerSideDrawAddForm(RenderInfo).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtCityName').focus();\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(CityId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.City.ServerSideDrawUpdateForm(RenderInfo, CityId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtCityName').focus();\r\n" +
                    "   }\r\n" +

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       CountryId = document.getElementById('txtCountryId').value;\r\n" +
                    "       CityName = document.getElementById('txtCityName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.City.ServerSideAdd(RenderInfo, CountryId, CityName, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n"+
                    "       CallReading();\r\n"+
                    "       document.getElementById('txtCityName').value='';\r\n"+
                    "       document.getElementById('txtCityName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n"+
                    "   }\r\n" +


                    "   function CallActionUpdate(CityId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       CityName = document.getElementById('txtCityName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.City.ServerSideUpdate(RenderInfo, CityId, CityName, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n"+
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(CityId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa thành phố này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OnlineTour.WebParts.City.ServerSideDelete(RenderInfo, CityId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"thành phố đã được xóa thành công!")+".\", \"success\"); \r\n"+
                    "       }); \r\n" +
                    
                    "   }\r\n" +


                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallReading()',10);\r\n"+
                    "   }\r\n" +

                    "   function RealCallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       CountryId = document.getElementById('txtCountryId').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OnlineTour.WebParts.City.ServerSideDrawSearchResult(RenderInfo, CountryId, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divCityContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<input id=\"txtCountryId\" type=\"hidden\" value=\"" + CountryId + "\">\r\n" +
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục thành phố") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "                 <div>\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:window.open('"+BackUrl+"','_self');\"> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divCityContent\">"+  ServerSideDrawSearchResult(ORenderInfo, CountryId, "").HtmlContent +"</div>\r\n"+
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +

                        "</div>\r\n" +
                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n"
                        );

                
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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, string CountryId, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                CityFilterCls
                    OCityFilter = new CityFilterCls();
                OCityFilter.Keyword = Keyword;
                OCityFilter.CountryId = CountryId;

                CityCls[] 
                    Coutries = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(OActionSqlParam, OCityFilter);
                string Html = "";
                if (Coutries.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+Coutries.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên thành phố") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < Coutries.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].CityName + "</td> \r\n" +
                            "                     <td>" + (Coutries[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa thành phố") + "\" href=\"javascript:CallUpdateForm('" + Coutries[iIndex].CityId + "');\"><i class=\""+WebScreen.GetEditGridIcon()+"\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa thành phố") + "\" href=\"javascript:CallActionDelete('" + Coutries[iIndex].CityId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n"+
                        "       </div>\r\n";
                }
                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên thành phố") + "</label> <input id=\"txtCityName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên thành phố") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> "+SelectActiveText+"</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                       "             </div> \r\n" +
                       "         </div> \r\n" +
                       "     </div> \r\n" +
                       " </div> \r\n";

                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string CityId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                CityCls
                    OCity = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().CreateModel(OActionSqlParam, CityId);
                if (OCity == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "thành phố đã bị xóa hoặc không tìm thấy"));
                }
                string SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";

                if (OCity.Active == 1)
                {
                    SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";
                }
                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên thành phố") + "</label> <input id=\"txtCityName\" value=\"" + OCity.CityName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên thành phố") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"" + OCity.SortIndex + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + CityId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                       "             </div> \r\n" +
                       "         </div> \r\n" +
                       "     </div> \r\n" +
                       " </div> \r\n";

                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideAdd(
            RenderInfoCls ORenderInfo, 
            string CountryId,
            string CityName,
            string SortIndex,
            int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(CityName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên thành phố không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

        
                CityCls 
                    OCity = new CityCls();
                OCity.CityId = System.Guid.NewGuid().ToString();
                OCity.CityName = CityName;
                OCity.frkCountryId = CountryId;
                OCity.SortIndex = int.Parse(SortIndex);
                OCity.Active = Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Add(OActionSqlParam, OCity);
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
        public static AjaxOut ServerSideUpdate(
            RenderInfoCls ORenderInfo,
            string CityId, 
            string CityName,
            string SortIndex,
            int    Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                     OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                if (string.IsNullOrEmpty(CityName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên thành phố không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                CityCls
                    OCity = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().CreateModel(OActionSqlParam, CityId);
                OCity.CityName = CityName;
                OCity.SortIndex = int.Parse(SortIndex);
                OCity.Active = Active;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Save(OActionSqlParam, CityId, OCity);
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
        public static AjaxOut ServerSideDelete(
            RenderInfoCls ORenderInfo,
            string CityId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Delete(OActionSqlParam, CityId);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        public override bool RunAlone
        {
            get
            {
                return false;
            }
        }
    }
}
