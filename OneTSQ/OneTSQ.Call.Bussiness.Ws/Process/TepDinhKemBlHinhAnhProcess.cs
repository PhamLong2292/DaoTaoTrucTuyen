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
public class TepDinhKemBlHinhAnhProcessBll : TepDinhKemBlHinhAnhTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlTepDinhKemBlHinhAnhProcessBll";
    }
}


public override TepDinhKemBlHinhAnhCls[] Reading(
    RenderInfoCls ORenderInfo,
    TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().Reading(ActionSqlParam, OTepDinhKemBlHinhAnhFilter);
}


public override void Add(RenderInfoCls ORenderInfo, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().Add(ActionSqlParam, OTepDinhKemBlHinhAnh);
}


public override void Save(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().Save(ActionSqlParam, TepDinhKemBlHinhAnhId, OTepDinhKemBlHinhAnh);
}


public override void Delete(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().Delete(ActionSqlParam, TepDinhKemBlHinhAnhId);
}


public override TepDinhKemBlHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().CreateModel(ActionSqlParam, TepDinhKemBlHinhAnhId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemBlHinhAnhProcess().Duplicate(ActionSqlParam, TepDinhKemBlHinhAnhId);
}


}
}
