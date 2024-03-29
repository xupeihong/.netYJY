﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SHSurvey
    {
        public string strDCID = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DCID", "varchar")]
        //回访编号
        public string DCID
        {
            get { return strDCID; }
            set { strDCID = value; }
        }
        public string strUntiID = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UntiID", "varchar")]
        //调查单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public DateTime strSurveyDate;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SurveyDate", "datetime")]
        //调查日期
        public DateTime SurveyDate
        {
            get { return strSurveyDate; }
            set { strSurveyDate = value; }
        }

        public string strCustomerID = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerID", "varchar")]
        //客户编号
        public string CustomerID
        {
            get { return strCustomerID; }
            set { strCustomerID = value; }
        }
        public string strCustomer = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Customer", "nvarchar")]
        //客户名称
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }
        public string strProductQuality = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductQuality", "nvarchar")]
        //产品质量调查结果
        public string ProductQuality
        {
            get { return strProductQuality; }
            set { strProductQuality = value; }
        }
        public string strProductQrice = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductQrice", "nvarchar")]
        //产品价格调查结果
        public string ProductQrice
        {
            get { return strProductQrice; }
            set { strProductQrice = value; }
        }

        public string strProductDelivery = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductDelivery", "nvarchar")]
        //产品交货期调查结果
        public string ProductDelivery
        {
            get { return strProductDelivery; }
            set { strProductDelivery = value; }
        }
        public string strProductSurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductSurvey", "nvarchar")]
        //产品调查说明原因
        public string ProductSurvey
        {
            get { return strProductSurvey; }
            set { strProductSurvey = value; }
        }
        public string strCustomerServiceSurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerServiceSurvey", "nvarchar")]
        //服务售后维修，保养服务调查结果
        public string CustomerServiceSurvey
        {
            get { return strCustomerServiceSurvey; }
            set { strCustomerServiceSurvey = value; }
        }
        public string strSupplySurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SupplySurvey", "nvarchar")]
        //服务备品，备件供应调查结果
        public string SupplySurvey
        {
            get { return strSupplySurvey; }
            set { strSupplySurvey = value; }
        }
        public string strLeakSurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("LeakSurvey", "nvarchar")]
        //有无漏气现象调查结果
        public string LeakSurvey
        {
            get { return strLeakSurvey; }
            set { strLeakSurvey = value; }
        }

        public string strServiceSurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ServiceSurvey", "nvarchar")]
        //服务调查说明原因
        public string ServiceSurvey
        {
            get { return strServiceSurvey; }
            set { strServiceSurvey = value; }
        }
        public string strAgencySales = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AgencySales", "nvarchar")]
        //代理售后维修，保养服务调查结果
        public string AgencySales
        {
            get { return strAgencySales; }
            set { strAgencySales = value; }
        }
        public string strAgencyConsultation = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AgencyConsultation", "nvarchar")]
        //代理咨询，维护培训调查结果
        public string AgencyConsultation
        {
            get { return strAgencyConsultation; }
            set { strAgencyConsultation = value; }
        }

        public string strAgencySpareParts = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AgencySpareParts", "nvarchar")]
        //代理备品，备件供应调查结果
        public string AgencySpareParts
        {
            get { return strAgencySpareParts; }
            set { strAgencySpareParts = value; }
        }
        public string strAgencySurvey = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AgencySurvey", "nvarchar")]
        //代理调查说明原因
        public string AgencySurvey
        {
            get { return strAgencySurvey; }
            set { strAgencySurvey = value; }
        }
        public string strRemark = "";

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strUserSign = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserSign", "nvarchar")]
        //用户签字
        public string UserSign
        {
            get { return strUserSign; }
            set { strUserSign = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "nvarchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strOther = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Other", "nvarchar")]
        public string Other
        {
            get { return strOther; }
            set { strOther = value; }
        }
        public string strHFIDID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute(" HFIDID", "varchar")]
        //用户回访编号
        public string HFIDID
        {
            get { return strHFIDID; }
            set { strHFIDID = value; }
        }
        public string strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute(" Address", "varchar")]
        //地址
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute(" Tel", "varchar")]
        //电话/传真
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strUserPerson = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute(" UserPerson", "varchar")]
        //联系人
        public string UserPerson
        {
            get { return strUserPerson; }
            set { strUserPerson = value; }
        }
    }
}
