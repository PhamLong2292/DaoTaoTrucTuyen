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
    public interface IDM_TieuChuanThamGiaKhoaHocProcess
    {
        string ServiceId { get; }
        DM_TieuChuanThamGiaKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        long Count(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        DM_TieuChuanThamGiaKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        void Add(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc);
        void Save(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc);
        void Delete(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId);
        DM_TieuChuanThamGiaKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId);
        string Duplicate(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId);
        DM_TieuChuanThamGiaKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter, ref int recordTotal);
        DM_TieuChuanThamGiaKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTieuChuanThamGiaKhoaHoc);
    }

    public class DM_TieuChuanThamGiaKhoaHocTemplate : IDM_TieuChuanThamGiaKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return 0; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId) { }
        public virtual DM_TieuChuanThamGiaKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId) { return null; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTieuChuanThamGiaKhoaHoc) { return null; }
    }
}
