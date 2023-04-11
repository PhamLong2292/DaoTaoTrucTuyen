using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_DiemDanhThucHanhProcess
    {
        string ServiceId { get; }
        DT_DiemDanhThucHanhCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId);
        DT_DiemDanhThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId);
    }

    public class DT_DiemDanhThucHanhTemplate : IDT_DiemDanhThucHanhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_DiemDanhThucHanhCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId) { }
        public virtual DT_DiemDanhThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_DiemDanhThucHanhId) { return null; }
    }
}
