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
public class ChuyenGiaHoiChanProcessBll : ChuyenGiaHoiChanTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlChuyenGiaHoiChanProcessBll";
    }
}


public override ChuyenGiaHoiChanCls[] Reading(
    RenderInfoCls ORenderInfo,
    ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().Reading(ActionSqlParam, OChuyenGiaHoiChanFilter);
}


public override void Add(RenderInfoCls ORenderInfo, ChuyenGiaHoiChanCls OChuyenGiaHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().Add(ActionSqlParam, OChuyenGiaHoiChan);
}


public override void Save(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId, ChuyenGiaHoiChanCls OChuyenGiaHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().Save(ActionSqlParam, ChuyenGiaHoiChanId, OChuyenGiaHoiChan);
}


public override void Delete(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().Delete(ActionSqlParam, ChuyenGiaHoiChanId);
}


public override ChuyenGiaHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().CreateModel(ActionSqlParam, ChuyenGiaHoiChanId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChuyenGiaHoiChanProcess().Duplicate(ActionSqlParam, ChuyenGiaHoiChanId);
}


}
}
