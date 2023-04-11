using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface INoiDungHoiChanProcess
   {
       string ServiceId { get; }
       NoiDungHoiChanCls[] Reading(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter);
       void Add(RenderInfoCls ORenderInfo,  NoiDungHoiChanCls ONoiDungHoiChan);
       void Save(RenderInfoCls ORenderInfo,  string NoiDungHoiChanId,NoiDungHoiChanCls ONoiDungHoiChan);
       void Delete(RenderInfoCls ORenderInfo,  string NoiDungHoiChanId);
       NoiDungHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string NoiDungHoiChanId);
       string Duplicate(RenderInfoCls ORenderInfo, string NoiDungHoiChanId);
       long Count(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter);
       NoiDungHoiChanCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize);
   }
   
   public class NoiDungHoiChanTemplate : INoiDungHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual NoiDungHoiChanCls[] Reading(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, NoiDungHoiChanCls ONoiDungHoiChan) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string NoiDungHoiChanId, NoiDungHoiChanCls ONoiDungHoiChan) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string NoiDungHoiChanId) { }
       public virtual NoiDungHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string NoiDungHoiChanId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string NoiDungHoiChanId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter) { return 0; }
       public virtual NoiDungHoiChanCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize) { return null; }
   }
}
