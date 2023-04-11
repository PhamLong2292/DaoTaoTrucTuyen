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
public class TepDinhKemProcessBll : TepDinhKemTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlTepDinhKemProcessBll";
    }
}


public override TepDinhKemCls[] Reading(
    RenderInfoCls ORenderInfo,
    TepDinhKemFilterCls OTepDinhKemFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().Reading(ActionSqlParam, OTepDinhKemFilter);
}


public override void Add(RenderInfoCls ORenderInfo, TepDinhKemCls OTepDinhKem)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().Add(ActionSqlParam, OTepDinhKem);
}


public override void Save(RenderInfoCls ORenderInfo, string TepDinhKemId, TepDinhKemCls OTepDinhKem)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().Save(ActionSqlParam, TepDinhKemId, OTepDinhKem);
}


public override void Delete(RenderInfoCls ORenderInfo, string TepDinhKemId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().Delete(ActionSqlParam, TepDinhKemId);
}


public override TepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().CreateModel(ActionSqlParam, TepDinhKemId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepDinhKemProcess().Duplicate(ActionSqlParam, TepDinhKemId);
}


}
}
