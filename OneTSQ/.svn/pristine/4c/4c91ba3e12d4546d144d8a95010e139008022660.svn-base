using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class AddService : WebPartTemplate
    {
        class RangePriceCls
        {
            public string Subject;
            public int Value;

            public RangePriceCls()
            {
            }

            public RangePriceCls(string _Subject, int _Value)
            {
                Subject = _Subject;
                Value = _Value;
            }
        }

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

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new Service().WebPartId };
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AddService),Page);
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
                string ServiceTypeId = (string)WebEnvironments.Request("ServiceTypeId");
                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new Service().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId)
                });
                RetAjaxOut.HtmlContent =
                    "<script>\r\n" +
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('" + BackUrl + "','_self');\r\n" +
                    "   }\r\n" +



                    "   function CallActionAdd(Type)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AssetSupplierId= document.getElementById('drpSelectAssetSupplier').value;\r\n" +
                    "       StdServiceId= document.getElementById('drpSelectStdService').value;\r\n" +
                    "       ServiceTypeId = '"+ServiceTypeId+"';\r\n"+
                    "       ServiceCode = document.getElementById('txtServiceCode').value;\r\n" +
                    "       ServiceName = document.getElementById('txtServiceName').value;\r\n" +
                    "       ShortDescription = document.getElementById('txtShortDescription').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       UnitId = document.getElementById('drpSelectUnit').value;\r\n" +


                    "       BuyPrice = document.getElementById('txtBuyPrice').value;\r\n" +
                    "       SalePrice = document.getElementById('txtSalePrice').value;\r\n" +
                    "       WebPrice = document.getElementById('txtWebPrice').value;\r\n" +
                    "       PriceType= document.getElementById('drpSelectPriceType').value;\r\n" +

                    "       SeoTitle = document.getElementById('txtSeoTitle').value;\r\n" +
                    "       SeoKeyword = document.getElementById('txtSeoKeyword').value;\r\n" +
                    "       SeoDescription = document.getElementById('txtSeoDescription').value;\r\n" +
                    "       SeoAuthor = document.getElementById('txtSeoAuthor').value;\r\n" +
                    "       CurrencyId= document.getElementById('drpSelectCurrency').value;\r\n" +

                    "       ServiceToolTips = document.getElementById('txtServiceToolTips').value;\r\n" +
                    "       SpecialConditions = document.getElementById('txtSpecialConditions').value;\r\n" +

                    "       SharedScope= parseInt(document.getElementById('drpSharedScope').value,10);\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveService = OneTSQ.WebParts.AddService.ServerSideCreateServiceObject(RenderInfo, null).value.RetObject;\r\n" +
                    "       OSaveService.ServiceCode = ServiceCode;\r\n" +
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
                    "       OSaveService.PriceType  = PriceType;\r\n" +
                    "       OSaveService.frkCurrencyId = CurrencyId; \r\n" +
                    "       OSaveService.SharedScope  = SharedScope;\r\n" +
                    "       OSaveService.SeoTitle  = SeoTitle;\r\n" +
                    "       OSaveService.SeoDescription  = SeoDescription;\r\n" +
                    "       OSaveService.SeoAuthor  = SeoAuthor;\r\n" +
                    "       OSaveService.SeoKeyword  = SeoKeyword;\r\n" +
                    "       OSaveService.ServiceToolTips  = ServiceToolTips;\r\n" +
                    "       OSaveService.SpecialConditions  = SpecialConditions;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.AddService.ServerSideAdd(RenderInfo, OSaveService, Type).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "</script>\r\n" +
                    ServerSideDrawAddForm(ORenderInfo, ServiceTypeId).HtmlContent +
                    "<script>\r\n" +
                    "   $('#drpSelectAssetSupplier').select2();\r\n"+
                    "   $('#drpSelectStdService').select2();\r\n" +
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo, string AddServiceTypeId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";

                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = CallBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ORenderInfo, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                }
                SelectUnitText += "</select>\r\n";

                CurrencyFilterCls
               OCurrencyFilter = new CurrencyFilterCls();
                OCurrencyFilter.ActiveOnly = 1;

                CurrencyCls[]
                    Currencies = CallBussinessUtility.CreateBussinessProcess().CreateCurrencyProcess().Reading(ORenderInfo, OCurrencyFilter);
                string SelectCurrencyText =
                    "<select id=\"drpSelectCurrency\" class=\"form-control select2\">\r\n";
                for (int iIndex = 0; iIndex < Currencies.Length; iIndex++)
                {
                    SelectCurrencyText += "    <option value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                }
                SelectCurrencyText += "</select>\r\n";
                
                AssetSupplierFilterCls
                    OAssetSupplierFilter = new AssetSupplierFilterCls();
                AssetSupplierCls[]
                    AssetSuppliers = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ORenderInfo, OAssetSupplierFilter);

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
                    StdServices = CallBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Reading(ORenderInfo, OStdServiceFilter);
                string SelectAddServiceText =
                    "<select id=\"drpSelectStdService\" class=\"form-control select2\">\r\n" +
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

                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd(0);\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ghi và tiếp tục") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd(1);\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ghi và sửa") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới dịch vụ") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-4\">\r\n"+
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</label> "+SelectAddServiceText+"</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</label> " + SelectAssetSupplierText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã dịch vụ") + "</label> <input id=\"txtServiceCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã dịch vụ để trống lấy tự động") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + "</label> <input id=\"txtServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> "+SelectUnitText+"</div> \r\n" +
                       
                       "                </div>\r\n"+
                       "                <div class=\"col-md-4\">\r\n" +

                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "</label> <input value=\"0\" id=\"txtBuyPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "</label> <input value=\"0\" id=\"txtSalePrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "</label> <input value=\"0\" id=\"txtWebPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại tiền gốc") + "</label> " + SelectCurrencyText + "</div> \r\n" +
                       //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Kiểu giá") + "</label> " + SelectPriceText + "</div> \r\n" +

                       "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "</label> <input id=\"txtServiceToolTips\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <input id=\"txtSpecialConditions\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "\" class=\"form-control\"></div> \r\n" +

                       "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"1\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
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
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd(0);\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ghi và tiếp tục") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd(1);\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ghi và sửa") + "</strong></button> \r\n" +
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
                WebSession.CheckSessionTimeOut(ORenderInfo);

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
            SaveServiceCls OSaveAddService,
            int Type)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                if (string.IsNullOrEmpty(OSaveAddService.ServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddService.frkAssetSupplierId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn nhà cung cấp dịch vụ"));
                if (FunctionUtility.checkDecimal(OSaveAddService.BuyPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá mua không hợp lệ"));
                if (FunctionUtility.checkDecimal(OSaveAddService.SalePrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán không hợp lệ"));
                if (FunctionUtility.checkDecimal(OSaveAddService.WebPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán trên web không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddService.frkCurrencyId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn loại tiền gốc"));
                
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
        
                ServiceCls 
                    OAddService = new ServiceCls();
                OAddService.ServiceId = OSaveAddService.ServiceId;

                OAddService.ServiceCode = OSaveAddService.ServiceCode;
                OAddService.frkOwnerId = OwnerId;
                OAddService.frkServiceTypeId = OSaveAddService.frkServiceTypeId;
                OAddService.frkAssetSupplierId = OSaveAddService.frkAssetSupplierId;
                OAddService.ServiceName = OSaveAddService.ServiceName;
                OAddService.frkUnitId = OSaveAddService.frkUnitId;
                OAddService.frkCurrencyId = OSaveAddService.frkCurrencyId;
                OAddService.BuyPrice = decimal.Parse(OSaveAddService.BuyPrice);
                OAddService.SalePrice = decimal.Parse(OSaveAddService.SalePrice);
                OAddService.WebPrice = decimal.Parse(OSaveAddService.WebPrice);

                OAddService.ShortDescription = OSaveAddService.ShortDescription;
                OAddService.Description = OSaveAddService.Description;

                OAddService.SharedScope = OSaveAddService.SharedScope;
                OAddService.PriceType = OSaveAddService.PriceType;

                OAddService.ServiceToolTips = OSaveAddService.ServiceToolTips;
                OAddService.SpecialConditions = OSaveAddService.SpecialConditions;

                OAddService.SeoTitle = OSaveAddService.SeoTitle;
                OAddService.SeoAuthor = OSaveAddService.SeoAuthor;
                OAddService.SeoKeyword = OSaveAddService.SeoKeyword;
                OAddService.SeoDescription = OSaveAddService.SeoDescription;

                OAddService.SortIndex = OSaveAddService.SortIndex;
                OAddService.Active = OSaveAddService.Active;

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Add(ORenderInfo, OAddService);

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (Type == 1)
                {
                    RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new UpdateService().WebPartId, new WebParamCls[]
                    {
                        new WebParamCls("ServiceId",OAddService.ServiceId)
                    });
                }
                else
                {
                    RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AddService().WebPartId, new WebParamCls[]
                    {
                        new WebParamCls("ServiceTypeId",OAddService.frkServiceTypeId)
                    });
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
        public static AjaxOut ServerSideCreateServiceObject(
            RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
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
