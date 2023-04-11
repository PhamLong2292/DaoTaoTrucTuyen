using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ICaBenhProcess
    {
        string ServiceId { get; }
        CaBenhCls[] Reading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter);
        CaBenhCls[] PageReading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter, ref long recordTotal);
        long Count(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter);
        void Add(ActionSqlParamCls ActionSqlParam, CaBenhCls OCaBenh);
        void Save(ActionSqlParamCls ActionSqlParam, string CaBenhId, CaBenhCls OCaBenh);
        void Delete(ActionSqlParamCls ActionSqlParam, string CaBenhId);
        CaBenhCls CreateModel(ActionSqlParamCls ActionSqlParam, string CaBenhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string CaBenhId);
        DataTable BCQuery(ActionSqlParamCls ActionSqlParam, string Query);
    }

    public class CaBenhTemplate : ICaBenhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual CaBenhCls[] Reading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter) { return null; }
        public virtual CaBenhCls[] PageReading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter, ref long recordTotal) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls OCaBenhFilter) { return -1; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, CaBenhCls OCaBenh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string CaBenhId, CaBenhCls OCaBenh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string CaBenhId) { }
        public virtual CaBenhCls CreateModel(ActionSqlParamCls ActionSqlParam, string CaBenhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string CaBenhId) { return null; }
        public virtual DataTable BCQuery(ActionSqlParamCls ActionSqlParam, string Query) { return null; }
    }
}
