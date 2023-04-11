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
    public class DM_TieuChiThoiGianDaoTaoTtProcessBll : DM_TieuChiThoiGianDaoTaoTtTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TieuChiThoiGianDaoTaoTtProcessBll";
            }
        }

        public override DM_TieuChiThoiGianDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
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
                if (OTieuChiThoiGianDaoTaoTtFilter == null)
                {
                    OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DM_TieuChiThoiGianDaoTaoTt where 1=1 ";
                if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTtFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%"));
                    Query += " and (UPPER(MA) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword) OR UPPER(TEN) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword ))";
                }
                if (OTieuChiThoiGianDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                {
                    ColDbParams.Add(new DbParam("HIEULUC", OTieuChiThoiGianDaoTaoTtFilter.HieuLuc));
                    Query += " and HIEULUC=" + ActionSqlParam.SpecialChar + "HIEULUC";
                }
                Query += " order by STT";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts = DM_TieuChiThoiGianDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return TieuChiThoiGianDaoTaoTts;
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

        public override DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal)
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
                if (OTieuChiThoiGianDaoTaoTtFilter == null)
                {
                    OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = ""; ;
                string recordTotalQuery = " select count(1) from DM_TieuChiThoiGianDaoTaoTt where 1 = 1 ";
                if (ActionSqlParam.SupportDb == ESupportDb.ORACLE)
                {
                    if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTtFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                    }
                    if (OTieuChiThoiGianDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;
                    }

                    Query = string.Format("SELECT * FROM " +
                        "( " +
                        "    SELECT a.*, rownum r " +
                        "    FROM " +
                        "    ( " +
                        "        SELECT * FROM DM_TieuChiThoiGianDaoTaoTt WHERE 1 = 1 {0} " +
                        "        ORDER BY STT " +
                        "    ) a " +
                        "    WHERE rownum < (({1} * {2}) + 1 ) " +
                        ") " +
                        "WHERE r >= ((({1}-1) * {2}) + 1) ", Query, OTieuChiThoiGianDaoTaoTtFilter.PageIndex + 1, OTieuChiThoiGianDaoTaoTtFilter.PageSize);
                }
                else
                {
                    Query = " select * from DM_TieuChiThoiGianDaoTaoTt where 1=1 ";

                    if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTtFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                    }
                    if (OTieuChiThoiGianDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;
                    }

                    Query += "ORDER BY STT " +
                        "OFFSET " + (OTieuChiThoiGianDaoTaoTtFilter.PageIndex * OTieuChiThoiGianDaoTaoTtFilter.PageSize) + " ROWS " +
                        "FETCH NEXT " + OTieuChiThoiGianDaoTaoTtFilter.PageSize + " ROWS ONLY ";
                }
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts = DM_TieuChiThoiGianDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = int.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return TieuChiThoiGianDaoTaoTts;
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

        public override void Add(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt)
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
                if (string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTt.Id))
                {
                    OTieuChiThoiGianDaoTaoTt.Id = System.Guid.NewGuid().ToString();
                }
                string Query = "select * from DM_TieuChiThoiGianDaoTaoTt where MA=" + ActionSqlParam.SpecialChar + "MA ";
                DataTable dtCheck = DBService.GetDataTable(ActionSqlParam.Trans, Query, new DbParam[]
            {
                new DbParam("MA",OTieuChiThoiGianDaoTaoTt.Ma)
            });

                if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTt.ChaId) || dtCheck.Rows.Count == 0)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DM_TieuChiThoiGianDaoTaoTt",
                        new DbParam[]{
                    new DbParam("ID",OTieuChiThoiGianDaoTaoTt.Id),
                    new DbParam("MA",OTieuChiThoiGianDaoTaoTt.Ma),
                    new DbParam("TEN",OTieuChiThoiGianDaoTaoTt.Ten),
                    new DbParam("MOTA",OTieuChiThoiGianDaoTaoTt.MoTa),
                    new DbParam("HIEULUC",OTieuChiThoiGianDaoTaoTt.HieuLuc),
                    new DbParam("STT",OTieuChiThoiGianDaoTaoTt.Stt),
                    new DbParam("NGAYTAO",OTieuChiThoiGianDaoTaoTt.NgayTao),
                    new DbParam("TUNGAY",OTieuChiThoiGianDaoTaoTt.TuNgay),
                    new DbParam("DENNGAY",OTieuChiThoiGianDaoTaoTt.DenNgay),
                    new DbParam("GHICHU",OTieuChiThoiGianDaoTaoTt.GhiChu),
                    new DbParam("CHAID",OTieuChiThoiGianDaoTaoTt.ChaId),
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

        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt)
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
                OTieuChiThoiGianDaoTaoTt.Id = ID;
                DBService.Update(ActionSqlParam.Trans, "DM_TieuChiThoiGianDaoTaoTt", "ID", ID,
                    new DbParam[]{
                    new DbParam("MA",OTieuChiThoiGianDaoTaoTt.Ma),
                    new DbParam("TEN",OTieuChiThoiGianDaoTaoTt.Ten),
                    new DbParam("MOTA",OTieuChiThoiGianDaoTaoTt.MoTa),
                    new DbParam("HIEULUC",OTieuChiThoiGianDaoTaoTt.HieuLuc),
                    new DbParam("STT",OTieuChiThoiGianDaoTaoTt.Stt),
                    new DbParam("NGAYTAO",OTieuChiThoiGianDaoTaoTt.NgayTao),
                    new DbParam("TUNGAY",OTieuChiThoiGianDaoTaoTt.TuNgay),
                    new DbParam("DENNGAY",OTieuChiThoiGianDaoTaoTt.DenNgay),
                    new DbParam("GHICHU",OTieuChiThoiGianDaoTaoTt.GhiChu),
                    new DbParam("CHAID",OTieuChiThoiGianDaoTaoTt.ChaId),
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
                string DelQuery = " Delete from DM_TieuChiThoiGianDaoTaoTt where ID=" + ActionSqlParam.SpecialChar + "ID";
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

        public override DM_TieuChiThoiGianDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_TieuChiThoiGianDaoTaoTt where (ID=" + ActionSqlParam.SpecialChar + "Id or MA =" + ActionSqlParam.SpecialChar + "Id)  ", new DbParam[]{
                    new DbParam("Id",ID)
                });
                DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OTieuChiThoiGianDaoTaoTt = DM_TieuChiThoiGianDaoTaoTtParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OTieuChiThoiGianDaoTaoTt;
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
                DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = CreateModel(ActionSqlParam, ID);
                OTieuChiThoiGianDaoTaoTt.Id = NewID;
                Add(ActionSqlParam, OTieuChiThoiGianDaoTaoTt);

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

        public override long Count(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OTieuChiThoiGianDaoTaoTtFilter == null)
                    OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
                string Query = " select COUNT (*) from DM_TieuChiThoiGianDaoTaoTt where 1=1 ";

                if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTtFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                if (OTieuChiThoiGianDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC=" + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = DM_TieuChiThoiGianDaoTaoTtParser.CountFromDataTable(dsResult.Tables[0]);

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
        public override DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OTieuChiThoiGianDaoTaoTtFilter == null)
                    OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
                var skip = OTieuChiThoiGianDaoTaoTtFilter.PageIndex * OTieuChiThoiGianDaoTaoTtFilter.PageSize;
                string Query = " select * from DM_TieuChiThoiGianDaoTaoTt where 4=4";

                if (!string.IsNullOrEmpty(OTieuChiThoiGianDaoTaoTtFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OTieuChiThoiGianDaoTaoTtFilter.Keyword + "%')) ";
                if (OTieuChiThoiGianDaoTaoTtFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC = " + OTieuChiThoiGianDaoTaoTtFilter.HieuLuc;

                Query += " ORDER BY STT " +
                    "OFFSET " + skip.ToString() + " ROWS " +
                    "FETCH NEXT " + OTieuChiThoiGianDaoTaoTtFilter.PageSize + " ROWS ONLY";


                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts = DM_TieuChiThoiGianDaoTaoTtParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return TieuChiThoiGianDaoTaoTts;
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
        public override DM_TieuChiThoiGianDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTieuChiThoiGianDaoTaoTt)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_TieuChiThoiGianDaoTaoTt where UPPER(MA)=UPPER('" + MaTieuChiThoiGianDaoTaoTt + "')", new DbParam[] { });
                DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OTieuChiThoiGianDaoTaoTt = DM_TieuChiThoiGianDaoTaoTtParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OTieuChiThoiGianDaoTaoTt;
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
