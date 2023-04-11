using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichLyThuyetChiTietProcess
    {
        string ServiceId { get; }
        DT_LichLyThuyetChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId);
        DT_LichLyThuyetChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId);
    }

    public class DT_LichLyThuyetChiTietTemplate : IDT_LichLyThuyetChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichLyThuyetChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter) { return 0; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId) { }
        public virtual DT_LichLyThuyetChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetChiTietId) { return null; }
    }
}
