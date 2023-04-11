using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IYKienChuyenGiaProcess
   {
       string ServiceId { get; }
       YKienChuyenGiaCls[] Reading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter);
       YKienChuyenGiaCls[] PageReading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal);
       YKienChuyenGiaCls[] GetChuyenGias(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  YKienChuyenGiaCls OYKienChuyenGia);
       void Save(ActionSqlParamCls ActionSqlParam,  string YKienChuyenGiaId,YKienChuyenGiaCls OYKienChuyenGia);
       void Delete(ActionSqlParamCls ActionSqlParam,  string YKienChuyenGiaId);
       YKienChuyenGiaCls CreateModel(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId);
   }
   
   public class YKienChuyenGiaTemplate : IYKienChuyenGiaProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual YKienChuyenGiaCls[] Reading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter) { return null; }
       public virtual YKienChuyenGiaCls[] PageReading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal) { return null; }
       public virtual YKienChuyenGiaCls[] GetChuyenGias(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaCls OYKienChuyenGia) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId, YKienChuyenGiaCls OYKienChuyenGia) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId) { }
       public virtual YKienChuyenGiaCls CreateModel(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string YKienChuyenGiaId) { return null; }
   }
}
