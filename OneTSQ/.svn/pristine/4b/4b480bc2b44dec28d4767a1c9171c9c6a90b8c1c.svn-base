using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class KetQuaXetNghiemProcessBll : KetQuaXetNghiemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlKetQuaXetNghiemProcessBll";
            }
        }


        public override KetQuaXetNghiemCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Reading(ActionSqlParam, OKetQuaXetNghiemFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemCls OKetQuaXetNghiem)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Add(ActionSqlParam, OKetQuaXetNghiem);
        }


        public override void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId, KetQuaXetNghiemCls OKetQuaXetNghiem)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Save(ActionSqlParam, KetQuaXetNghiemId, OKetQuaXetNghiem);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Delete(ActionSqlParam, KetQuaXetNghiemId);
        }


        public override KetQuaXetNghiemCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().CreateModel(ActionSqlParam, KetQuaXetNghiemId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Duplicate(ActionSqlParam, KetQuaXetNghiemId);
        }
        public override long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().Count(ActionSqlParam, OKetQuaXetNghiemFilter);
        }

        public override KetQuaXetNghiemCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().ReadingWithPaging(ActionSqlParam, OKetQuaXetNghiemFilter, PageIndex, PageSize);
        }


    }
}
