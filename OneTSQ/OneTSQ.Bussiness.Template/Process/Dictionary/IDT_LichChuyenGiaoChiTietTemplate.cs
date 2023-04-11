using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichChuyenGiaoChiTietProcess
    {
        string ServiceId { get; }
        DT_LichChuyenGiaoChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId);
        DT_LichChuyenGiaoChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId);
    }

    public class DT_LichChuyenGiaoChiTietTemplate : IDT_LichChuyenGiaoChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichChuyenGiaoChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId) { }
        public virtual DT_LichChuyenGiaoChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichChuyenGiaoChiTietId) { return null; }
    }
}
