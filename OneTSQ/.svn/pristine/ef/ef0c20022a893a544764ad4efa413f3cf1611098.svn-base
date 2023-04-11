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
    public class DT_LichLyThuyetChiTietProcessBll : DT_LichLyThuyetChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichLyThuyetChiTietProcessBll";
            }
        }
        public override DT_LichLyThuyetChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter)
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
                if (ODT_LichLyThuyetChiTietFilter == null)
                {
                    ODT_LichLyThuyetChiTietFilter = new DT_LichLyThuyetChiTietFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == null)
                    Query = " select * from DT_LichLyThuyetChiTiet where 1=1 ";
                else
                    Query = " select * from DT_LichLyThuyetChiTiet llt inner join DT_KHOAHOC kh on llt.LICHLYTHUYET_ID = kh.id where 1=1 ";

                if (!string.IsNullOrEmpty(ODT_LichLyThuyetChiTietFilter.LICHLYTHUYET_ID))
                {
                    ColDbParams.Add(new DbParam("LICHLYTHUYET_ID", ODT_LichLyThuyetChiTietFilter.LICHLYTHUYET_ID));
                    Query += " and LICHLYTHUYET_ID = " + ActionSqlParam.SpecialChar + "LICHLYTHUYET_ID ";
                }
                if (ODT_LichLyThuyetChiTietFilter.NGAY != null)
                {
                    ColDbParams.Add(new DbParam("NGAY", ODT_LichLyThuyetChiTietFilter.NGAY));
                    Query += " and NGAY = " + ActionSqlParam.SpecialChar + "NGAY ";
                }
                if (!string.IsNullOrEmpty(ODT_LichLyThuyetChiTietFilter.GIANGVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("GIANGVIEN_ID", ODT_LichLyThuyetChiTietFilter.GIANGVIEN_ID));
                    Query += " and GIANGVIEN_ID = " + ActionSqlParam.SpecialChar + "GIANGVIEN_ID ";
                }
                if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == true)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI =" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                else if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == false)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI <>" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                Query += " order by NGAY, THOIGIAN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichLyThuyetChiTietCls[] DT_LichLyThuyetChiTiets = DT_LichLyThuyetChiTietParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichLyThuyetChiTiets;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter)
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
                if (ODT_LichLyThuyetChiTietFilter == null)
                {
                    ODT_LichLyThuyetChiTietFilter = new DT_LichLyThuyetChiTietFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == null)
                    Query = " select count(1) from DT_LichLyThuyetChiTiet where 1=1 ";
                else
                    Query = " select count(1) from DT_LichLyThuyetChiTiet llt inner join DT_KHOAHOC kh on llt.LICHLYTHUYET_ID = kh.id where 1=1 ";

                if (!string.IsNullOrEmpty(ODT_LichLyThuyetChiTietFilter.LICHLYTHUYET_ID))
                {
                    ColDbParams.Add(new DbParam("LICHLYTHUYET_ID", ODT_LichLyThuyetChiTietFilter.LICHLYTHUYET_ID));
                    Query += " and LICHLYTHUYET_ID = " + ActionSqlParam.SpecialChar + "LICHLYTHUYET_ID ";
                }
                if (ODT_LichLyThuyetChiTietFilter.NGAY != null)
                {
                    ColDbParams.Add(new DbParam("NGAY", ODT_LichLyThuyetChiTietFilter.NGAY));
                    Query += " and NGAY = " + ActionSqlParam.SpecialChar + "NGAY ";
                }
                if (!string.IsNullOrEmpty(ODT_LichLyThuyetChiTietFilter.GIANGVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("GIANGVIEN_ID", ODT_LichLyThuyetChiTietFilter.GIANGVIEN_ID));
                    Query += " and GIANGVIEN_ID = " + ActionSqlParam.SpecialChar + "GIANGVIEN_ID ";
                }
                if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == true)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI =" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                else if (ODT_LichLyThuyetChiTietFilter.ISKHOAHOCKETTHUC == false)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI <>" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }

                long result = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return result;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet)
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
                if (string.IsNullOrEmpty(ODT_LichLyThuyetChiTiet.ID))
                {
                    ODT_LichLyThuyetChiTiet.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LichLyThuyetChiTiet",
                    new DbParam[]{
                    new DbParam("ID",ODT_LichLyThuyetChiTiet.ID),
                    new DbParam("LICHLYTHUYET_ID",ODT_LichLyThuyetChiTiet.LICHLYTHUYET_ID),
                    new DbParam("NGAY",ODT_LichLyThuyetChiTiet.NGAY),
                    new DbParam("THOIGIAN",ODT_LichLyThuyetChiTiet.THOIGIAN),
                    new DbParam("THOIGIANKETTHUC",ODT_LichLyThuyetChiTiet.THOIGIANKETTHUC),
                    new DbParam("NOIDUNG",ODT_LichLyThuyetChiTiet.NOIDUNG),
                    new DbParam("GIANGVIEN_ID",ODT_LichLyThuyetChiTiet.GIANGVIEN_ID),
                    new DbParam("GHICHU",ODT_LichLyThuyetChiTiet.GHICHU),
                    new DbParam("HINHTHUCHOC",ODT_LichLyThuyetChiTiet.HINHTHUCHOC)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LichLyThuyetChiTiet", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHLYTHUYET_ID",ODT_LichLyThuyetChiTiet.LICHLYTHUYET_ID),
                   new DbParam("NGAY",ODT_LichLyThuyetChiTiet.NGAY),
                   new DbParam("THOIGIAN",ODT_LichLyThuyetChiTiet.THOIGIAN),
                   new DbParam("THOIGIANKETTHUC",ODT_LichLyThuyetChiTiet.THOIGIANKETTHUC),
                   new DbParam("NOIDUNG",ODT_LichLyThuyetChiTiet.NOIDUNG),
                   new DbParam("GIANGVIEN_ID",ODT_LichLyThuyetChiTiet.GIANGVIEN_ID),
                   new DbParam("GHICHU",ODT_LichLyThuyetChiTiet.GHICHU),
                   new DbParam("HINHTHUCHOC",ODT_LichLyThuyetChiTiet.HINHTHUCHOC)
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
                string DelQuery = " Delete from DT_LichLyThuyetChiTiet where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_LichLyThuyetChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LichLyThuyetChiTiet where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichLyThuyetChiTiet = DT_LichLyThuyetChiTietParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichLyThuyetChiTiet;
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
                DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet = CreateModel(ActionSqlParam, ID);
                ODT_LichLyThuyetChiTiet.ID = NewID;
                Add(ActionSqlParam, ODT_LichLyThuyetChiTiet);

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
