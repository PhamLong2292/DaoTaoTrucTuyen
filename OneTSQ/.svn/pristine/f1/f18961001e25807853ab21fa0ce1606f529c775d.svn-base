using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ICongTacVienDeTaiProcess
    {
        string ServiceId { get; }
        CongTacVienDeTaiCls[] Reading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter);
        CongTacVienDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, CongTacVienDeTaiCls OCongTacVienDeTai);
        void Save(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId, CongTacVienDeTaiCls OCongTacVienDeTai);
        void Delete(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId);
        CongTacVienDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId);
        string Duplicate(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId);
        long Count(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter);
    }

    public class CongTacVienDeTaiTemplate : ICongTacVienDeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual CongTacVienDeTaiCls[] Reading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter) { return null; }
        public virtual CongTacVienDeTaiCls[] PageReading(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, CongTacVienDeTaiCls OCongTacVienDeTai) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId, CongTacVienDeTaiCls OCongTacVienDeTai) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId) { }
        public virtual CongTacVienDeTaiCls CreateModel(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string CongTacVienDeTaiId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter) { return 0; }
    }
}
