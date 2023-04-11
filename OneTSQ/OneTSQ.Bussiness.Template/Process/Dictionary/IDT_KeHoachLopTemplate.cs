using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_KeHoachLopProcess
    {
        string ServiceId { get; }
        DT_KeHoachLopCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KeHoachLopFilterCls ODT_KeHoachLopFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_KeHoachLopCls ODT_KeHoachLop);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId, DT_KeHoachLopCls ODT_KeHoachLop);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId);
        DT_KeHoachLopCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId);
    }

    public class DT_KeHoachLopTemplate : IDT_KeHoachLopProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KeHoachLopCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KeHoachLopFilterCls ODT_KeHoachLopFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_KeHoachLopCls ODT_KeHoachLop) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId, DT_KeHoachLopCls ODT_KeHoachLop) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId) { }
        public virtual DT_KeHoachLopCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KeHoachLopId) { return null; }
    }
}
