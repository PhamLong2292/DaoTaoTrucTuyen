using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichThucHanhChiTietProcess
    {
        string ServiceId { get; }
        DT_LichThucHanhChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter);
        long Count(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter);
        void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet);
        void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId);
        DT_LichThucHanhChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId);
    }

    public class DT_LichThucHanhChiTietTemplate : IDT_LichThucHanhChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichThucHanhChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter) { return 0; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId) { }
        public virtual DT_LichThucHanhChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhChiTietId) { return null; }
    }
}
