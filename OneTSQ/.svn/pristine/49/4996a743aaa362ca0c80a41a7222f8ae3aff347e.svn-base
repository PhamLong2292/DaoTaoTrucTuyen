using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichLyThuyetProcess
    {
        string ServiceId { get; }
        DT_LichLyThuyetCls[] Reading(RenderInfoCls ORenderInfo, DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter);
        void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetCls ODT_LichLyThuyet);
        void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId, DT_LichLyThuyetCls ODT_LichLyThuyet);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId);
        DT_LichLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId);
    }

    public class DT_LichLyThuyetTemplate : IDT_LichLyThuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichLyThuyetCls[] Reading(RenderInfoCls ORenderInfo, DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichLyThuyetCls ODT_LichLyThuyet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId, DT_LichLyThuyetCls ODT_LichLyThuyet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId) { }
        public virtual DT_LichLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichLyThuyetId) { return null; }
    }
}
