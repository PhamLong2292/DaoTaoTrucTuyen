using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ILichHoiChanProcess
    {
        string ServiceId { get; }
        LichHoiChanCls[] Reading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter);
        LichHoiChanCls[] PageReading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, LichHoiChanCls OLichHoiChan);
        void Save(RenderInfoCls ORenderInfo, string LichHoiChanId, LichHoiChanCls OLichHoiChan);
        void Delete(RenderInfoCls ORenderInfo, string LichHoiChanId);
        void AddChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens);
        void RemoveChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds);
        void AddCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds);
        void RemoveCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds);
        void UpdateCaBenhStt(RenderInfoCls ORenderInfo, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs);
        LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId);
        LichHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId);
        string Duplicate(RenderInfoCls ORenderInfo, string LichHoiChanId);
        bool IsTrucTiepHoiChan(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId);
    }

    public class LichHoiChanTemplate : ILichHoiChanProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LichHoiChanCls[] Reading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter) { return null; }
        public virtual LichHoiChanCls[] PageReading(RenderInfoCls ORenderInfo, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, LichHoiChanCls OLichHoiChan) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string LichHoiChanId, LichHoiChanCls OLichHoiChan) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string LichHoiChanId) { }
        public virtual void AddChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens) { }
        public virtual void RemoveChuyenGias(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] bacSyIds) { }
        public virtual void AddCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds) { }
        public virtual void RemoveCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId, string[] caBenhIds) { }
        public virtual void UpdateCaBenhStt(RenderInfoCls ORenderInfo, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs) { }
        public virtual LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(RenderInfoCls ORenderInfo, string lichHoiChanId) { return null; }
        public virtual LichHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LichHoiChanId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string LichHoiChanId) { return null; }
        public virtual bool IsTrucTiepHoiChan(RenderInfoCls ORenderInfo, string LichHoiChanId, string BacSyId) { return false; }
    }
}
