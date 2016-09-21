using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StockIn
    {
        private string strListInID = "";
        [DataFieldAttribute("ListInID", "varchar")]
        public string ListInID
        {
            get { return strListInID; }
            set { strListInID = value; }
        }

        private string strBatchID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BatchID", "varchar")]
        public string BatchID
        {
            get { return strBatchID; }
            set { strBatchID = value; }
        }

        private string strSubjectID = "";
        [DataFieldAttribute("SubjectID", "varchar")]
        public string SubjectID
        {
            get { return strSubjectID; }
            set { strSubjectID = value; }
        }

        private DateTime strStockInTime;
        [DataFieldAttribute("StockInTime", "datetime")]
        public DateTime StockInTime
        {
            get { return strStockInTime; }
            set { strStockInTime = value; }
        }

        private string strStockInUser = "";
        [DataFieldAttribute("StockInUser", "nvarchar")]
        public string StockInUser
        {
            get { return strStockInUser; }
            set { strStockInUser = value; }
        }

        private string strHouseID = "";
        [DataFieldAttribute("HouseID", "varchar")]
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }


        private string strProPlace = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProPlace", "varchar")]
        public string ProPlace
        {
            get { return strProPlace; }
            set { strProPlace = value; }
        }

        private int strState;
        [DataFieldAttribute("State", "varchar")]
        public int State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strValiDate = "";
        [DataFieldAttribute("ValiDate", "varchar")]
        public string ValiDate
        {
            get { return strValiDate; }
            set { strValiDate = value; }
        }

        private decimal strAmountM;
        [DataFieldAttribute("Amount", "varchar")]
        public decimal AmountM
        {
            get { return strAmountM; }
            set { strAmountM = value; }
        }

        private string strType = "";
        [DataFieldAttribute("Type", "varchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }

        private string strDrawID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DrawID", "varchar")]
        public string DrawID
        {
            get { return strDrawID; }
            set { strDrawID = value; }
        }
        private string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private string strApplyPer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ApplyPer", "varchar")]
        public string ApplyPer
        {
            get { return strApplyPer; }
            set { strApplyPer = value; }
        }
        private DateTime strApplyTime;
        [DataFieldAttribute("ApplyTime", "datetime")]
        public DateTime ApplyTime
        {
            get { return strApplyTime; }
            set { strApplyTime = value; }
        }
        private string strHandwrittenAccount = "";
        [DataFieldAttribute("HandwrittenAccount", "nvarchar")]
        public string HandwrittenAccount
        {
            get { return strHandwrittenAccount; }
            set { strHandwrittenAccount = value; }
        }

    }
}
