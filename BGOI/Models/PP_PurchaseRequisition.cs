using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_PurchaseRequisition
    {
        /// <summary>
        /// 编号
        /// </summary>
        private string strCID = "";
        [DataFieldAttribute("CID", "nvarchar")]
        public string CID
        {
            get { return strCID; }
            set { strCID = value; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        private string strOrderNumber = "";

        [DataFieldAttribute("OrderNumber", "nvarchar")]
        public string OrderNumber
        {
            get { return strOrderNumber; }
            set { strOrderNumber = value; }
        }
        /// <summary>
        /// 订货单位ID
        /// </summary>
        private string strOrderUnit = "";
        [DataFieldAttribute("OrderUnit", "nvarchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }
        /// <summary>
        /// 申购人
        /// </summary>
        private string strOrderContacts = "";
        [DataFieldAttribute("OrderContacts", "nvarchar")]
        public string OrderContacts
        {
            get { return strOrderContacts; }
            set { strOrderContacts = value; }
        }
        /// <summary>
        /// 审批人1
        /// </summary>
        private string strApprover1 = "";
        [DataFieldAttribute("Approver1", "nvarchar")]
        public string Approver1
        {
            get { return strApprover1; }
            set { strApprover1 = value; }
        }
        /// <summary>
        /// 审批人2
        /// </summary>
        private string strApprover2 = "";

        [DataFieldAttribute("Approver2", "nvarchar")]
        public string Approver2
        {
            get { return strApprover2; }
            set { strApprover2 = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        private string strState = "";

        [DataFieldAttribute("State", "nvarchar")]

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        private string strBusinessTypes = "";
        [DataFieldAttribute("BusinessTypes", "nvarchar")]
        public string BusinessTypes
        {
            get { return strBusinessTypes; }
            set { strBusinessTypes = value; }
        }
        /// <summary>
        /// 请购说明
        /// </summary>
        private string strPleaseExplain = "";
        [DataFieldAttribute("PleaseExplain", "nvarchar")]
        public string PleaseExplain
        {
            get { return strPleaseExplain; }
            set { strPleaseExplain = value; }
        }
        /// <summary>
        /// 请购日期
        /// </summary>
        private string strPleaseDate;
        [DataFieldAttribute("PleaseDate", "datetime")]
        public string PleaseDate
        {
            get { return strPleaseDate; }
            set { strPleaseDate = value; }
        }


        /// <summary>
        /// 期望交货日期
        /// </summary>
        private string strDeliveryDate;
        [DataFieldAttribute("DeliveryDate", "datetime")]
        public string DeliveryDate
        {
            get { return strDeliveryDate; }
            set { strDeliveryDate = value; }
        }
        

        /// <summary>
        /// 预计总金额
        /// </summary>
        private decimal DcmExpectedTotal;
        [DataFieldAttribute("ExpectedTotal", "decimal")]
        public decimal ExpectedTotal
        {
            get { return DcmExpectedTotal; }
            set { DcmExpectedTotal = value; }
        }

        private DateTime dtCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }

        private string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate = "";
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }



    }
}
