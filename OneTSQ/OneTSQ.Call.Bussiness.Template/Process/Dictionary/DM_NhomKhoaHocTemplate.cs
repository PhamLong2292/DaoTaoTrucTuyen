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
    public interface IDM_NhomKhoaHocProcess
    {
        string ServiceId { get; }
        DM_NhomKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        long Count(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        DM_NhomKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        void Add(RenderInfoCls ORenderInfo, DM_NhomKhoaHocCls ONhomKhoaHoc);
        void Save(RenderInfoCls ORenderInfo, string NhomKhoaHocId, DM_NhomKhoaHocCls ONhomKhoaHoc);
        void Delete(RenderInfoCls ORenderInfo, string NhomKhoaHocId);
        DM_NhomKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string NhomKhoaHocId);
        string Duplicate(RenderInfoCls ORenderInfo, string NhomKhoaHocId);
        DM_NhomKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter, ref int recordTotal);
        DM_NhomKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaNhomKhoaHoc);
    }

    public class DM_NhomKhoaHocTemplate : IDM_NhomKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_NhomKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return 0; }
        public virtual DM_NhomKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_NhomKhoaHocCls ONhomKhoaHoc) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string NhomKhoaHocId, DM_NhomKhoaHocCls ONhomKhoaHoc) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string NhomKhoaHocId) { }
        public virtual DM_NhomKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string NhomKhoaHocId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string NhomKhoaHocId) { return null; }
        public virtual DM_NhomKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_NhomKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaNhomKhoaHoc) { return null; }
    }
}
