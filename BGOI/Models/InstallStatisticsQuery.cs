using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class InstallStatisticsQuery
    {
        public string strCustomerName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerName", "varchar")]
        //用户姓名
        public string CustomerName
        {
            get { return strCustomerName; }
            set { strCustomerName = value; }
        }

        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
    }
}
