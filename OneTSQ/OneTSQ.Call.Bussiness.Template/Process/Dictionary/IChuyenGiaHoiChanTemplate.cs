using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IChuyenGiaHoiChanProcess
   {
       string ServiceId { get; }
       ChuyenGiaHoiChanCls[] Reading(RenderInfoCls ORenderInfo, ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter);
       void Add(RenderInfoCls ORenderInfo,  ChuyenGiaHoiChanCls OChuyenGiaHoiChan);
       void Save(RenderInfoCls ORenderInfo,  string ChuyenGiaHoiChanId,ChuyenGiaHoiChanCls OChuyenGiaHoiChan);
       void Delete(RenderInfoCls ORenderInfo,  string ChuyenGiaHoiChanId);
       ChuyenGiaHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId);
       string Duplicate(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId);
   }
   
   public class ChuyenGiaHoiChanTemplate : IChuyenGiaHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ChuyenGiaHoiChanCls[] Reading(RenderInfoCls ORenderInfo, ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, ChuyenGiaHoiChanCls OChuyenGiaHoiChan) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId, ChuyenGiaHoiChanCls OChuyenGiaHoiChan) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId) { }
       public virtual ChuyenGiaHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string ChuyenGiaHoiChanId) { return null; }
   }
}
