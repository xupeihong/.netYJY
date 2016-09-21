using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ConsumptionInfo
    {
        private string strPayId;
        private string strYYCode;
        private string strMCode;
        private double dAmount;
        private DateTime? daCTime;
        private int intType;
        private DateTime? daCreateTime;
        private string strCreateUser;
        private string strValidate;
        /// <summary>
        /// 缴费单号
        /// </summary>
        [DataFieldAttribute("PayId", "varchar")]
        public string PayId
        {
            get { return strPayId; }
            set { strPayId = value; }
        }
        /// <summary>
        /// 预约号
        /// </summary>
        [DataFieldAttribute("YYCode", "varchar")]
        public string YYCode
        {
            get { return strYYCode; }
            set { strYYCode = value; }
        }
        /// <summary>
        /// 委托单号
        /// </summary>
        [DataFieldAttribute("MCode", "varchar")]
        public string MCode
        {
            get { return strMCode; }
            set { strMCode = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        [DataFieldAttribute("Amount", "decimal")]
        public double Amount
        {
            get { return dAmount; }
            set { dAmount = value; }
        }
        /// <summary>
        /// 消费时间
        /// </summary>
        [DataFieldAttribute("CTime", "datetime")]
        public DateTime? CTime
        {
            get { return daCTime; }
            set { daCTime = value; }
        }
        /// <summary>
        /// 消费类型
        /// </summary>
        [DataFieldAttribute("type", "nvarchar")]
        public int Type
        {
            get { return intType; }
            set { intType = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? CreateTime
        {
            get { return daCreateTime; }
            set { daCreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
