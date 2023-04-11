using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface ITaiLieuProcess
   {
       string ServiceId { get; }
       TaiLieuCls[] Reading(ActionSqlParamCls ActionSqlParam, TaiLieuFilterCls OTaiLieuFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  TaiLieuCls OTaiLieu);
       void Save(ActionSqlParamCls ActionSqlParam,  string TaiLieuId,TaiLieuCls OTaiLieu);
       void Delete(ActionSqlParamCls ActionSqlParam,  string TaiLieuId);
       TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string TaiLieuId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string TaiLieuId);
   }
   
   public class TaiLieuTemplate : ITaiLieuProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TaiLieuCls[] Reading(ActionSqlParamCls ActionSqlParam, TaiLieuFilterCls OTaiLieuFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, TaiLieuCls OTaiLieu) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string TaiLieuId, TaiLieuCls OTaiLieu) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TaiLieuId) { }
       public virtual TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string TaiLieuId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TaiLieuId) { return null; }
   }
}
