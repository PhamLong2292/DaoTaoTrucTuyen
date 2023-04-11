using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IBacSyProcess
    {
        string ServiceId { get; }
        BacSyCls[] Reading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter);
        BacSyCls[] PageReading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, BacSyCls OBacSy);
        void Save(RenderInfoCls ORenderInfo, string BacSyId, BacSyCls OBacSy);
        void Delete(RenderInfoCls ORenderInfo, string BacSyId);
        BacSyCls CreateModel(RenderInfoCls ORenderInfo, string BacSyId);
        string Duplicate(RenderInfoCls ORenderInfo, string BacSyId);
    }

    public class BacSyTemplate : IBacSyProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual BacSyCls[] Reading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter) { return null; }
        public virtual BacSyCls[] PageReading(RenderInfoCls ORenderInfo, BacSyFilterCls OBacSyFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, BacSyCls OBacSy) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string BacSyId, BacSyCls OBacSy) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string BacSyId) { }
        public virtual BacSyCls CreateModel(RenderInfoCls ORenderInfo, string BacSyId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string BacSyId) { return null; }
    }
}
