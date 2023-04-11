using System.Linq;
using OneTSQ.Model;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using System;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class BienBanHoiChanToanLichProcessBll : BienBanHoiChanToanLichTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlBienBanHoiChanToanLichProcessBll";
    }
}


public override BienBanHoiChanToanLichCls[] Reading(RenderInfoCls ORenderInfo,BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Reading(ActionSqlParam, OBienBanHoiChanToanLichFilter);
}


public override void Add(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Add(ActionSqlParam, OBienBanHoiChanToanLich);
}


public override void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Save(ActionSqlParam, BienBanHoiChanToanLichId, OBienBanHoiChanToanLich);
}


public override void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Delete(ActionSqlParam, BienBanHoiChanToanLichId);
}


public override BienBanHoiChanToanLichCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().CreateModel(ActionSqlParam, BienBanHoiChanToanLichId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Duplicate(ActionSqlParam, BienBanHoiChanToanLichId);
}
public override long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().Count(ActionSqlParam, OBienBanHoiChanToanLichFilter);
}

public override BienBanHoiChanToanLichCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls  ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().ReadingWithPaging(ActionSqlParam, OBienBanHoiChanToanLichFilter, PageIndex, PageSize);
        }
        public override int GetSoBuoiHoiChanThamGia(RenderInfoCls ORenderInfo, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().GetSoBuoiHoiChanThamGia(ActionSqlParam, donViThamVanMa, donViTuVanMa, tuNgay, denNgay);
        }
        public override string[] GetBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, string donViTuVanMa, DateTime tuNgay, DateTime denNgay)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBienBanHoiChanToanLichProcess().GetBenhVienThamGiaHoiChan(ActionSqlParam, donViTuVanMa, tuNgay, denNgay);
        }


    }
}
