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
    public class AddCaseProfile : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "AddCaseProfile";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thêm mới sự vụ";
            }
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new CaseProfile().WebPartId };
            }
        }

        public override string Description
        {
            get
            {
                return "Thêm mới sự vụ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AddCaseProfile),Page);
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
                string CaseProfileTypeId = (string)WebEnvironments.Request("CaseProfileTypeId");
                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CaseProfile().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("CaseProfileTypeId",CaseProfileTypeId)
                });




                RetAjaxOut.HtmlContent =
                    "<script>\r\n" +
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('" + BackUrl + "','_self');\r\n" +
                    "   }\r\n" +



                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       CustomerId = document.getElementById('drpSelectCustomer').value;\r\n" +
                    "       CaseProfileName = document.getElementById('txtCaseProfileName').value;\r\n" +
                    "       Description = tinyMCE.get('txtDescription').getContent();\r\n" +
                    "       SharedOwnerIds=$('#drpSelectSharedOwner').val();\r\n" +
                    "       SharedOwnerUserIds=$('#drpSelectSharedOwnerUser').val();\r\n" +
                    "       OpenDate = document.getElementById('txtOpenDate').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveCaseProfile = OneTSQ.WebParts.AddCaseProfile.ServerSideCreateCaseProfileObject(RenderInfo, null).value.RetObject;\r\n" +
                    "       OSaveCaseProfile.CustomerId = CustomerId;\r\n"+
                    "       OSaveCaseProfile.CaseProfileName  = CaseProfileName;\r\n" +
                    "       OSaveCaseProfile.Description  = Description;\r\n" +
                    "       OSaveCaseProfile.OpenDate = OpenDate;\r\n" +
                    "       OSaveCaseProfile.SharedOwnerIds=SharedOwnerIds;\r\n" +
                    "       OSaveCaseProfile.SharedOwnerUserIds=SharedOwnerUserIds;\r\n" +
                    "       OSaveCaseProfile.Active = Active;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.AddCaseProfile.ServerSideAdd(RenderInfo, OSaveCaseProfile).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +

                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "</script>\r\n" +
                    ServerSideDrawAddForm(ORenderInfo, CaseProfileTypeId).HtmlContent +
                    " <script>\r\n" +
                    "       $('.CssDate').datepicker({\r\n" +
                    "           format: 'dd/mm/yyyy'\r\n" +
                    "       });\r\n" +
                    "   $('#drpSelectCustomer').select2();\r\n"+
                        WebScreen.GetMceEditor("txtDescription",300)+
                    "    $('#drpSelectSharedOwner').select2();\r\n" +
                    "    $('#drpSelectSharedOwnerUser').select2();\r\n" +
                    " </script>\r\n";

                
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo, string AddCaseProfileTypeId)
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



                CustomerFilterCls
                   OCustomerFilter = new CustomerFilterCls();
                OCustomerFilter.ActiveOnly = 1;
                CustomerCls[]
                    Customers = CallBussinessUtility.CreateBussinessProcess().CreateCustomerProcess().Reading(ORenderInfo, OCustomerFilter);

                string SelectCustomerText =
                    "<select id=\"drpSelectCustomer\" class=\"form-control\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn khách hàng") + "</option>\r\n";
                for (int iIndex = 0; iIndex < Customers.Length; iIndex++)
                {
                    SelectCustomerText += "    <option value=\"" + Customers[iIndex].CustomerId + "\">" + Customers[iIndex].Company + "</option>\r\n";
                }
                SelectCustomerText += "</select>\r\n";

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                OwnerFilterCls
                    OwnerFilter = new OwnerFilterCls();
                OwnerFilter.ExcOwnerId = OwnerId;

                OwnerCls[]
                    Owners = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().Reading(ORenderInfo, OwnerFilter);

                string SelectSharedScopeText =
                    "<select id=\"drpSelectSharedOwner\" class=\"form-control select\" multiple>\r\n" +
                    "   <option value=\"\"></option>\r\n";
                for (int iIndex = 0; iIndex < Owners.Length; iIndex++)
                {
                     SelectSharedScopeText += "    <option value=\"" + Owners[iIndex].OwnerId + "\">" + Owners[iIndex].OwnerName + "</option>\r\n";
                }
                SelectSharedScopeText +=
                    "</select>\r\n";


                OwnerUserFilterCls
                   OwnerUserFilter = new OwnerUserFilterCls();
                OwnerUserFilter.OwnerId = OwnerId;
                OwnerUserCls[]
                    OwnerUsers = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Reading(ORenderInfo, OwnerUserFilter);

                string SelectSharedScopeOwnerText =
                    " <select id=\"drpSelectSharedOwnerUser\" class=\"form-control select\" multiple>\r\n" +
                    "   <option value=\"\"></option>\r\n";
                for (int iIndex = 0; iIndex < OwnerUsers.Length; iIndex++)
                {
                    SelectSharedScopeOwnerText += "    <option value=\"" + OwnerUsers[iIndex].OwnerUserId + "\">" + OwnerUsers[iIndex].LoginName + "</option>\r\n";
                }
                SelectSharedScopeOwnerText +=
                    "</select>\r\n";

                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới sự vụ") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-12\">\r\n"+
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã sự vụ") + "</label> " + SelectCustomerText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ") + "</label> <input id=\"txtCaseProfileName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả sự vụ") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả sự vụ") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ") + "</label> <input id=\"txtOpenDate\" value=\""+System.DateTime.Now.ToString("dd/MM/yyyy")+"\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ") + "\" class=\"form-control CssDate\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chia sẻ làm việc") + "</label> " + SelectSharedScopeText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhân viên quản lý") + "</label> " + SelectSharedScopeOwnerText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái sự vụ") + "</label> "+SelectActiveText+"</div> \r\n" +
                       "                </div>\r\n"+
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
        public static AjaxOut ServerSideCreateAddCaseProfileObject(
            RenderInfoCls ORenderInfo, 
            string CaseProfileId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                

                SaveCaseProfileCls OSaveAddCaseProfile = new SaveCaseProfileCls();
                if (string.IsNullOrEmpty(CaseProfileId))
                {
                    OSaveAddCaseProfile.CaseProfileId = System.Guid.NewGuid().ToString();
                }
                else
                {
                    OSaveAddCaseProfile.CaseProfileId = CaseProfileId;
                }
                RetAjaxOut.RetObject = OSaveAddCaseProfile;
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
        public static AjaxOut ServerSideCreateCaseProfileObject(
            RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);


                CaseProfileCls
                    OCaseProfile = new CaseProfileCls();
                OCaseProfile.CaseProfileId = System.Guid.NewGuid().ToString();

                RetAjaxOut.RetObject = OCaseProfile;
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
            SaveCaseProfileCls OSaveAddCaseProfile)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                //if (string.IsNullOrEmpty(OSaveAddCaseProfile.CaseProfileCode)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã sự vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddCaseProfile.CaseProfileName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddCaseProfile.OpenDate))
                {
                    throw new Exception("Ngày mở sự vụ chưa xác định");
                }
                if (FunctionUtility.checkVnDate(OSaveAddCaseProfile.OpenDate)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddCaseProfile.CustomerId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Chưa chọn khách hàng"));
                }
                
                    

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;

                string[] ItemSharedOwnerIds = FunctionUtility.GetMultiComboboxValue(OSaveAddCaseProfile.SharedOwnerIds);
                string[] ItemSharedOwnerUserIds = FunctionUtility.GetMultiComboboxValue(OSaveAddCaseProfile.SharedOwnerUserIds);
          
                CaseProfileCls 
                    OAddCaseProfile = new CaseProfileCls();
                OAddCaseProfile.CaseProfileId = OSaveAddCaseProfile.CaseProfileId;
                OAddCaseProfile.frkOwnerId = OwnerId;
                OAddCaseProfile.CaseProfileCode = OSaveAddCaseProfile.CaseProfileCode;
                OAddCaseProfile.CaseProfileName = OSaveAddCaseProfile.CaseProfileName;
                OAddCaseProfile.OpenDate = FunctionUtility.VNDateToDate(OSaveAddCaseProfile.OpenDate);
                OAddCaseProfile.EntryDate = System.DateTime.Now;
                OAddCaseProfile.OpenByUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OAddCaseProfile.frkCustomerId=OSaveAddCaseProfile.CustomerId;
                OAddCaseProfile.Active = OSaveAddCaseProfile.Active;
                OAddCaseProfile.Description = OSaveAddCaseProfile.Description;


                OAddCaseProfile.SharedOwnerIds = ItemSharedOwnerIds;
                OAddCaseProfile.SharedOwnerUserIds = ItemSharedOwnerUserIds;

                CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().Add(ORenderInfo, OAddCaseProfile);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new ManageCaseProfile().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("CaseProfileId",OAddCaseProfile.CaseProfileId)
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
    }
}
