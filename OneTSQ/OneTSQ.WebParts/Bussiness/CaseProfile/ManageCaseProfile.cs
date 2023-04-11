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
    public class ManageCaseProfile : WebPartTemplate
    {
        public static string StaticWebPartId
        {
            get
            {
                return "ManageCaseProfile";
            }
        }

        public override string WebPartId
        {
            get
            {
                return StaticWebPartId;
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Quản lý hồ sơ";
            }
        }

        public override string Description
        {
            get
            {
                return "Quản lý hồ sơ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ManageCaseProfile),Page);
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
                string CaseProfileId = (string)WebEnvironments.Request("CaseProfileId");
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                CaseProfileCls
                 OCaseProfile = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(OActionSqlParam, CaseProfileId);
                if (OCaseProfile == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "hồ sơ đã bị xóa hoặc không tìm thấy"));
                }

                string BackUrl = WebScreen.BuildUrl(OwnerCode, new CaseProfile().WebPartId);
                RetAjaxOut.HtmlContent =
                    "<script>\r\n"+
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('"+BackUrl+"','_self');\r\n"+
                    "   }\r\n" +



                    "   function CallActionUpdate(CaseProfileId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       CaseProfileCode = document.getElementById('txtCaseProfileCode').value;\r\n" +
                    "       CaseProfileName = document.getElementById('txtCaseProfileName').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       OpenDate = document.getElementById('txtOpenDate').value;\r\n" +

                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveCaseProfile = OnlineTour.WebParts.ManageCaseProfile.ServerSideCreateCaseProfileObject(RenderInfo, CaseProfileId).value.RetObject;\r\n" +
                    "       OSaveCaseProfile.CaseProfileCode = CaseProfileCode;\r\n" +
                    "       OSaveCaseProfile.CaseProfileName  = CaseProfileName;\r\n" +
                    "       OSaveCaseProfile.Description  = Description;\r\n" +
                    "       OSaveCaseProfile.OpenDate = OpenDate;\r\n" +
                    "       OSaveCaseProfile.Active = Active;\r\n"+


                    "       AjaxOut = OnlineTour.WebParts.ManageCaseProfile.ServerSideUpdate(RenderInfo, CaseProfileId, OSaveCaseProfile).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallBack();\r\n" +
                    "   }\r\n" +


                    "</script>\r\n"+
                    ServerSideDrawUpdateForm(ORenderInfo, OCaseProfile).HtmlContent+
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, CaseProfileCls OCaseProfile)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                string CaseProfileId = OCaseProfile.CaseProfileId;
                string SelectActiveText =
                     "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                     "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Đã đóng") + "</option>\r\n" +
                     "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang mở") + "</option>\r\n" +
                     "</select>\r\n";

                if (OCaseProfile.Active == 1)
                {
                    SelectActiveText =
                         "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                         "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Đã đóng") + "</option>\r\n" +
                         "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang mở") + "</option>\r\n" +
                         "</select>\r\n";
                }

                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string group = (string)WebEnvironments.Request("group");
                if (string.IsNullOrEmpty(group)) group = "profile";

                RefItemCls[]
                    RefItems = new RefItemCls[]
                    {
                        new RefItemCls("profile",WebLanguage.GetLanguage(OSiteParam,"Hồ sơ"),"",0,"fa fa-folder-open menu-icon"),
                        new RefItemCls("email",WebLanguage.GetLanguage(OSiteParam,"Email"),"",0,"fa fa-envelope-square menu-icon"),
                        new RefItemCls("invoices",WebLanguage.GetLanguage(OSiteParam,"Hóa đơn"),"",0,"fa fa-money menu-icon"),
                        new RefItemCls("payments",WebLanguage.GetLanguage(OSiteParam,"Thanh toán"),"",0,"fa  fa-line-chart menu-icon"),
                        new RefItemCls("proposals",WebLanguage.GetLanguage(OSiteParam,"Chào giá"),"",0,"fa fa-external-link menu-icon"),
                        new RefItemCls("estimates",WebLanguage.GetLanguage(OSiteParam,"Ước lượng"),"",0,"fa fa-book menu-icon"),
                        new RefItemCls("expenses",WebLanguage.GetLanguage(OSiteParam,"Chi phí"),"",0,"fa fa-heartbeat menu-icon"),
                        new RefItemCls("contracts",WebLanguage.GetLanguage(OSiteParam,"Hợp đồng"),"",0,"fa fa-file menu-icon"),
                        new RefItemCls("projects",WebLanguage.GetLanguage(OSiteParam,"Dự án"),"",0,"fa fa-file-powerpoint-o"),
                        new RefItemCls("tickets",WebLanguage.GetLanguage(OSiteParam,"Ticket"),"",0,"fa  fa-ticket  menu-icon"),
                        new RefItemCls("tasks",WebLanguage.GetLanguage(OSiteParam,"Công việc"),"",0,"fa fa-tasks menu-icon"),
                        new RefItemCls("attachments",WebLanguage.GetLanguage(OSiteParam,"Tài liệu"),"",0,"fa fa-paperclip menu-icon"),
                        new RefItemCls("reminders",WebLanguage.GetLanguage(OSiteParam,"Nhắc việc"),"",0,"fa fa-clock-o menu-icon"),
                        new RefItemCls("notes",WebLanguage.GetLanguage(OSiteParam,"Ghi chú"),"",0,"fa fa-file-text-o menu-icon"),
                    };

                string Html =
                   " <div class=\"ibox-content\"> \r\n" +
                   "     <div class=\"row\"> \r\n" +
                   "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Quản lý hồ sơ") + "</h3> \r\n" +

                    "            <div class=\"row\">\r\n" +
                    "               <div class=\"col-md-2\">\r\n" +
                    "               <div class=\"tabs-left\">\r\n" +
                    "               <ul class=\"nav\">\r\n";

                for (int iIndex = 0; iIndex < RefItems.Length; iIndex++)
                {
                    if (RefItems[iIndex].Code.Equals(group))
                    {
                        Html +=
                            "               <li style=\"background-color:silver\" class=\"\">\r\n" +
                            "                    <a style=\"padding:6px;color:maroon;font-weight:bold\" data-group=\"" + RefItems[iIndex].Code + "\" href=\"" + WebScreen.BuildUrl(OSiteParam, OwnerCode, StaticWebPartId, new WebParamCls[] { new WebParamCls("CaseProfileId", CaseProfileId), new WebParamCls("group", RefItems[iIndex].Code) }) + "\" ><i class=\"" + RefItems[iIndex].Class + "\" aria-hidden=\"true\"></i>" + RefItems[iIndex].Subject + "</a>\r\n" +
                            "               </li>\r\n";
                    }
                    else
                    {
                        Html +=
                            "               <li class=\"\">\r\n" +
                            "                    <a style=\"padding:6px;color:green;font-weight:bold\" data-group=\"" + RefItems[iIndex].Code + "\" href=\"" + WebScreen.BuildUrl(OSiteParam, OwnerCode, StaticWebPartId, new WebParamCls[] { new WebParamCls("CaseProfileId", CaseProfileId), new WebParamCls("group", RefItems[iIndex].Code) }) + "\" ><i class=\"" + RefItems[iIndex].Class + "\" aria-hidden=\"true\"></i>" + RefItems[iIndex].Subject + "</a>\r\n" +
                            "               </li>\r\n";
                    }
                }

                Html+=
                    "           </ul>\r\n" +
                    "           </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"col-md-10\">\r\n";

                if (group.Equals("profile"))
                {
                    Html +=
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ") + "</label> <input value=\"" + OCaseProfile.CaseProfileCode + "\" id=\"txtCaseProfileCode\" READONLY type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ") + "</label> <input value=\"" + OCaseProfile.CaseProfileName + "\"  id=\"txtCaseProfileName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả hồ sơ") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả hồ sơ") + "\" class=\"form-control\">" + OCaseProfile.Description + "</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ") + "</label> <input value=\"" + OCaseProfile.OpenDate.ToString("dd/MM/yyyy") + "\" id=\"txtOpenDate\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ") + "\" class=\"form-control CssDate\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái hồ sơ") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OCaseProfile.CaseProfileId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n";
                }
                else
                {
                    ReferenceObjectFilterCls
                      OReferenceObjectFilter = new ReferenceObjectFilterCls();
                    OReferenceObjectFilter.SrcObjectId = CaseProfileId;
                    OReferenceObjectFilter.RefecenceObjectType = group;
                    ReferenceObjectCls[]
                        ReferenceObjects = OnlineTourBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Reading(ActionSqlParam, OReferenceObjectFilter);
                    Html +=
                        "    <h3>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách - ") + WebLanguage.GetLanguage(OSiteParam, group) + "</h3>\r\n" +
                        "    <hr />\r\n" +
                       "         <div class=\"table-responsive\"> \r\n" +
                       "             <table class=\"table table-striped\"> \r\n" +
                       "                 <thead> \r\n" +
                       "                 <tr> \r\n" +
                       "                     <th class=\"th-func-20\"></th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Hồ sơ") + " </th> \r\n" +
                       "                     <th class=\"th-func-20\"></th> \r\n" +
                       "                 </tr> \r\n" +
                       "                 </thead> \r\n" +
                       "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < ReferenceObjects.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + ReferenceObjects[iIndex].ReferenceObjectId + "\"> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + ReferenceObjects[iIndex].LinkObjectCode + "</td> \r\n" +
                            "                     <td>" + ReferenceObjects[iIndex].LinkObjectSubject + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa khỏi hồ sơ") + "\" href=\"javascript:CallActionDeleteReferenceObject('" + ReferenceObjects[iIndex].ReferenceObjectId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }

                Html+=
                   "                </div>\r\n"+
                   "                </div>\r\n" +
                    "           </div>\r\n"+
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
        public static AjaxOut ServerSideCreateManageCaseProfileObject(
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
                SaveCaseProfileCls OSaveManageCaseProfile = new SaveCaseProfileCls();
                if (string.IsNullOrEmpty(CaseProfileId))
                {
                    OSaveManageCaseProfile.CaseProfileId = System.Guid.NewGuid().ToString();
                }
                else
                {
                    OSaveManageCaseProfile.CaseProfileId = CaseProfileId;
                }
                RetAjaxOut.RetObject = OSaveManageCaseProfile;
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
            string CaseProfileId,
            SaveCaseProfileCls OSaveCaseProfile)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (string.IsNullOrEmpty(OSaveCaseProfile.CaseProfileCode)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã hồ sơ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveCaseProfile.CaseProfileName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên hồ sơ không hợp lệ"));
                if (FunctionUtilities.checkVnDate(OSaveCaseProfile.OpenDate) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày mở hồ sơ không hợp lệ"));
                

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                CaseProfileCls
                    OCaseProfile = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(OActionSqlParam, CaseProfileId);

                OCaseProfile.CaseProfileCode = OSaveCaseProfile.CaseProfileCode;
                OCaseProfile.CaseProfileName = OSaveCaseProfile.CaseProfileName;
                OCaseProfile.OpenDate = FunctionUtilities.VNDateToDate(OSaveCaseProfile.OpenDate);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().Save(OActionSqlParam, CaseProfileId, OCaseProfile);
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
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                CaseProfileCls
                    OCaseProfile = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(OActionSqlParam, CaseProfileId);

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


        public override bool RunAlone
        {
            get
            {
                return false;
            }
        }            
    }
}
