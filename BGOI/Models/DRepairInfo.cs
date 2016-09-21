using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class DRepairInfo
    {
        private string ECode;
        private string ServiceReason;
        private string ServiceReasonO;
        private string ServiceTime;
        private string ServiceCompany;
        private string ServiceRecord;
        private string ServiceResults;
        private string ReturnTime;
        private string Remark;

        [DataFieldAttribute("ECode", "varchar")]
        public string StrECode
        {
            get { return ECode; }
            set { ECode = value; }
        }

        [DataFieldAttribute("ServiceReason", "nvarchar")]
        public string StrServiceReason
        {
            get { return ServiceReason; }
            set { ServiceReason = value; }
        }

        [DataFieldAttribute("ServiceReasonO", "nvarchar")]
        public string StrServiceReasonO
        {
            get { return ServiceReasonO; }
            set { ServiceReasonO = value; }
        }

        [DataFieldAttribute("ServiceTime", "date")]
        public string StrServiceTime
        {
            get { return ServiceTime; }
            set { ServiceTime = value; }
        }

        [DataFieldAttribute("ServiceCompany", "nvarchar")]
        public string StrServiceCompany
        {
            get { return ServiceCompany; }
            set { ServiceCompany = value; }
        }

        [DataFieldAttribute("ServiceRecord", "nvarchar")]
        public string StrServiceRecord
        {
            get { return ServiceRecord; }
            set { ServiceRecord = value; }
        }

        [DataFieldAttribute("ServiceResults", "nvarchar")]
        public string StrServiceResults
        {
            get { return ServiceResults; }
            set { ServiceResults = value; }
        }

        [DataFieldAttribute("ReturnTime", "date")]
        public string StrReturnTime
        {
            get { return ReturnTime; }
            set { ReturnTime = value; }
        }

        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }
    }
}
