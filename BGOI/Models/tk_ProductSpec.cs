using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductSpec
    {

        public string strGGID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("GGID", "varchar")]
        //编号
        public string GGID
        {
            get { return strGGID; }
            set { strGGID = value; }
        }
        public string strSpec = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Spec", "varchar")]
        //规格型号
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
    }
}
