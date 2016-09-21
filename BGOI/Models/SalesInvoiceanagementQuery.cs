using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class SalesInvoiceanagementQuery
    {
        public string strOrderID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderID", "varchar")]
        //订单编号
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
        public string strProID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProID", "varchar")]
        //物料编号
        public string ProID
        {
            get { return strProID; }
            set { strProID = value; }
        }
        public string strShipGoodeID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ShipGoodeID", "varchar")]
        //发货单号
        public string ShipGoodeID
        {
            get { return strShipGoodeID; }
            set { strShipGoodeID = value; }
        }
        public string strContractID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContractID", "varchar")]
        //合同号
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }
    }
}
