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
    public class BacSyProcessBll : BacSyTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlBacSyProcessBll";
            }
        }
        public override BacSyCls[] Reading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter)
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
                if (OBacSyFilter == null)
                {
                    OBacSyFilter = new BacSyFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                if (!string.IsNullOrEmpty(OBacSyFilter.LICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("LICHHOICHANID", OBacSyFilter.LICHHOICHANID));
                    Query = " select BacSy.* from BacSy " +
                        " where BacSy.ID in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) ";
                }
                else if (!string.IsNullOrEmpty(OBacSyFilter.KHACLICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("KHACLICHHOICHANID", OBacSyFilter.KHACLICHHOICHANID));
                    Query = " select BacSy.* from BacSy " +
                        " where BacSy.ID not in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) ";
                }
                else
                    Query = " select * from BacSy where 1=1 ";

                if (!string.IsNullOrEmpty(OBacSyFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OBacSyFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + OBacSyFilter.Keyword.ToUpper() + "%"));
                    Query += " and upper(BacSy.MA) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(BacSy.HOTEN) like " + ActionSqlParam.SpecialChar + "Keyword1 ";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.DONVIMA))
                {
                    ColDbParams.Add(new DbParam("DONVIMA", OBacSyFilter.DONVIMA));
                    Query += " and BacSy.DONVIMA = " + ActionSqlParam.SpecialChar + "DONVIMA";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.KHACDONVIMA))
                {
                    ColDbParams.Add(new DbParam("KHACDONVIMA", OBacSyFilter.KHACDONVIMA));
                    Query += " and BacSy.DONVIMA <> " + ActionSqlParam.SpecialChar + "KHACDONVIMA";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.CHUYENKHOAMA))
                {
                    ColDbParams.Add(new DbParam("CHUYENKHOAMA", OBacSyFilter.CHUYENKHOAMA));
                    Query += " and BacSy.CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA";
                }
                Query += " order by BacSy.HOTEN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BacSyCls[] BacSys = BacSyParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return BacSys;
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
        public override BacSyCls[] PageReading(ActionSqlParamCls ActionSqlParam, BacSyFilterCls OBacSyFilter, ref long recordTotal)
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
                if (OBacSyFilter == null)
                {
                    OBacSyFilter = new BacSyFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";
                if (!string.IsNullOrEmpty(OBacSyFilter.LICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("LICHHOICHANID", OBacSyFilter.LICHHOICHANID));
                    Query = " select BacSy.* from BacSy " +
                        " where BacSy.ID in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) ";
                    recordTotalQuery = " select count(1) from BacSy " +
                        " where BacSy.ID in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) ";
                }
                else if (!string.IsNullOrEmpty(OBacSyFilter.KHACLICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("KHACLICHHOICHANID", OBacSyFilter.KHACLICHHOICHANID));
                    Query = " select BacSy.* from BacSy " +
                        " where BacSy.ID not in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) ";
                    recordTotalQuery = " select count(1) from BacSy " +
                        " where BacSy.ID not in (select lltvhc.BACSYID from LAPLICHTHANHVIENHOICHAN lltvhc where lltvhc.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) ";
                }
                else
                {
                    Query = " select * from BacSy where 1=1 ";
                    recordTotalQuery = " select count(1) from BacSy where 1 = 1 ";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OBacSyFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + OBacSyFilter.Keyword.ToUpper() + "%"));
                    Query += " and upper(BacSy.MA) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(BacSy.HOTEN) like " + ActionSqlParam.SpecialChar + "Keyword1 ";
                    recordTotalQuery += " and upper(BacSy.MA) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(BacSy.HOTEN) like " + ActionSqlParam.SpecialChar + "Keyword1";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.DONVIMA))
                {
                    Query += " and upper (DONVIMA) like upper ('%" + OBacSyFilter.DONVIMA+"%')";
                    recordTotalQuery += " and upper (DONVIMA) like upper ('%" + OBacSyFilter.DONVIMA + "%')";
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.KHACDONVIMA))
                {
                    Query += " and upper (DONVIMA) NOT " + OBacSyFilter.KHACDONVIMA;
                    recordTotalQuery += " and upper (DONVIMA) NOT" + OBacSyFilter.KHACDONVIMA;
                }
                if (!string.IsNullOrEmpty(OBacSyFilter.CHUYENKHOAMA))
                {
                    Query += " and upper (CHUYENKHOAMA) like upper('%" + OBacSyFilter.CHUYENKHOAMA + "%')";
                    recordTotalQuery += " and upper (CHUYENKHOAMA) like upper ('%" + OBacSyFilter.CHUYENKHOAMA  + "%')";
                }
                Query += " ORDER BY BacSy.HOTEN " +
                    "OFFSET " + (OBacSyFilter.PageIndex * OBacSyFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + OBacSyFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BacSyCls[] BacSys = BacSyParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return BacSys;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, BacSyCls OBacSy)
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
                if (string.IsNullOrEmpty(OBacSy.ID))
                {
                    OBacSy.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "BacSy",
                    new DbParam[]{
                    new DbParam("ID",OBacSy.ID),
                    new DbParam("MA",OBacSy.MA),
                    new DbParam("HO",OBacSy.HO),
                    new DbParam("TEN",OBacSy.TEN),
                    new DbParam("HOTEN",OBacSy.HOTEN),
                    new DbParam("TENTHUONGGOI",OBacSy.TENTHUONGGOI),
                    new DbParam("BIDANH",OBacSy.BIDANH),
                    new DbParam("GIOITINH",OBacSy.GIOITINH),
                    new DbParam("NGAYSINH",OBacSy.NGAYSINH),
                    new DbParam("DIENTHOAI",OBacSy.DIENTHOAI),
                    new DbParam("EMAIL",OBacSy.EMAIL),
                    new DbParam("DIACHISONHA",OBacSy.DIACHISONHA),
                    new DbParam("DIACHIHANHCHINHMA",OBacSy.DIACHIHANHCHINHMA),
                    new DbParam("CMND",OBacSy.CMND),
                    new DbParam("NGAYCAPCMND",OBacSy.NGAYCAPCMND),
                    new DbParam("NOICAPCMND",OBacSy.NOICAPCMND),
                    new DbParam("DANTOCMA",OBacSy.DANTOCMA),
                    new DbParam("QUOCTICHMA",OBacSy.QUOCTICHMA),
                    new DbParam("CHUYENMONMA",OBacSy.CHUYENMONMA),
                    new DbParam("CAPBACMA",OBacSy.CAPBACMA),
                    new DbParam("CHUYENNGANHMA",OBacSy.CHUYENNGANHMA),
                    new DbParam("TRINHDOMA",OBacSy.TRINHDOMA),
                    new DbParam("CHUCDANHMA",OBacSy.CHUCDANHMA),
                    new DbParam("CHUYENKHOAMA",OBacSy.CHUYENKHOAMA),
                    new DbParam("DONVIMA",OBacSy.DONVIMA),
                    new DbParam("QUATRINHCONGTAC",OBacSy.QUATRINHCONGTAC),
                    new DbParam("NDANH",OBacSy.NDANH),
                    new DbParam("EXTANH",OBacSy.EXTANH)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BacSyCls OBacSy)
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
                DBService.Update(ActionSqlParam.Trans, "BacSy", "ID", ID,
                    new DbParam[]{
                   new DbParam("MA",OBacSy.MA),
                   new DbParam("HO",OBacSy.HO),
                   new DbParam("TEN",OBacSy.TEN),
                   new DbParam("HOTEN",OBacSy.HOTEN),
                   new DbParam("TENTHUONGGOI",OBacSy.TENTHUONGGOI),
                   new DbParam("BIDANH",OBacSy.BIDANH),
                   new DbParam("GIOITINH",OBacSy.GIOITINH),
                   new DbParam("NGAYSINH",OBacSy.NGAYSINH),
                   new DbParam("DIENTHOAI",OBacSy.DIENTHOAI),
                   new DbParam("EMAIL",OBacSy.EMAIL),
                   new DbParam("DIACHISONHA",OBacSy.DIACHISONHA),
                   new DbParam("DIACHIHANHCHINHMA",OBacSy.DIACHIHANHCHINHMA),
                   new DbParam("CMND",OBacSy.CMND),
                   new DbParam("NGAYCAPCMND",OBacSy.NGAYCAPCMND),
                   new DbParam("NOICAPCMND",OBacSy.NOICAPCMND),
                   new DbParam("DANTOCMA",OBacSy.DANTOCMA),
                   new DbParam("QUOCTICHMA",OBacSy.QUOCTICHMA),
                   new DbParam("CHUYENMONMA",OBacSy.CHUYENMONMA),
                   new DbParam("CAPBACMA",OBacSy.CAPBACMA),
                   new DbParam("CHUYENNGANHMA",OBacSy.CHUYENNGANHMA),
                   new DbParam("TRINHDOMA",OBacSy.TRINHDOMA),
                   new DbParam("CHUCDANHMA",OBacSy.CHUCDANHMA),
                   new DbParam("CHUYENKHOAMA",OBacSy.CHUYENKHOAMA),
                   new DbParam("DONVIMA",OBacSy.DONVIMA),
                   new DbParam("QUATRINHCONGTAC",OBacSy.QUATRINHCONGTAC),
                   new DbParam("NDANH",OBacSy.NDANH),
                   new DbParam("EXTANH",OBacSy.EXTANH)
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
                string DelQuery = " Delete from BacSy where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override BacSyCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from BacSy where (ID=" + ActionSqlParam.SpecialChar + "ID or MA=" + ActionSqlParam.SpecialChar + "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                BacSyCls OBacSy = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OBacSy = BacSyParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OBacSy;
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
                BacSyCls OBacSy = CreateModel(ActionSqlParam, ID);
                OBacSy.ID = NewID;
                Add(ActionSqlParam, OBacSy);

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
