using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_MaterialFDetail
    {
        private string strLLID = "";
        [DataFieldAttribute("LLID", "varchar")]

        public string LLID
        {
            get { return strLLID; }
            set { strLLID = value; }
        }

        private string strDID = "";
        [DataFieldAttribute("DID", "varchar")]

        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strPID = "";
        [DataFieldAttribute("PID", "varchar")]

        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]

        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        private string strSpecsModels = "";
        [DataFieldAttribute("SpecsModels", "varchar")]

        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        private string strSpecifications = "";
        [DataFieldAttribute("Specifications", "varchar")]

        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }

        private string strManufacturer = "";
        [DataFieldAttribute("Manufacturer", "varchar")]

        public string Manufacturer
        {
            get { return strManufacturer; }
            set { strManufacturer = value; }
        }

        private string strOrderUnit = "";
        [DataFieldAttribute("OrderUnit", "varchar")]

        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }

        private string strTechnology = "";
        [DataFieldAttribute("Technology", "varchar")]

        public string Technology
        {
            get { return strTechnology; }
            set { strTechnology = value; }
        }

        private int strOrderNum;
        [DataFieldAttribute("OrderNum", "int")]

        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }

        private string strBatchID = "";
        [DataFieldAttribute("BatchID", "varchar")]

        public string BatchID
        {
            get { return strBatchID; }
            set { strBatchID = value; }
        }

        private DateTime strDeliveryTime;
        [DataFieldAttribute("DeliveryTime", "datetime")]

        public DateTime DeliveryTime
        {
            get { return strDeliveryTime; }
            set { strDeliveryTime = value; }
        }

        private string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]

        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
         public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private string strIdentitySharing = "";
        [DataFieldAttribute("IdentitySharing", "varchar")]
        public string IdentitySharing
        {
            get { return strIdentitySharing; }
            set { strIdentitySharing = value; }
        }
    }
}
