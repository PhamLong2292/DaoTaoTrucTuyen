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
    public class DT_KeHoachLopProcessBll : DT_KeHoachLopTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_KeHoachLopProcessBll";
            }
        }
        public override DT_KeHoachLopCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_KeHoachLopFilterCls ODT_KeHoachLopFilter)
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
                if (ODT_KeHoachLopFilter == null)
                {
                    ODT_KeHoachLopFilter = new DT_KeHoachLopFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_KeHoachLop where 1=1 ";
                Query += " order by BATDAU desc";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_KeHoachLopCls[] DT_KeHoachLops = DT_KeHoachLopParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_KeHoachLops;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_KeHoachLopCls ODT_KeHoachLop)
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
                if (string.IsNullOrEmpty(ODT_KeHoachLop.ID))
                {
                    ODT_KeHoachLop.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_KeHoachLop",
                    new DbParam[]{
                    new DbParam("ID",ODT_KeHoachLop.ID),
                    new DbParam("BATDAU",ODT_KeHoachLop.BATDAU),
                    new DbParam("KETTHUC",ODT_KeHoachLop.KETTHUC),
                    new DbParam("THOIGIANTIEPDON",ODT_KeHoachLop.THOIGIANTIEPDON),
                    new DbParam("DIADIEMTIEPDON",ODT_KeHoachLop.DIADIEMTIEPDON),
                    new DbParam("BATDAULT",ODT_KeHoachLop.BATDAULT),
                    new DbParam("KETTHUCLT",ODT_KeHoachLop.KETTHUCLT),
                    new DbParam("DIADIEMLT",ODT_KeHoachLop.DIADIEMLT),
                    new DbParam("BATDAUTH",ODT_KeHoachLop.BATDAUTH),
                    new DbParam("KETTHUCTH",ODT_KeHoachLop.KETTHUCTH),
                    new DbParam("DIADIEMTH",ODT_KeHoachLop.DIADIEMTH),
                    new DbParam("SOLUONGNHOMTH",ODT_KeHoachLop.SOLUONGNHOMTH),
                    new DbParam("SOHVTRONGNHOMTH",ODT_KeHoachLop.SOHVTRONGNHOMTH),
                    new DbParam("THOIGIANDANHGIATDT",ODT_KeHoachLop.THOIGIANDANHGIATDT),
                    new DbParam("DIADIEMDANHGIATDT",ODT_KeHoachLop.DIADIEMDANHGIATDT),
                    new DbParam("THOIGIANGIAIDAPTHACMAC",ODT_KeHoachLop.THOIGIANGIAIDAPTHACMAC),
                    new DbParam("DIADIEMGIAIDAPTHACMAC",ODT_KeHoachLop.DIADIEMGIAIDAPTHACMAC),
                    new DbParam("BATDAUTHILT",ODT_KeHoachLop.BATDAUTHILT),
                    new DbParam("KETTHUCTHILT",ODT_KeHoachLop.KETTHUCTHILT),
                    new DbParam("DIADIEMTHILT",ODT_KeHoachLop.DIADIEMTHILT),
                    new DbParam("BATDAUTHIVD",ODT_KeHoachLop.BATDAUTHIVD),
                    new DbParam("KETTHUCTHIVD",ODT_KeHoachLop.KETTHUCTHIVD),
                    new DbParam("DIADIEMTHIVD",ODT_KeHoachLop.DIADIEMTHIVD),
                    new DbParam("BATDAUTHITH",ODT_KeHoachLop.BATDAUTHITH),
                    new DbParam("KETTHUCTHITH",ODT_KeHoachLop.KETTHUCTHITH),
                    new DbParam("DIADIEMTHITH",ODT_KeHoachLop.DIADIEMTHITH),
                    new DbParam("THOIGIANBEGIANG",ODT_KeHoachLop.THOIGIANBEGIANG),
                    new DbParam("DIADIEMBEGIANG",ODT_KeHoachLop.DIADIEMBEGIANG),
                    new DbParam("LANHDAO_ID",ODT_KeHoachLop.LANHDAO_ID),
                    new DbParam("NGUOILAP",ODT_KeHoachLop.NGUOILAP),
                    new DbParam("NGUOITAO_ID",ODT_KeHoachLop.NGUOITAO_ID),
                    new DbParam("NGAYTAO",ODT_KeHoachLop.NGAYTAO),
                    new DbParam("NGUOISUA_ID",ODT_KeHoachLop.NGUOISUA_ID),
                    new DbParam("NGAYSUA",ODT_KeHoachLop.NGAYSUA)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_KeHoachLopCls ODT_KeHoachLop)
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
                DBService.Update(ActionSqlParam.Trans, "DT_KeHoachLop", "ID", ID,
                    new DbParam[]{
                   new DbParam("BATDAU",ODT_KeHoachLop.BATDAU),
                   new DbParam("KETTHUC",ODT_KeHoachLop.KETTHUC),
                   new DbParam("THOIGIANTIEPDON",ODT_KeHoachLop.THOIGIANTIEPDON),
                   new DbParam("DIADIEMTIEPDON",ODT_KeHoachLop.DIADIEMTIEPDON),
                   new DbParam("BATDAULT",ODT_KeHoachLop.BATDAULT),
                   new DbParam("KETTHUCLT",ODT_KeHoachLop.KETTHUCLT),
                   new DbParam("DIADIEMLT",ODT_KeHoachLop.DIADIEMLT),
                   new DbParam("BATDAUTH",ODT_KeHoachLop.BATDAUTH),
                   new DbParam("KETTHUCTH",ODT_KeHoachLop.KETTHUCTH),
                   new DbParam("DIADIEMTH",ODT_KeHoachLop.DIADIEMTH),
                   new DbParam("SOLUONGNHOMTH",ODT_KeHoachLop.SOLUONGNHOMTH),
                   new DbParam("SOHVTRONGNHOMTH",ODT_KeHoachLop.SOHVTRONGNHOMTH),
                   new DbParam("THOIGIANDANHGIATDT",ODT_KeHoachLop.THOIGIANDANHGIATDT),
                   new DbParam("DIADIEMDANHGIATDT",ODT_KeHoachLop.DIADIEMDANHGIATDT),
                   new DbParam("THOIGIANGIAIDAPTHACMAC",ODT_KeHoachLop.THOIGIANGIAIDAPTHACMAC),
                   new DbParam("DIADIEMGIAIDAPTHACMAC",ODT_KeHoachLop.DIADIEMGIAIDAPTHACMAC),
                   new DbParam("BATDAUTHILT",ODT_KeHoachLop.BATDAUTHILT),
                   new DbParam("KETTHUCTHILT",ODT_KeHoachLop.KETTHUCTHILT),
                   new DbParam("DIADIEMTHILT",ODT_KeHoachLop.DIADIEMTHILT),
                   new DbParam("BATDAUTHIVD",ODT_KeHoachLop.BATDAUTHIVD),
                   new DbParam("KETTHUCTHIVD",ODT_KeHoachLop.KETTHUCTHIVD),
                   new DbParam("DIADIEMTHIVD",ODT_KeHoachLop.DIADIEMTHIVD),
                   new DbParam("BATDAUTHITH",ODT_KeHoachLop.BATDAUTHITH),
                   new DbParam("KETTHUCTHITH",ODT_KeHoachLop.KETTHUCTHITH),
                   new DbParam("DIADIEMTHITH",ODT_KeHoachLop.DIADIEMTHITH),
                   new DbParam("THOIGIANBEGIANG",ODT_KeHoachLop.THOIGIANBEGIANG),
                   new DbParam("DIADIEMBEGIANG",ODT_KeHoachLop.DIADIEMBEGIANG),
                   new DbParam("LANHDAO_ID",ODT_KeHoachLop.LANHDAO_ID),
                   new DbParam("NGUOILAP",ODT_KeHoachLop.NGUOILAP),
                   new DbParam("NGUOITAO_ID",ODT_KeHoachLop.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_KeHoachLop.NGAYTAO),
                   new DbParam("NGUOISUA_ID",ODT_KeHoachLop.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_KeHoachLop.NGAYSUA)
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
                string DelQuery = " Delete from DT_KeHoachLop where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_KeHoachLopCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_KeHoachLop where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_KeHoachLopCls ODT_KeHoachLop = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_KeHoachLop = DT_KeHoachLopParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_KeHoachLop;
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
                DT_KeHoachLopCls ODT_KeHoachLop = CreateModel(ActionSqlParam, ID);
                ODT_KeHoachLop.ID = NewID;
                Add(ActionSqlParam, ODT_KeHoachLop);

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
