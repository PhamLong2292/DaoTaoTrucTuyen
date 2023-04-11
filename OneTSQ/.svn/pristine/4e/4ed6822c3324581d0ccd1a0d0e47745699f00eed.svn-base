using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Bussiness.Template;
using OneTSQ.Database.Service;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Sql
{
public class YKienChuyenGiaProcessBll : YKienChuyenGiaTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlYKienChuyenGiaProcessBll";
        }
    }
    public override YKienChuyenGiaCls[] Reading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter)
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
            if (OYKienChuyenGiaFilter == null)
            {
                OYKienChuyenGiaFilter = new YKienChuyenGiaFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from YKIENCHUYENGIA where 1=1 ";
            if (!string.IsNullOrEmpty(OYKienChuyenGiaFilter.CABENHID))
            {
                ColDbParams.Add(new DbParam("CABENHID", OYKienChuyenGiaFilter.CABENHID));
                Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
            }
            if (!string.IsNullOrEmpty(OYKienChuyenGiaFilter.YKIENCHUYENGIAID))
            {
                ColDbParams.Add(new DbParam("YKIENCHUYENGIAID", OYKienChuyenGiaFilter.YKIENCHUYENGIAID));
                Query += " and YKIENCHUYENGIAID = " + ActionSqlParam.SpecialChar + "YKIENCHUYENGIAID";
            }
            if (OYKienChuyenGiaFilter.isParent == true)
            {
                Query += " and YKIENCHUYENGIAID is null ";
            }
            if (OYKienChuyenGiaFilter.isChildren == true)
            {
                Query += " and YKIENCHUYENGIAID is not null ";
            }
            Query += " order by THOIGIAN";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            YKienChuyenGiaCls[] YKienChuyenGias = YKienChuyenGiaParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return YKienChuyenGias;
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
    public override YKienChuyenGiaCls[] PageReading(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter, ref long recordTotal)
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
            if (OYKienChuyenGiaFilter == null)
            {
                OYKienChuyenGiaFilter = new YKienChuyenGiaFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = " select * from BenhVien where 1=1 ";
            string recordTotalQuery = " select count(1) from BenhVien where 1 = 1 ";
            if (!string.IsNullOrEmpty(OYKienChuyenGiaFilter.CABENHID))
            {
                ColDbParams.Add(new DbParam("CABENHID", OYKienChuyenGiaFilter.CABENHID));
                Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
                recordTotalQuery += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
            }
            if (!string.IsNullOrEmpty(OYKienChuyenGiaFilter.YKIENCHUYENGIAID))
            {
                ColDbParams.Add(new DbParam("YKIENCHUYENGIAID", OYKienChuyenGiaFilter.YKIENCHUYENGIAID));
                Query += " and YKIENCHUYENGIAID = " + ActionSqlParam.SpecialChar + "YKIENCHUYENGIAID";
                recordTotalQuery += " and YKIENCHUYENGIAID = " + ActionSqlParam.SpecialChar + "YKIENCHUYENGIAID";
            }
            if (OYKienChuyenGiaFilter.isParent == true)
            {
                Query += " and YKIENCHUYENGIAID is null ";
                recordTotalQuery += " and YKIENCHUYENGIAID is null ";
            }
            if (OYKienChuyenGiaFilter.isChildren == true)
            {
                Query += " and YKIENCHUYENGIAID is not null ";
                recordTotalQuery += " and YKIENCHUYENGIAID is not null ";
            }
            Query += "ORDER BY THOIGIAN " +
                "OFFSET " + (OYKienChuyenGiaFilter.PageIndex * OYKienChuyenGiaFilter.PageSize) + " ROWS " +
                "FETCH NEXT " + OYKienChuyenGiaFilter.PageSize + " ROWS ONLY ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            YKienChuyenGiaCls[] yKienChuyenGias = YKienChuyenGiaParser.ParseFromDataTable(dsResult.Tables[0]);
            recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return yKienChuyenGias;
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
    //Lấy về danh sách chuyên gia tham gia hội chẩn.
    public override YKienChuyenGiaCls[] GetChuyenGias(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaFilterCls OYKienChuyenGiaFilter)
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
            if (OYKienChuyenGiaFilter == null)
            {
                OYKienChuyenGiaFilter = new YKienChuyenGiaFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select distinct '' AS ID, '' AS CABENHID, BACSY, NULL AS THOIGIAN, '' AS NOIDUNG, '' AS YKIENCHUYENGIAID, DONVI from YKIENCHUYENGIA where 1=1 ";
            if (!string.IsNullOrEmpty(OYKienChuyenGiaFilter.CABENHID))
            {
                ColDbParams.Add(new DbParam("CABENHID", OYKienChuyenGiaFilter.CABENHID));
                Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
            }
            Query += " order by BACSY";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            YKienChuyenGiaCls[] YKienChuyenGias = YKienChuyenGiaParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return YKienChuyenGias;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, YKienChuyenGiaCls OYKienChuyenGia)
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
            if (string.IsNullOrEmpty(OYKienChuyenGia.ID))
            {
                OYKienChuyenGia.ID = System.Guid.NewGuid().ToString();
            }
                DBService.Insert(ActionSqlParam.Trans, "YKIENCHUYENGIA",
                    new DbParam[]{
                    new DbParam("ID",OYKienChuyenGia.ID),
                    new DbParam("YKIENCHUYENGIAID",OYKienChuyenGia.YKIENCHUYENGIAID),
                    new DbParam("CABENHID",OYKienChuyenGia.CABENHID),
                    new DbParam("NOIDUNG",OYKienChuyenGia.NOIDUNG),
                    new DbParam("BACSYIDS",OYKienChuyenGia.BACSYIDS),
                    new DbParam("BACSY",OYKienChuyenGia.BACSY),
                    new DbParam("DONVI",OYKienChuyenGia.DONVI),
                    new DbParam("THOIGIAN",OYKienChuyenGia.THOIGIAN)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, YKienChuyenGiaCls OYKienChuyenGia)
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
            DBService.Update(ActionSqlParam.Trans, "YKIENCHUYENGIA", "ID", ID,
                new DbParam[]{
                   new DbParam("NOIDUNG",OYKienChuyenGia.NOIDUNG),
                   new DbParam("YKIENCHUYENGIAID",OYKienChuyenGia.YKIENCHUYENGIAID),
                   new DbParam("CABENHID",OYKienChuyenGia.CABENHID),
                   new DbParam("BACSYIDS",OYKienChuyenGia.BACSYIDS),
                   new DbParam("BACSY",OYKienChuyenGia.BACSY),
                   new DbParam("DONVI",OYKienChuyenGia.DONVI),
                   new DbParam("THOIGIAN",OYKienChuyenGia.THOIGIAN)
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
            string DelQuery = " DECLARE BEGIN " +
                              " Delete from TepDinhKem where YKienChuyenGiaId in (select id from YKienChuyenGia where YKienChuyenGiaId=" + ActionSqlParam.SpecialChar + "ID); " +
                              " Delete from TepDinhKem where YKienChuyenGiaId =" + ActionSqlParam.SpecialChar + "ID; " +
                              " Delete from YKienChuyenGia where YKienChuyenGiaId=" + ActionSqlParam.SpecialChar + "ID; " +
                              " Delete from YKienChuyenGia where ID=" + ActionSqlParam.SpecialChar + "ID; " +
                              " END;";
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
    public override YKienChuyenGiaCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from YKIENCHUYENGIA where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            YKienChuyenGiaCls OYKienChuyenGia = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OYKienChuyenGia = YKienChuyenGiaParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OYKienChuyenGia;
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
            YKienChuyenGiaCls OYKienChuyenGia = CreateModel(ActionSqlParam, ID);
            OYKienChuyenGia.ID = NewID;
            Add(ActionSqlParam, OYKienChuyenGia);

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
