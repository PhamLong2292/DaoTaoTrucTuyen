using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDangKyDeTaiProcess
    {
        string ServiceId { get; }
        DangKyDeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter);
        DangKyDeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DangKyDeTaiCls ODangKyDeTai);
        void Save(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId, DangKyDeTaiCls ODangKyDeTai);
        void Delete(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId);
        DangKyDeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId);
        long Count(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter);
    }

    public class DangKyDeTaiTemplate : IDangKyDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DangKyDeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter) { return null; }
        public virtual DangKyDeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DangKyDeTaiCls ODangKyDeTai) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId, DangKyDeTaiCls ODangKyDeTai) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId) { }
        public virtual DangKyDeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DangKyDeTaiId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DangKyDeTaiFilterCls ODangKyDeTaiFilter) { return 0; }
    }
}
