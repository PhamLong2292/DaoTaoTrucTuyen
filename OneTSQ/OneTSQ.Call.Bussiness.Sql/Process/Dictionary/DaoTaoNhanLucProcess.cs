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
    public class DaoTaoNhanLucProcessBll : DaoTaoNhanLucTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDaoTaoNhanLucProcessBll";
            }
        }


        public override DaoTaoNhanLucCls[] Reading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Reading(ActionSqlParam, ODaoTaoNhanLucFilter);
        }      
        public override DaoTaoNhanLucCls[] PageReading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().PageReading(ActionSqlParam, ODaoTaoNhanLucFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, DaoTaoNhanLucCls ODaoTaoNhanLuc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Add(ActionSqlParam, ODaoTaoNhanLuc);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId, DaoTaoNhanLucCls ODaoTaoNhanLuc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Save(ActionSqlParam, DaoTaoNhanLucId, ODaoTaoNhanLuc);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Delete(ActionSqlParam, DaoTaoNhanLucId);
        }


        public override DaoTaoNhanLucCls CreateModel(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().CreateModel(ActionSqlParam, DaoTaoNhanLucId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Duplicate(ActionSqlParam, DaoTaoNhanLucId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().Count(ActionSqlParam, ODaoTaoNhanLucFilter);
        }


    }
}
