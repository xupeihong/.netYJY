using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_Budget
    {
        private string bid;
        private string PID;
        private string Pview;
        private string UnitID;
        private string CreatePerson;
        private DateTime? CreateTime;
        private int State;
        private string Validate;

        [DataFieldAttribute("bid", "varchar")]
        public string Strbid
        {
            get { return bid; }
            set { bid = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "概述不能为空")]
        [StringLength(200, ErrorMessage = "概述长度不能超过200个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPview
        {
            get { return Pview; }
            set { Pview = value; }
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

        public int StrState
        {
            get { return State; }
            set { State = value; }
        }

        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
