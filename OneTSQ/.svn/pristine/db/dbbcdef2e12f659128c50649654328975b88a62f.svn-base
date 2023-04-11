using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface ITepTinProcess
   {
       string ServiceId { get; }
       TepTinCls[] Reading(RenderInfoCls ORenderInfo, TepTinFilterCls OTepTinFilter);
       void Add(RenderInfoCls ORenderInfo,  TepTinCls OTepTin);
       void Save(RenderInfoCls ORenderInfo,  string TepTinId,TepTinCls OTepTin);
       void Delete(RenderInfoCls ORenderInfo,  string TepTinId);
       TepTinCls CreateModel(RenderInfoCls ORenderInfo, string TepTinId);
       string Duplicate(RenderInfoCls ORenderInfo, string TepTinId);
   }
   
   public class TepTinTemplate : ITepTinProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepTinCls[] Reading(RenderInfoCls ORenderInfo, TepTinFilterCls OTepTinFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, TepTinCls OTepTin) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string TepTinId, TepTinCls OTepTin) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string TepTinId) { }
       public virtual TepTinCls CreateModel(RenderInfoCls ORenderInfo, string TepTinId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string TepTinId) { return null; }
   }
}
