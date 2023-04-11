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
    public class DM_TieuChiThoiGianDaoTaoTtProcessBll : DM_TieuChiThoiGianDaoTaoTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TieuChiThoiGianDaoTaoTtProcessBll";
            }
        }


        public override DM_TieuChiThoiGianDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Reading(ActionSqlParam, OTieuChiThoiGianDaoTaoTtFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Count(ActionSqlParam, OTieuChiThoiGianDaoTaoTtFilter);
        }

        public override DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, int PageIndex, int PageSize)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().ReadingWithPaging(ActionSqlParam, OTieuChiThoiGianDaoTaoTtFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Add(ActionSqlParam, OTieuChiThoiGianDaoTaoTt);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Save(ActionSqlParam, TieuChiThoiGianDaoTaoTtId, OTieuChiThoiGianDaoTaoTt);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Delete(ActionSqlParam, TieuChiThoiGianDaoTaoTtId);
        }


        public override DM_TieuChiThoiGianDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().CreateModel(ActionSqlParam, TieuChiThoiGianDaoTaoTtId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Duplicate(ActionSqlParam, TieuChiThoiGianDaoTaoTtId);
        }

        public override DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().PageReading(ActionSqlParam, OTieuChiThoiGianDaoTaoTtFilter, ref recordTotal);
        }
    }
}
