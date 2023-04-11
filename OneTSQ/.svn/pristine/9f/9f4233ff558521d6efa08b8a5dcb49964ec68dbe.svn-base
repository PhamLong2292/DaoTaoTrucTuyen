using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_KhoaHocProcess
    {
        string ServiceId { get; }
        DT_KhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter);
        DT_KhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal);
        DT_KhoaHocCls[] LopHocPageReading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DT_KhoaHocCls ODT_KhoaHoc);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId, DT_KhoaHocCls ODT_KhoaHoc);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId);
        DT_KhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId);
        long Count(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter);
    }

    public class DT_KhoaHocTemplate : IDT_KhoaHocProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KhoaHocCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter) { return null; }
        public virtual DT_KhoaHocCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal) { return null; }
        public virtual DT_KhoaHocCls[] LopHocPageReading(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_KhoaHocCls ODT_KhoaHoc) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId, DT_KhoaHocCls ODT_KhoaHoc) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId) { }
        public virtual DT_KhoaHocCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KhoaHocId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DT_KhoaHocFilterCls ODT_KhoaHocFilter) { return 0; }
    }
}
