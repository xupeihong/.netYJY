using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_TransferS
    {
       private string strSHID;
        [DataFieldAttribute("SHID", "varchar")]
       public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }


        private string strTransferNum;
        [DataFieldAttribute("TransferNum", "varchar")]
        public string TransferNum
        {
            get { return strTransferNum; }
            set { strTransferNum = value; }
        }


        private string strSJPeople;
        [DataFieldAttribute("SJPeople", "varchar")]
        public string SJPeople
        {
            get { return strSJPeople; }
            set { strSJPeople = value; }
        }

        private DateTime strInspectiondate;
        [DataFieldAttribute("Inspectiondate", "varchar")]
        public DateTime Inspectiondate
        {
            get { return strInspectiondate; }
            set { strInspectiondate = value; }
        }



        private DateTime strGooddate;
        [DataFieldAttribute("Gooddate", "varchar")]
        public DateTime Gooddate
        {
            get { return strGooddate; }
            set { strGooddate = value; }
        }



        private DateTime strLJReturnDate;
        [DataFieldAttribute("LJReturnDate", "varchar")]
        public DateTime LJReturnDate
        {
            get { return strLJReturnDate; }
            set { strLJReturnDate = value; }
        }



        private string strSummary;
        [DataFieldAttribute("Summary", "varchar")]
        public string Summary
        {
            get { return strSummary; }
            set { strSummary = value; }
        }




        private string strtestPeople;
        [DataFieldAttribute("testPeople", "varchar")]
        public string testPeople
        {
            get { return strtestPeople; }
            set { strtestPeople = value; }
        }



        private string strqualified;
        [DataFieldAttribute("qualified", "varchar")]
        public string qualified
        {
            get { return strqualified; }
            set { strqualified = value; }
        }




        private string strNoqualified;
        [DataFieldAttribute("Noqualified", "varchar")]
        public string Noqualified
        {
            get { return strNoqualified; }
            set { strNoqualified = value; }
        }




        private string strproductionPeople;
        [DataFieldAttribute("productionPeople", "varchar")]
        public string productionPeople
        {
            get { return strproductionPeople; }
            set { strproductionPeople = value; }
        }




        private string strplanPeople;
        [DataFieldAttribute("planPeople", "varchar")]
        public string planPeople
        {
            get { return strplanPeople; }
            set { strplanPeople = value; }
        }




        private string strWarehouse;
        [DataFieldAttribute("Warehouse", "varchar")]
        public string Warehouse
        {
            get { return strWarehouse; }
            set { strWarehouse = value; }
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
