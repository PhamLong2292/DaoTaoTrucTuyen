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
    public class DT_LichChuyenGiaoProcessBll : DT_LichChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichChuyenGiaoProcessBll";
            }
        }


        public override DT_LichChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Reading(ActionSqlParam, ODT_LichChuyenGiaoFilter);
        }

        public override DT_LichChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().PageReading(ActionSqlParam, ODT_LichChuyenGiaoFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoCls ODT_LichChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Add(ActionSqlParam, ODT_LichChuyenGiao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId, DT_LichChuyenGiaoCls ODT_LichChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Save(ActionSqlParam, DT_LichChuyenGiaoId, ODT_LichChuyenGiao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Delete(ActionSqlParam, DT_LichChuyenGiaoId);
        }


        public override DT_LichChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ActionSqlParam, DT_LichChuyenGiaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Duplicate(ActionSqlParam, DT_LichChuyenGiaoId);
        }


    }
}
