using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface ITepDinhKemBlHinhAnhProcess
   {
       string ServiceId { get; }
       TepDinhKemBlHinhAnhCls[] Reading(RenderInfoCls ORenderInfo, TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter);
       void Add(RenderInfoCls ORenderInfo,  TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh);
       void Save(RenderInfoCls ORenderInfo,  string TepDinhKemBlHinhAnhId,TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh);
       void Delete(RenderInfoCls ORenderInfo,  string TepDinhKemBlHinhAnhId);
       TepDinhKemBlHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId);
       string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId);
   }
   
   public class TepDinhKemBlHinhAnhTemplate : ITepDinhKemBlHinhAnhProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual TepDinhKemBlHinhAnhCls[] Reading(RenderInfoCls ORenderInfo, TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId) { }
       public virtual TepDinhKemBlHinhAnhCls CreateModel(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string TepDinhKemBlHinhAnhId) { return null; }
   }
}
