using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_InspecSearch
    {
        private string m_strSID;
        private string m_strUnitName;
        private string m_strSInspecDate;
        private string m_strEInspecDate;
        private string m_strBathID;

        [StringLength(20, ErrorMessage = "送检单编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SID
        {
            get { return m_strSID; }
            set { m_strSID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string UnitName
        {
            get { return m_strUnitName; }
            set { m_strUnitName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SInspecDate
        {
            get { return m_strSInspecDate; }
            set { m_strSInspecDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string EInspecDate
        {
            get { return m_strEInspecDate; }
            set { m_strEInspecDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string BathID
        {
            get { return m_strBathID; }
            set { m_strBathID = value; }
        }

    }
}
