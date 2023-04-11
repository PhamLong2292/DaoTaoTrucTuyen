using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_DiemDanhLyThuyetProcess
    {
        string ServiceId { get; }
        DT_DiemDanhLyThuyetCls[] Reading(RenderInfoCls ORenderInfo, DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter);
        void Add(RenderInfoCls ORenderInfo, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet);
        void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet);
        void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId);
        DT_DiemDanhLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId);
    }

    public class DT_DiemDanhLyThuyetTemplate : IDT_DiemDanhLyThuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_DiemDanhLyThuyetCls[] Reading(RenderInfoCls ORenderInfo, DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId) { }
        public virtual DT_DiemDanhLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId) { return null; }
    }
}
