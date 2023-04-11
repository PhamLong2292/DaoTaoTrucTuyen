using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{

public class MucDoPhongPhuBaiBaoCaoProcessBll : MucDoPhongPhuBaiBaoCaoTemplate
{
public override string ServiceId
{
    get
    {
        return "WsMucDoPhongPhuBaiBaoCaoProcessBll";
    }
}


public override MucDoPhongPhuBaiBaoCaoCls[] Reading(RenderInfoCls ORenderInfo,MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Reading(ActionSqlParam, OMucDoPhongPhuBaiBaoCaoFilter);
}


public override void Add(RenderInfoCls ORenderInfo, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Add(ActionSqlParam, OMucDoPhongPhuBaiBaoCao);
}


public override void Save(RenderInfoCls ORenderInfo, string MucDoPhongPhuBaiBaoCaoId, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Save(ActionSqlParam, MucDoPhongPhuBaiBaoCaoId, OMucDoPhongPhuBaiBaoCao);
}


public override void Delete(RenderInfoCls ORenderInfo, string MucDoPhongPhuBaiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Delete(ActionSqlParam, MucDoPhongPhuBaiBaoCaoId);
}


public override MucDoPhongPhuBaiBaoCaoCls CreateModel(RenderInfoCls ORenderInfo, string MucDoPhongPhuBaiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().CreateModel(ActionSqlParam, MucDoPhongPhuBaiBaoCaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string MucDoPhongPhuBaiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Duplicate(ActionSqlParam, MucDoPhongPhuBaiBaoCaoId);
}
public override long Count(RenderInfoCls ORenderInfo, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().Count(ActionSqlParam, OMucDoPhongPhuBaiBaoCaoFilter);
}

public override MucDoPhongPhuBaiBaoCaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateMucDoPhongPhuBaiBaoCaoProcess().ReadingWithPaging(ActionSqlParam, OMucDoPhongPhuBaiBaoCaoFilter, PageIndex, PageSize);
}


}
}
