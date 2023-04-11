using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ILichXetDuyetProcess
    {
        string ServiceId { get; }
        LichXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter);
        LichXetDuyetCls[] PageReadingDC(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal);
        LichXetDuyetCls[] PageReadingDT(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, LichXetDuyetCls OLichXetDuyet);
        void Save(RenderInfoCls ORenderInfo, string LichXetDuyetId, LichXetDuyetCls OLichXetDuyet);
        void Delete(RenderInfoCls ORenderInfo, string LichXetDuyetId);
        LichXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string LichXetDuyetId);
        string Duplicate(RenderInfoCls ORenderInfo, string LichXetDuyetId);
        long Count(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter);
    }

    public class LichXetDuyetTemplate : ILichXetDuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LichXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter) { return null; }
        public virtual LichXetDuyetCls[] PageReadingDC(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual LichXetDuyetCls[] PageReadingDT(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, LichXetDuyetCls OLichXetDuyet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string LichXetDuyetId, LichXetDuyetCls OLichXetDuyet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string LichXetDuyetId) { }
        public virtual LichXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string LichXetDuyetId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string LichXetDuyetId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter) { return 0; }
    }
}
