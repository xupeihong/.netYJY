using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_Payment
    {
        private string strPayId;
        [DataFieldAttribute("PayId", "varchar")]
        public string PayId
        {
            get { return strPayId; }
            set { strPayId = value; }
        }

        private string strDDID;
        [DataFieldAttribute("DDID", "varchar")]
        public string DDID
        {
            get { return strDDID; }
            set { strDDID = value; }
        }


        private string strPaymentMenthod;
        [DataFieldAttribute("PaymentMenthod", "varchar")]
        public string PaymentMenthod
        {
            get { return strPaymentMenthod; }
            set { strPaymentMenthod = value; }
        }


        private string strPaymoney;
        [DataFieldAttribute("Paymoney", "varchar")]
        public string Paymoney
        {
            get { return strPaymoney; }
            set { strPaymoney = value; }
        }


        private string strPayCompany;
        [DataFieldAttribute("PayCompany", "varchar")]
        public string PayCompany
        {
            get { return strPayCompany; }
            set { strPayCompany = value; }
        }


        private string strRemark;
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }


        private DateTime strPayTime;
        [DataFieldAttribute("PayTime", "DateTime")]
        public DateTime PayTime
        {
            get { return strPayTime; }
            set { strPayTime = value; }
        }


        private string strState;
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }


        private string strOrderContacts;
        [DataFieldAttribute("OrderContacts", "varchar")]
        public string OrderContacts
        {
            get { return strOrderContacts; }
            set { strOrderContacts = value; }
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
