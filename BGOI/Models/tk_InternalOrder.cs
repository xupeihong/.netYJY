using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_InternalOrder
    {
        private string m_strIOID = "";

        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IOID", "varchar")]
        public string IOID
        {
            get { return m_strIOID; }
            set { m_strIOID = value; }
        }

        private string m_strWarehouse = "";
        [DataField("Warehouse", "nvarchar")]
        public string Warehouse
        {
            get { return m_strWarehouse; }
            set { m_strWarehouse = value; }
        }

        private DateTime m_strOrderDate;
        [DataField("OrderDate", "date")]
        public DateTime OrderDate
        {
            get { return m_strOrderDate; }
            set { m_strOrderDate = value; }
        }

        private decimal strAmount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Amount", "decimal")]
        public decimal Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }

        private string m_strApplyer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Applyer", "nvarchar")]
        public string Applyer
        {
            get { return m_strApplyer; }
            set { m_strApplyer = value; }
        }

        private string m_strApplyTel = "";
        [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的申请人联系电话格式")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyTel", "nvarchar")]
        public string ApplyTel
        {
            get { return m_strApplyTel; }
            set { m_strApplyTel = value; }
        }

        private string m_strGoodsUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("GoodsUser", "nvarchar")]
        public string GoodsUser
        {
            get { return m_strGoodsUser; }
            set { m_strGoodsUser = value; }
        }

        private string m_strUserTel = "";
        [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UserTel", "nvarchar")]
        public string UserTel
        {
            get { return m_strUserTel; }
            set { m_strUserTel = value; }
        }

        private decimal dcGoodsTotal;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("GoodsTotal", "decimal")]
        public decimal GoodsTotal
        {
            get { return dcGoodsTotal; }
            set { dcGoodsTotal = value; }
        }

        private string m_strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Address", "nvarchar")]
        public string Address
        {
            get { return m_strAddress; }
            set { m_strAddress = value; }
        }

        private string m_strSendReason = "";         //新增--赠送原因
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SendReason", "nvarchar")]
        public string SendReason
        {
            get { return m_strSendReason; }
            set { m_strSendReason = value; }
        }
        private string m_strSendDepartment;//新增---160808-赠送部门
        [DataField("SendDepartment", "nvarchar")]
        public string SendDepartment
        {
            get { return m_strSendDepartment; }
            set { m_strSendDepartment = value; }
        }
        private string m_SendRemark;//新增---160808-赠送备注
        [DataField("SendRemark", "nvarchar")]
        public string SendRemark
        {
            get { return m_SendRemark; }
            set { m_SendRemark = value; }
        }

        private string m_strRecipiments = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Recipiments", "nvarchar")]
        public string Recipiments
        {
            get { return m_strRecipiments; }
            set { m_strRecipiments = value; }
        }

        private string m_strExecutives = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Executives", "nvarchar")]
        public string Executives
        {
            get { return m_strExecutives; }
            set { m_strExecutives = value; }
        }

        private string m_strSteering = "";
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Steering", "nvarchar")]
        public string Steering
        {
            get { return m_strSteering; }
            set { m_strSteering = value; }
        }

        private string m_strType = "";
        [DataField("Type", "varchar")]
        public string Type
        {
            get { return m_strType; }
            set { m_strType = value; }
        }

        private string m_strState = "";
        [DataField("State", "varchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "varchar")]
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

        private string m_strUnitID = "";
        [DataField("UnitID", "varchar")]
        public string UnitID
        {
            get { return m_strUnitID; }
            set { m_strUnitID = value; }
        }
    }
}
