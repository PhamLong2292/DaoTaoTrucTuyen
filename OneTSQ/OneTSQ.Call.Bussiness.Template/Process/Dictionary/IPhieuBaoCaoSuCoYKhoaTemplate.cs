using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IPhieuBaoCaoSuCoYKhoaProcess
    {
        string ServiceId { get; }
        PhieuBaoCaoSuCoYKhoaCls[] Reading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter);
        PhieuBaoCaoSuCoYKhoaCls[] PageReading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa);
        void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa);
        void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId);
        PhieuBaoCaoSuCoYKhoaCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId);
        string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId);
        long Count(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter);
    }

    public class PhieuBaoCaoSuCoYKhoaTemplate : IPhieuBaoCaoSuCoYKhoaProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuBaoCaoSuCoYKhoaCls[] Reading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter) { return null; }
        public virtual PhieuBaoCaoSuCoYKhoaCls[] PageReading(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId) { }
        public virtual PhieuBaoCaoSuCoYKhoaCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoSuCoYKhoaId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter) { return 0; }
    }
}
