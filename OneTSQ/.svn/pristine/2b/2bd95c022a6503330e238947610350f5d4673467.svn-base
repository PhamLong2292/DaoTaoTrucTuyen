using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IBacSyOwnerUserProcess
    {
        string ServiceId { get; }
        BacSyOwnerUserCls[] Reading(ActionSqlParamCls ActionSqlParam, BacSyOwnerUserFilterCls OBacSyOwnerUserFilter);
        void Add(ActionSqlParamCls ActionSqlParam, BacSyOwnerUserCls OBacSyOwnerUser);
        void Save(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId, BacSyOwnerUserCls OBacSyOwnerUser);
        void Delete(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId);
        BacSyOwnerUserCls CreateModel(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId);
    }

    public class BacSyOwnerUserTemplate : IBacSyOwnerUserProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual BacSyOwnerUserCls[] Reading(ActionSqlParamCls ActionSqlParam, BacSyOwnerUserFilterCls OBacSyOwnerUserFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, BacSyOwnerUserCls OBacSyOwnerUser) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId, BacSyOwnerUserCls OBacSyOwnerUser) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId) { }
        public virtual BacSyOwnerUserCls CreateModel(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string BacSyOwnerUserId) { return null; }
    }
}
