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
    public interface IDM_GiayToDiChuyenGiaoProcess
    {
        string ServiceId { get; }
        DM_GiayToDiChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        long Count(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        DM_GiayToDiChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        void Add(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao);
        void Save(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao);
        void Delete(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId);
        DM_GiayToDiChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId);
        DM_GiayToDiChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter, ref int recordTotal);
        DM_GiayToDiChuyenGiaoCls CheckCode(RenderInfoCls ORenderInfo, string MaGiayToDiChuyenGiao);
    }

    public class DM_GiayToDiChuyenGiaoTemplate : IDM_GiayToDiChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_GiayToDiChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return 0; }
        public virtual DM_GiayToDiChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId) { }
        public virtual DM_GiayToDiChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string GiayToDiChuyenGiaoId) { return null; }
        public virtual DM_GiayToDiChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter, ref int recordTotal) { return null; }
        public virtual DM_GiayToDiChuyenGiaoCls CheckCode(RenderInfoCls ORenderInfo, string MaGiayToDiChuyenGiao) { return null; }
    }
}
