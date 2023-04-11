using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_DiemDanhThucHanhProcess
    {
        string ServiceId { get; }
        DT_DiemDanhThucHanhCls[] Reading(RenderInfoCls ORenderInfo, DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter);
        void Add(RenderInfoCls ORenderInfo, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh);
        void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh);
        void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId);
        DT_DiemDanhThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId);
    }

    public class DT_DiemDanhThucHanhTemplate : IDT_DiemDanhThucHanhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_DiemDanhThucHanhCls[] Reading(RenderInfoCls ORenderInfo, DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId) { }
        public virtual DT_DiemDanhThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId) { return null; }
    }
}
