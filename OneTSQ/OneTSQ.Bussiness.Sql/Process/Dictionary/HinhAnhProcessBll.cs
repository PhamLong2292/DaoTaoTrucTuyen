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
    public class HinhAnhProcessBll : HinhAnhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlHinhAnhProcessBll";
            }
        }
        public override HinhAnhCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            HinhAnhFilterCls OHinhAnhFilter)
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
                if (OHinhAnhFilter == null)
                {
                    OHinhAnhFilter = new HinhAnhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from HINHANH where 1=1 ";
                if (!string.IsNullOrEmpty(OHinhAnhFilter.CABENHID))
                {
                    ColDbParams.Add(new DbParam("CABENHID", OHinhAnhFilter.CABENHID));
                    Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
                }
                Query += " order by TIMEEX";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                HinhAnhCls[] HinhAnhs = HinhAnhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return HinhAnhs;
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
        public override HinhAnhCls[] PageReading(ActionSqlParamCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal)
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
                if (OHinhAnhFilter == null)
                {
                    OHinhAnhFilter = new HinhAnhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from HINHANH where 1=1 ";
                string recordTotalQuery = " select count(1) from HINHANH where 1 = 1 ";
                if (!string.IsNullOrEmpty(OHinhAnhFilter.CABENHID))
                {
                    ColDbParams.Add(new DbParam("CABENHID", OHinhAnhFilter.CABENHID));
                    Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID ";
                    recordTotalQuery += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID ";
                }
                Query += "ORDER BY TIMEEX " +
                    "OFFSET " + (OHinhAnhFilter.PageIndex * OHinhAnhFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + OHinhAnhFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                HinhAnhCls[] HinhAnhs = HinhAnhParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return HinhAnhs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, HinhAnhCls OHinhAnh)
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
                if (string.IsNullOrEmpty(OHinhAnh.ID))
                {
                    OHinhAnh.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "HINHANH",
                    new DbParam[]{
                    new DbParam("ID",OHinhAnh.ID),
                    new DbParam("CABENHID",OHinhAnh.CABENHID),
                    new DbParam("TENTEP",OHinhAnh.TENTEP),
                    new DbParam("TENHIENTHI",OHinhAnh.TENHIENTHI),
                    new DbParam("KEY",OHinhAnh.KEY),
                    new DbParam("SCODE",OHinhAnh.SCODE),
                    new DbParam("LINK",OHinhAnh.LINK),
                    new DbParam("MODALITY",OHinhAnh.MODALITY),
                    new DbParam("TIMEEX",OHinhAnh.TIMEEX),
                    new DbParam("MOTA",OHinhAnh.MOTA),
                    new DbParam("TYPE",OHinhAnh.TYPE)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, HinhAnhCls OHinhAnh)
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
                DBService.Update(ActionSqlParam.Trans, "HINHANH", "ID", ID,
                    new DbParam[]{
                   new DbParam("CABENHID",OHinhAnh.CABENHID),
                   new DbParam("TENTEP",OHinhAnh.TENTEP),
                   new DbParam("TENHIENTHI",OHinhAnh.TENHIENTHI),
                   new DbParam("KEY",OHinhAnh.KEY),
                   new DbParam("SCODE",OHinhAnh.SCODE),
                   new DbParam("LINK",OHinhAnh.LINK),
                   new DbParam("MODALITY",OHinhAnh.MODALITY),
                   new DbParam("TIMEEX",OHinhAnh.TIMEEX),
                   new DbParam("MOTA",OHinhAnh.MOTA),
                   new DbParam("TYPE",OHinhAnh.TYPE)
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
                                  " Delete from BinhLuanHinhAnh where HinhAnhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from HINHANH where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override HinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from HINHANH where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                HinhAnhCls OHinhAnh = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OHinhAnh = HinhAnhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OHinhAnh;
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
                HinhAnhCls OHinhAnh = CreateModel(ActionSqlParam, ID);
                OHinhAnh.ID = NewID;
                Add(ActionSqlParam, OHinhAnh);

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
