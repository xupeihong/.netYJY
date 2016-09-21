using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class OperationAnalysis
    {
        private string strOId;
        private string strYear;
        private string strMonth;
        private string strOverview;
        private string strBusinesAnalysi;
        private string strBusinesContent;
        private string strPayableAnalysi;
        private string strPayableContent;
        private string strExperience;
        private string strOther;
        private string strUnit;
        private string strCreateUser;
        private DateTime? strCreateTime;
        /// <summary>
        /// 经营分析编号
        /// </summary>
        [DataFieldAttribute("OId", "varchar")]
        public string OId
        {
            get { return strOId; }
            set { strOId = value; }
        }
        /// <summary>
        /// 年份
        /// </summary>
        [DataFieldAttribute("Year", "varchar")]
        public string Year
        {
            get { return strYear; }
            set { strYear = value; }
        }
        /// <summary>
        /// 月份
        /// </summary>
        [DataFieldAttribute("Month", "varchar")]
        public string Month
        {
            get { return strMonth; }
            set { strMonth = value; }
        }
        /// <summary>
        /// 概述
        /// </summary>
        [DataFieldAttribute("Overview", "nvarchar")]
        public string Overview
        {
            get { return strOverview; }
            set { strOverview = value; }
        }
        /// <summary>
        /// 业务开展情况分析
        /// </summary>
        [DataFieldAttribute("BusinesAnalysi", "nvarchar")]
        public string BusinesAnalysi
        {
            get { return strBusinesAnalysi; }
            set { strBusinesAnalysi = value; }
        }
        /// <summary>
        /// 开展情况具体内容
        /// </summary>
        [DataFieldAttribute("BusinesContent", "nvarchar")]
        public string BusinesContent
        {
            get { return strBusinesContent; }
            set { strBusinesContent = value; }
        }
        /// <summary>
        /// 应收账款分析
        /// </summary>
        [DataFieldAttribute("PayableAnalysi", "nvarchar")]
        public string PayableAnalysi
        {
            get { return strPayableAnalysi; }
            set { strPayableAnalysi = value; }
        }
        /// <summary>
        /// 账款具体内容
        /// </summary>
        [DataFieldAttribute("PayableContent", "nvarchar")]
        public string PayableContent
        {
            get { return strPayableContent; }
            set { strPayableContent = value; }
        }
        /// <summary>
        /// 管理心得
        /// </summary>
        [DataFieldAttribute("Experience", "nvarchar")]
        public string Experience
        {
            get { return strExperience; }
            set { strExperience = value; }
        }
        /// <summary>
        /// 其他说明
        /// </summary>
        [DataFieldAttribute("Other", "nvarchar")]
        public string Other
        {
            get { return strOther; }
            set { strOther = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }

        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
    }
}
