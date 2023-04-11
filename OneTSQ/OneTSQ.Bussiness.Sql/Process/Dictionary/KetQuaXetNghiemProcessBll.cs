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
    public class KetQuaXetNghiemProcessBll : KetQuaXetNghiemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlKetQuaXetNghiemProcessBll";
            }
        }
        public override KetQuaXetNghiemCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter)
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
                if (OKetQuaXetNghiemFilter == null)
                {
                    OKetQuaXetNghiemFilter = new KetQuaXetNghiemFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from KETQUAXETNGHIEM where 1=1 ";
                if (!string.IsNullOrEmpty(OKetQuaXetNghiemFilter.CABENHID))
                {
                    ColDbParams.Add(new DbParam("CABENHID", OKetQuaXetNghiemFilter.CABENHID));
                    Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
                }
                Query += " order by THOIGIAN asc ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                KetQuaXetNghiemCls[] KetQuaXetNghiems = KetQuaXetNghiemParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return KetQuaXetNghiems;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OKetQuaXetNghiemFilter == null)
                    OKetQuaXetNghiemFilter = new KetQuaXetNghiemFilterCls();
                string Query = " select COUNT (*) from KETQUAXETNGHIEM ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = KetQuaXetNghiemParser.CountFromDataTable(dsResult.Tables[0]);
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
        public override KetQuaXetNghiemCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OKetQuaXetNghiemFilter == null)
                    OKetQuaXetNghiemFilter = new KetQuaXetNghiemFilterCls();
                var skip = PageIndex * PageSize;
                string Query = " select * from KETQUAXETNGHIEM OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                KetQuaXetNghiemCls[] KetQuaXetNghiems = KetQuaXetNghiemParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return KetQuaXetNghiems;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemCls OKetQuaXetNghiem)
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
                DBService.Insert(ActionSqlParam.Trans, "KetQuaXetNghiem",
                    new DbParam[]{
                    new DbParam("ID",OKetQuaXetNghiem.ID),
                    new DbParam("CABENHID",OKetQuaXetNghiem.CABENHID),
                    new DbParam("DICHVUMA",OKetQuaXetNghiem.DICHVUMA),
                    new DbParam("DICHVUTEN",OKetQuaXetNghiem.DICHVUTEN),
                    new DbParam("THOIGIAN",OKetQuaXetNghiem.THOIGIAN),
                    new DbParam("CHANDOANMA",OKetQuaXetNghiem.CHANDOANMA),
                    new DbParam("KYTHUAT",OKetQuaXetNghiem.KYTHUAT),
                    new DbParam("LOAIMAU",OKetQuaXetNghiem.LOAIMAU),
                    new DbParam("KETLUAN",OKetQuaXetNghiem.KETLUAN),
                    new DbParam("NHANXET",OKetQuaXetNghiem.NHANXET),
                    new DbParam("DENGHI",OKetQuaXetNghiem.DENGHI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, KetQuaXetNghiemCls OKetQuaXetNghiem)
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
                DBService.Update(ActionSqlParam.Trans, "KETQUAXETNGHIEM", "ID", ID,
                   new DbParam[]{
                    new DbParam("CABENHID",OKetQuaXetNghiem.CABENHID),
                    new DbParam("DICHVUMA",OKetQuaXetNghiem.DICHVUMA),
                    new DbParam("DICHVUTEN",OKetQuaXetNghiem.DICHVUTEN),
                    new DbParam("THOIGIAN",OKetQuaXetNghiem.THOIGIAN),
                    new DbParam("CHANDOANMA",OKetQuaXetNghiem.CHANDOANMA),
                    new DbParam("KYTHUAT",OKetQuaXetNghiem.KYTHUAT),
                    new DbParam("LOAIMAU",OKetQuaXetNghiem.LOAIMAU),
                    new DbParam("KETLUAN",OKetQuaXetNghiem.KETLUAN),
                    new DbParam("NHANXET",OKetQuaXetNghiem.NHANXET),
                    new DbParam("DENGHI",OKetQuaXetNghiem.DENGHI)
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
                                  " Delete from KetQuaXetNghiemChiTiet where KETQUAXETNGHIEM_ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from KetQuaXetNghiem where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override KetQuaXetNghiemCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from KETQUAXETNGHIEM where (ID=" + ActionSqlParam.SpecialChar + "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
                KetQuaXetNghiemCls OKetQuaXetNghiem = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OKetQuaXetNghiem = KetQuaXetNghiemParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OKetQuaXetNghiem;
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
                KetQuaXetNghiemCls OKetQuaXetNghiem = CreateModel(ActionSqlParam, ID);
                OKetQuaXetNghiem.ID = NewID;
                Add(ActionSqlParam, OKetQuaXetNghiem);

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
