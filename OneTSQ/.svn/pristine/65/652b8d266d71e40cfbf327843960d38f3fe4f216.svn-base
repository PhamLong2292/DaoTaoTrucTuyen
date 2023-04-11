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
    public class DM_TieuChiThoiLuongDaoTaoTtProcessBll : DM_TieuChiThoiLuongDaoTaoTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TieuChiThoiLuongDaoTaoTtProcessBll";
            }
        }


        public override DM_TieuChiThoiLuongDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Reading(ActionSqlParam, OTieuChiThoiLuongDaoTaoTtFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Count(ActionSqlParam, OTieuChiThoiLuongDaoTaoTtFilter);
        }

        public override DM_TieuChiThoiLuongDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter, int PageIndex, int PageSize)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().ReadingWithPaging(ActionSqlParam, OTieuChiThoiLuongDaoTaoTtFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Add(ActionSqlParam, OTieuChiThoiLuongDaoTaoTt);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TieuChiThoiLuongDaoTaoTtId, DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Save(ActionSqlParam, TieuChiThoiLuongDaoTaoTtId, OTieuChiThoiLuongDaoTaoTt);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TieuChiThoiLuongDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Delete(ActionSqlParam, TieuChiThoiLuongDaoTaoTtId);
        }


        public override DM_TieuChiThoiLuongDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string TieuChiThoiLuongDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().CreateModel(ActionSqlParam, TieuChiThoiLuongDaoTaoTtId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string TieuChiThoiLuongDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Duplicate(ActionSqlParam, TieuChiThoiLuongDaoTaoTtId);
        }

        public override DM_TieuChiThoiLuongDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().PageReading(ActionSqlParam, OTieuChiThoiLuongDaoTaoTtFilter, ref recordTotal);
        }
    }
}
