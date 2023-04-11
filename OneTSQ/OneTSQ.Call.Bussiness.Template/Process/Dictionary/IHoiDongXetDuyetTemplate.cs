using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IHoiDongXetDuyetProcess
    {
        string ServiceId { get; }
        HoiDongXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter);
        HoiDongXetDuyetCls[] PageReading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, HoiDongXetDuyetCls OHoiDongXetDuyet);
        void Save(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId, HoiDongXetDuyetCls OHoiDongXetDuyet);
        void Delete(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId);
        HoiDongXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId);
        string Duplicate(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId);
        long Count(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter);
    }

    public class HoiDongXetDuyetTemplate : IHoiDongXetDuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual HoiDongXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter) { return null; }
        public virtual HoiDongXetDuyetCls[] PageReading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, HoiDongXetDuyetCls OHoiDongXetDuyet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId, HoiDongXetDuyetCls OHoiDongXetDuyet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId) { }
        public virtual HoiDongXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter) { return 0; }
    }
}
