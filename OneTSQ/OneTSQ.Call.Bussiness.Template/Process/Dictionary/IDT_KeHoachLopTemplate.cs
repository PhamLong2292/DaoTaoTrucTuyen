using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_KeHoachLopProcess
    {
        string ServiceId { get; }
        DT_KeHoachLopCls[] Reading(RenderInfoCls ORenderInfo, DT_KeHoachLopFilterCls ODT_KeHoachLopFilter);
        void Add(RenderInfoCls ORenderInfo, DT_KeHoachLopCls ODT_KeHoachLop);
        void Save(RenderInfoCls ORenderInfo, string DT_KeHoachLopId, DT_KeHoachLopCls ODT_KeHoachLop);
        void Delete(RenderInfoCls ORenderInfo, string DT_KeHoachLopId);
        DT_KeHoachLopCls CreateModel(RenderInfoCls ORenderInfo, string DT_KeHoachLopId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_KeHoachLopId);
    }

    public class DT_KeHoachLopTemplate : IDT_KeHoachLopProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KeHoachLopCls[] Reading(RenderInfoCls ORenderInfo, DT_KeHoachLopFilterCls ODT_KeHoachLopFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_KeHoachLopCls ODT_KeHoachLop) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_KeHoachLopId, DT_KeHoachLopCls ODT_KeHoachLop) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_KeHoachLopId) { }
        public virtual DT_KeHoachLopCls CreateModel(RenderInfoCls ORenderInfo, string DT_KeHoachLopId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_KeHoachLopId) { return null; }
    }
}
