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
    public interface IDM_TenKhoaHocProcess
    {
        string ServiceId { get; }
        DM_TenKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        long Count(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        DM_TenKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        void Add(RenderInfoCls ORenderInfo, DM_TenKhoaHocCls OTenKhoaHoc);
        void Save(RenderInfoCls ORenderInfo, string TenKhoaHocId, DM_TenKhoaHocCls OTenKhoaHoc);
        void Delete(RenderInfoCls ORenderInfo, string TenKhoaHocId);
        DM_TenKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TenKhoaHocId);
        string Duplicate(RenderInfoCls ORenderInfo, string TenKhoaHocId);
        DM_TenKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter, ref int recordTotal);
        DM_TenKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTenKhoaHoc);
    }

    public class DM_TenKhoaHocTemplate : IDM_TenKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TenKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return 0; }
        public virtual DM_TenKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_TenKhoaHocCls OTenKhoaHoc) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string TenKhoaHocId, DM_TenKhoaHocCls OTenKhoaHoc) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string TenKhoaHocId) { }
        public virtual DM_TenKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TenKhoaHocId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string TenKhoaHocId) { return null; }
        public virtual DM_TenKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_TenKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTenKhoaHoc) { return null; }
    }
}
