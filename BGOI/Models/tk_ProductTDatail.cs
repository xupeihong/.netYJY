using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public class tk_ProductTDatail
    {
        private string strRWID = "";
        [DataFieldAttribute("RWID", "varchar")]

        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
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
        [DataFieldAttribute("SpecsModels", "nvarchar")]

        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
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

        private int strOrderNum ;
        [DataFieldAttribute("OrderNum", "int")]

        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }

        private int strHasthematerialquantity;
        [DataFieldAttribute("Hasthematerialquantity", "int")]

        public int Hasthematerialquantity
        {
            get { return strHasthematerialquantity; }
            set { strHasthematerialquantity = value; }
        }

        private int strnumber;
        [DataFieldAttribute("number", "int")]

        public int number
        {
            get { return strnumber; }
            set { strnumber = value; }
        }

        private string strTechnology = "";
        [DataFieldAttribute("Technology", "varchar")]

        public string Technology
        {
            get { return strTechnology; }
            set { strTechnology = value; }
        }

        private string strphoto = "";
        [DataFieldAttribute("photo", "varchar")]

        public string photo
        {
            get { return strphoto; }
            set { strphoto = value; }
        }

        private string strDeliveryTime = "";
        [DataFieldAttribute("DeliveryTime", "varchar")]

        public string DeliveryTime
        {
            get { return strDeliveryTime; }
            set { strDeliveryTime = value; }
        }

        private string strState = "";
        [DataFieldAttribute("State", "varchar")]

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]

        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strLstate = "";
        [DataFieldAttribute("Lstate", "varchar")]

        public string Lstate
        {
            get { return strLstate; }
            set { strLstate = value; }
        }

        private string strSstate = "";
        [DataFieldAttribute("Sstate", "varchar")]

        public string Sstate
        {
            get { return strSstate; }
            set { strSstate = value; }
        }
    }
}
