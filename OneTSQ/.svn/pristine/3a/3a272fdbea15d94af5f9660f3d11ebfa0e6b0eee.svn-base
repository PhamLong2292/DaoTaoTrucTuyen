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

namespace OneTSQ.Call.Bussiness.Sql
{
    public class DM_TrangThietBiTruyenHinhTtProcessBll : DM_TrangThietBiTruyenHinhTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TrangThietBiTruyenHinhTtProcessBll";
            }
        }


        public override DM_TrangThietBiTruyenHinhTtCls[] Reading(
            RenderInfoCls ORenderInfo,
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Reading(ActionSqlParam, OTrangThietBiTruyenHinhTtFilter);
        }

        public override long Count(
            RenderInfoCls ORenderInfo,
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Count(ActionSqlParam, OTrangThietBiTruyenHinhTtFilter);
        }

        public override DM_TrangThietBiTruyenHinhTtCls[] ReadingWithPaging(
            RenderInfoCls ORenderInfo,
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter,
                int PageIndex,
                int PageSize)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().ReadingWithPaging(ActionSqlParam, OTrangThietBiTruyenHinhTtFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Add(ActionSqlParam, OTrangThietBiTruyenHinhTt);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Save(ActionSqlParam, TrangThietBiTruyenHinhTtId, OTrangThietBiTruyenHinhTt);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Delete(ActionSqlParam, TrangThietBiTruyenHinhTtId);
        }


        public override DM_TrangThietBiTruyenHinhTtCls CreateModel(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().CreateModel(ActionSqlParam, TrangThietBiTruyenHinhTtId);
        }
        public override string Duplicate(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Duplicate(ActionSqlParam, TrangThietBiTruyenHinhTtId);
        }
        public override DM_TrangThietBiTruyenHinhTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter, ref int recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().PageReading(ActionSqlParam, OTrangThietBiTruyenHinhTtFilter, ref recordTotal);
        }
        public override DM_TrangThietBiTruyenHinhTtCls CheckCode(RenderInfoCls ORenderInfo, string MaTrangThietBiTruyenHinhTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().CheckCode(ActionSqlParam, MaTrangThietBiTruyenHinhTt);
        }
    }
}
