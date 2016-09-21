using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_OrderContent
    {
        private string strCID = "";

        [DataFieldAttribute("CID", "nvarchar")]
        public string CID
        {
            get { return strCID; }
            set { strCID = value; }
        }

        private string strDID = "";

        [DataFieldAttribute("DID", "nvarchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strOrderName = "";

        [DataFieldAttribute("OrderName", "nvarchar")]
        public string OrderName
        {
            get { return strOrderName; }
            set { strOrderName = value; }
        }
        private string strSpecifications = "";

        [DataFieldAttribute("Specifications", "nvarchar")]
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }
        private string strSupplier = "";

        [DataFieldAttribute("Supplier", "nvarchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }
        private string strUnit = "";

        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        private int IntAmount = 0;

        [DataFieldAttribute("Amount", "int")]
        public int Amount
        {
            get { return IntAmount; }
            set { IntAmount = value; }
        }
        private decimal DcmUnitPriceNoTax;

        [DataFieldAttribute("UnitPriceNoTax", "decimal")]
        public decimal UnitPriceNoTax
        {
            get { return DcmUnitPriceNoTax; }
            set { DcmUnitPriceNoTax = value; }
        }
        private decimal DcmTotalNoTax;

        [DataFieldAttribute("TotalNoTax", "decimal")]
        public decimal TotalNoTax
        {
            get { return DcmTotalNoTax; }
            set { DcmTotalNoTax = value; }
        }
        private decimal DcmUnitPriceTax;

        [DataFieldAttribute("UnitPriceTax", "decimal")]
        public decimal UnitPriceTax
        {
            get { return DcmUnitPriceTax; }
            set { DcmUnitPriceTax = value; }
        }
        private decimal DcmTotalTax;

        [DataFieldAttribute("TotalTax", "decimal")]
        public decimal TotalTax
        {
            get { return DcmTotalTax; }
            set { DcmTotalTax = value; }
        }
        private float FloatRate;

        [DataFieldAttribute("Rate", "float")]
        public float Rate
        {
            get { return FloatRate; }
            set { FloatRate = value; }
        }
        private DateTime dtPurchaseDate;

        [DataFieldAttribute("PurchaseDate", "datetime")]
        public DateTime PurchaseDate
        {
            get { return dtPurchaseDate; }
            set { dtPurchaseDate = value; }
        }
        private string strRemark = "";

        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
    }
}
