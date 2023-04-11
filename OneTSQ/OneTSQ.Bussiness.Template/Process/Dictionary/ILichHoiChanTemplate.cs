using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ILichHoiChanProcess
    {
        string ServiceId { get; }
        LichHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter);
        LichHoiChanCls[] PageReading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, LichHoiChanCls OLichHoiChan);
        void Save(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, LichHoiChanCls OLichHoiChan);
        void Delete(ActionSqlParamCls ActionSqlParam, string LichHoiChanId);
        void AddChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens);
        void RemoveChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds);
        void AddCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds);
        void RemoveCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds);
        void UpdateCaBenhStt(ActionSqlParamCls ActionSqlParam, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs);
        LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId);
        LichHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string LichHoiChanId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string LichHoiChanId);
        bool IsTrucTiepHoiChan(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, string BacSyId);
    }

    public class LichHoiChanTemplate : ILichHoiChanProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LichHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter) { return null; }
        public virtual LichHoiChanCls[] PageReading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, LichHoiChanCls OLichHoiChan) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, LichHoiChanCls OLichHoiChan) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string LichHoiChanId) { }
        public virtual void AddChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens) { }
        public virtual void RemoveChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds) { }
        public virtual void AddCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds) { }
        public virtual void RemoveCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds) { }
        public virtual void UpdateCaBenhStt(ActionSqlParamCls ActionSqlParam, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs) { }
        public virtual LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId) { return null; }
        public virtual LichHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string LichHoiChanId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string LichHoiChanId) { return null; }
        public virtual bool IsTrucTiepHoiChan(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, string BacSyId) { return false; }
    }
}
