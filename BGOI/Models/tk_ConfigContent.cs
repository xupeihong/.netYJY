using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TECOCITY_BGOI
{
    public class tk_ConfigContent
    {
        private int strXID;
        [DataFieldAttribute("XID", "int")]
        public int XID
        {
            get { return strXID; }
            set { strXID = value; }
        }

        private string strSID = "";
        [DataFieldAttribute("SID", "nvarchar")]
        public string SID
        {
            get { return strSID; }
            set { strSID = value; }
        }

        private string strText = "";
        [DataFieldAttribute("Text", "nvarchar")]
        public string Text
        {
            get { return strText; }
            set { strText = value; }
        }

        private string strType = "";
        [DataFieldAttribute("Type", "nvarchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }

        private string strTypeDesc = "";
        [DataFieldAttribute("TypeDesc", "nvarchar")]
        public string TypeDesc
        {
            get { return strTypeDesc; }
            set { strTypeDesc = value; }
        }
    }
}