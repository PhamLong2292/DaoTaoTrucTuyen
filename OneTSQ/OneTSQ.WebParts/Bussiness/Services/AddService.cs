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
    public class AddService : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "AddService";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thêm mới dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Thêm mới dịch vụ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AddService),Page);
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
                string ServiceTypeId = (string)WebEnvironments.Request("ServiceTypeId");
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Service().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId)
                });
                RetAjaxOut.HtmlContent =
                    "<script>\r\n"+
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('"+BackUrl+"','_self');\r\n"+
                    "   }\r\n" +



                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AssetSupplierId= document.getElementById('drpSelectAssetSupplier').value;\r\n" +
                    "       StdServiceId= document.getElementById('drpSelectStdService').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
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

                    "       OSaveService = OnlineTour.WebParts.AddService.ServerSideCreateServiceObject(RenderInfo, null).value.RetObject;\r\n" +
                    "       OSaveService.ServiceName  = ServiceName;\r\n" +
                    "       OSaveService.frkStdServiceId = StdServiceId;\r\n" +
                    "       OSaveService.frkAssetSupplierId  = AssetSupplierId;\r\n" +
                    "       OSaveService.frkServiceTypeId  = ServiceTypeId;\r\n" +
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

                    "       AjaxOut = OnlineTour.WebParts.AddService.ServerSideAdd(RenderInfo, OSaveService).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +


                    "</script>\r\n"+
                    ServerSideDrawAddForm(ORenderInfo, ServiceTypeId).HtmlContent;

                
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo, string AddServiceTypeId)
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

                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = OnlineTourBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ActionSqlParam, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                }
                SelectUnitText += "</select>\r\n";


                ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                AssetSupplierFilterCls
                    OAssetSupplierFilter = new AssetSupplierFilterCls();
                AssetSupplierCls[]
                    AssetSuppliers = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ActionSqlParam, OAssetSupplierFilter);

                string SelectAssetSupplierText = "<select style=\"width:100%\" id=\"drpSelectAssetSupplier\" class=\"form-control select2\">\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    SelectAssetSupplierText += "   <option value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                }
                SelectAssetSupplierText += "</select>\r\n";

                StdServiceFilterCls
                    OStdServiceFilter = new StdServiceFilterCls();
                OStdServiceFilter.ActiveOnly = 1;
                OStdServiceFilter.ServiceTypeId = AddServiceTypeId;
                StdServiceCls[]
                    StdServices = OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Reading(ActionSqlParam, OStdServiceFilter);
                string SelectAddServiceText =
                    "<select id=\"drpSelectStdAddService\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</option>\r\n";
                for (int iIndex = 0; iIndex < StdServices.Length; iIndex++)
                {
                    SelectAddServiceText += "    <option value=\"" + StdServices[iIndex].StdServiceId + "\">" + StdServices[iIndex].StdServiceName + "</option>\r\n";
                }
                SelectAddServiceText += "</select>\r\n";

                string SelectShareScopeText =
                    "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                    "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                    "</select>\r\n";


                string SelectRangeAddServiceText =
                  "<select id=\"drpSelectRangePrice\" class=\"form-control\">\r\n" +
                  "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Bình thường") + "</option>\r\n" +
                  "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa") + "</option>\r\n" +
                  "</select>\r\n";
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới dịch vụ") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-4\">\r\n"+
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</label> "+SelectAddServiceText+"</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</label> " + SelectAssetSupplierText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + "</label> <input id=\"txtServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> "+SelectUnitText+"</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "</label> <input value=\"0\" id=\"txtBuyPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "</label> <input value=\"0\" id=\"txtSalePrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "</label> <input value=\"0\" id=\"txtWebPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Kiểu giá") + "</label> " + SelectRangeAddServiceText + "</div> \r\n" +
                       "                </div>\r\n"+
                       "                <div class=\"col-md-4\">\r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "</label> <input id=\"txtServiceToolTips\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <input id=\"txtSpecialConditions\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "\" class=\"form-control\"></div> \r\n" +

                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"1\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi") + "</label> " + SelectShareScopeText + "</div> \r\n" +
                       
                       "                </div>\r\n"+
                       "              <div class=\"col-md-4\"> \r\n" +

                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +

                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "              </div>\r\n"+
                       "            </div>\r\n"+
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
        public static AjaxOut ServerSideCreateAddServiceObject(
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
                SaveServiceCls OSaveAddService = new SaveServiceCls();
                if (string.IsNullOrEmpty(ServiceId))
                {
                    OSaveAddService.ServiceId = System.Guid.NewGuid().ToString();
                }
                else
                {
                    OSaveAddService.ServiceId = ServiceId;
                }
                RetAjaxOut.RetObject = OSaveAddService;
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
            SaveServiceCls OSaveAddService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(OSaveAddService.ServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddService.frkAssetSupplierId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn nhà cung cấp dịch vụ"));
                if (FunctionUtilities.checkDecimal(OSaveAddService.BuyPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá mua không hợp lệ"));
                if (FunctionUtilities.checkDecimal(OSaveAddService.SalePrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán không hợp lệ"));
                if (FunctionUtilities.checkDecimal(OSaveAddService.WebPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán trên web không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
        
                ServiceCls 
                    OAddService = new ServiceCls();
                OAddService.ServiceId = OSaveAddService.ServiceId;
                OAddService.frkOwnerId = OwnerId;
                OAddService.frkServiceTypeId = OSaveAddService.frkServiceTypeId;
                OAddService.frkAssetSupplierId = OSaveAddService.frkAssetSupplierId;
                OAddService.ServiceName = OSaveAddService.ServiceName;
                OAddService.frkUnitId = OSaveAddService.frkUnitId;

                OAddService.BuyPrice = decimal.Parse(OSaveAddService.BuyPrice);
                OAddService.SalePrice = decimal.Parse(OSaveAddService.SalePrice);
                OAddService.WebPrice = decimal.Parse(OSaveAddService.WebPrice);

                OAddService.ShortDescription = OSaveAddService.ShortDescription;
                OAddService.Description = OSaveAddService.Description;

                OAddService.SharedScope = OSaveAddService.SharedScope;
                OAddService.ActiveRangePrice = OSaveAddService.ActiveRangePrice;

                OAddService.ServiceToolTips = OSaveAddService.ServiceToolTips;
                OAddService.SpecialConditions = OSaveAddService.SpecialConditions;

                OAddService.SeoTitle = OSaveAddService.SeoTitle;
                OAddService.SeoAuthor = OSaveAddService.SeoAuthor;
                OAddService.SeoKeyword = OSaveAddService.SeoKeyword;
                OAddService.SeoDescription = OSaveAddService.SeoDescription;

                OAddService.SortIndex = OSaveAddService.SortIndex;
                OAddService.Active = OSaveAddService.Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Add(OActionSqlParam, OAddService);

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, new UpdateService().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceId",OAddService.ServiceId)
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


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCreateServiceObject(
            RenderInfoCls ORenderInfo)
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
                    OService = new ServiceCls();
                OService.ServiceId = System.Guid.NewGuid().ToString();

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

    }
}
