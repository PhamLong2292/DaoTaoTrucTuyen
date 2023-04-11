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
    public interface IDM_TieuChiThoiGianDaoTaoTtProcess
    {
        string ServiceId { get; }
        DM_TieuChiThoiGianDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter);
        long Count(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter);
        DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, int PageIndex, int PageSize);
        void Add(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt);
        void Save(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt);
        void Delete(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId);
        DM_TieuChiThoiGianDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId);
        string Duplicate(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId);
        DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal);
        DM_TieuChiThoiGianDaoTaoTtCls CheckCode(RenderInfoCls ORenderInfo, string MaTieuChiThoiGianDaoTaoTt);
    }

    public class DM_TieuChiThoiGianDaoTaoTtTemplate : IDM_TieuChiThoiGianDaoTaoTtProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter) { return 0; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, int PageIndex, int PageSize) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId) { }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string TieuChiThoiGianDaoTaoTtId) { return null; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal) { return null; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls CheckCode(RenderInfoCls ORenderInfo, string MaTieuChiThoiGianDaoTaoTt) { return null; }
    }
}
