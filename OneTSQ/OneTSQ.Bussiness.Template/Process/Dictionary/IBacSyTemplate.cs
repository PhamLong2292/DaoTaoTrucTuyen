using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IBacSyProcess
    {
        string ServiceId { get; }
        BacSyCls[] Reading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter);
        BacSyCls[] PageReading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, BacSyCls OBacSy);
        void Save(ActionSqlParamCls ActionSqlParam, string BacSyId, BacSyCls OBacSy);
        void Delete(ActionSqlParamCls ActionSqlParam, string BacSyId);
        BacSyCls CreateModel(ActionSqlParamCls ActionSqlParam, string BacSyId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string BacSyId);
    }

    public class BacSyTemplate : IBacSyProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual BacSyCls[] Reading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter) { return null; }
        public virtual BacSyCls[] PageReading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, BacSyCls OBacSy) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string BacSyId, BacSyCls OBacSy) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string BacSyId) { }
        public virtual BacSyCls CreateModel(ActionSqlParamCls ActionSqlParam, string BacSyId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string BacSyId) { return null; }
    }
}
