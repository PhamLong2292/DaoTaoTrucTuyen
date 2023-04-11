using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface INoiDungHoiChanProcess
   {
       string ServiceId { get; }
       NoiDungHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  NoiDungHoiChanCls ONoiDungHoiChan);
       void Save(ActionSqlParamCls ActionSqlParam,  string NoiDungHoiChanId,NoiDungHoiChanCls ONoiDungHoiChan);
       void Delete(ActionSqlParamCls ActionSqlParam,  string NoiDungHoiChanId);
       NoiDungHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId);
       long Count(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter);
       NoiDungHoiChanCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize);
   }
   
   public class NoiDungHoiChanTemplate : INoiDungHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual NoiDungHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanCls ONoiDungHoiChan) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId, NoiDungHoiChanCls ONoiDungHoiChan) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId) { }
       public virtual NoiDungHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string NoiDungHoiChanId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter) { return 0; }
       public virtual NoiDungHoiChanCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize) { return null; }
   }
}
