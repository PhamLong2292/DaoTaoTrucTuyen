using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IMucDoPhongPhuBaiBaoCaoProcess
   {
       string ServiceId { get; }
       MucDoPhongPhuBaiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao);
       void Save(ActionSqlParamCls ActionSqlParam,  string MucDoPhongPhuBaiBaoCaoId,MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao);
       void Delete(ActionSqlParamCls ActionSqlParam,  string MucDoPhongPhuBaiBaoCaoId);
       MucDoPhongPhuBaiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId);
       long Count(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter);
       MucDoPhongPhuBaiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter, int PageIndex, int PageSize);
   }
   
   public class MucDoPhongPhuBaiBaoCaoTemplate : IMucDoPhongPhuBaiBaoCaoProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual MucDoPhongPhuBaiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId) { }
       public virtual MucDoPhongPhuBaiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string MucDoPhongPhuBaiBaoCaoId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter) { return 0; }
       public virtual MucDoPhongPhuBaiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter, int PageIndex, int PageSize) { return null; }
   }
}
