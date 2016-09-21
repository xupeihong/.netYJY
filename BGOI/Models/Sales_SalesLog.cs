using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Sales_SalesLog
    {
        private string strLogTime = "";
        [DataFieldAttribute("LogTime", "datetime")]
        public string LogTime
        {
            get { return strLogTime; }
            set { strLogTime = value; }
        }

        private string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strLogContent = "";
        [DataFieldAttribute("LogContent", "nvarchar")]
        public string LogContent
        {
            get { return strLogContent; }
            set { strLogContent = value; }
        }

        private string strProductType = "";
        [DataFieldAttribute("ProductType", "nvarchar")]
        public string ProductType
        {
            get { return strProductType; }
            set { strProductType = value; }
        }

        private string m_strSalesType = "";
        [DataFieldAttribute("SalesType", "nvarchar")]
        public string SalesType
        {
            get { return m_strSalesType; }
            set { m_strSalesType = value; }
        }

        private string m_strSignTime = "";
        [DataFieldAttribute("SignTime", "datetime")]
        public string SignTime
        {
            get { return m_strSignTime; }
            set { m_strSignTime = value; }
        }

        private string strActor = "";
        [DataFieldAttribute("Actor", "nvarchar")]
        public string Actor
        {
            get { return strActor; }
            set { strActor = value; }
        }

        private string strUnit = "";
        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
    }
}
