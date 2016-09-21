using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_Inquirys
    {
     
        private string strXJID;
        [DataFieldAttribute("XJID", "varchar")]
        public string XJID
        {
            get { return strXJID; }
            set { strXJID = value; }
        }
        private string strCID;
        [DataFieldAttribute("CID", "varchar")]
        public string CID
        {
            get { return strCID; }
            set { strCID = value; }
        }
        private string strOrderNumber;
        [DataFieldAttribute("OrderNumber", "varchar")]
        public string OrderNumber
        {
            get { return strOrderNumber; }
            set { strOrderNumber = value; }
        }
        private string strOrderUnit;
        [DataFieldAttribute("OrderUnit", "varchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }
        private string strInquiryTitle;
        [DataFieldAttribute("InquiryTitle", "varchar")]
        public string InquiryTitle
        {
            get { return strInquiryTitle; }
            set { strInquiryTitle = value; }
        }
        private string strOrderContacts;
        [DataFieldAttribute("OrderContacts", "varchar")]
        public string OrderContacts
        {
            get { return strOrderContacts; }
            set { strOrderContacts = value; }
        }
        private string strApprover1;
        [DataFieldAttribute("Approver1", "varchar")]
        public string Approver1
        {
            get { return strApprover1; }
            set { strApprover1 = value; }
        }
        private string strApprover2;
        [DataFieldAttribute("Approver2", "varchar")]
        public string Approver2
        {
            get { return strApprover2; }
            set { strApprover2 = value; }
        }
        /// <summary>
        /// 状态1.新建2.已订购
        /// </summary>
        /// 

        private string strXJState;
        [DataFieldAttribute("XJState", "varchar")]
        public string XJState
        {
            get { return strXJState; }
            set { strXJState = value; }
        }
        private string strState;
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        /// <summary>
        /// 业务类型 1.零售2.零售配件3.项目4.项目配件5.其他
        /// </summary>
        private string strBusinessTypes;
        [DataFieldAttribute("BusinessTypes", "varchar")]
        public string BusinessTypes
        {
            get { return strBusinessTypes; }
            set { strBusinessTypes = value; }
        }
        private string strInquiryExplain;
        [DataFieldAttribute("InquiryExplain", "varchar")]
        public string InquiryExplain
        {
            get { return strInquiryExplain; }
            set { strInquiryExplain = value; }
        }
        private DateTime dtInquiryDate;
        [DataFieldAttribute("InquiryDate", "datetime")]
        public DateTime InquiryDate
        {
            get { return dtInquiryDate; }
            set { dtInquiryDate = value; }
        }
        private string strDeliveryLimit;
        [DataFieldAttribute("DeliveryLimit", "varchar")]
        public string DeliveryLimit
        {
            get { return strDeliveryLimit; }
            set { strDeliveryLimit = value; }
        }
        private string strDeliveryMethod;
        [DataFieldAttribute("DeliveryMethod", "varchar")]
        public string DeliveryMethod
        {
            get { return strDeliveryMethod; }
            set { strDeliveryMethod = value; }
        }
        private string strIsInvoice;
        [DataFieldAttribute("IsInvoice", "varchar")]
        public string IsInvoice
        {
            get { return strIsInvoice; }
            set { strIsInvoice = value; }
        }
        /// <summary>
        /// 支付方式1.现金2.银行转账3.在线支付
        /// </summary>
        private string strPaymentMethod;
        [DataFieldAttribute("PaymentMethod", "varchar")]
        public string PaymentMethod
        {
            get { return strPaymentMethod; }
            set { strPaymentMethod = value; }
        }
        /// <summary>
        /// 付款约定 1.货到付款2.贷款预付3.分期付款
        /// </summary>
        private string strPaymentAgreement;
        [DataFieldAttribute("PaymentAgreement", "varchar")]
        public string PaymentAgreement
        {
            get { return strPaymentAgreement; }
            set { strPaymentAgreement = value; }
        }
        private decimal dcmTotalTax;
        [DataFieldAttribute("TotalTax", "decimal")]
        public decimal TotalTax
        {
            get { return dcmTotalTax; }
            set { dcmTotalTax = value; }
        }
        private decimal dcmTotalNoTax;
       [DataFieldAttribute("TotalNoTax", "decimal")]
        public decimal TotalNoTax
        {
            get { return dcmTotalNoTax; }
            set { dcmTotalNoTax = value; }
        }
       private DateTime dtCreateTime;
          [DataFieldAttribute("CreateTime", "datetime")]
       public DateTime CreateTime
       {
           get { return dtCreateTime; }
           set { dtCreateTime = value; }
       }
          private string strCreateUser;
        [DataFieldAttribute("CreateUser", "string")]
          public string CreateUser
          {
              get { return strCreateUser; }
              set { strCreateUser = value; }
          }
        private string strValidate;
        [DataFieldAttribute("Validate", "string")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }



     
    }
}
