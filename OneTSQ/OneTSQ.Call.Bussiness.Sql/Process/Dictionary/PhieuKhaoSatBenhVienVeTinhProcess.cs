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
    public class PhieuKhaoSatBenhVienVeTinhProcessBll : PhieuKhaoSatBenhVienVeTinhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuKhaoSatBenhVienVeTinhProcessBll";
            }
        }


        public override PhieuKhaoSatBenhVienVeTinhCls[] Reading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Reading(ActionSqlParam, OPhieuKhaoSatBenhVienVeTinhFilter);
        }
        public override PhieuKhaoSatBenhVienVeTinhCls[] PageReading(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().PageReading(ActionSqlParam, OPhieuKhaoSatBenhVienVeTinhFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Add(ActionSqlParam, OPhieuKhaoSatBenhVienVeTinh);
        }


        public override void Save(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Save(ActionSqlParam, PhieuKhaoSatBenhVienVeTinhId, OPhieuKhaoSatBenhVienVeTinh);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Delete(ActionSqlParam, PhieuKhaoSatBenhVienVeTinhId);
        }


        public override PhieuKhaoSatBenhVienVeTinhCls CreateModel(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ActionSqlParam, PhieuKhaoSatBenhVienVeTinhId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string PhieuKhaoSatBenhVienVeTinhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Duplicate(ActionSqlParam, PhieuKhaoSatBenhVienVeTinhId);
        }
        public override long Count(RenderInfoCls ORenderInfo, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().Count(ActionSqlParam, OPhieuKhaoSatBenhVienVeTinhFilter);
        }


    }
}
