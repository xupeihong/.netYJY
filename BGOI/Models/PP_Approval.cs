using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_Approval
    {
        private string strPID;
         [DataFieldAttribute("PID", "nvarchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

         private string strApprovalContent;
         [DataFieldAttribute("ApprovalContent", "nvarchar")]
         public string ApprovalContent
         {
             get { return strApprovalContent; }
             set { strApprovalContent = value; }
         }
         private string strPIDS;
         [DataFieldAttribute("PIDS", "nvarchar")]
         public string PIDS
         {
             get { return strPIDS; }
             set { strPIDS = value; }
         }

         private string strApprovalType;
         [DataFieldAttribute("ApprovalType", "nvarchar")]
         public string ApprovalType
         {
             get { return strApprovalType; }
             set { strApprovalType = value; }
         }

         private string strApprovalLevel;
         [DataFieldAttribute("ApprovalLevel", "nvarchar")]
         public string ApprovalLevel
         {
             get { return strApprovalLevel; }
             set { strApprovalLevel = value; }
         }

         private string strApprovaler;
         [DataFieldAttribute("Approvaler", "nvarchar")]
         public string Approvaler
         {
             get { return strApprovaler; }
             set { strApprovaler = value; }
         }

         private string strJob;
         [DataFieldAttribute("Job", "nvarchar")]
         public string Job
         {
             get { return strJob; }
             set { strJob = value; }
         }
         private DateTime dtApprovalTime;
         [DataFieldAttribute("ApprovalTime", "DateTime")]
         public DateTime ApprovalTime
         {
             get { return dtApprovalTime; }
             set { dtApprovalTime = value; }
         }

         private string strIsPass;
         [DataFieldAttribute("IaPass", "nvarchar")]
         public string IsPass
         {
             get { return strIsPass; }
             set { strIsPass = value; }
         }
         private string strNoPassReason;
         [DataFieldAttribute("NoPassReason", "nvarchar")]
         public string NoPassReason
         {
             get { return strNoPassReason; }
             set { strNoPassReason = value; }
         }
         private string strApprovalExplain;
         [DataFieldAttribute("ApprovalExplain", "nvarchar")]
         public string ApprovalExplain
         {
             get { return strApprovalExplain; }
             set { strApprovalExplain = value; }
         }
    }
}
