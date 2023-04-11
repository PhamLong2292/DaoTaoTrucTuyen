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
    public class DanhGiaDeCuong_DeTaiProcessBll : DanhGiaDeCuong_DeTaiTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDanhGiaDeCuong_DeTaiProcessBll";
            }
        }


        public override DanhGiaDeCuong_DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Reading(ActionSqlParam, ODanhGiaDeCuong_DeTaiFilter);
        }      
        public override DanhGiaDeCuong_DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().PageReading(ActionSqlParam, ODanhGiaDeCuong_DeTaiFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Add(ActionSqlParam, ODanhGiaDeCuong_DeTai);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Save(ActionSqlParam, DanhGiaDeCuong_DeTaiId, ODanhGiaDeCuong_DeTai);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Delete(ActionSqlParam, DanhGiaDeCuong_DeTaiId);
        }


        public override DanhGiaDeCuong_DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().CreateModel(ActionSqlParam, DanhGiaDeCuong_DeTaiId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DanhGiaDeCuong_DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Duplicate(ActionSqlParam, DanhGiaDeCuong_DeTaiId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDanhGiaDeCuong_DeTaiProcess().Count(ActionSqlParam, ODanhGiaDeCuong_DeTaiFilter);
        }


    }
}
