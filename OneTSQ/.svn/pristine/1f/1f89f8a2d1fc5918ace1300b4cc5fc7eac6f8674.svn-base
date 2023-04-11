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
public class DT_LichLyThuyetProcessBll : DT_LichLyThuyetTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlDT_LichLyThuyetProcessBll";
    }
}


public override DT_LichLyThuyetCls[] Reading(
    RenderInfoCls ORenderInfo,
    DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Reading(ActionSqlParam, ODT_LichLyThuyetFilter);
}


public override void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetCls ODT_LichLyThuyet)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Add(ActionSqlParam, ODT_LichLyThuyet);
}


public override void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId, DT_LichLyThuyetCls ODT_LichLyThuyet)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Save(ActionSqlParam, DT_LichLyThuyetId, ODT_LichLyThuyet);
}


public override void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Delete(ActionSqlParam, DT_LichLyThuyetId);
}


public override DT_LichLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().CreateModel(ActionSqlParam, DT_LichLyThuyetId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().Duplicate(ActionSqlParam, DT_LichLyThuyetId);
}


}
}
