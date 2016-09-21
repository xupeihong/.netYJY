using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_MarketSales
    {
        private string m_strPID = "";
        [DataField("PID", "varchar")]
        public string PID
        {
            get { return m_strPID; }
            set { m_strPID = value; }
        }

        private string m_strUnitID = "";
        [DataField("UnitID", "UnitID")]
        public string UnitID
        {
            get { return m_strUnitID; }
            set { m_strUnitID = value; }
        }

        private string m_strApplyType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyType", "nvarchar")]
        public string ApplyType
        {
            get { return m_strApplyType; }
            set { m_strApplyType = value; }
        }

        private string m_strApplyTitle = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyTitle", "nvarchar")]
        public string ApplyTitle
        {
            get { return m_strApplyTitle; }
            set { m_strApplyTitle = value; }
        }

        private DateTime dtApplyTime;
        [DataField("ApplyTime", "datetime")]
        public DateTime ApplyTime
        {
            get { return dtApplyTime; }
            set { dtApplyTime = value; }
        }

        private string m_strManager = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Manager", "nvarchar")]
        public string Manager
        {
            get { return m_strManager; }
            set { m_strManager = value; }
        }

        private string m_strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }

        private string m_strState = "";
        [DataField("State", "nvarchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return m_strCreateUser; }
            set { m_strCreateUser = value; }
        }

        private DateTime dtCreateTime;
        [DataField("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }

        private string m_strValidate = "";
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return m_strValidate; }
            set { m_strValidate = value; }
        }
    }
}
