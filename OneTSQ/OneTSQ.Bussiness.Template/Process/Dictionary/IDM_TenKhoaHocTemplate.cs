using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_TenKhoaHocProcess
    {
        string ServiceId { get; }
        DM_TenKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        DM_TenKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocCls OTenKhoaHoc);
        void Save(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId, DM_TenKhoaHocCls OTenKhoaHoc);
        void Delete(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId);
        DM_TenKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId);
        DM_TenKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter, ref int recordTotal);
        DM_TenKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTenKhoaHoc);
    }

    public class DM_TenKhoaHocTemplate : IDM_TenKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TenKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return 0; }
        public virtual DM_TenKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocCls OTenKhoaHoc) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId, DM_TenKhoaHocCls OTenKhoaHoc) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId) { }
        public virtual DM_TenKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TenKhoaHocId) { return null; }
        public virtual DM_TenKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TenKhoaHocFilterCls OTenKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_TenKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTenKhoaHoc) { return null; }
    }
}
