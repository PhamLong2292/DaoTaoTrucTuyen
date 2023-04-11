using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_KetQuaDaoTaoProcess
    {
        string ServiceId { get; }
        DT_KetQuaDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
        DT_KetQuaDaoTaoCls[] ReadingDiemDanh(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
        DT_KetQuaDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId);
        DT_KetQuaDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId);
        bool? IsTrungThoiGianHoc(ActionSqlParamCls ActionSqlParam, string HocVienId, DateTime TuNgay, DateTime DenNgay, string KetQuaDaoTaoId);
        long Count(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter);
    }

    public class DT_KetQuaDaoTaoTemplate : IDT_KetQuaDaoTaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KetQuaDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return null; }
        public virtual DT_KetQuaDaoTaoCls[] ReadingDiemDanh(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return null; }
        public virtual DT_KetQuaDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId) { }
        public virtual DT_KetQuaDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KetQuaDaoTaoId) { return null; }
        public virtual bool? IsTrungThoiGianHoc(ActionSqlParamCls ActionSqlParam, string HocVienId, DateTime TuNgay, DateTime DenNgay, string KetQuaDaoTaoId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter) { return 0; }
    }
}
