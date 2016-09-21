using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_PurchaseGoods
    {
        private string strCID;
        [DataFieldAttribute("CID", "nvarchar")]
        public string CID
        {
            get { return strCID; }
            set { strCID = value; }
        }
        private string strDID;
        [DataFieldAttribute("DID", "nvarchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        private string strSupplier;
        [DataFieldAttribute("Supplier", "nvarchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }
        private string strINID;
        [DataFieldAttribute("INID", "nvarchar")]
        public string INID
        {
            get { return strINID; }
            set { strINID = value; }
        }
        private string strOrderContent;
        [DataFieldAttribute("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        private string strSpecifications;
        [DataFieldAttribute("Specifications", "nvarchar")]
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }
        private string strUnit;
        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        private int intAmount;
        [DataFieldAttribute("Amount", "nvarchar")]
        public int Amount
        {
            get { return intAmount; }
            set { intAmount = value; }
        }
        private decimal dclUnitpriceNoTax;
        [DataFieldAttribute("UnitpriceNoTax", "nvarchar")]
        public decimal UnitpriceNoTax
        {
            get { return dclUnitpriceNoTax; }
            set { dclUnitpriceNoTax = value; }
        }
        private decimal dclTotalNoTax;
        [DataFieldAttribute("TotalNoTax", "nvarchar")]
        public decimal TotalNoTax
        {
            get { return dclTotalNoTax; }
            set { dclTotalNoTax = value; }
        }
        private decimal dclRate;
        [DataFieldAttribute("Rate", "nvarchar")]
        public decimal Rate
        {
            get { return dclRate; }
            set { dclRate = value; }
        }
        private string strGoodsUse;
          [DataFieldAttribute("GoodsUse", "nvarchar")]
        public string GoodsUse
        {
            get { return strGoodsUse; }
            set { strGoodsUse = value; }
        }


          private string strDrawingNum;
          [DataFieldAttribute("DrawingNum", "nvarchar")]
          public string DrawingNum
          {
              get { return strDrawingNum; }
              set { strDrawingNum = value; }
          }


        private string strRemark;
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private DateTime dtPurchaseDate;
        [DataFieldAttribute("PurchaseDate", "datetime")]
        public DateTime PurchaseDate
        {
            get { return dtPurchaseDate; }
            set { dtPurchaseDate = value; }
        }

        private DateTime dtCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }

        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate;
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
