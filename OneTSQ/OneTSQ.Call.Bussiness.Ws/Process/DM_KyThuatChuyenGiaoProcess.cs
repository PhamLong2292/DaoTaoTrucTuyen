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
    public class DM_KyThuatChuyenGiaoProcessBll : DM_KyThuatChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_KyThuatChuyenGiaoProcessBll";
            }
        }


        public override DM_KyThuatChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Reading(ActionSqlParam, OKyThuatChuyenGiaoFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Count(ActionSqlParam, OKyThuatChuyenGiaoFilter);
        }

        public override DM_KyThuatChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().ReadingWithPaging(ActionSqlParam, OKyThuatChuyenGiaoFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Add(ActionSqlParam, OKyThuatChuyenGiao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Save(ActionSqlParam, KyThuatChuyenGiaoId, OKyThuatChuyenGiao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Delete(ActionSqlParam, KyThuatChuyenGiaoId);
        }


        public override DM_KyThuatChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ActionSqlParam, KyThuatChuyenGiaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Duplicate(ActionSqlParam, KyThuatChuyenGiaoId);
        }

        public override DM_KyThuatChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().PageReading(ActionSqlParam, OKyThuatChuyenGiaoFilter, ref recordTotal);
        }
    }
}
