using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.ChatUtility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class ExamForm: WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "ExamForm";
            }
        }

        

        public override string WebPartTitle
        {
            get
            {
                return "ExamForm demo";
            }
        }

        public override string Description
        {
            get
            {
                return "ExamForm demo";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ExamForm), Page);
        }

       
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
               //vẽ cái form khám bệnh....!

                string Html =
                    "<script>\r\n" +
                    "   function SubmitPatient()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo = CreateRenderInfo();\r\n" +
                    "       PatientObject = OneTSQ.WebParts.ExamForm.ServerSideCreatePatientObject(RenderInfo).value.RetObject;\r\n" +
                    "       PatientObject.PatientName = document.getElementById('txtPatientName').value;\r\n" +
                    "       AjaxOut  = OneTSQ.WebParts.ExamForm.ServerSideSubmitPatientProfile(RenderInfo, PatientObject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +
                    "</script>\r\n" +
                    "<input id=\"txtPatientName\" value=\"Nguyên Thị B\" class=\"form-control\">\r\n" +
                    "<button onclick=javascript:SubmitPatient();>Ghi hồ sơ</button>\r\n";

                RetAjaxOut.HtmlContent = Html;
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
        public static AjaxOut ServerSideCreatePatientObject(
            RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                WebSession.CheckSessionTimeOut(ORenderInfo);
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                PatientCls OPatient = new PatientCls();
                OPatient.PatientId = System.Guid.NewGuid().ToString();
                OPatient.CreatedByOwnerId =  WebEnvironments.GetOwnerId(ORenderInfo);
                OPatient.CreatedByUserId = WebEnvironments.GetOwnerUserId(ORenderInfo);
                OPatient.CreatedByRoleId = WebEnvironments.GetActiveRoleId(ORenderInfo);
                OPatient.CreatedByDepartmentId = WebEnvironments.GetOwnerDepartmentId(ORenderInfo);
                OPatient.OwnerCode = WebEnvironments.GetOwnerCode(ORenderInfo);

                RetAjaxOut.RetObject = OPatient;
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
        public static AjaxOut ServerSideSubmitPatientProfile(
            RenderInfoCls ORenderInfo,
            PatientCls OPatient)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                WebSession.CheckSessionTimeOut(ORenderInfo);
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                if (string.IsNullOrEmpty(OPatient.PatientName)) throw new Exception("Missing patient name");

                OPatient.PatientCode = System.Guid.NewGuid().ToString().Substring(0, 8);

                StartWorkflowRetCls 
                    OOStartWorkflowRet = CallBussinessUtility.CreateBussinessProcess().CreatePatientProcess().Add(ORenderInfo, OPatient);
                if (OOStartWorkflowRet.Error) throw new Exception(OOStartWorkflowRet.InfoMessage);

                RetAjaxOut.RetUrl = WebScreen.BuildWorkflowUrl(OSiteParam, OOStartWorkflowRet, null);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }



        public override bool NeedCheckPermission
        {
            get
            {
                return false;
            }
        }
    }
}
