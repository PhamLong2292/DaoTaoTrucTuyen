using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichLyThuyetProcess
    {
        string ServiceId { get; }
        DT_LichLyThuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetCls ODT_LichLyThuyet);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId, DT_LichLyThuyetCls ODT_LichLyThuyet);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId);
        DT_LichLyThuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId);
    }

    public class DT_LichLyThuyetTemplate : IDT_LichLyThuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichLyThuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetCls ODT_LichLyThuyet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId, DT_LichLyThuyetCls ODT_LichLyThuyet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId) { }
        public virtual DT_LichLyThuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichLyThuyetId) { return null; }
    }
}
