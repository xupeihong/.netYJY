using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public class ProduceList
    {
       public string strUnitID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "varchar")]
        //入库单号
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }


        public string strOrderUnit = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderUnit", "varchar")]
        //入库单号
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }

       

        public string strRWID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWID", "varchar")]
        //入库单号
        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
        }


        public string strRKID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RKID", "varchar")]
        //入库单号
        public string RKID
        {
            get { return strRKID; }
            set { strRKID = value; }
        }

        public string strOrderContent = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContent", "varchar")]
        //入库单号
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        public string strSpecsModels = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SpecsModels", "varchar")]
        //入库单号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        public string strName = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Name", "varchar")]
        //入库单号
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
    }
}
