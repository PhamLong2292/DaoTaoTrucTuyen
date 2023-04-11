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
    public class PhieuPhanTichNguyenNhanSuCoProcessBll : PhieuPhanTichNguyenNhanSuCoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuPhanTichNguyenNhanSuCoProcessBll";
            }
        }


        public override PhieuPhanTichNguyenNhanSuCoCls[] Reading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Reading(ActionSqlParam, OPhieuPhanTichNguyenNhanSuCoFilter);
        }      
        public override PhieuPhanTichNguyenNhanSuCoCls[] PageReading(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().PageReading(ActionSqlParam, OPhieuPhanTichNguyenNhanSuCoFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Add(ActionSqlParam, OPhieuPhanTichNguyenNhanSuCo);
        }


        public override void Save(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Save(ActionSqlParam, PhieuPhanTichNguyenNhanSuCoId, OPhieuPhanTichNguyenNhanSuCo);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Delete(ActionSqlParam, PhieuPhanTichNguyenNhanSuCoId);
        }


        public override PhieuPhanTichNguyenNhanSuCoCls CreateModel(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().CreateModel(ActionSqlParam, PhieuPhanTichNguyenNhanSuCoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string PhieuPhanTichNguyenNhanSuCoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Duplicate(ActionSqlParam, PhieuPhanTichNguyenNhanSuCoId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuPhanTichNguyenNhanSuCoProcess().Count(ActionSqlParam, OPhieuPhanTichNguyenNhanSuCoFilter);
        }


    }
}
