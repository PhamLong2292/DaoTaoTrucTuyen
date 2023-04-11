using System.Linq;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
public class ChatLuongHoatDongTtbProcessBll : ChatLuongHoatDongTtbTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlChatLuongHoatDongTtbProcessBll";
    }
}


public override ChatLuongHoatDongTtbCls[] Reading(RenderInfoCls ORenderInfo,ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Reading(ActionSqlParam, OChatLuongHoatDongTtbFilter);
}


public override void Add(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Add(ActionSqlParam, OChatLuongHoatDongTtb);
}


public override void Save(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Save(ActionSqlParam, ChatLuongHoatDongTtbId, OChatLuongHoatDongTtb);
}


public override void Delete(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Delete(ActionSqlParam, ChatLuongHoatDongTtbId);
}


public override ChatLuongHoatDongTtbCls CreateModel(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().CreateModel(ActionSqlParam, ChatLuongHoatDongTtbId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string ChatLuongHoatDongTtbId)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Duplicate(ActionSqlParam, ChatLuongHoatDongTtbId);
}
public override long Count(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter)
{
    ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().Count(ActionSqlParam, OChatLuongHoatDongTtbFilter);
}

public override ChatLuongHoatDongTtbCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize)
{
    ActionSqlParamCls  ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateChatLuongHoatDongTtbProcess().ReadingWithPaging(ActionSqlParam, OChatLuongHoatDongTtbFilter, PageIndex, PageSize);
}


}
}
