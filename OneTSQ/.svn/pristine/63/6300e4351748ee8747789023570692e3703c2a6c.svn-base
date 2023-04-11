using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichChuyenGiaoProcess
    {
        string ServiceId { get; }
        DT_LichChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter);
        DT_LichChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoCls ODT_LichChuyenGiao);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId, DT_LichChuyenGiaoCls ODT_LichChuyenGiao);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId);
        DT_LichChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId);
    }

    public class DT_LichChuyenGiaoTemplate : IDT_LichChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter) { return null; }
        public virtual DT_LichChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoCls ODT_LichChuyenGiao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId, DT_LichChuyenGiaoCls ODT_LichChuyenGiao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId) { }
        public virtual DT_LichChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoId) { return null; }
    }
}
