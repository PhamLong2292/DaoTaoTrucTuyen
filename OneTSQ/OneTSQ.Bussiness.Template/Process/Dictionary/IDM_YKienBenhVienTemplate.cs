using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_YKienBenhVienProcess
    {
        string ServiceId { get; }
        DM_YKienBenhVienCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        DM_YKienBenhVienCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienCls OYKienBenhVien);
        void Save(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId, DM_YKienBenhVienCls OYKienBenhVien);
        void Delete(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId);
        DM_YKienBenhVienCls CreateModel(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId);
        DM_YKienBenhVienCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal);
        DM_YKienBenhVienCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaYKienBenhVien);
    }

    public class DM_YKienBenhVienTemplate : IDM_YKienBenhVienProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_YKienBenhVienCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return 0; }
        public virtual DM_YKienBenhVienCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienCls OYKienBenhVien) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId, DM_YKienBenhVienCls OYKienBenhVien) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId) { }
        public virtual DM_YKienBenhVienCls CreateModel(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string YKienBenhVienId) { return null; }
        public virtual DM_YKienBenhVienCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal) { return null; }
        public virtual DM_YKienBenhVienCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaYKienBenhVien) { return null; }
    }
}
