using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_VanBangProcess
    {
        string ServiceId { get; }
        DT_VanBangCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_VanBangFilterCls ODT_VanBangFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DT_VanBangCls ODT_VanBang);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_VanBangId, DT_VanBangCls ODT_VanBang);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_VanBangId);
        DT_VanBangCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_VanBangId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_VanBangId);
    }

    public class DT_VanBangTemplate : IDT_VanBangProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_VanBangCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_VanBangFilterCls ODT_VanBangFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_VanBangCls ODT_VanBang) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_VanBangId, DT_VanBangCls ODT_VanBang) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_VanBangId) { }
        public virtual DT_VanBangCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_VanBangId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_VanBangId) { return null; }
    }
}
