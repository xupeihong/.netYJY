using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Sales_RecordInfo
    {
        private string strPID = "";

        [DataFieldAttribute("PID", "nvarchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private DateTime dtRecordDate;

        [DataFieldAttribute("RecordDate", "datetime")]
        public DateTime RecordDate
        {
            get { return dtRecordDate; }
            set { dtRecordDate = value; }
        }

        private string strPlanID = "";

        [DataFieldAttribute("PlanID", "nvarchar")]
        public string PlanID
        {
            get { return strPlanID; }
            set { strPlanID = value; }
        }

        private string strPlanName = "";

        [DataFieldAttribute("PlanName", "nvarchar")]
        public string PlanName
        {
            get { return strPlanName; }
            set { strPlanName = value; }
        }

        private string strRemark = "";

        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
    }
}
