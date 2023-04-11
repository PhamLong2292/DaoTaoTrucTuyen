using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface ITepDinhKemProcess
   {
       string ServiceId { get; }
       TepDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, TepDinhKemFilterCls OTepDinhKemFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  TepDinhKemCls OTepDinhKem);
       void Save(ActionSqlParamCls ActionSqlParam,  string TepDinhKemId,TepDinhKemCls OTepDinhKem);
       void Delete(ActionSqlParamCls ActionSqlParam,  string TepDinhKemId);
       TepDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepDinhKemId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string TepDinhKemId);
   }
   
   public class TepDinhKemTemplate : ITepDinhKemProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, TepDinhKemFilterCls OTepDinhKemFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, TepDinhKemCls OTepDinhKem) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string TepDinhKemId, TepDinhKemCls OTepDinhKem) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TepDinhKemId) { }
       public virtual TepDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepDinhKemId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TepDinhKemId) { return null; }
   }
}
