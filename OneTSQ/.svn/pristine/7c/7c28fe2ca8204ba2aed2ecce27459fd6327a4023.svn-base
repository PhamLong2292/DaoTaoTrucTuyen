using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IBienBanHoiChanProcess
   {
       string ServiceId { get; }
       BienBanHoiChanCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanFilterCls OBienBanHoiChanFilter);
       void Add(RenderInfoCls ORenderInfo,  BienBanHoiChanCls OBienBanHoiChan);
       void Save(RenderInfoCls ORenderInfo,  string BienBanHoiChanId,BienBanHoiChanCls OBienBanHoiChan);
       void Delete(RenderInfoCls ORenderInfo,  string BienBanHoiChanId);
       BienBanHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanId);
       string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanId);
   }
   
   public class BienBanHoiChanTemplate : IBienBanHoiChanProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual BienBanHoiChanCls[] Reading(RenderInfoCls ORenderInfo, BienBanHoiChanFilterCls OBienBanHoiChanFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, BienBanHoiChanCls OBienBanHoiChan) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string BienBanHoiChanId, BienBanHoiChanCls OBienBanHoiChan) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string BienBanHoiChanId) { }
       public virtual BienBanHoiChanCls CreateModel(RenderInfoCls ORenderInfo, string BienBanHoiChanId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string BienBanHoiChanId) { return null; }
   }
}
