using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_SubWork
    {
        private string EID;
        private string PID;
        private string SubUnit;
        private string Principal;
        private decimal SubPrice;
        private string WorkCycle;
        private string SubWorkReason;
        private string MainContent;
        private int IsSign;
        private int State;
        private string UnitID;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;


        public string StrEID
        {
            get { return EID; }
            set { EID = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "分包单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrSubUnit
        {
            get { return SubUnit; }
            set { SubUnit = value; }
        }

        [Required(ErrorMessage = "分包单位项目负责人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPrincipal
        {
            get { return Principal; }
            set { Principal = value; }
        }

        [Display(Name = "分包费用")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrSubPrice
        {
            get { return SubPrice; }
            set { SubPrice = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrWorkCycle
        {
            get { return WorkCycle; }
            set { WorkCycle = value; }
        }

        [StringLength(100, ErrorMessage = "分包施工原因不能超过100个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrSubWorkReason
        {
            get { return SubWorkReason; }
            set { SubWorkReason = value; }
        }

        [StringLength(100, ErrorMessage = "分包施工主要内容不能超过100个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrMainContent
        {
            get { return MainContent; }
            set { MainContent = value; }
        }

        public int StrIsSign
        {
            get { return IsSign; }
            set { IsSign = value; }
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
