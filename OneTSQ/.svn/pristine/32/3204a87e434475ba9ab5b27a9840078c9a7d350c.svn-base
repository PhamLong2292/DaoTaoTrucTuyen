using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IBacSyOwnerUserProcess
    {
        string ServiceId { get; }
        BacSyOwnerUserCls[] Reading(RenderInfoCls ORenderInfo, BacSyOwnerUserFilterCls OBacSyOwnerUserFilter);
        void Add(RenderInfoCls ORenderInfo, BacSyOwnerUserCls OBacSyOwnerUser);
        void Save(RenderInfoCls ORenderInfo, string BacSyOwnerUserId, BacSyOwnerUserCls OBacSyOwnerUser);
        void Delete(RenderInfoCls ORenderInfo, string BacSyOwnerUserId);
        BacSyOwnerUserCls CreateModel(RenderInfoCls ORenderInfo, string BacSyOwnerUserId);
        string Duplicate(RenderInfoCls ORenderInfo, string BacSyOwnerUserId);
    }

    public class BacSyOwnerUserTemplate : IBacSyOwnerUserProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual BacSyOwnerUserCls[] Reading(RenderInfoCls ORenderInfo, BacSyOwnerUserFilterCls OBacSyOwnerUserFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, BacSyOwnerUserCls OBacSyOwnerUser) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string BacSyOwnerUserId, BacSyOwnerUserCls OBacSyOwnerUser) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string BacSyOwnerUserId) { }
        public virtual BacSyOwnerUserCls CreateModel(RenderInfoCls ORenderInfo, string BacSyOwnerUserId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string BacSyOwnerUserId) { return null; }
    }
}
