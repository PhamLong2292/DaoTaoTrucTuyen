using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{

public class DoHieuQuaGiangDayProcessBll : DoHieuQuaGiangDayTemplate
{
public override string ServiceId
{
    get
    {
        return "WsDoHieuQuaGiangDayProcessBll";
    }
}


public override DoHieuQuaGiangDayCls[] Reading(RenderInfoCls ORenderInfo,DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Reading(ActionSqlParam, ODoHieuQuaGiangDayFilter);
}


public override void Add(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Add(ActionSqlParam, ODoHieuQuaGiangDay);
}


public override void Save(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Save(ActionSqlParam, DoHieuQuaGiangDayId, ODoHieuQuaGiangDay);
}


public override void Delete(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Delete(ActionSqlParam, DoHieuQuaGiangDayId);
}


public override DoHieuQuaGiangDayCls CreateModel(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().CreateModel(ActionSqlParam, DoHieuQuaGiangDayId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Duplicate(ActionSqlParam, DoHieuQuaGiangDayId);
}
public override long Count(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().Count(ActionSqlParam, ODoHieuQuaGiangDayFilter);
}

public override DoHieuQuaGiangDayCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDoHieuQuaGiangDayProcess().ReadingWithPaging(ActionSqlParam, ODoHieuQuaGiangDayFilter, PageIndex, PageSize);
}


}
}
