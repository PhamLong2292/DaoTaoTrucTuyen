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
public class TaiLieuProcessBll : TaiLieuTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlTaiLieuProcessBll";
    }
}


public override TaiLieuCls[] Reading(
    RenderInfoCls ORenderInfo,
    TaiLieuFilterCls OTaiLieuFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().Reading(ActionSqlParam, OTaiLieuFilter);
}


public override void Add(RenderInfoCls ORenderInfo, TaiLieuCls OTaiLieu)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().Add(ActionSqlParam, OTaiLieu);
}


public override void Save(RenderInfoCls ORenderInfo, string TaiLieuId, TaiLieuCls OTaiLieu)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().Save(ActionSqlParam, TaiLieuId, OTaiLieu);
}


public override void Delete(RenderInfoCls ORenderInfo, string TaiLieuId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().Delete(ActionSqlParam, TaiLieuId);
}


public override TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().CreateModel(ActionSqlParam, TaiLieuId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuProcess().Duplicate(ActionSqlParam, TaiLieuId);
}


}
}
