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
    public interface IDeTaiProcess
    {
        string ServiceId { get; }
        DeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter);
        DeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DeTaiCls ODeTai);
        void Save(ActionSqlParamCls ActionSqlParam, string DeTaiId, DeTaiCls ODeTai);
        void Delete(ActionSqlParamCls ActionSqlParam, string DeTaiId);
        DeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DeTaiId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DeTaiId);
        long Count(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter);
        void UpdateLichXetDuyetID(ActionSqlParamCls ActionSqlParam, string DelQuery, DbParam[] Params = null);
    }

    public class DeTaiTemplate : IDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter) { return null; }
        public virtual DeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DeTaiCls ODeTai) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DeTaiId, DeTaiCls ODeTai) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DeTaiId) { }
        public virtual DeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DeTaiId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DeTaiId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DeTaiFilterCls ODeTaiFilter) { return 0; }
        public virtual void UpdateLichXetDuyetID(ActionSqlParamCls ActionSqlParam, string DelQuery, DbParam[] Params = null) { }
    }
}
