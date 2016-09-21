using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_Property
    {
        private string m_strPAID = "";
        [StringLength(20, ErrorMessage = "产品编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PAID", "varchar")]
        public string PAID
        {
            get { return m_strPAID; }
            set { m_strPAID = value; }
        }

        private string m_strUnitID = "";
        [DataField("UnitID", "varchar")]
        public string UnitID
        {
            get { return m_strUnitID; }
            set { m_strUnitID = value; }
        }

        private string m_strApplyer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Applyer", "nvarchar")]
        public string Applyer
        {
            get { return m_strApplyer; }
            set { m_strApplyer = value; }
        }

        private string m_strApplyDate = "";
        [DataField("ApplyDate", "date")]
        public string ApplyDate
        {
            get { return m_strApplyDate; }
            set { m_strApplyDate = value; }
        }

        private string m_strMalls = "";
        [Required(ErrorMessage = "商场名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Malls", "nvarchar")]
        public string Malls
        {
            get { return m_strMalls; }
            set { m_strMalls = value; }
        }

        private string m_strExPlain = "";
        [Required(ErrorMessage = "活动说明不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ExPlain", "nvarchar")]
        public string ExPlain
        {
            get { return m_strExPlain; }
            set { m_strExPlain = value; }
        }

        private decimal dcSampleNum;
        [DataField("SampleNum", "decimal")]
        public decimal SampleNum
        {
            get { return dcSampleNum; }
            set { dcSampleNum = value; }
        }

        private decimal dcSampleAmount;
        [DataField("SampleAmount", "decimal")]
        public decimal SampleAmount
        {
            get { return dcSampleAmount; }
            set { dcSampleAmount = value; }
        }

        private decimal dcRevokeNum;
        [DataField("RevokeNum", "decimal")]
        public decimal RevokeNum
        {
            get { return dcRevokeNum; }
            set { dcRevokeNum = value; }
        }

        private decimal dcRevokeAmount;
        [DataField("RevokeAmount", "decimal")]
        public decimal RevokeAmount
        {
            get { return dcRevokeAmount; }
            set { dcRevokeAmount = value; }
        }

        private string m_strState = "";
        [DataField("State", "nvarchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return m_strCreateUser; }
            set { m_strCreateUser = value; }
        }

        private DateTime strCreateTime;
        [DataField("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string m_strValidate = "";
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return m_strValidate; }
            set { m_strValidate = value; }
        }
    }
}
