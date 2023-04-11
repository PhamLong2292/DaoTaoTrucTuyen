﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;
using OneTSQ.Database.Service;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDeCuongProcess
    {
        string ServiceId { get; }
        DeCuongCls[] Reading(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter);
        DeCuongCls[] PageReading(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DeCuongCls ODeCuong);
        void Save(RenderInfoCls ORenderInfo, string DeCuongId, DeCuongCls ODeCuong);
        void Delete(RenderInfoCls ORenderInfo, string DeCuongId);
        DeCuongCls CreateModel(RenderInfoCls ORenderInfo, string DeCuongId);
        string Duplicate(RenderInfoCls ORenderInfo, string DeCuongId);
        long Count(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter);
        void UpdateLichXetDuyetID(RenderInfoCls ORenderInfo, string DelQuery, DbParam[] Params = null);
    }

    public class DeCuongTemplate : IDeCuongProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DeCuongCls[] Reading(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter) { return null; }
        public virtual DeCuongCls[] PageReading(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DeCuongCls ODeCuong) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DeCuongId, DeCuongCls ODeCuong) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DeCuongId) { }
        public virtual DeCuongCls CreateModel(RenderInfoCls ORenderInfo, string DeCuongId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DeCuongId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DeCuongFilterCls ODeCuongFilter) { return 0; }
        public virtual void UpdateLichXetDuyetID(RenderInfoCls ORenderInfo, string DelQuery, DbParam[] Params = null) { }
    }
}
