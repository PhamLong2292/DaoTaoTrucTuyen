using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ICaBenhProcess
    {
        string ServiceId { get; }
        CaBenhCls[] Reading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter);
        CaBenhCls[] PageReading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter, ref long recordTotal);
        long Count(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter);
        void Add(RenderInfoCls ORenderInfo, CaBenhCls OCaBenh);
        void Save(RenderInfoCls ORenderInfo, string CaBenhId, CaBenhCls OCaBenh);
        void Delete(RenderInfoCls ORenderInfo, string CaBenhId);
        CaBenhCls CreateModel(RenderInfoCls ORenderInfo, string CaBenhId);
        string Duplicate(RenderInfoCls ORenderInfo, string CaBenhId);
        DataTable BCQuery(RenderInfoCls ORenderInfo, string Query);
    }

    public class CaBenhTemplate : ICaBenhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual CaBenhCls[] Reading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter) { return null; }
        public virtual CaBenhCls[] PageReading(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter, ref long recordTotal) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, CaBenhFilterCls OCaBenhFilter) { return -1; }
        public virtual void Add(RenderInfoCls ORenderInfo, CaBenhCls OCaBenh) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string CaBenhId, CaBenhCls OCaBenh) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string CaBenhId) { }
        public virtual CaBenhCls CreateModel(RenderInfoCls ORenderInfo, string CaBenhId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string CaBenhId) { return null; }
        public virtual DataTable BCQuery(RenderInfoCls ORenderInfo, string Query) { return null; }
    }
}
