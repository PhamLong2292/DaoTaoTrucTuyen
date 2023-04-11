using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Ws
{
    public class DM_NhomKhoaHocProcessBll : DM_NhomKhoaHocTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_NhomKhoaHocProcessBll";
            }
        }


        public override DM_NhomKhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Reading(ActionSqlParam, ONhomKhoaHocFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Count(ActionSqlParam, ONhomKhoaHocFilter);
        }

        public override DM_NhomKhoaHocCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().ReadingWithPaging(ActionSqlParam, ONhomKhoaHocFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_NhomKhoaHocCls ONhomKhoaHoc)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Add(ActionSqlParam, ONhomKhoaHoc);
        }


        public override void Save(RenderInfoCls ORenderInfo, string NhomKhoaHocId, DM_NhomKhoaHocCls ONhomKhoaHoc)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Save(ActionSqlParam, NhomKhoaHocId, ONhomKhoaHoc);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string NhomKhoaHocId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Delete(ActionSqlParam, NhomKhoaHocId);
        }


        public override DM_NhomKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string NhomKhoaHocId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().CreateModel(ActionSqlParam, NhomKhoaHocId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string NhomKhoaHocId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Duplicate(ActionSqlParam, NhomKhoaHocId);
        }

        public override DM_NhomKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().PageReading(ActionSqlParam, ONhomKhoaHocFilter, ref recordTotal);
        }
    }
}
