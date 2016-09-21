using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_WXRecord_Product
    {
        public string strWXID = "";
        [DataFieldAttribute("WXID", "varchar")]
        //关联维修单编号
        public string WXID
        {
            get { return strWXID; }
            set { strWXID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //编号规则WXID+“-”+序号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strLname = "";
        [DataFieldAttribute("Lname", "varchar")]
        //更换零件名称
        public string Lname
        {
            get { return strLname; }
            set { strLname = value; }
        }
        public decimal strUnitPrice;
        [DataFieldAttribute("UnitPrice", "decimal")]
        //单价
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        public int strAmount;
        [DataFieldAttribute("Amount", "int")]
        //数量
        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }
        public decimal strTotal;
        [DataFieldAttribute("Total", "decimal")]
        //合计
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
    }
}
