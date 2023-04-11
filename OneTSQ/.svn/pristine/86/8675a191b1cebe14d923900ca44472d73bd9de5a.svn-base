using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{

public class DanhGiaThoiGianBuoiBaoCaoProcessBll : DanhGiaThoiGianBuoiBaoCaoTemplate
{
public override string ServiceId
{
    get
    {
        return "WsDanhGiaThoiGianBuoiBaoCaoProcessBll";
    }
}


public override DanhGiaThoiGianBuoiBaoCaoCls[] Reading(RenderInfoCls ORenderInfo,DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Reading(ActionSqlParam, ODanhGiaThoiGianBuoiBaoCaoFilter);
}


public override void Add(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Add(ActionSqlParam, ODanhGiaThoiGianBuoiBaoCao);
}


public override void Save(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Save(ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoId, ODanhGiaThoiGianBuoiBaoCao);
}


public override void Delete(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Delete(ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoId);
}


public override DanhGiaThoiGianBuoiBaoCaoCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().CreateModel(ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Duplicate(ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoId);
}
public override long Count(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().Count(ActionSqlParam, ODanhGiaThoiGianBuoiBaoCaoFilter);
}

public override DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiGianBuoiBaoCaoProcess().ReadingWithPaging(ActionSqlParam, ODanhGiaThoiGianBuoiBaoCaoFilter, PageIndex, PageSize);
}


}
}
