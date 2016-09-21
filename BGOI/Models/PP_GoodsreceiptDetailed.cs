using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_GoodsreceiptDetailed
    {
        private string strRKID;
        [DataFieldAttribute("RKID", "varchar")]
        public string RKID
        {
            get { return strRKID; }
            set { strRKID = value; }
        }

        private string strDID;
        [DataFieldAttribute("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strINID;
        [DataFieldAttribute("INID", "varchar")]
        public string INID
        {
            get { return strINID; }
            set { strINID = value; }
        }


        private string strSHDID;
        [DataFieldAttribute("SHDID", "varchar")]
        public string SHDID
        {
            get { return strSHDID; }
            set { strSHDID = value; }
        }

        private string strLJCPID;
        [DataFieldAttribute("LJCPID", "varchar")]
        public string LJCPID
        {
            get { return strLJCPID; }
            set { strLJCPID = value; }
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



        private string strBak;
        [DataFieldAttribute("Bak", "varchar")]
        public string Bak
        {
            get { return strBak; }
            set { strBak = value; }
        }


        private string strUnitPriceNoTax;
        [DataFieldAttribute("UnitPriceNoTax", "decimal")]
        public string UnitPriceNoTax
        {
            get { return strUnitPriceNoTax; }
            set { strUnitPriceNoTax = value; }
        }





        private string strTotalNoTax;
        [DataFieldAttribute("TotalNoTax", "decimal")]
        public string TotalNoTax
        {
            get { return strTotalNoTax; }
            set { strTotalNoTax = value; }
        }

        private string strSJAmount;
        [DataFieldAttribute("SJAmount", "varchar")]
        public string SJAmount
        {
            get { return strSJAmount; }
            set { strSJAmount = value; }
        }




        private string strShuLiang;
        [DataFieldAttribute("ShuLiang", "varchar")]
        public string ShuLiang
        {
            get { return strShuLiang; }
            set { strShuLiang = value; }
        }
    }
}
