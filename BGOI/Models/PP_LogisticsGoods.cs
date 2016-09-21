using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_LogisticsGoods
    {
       private string dtID;
        [DataFieldAttribute("ID", "string")]
       public string ID
        {
            get { return dtID; }
            set { dtID = value; }
        }
        private string strProName;
        [DataFieldAttribute("ProName", "string")]
        public string ProName
        {
            get { return strProName; }
            set { strProName = value; }
        }
        private string strSpec;
        [DataFieldAttribute("Spec", "string")]
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
        private string strAmount;
        [DataFieldAttribute("Amount", "string")]
        public string Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
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
