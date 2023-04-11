using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDangKyDeTaiProcess
    {
        string ServiceId { get; }
        DangKyDeTaiCls[] Reading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter);
        DangKyDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DangKyDeTaiCls ODangKyDeTai);
        void Save(RenderInfoCls ORenderInfo, string DangKyDeTaiId, DangKyDeTaiCls ODangKyDeTai);
        void Delete(RenderInfoCls ORenderInfo, string DangKyDeTaiId);
        DangKyDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DangKyDeTaiId);
        string Duplicate(RenderInfoCls ORenderInfo, string DangKyDeTaiId);
        long Count(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter);
    }

    public class DangKyDeTaiTemplate : IDangKyDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DangKyDeTaiCls[] Reading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter) { return null; }
        public virtual DangKyDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DangKyDeTaiCls ODangKyDeTai) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DangKyDeTaiId, DangKyDeTaiCls ODangKyDeTai) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DangKyDeTaiId) { }
        public virtual DangKyDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DangKyDeTaiId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DangKyDeTaiId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DangKyDeTaiFilterCls ODangKyDeTaiFilter) { return 0; }
    }
}
