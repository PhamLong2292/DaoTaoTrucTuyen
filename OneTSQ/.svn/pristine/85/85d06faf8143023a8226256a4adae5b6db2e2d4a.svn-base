using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ObjectType
{
    public class ObjectTypeCaseProfile : ObjectTypeInfoTemplate
    {
        public override string ObjectType
        {
            get
            {
                return "profile";
            }
        }

        public override AjaxOut GetViewUrl(
            RenderInfoCls ORenderInfo, 
            string OwnerCode,
            string ObjectType, 
            string ObjectId, 
            string ObjectExtraInfo, 
            OwnerUserCls OViewUser)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, "profile", new WebParamCls[]
            {
                new WebParamCls("profileid",ObjectId),
            });

            return RetAjaxOut;
        }

        public override void SetHasRead(RenderInfoCls ORenderInfo, string OwnerCode, string ObjectType, string ObjectId,string ChatId, string OwnerUserId)
        {
            SiteParam 
                OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            CoreCallBussinessUtility.CreateBussinessProcess().CreateChatProcess().SetHeadRead(ORenderInfo, ChatId, OwnerUserId);
        }
    }
}
