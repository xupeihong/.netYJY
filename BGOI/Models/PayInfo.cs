using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PayInfo
    {
        private string strPayId;
        private int intPaymentMethod;
        private double doPayMoney;
        private string strhkhORzph;
        private string strPayCompany;
        private string strPayType;
        private string strRendingReason;
        private string strRemark;
        private DateTime? daPayTime;
        private DateTime? daCreateTime;
        private string strCreateUser;
        private string strValidate;
        private string strSFKP;
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
        /// 缴费方式
        /// </summary>
        [DataFieldAttribute("PaymentMethod", "int")]
        public int PaymentMethod
        {
            get { return intPaymentMethod; }
            set { intPaymentMethod = value; }
        }
        /// <summary>
        /// 缴费金额
        /// </summary>
        [DataFieldAttribute("PayMoney", "decimal")]
        public double PayMoney
        {
            get { return doPayMoney; }
            set { doPayMoney = value; }
        }
        /// <summary>
        /// 汇款或支票号
        /// </summary>
        [DataFieldAttribute("hkhORzph", "varchar")]
        public string hkhORzph
        {
            get { return strhkhORzph; }
            set { strhkhORzph = value; }
        }
        /// <summary>
        /// 缴费单位
        /// </summary>
        [DataFieldAttribute("PayCompany", "nvarchar")]
        public string PayCompany
        {
            get { return strPayCompany; }
            set { strPayCompany = value; }
        }
        /// <summary>
        /// 费用类别
        /// </summary>
        [DataFieldAttribute("PayType", "nvarchar")]
        public string PayType
        {
            get { return strPayType; }
            set { strPayType = value; }
        }
        /// <summary>
        /// 挂起说明
        /// </summary>
        [DataFieldAttribute("RendingReason", "nvarchar")]
        public string RendingReason
        {
            get { return strRendingReason; }
            set { strRendingReason = value; }
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
        /// <summary>
        /// 缴费时间
        /// </summary>
        [DataFieldAttribute("PayTime", "dateTime")]
        public DateTime? PayTime
        {
            get { return daPayTime; }
            set { daPayTime = value; }
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
        /// <summary>
        /// 创建人
        /// </summary>
        [DataFieldAttribute("CreateUser", "nvarchar")]
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
        /// <summary>
        /// 是否开票
        /// </summary>
        [DataFieldAttribute("SFKP", "nvarch")]
        public string SFKP
        {
            get { return strSFKP; }
            set { strSFKP = value; }
        }
    }
}
