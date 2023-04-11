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
public class BienBanHoiChanProcessBll : BienBanHoiChanTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlBienBanHoiChanProcessBll";
    }
}


public override BienBanHoiChanCls[] Reading(
    RenderInfoCls ORenderInfo,
    BienBanHoiChanFilterCls OBienBanHoiChanFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Reading(ActionSqlParam, OBienBanHoiChanFilter);
}


public override void Add(RenderInfoCls ORenderInfo, BienBanHoiChanCls OBienBanHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Add(ActionSqlParam, OBienBanHoiChan);
}


public override void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanId, BienBanHoiChanCls OBienBanHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Save(ActionSqlParam, BienBanHoiChanId, OBienBanHoiChan);
}


public override void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Delete(ActionSqlParam, BienBanHoiChanId);
}


public override BienBanHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().CreateModel(ActionSqlParam, BienBanHoiChanId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanProcess().Duplicate(ActionSqlParam, BienBanHoiChanId);
}


}
}
