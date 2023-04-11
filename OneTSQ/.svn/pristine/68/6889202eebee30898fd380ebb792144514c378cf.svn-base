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
    public interface IDM_YKienBenhVienProcess
    {
        string ServiceId { get; }
        DM_YKienBenhVienCls[] Reading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        long Count(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        DM_YKienBenhVienCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        void Add(RenderInfoCls ORenderInfo, DM_YKienBenhVienCls OYKienBenhVien);
        void Save(RenderInfoCls ORenderInfo, string YKienBenhVienId, DM_YKienBenhVienCls OYKienBenhVien);
        void Delete(RenderInfoCls ORenderInfo, string YKienBenhVienId);
        DM_YKienBenhVienCls CreateModel(RenderInfoCls ORenderInfo, string YKienBenhVienId);
        string Duplicate(RenderInfoCls ORenderInfo, string YKienBenhVienId);
        DM_YKienBenhVienCls[] PageReading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal);
        DM_YKienBenhVienCls CheckCode(RenderInfoCls ORenderInfo, string MaYKienBenhVien);
    }

    public class DM_YKienBenhVienTemplate : IDM_YKienBenhVienProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_YKienBenhVienCls[] Reading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return 0; }
        public virtual DM_YKienBenhVienCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_YKienBenhVienCls OYKienBenhVien) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string YKienBenhVienId, DM_YKienBenhVienCls OYKienBenhVien) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string YKienBenhVienId) { }
        public virtual DM_YKienBenhVienCls CreateModel(RenderInfoCls ORenderInfo, string YKienBenhVienId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string YKienBenhVienId) { return null; }
        public virtual DM_YKienBenhVienCls[] PageReading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal) { return null; }
        public virtual DM_YKienBenhVienCls CheckCode(RenderInfoCls ORenderInfo, string MaYKienBenhVien) { return null; }
    }
}
