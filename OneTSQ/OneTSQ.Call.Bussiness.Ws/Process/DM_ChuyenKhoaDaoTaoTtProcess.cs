using System;
using System.Collections.ObjectModel;
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
    public class DM_ChuyenKhoaDaoTaoTtProcessBll : DM_ChuyenKhoaDaoTaoTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_ChuyenKhoaDaoTaoTtProcessBll";
            }
        }


        public override DM_ChuyenKhoaDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Reading(ActionSqlParam, OChuyenKhoaDaoTaoTtFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Count(ActionSqlParam, OChuyenKhoaDaoTaoTtFilter);
        }

        public override DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().ReadingWithPaging(ActionSqlParam, OChuyenKhoaDaoTaoTtFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Add(ActionSqlParam, OChuyenKhoaDaoTaoTt);
        }


        public override void Save(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Save(ActionSqlParam, ChuyenKhoaDaoTaoTtId, OChuyenKhoaDaoTaoTt);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Delete(ActionSqlParam, ChuyenKhoaDaoTaoTtId);
        }


        public override DM_ChuyenKhoaDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ActionSqlParam, ChuyenKhoaDaoTaoTtId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Duplicate(ActionSqlParam, ChuyenKhoaDaoTaoTtId);
        }

        public override DM_ChuyenKhoaDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().PageReading(ActionSqlParam, OChuyenKhoaDaoTaoTtFilter, ref recordTotal);
        }
    }
}
