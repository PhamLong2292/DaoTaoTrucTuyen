using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IMucDoYNghiaChuongTrinhDaoTaoProcess
   {
       string ServiceId { get; }
       MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao);
       void Save(ActionSqlParamCls ActionSqlParam,  string MucDoYNghiaChuongTrinhDaoTaoId,MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao);
       void Delete(ActionSqlParamCls ActionSqlParam,  string MucDoYNghiaChuongTrinhDaoTaoId);
       MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId);
       long Count(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter);
       MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize);
   }
   
   public class MucDoYNghiaChuongTrinhDaoTaoTemplate : IMucDoYNghiaChuongTrinhDaoTaoProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId) { }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string MucDoYNghiaChuongTrinhDaoTaoId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter) { return 0; }
       public virtual MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize) { return null; }
   }
}
