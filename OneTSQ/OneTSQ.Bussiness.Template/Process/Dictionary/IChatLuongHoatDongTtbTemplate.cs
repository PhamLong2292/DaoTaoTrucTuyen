using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
   public interface IChatLuongHoatDongTtbProcess
   {
       string ServiceId { get; }
       ChatLuongHoatDongTtbCls[] Reading(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter);
       void Add(ActionSqlParamCls ActionSqlParam,  ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb);
       void Save(ActionSqlParamCls ActionSqlParam,  string ChatLuongHoatDongTtbId,ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb);
       void Delete(ActionSqlParamCls ActionSqlParam,  string ChatLuongHoatDongTtbId);
       ChatLuongHoatDongTtbCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId);
       string Duplicate(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId);
       long Count(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter);
       ChatLuongHoatDongTtbCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize);
   }
   
   public class ChatLuongHoatDongTtbTemplate : IChatLuongHoatDongTtbProcess
   {
       public virtual string ServiceId { get { return null; } }
       public virtual ChatLuongHoatDongTtbCls[] Reading(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter) { return null; }
       public virtual void Add(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb) { }
       public virtual void Save(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb) { }
       public virtual void Delete(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId) { }
       public virtual ChatLuongHoatDongTtbCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId) { return null; }
       public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string ChatLuongHoatDongTtbId) { return null; }
       public virtual long Count(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter) { return 0; }
       public virtual ChatLuongHoatDongTtbCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize) { return null; }
   }
}
