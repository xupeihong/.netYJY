using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_InquiryGoods
    {
        private string strXJID;
        [DataFieldAttribute("XJID", "varchar")]
        public string XJID
        {
            get { return strXJID; }
            set { strXJID = value; }
        }
        private string strDrawingNum;
        [DataFieldAttribute("DrawingNum", "nvarchar")]
        public string DrawingNum
        {
            get { return strDrawingNum; }
            set { strDrawingNum = value; }
        }
        private string strXID;
        [DataFieldAttribute("XID", "varchar")]
        public string XID
        {
            get { return strXID; }
            set { strXID = value; }
        }


        private string strGoodsUse;
        [DataFieldAttribute("GoodsUse", "nvarchar")]
        public string GoodsUse
        {
            get { return strGoodsUse; }
            set { strGoodsUse = value; }
        }


        private string strXXID;
        [DataFieldAttribute("XXID", "varchar")]
        public string XXID
        {
            get { return strXXID; }
            set { strXXID = value; }
        }

        private string strOrderContent;
        [DataFieldAttribute("OrderContent", "varchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        private string strSpecifications;
        [DataFieldAttribute("Specifications", "varchar")]
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }

        private string strSupplier;
        [DataFieldAttribute("Supplier", "varchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }

        private string strUnit;
        [DataFieldAttribute("Unit", "varchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }


        private int intAmount;
        [DataFieldAttribute("Amount", "int")]
        public int Amount
        {
            get { return intAmount; }
            set { intAmount = value; }
        }
        private decimal dclNegotiatedPricingNoTax;
         [DataFieldAttribute("NegotiatedPricingNoTax", "decimal")]
        public decimal NegotiatedPricingNoTax
        {
            get { return dclNegotiatedPricingNoTax; }
            set { dclNegotiatedPricingNoTax = value; }
        }

         private decimal dclTotalNegotiationNoTax;
         [DataFieldAttribute("TotalNegotiationNoTax", "decimal")]
         public decimal TotalNegotiationNoTax
         {
             get { return dclTotalNegotiationNoTax; }
             set { dclTotalNegotiationNoTax = value; }
         }

         private decimal dclUnitPriceTax;
         [DataFieldAttribute("UnitPriceTax", "decimal")]
         public decimal UnitPriceTax
         {
             get { return dclUnitPriceTax; }
             set { dclUnitPriceTax = value; }
         }

         private decimal dclTotalTax;
         [DataFieldAttribute("TotalTax", "decimal")]
         public decimal TotalTax
         {
             get { return dclTotalTax; }
             set { dclTotalTax = value; }
         }
         private string strRate;
         [DataFieldAttribute("Rate", "varchar")]
         public string Rate
         {
             get { return strRate; }
             set { strRate = value; }
         }

         private string strRemark;
         [DataFieldAttribute("Remark", "varchar")]
         public string Remark
         {
             get { return strRemark; }
             set { strRemark = value; }
         }

         //private DateTime dtPurchaseDate;
         //[DataFieldAttribute("PurchaseDate", "datetime")]
         //public DateTime PurchaseDate
         //{
         //    get { return dtPurchaseDate; }
         //    set { dtPurchaseDate = value; }
         //}

         private DateTime dtCreateTime;
         [DataFieldAttribute("CreateTime", "datetime")]
         public DateTime CreateTime
         {
             get { return dtCreateTime; }
             set { dtCreateTime = value; }
         }
         private string strCreateUser;
         [DataFieldAttribute("CreateUser", "varchar")]
         public string CreateUser
         {
             get { return strCreateUser; }
             set { strCreateUser = value; }
         }
         private string strValidate;
         [DataFieldAttribute("Validate", "datetime")]
         public string Validate
         {
             get { return strValidate; }
             set { strValidate = value; }
         }
    }
}
