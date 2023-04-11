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
    public class DT_TaiLieuChuyenGiaoProcessBll : DT_TaiLieuChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_TaiLieuChuyenGiaoProcessBll";
            }
        }


        public override DT_TaiLieuChuyenGiaoCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Reading(ActionSqlParam, ODT_TaiLieuChuyenGiaoFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Add(ActionSqlParam, ODT_TaiLieuChuyenGiao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Save(ActionSqlParam, DT_TaiLieuChuyenGiaoId, ODT_TaiLieuChuyenGiao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Delete(ActionSqlParam, DT_TaiLieuChuyenGiaoId);
        }


        public override DT_TaiLieuChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().CreateModel(ActionSqlParam, DT_TaiLieuChuyenGiaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuChuyenGiaoProcess().Duplicate(ActionSqlParam, DT_TaiLieuChuyenGiaoId);
        }

    }
}
