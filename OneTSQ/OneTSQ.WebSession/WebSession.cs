using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Utility
{
    public class WebSession
    {
        public static void CheckSessionTimeOut(RenderInfoCls ORenderInfo)
        {
            
            SiteParam
                OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

            if (WebSessionUtility.GetCurrentLoginUser(OSiteParam) == null)
            {
                //doan nay reconnect la session
                //WebEnvironments.ReConnectionSession(OSiteParam);

                LoginParamCls
                    OLoginParam = new LoginParamCls();
                OLoginParam.ReconnectionSession = 1;
                OLoginParam.AutoSessionLoginId = ORenderInfo.UserSessionId;
                OwnerUserCls OOwnerUser = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Login(ORenderInfo, OLoginParam);
                WebSessionUtility.SetCurrentLoginUser(OSiteParam, OOwnerUser);
                if (WebSessionUtility.GetCurrentLoginUser(OSiteParam) == null)
                {
                    throw new Exception("Session timeout. Xin vui lòng đăng nhập lại!");
                }
            }

            if (ORenderInfo.UserSessionId.Length > 8)
            {
                string Id = ORenderInfo.UserSessionId.Substring(0,ORenderInfo.UserSessionId.Length - 8);
                if (!Id.Equals(WebEnvironments.GetUseSessionId()))
                {
                    throw new Exception("Access denied");
                }
            }
        }
    }
}
