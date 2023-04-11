using OnlineTour.Bussiness.Utility;
using OnlineTour.Model;
using OnlineTour.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineTour.WebParts
{
    public class ViewEmailFollowUp : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "view.email.followup";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xem followup email";
            }
        }

        public override string Description
        {
            get
            {
                return "Xem followup email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ViewEmailFollowUp), Page);
        }

        public override AjaxOut CheckPermission(SiteParam OSiteParam)
        {
            string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
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
                //AjaxOut RetAjaxOutCheckPermission = CheckPermission(OSiteParam);
                //if (RetAjaxOutCheckPermission.RetBoolean == false)
                //{
                //    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này") + "<br>" + DictionaryPermission.StaticPermissionFunctionCode + ".Access", false);
                //    return RetAjaxOut;
                //}

                string EmailFollowUpId = (string)WebEnvironments.Request("EmailFollowUpId");
                string NotifyId = (string)WebEnvironments.Request("NotifyId");

                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +

                    "   function CloseNotify(NotifyId, EmailFollowUpId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = Office.Application.Gui.WebParts.ViewEmailFollowUp.ServerSideCloseNotify(RenderInfo, NotifyId, EmailFollowUpId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       alert(AjaxOut.InfoMessage);\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallApproved(EmailFollowUpId, NotifyId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = Office.Application.Gui.WebParts.ViewEmailFollowUp.ServerSideApproved(RenderInfo, EmailFollowUpId, NotifyId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       alert(AjaxOut.InfoMessage);\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallReject(EmailFollowUpId, NotifyId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = Office.Application.Gui.WebParts.ViewEmailFollowUp.ServerSideApproved(RenderInfo, EmailFollowUpId, NotifyId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       alert(AjaxOut.InfoMessage);\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    ServerSideDrawViewForm(ORenderInfo, EmailFollowUpId, NotifyId).HtmlContent;

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
        public static AjaxOut ServerSideDrawViewForm(
            RenderInfoCls ORenderInfo,
            string EmailFollowUpId,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailFollowUpCls
                    OEmailFollowUp = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateEmailFollowUpModel(OActionSqlParam, EmailFollowUpId);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                if (OEmailFollowUp == null)
                {
                    RetAjaxOut.HtmlContent = WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy dữ liệu theo dõi. Có thể đã bị xóa");
                    return RetAjaxOut;
                }
                string FollowUpStatus = WebLanguage.GetLanguage(OSiteParam, "Chưa duyệt");
                if (OEmailFollowUp.Approved == 1)
                {
                    FollowUpStatus = WebLanguage.GetLanguage(OSiteParam, "Đã duyệt");
                }
                if (OEmailFollowUp.Approved == 2)
                {
                    FollowUpStatus = WebLanguage.GetLanguage(OSiteParam, "Không duyệt");
                }

                string BackUrl = WebScreen.BuildUrl(OwnerCode, ViewEmail.StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Pop3EmailUserId",OEmailFollowUp.frkPop3EmailUserId),
                        new WebParamCls("EmailId",OEmailFollowUp.frkEmailId),
                    });
                string ApprovedInfo = "";
                if (OEmailFollowUp.Approved != 0)
                {
                    ApprovedInfo = OEmailFollowUp.ApprovedByUserLoginName + WebLanguage.GetLanguage(OSiteParam, " lúc ") + OEmailFollowUp.ApprovedDate.ToString("dd/MM/yyyy HH:mm");
                }
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thông tin") + "</h3> \r\n" +
                    "             <div> \r\n" +


                "                 <div style=\"margin-bottom:10px\"> \r\n";

                if (!string.IsNullOrEmpty(NotifyId))
                {
                    Html +=
                        "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CloseNotify('" + NotifyId + "','" + EmailFollowUpId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng thông báo") + "</strong></button> \r\n";
                }
                if (OEmailFollowUp.Approved == 0)// && OEmailFollowUp.IsTask==1
                {
                    EmailCls
                        OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, OEmailFollowUp.frkPop3EmailUserId, OEmailFollowUp.frkEmailId);
                    if (OEmail.CreatedOwnerUserId.Equals(OwnerUserId))
                    {
                        Html +=
                            "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallApproved('" + OEmailFollowUp.EmailFollowUpId + "', '" + NotifyId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "</strong></button> \r\n" +
                            "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallReject('" + OEmailFollowUp.EmailFollowUpId + "','" + NotifyId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Không duyệt") + "</strong></button> \r\n";
                    }
                }
                Html +=
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xem email") + "</strong></button> \r\n" +
                    "                 </div> \r\n" +

                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Người tạo") + "</label> <input  type=\"textbox\" READONLY class=\"form-control\" value=\"" + OEmailFollowUp.CreateByUserLoginName + "\"></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày tạo") + "</label> <input  type=\"textbox\" READONLY class=\"form-control\" value=\"" + OEmailFollowUp.CommentDate.ToString("dd/MM/yyyy HH:mm") + "\"></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input  type=\"textbox\" READONLY class=\"form-control\" value=\"" + OEmailFollowUp.FollowUpSubject + "\"></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + "</label> <textarea style=\"height:200px\" READONLY class=\"form-control\">" + OEmailFollowUp.FollowUpComment + "</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> <input  type=\"textbox\" READONLY class=\"form-control\" value=\"" + FollowUpStatus + "\"></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin duyệt") + "</label> <input  type=\"textbox\" READONLY class=\"form-control\" value=\"" + ApprovedInfo + "\"></div> \r\n" +
                    "                 <div> \r\n";

                if (!string.IsNullOrEmpty(NotifyId))
                {
                    Html +=
                        "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CloseNotify('" + NotifyId + "','" + EmailFollowUpId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng thông báo") + "</strong></button> \r\n";
                }
                if (OEmailFollowUp.Approved == 0)// && OEmailFollowUp.IsTask==1
                {
                    EmailCls
                        OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, OEmailFollowUp.frkPop3EmailUserId, OEmailFollowUp.frkEmailId);
                    if (OEmail.CreatedOwnerUserId.Equals(OwnerUserId))
                    {
                        Html +=
                            "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallApproved('" + OEmailFollowUp.EmailFollowUpId + "','" + NotifyId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "</strong></button> \r\n" +
                            "   <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallReject('" + OEmailFollowUp.EmailFollowUpId + "','" + NotifyId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Không duyệt") + "</strong></button> \r\n";
                    }
                }
                Html +=
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xem email") + "</strong></button> \r\n" +
                    "                 </div> \r\n" +
                    "             </div> \r\n" +
                    "         </div> \r\n" +
                    "     </div> \r\n" +
                    " </div> \r\n";


                Html = WebEnvironments.EncryptHtml(Html) +
                    " <script>\r\n" +
                    "       $('.CssDate').datepicker({\r\n" +
                    "           format: 'dd/mm/yyyy'\r\n" +
                    "       });\r\n" +
                    "</script>\r\n";
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
        public static AjaxOut ServerSideApproved(
            RenderInfoCls ORenderInfo,
            string EmailFollowUpId,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                   OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                DateTime ApprovedDate = System.DateTime.Now;
                string ApprovedByUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ApproveEmailFollowUp(OActionSqlParam, EmailFollowUpId, ApprovedDate, ApprovedByUserId, NotifyId);



                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Duyệt thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, 
                    new ViewEmailFollowUp().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("EmailFollowUpId",EmailFollowUpId)
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
        public static AjaxOut ServerSideReject(
            RenderInfoCls ORenderInfo,
            string EmailFollowUpId,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
             
                DateTime ApprovedDate = System.DateTime.Now;
                string ApprovedByUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().RejectEmailFollowUp(OActionSqlParam, EmailFollowUpId, ApprovedDate, ApprovedByUserId, NotifyId);

                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Không duyệt thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, new ViewEmailFollowUp().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("EmailFollowUpId",EmailFollowUpId)
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
        public static AjaxOut ServerSideCloseNotify(
            RenderInfoCls ORenderInfo,
            string NotifyId,
            string EmailFollowUpId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                 OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                //OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CloseNotify(null, OSiteParam, NotifyId, UserId);

                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, new ViewEmailFollowUp().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("EmailFollowUpId",EmailFollowUpId)
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
