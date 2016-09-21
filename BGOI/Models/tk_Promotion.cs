using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_Promotion
    {
        private string m_strPID = "";
        [DataField("PID", "nvarchar")]
        public string PID
        {
            get { return m_strPID; }
            set { m_strPID = value; }
        }

        private string m_strUnitID = "";

        [DataField("UnitID","varchar")]
        public string UnitID
        {
            get { return m_strUnitID; }
            set { m_strUnitID = value; }
        }

        private string m_strApplyer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Applyer", "nvarchar")]
        public string Applyer
        {
            get { return m_strApplyer; }
            set { m_strApplyer = value; }
        }

        private DateTime dtApplyTime;
        [DataField("ApplyTime", "datetime")]
        public DateTime ApplyTime
        {
            get { return dtApplyTime; }
            set { dtApplyTime = value; }
        }

        private DateTime dtStartTime;
        [DataField("StartTime", "datetime")]
        public DateTime StartTime
        {
            get { return dtStartTime; }
            set { dtStartTime = value; }
        }

        private DateTime dtEndTime;
        [DataField("EndTime", "datetime")]
        public DateTime EndTime
        {
            get { return dtEndTime; }
            set { dtEndTime = value; }
        }

        private string m_strActionTitle = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ActionTitle", "nvarchar")]
        public string ActionTitle
        {
            get { return m_strActionTitle; }
            set { m_strActionTitle = value; }
        }

        private string m_strActionProject = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ActionProject", "nvarchar")]
        public string ActionProject
        {
            get { return m_strActionProject; }
            set { m_strActionProject = value; }
        }

        private string m_strPosition = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Position", "nvarchar")]
        public string Position
        {
            get { return m_strPosition; }
            set { m_strPosition = value; }
        }

        private string m_strActionStyle = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ActionStyle", "nvarchar")]
        public string ActionStyle
        {
            get { return m_strActionStyle; }
            set { m_strActionStyle = value; }
        }

        private string m_strPurPose = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PurPose", "nvarchar")]
        public string PurPose
        {
            get { return m_strPurPose; }
            set { m_strPurPose = value; }
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

        private DateTime strCreateTime;
        [DataField("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return m_strCreateUser; }
            set { m_strCreateUser = value; }
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
