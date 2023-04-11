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
    public class DT_DiemDanhThucHanhProcessBll : DT_DiemDanhThucHanhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DiemDanhThucHanhProcessBll";
            }
        }
        public override DT_DiemDanhThucHanhCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter)
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
                if (ODT_DiemDanhThucHanhFilter == null)
                {
                    ODT_DiemDanhThucHanhFilter = new DT_DiemDanhThucHanhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                if (!string.IsNullOrEmpty(ODT_DiemDanhThucHanhFilter.LichThucHanhId))
                {
                    ColDbParams.Add(new DbParam("LICHTHUCHANH_ID", ODT_DiemDanhThucHanhFilter.LichThucHanhId));
                    Query = " select DT_DIEMDANHTHUCHANH.* from DT_DIEMDANHTHUCHANH join DT_LICHTHUCHANHCHITIET on DT_DIEMDANHTHUCHANH.LICHTHUCHANHCHITIET_ID = DT_LICHTHUCHANHCHITIET.ID " +
                        " where DT_LICHTHUCHANHCHITIET.LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LICHTHUCHANH_ID ";
                }
                else
                    Query = " select * from DT_DIEMDANHTHUCHANH where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_DiemDanhThucHanhFilter.LichThucHanhChiTietId))
                {
                    ColDbParams.Add(new DbParam("LICHTHUCHANHCHITIET_ID", ODT_DiemDanhThucHanhFilter.LichThucHanhChiTietId));
                    Query += " and LICHTHUCHANHCHITIET_ID = " + ActionSqlParam.SpecialChar + "LICHTHUCHANHCHITIET_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_DiemDanhThucHanhFilter.HocVienId))
                {
                    ColDbParams.Add(new DbParam("HOCVIEN_ID", ODT_DiemDanhThucHanhFilter.HocVienId));
                    Query += " and HOCVIEN_ID = " + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                }

                DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_DiemDanhThucHanhCls[] DT_DiemDanhThucHanhs = DT_DiemDanhThucHanhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_DiemDanhThucHanhs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh)
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
                if (string.IsNullOrEmpty(ODT_DiemDanhThucHanh.ID))
                {
                    ODT_DiemDanhThucHanh.ID = System.Guid.NewGuid().ToString();
                }
                string Query = "select * from DT_DiemDanhThucHanh where LICHTHUCHANHCHITIET_ID=" + ActionSqlParam.SpecialChar + "LICHTHUCHANHCHITIET_ID and HOCVIEN_ID=" + ActionSqlParam.SpecialChar + "HOCVIEN_ID ";
                DataTable dtCheck = DBService.GetDataTable(ActionSqlParam.Trans, Query, new DbParam[]
                {
               new DbParam("LICHTHUCHANHCHITIET_ID",ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID),
               new DbParam("HOCVIEN_ID",ODT_DiemDanhThucHanh.HOCVIEN_ID)
                });

                if (dtCheck.Rows.Count == 0)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DT_DiemDanhThucHanh",
                        new DbParam[]{
                        new DbParam("ID",ODT_DiemDanhThucHanh.ID),
                       new DbParam("LICHTHUCHANHCHITIET_ID",ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID),
                       new DbParam("HOCVIEN_ID",ODT_DiemDanhThucHanh.HOCVIEN_ID)
                    });
                }
                else
                {
                    string ID = (string)dtCheck.Rows[0]["ID"];
                    DBService.Update(ActionSqlParam.Trans, "DT_DiemDanhThucHanh", "ID", ID,
                        new DbParam[]{
                       new DbParam("LICHTHUCHANHCHITIET_ID",ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID),
                       new DbParam("HOCVIEN_ID",ODT_DiemDanhThucHanh.HOCVIEN_ID)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh)
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
                DBService.Update(ActionSqlParam.Trans, "DT_DiemDanhThucHanh", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHTHUCHANHCHITIET_ID",ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID),
                   new DbParam("HOCVIEN_ID",ODT_DiemDanhThucHanh.HOCVIEN_ID)
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
                string DelQuery = " Delete from DT_DiemDanhThucHanh where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_DiemDanhThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_DiemDanhThucHanh where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_DiemDanhThucHanh = DT_DiemDanhThucHanhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_DiemDanhThucHanh;
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
                DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh = CreateModel(ActionSqlParam, ID);
                ODT_DiemDanhThucHanh.ID = NewID;
                Add(ActionSqlParam, ODT_DiemDanhThucHanh);

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
