using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_ProFinishDefine_HIS
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
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }

        public string strValiDate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ValiDate", "varchar")]
        public string ValiDate
        {
            get { return strValiDate; }
            set { strValiDate = value; }
        }
    }
}
