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
    public class PhieuBaoCaoSuCoYKhoaProcessBll : PhieuBaoCaoSuCoYKhoaTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuBaoCaoSuCoYKhoaProcessBll";
            }
        }


        public override PhieuBaoCaoSuCoYKhoaCls[] Reading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Reading(ActionSqlParam, OPhieuBaoCaoSuCoYKhoaFilter);
        }      
        public override PhieuBaoCaoSuCoYKhoaCls[] PageReading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().PageReading(ActionSqlParam, OPhieuBaoCaoSuCoYKhoaFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Add(ActionSqlParam, OPhieuBaoCaoSuCoYKhoa);
        }


        public override void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Save(ActionSqlParam, PhieuBaoCaoSuCoYKhoaId, OPhieuBaoCaoSuCoYKhoa);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Delete(ActionSqlParam, PhieuBaoCaoSuCoYKhoaId);
        }


        public override PhieuBaoCaoSuCoYKhoaCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().CreateModel(ActionSqlParam, PhieuBaoCaoSuCoYKhoaId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Duplicate(ActionSqlParam, PhieuBaoCaoSuCoYKhoaId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoSuCoYKhoaProcess().Count(ActionSqlParam, OPhieuBaoCaoSuCoYKhoaFilter);
        }


    }
}
