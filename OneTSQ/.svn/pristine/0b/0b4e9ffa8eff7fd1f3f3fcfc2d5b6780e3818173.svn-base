using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_TaiLieuChuyenGiaoProcess
    {
        string ServiceId { get; }
        DT_TaiLieuChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter);
        void Add(RenderInfoCls ORenderInfo, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao);
        void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao);
        void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId);
        DT_TaiLieuChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId);
    }

    public class DT_TaiLieuChuyenGiaoTemplate : IDT_TaiLieuChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_TaiLieuChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId) { }
        public virtual DT_TaiLieuChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuChuyenGiaoId) { return null; }
    }
}
