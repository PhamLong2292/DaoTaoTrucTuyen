using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IThuocADRProcess
    {
        string ServiceId { get; }
        ThuocADRCls[] Reading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter);
        ThuocADRCls[] PageReading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, ThuocADRCls OThuocADR);
        void Save(RenderInfoCls ORenderInfo, string ThuocADRId, ThuocADRCls OThuocADR);
        void Delete(RenderInfoCls ORenderInfo, string ThuocADRId);
        ThuocADRCls CreateModel(RenderInfoCls ORenderInfo, string ThuocADRId);
        string Duplicate(RenderInfoCls ORenderInfo, string ThuocADRId);
        long Count(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter);
    }

    public class ThuocADRTemplate : IThuocADRProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual ThuocADRCls[] Reading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter) { return null; }
        public virtual ThuocADRCls[] PageReading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, ThuocADRCls OThuocADR) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string ThuocADRId, ThuocADRCls OThuocADR) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string ThuocADRId) { }
        public virtual ThuocADRCls CreateModel(RenderInfoCls ORenderInfo, string ThuocADRId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string ThuocADRId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter) { return 0; }
    }
}
