using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class LowAlarmQuery
    {
        public string strNum = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Num", "varchar")]
        //上限数量
        public string Num
        {
            get { return strNum; }
            set { strNum = value; }
        }

        public string strProName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProName", "varchar")]
        //产品名称
        public string ProName
        {
            get { return strProName; }
            set { strProName = value; }
        }
        public string strPIDnew = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PIDnew", "varchar")]
        //产品编号
        public string PIDnew
        {
            get { return strPIDnew; }
            set { strPIDnew = value; }
        }
        public string strSpec = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Spec", "varchar")]
        //规格型号
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
    }
}
