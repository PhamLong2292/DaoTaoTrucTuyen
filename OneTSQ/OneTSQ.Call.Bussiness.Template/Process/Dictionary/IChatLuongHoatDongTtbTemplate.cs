using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
   public interface IChatLuongHoatDongTtbProcess
   {
       string ServiceId { get; }
       ChatLuongHoatDongTtbCls[] Reading(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter);
       void Add(RenderInfoCls ORenderInfo,  ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb);
       void Save(RenderInfoCls ORenderInfo,  string ChatLuongHoatDongTtbId,ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb);
       void Delete(RenderInfoCls ORenderInfo,  string ChatLuongHoatDongTtbId);
       ChatLuongHoatDongTtbCls CreateModel(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId);
       string Duplicate(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId);
       long Count(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter);
       ChatLuongHoatDongTtbCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize);
   }
   
   public class ChatLuongHoatDongTtbTemplate : IChatLuongHoatDongTtbProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ChatLuongHoatDongTtbCls[] Reading(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter) { return null; }
       public virtual void Add(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb) { }
       public virtual void Save(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb) { }
       public virtual void Delete(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId) { }
       public virtual ChatLuongHoatDongTtbCls CreateModel(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId) { return null; }
       public virtual string Duplicate(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId) { return null; }
       public virtual long Count(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter) { return 0; }
       public virtual ChatLuongHoatDongTtbCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize) { return null; }
   }
}
