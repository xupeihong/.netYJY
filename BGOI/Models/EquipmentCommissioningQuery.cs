using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class EquipmentCommissioningQuery
    {
        public string strUserName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserName", "varchar")]
        //用户名称
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //用户电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        //产品编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
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
