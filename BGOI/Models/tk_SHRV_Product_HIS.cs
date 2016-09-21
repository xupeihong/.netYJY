using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHRV_Product_HIS
    {

        public string strHFID = "";
        [DataFieldAttribute("HFID", "varchar")]
        //关联回访单编号
        public string HFID
        {
            get { return strHFID; }
            set { strHFID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //内容编号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        //物料编码
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
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
        public decimal strUnitPrice;
        [DataFieldAttribute("UnitPrice", "decimal")]
        //单价
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        public decimal strTotal;
        [DataFieldAttribute("Total", "decimal")]
        //总价
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
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
    }
}

