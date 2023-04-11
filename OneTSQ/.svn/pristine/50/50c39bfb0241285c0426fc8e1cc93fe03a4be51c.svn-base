using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class ViewService : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "ViewService";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xem dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Xem dịch vụ";
            }
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new Service().WebPartId };
            }
        }
        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ViewService),Page);
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
            try
            {
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(ORenderInfo);
                if (RetAjaxOutCheckPermission.RetBoolean == false)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }

                string OwnerCode=(string)WebEnvironments.Request("OwnerCode");
                string ServiceId = (string)WebEnvironments.Request("ServiceId");
                ServiceCls
                 OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);
                if (OService == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "dịch vụ đã bị xóa hoặc không tìm thấy"));
                }

                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new Service().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",OService.frkServiceTypeId),
                    new WebParamCls("AssetSupplierId",OService.frkAssetSupplierId)
                });
                RetAjaxOut.HtmlContent =
                    "<script>\r\n" +
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "        window.open('" + BackUrl + "','_self');\r\n" +
                    "   }\r\n" +

                    "   function CallActionUpdate(ServiceId)\r\n"+
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ViewService.ServerSideUpdate(RenderInfo, ServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n"+
                    "</script>\r\n" +
                    ServerSideDrawViewForm(ORenderInfo, OService).HtmlContent +
                    "<script>\r\n" +
                    "</script>\r\n";

                
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
        public static AjaxOut ServerSideDrawViewForm(RenderInfoCls ORenderInfo, ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string Scope = "private";
                if (OService.SharedScope == 1)
                {
                    Scope = "public";
                }
                if (OService.SharedScope == 2)
                {
                    Scope = "Group";
                }
                string Html =
                      " <div class=\"ibox-content\"> \r\n" +
                      "     <div class=\"row\"> \r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OService.ServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Sửa dịch vụ") + "</strong></button> \r\n" +
                      
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd('" + OService.frkServiceTypeId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                      "                 </div> \r\n" +
                      "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\"><span style=\"font-weight:bold;color:green;font-size:18px\">" +  OService.ServiceName + "</span></h3> \r\n" +
                      "             <div> \r\n" +
                      "             <div class=\"row\"> \r\n" +
                      "                <div class=\"col-md-4\">\r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ chuẩn") + "</label> <input READONLY value=\"" + OService.StdServiceName + "\" id=\"txtCurrencyName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Loại tiền") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</label> <input READONLY value=\"" + OService.AssetSupplierName + "\" id=\"txtCurrencyName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Loại tiền") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã dịch vụ") + "</label> <input READONLY value=\"" + OService.ServiceName + "\" id=\"txtServiceCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mã dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + "</label> <input  READONLY value=\"" + OService.ServiceName + "\" id=\"txtServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea READONLY id=\"txtShortDescription\" style=\"height:200px\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\">" + OService.ShortDescription + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea READONLY id=\"txtDescription\" style=\"height:200px\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\">" + OService.Description + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input READONLY value=\"" + OService.UnitName + "\" id=\"txtCurrencyName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "</label> <input READONLY value=\"" + FunctionUtility.FormatNumber(OService.BuyPrice) + "\" id=\"txtBuyPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "</label> <input READONLY value=\"" + FunctionUtility.FormatNumber(OService.SalePrice) + "\" id=\"txtSalePrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "</label> <input READONLY value=\"" + FunctionUtility.FormatNumber(OService.WebPrice) + "\" id=\"txtWebPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input READONLY value=\"" + OService.CurrencyName + "\" id=\"txtCurrencyName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Loại tiền") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "</label> <input READONLY value=\"" + OService.ServiceToolTips + "\" id=\"txtServiceToolTips\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <input READONLY id=\"txtSpecialConditions\" value=\"" + OService.SpecialConditions + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input READONLY value=\"" + OService.SortIndex + "\" id=\"txtSortIndex\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi") + "</label> <input READONLY value=\"" + Scope + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input READONLY value=\"" + OService.SeoTitle + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input READONLY value=\"" + OService.SeoKeyword + "\"  id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input READONLY value=\"" + OService.SeoDescription + "\"  id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input READONLY value=\"" + OService.SeoAuthor + "\"   id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> <input READONLY value=\"" + (OService.Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Đang sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "\"   id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +

                      "                </div>\r\n" +
                      "                <div class=\"col-md-8\">\r\n" +
                                            ServerSideDrawTab(ORenderInfo, OService).HtmlContent +
                      "                </div>\r\n" +
                      "            </div>\r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OService.ServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Sửa dịch vụ") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd('" + OService.frkServiceTypeId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawTab(
            RenderInfoCls ORenderInfo,
            ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                AjaxOut 
                    RetAjaxOutExtraAttributes =  DrawExtraAttributeForm(ORenderInfo, OService.ServiceId);
                string Html =

                     " <div class=\"tabs-container\"> \r\n" +
                     "        <ul class=\"nav nav-tabs\"> \r\n" +
                     "            <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\">"+WebLanguage.GetLanguage(OSiteParam,"Thông tin mở rộng")+"</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">"+WebLanguage.GetLanguage(OSiteParam,"Giá theo mùa")+"</a></li> \r\n" +
                     //"            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">"+WebLanguage.GetLanguage(OSiteParam,"Chia sẻ dịch vụ")+"</a></li> \r\n" +
                     "        </ul> \r\n" +
                     "        <div class=\"tab-content\"> \r\n" +

                     "            <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                     "                <div class=\"panel-body\" style=\"min-height:400px\" id=\"divExtraAttriuteContent\"> \r\n" +
                                           RetAjaxOutExtraAttributes.HtmlContent +
                     "                </div> \r\n" +
                     "            </div> \r\n" +



                     "            <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                                            DrawServicePriceRangeForm(ORenderInfo, OService.ServiceId).HtmlContent +
                     "                </div> \r\n" +
                     "            </div> \r\n" +
                     //"            <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                     //"                <div class=\"panel-body\"> \r\n" +
                     //                       DrawServiceSharedForm(ORenderInfo, OService.ServiceId).HtmlContent +
                     //"                </div> \r\n" +
                     //"            </div> \r\n" +

                     "        </div> \r\n" +


                     "    </div> \r\n";
                   

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
        public static AjaxOut DrawServicePriceRangeForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

              

                string Html =
                    "<div style=\"font-weight:bold;margin-bottom:10px\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách giá theo mùa") + "</div>\r\n" +
                    "<div id=\"divListRangePriceContent\">" + ServerSideReadingRangeServicePrice(ORenderInfo,ServiceId).HtmlContent + "</div>\r\n";
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
        public static AjaxOut ServerSideReadingRangeServicePrice(RenderInfoCls ORenderInfo, string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                RangeServicePriceCls[]
                    RangeServicePrices = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().ReadingRangeServicePrices(ORenderInfo, ServiceId);


                string Html =
                        "         <div id=\"divProcessingUpdate\" style=\"height:20px;color:maroon;font-weight:bold\"></div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Từ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đến") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + " </th> \r\n" +
                        //"                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < RangeServicePrices.Length; iIndex++)
                {
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                        "                     <td>" + RangeServicePrices[iIndex].StartDateText + "</td> \r\n" +
                        "                     <td>" + RangeServicePrices[iIndex].EndDateText + "</td> \r\n" +
                        "                     <td style=\"width:100px;\"><input READONLY style=\"font-family:consola;font-size:16px;font-weight:bold;padding:4px;width:100px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtSalePrice" + RangeServicePrices[iIndex].RangeServicePriceId + "\" value=\"" + FunctionUtility.FormatNumber(RangeServicePrices[iIndex].SalePrice) + "\"></td> \r\n" +
                        "                     <td style=\"width:100px\"><input  READONLY style=\"font-family:consola;font-size:16px;font-weight:bold;padding:4px;width:100px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtWebPrice" + RangeServicePrices[iIndex].RangeServicePriceId + "\" value=\"" + FunctionUtility.FormatNumber(RangeServicePrices[iIndex].WebPrice) + "\"></td> \r\n" +
                        //"                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa giá theo mùa") + "\" href=\"javascript:CallActionDeleteRangeServicePrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

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
        public static AjaxOut DrawServiceSharedForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string Html =
                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đại lý chia sẻ") + "</div>\r\n" +
                    "<div id=\"divListSharedContent\">" + ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId).HtmlContent + "</div>\r\n";
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
        public static AjaxOut DrawExtraAttributeForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().InitServiceExtraInfo(ORenderInfo, ServiceId);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

                

                string Html = "";
                
                ServiceTypeExtraInfoCls[]
                    ServiceTypeExtraInfos = OService.ServiceTypeExtraInfos;
               
                string[] Ids = new string[ServiceTypeExtraInfos.Length];
                for (int iIndex = 0; iIndex < ServiceTypeExtraInfos.Length; iIndex++)
                {
                    Ids[iIndex] = ServiceTypeExtraInfos[iIndex].ServiceTypeExtraInfoId;
                    Html+=
                        "<div style=\"font-weight:bold;margin-bottom:10px;color:green;font-size:16px\">" + WebLanguage.GetLanguage(OSiteParam, ServiceTypeExtraInfos[iIndex].ExtraFieldType) + "</div>\r\n" +
                        "<div style=\"margin-bottom:20px\"><div>" + ServiceTypeExtraInfos[iIndex].Content + "</div></div>\r\n";
                }

                RetAjaxOut.RetObject = Ids;
                if (ServiceTypeExtraInfos.Length == 0)
                {
                    Html += "<div class=\"note note-info\">" + WebLanguage.GetLanguage(OSiteParam, "Không có thuộc tính mở rộng") + "</div>\r\n";
                }
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
        public static AjaxOut ServerSideReadingServiceSharedOwner(RenderInfoCls ORenderInfo, string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                ServiceSharedOwnerCls[]
                    ServiceSharedOwners = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().ReadingSharedOwner(ORenderInfo, ServiceId);


                string Html =
                        "         <div id=\"divProcessingUpdateShared\" style=\"height:20px;color:maroon;font-weight:bold\"></div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã đại lý") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên đại lý") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tỉ lệ tăng/giảm") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < ServiceSharedOwners.Length; iIndex++)
                {
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                        "                     <td>" + ServiceSharedOwners[iIndex].OwnerCode + "</td> \r\n" +
                        "                     <td>" + ServiceSharedOwners[iIndex].OwnerName + "</td> \r\n" +
                        "                     <td style=\"width:100px;\"><input onkeypress=\"if(event.keyCode==13)UpdateSharedMarkupPercent('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "',1);\" onblur=\"javascript:UpdateSharedMarkupPercent('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "',0);\"  style=\"font-family:consola;font-size:16px;font-weight:bold;padding:4px;width:100px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtServiceSharedOwnerMarkUpPercent" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "\" value=\"" + FunctionUtility.FormatNumber(ServiceSharedOwners[iIndex].MarkUpPercent) + "\"></td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa chia sẻ đại lý") + "\" href=\"javascript:CallActionDeleteServiceSharedOwner('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

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
        public static AjaxOut ServerSideAddShared(
            RenderInfoCls ORenderInfo, 
            string ServiceId,
            string OwnerId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (string.IsNullOrEmpty(OwnerId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Chưa chọn đại lý"));
                }

                
                ServiceCls
                    OService=CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo,ServiceId);
                
                ServiceSharedOwnerCls
                    OServiceSharedOwner = new ServiceSharedOwnerCls();
                OServiceSharedOwner.ServiceSharedOwnerId = System.Guid.NewGuid().ToString();
                OServiceSharedOwner.frkOwnerId = OwnerId;
                OServiceSharedOwner.frkServiceId = ServiceId;
                OServiceSharedOwner.MarkUpPercent = 0;
                

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().AddSharedOwner(ORenderInfo, ServiceId, OServiceSharedOwner);
                RetAjaxOut = ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideDeleteServiceSharedOwner(
            RenderInfoCls ORenderInfo,
            string ServiceSharedOwnerId,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().DeleteSharedOwner(ORenderInfo, ServiceSharedOwnerId);
                RetAjaxOut = ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideViewServiceSharedOwner(
            RenderInfoCls ORenderInfo,
            string ServiceSharedOwnerId,
            string MarkUpPercent)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (FunctionUtility.checkInteger(MarkUpPercent) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Tỉ lệ tăng giảm % không hợp lệ"));
                }

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().SaveSharedOwnerMarkUpPercent(ORenderInfo, ServiceSharedOwnerId, int.Parse(MarkUpPercent));
                RetAjaxOut.InfoMessage=WebLanguage.GetLanguage(OSiteParam,"Cập nhật tỉ lệ tăng giảm thành công");
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
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new UpdateService().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceId",ServiceId)
                });
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
