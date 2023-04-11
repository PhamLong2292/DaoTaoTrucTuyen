using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDoHieuQuaGiangDayProcess
    {
        string ServiceId { get; }
        DoHieuQuaGiangDayCls[] Reading(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay);
        void Save(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay);
        void Delete(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId);
        DoHieuQuaGiangDayCls CreateModel(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId);
        long Count(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter);
        DoHieuQuaGiangDayCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize);
    }

    public class DoHieuQuaGiangDayTemplate : IDoHieuQuaGiangDayProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DoHieuQuaGiangDayCls[] Reading(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId) { }
        public virtual DoHieuQuaGiangDayCls CreateModel(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DoHieuQuaGiangDayId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter) { return 0; }
        public virtual DoHieuQuaGiangDayCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize) { return null; }
    }
}
