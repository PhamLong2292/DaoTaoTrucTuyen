using System.Linq;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using System;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{
public class BcDanhGiaChatLuongDaoTaoProcessBll : BcDanhGiaChatLuongDaoTaoTemplate
{
public override string ServiceId
{
    get
    {
        return "WsBcDanhGiaChatLuongDaoTaoProcessBll";
    }
}


public override BcDanhGiaChatLuongDaoTaoCls[] Reading(RenderInfoCls ORenderInfo,BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Reading(ActionSqlParam, OBcDanhGiaChatLuongDaoTaoFilter);
}


public override void Add(RenderInfoCls ORenderInfo, BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Add(ActionSqlParam, OBcDanhGiaChatLuongDaoTao);
}


public override void Save(RenderInfoCls ORenderInfo, string BcDanhGiaChatLuongDaoTaoId, BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Save(ActionSqlParam, BcDanhGiaChatLuongDaoTaoId, OBcDanhGiaChatLuongDaoTao);
}


public override void Delete(RenderInfoCls ORenderInfo, string BcDanhGiaChatLuongDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Delete(ActionSqlParam, BcDanhGiaChatLuongDaoTaoId);
}


public override BcDanhGiaChatLuongDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string BcDanhGiaChatLuongDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().CreateModel(ActionSqlParam, BcDanhGiaChatLuongDaoTaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string BcDanhGiaChatLuongDaoTaoId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Duplicate(ActionSqlParam, BcDanhGiaChatLuongDaoTaoId);
}
public override long Count(RenderInfoCls ORenderInfo, BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().Count(ActionSqlParam, OBcDanhGiaChatLuongDaoTaoFilter);
}

public override BcDanhGiaChatLuongDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter, ref long recordTotal)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().PageReading(ActionSqlParam, OBcDanhGiaChatLuongDaoTaoFilter, ref recordTotal);
        }
        public override void AddBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().AddBenhVienThamGiaHoiChan(ActionSqlParam, oBCDGCLuongDaoTaoDonViCongTac);
        }
        public override void UpdateBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().UpdateBenhVienThamGiaHoiChan(ActionSqlParam, oBCDGCLuongDaoTaoDonViCongTac);
        }
        public override void DeleteBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().DeleteBenhVienThamGiaHoiChan(ActionSqlParam, oBCDGCLuongDaoTaoDonViCongTac);
        }
        public override BCDGCLuongDaoTaoDonViCongTacCls[] GetBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, string bcDanhGiaChatLuongDaoTaoId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetBenhVienThamGiaHoiChan(ActionSqlParam, bcDanhGiaChatLuongDaoTaoId);
        }
        public override string[] GetBenhVienDanhGiaChatLuongDaoTao(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetBenhVienDanhGiaChatLuongDaoTao(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaChatLuongHoatDongTtb(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaChatLuongHoatDongTtb(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaDoHieuQua(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaDoHieuQua(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaMucDoPhongPhu(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaMucDoPhongPhu(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaThoiGian(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaThoiGian(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaThoiLuong(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaThoiLuong(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }
        public override TongHopDanhGiaMucYNghiaCls[] GetTongHopDanhGiaMucYNghia(RenderInfoCls ORenderInfo, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateBcDanhGiaChatLuongDaoTaoProcess().GetTongHopDanhGiaMucYNghia(ActionSqlParam, donViTuVanId, quy, nam, benhVienHoiChanMas);
        }


    }
}
