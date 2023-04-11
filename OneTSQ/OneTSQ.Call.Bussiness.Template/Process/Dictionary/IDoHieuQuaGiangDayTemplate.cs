using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IDoHieuQuaGiangDayProcess
   {
       string ServiceId { get; }
       DoHieuQuaGiangDayCls[] Reading(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter);
       void Add(RenderInfoCls ORenderInfo,  DoHieuQuaGiangDayCls ODoHieuQuaGiangDay);
       void Save(RenderInfoCls ORenderInfo,  string DoHieuQuaGiangDayId,DoHieuQuaGiangDayCls ODoHieuQuaGiangDay);
       void Delete(RenderInfoCls ORenderInfo,  string DoHieuQuaGiangDayId);
       DoHieuQuaGiangDayCls CreateModel(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId);
       string Duplicate(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId);
       long Count(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter);
       DoHieuQuaGiangDayCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize);
   }
   
   public class DoHieuQuaGiangDayTemplate : IDoHieuQuaGiangDayProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual DoHieuQuaGiangDayCls[] Reading(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId) { }
       public virtual DoHieuQuaGiangDayCls CreateModel(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string DoHieuQuaGiangDayId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter) { return 0; }
       public virtual DoHieuQuaGiangDayCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize) { return null; }
   }
}
