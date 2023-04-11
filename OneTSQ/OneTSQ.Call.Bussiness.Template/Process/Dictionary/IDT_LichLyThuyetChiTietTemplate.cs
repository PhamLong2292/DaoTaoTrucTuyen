using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichLyThuyetChiTietProcess
    {
        string ServiceId { get; }
        DT_LichLyThuyetChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter);
        long Count(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter);
        void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet);
        void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId);
        DT_LichLyThuyetChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId);
    }

    public class DT_LichLyThuyetChiTietTemplate : IDT_LichLyThuyetChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichLyThuyetChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter) { return 0; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId) { }
        public virtual DT_LichLyThuyetChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetChiTietId) { return null; }
    }
}
