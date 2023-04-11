using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_KetQuaDaoTaoProcess
    {
        string ServiceId { get; }
        DT_KetQuaDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
        DT_KetQuaDaoTaoCls[] ReadingDiemDanh(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
        DT_KetQuaDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao);
        void Save(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao);
        void Delete(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId);
        DT_KetQuaDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId);
        bool? IsTrungThoiGianHoc(RenderInfoCls ORenderInfo, string HocVienId, DateTime TuNgay, DateTime DenNgay, string KetQuaDaoTaoId);
        long Count(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
    }

    public class DT_KetQuaDaoTaoTemplate : IDT_KetQuaDaoTaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KetQuaDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return null; }
        public virtual DT_KetQuaDaoTaoCls[] ReadingDiemDanh(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return null; }
        public virtual DT_KetQuaDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId) { }
        public virtual DT_KetQuaDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId) { return null; }
        public virtual bool? IsTrungThoiGianHoc(RenderInfoCls ORenderInfo, string HocVienId, DateTime TuNgay, DateTime DenNgay, string KetQuaDaoTaoId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return 0; }
    }
}
