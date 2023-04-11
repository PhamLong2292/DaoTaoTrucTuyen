using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class YKienChuyenGiaProcessBll : YKienChuyenGiaTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlYKienChuyenGiaProcessBll";
    }
}


public override YKienChuyenGiaCls[] Reading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().Reading(ActionSqlParam, OYKienChuyenGiaFilter);
}
public override YKienChuyenGiaCls[] PageReading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().PageReading(ActionSqlParam, OYKienChuyenGiaFilter, ref recordTotal);
}
public override YKienChuyenGiaCls[] GetChuyenGias(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().GetChuyenGias(ActionSqlParam, OYKienChuyenGiaFilter);
}


public override void Add(RenderInfoCls ORenderInfo, YKienChuyenGiaCls OYKienChuyenGia)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().Add(ActionSqlParam, OYKienChuyenGia);
}


public override void Save(RenderInfoCls ORenderInfo, string YKienChuyenGiaId, YKienChuyenGiaCls OYKienChuyenGia)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().Save(ActionSqlParam, YKienChuyenGiaId, OYKienChuyenGia);
}


public override void Delete(RenderInfoCls ORenderInfo, string YKienChuyenGiaId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().Delete(ActionSqlParam, YKienChuyenGiaId);
}


public override YKienChuyenGiaCls CreateModel(RenderInfoCls ORenderInfo, string YKienChuyenGiaId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().CreateModel(ActionSqlParam, YKienChuyenGiaId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string YKienChuyenGiaId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienChuyenGiaProcess().Duplicate(ActionSqlParam, YKienChuyenGiaId);
}


}
}
