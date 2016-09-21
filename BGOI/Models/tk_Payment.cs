using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_Payment
    {
        private string PayID;
        private string QID;
        private string PayUnit;
        private string PayPerson;
        private decimal? PayMount;
        private string PayDate;
        private string Comments;
        private string Validate;
        private string CreateUser;
        private DateTime? CreateTime;

        [Required(ErrorMessage = "缴费单号不能为空")]
        [StringLength(20, ErrorMessage = "缴费单号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayID", "nvarchar")]
        public string StrPayID
        {
            get { return PayID; }
            set { PayID = value; }
        }

        [DataFieldAttribute("QID", "nvarchar")]
        public string StrQID
        {
            get { return QID; }
            set { QID = value; }
        }

        [Required(ErrorMessage = "缴费单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayUnit", "nvarchar")]
        public string StrPayUnit
        {
            get { return PayUnit; }
            set { PayUnit = value; }
        }

        [Required(ErrorMessage = "缴费人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayPerson", "nvarchar")]
        public string StrPayPerson
        {
            get { return PayPerson; }
            set { PayPerson = value; }
        }

        [Required(ErrorMessage = "缴费金额不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayMount", "decimal")]
        public decimal? StrPayMount
        {
            get { return PayMount; }
            set { PayMount = value; }
        }

        [Required(ErrorMessage = "缴费时间不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayDate", "nvarchar")]
        public string StrPayDate
        {
            get { return PayDate; }
            set { PayDate = value; }
        }

        [DataFieldAttribute("Comments", "text")]
        public string StrComments
        {
            get { return Comments; }
            set { Comments = value; }
        }

        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("Validate", "char")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}