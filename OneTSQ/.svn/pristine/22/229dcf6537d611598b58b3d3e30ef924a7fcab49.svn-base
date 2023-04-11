using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IButPheProcess
   {
       string ServiceId { get; }
       ButPheCls[] Reading(ActionSqlParamCls ActionSqlParam, ButPheFilterCls OButPheFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  ButPheCls OButPhe);
       void Save(ActionSqlParamCls ActionSqlParam,  string ButPheId,ButPheCls OButPhe);
       void Delete(ActionSqlParamCls ActionSqlParam,  string ButPheId);
       ButPheCls CreateModel(ActionSqlParamCls ActionSqlParam, string ButPheId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string ButPheId);
       ButPheCls[] PageReading(ActionSqlParamCls ActionSqlParam, ButPheFilterCls OButPheFilter, ref long totalRow);
    }

    public class ButPheTemplate : IButPheProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ButPheCls[] Reading(ActionSqlParamCls ActionSqlParam, ButPheFilterCls OButPheFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, ButPheCls OButPhe) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string ButPheId, ButPheCls OButPhe) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string ButPheId) { }
       public virtual ButPheCls CreateModel(ActionSqlParamCls ActionSqlParam, string ButPheId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string ButPheId) { return null; }
        public virtual ButPheCls[] PageReading(ActionSqlParamCls ActionSqlParam, ButPheFilterCls OButPheFilter, ref long totalRow) { return null; }
    }
}
