using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_StorageDetailed
    {
        private string strSHID;
        [DataFieldAttribute("SHID", "varchar")]
        public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }

        private string strSHState;
        [DataFieldAttribute("SHState", "varchar")]
        public string SHState
        {
            get { return strSHState; }
            set { strSHState = value; }
        }


        private string strDID;
        [DataFieldAttribute("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strLJCPID;
        [DataFieldAttribute("LJCPID", "varchar")]
        public string LJCPID
        {
            get { return strLJCPID; }
            set { strLJCPID = value; }
        }


        private string strINID;
        [DataFieldAttribute("INID", "varchar")]
        public string INID
        {
            get { return strINID; }
            set { strINID = value; }
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

        private int intActualAmount;
        [DataFieldAttribute("ActualAmount", "int")]
        public int ActualAmount
        {
            get { return intActualAmount; }
            set { intActualAmount = value; }
        }

        private string strBak;
        [DataFieldAttribute("Bak", "varchar")]
        public string Bak
        {
            get { return strBak; }
            set { strBak = value; }
        }

        private DateTime dtCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }
        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "string")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataFieldAttribute("Validate", "string")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }



        private decimal dclUnitPriceNoTax;
        [DataFieldAttribute("UnitPriceNoTax", "decimal")]
        public decimal UnitPriceNoTax
        {
            get { return dclUnitPriceNoTax; }
            set { dclUnitPriceNoTax = value; }
        }
        private decimal dclTotalNoTax;
        [DataFieldAttribute("TotalNoTax", "decimal")]
        public decimal TotalNoTax
        {
            get { return dclTotalNoTax; }
            set { dclTotalNoTax = value; }
        }

        private string strShuLiang;
        [DataFieldAttribute("ShuLiang", "string")]
        public string ShuLiang
        {
            get { return strShuLiang; }
            set { strShuLiang = value; }
        }

        private string strTHAmount;
        [DataFieldAttribute("THAmount", "string")]
        public string THAmount
        {
            get { return strTHAmount; }
            set { strTHAmount = value; }
        }
    }
}
