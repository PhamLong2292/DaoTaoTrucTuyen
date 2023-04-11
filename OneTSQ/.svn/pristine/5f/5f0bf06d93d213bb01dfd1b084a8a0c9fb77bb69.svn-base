using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class BacSyOwnerUserProcessBll : BacSyOwnerUserTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlBacSyOwnerUserProcessBll";
            }
        }


        public override BacSyOwnerUserCls[] Reading(
            RenderInfoCls ORenderInfo,
            BacSyOwnerUserFilterCls OBacSyOwnerUserFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Reading(ActionSqlParam, OBacSyOwnerUserFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, BacSyOwnerUserCls OBacSyOwnerUser)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Add(ActionSqlParam, OBacSyOwnerUser);
        }


        public override void Save(RenderInfoCls ORenderInfo, string BacSyOwnerUserId, BacSyOwnerUserCls OBacSyOwnerUser)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Save(ActionSqlParam, BacSyOwnerUserId, OBacSyOwnerUser);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string BacSyOwnerUserId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Delete(ActionSqlParam, BacSyOwnerUserId);
        }


        public override BacSyOwnerUserCls CreateModel(RenderInfoCls ORenderInfo, string BacSyOwnerUserId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().CreateModel(ActionSqlParam, BacSyOwnerUserId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string BacSyOwnerUserId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyOwnerUserProcess().Duplicate(ActionSqlParam, BacSyOwnerUserId);
        }


    }
}
