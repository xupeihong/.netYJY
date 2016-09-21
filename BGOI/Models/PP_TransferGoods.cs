using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_TransferGoods
    {


        private string strID;
        [DataFieldAttribute("ID", "varchar")]
        public string ID
        {
            get { return strID; }
            set { strID = value; }
        }



       private string strPID;
        [DataFieldAttribute("PID", "varchar")]
       public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }



        private string strGoodsNum;
        [DataFieldAttribute("GoodsNum", "varchar")]
        public string GoodsNum
        {
            get { return strGoodsNum; }
            set { strGoodsNum = value; }
        }

        private string strSupplier;
        [DataFieldAttribute("Supplier", "varchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }

        private string strGoodsName;
        [DataFieldAttribute("GoodsName", "varchar")]
        public string GoodsName
        {
            get { return strGoodsName; }
            set { strGoodsName = value; }
        }


        private string strGoodsSpe;
        [DataFieldAttribute("GoodsSpe", "varchar")]
        public string GoodsSpe
        {
            get { return strGoodsSpe; }
            set { strGoodsSpe = value; }
        }



        private string strAmount;
        [DataFieldAttribute("Amount", "varchar")]
        public string Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }



        private string strUnit;
        [DataFieldAttribute("Unit", "varchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }



        private string strYesAmount;
        [DataFieldAttribute("YesAmount", "varchar")]
        public string YesAmount
        {
            get { return strYesAmount; }
            set { strYesAmount = value; }
        }



        private string strNoAmount;
        [DataFieldAttribute("NoAmount", "varchar")]
        public string NoAmount
        {
            get { return strNoAmount; }
            set { strNoAmount = value; }
        }



        private string strBak;
        [DataFieldAttribute("Bak", "varchar")]
        public string Bak
        {
            get { return strBak; }
            set { strBak = value; }
        }


        private string strRemark;
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }


    }
}
