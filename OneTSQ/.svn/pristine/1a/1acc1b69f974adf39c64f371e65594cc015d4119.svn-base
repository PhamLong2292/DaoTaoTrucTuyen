using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_DaoTaoVienTruongProcess
    {
        string ServiceId { get; }
        DT_DaoTaoVienTruongCls[] Reading(RenderInfoCls ORenderInfo, DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter);
        void Add(RenderInfoCls ORenderInfo, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong);
        void Save(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong);
        void Delete(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId);
        DT_DaoTaoVienTruongCls CreateModel(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId);
    }

    public class DT_DaoTaoVienTruongTemplate : IDT_DaoTaoVienTruongProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_DaoTaoVienTruongCls[] Reading(RenderInfoCls ORenderInfo, DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId) { }
        public virtual DT_DaoTaoVienTruongCls CreateModel(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId) { return null; }
    }
}
