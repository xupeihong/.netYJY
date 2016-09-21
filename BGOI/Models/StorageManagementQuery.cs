using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StorageManagementQuery
    {
        public string strListInID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListInID", "varchar")]
        //入库单号
        public string ListInID
        {
            get { return strListInID; }
            set { strListInID = value; }
        }
        public string strBatchID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BatchID", "varchar")]
        //入库批号
        public string BatchID
        {
            get { return strBatchID; }
            set { strBatchID = value; }
        }
    }
}
