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
public class TepTinProcessBll : TepTinTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlTepTinProcessBll";
    }
}


public override TepTinCls[] Reading(
    RenderInfoCls ORenderInfo,
    TepTinFilterCls OTepTinFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Reading(ActionSqlParam, OTepTinFilter);
}


public override void Add(RenderInfoCls ORenderInfo, TepTinCls OTepTin)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Add(ActionSqlParam, OTepTin);
}


public override void Save(RenderInfoCls ORenderInfo, string TepTinId, TepTinCls OTepTin)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Save(ActionSqlParam, TepTinId, OTepTin);
}


public override void Delete(RenderInfoCls ORenderInfo, string TepTinId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Delete(ActionSqlParam, TepTinId);
}


public override TepTinCls CreateModel(RenderInfoCls ORenderInfo, string TepTinId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().CreateModel(ActionSqlParam, TepTinId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string TepTinId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTepTinProcess().Duplicate(ActionSqlParam, TepTinId);
}


}
}
