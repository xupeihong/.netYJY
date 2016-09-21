using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class RetailSalesOutQuery
    {
        public string strListOutID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListOutID", "varchar")]
        //出库单号
        public string ListOutID
        {
            get { return strListOutID; }
            set { strListOutID = value; }
        }
        //OrderContactor
        public string strOrderContactor = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContactor", "varchar")]
        //购买人
        public string OrderContactor
        {
            get { return strOrderContactor; }
            set { strOrderContactor = value; }
        }
    }
}
