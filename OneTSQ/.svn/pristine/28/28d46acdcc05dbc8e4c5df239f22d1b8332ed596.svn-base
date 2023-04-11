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
                return "Thêm mới hồ sơ";
            }
        }

        public override string Description
        {
            get
            {
                return "Thêm mới hồ sơ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AddCaseProfile),Page);
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
                string CaseProfileTypeId = (string)WebEnvironments.Request("CaseProfileTypeId");
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new CaseProfile().WebPartId, new WebParamCls[]
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

                    
                    "       CaseProfileName = document.getElementById('txtCaseProfileName').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       OpenDate = document.getElementById('txtOpenDate').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveCaseProfile = OnlineTour.WebParts.AddCaseProfile.ServerSideCreateCaseProfileObject(RenderInfo, null).value.RetObject;\r\n" +
                    "       OSaveCaseProfile.CaseProfileName  = CaseProfileName;\r\n" +
                    "       OSaveCaseProfile.Description  = Description;\r\n" +
                    "       OSaveCaseProfile.OpenDate = OpenDate;\r\n" +
                    "       OSaveCaseProfile.Active = Active;\r\n" +

                    "       AjaxOut = OnlineTour.WebParts.AddCaseProfile.ServerSideAdd(RenderInfo, OSaveCaseProfile).value;\r\n" +
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";

                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
             
              
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới hồ sơ") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-12\">\r\n"+
                       //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ") + "</label> <input id=\"txtCaseProfileCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ") + "</label> <input id=\"txtCaseProfileName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả hồ sơ") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả hồ sơ") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ") + "</label> <input id=\"txtOpenDate\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ") + "\" class=\"form-control CssDate\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái hồ sơ") + "</label> "+SelectActiveText+"</div> \r\n" +
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    ActionSqlParam=new ActionSqlParamCls();
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
        public static AjaxOut ServerSideAdd(
            RenderInfoCls ORenderInfo,
            SaveCaseProfileCls OSaveAddCaseProfile)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                //if (string.IsNullOrEmpty(OSaveAddCaseProfile.CaseProfileCode)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddCaseProfile.CaseProfileName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveAddCaseProfile.OpenDate))
                {
                    throw new Exception("Ngày mở hồ sơ chưa xác định");
                }
                if (FunctionUtilities.checkVnDate(OSaveAddCaseProfile.OpenDate)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ không hợp lệ"));
                
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
        
                CaseProfileCls 
                    OAddCaseProfile = new CaseProfileCls();
                OAddCaseProfile.CaseProfileId = OSaveAddCaseProfile.CaseProfileId;
                OAddCaseProfile.frkOwnerId = OwnerId;
                OAddCaseProfile.CaseProfileCode = OSaveAddCaseProfile.CaseProfileCode;
                OAddCaseProfile.CaseProfileName = OSaveAddCaseProfile.CaseProfileName;
                OAddCaseProfile.OpenDate = FunctionUtilities.VNDateToDate(OSaveAddCaseProfile.OpenDate);
                OAddCaseProfile.EntryDate = System.DateTime.Now;
                OAddCaseProfile.OpenByUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OAddCaseProfile.Active = OSaveAddCaseProfile.Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().Add(OActionSqlParam, OAddCaseProfile);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, new ManageCaseProfile().WebPartId, new WebParamCls[]
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


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCreateCaseProfileObject(
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

    }
}
