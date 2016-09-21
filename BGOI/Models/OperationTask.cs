using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class OperationTask
    {
        private string strOId;
        private string strYearTarget;
        private string strMonthTarget;
        private string strMonthComplete;
        private string strProblem;
        private string strNextMonthTarget;
        private string strType;
        private string strRemark;
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
        /// 年度目标
        /// </summary>
        [DataFieldAttribute("YearTarget", "nvarchar")]
        public string YearTarget
        {
            get { return strYearTarget; }
            set { strYearTarget = value; }
        }
        /// <summary>
        /// 月度分解
        /// </summary>
        [DataFieldAttribute("MonthTarget", "nvarchar")]
        public string MonthTarget
        {
            get { return strMonthTarget; }
            set { strMonthTarget = value; }
        }
        /// <summary>
        /// 本月完成
        /// </summary>
        [DataFieldAttribute("MonthComplete", "nvarchar")]
        public string MonthComplete
        {
            get { return strMonthComplete; }
            set { strMonthComplete = value; }
        }
        /// <summary>
        /// 存在问题
        /// </summary>
        [DataFieldAttribute("Problem", "nvarchar")]
        public string Problem
        {
            get { return strProblem; }
            set { strProblem = value; }
        }
        /// <summary>
        /// 下月目标
        /// </summary>
        [DataFieldAttribute("NextMonthTarget", "nvarchar")]
        public string NextMonthTarget
        {
            get { return strNextMonthTarget; }
            set { strNextMonthTarget = value; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        [DataFieldAttribute("Type", "nvarchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
    }
}
