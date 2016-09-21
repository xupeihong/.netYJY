using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ScrapManagementQuery
    {
        public string strListScrapID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListScrapID", "varchar")]
        //报废编号
        public string ListScrapID
        {
            get { return strListScrapID; }
            set { strListScrapID = value; }
        }

        public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        //货物编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strHandlers = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Handlers", "varchar")]
        //经手人
        public string Handlers
        {
            get { return strHandlers; }
            set { strHandlers = value; }
        }
    }
}
