using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IDanhGiaThoiGianBuoiBaoCaoProcess
   {
       string ServiceId { get; }
       DanhGiaThoiGianBuoiBaoCaoCls[] Reading(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter);
       void Add(RenderInfoCls ORenderInfo,  DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao);
       void Save(RenderInfoCls ORenderInfo,  string DanhGiaThoiGianBuoiBaoCaoId,DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao);
       void Delete(RenderInfoCls ORenderInfo,  string DanhGiaThoiGianBuoiBaoCaoId);
       DanhGiaThoiGianBuoiBaoCaoCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId);
       string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId);
       long Count(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter);
       DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize);
   }
   
   public class DanhGiaThoiGianBuoiBaoCaoTemplate : IDanhGiaThoiGianBuoiBaoCaoProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls[] Reading(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId) { }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaThoiGianBuoiBaoCaoId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter) { return 0; }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize) { return null; }
   }
}
