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
    public class KetQuaXetNghiemChiTietProcessBll : KetQuaXetNghiemChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlKetQuaXetNghiemChiTietProcessBll";
            }
        }
        public override KetQuaXetNghiemChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter)
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
                if (OKetQuaXetNghiemChiTietFilter == null)
                {
                    OKetQuaXetNghiemChiTietFilter = new KetQuaXetNghiemChiTietFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from KetQuaXetNghiemChiTiet where 1=1 ";
                if (!string.IsNullOrEmpty(OKetQuaXetNghiemChiTietFilter.KETQUAXETNGHIEMID))
                {
                    ColDbParams.Add(new DbParam("KETQUAXETNGHIEMID", OKetQuaXetNghiemChiTietFilter.KETQUAXETNGHIEMID));
                    Query += " and KETQUAXETNGHIEM_ID = " + ActionSqlParam.SpecialChar + "KETQUAXETNGHIEMID";
                }
                if (!string.IsNullOrEmpty(OKetQuaXetNghiemChiTietFilter.CABENHID))
                {
                    ColDbParams.Add(new DbParam("CABENHID", OKetQuaXetNghiemChiTietFilter.CABENHID));
                    Query += " and KETQUAXETNGHIEM_ID IN (select ID from KETQUAXETNGHIEM where CABENHID =" + ActionSqlParam.SpecialChar + "CABENHID)";
                }
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                KetQuaXetNghiemChiTietCls[] KetQuaXetNghiemChiTiets = KetQuaXetNghiemChiTietParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return KetQuaXetNghiemChiTiets;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OKetQuaXetNghiemChiTietFilter == null)
                    OKetQuaXetNghiemChiTietFilter = new KetQuaXetNghiemChiTietFilterCls();
                string Query = " select COUNT (*) from KetQuaXetNghiemChiTiet ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = KetQuaXetNghiemChiTietParser.CountFromDataTable(dsResult.Tables[0]);
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
        public override KetQuaXetNghiemChiTietCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OKetQuaXetNghiemChiTietFilter == null)
                    OKetQuaXetNghiemChiTietFilter = new KetQuaXetNghiemChiTietFilterCls();
                var skip = PageIndex * PageSize;
                string Query = " select * from KetQuaXetNghiemChiTiet OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                KetQuaXetNghiemChiTietCls[] KetQuaXetNghiemChiTiets = KetQuaXetNghiemChiTietParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return KetQuaXetNghiemChiTiets;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet)
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
                DBService.Insert(ActionSqlParam.Trans, "KetQuaXetNghiemChiTiet",
                    new DbParam[]{
                    new DbParam("ID",OKetQuaXetNghiemChiTiet.ID),
                    new DbParam("KETQUAXETNGHIEM_ID",OKetQuaXetNghiemChiTiet.KETQUAXETNGHIEM_ID),
                    new DbParam("CHISOMA",OKetQuaXetNghiemChiTiet.CHISOMA),
                    new DbParam("CHISOTEN",OKetQuaXetNghiemChiTiet.CHISOTEN),
                    new DbParam("GIATRI",OKetQuaXetNghiemChiTiet.GIATRI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet)
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
                DBService.Update(ActionSqlParam.Trans, "KetQuaXetNghiemChiTiet", "ID", ID,
                   new DbParam[]{
               new DbParam("KETQUAXETNGHIEM_ID",OKetQuaXetNghiemChiTiet.KETQUAXETNGHIEM_ID),
               new DbParam("CHISOMA",OKetQuaXetNghiemChiTiet.CHISOMA),
               new DbParam("CHISOTEN",OKetQuaXetNghiemChiTiet.CHISOTEN),
               new DbParam("GIATRI",OKetQuaXetNghiemChiTiet.GIATRI)
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
                string DelQuery = " Delete from KetQuaXetNghiemChiTiet where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override KetQuaXetNghiemChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from KetQuaXetNghiemChiTiet where (ID=" + ActionSqlParam.SpecialChar + "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
                KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OKetQuaXetNghiemChiTiet = KetQuaXetNghiemChiTietParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OKetQuaXetNghiemChiTiet;
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
                KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet = CreateModel(ActionSqlParam, ID);
                OKetQuaXetNghiemChiTiet.ID = NewID;
                Add(ActionSqlParam, OKetQuaXetNghiemChiTiet);

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
