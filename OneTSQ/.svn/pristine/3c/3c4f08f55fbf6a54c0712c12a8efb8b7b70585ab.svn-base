using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ILapLichThanhVienHoiChanProcess
    {
        string ServiceId { get; }
        LapLichThanhVienHoiChanCls[] Reading(RenderInfoCls ORenderInfo, LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter);
        void Add(RenderInfoCls ORenderInfo, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan);
        void Save(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan);
        void Delete(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId);
        LapLichThanhVienHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId);
        string Duplicate(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId);
    }

    public class LapLichThanhVienHoiChanTemplate : ILapLichThanhVienHoiChanProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LapLichThanhVienHoiChanCls[] Reading(RenderInfoCls ORenderInfo, LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId) { }
        public virtual LapLichThanhVienHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string LapLichThanhVienHoiChanId) { return null; }
    }
}
