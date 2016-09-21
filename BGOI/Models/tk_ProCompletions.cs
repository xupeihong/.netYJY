using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProCompletions
    {
        private string cid;
        private string PID;
        private string CompleteDate;
        private string CompleteFileName;
        private string UnitID;
        private string CompleteRmark;
        private string CompletePerson;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;

        public string Strcid
        {
            get { return cid; }
            set { cid = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "竣工日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCompleteDate
        {
            get { return CompleteDate; }
            set { CompleteDate = value; }
        }

        public string StrCompleteFileName
        {
            get { return CompleteFileName; }
            set { CompleteFileName = value; }
        }

        [Required(ErrorMessage = "验收人员不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCompletePerson
        {
            get { return CompletePerson; }
            set { CompletePerson = value; }
        }

        [StringLength(20, ErrorMessage = "情况说明不能超过200个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCompleteRmark
        {
            get { return CompleteRmark; }
            set { CompleteRmark = value; }
        }

        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
        }

        public string StrCreatePerson
        {
            get { return CreatePerson; }
            set { CreatePerson = value; }
        }

        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
