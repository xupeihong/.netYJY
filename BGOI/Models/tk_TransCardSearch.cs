using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_TransCardSearch
    {
        private string m_strRepairID;
        private string m_strCustomerName;
        private string m_strMeterID;

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
        public string MeterID
        {
            get { return m_strMeterID; }
            set { m_strMeterID = value; }
        }
        
    }
}
