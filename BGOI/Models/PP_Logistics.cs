using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
  public   class PP_Logistics
    {
      private string strID;
        [DataFieldAttribute("ID", "string")]
      public string ID
        {
            get { return strID; }
            set { strID = value; }
        }
        private string strSQCompany;
        [DataFieldAttribute("SQCompany", "string")]
        public string SQCompany
        {
            get { return strSQCompany; }
            set { strSQCompany = value; }
        }


        private string strTHCompany;
        [DataFieldAttribute("THCompany", "string")]
        public string THCompany
        {
            get { return strTHCompany; }
            set { strTHCompany = value; }
        }

        private string strSHaddress;
        [DataFieldAttribute("SHaddress", "string")]
        public string SHaddress
        {
            get { return strSHaddress; }
            set { strSHaddress = value; }
        }


        private string dtSHContacts;
        [DataFieldAttribute("SHContacts", "string")]
        public string SHContacts
        {
            get { return dtSHContacts; }
            set { dtSHContacts = value; }
        }
        private string strSHTel;
        [DataFieldAttribute("SHTel", "string")]
        public string SHTel
        {
            get { return strSHTel; }
            set { strSHTel = value; }
        }
        private string strFHConsignor;
        [DataFieldAttribute("FHConsignor", "string")]
        public string FHConsignor
        {
            get { return strFHConsignor; }
            set { strFHConsignor = value; }
        }


        private string strFHTel;
        [DataFieldAttribute("FHTel", "string")]
        public string FHTel
        {
            get { return strFHTel; }
            set { strFHTel = value; }
        }
        private string strFHFax;
        [DataFieldAttribute("FHFax", "string")]
        public string FHFax
        {
            get { return strFHFax; }
            set { strFHFax = value; }
        }


        private string strLogisticsS;
        [DataFieldAttribute("LogisticsS", "string")]
        public string LogisticsS
        {
            get { return strLogisticsS; }
            set { strLogisticsS = value; }
        }

        private string strLogisticsSTel;
        [DataFieldAttribute("LogisticsSTel", "string")]
        public string LogisticsSTel
        {
            get { return strLogisticsSTel; }
            set { strLogisticsSTel = value; }
        }

        private string strLogisticsSFax;
        [DataFieldAttribute("LogisticsSFax", "string")]
        public string LogisticsSFax
        {
            get { return strLogisticsSFax; }
            set { strLogisticsSFax = value; }
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
