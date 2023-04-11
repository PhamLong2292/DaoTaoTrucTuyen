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
    public class BinhLuanHinhAnhProcessBll : BinhLuanHinhAnhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlBinhLuanHinhAnhProcessBll";
            }
        }
        public override BinhLuanHinhAnhCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter)
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
                if (OBinhLuanHinhAnhFilter == null)
                {
                    OBinhLuanHinhAnhFilter = new BinhLuanHinhAnhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from BINHLUANHINHANH where 1=1 ";
                if (!string.IsNullOrEmpty(OBinhLuanHinhAnhFilter.HINHANHID))
                {
                    ColDbParams.Add(new DbParam("HINHANHID", OBinhLuanHinhAnhFilter.HINHANHID));
                    Query += " and HINHANHID = " + ActionSqlParam.SpecialChar + "HINHANHID";
                }
                if (!string.IsNullOrEmpty(OBinhLuanHinhAnhFilter.CABENHANHCLSID))
                {
                    ColDbParams.Add(new DbParam("CABENHANHCLSID", OBinhLuanHinhAnhFilter.CABENHANHCLSID));
                    Query += " and CABENHANHCLSID = " + ActionSqlParam.SpecialChar + "CABENHANHCLSID";
                }
                if (!string.IsNullOrEmpty(OBinhLuanHinhAnhFilter.BINHLUANHINHANHID))
                {
                    ColDbParams.Add(new DbParam("BINHLUANHINHANHID", OBinhLuanHinhAnhFilter.BINHLUANHINHANHID));
                    Query += " and BINHLUANHINHANHID = " + ActionSqlParam.SpecialChar + "BINHLUANHINHANHID";
                }
                if (OBinhLuanHinhAnhFilter.isParent == true)
                {
                    Query += " and BINHLUANHINHANHID is null ";
                }
                if (OBinhLuanHinhAnhFilter.isChildren == true)
                {
                    Query += " and BINHLUANHINHANHID is not null ";
                }
                Query += " order by THOIGIAN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BinhLuanHinhAnhCls[] BinhLuanHinhAnhs = BinhLuanHinhAnhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return BinhLuanHinhAnhs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, BinhLuanHinhAnhCls OBinhLuanHinhAnh)
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
                if (string.IsNullOrEmpty(OBinhLuanHinhAnh.ID))
                {
                    OBinhLuanHinhAnh.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "BINHLUANHINHANH",
                    new DbParam[]{
                    new DbParam("ID",OBinhLuanHinhAnh.ID),
                    new DbParam("HINHANHID",OBinhLuanHinhAnh.HINHANHID),
                    new DbParam("CABENHANHCLSID",OBinhLuanHinhAnh.CABENHANHCLSID),
                    new DbParam("BINHLUANHINHANHID",OBinhLuanHinhAnh.BINHLUANHINHANHID),
                    new DbParam("BACSYIDS",OBinhLuanHinhAnh.BACSYIDS),
                    new DbParam("BACSY",OBinhLuanHinhAnh.BACSY),
                    new DbParam("THOIGIAN",OBinhLuanHinhAnh.THOIGIAN),
                    new DbParam("NOIDUNG",OBinhLuanHinhAnh.NOIDUNG)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BinhLuanHinhAnhCls OBinhLuanHinhAnh)
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
                DBService.Update(ActionSqlParam.Trans, "BINHLUANHINHANH", "ID", ID,
                    new DbParam[]{
                    new DbParam("HINHANHID",OBinhLuanHinhAnh.HINHANHID),
                    new DbParam("CABENHANHCLSID",OBinhLuanHinhAnh.CABENHANHCLSID),
                    new DbParam("BINHLUANHINHANHID",OBinhLuanHinhAnh.BINHLUANHINHANHID),
                    new DbParam("BACSYIDS",OBinhLuanHinhAnh.BACSYIDS),
                    new DbParam("BACSY",OBinhLuanHinhAnh.BACSY),
                    new DbParam("THOIGIAN",OBinhLuanHinhAnh.THOIGIAN),
                    new DbParam("NOIDUNG",OBinhLuanHinhAnh.NOIDUNG)
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
                                  " Delete from BinhLuanHinhAnh where BinhLuanHinhAnhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from BinhLuanHinhAnh where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override BinhLuanHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from BINHLUANHINHANH where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                BinhLuanHinhAnhCls OBinhLuanHinhAnh = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OBinhLuanHinhAnh = BinhLuanHinhAnhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OBinhLuanHinhAnh;
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
                BinhLuanHinhAnhCls OBinhLuanHinhAnh = CreateModel(ActionSqlParam, ID);
                OBinhLuanHinhAnh.ID = NewID;
                Add(ActionSqlParam, OBinhLuanHinhAnh);

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
