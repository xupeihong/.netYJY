using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_AllocationSheetDetailed
    {
        public string strDBID = "";
        [DataFieldAttribute("DBID", "varchar")]
        //关联调拨单单号
        public string DBID
        {
            get { return strDBID; }
            set { strDBID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //该单内各项内容编号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strProductID = "";
        [DataFieldAttribute("ProductID", "varchar")]
        //物品物料编号
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
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
      
        public string strOrderUnit = "";
        [DataFieldAttribute("OrderUnit", "varchar")]
        //单位
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }
        public int strOrderNum;
        [DataFieldAttribute("OrderNum", "int")]
        //数量
        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }
        public decimal strNoTaxuUnit;
        [DataFieldAttribute("NoTaxuUnit", "decimal")]
        //定价单价（不含税）
        public decimal NoTaxuUnit
        {
            get { return strNoTaxuUnit; }
            set { strNoTaxuUnit = value; }
        }
        public decimal strTaxUnitPrice;
        [DataFieldAttribute("TaxUnitPrice", "decimal")]
        //定价单价（含税）
        public decimal TaxUnitPrice
        {
            get { return strTaxUnitPrice; }
            set { strTaxUnitPrice = value; }
        }
        public decimal strNOPrice;
        [DataFieldAttribute("NOPrice", "decimal")]
        //小计（不含税）
        public decimal NOPrice
        {
            get { return strNOPrice; }
            set { strNOPrice = value; }
        }
        public decimal strPrice;
        [DataFieldAttribute("Price", "decimal")]
        //小计（含税）
        public decimal Price
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
        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
    }
}
