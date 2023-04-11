using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IYKienChuyenGiaProcess
   {
       string ServiceId { get; }
       YKienChuyenGiaCls[] Reading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter);
       YKienChuyenGiaCls[] PageReading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal);
       YKienChuyenGiaCls[] GetChuyenGias(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter);
       void Add(RenderInfoCls ORenderInfo,  YKienChuyenGiaCls OYKienChuyenGia);
       void Save(RenderInfoCls ORenderInfo,  string YKienChuyenGiaId,YKienChuyenGiaCls OYKienChuyenGia);
       void Delete(RenderInfoCls ORenderInfo,  string YKienChuyenGiaId);
       YKienChuyenGiaCls CreateModel(RenderInfoCls ORenderInfo, string YKienChuyenGiaId);
       string Duplicate(RenderInfoCls ORenderInfo, string YKienChuyenGiaId);
   }
   
   public class YKienChuyenGiaTemplate : IYKienChuyenGiaProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual YKienChuyenGiaCls[] Reading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter) { return null; }
       public virtual YKienChuyenGiaCls[] PageReading(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal) { return null; }
       public virtual YKienChuyenGiaCls[] GetChuyenGias(RenderInfoCls ORenderInfo, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, YKienChuyenGiaCls OYKienChuyenGia) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string YKienChuyenGiaId, YKienChuyenGiaCls OYKienChuyenGia) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string YKienChuyenGiaId) { }
       public virtual YKienChuyenGiaCls CreateModel(RenderInfoCls ORenderInfo, string YKienChuyenGiaId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string YKienChuyenGiaId) { return null; }
   }
}
