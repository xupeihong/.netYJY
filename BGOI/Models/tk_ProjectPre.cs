using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProjectPre
    {
        private string PID;
        private string ProID;
        private string Pname;
        private string Psource;
        private string CustomerName;
        private string Goal;
        private string MainContent;
        private string CreateUser;
        private DateTime? CreateTime;
        private string UnitID;
        private string Validate;


        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrProID
        {
            get { return ProID; }
            set { ProID = value; }
        }

        [Required(ErrorMessage = "项目名称不能为空")]
        [StringLength(20, ErrorMessage = "项目名称长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPname
        {
            get { return Pname; }
            set { Pname = value; }
        }

        [Required(ErrorMessage = "项目来源不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPsource
        {
            get { return Psource; }
            set { Psource = value; }
        }

        [Required(ErrorMessage = "客户名称不能为空")]
        [StringLength(50, ErrorMessage = "客户名称长度不能超过50个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCustomerName
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrGoal
        {
            get { return Goal; }
            set { Goal = value; }
        }

        [Required(ErrorMessage = "项目主要内容不能为空")]
        [StringLength(200, ErrorMessage = "项目主要内容长度不能超过200个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrMainContent
        {
            get { return MainContent; }
            set { MainContent = value; }
        }

        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
        }

        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
