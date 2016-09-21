using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHComplain_Product_HIS
    {
        public string strTSID = "";
        [DataFieldAttribute("TSID", "varchar")]
        //投诉编号
        public string TSID
        {
            get { return strTSID; }
            set { strTSID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        // 内容编号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        // 物料编码
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //内容，产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        public string strSpecsModels = "";
        [DataFieldAttribute("SpecsModels", "varchar")]
        //规格型号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }
        public string strUnit = "";
        [DataFieldAttribute("Unit", "varchar")]
        //单位
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        public int strNum;
        [DataFieldAttribute("Num", "int")]
        //数量
        public int Num
        {
            get { return strNum; }
            set { strNum = value; }
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
        public string strOrderID = "";
        [DataFieldAttribute("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
    }
}

