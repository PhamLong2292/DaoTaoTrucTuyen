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
    public class DT_LichThucHanhChiTietProcessBll : DT_LichThucHanhChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichThucHanhChiTietProcessBll";
            }
        }
        public override DT_LichThucHanhChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter)
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
                if (ODT_LichThucHanhChiTietFilter == null)
                {
                    ODT_LichThucHanhChiTietFilter = new DT_LichThucHanhChiTietFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();

                string Query = " select lthct.* from DT_LichThucHanhChiTiet lthct ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.KHOAHOC_ID) || ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC != null || !string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                    Query += " inner join DT_LichThucHanh lth on lth.ID = lthct.LICHTHUCHANH_ID ";
                if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC != null)
                    Query += " inner join DT_KHOAHOC kh on lth.KHOAHOC_ID = kh.id ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                    Query += " inner join DT_LichThucHanh_HocVien lthhv on lthhv.LICHTHUCHANH_ID = lth.ID ";
                Query += " where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.KHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_LichThucHanhChiTietFilter.KHOAHOC_ID));
                    Query += " and lth.KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.LICHTHUCHANH_ID))
                {
                    ColDbParams.Add(new DbParam("LICHTHUCHANH_ID", ODT_LichThucHanhChiTietFilter.LICHTHUCHANH_ID));
                    Query += " and lthct.LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LICHTHUCHANH_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("HOCVIEN_ID", ODT_LichThucHanhChiTietFilter.HOCVIEN_ID));
                    Query += " and lthhv.HOCVIEN_ID = " + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                }
                if (ODT_LichThucHanhChiTietFilter.NGAY != null)
                {
                    ColDbParams.Add(new DbParam("NGAY", ODT_LichThucHanhChiTietFilter.NGAY));
                    Query += " and lthct.NGAY = " + ActionSqlParam.SpecialChar + "NGAY ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.GIANGVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("GIANGVIEN_ID", ODT_LichThucHanhChiTietFilter.GIANGVIEN_ID));
                    Query += " and lthct.GIANGVIEN_ID = " + ActionSqlParam.SpecialChar + "GIANGVIEN_ID ";
                }
                if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC == true)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI =" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                else if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC == false)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI <>" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                Query += " order by NGAY, THOIGIAN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichThucHanhChiTietCls[] DT_LichThucHanhChiTiets = DT_LichThucHanhChiTietParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichThucHanhChiTiets;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet)
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
                if (string.IsNullOrEmpty(ODT_LichThucHanhChiTiet.ID))
                {
                    ODT_LichThucHanhChiTiet.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LichThucHanhChiTiet",
                    new DbParam[]{
                    new DbParam("ID",ODT_LichThucHanhChiTiet.ID),
                    new DbParam("LICHTHUCHANH_ID",ODT_LichThucHanhChiTiet.LICHTHUCHANH_ID),
                    new DbParam("NGAY",ODT_LichThucHanhChiTiet.NGAY),
                    new DbParam("THOIGIAN",ODT_LichThucHanhChiTiet.THOIGIAN),
                    new DbParam("THOIGIANKETTHUC",ODT_LichThucHanhChiTiet.THOIGIANKETTHUC),
                    new DbParam("NOIDUNG",ODT_LichThucHanhChiTiet.NOIDUNG),
                    new DbParam("GIANGVIEN_ID",ODT_LichThucHanhChiTiet.GIANGVIEN_ID),
                    new DbParam("GHICHU",ODT_LichThucHanhChiTiet.GHICHU)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LichThucHanhChiTiet", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHTHUCHANH_ID",ODT_LichThucHanhChiTiet.LICHTHUCHANH_ID),
                   new DbParam("NGAY",ODT_LichThucHanhChiTiet.NGAY),
                   new DbParam("THOIGIAN",ODT_LichThucHanhChiTiet.THOIGIAN),
                   new DbParam("THOIGIANKETTHUC",ODT_LichThucHanhChiTiet.THOIGIANKETTHUC),
                   new DbParam("NOIDUNG",ODT_LichThucHanhChiTiet.NOIDUNG),
                   new DbParam("GIANGVIEN_ID",ODT_LichThucHanhChiTiet.GIANGVIEN_ID),
                   new DbParam("GHICHU",ODT_LichThucHanhChiTiet.GHICHU)
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
                string DelQuery = " Delete from DT_LichThucHanhChiTiet where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_LichThucHanhChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LichThucHanhChiTiet where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichThucHanhChiTiet = DT_LichThucHanhChiTietParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichThucHanhChiTiet;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter)
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
                if (ODT_LichThucHanhChiTietFilter == null)
                {
                    ODT_LichThucHanhChiTietFilter = new DT_LichThucHanhChiTietFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from DT_LichThucHanhChiTiet lthct ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.KHOAHOC_ID) || ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC != null || !string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                    Query += " inner join DT_LichThucHanh lth on lth.ID = lthct.LICHTHUCHANH_ID ";
                if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC != null)
                    Query += " inner join DT_KHOAHOC kh on lth.KHOAHOC_ID = kh.id ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                    Query += " inner join DT_LichThucHanh_HocVien lthhv on lthhv.LICHTHUCHANH_ID = lth.ID ";
                Query += " where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.KHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_LichThucHanhChiTietFilter.KHOAHOC_ID));
                    Query += " and lth.KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.LICHTHUCHANH_ID))
                {
                    ColDbParams.Add(new DbParam("LICHTHUCHANH_ID", ODT_LichThucHanhChiTietFilter.LICHTHUCHANH_ID));
                    Query += " and lthct.LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LICHTHUCHANH_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.HOCVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("HOCVIEN_ID", ODT_LichThucHanhChiTietFilter.HOCVIEN_ID));
                    Query += " and lthhv.HOCVIEN_ID = " + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                }
                if (ODT_LichThucHanhChiTietFilter.NGAY != null)
                {
                    ColDbParams.Add(new DbParam("NGAY", ODT_LichThucHanhChiTietFilter.NGAY));
                    Query += " and lthct.NGAY = " + ActionSqlParam.SpecialChar + "NGAY ";
                }
                if (!string.IsNullOrEmpty(ODT_LichThucHanhChiTietFilter.GIANGVIEN_ID))
                {
                    ColDbParams.Add(new DbParam("GIANGVIEN_ID", ODT_LichThucHanhChiTietFilter.GIANGVIEN_ID));
                    Query += " and lthct.GIANGVIEN_ID = " + ActionSqlParam.SpecialChar + "GIANGVIEN_ID ";
                }
                if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC == true)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAIKHOAHOC", (int)DT_KhoaHocCls.eTrangThai.KetThuc));
                    Query += " and kh.TRANGTHAI =" + ActionSqlParam.SpecialChar + "TRANGTHAIKHOAHOC ";
                }
                else if (ODT_LichThucHanhChiTietFilter.ISKHOAHOCKETTHUC == false)
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
                DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet = CreateModel(ActionSqlParam, ID);
                ODT_LichThucHanhChiTiet.ID = NewID;
                Add(ActionSqlParam, ODT_LichThucHanhChiTiet);

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
