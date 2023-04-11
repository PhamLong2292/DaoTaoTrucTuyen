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
    public class BacSyOwnerUserProcessBll : BacSyOwnerUserTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlBacSyOwnerUserProcessBll";
            }
        }
        public override BacSyOwnerUserCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            BacSyOwnerUserFilterCls OBacSyOwnerUserFilter)
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
                if (OBacSyOwnerUserFilter == null)
                {
                    OBacSyOwnerUserFilter = new BacSyOwnerUserFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from BacSyOwnerUser where 1=1 ";
                if (!string.IsNullOrEmpty(OBacSyOwnerUserFilter.BACSYID))
                {
                    ColDbParams.Add(new DbParam("BACSYID", OBacSyOwnerUserFilter.BACSYID));
                    Query += " and BACSYID = " + ActionSqlParam.SpecialChar + "BACSYID";
                }
                if (!string.IsNullOrEmpty(OBacSyOwnerUserFilter.OWNERUSERID))
                {
                    ColDbParams.Add(new DbParam("OWNERUSERID", OBacSyOwnerUserFilter.OWNERUSERID));
                    Query += " and OWNERUSERID = " + ActionSqlParam.SpecialChar + "OWNERUSERID";
                }
                Query += " order by OWNERUSERID";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BacSyOwnerUserCls[] BacSyOwnerUsers = BacSyOwnerUserParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return BacSyOwnerUsers;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, BacSyOwnerUserCls OBacSyOwnerUser)
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
                if (string.IsNullOrEmpty(OBacSyOwnerUser.ID))
                {
                    OBacSyOwnerUser.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "BacSyOwnerUser",
                    new DbParam[]{
                    new DbParam("ID",OBacSyOwnerUser.ID),
                    new DbParam("BACSYID",OBacSyOwnerUser.BACSYID),
                    new DbParam("OWNERUSERID",OBacSyOwnerUser.OWNERUSERID)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BacSyOwnerUserCls OBacSyOwnerUser)
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
                DBService.Update(ActionSqlParam.Trans, "BacSyOwnerUser", "ID", ID,
                    new DbParam[]{
                   new DbParam("BACSYID",OBacSyOwnerUser.BACSYID),
                   new DbParam("OWNERUSERID",OBacSyOwnerUser.OWNERUSERID)
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
                string DelQuery = " Delete from BacSyOwnerUser where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override BacSyOwnerUserCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from BacSyOwnerUser where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                BacSyOwnerUserCls OBacSyOwnerUser = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OBacSyOwnerUser = BacSyOwnerUserParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OBacSyOwnerUser;
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
                BacSyOwnerUserCls OBacSyOwnerUser = CreateModel(ActionSqlParam, ID);
                OBacSyOwnerUser.ID = NewID;
                Add(ActionSqlParam, OBacSyOwnerUser);

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
