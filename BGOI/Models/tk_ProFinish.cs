using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProFinish
    {
        private string PID;
        private string FinishDate;
        private int IsDebt;
        private decimal DebtAmount;
        private string DebtReason;
        private string Remark;
        private string UnitID;
        private DateTime? CreateTime;
        private string CreatePerson;
        private string Validate;

        [DataFieldAttribute("PID", "varchar")]
        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [Required(ErrorMessage = "结项日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishDate", "date")]
        public string StrFinishDate
        {
            get { return FinishDate; }
            set { FinishDate = value; }
        }

        [DataFieldAttribute("IsDebt", "int")]
        public int StrIsDebt
        {
            get { return IsDebt; }
            set { IsDebt = value; }
        }

        [Display(Name = "欠款金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [DataFieldAttribute("DebtAmount", "decimal")]
        public decimal StrDebtAmount
        {
            get { return DebtAmount; }
            set { DebtAmount = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebtReason", "nvarchar")]
        public string StrDebtReason
        {
            get { return DebtReason; }
            set { DebtReason = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }
        [DataFieldAttribute("UnitID", "nvarchar")]
        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreatePerson", "varchar")]
        public string StrCreatePerson
        {
            get { return CreatePerson; }
            set { CreatePerson = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
