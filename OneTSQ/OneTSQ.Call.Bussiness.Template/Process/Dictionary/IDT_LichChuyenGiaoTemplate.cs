using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichChuyenGiaoProcess
    {
        string ServiceId { get; }
        DT_LichChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter);
        DT_LichChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoCls ODT_LichChuyenGiao);
        void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId, DT_LichChuyenGiaoCls ODT_LichChuyenGiao);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId);
        DT_LichChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId);
    }

    public class DT_LichChuyenGiaoTemplate : IDT_LichChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter) { return null; }
        public virtual DT_LichChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichChuyenGiaoCls ODT_LichChuyenGiao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId, DT_LichChuyenGiaoCls ODT_LichChuyenGiao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId) { }
        public virtual DT_LichChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichChuyenGiaoId) { return null; }
    }
}
