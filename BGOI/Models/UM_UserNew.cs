using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public   class UM_UserNew
    {
        private string strUserName;
        [DataFieldAttribute("UserName", "nvarchar")]
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
    }
}
