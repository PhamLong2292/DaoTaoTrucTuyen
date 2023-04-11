using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_DaoTaoVienTruongProcess
    {
        string ServiceId { get; }
        DT_DaoTaoVienTruongCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId);
        DT_DaoTaoVienTruongCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId);
    }

    public class DT_DaoTaoVienTruongTemplate : IDT_DaoTaoVienTruongProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_DaoTaoVienTruongCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId) { }
        public virtual DT_DaoTaoVienTruongCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_DaoTaoVienTruongId) { return null; }
    }
}
