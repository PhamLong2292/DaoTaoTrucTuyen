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
    public class DM_GiayToDiChuyenGiaoProcessBll : DM_GiayToDiChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_GiayToDiChuyenGiaoProcessBll";
            }
        }


        public override DM_GiayToDiChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Reading(ActionSqlParam, OGiayToDiChuyenGiaoFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Count(ActionSqlParam, OGiayToDiChuyenGiaoFilter);
        }

        public override DM_GiayToDiChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().ReadingWithPaging(ActionSqlParam, OGiayToDiChuyenGiaoFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Add(ActionSqlParam, OGiayToDiChuyenGiao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Save(ActionSqlParam, GiayToDiChuyenGiaoId, OGiayToDiChuyenGiao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Delete(ActionSqlParam, GiayToDiChuyenGiaoId);
        }


        public override DM_GiayToDiChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().CreateModel(ActionSqlParam, GiayToDiChuyenGiaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Duplicate(ActionSqlParam, GiayToDiChuyenGiaoId);
        }

        public override DM_GiayToDiChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().PageReading(ActionSqlParam, OGiayToDiChuyenGiaoFilter, ref recordTotal);
        }
    }
}
