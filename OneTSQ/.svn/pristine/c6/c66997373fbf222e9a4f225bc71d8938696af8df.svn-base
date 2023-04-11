using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ICongTacVienDeTaiProcess
    {
        string ServiceId { get; }
        CongTacVienDeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter);
        CongTacVienDeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiCls OCongTacVienDeTai);
        void Save(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId, CongTacVienDeTaiCls OCongTacVienDeTai);
        void Delete(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId);
        CongTacVienDeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId);
        long Count(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter);
    }

    public class CongTacVienDeTaiTemplate : ICongTacVienDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual CongTacVienDeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter) { return null; }
        public virtual CongTacVienDeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiCls OCongTacVienDeTai) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId, CongTacVienDeTaiCls OCongTacVienDeTai) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId) { }
        public virtual CongTacVienDeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string CongTacVienDeTaiId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter) { return 0; }
    }
}
