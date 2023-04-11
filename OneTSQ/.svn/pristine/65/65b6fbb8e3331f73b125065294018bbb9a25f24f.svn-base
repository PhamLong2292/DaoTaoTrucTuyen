using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IPhieuPhanTichNguyenNhanSuCoProcess
    {
        string ServiceId { get; }
        PhieuPhanTichNguyenNhanSuCoCls[] Reading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter);
        PhieuPhanTichNguyenNhanSuCoCls[] PageReading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo);
        void Save(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo);
        void Delete(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId);
        PhieuPhanTichNguyenNhanSuCoCls CreateModel(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId);
        string Duplicate(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId);
        long Count(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter);
    }

    public class PhieuPhanTichNguyenNhanSuCoTemplate : IPhieuPhanTichNguyenNhanSuCoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuPhanTichNguyenNhanSuCoCls[] Reading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter) { return null; }
        public virtual PhieuPhanTichNguyenNhanSuCoCls[] PageReading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId) { }
        public virtual PhieuPhanTichNguyenNhanSuCoCls CreateModel(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter) { return 0; }
    }
}
