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
public class LapLichThanhVienHoiChanProcessBll : LapLichThanhVienHoiChanTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlLapLichThanhVienHoiChanProcessBll";
    }
}


public override LapLichThanhVienHoiChanCls[] Reading(
    RenderInfoCls ORenderInfo,
    LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Reading(ActionSqlParam, OLapLichThanhVienHoiChanFilter);
}


public override void Add(RenderInfoCls ORenderInfo, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Add(ActionSqlParam, OLapLichThanhVienHoiChan);
}


public override void Save(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Save(ActionSqlParam, LapLichThanhVienHoiChanId, OLapLichThanhVienHoiChan);
}


public override void Delete(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Delete(ActionSqlParam, LapLichThanhVienHoiChanId);
}


public override LapLichThanhVienHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().CreateModel(ActionSqlParam, LapLichThanhVienHoiChanId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichThanhVienHoiChanProcess().Duplicate(ActionSqlParam, LapLichThanhVienHoiChanId);
}


}
}
