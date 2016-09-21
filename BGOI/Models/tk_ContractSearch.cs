using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_ContractSearch
    {
        private string m_strCname;
        private string m_strContractID;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Cname
        {
            get { return m_strCname; }
            set { m_strCname = value; }
        }

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ContractID
        {
            get { return m_strContractID; }
            set { m_strContractID = value; }
        }

    }
}
