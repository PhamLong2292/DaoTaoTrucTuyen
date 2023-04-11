using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IMucDoYNghiaChuongTrinhDaoTaoProcess
   {
       string ServiceId { get; }
       MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter);
       void Add(RenderInfoCls ORenderInfo,  MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao);
       void Save(RenderInfoCls ORenderInfo,  string MucDoYNghiaChuongTrinhDaoTaoId,MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao);
       void Delete(RenderInfoCls ORenderInfo,  string MucDoYNghiaChuongTrinhDaoTaoId);
       MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId);
       string Duplicate(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId);
       long Count(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter);
       MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize);
   }
   
   public class MucDoYNghiaChuongTrinhDaoTaoTemplate : IMucDoYNghiaChuongTrinhDaoTaoProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId) { }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string MucDoYNghiaChuongTrinhDaoTaoId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter) { return 0; }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize) { return null; }
   }
}
