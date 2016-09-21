using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProFinishDefine
    {
        private string strProductID = "";
        [DataFieldAttribute("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        private string strPartPID = "";
        [DataFieldAttribute("PartPID", "varchar")]
        public string PartPID
        {
            get { return strPartPID; }
            set { strPartPID = value; }
        }
        private int strNumber;
        [DataFieldAttribute("Number", "int")]
        public int Number
        {
            get { return strNumber; }
            set { strNumber = value; }
        }
        private string strSpec = "";
        [DataFieldAttribute("Spec", "varchar")]
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
        public string strValiDate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ValiDate", "varchar")]
        public string ValiDate
        {
            get { return strValiDate; }
            set { strValiDate = value; }
        }
        public string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        public string strIdentifierStr = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IdentifierStr", "varchar")]
        public string IdentifierStr
        {
            get { return strIdentifierStr; }
            set { strIdentifierStr = value; }
        }
        public string strIdentitySharing = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IdentitySharing", "varchar")]
        public string IdentitySharing
        {
            get { return strIdentitySharing; }
            set { strIdentitySharing = value; }
        }
    }
}
