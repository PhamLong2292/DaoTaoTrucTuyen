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
public class LichHoiChanProcessBll : LichHoiChanTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlLichHoiChanProcessBll";
    }
}


public override LichHoiChanCls[] Reading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Reading(ActionSqlParam, OLichHoiChanFilter);
}
public override LichHoiChanCls[] PageReading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().PageReading(ActionSqlParam, OLichHoiChanFilter, ref recordTotal);
}
        public override void Add(RenderInfoCls ORenderInfo, LichHoiChanCls OLichHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Add(ActionSqlParam, OLichHoiChan);
}


public override void Save(RenderInfoCls ORenderInfo, string LichHoiChanId, LichHoiChanCls OLichHoiChan)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Save(ActionSqlParam, LichHoiChanId, OLichHoiChan);
}


public override void Delete(RenderInfoCls ORenderInfo, string LichHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Delete(ActionSqlParam, LichHoiChanId);
}


public override LichHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().CreateModel(ActionSqlParam, LichHoiChanId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string LichHoiChanId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Duplicate(ActionSqlParam, LichHoiChanId);
}

        public override void AddChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().AddChuyenGias(ActionSqlParam, lichHoiChanId, bacSyIds, donViCongTacTens);
        }
        public override void RemoveChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().RemoveChuyenGias(ActionSqlParam, lichHoiChanId, bacSyIds);
        }
        public override void AddCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().AddCaBenhs(ActionSqlParam, lichHoiChanId, caBenhIds);
        }
        public override void RemoveCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().RemoveCaBenhs(ActionSqlParam, lichHoiChanId, caBenhIds);
        }
        public override void UpdateCaBenhStt(RenderInfoCls ORenderInfo, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().UpdateCaBenhStt(ActionSqlParam, lichHoiChanCaBenhs);
        }
        public override LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().GetLichHoiChanCaBenhs(ActionSqlParam, lichHoiChanId);
        }
        public override bool IsTrucTiepHoiChan(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().IsTrucTiepHoiChan(ActionSqlParam, LichHoiChanId, BacSyId);
        }

    }
}
