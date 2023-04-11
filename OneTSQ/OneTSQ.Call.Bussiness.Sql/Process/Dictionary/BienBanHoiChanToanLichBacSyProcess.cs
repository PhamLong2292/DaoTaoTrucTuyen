using System.Linq;
using OneTSQ.Model;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class BienBanHoiChanToanLichBacSyProcessBll : BienBanHoiChanToanLichBacSyTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlBienBanHoiChanToanLichBacSyProcessBll";
    }
}


public override BienBanHoiChanToanLichBacSyCls[] Reading(RenderInfoCls ORenderInfo,BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Reading(ActionSqlParam, OBienBanHoiChanToanLichBacSyFilter);
}


public override void Add(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Add(ActionSqlParam, OBienBanHoiChanToanLichBacSy);
}


public override void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Save(ActionSqlParam, BienBanHoiChanToanLichBacSyId, OBienBanHoiChanToanLichBacSy);
}


public override void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Delete(ActionSqlParam, BienBanHoiChanToanLichBacSyId);
}


public override BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().CreateModel(ActionSqlParam, BienBanHoiChanToanLichBacSyId);
}
public override BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().CreateModel(ActionSqlParam, LichHoiChanId, BacSyId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Duplicate(ActionSqlParam, BienBanHoiChanToanLichBacSyId);
}
public override long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().Count(ActionSqlParam, OBienBanHoiChanToanLichBacSyFilter);
}

public override BienBanHoiChanToanLichBacSyCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls  ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichBacSyProcess().ReadingWithPaging(ActionSqlParam, OBienBanHoiChanToanLichBacSyFilter, PageIndex, PageSize);
}


}
}
