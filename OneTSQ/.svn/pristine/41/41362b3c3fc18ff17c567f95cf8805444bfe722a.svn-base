using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_KhoaHocProcess
    {
        string ServiceId { get; }
        DT_KhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter);
        DT_KhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal);
        DT_KhoaHocCls[] LopHocPageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DT_KhoaHocCls ODT_KhoaHoc);
        void Save(RenderInfoCls ORenderInfo, string DT_KhoaHocId, DT_KhoaHocCls ODT_KhoaHoc);
        void Delete(RenderInfoCls ORenderInfo, string DT_KhoaHocId);
        DT_KhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string DT_KhoaHocId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_KhoaHocId);
        long Count(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter);
    }

    public class DT_KhoaHocTemplate : IDT_KhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter) { return null; }
        public virtual DT_KhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal) { return null; }
        public virtual DT_KhoaHocCls[] LopHocPageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_KhoaHocCls ODT_KhoaHoc) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_KhoaHocId, DT_KhoaHocCls ODT_KhoaHoc) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_KhoaHocId) { }
        public virtual DT_KhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string DT_KhoaHocId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_KhoaHocId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter) { return 0; }
    }
}
