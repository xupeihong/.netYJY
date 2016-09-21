using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductRProduct
    {
        private string strSGID = "";
        [DataFieldAttribute("SGID", "nvarchar")]

        public string SGID
        {
            get { return strSGID; }
            set { strSGID = value; }
        }

        private string strSGDID = "";
        [DataFieldAttribute("SGDID", "nvarchar")]

        public string SGDID
        {
            get { return strSGDID; }
            set { strSGDID = value; }
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

        private int strOrderNum;
        [DataFieldAttribute("OrderNum", "int")]

        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }

    }
}
