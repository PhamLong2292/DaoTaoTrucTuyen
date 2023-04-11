using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_KetQuaChuyenGiaoProcess
    {
        string ServiceId { get; }
        DT_KetQuaChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId);
        DT_KetQuaChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId);
    }

    public class DT_KetQuaChuyenGiaoTemplate : IDT_KetQuaChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_KetQuaChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId) { }
        public virtual DT_KetQuaChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_KetQuaChuyenGiaoId) { return null; }
    }
}
