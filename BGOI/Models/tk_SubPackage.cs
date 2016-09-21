using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_SubPackage
    {
        private string SID;
        private string PID;
        private string DesignUnit;
        private string DesignPrincipal;
        private decimal DesignPrice;
        private string PredictCycle;
        private string SubReason;
        private string MainContent;
        private int State;
        private string UnitID;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;


        public string StrSID
        {
            get { return SID; }
            set { SID = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "分包设计单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrDesignUnit
        {
            get { return DesignUnit; }
            set { DesignUnit = value; }
        }

        [Required(ErrorMessage = "分包设计单位项目负责人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrDesignPrincipal
        {
            get { return DesignPrincipal; }
            set { DesignPrincipal = value; }
        }

        [Display(Name = "分包设计费用")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrDesignPrice
        {
            get { return DesignPrice; }
            set { DesignPrice = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPredictCycle
        {
            get { return PredictCycle; }
            set { PredictCycle = value; }
        }

        [StringLength(100, ErrorMessage = "分包设计原因不能超过100个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrSubReason
        {
            get { return SubReason; }
            set { SubReason = value; }
        }

        [StringLength(100, ErrorMessage = "分包设计主要内容不能超过100个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrMainContent
        {
            get { return MainContent; }
            set { MainContent = value; }
        }

        public int StrState
        {
            get { return State; }
            set { State = value; }
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
