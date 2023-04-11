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
public class BinhLuanHinhAnhProcessBll : BinhLuanHinhAnhTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlBinhLuanHinhAnhProcessBll";
    }
}


public override BinhLuanHinhAnhCls[] Reading(
    RenderInfoCls ORenderInfo,
    BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().Reading(ActionSqlParam, OBinhLuanHinhAnhFilter);
}


public override void Add(RenderInfoCls ORenderInfo, BinhLuanHinhAnhCls OBinhLuanHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().Add(ActionSqlParam, OBinhLuanHinhAnh);
}


public override void Save(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId, BinhLuanHinhAnhCls OBinhLuanHinhAnh)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().Save(ActionSqlParam, BinhLuanHinhAnhId, OBinhLuanHinhAnh);
}


public override void Delete(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().Delete(ActionSqlParam, BinhLuanHinhAnhId);
}


public override BinhLuanHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().CreateModel(ActionSqlParam, BinhLuanHinhAnhId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBinhLuanHinhAnhProcess().Duplicate(ActionSqlParam, BinhLuanHinhAnhId);
}


}
}
