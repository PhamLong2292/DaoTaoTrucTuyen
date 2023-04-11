using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDM_TrangThietBiTruyenHinhTtProcess
    {
        string ServiceId { get; }
        DM_TrangThietBiTruyenHinhTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter);
        long Count(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter);
        DM_TrangThietBiTruyenHinhTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter, int PageIndex, int PageSize);
        void Add(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt);
        void Save(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt);
        void Delete(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId);
        DM_TrangThietBiTruyenHinhTtCls CreateModel(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId);
        string Duplicate(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId);
        DM_TrangThietBiTruyenHinhTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter, ref int recordTotal);
        DM_TrangThietBiTruyenHinhTtCls CheckCode(RenderInfoCls ORenderInfo, string MaTrangThietBiTruyenHinhTt);
    }

    public class DM_TrangThietBiTruyenHinhTtTemplate : IDM_TrangThietBiTruyenHinhTtProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TrangThietBiTruyenHinhTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter) { return 0; }
        public virtual DM_TrangThietBiTruyenHinhTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter, int PageIndex, int PageSize) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId, DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId) { }
        public virtual DM_TrangThietBiTruyenHinhTtCls CreateModel(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string TrangThietBiTruyenHinhTtId) { return null; }
        public virtual DM_TrangThietBiTruyenHinhTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter, ref int recordTotal) { return null; }
        public virtual DM_TrangThietBiTruyenHinhTtCls CheckCode(RenderInfoCls ORenderInfo, string MaTrangThietBiTruyenHinhTt) { return null; }
    }
}
