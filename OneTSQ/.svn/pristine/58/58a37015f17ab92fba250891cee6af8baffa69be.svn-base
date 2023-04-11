using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IButPheProcess
   {
       string ServiceId { get; }
       ButPheCls[] Reading(RenderInfoCls ORenderInfo, ButPheFilterCls OButPheFilter);
       ButPheCls[] PageReading(RenderInfoCls ORenderInfo, ButPheFilterCls OButPheFilter,ref long totalRow);
       void Add(RenderInfoCls ORenderInfo,  ButPheCls OButPhe);
       void Save(RenderInfoCls ORenderInfo,  string ButPheId,ButPheCls OButPhe);
       void Delete(RenderInfoCls ORenderInfo,  string ButPheId);
       ButPheCls CreateModel(RenderInfoCls ORenderInfo, string ButPheId);
       string Duplicate(RenderInfoCls ORenderInfo, string ButPheId);
   }
   
   public class ButPheTemplate : IButPheProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ButPheCls[] Reading(RenderInfoCls ORenderInfo, ButPheFilterCls OButPheFilter) { return null; }
       public virtual ButPheCls[] PageReading(RenderInfoCls ORenderInfo, ButPheFilterCls OButPheFilter, ref long totalRow) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, ButPheCls OButPhe) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string ButPheId, ButPheCls OButPhe) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string ButPheId) { }
       public virtual ButPheCls CreateModel(RenderInfoCls ORenderInfo, string ButPheId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string ButPheId) { return null; }
   }
}
