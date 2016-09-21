using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public class ProductInstallationQuery
    {
       public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
       //产品编号
       public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strOrderContent = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        public string strInstallName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("InstallName", "varchar")]
        //安装人员
        public string InstallName
        {
            get { return strInstallName; }
            set { strInstallName = value; }
        }
    }
}
