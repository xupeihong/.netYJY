using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_PurchaseOrder
    {
        private string strDDID;
        [DataFieldAttribute("DDID", "varchar")]
        public string DDID
        {
            get { return strDDID; }
            set { strDDID = value; }
        }
        private string strCID;
        [DataFieldAttribute("CID", "varchar")]
        public string CID
        {
            get { return strCID; }
            set { strCID = value; }
        }

        private string strPID;
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
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
        private string strArrivalStatus;
        [DataFieldAttribute("ArrivalStatus", "varchar")]
        public string ArrivalStatus
        {
            get { return strArrivalStatus; }
            set { strArrivalStatus = value; }
        }
        private string strPayStatus;
        [DataFieldAttribute("PayStatus", "varchar")]
        public string PayStatus
        {
            get { return strPayStatus; }
            set { strPayStatus = value; }
        }


        private string strDDState;
        [DataFieldAttribute("DDState", "varchar")]
        public string DDState
        {
            get { return strDDState; }
            set { strDDState = value; }
        }

        private string strState;
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        private string strBusinessTypes;
        [DataFieldAttribute("BusinessTypes", "varchar")]
        public string BusinessTypes
        {
            get { return strBusinessTypes; }
            set { strBusinessTypes = value; }
        }
        private string strPleaseExplain;
        [DataFieldAttribute("PleaseExplain", "varchar")]
        public string PleaseExplain
        {
            get { return strPleaseExplain; }
            set { strPleaseExplain = value; }
        }
        private DateTime dtOrderDate;
        [DataFieldAttribute("OrderDate", "DateTime")]
        public DateTime OrderDate
        {
            get { return dtOrderDate; }
            set { dtOrderDate = value; }
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
        private string strPaymentMethod;
        [DataFieldAttribute("PaymentMethod", "varchar")]
        public string PaymentMethod
        {
            get { return strPaymentMethod; }
            set { strPaymentMethod = value; }
        }

        private string strPaymentAgreement;
        [DataFieldAttribute("PaymentAgreement", "varchar")]
        public string PaymentAgreement
        {
            get { return strPaymentAgreement; }
            set { strPaymentAgreement = value; }
        }

        private string strContractNO;
        [DataFieldAttribute("ContractNO", "varchar")]
        public string ContractNO
        {
            get { return strContractNO; }
            set { strContractNO = value; }
        }

        private string strTheProject;
        [DataFieldAttribute("TheProject", "varchar")]
        public string TheProject
        {
            get { return strTheProject; }
            set { strTheProject = value; }
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
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate;
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

     
    }
}
