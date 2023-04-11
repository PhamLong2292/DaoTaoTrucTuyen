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
public class CaBenhProcessBll : CaBenhTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlCaBenhProcessBll";
    }
}


public override CaBenhCls[] Reading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Reading(ActionSqlParam, OCaBenhFilter);
}
public override CaBenhCls[] PageReading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter, ref long recordTotal)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().PageReading(ActionSqlParam, OCaBenhFilter, ref recordTotal);
}
public override long Count(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Count(ActionSqlParam, OCaBenhFilter);
}


public override void Add(RenderInfoCls ORenderInfo, CaBenhCls OCaBenh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Add(ActionSqlParam, OCaBenh);
}


public override void Save(RenderInfoCls ORenderInfo, string CaBenhId, CaBenhCls OCaBenh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Save(ActionSqlParam, CaBenhId, OCaBenh);
}


public override void Delete(RenderInfoCls ORenderInfo, string CaBenhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Delete(ActionSqlParam, CaBenhId);
}


public override CaBenhCls CreateModel(RenderInfoCls ORenderInfo, string CaBenhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().CreateModel(ActionSqlParam, CaBenhId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string CaBenhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().Duplicate(ActionSqlParam, CaBenhId);
}
        public override DataTable BCQuery(RenderInfoCls ORenderInfo, string Query)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ActionSqlParam, Query);
        }

    }
}
