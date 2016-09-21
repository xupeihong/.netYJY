using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHInstallR_Product
    {
     
        public string strBZID = "";
        [DataFieldAttribute("BZID", "varchar")]
        //报装编号
        public string BZID
        {
            get { return strBZID; }
            set { strBZID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //内容编号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        //物料编码
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //内容
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
      
        public string strSpecsModels = "";
        [DataFieldAttribute("SpecsModels", "varchar")]
        //规格型号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }
        public string strUnit = "";
        [DataFieldAttribute("Unit", "varchar")]
        //单位
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        public int strNum ;
        [DataFieldAttribute("Num", "int")]
        //数量
        public int Num
        {
            get { return strNum; }
            set { strNum = value; }
        }
       
        public string strPrice = "";
        [DataFieldAttribute("Price", "varchar")]
        //价格
        public string Price
        {
            get { return strPrice; }
            set { strPrice = value; }
        }
        public string strState = "";
        [DataFieldAttribute("State", "varchar")]
        //状态
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        public string strUnitPrice = "";
        [DataFieldAttribute("UnitPrice", "varchar")]
        //单价
        public string UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        public string strSalesChannel = "";
        [DataFieldAttribute("SalesChannel", "varchar")]
        //销售渠道
        public string SalesChannel
        {
            get { return strSalesChannel; }
            set { strSalesChannel = value; }
        }
        public string strIsPendingCollection = "";
        [DataFieldAttribute("IsPendingCollection", "varchar")]
        //是否待收款
        public string IsPendingCollection
        {
            get { return strIsPendingCollection; }
            set { strIsPendingCollection = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
