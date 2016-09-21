using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
 public   class Orders_DetailInfoNew
    { 
        #region old
        private string strPID;
        [DataField("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strOrderID;
        [DataField("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
        private string strDID;
        [DataField("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        private string strProductID;
        [DataField("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        private string strOrderContent;
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        private string strSpecsModels;
        [DataField("SpecsModels", "nvarchar")]
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }
        private string strManufacturer;
        [DataField("Manufacturer", "nvarchar")]
        public string Manufacturer
        {
            get { return strManufacturer; }
            set { strManufacturer = value; }
        }
        private string strOrderUnit;
        [DataField("OrderUnit", "nvarchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }
        private int strOrderNum;
        [DataField("OrderNum", "int")]
        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }

        private decimal strTaxUnitPrice;
        [DataField("TaxUnitPrice", "decimal")]
        public decimal TaxUnitPrice
        {
            get { return strTaxUnitPrice; }
            set { strTaxUnitPrice = value; }
        }

        private decimal strUnitPrice;
        [DataField("UnitPrice", "decimal")]
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }

        private decimal strDUnitPrice;
        [DataField("DUnitPrice", "decimal")]
        public decimal DUnitPrice
        {
            get { return strDUnitPrice; }
            set { strDUnitPrice = value; }
        }

        private decimal strDTotalPrice;
        [DataField("DTotalPrice", "decimal")]
        public decimal DTotalPrice
        {
            get { return strDTotalPrice; }
            set { strDTotalPrice = value; }
        }

        private decimal strPrice;
        [DataField("Price", "decimal")]
        public decimal Price
        {
            get { return strPrice; }
            set { strPrice = value; }
        }
        private decimal strSubtotal;
        [DataField("Subtotal", "decimal")]
        public decimal Subtotal
        {
            get { return strSubtotal; }
            set { strSubtotal = value; }
        }
        private string strTechnology;
        [DataField("Technology", "nvarchar")]
        public string Technology
        {
            get { return strTechnology; }
            set { strTechnology = value; }
        }
        private DateTime strDeliveryTime;
        [DataField("DeliveryTime", "datetime")]
        public DateTime DeliveryTime
        {
            get { return strDeliveryTime; }
            set { strDeliveryTime = value; }
        }
        private string strState;
        [DataField("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strRemark = "";
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private string strTaxRate = "";
        [DataField("TaxRate", "nvarchar")]
      public string TaxRate
        {
            get { return strTaxRate; }
            set { strTaxRate = value; }
        }
     
        private string strCreateTime;
        [DataField("CreateTime", "datetime")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

      
        #endregion
     
    }
}
