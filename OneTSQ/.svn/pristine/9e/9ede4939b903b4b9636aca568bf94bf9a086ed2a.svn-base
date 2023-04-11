using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class PhieuDanhGiaChatLuongDaoTaoProcessBll : PhieuDanhGiaChatLuongDaoTaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuDanhGiaChatLuongDaoTaoProcessBll";
            }
        }


        public override PhieuDanhGiaChatLuongDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Reading(ActionSqlParam, OPhieuDanhGiaChatLuongDaoTaoFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Add(ActionSqlParam, OPhieuDanhGiaChatLuongDaoTao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string PhieuDanhGiaChatLuongDaoTaoId, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Save(ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoId, OPhieuDanhGiaChatLuongDaoTao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string PhieuDanhGiaChatLuongDaoTaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Delete(ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoId);
        }


        public override PhieuDanhGiaChatLuongDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string PhieuDanhGiaChatLuongDaoTaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().CreateModel(ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string PhieuDanhGiaChatLuongDaoTaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Duplicate(ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoId);
        }
        public override long Count(RenderInfoCls ORenderInfo, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().Count(ActionSqlParam, OPhieuDanhGiaChatLuongDaoTaoFilter);
        }

        public override PhieuDanhGiaChatLuongDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter, ref long recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().PageReading(ActionSqlParam, OPhieuDanhGiaChatLuongDaoTaoFilter, ref recordTotal);
        }


    }
}
