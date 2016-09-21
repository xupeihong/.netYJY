using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SupplySearch
    {
        private string _COMNameC;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMNameC", "nvarchar")]
        public string COMNameC
        {
            get { return _COMNameC; }
            set { _COMNameC = value; }
        }
        private string _CName;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CName", "nvarchar")]
        public string CName
        {
            get { return _CName; }
            set { _CName = value; }
        }
    }
}
