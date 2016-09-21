using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StockInDetail
    {
        private string strProductID = "";
        [DataFieldAttribute("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }

        private string strProName = "";
        [DataFieldAttribute("ProName", "varchar")]
        public string ProName
        {
            get { return strProName; } 
            set { strProName = value; }
        }
        private string strSpec = "";
        [DataFieldAttribute("Spec", "varchar")]
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
        private string strUnits = "";
        [DataFieldAttribute("Units", "varchar")]
        public string Units
        {
            get { return strUnits; }
            set { strUnits = value; }
        }
        private int strStockInCount = 0;
        [DataFieldAttribute("StockInCount", "int")]
        public int StockInCount
        {
            get { return strStockInCount; }
            set { strStockInCount = value; }
        }

        private decimal strUnitPrice;
        [DataFieldAttribute("UnitPrice", "varchar")]
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }

        private decimal strTotalAmount ;
        [DataFieldAttribute("TotalAmount", "varchar")]
        public decimal TotalAmount
        {
            get { return strTotalAmount; }
            set { strTotalAmount = value; }
        }

        private string strManufacturer = "";
        [DataFieldAttribute("Manufacturer", "varchar")]
        public string Manufacturer
        {
            get { return strManufacturer; }
            set { strManufacturer = value; }
        }

        private string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strMainContent = "";
        [DataFieldAttribute("MainContent", "varchar")]
        public string MainContent
        {
            get { return strMainContent; }
            set { strMainContent = value; }
        }

        private string strListInID = "";
        [DataFieldAttribute("ListInID", "varchar")]
        public string ListInID
        {
            get { return strListInID; }
            set { strListInID = value; }
        }

        private string strDIID = "";
        [DataFieldAttribute("DIID", "varchar")]
        public string DIID
        {
            get { return strDIID; }
            set { strDIID = value; }
        }

        private string strOrderID = "";
        [DataFieldAttribute("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }


        private string strBatchNumber = "";
        [DataFieldAttribute("BatchNumber", "varchar")]
        public string BatchNumber
        {
            get { return strBatchNumber; }
            set { strBatchNumber = value; }
        }
        private int strNumupdate = 0;
        [DataFieldAttribute("Numupdate", "int")]
        public int Numupdate
        {
            get { return strNumupdate; }
            set { strNumupdate = value; }
        }


        private string strUpState = "";
        [DataFieldAttribute("UpState", "varchar")]
        public string UpState
        {
            get { return strUpState; }
            set { strUpState = value; }
        }
    }
}
