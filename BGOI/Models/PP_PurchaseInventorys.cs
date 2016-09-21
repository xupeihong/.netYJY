using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_PurchaseInventorys
    {
        private string strRKID;
        [DataFieldAttribute("RKID", "varchar")]
        public string RKID
        {
            get { return strRKID; }
            set { strRKID = value; }
        }

        private string strSHID;
        [DataFieldAttribute("SHID", "varchar")]
        public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }


        private DateTime dtRkdate;
        [DataFieldAttribute("Rkdate", "DateTime")]
        public DateTime Rkdate
        {
            get { return dtRkdate; }
            set { dtRkdate = value; }
        }


        private string strCKID;
        [DataFieldAttribute("CKID", "varchar")]
        public string CKID
        {
            get { return strCKID; }
            set { strCKID = value; }
        }


        private string strRKInstructions;
        [DataFieldAttribute("RKInstructions", "varchar")]
        public string RKInstructions
        {
            get { return strRKInstructions; }
            set { strRKInstructions = value; }
        }


        private string strHandler;
        [DataFieldAttribute("Handler", "varchar")]
        public string Handler
        {
            get { return strHandler; }
            set { strHandler = value; }
        }


        private string strRKType;
        [DataFieldAttribute("RKType", "varchar")]
        public string RKType
        {
            get { return strRKType; }
            set { strRKType = value; }
        }


        private string strUnitID;
        [DataFieldAttribute("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }


        private string strState;
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
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
