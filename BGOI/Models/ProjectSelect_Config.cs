using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ProjectSelect_Config
    {
        private string sID = "";

        public string ID
        {
            get { return sID; }
            set { sID = value; }
        }
        private string text = "";

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string type = "";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
