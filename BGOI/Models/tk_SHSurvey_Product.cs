using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHSurvey_Product
    {
        public string strDCID = "";
        [DataFieldAttribute("DCID", "varchar")]
        //回访编号
        public string DCID
        {
            get { return strDCID; }
            set { strDCID = value; }
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
        //关联订购单内物料编码
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public DateTime strOrderDate;
        [DataFieldAttribute("OrderDate", "datetime")]
        //订购时间
        public DateTime OrderDate
        {
            get { return strOrderDate; }
            set { strOrderDate = value; }
        }
        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //内容，产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
       //, , , , Total
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
        public int strNum;
        [DataFieldAttribute("Num", "int")]
        //数量
        public int Num
        {
            get { return strNum; }
            set { strNum = value; }
        }
        public decimal strUnitPrice;
        [DataFieldAttribute("UnitPrice", "decimal")]
        //单价
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        public decimal strTotal;
        [DataFieldAttribute("Total", "decimal")]
        //总价
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        public string  strOrderForm="";
        [DataFieldAttribute("OrderForm", "varchar")]
        //订购方式
        public string OrderForm
        {
            get { return strOrderForm; }
            set { strOrderForm = value; }
        }
    }
}
