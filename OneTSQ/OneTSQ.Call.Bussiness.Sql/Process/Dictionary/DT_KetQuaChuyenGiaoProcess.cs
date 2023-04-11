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
    public class DT_KetQuaChuyenGiaoProcessBll : DT_KetQuaChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_KetQuaChuyenGiaoProcessBll";
            }
        }


        public override DT_KetQuaChuyenGiaoCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Reading(ActionSqlParam, ODT_KetQuaChuyenGiaoFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Add(ActionSqlParam, ODT_KetQuaChuyenGiao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_KetQuaChuyenGiaoId, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Save(ActionSqlParam, DT_KetQuaChuyenGiaoId, ODT_KetQuaChuyenGiao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_KetQuaChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Delete(ActionSqlParam, DT_KetQuaChuyenGiaoId);
        }


        public override DT_KetQuaChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_KetQuaChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().CreateModel(ActionSqlParam, DT_KetQuaChuyenGiaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_KetQuaChuyenGiaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().Duplicate(ActionSqlParam, DT_KetQuaChuyenGiaoId);
        }


    }
}
