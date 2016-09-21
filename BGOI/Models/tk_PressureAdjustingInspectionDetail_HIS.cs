using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_PressureAdjustingInspectionDetail_HIS
    {
        //, , , ,
        public string strTXID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TXID", "varchar")]
        //调压记录编号
        public string TXID
        {
            get { return strTXID; }
            set { strTXID = value; }
        }
        public string strTYID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TYID", "varchar")]
        //调压记录编号
        public string TYID
        {
            get { return strTYID; }
            set { strTYID = value; }
        }
        public string strUsePressureShangP1 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureShangP1", "varchar")]
        //使用压力    上台 P1    MPa 
        public string UsePressureShangP1
        {
            get { return strUsePressureShangP1; }
            set { strUsePressureShangP1 = value; }
        }
        public string strUsePressureShangP2 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureShangP2", "varchar")]
        //使用压力    上台 P2    kPa 
        public string UsePressureShangP2
        {
            get { return strUsePressureShangP2; }
            set { strUsePressureShangP2 = value; }
        }
        //, , , , 
        public string strUsePressureShangPb = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureShangPb", "varchar")]
        //使用压力    上台 Pb    kPa
        public string UsePressureShangPb
        {
            get { return strUsePressureShangPb; }
            set { strUsePressureShangPb = value; }
        }
        public string strUsePressureShangPf = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureShangPf", "varchar")]
        //使用压力    Pf    kPa 
        public string UsePressureShangPf
        {
            get { return strUsePressureShangPf; }
            set { strUsePressureShangPf = value; }
        }
        public string strUsePressureXiaP1 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureXiaP1", "varchar")]
        //使用压力  下台 P1    MPa 
        public string UsePressureXiaP1
        {
            get { return strUsePressureXiaP1; }
            set { strUsePressureXiaP1 = value; }
        }
        public string strUsePressureXiaP2 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureXiaP2", "varchar")]
        //使用压力  下台 P2    kPa 
        public string UsePressureXiaP2
        {
            get { return strUsePressureXiaP2; }
            set { strUsePressureXiaP2 = value; }
        }
        public string strUsePressureXiaPb = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UsePressureXiaPb", "varchar")]
        //使用压力  下台 Pb    kPa 
        public string UsePressureXiaPb
        {
            get { return strUsePressureXiaPb; }
            set { strUsePressureXiaPb = value; }
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
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
