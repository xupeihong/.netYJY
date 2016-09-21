using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class MaintenanceTaskQuery
    {
       public string strDeviceID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeviceID", "varchar")]
       //设备编号
       public string DeviceID
        {
            get { return strDeviceID; }
            set { strDeviceID = value; }
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
       
    }
}
