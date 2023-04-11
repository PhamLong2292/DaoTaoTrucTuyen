using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_TaiLieuProcess
    {
        string ServiceId { get; }
        DT_TaiLieuCls[] Reading(RenderInfoCls ORenderInfo, DT_TaiLieuFilterCls ODT_TaiLieuFilter);
        void Add(RenderInfoCls ORenderInfo, DT_TaiLieuCls ODT_TaiLieu);
        void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuId, DT_TaiLieuCls ODT_TaiLieu);
        void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuId);
        DT_TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuId);
    }

    public class DT_TaiLieuTemplate : IDT_TaiLieuProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_TaiLieuCls[] Reading(RenderInfoCls ORenderInfo, DT_TaiLieuFilterCls ODT_TaiLieuFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_TaiLieuCls ODT_TaiLieu) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuId, DT_TaiLieuCls ODT_TaiLieu) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuId) { }
        public virtual DT_TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuId) { return null; }
    }
}
