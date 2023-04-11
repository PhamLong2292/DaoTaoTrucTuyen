using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IBienBanHoiChanProcess
   {
       string ServiceId { get; }
       BienBanHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, BienBanHoiChanFilterCls OBienBanHoiChanFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  BienBanHoiChanCls OBienBanHoiChan);
       void Save(ActionSqlParamCls ActionSqlParam,  string BienBanHoiChanId,BienBanHoiChanCls OBienBanHoiChan);
       void Delete(ActionSqlParamCls ActionSqlParam,  string BienBanHoiChanId);
       BienBanHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId);
   }
   
   public class BienBanHoiChanTemplate : IBienBanHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BienBanHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, BienBanHoiChanFilterCls OBienBanHoiChanFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, BienBanHoiChanCls OBienBanHoiChan) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId, BienBanHoiChanCls OBienBanHoiChan) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId) { }
       public virtual BienBanHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string BienBanHoiChanId) { return null; }
   }
}
