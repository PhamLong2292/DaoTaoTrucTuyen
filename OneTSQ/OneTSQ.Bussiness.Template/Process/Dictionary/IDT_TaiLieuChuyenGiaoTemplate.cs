using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_TaiLieuChuyenGiaoProcess
    {
        string ServiceId { get; }
        DT_TaiLieuChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId);
        DT_TaiLieuChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId);
    }

    public class DT_TaiLieuChuyenGiaoTemplate : IDT_TaiLieuChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_TaiLieuChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId) { }
        public virtual DT_TaiLieuChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuChuyenGiaoId) { return null; }
    }
}
