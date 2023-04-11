using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ITaiLieuDinhKemProcess
    {
        string ServiceId { get; }
        TaiLieuDinhKemCls[] Reading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter);
        TaiLieuDinhKemCls[] PageReading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, TaiLieuDinhKemCls OTaiLieuDinhKem);
        void Save(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId, TaiLieuDinhKemCls OTaiLieuDinhKem);
        void Delete(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId);
        TaiLieuDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId);
        string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId);
        long Count(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter);
    }

    public class TaiLieuDinhKemTemplate : ITaiLieuDinhKemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual TaiLieuDinhKemCls[] Reading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter) { return null; }
        public virtual TaiLieuDinhKemCls[] PageReading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, TaiLieuDinhKemCls OTaiLieuDinhKem) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId, TaiLieuDinhKemCls OTaiLieuDinhKem) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId) { }
        public virtual TaiLieuDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter) { return 0; }
    }
}
