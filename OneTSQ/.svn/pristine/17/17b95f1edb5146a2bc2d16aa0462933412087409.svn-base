using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface ITaiLieuProcess
   {
       string ServiceId { get; }
       TaiLieuCls[] Reading(RenderInfoCls ORenderInfo, TaiLieuFilterCls OTaiLieuFilter);
       void Add(RenderInfoCls ORenderInfo,  TaiLieuCls OTaiLieu);
       void Save(RenderInfoCls ORenderInfo,  string TaiLieuId,TaiLieuCls OTaiLieu);
       void Delete(RenderInfoCls ORenderInfo,  string TaiLieuId);
       TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuId);
       string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuId);
   }
   
   public class TaiLieuTemplate : ITaiLieuProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TaiLieuCls[] Reading(RenderInfoCls ORenderInfo, TaiLieuFilterCls OTaiLieuFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, TaiLieuCls OTaiLieu) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string TaiLieuId, TaiLieuCls OTaiLieu) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string TaiLieuId) { }
       public virtual TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuId) { return null; }
   }
}
