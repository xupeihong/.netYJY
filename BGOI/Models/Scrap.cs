using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Scrap
    {

        private string strListScrapID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListScrapID", "varchar")]
        public string ListScrapID
        {
            get { return strListScrapID; }
            set { strListScrapID = value; }
        }

        private string strInspector = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Inspector", "varchar")]
        public string Inspector
        {
            get { return strInspector; }
            set { strInspector = value; }
        }

        private DateTime strScrapTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ScrapTime", "datetime")]
        public DateTime ScrapTime
        {
            get { return strScrapTime; }
            set { strScrapTime = value; }
        }

        private string strHandlers = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Handlers", "nvarchar")]
        public string Handlers
        {
            get { return strHandlers; }
            set { strHandlers = value; }
        }

        private string strUserID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserID", "varchar")]
        public string UserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }

        private int strState;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        public int State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private int strScrapCount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ScrapCount", "varchar")]
        public int ScrapCount
        {
            get { return strScrapCount; }
            set { strScrapCount = value; }
        }

        private string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strFactoryNum = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FactoryNum", "varchar")]
        public string FactoryNum
        {
            get { return strFactoryNum; }
            set { strFactoryNum = value; }
        }

        private string strReasonRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReasonRemark", "varchar")]
        public string ReasonRemark
        {
            get { return strReasonRemark; }
            set { strReasonRemark = value; }
        }

        private string strHandling = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Handling", "varchar")]
        public string Handling
        {
            get { return strHandling; }
            set { strHandling = value; }
        }

        //private int strID;
        //[DataFieldAttribute("ID", "varchar")]
        //public int ID
        //{
        //    get { return strID; }
        //    set { strID = value; }
        //}

        private string strHouseID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HouseID", "varchar")]
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }

        private string strSubjectID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SubjectID", "varcar")]
        public string SubjectID
        {
            get { return strSubjectID; }
            set { strSubjectID = value; }
        }

        public decimal strAmountM;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AmountM", "varchar")]
        public decimal AmountM
        {
            get { return strAmountM; }
            set { strAmountM = value; }
        }

    }
}
