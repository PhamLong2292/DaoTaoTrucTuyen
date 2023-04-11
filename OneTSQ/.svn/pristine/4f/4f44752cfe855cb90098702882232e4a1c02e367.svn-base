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
    public class UpdateService : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "UpdateService";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Sửa chữa dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Sửa chữa dịch vụ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(UpdateService),Page);
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

                string OwnerCode=(string)WebEnvironments.Request("OwnerCode");
                string ServiceId = (string)WebEnvironments.Request("ServiceId");
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                ServiceCls
                 OService = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(OActionSqlParam, ServiceId);
                if (OService == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "dịch vụ đã bị xóa hoặc không tìm thấy"));
                }

                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Service().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",OService.frkServiceTypeId)
                });
                RetAjaxOut.HtmlContent =
                    "<script>\r\n"+
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('"+BackUrl+"','_self');\r\n"+
                    "   }\r\n" +



                    "   function CallActionUpdate(ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AssetSupplierId= document.getElementById('drpSelectAssetSupplier').value;\r\n" +
                    "       StdServiceId= document.getElementById('drpSelectStdService').value;\r\n" +
                    "       ServiceName = document.getElementById('txtServiceName').value;\r\n" +
                    "       ShortDescription = document.getElementById('txtShortDescription').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       UnitId = document.getElementById('drpSelectUnit').value;\r\n" +


                    "       BuyPrice = document.getElementById('txtBuyPrice').value;\r\n" +
                    "       SalePrice = document.getElementById('txtSalePrice').value;\r\n" +
                    "       WebPrice = document.getElementById('txtWebPrice').value;\r\n" +
                    "       ActiveRangePrice= document.getElementById('drpSelectRangePrice').value;\r\n" +

                    "       SeoTitle = document.getElementById('txtSeoTitle').value;\r\n" +
                    "       SeoKeyword = document.getElementById('txtSeoKeyword').value;\r\n" +
                    "       SeoDescription = document.getElementById('txtSeoDescription').value;\r\n" +
                    "       SeoAuthor = document.getElementById('txtSeoAuthor').value;\r\n" +


                    "       ServiceToolTips = document.getElementById('txtServiceToolTips').value;\r\n" +
                    "       SpecialConditions = document.getElementById('txtSpecialConditions').value;\r\n" +

                    "       SharedScope= parseInt(document.getElementById('drpSharedScope').value,10);\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveService = OnlineTour.WebParts.UpdateService.ServerSideCreateServiceObject(RenderInfo, ServiceId).value.RetObject;\r\n" +
                    "       OSaveService.ServiceName  = ServiceName;\r\n" +
                    "       OSaveService.frkAssetSupplierId  = AssetSupplierId;\r\n" +
                    "       OSaveService.frkStdServiceId = StdServiceId;\r\n" +
                    "       OSaveService.ShortDescription  = ShortDescription;\r\n" +
                    "       OSaveService.Description  = Description;\r\n" +
                    "       OSaveService.frkUnitId  = UnitId;\r\n" +
                    "       OSaveService.BuyPrice  = BuyPrice;\r\n" +
                    "       OSaveService.SalePrice  = SalePrice;\r\n" +
                    "       OSaveService.WebPrice  = WebPrice;\r\n" +
                    "       OSaveService.ActiveRangePrice  = ActiveRangePrice;\r\n" +

                    "       OSaveService.SharedScope  = SharedScope;\r\n" +
                    "       OSaveService.SeoTitle  = SeoTitle;\r\n" +
                    "       OSaveService.SeoDescription  = SeoDescription;\r\n" +
                    "       OSaveService.SeoAuthor  = SeoAuthor;\r\n" +
                    "       OSaveService.SeoKeyword  = SeoKeyword;\r\n" +
                    "       OSaveService.ServiceToolTips  = ServiceToolTips;\r\n" +
                    "       OSaveService.SpecialConditions  = SpecialConditions;\r\n" +
                    "       OSaveService.Active = Active;\r\n"+


                    "       AjaxOut = OnlineTour.WebParts.UpdateService.ServerSideUpdate(RenderInfo, ServiceId, OSaveService).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallBack();\r\n" +
                    "   }\r\n" +


                    "</script>\r\n"+
                    ServerSideDrawUpdateForm(ORenderInfo, OService).HtmlContent;

                
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

             
                string SelectActiveText =
                     "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                     "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                     "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                     "</select>\r\n";

                if (OService.Active == 1)
                {
                    SelectActiveText =
                         "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                         "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                         "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                         "</select>\r\n";
                }

                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);



                StdServiceFilterCls
                  OStdServiceFilter = new StdServiceFilterCls();
                OStdServiceFilter.ActiveOnly = 1;
                OStdServiceFilter.ServiceTypeId = OService.frkServiceTypeId;
                StdServiceCls[]
                    StdServices = OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Reading(ActionSqlParam, OStdServiceFilter);
                string SelectServiceText =
                    "<select id=\"drpSelectStdService\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</option>\r\n";
                for (int iIndex = 0; iIndex < StdServices.Length; iIndex++)
                {
                    if (StdServices[iIndex].StdServiceId.Equals(OService.frkStdServiceId))
                    {
                        SelectServiceText += "    <option selected value=\"" + StdServices[iIndex].StdServiceId + "\">" + StdServices[iIndex].StdServiceName + "</option>\r\n";
                    }
                    else
                    {
                        SelectServiceText += "    <option value=\"" + StdServices[iIndex].StdServiceId + "\">" + StdServices[iIndex].StdServiceName + "</option>\r\n";
                    }
                }
                SelectServiceText += "</select>\r\n";

                string SelectShareScopeText =
                    "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                    "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                    "</select>\r\n";
                if (OService.SharedScope == 1)
                {
                    SelectShareScopeText =
                        "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                        "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                        "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                        "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                        "</select>\r\n";
                }
                if (OService.SharedScope == 2)
                {
                    SelectShareScopeText =
                        "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                        "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                        "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                        "   <option selected value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                        "</select>\r\n";
                }

                string SelectRangeServiceText =
                  "<select id=\"drpSelectRangePrice\" class=\"form-control\">\r\n" +
                  "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Bình thường") + "</option>\r\n" +
                  "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa") + "</option>\r\n" +
                  "</select>\r\n";
                if (OService.ActiveRangePrice == 1)
                {
                    SelectRangeServiceText =
                      "<select id=\"drpSelectRangePrice\" class=\"form-control\">\r\n" +
                      "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Bình thường") + "</option>\r\n" +
                      "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa") + "</option>\r\n" +
                      "</select>\r\n";
                }

                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = OnlineTourBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ActionSqlParam, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    if (Units[iIndex].UnitId.Equals(OService.frkUnitId))
                    {
                        SelectUnitText += "   <option selected value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                    else
                    {
                        SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                }
                SelectUnitText += "</select>\r\n";

                AssetSupplierFilterCls
                 OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.ActiveOnly = 1;
                AssetSupplierCls[]
                    AssetSuppliers = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ActionSqlParam, OAssetSupplierFilter);

                string SelectAssetSupplierText =
                    " <select onchange=\"javascript:CallReading();\" id=\"drpSelectAssetSupplier\" class=\"form-control select2\">\r\n" +
                    "       <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn nhà cung cấp") + "</option>\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    if (OService.frkAssetSupplierId.Equals(AssetSuppliers[iIndex].AssetSupplierId))
                    {
                        SelectAssetSupplierText +=
                            "   <option selected value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                    }
                    else
                    {
                        SelectAssetSupplierText +=
                            "   <option value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                    }
                }
                SelectAssetSupplierText += "</select>\r\n";

                string Html =
                   " <div class=\"ibox-content\"> \r\n" +
                   "     <div class=\"row\"> \r\n" +
                   "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa dịch vụ") + "</h3> \r\n" +
                      "             <div> \r\n" +
                      "             <div class=\"row\"> \r\n" +
                      "                <div class=\"col-md-4\">\r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</label> " + SelectServiceText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</label> " + SelectAssetSupplierText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + "</label> <input value=\"" + OService.ServiceName + "\" id=\"txtServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\">" + OService.ShortDescription + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\">" + OService.Description + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> " + SelectUnitText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "</label> <input value=\"" + OService.BuyPrice.ToString("###0") + "\" id=\"txtBuyPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "</label> <input value=\"" + OService.SalePrice.ToString("###0") + "\" id=\"txtSalePrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "</label> <input value=\"" + OService.WebPrice.ToString("###0") + "\" id=\"txtWebPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Kiểu giá") + "</label> " + SelectRangeServiceText + "</div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "</label> <input value=\"" + OService.ServiceToolTips + "\" id=\"txtServiceToolTips\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <input id=\"txtSpecialConditions\" value=\"" + OService.SpecialConditions + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"" + OService.SortIndex + "\" id=\"txtSortIndex\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi") + "</label> " + SelectShareScopeText + "</div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input value=\"" + OService.SeoTitle + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input value=\"" + OService.SeoKeyword + "\"  id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input value=\"" + OService.SeoDescription + "\"  id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input value=\"" + OService.SeoAuthor + "\"   id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +

                      "                </div>\r\n" +
                      "                <div class=\"col-md-8\">\r\n" +
                                            ServerSideDrawTab(ORenderInfo, OService).HtmlContent +
                      "                </div>\r\n" +
                      "            </div>\r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OService.ServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawTab(
            RenderInfoCls ORenderInfo,
            ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                string Html =
                     " <div class=\"tabs-container\"> \r\n" +
                     "        <ul class=\"nav nav-tabs\"> \r\n" +
                     "            <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\"> Giá theo mùa</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">Chia sẻ dịch vụ</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">Ảnh dịch vụ</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-4\">Mô tả</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-5\">Phân loại</a></li> \r\n" +
                     "        </ul> \r\n" +
                     "        <div class=\"tab-content\"> \r\n" +
                     "            <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                     "                    <strong>Lorem ipsum dolor sit amet, consectetuer adipiscing</strong> \r\n" +

                     "                    <p>A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of \r\n" +
                     "                        existence in this spot, which was created for the bliss of souls like mine.</p> \r\n" +

                     "                    <p>I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at \r\n" +
                     "                        the present moment; and yet I feel that I never was a greater artist than now. When.</p> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +
                     "            <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                     "                    <strong>Donec quam felis</strong> \r\n" +

                     "                    <p>Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects \r\n" +
                     "                        and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath </p> \r\n" +

                     "                    <p>I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite \r\n" +
                     "                        sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.</p> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +

                     "            <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                     "                    <strong>Donec quam felis</strong> \r\n" +

                     "                    <p>Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects \r\n" +
                     "                        and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath </p> \r\n" +

                     "                    <p>I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite \r\n" +
                     "                        sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.</p> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +

                     "            <div id=\"tab-4\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                     "                    <strong>Donec quam felis</strong> \r\n" +

                     "                    <p>Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects \r\n" +
                     "                        and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath </p> \r\n" +

                     "                    <p>I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite \r\n" +
                     "                        sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.</p> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +

                      "            <div id=\"tab-5\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                     "                    <strong>Donec quam felis</strong> \r\n" +

                     "                    <p>Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects \r\n" +
                     "                        and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath </p> \r\n" +

                     "                    <p>I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite \r\n" +
                     "                        sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.</p> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +
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
        public static AjaxOut ServerSideCreateUpdateServiceObject(
            RenderInfoCls ORenderInfo, 
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    ActionSqlParam=new ActionSqlParamCls();
                SaveServiceCls OSaveUpdateService = new SaveServiceCls();
                if (string.IsNullOrEmpty(ServiceId))
                {
                    OSaveUpdateService.ServiceId = System.Guid.NewGuid().ToString();
                }
                else
                {
                    OSaveUpdateService.ServiceId = ServiceId;
                }
                RetAjaxOut.RetObject = OSaveUpdateService;
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
            string ServiceId,
            SaveServiceCls OSaveService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(OSaveService.ServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveService.frkAssetSupplierId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn nhà cung cấp dịch vụ"));
                if (FunctionUtilities.checkDecimal(OSaveService.BuyPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá mua không hợp lệ"));
                if (FunctionUtilities.checkDecimal(OSaveService.SalePrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán không hợp lệ"));
                if (FunctionUtilities.checkDecimal(OSaveService.WebPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán trên web không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                ServiceCls
                    OService = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(OActionSqlParam, ServiceId);

                OService.frkServiceTypeId = OSaveService.frkServiceTypeId;
                OService.frkAssetSupplierId = OSaveService.frkAssetSupplierId;
                OService.ServiceName = OSaveService.ServiceName;
                OService.frkUnitId = OSaveService.frkUnitId;

                OService.BuyPrice = decimal.Parse(OSaveService.BuyPrice);
                OService.SalePrice = decimal.Parse(OSaveService.SalePrice);
                OService.WebPrice = decimal.Parse(OSaveService.WebPrice);

                OService.ShortDescription = OSaveService.ShortDescription;
                OService.Description = OSaveService.Description;

                OService.SharedScope = OSaveService.SharedScope;
                OService.ActiveRangePrice = OSaveService.ActiveRangePrice;

                OService.ServiceToolTips = OSaveService.ServiceToolTips;
                OService.SpecialConditions = OSaveService.SpecialConditions;

                OService.SeoTitle = OSaveService.SeoTitle;
                OService.SeoAuthor = OSaveService.SeoAuthor;
                OService.SeoKeyword = OSaveService.SeoKeyword;
                OService.SeoDescription = OSaveService.SeoDescription;
                OService.frkStdServiceId = OSaveService.frkStdServiceId;
                OService.SortIndex = OSaveService.SortIndex;
                OService.Active = OSaveService.Active;


                OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Save(OActionSqlParam, ServiceId, OService);
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
        public static AjaxOut ServerSideCreateServiceObject(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                ServiceCls
                    OService = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(OActionSqlParam, ServiceId);

                RetAjaxOut.RetObject = OService;
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
