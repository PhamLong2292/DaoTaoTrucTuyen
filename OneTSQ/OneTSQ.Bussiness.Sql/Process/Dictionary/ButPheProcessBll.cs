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
    public class ButPheProcessBll : ButPheTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlButPheProcessBll";
            }
        }
        public override ButPheCls[] Reading(ActionSqlParamCls ActionSqlParam,ButPheFilterCls OButPheFilter)
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
                if (OButPheFilter == null)
                {
                    OButPheFilter = new ButPheFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from ButPhe where 1=1 ";
                if (!string.IsNullOrEmpty(OButPheFilter.CABENHID))
                {
                    ColDbParams.Add(new DbParam("CABENHID", OButPheFilter.CABENHID));
                    Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
                }
                if (OButPheFilter.HanhDongs.Count() > 0)
                {
                    string hanhDongQuery = "";
                    for (int i = OButPheFilter.HanhDongs.Count() - 1; i >= 0; i--)
                    {
                        hanhDongQuery += string.Format("{0},", OButPheFilter.HanhDongs[i]);
                    }
                    Query += " and HANHDONG in ( " + hanhDongQuery.Substring(0, hanhDongQuery.Length - 1) + ") ";
                }
                Query += " order by THOIGIAN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                ButPheCls[] ButPhes = ButPheParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ButPhes;
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
        public override ButPheCls[] PageReading(ActionSqlParamCls ActionSqlParam, ButPheFilterCls OButPheFilter,ref long recordTotal)
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
                if (OButPheFilter == null)
                {
                    OButPheFilter = new ButPheFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select bp.* from BUTPHE bp inner join CABENH cb on bp.CABENHID=cb.ID where 1 = 1 ";
                string recordTotalQuery = " select count(1) from BUTPHE bp inner join CABENH cb on bp.CABENHID=cb.ID where 1 = 1 ";
                if (OButPheFilter.TuNgay.HasValue)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", OButPheFilter.TuNgay.Value));
                    Query += " and bp.THOIGIAN >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                    recordTotalQuery += " and bp.THOIGIAN >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (OButPheFilter.DenNgay.HasValue)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", OButPheFilter.DenNgay.Value));
                    Query += " and bp.THOIGIAN <= " + ActionSqlParam.SpecialChar + "DENNGAY ";
                    recordTotalQuery += " and bp.THOIGIAN <= " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(OButPheFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OButPheFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + OButPheFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(bp.NOIDUNG) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(cb.HOTENBN) like " + ActionSqlParam.SpecialChar + "Keyword ) ";
                    recordTotalQuery += " and (upper(bp.NOIDUNG) like " + ActionSqlParam.SpecialChar + "Keyword1 OR upper(cb.HOTENBN) like " + ActionSqlParam.SpecialChar + "Keyword1)  ";
                }
                Query += "ORDER BY bp.THOIGIAN DESC " +
                    "OFFSET " + (OButPheFilter.PageIndex * OButPheFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + OButPheFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                ButPheCls[] CaBenhs = ButPheParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return CaBenhs;
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

        public override void Add(ActionSqlParamCls ActionSqlParam, ButPheCls OButPhe)
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
                if (string.IsNullOrEmpty(OButPhe.ID))
                {
                    OButPhe.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "ButPhe",
                    new DbParam[]{
                    new DbParam("ID",OButPhe.ID),
                    new DbParam("CABENHID",OButPhe.CABENHID),
                    new DbParam("THOIGIAN",OButPhe.THOIGIAN),
                    new DbParam("NGUOIPHE",OButPhe.NGUOIPHE),
                    new DbParam("HANHDONG",OButPhe.HANHDONG),
                    new DbParam("NOIDUNG",OButPhe.NOIDUNG)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, ButPheCls OButPhe)
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
                DBService.Update(ActionSqlParam.Trans, "ButPhe", "ID", ID,
                    new DbParam[]{
                new DbParam("CABENHID",OButPhe.CABENHID),
                new DbParam("THOIGIAN",OButPhe.THOIGIAN),
                new DbParam("NGUOIPHE",OButPhe.NGUOIPHE),
                new DbParam("HANHDONG",OButPhe.HANHDONG),
                new DbParam("NOIDUNG",OButPhe.NOIDUNG)
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
                string DelQuery = " Delete from ButPhe where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override ButPheCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from ButPhe where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                ButPheCls OButPhe = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OButPhe = ButPheParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OButPhe;
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
                ButPheCls OButPhe = CreateModel(ActionSqlParam, ID);
                OButPhe.ID = NewID;
                Add(ActionSqlParam, OButPhe);

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
