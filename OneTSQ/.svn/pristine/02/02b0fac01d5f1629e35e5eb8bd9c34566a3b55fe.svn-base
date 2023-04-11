using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class LapLichTepDinhKemProcessBll : LapLichTepDinhKemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlLapLichTepDinhKemProcessBll";
            }
        }


        public override LapLichTepDinhKemCls[] Reading(
            RenderInfoCls ORenderInfo,
            LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Reading(ActionSqlParam, OLapLichTepDinhKemFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, LapLichTepDinhKemCls OLapLichTepDinhKem)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Add(ActionSqlParam, OLapLichTepDinhKem);
        }


        public override void Save(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId, LapLichTepDinhKemCls OLapLichTepDinhKem)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Save(ActionSqlParam, LapLichTepDinhKemId, OLapLichTepDinhKem);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Delete(ActionSqlParam, LapLichTepDinhKemId);
        }


        public override LapLichTepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().CreateModel(ActionSqlParam, LapLichTepDinhKemId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLapLichTepDinhKemProcess().Duplicate(ActionSqlParam, LapLichTepDinhKemId);
        }


    }
}
