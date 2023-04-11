using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;
using OneTSQ.Database.Service;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDeTaiProcess
    {
        string ServiceId { get; }
        DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter);
        DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DeTaiCls ODeTai);
        void Save(RenderInfoCls ORenderInfo, string DeTaiId, DeTaiCls ODeTai);
        void Delete(RenderInfoCls ORenderInfo, string DeTaiId);
        DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DeTaiId);
        string Duplicate(RenderInfoCls ORenderInfo, string DeTaiId);
        long Count(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter);
        void UpdateLichXetDuyetID(RenderInfoCls ORenderInfo, string DelQuery, DbParam[] Params = null);
    }

    public class DeTaiTemplate : IDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter) { return null; }
        public virtual DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DeTaiCls ODeTai) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DeTaiId, DeTaiCls ODeTai) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DeTaiId) { }
        public virtual DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DeTaiId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DeTaiId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter) { return 0; }
        public virtual void UpdateLichXetDuyetID(RenderInfoCls ORenderInfo, string DelQuery, DbParam[] Params = null) { }
    }
}
