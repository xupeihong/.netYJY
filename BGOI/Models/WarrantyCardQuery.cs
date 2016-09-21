using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class WarrantyCardQuery
    {
        public string strContact = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Contact", "varchar")]
        //联系人
        public string Contact
        {
            get { return strContact; }
            set { strContact = value; }
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
        public string strSpecsModels = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SpecsModels", "varchar")]
        //规格型号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        public string strUserAdd = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserAdd", "varchar")]
        //用户地址
        public string UserAdd
        {
            get { return strUserAdd; }
            set { strUserAdd = value; }
        }
    }
}
