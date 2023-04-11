using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDaoTaoNhanLucProcess
    {
        string ServiceId { get; }
        DaoTaoNhanLucCls[] Reading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter);
        DaoTaoNhanLucCls[] PageReading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucCls ODaoTaoNhanLuc);
        void Save(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId, DaoTaoNhanLucCls ODaoTaoNhanLuc);
        void Delete(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId);
        DaoTaoNhanLucCls CreateModel(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId);
        long Count(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter);
    }

    public class DaoTaoNhanLucTemplate : IDaoTaoNhanLucProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DaoTaoNhanLucCls[] Reading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter) { return null; }
        public virtual DaoTaoNhanLucCls[] PageReading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucCls ODaoTaoNhanLuc) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId, DaoTaoNhanLucCls ODaoTaoNhanLuc) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId) { }
        public virtual DaoTaoNhanLucCls CreateModel(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DaoTaoNhanLucId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter) { return 0; }
    }
}
