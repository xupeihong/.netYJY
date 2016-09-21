using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class AllocationSheetQuery
    {
        public string strID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ID", "varchar")]
        //调拨编号
        public string ID
        {
            get { return strID; }
            set { strID = value; }
        }
        public string strOrderContent = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
    }
}
