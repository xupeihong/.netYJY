using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StockOutDetail
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
        private int strStockOutCount = 0;
        [DataFieldAttribute("StockOutCount", "int")]
        public int StockOutCount
        {
            get { return strStockOutCount; }
            set { strStockOutCount = value; }
        }

        private decimal strUnitPrice;
        [DataFieldAttribute("UnitPrice", "varchar")]
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }

        private decimal strTotalAmount;
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

        private string strListOutID = "";
        [DataFieldAttribute("ListOutID", "varchar")]
        public string ListOutID
        {
            get { return strListOutID; }
            set { strListOutID = value; }
        }

        private string strDOID = "";
        [DataFieldAttribute("DOID", "varchar")]
        public string DOID
        {
            get { return strDOID; }
            set { strDOID = value; }
        }

        private string strOrderID = "";
        [DataFieldAttribute("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
        private int strStockOutCountActual = 0;
        [DataFieldAttribute("StockOutCountActual", "int")]
        public int StockOutCountActual
        {
            get { return strStockOutCountActual; }
            set { strStockOutCountActual = value; }
        }

        private string strBatchID ="";
        [DataFieldAttribute("BatchID", "varchar")]
        public string BatchID
        {
            get { return strBatchID; }
            set { strBatchID = value; }
        }
    }
}
