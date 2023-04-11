using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class DanhGiaThoiLuongBuoiBaoCaoProcessBll : DanhGiaThoiLuongBuoiBaoCaoTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlDanhGiaThoiLuongBuoiBaoCaoProcessBll";
    }
}


public override DanhGiaThoiLuongBuoiBaoCaoCls[] Reading(RenderInfoCls ORenderInfo,DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Reading(ActionSqlParam, ODanhGiaThoiLuongBuoiBaoCaoFilter);
}


public override void Add(RenderInfoCls ORenderInfo, DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Add(ActionSqlParam, ODanhGiaThoiLuongBuoiBaoCao);
}


public override void Save(RenderInfoCls ORenderInfo, string DanhGiaThoiLuongBuoiBaoCaoId, DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Save(ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoId, ODanhGiaThoiLuongBuoiBaoCao);
}


public override void Delete(RenderInfoCls ORenderInfo, string DanhGiaThoiLuongBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Delete(ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoId);
}


public override DanhGiaThoiLuongBuoiBaoCaoCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaThoiLuongBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().CreateModel(ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaThoiLuongBuoiBaoCaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Duplicate(ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoId);
}
public override long Count(RenderInfoCls ORenderInfo, DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().Count(ActionSqlParam, ODanhGiaThoiLuongBuoiBaoCaoFilter);
}

public override DanhGiaThoiLuongBuoiBaoCaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls  ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaThoiLuongBuoiBaoCaoProcess().ReadingWithPaging(ActionSqlParam, ODanhGiaThoiLuongBuoiBaoCaoFilter, PageIndex, PageSize);
}


}
}
