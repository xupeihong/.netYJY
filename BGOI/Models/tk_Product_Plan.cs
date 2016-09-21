using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
     public class tk_Product_Plan
    {
         private string strJHID = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("JHID", "nvarchar")]

        public string JHID
        {
            get { return strJHID; }
            set { strJHID = value; }
        }

        private string strUnitID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "nvarchar")]

        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }

        private string strPlannedyear = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Plannedyear", "nvarchar")]

        public string Plannedyear
        {
            get { return strPlannedyear; }
            set { strPlannedyear = value; }
        }

        private string strPlannedmonth = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Plannedmonth", "nvarchar")]

        public string Plannedmonth
        {
            get { return strPlannedmonth; }
            set { strPlannedmonth = value; }
        }

        private DateTime strSpecifieddate;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Specifieddate", "DateTime")]

        public DateTime Specifieddate
        {
            get { return strSpecifieddate; }
            set { strSpecifieddate = value; }
        }

        private string strFormulation = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Formulation", "nvarchar")]

        public string Formulation
        {
            get { return strFormulation; }
            set { strFormulation = value; }
        }

        private string strRemarks = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remarks", "nvarchar")]

        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        private string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "nvarchar")]

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strApprovalstatus = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Approvalstatus", "nvarchar")]

        public string Approvalstatus
        {
            get { return strApprovalstatus; }
            set { strApprovalstatus = value; }
        }


        private string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "nvarchar")]

        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private DateTime strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "DateTime")]

        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "nvarchar")]

        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
