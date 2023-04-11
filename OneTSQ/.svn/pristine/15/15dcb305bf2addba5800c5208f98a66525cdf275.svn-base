using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IPhieuKhaoSatBenhVienVeTinhProcess
    {
        string ServiceId { get; }
        PhieuKhaoSatBenhVienVeTinhCls[] Reading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter);
        PhieuKhaoSatBenhVienVeTinhCls[] PageReading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh);
        void Save(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh);
        void Delete(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId);
        PhieuKhaoSatBenhVienVeTinhCls CreateModel(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId);
        string Duplicate(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId);
        long Count(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter);
    }

    public class PhieuKhaoSatBenhVienVeTinhTemplate : IPhieuKhaoSatBenhVienVeTinhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuKhaoSatBenhVienVeTinhCls[] Reading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter) { return null; }
        public virtual PhieuKhaoSatBenhVienVeTinhCls[] PageReading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId) { }
        public virtual PhieuKhaoSatBenhVienVeTinhCls CreateModel(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter) { return 0; }
    }
}
