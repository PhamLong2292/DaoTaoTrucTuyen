using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.ChatUtility;
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

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new CaseProfile().WebPartId };
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Quản lý sự vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Quản lý sự vụ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ManageCaseProfile),Page);
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ChatCommentUtility), Page); 
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
                string CaseProfileId = (string)WebEnvironments.Request("CaseProfileId");
                
                    
                CaseProfileCls
                     OCaseProfile = CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(ORenderInfo, CaseProfileId);
                if (OCaseProfile == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "sự vụ đã bị xóa hoặc không tìm thấy"));
                }

                string[] OwnerUserIds = CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().ReadingSharedOwnerUsers(ORenderInfo, CaseProfileId);

                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CaseProfile().WebPartId);
                RetAjaxOut.HtmlContent =
                    ChatCommentUtility.GetPlugInJavascript(ORenderInfo,OCaseProfile.CaseProfileCode+":"+ OCaseProfile.CaseProfileName, OwnerUserIds)+
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
                    "       Description = tinyMCE.get('txtDescription').getContent();\r\n" +
                    "       OpenDate = document.getElementById('txtOpenDate').value;\r\n" +
                    "       SharedOwnerIds=$('#drpSelectSharedOwner').val();\r\n"+
                    "       SharedOwnerUserIds=$('#drpSelectSharedOwnerUser').val();\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       OSaveCaseProfile = OneTSQ.WebParts.ManageCaseProfile.ServerSideCreateCaseProfileObject(RenderInfo, CaseProfileId).value.RetObject;\r\n" +
                    "       OSaveCaseProfile.CaseProfileCode = CaseProfileCode;\r\n" +
                    "       OSaveCaseProfile.CaseProfileName  = CaseProfileName;\r\n" +
                    "       OSaveCaseProfile.Description  = Description;\r\n" +
                    "       OSaveCaseProfile.OpenDate = OpenDate;\r\n" +
                    "       OSaveCaseProfile.Active = Active;\r\n"+
                    "       OSaveCaseProfile.SharedOwnerIds=SharedOwnerIds;\r\n" +
                    "       OSaveCaseProfile.SharedOwnerUserIds=SharedOwnerUserIds;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.ManageCaseProfile.ServerSideUpdate(RenderInfo, CaseProfileId, OSaveCaseProfile).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    //"       CallBack();\r\n" +
                    "   }\r\n" +


                    "</script>\r\n"+
                    ServerSideDrawUpdateForm(ORenderInfo, OCaseProfile).HtmlContent+
                    " <script>\r\n" +
                    "       $('.CssDate').datepicker({\r\n" +
                    "           format: 'dd/mm/yyyy'\r\n" +
                    "       });\r\n" +
                    WebScreen.GetMceEditor("txtDescription",300)+
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
                WebSession.CheckSessionTimeOut(ORenderInfo);

                
                    


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



                string OwnerId = OCaseProfile.frkOwnerId;
                if (OCaseProfile.SharedOwnerIds.Length == 0)
                {
                    OCaseProfile.SharedOwnerIds = new string[] { OwnerId };
                }
                
                OwnerFilterCls
                    OwnerFilter = new OwnerFilterCls();
                OwnerFilter.ActiveOnly = 1;
                //OwnerFilter.ExcOwnerId = OwnerId;

                OwnerCls[]
                    Owners = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().Reading(ORenderInfo, OwnerFilter);

                string OwnerText = OwnerId;
                string SelectSharedScopeText = 
                    "<select id=\"drpSelectSharedOwner\" class=\"form-control select\" multiple>\r\n";
                for (int iIndex = 0; iIndex < Owners.Length; iIndex++)
                {
                    bool HasChecked = false;
                    for (int iCheck = 0; iCheck < OCaseProfile.SharedOwnerIds.Length; iCheck++)
                    {
                        if (OCaseProfile.SharedOwnerIds[iCheck].Equals(Owners[iIndex].OwnerId))
                        {
                            if (!string.IsNullOrEmpty(OwnerText)) OwnerText += ";";
                            OwnerText = OwnerText + Owners[iIndex].OwnerId;
                            HasChecked = true;
                            break;
                        }
                    }
                    if (HasChecked)
                    {
                        SelectSharedScopeText += "    <option selected value=\"" + Owners[iIndex].OwnerId + "\">" + Owners[iIndex].OwnerName + "</option>\r\n";
                    }
                    else
                    {
                        SelectSharedScopeText += "    <option value=\"" + Owners[iIndex].OwnerId + "\">" + Owners[iIndex].OwnerName + "</option>\r\n";
                    }
                }
                SelectSharedScopeText+=
                    "</select>\r\n";




                OwnerUserFilterCls
                    OwnerUserFilter = new OwnerUserFilterCls();
                OwnerUserFilter.OwnerId = OwnerText;
                OwnerUserFilter.ActiveOnly = 1;
                OwnerUserFilter.EnabledScopeOwner = 1;
                OwnerUserCls[]
                    OwnerUsers = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Reading(ORenderInfo, OwnerUserFilter);

                string SelectSharedScopeOwnerText =
                    "<select id=\"drpSelectSharedOwnerUser\" class=\"form-control select\" multiple>\r\n";
                for (int iIndex = 0; iIndex < OwnerUsers.Length; iIndex++)
                {
                    bool HasChecked = false;
                    for (int iCheck = 0; iCheck < OCaseProfile.SharedOwnerUserIds.Length; iCheck++)
                    {
                        if (OCaseProfile.SharedOwnerUserIds[iCheck].Equals(OwnerUsers[iIndex].OwnerUserId))
                        {
                            HasChecked = true;
                            break;
                        }
                    }
                    if (HasChecked)
                    {
                        SelectSharedScopeOwnerText += "    <option selected value=\"" + OwnerUsers[iIndex].OwnerUserId + "\">" + OwnerUsers[iIndex].LoginName + "</option>\r\n";
                    }
                    else
                    {
                        SelectSharedScopeOwnerText += "    <option value=\"" + OwnerUsers[iIndex].OwnerUserId + "\">" + OwnerUsers[iIndex].LoginName + "</option>\r\n";
                    }
                }
                SelectSharedScopeOwnerText +=
                    "</select>\r\n";


                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string group = (string)WebEnvironments.Request("group");
                if (string.IsNullOrEmpty(group)) group = "profile";

                RefItemCls[]
                    RefItems = new RefItemCls[]
                    {
                        new RefItemCls("profile",WebLanguage.GetLanguage(OSiteParam,"Thông tin"),"",0,"fa fa-folder-open menu-icon",1),
                        new RefItemCls("chat",WebLanguage.GetLanguage(OSiteParam,"Thảo luận"),"",0,"fa fa-wechat",1),
                        new RefItemCls("email",WebLanguage.GetLanguage(OSiteParam,"Email"),"",0,"fa fa-envelope-square menu-icon",1),

                        new RefItemCls("estimates",WebLanguage.GetLanguage(OSiteParam,"Ước lượng"),"",0,"fa fa-book menu-icon",0),
                        new RefItemCls("proposals",WebLanguage.GetLanguage(OSiteParam,"Chào giá"),"",0,"fa fa-external-link menu-icon",0),
                        

                        new RefItemCls("invoices",WebLanguage.GetLanguage(OSiteParam,"Hóa đơn"),"",0,"fa fa-money menu-icon",0),
                        new RefItemCls("payments",WebLanguage.GetLanguage(OSiteParam,"Thanh toán"),"",0,"fa  fa-line-chart menu-icon",0),
                        
                        new RefItemCls("expenses",WebLanguage.GetLanguage(OSiteParam,"Chi phí"),"",0,"fa fa-heartbeat menu-icon",0),
                        new RefItemCls("contracts",WebLanguage.GetLanguage(OSiteParam,"Hợp đồng"),"",0,"fa fa-file menu-icon",0),
                        new RefItemCls("projects",WebLanguage.GetLanguage(OSiteParam,"Dự án"),"",0,"fa fa-file-powerpoint-o",0),
                        new RefItemCls("tickets",WebLanguage.GetLanguage(OSiteParam,"Ticket"),"",0,"fa  fa-ticket  menu-icon",0),
                        new RefItemCls("tasks",WebLanguage.GetLanguage(OSiteParam,"Công việc"),"",0,"fa fa-tasks menu-icon",0),
                        new RefItemCls("attachments",WebLanguage.GetLanguage(OSiteParam,"Tài liệu"),"",0,"fa fa-paperclip menu-icon",0),
                        new RefItemCls("reminders",WebLanguage.GetLanguage(OSiteParam,"Nhắc việc"),"",0,"fa fa-clock-o menu-icon",0),
                        new RefItemCls("notes",WebLanguage.GetLanguage(OSiteParam,"Ghi chú"),"",0,"fa fa-file-text-o menu-icon",0),
                    };

                string Html =
                   " <div class=\"ibox-content\"> \r\n" +
                   "     <div class=\"row\"> \r\n" +
                   "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" +WebLanguage.GetLanguage(OSiteParam,"Hồ sơ sự vụ")+" / "+ OCaseProfile.CaseProfileCode+" - "+OCaseProfile.CaseProfileName + "</h3> \r\n" +

                    "            <div class=\"row\">\r\n" +
                    "               <div class=\"col-md-2\">\r\n" +
                    "               <div class=\"tabs-left\">\r\n" +
                    "               <ul class=\"nav\">\r\n";

                for (int iIndex = 0; iIndex < RefItems.Length; iIndex++)
                {
                    if (RefItems[iIndex].Use == 0)
                    {
                        Html +=
                               "               <li class=\"\">\r\n" +
                               "                    <a style=\"padding:6px;color:silver;font-weight:bold\" data-group=\"" + RefItems[iIndex].Code + "\" href=\"javascript:alert('Under construction');\" ><i class=\"" + RefItems[iIndex].Class + "\" aria-hidden=\"true\"></i>" + RefItems[iIndex].Subject + "</a>\r\n" +
                               "               </li>\r\n";
                    }
                    else
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
                }

                Html+=
                    "           </ul>\r\n" +
                    "           </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"col-md-10\">\r\n";
                bool HasProcess = false;
                if (group.Equals("profile"))
                {
                    HasProcess = true;
                    Html +=
                        "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thuộc khách hàng") + "</label>: <span style=\"color:green;font-weight:bold\">" + OCaseProfile.Company + "</span></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã sự vụ") + "</label> <input value=\"" + OCaseProfile.CaseProfileCode + "\" id=\"txtCaseProfileCode\" READONLY type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã sự vụ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ") + "</label> <input value=\"" + OCaseProfile.CaseProfileName + "\"  id=\"txtCaseProfileName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả sự vụ") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả sự vụ") + "\" class=\"form-control\">" + OCaseProfile.Description + "</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ") + "</label> <input value=\"" + OCaseProfile.OpenDate.ToString("dd/MM/yyyy") + "\" id=\"txtOpenDate\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ") + "\" class=\"form-control CssDate\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chia sẻ làm việc") + "</label> "+SelectSharedScopeText+"</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhân viên quản lý") + "</label> " + SelectSharedScopeOwnerText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái sự vụ") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OCaseProfile.CaseProfileId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                       "                 </div> \r\n";
                }
                if (group.Equals("chat"))
                {
                    HasProcess = true;
                    Html +=
                        ChatCommentUtility.ServerSideDrawChat(ORenderInfo,"case.profile", CaseProfileId).HtmlContent +
                        "<script>\r\n" +
                            WebScreen.GetMceEditor("txtChatContent",200)+
                        "</script>\r\n";
                }

                if(!HasProcess)
                {
                    ReferenceObjectFilterCls
                      OReferenceObjectFilter = new ReferenceObjectFilterCls();
                    OReferenceObjectFilter.SrcObjectId = CaseProfileId;
                    OReferenceObjectFilter.RefecenceObjectType = group;
                    ReferenceObjectCls[]
                        ReferenceObjects = CallBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Reading(ORenderInfo, OReferenceObjectFilter);
                    Html +=
                        "    <h3>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách - ") + WebLanguage.GetLanguage(OSiteParam, group) + "</h3>\r\n" +
                        "    <hr />\r\n" +
                       "         <div class=\"table-responsive\"> \r\n" +
                       "             <table class=\"table table-striped\"> \r\n" +
                       "                 <thead> \r\n" +
                       "                 <tr> \r\n" +
                       "                     <th class=\"th-func-20\"></th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + " </th> \r\n" +
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
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa khỏi sự vụ") + "\" href=\"javascript:CallActionDeleteReferenceObject('" + ReferenceObjects[iIndex].ReferenceObjectId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }

                Html +=
                   "                </div>\r\n" +
                   "                </div>\r\n" +
                    "           </div>\r\n" +
                   "         </div> \r\n" +
                   "     </div> \r\n" +
                   " </div> \r\n" +
                   " <script>\r\n" +
                   "    $('#drpSelectSharedOwner').select2();\r\n"+
                   "    $('#drpSelectSharedOwnerUser').select2();\r\n" +
                   " </script>\r\n";

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
                WebSession.CheckSessionTimeOut(ORenderInfo);

                
                    
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
                WebSession.CheckSessionTimeOut(ORenderInfo);

                CaseProfileCls
                    OCaseProfile = CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(ORenderInfo, CaseProfileId);

                if (!WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId.Equals(OCaseProfile.frkOwnerId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không có quyền cập nhật hồ sơ"));
                }
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (string.IsNullOrEmpty(OSaveCaseProfile.CaseProfileCode)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã sự vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveCaseProfile.CaseProfileName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên sự vụ không hợp lệ"));
                if (FunctionUtility.checkVnDate(OSaveCaseProfile.OpenDate) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày mở sự vụ không hợp lệ"));


                string[] ItemSharedOwnerIds = FunctionUtility.GetMultiComboboxValue(OSaveCaseProfile.SharedOwnerIds);
                string[] ItemSharedOwnerUserIds = FunctionUtility.GetMultiComboboxValue(OSaveCaseProfile.SharedOwnerUserIds);
                    
                
                OCaseProfile.CaseProfileCode = OSaveCaseProfile.CaseProfileCode;
                OCaseProfile.CaseProfileName = OSaveCaseProfile.CaseProfileName;
                OCaseProfile.OpenDate = FunctionUtility.VNDateToDate(OSaveCaseProfile.OpenDate);
                OCaseProfile.SharedOwnerIds = ItemSharedOwnerIds;
                OCaseProfile.SharedOwnerUserIds = ItemSharedOwnerUserIds;
                OCaseProfile.Description = OSaveCaseProfile.Description;

                CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().Save(ORenderInfo, CaseProfileId, OCaseProfile);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật hồ sơ thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, "managecaseprofile", new WebParamCls[]
                {
                    new WebParamCls("caseprofileid",CaseProfileId)
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
            RenderInfoCls ORenderInfo,
            string CaseProfileId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                
                    
                CaseProfileCls
                    OCaseProfile = CallBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(ORenderInfo, CaseProfileId);

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
