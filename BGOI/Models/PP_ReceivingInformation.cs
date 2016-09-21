using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_ReceivingInformation
    {
        private string strSHID;
        [DataFieldAttribute("SHID", "varchar")]
        public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }

        private string strXXID;
        [DataFieldAttribute("XXID", "varchar")]
        public string XXID
        {
            get { return strXXID; }
            set { strXXID = value; }
        }

        private string strDDID;
        [DataFieldAttribute("DDID", "varchar")]
        public string DDID
        {
            get { return strDDID; }
            set { strDDID = value; }
        }



        private string strOrderUnit;
        [DataFieldAttribute("OrderUnit", "varchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }


        private string strArrivalProcess;
        [DataFieldAttribute("ArrivalProcess", "varchar")]
        public string ArrivalProcess
        {
            get { return strArrivalProcess; }
            set { strArrivalProcess = value; }
        }


        private string strArrivalDescription;
        [DataFieldAttribute("ArrivalDescription", "varchar")]
        public string ArrivalDescription
        {
            get { return strArrivalDescription; }
            set { strArrivalDescription = value; }
        }


        private string dtArrivalDate;
        [DataFieldAttribute("ArrivalDate", "datetime")]
        public string ArrivalDate
        {
            get { return dtArrivalDate; }
            set { dtArrivalDate = value; }
        }


        private DateTime dtCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }
        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }


       
    }
}
