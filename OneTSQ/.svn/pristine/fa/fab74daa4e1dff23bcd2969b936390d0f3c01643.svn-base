using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface ITepDinhKemProcess
   {
       string ServiceId { get; }
       TepDinhKemCls[] Reading(RenderInfoCls ORenderInfo, TepDinhKemFilterCls OTepDinhKemFilter);
       void Add(RenderInfoCls ORenderInfo,  TepDinhKemCls OTepDinhKem);
       void Save(RenderInfoCls ORenderInfo,  string TepDinhKemId,TepDinhKemCls OTepDinhKem);
       void Delete(RenderInfoCls ORenderInfo,  string TepDinhKemId);
       TepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemId);
       string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemId);
   }
   
   public class TepDinhKemTemplate : ITepDinhKemProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepDinhKemCls[] Reading(RenderInfoCls ORenderInfo, TepDinhKemFilterCls OTepDinhKemFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, TepDinhKemCls OTepDinhKem) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string TepDinhKemId, TepDinhKemCls OTepDinhKem) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string TepDinhKemId) { }
       public virtual TepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemId) { return null; }
   }
}
