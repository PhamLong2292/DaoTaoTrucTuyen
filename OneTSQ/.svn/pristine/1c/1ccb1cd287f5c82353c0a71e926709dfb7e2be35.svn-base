using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_TieuChuanThamGiaKhoaHocProcess
    {
        string ServiceId { get; }
        DM_TieuChuanThamGiaKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        DM_TieuChuanThamGiaKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc);
        void Save(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc);
        void Delete(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId);
        DM_TieuChuanThamGiaKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId);
        DM_TieuChuanThamGiaKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter, ref int recordTotal);
        DM_TieuChuanThamGiaKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTieuChuanThamGiaKhoaHoc);
    }

    public class DM_TieuChuanThamGiaKhoaHocTemplate : IDM_TieuChuanThamGiaKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return 0; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId) { }
        public virtual DM_TieuChuanThamGiaKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TieuChuanThamGiaKhoaHocId) { return null; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_TieuChuanThamGiaKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTieuChuanThamGiaKhoaHoc) { return null; }
    }
}
