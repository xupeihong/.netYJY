using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_Project
    {
        private string PID;
        private string JQType;
        private string Pview;
        private string JQTime;
        private string FollowPerson;
        private string UnitID;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;


        [DataFieldAttribute("PID", "varchar")]
        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "跟踪洽谈类型不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("JQType", "varchar")]
        public string StrJQType
        {
            get { return JQType; }
            set { JQType = value; }
        }

        [Required(ErrorMessage = "内容概述不能为空")]
        [StringLength(200, ErrorMessage = "接洽内容不能超过200个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Pview", "varchar")]
        public string StrPview
        {
            get { return Pview; }
            set { Pview = value; }
        }

        [Required(ErrorMessage = "接洽时间不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("JQTime", "varchar")]
        public string StrJQTime
        {
            get { return JQTime; }
            set { JQTime = value; }
        }

        [Required(ErrorMessage = "跟踪人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FollowPerson", "varchar")]
        public string StrFollowPerson
        {
            get { return FollowPerson; }
            set { FollowPerson = value; }
        }

        [DataFieldAttribute("UnitID", "varchar")]
        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
        }

        [DataFieldAttribute("CreatePerson", "varchar")]
        public string StrCreatePerson
        {
            get { return CreatePerson; }
            set { CreatePerson = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
