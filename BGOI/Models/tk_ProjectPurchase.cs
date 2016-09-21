using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProjectPurchase
    {
        private string PcID;
        private string PID;
        private string PcName;
        private string PartA;
        private string PartB;
        private string PcNum;
        private string Pname;
        private decimal PcAmount;
        private string PcType;
        private string UnitID;
        private int State;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;


        public string StrPcID
        {
            get { return PcID; }
            set { PcID = value; }
        }

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "合同名称不能为空")]
        [StringLength(50, ErrorMessage = "概述长度不能超过50个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPcName
        {
            get { return PcName; }
            set { PcName = value; }
        }

        [StringLength(50, ErrorMessage = "甲方不能超过50个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPartA
        {
            get { return PartA; }
            set { PartA = value; }
        }

        [StringLength(50, ErrorMessage = "乙方不能超过50个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPartB
        {
            get { return PartB; }
            set { PartB = value; }
        }

        [Required(ErrorMessage = "合同编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPcNum
        {
            get { return PcNum; }
            set { PcNum = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPname
        {
            get { return Pname; }
            set { Pname = value; }
        }

        [Display(Name = "合同额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrPcAmount
        {
            get { return PcAmount; }
            set { PcAmount = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPcType
        {
            get { return PcType; }
            set { PcType = value; }
        }

        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
        }

        public int StrState
        {
            get { return State; }
            set { State = value; }
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
