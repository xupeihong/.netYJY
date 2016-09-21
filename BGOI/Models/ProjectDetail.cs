using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class ProjectDetail
    {
       
        private string strPID;
        [DataFieldAttribute("PID", "nvarchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        private string strXID;
        [DataFieldAttribute("XID", "nvarchar")]
        public string XID
        {
            get { return strXID; }
            set { strXID = value; }
        }
       
        private string strProductID;
        [DataFieldAttribute("ProductID", "nvarchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        
        private string strOrderContnet;
       [DataFieldAttribute("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return strOrderContnet; }
            set { strOrderContnet = value; }
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
       
        private int strAmount;
        [DataFieldAttribute("Amount", "int")]
        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }
       
        private DateTime strPurchaseDaet;
        [DataFieldAttribute("PurchaseDate", "DateTime")]
        public DateTime PurchaseDate
        {
            get { return strPurchaseDaet; }
            set { strPurchaseDaet = value; }
        }
        
        private string strRemark;
       [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
    }
}
