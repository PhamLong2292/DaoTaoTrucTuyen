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

namespace OneTSQ.Call.Bussiness.Ws
{
    public class BacSyProcessBll : BacSyTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlBacSyProcessBll";
            }
        }


        public override BacSyCls[] Reading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Reading(ActionSqlParam, OBacSyFilter);
        }
        public override BacSyCls[] PageReading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().PageReading(ActionSqlParam, OBacSyFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, BacSyCls OBacSy)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Add(ActionSqlParam, OBacSy);
        }


        public override void Save(RenderInfoCls ORenderInfo, string BacSyId, BacSyCls OBacSy)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Save(ActionSqlParam, BacSyId, OBacSy);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string BacSyId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Delete(ActionSqlParam, BacSyId);
        }


        public override BacSyCls CreateModel(RenderInfoCls ORenderInfo, string BacSyId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ActionSqlParam, BacSyId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string BacSyId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Duplicate(ActionSqlParam, BacSyId);
        }


    }
}
