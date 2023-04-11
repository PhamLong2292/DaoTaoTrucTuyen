using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Bussiness.Template;
using OneTSQ.Database.Service;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Sql
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
    public override ChatLuongHoatDongTtbCls[] Reading(ActionSqlParamCls ActionSqlParam,ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            if (OChatLuongHoatDongTtbFilter == null)
            {
                OChatLuongHoatDongTtbFilter = new ChatLuongHoatDongTtbFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from CHATLUONGHOATDONGTTB where 1=1 ";
            if (!string.IsNullOrEmpty(OChatLuongHoatDongTtbFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
            {
                ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OChatLuongHoatDongTtbFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
            }
            Query += "ORDER BY STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            ChatLuongHoatDongTtbCls[] ChatLuongHoatDongTtbs = ChatLuongHoatDongTtbParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ChatLuongHoatDongTtbs;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override long Count( ActionSqlParamCls ActionSqlParam,ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
         if (OChatLuongHoatDongTtbFilter == null)
             OChatLuongHoatDongTtbFilter = new ChatLuongHoatDongTtbFilterCls();
         Collection<DbParam> ColDbParams = new Collection<DbParam>();
         string Query = " select COUNT (*) from CHATLUONGHOATDONGTTB where 1=1 ";
         if (!string.IsNullOrEmpty(OChatLuongHoatDongTtbFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
         {
            ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OChatLuongHoatDongTtbFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
            Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
         }
         DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = ChatLuongHoatDongTtbParser.CountFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return count;
    }
    catch (Exception ex)
    {
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Rollback();
            ActionSqlParam.Trans = null;
        }
        throw (ex);
    }
}
public override ChatLuongHoatDongTtbCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (OChatLuongHoatDongTtbFilter == null)
            OChatLuongHoatDongTtbFilter = new ChatLuongHoatDongTtbFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from CHATLUONGHOATDONGTTB OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        ChatLuongHoatDongTtbCls[] ChatLuongHoatDongTtbs = ChatLuongHoatDongTtbParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return ChatLuongHoatDongTtbs;
    }
    catch (Exception ex)
    {
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Rollback();
            ActionSqlParam.Trans = null;
        }
        throw (ex);
    }
}
    public override void Add(ActionSqlParamCls ActionSqlParam, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
                DBService.Insert(ActionSqlParam.Trans, "ChatLuongHoatDongTtb",
                    new DbParam[]{
                    new DbParam("ID",OChatLuongHoatDongTtb.ID),
                    new DbParam("TRANGTHIETBITRUYENHINHTTMA",OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OChatLuongHoatDongTtb.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("TRANGTHIETBITRUYENHINHTTCHAMA",OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTCHAMA),
                    new DbParam("DANHGIA",OChatLuongHoatDongTtb.DANHGIA),
                    new DbParam("STT",OChatLuongHoatDongTtb.STT)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            DBService.Update(ActionSqlParam.Trans, "CHATLUONGHOATDONGTTB", "ID", ID,
               new DbParam[]{
               new DbParam("TRANGTHIETBITRUYENHINHTTMA",OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OChatLuongHoatDongTtb.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("TRANGTHIETBITRUYENHINHTTCHAMA",OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTCHAMA),
               new DbParam("DANHGIA",OChatLuongHoatDongTtb.DANHGIA),
               new DbParam("STT",OChatLuongHoatDongTtb.STT)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override void Delete(ActionSqlParamCls ActionSqlParam, string ID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            string DelQuery = " Delete from CHATLUONGHOATDONGTTB where ID="+ActionSqlParam.SpecialChar+"ID";
            DBService.ExecuteNonQuery(ActionSqlParam.Trans, DelQuery, 
                   new DbParam[]
                {
                   new DbParam("ID", ID)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override ChatLuongHoatDongTtbCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from CHATLUONGHOATDONGTTB where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OChatLuongHoatDongTtb = ChatLuongHoatDongTtbParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OChatLuongHoatDongTtb;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }

    public override string Duplicate(ActionSqlParamCls ActionSqlParam, string ID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        string NewID = System.Guid.NewGuid().ToString();
        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb = CreateModel(ActionSqlParam, ID);
            OChatLuongHoatDongTtb.ID = NewID;
            Add(ActionSqlParam, OChatLuongHoatDongTtb);

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return NewID;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
}
}
