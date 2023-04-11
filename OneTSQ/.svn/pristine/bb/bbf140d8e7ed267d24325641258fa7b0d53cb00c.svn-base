using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_TaiLieuProcess
    {
        string ServiceId { get; }
        DT_TaiLieuCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_TaiLieuFilterCls ODT_TaiLieuFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuCls ODT_TaiLieu);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId, DT_TaiLieuCls ODT_TaiLieu);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId);
        DT_TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId);
    }

    public class DT_TaiLieuTemplate : IDT_TaiLieuProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_TaiLieuCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_TaiLieuFilterCls ODT_TaiLieuFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuCls ODT_TaiLieu) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId, DT_TaiLieuCls ODT_TaiLieu) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId) { }
        public virtual DT_TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_TaiLieuId) { return null; }
    }
}
