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
    public class DM_ChuyenKhoaDaoTaoTtProcessBll : DM_ChuyenKhoaDaoTaoTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_ChuyenKhoaDaoTaoTtProcessBll";
            }
        }

        public override DM_ChuyenKhoaDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
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
                if (OChuyenKhoaDaoTaoTtFilter == null)
                {
                    OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DM_ChuyenKhoaDaoTaoTt where 1=1 ";
                if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTtFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%"));
                    Query += " and (UPPER(MA) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword) OR UPPER(TEN) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword ))";
                }
                if (OChuyenKhoaDaoTaoTtFilter.HieuLuc != (int) eSearch.SearchAll)
                {
                    ColDbParams.Add(new DbParam("HIEULUC", OChuyenKhoaDaoTaoTtFilter.HieuLuc));
                    Query += " and HIEULUC=" + ActionSqlParam.SpecialChar + "HIEULUC";
                }
                Query += " order by STT";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = DM_ChuyenKhoaDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ChuyenKhoaDaoTaoTts;
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

        public override DM_ChuyenKhoaDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal)
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
                if (OChuyenKhoaDaoTaoTtFilter == null)
                {
                    OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = ""; ;
                string recordTotalQuery = " select count(1) from DM_ChuyenKhoaDaoTaoTt where 1 = 1 ";
                if (ActionSqlParam.SupportDb == ESupportDb.ORACLE)
                {
                    if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTtFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                    }
                    if (OChuyenKhoaDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OChuyenKhoaDaoTaoTtFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OChuyenKhoaDaoTaoTtFilter.HieuLuc;
                    }

                    Query = string.Format("SELECT * FROM " +
                        "( " +
                        "    SELECT a.*, rownum r " +
                        "    FROM " +
                        "    ( " +
                        "        SELECT * FROM DM_ChuyenKhoaDaoTaoTt WHERE 1 = 1 {0} " +
                        "        ORDER BY STT " +
                        "    ) a " +
                        "    WHERE rownum < (({1} * {2}) + 1 ) " +
                        ") " +
                        "WHERE r >= ((({1}-1) * {2}) + 1) ", Query, OChuyenKhoaDaoTaoTtFilter.PageIndex + 1, OChuyenKhoaDaoTaoTtFilter.PageSize);
                }
                else
                {
                    Query = " select * from DM_ChuyenKhoaDaoTaoTt where 1=1 ";

                    if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTtFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                    }
                    if (OChuyenKhoaDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OChuyenKhoaDaoTaoTtFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OChuyenKhoaDaoTaoTtFilter.HieuLuc;
                    }

                    Query += "ORDER BY STT " +
                        "OFFSET " + (OChuyenKhoaDaoTaoTtFilter.PageIndex * OChuyenKhoaDaoTaoTtFilter.PageSize) + " ROWS " +
                        "FETCH NEXT " + OChuyenKhoaDaoTaoTtFilter.PageSize + " ROWS ONLY ";
                }
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = DM_ChuyenKhoaDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = int.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ChuyenKhoaDaoTaoTts;
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

        public override void Add(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt)
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
                if (string.IsNullOrEmpty(OChuyenKhoaDaoTaoTt.Id))
                {
                    OChuyenKhoaDaoTaoTt.Id = System.Guid.NewGuid().ToString();
                }
                string Query = "select * from DM_ChuyenKhoaDaoTaoTt where MA=" + ActionSqlParam.SpecialChar + "MA ";
                DataTable dtCheck = DBService.GetDataTable(ActionSqlParam.Trans, Query, new DbParam[]
            {
                new DbParam("MA",OChuyenKhoaDaoTaoTt.Ma)
            });

                if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTt.ChaId) || dtCheck.Rows.Count == 0)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DM_ChuyenKhoaDaoTaoTt",
                        new DbParam[]{
                    new DbParam("ID",OChuyenKhoaDaoTaoTt.Id),
                    new DbParam("MA",OChuyenKhoaDaoTaoTt.Ma),
                    new DbParam("TEN",OChuyenKhoaDaoTaoTt.Ten),
                    new DbParam("TENNGAN",OChuyenKhoaDaoTaoTt.TenNgan),
                    new DbParam("MOTA",OChuyenKhoaDaoTaoTt.MoTa),
                    new DbParam("HIEULUC",OChuyenKhoaDaoTaoTt.HieuLuc),
                    new DbParam("STT",OChuyenKhoaDaoTaoTt.Stt),
                    new DbParam("NGAYTAO",OChuyenKhoaDaoTaoTt.NgayTao),
                    new DbParam("TUNGAY",OChuyenKhoaDaoTaoTt.TuNgay),
                    new DbParam("DENNGAY",OChuyenKhoaDaoTaoTt.DenNgay),
                    new DbParam("GHICHU",OChuyenKhoaDaoTaoTt.GhiChu),
                    new DbParam("CHAID",OChuyenKhoaDaoTaoTt.ChaId),
                });
                }
                else throw new Exception("Mã đã tồn tại không thể tạo được bản ghi mới");
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

        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt)
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
                OChuyenKhoaDaoTaoTt.Id = ID;
                DBService.Update(ActionSqlParam.Trans, "DM_ChuyenKhoaDaoTaoTt", "ID", ID,
                    new DbParam[]{
                    new DbParam("MA",OChuyenKhoaDaoTaoTt.Ma),
                    new DbParam("TEN",OChuyenKhoaDaoTaoTt.Ten),
                    new DbParam("TENNGAN",OChuyenKhoaDaoTaoTt.TenNgan),
                    new DbParam("MOTA",OChuyenKhoaDaoTaoTt.MoTa),
                    new DbParam("HIEULUC",OChuyenKhoaDaoTaoTt.HieuLuc),
                    new DbParam("STT",OChuyenKhoaDaoTaoTt.Stt),
                    new DbParam("NGAYTAO",OChuyenKhoaDaoTaoTt.NgayTao),
                    new DbParam("TUNGAY",OChuyenKhoaDaoTaoTt.TuNgay),
                    new DbParam("DENNGAY",OChuyenKhoaDaoTaoTt.DenNgay),
                    new DbParam("GHICHU",OChuyenKhoaDaoTaoTt.GhiChu),
                    new DbParam("CHAID",OChuyenKhoaDaoTaoTt.ChaId),
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
                string DelQuery = " Delete from DM_ChuyenKhoaDaoTaoTt where ID=" + ActionSqlParam.SpecialChar + "ID";
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

        public override DM_ChuyenKhoaDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_ChuyenKhoaDaoTaoTt where (ID=" + ActionSqlParam.SpecialChar + "Id or MA =" + ActionSqlParam.SpecialChar + "Id)  ", new DbParam[]{
                    new DbParam("Id",ID)
                });
                DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OChuyenKhoaDaoTaoTt = DM_ChuyenKhoaDaoTaoTtParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OChuyenKhoaDaoTaoTt;
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
                DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = CreateModel(ActionSqlParam, ID);
                OChuyenKhoaDaoTaoTt.Id = NewID;
                Add(ActionSqlParam, OChuyenKhoaDaoTaoTt);

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

        public override long Count(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OChuyenKhoaDaoTaoTtFilter == null)
                    OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
                string Query = " select COUNT (*) from DM_ChuyenKhoaDaoTaoTt where 1=1 ";

                if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTtFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                if (OChuyenKhoaDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC=" + OChuyenKhoaDaoTaoTtFilter.HieuLuc;

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = DM_ChuyenKhoaDaoTaoTtParser.CountFromDataTable(dsResult.Tables[0]);

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
        public override DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OChuyenKhoaDaoTaoTtFilter == null)
                    OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
                var skip = OChuyenKhoaDaoTaoTtFilter.PageIndex * OChuyenKhoaDaoTaoTtFilter.PageSize;
                string Query = " select * from DM_ChuyenKhoaDaoTaoTt where 4=4";

                if (!string.IsNullOrEmpty(OChuyenKhoaDaoTaoTtFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OChuyenKhoaDaoTaoTtFilter.Keyword + "%')) ";
                if (OChuyenKhoaDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC = " + OChuyenKhoaDaoTaoTtFilter.HieuLuc;

                Query += " ORDER BY STT " +
                    "OFFSET " + skip.ToString() + " ROWS " +
                    "FETCH NEXT " + OChuyenKhoaDaoTaoTtFilter.PageSize + " ROWS ONLY";


                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = DM_ChuyenKhoaDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ChuyenKhoaDaoTaoTts;
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
        public override DM_ChuyenKhoaDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaChuyenKhoaDaoTaoTt)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            try
            {
                if (!HasTrans)
                {
                    ActionSqlParam.Trans = DBService.BeginTransaction();
                }
                DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_ChuyenKhoaDaoTaoTt where UPPER(MA)=UPPER('" + MaChuyenKhoaDaoTaoTt + "')", new DbParam[] { });
                DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OChuyenKhoaDaoTaoTt = DM_ChuyenKhoaDaoTaoTtParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OChuyenKhoaDaoTaoTt;
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
