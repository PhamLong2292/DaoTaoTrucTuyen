using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_NhomKhoaHocProcess
    {
        string ServiceId { get; }
        DM_NhomKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        DM_NhomKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocCls ONhomKhoaHoc);
        void Save(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId, DM_NhomKhoaHocCls ONhomKhoaHoc);
        void Delete(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId);
        DM_NhomKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId);
        DM_NhomKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter, ref int recordTotal);
        DM_NhomKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaNhomKhoaHoc);
    }

    public class DM_NhomKhoaHocTemplate : IDM_NhomKhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_NhomKhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return 0; }
        public virtual DM_NhomKhoaHocCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocCls ONhomKhoaHoc) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId, DM_NhomKhoaHocCls ONhomKhoaHoc) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId) { }
        public virtual DM_NhomKhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string NhomKhoaHocId) { return null; }
        public virtual DM_NhomKhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter, ref int recordTotal) { return null; }
        public virtual DM_NhomKhoaHocCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaNhomKhoaHoc) { return null; }
    }
}
