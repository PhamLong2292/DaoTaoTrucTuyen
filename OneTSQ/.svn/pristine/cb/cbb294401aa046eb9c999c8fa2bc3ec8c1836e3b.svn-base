using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichThucHanhChiTietProcess
    {
        string ServiceId { get; }
        DT_LichThucHanhChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId);
        DT_LichThucHanhChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId);
    }

    public class DT_LichThucHanhChiTietTemplate : IDT_LichThucHanhChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichThucHanhChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter) { return 0; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId) { }
        public virtual DT_LichThucHanhChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhChiTietId) { return null; }
    }
}
