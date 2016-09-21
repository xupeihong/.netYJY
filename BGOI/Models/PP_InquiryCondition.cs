using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_InquiryCondition
    {

        private string strXJID;
        [DataFieldAttribute("XJID", "string")]
        public string XJID
        {
            get { return strXJID; }
            set { strXJID = value; }
        }
        private string strXID;
        [DataFieldAttribute("XID", "string")]
        public string XID
        {
            get { return strXID; }
            set { strXID = value; }
        }


        private string strSupplier;
        [DataFieldAttribute("Supplier", "string")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }

        private string strContractCondition;
        [DataFieldAttribute("ContractCondition", "string")]
        public string ContractCondition
        {
            get { return strContractCondition; }
            set { strContractCondition = value; }
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
