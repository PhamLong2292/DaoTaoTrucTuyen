using System.Linq;
using OneTSQ.Model;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{
public class NoiDungHoiChanProcessBll : NoiDungHoiChanTemplate
{
public override string ServiceId
{
    get
    {
        return "WsNoiDungHoiChanProcessBll";
    }
}


public override NoiDungHoiChanCls[] Reading(RenderInfoCls ORenderInfo,NoiDungHoiChanFilterCls ONoiDungHoiChanFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Reading(ActionSqlParam, ONoiDungHoiChanFilter);
}


public override void Add(RenderInfoCls ORenderInfo, NoiDungHoiChanCls ONoiDungHoiChan)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Add(ActionSqlParam, ONoiDungHoiChan);
}


public override void Save(RenderInfoCls ORenderInfo, string NoiDungHoiChanId, NoiDungHoiChanCls ONoiDungHoiChan)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Save(ActionSqlParam, NoiDungHoiChanId, ONoiDungHoiChan);
}


public override void Delete(RenderInfoCls ORenderInfo, string NoiDungHoiChanId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Delete(ActionSqlParam, NoiDungHoiChanId);
}


public override NoiDungHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string NoiDungHoiChanId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().CreateModel(ActionSqlParam, NoiDungHoiChanId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string NoiDungHoiChanId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Duplicate(ActionSqlParam, NoiDungHoiChanId);
}
public override long Count(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().Count(ActionSqlParam, ONoiDungHoiChanFilter);
}

public override NoiDungHoiChanCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateNoiDungHoiChanProcess().ReadingWithPaging(ActionSqlParam, ONoiDungHoiChanFilter, PageIndex, PageSize);
}


}
}
