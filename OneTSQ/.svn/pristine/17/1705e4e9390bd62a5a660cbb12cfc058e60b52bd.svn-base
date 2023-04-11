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
    public interface IDM_ChuyenKhoaDaoTaoTtProcess
    {
        string ServiceId { get; }
        DM_ChuyenKhoaDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        long Count(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        void Add(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt);
        void Save(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt);
        void Delete(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId);
        DM_ChuyenKhoaDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId);
        string Duplicate(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId);
        DM_ChuyenKhoaDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal);
        DM_ChuyenKhoaDaoTaoTtCls CheckCode(RenderInfoCls ORenderInfo, string MaChuyenKhoaDaoTaoTt);
    }

    public class DM_ChuyenKhoaDaoTaoTtTemplate : IDM_ChuyenKhoaDaoTaoTtProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] Reading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return 0; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId) { }
        public virtual DM_ChuyenKhoaDaoTaoTtCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string ChuyenKhoaDaoTaoTtId) { return null; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] PageReading(RenderInfoCls ORenderInfo, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal) { return null; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls CheckCode(RenderInfoCls ORenderInfo, string MaChuyenKhoaDaoTaoTt) { return null; }
    }
}
