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
    public class DM_YKienBenhVienProcessBll : DM_YKienBenhVienTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_YKienBenhVienProcessBll";
            }
        }

        public override DM_YKienBenhVienCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
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
                if (OYKienBenhVienFilter == null)
                {
                    OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DM_YKienBenhVien where 1=1 ";
                if (!string.IsNullOrEmpty(OYKienBenhVienFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OYKienBenhVienFilter.Keyword + "%"));
                    Query += " and (UPPER(MA) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword) OR UPPER(TEN) like UPPER(" + ActionSqlParam.SpecialChar + "Keyword ))";
                }
                if (OYKienBenhVienFilter.HieuLuc != (int)eSearch.SearchAll)
                {
                    ColDbParams.Add(new DbParam("HIEULUC", OYKienBenhVienFilter.HieuLuc));
                    Query += " and HIEULUC=" + ActionSqlParam.SpecialChar + "HIEULUC";
                }
                Query += " order by STT";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_YKienBenhVienCls[] YKienBenhViens = DM_YKienBenhVienParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return YKienBenhViens;
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

        public override DM_YKienBenhVienCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal)
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
                if (OYKienBenhVienFilter == null)
                {
                    OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = ""; ;
                string recordTotalQuery = " select count(1) from DM_YKienBenhVien where 1 = 1 ";
                if (ActionSqlParam.SupportDb == ESupportDb.ORACLE)
                {
                    if (!string.IsNullOrEmpty(OYKienBenhVienFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                    }
                    if (OYKienBenhVienFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OYKienBenhVienFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OYKienBenhVienFilter.HieuLuc;
                    }

                    Query = string.Format("SELECT * FROM " +
                        "( " +
                        "    SELECT a.*, rownum r " +
                        "    FROM " +
                        "    ( " +
                        "        SELECT * FROM DM_YKienBenhVien WHERE 1 = 1 {0} " +
                        "        ORDER BY STT " +
                        "    ) a " +
                        "    WHERE rownum < (({1} * {2}) + 1 ) " +
                        ") " +
                        "WHERE r >= ((({1}-1) * {2}) + 1) ", Query, OYKienBenhVienFilter.PageIndex + 1, OYKienBenhVienFilter.PageSize);
                }
                else
                {
                    Query = " select * from DM_YKienBenhVien where 1=1 ";

                    if (!string.IsNullOrEmpty(OYKienBenhVienFilter.Keyword))
                    {
                        Query += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                        recordTotalQuery += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                    }
                    if (OYKienBenhVienFilter.HieuLuc != (int)eSearch.SearchAll)
                    {
                        Query += " and HIEULUC=" + OYKienBenhVienFilter.HieuLuc;
                        recordTotalQuery += " and HIEULUC=" + OYKienBenhVienFilter.HieuLuc;
                    }

                    Query += "ORDER BY STT " +
                        "OFFSET " + (OYKienBenhVienFilter.PageIndex * OYKienBenhVienFilter.PageSize) + " ROWS " +
                        "FETCH NEXT " + OYKienBenhVienFilter.PageSize + " ROWS ONLY ";
                }
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DM_YKienBenhVienCls[] YKienBenhViens = DM_YKienBenhVienParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = int.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return YKienBenhViens;
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

        public override void Add(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienCls OYKienBenhVien)
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
                if (string.IsNullOrEmpty(OYKienBenhVien.Id))
                {
                    OYKienBenhVien.Id = System.Guid.NewGuid().ToString();
                }
                string Query = "select * from DM_YKienBenhVien where MA=" + ActionSqlParam.SpecialChar + "MA ";
                DataTable dtCheck = DBService.GetDataTable(ActionSqlParam.Trans, Query, new DbParam[]
            {
                new DbParam("MA",OYKienBenhVien.Ma)
            });

                if (!string.IsNullOrEmpty(OYKienBenhVien.ChaId) || dtCheck.Rows.Count == 0)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DM_YKienBenhVien",
                        new DbParam[]{
                    new DbParam("ID",OYKienBenhVien.Id),
                    new DbParam("MA",OYKienBenhVien.Ma),
                    new DbParam("TEN",OYKienBenhVien.Ten),
                    new DbParam("NOIDUNG",OYKienBenhVien.NoiDung),
                    new DbParam("MOTA",OYKienBenhVien.MoTa),
                    new DbParam("HIEULUC",OYKienBenhVien.HieuLuc),
                    new DbParam("STT",OYKienBenhVien.Stt),
                    new DbParam("NGAYTAO",OYKienBenhVien.NgayTao),
                    new DbParam("TUNGAY",OYKienBenhVien.TuNgay),
                    new DbParam("DENNGAY",OYKienBenhVien.DenNgay),
                    new DbParam("GHICHU",OYKienBenhVien.GhiChu),
                    new DbParam("CHAID",OYKienBenhVien.ChaId),
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

        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DM_YKienBenhVienCls OYKienBenhVien)
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
                OYKienBenhVien.Id = ID;
                DBService.Update(ActionSqlParam.Trans, "DM_YKienBenhVien", "ID", ID,
                    new DbParam[]{
                    new DbParam("MA",OYKienBenhVien.Ma),
                    new DbParam("TEN",OYKienBenhVien.Ten),
                    new DbParam("NOIDUNG",OYKienBenhVien.NoiDung),
                    new DbParam("MOTA",OYKienBenhVien.MoTa),
                    new DbParam("HIEULUC",OYKienBenhVien.HieuLuc),
                    new DbParam("STT",OYKienBenhVien.Stt),
                    new DbParam("NGAYTAO",OYKienBenhVien.NgayTao),
                    new DbParam("TUNGAY",OYKienBenhVien.TuNgay),
                    new DbParam("DENNGAY",OYKienBenhVien.DenNgay),
                    new DbParam("GHICHU",OYKienBenhVien.GhiChu),
                    new DbParam("CHAID",OYKienBenhVien.ChaId),
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
                string DelQuery = " Delete from DM_YKienBenhVien where ID=" + ActionSqlParam.SpecialChar + "ID";
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

        public override DM_YKienBenhVienCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_YKienBenhVien where (ID=" + ActionSqlParam.SpecialChar + "Id or MA =" + ActionSqlParam.SpecialChar + "Id)  ", new DbParam[]{
                    new DbParam("Id",ID)
                });
                DM_YKienBenhVienCls OYKienBenhVien = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OYKienBenhVien = DM_YKienBenhVienParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OYKienBenhVien;
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
                DM_YKienBenhVienCls OYKienBenhVien = CreateModel(ActionSqlParam, ID);
                OYKienBenhVien.Id = NewID;
                Add(ActionSqlParam, OYKienBenhVien);

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

        public override long Count(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OYKienBenhVienFilter == null)
                    OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
                string Query = " select COUNT (*) from DM_YKienBenhVien where 1=1 ";

                if (!string.IsNullOrEmpty(OYKienBenhVienFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                if (OYKienBenhVienFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC=" + OYKienBenhVienFilter.HieuLuc;

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = DM_YKienBenhVienParser.CountFromDataTable(dsResult.Tables[0]);

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
        public override DM_YKienBenhVienCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OYKienBenhVienFilter == null)
                    OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
                var skip = OYKienBenhVienFilter.PageIndex * OYKienBenhVienFilter.PageSize;
                string Query = " select * from DM_YKienBenhVien where 4=4";

                if (!string.IsNullOrEmpty(OYKienBenhVienFilter.Keyword))
                    Query += " and (UPPER(MA) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%') OR UPPER(TEN) like UPPER('%" + OYKienBenhVienFilter.Keyword + "%')) ";
                if (OYKienBenhVienFilter.HieuLuc != (int)eSearch.SearchAll)
                    Query += " and HIEULUC = " + OYKienBenhVienFilter.HieuLuc;

                Query += " ORDER BY STT " +
                    "OFFSET " + skip.ToString() + " ROWS " +
                    "FETCH NEXT " + OYKienBenhVienFilter.PageSize + " ROWS ONLY";


                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                DM_YKienBenhVienCls[] YKienBenhViens = DM_YKienBenhVienParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return YKienBenhViens;
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
        public override DM_YKienBenhVienCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaYKienBenhVien)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from DM_YKienBenhVien where UPPER(MA)=UPPER('" + MaYKienBenhVien + "')", new DbParam[] { });
                DM_YKienBenhVienCls OYKienBenhVien = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OYKienBenhVien = DM_YKienBenhVienParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OYKienBenhVien;
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
