using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_ChoseGoods
    {

        private string strID;
        [DataFieldAttribute("ID", "varchar")]
        public string ID
        {
            get { return strID; }
            set { strID = value; }
        }
        private string strDDID;
        [DataFieldAttribute("DDID", "varchar")]
        public string DDID
        {
            get { return strDDID; }
            set { strDDID = value; }
        }

        private string strPID;
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strName;
        [DataFieldAttribute("Name", "varchar")]
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strSpc;
        [DataFieldAttribute("Spc", "varchar")]
        public string Spc
        {
            get { return strSpc; }
            set { strSpc = value; }
        }
        private string strNum;
        [DataFieldAttribute("Num", "varchar")]
        public string Num
        {
            get { return strNum; }
            set { strNum = value; }
        }

        private string strUnits;
        [DataFieldAttribute("Units", "varchar")]
        public string Units
        {
            get { return strUnits; }
            set { strUnits = value; }
        }

        private string strFKnum;
        [DataFieldAttribute("FKnum", "varchar")]
        public string FKnum
        {
            get { return strFKnum; }
            set { strFKnum = value; }
        }

        private string strSHnum;
        [DataFieldAttribute("SHnum", "varchar")]
        public string SHnum
        {
            get { return strSHnum; }
            set { strSHnum = value; }
        }


        private string strUnitPrice;
        [DataFieldAttribute("UnitPrice", "varchar")]
        public string UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        private string strUnitPrices;
        [DataFieldAttribute("UnitPrices", "varchar")]
        public string UnitPrices
        {
            get { return strUnitPrices; }
            set { strUnitPrices = value; }
        }
        private string strPrice2;
        [DataFieldAttribute("Price2", "varchar")]
        public string Price2
        {
            get { return strPrice2; }
            set { strPrice2 = value; }
        }
        private string strPrice2s;
        [DataFieldAttribute("Price2s", "varchar")]
        public string Price2s
        {
            get { return strPrice2s; }
            set { strPrice2s = value; }
        }

        private string strCreateTime;
        [DataFieldAttribute("CreateTime", "varchar")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }


        private string strValidate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
