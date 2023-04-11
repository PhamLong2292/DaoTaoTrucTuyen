using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class KetQuaXetNghiemChiTietProcessBll : KetQuaXetNghiemChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlKetQuaXetNghiemChiTietProcessBll";
            }
        }


        public override KetQuaXetNghiemChiTietCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Reading(ActionSqlParam, OKetQuaXetNghiemChiTietFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Add(ActionSqlParam, OKetQuaXetNghiemChiTiet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Save(ActionSqlParam, KetQuaXetNghiemChiTietId, OKetQuaXetNghiemChiTiet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Delete(ActionSqlParam, KetQuaXetNghiemChiTietId);
        }


        public override KetQuaXetNghiemChiTietCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().CreateModel(ActionSqlParam, KetQuaXetNghiemChiTietId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Duplicate(ActionSqlParam, KetQuaXetNghiemChiTietId);
        }
        public override long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().Count(ActionSqlParam, OKetQuaXetNghiemChiTietFilter);
        }

        public override KetQuaXetNghiemChiTietCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemChiTietProcess().ReadingWithPaging(ActionSqlParam, OKetQuaXetNghiemChiTietFilter, PageIndex, PageSize);
        }


    }
}
