using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IHinhAnhProcess
   {
       string ServiceId { get; }
       HinhAnhCls[] Reading(RenderInfoCls ORenderInfo, HinhAnhFilterCls OHinhAnhFilter);
       HinhAnhCls[] PageReading(RenderInfoCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal);
       void Add(RenderInfoCls ORenderInfo,  HinhAnhCls OHinhAnh);
       void Save(RenderInfoCls ORenderInfo,  string HinhAnhId,HinhAnhCls OHinhAnh);
       void Delete(RenderInfoCls ORenderInfo,  string HinhAnhId);
       HinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string HinhAnhId);
       string Duplicate(RenderInfoCls ORenderInfo, string HinhAnhId);
   }
   
   public class HinhAnhTemplate : IHinhAnhProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual HinhAnhCls[] Reading(RenderInfoCls ORenderInfo, HinhAnhFilterCls OHinhAnhFilter) { return null; }
       public virtual HinhAnhCls[] PageReading(RenderInfoCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, HinhAnhCls OHinhAnh) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string HinhAnhId, HinhAnhCls OHinhAnh) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string HinhAnhId) { }
       public virtual HinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string HinhAnhId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string HinhAnhId) { return null; }
   }
}
