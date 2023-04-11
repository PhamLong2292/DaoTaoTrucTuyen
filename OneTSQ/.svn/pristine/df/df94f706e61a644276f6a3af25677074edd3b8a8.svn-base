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
    public class CongTacVienDeTaiProcessBll : CongTacVienDeTaiTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlCongTacVienDeTaiProcessBll";
            }
        }

        public override CongTacVienDeTaiCls[] Reading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Reading(ActionSqlParam, OCongTacVienDeTaiFilter);
        }      
        public override CongTacVienDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().PageReading(ActionSqlParam, OCongTacVienDeTaiFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, CongTacVienDeTaiCls OCongTacVienDeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Add(ActionSqlParam, OCongTacVienDeTai);
        }


        public override void Save(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId, CongTacVienDeTaiCls OCongTacVienDeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Save(ActionSqlParam, CongTacVienDeTaiId, OCongTacVienDeTai);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Delete(ActionSqlParam, CongTacVienDeTaiId);
        }


        public override CongTacVienDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().CreateModel(ActionSqlParam, CongTacVienDeTaiId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Duplicate(ActionSqlParam, CongTacVienDeTaiId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCongTacVienDeTaiProcess().Count(ActionSqlParam, OCongTacVienDeTaiFilter);
        }


    }
}
