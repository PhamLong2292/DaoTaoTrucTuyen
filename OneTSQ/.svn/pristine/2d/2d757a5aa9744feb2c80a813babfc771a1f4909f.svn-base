using OneTSQ.Bussiness.Template;
using OneTSQ.Core.Model;
using OneTSQ.Database.Service;
using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Bussiness.Sql
{
    public class CommonProcessBll : CommonTemplate
    {
        //Lấy dữ liệu được phân trang
        public override DataTable GetData(ActionSqlParamCls ActionSqlParam, FilterCls filter, string query, Dictionary<string, object> param)
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
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                foreach (var p in param)
                {
                    ColDbParams.Add(new DbParam(p.Key, p.Value));
                }
                if (filter != null)
                {
                    query += 
                        "OFFSET " + (filter.PageIndex * filter.PageSize) + " ROWS " +
                        "FETCH NEXT " + filter.PageSize + " ROWS ONLY ";
                }

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, query, ColDbParams.ToArray());
                DataTable dtResult = dsResult.Tables[0];

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return dtResult;
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
