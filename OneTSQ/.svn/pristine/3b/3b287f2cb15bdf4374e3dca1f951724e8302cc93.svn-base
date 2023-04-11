using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IChuyenGiaHoiChanProcess
   {
       string ServiceId { get; }
       ChuyenGiaHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  ChuyenGiaHoiChanCls OChuyenGiaHoiChan);
       void Save(ActionSqlParamCls ActionSqlParam,  string ChuyenGiaHoiChanId,ChuyenGiaHoiChanCls OChuyenGiaHoiChan);
       void Delete(ActionSqlParamCls ActionSqlParam,  string ChuyenGiaHoiChanId);
       ChuyenGiaHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId);
   }
   
   public class ChuyenGiaHoiChanTemplate : IChuyenGiaHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ChuyenGiaHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, ChuyenGiaHoiChanCls OChuyenGiaHoiChan) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId, ChuyenGiaHoiChanCls OChuyenGiaHoiChan) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId) { }
       public virtual ChuyenGiaHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string ChuyenGiaHoiChanId) { return null; }
   }
}
