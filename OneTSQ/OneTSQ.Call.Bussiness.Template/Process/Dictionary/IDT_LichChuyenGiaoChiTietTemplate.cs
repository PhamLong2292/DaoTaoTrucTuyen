using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichChuyenGiaoChiTietProcess
    {
        string ServiceId { get; }
        DT_LichChuyenGiaoChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter);
        void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet);
        void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId);
        DT_LichChuyenGiaoChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId);
    }

    public class DT_LichChuyenGiaoChiTietTemplate : IDT_LichChuyenGiaoChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichChuyenGiaoChiTietCls[] Reading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId) { }
        public virtual DT_LichChuyenGiaoChiTietCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoChiTietId) { return null; }
    }
}
