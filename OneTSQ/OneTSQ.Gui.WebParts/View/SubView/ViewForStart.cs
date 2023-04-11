using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Permission;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class ViewForDoctor : ViewProfileExamTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "Start";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Màn hình ở điểm start";
            }
        }

        public override string WorkflowStatus
        {
            get
            {
                return "Start";
            }
        }

        public override AjaxOut Draw(RenderInfoCls ORenderInfo, WorkflowInstanceCls OWorkflowInstance, WorkflowInstanceSessionCls OWorkflowInstanceSession, WorkflowInstanceSessionUserCls OWorkflowInstanceSessionUser)
        {
            AjaxOut
                 RetAjaxOut = new AjaxOut();

            RetAjaxOut.HtmlContent = "MAN HINH BAT DAU";

            return RetAjaxOut;
        }
    }
}
