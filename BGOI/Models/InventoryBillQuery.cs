using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class InventoryBillQuery
    {
        public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        public string strProName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProName", "varchar")]
        public string ProName
        {
            get { return strProName; }
            set { strProName = value; }
        }

        public string strListInID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListInID", "varchar")]
        public string ListInID
        {
            get { return strListInID; }
            set { strListInID = value; }
        }
        public string strListOutID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListOutID", "varchar")]
        public string ListOutID
        {
            get { return strListOutID; }
            set { strListOutID = value; }
        }
    }
}
