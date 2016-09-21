using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_ReturngoodsDetails
    {
        private string strEID;
        [DataField("EID", "varchar")]
        public string EID
        {
            get { return strEID; }
            set { strEID = value; }
        }

        private string strDID;
        [DataField("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strINID;
        [DataField("INID", "varchar")]
        public string INID
        {
            get { return strINID; }
            set { strINID = value; }
        }


        private string strSHID;
        [DataField("SHID", "varchar")]
        public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }
        


        private string strOrderContent;
        [DataField("OrderContent", "varchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        private string strSpecifications;
        [DataField("Specifications", "varchar")]
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }

        private string strSupplier;
        [DataField("Supplier", "varchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }


        private string strUnit;
        [DataField("Unit", "varchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }

        private string strAmount;
        [DataField("Amount", "varchar")]
        public string Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }

        private string strUnitPriceNoTax;
        [DataField("UnitPriceNoTax", "varchar")]
        public string UnitPriceNoTax
        {
            get { return strUnitPriceNoTax; }
            set { strUnitPriceNoTax = value; }
        }


        private string strTotalNoTax;
        [DataField("TotalNoTax", "varchar")]
        public string TotalNoTax
        {
            get { return strTotalNoTax; }
            set { strTotalNoTax = value; }
        }

        private string strUnitPrice;
        [DataField("UnitPrice", "varchar")]
        public string UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }


        private string strTotal;
        [DataField("Total", "varchar")]
        public string Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        private string strBak;
        [DataField("Bak", "varchar")]
        public string Bak
        {
            get { return strBak; }
            set { strBak = value; }
        }

        private string strUse;
        [DataField("GoodsUse", "varchar")]
        public string GoodsUse
        {
            get { return strUse; }
            set { strUse = value; }
        }


        private string strTHAmount;
        [DataField("THAmount", "varchar")]
        public string THAmount
        {
            get { return strTHAmount; }
            set { strTHAmount = value; }
        }


        private string strshuliang;
        [DataField("shuliang", "varchar")]
        public string shuliang
        {
            get { return strshuliang; }
            set { strshuliang = value; }
        }
    }
}
