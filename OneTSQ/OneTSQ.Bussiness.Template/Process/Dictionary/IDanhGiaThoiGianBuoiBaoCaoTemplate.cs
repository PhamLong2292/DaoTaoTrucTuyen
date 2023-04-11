using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IDanhGiaThoiGianBuoiBaoCaoProcess
   {
       string ServiceId { get; }
       DanhGiaThoiGianBuoiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao);
       void Save(ActionSqlParamCls ActionSqlParam,  string DanhGiaThoiGianBuoiBaoCaoId,DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao);
       void Delete(ActionSqlParamCls ActionSqlParam,  string DanhGiaThoiGianBuoiBaoCaoId);
       DanhGiaThoiGianBuoiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId);
       long Count(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter);
       DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize);
   }
   
   public class DanhGiaThoiGianBuoiBaoCaoTemplate : IDanhGiaThoiGianBuoiBaoCaoProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId) { }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DanhGiaThoiGianBuoiBaoCaoId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter) { return 0; }
       public virtual DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize) { return null; }
   }
}
