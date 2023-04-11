using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_HocVienProcess
    {
        string ServiceId { get; }
        DT_HocVienCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_HocVienFilterCls ODT_HocVienFilter);
        DT_HocVienCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_HocVienFilterCls ODT_HocVienFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DT_HocVienCls ODT_HocVien);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_HocVienId, DT_HocVienCls ODT_HocVien);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_HocVienId);
        DT_HocVienCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_HocVienId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_HocVienId);
    }

    public class DT_HocVienTemplate : IDT_HocVienProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_HocVienCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_HocVienFilterCls ODT_HocVienFilter) { return null; }
        public virtual DT_HocVienCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_HocVienFilterCls ODT_HocVienFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_HocVienCls ODT_HocVien) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_HocVienId, DT_HocVienCls ODT_HocVien) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_HocVienId) { }
        public virtual DT_HocVienCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_HocVienId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_HocVienId) { return null; }
    }
}
