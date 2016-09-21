using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_PStockingDetail
    {
        private string strRKID = "";
        [DataFieldAttribute("RKID", "nvarchar")]

        public string RKID
        {
            get { return strRKID; }
            set { strRKID = value; }
        }

        private string strDID = "";
        [DataFieldAttribute("DID", "nvarchar")]

        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strPID = "";
        [DataFieldAttribute("PID", "nvarchar")]

        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "nvarchar")]

        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
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

        private int strAmount;
        [DataFieldAttribute("Amount", "int")]

        public int  Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }

        private string strRemark = "";
        [DataFieldAttribute("Remark", "nvarchar")]

        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }


        private string strRWIDID = "";
        [DataFieldAttribute("RWDID", "nvarchar")]

        public string RWIDID
        {
            get { return strRWIDID; }
            set { strRWIDID = value; }
        }
    }
}
