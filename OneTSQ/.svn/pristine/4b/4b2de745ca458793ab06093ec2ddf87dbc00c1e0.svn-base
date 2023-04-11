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
public class ButPheProcessBll : ButPheTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlButPheProcessBll";
    }
}


public override ButPheCls[] Reading(
    RenderInfoCls ORenderInfo,
    ButPheFilterCls OButPheFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Reading(ActionSqlParam, OButPheFilter);
}


public override void Add(RenderInfoCls ORenderInfo, ButPheCls OButPhe)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Add(ActionSqlParam, OButPhe);
}


public override void Save(RenderInfoCls ORenderInfo, string ButPheId, ButPheCls OButPhe)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Save(ActionSqlParam, ButPheId, OButPhe);
}


public override void Delete(RenderInfoCls ORenderInfo, string ButPheId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Delete(ActionSqlParam, ButPheId);
}


public override ButPheCls CreateModel(RenderInfoCls ORenderInfo, string ButPheId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().CreateModel(ActionSqlParam, ButPheId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string ButPheId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateButPheProcess().Duplicate(ActionSqlParam, ButPheId);
}


}
}
