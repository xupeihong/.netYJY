using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_DeliverySearch
    {
        private string m_strTakeID;
        private string m_strUnitName;
        private string m_strReceiveName;
        private string m_strSReceiveDate;
        private string m_strEReceiveDate;
        private string m_strSDeliverDate;
        private string m_strEDeliverDate;

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string TakeID
        {
            get { return m_strTakeID; }
            set { m_strTakeID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string UnitName
        {
            get { return m_strUnitName; }
            set { m_strUnitName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ReceiveName
        {
            get { return m_strReceiveName; }
            set { m_strReceiveName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SReceiveDate
        {
            get { return m_strSReceiveDate; }
            set { m_strSReceiveDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string EReceiveDate
        {
            get { return m_strEReceiveDate; }
            set { m_strEReceiveDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SDeliverDate
        {
            get { return m_strSDeliverDate; }
            set { m_strSDeliverDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string EDeliverDate
        {
            get { return m_strEDeliverDate; }
            set { m_strEDeliverDate = value; }
        }
    }

}

