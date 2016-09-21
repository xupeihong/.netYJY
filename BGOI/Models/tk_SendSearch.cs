using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_SendSearch
    {
        private string m_strDeliverID;
        private string m_strUnitName;
        private string m_strSReceiveDate;
        private string m_strEReceiveDate;
        private string m_strSSendDate;
        private string m_strESendDate;

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string DeliverID
        {
            get { return m_strDeliverID; }
            set { m_strDeliverID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string UnitName
        {
            get { return m_strUnitName; }
            set { m_strUnitName = value; }
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
        public string SSendDate
        {
            get { return m_strSSendDate; }
            set { m_strSSendDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ESendDate
        {
            get { return m_strESendDate; }
            set { m_strESendDate = value; }
        }
    }

}
