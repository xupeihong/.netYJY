using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_SupplierBasHis
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _COMNameC;
        [Remote("CheckSupplyName", "SuppliesManage", ErrorMessage = "该供应商名称已存在")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMNameC", "nvarchar")]
        public string COMNameC
        {
            get { return _COMNameC; }
            set { _COMNameC = value; }
        }
        private string _NCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return _NCreateUser; }
            set { _NCreateUser = value; }
        }
        private string _NCreateTime;
        [DataFieldAttribute("NCreateTime", "varchar")]
        public string NCreateTime
        {
            get { return _NCreateTime; }
            set { _NCreateTime = value; }
        }
    }
}
