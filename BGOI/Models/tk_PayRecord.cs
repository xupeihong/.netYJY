using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_PayRecord
    {
        private string PayID;
        private string PID;
        private string PayType;
        private decimal PayPrice;
        private string PayDate;
        private string PayPerson;
        private int State;
        private string UnitID;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;


        public string StrPayID
        {
            get { return PayID; }
            set { PayID = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "费用支出类型不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPayType
        {
            get { return PayType; }
            set { PayType = value; }
        }

        [Display(Name = "费用支出金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrPayPrice
        {
            get { return PayPrice; }
            set { PayPrice = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPayDate
        {
            get { return PayDate; }
            set { PayDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPayPerson
        {
            get { return PayPerson; }
            set { PayPerson = value; }
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
