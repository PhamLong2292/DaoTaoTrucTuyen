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
    public class DT_LichLyThuyetChiTietProcessBll : DT_LichLyThuyetChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichLyThuyetChiTietProcessBll";
            }
        }


        public override DT_LichLyThuyetChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ActionSqlParam, ODT_LichLyThuyetChiTietFilter);
        }
        public override long Count(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Count(ActionSqlParam, ODT_LichLyThuyetChiTietFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Add(ActionSqlParam, ODT_LichLyThuyetChiTiet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Save(ActionSqlParam, DT_LichLyThuyetChiTietId, ODT_LichLyThuyetChiTiet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Delete(ActionSqlParam, DT_LichLyThuyetChiTietId);
        }


        public override DT_LichLyThuyetChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().CreateModel(ActionSqlParam, DT_LichLyThuyetChiTietId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Duplicate(ActionSqlParam, DT_LichLyThuyetChiTietId);
        }


    }
}
