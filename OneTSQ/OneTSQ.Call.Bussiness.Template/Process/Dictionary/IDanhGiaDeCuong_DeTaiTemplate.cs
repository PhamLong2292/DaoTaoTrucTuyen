using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDanhGiaDeCuong_DeTaiProcess
    {
        string ServiceId { get; }
        DanhGiaDeCuong_DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter);
        DanhGiaDeCuong_DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai);
        void Save(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai);
        void Delete(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId);
        DanhGiaDeCuong_DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId);
        string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId);
        long Count(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter);
    }

    public class DanhGiaDeCuong_DeTaiTemplate : IDanhGiaDeCuong_DeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DanhGiaDeCuong_DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter) { return null; }
        public virtual DanhGiaDeCuong_DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId) { }
        public virtual DanhGiaDeCuong_DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter) { return 0; }
    }
}
