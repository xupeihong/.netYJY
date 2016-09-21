using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
    public class ReceivePayment
    {
        private string strRID;
        [Required(ErrorMessage = "编号不能为空")]
        [StringLength(20, ErrorMessage = "编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("RID", "varchar")]
        public string RID
        {
            get { return strRID; }
            set { strRID = value; }
        }
        private string strOrderID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }

        private string strContractID;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ContractID", "varchar")]
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }
        private decimal strAmount;
        [Required(ErrorMessage = "数量不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Amount", "decimal")]
        public decimal Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }
        private string strMothods;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Mothods", "nvarchar")]
        public string Mothods
        {
            get { return strMothods; }
            set { strMothods = value; }
        }
        private string strPayTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PayTime", "datetime")]
        public string PayTime
        {
            get { return strPayTime; }
            set { strPayTime = value; }
        }
        private string strPaymentUnit;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PaymentUnit", "nvarchar")]
        public string PaymentUnit
        {
            get { return strPaymentUnit; }
            set { strPaymentUnit = value; }
        }
        private string strIsInvoice;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IsInvoice", "varchar")]
        public string IsInvoice
        {
            get { return strIsInvoice; }
            set { strIsInvoice = value; }
        }
        private string strInvoiceNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("InvoiceNum", "varchar")]
        public string InvoiceNum
        {
            get { return strInvoiceNum; }
            set { strInvoiceNum = value; }
        }
        private string strRemark;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private string strManager;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Manager", "nvarchar")]
        public string Manager
        {
            get { return strManager; }
            set { strManager = value; }
        }
        private string strChequeID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ChequeID", "varchar")]
        public string ChequeID
        {
            get { return strChequeID; }
            set { strChequeID = value; }
        }

        private string strValidate;
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private string strCreateTime;
        [DataField("CreateTime", "varchar")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

    }
}
