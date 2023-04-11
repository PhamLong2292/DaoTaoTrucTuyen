using System;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IBienBanHoiChanToanLichProcess
   {
       string ServiceId { get; }
       BienBanHoiChanToanLichCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter);
       void Add(RenderInfoCls ORenderInfo,  BienBanHoiChanToanLichCls OBienBanHoiChanToanLich);
       void Save(RenderInfoCls ORenderInfo,  string BienBanHoiChanToanLichId,BienBanHoiChanToanLichCls OBienBanHoiChanToanLich);
       void Delete(RenderInfoCls ORenderInfo,  string BienBanHoiChanToanLichId);
       BienBanHoiChanToanLichCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId);
       string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId);
       long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter);
       BienBanHoiChanToanLichCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize);
        int GetSoBuoiHoiChanThamGia(RenderInfoCls ORenderInfo, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay);
        string[] GetBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, string donViTuVanMa, DateTime tuNgay, DateTime denNgay);
    }
   
   public class BienBanHoiChanToanLichTemplate : IBienBanHoiChanToanLichProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BienBanHoiChanToanLichCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId) { }
       public virtual BienBanHoiChanToanLichCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter) { return 0; }
       public virtual BienBanHoiChanToanLichCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize) { return null; }
        public virtual int GetSoBuoiHoiChanThamGia(RenderInfoCls ORenderInfo, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay) { return 0; }
        public virtual string[] GetBenhVienThamGiaHoiChan(RenderInfoCls ORenderInfo, string donViTuVanMa, DateTime tuNgay, DateTime denNgay) { return null; }
    }
}
