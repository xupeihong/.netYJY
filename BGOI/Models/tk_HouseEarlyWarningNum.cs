using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_HouseEarlyWarningNum
    {
       public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        //货品编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strNum = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Num", "varchar")]
        //上限数量
        public string Num
        {
            get { return strNum; }
            set { strNum = value; }
        }

        public string strRemarks = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remarks", "varchar")]
        //状态说明
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
    }
}
