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
    public class DangKyDeTaiProcessBll : DangKyDeTaiTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDangKyDeTaiProcessBll";
            }
        }


        public override DangKyDeTaiCls[] Reading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Reading(ActionSqlParam, ODangKyDeTaiFilter);
        }      
        public override DangKyDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().PageReading(ActionSqlParam, ODangKyDeTaiFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, DangKyDeTaiCls ODangKyDeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Add(ActionSqlParam, ODangKyDeTai);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DangKyDeTaiId, DangKyDeTaiCls ODangKyDeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Save(ActionSqlParam, DangKyDeTaiId, ODangKyDeTai);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Delete(ActionSqlParam, DangKyDeTaiId);
        }


        public override DangKyDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().CreateModel(ActionSqlParam, DangKyDeTaiId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DangKyDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Duplicate(ActionSqlParam, DangKyDeTaiId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDangKyDeTaiProcess().Count(ActionSqlParam, ODangKyDeTaiFilter);
        }


    }
}
