using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public  class tk_ConfigState
    {
        private string strStateId = "";
        [DataFieldAttribute("StateId", "nvarchar")]
        public string StateId
        {
            get { return strStateId; }
            set { strStateId = value; }
        }

        private string strname = "";
        [DataFieldAttribute("name", "nvarchar")]
        public string name
        {
            get { return strname; }
            set { strname = value; }
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
