using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TECOCITY_BGOI
{
    public class UserVisitQuery
    {
        public string strHFID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HFID", "varchar")]
        //回访编号
        public string HFID
        {
            get { return strHFID; }
            set { strHFID = value; }
        }
        public string strReturnVisit = "";
        // [StringLength(5, ErrorMessage = "回访人不能超过5个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReturnVisit", "varchar")]
        //回访人
        public string ReturnVisit
        {
            get { return strReturnVisit; }
            set { strReturnVisit = value; }
        }

        public string strTel = "";
        // [StringLength(5, ErrorMessage = "回访人不能超过5个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
    }
}
