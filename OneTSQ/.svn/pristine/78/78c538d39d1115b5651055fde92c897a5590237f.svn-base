using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class MucDoYNghiaChuongTrinhDaoTaoProcessBll : MucDoYNghiaChuongTrinhDaoTaoTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlMucDoYNghiaChuongTrinhDaoTaoProcessBll";
    }
}


public override MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(RenderInfoCls ORenderInfo,MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Reading(ActionSqlParam, OMucDoYNghiaChuongTrinhDaoTaoFilter);
}


public override void Add(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Add(ActionSqlParam, OMucDoYNghiaChuongTrinhDaoTao);
}


public override void Save(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Save(ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoId, OMucDoYNghiaChuongTrinhDaoTao);
}


public override void Delete(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Delete(ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoId);
}


public override MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().CreateModel(ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Duplicate(ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoId);
}
public override long Count(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().Count(ActionSqlParam, OMucDoYNghiaChuongTrinhDaoTaoFilter);
}

public override MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls  ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoYNghiaChuongTrinhDaoTaoProcess().ReadingWithPaging(ActionSqlParam, OMucDoYNghiaChuongTrinhDaoTaoFilter, PageIndex, PageSize);
}


}
}
