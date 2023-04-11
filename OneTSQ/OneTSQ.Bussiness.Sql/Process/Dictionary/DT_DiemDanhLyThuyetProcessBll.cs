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
    public class DT_DiemDanhLyThuyetProcessBll : DT_DiemDanhLyThuyetTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DiemDanhLyThuyetProcessBll";
            }
        }
        public override DT_DiemDanhLyThuyetCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter)
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
                if (ODT_DiemDanhLyThuyetFilter == null)
                {
                    ODT_DiemDanhLyThuyetFilter = new DT_DiemDanhLyThuyetFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                if (!string.IsNullOrEmpty(ODT_DiemDanhLyThuyetFilter.KhoaHocId))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_DiemDanhLyThuyetFilter.KhoaHocId));
                    Query = " select DT_DiemDanhLyThuyet.* from DT_DiemDanhLyThuyet join DT_LichLyThuyetChiTiet on DT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID = DT_LichLyThuyetChiTiet.ID " +
                        " where DT_LichLyThuyetChiTiet.LICHLYTHUYET_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                else
                    Query = " select * from DT_DiemDanhLyThuyet where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_DiemDanhLyThuyetFilter.LichLyThuyetChiTietId))
                {
                    ColDbParams.Add(new DbParam("LICHLYTHUYETCHITIET_ID", ODT_DiemDanhLyThuyetFilter.LichLyThuyetChiTietId));
                    Query += " and LICHLYTHUYETCHITIET_ID = " + ActionSqlParam.SpecialChar + "LICHLYTHUYETCHITIET_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_DiemDanhLyThuyetFilter.HocVienId))
                {
                    ColDbParams.Add(new DbParam("HOCVIEN_ID", ODT_DiemDanhLyThuyetFilter.HocVienId));
                    Query += " and HOCVIEN_ID = " + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                }

                DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_DiemDanhLyThuyetCls[] DT_DiemDanhLyThuyets = DT_DiemDanhLyThuyetParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_DiemDanhLyThuyets;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet)
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
                if (string.IsNullOrEmpty(ODT_DiemDanhLyThuyet.ID))
                {
                    ODT_DiemDanhLyThuyet.ID = System.Guid.NewGuid().ToString();
                }
                string Query = "select * from DT_DiemDanhLyThuyet where LICHLYTHUYETCHITIET_ID=" + ActionSqlParam.SpecialChar + "LICHLYTHUYETCHITIET_ID and HOCVIEN_ID=" + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                DataTable dtCheck = DBService.GetDataTable(ActionSqlParam.Trans, Query, new DbParam[]
                {
               new DbParam("LICHLYTHUYETCHITIET_ID",ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID),
               new DbParam("HOCVIEN_ID",ODT_DiemDanhLyThuyet.HOCVIEN_ID)
                });

                if (dtCheck.Rows.Count == 0)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DT_DiemDanhLyThuyet",
                        new DbParam[]{
                        new DbParam("ID",ODT_DiemDanhLyThuyet.ID),
                       new DbParam("LICHLYTHUYETCHITIET_ID",ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID),
                       new DbParam("HOCVIEN_ID",ODT_DiemDanhLyThuyet.HOCVIEN_ID)
                    });
                }
                else
                {
                    string ID = (string)dtCheck.Rows[0]["ID"];
                    DBService.Update(ActionSqlParam.Trans, "DT_DiemDanhLyThuyet", "ID", ID,
                        new DbParam[]{
                       new DbParam("LICHLYTHUYETCHITIET_ID",ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID),
                       new DbParam("HOCVIEN_ID",ODT_DiemDanhLyThuyet.HOCVIEN_ID)
                    });
                }
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet)
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
                DBService.Update(ActionSqlParam.Trans, "DT_DiemDanhLyThuyet", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHLYTHUYETCHITIET_ID",ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID),
                   new DbParam("HOCVIEN_ID",ODT_DiemDanhLyThuyet.HOCVIEN_ID)
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
                string DelQuery = " Delete from DT_DiemDanhLyThuyet where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_DiemDanhLyThuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_DiemDanhLyThuyet where ID=" + ActionSqlParam.SpecialChar + "ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_DiemDanhLyThuyet = DT_DiemDanhLyThuyetParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_DiemDanhLyThuyet;
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
                DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet = CreateModel(ActionSqlParam, ID);
                ODT_DiemDanhLyThuyet.ID = NewID;
                Add(ActionSqlParam, ODT_DiemDanhLyThuyet);

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
