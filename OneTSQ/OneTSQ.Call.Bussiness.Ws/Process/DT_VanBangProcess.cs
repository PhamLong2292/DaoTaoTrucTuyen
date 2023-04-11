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
public class DT_VanBangProcessBll : DT_VanBangTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlDT_VanBangProcessBll";
    }
}


public override DT_VanBangCls[] Reading(
    RenderInfoCls ORenderInfo,
    DT_VanBangFilterCls ODT_VanBangFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Reading(ActionSqlParam, ODT_VanBangFilter);
}


public override void Add(RenderInfoCls ORenderInfo, DT_VanBangCls ODT_VanBang)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Add(ActionSqlParam, ODT_VanBang);
}


public override void Save(RenderInfoCls ORenderInfo, string DT_VanBangId, DT_VanBangCls ODT_VanBang)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Save(ActionSqlParam, DT_VanBangId, ODT_VanBang);
}


public override void Delete(RenderInfoCls ORenderInfo, string DT_VanBangId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Delete(ActionSqlParam, DT_VanBangId);
}


public override DT_VanBangCls CreateModel(RenderInfoCls ORenderInfo, string DT_VanBangId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().CreateModel(ActionSqlParam, DT_VanBangId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DT_VanBangId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_VanBangProcess().Duplicate(ActionSqlParam, DT_VanBangId);
}


}
}
