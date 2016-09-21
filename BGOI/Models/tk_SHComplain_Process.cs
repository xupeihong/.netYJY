using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHComplain_Process
    {
        public string strTSID = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TSID", "varchar")]
        //投诉编号
        public string TSID
        {
            get { return strTSID; }
            set { strTSID = value; }
        }
        public string strCLID = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CLID", "varchar")]
        //处理编号
        public string CLID
        {
            get { return strCLID; }
            set { strCLID = value; }
        }
        public string strHandleState = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HandleState", "varchar")]
        //处理状态
        public string HandleState
        {
            get { return strHandleState; }
            set { strHandleState = value; }
        }
        public string strHandleProcess = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HandleProcess", "varchar")]
        //处理过程
        public string HandleProcess
        {
            get { return strHandleProcess; }
            set { strHandleProcess = value; }
        }
       //, , ,
        public DateTime strHandleDate;
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HandleDate", "datetime")]
        //处理时间
        public DateTime HandleDate
        {
            get { return strHandleDate; }
            set { strHandleDate = value; }
        }
        public string strCustomerFeedback = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerFeedback", "varchar")]
        //客户反馈
        public string CustomerFeedback
        {
            get { return strCustomerFeedback; }
            set { strCustomerFeedback = value; }
        }
        public string strCostDate;
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CostDate", "varchar")]
        //花费时间
        public string CostDate
        {
            get { return strCostDate; }
            set { strCostDate = value; }
        }
       //, , , Validate
        public string strHandleUser = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HandleUser", "varchar")]
        //处理人
        public string HandleUser
        {
            get { return strHandleUser; }
            set { strHandleUser = value; }
        }
        public DateTime strCreateTime;
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }


        public string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
    }
}
