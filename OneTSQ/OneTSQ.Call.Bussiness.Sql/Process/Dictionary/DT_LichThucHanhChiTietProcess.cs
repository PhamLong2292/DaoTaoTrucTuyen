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
    public class DT_LichThucHanhChiTietProcessBll : DT_LichThucHanhChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichThucHanhChiTietProcessBll";
            }
        }


        public override DT_LichThucHanhChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Reading(ActionSqlParam, ODT_LichThucHanhChiTietFilter);
        }
        public override long Count(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Count(ActionSqlParam, ODT_LichThucHanhChiTietFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Add(ActionSqlParam, ODT_LichThucHanhChiTiet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Save(ActionSqlParam, DT_LichThucHanhChiTietId, ODT_LichThucHanhChiTiet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Delete(ActionSqlParam, DT_LichThucHanhChiTietId);
        }


        public override DT_LichThucHanhChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().CreateModel(ActionSqlParam, DT_LichThucHanhChiTietId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhChiTietProcess().Duplicate(ActionSqlParam, DT_LichThucHanhChiTietId);
        }


    }
}
