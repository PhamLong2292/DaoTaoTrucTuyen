using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IPhieuBaoCaoPhanUngCoHaiADRProcess
    {
        string ServiceId { get; }
        PhieuBaoCaoPhanUngCoHaiADRCls[] Reading(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter);
        PhieuBaoCaoPhanUngCoHaiADRCls[] PageReading(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter, ref long recordTotal);
        long Count(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter);
        void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR);
        void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR);
        void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId);
        PhieuBaoCaoPhanUngCoHaiADRCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId);
        string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId);
        DataTable BCQuery(RenderInfoCls ORenderInfo, string Query);
    }

    public class PhieuBaoCaoPhanUngCoHaiADRTemplate : IPhieuBaoCaoPhanUngCoHaiADRProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls[] Reading(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter) { return null; }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls[] PageReading(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter, ref long recordTotal) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter) { return -1; }
        public virtual void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId) { }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId) { return null; }
        public virtual DataTable BCQuery(RenderInfoCls ORenderInfo, string Query) { return null; }
    }
}
