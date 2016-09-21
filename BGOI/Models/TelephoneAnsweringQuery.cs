using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class TelephoneAnsweringQuery
    {
        public string strUserName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserName", "varchar")]
        //联系人
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Address", "varchar")]
        //地址
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //联系电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
    }
}
