using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_LichThucHanhProcess
    {
        string ServiceId { get; }
        DT_LichThucHanhCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhFilterCls ODT_LichThucHanhFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhCls ODT_LichThucHanh);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, DT_LichThucHanhCls ODT_LichThucHanh);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId);
        DT_LichThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId);
        DT_HocVienCls[] GetHocViens(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId);
        int GetHocVienQuantity(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId);
        void AddHocViens(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens);
        void RemoveHocViens(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, string[] HocVienIds);
        void DeleteHocVien(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, string HocVienId);
    }

    public class DT_LichThucHanhTemplate : IDT_LichThucHanhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_LichThucHanhCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhFilterCls ODT_LichThucHanhFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhCls ODT_LichThucHanh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, DT_LichThucHanhCls ODT_LichThucHanh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId) { }
        public virtual DT_LichThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId) { return null; }
        public virtual DT_HocVienCls[] GetHocViens(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId) { return null; }
        public virtual int GetHocVienQuantity(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId) { return 0; }
        public virtual void AddHocViens(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens) { }
        public virtual void RemoveHocViens(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, string[] HocVienIds) { }
        public virtual void DeleteHocVien(ActionSqlParamCls ActionSqlParam, string DT_LichThucHanhId, string HocVienId) { }
    }
}
