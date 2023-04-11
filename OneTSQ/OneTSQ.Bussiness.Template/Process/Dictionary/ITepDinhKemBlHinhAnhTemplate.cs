using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface ITepDinhKemBlHinhAnhProcess
   {
       string ServiceId { get; }
       TepDinhKemBlHinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh);
       void Save(ActionSqlParamCls ActionSqlParam,  string TepDinhKemBlHinhAnhId,TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh);
       void Delete(ActionSqlParamCls ActionSqlParam,  string TepDinhKemBlHinhAnhId);
       TepDinhKemBlHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId);
   }
   
   public class TepDinhKemBlHinhAnhTemplate : ITepDinhKemBlHinhAnhProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepDinhKemBlHinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId) { }
       public virtual TepDinhKemBlHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TepDinhKemBlHinhAnhId) { return null; }
   }
}
