using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_ProjectSearch
    {
        private string m_strProID;
        private string m_strPname;
        private string m_strStartDate;
        private string m_strEndDate;
        private string m_strPrincipal;

        private string m_strJQType;//接洽类型

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection (ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ProID
        {
            get { return m_strProID; }
            set { m_strProID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Pname
        {
            get { return m_strPname; }
            set { m_strPname = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StartDate
        {
            get { return m_strStartDate; }
            set { m_strStartDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string EndDate
        {
            get { return m_strEndDate; }
            set { m_strEndDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string JQType
        {
            get { return m_strJQType; }
            set { m_strJQType = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Principal
        {
            get { return m_strPrincipal; }
            set { m_strPrincipal = value; }
        }
    }
}
