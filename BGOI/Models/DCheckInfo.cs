using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class DCheckInfo
    {
        private string ECode;
        private string CheckDate;
        private string CheckWay;
        private string CheckCompany;
        private float Charge;
        private string Precision;
        private string CensorshipRemark;
        private string Principal;
        private string CalibrationResults;

        [DataFieldAttribute("ECode", "varchar")]
        public string StrECode
        {
            get { return ECode; }
            set { ECode = value; }
        }

        [DataFieldAttribute("CheckDate", "date")]
        public string StrCheckDate
        {
            get { return CheckDate; }
            set { CheckDate = value; }
        }

        [DataFieldAttribute("CheckWay", "varchar")]
        public string StrCheckWay
        {
            get { return CheckWay; }
            set { CheckWay = value; }
        }

        [DataFieldAttribute("CheckCompany", "nvarchar")]
        public string StrCheckCompany
        {
            get { return CheckCompany; }
            set { CheckCompany = value; }
        }

        [DataFieldAttribute("Charge", "decimal")]
        public float StrCharge
        {
            get { return Charge; }
            set { Charge = value; }
        }

        [DataFieldAttribute("Precision", "varchar")]
        public string StrPrecision
        {
            get { return Precision; }
            set { Precision = value; }
        }

        [DataFieldAttribute("CensorshipRemark", "nvarchar")]
        public string StrCensorshipRemark
        {
            get { return CensorshipRemark; }
            set { CensorshipRemark = value; }
        }

        [DataFieldAttribute("Principal", "nvarchar")]
        public string StrPrincipal
        {
            get { return Principal; }
            set { Principal = value; }
        }

        [DataFieldAttribute("CalibrationResults", "nvarchar")]
        public string StrCalibrationResults
        {
            get { return CalibrationResults; }
            set { CalibrationResults = value; }
        }
    }
}
