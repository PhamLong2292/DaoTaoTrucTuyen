using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;
using OneTSQ.Database.Service;

namespace OneTSQ.Bussiness.Template
{
    public interface IDeCuongProcess
    {
        string ServiceId { get; }
        DeCuongCls[] Reading(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter);
        DeCuongCls[] PageReading(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DeCuongCls ODeCuong);
        void Save(ActionSqlParamCls ActionSqlParam, string DeCuongId, DeCuongCls ODeCuong);
        void Delete(ActionSqlParamCls ActionSqlParam, string DeCuongId);
        DeCuongCls CreateModel(ActionSqlParamCls ActionSqlParam, string DeCuongId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DeCuongId);
        long Count(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter);
        void UpdateLichXetDuyetID(ActionSqlParamCls ActionSqlParam, string DelQuery, DbParam[] Params = null);
    }

    public class DeCuongTemplate : IDeCuongProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DeCuongCls[] Reading(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter) { return null; }
        public virtual DeCuongCls[] PageReading(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DeCuongCls ODeCuong) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DeCuongId, DeCuongCls ODeCuong) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DeCuongId) { }
        public virtual DeCuongCls CreateModel(ActionSqlParamCls ActionSqlParam, string DeCuongId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DeCuongId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DeCuongFilterCls ODeCuongFilter) { return 0; }
        public virtual void UpdateLichXetDuyetID(ActionSqlParamCls ActionSqlParam, string DelQuery, DbParam[] Params = null) { }
    }
}
