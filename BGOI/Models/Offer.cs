using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
    public class Offer
    {
        private string strBJID;
        [DataField("BJID", "varchar")]
        public string BJID
        {
            get { return strBJID; }
            set { strBJID = value; }
        }

        private string strPID;
        //[Required(ErrorMessage = "编号不能为空")]
        //[StringLength(20, ErrorMessage = "编号长度不能超过20个字符")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        private string strOfferTitle;
        [DataField("OfferTitle", "nvarchar")]
        public string OfferTitle
        {
            get { return strOfferTitle; }
            set { strOfferTitle = value; }
        }
        private string strOfferUnit;
        [DataField("OfferUnit", "nvarchar")]
        public string OfferUnit
        {
            get { return strOfferUnit; }
            set { strOfferUnit = value; }
        }
        private string strOfferContacts;
        [DataField("OfferContacts", "nvarchar")]
        public string OfferContacts
        {
            get { return strOfferContacts; }
            set { strOfferContacts = value; }
        }
        private string strApprovaler1;
        [DataField("Approvaler1", "nvarchar")]
        public string Approvaler1
        {
            get { return strApprovaler1; }
            set { strApprovaler1 = value; }
        }

        private string strApprovaler2;
        [DataField("Approvaler2", "nvarchar")]
        public string Approvaler2
        {
            get { return strApprovaler2; }
            set { strApprovaler2 = value; }
        }
        private string strDescription;
        [DataField("Description", "varchar")]
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        private string strOfferTime;
        [DataField("OfferTime", "DateTime")]
        public string  OfferTime
        {
            get { return strOfferTime; }
            set { strOfferTime = value; }
        }
        private string strPeople;
        [DataField("People", "varchar")]
        public string People
        {
            get { return strPeople; }
            set { strPeople = value; }
        }

        private string strTel;
        //[RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Tel", "varchar")]
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        private string strCustomer;
        [DataField("Customer", "varchar")]
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }

        private string strCustomerTel;
        [DataField("CustomerTel", "varchar")]
        public string CustomerTel
        {
            get { return strCustomerTel; }
            set { strCustomerTel = value; }
        }

        private string strCustomerPeople;
        [DataField("CustomerPeople", "varchar")]
        public string CustomerPeople
        {
            get { return strCustomerPeople; }
            set { strCustomerPeople = value; }
        }

        private string strDelivery;
        [DataField("Delivery", "varchar")]
        public string Delivery
        {
            get { return strDelivery; }
            set { strDelivery = value; }
        }

        private string strIsInvoice;
        [DataField("IsInvoice", "varchar")]
        public string IsInvoice
        {
            get { return strIsInvoice; }
            set { strIsInvoice = value; }
        }

        private string strPayment;
        [DataField("Payment", "varchar")]
        public string Payment
        {
            get { return strPayment; }
            set { strPayment = value; }
        }

        private string strFKYD;
        [DataField("FKYD", "varchar")]
        public string FKYD
        {
            get { return strFKYD; }
            set { strFKYD = value; }
        }

        private decimal strTotal;
        [DataField("Total", "varchar")]
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        private string strState;
        [DataField("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strOstate;
         [DataField("Ostate", "varchar")]
        public string Ostate
        {
            get { return strOstate; }
            set { strOstate = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private DateTime? strCreateTime;

        [DataField("CreateTime", "datetime")]
        public DateTime? CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strValidate;
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private string strISF;
         [DataField("ISF", "varchar")]
        public string ISF
        {
            get { return strISF; }
            set { strISF = value; }
        }
    }
}
