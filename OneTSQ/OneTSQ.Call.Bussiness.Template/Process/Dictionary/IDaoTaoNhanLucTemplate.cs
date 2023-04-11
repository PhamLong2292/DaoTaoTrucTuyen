using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDaoTaoNhanLucProcess
    {
        string ServiceId { get; }
        DaoTaoNhanLucCls[] Reading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter);
        DaoTaoNhanLucCls[] PageReading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DaoTaoNhanLucCls ODaoTaoNhanLuc);
        void Save(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId, DaoTaoNhanLucCls ODaoTaoNhanLuc);
        void Delete(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId);
        DaoTaoNhanLucCls CreateModel(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId);
        string Duplicate(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId);
        long Count(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter);
    }

    public class DaoTaoNhanLucTemplate : IDaoTaoNhanLucProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DaoTaoNhanLucCls[] Reading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter) { return null; }
        public virtual DaoTaoNhanLucCls[] PageReading(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DaoTaoNhanLucCls ODaoTaoNhanLuc) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId, DaoTaoNhanLucCls ODaoTaoNhanLuc) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId) { }
        public virtual DaoTaoNhanLucCls CreateModel(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DaoTaoNhanLucId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter) { return 0; }
    }
}
