using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_InspecDetailSearch
    {
        private string m_strOutUnit;
        private string m_strBathID;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string OutUnit
        {
            get { return m_strOutUnit; }
            set { m_strOutUnit = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string BathID
        {
            get { return m_strBathID; }
            set { m_strBathID = value; }
        }


    }
}
