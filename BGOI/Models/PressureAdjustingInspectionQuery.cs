using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PressureAdjustingInspectionQuery
    {
        public string strTYID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TYID", "varchar")]
        //调压记录编号
        public string TYID
        {
            get { return strTYID; }
            set { strTYID = value; }
        }
        public string strInspectionPersonnel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("InspectionPersonnel", "varchar")]
        //巡检人员
        public string InspectionPersonnel
        {
            get { return strInspectionPersonnel; }
            set { strInspectionPersonnel = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strUserName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserName", "varchar")]
        //用户名称
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
    }
}
