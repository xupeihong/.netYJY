using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_CardSearch
    {
        private string m_strRepairID;
        private string m_strCustomerName;
        private string m_strCustomerAddr;
        private string m_strMeterID;
        private string m_strMeterName;
        private string m_strSS_Date;
        private string m_strES_Date;

        private string m_strRID;
        private string m_strModelType;
        private string m_strSubUnit;
        public string SubUnit
        {
            get { return m_strSubUnit; }
            set { m_strSubUnit = value; }
        }
        public string RID
        {
            get { return m_strRID; }
            set { m_strRID = value; }
        }

        public string ModelType
        {
            get { return m_strModelType; }
            set { m_strModelType = value; }
        }

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string RepairID
        {
            get { return m_strRepairID; }
            set { m_strRepairID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string CustomerName
        {
            get { return m_strCustomerName; }
            set { m_strCustomerName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string CustomerAddr
        {
            get { return m_strCustomerAddr; }
            set { m_strCustomerAddr = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string MeterID
        {
            get { return m_strMeterID; }
            set { m_strMeterID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string MeterName
        {
            get { return m_strMeterName; }
            set { m_strMeterName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SS_Date
        {
            get { return m_strSS_Date; }
            set { m_strSS_Date = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ES_Date
        {
            get { return m_strES_Date; }
            set { m_strES_Date = value; }
        }


    }
}
