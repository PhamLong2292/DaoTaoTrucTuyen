using System;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IBienBanHoiChanToanLichProcess
   {
       string ServiceId { get; }
       BienBanHoiChanToanLichCls[] Reading(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  BienBanHoiChanToanLichCls OBienBanHoiChanToanLich);
       void Save(ActionSqlParamCls ActionSqlParam,  string BienBanHoiChanToanLichId,BienBanHoiChanToanLichCls OBienBanHoiChanToanLich);
       void Delete(ActionSqlParamCls ActionSqlParam,  string BienBanHoiChanToanLichId);
       BienBanHoiChanToanLichCls CreateModel(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId);
       long Count(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter);
       BienBanHoiChanToanLichCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize);
        int GetSoBuoiHoiChanThamGia(ActionSqlParamCls ActionSqlParam, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay);
        string[] GetBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, string donViTuVanMa, DateTime tuNgay, DateTime denNgay);
    }
   
   public class BienBanHoiChanToanLichTemplate : IBienBanHoiChanToanLichProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BienBanHoiChanToanLichCls[] Reading(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId) { }
       public virtual BienBanHoiChanToanLichCls CreateModel(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanToanLichId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter) { return 0; }
       public virtual BienBanHoiChanToanLichCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize) { return null; }
        public virtual int GetSoBuoiHoiChanThamGia(ActionSqlParamCls ActionSqlParam, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay) { return 0; }
        public virtual string[] GetBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, string donViTuVanMa, DateTime tuNgay, DateTime denNgay) { return null; }
    }
}
