﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_LichThucHanhProcess
    {
        string ServiceId { get; }
        DT_LichThucHanhCls[] Reading(RenderInfoCls ORenderInfo, DT_LichThucHanhFilterCls ODT_LichThucHanhFilter);
        void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhCls ODT_LichThucHanh);
        void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, DT_LichThucHanhCls ODT_LichThucHanh);
        void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhId);
        DT_LichThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhId);
        DT_HocVienCls[] GetHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId);
        int GetHocVienQuantity(RenderInfoCls ORenderInfo, string DT_LichThucHanhId);
        void AddHocViens(RenderInfoCls ORenderInfo, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens);
        void RemoveHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string[] HocVienIds);
        void DeleteHocVien(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string HocVienId);
    }

    public class DT_LichThucHanhTemplate : IDT_LichThucHanhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichThucHanhCls[] Reading(RenderInfoCls ORenderInfo, DT_LichThucHanhFilterCls ODT_LichThucHanhFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhCls ODT_LichThucHanh) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, DT_LichThucHanhCls ODT_LichThucHanh) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhId) { }
        public virtual DT_LichThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhId) { return null; }
        public virtual DT_HocVienCls[] GetHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId) { return null; }
        public virtual int GetHocVienQuantity(RenderInfoCls ORenderInfo, string DT_LichThucHanhId) { return 0; }
        public virtual void AddHocViens(RenderInfoCls ORenderInfo, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens) { }
        public virtual void RemoveHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string[] HocVienIds) { }
        public virtual void DeleteHocVien(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string HocVienId) { }
    }
}
