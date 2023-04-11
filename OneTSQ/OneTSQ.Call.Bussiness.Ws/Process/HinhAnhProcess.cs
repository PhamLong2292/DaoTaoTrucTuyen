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

namespace OneTSQ.Call.Bussiness.Ws
{
public class HinhAnhProcessBll : HinhAnhTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlHinhAnhProcessBll";
    }
}


public override HinhAnhCls[] Reading(RenderInfoCls ORenderInfo, HinhAnhFilterCls OHinhAnhFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Reading(ActionSqlParam, OHinhAnhFilter);
}
public override HinhAnhCls[] PageReading(RenderInfoCls ORenderInfo, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().PageReading(ActionSqlParam, OHinhAnhFilter, ref recordTotal);
}


public override void Add(RenderInfoCls ORenderInfo, HinhAnhCls OHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Add(ActionSqlParam, OHinhAnh);
}


public override void Save(RenderInfoCls ORenderInfo, string HinhAnhId, HinhAnhCls OHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Save(ActionSqlParam, HinhAnhId, OHinhAnh);
}


public override void Delete(RenderInfoCls ORenderInfo, string HinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Delete(ActionSqlParam, HinhAnhId);
}


public override HinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string HinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().CreateModel(ActionSqlParam, HinhAnhId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string HinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Duplicate(ActionSqlParam, HinhAnhId);
}


}
}
