using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface ITepTinProcess
   {
       string ServiceId { get; }
       TepTinCls[] Reading(ActionSqlParamCls ActionSqlParam, TepTinFilterCls OTepTinFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  TepTinCls OTepTin);
       void Save(ActionSqlParamCls ActionSqlParam,  string TepTinId,TepTinCls OTepTin);
       void Delete(ActionSqlParamCls ActionSqlParam,  string TepTinId);
       TepTinCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepTinId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string TepTinId);
   }
   
   public class TepTinTemplate : ITepTinProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepTinCls[] Reading(ActionSqlParamCls ActionSqlParam, TepTinFilterCls OTepTinFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, TepTinCls OTepTin) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string TepTinId, TepTinCls OTepTin) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TepTinId) { }
       public virtual TepTinCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepTinId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TepTinId) { return null; }
   }
}
