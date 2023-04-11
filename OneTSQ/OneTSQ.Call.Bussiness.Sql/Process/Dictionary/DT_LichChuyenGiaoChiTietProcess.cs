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
    public class DT_LichChuyenGiaoChiTietProcessBll : DT_LichChuyenGiaoChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichChuyenGiaoChiTietProcessBll";
            }
        }


        public override DT_LichChuyenGiaoChiTietCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ActionSqlParam, ODT_LichChuyenGiaoChiTietFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Add(ActionSqlParam, ODT_LichChuyenGiaoChiTiet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Save(ActionSqlParam, DT_LichChuyenGiaoChiTietId, ODT_LichChuyenGiaoChiTiet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Delete(ActionSqlParam, DT_LichChuyenGiaoChiTietId);
        }


        public override DT_LichChuyenGiaoChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().CreateModel(ActionSqlParam, DT_LichChuyenGiaoChiTietId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Duplicate(ActionSqlParam, DT_LichChuyenGiaoChiTietId);
        }


    }
}
