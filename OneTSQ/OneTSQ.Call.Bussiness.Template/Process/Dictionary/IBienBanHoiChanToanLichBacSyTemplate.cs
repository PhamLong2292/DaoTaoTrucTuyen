
using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IBienBanHoiChanToanLichBacSyProcess
   {
       string ServiceId { get; }
       BienBanHoiChanToanLichBacSyCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter);
       void Add(RenderInfoCls ORenderInfo,  BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy);
       void Save(RenderInfoCls ORenderInfo,  string BienBanHoiChanToanLichBacSyId,BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy);
       void Delete(RenderInfoCls ORenderInfo,  string BienBanHoiChanToanLichBacSyId);
       BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId);
       BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId);
       string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId);
       long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter);
       BienBanHoiChanToanLichBacSyCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter, int PageIndex, int PageSize);
   }
   
   public class BienBanHoiChanToanLichBacSyTemplate : IBienBanHoiChanToanLichBacSyProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BienBanHoiChanToanLichBacSyCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId) { }
       public virtual BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId) { return null; }
       public virtual BienBanHoiChanToanLichBacSyCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanToanLichBacSyId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter) { return 0; }
       public virtual BienBanHoiChanToanLichBacSyCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter, int PageIndex, int PageSize) { return null; }
   }
}
