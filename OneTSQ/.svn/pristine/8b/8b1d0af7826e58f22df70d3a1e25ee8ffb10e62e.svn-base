using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IBinhLuanHinhAnhProcess
   {
       string ServiceId { get; }
       BinhLuanHinhAnhCls[] Reading(RenderInfoCls ORenderInfo, BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter);
       void Add(RenderInfoCls ORenderInfo,  BinhLuanHinhAnhCls OBinhLuanHinhAnh);
       void Save(RenderInfoCls ORenderInfo,  string BinhLuanHinhAnhId,BinhLuanHinhAnhCls OBinhLuanHinhAnh);
       void Delete(RenderInfoCls ORenderInfo,  string BinhLuanHinhAnhId);
       BinhLuanHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId);
       string Duplicate(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId);
   }
   
   public class BinhLuanHinhAnhTemplate : IBinhLuanHinhAnhProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BinhLuanHinhAnhCls[] Reading(RenderInfoCls ORenderInfo, BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, BinhLuanHinhAnhCls OBinhLuanHinhAnh) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId, BinhLuanHinhAnhCls OBinhLuanHinhAnh) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId) { }
       public virtual BinhLuanHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string BinhLuanHinhAnhId) { return null; }
   }
}
